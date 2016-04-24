<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="Login" %>

<!DOCTYPE html>

<html lang="en"><head><meta http-equiv="Content-Type" content="text/html; charset=UTF-8">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">
    <link rel="shortcut icon" href="images/favicon.png">

    <title>Haras - Login</title>

    <!-- Bootstrap core CSS -->
    <link href="CSS/bootstrap.css" rel="stylesheet" type="text/css" />
    <!-- Custom styles for this template -->
    <link href="CSS/signin.css" rel="stylesheet" type="text/css" />


    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="assets/js/html5shiv.js"></script>
      <script src="assets/js/respond.min.js"></script>
    <![endif]-->
  </head>

  <body cz-shortcut-listen="true">

    <div class="container">

      <form id="Form1" class="form-signin" runat="server">
        <h2 class="form-signin-heading">login</h2>
        <asp:TextBox id="email" type="email" class="form-control" placeholder="Email" required="required" runat="server"></asp:TextBox>
        <asp:TextBox id="pass" type="password" class="form-control" placeholder="Password" required="required" pattern="[A-Za-z0-9]{8,}" title="Ingresar letras mayusculas, minusculas y numeros" runat="server"></asp:TextBox>
        <asp:Button ID="enviar" class="btn btn-lg btn-primary btn-block" type="submit" runat="server" Text="Ingresar"/>
      <asp:CheckBox ID="CheckBoxCookie" runat="server" />
      <asp:Label ID="Label1" runat="server" Text="Recordarme"></asp:Label>
        <h4><asp:Label ID="lblerror" runat="server"></asp:Label></h4>
      </form>
    </div>
</body>
</html>