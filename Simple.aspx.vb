Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class Simple
    Inherits System.Web.UI.Page
    Dim oConn As New OleDbConnection
    Dim oComm As New OleDbCommand
    Dim strConn As String
    Dim strTable As String
    Dim strUbicacion As String
    Dim Flag As Boolean = False

    Protected Sub Buscar_Click(sender As Object, e As System.EventArgs) Handles Buscar.Click
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Caballos.id_caballo, Caballos.prefijo, Caballos.nombre, Caballos.rp, Caballos.camada, Ubicacion.nombre "
        strConn += "FROM Ubicacion INNER JOIN Caballos ON Ubicacion.acronimo = Caballos.acronimo "
        strConn += "WHERE Caballos.Nombre Like '%" & txtNombre.Text & "%' "
        strConn += "AND Caballos.prefijo Like '%" & txtPrefijo.Text & "%' "
        strConn += IIf(Not (String.IsNullOrEmpty(txtRP.Text)), "AND Caballos.rp = '" & txtRP.Text & "' ", "")
        strConn += "AND Ubicacion.nombre Like '%" & DropDownList1.SelectedValue & "%' "

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()

        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        strTable = "<table class='table table-hover'>"
        strTable += "<tr><td>Prefijo</td><td>Nombre</td><td>RP</td><td>Camada</td><td>Ubicacion</td></tr>"

        Do While oDR.Read()
            strTable += "<tr onclick=""location.href='Ficha.aspx?ID=" & oDR.Item(0) & "'""><td>" & oDR.Item(1) & "</td><td>" & oDR.Item(2) & "</td><td>" & oDR.Item(3) & "</td><td>" & oDR.Item(4) & "</td><td>" & oDR.Item(5) & "</td></a></tr>"
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

        strConn = "SELECT Count(Caballos.nombre) "
        strConn += "FROM Ubicacion INNER JOIN Caballos ON Ubicacion.acronimo = Caballos.acronimo "
        strConn += "WHERE Caballos.Nombre Like '%" & txtNombre.Text & "%' "
        strConn += "AND Caballos.prefijo Like '%" & txtPrefijo.Text & "%' "
        strConn += IIf(Not (String.IsNullOrEmpty(txtRP.Text)), "AND Caballos.rp = '" & txtRP.Text & "' ", "")
        strConn += "AND Ubicacion.nombre Like '%" & DropDownList1.SelectedValue & "%' "

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
        End If

        Session("Pagina") = "Simple.aspx"

        If Session("Usuario") = "" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub


    Sub CargarRP()
        Dim oDA As New OleDbDataAdapter
        Dim oDS As New DataSet

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Acronimo, Nombre FROM Ubicacion Order BY Nombre ASC"

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oDA.SelectCommand = oComm
        oDA.Fill(oDS)

        oConn.Open()

        Dim oDR As OleDbDataReader = oComm.ExecuteReader()
        DropDownList1.DataSource = oDS
        DropDownList1.DataTextField = "Nombre"
        DropDownList1.DataBind()

        oDR.Close()
        oConn.Close()

        DropDownList1.Items.Insert(0, "")

    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles DropDownList1.SelectedIndexChanged
        strUbicacion = DropDownList1.SelectedValue
    End Sub
End Class
