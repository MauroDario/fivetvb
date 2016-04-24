Imports System
Imports System.Data
Imports System.Data.OleDb
Imports System.IO


Partial Class upload
    Inherits System.Web.UI.Page

    Protected Sub form1_Load(sender As Object, e As System.EventArgs) Handles form1.Load
        If (Not String.IsNullOrEmpty(Request.QueryString("CommentId"))) Then
            Session.Add("CommentId", Request.QueryString("CommentId"))
        End If
        If Session("Usuario") = "" Then
            Session("Pagina") = "Upload.aspx"
            Response.Redirect("Login.aspx")
        End If
        If (Not Page.IsPostBack()) Then
            GenerateDownloadLinks()
        End If


    End Sub

    Sub UploadButton_Click(sender As Object, e As System.EventArgs) Handles UploadButton.Click
        upload()
    End Sub

    Sub upload()
        If FileUploadControl.HasFile Then
            Try
                Dim filename As String = Path.GetFileName(FileUploadControl.FileName)
                Dim PathName As String = Server.MapPath("~/Images/" & Session("ID") & "/Documents/" & filename)
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

        'Si hay comentario
        If (Not String.IsNullOrEmpty(Request.QueryString("CommentId"))) Then
            oConn.Open()

            Dim strConn As String = "SELECT Files.id_files FROM Files WHERE Files.id_caballo = " & pID & " AND Files.Nombre Like '%" & pFileName & "%'"
            'And Files.Nombre = @nombre"
            oComm.Connection = oConn
            oComm.CommandText = strConn

            Dim oDR As OleDbDataReader = oComm.ExecuteReader()
            Dim idFile As String = String.Empty

            Do While oDR.Read()
                idFile = oDR.Item(0).ToString
            Loop

            oDR.Close()
            oConn.Close()

            oConn.Open()

            strConn = "INSERT INTO files_comment (id_files,id_comment) VALUES (" & idFile & " , " & Session("CommentId") & ")"

            oComm.Connection = oConn
            oComm.CommandText = strConn
            oComm.ExecuteNonQuery()
            oConn.Close()
        End If

        StatusLabel.Text = "Upload status: Archivos asociado OK"
        filesPanel.Visible = False
    End Sub

    Sub GenerateDownloadLinks()
        Dim path As String = Server.MapPath("~/Images/" & Session("ID") & "/Documents/")
        If (Directory.Exists(path)) Then
            Dim ShowContent As DataTable = New DataTable()
            ShowContent.Columns.Add("Icon")
            ShowContent.Columns.Add("DownloadLink")
            ShowContent.Columns.Add("FileName")
            ShowContent.Columns.Add("Id")
            Dim di As DirectoryInfo = New DirectoryInfo(path)

            For Each fi As FileInfo In di.GetFiles()
                Dim dr As DataRow = ShowContent.NewRow()
                dr.Item("FileName") = fi.Name
                dr.Item("DownloadLink") = Server.MapPath("~/Images/" & Session("ID") & "/Documents/") + fi.Name
                Dim ext As String = IO.Path.GetExtension(fi.Name)
                ShowContent.Rows.Add(dr)
            Next fi

            DataListContent.DataSource = ShowContent
            DataListContent.DataBind()

        End If

    End Sub
    'metodo encargado de realizar la asociacion
    Protected Sub ButtonDownloadContent(source As Object, e As DataListCommandEventArgs)
        If (e.CommandName.ToString().Equals("Download")) Then

            Dim filename As String = e.CommandArgument.ToString()
            Dim oConn As New OleDbConnection
            Dim oComm As New OleDbCommand
            Dim idFile As String = String.Empty

            oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Server.MapPath("App_Data/Haras.mdb")

            oConn.Open()

            Dim strConn As String = "SELECT Files.id_files FROM Files WHERE Files.id_caballo = @idCaballo AND Files.Nombre Like '%" & filename & "%'"
            'And Files.Nombre = @nombre"
            oComm.Connection = oConn
            oComm.CommandText = strConn
            oComm.Parameters.AddWithValue("idCaballo", Session("ID"))


            Dim oDR As OleDbDataReader = oComm.ExecuteReader()

            Do While oDR.Read()
                idFile = oDR.Item(0).ToString
            Loop

            oDR.Close()
            oConn.Close()

            oConn.Open()

            strConn = "INSERT INTO files_comment (id_files,id_comment) VALUES (" & idFile & " , " & Session("CommentId") & ")"

            oComm.Connection = oConn
            oComm.CommandText = strConn
            oComm.ExecuteNonQuery()
            oConn.Close()

            StatusLabel.Text = "Upload status: Archivos asociado OK"
            filesPanel.Visible = False

        End If

    End Sub
    Protected Sub ShowPanel_Click(sender As Object, e As EventArgs) Handles ShowPanel.Click
        filesPanel.Visible = True
    End Sub

End Class
