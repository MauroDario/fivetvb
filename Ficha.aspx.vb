Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class Ficha
    Inherits System.Web.UI.Page

    Dim ID As Integer
    Dim oConn As New OleDbConnection
    Dim oComm As New OleDbCommand
    Dim oDA As New OleDbDataAdapter
    Dim oDS As New DataSet
    Dim strConn As String
    Dim Flag As Boolean = False

    Sub Buscar(ByVal pID As String)

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Caballos.prefijo, Caballos.nombre, Caballos.padre, Caballos.madre, Camada.camada, Caballos.rp, Ubicacion.nombre "
        strConn += "FROM Ubicacion INNER JOIN (Camada INNER JOIN Caballos ON Camada.camada = Caballos.camada) ON Ubicacion.acronimo = Caballos.acronimo "
        strConn += "WHERE Caballos.id_caballo=" & pID

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()
        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        Do While oDR.Read()
            txtNombre.Text = oDR.Item(0).ToString & " " & oDR.Item(1).ToString
            txtPadre.Text = oDR.Item(2).ToString
            txtMadre.Text = oDR.Item(3).ToString
            txtCamada.Text = oDR.Item(4).ToString
            txtRP.Text = oDR.Item(5).ToString
            txtUbicacion.Text = oDR.Item(6).ToString

            Flag = True
        Loop

        oDR.Close()
        oConn.Close()

        If Flag = False Then
            If BuscarConCamada(pID) = False Then
                If BuscarUbicacion(pID) = False Then
                    If BuscarDatosBasicos(pID) = False Then
                        txtDescripcion.Text = "No hay datos disponibles"
                    End If
                End If
            End If
        Else
            CargarComentarios(pID)
            VerFiles(pID)
        End If

    End Sub

    Function BuscarConCamada(ByVal pID As Integer) As Boolean
        Flag = False

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Caballos.prefijo, Caballos.nombre, Caballos.padre, Caballos.madre, Caballos.rp "
        strConn += "FROM Caballos INNER JOIN Camada ON Caballos.camada = Camada.camada "
        strConn += "WHERE Caballos.id_caballo =" & pID

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()
        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        Do While oDR.Read()
            txtNombre.Text = oDR.Item(0).ToString & " " & oDR.Item(1).ToString
            txtPadre.Text = oDR.Item(2).ToString
            txtMadre.Text = oDR.Item(3).ToString
            txtCamada.Text = oDR.Item(4).ToString
            txtUbicacion.Text = oDR.Item(5).ToString

            Flag = True
        Loop

        oDR.Close()
        oConn.Close()

        Return Flag

    End Function

    Function BuscarUbicacion(ByVal pID As Integer) As Boolean
        Flag = False

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Caballos.prefijo, Caballos.nombre, Caballos.padre, Caballos.madre, Caballos.rp "
        strConn += "FROM Caballos INNER JOIN Ubicacion ON Caballos.acronimo = Ubicacion.acronimo "
        strConn += "WHERE Caballos.id_caballo=" & pID

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()
        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        Do While oDR.Read()
            txtNombre.Text = oDR.Item(0).ToString & " " & oDR.Item(1).ToString
            txtPadre.Text = oDR.Item(2).ToString
            txtMadre.Text = oDR.Item(3).ToString
            txtRP.Text = oDR.Item(4).ToString
            txtUbicacion.Text = oDR.Item(5).ToString

            Flag = True
        Loop

        oDR.Close()
        oConn.Close()

        Return Flag

    End Function

    Function BuscarDatosBasicos(ByVal pID As Integer) As Boolean
        Flag = False

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Caballos.prefijo, Caballos.nombre, Caballos.padre, Caballos.madre, Caballos.rp "
        strConn += "FROM Caballos "
        strConn += "WHERE Caballos.id_caballo=" & pID

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()
        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        Do While oDR.Read()
            txtNombre.Text = oDR.Item(0).ToString & " " & oDR.Item(1).ToString
            txtPadre.Text = oDR.Item(2).ToString
            txtMadre.Text = oDR.Item(3).ToString
            txtRP.Text = oDR.Item(4).ToString

            Flag = True
        Loop

        oDR.Close()
        oConn.Close()

        Return Flag

    End Function


    Sub CargarComentarios(ByVal pID As Integer)
        Dim Files As String = String.Empty
        Dim oIcon As String = String.Empty

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT TOP 1 historias.id_historia, Caballos.nombre, Usuarios.Usuario, historias.fecha, historias.fecha_evento, historias.descripcion "
        strConn += "FROM Usuarios INNER JOIN (Caballos INNER JOIN historias ON Caballos.id_caballo = historias.id_caballo) ON Usuarios.id_usuario = historias.id_usuario "
        strConn += "WHERE historias.id_caballo = " & pID
        strConn += " ORDER BY historias.id_historia DESC"

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()
        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        Dim dateValue As String
        Dim idComment As String = String.Empty
        Do While oDR.Read()
            idComment = oDR.Item(0).ToString()
            If String.IsNullOrEmpty(oDR.Item(4).ToString()) Then
                dateValue = DateTime.Parse(oDR.Item(3)).ToString("dd/MM/yyyy")
                lblFecha.InnerText = "Fecha de Comentario"
            Else
                dateValue = DateTime.Parse(oDR.Item(4)).ToString("dd/MM/yyyy")
                lblFecha.InnerText = "Fecha de Evento"
            End If

            lblUsuario.Text = oDR.Item(2).ToString
            txtFecha.Text = dateValue
            txtDescripcion.Text = oDR.Item(5)
            'idComentario.Text = oDR.Item(0)
            Flag = True
        Loop

        oDR.Close()
        oConn.Close()


        If (Not String.IsNullOrEmpty(idComment)) Then
            TablaCommentFiles.Text = String.Empty

            strConn = "SELECT Files.id_Files, Files.Nombre, Files.Fecha "
            strConn += "FROM files_comment INNER JOIN Files ON ( files_comment.id_files = Files.id_files ) "
            strConn += "WHERE files_comment.id_comment = " & idComment

            oComm.Connection = oConn
            oComm.CommandText = strConn

            oConn.Open()
            oDR = oComm.ExecuteReader()

            Files = "<table>"
            Files += "<tr><td>File</td><td>Fecha</td><td></td></tr>"
            Do While oDR.Read()
                Dim extension As String = Extraer("Images\" & pID & "\Documents\" & oDR.Item(1).ToString, ".")

                Select Case extension
                    Case "txt", "doc", "docx", "xls", "xlsx", "pdf"
                        oIcon = "glyphicon glyphicon-book"
                    Case "gif", "jpg", "jpeg", "png"
                        oIcon = "glyphicon glyphicon-picture"
                    Case "mov", "avi", "mp4"
                        oIcon = "glyphicon glyphicon-film"
                    Case "zip", "rar"
                        oIcon = "glyphicon glyphicon-compressed"
                    Case Else
                        oIcon = "glyphicon glyphicon-paperclip"
                End Select

                Files += "<tr>"
                Files += "<td><span class='" & oIcon & "'></span>&nbsp;<a href='Images\" & pID & "\Documents\" & oDR.Item(1).ToString & "'>" & oDR.Item(1).ToString & "</a></td>"
                Files += "<td>" & oDR.Item(2).ToString & "</td><td><a class='btn btn-default btn-xs' href='FichaRemoveAsoc.aspx?IdFile=" & oDR.Item(0).ToString & "&idComment=" & idComment & "'><span class='glyphicon glyphicon-remove'></span></a></td>"
                Files += "</tr>"

            Loop
            Files += "</table>"

            TablaCommentFiles.Text = Files

            oDR.Close()
            oConn.Close()
        End If

    End Sub

    Sub CargarTablaComentarios(ByVal pID As Integer)

        Dim strTable As String

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT historias.id_historia, Caballos.nombre, Usuarios.Usuario, historias.fecha, historias.fecha_evento, historias.descripcion  "
        strConn += "FROM Usuarios INNER JOIN (Caballos INNER JOIN historias ON Caballos.id_caballo = historias.id_caballo) ON Usuarios.id_usuario = historias.id_usuario "
        strConn += "WHERE historias.id_caballo = " & pID
        strConn += " ORDER BY historias.id_historia DESC"

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()

        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        strTable = "<table class='table table-hover'>"
        strTable += "<tr><td>Usuario</td><td>Fecha</td><td>Descripcion</td><td></td><td></td></tr>"

        Dim dateValue As String

        Do While oDR.Read()

            If String.IsNullOrEmpty(oDR.Item(4).ToString()) Then
                dateValue = DateTime.Parse(oDR.Item(3)).ToString("dd/MM/yyyy") & "*"
            Else
                dateValue = DateTime.Parse(oDR.Item(4)).ToString("dd/MM/yyyy")
            End If

            strTable += "<tr onclick=""location.href='Ficha.aspx?ID=" & ID & "&Detalle=" & oDR.Item(0) & "&action=ver'""><td>" & oDR.Item(2) & "</td><td>" & dateValue & "</td><td>" & Mid(oDR.Item(5), 1, 30) & "...</td>"


            'strTable += "<tr><td>"

            'strTable += "<a href='Ficha.aspx?ID=" & ID & "&Detalle=" & oDR.Item(0) & "&action=ver' class='btn btn-info' role='button'><span class='glyphicon glyphicon-search'></span></a></td>"
            'strTable += "<td>" & oDR.Item(1) & "</td><td>" & oDR.Item(2) & "</td><td>" & Mid(oDR.Item(3), 1, 10) & "</td><td>" & Mid(oDR.Item(4), 1, 30) & "...</td>"

            strTable += "<td>"
            If oDR.Item(2) <> Session("Usuario") Then
                strTable += "<a href='Ficha.aspx?ID=" & ID & "&Detalle=" & oDR.Item(0) & "&action=borrar' class='btn btn-danger' role='button' disabled='disabled'><span class='glyphicon glyphicon-remove'></span></a>&nbsp;"
            Else
                strTable += "<a href='Ficha.aspx?ID=" & ID & "&Detalle=" & oDR.Item(0) & "&action=borrar' class='btn btn-danger' role='button'><span class='glyphicon glyphicon-remove'></span></a>&nbsp;"
            End If
            strTable += "</td>"
            strTable += "<td>"
            strTable += "<a href = 'Upload.aspx?id=" & Session("ID") & "&CommentId=" & oDR.Item(0) & "' Class='btn btn-primary'>Asociar Documentos</a>"
            strTable += "</td>"
            strTable += "<td>"
            'If (IsDBNull(oDR.Item(5))) Then
            '    strTable += "</td>"
            'Else
            '    Dim filePath As String = Server.MapPath("~/Images/" & ID & "/Documents/" & oDR.Item(5))
            '    If (IO.File.Exists(filePath)) Then
            '        Dim path As String = "Images/" & ID & "/Documents/" & oDR.Item(5)
            '        strTable += "<a href = '" & path & "' Class='btn btn-primary'>Ver imagen</a>"
            '        strTable += "</td>"
            '    End If
            'End If


            strTable += "</tr>"


            Flag = True
        Loop
        strTable += "</table>"

        Tabla.Text = strTable

        oDR.Close()
        oConn.Close()
    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        ID = CInt(Request.QueryString("ID"))

        If ID = Nothing Or Session("Usuario") = "" Then
            Session("Pagina") = "Simple.aspx"
            Response.Redirect("Login.aspx")
        Else
            Session("ID") = ID
            imgCaballo.ImageUrl = "~/Images/" & Session("ID") & "/Default.jpg"
        End If


        If Not Page.IsPostBack Then
            Buscar(ID)
            CargarTablaComentarios(ID)

            If Request.QueryString("Action") = "ver" Then
                VerDatos(Request.QueryString("Detalle"))
            ElseIf Request.QueryString("Action") = "borrar" Then
                BorrarDatos(Request.QueryString("Detalle"))
            End If

        End If

    End Sub


    Protected Sub BorrarComentario_Click(sender As Object, e As System.EventArgs) Handles BorrarComentario.Click
        lblUsuario.Text = Session("Usuario")
        lblFecha.InnerText = "Fecha de Evento"
        txtFecha.Text = ""
        txtFecha.ReadOnly = False
        GuardarFecha.Visible = False
        EditarFecha.Visible = False
        txtDescripcion.Text = ""
    End Sub


    Protected Sub NuevoComentario_Click(sender As Object, e As System.EventArgs) Handles NuevoComentario.Click
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        oConn.Open()

        strConn = "INSERT INTO historias ( id_usuario, id_caballo, fecha, descripcion ) "
        strConn += "VALUES (" & Session("Id_Usuario") & "," & ID & ",'" & DateTime.Now.ToString() & "','" & txtDescripcion.Text & "') "

        oComm.Connection = oConn
        oComm.CommandText = strConn
        oComm.ExecuteNonQuery()

        If Not (String.IsNullOrEmpty(txtFecha.Text)) And txtFecha.ReadOnly = False Then

            strConn = "SELECT MAX(id_historia) FROM historias"
            oComm.CommandText = strConn
            Dim idComentarioAux As String = oComm.ExecuteScalar()

            strConn = "UPDATE historias SET fecha_evento = '" & txtFecha.Text & "' WHERE id_historia = " & idComentarioAux

            oComm.CommandText = strConn
            oComm.ExecuteNonQuery()

            txtFecha.ReadOnly = True
            EditarFecha.Visible = True
        End If

        oConn.Close()

        CargarTablaComentarios(ID)

    End Sub

    Sub VerDatos(ByVal pID As Integer)
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Usuarios.Usuario, historias.fecha, historias.fecha_evento, historias.descripcion "
        strConn += "FROM Usuarios INNER JOIN historias ON Usuarios.id_usuario = historias.id_usuario "
        strConn += "WHERE historias.id_historia = " & pID

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()
        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        Do While oDR.Read()
            lblUsuario.Text = oDR.Item(0).ToString
            txtDescripcion.Text = oDR.Item(3).ToString
            If String.IsNullOrEmpty(oDR.Item(2).ToString()) Then
                txtFecha.Text = DateTime.Parse(oDR.Item(1)).ToString("dd/MM/yyyy")
                lblFecha.InnerText = "Fecha de Comentario"
            Else
                txtFecha.Text = DateTime.Parse(oDR.Item(2)).ToString("dd/MM/yyyy")
                lblFecha.InnerText = "Fecha de Evento"
            End If

            idComentario.Text = pID

            Flag = True
        Loop

        oDR.Close()
        oConn.Close()



    End Sub

    Sub BorrarDatos(ByVal pID As Integer)
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        oConn.Open()

        strConn = "DELETE * FROM Historias WHERE id_historia = " & pID

        oComm.Connection = oConn
        oComm.CommandText = strConn
        oComm.ExecuteNonQuery()

        oConn.Close()

        CargarComentarios(ID)
        CargarTablaComentarios(ID)

    End Sub

    Sub VerFiles(ByVal pID As Integer)
        Dim oConnFiles As New OleDbConnection

        oConnFiles.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT id_Files, Nombre, Fecha "
        strConn += "FROM Files "
        strConn += "WHERE id_caballo = " & pID

        oComm.Connection = oConnFiles
        oComm.CommandText = strConn

        oConnFiles.Open()
        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        Dim Files As String
        Dim oIcon As String

        Files = "<table>"
        Files += "<tr><td>File</td><td>Fecha</td><td></td><td></td></tr>"
        Do While oDR.Read()
            Dim extension As String = Extraer("Images\" & pID & "\Documents\" & oDR.Item(1).ToString, ".")

            Select Case extension
                Case "txt", "doc", "docx", "xls", "xlsx", "pdf"
                    oIcon = "glyphicon glyphicon-book"
                Case "gif", "jpg", "jpeg", "png"
                    oIcon = "glyphicon glyphicon-picture"
                Case "mov", "avi", "mp4"
                    oIcon = "glyphicon glyphicon-film"
                Case "zip", "rar"
                    oIcon = "glyphicon glyphicon-compressed"
                Case Else
                    oIcon = "glyphicon glyphicon-paperclip"
            End Select


            Files += "<tr>"
            Files += "<td><span class='" & oIcon & "'></span>&nbsp;<a href='Images\" & pID & "\Documents\" & oDR.Item(1).ToString & "'>" & oDR.Item(1).ToString & "</a></td>"
            Files += "<td>" & oDR.Item(2).ToString & "</td><td><a href='FichaRemove.aspx?IdFile=" & oDR.Item(0).ToString & "&FileName=" & oDR.Item(1).ToString & "'><button type='button' class='btn btn-default btn-xs'><span class='glyphicon glyphicon-remove'></span></td>"
            Files += "<td>"
            Files += "<button type='button' class='btn btn-primary btn-xs' data-toggle='modal' data-target='#myModal' data-pathFile='" & oDR.Item(1).ToString() & "' data-idFile='" & oDR.Item(0).ToString() & "'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span></button>"
            Files += "</td>"
            Files += "</tr>"

        Loop
        Files += "</table>"

        TablaDocs.Text = Files

        oDR.Close()
        oConnFiles.Close()

        'archivos de comentarios

        If (Not String.IsNullOrEmpty(Request.QueryString("Detalle"))) Then
            TablaCommentFiles.Text = String.Empty

            strConn = "SELECT Files.id_Files, Files.Nombre, Files.Fecha "
            strConn += "FROM files_comment INNER JOIN Files ON ( files_comment.id_files = Files.id_files ) "
            strConn += "WHERE files_comment.id_comment = " & Request.QueryString("Detalle").ToString()

            oComm.Connection = oConnFiles
            oComm.CommandText = strConn

            oConnFiles.Open()
            oDR = oComm.ExecuteReader()

            Files = "<table>"
            Files += "<tr><td>File</td><td>Fecha</td><td></td></tr>"
            Do While oDR.Read()
                Dim extension As String = Extraer("Images\" & pID & "\Documents\" & oDR.Item(1).ToString, ".")

                Select Case extension
                    Case "txt", "doc", "docx", "xls", "xlsx", "pdf"
                        oIcon = "glyphicon glyphicon-book"
                    Case "gif", "jpg", "jpeg", "png"
                        oIcon = "glyphicon glyphicon-picture"
                    Case "mov", "avi", "mp4"
                        oIcon = "glyphicon glyphicon-film"
                    Case "zip", "rar"
                        oIcon = "glyphicon glyphicon-compressed"
                    Case Else
                        oIcon = "glyphicon glyphicon-paperclip"
                End Select

                Files += "<tr>"
                Files += "<td><span class='" & oIcon & "'></span>&nbsp;<a href='Images\" & pID & "\Documents\" & oDR.Item(1).ToString & "'>" & oDR.Item(1).ToString & "</a></td>"
                Files += "<td>" & oDR.Item(2).ToString & "</td><td><a class='btn btn-default btn-xs' href='FichaRemoveAsoc.aspx?IdFile=" & oDR.Item(0).ToString & "&idComment=" & Request.QueryString("Detalle").ToString() & "'><span class='glyphicon glyphicon-remove'></span></a></td>"
                Files += "</tr>"

            Loop
            Files += "</table>"

            TablaCommentFiles.Text = Files

            oDR.Close()
            oConnFiles.Close()
        End If

    End Sub

    Function Extraer(Path As String, Caracter As String) As String
        Dim ret As String
        ret = Right(Path, Len(Path) - InStrRev(Path, Caracter))
        Extraer = ret
    End Function

    Protected Sub EditarFecha_Click(sender As Object, e As System.EventArgs) Handles EditarFecha.Click
        EditarFecha.Visible = False
        GuardarFecha.Visible = True
        txtFecha.ReadOnly = False
        If lblFecha.InnerText.Equals("Fecha de Comentario") Then
            txtFecha.Text = ""
            lblFecha.InnerText = "Fecha de Evento"
        End If
    End Sub

    Protected Sub GuardarFecha_Click(sender As Object, e As System.EventArgs) Handles GuardarFecha.Click
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")
        oConn.Open()

        strConn = "UPDATE historias SET fecha_evento = '" & txtFecha.Text & "' WHERE id_historia = " + idComentario.Text + ""

        oComm.Connection = oConn
        oComm.CommandText = strConn
        oComm.ExecuteNonQuery()
        oConn.Close()

        txtFecha.ReadOnly = True
        GuardarFecha.Visible = False
        EditarFecha.Visible = True

        CargarTablaComentarios(ID)
    End Sub

    Protected Sub btnRename_Click(sender As Object, e As EventArgs)
        If (Not String.IsNullOrEmpty(txtFilePath.Value) And Not String.IsNullOrEmpty(txtFileName.Text)) Then
            Try
                RenombrarArchivo()
                Response.Redirect(Request.RawUrl.TrimStart("/"))
            Catch ex As Exception
                txtFileName.Text = "Upload status: " & ex.Message
            End Try
        End If
    End Sub

    Protected Sub RenombrarArchivo()
        ' Renombrar archivo en file system
        FileSystem.Rename(Server.MapPath("~/Images/" & Session("ID") & "/Documents/") & txtFilePath.Value, Server.MapPath("~/Images/" & Session("ID") & "/Documents/") & txtFileName.Text)

        ' Renombrar archivo en base de datos
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")
        oConn.Open()

        strConn = "UPDATE Files SET Nombre = '" & txtFileName.Text & "' WHERE id_files = " + txtFileId.Value + ""

        oComm.Connection = oConn
        oComm.CommandText = strConn
        oComm.ExecuteNonQuery()
        oConn.Close()
    End Sub
End Class
