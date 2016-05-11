<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Ficha.aspx.vb" Inherits="Ficha" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Haras - Ficha T&eacute;cnica</title>

    <!-- Bootstrap core CSS -->
    <link href="CSS/bootstrap.css" rel="stylesheet" type="text/css" />
    <!-- Custom styles for this template -->
    <link href="CSS/signin.css" rel="stylesheet" type="text/css" />
    <!-- Modal -->
    <link href="CSS/Modal.css" rel="stylesheet" type="text/css" />
    <!-- Custom styles for this template -->
    <link href="css/navbar.css" rel="stylesheet" type="text/css" />

    <!--scripts-->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.0/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

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
                    <a class="navbar-brand" href="#">Haras</a>
                </div>
                <div class="collapse navbar-collapse">
                    <ul class="nav navbar-nav">
                        <li class="active"><a href="default.aspx">Home</a></li>
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown">Busquedas<b class="caret"></b></a>
                            <ul class="dropdown-menu">
                                <li><a href="Simple.aspx">Simple</a></li>
                                <li><a href="Compleja.aspx">Compleja</a></li>
                                <li><a href="Voz.aspx">Por voz</a></li>
                            </ul>
                        </li>
                        <%If Session("Rol") = "Administrador" Or Session("Rol") = "Power User" Then%>
                        <li><a href="admin/default.aspx">Administracion</a></li>
                        <%End If%>
                        <li><a href="#contact">Contacto</a></li>
                    </ul>
                    <ul class="nav navbar-nav navbar-right">
                        <%If Session("Usuario") = "" Then%>
                        <li><a href="login.aspx">Login</a></li>
                        <%Else%>
                        <li><a href="#"><%=Session("Usuario")%></a></li>
                        <li><a href="Logout.aspx">Log off</a></li>
                        <%End If%>
                    </ul>
                </div>
                <!--/.nav-collapse -->

            </div>
        </div>
        <!-- contenido gral -->
        <!-- Begin page content -->
        <form id="frmFicha" class="form-horizontal" runat="server">
            <div class="container">
                <div class="page-header">
                </div>

                <div class="row">
                    <div class="col-md-4">
                        <asp:Image ID="imgCaballo" runat="server" Width="300" Height="200" />
                        <a href="FichaUpload.aspx?id=<%=Session("ID")%>" class="btn btn-primary btn-xs">Subir Foto principal</a>
                        <a href="Simple.aspx" class="btn btn-primary btn-xs">Consulta Simple</a>
                    </div>
                    <div class="col-md-8">
                        <table class="table table-condensed">
                            <tr>
                                <td>
                                    <label for="txtNombre">Nombre</label></td>
                                <td>
                                    <asp:Label ID="txtNombre" runat="server"></asp:Label></td>
                                <td>
                                    <label for="txtRP">RP</label></td>
                                <td>
                                    <asp:Label ID="txtRP" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtPadre">Padre</label></td>
                                <td>
                                    <asp:Label ID="txtPadre" runat="server"></asp:Label></td>
                                <td>
                                    <label for="txtCamada">Camada</label></td>
                                <td>
                                    <asp:Label ID="txtCamada" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <label for="txtMadre">Madre</label></td>
                                <td>
                                    <asp:Label ID="txtMadre" runat="server"></asp:Label></td>
                                <td>
                                    <label for="txtUbicacion">Ubicacion</label></td>
                                <td>
                                    <asp:Label ID="txtUbicacion" runat="server"></asp:Label></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="col-md-8">
                    <div class="col-md-2">
                        <asp:Label ID="lblUsuario" runat="server" Text="Label"></asp:Label>
                        <%--<br />
                    <asp:Label ID="lblFecha" runat="server" Text="Label"></asp:Label>--%>
                    </div>
                    <div class="col-md-8">

                        <asp:TextBox ID="txtDescripcion" TextMode="multiline" class="form-control" Rows="5" runat="server" />
                        <asp:TextBox ID="idComentario" Visible="false" runat="server"></asp:TextBox>
                        <br />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8">
                    <div class="col-md-2"></div>
                    <div class="col-md-4">
                        <label id="lblFecha" for="txtFecha" runat="server">Fecha de Evento</label>
                        <div class="form-inline">
                            <asp:TextBox ID="txtFecha" Type="Date" class="form-control" runat="server" Width="60%" ReadOnly="true"></asp:TextBox>
                            <%--<a href="#" class='btn btn-success' role='button' visible="false" id="GuardarFecha" runat="server"><span class='glyphicon glyphicon-ok'></span></a>&nbsp;--%>
                            <asp:LinkButton ID="GuardarFecha" runat="server" CssClass="btn btn-success" Visible="false"> <span aria-hidden="true" class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                            <asp:LinkButton ID="EditarFecha" class="btn btn-primary btn-sm" type="button" runat="server"> <span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></asp:LinkButton>
                        </div>
                        <br />
                    </div>
                    <div class="col-md-6" style="padding-left: 20px; border-left: 1px solid black;">
                        <asp:Label ID="TablaCommentFiles" runat="server"></asp:Label>
                    </div>
                </div>

            </div>

            <div class="row">
                <div class="col-md-8">
                    <div class="col-md-2"></div>
                    <div class="col-md-8">
                        <asp:Button ID="BorrarComentario" class="btn btn-primary" type="button" runat="server" Text="Nuevo Comentario" />
                        <asp:Button ID="NuevoComentario" class="btn btn-primary" type="button" runat="server" Text="Guardar Comentario" />
                        <%--<asp:LinkButton ID="btnAsociarImagen" runat="server" class="btn btn-primary"></asp:LinkButton>--%>
                        <%--<a href="Upload.aspx?id=<%=Session("ID")%>" class="btn btn-primary">Asociar Imagen</a>--%>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <div class="row">
                <div class="col-md-12">
                    <div class="col-md-8">
                        <asp:Label ID="Tabla" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="col-md-4">
                        <a href="Upload.aspx?id=<%=Session("ID")%>" class="btn btn-primary">Agregar Documentos</a>
                        <asp:Label ID="TablaDocs" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <!-- Modal para cambiar el nombre del archivo -->
            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title" id="myModalLabel">Renombrar</h4>
                        </div>
                        <div class="modal-body">
                            <asp:HiddenField ID="txtFilePath" runat="server" />
                            <asp:TextBox ID="txtFileName" runat="server"></asp:TextBox>
                            <asp:Button ID="btnRename" runat="server" Text="Button" CssClass="btn btn-primary" OnClick="btnRename_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </form>

    </div>


    <div id="openModal" class="modalDialog">
        <div>
            <a href="#close" title="Cerrar" class="close">X</a>
            <h2>Ficha Veterinaria</h2>
            <asp:Label ID="ImageModal" runat="server"></asp:Label>
        </div>
    </div>
    <script type="text/javascript">
        $('#myModal').on('shown.bs.modal', function (e) {
            var path = e.relatedTarget.attributes["data-path"].value
            $('#txtFilePath').val(path);
            console.log($('#txtFilePath').val(path));
        })
    </script>
</body>
</html>

