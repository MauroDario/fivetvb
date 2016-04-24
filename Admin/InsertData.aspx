<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InsertData.aspx.vb" Inherits="Admin_InsertData" %>

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

      <div class="container">
        
        <form id="form1" runat="server" class="form-signin">
            <h2>Normalizacion de Tablas</h2>
            <asp:Button runat="server" id="UploadButton" text="Cargar" class="btn btn-lg btn-primary btn-block"/>
            <p><asp:Label runat="server" id="lblImporacion" /></p>
            <p><asp:Label runat="server" id="lblCamada" /></p>
            <p><asp:Label runat="server" id="lblPelaje" /></p>
            <p><asp:Label runat="server" id="lblPrefijo" /></p>
            <p><asp:Label runat="server" id="lblSexo" /></p>
            <p><asp:Label runat="server" id="lblInsert" /></p>
            <p><asp:Label runat="server" id="lblFolder" /></p>
        </form>
      </div>
  </body>
</html>