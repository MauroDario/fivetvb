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
            Session("Pagina") = "Admin/BajaCaballo.aspx"
            Response.Redirect("../Login.aspx")
        End If
        If Not Page.IsPostBack Then
            CargarNombre()
        End If


    End Sub

    

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        DeleteCaballo()
        LimpiarText()
        lblResultado.Text = "Datos han sido borrados correctamente"
    End Sub


    Sub CargarNombre()

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn
        oComm.CommandText = "SELECT id_caballo, nombre FROM Caballos ORDER BY nombre desc"

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "Caballos")

        oConn.Open()
        ddNombre.DataTextField = oDS.Tables("Caballos").Columns("nombre").ToString()
        ddNombre.DataValueField = oDS.Tables("Caballos").Columns("id_caballo").ToString()
        ddNombre.DataSource = oDS.Tables("Caballos")
        ddNombre.DataBind()
        oConn.Close()
    End Sub


    Sub CargarDatos()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn
        oComm.CommandText = "SELECT * FROM Caballos WHERE id_caballo = " & ddNombre.SelectedItem.Value & ""

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "Caballos")

        txtPadre.Text = oDS.Tables("Caballos").Rows(0).Item("Padre")
        txtacronimo.Text = oDS.Tables("Caballos").Rows(0).Item("Acronimo")
        txtcamada.Text = oDS.Tables("Caballos").Rows(0).Item("Camada")
        txtMadre.Text = oDS.Tables("Caballos").Rows(0).Item("Madre")
        'txtpelaje.Text = oDS.Tables("Caballos").Rows(0).Item("Pelaje")
        txtPrefijo.Text = oDS.Tables("Caballos").Rows(0).Item("Prefijo")
        txtProvisorio.Text = oDS.Tables("Caballos").Rows(0).Item("provisorio")
        txtrp.Text = oDS.Tables("Caballos").Rows(0).Item("rp")
        'txtSexo.Text = oDS.Tables("Caballos").Rows(0).Item("sexo")

        
    End Sub


    Sub DeleteCaballo()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oComm.Connection = oConn

        oComm.CommandText = "DELETE * FROM Caballos WHERE  id_caballo = " & ddNombre.SelectedItem.Value & ""

        oConn.Open()
        oComm.ExecuteNonQuery()
        oConn.Close()

    End Sub

    Sub DeleteRol()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oComm.Connection = oConn

        oComm.CommandText = "DELETE * FROM Caballos WHERE Prefijo = " & txtPrefijo.Text & ""

        oConn.Open()
        oComm.ExecuteNonQuery()
        oConn.Close()
    End Sub

    Sub LimpiarText()
        txtPadre.Text = ""
        txtacronimo.Text = ""
        txtcamada.Text = ""
        txtMadre.Text = ""
        'txtpelaje.Text = oDS.Tables("Caballos").Rows(0).Item("Pelaje")
        txtPrefijo.Text = ""
        txtProvisorio.Text = ""
        txtrp.Text = ""
    End Sub

    Protected Sub ddNombre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddNombre.SelectedIndexChanged
        CargarDatos()
        lblResultado.Text = ""
    End Sub
End Class
