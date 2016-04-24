Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub enviar_Click(sender As Object, e As System.EventArgs) Handles enviar.Click
        Dim oConn As New OleDbConnection
        Dim oComm As New OleDbCommand
        Dim strConn As String
        Dim Flag As Boolean = False

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

        strConn = "SELECT Usuarios.Usuario, Role.role, Usuarios.id_usuario "
        strConn += "FROM Usuarios INNER JOIN (Role INNER JOIN user_role ON Role.id_role = user_role.id_role) "
        strConn += "ON Usuarios.id_usuario = user_role.id_usuario "
        strConn += "WHERE Usuarios.email='" & email.Text & "' AND Usuarios.pass='" & pass.Text & "'"

        oComm.Connection = oConn
        oComm.CommandText = strConn

        oConn.Open()

        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        Do While oDR.Read()
            Session("Usuario") = oDR.Item(0)
            Session("Rol") = oDR.Item(1)
            Session("Id_Usuario") = oDR.Item(2)
            Flag = True
        Loop

        oDR.Close()

        If Flag = True Then
            If Session("Pagina") = "" Then Session("Pagina") = "simple.aspx"
            Response.Redirect(Session("Pagina"))
        Else
            lblerror.Text = "Los datos ingresados son erroneos"
        End If


    End Sub



    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load

        If (Request.Cookies("UserSettings") IsNot Nothing) Then
            If (Request.Cookies("UserSettings")("Email") IsNot Nothing) Then
                email.Text = Request.Cookies("UserSettings")("Email")
            End If

            If (Request.Cookies("UserSettings")("Password") IsNot Nothing) Then
                pass.Text = Request.Cookies("UserSettings")("Password")
            End If
        End If

        If Page.IsPostBack Then
            If CheckBoxCookie.Checked Then
                Dim Haras As HttpCookie = New HttpCookie("UserSettings")
                Haras("Email") = email.Text
                Haras("Password") = pass.Text
                Response.Cookies.Add(Haras)
            End If
        End If


    End Sub
End Class
