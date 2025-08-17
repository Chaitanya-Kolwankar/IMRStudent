<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="neft_details.aspx.cs" Inherits="neft_details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="js/jquery-min.js"></script>
    <link href="bootstrap-datepicker-1.9.0-dist/css/bootstrap-datepicker.min.css" rel="stylesheet" />
    <script src="bootstrap-datepicker-1.9.0-dist/js/bootstrap-datepicker.min.js"></script>
    <style>
        fieldset.scheduler-border {
            border: 1px groove #ddd !important;
            padding: 0 1.4em 1.4em 1.4em !important;
            margin: 0 0 1.5em 0 !important;
            -webkit-box-shadow: 0px 0px 0px 0px #000;
            box-shadow: 0px 0px 0px 0px #000;
        }

        legend.scheduler-border {
            width: inherit; /* Or auto */
            padding: 0 10px; /* To give a bit of padding on the left and right */
            border-bottom: none;
        }
    </style>
    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <div class="row">
                    <div class="col-lg-6">
                        <strong>NEFT Details</strong>
                    </div>
                    <div class="col-lg-6">
                        <a class="btn btn-sm btn-success pull-right" href="Apply_Course.aspx">Go back</a>
                    </div>
                </div>
            </div>
            <div class="container-fluid">
                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                <div class="row">
                    <div class="col-lg-3">
                        Payment for Academic Year:
                                <asp:DropDownList ID="ddlyear" CssClass="form-control" AutoPostBack="true" runat="server">
                                </asp:DropDownList>
                    </div>
                    <div class="col-lg-3">
                        Select Your Sub-Course:
                                <asp:DropDownList ID="ddl_subcourse" CssClass="form-control" AutoPostBack="true" runat="server">
                                </asp:DropDownList>
                    </div>

                    <div id="Div1" class="row" runat="server" style="display: none">
                        <asp:Label ID="getid" runat="server"></asp:Label>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-3">
                        Bank Name:
                        <asp:TextBox ID="txt_bnk" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        Amount:
                      <asp:TextBox ID="txtamt" MaxLength="9" onkeypress="return isNumber(event)" class="form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        Date of Payment
                      <asp:TextBox ID="txt_dt" class="datepickeradmdate form-control" runat="server"></asp:TextBox>
                    </div>
                    <div class="col-lg-3">
                        Transaction No.
                      <asp:TextBox ID="txt_trans" class="form-control" runat="server"></asp:TextBox>
                    </div>
                </div>
                <%--   </ContentTemplate>
                </asp:UpdatePanel>--%>
                <br />

                <fieldset class="scheduler-border">
                    <legend class="scheduler-border" style="margin-bottom: 0px; font-weight: bold; font-family: 'Times New Roman'">Upload Receipt</legend>
                    <div class="row">
                        <div class="col-lg-3">
                            <asp:Image ID="imgphoto" runat="server" CssClass="form-control img-responsive" Visible="true" ToolTip="Photo"
                                Height="200px" Width="200px"></asp:Image>
                        </div>
                        <div class="col-lg-4">
                            <div class="row">
                                <asp:FileUpload ID="filephoto2" CssClass="form-control" runat="server"></asp:FileUpload>
                                <br />
                                <asp:Button ID="btnuploadphoto" OnClick="btnuploadphoto_Click" runat="server" Text="Upload Receipt" class="form-control btn-default"></asp:Button>
                            </div>
                        </div>
                    </div>
                </fieldset>

                <div class="row">
                    <br />
                    <div class="row">
                        <div class="col-lg-4"></div>
                        <div class="col-lg-4">
                            <br />
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit Details" class="btn btn-success btn-block" OnClick="btnsubmit_Click"></asp:Button>
                        </div>
                        <div class="col-lg-4"></div>
                    </div>
                    <br />
                </div>
                <div class="row" runat="server" visible="false" id="grid_show">
                    <div class="col-lg-12">
                        <div class="table-responsive">
                            <div style="OVERFLOW: scroll; height: auto;">
                                <br />
                                <asp:GridView ID="grd" runat="server" class="table table-hover table-striped" AutoGenerateColumns="False" Style="border: 2px solid;">
                                    <RowStyle HorizontalAlign="Center"></RowStyle>
                                    <AlternatingRowStyle Wrap="false" />
                                    <Columns>
                                        <asp:TemplateField Visible="false" HeaderText="rateid">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_idd" runat="server" Text='<%# Eval("id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_bnk" runat="server" Text='<%# Eval("bank_name")%>' Font-Size="Large"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Transaction No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_trans" runat="server" Text='<%# Eval("trans_no")%>' Font-Size="Large"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount Paid">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_amt" runat="server" Text='<%# Eval("amount")%>' Font-Size="Large"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount Paid">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_paydate" runat="server" Text='<%# Eval("pay_dt")%>' Font-Size="Large"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_subcrs" runat="server" Text='<%# Eval("subcourse_id")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                                                    <ContentTemplate>--%>
                                                <asp:Button ID="btnview" runat="server" Text="EDIT" OnClick="btnview_Click" CssClass="btn btn-block btn-info" />
                                                <%--  </ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
    </form>
    <script>
        $('.datepickeradmdate').datepicker({
            format: "dd/mm/yyyy"
        });
    </script>
    <script>
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

