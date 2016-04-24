Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class upload
    Inherits System.Web.UI.Page

    Dim IdFile As Integer
    Dim IdComment As String


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("Usuario") = "" Then
            Session("Pagina") = "Simple.aspx"
            Response.Redirect("Login.aspx")
        Else
            IdFile = CInt(Request.QueryString("IdFile"))

            IdComment = Request.QueryString("idComment")

            DeleteRow(IdFile)

            Response.Redirect("Ficha.aspx?ID=" & Session("ID"))
        End If
    End Sub
    Sub DeleteRow(ByVal idFile As String)
        Dim oConn As New OleDbConnection
        Dim oComm As New OleDbCommand

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("App_Data/Haras.mdb")
        oComm.Connection = oConn
        oConn.Open()
        oComm.CommandText = "DELETE FROM files_comment WHERE id_files = " & idFile & " AND id_comment = " & IdComment

        oComm.ExecuteNonQuery()
        oConn.Close()
    End Sub

End Class
