Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class Compleja
    Inherits System.Web.UI.Page

    Dim oConn As New OleDbConnection
    Dim oComm As New OleDbCommand
    Dim strConn As String
    Dim strTable As String
    Dim Flag As Boolean = False

    Protected Sub Buscar_Click(sender As Object, e As System.EventArgs) Handles Buscar.Click
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Caballos.id_caballo, Caballos.prefijo, Caballos.nombre,  Caballos.rp, Ubicacion.nombre, Caballos.padre, Caballos.madre, Caballos.camada, Sexo.sexo, Pelaje.pelaje "
        strConn += "FROM Pelaje INNER JOIN (Sexo INNER JOIN (Ubicacion INNER JOIN Caballos ON Ubicacion.acronimo = Caballos.acronimo) ON Sexo.id_sexo = Caballos.id_sexo) ON Pelaje.id_pelaje = Caballos.id_pelaje "
        strConn += "WHERE Caballos.nombre Like '%" & txtNombre.Text & "%' "
        strConn += "AND Caballos.prefijo Like '%" & txtPrefijo.Text & "%' "
        strConn += IIf(Not (String.IsNullOrEmpty(txtRP.Text)), "AND Caballos.rp = '" & txtRP.Text & "' ", "")
        strConn += "AND Ubicacion.nombre Like '%" & ddUbicacion.SelectedValue & "%' "
        strConn += "AND Caballos.padre Like '%" & txtPadre.Text & "%' "
        strConn += "AND Caballos.madre Like '%" & txtMadre.Text & "%' "
        strConn += "AND Caballos.camada Like '%" & txtCamada.Text & "%' "
        strConn += "AND Sexo.sexo Like '%" & ddSexo.SelectedValue & "%' "
        strConn += "AND Pelaje.pelaje Like '%" & ddPelaje.SelectedValue & "%'"

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()

        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        strTable = "<table class='table table-hover'>"
        strTable += "<tr><td>#</td><td>Prefijo</td><td>Nombre</td><td>RP</td><td>Ubicacion</td><td>Padre</td><td>Madre</td><td>Camada</td><td>Sexo</td><td>Pelaje</td></tr>"

        Do While oDR.Read()
            strTable += "<tr onclick=""location.href='Ficha.aspx?ID=" & oDR.Item(0) & "'""><td>" & oDR.Item(0) & "</td><td>" & oDR.Item(1) & "</td><td>" & oDR.Item(2) & "</td><td>" & oDR.Item(3) & "</td><td>" & oDR.Item(4) & "</td><td>" & oDR.Item(5) & "</td><td>" & oDR.Item(6) & "</td><td>" & oDR.Item(7) & "</td><td>" & oDR.Item(8) & "</td><td>" & oDR.Item(9) & "</td></a></tr>"
            Flag = True
        Loop
        strTable += "</table>"

        Tabla.Text = strTable

        oDR.Close()
        oConn.Close()

        If Flag = False Then
            lblDatos.Text = "No hay datos"
        Else
            ContarEjemplares()
        End If

    End Sub

    Sub ContarEjemplares()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT COUNT(Caballos.id_caballo) "
        strConn += "FROM Pelaje INNER JOIN (Sexo INNER JOIN (Ubicacion INNER JOIN Caballos ON Ubicacion.acronimo = Caballos.acronimo) ON Sexo.id_sexo = Caballos.id_sexo) ON Pelaje.id_pelaje = Caballos.id_pelaje "
        strConn += "WHERE Caballos.nombre Like '%" & txtNombre.Text & "%' "
        strConn += "AND Caballos.prefijo Like '%" & txtPrefijo.Text & "%' "
        strConn += IIf(Not (String.IsNullOrEmpty(txtRP.Text)), "AND Caballos.rp = '" & txtRP.Text & "' ", "")
        strConn += "AND Ubicacion.nombre Like '%" & ddUbicacion.SelectedValue & "%' "
        strConn += "AND Caballos.padre Like '%" & txtPadre.Text & "%' "
        strConn += "AND Caballos.madre Like '%" & txtMadre.Text & "%' "
        strConn += "AND Caballos.camada Like '%" & txtCamada.Text & "%' "
        strConn += "AND Sexo.sexo Like '%" & ddSexo.SelectedValue & "%' "
        strConn += "AND Pelaje.pelaje Like '%" & ddPelaje.SelectedValue & "%'"

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()

        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        Do While oDR.Read()
            lblDatos.Text = oDR(0) & " Datos encontrados"
        Loop

        oDR.Close()
        oConn.Close()

    End Sub


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            CargarRP()
            CargarSexo()
            CargarPelaje()
        End If

        Session("Pagina") = "Compleja.aspx"

        If Session("Usuario") = "" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub

    Sub CargarRP()
        Dim oDA As New OleDbDataAdapter
        Dim oDS As New DataSet

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Nombre FROM Ubicacion Order BY Nombre ASC"

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oDA.SelectCommand = oComm
        oDA.Fill(oDS)

        oConn.Open()

        Dim oDR As OleDbDataReader = oComm.ExecuteReader()
        ddUbicacion.DataSource = oDS
        ddUbicacion.DataTextField = "Nombre"
        ddUbicacion.DataBind()

        oDR.Close()
        oConn.Close()

        ddUbicacion.Items.Insert(0, "")

    End Sub

    Sub CargarSexo()
        Dim oDA As New OleDbDataAdapter
        Dim oDS As New DataSet

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Sexo FROM Sexo Order BY Sexo ASC"

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oDA.SelectCommand = oComm
        oDA.Fill(oDS)

        oConn.Open()

        Dim oDR As OleDbDataReader = oComm.ExecuteReader()
        ddSexo.DataSource = oDS
        ddSexo.DataTextField = "Sexo"
        ddSexo.DataBind()

        oDR.Close()
        oConn.Close()

        ddSexo.Items.Insert(0, "")

    End Sub

    Sub CargarPelaje()
        Dim oDA As New OleDbDataAdapter
        Dim oDS As New DataSet

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Pelaje FROM Pelaje Order BY Pelaje ASC"

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oDA.SelectCommand = oComm
        oDA.Fill(oDS)

        oConn.Open()

        Dim oDR As OleDbDataReader = oComm.ExecuteReader()
        ddPelaje.DataSource = oDS
        ddPelaje.DataTextField = "Pelaje"
        ddPelaje.DataBind()

        oDR.Close()
        oConn.Close()

        ddPelaje.Items.Insert(0, "")

    End Sub

End Class
