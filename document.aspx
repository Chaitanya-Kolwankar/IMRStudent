<%@ Page Language="C#" AutoEventWireup="true" CodeFile="document.aspx.cs" Inherits="document" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="bootstrap-4.0.0-alpha.6-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome-4.5.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <script src="js/jquery-min.js"></script>
    <script src="notify-master/js/notify.js"></script>
    <script src="bootstrap-4.0.0-alpha.6-dist/js/bootstrap.min.js"></script>
    <style>
        th {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server"></asp:ScriptManager>
        <div class="container" style="padding-top: 40px">
            <div class="card">
                <div class="card-header card-primary">
                    <div class="row">
                        <div class="col-md-6">
                            <h3 style="color: white">Document Upload</h3>
                        </div>
                        <div class="col-md-6">
                            <asp:Button ID="btnmodal" runat="server" CssClass="btn btn-success" Text="Upload" Style="float: right" OnClick="btnmodal_Click" />
                        </div>

                    </div>

                </div>
                <div class="card-body">
                    <div class="row" style="padding-top:10px">
                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                            <div class="container" style="padding-top: 10px">
                                <center>
                        <asp:Image ID="imgphoto" runat="server" CssClass="form-control img-responsive" Visible="true" ToolTip="Photo" style="Height:200px;Width:200px" ImageAlign="AbsMiddle"></asp:Image>
                                                                </center>
                            </div>
                            <div class="container" style="padding-top: 10px">
                                <center>
                        <asp:Image ID="imgsign" runat="server" CssClass="form-control img-responsive" Visible="true" ToolTip="Photo" Height="100px" Width="200px"></asp:Image>
                                                                </center>
                            </div>
                        </div>
                        <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                            <div class="table-responsive" style="overflow-y:scroll;height:500px">
                                <asp:GridView ID="grid" runat="server" class="table table-hover table-striped" AutoGenerateColumns="false" ForeColor="#333333" GridLines="None" CellPadding="4" OnRowCommand="grid_RowCommand" >
                                    <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Document Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblfilename" runat="server" Text='<%#Eval("doc_name")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbllink" runat="server" Text='<%#Eval("doc_path")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:Button ID="btn_view" runat="server" Text="View" CssClass="btn btn-success" CommandName="view" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:Button ID="btn_delete" runat="server" Text="Delete" CssClass="btn btn-danger" CommandName="delflag" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>
                                    <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                                    <HeaderStyle HorizontalAlign="Center" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                                    <EditRowStyle BackColor="#999999"></EditRowStyle>
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                    <%--       <div class="row">
                        <div class="container">
                                <center>
                        <asp:Image ID="Image1" runat="server" CssClass="form-control img-responsive" ImageUrl="" Visible="true" ToolTip="Photo" style="Height:200px;Width:200px" ImageAlign="AbsMiddle"></asp:Image>
                                                                </center>
                            <asp:Button ID="btn_upload" runat="server" CssClass="btn btn-success" Text="Upload" style="float:right" OnClick="btn_upload_Click" />
                            </div>
                    </div>--%>


                    <div id="myModal" class="modal fade " role="dialog">

                        <div class="modal-dialog">


                            <div class="modal-content">
                                <div class="modal-header card card-primary ">
                                    <h3 style="color: white">Upload Document</h3>

                                </div>
                                <div class="modal-body">
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <%--<asp:PostBackTrigger ControlID="ddl_doc" />--%>
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-4 col-md-4 col-sm-12">
                                                    Document Type :
                                                </div>
                                                <div class="col-lg-8 col-md-8 col-sm-12">

                                                    <asp:DropDownList ID="ddl_doc" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_doc_SelectedIndexChanged" AutoPostBack="true">
                                                       <%-- <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="STUDENT_SSCMARKSHEET">SSC Marksheet</asp:ListItem>
                                                        <asp:ListItem Value="STUDENT_HSCMARKSHEET">HSC Marksheet</asp:ListItem>
                                                        <asp:ListItem Value="STUDENT_DIPLOMA">Diploma Final Marksheet</asp:ListItem>
                                                        <asp:ListItem Value="STUDENT_LEAVING">Leaving Certificate</asp:ListItem>
                                                        <asp:ListItem Value="STUDENT_AADHAR">Aadhar Card</asp:ListItem>
                                                        <asp:ListItem Value="STUDENT_INCOME">Income Certificate</asp:ListItem>
                                                        <asp:ListItem Value="STUDENT_BIRTHDOMICILE">Birth/Domicile</asp:ListItem>
                                                        <asp:ListItem Value="STUDENT_UOM">Application of UOM</asp:ListItem>
                                                        <asp:ListItem Value="STUDENT_CET">Cet Scorecard</asp:ListItem>
                                                        <asp:ListItem Value="STUDENT_CASTE">Caste Certificate</asp:ListItem>
                                                        <asp:ListItem Value="STUDENT_VALIDITY">Caste Validity</asp:ListItem>--%>
                                                    </asp:DropDownList>

                                                </div>


                                            </div>

                                            <div class="row" style="justify-content: center; padding-top: 10px">
                                                <asp:Image ID="imgdoc" runat="server" CssClass="form-control" Style="height: 600px; width: 500px" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <Triggers>
                                            <asp:PostBackTrigger ControlID="btn_save" />
                                            <asp:PostBackTrigger ControlID="btn_clear" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <div class="row" style="justify-content: center; padding-top: 10px">
                                                <asp:FileUpload ID="filedoc" runat="server" CssClass="form-control" onchange="previewFile()" accept="image/*" />

                                            </div>




                                     
                                            <div class="row" style="padding-top: 20px">
                                                <div class="col-md-4 col-md-4 col-sm-12">
                                                    <asp:Button ID="btn_save" runat="server" CssClass="form-control btn btn-info" Text="Save" OnClick="btn_save_Click" />
                                                </div>
                                                <div class="col-md-4 col-md-4 col-sm-12">
                                                    <asp:Button ID="btn_clear" runat="server" CssClass="form-control btn btn-info" Text="Clear" OnClick="btn_clear_Click" />
                                                </div>
                                                <div class="col-md-4 col-md-4 col-sm-12">
                                                    <asp:Button ID="btn_close" runat="server" CssClass="form-control btn btn-info" data-dismiss="modal" Text="Close" OnClick="btn_close_Click" />
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>

                                </div>
                            </div>
                        </div>

                    </div>



                    <div id="photoviewer" class="modal fade " role="dialog">

                        <div style="height: 40px; background-color: black; opacity: 0.8">
                            <a id="Button1" runat="server" style="float: right; color: white" data-dismiss="modal"><i class="fa fa-times form-control" style="font-size: 22px; background-color: black; color: white"></i></a>
                            <a id="btn_download" runat="server" style="float: right; color: white" download><i class="fa fa-download form-control" style="font-size: 22px; background-color: black; color: white"></i></a>

                            <asp:Label  id="lbldocname"  style="float: left; color: white;    padding: 8px;"  runat="server" ></asp:Label>
                        </div>
                        <div class="modal-dialog">

                            <div class="row" style="justify-content: center; padding-top: 10px">
                                <asp:Image ID="imgdocviewer" runat="server" CssClass="form-control" Style="height: 100%; width: 100%" />
                            </div>



                          
                        </div>

                    </div>
                 
                </div>
            </div>
        </div>

    </form>
</body>
<script type="text/javascript">
    function previewFile() {
        var preview = document.querySelector('#<%=imgdoc.ClientID %>');
        var file = document.querySelector('#<%=filedoc.ClientID %>').files[0];
        var reader = new FileReader();

        reader.onloadend = function () {
            preview.src = reader.result;
        }

        if (file) {
            reader.readAsDataURL(file);
        } else {
            preview.src = "";
        }
    }

    $(document).keydown(function (event) {
        if (event.keyCode == 27) {
            $('#photoviewer').hide();
            $('.modal-backdrop').remove();
        }
    });
 

 

</script>

</html>
