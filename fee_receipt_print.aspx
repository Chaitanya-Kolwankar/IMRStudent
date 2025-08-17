<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="fee_receipt_print.aspx.cs" Inherits="fee_receipt_print" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="js/jquery-min.js"></script>
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <script src="notify-master/js/notify.js"></script>
    <style>
        .mygrid th {
            color: White;
            background-color: #0078BC;
            font-weight: bold;
            text-transform: uppercase;
            text-align: center;
        }
    </style>
    <form id="Form1" runat="server">
        <br />
        <div class="container-fluid">
            <div class="panel panel-primary">
                <div class="panel-heading">Student Receipt</div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-md-2">
                            Academic Year   
                    <asp:DropDownList ID="ddlayid" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddlayid_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="row" id="transaction" style="margin-top: 10px">
                        <div class="col-md-12">
                            <div class="table-responsive" style="max-height: 500px; overflow: auto;">
                                <asp:GridView ID="grdtransaction" CssClass="table table-condensed table-bordered mygrid" runat="server" OnRowCommand="grdtransaction_RowCommand" AutoGenerateColumns="false">
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <RowStyle HorizontalAlign="Center" Height="10px"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="RECEIPT NO">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrecptno" runat="server" Text='<%# Eval("Recpt_no")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RECEIPT TYPE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblstruct" runat="server" Text='<%# Eval("struct")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AMOUNT">
                                            <ItemTemplate>
                                                <asp:Label ID="lblamount" runat="server" Text='<%# Eval("amt")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAYMENT MODE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmode" runat="server" Text='<%# Eval("Recpt_mode")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAYMENT DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="lbldate" runat="server" Text='<%# Eval("PAYDATE")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RECEIPT">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnprint" CssClass="btn btn-success" runat="server" CommandName="Print" CommandArgument="<%# Container.DataItemIndex %>" Text="RECEIPT" />
                                                <asp:Label ID="lblstatus" runat="server" Text='<%# Eval("Status")%>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="15%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <script>
            function redirect(name) {
                window.open(name, '_blank');
            }
        </script>
    </form>
</asp:Content>

