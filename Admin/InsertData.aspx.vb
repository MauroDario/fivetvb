Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class Admin_InsertData
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(sender As Object, e As System.EventArgs) Handles form1.Load
        If Session("Usuario") = "" And Session("Rol") <> "Administrador" Then
            Session("Pagina") = "Admin/Upload.aspx"
            Response.Redirect("../Login.aspx")
        ElseIf Session("FileNameXLS") = "" Then
            Session("Pagina") = "Admin/Upload.aspx"
        End If
    End Sub

    Protected Sub UploadButton_Click(sender As Object, e As System.EventArgs) Handles UploadButton.Click
        ImportToDB()
        ImportCamada()
        ImportAcronimo()
        ImportPelaje()
        ImportPrefijo()
        UpdateSexo()
        InsertCaballos()
        ''CreateFolder()
        DeleteImportacion()
    End Sub

    Sub ImportToDB()
        Dim cnt As Integer = 0
        Dim oConnXLS As New OleDbConnection
        Dim oCommXLS As New OleDbCommand

        Dim oConnDB As New OleDbConnection
        Dim oCommDB As New OleDbCommand

        oConnXLS.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("Files/" & Session("FileNameXLS") & ";Extended Properties='Excel 8.0;HDR=No;IMEX=1'")
        oConnDB.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oCommDB.Connection = oConnDB

        oCommXLS.Connection = oConnXLS
        oCommXLS.CommandText = "SELECT * FROM [Sheet1$]"

        oConnXLS.Open()

        Dim oDR As OleDbDataReader = oCommXLS.ExecuteReader()

        Do While oDR.Read()
            If cnt > 0 Then
                oCommDB.CommandText = "INSERT INTO Importacion VALUES (@Nombre, @RP, @Camada, @Pelo, @Prefijo, @Lugar, @FecNac, @Padre, @Madre, @Sexo)"
                oCommDB.Parameters.AddWithValue("Nombre", oDR.Item(0))
                oCommDB.Parameters.AddWithValue("RP", oDR.Item(1))
                oCommDB.Parameters.AddWithValue("Camada", oDR.Item(2))
                oCommDB.Parameters.AddWithValue("Pelo", oDR.Item(3))
                oCommDB.Parameters.AddWithValue("Prefijo", oDR.Item(4))
                oCommDB.Parameters.AddWithValue("Lugar", oDR.Item(5))
                oCommDB.Parameters.AddWithValue("FecNac", oDR.Item(6))
                oCommDB.Parameters.AddWithValue("Padre", oDR.Item(7))
                oCommDB.Parameters.AddWithValue("Madre", oDR.Item(8))
                oCommDB.Parameters.AddWithValue("Sexo", oDR.Item(9))

                oConnDB.Open()
                oCommDB.ExecuteNonQuery()
                oConnDB.Close()

                oCommDB.Parameters.Clear()
            End If
            cnt += 1
        Loop

        oDR.Close()
        lblImporacion.Text = "Importacion de registros: OK"
    End Sub

    Sub ImportCamada()
        Dim oConnDB As New OleDbConnection
        Dim oCommDB As New OleDbCommand
        Dim strQuery As String

        oConnDB.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oCommDB.Connection = oConnDB

        strQuery = "INSERT INTO TempCamada (camada) "
        strQuery += "SELECT DISTINCT Importacion.Camada "
        strQuery += "FROM Importacion "
        strQuery += "WHERE Importacion.[Camada]<>''"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        strQuery = "INSERT INTO Camada (camada) "
        strQuery += "Select camada "
        strQuery += "FROM TempCamada "
        strQuery += "WHERE camada NOT IN (SELECT camada FROM Camada)"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        strQuery = "DELETE FROM TempCamada"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        lblCamada.Text = "Importacion a tabla Camada: OK"
    End Sub

    Sub ImportAcronimo()
        Dim oConnDB As New OleDbConnection
        Dim oCommDB As New OleDbCommand
        Dim strQuery As String

        oConnDB.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oCommDB.Connection = oConnDB

        strQuery = "INSERT INTO TempAcronimo (acronimo) "
        strQuery += "SELECT DISTINCT Importacion.Lugar "
        strQuery += "FROM Importacion "
        strQuery += "WHERE Importacion.[Lugar]<>''"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        strQuery = "INSERT INTO Ubicacion (acronimo, nombre) "
        strQuery += "Select acronimo, acronimo as nombre "
        strQuery += "FROM TempAcronimo "
        strQuery += "WHERE acronimo NOT IN (SELECT acronimo FROM Ubicacion)"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        strQuery = "DELETE FROM TempAcronimo"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        lblCamada.Text = "Importacion a tabla Acronimo: OK"
    End Sub

    Sub ImportPelaje()
        Dim oConnDB As New OleDbConnection
        Dim oCommDB As New OleDbCommand
        Dim strQuery As String

        oConnDB.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oCommDB.Connection = oConnDB

        strQuery = "INSERT INTO TempPelaje (Pelaje) "
        strQuery += "SELECT DISTINCT Importacion.Pelo "
        strQuery += "FROM Importacion "
        strQuery += "WHERE Importacion.[Pelo]<>''"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        strQuery = "INSERT INTO Pelaje (pelaje) "
        strQuery += "Select pelaje "
        strQuery += "FROM TempPelaje "
        strQuery += "WHERE pelaje NOT IN (SELECT pelaje FROM Pelaje)"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        strQuery = "DELETE FROM TempPelaje"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        lblPelaje.Text = "Importacion a tabla Pelaje: OK"
    End Sub

    Sub ImportPrefijo()
        Dim oConnDB As New OleDbConnection
        Dim oCommDB As New OleDbCommand
        Dim strQuery As String

        oConnDB.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oCommDB.Connection = oConnDB

        strQuery = "INSERT INTO TempPrefijo (prefijo) "
        strQuery += "SELECT DISTINCT Importacion.Prefijo "
        strQuery += "FROM Importacion "
        strQuery += "WHERE Importacion.[Prefijo]<>''"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        strQuery = "INSERT INTO Prefijos (prefijo) "
        strQuery += "Select prefijo "
        strQuery += "FROM TempPrefijo "
        strQuery += "WHERE prefijo NOT IN (SELECT prefijo FROM Prefijos)"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        strQuery = "DELETE FROM TempPrefijo"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        lblPrefijo.Text = "Importacion a tabla Prefijo: OK"
    End Sub

    Sub UpdateSexo()
        Dim oConnDB As New OleDbConnection
        Dim oCommDB As New OleDbCommand
        Dim strQuery As String

        oConnDB.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oCommDB.Connection = oConnDB

        strQuery = "UPDATE Importacion SET Sexo = 'Hembra' "
        strQuery += "WHERE Sexo = 'H'"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        strQuery = "UPDATE Importacion SET Sexo = 'Entero' "
        strQuery += "WHERE Sexo = 'M'"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()

        lblSexo.Text = "Normalizacion de Columna Sexo: OK"
    End Sub

    Sub InsertCaballos()
        Dim oConnDBI As New OleDbConnection
        Dim oConnDBC As New OleDbConnection
        Dim oConnDBT As New OleDbConnection
        Dim oCommDB As New OleDbCommand
        Dim oCommImport As New OleDbCommand
        Dim oCommDBT As New OleDbCommand
        Dim oDAT As New OleDbDataAdapter
        Dim oDST As New DataSet
        Dim strQuery As String
        Dim strQueryT As String
        'Dim Flag As Boolean

        oConnDBI.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oConnDBC.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oConnDBT.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oCommDB.Connection = oConnDBC
        oCommImport.Connection = oConnDBI
        oCommDBT.Connection = oConnDBT

        strQuery = "SELECT Importacion.Prefijo, Importacion.Nombre, Importacion.Padre, Importacion.Madre, Importacion.camada, Importacion.Lugar, Importacion.RP, Sexo.id_sexo, Pelaje.id_pelaje "
        strQuery += "FROM (((Importacion INNER JOIN Camada ON Importacion.Camada = Camada.camada) INNER JOIN Ubicacion ON Importacion.Lugar = Ubicacion.acronimo) INNER JOIN Sexo ON Importacion.Sexo = Sexo.sexo) INNER JOIN Pelaje ON Importacion.Pelo = Pelaje.pelaje"

        oCommImport.CommandText = strQuery

        oConnDBI.Open()

        Dim oDR As OleDbDataReader = oCommImport.ExecuteReader()

        Do While oDR.Read()

            strQueryT = "SELECT * FROM Caballos WHERE RP='" & oDR.Item(6) & "' AND Prefijo='" & oDR.Item(0) & "' AND Nombre='" & oDR.Item(1).ToString.Replace("'", "") & "'"

            oCommDBT.CommandText = strQueryT
            oDAT.SelectCommand = oCommDBT
            oDAT.Fill(oDST, "Temporal")

            If oDST.Tables("Temporal").Rows.Count > 0 Then
                If oDST.Tables("Temporal").Rows(0).Item(6).ToString = oDR.Item(5) Then
                    'no hago nada
                Else
                    oCommDB.CommandText = "UPDATE Caballos SET Acronimo = '" & oDR.Item(5) & "' WHERE RP='" & oDR.Item(6) & "' AND Prefijo='" & oDR.Item(0) & "' AND Nombre='" & oDR.Item(1) & "'"

                    oConnDBC.Open()
                    oCommDB.ExecuteNonQuery()
                    oConnDBC.Close()
                End If
            Else
                oCommDB.CommandText = "INSERT INTO Caballos (Prefijo, Nombre, Padre, Madre, Camada, Acronimo, RP, id_Sexo, id_Pelaje) VALUES (@Prefijo, @Nombre, @Padre, @Madre, @Camada, @Acronimo, @RP, @Sexo, @Pelaje)"
                oCommDB.Parameters.AddWithValue("Prefijo", oDR.Item(0))
                oCommDB.Parameters.AddWithValue("Nombre", oDR.Item(1))
                oCommDB.Parameters.AddWithValue("Padre", oDR.Item(2))
                oCommDB.Parameters.AddWithValue("Madre", oDR.Item(3))
                oCommDB.Parameters.AddWithValue("Camada", oDR.Item(4))
                oCommDB.Parameters.AddWithValue("Acronimo", oDR.Item(5))
                oCommDB.Parameters.AddWithValue("RP", oDR.Item(6))
                oCommDB.Parameters.AddWithValue("Sexo", oDR.Item(7))
                oCommDB.Parameters.AddWithValue("Pelaje", oDR.Item(8))

                oConnDBC.Open()
                oCommDB.ExecuteNonQuery()
                oConnDBC.Close()

                CreateFolder()

                oCommDB.Parameters.Clear()
            End If


            oDST.Clear()


        Loop
        oConnDBI.Close()

        lblInsert.Text = "Insercion de Datos: OK"
        lblFolder.Text = "Creacion de Folders: OK"
    End Sub

    Sub DeleteImportacion()
        Dim oConnDB As New OleDbConnection
        Dim oCommDB As New OleDbCommand
        Dim strQuery As String

        oConnDB.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oCommDB.Connection = oConnDB

        strQuery = "DELETE FROM Importacion"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        oCommDB.ExecuteNonQuery()
        oConnDB.Close()
    End Sub

    Sub CreateFolder()
        Dim oConnDB As New OleDbConnection
        Dim oCommDB As New OleDbCommand
        Dim strQuery As String
        Dim intID As Integer

        oConnDB.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oCommDB.Connection = oConnDB

        strQuery = "SELECT TOP 1 Caballos.id_caballo "
        strQuery += "FROM Caballos "
        strQuery += "ORDER BY Caballos.id_caballo DESC"
        oCommDB.CommandText = strQuery

        oConnDB.Open()
        Dim oDR As OleDbDataReader = oCommDB.ExecuteReader()

        Do While oDR.Read()
            intID = oDR(0)
        Loop

        oConnDB.Close()

        Dim PathImages As DirectoryInfo = New DirectoryInfo(Context.Server.MapPath("../Images/" & intID))
        Dim PathDocs As DirectoryInfo = New DirectoryInfo(Context.Server.MapPath("../Images/" & intID & "/Documents"))
        If Not PathImages.Exists Then
            PathImages.Create()
            PathDocs.Create()
        End If
    End Sub

End Class
