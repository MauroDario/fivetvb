<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Haras - Home</title>

    <!-- Script JQuery -->
    <script src="http://code.jquery.com/jquery.js"></script>
    <!-- Bootstrap core CSS -->
    <link href="CSS/bootstrap.css" rel="stylesheet" type="text/css" />
    <!-- Custom styles for this template -->
    <link href="CSS/signin.css" rel="stylesheet" type="text/css" />

	
    <!-- Custom styles for this template -->
    <link href="css/sticky-footer-navbar.css" rel="stylesheet">

    <!-- HTML5 shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!--[if lt IE 9]>
      <script src="assets/js/html5shiv.js"></script>
      <script src="assets/js/respond.min.js"></script>
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
              <span class="icon-bar"></span>
              <span class="icon-bar"></span>
            </button>
            <a class="navbar-brand" href="#">Haras</a>
          </div>
          <div class="collapse navbar-collapse">
            <ul class="nav navbar-nav">
              <li class="active"><a href="default.aspx">Home</a></li>
              <li><a href="#about">Historial</a></li>
			  <li class="dropdown">
                <a href="#" class="dropdown-toggle" data-toggle="dropdown">Busquedas<b class="caret"></b></a>
                <ul class="dropdown-menu">
                  <li><a href="Simple.aspx">Simple</a></li>
                  <li><a href="Compleja.aspx">Compleja</a></li>
                  <li><a href="Voz.aspx">Por voz</a></li>
                </ul>
              </li>
              <li><a href="#contact">Contacto</a></li>
              <% If Session("Usuario") = "" Then%>
              <li><a href="login.aspx">Login</a></li>
              <%Else%>
              <li><a href="#"><%=Session("Usuario")%></a></li>
              <li><a href="LogOut.aspx">Log off</a></li>
              <%End If%>
            </ul>
          </div>
		  <!--/.nav-collapse -->
        </div>
      </div>

	  
	<!-- contenido gral -->
	
	  
	  
	  
      <!-- Begin page content -->
      <div class="container">
        <div class="page-header">
          <h1>Haras</h1>
        </div>
		
    	<hr/>

<!--Carousel-->
<div class="row">
  <div class="col-md-2"></div>
  <div class="col-md-8">
    <div id="carousel" class="carousel slide" data-interval="2000">
    <!-- Indicators -->
        <ol class="carousel-indicators">
        <li data-target="#carousel" data-slide-to="0" class="active"></li>
        <li data-target="#carousel" data-slide-to="1"></li>
        <li data-target="#carousel" data-slide-to="2"></li>
        </ol>

        <!-- Wrapper for slides -->
        <div class="carousel-inner">
        <div class="item active">
            <img src="images/01.jpg" alt="Pradera"/>
            <div class="carousel-caption">
            <h1>Pradera</h1>
            </div>
        </div>
        <div class="item">
            <img src="images/02.jpg" alt="Playa"/>
            <div class="carousel-caption">
            <h1>Playa</h1>
            </div>
        </div>
        <div class="item">
            <img src="images/03.jpg" alt="Sierras"/>
            <div class="carousel-caption">
            <h1>Sierras</h1>
            </div>
        </div>
        </div>

        <!-- Controls -->
        <a class="left carousel-control" href="#carousel" data-slide="prev">
        <span class="icon-prev"></span>
        </a>
        <a class="right carousel-control" href="#carousel" data-slide="next">
        <span class="icon-next"></span>
        </a>
    </div>		
 
    <script>
        $(document).ready(function () {
            $('.carousel').carousel({
                interval: 4000
            });
        });
    </script>
  </div>
  <div class="col-md-2"></div>
</div>
<!--End Carousel-->


      </div>
    </div>




	
    <!-- Bootstrap core JavaScript
    ================================================== -->
    <!-- Placed at the end of the document so the pages load faster -->
    <script src="assets/js/jquery.js"></script>
    <script src="Dist/js/bootstrap.min.js"></script>
  </body>
</html>
