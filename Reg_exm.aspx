<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reg_exm.aspx.cs" Inherits="Reg_exm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                    <h4>REGULAR EXAM</h4>
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
                            <div class="col-lg-3">
                                Branch:-
                            <asp:DropDownList ID="drp_dwn_branch" runat="server" CssClass="form-control" OnSelectedIndexChanged="drp_dwn_branch_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem>COMPUTER</asp:ListItem>
                                <asp:ListItem>CIVIL</asp:ListItem>
                                <asp:ListItem>MECHANICAL</asp:ListItem>
                                <asp:ListItem>EXTC</asp:ListItem>
                                <asp:ListItem>ELECTRICAL</asp:ListItem>
                                <asp:ListItem>CSE (AI AND ML)</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
                                Pattern:-
                            <asp:DropDownList ID="drp_dwn_pattern" runat="server" CssClass="form-control" OnSelectedIndexChanged="drp_dwn_pattern_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>--SELECT--</asp:ListItem>
                                <asp:ListItem Value="CBGS">CBGS</asp:ListItem>
                                <asp:ListItem Value="CBCS">CBCS</asp:ListItem>
                                <asp:ListItem Value="C SCHEME">C SCHEME</asp:ListItem>
                            </asp:DropDownList>
                            </div>
                            <div class="col-lg-3">
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
                        </div>
                    </div>

                    <div class="row" style="margin-top: 20px">
                        <div class="col-lg-6 col-lg-offset-3">
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Height="100%" OnRowDataBound="GridView1_RowDataBound" BorderColor="#D9534F" ShowHeaderWhenEmpty="true" CssClass="table table-container table-bordered table-hover" Style="font-family: sans-serif" HeaderStyle-BackColor="#DFF0D8" HeaderStyle-ForeColor="#3C763D" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sr No.">
                                        <ItemTemplate>
                                            <asp:Label ID="sr_no" runat="server" Text='<%# Bind("Sr_No") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Name Of Subject">
                                        <ItemTemplate>
                                            <asp:Label ID="name_of_subj" runat="server" Text='<%# Bind("Subject_Name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Question Paper Code" ControlStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="q_ppr_cd" runat="server" CssClass="form-control" Text='<%# Bind("paper_code") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Marks Obtained" ControlStyle-Width="200px">
                                        <ItemTemplate>
                                            <asp:TextBox ID="mrks_obt" runat="server" CssClass="form-control" Text='<%# Bind("marks_obtained") %>'></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
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
                            <asp:Button ID="print_btn" runat="server" CssClass="form-control btn-primary" OnClick="print_btn_Click" Text="Print" Enabled="false" />
                        </div>
                    </div>

                </div>
            </div>
        </div>

    </form>
</asp:Content>

