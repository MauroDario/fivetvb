Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class Admin_Alta
    Inherits System.Web.UI.Page

    Dim oConn As New OleDbConnection
    Dim oComm As New OleDbCommand
    Dim oDA As New OleDbDataAdapter
    Dim oDS As New DataSet
    Dim IDUsuario As Integer
    Dim Flag As Boolean = False

    Protected Sub frmAlta_Load(sender As Object, e As System.EventArgs) Handles frmAlta.Load
        If Session("Usuario") = "" And Session("Rol") <> "Administrador" Then
            Session("Pagina") = "Admin/Alta.aspx"
            Response.Redirect("../Login.aspx")
        End If

        If Not Page.IsPostBack Then
            CargarRol()
        End If

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        If VerificarEmail() <> True Then
            InsertarUsuario()
            SeleccionarRol()
            InsertarRol()
            LimpiarTexto()
            lblResultado.Text = "Datos guardados correctamente"
        Else
            lblResultado.Text = "El Usuario o Email ingresado ya existe"
        End If
    End Sub

    Sub CargarRol()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn
        oComm.CommandText = "SELECT Id_Role, Role FROM Role ORDER BY Role Desc"

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "Rol")

        oConn.Open()
        ddRol.DataTextField = oDS.Tables("Rol").Columns("Role").ToString()
        ddRol.DataValueField = oDS.Tables("Rol").Columns("Id_Role").ToString()
        ddRol.DataSource = oDS.Tables("Rol")
        ddRol.DataBind()
        oConn.Close()
    End Sub

    Function VerificarEmail() As Boolean
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn
        oComm.CommandText = "SELECT * FROM Usuarios WHERE Email = '" & txtEmail.Text & "' OR Usuario = '" & txtUsuario.Text & "'"

        oConn.Open()
        Dim oDR As OleDbDataReader = oComm.ExecuteReader()

        Do While oDR.Read()
            Flag = True
        Loop
        oDR.Close()

        Return Flag
    End Function

    Sub InsertarUsuario()
        oConn.Close()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oComm.Connection = oConn

        oComm.CommandText = "INSERT INTO Usuarios (Nombre, Apellido, Usuario, Email, Pass) VALUES (@Nombre, @Apellido, @Usuario, @Email, @Pass)"
        oComm.Parameters.AddWithValue("Nombre", txtNombre.Text)
        oComm.Parameters.AddWithValue("Apellido", txtApellido.Text)
        oComm.Parameters.AddWithValue("Usuario", txtUsuario.Text)
        oComm.Parameters.AddWithValue("Email", txtEmail.Text)
        oComm.Parameters.AddWithValue("Pass", txtPassword.Text)

        oConn.Open()
        oComm.ExecuteNonQuery()
        oConn.Close()

    End Sub

    Sub SeleccionarRol()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oComm.Connection = oConn

        oComm.CommandText = "SELECT TOP 1 Id_Usuario FROM Usuarios ORDER BY Id_Usuario DESC"

        oConn.Open()

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "ID")

        IDUsuario = CInt(oDS.Tables("ID").Rows(0).Item("Id_Usuario"))

        oConn.Close()

    End Sub


    Sub InsertarRol()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oComm.Connection = oConn

        'oComm.CommandText = "INSERT INTO User_Role (ID_Usuario, ID_Role) VALUES (@Usuario, @Role)"
        'oComm.Parameters.AddWithValue("Usuario", IDUsuario)
        'oComm.Parameters.AddWithValue("Role", CInt(ddRol.SelectedItem.Value))

        oComm.CommandText = "INSERT INTO User_Role (ID_Usuario, ID_Role) VALUES (" & IDUsuario & ", " & CInt(ddRol.SelectedItem.Value) & ")"

        oConn.Open()
        oComm.ExecuteNonQuery()
        oConn.Close()
    End Sub

    Sub LimpiarTexto()
        txtApellido.Text = ""
        txtEmail.Text = ""
        txtNombre.Text = ""
        txtPassword.Text = ""
        txtUsuario.Text = ""
    End Sub

End Class
