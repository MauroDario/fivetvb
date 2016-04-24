Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class Admin_Modificacion
    Inherits System.Web.UI.Page

    Dim oConn As New OleDbConnection
    Dim oComm As New OleDbCommand
    Dim oDA As New OleDbDataAdapter
    Dim oDS As New DataSet
    Dim IDUsuario As Integer
    Dim Flag As Boolean

    Protected Sub frmModificacion_Load(sender As Object, e As System.EventArgs) Handles frmModificacion.Load
        If Session("Usuario") = "" And Session("Rol") <> "Administrador" Then
            Session("Pagina") = "Admin/Modificacion.aspx"
            Response.Redirect("../Login.aspx")
        End If

        If Not Page.IsPostBack Then
            CargarApellidos()
            CargarRol()
        End If

    End Sub

    Protected Sub ddlApellido_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlApellido.SelectedIndexChanged
        CargarDatos()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        UpdateUsuario()
        UpDateRol()
        LimpiarText()
        lblResultado.Text = "Datos han sido modificados correctamente"
    End Sub

    Sub CargarApellidos()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn
        oComm.CommandText = "SELECT Id_Usuario, Apellido FROM Usuarios ORDER BY Apellido Asc"

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "Usuarios")

        ddlApellido.DataTextField = oDS.Tables("Usuarios").Columns("Apellido").ToString()
        ddlApellido.DataValueField = oDS.Tables("Usuarios").Columns("Id_Usuario").ToString()
        ddlApellido.DataSource = oDS.Tables("Usuarios")
        ddlApellido.DataBind()

        ddlApellido.Items.Insert(0, "")
    End Sub

    Sub CargarRol()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn
        oComm.CommandText = "SELECT Id_Role, Role FROM Role ORDER BY Role Asc"

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "Rol")

        ddRol.DataTextField = oDS.Tables("Rol").Columns("Role").ToString()
        ddRol.DataValueField = oDS.Tables("Rol").Columns("Id_Role").ToString()
        ddRol.DataSource = oDS.Tables("Rol")
        ddRol.DataBind()
    End Sub

    Sub CargarDatos()
        Dim strConn As String
        Dim IDRol As String

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn

        strConn = "SELECT Usuarios.*, user_role.id_role "
        strConn += "FROM Usuarios INNER JOIN user_role ON Usuarios.id_usuario = user_role.id_usuario "
        strConn += "WHERE Usuarios.Apellido = '" & ddlApellido.SelectedItem.ToString & "'"

        oComm.CommandText = strConn

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "Usuarios")

        'ddRol.SelectedValue = ""
        txtNombre.Text = oDS.Tables("Usuarios").Rows(0).Item("Nombre")
        txtUsuario.Text = oDS.Tables("Usuarios").Rows(0).Item("Usuario")
        txtEmail.Text = oDS.Tables("Usuarios").Rows(0).Item("Email")
        txtPassword.Text = oDS.Tables("Usuarios").Rows(0).Item("Pass")

        IDRol = oDS.Tables("Usuarios").Rows(0).Item("Id_Role")
        ddRol.ClearSelection()
        ddRol.Items.FindByValue(IDRol).Selected = True


        txtNombre.DataBind()
        txtUsuario.DataBind()
        txtEmail.DataBind()
        txtPassword.DataBind()
    End Sub


    Sub UpdateUsuario()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oComm.Connection = oConn

        oComm.CommandText = "UPDATE Usuarios SET Nombre = @Nombre, Apellido = @Apellido, Usuario = @Usuario, Email = @Email, Pass = @Pass WHERE Id_Usuario = " & ddlApellido.SelectedItem.Value & ""
        oComm.Parameters.AddWithValue("Nombre", txtNombre.Text)
        oComm.Parameters.AddWithValue("Apellido", ddlApellido.SelectedItem.ToString)
        oComm.Parameters.AddWithValue("Usuario", txtUsuario.Text)
        oComm.Parameters.AddWithValue("Email", txtEmail.Text)
        oComm.Parameters.AddWithValue("Pass", txtPassword.Text)

        oConn.Open()
        oComm.ExecuteNonQuery()
        oConn.Close()

    End Sub

    Sub UpDateRol()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oComm.Connection = oConn

        oComm.CommandText = "UPDATE User_Role SET ID_Role = " & ddRol.SelectedItem.Value & " WHERE Id_Usuario = " & ddlApellido.SelectedItem.Value & ""

        oConn.Open()
        oComm.ExecuteNonQuery()
        oConn.Close()
    End Sub

    Sub LimpiarText()
        txtEmail.Text = ""
        txtNombre.Text = ""
        txtPassword.Text = ""
        txtUsuario.Text = ""
    End Sub


End Class
