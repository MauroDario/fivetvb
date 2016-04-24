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
            Session("Pagina") = "Admin/AltaCaballo.aspx"
            Response.Redirect("../Login.aspx")
        End If

        If Not Page.IsPostBack Then
            CargarProvisorio()
            CargarSexo()
            CargarPelaje()
        End If

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As System.EventArgs) Handles btnGuardar.Click

        InsertarCaballo()
        LimpiarTexto()
        lblResultado.Text = "Datos guardados correctamente"
       
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
  

    Sub InsertarCaballo()
        oConn.Close()
        oConn.ConnectionString = ConfigurationManager.ConnectionStrings("Haras").ConnectionString & Context.Server.MapPath("../App_Data/Haras.mdb")
        oComm.Connection = oConn

        oComm.CommandText = "INSERT INTO Caballos (Prefijo, Nombre, Padre, Madre, Camada, Acronimo, rp, provisorio, id_sexo, id_pelaje) VALUES (@Prefijo, @Nombre, @Padre, @Madre, @Camada, @Acronimo, @rp, @provisorio, @id_sexo, @id_pelaje)"
        oComm.Parameters.AddWithValue("Prefijo", txtPrefijo.Text)
        oComm.Parameters.AddWithValue("Nombre", txtNombre.Text)
        oComm.Parameters.AddWithValue("Padre", txtPadre.Text)
        oComm.Parameters.AddWithValue("Madre", txtMadre.Text)
        oComm.Parameters.AddWithValue("Camada", txtcamada.Text)
        oComm.Parameters.AddWithValue("Acronimo", txtacronimo.Text)
        oComm.Parameters.AddWithValue("rp", txtrp.Text)
        Dim str As String = ddProvisorio.SelectedValue.ToString
        Dim result As Boolean = False

        If str = "SI" Then
            result = True
        Else
            result = False
        End If
        oComm.Parameters.AddWithValue("provisorio", result)
        oComm.Parameters.AddWithValue("id_sexo", ddSexo.SelectedValue.ToString)
        oComm.Parameters.AddWithValue("id_pelaje", ddPelaje.SelectedValue.ToString)


        oConn.Open()
        oComm.ExecuteNonQuery()
        oConn.Close()

    End Sub

   

    Sub LimpiarTexto()
        txtPrefijo.Text = ""
        txtNombre.Text = ""
        txtNombre.Text = ""
        txtPadre.Text = ""
        txtMadre.Text = ""
        txtcamada.Text = ""
        txtacronimo.Text = ""
        txtrp.Text = ""
        
    End Sub

End Class
