
Partial Class LogOut
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Session("Usuario") = ""
        Response.Redirect("Default.aspx")
    End Sub
End Class
