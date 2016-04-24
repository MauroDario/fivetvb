Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class Voz
    Inherits System.Web.UI.Page

    Protected Sub Buscar_Click(sender As Object, e As System.EventArgs) Handles Buscar.Click
        Dim oConn As New OleDbConnection
        Dim oComm As New OleDbCommand
        Dim strConn As String
        Dim strTable As String
        Dim Flag As Boolean = False

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Caballos.id_caballo, Caballos.prefijo, Caballos.nombre, Caballos.rp, Ubicacion.nombre "
        strConn += "FROM Ubicacion INNER JOIN Caballos ON Ubicacion.acronimo = Caballos.acronimo "
        strConn += "WHERE Caballos.Nombre Like '%" & txtNombre.Text & "%' "

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()

        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        strTable = "<table class='table table-hover'>"
        strTable += "<tr><td>#</td><td>Prefijo</td><td>Nombre</td><td>RP</td><td>Ubicacion</td></tr>"

        Do While oDR.Read()
            strTable += "<tr onclick=""location.href='Ficha.aspx?ID=" & oDR.Item(0) & "'""><td>" & oDR.Item(0) & "</td><td>" & oDR.Item(1) & "</td><td>" & oDR.Item(2) & "</td><td>" & oDR.Item(3) & "</td><td>" & oDR.Item(4) & "</td></a></tr>"
            Flag = True
        Loop
        strTable += "</table>"

        Tabla.Text = strTable

        oDR.Close()

        If Flag = False Then
            lblDatos.Text = "No se encontraron datos"
        Else
            lblDatos.Text = ""
        End If

    End Sub

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Session("Pagina") = "Voz.aspx"

        If Session("Usuario") = "" Then
            Response.Redirect("Login.aspx")
        End If
    End Sub

End Class
