
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        'Session("Pagina") = "Default.aspx"
		response.redirect("Simple.aspx")
    End Sub
End Class
