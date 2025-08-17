<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="KT_exm.aspx.cs" Inherits="KT_exm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <script src="js/jquery-min.js"></script>
    <script src="notify-master/js/notify.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <style>
        .table-container {
            height: 500px;
        }

        table {
            display: flex;
            flex-flow: column;
            height: 100%;
            width: 100%;
        }

            table thead {
                flex: 0 0 auto;
                width: calc(100% - 0.9em);
            }

            table tbody {
                flex: 1 1 auto;
                display: block;
                overflow-y: scroll;
            }

                table tbody tr {
                    width: 100%;
                }

                table thead,
                table tbody tr {
                    display: table;
                    table-layout: fixed;
                }

            table td, table th {
                padding: 0.3em;
            }


        .header-cont {
            width: 100%;
            position: fixed;
            top: 0px;
        }

        body {
            font-family: sans-serif;
            font-size: 10pt;
        }

        td {
            cursor: pointer;
        }

        .hover_row {
            background-color: #A1DCF2;
        }
    </style>
    <form runat="server" style="margin-top: 15px">
        <div class="panel panel-red">
            <div class="panel-heading">
                <div>
                    <h4>KT EXAM</h4>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-6">
                            Shri/Smt./Kum.:
                            <asp:TextBox ID="name_txt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                            <div class="row" style="margin-top: 5px">
                                <div class="col-lg-6">
                                    Caste:-
                                            <asp:TextBox ID="caste_txt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                                <div class="col-lg-6">
                                    Mobile No.:-
                                            <asp:TextBox ID="mobno_txt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 5px">
                                <div class="col-lg-12">
                                    E-mail ID:-
                                            <asp:TextBox ID="emailid_txt" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            Address:
                            <asp:TextBox ID="add_txt" runat="server" CssClass="form-control" Enabled="false" TextMode="MultiLine" Height="153px"></asp:TextBox>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 10px">
                        <div class="col-lg-3">
                            Branch:-
                            <asp:DropDownList ID="drp_dwn_branch" Enabled="false" runat="server" CssClass="form-control" OnSelectedIndexChanged="drp_dwn_branch_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem>COMPUTER</asp:ListItem>
                                <asp:ListItem>CIVIL</asp:ListItem>
                                <asp:ListItem>MECHANICAL</asp:ListItem>
                                <asp:ListItem>EXTC</asp:ListItem>
                                <asp:ListItem>ELECTRICAL</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            Marksheet Registration No (only for Sem-3,4,5,6) :-
                            <asp:TextBox ID="txtstud" runat="server" MaxLength="8" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Semester:-
                            <asp:DropDownList ID="drp_dwn_sem" runat="server" CssClass="form-control" OnSelectedIndexChanged="drp_dwn_sem_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem Value="Sem-1">SEMESTER-1</asp:ListItem>
                                <asp:ListItem Value="Sem-2">SEMESTER-2</asp:ListItem>
                                <asp:ListItem Value="Sem-3">SEMESTER-3</asp:ListItem>
                                <asp:ListItem Value="Sem-4">SEMESTER-4</asp:ListItem>
                                <asp:ListItem Value="Sem-5">SEMESTER-5</asp:ListItem>
                                <asp:ListItem Value="Sem-6">SEMESTER-6</asp:ListItem>
                                <asp:ListItem Value="Sem-7">SEMESTER-7</asp:ListItem>
                                <asp:ListItem Value="Sem-8">SEMESTER-8</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            Pattern:-
                            <asp:DropDownList ID="drp_dwn_pattern" runat="server" CssClass="form-control" OnSelectedIndexChanged="drp_dwn_pattern_SelectedIndexChanged" AutoPostBack="false">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem Value="CBGS">CBGS</asp:ListItem>
                                <asp:ListItem Value="CBCS">CBCS</asp:ListItem>
                                <asp:ListItem Value="C SCHEME">C SCHEME</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3">
                            Exam
                            <asp:DropDownList ID="ddl_exam" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_exam_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px">
                        <div class="col-lg-8 col-lg-offset-2">
                            <asp:GridView ID="GridView1" Enabled="false" runat="server" AutoGenerateColumns="false" Height="100%" OnRowDataBound="GridView1_RowDataBound" BorderColor="#D9534F" ShowHeaderWhenEmpty="true" CssClass="table table-container table-bordered table-hover" Style="font-family: sans-serif" HeaderStyle-BackColor="#DFF0D8" HeaderStyle-ForeColor="#3C763D" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Check for KT">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="kt_chk" Checked="true" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sr No.">
                                        <ItemTemplate>
                                            <asp:Label ID="sr_no" runat="server" Text='<%# Bind("Sr_No") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject Code">
                                        <ItemTemplate>
                                            <asp:Label ID="subject_code" runat="server" Text='<%# Bind("subject_code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name Of Subject">
                                        <ItemTemplate>
                                            <asp:Label ID="name_of_subj" runat="server" Text='<%# Bind("Subject_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Theory Marks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="theory_mrks" runat="server" CssClass="form-control" Text='<%# Bind("theory_mrks") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Internal Marks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="internal_mrks" runat="server" CssClass="form-control" Text='<%# Bind("internal_mrks") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Term Work">
                                        <ItemTemplate>
                                            <asp:TextBox ID="term_wrks" runat="server" CssClass="form-control" Text='<%# Bind("term_wrks") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Practical/Oral">
                                        <ItemTemplate>
                                            <asp:TextBox ID="pract_mrks" runat="server" CssClass="form-control" Text='<%# Bind("pract_mrks") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>
                    <div class="panel" id="university" runat="server" visible="false">
                        <div class="panel panel-heading">
                            <div class="row">
                                <h4 style="float:left;margin-left: 10px;">University Exam </h4>
                                <div class="col-md-3" style="float: right;" >
                                    <asp:Button ID="btn_add" runat="server" CssClass="btn btn-block btn-success" Text="Add Subject" OnClick="btn_add_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-body">
                       <%-- <asp:UpdatePanel runat="server">
                            <ContentTemplate>--%>

                           
                        <asp:GridView ID="grdfees" runat="server" Font-Size="10pt" Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" OnRowDataBound="grdfees_RowDataBound" OnRowDeleting="grdfees_RowDeleting">
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <RowStyle HorizontalAlign="Center" Height="10px"></RowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="SUBJECT NAME">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_subject" runat="server" CssClass="form-control" onkeypress="return subject(event)" MaxLength="100" OnTextChanged="txt_subject_TextChanged" AutoPostBack="true" Text='<%# Eval("Subject_Name")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="30%" />
                                    <HeaderStyle Width="30%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="THEORY(MARKS)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txttheory" runat="server" CssClass="form-control" onkeypress="return alphanumeric(event)" MaxLength="3" autocomplete="off" AutoPostBack="true"   OnTextChanged="txttheory_TextChanged"  Text='<%# Eval("theory_mrks")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                    <HeaderStyle Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="INTERNAL(MARKS)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtinternal" runat="server" CssClass="form-control" onkeypress="return alphanumeric(event)" MaxLength="4" autocomplete="off" AutoPostBack="true" OnTextChanged="txtinternal_TextChanged" Text='<%# Eval("internal_mrks")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                    <HeaderStyle Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TERM WORK(MARKS)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txttermwork" runat="server" CssClass="form-control" onkeypress="return alphanumeric(event)" MaxLength="4" autocomplete="off" AutoPostBack="true" OnTextChanged="txttermwork_TextChanged" Text='<%# Eval("term_wrks")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                    <HeaderStyle Width="15%" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PRACTICAL/ORAL(MARKS)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtpractical" runat="server" CssClass="form-control" onkeypress="return alphanumeric(event)" MaxLength="4" autocomplete="off" AutoPostBack="true" OnTextChanged="txtpractical_TextChanged" Text='<%# Eval("pract_mrks")%>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Width="15%" />
                                    <HeaderStyle Width="15%" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="DELETE">
                                    <ItemTemplate>
                                        <asp:Button ID="btndel" runat="server" Text="Remove" CommandName="delete" CssClass="btn btn-block btn-danger" />
                                    </ItemTemplate>
                                    <ItemStyle Width="10%" />
                                    <HeaderStyle Width="10%"/>
                                </asp:TemplateField>
                            </Columns>

                        </asp:GridView>
                       <%--  </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </div>
                </div>

                <div class="row">
                    <div class="col-lg-10 col-lg-offset-1"></div>
                    <div class="col-lg-3"></div>
                    <div class="col-lg-3"></div>
                    <div class="col-lg-3"></div>
                    <div class="col-lg-3">
                        <asp:Button ID="print_btn" runat="server" CssClass="form-control btn-primary" OnClick="print_btn_Click" Text="Print" Enabled="false" />
                    </div>
                </div>

            </div>
            </div>
       
        <div id="fyModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!--content-->
                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Payment Mode</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-5">
                                <asp:Button ID="Payment" CssClass="btn btn-success" Text="Online Payment" runat="server" Width="100%" OnClick="Payment_Click" />
                            </div>
                            <div class="col-lg-5">

                                <asp:Button ID="Button1" CssClass="btn btn-success" Text="Offline" runat="server" Width="100%" OnClick="Button1_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script type="text/javascript">
        $(document).ready(function (e) {
            $("#<%=txtstud.ClientID%>").bind('keypress', function (event) {
                var regex = new RegExp("^[0-9]{1,18}(?:\.[0-9]{0,2})?$");
                var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
                if (!regex.test(key)) {
                    event.preventDefault();
                    return false;
                }
            });
        });

        function subject(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57')) || (keyEntry == '45') || (keyEntry == '32') || (keyEntry == '44') || (keyEntry == '46') || (keyEntry == '38') || (keyEntry == '47') || (keyEntry == '39') || (keyEntry == '41') || (keyEntry == '40'))
                return true;
            else {
                return false;
            }
        }
        function alphanumeric(e) {
            isIE = document.all ? 1 : 0
            keyEntry = !isIE ? e.which : event.keyCode;
            if (((keyEntry >= '65') && (keyEntry <= '90')) || ((keyEntry >= '97') && (keyEntry <= '122')) || ((keyEntry >= '48') && (keyEntry <= '57'))  || (keyEntry == '43') )
                return true;
            else {
                return false;
            }
        }
    </script>

</asp:Content>

