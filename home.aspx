<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">

        <div class="row" style="margin-top: 10px" id="div_admission" runat="server" visible="false">
            <div class="col-lg-6">
                <marquee behavior="alternate"><a href="admission.aspx" class="text1 blink_me">Online Admission</a></marquee>
            </div>
        </div>

        <div class="row" style="margin-top: 10px">

            <div class="col-lg-6 col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Important Notices
                    </div>
                    <div class="panel-body">
                        <div class="list-group" id="list_notice" runat="server">
                        </div>
                        <div class="table-responsive">

                            <asp:GridView ID="GridView1" runat="server" Width="100%" Height="100px" CssClass="table table-bordered" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                    <asp:BoundField DataField="title" HeaderText="Title" />

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkView" runat="server" Text="View / Download" OnClick="View" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="time" HeaderText="Time" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>

            <div class="col-lg-6 col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Notice from Principal                    </div>
                    <div class="panel-body">
                        <div class="list-group" id="listNoticePrincipal" runat="server">
                        </div>
                        <div class="table-responsive">

                            <asp:GridView ID="GridView2" runat="server" Width="100%" Height="100px" CssClass="table table-bordered" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                    <asp:BoundField DataField="title" HeaderText="Title" />

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkViewPrincipal" runat="server" Text="View / Download" OnClick="View" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="time" HeaderText="Time" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>

        </div>

        <div class="row" style="margin-top: 10px">

            <div class="col-lg-6 col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Notice from Office
                    </div>
                    <div class="panel-body">
                        <div class="list-group" id="listNoticeOffice" runat="server">
                        </div>
                        <div class="table-responsive">

                            <asp:GridView ID="GridView3" runat="server" Width="100%" Height="100px" CssClass="table table-bordered" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                    <asp:BoundField DataField="title" HeaderText="Title" />

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkViewOffice" runat="server" Text="View / Download" OnClick="View" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="time" HeaderText="Time" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>

            <div class="col-lg-6 col-md-6">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Notice from Staff
                    </div>
                    <div class="panel-body">
                        <div class="list-group" id="listNoticeStaff" runat="server">
                        </div>
                        <div class="table-responsive">

                            <asp:GridView ID="GridView4" runat="server" Width="100%" Height="100px" CssClass="table table-bordered" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField DataField="Id" HeaderText="Id" Visible="false" />
                                    <asp:BoundField DataField="title" HeaderText="Title" />

                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkViewStaff" runat="server" Text="View / Download" OnClick="View" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="time" HeaderText="Time" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>

        </div>

    </form>
</asp:Content>

