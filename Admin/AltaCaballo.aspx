<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AltaCaballo.aspx.vb" Inherits="Admin_Alta" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Haras - Administracion</title>


    <!-- Bootstrap core CSS -->
    <link href="../CSS/bootstrap.css" rel="stylesheet" type="text/css" />
    <!-- Custom styles for this template -->
    <link href="../CSS/signin.css" rel="stylesheet" type="text/css" />
    <!-- Modal -->
    <link href="../CSS/Modal.css" rel="stylesheet" type="text/css" />
    <!-- Custom styles for this template -->
    <link href="../css/navbar.css" rel="stylesheet" type="text/css" />

    <!--scripts-->
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>

    <!-- Just for debugging purposes. Don't actually copy this line! -->
    <!--[if lt IE 9]><script src="../../assets/js/ie8-responsive-file-warning.js"></script><![endif]-->
             
    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
      <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
</head>

  <body>

     <!-- Wrap all page content here -->
    <div id="wrap">

      <!-- Fixed navbar -->
      <div class="navbar navbar-default navbar-fixed-top">
        <div class="container">
          <div class="navbar-header">
            <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
              <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="../simple.aspx">Haras</a>
          </div>
          <div class="collapse navbar-collapse">
            <ul class="nav navbar-nav">
              <li class="active"><a href="../simple.aspx">Ficha Medica</a></li>
              <%If Session("Rol") = "Administrador" Then%>
			  <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Usuarios<b class="caret"></b></a>
                <ul class="dropdown-menu">
                  <li><a href="Alta.aspx">Alta</a></li>
                  <li><a href="Modificacion.aspx">Modificacion</a></li>
                  <li><a href="Baja.aspx">Baja</a></li>
                </ul>
              </li>
              <%End If%>
<%If Session("Rol") = "Administrador" Then%>
			  <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Caballos<b class="caret"></b></a>
                <ul class="dropdown-menu">
                  <li><a href="AltaCaballo.aspx">Alta</a></li>
                  <li><a href="ModificacionCaballo.aspx">Modificacion</a></li>
                  <li><a href="BajaCaballo.aspx">Baja</a></li>
                </ul>
              </li>
              <%End If%>
              <li><a href="Upload.aspx">Subir Archivos</a></li>
              <li><a href="Comentarios.aspx">Comentarios</a></li>
            </ul>
              <ul class="nav navbar-nav navbar-right">
                  <%If Session("Usuario") = "" Then%>
              <li><a href="../login.aspx">Login</a></li>
                  <%Else%>
              <li><a href="#"><%=Session("Usuario")%></a></li>
              <li><a href="../Logout.aspx">Log off</a></li>
                  <%End If%>
            </ul>
          </div>
		  <!--/.nav-collapse -->
 
        </div>
      </div>
	<!-- contenido gral -->	
<br />
<br />	  
	  
      <!-- Begin page content -->
      <div class="container">
        <div class="row">
            <div class="col-md-6">
                <h2>Alta de Caballos</h2>
                <form id="frmAlta" class="form-horizontal" role="form" runat="server">

                <table class="table table-condensed">
                    <tr>
                        <td><label for="txtPrefijo">Prefijo</label></td>
                        <td><asp:TextBox ID="txtPrefijo" runat="server" class="form-control"></asp:TextBox></td>
                        <td><label for="txtNombre">Nombre</label></td>
                        <td><asp:TextBox ID="txtNombre" runat="server" class="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><label for="txtPadre">Padre</label></td>
                        <td><asp:TextBox ID="txtPadre" runat="server" class="form-control"></asp:TextBox></td>
                        <td><label for="txtMadre">Madre</label></td>
                        <td><asp:TextBox ID="txtMadre" runat="server" class="form-control"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td><label for="txtcamda">Camada</label></td>
                        <td><asp:TextBox ID="txtcamada" runat="server" class="form-control"></asp:TextBox></td>
                        <td><label for="txtacronimo">Acronimo</label></td>
                        <td><asp:TextBox ID="txtacronimo" runat="server" class="form-control"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td><label for="txtrp">Rp</label></td>
                        <td><asp:TextBox ID="txtrp" runat="server" class="form-control"></asp:TextBox></td>
                         <td><label for="ddProvisorio">Provisorio</label></td>
                        <td><asp:DropDownList ID="ddProvisorio" runat="server" class="form-control"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td><label for="ddSexo">Sexo</label></td>
                        <td><asp:DropDownList ID="ddSexo" runat="server" class="form-control"></asp:DropDownList></td>
                         <td><label for="ddPelaje">Pelaje</label></td>
                        <td><asp:DropDownList ID="ddPelaje" runat="server" class="form-control"></asp:DropDownList></td>
                    </tr>
                     <tr>
                        <td><asp:Button ID="btnGuardar" runat="server" Text="Guardar" class="btn btn-primary" /></td>
						<td colspan="3">
                            <asp:Label ID="lblResultado" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>							
                 </table>
                 </form>
            </div>
            <div class="col-md-6">
            </div>

       </div>
  </body>
</html>

