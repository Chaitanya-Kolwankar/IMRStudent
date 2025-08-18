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

        table, td, th {
            border: 1px solid #ddd;
            /*text-align: left;*/
        }

        table {
            border-collapse: collapse;
            width: 100%;
        }

        th, td {
            padding: 15px;
        }

        th {
            color: #012970;
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
                        <asp:HiddenField ID="group_id" runat="server" ClientIDMode="Static" />
                        <div class="col-md-12">
                            <div class="table-responsive" style="max-height: 500px; overflow: auto;">
                                <asp:GridView ID="grdedit" runat="server" Style="text-align: left;" AutoGenerateColumns="False" CssClass="table">
                                    <Columns>
                                        <asp:TemplateField HeaderText="SR.NO">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_sr_no" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Chq_status" runat="server" Text='<%# Eval("Chq_status")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Install_id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="Install_id" runat="server" Text='<%# Eval("Install_id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RECEIPT NO">
                                            <ItemTemplate>
                                                <asp:Label ID="Receipt_no" runat="server" Text='<%# Eval("Receipt_no")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="STRUCTURE TYPE">
                                            <ItemTemplate>
                                                <asp:Label ID="Type" runat="server" Text='<%# Eval("Type")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AMOUNT">
                                            <ItemTemplate>
                                                <asp:Label ID="Amount" runat="server" Text='<%# Eval("Amount")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAYMENT MODE">
                                            <ItemTemplate>
                                                <asp:Label ID="Recpt_mode" runat="server" Text='<%# Eval("Recpt_mode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PAY DATE">
                                            <ItemTemplate>
                                                <asp:Label ID="Pay_date" runat="server" Text='<%# Eval("Pay_date")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="RECEIPT">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnprint" runat="server" CssClass="btn btn-primary form-control fa fa-print" OnClick="btnprint_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
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

