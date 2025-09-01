<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="pendingFee.aspx.cs" Inherits="pendingFee" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .form-control:disabled {
            background-color: #e9ecef;
        }

        .form-control[readonly] {
            background-color: #fff;
        }

        .form-control:disabled {
            background-color: #e9ecef;
        }

        .modal-backdrop {
            background-color: transparent !important;
        }


            .modal-backdrop.in {
                all: unset;
            }
    </style>
    <style>
        input[type=number] {
            -moz-appearance: textfield;
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

        .caps {
            text-transform: uppercase;
        }

        .FixedHeader {
            position: sticky;
            font-weight: bold;
            top: 0;
        }

        .transparent-modal {
            background-color: #0000;
            border: 0px;
        }

        .form-control:disabled {
            background-color: #e9ecef;
        }

        .form-control[readonly] {
            background-color: #fff;
        }

        .form-control:disabled {
            background-color: #e9ecef;
        }
    </style>

    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <script src="js/jquery-min.js"></script>

    <form id="Form1" runat="server">
        <br />
        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-lg-8">
                        <asp:Label ID="lbl_h" runat="server" Font-Size="Medium">Fees</asp:Label>
                    </div>
                </div>
            </div>
            <div class=" panel panel-body">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <div class="row">
                            <div class="col-lg-2">
                                Academic Year
                                <asp:DropDownList ID="ddlyear" CssClass="form-control" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <br />
                        <div id="feedetail" runat="server">
                            <div class="row">
                                <div class="col-lg-3">
                                    <span runat="server"><b>Total Fee :</b></span>
                                    <i class="fas fa-rupee-sign"></i>
                                    <asp:Label ID="lblfee" runat="server"></asp:Label>
                                    <asp:Label ID="lblotherfees" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblgroup_id" runat="server" Visible="false"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    <span runat="server"><b>Amount Paid :</b></span>
                                    <asp:Label ID="lblpaid" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    <span runat="server"><b>Balance Amount :</b></span>
                                    <asp:Label ID="lblbal" runat="server"></asp:Label>
                                </div>
                                <div class="col-lg-3">
                                    <span runat="server"><b>Category :</b></span>
                                    <asp:Label ID="txtCategory" runat="server"></asp:Label>
                                </div>
                            </div>
                            <br />
                            <div class="row" runat="server" id="feetable">
                                <div class="col-md-12 table-responsive" style="overflow: auto; max-height: 400px;">
                                    <asp:GridView ID="grdfees" runat="server" Style="text-align: left;" AutoGenerateColumns="False" CssClass="table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr.no">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_sr_no" runat="server" Text='<%# Container.DataItemIndex + 1 %>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="15%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblflag" runat="server" Text='<%# Eval("flag")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_struct_id" runat="server" Text='<%# Eval("Struct_id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_struct_type" runat="server" Text='<%# Eval("Struct_type")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="STRUCTURE NAME">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblstructname" runat="server" Text='<%# Eval("Struct_name")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="25%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="TOTAL FEE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblamount" runat="server" Text='<%# Eval("TotalFees")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PAID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpaid" runat="server" Text='<%# Eval("Paid")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="BALANCE">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpending" runat="server" Text='<%# Eval("Balance")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <br />
                            <div class="row" id="amtshw" runat="server">
                                <div class="col-lg-3 col-md-2" id="div_install" runat="server" visible="false">
                                    <b>Installment :</b>
                                    <asp:DropDownList ID="ddl_install" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddl_install_SelectedIndexChanged"></asp:DropDownList>
                                    <asp:HiddenField ID="hidden_install_amount" runat="server" ClientIDMode="Static" />
                                </div>
                                <div class="col-lg-3">
                                    <span id="Span1" runat="server"><b>Amount To Pay :</b></span>
                                    <asp:TextBox ID="txtamount" runat="server" CssClass="form-control" onkeypress="return isNumber(event)" ReadOnly="true"></asp:TextBox>
                                    <asp:HiddenField ID="txtAmntHdn" runat="server" ClientIDMode="Static" />
                                </div>
                                <div class="col-lg-2" id="div_fine" runat="server" visible="false">
                                    <div class="row">
                                        <div class="col-md-2 fs-3" style="align-items: center; display: flex; align-content: center; font-size: larger">
                                            <br />
                                            +
                                        </div>
                                        <div class="col-md-10">
                                            <asp:LinkButton ID="lnkFineInfo" runat="server" OnClick="lnkFineInfo_Click" CssClass="info-icon"><img src="images/information.png" style="height:16px;" alt="Info" />
                                            </asp:LinkButton>
                                            <b>Fine :</b></span>
                                            <asp:TextBox ID="txt_fine" runat="server" CssClass="form-control" onkeypress="return isNumber(event)" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <asp:HiddenField ID="group_id" runat="server" ClientIDMode="Static" />
                                <div class="col-lg-3" style="padding-top: 18px;">
                                    <asp:Button ID="btnpay" runat="server" Text="Pay" CssClass="btn btn-success form-control" OnClick="btnpay_Click" Style="max-width: 230px" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnpay" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="modal" id="modal_fine" data-bs-backdrop="static" data-bs-keyboard="false">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <div class="modal-header row">
                        <div class="col-md-10">
                            <h2 class="modal-title">Fine</h2>
                        </div>
                        <div class="col-md-2" style="display: flex; justify-content: flex-end;">
                            <button type="button" class="close btn btn-outline-secondary" data-bs-dismiss="modal" aria-bs-label="Close" onclick="closeModal('modal_fine')"><span aria-hidden="true">&times;</span></button>
                        </div>
                    </div>
                    <div class="modal-body">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="row" style="font-size: 15px; text-align: center">
                                    <div class="col-md-4">
                                        <b>STUDENT ID :</b>
                                        <asp:Label ID="fine_stud_id" runat="server"></asp:Label>
                                    </div>
                                    <div class="col-md-8">
                                        <b>NAME :</b>
                                        <asp:Label ID="fine_name" runat="server"></asp:Label>
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-md-12 table-responsive" style="max-height: 300px; overflow: auto">
                                        <asp:Literal ID="litFineTable" runat="server"></asp:Literal>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

    </form>
    <script src="notify-master/js/notify.js"></script>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>

    <script type="text/javascript">
        function openModal(name) {
            $("[id*=" + name + "]").modal('show');
        }
        function closeModal(name) {
            $("[id*=" + name + "]").modal('hide');
        }
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
        function redirect(name) {
            window.open(name, '_blank');
        }

    </script>

    <script>

</script>

</asp:Content>

