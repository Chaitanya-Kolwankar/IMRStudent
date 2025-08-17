<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="change.aspx.cs" Inherits="change" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form role="form" runat="server">

        <%--<div class="row" style="margin-top: 20px">
            <div class="col-lg-4">
                <div class="row">
                    <div class="col-lg-10">
                        <div class="form-group">
                            <input type="password" runat="server" id="txtoldPass" class="form-control" placeholder="Enter Old Password" />
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="form-group">
                            <input type="password" runat="server" id="txtNewPass" class="form-control" placeholder="Enter New Password" />
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="form-group">
                            <input type="password" runat="server" id="txtConfirm" class="form-control" placeholder="Confirm New Password" />
                        </div>
                    </div>
                    <div class="col-lg-10">
                        <div class="alert alert-danger" id="message" runat="server">
                        </div>
                    </div>

                    <div class="col-lg-10" style="text-align: right">
                        <div class="form-group">
                            <asp:Button type="submit" ID="btnChange" Text="Change" runat="server" class="btn btn-default" OnClick="btnChange_Click"></asp:Button>
                            <asp:Button type="reset" runat="server" Text="Clear" ID="btnReset" class="btn btn-default" OnClick="btnReset_Click"></asp:Button>
                        </div>
                    </div>
                </div>
            </div>
        </div>--%>

        <div class="row" style="margin-top: 20px;">
            <div class="col-lg-4">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        <h3 class="panel-title">Change Password</h3>
                    </div>
                    <div class="panel-body">
                        <div class="form-group">
                            <input type="password" runat="server" id="txtoldPass" class="form-control" placeholder="Enter Old Password" />
                        </div>

                        <div class="form-group">
                            <input type="password" runat="server" id="txtNewPass" class="form-control" placeholder="Enter New Password" />
                        </div>

                        <div class="form-group">
                            <input type="password" runat="server" id="txtConfirm" class="form-control" placeholder="Confirm New Password" />
                        </div>

                        <div class="form-group">
                            <div class="alert alert-danger" id="message" runat="server" visible="false">
                                <!-- Error message -->
                            </div>
                        </div>
                    </div>
                    <div class="panel-footer text-right">
                        <asp:Button type="submit" ID="btnChange" Text="Change" runat="server" class="btn btn-primary" OnClick="btnChange_Click"></asp:Button>
                        <asp:Button type="reset" runat="server" Text="Clear" ID="btnReset" class="btn btn-default" OnClick="btnReset_Click"></asp:Button>
                    </div>
                </div>
            </div>
        </div>

    </form>
</asp:Content>

