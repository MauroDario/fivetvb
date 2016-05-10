<%@ Page Language="VB" AutoEventWireup="false" CodeFile="upload.aspx.vb" Inherits="upload" %>

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
            <h2>Subir archivos</h2>
            <asp:FileUpload ID="FileUploadControl" runat="server" class="form-control" />
            <asp:Button runat="server" ID="UploadButton" Text="Subir" class="btn btn-lg btn-primary btn-block" />
            <asp:Button runat="server" ID="ShowPanel" Text="Asociar imagen" class="btn btn-lg btn-primary btn-block" />
            <button type="button" class="btn btn-lg btn-primary btn-block" onclick="location.href='ficha.aspx?id=<%=Session("ID") %>';">Regresar</button>
            <br />
            <br />
            <asp:Label runat="server" ID="StatusLabel" />
            <asp:Panel runat="server" ID="filesPanel" Visible="false">
                <div class="row">
                    <div class="col-md-12">
                        <asp:DataList ID="DataListContent" runat="server" OnItemCommand="ButtonDownloadContent"
                            RepeatDirection="Vertical" BorderStyle="None" Style="padding: 0px!important">
                            <ItemTemplate>
                                <div>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FileName") %>'></asp:Label>
                                    <asp:LinkButton ID="ButtonDownload" runat="server" Style="padding-left: 5px; text-decoration: none"
                                        ToolTip="Asociar" CommandName="Download" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "FileName") %>'
                                        Text='Asociar'></asp:LinkButton>
                                </div>
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>
            </asp:Panel>
        </form>
 
    </div>
</body>
</html>
