Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class Admin_Comentarios
    Inherits System.Web.UI.Page

    Dim oConn As New OleDbConnection
    Dim oComm As New OleDbCommand
    Dim oDA As New OleDbDataAdapter
    Dim oDS As New DataSet
    Dim strTable As String
    Dim strConn As String
    Dim Flag As Boolean

    Protected Sub frmComentarios_Load(sender As Object, e As System.EventArgs) Handles frmComentarios.Load
        If Session("Usuario") <> "" Or Session("Rol") = "Administrador" Or Session("Rol") = "Power User" Then
        Else
            Session("Pagina") = "Admin/Comentarios.aspx"
            Response.Redirect("../Login.aspx")
        End If
    End Sub

    Protected Sub btnBuscar_Click(sender As Object, e As System.EventArgs) Handles btnBuscar.Click
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("../App_Data/Haras.mdb")

        strConn = "SELECT Caballos.id_caballo, [Usuarios]![Usuario] & ' ' & Left([historias]![fecha],10) AS Usuario, Caballos.prefijo, Caballos.nombre, Caballos.rp, historias.fecha, historias.fecha_evento, historias.descripcion "
        strConn += "FROM (Usuarios INNER JOIN historias ON Usuarios.id_usuario = historias.id_usuario) INNER JOIN Caballos ON historias.id_caballo = Caballos.id_caballo "
        strConn += "WHERE historias.fecha >= #" & txtInicial.Text & "# And historias.fecha <= #" & txtFinal.Text & "# "
        strConn += "ORDER BY historias.fecha DESC"

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()

        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        strTable = "<table class='table table-hover'>"
        strTable += "<tr><td>Usuario</td><td>Prefijo</td><td>Nombre</td><td>RP</td><td>Fecha</td><td>Descripcion</td></tr>"

        Dim dateValue As String

        Do While oDR.Read()
            If String.IsNullOrEmpty(oDR.Item(6).ToString()) Then
                dateValue = oDR.Item(5).ToString() + "*"
            Else
                dateValue = oDR.Item(6).ToString()
            End If

            strTable += "<tr onclick=""location.href='../Ficha.aspx?ID=" & oDR.Item(0) & "'""><td>" & oDR.Item(1) & "</td><td>" & oDR.Item(2) & "</td><td>" & oDR.Item(3) & "</td><td>" & oDR.Item(4) & "</td><td>" & dateValue & "</td><td>" & oDR.Item(7) & "</td></tr>"
            Flag = True
        Loop
        strTable += "</table>"

        Tabla.Text = strTable

        oDR.Close()
        oConn.Close()


        If Flag = False Then
            lblDatos.Text = "No hay datos"
        End If

    End Sub
End Class
