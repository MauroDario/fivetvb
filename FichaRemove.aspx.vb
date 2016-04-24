Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class upload
    Inherits System.Web.UI.Page

    Dim IdFile As Integer
    Dim FileName As String


    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session("Usuario") = "" Then
            Session("Pagina") = "Simple.aspx"
            Response.Redirect("Login.aspx")
        Else
            IdFile = CInt(Request.QueryString("IdFile"))
            FileName = Request.QueryString("FileName")

            DeleteFiles()
            DeleteRow(IdFile)

            Response.Redirect("Ficha.aspx?ID=" & Session("ID"))
        End If
    End Sub

    Sub DeleteFiles()
        Dim PathName As String = Server.MapPath("~/Images/" & Session("ID") & "/Documents/" & FileName)

        If System.IO.File.Exists(PathName) = True Then
            System.IO.File.Delete(PathName)
        End If
    End Sub

    Sub DeleteRow(ByVal pIdFileName As String)
        Dim oConn As New OleDbConnection
        Dim oComm As New OleDbCommand

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("App_Data/Haras.mdb")
        oComm.Connection = oConn

        oComm.CommandText = "DELETE * FROM Files WHERE ID_Files = @file"
        oComm.Parameters.AddWithValue("file", pIdFileName)

        oConn.Open()
        oComm.ExecuteNonQuery()
        oConn.Close()

        oConn.Open()
        oComm.CommandText = "DELETE * FROM files_comment WHERE id_files = @file"
        oComm.Parameters.AddWithValue("file", pIdFileName)
        oComm.ExecuteNonQuery()
        oConn.Close()
    End Sub

End Class
