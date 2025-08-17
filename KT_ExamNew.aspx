<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="KT_ExamNew.aspx.cs" Inherits="KT_ExamNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script src="js/jquery-min.js"></script>
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
    <form id="Form1" runat="server" style="margin-top: 15px">
        <asp:ScriptManager ID="scriptmanager" runat="server"></asp:ScriptManager>
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
                            Student ID
                            <asp:TextBox ID="txtstud" runat="server" MaxLength="8" CssClass="form-control"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Semester:-
                            <asp:DropDownList ID="drp_dwn_sem" runat="server" CssClass="form-control" OnSelectedIndexChanged="drp_dwn_sem_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem Value="Sem-1">SEMESTER-1</asp:ListItem>
                                <asp:ListItem Value="Sem-2">SEMESTER-2</asp:ListItem>
                                <asp:ListItem Value="Sem-7">SEMESTER-7</asp:ListItem>
                                <asp:ListItem Value="Sem-8">SEMESTER-8</asp:ListItem>
                             <%--   <asp:ListItem Value="Sem-9">SEMESTER-9</asp:ListItem>--%>
                                
                            </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            Pattern:-
                            <asp:DropDownList ID="drp_dwn_pattern" runat="server" CssClass="form-control"  AutoPostBack="true">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem Value="CBGS">CBGS</asp:ListItem>
                                <asp:ListItem Value="CBCS">CBCS</asp:ListItem>
                                <asp:ListItem Value="C-SCHEME">C-SCHEME</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-lg-3">
                            Exam
                            <asp:DropDownList ID="ddl_exam" runat="server" CssClass="form-control"  OnSelectedIndexChanged="ddl_exam_SelectedIndexChanged"  AutoPostBack="true">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem Value="march-2020-atkt">MARCH-2020 ATKT</asp:ListItem>
                                <%-- <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem Value="Sem-1">SEMESTER-1</asp:ListItem>
                                <asp:ListItem Value="Sem-2">SEMESTER-2</asp:ListItem>
                                <asp:ListItem Value="Sem-3">SEMESTER-3</asp:ListItem>
                                <asp:ListItem Value="Sem-4">SEMESTER-4</asp:ListItem>
                                <asp:ListItem Value="Sem-5">SEMESTER-5</asp:ListItem>
                                <asp:ListItem Value="Sem-6">SEMESTER-6</asp:ListItem>--%>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="col-lg-8 col-lg-offset-2">
                            <asp:GridView ID="Gridview2" runat="server" ShowFooter="true" AutoGenerateColumns="false" BorderColor="#D9534F" ShowHeaderWhenEmpty="true" CssClass="table table-container table-bordered table-hover" Style="font-family: sans-serif" HeaderStyle-BackColor="#DFF0D8" HeaderStyle-ForeColor="#3C763D" Width="100%" Height="40%" OnRowDeleting="Gridview2_RowDeleting">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No.">
                                        <ItemTemplate>
                                            <asp:Label ID="sr_no" runat="server" Text='<%# Bind("Sr_No") %>'  CssClass="form-control"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsubname" runat="server" Text='<%# Bind("Subject_name") %>' Enabled='<%# Eval("Subject_name").ToString() == "" ? true : false %>'  CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Theory Marks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txttheory" runat="server" Text='<%# Bind("theory_mrks") %>'  CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Internal Marks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtinternal" runat="server" Text='<%# Bind("internal_mrks") %>'  CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Term Work">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtterm" runat="server" Text='<%# Bind("term_wrks") %>'  CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Practical Marks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtpractical" runat="server" Text='<%# Bind("pract_mrks") %>'  CssClass="form-control"></asp:TextBox>
                                        </ItemTemplate>
                                          <FooterStyle HorizontalAlign="Right" />
                                        <FooterTemplate>
                                            <asp:Button ID="ButtonAdd" runat="server" Text="Add New Row" OnClick="ButtonAdd_Click" />
                                        </FooterTemplate>
                                         
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="true" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                 
                    <div class="row">
                        <div class="col-lg-10 col-lg-offset-1"></div>
                        <div class="col-lg-3"></div>
                        <div class="col-lg-3"></div>
                        <div class="col-lg-3"></div>
                        <div class="col-lg-3">
                            <asp:Button ID="print_btn" runat="server" CssClass="form-control btn-primary"  Text="Print" Enabled="false" OnClick="print_btn_Click" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
        <div id="fyModal" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <div class="modal-content">
                    <div class="modal-header">
                        <h4 class="modal-title">Payment Mode</h4>
                    </div>
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-lg-1"></div>
                            <div class="col-lg-5">
                                <asp:Button ID="Payment"  CssClass="btn btn-success" Text="Online Payment" Enabled="false"  runat="server" Width="100%" OnClick="Payment_Click" />
                            </div>
                            <div class="col-lg-5">

                                <asp:Button ID="Button1" CssClass="btn btn-success" Text="Offline" Enabled="false"  runat="server" Width="100%" OnClick="Button1_Click"/>
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>

        <script type="text/javascript">
           // $(document).ready(function (e) {
            //    $("#<%=txtstud.ClientID%>").bind('keypress', function (event) {
            //          var regex = new RegExp("^[0-9]{1,18}(?:\.[0-9]{0,2})?$");
              //        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
              //        if (!regex.test(key)) {
              //            event.preventDefault();
              //            return false;
              //        }
             //     });
            //  });
        </script>
    </form>

</asp:Content>

