<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="documents.aspx.cs" Inherits="documents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .modal-backdrop {
    z-index: 1040 !important; /* backdrop below modal */
}
.modal {
    z-index: 1050 !important; /* modal above backdrop */
}

    </style>
    <form id="form1" runat="server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <div class="panel panel-info" style="margin-top:20px">
    <div class="panel-heading">
        <div class="row">
            <div class="col-md-6">
                <h3 style="margin-top:15px;margin-bottom:15px;line-height:0">Document Upload</h3>
            </div>
            <div class="col-md-6">
                <asp:Button ID="btnmodal" runat="server" CssClass="btn btn-success" Text="Upload" Style="float: right" OnClick="btnmodal_Click" />
            </div>
        </div>
    </div>
    <div class="panel-body">
        <div class="row" style="padding-top: 10px">
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                <div style="padding-top: 10px;display:flex;justify-content:center">
                    
                        <asp:Image ID="imgphoto" runat="server" CssClass="form-control img-responsive" Visible="true" ToolTip="Photo" Style="height: 200px; width: 200px" ImageAlign="AbsMiddle" />
                    
                </div>
                <div style="padding-top: 10px;display:flex;justify-content:center">
                    
                        <asp:Image ID="imgsign" runat="server" CssClass="form-control img-responsive" Visible="true" ToolTip="Signature" Height="100px" Width="200px" />
                    
                </div>
            </div>
            <div class="col-lg-9 col-md-9 col-sm-12 col-xs-12">
                <div class="table-responsive" style="overflow-y: scroll; height: 500px">
                    <asp:GridView ID="grid" runat="server" CssClass="table table-hover table-striped" AutoGenerateColumns="false" ForeColor="#333333" GridLines="None" CellPadding="4" OnRowCommand="grid_RowCommand">
                        <RowStyle HorizontalAlign="Center" BackColor="#F7F6F3" ForeColor="#333333" />
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
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#999999" />
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        <!-- Modal: Upload Document -->
        <div id="myModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <div class="modal-content">
                    <div class="modal-header" style="background-color: #337ab7; color: white;">
                        <h4 class="modal-title">Upload Document</h4>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-12">Document Type :</div>
                                    <div class="col-lg-8 col-md-8 col-sm-12">
                                        <asp:DropDownList ID="ddl_doc" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_doc_SelectedIndexChanged" AutoPostBack="true" />
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
                                    <div class="col-md-4 col-sm-12">
                                        <asp:Button ID="btn_save" runat="server" CssClass="form-control btn btn-info" Text="Save" OnClick="btn_save_Click" />
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <asp:Button ID="btn_clear" runat="server" CssClass="form-control btn btn-info" Text="Clear" OnClick="btn_clear_Click" />
                                    </div>
                                    <div class="col-md-4 col-sm-12">
                                        <asp:Button ID="btn_close" runat="server" CssClass="form-control btn btn-info" data-dismiss="modal" Text="Close" OnClick="btn_close_Click" />
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <!-- Modal: Photo Viewer -->
        <div id="photoviewer" class="modal fade" role="dialog">
            <div style="height: 40px; background-color: black; opacity: 0.8">
                <a id="Button1" runat="server" style="float: right; color: white" data-dismiss="modal">
                    <i class="fa fa-times form-control" style="font-size: 22px; background-color: black; color: white"></i>
                </a>
                <a id="btn_download" runat="server" style="float: right; color: white" download>
                    <i class="fa fa-download form-control" style="font-size: 22px; background-color: black; color: white"></i>
                </a>
                <asp:Label ID="lbldocname" Style="float: left; color: white; padding: 8px;" runat="server"></asp:Label>
            </div>
            <div class="modal-dialog">
                <div class="row" style="justify-content: center; padding-top: 10px">
                    <asp:Image ID="imgdocviewer" runat="server" CssClass="form-control" Style="height: 100%; width: 100%" />
                </div>
            </div>
        </div>
    </div>
</div>

        </form>
</asp:Content>

