<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reval.aspx.cs" Inherits="Reval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    <form runat="server" style="margin-top: 15px">
        <asp:ScriptManager ID="scriptmanager" runat="server"></asp:ScriptManager>
        <div class="panel panel-red">
            <div class="panel-heading">
                <div>
                    <h4>Revaluation</h4>
                </div>
            </div>
            <div class="panel-body">
                <div class="container-fluid">
                    <div class="row">
                        <div class="col-lg-12">
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

                    </div>

                    <div class="row" style="margin-top: 10px">
                        <div class="col-lg-10 col-lg-offset-1">
                            <div class="col-lg-4">
                                Pattern:-
                            <asp:DropDownList ID="drp_dwn_pattern" runat="server" CssClass="form-control" OnSelectedIndexChanged="drp_dwn_pattern_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem Value="CBGS">CBGS</asp:ListItem>
                                <asp:ListItem Value="CBCS">CBCS</asp:ListItem>
                                <asp:ListItem Value="C SCHEME">C SCHEME</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            <div class="col-lg-4">
                                Semester:-
                            <asp:DropDownList ID="drp_dwn_sem" runat="server" CssClass="form-control" OnSelectedIndexChanged="drp_dwn_sem_SelectedIndexChanged" AutoPostBack="true" Enabled="false">
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
                            <div class="col-lg-2">
                                Seat No:
                                 <asp:TextBox CssClass="form-control" ID="txt_seat" runat="server"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                    <div class="row" style="margin-top: 20px">
                        <div class="col-lg-8 col-lg-offset-2">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false"  Height="100%" BorderColor="#D9534F" ShowHeaderWhenEmpty="true" CssClass="table table-container table-bordered table-hover" Style="font-family: sans-serif" HeaderStyle-BackColor="#DFF0D8" HeaderStyle-ForeColor="#3C763D" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Check for Reval">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="reval_chk" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name Of Subject">
                                        <ItemTemplate>
                                            <asp:Label ID="name_of_subj" runat="server" Text='<%# Bind("Subject_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Question Paper Code" ControlStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="q_ppr_cd" runat="server" CssClass="form-control" Text='<%# Bind("paper_code") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Marks Obtained" ControlStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="mrks_obt" runat="server" CssClass="form-control" Text='<%# Bind("marks_obtained") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                            </asp:GridView>
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px">
                        <div class="col-lg-8 col-lg-offset-2">
                            <div class="panel" id="university" runat="server" visible="false">
                                <div class="panel panel-heading">
                                    <div class="row">
                                        <h4 style="float: left; margin-left: 10px;">University Exam </h4>
                                        <div class="col-md-3" style="float: right;">
                                            <asp:Button ID="btn_add" runat="server" CssClass="btn btn-block btn-success" Text="Add Subject" OnClick="btn_add_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="panel panel-body">
                                <asp:GridView ID="grdfees" runat="server" Font-Size="10pt" Style="text-align: center;" AutoGenerateColumns="False" CssClass="table table-bordered table-hover" OnRowDeleting="grdfees_RowDeleting">
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <RowStyle HorizontalAlign="Center" Height="10px"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Name Of Subject">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_subject" runat="server" CssClass="form-control" onkeypress="return subject(event)" MaxLength="100" OnTextChanged="txt_subject_TextChanged" AutoPostBack="true" Text='<%# Eval("Subject_Name")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="50%" />
                                            <HeaderStyle Width="50%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Question Paper Code">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_questpaper" runat="server" CssClass="form-control" onkeypress="return alphanumeric(event)" MaxLength="15" autocomplete="off" AutoPostBack="true" OnTextChanged="txt_questpaper_TextChanged" Text='<%# Eval("paper_code")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" />
                                            <HeaderStyle Width="20%" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Marks Obtained">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_marksobtained" runat="server" CssClass="form-control" onkeypress="return alphanumeric(event)" MaxLength="4" autocomplete="off" AutoPostBack="true" OnTextChanged="txt_marksobtained_TextChanged" Text='<%# Eval("marks_obtained")%>'></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Width="20%" />
                                            <HeaderStyle Width="20%" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="DELETE">
                                            <ItemTemplate>
                                                <asp:Button ID="btndel" runat="server" Text="Remove" CommandName="delete" CssClass="btn btn-block btn-danger" />
                                            </ItemTemplate>
                                            <ItemStyle Width="10%" />
                                            <HeaderStyle Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>

                                </asp:GridView>

                            </div>
                        </div>
                        
                    </div>

                    <div class="row">
                       
                        <div class="col-md-4"></div>
                     
                        <div class="col-lg-4">
                            <asp:Button ID="save_btn" runat="server" CssClass="form-control btn-success" OnClick="save_btn_Click" Text="Save" />
                        </div>
                             
                        <div class="col-md-4"></div>
                        <div class="col-lg-4">
                            <asp:Button ID="print_btn" runat="server" CssClass="form-control btn-primary" OnClick="print_btn_Click" Text="Print" Enabled="false" Visible="false" />
                        </div>
                    </div>

                </div>
            </div>
        </div>
        
        <div id="fyModal" class="modal fade" role="dialog">
            <div class="modal-dialog">

                <!-- 
                         content-->
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

                                <asp:Button ID="Button1" CssClass="btn btn-success" Text="Print Form" runat="server" Width="100%" OnClick="Button1_Click" />
                            </div>
                        </div>
                    </div>

                </div>

            </div>
        </div>
    </form>

</asp:Content>

