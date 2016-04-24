
Partial Class Admin_Default
    Inherits System.Web.UI.Page

    Protected Sub frmGral_Load(sender As Object, e As System.EventArgs) Handles frmGral.Load
        If Session("Usuario") = "" And Session("Rol") <> "Administrador" Then
            Session("Pagina") = "Admin/Default.aspx"
            Response.Redirect("../Login.aspx")
        End If
    End Sub
End Class
