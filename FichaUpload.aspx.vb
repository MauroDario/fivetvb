Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Partial Class upload
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(sender As Object, e As System.EventArgs) Handles form1.Load
        If Session("Usuario") = "" Then
            Session("Pagina") = "Upload.aspx"
            Response.Redirect("Login.aspx")
        End If
    End Sub

    Sub UploadButton_Click(sender As Object, e As System.EventArgs) Handles UploadButton.Click
        upload()
    End Sub

    Sub upload()
        If FileUploadControl.HasFile Then
            Try
                Dim filename As String = Path.GetFileName(FileUploadControl.FileName)
                Dim PathName As String = Server.MapPath("~/Images/" & Session("ID") & "/default.jpg")
                FileUploadControl.SaveAs(PathName)
                PathName = Server.MapPath("~/Images/" & Session("ID") & "/Documents/" & filename)
                FileUploadControl.SaveAs(PathName)
                StatusLabel.Text = "Upload status: Archivos subidos OK"
                InsertPath(filename, Session("ID"))
            Catch ex As Exception
                StatusLabel.Text = "Upload status: " & ex.Message
            End Try
        End If
    End Sub

    Sub InsertPath(ByVal pFileName As String, ByVal pID As String)
        Dim oConn As New OleDbConnection
        Dim oComm As New OleDbCommand

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("App_Data/Haras.mdb")
        oComm.Connection = oConn

        oComm.CommandText = "INSERT INTO Files (Nombre,id_caballo, fecha) VALUES (@file, @caballo, @fecha)"
        oComm.Parameters.AddWithValue("file", pFileName)
        oComm.Parameters.AddWithValue("caballo", pID)
        oComm.Parameters.AddWithValue("fecha", DateTime.Now.ToString())

        oConn.Open()
        oComm.ExecuteNonQuery()
        oConn.Close()
    End Sub
End Class
