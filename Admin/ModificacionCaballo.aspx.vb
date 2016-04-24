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
            Session("Pagina") = "Admin/ModificacionCaballo.aspx"
            Response.Redirect("../Login.aspx")
        End If
        If Not Page.IsPostBack Then
            CargarNombre()
            CargarProvisorio()
            CargarSexo()
            CargarPelaje()
            CargarUbicacion()
        End If


    End Sub

    Sub CargarSexo()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn
        oComm.CommandText = "SELECT id_sexo, sexo FROM Sexo ORDER BY sexo Desc"

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "sexo")

        oConn.Open()
        ddSexo.DataTextField = oDS.Tables("Sexo").Columns("sexo").ToString()
        ddSexo.DataValueField = oDS.Tables("Sexo").Columns("id_sexo").ToString()
        ddSexo.DataSource = oDS.Tables("Sexo")
        ddSexo.DataBind()
        oConn.Close()
    End Sub

    Sub CargarProvisorio()

        ddProvisorio.Items.Add("SI")
        ddProvisorio.Items.Add("NO")
    End Sub

    Sub CargarPelaje()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn
        oComm.CommandText = "SELECT Id_pelaje, pelaje  FROM Pelaje ORDER BY pelaje Desc"

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "pelaje")

        oConn.Open()
        ddPelaje.DataTextField = oDS.Tables("Pelaje").Columns("pelaje").ToString()
        ddPelaje.DataValueField = oDS.Tables("Pelaje").Columns("Id_pelaje").ToString()
        ddPelaje.DataSource = oDS.Tables("Pelaje")
        ddPelaje.DataBind()
        oConn.Close()
    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click
        ModificarCaballo()
        LimpiarText()
        lblResultado.Text = "Datos han sido modificados correctamente"
    End Sub


    Sub CargarNombre()

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn
        oComm.CommandText = "SELECT * FROM Caballos ORDER BY nombre desc"

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "Caballos")
        oConn.Open()
        ddNombre.DataTextField = oDS.Tables("Caballos").Columns("nombre").ToString()
        ddNombre.DataValueField = oDS.Tables("Caballos").Columns("id_caballo").ToString()
        ddNombre.DataSource = oDS.Tables("Caballos")
        ddNombre.DataBind()
        oConn.Close()
    End Sub

    Sub CargarUbicacion()

        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn
        oComm.CommandText = "SELECT * FROM Ubicacion ORDER BY nombre desc"

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "Ubicacion")
        oConn.Open()
        ddUbicacion.DataTextField = oDS.Tables("Ubicacion").Columns("nombre").ToString()
        ddUbicacion.DataValueField = oDS.Tables("Ubicacion").Columns("acronimo").ToString()
        ddUbicacion.DataSource = oDS.Tables("Ubicacion")
        ddUbicacion.DataBind()
        oConn.Close()
    End Sub


    Sub CargarDatos()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")

        oComm.Connection = oConn
        oComm.CommandText = "SELECT * FROM Caballos WHERE id_caballo = " & ddNombre.SelectedItem.Value & ""

        oDA.SelectCommand = oComm
        oDA.Fill(oDS, "Caballos")

        txtPadre.Text = oDS.Tables("Caballos").Rows(0).Item("Padre")
        txtcamada.Text = oDS.Tables("Caballos").Rows(0).Item("Camada")
        txtMadre.Text = oDS.Tables("Caballos").Rows(0).Item("Madre")
        txtNombre.Text = oDS.Tables("Caballos").Rows(0).Item("Nombre")
        txtPrefijo.Text = oDS.Tables("Caballos").Rows(0).Item("Prefijo")
        Dim bool As String
        bool = oDS.Tables("Caballos").Rows(0).Item("provisorio")


        ddProvisorio.ClearSelection()
        If bool = "True" Then
            ddProvisorio.Items.FindByText("SI").Selected = True
        Else
            ddProvisorio.Items.FindByText("NO").Selected = True
        End If

        ddPelaje.ClearSelection()
        ddPelaje.Items.FindByValue(oDS.Tables("Caballos").Rows(0).Item("Id_pelaje")).Selected = True

        ddSexo.ClearSelection()
        ddSexo.Items.FindByValue(oDS.Tables("Caballos").Rows(0).Item("Id_sexo")).Selected = True

        ddUbicacion.ClearSelection()
        ddUbicacion.Items.FindByValue(oDS.Tables("Caballos").Rows(0).Item("Acronimo")).Selected = True



        txtrp.Text = oDS.Tables("Caballos").Rows(0).Item("rp")




        'txtSexo.Text = oDS.Tables("Caballos").Rows(0).Item("sexo")


    End Sub


    Sub ModificarCaballo()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oComm.Connection = oConn

        oComm.CommandText = "UPDATE Caballos SET Prefijo =@Prefijo, Nombre = @Nombre,id_pelaje = @id_pelaje, id_sexo = @id_sexo, Provisorio = @Provisorio, Padre = @Padre, Madre = @Madre, Camada = @Camada, Acronimo = @Acronimo, rp = @rp WHERE id_caballo = " & ddNombre.SelectedItem.Value & ""

        oComm.Parameters.AddWithValue("Prefijo", txtPrefijo.Text)
        oComm.Parameters.AddWithValue("Nombre", txtNombre.Text)
        oComm.Parameters.AddWithValue("id_pelaje", ddPelaje.SelectedValue.ToString)
        oComm.Parameters.AddWithValue("id_sexo", ddSexo.SelectedValue.ToString)
        Dim str As String = ddProvisorio.SelectedValue.ToString
        Dim result As Boolean = False

        If str = "SI" Then
            result = True
        Else
            result = False
        End If

        oComm.Parameters.AddWithValue("Provisorio", result)
        oComm.Parameters.AddWithValue("Padre", txtPadre.Text)
        oComm.Parameters.AddWithValue("Madre", txtMadre.Text)
        oComm.Parameters.AddWithValue("Camada", txtcamada.Text)
        oComm.Parameters.AddWithValue("Acronimo", ddUbicacion.SelectedItem.Text)
        oComm.Parameters.AddWithValue("rp", txtrp.Text)

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
        txtcamada.Text = ""
        txtMadre.Text = ""
        'txtpelaje.Text = oDS.Tables("Caballos").Rows(0).Item("Pelaje")
        txtPrefijo.Text = ""
        txtrp.Text = ""
    End Sub

    Protected Sub ddNombre_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddNombre.SelectedIndexChanged
        CargarDatos()
        lblResultado.Text = ""
    End Sub

    Protected Sub txtNombre_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNombre.TextChanged

    End Sub
End Class
