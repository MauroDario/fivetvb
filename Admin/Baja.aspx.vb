Imports System
Imports System.Data
Imports System.Data.OleDb

Partial Class Admin_Baja
    Inherits System.Web.UI.Page

    Dim oConn As New OleDbConnection
    Dim oComm As New OleDbCommand
    Dim oDA As New OleDbDataAdapter
    Dim oDS As New DataSet
    Dim IDUsuario As Integer

    Protected Sub frmBaja_Load(sender As Object, e As System.EventArgs) Handles frmBaja.Load
        If Session("Usuario") = "" And Session("Rol") <> "Administrador" Then
            Session("Pagina") = "Admin/Baja.aspx"
            Response.Redirect("../Login.aspx")
        End If

        If Not Page.IsPostBack Then
            CargarApellidos()
            'CargarRol()
        End If

    End Sub

    Protected Sub ddlApellido_SelectedIndexChanged(sender As Object, e As System.EventArgs) Handles ddlApellido.SelectedIndexChanged
        CargarDatos()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        DeleteUsuario()
        DeleteRol()
        LimpiarText()
        lblResultado.Text = "Datos han sido borrados correctamente"
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
    End Sub

    'Sub CargarRol()
    '    oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

    '    oComm.Connection = oConn
    '    oComm.CommandText = "SELECT Id_Role, Role FROM Role ORDER BY Role Desc"

    '    oDA.SelectCommand = oComm
    '    oDA.Fill(oDS, "Rol")

    '    ddRol.DataTextField = oDS.Tables("Rol").Columns("Role").ToString()
    '    ddRol.DataValueField = oDS.Tables("Rol").Columns("Id_Role").ToString()
    '    ddRol.DataSource = oDS.Tables("Rol")
    '    ddRol.DataBind()

    '    'ddRol.Items.Insert(0, "")
    'End Sub

    Sub CargarDatos()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn
        oComm.CommandText = "SELECT * FROM Usuarios WHERE Apellido = '" & ddlApellido.SelectedItem.ToString & "'"

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "Usuarios")

        txtNombre.Text = oDS.Tables("Usuarios").Rows(0).Item("Nombre")
        txtUsuario.Text = oDS.Tables("Usuarios").Rows(0).Item("Usuario")
        txtEmail.Text = oDS.Tables("Usuarios").Rows(0).Item("Email")
        txtPassword.Text = oDS.Tables("Usuarios").Rows(0).Item("Pass")

        txtNombre.DataBind()
        txtUsuario.DataBind()
        txtEmail.DataBind()
        txtPassword.DataBind()
    End Sub


    Sub DeleteUsuario()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oComm.Connection = oConn

        oComm.CommandText = "DELETE * FROM Usuarios WHERE Id_Usuario = " & ddlApellido.SelectedItem.Value & ""

        oConn.Open()
        oComm.ExecuteNonQuery()
        oConn.Close()

    End Sub

    Sub DeleteRol()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oComm.Connection = oConn

        oComm.CommandText = "DELETE * FROM User_Role WHERE Id_Usuario = " & ddlApellido.SelectedItem.Value & ""

        oConn.Open()
        oComm.ExecuteNonQuery()
        oConn.Close()
    End Sub

    Sub LimpiarText()
        txtEmail.Text = ""
        txtNombre.Text = ""
        txtPassword.Text = ""
        txtUsuario.Text = ""
        ddlApellido.ClearSelection()
    End Sub

End Class
