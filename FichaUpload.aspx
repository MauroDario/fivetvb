<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FichaUpload.aspx.vb" Inherits="upload" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Haras - Upload Files</title>

    <!-- Bootstrap core CSS -->
    <link href="CSS/bootstrap.css" rel="stylesheet" type="text/css" />
    <!-- Custom styles for this template -->
    <link href="CSS/signin.css" rel="stylesheet" type="text/css" />

	
    <!-- Custom styles for this template -->
    <link href="css/sticky-footer-navbar.css" rel="stylesheet">
    <script type="text/javascript" src="js/jquery-1.7.2.min.js"></script>
    <script type="text/javascript" src="js/modernizr-2.5.3.js"></script>

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="assets/js/html5shiv.js"></script>
      <script src="assets/js/respond.min.js"></script>
    <![endif]-->
  
</head>

  <body cz-shortcut-listen="true">
      <div class="container">
        <form id="form1" runat="server" class="form-signin">
            <h2>Subir Foto Principal</h2>
            <asp:FileUpload id="FileUploadControl" runat="server" class="form-control"/>
            <asp:Button runat="server" id="UploadButton" text="Subir" class="btn btn-lg btn-primary btn-block"/>
            <button type="button" class="btn btn-lg btn-primary btn-block" onclick="location.href='ficha.aspx?id=<%=Session("ID") %>';">Regresar</button>
            <br /><br />
            <asp:Label runat="server" id="StatusLabel" />
        </form>
      </div>
  </body>
</html>