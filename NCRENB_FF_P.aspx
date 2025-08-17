<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NCRENB_FF_P.aspx.cs" Inherits="NCRENB_FF_P" %>
<%--<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="photocopy.aspx.cs" Inherits="photocopy" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<%--</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">--%>
      <form id="Form1" runat="server" style="margin-top: 15px">
      <div class="panel panel-primary">
        <div class="panel-heading">
            <div class="row">
                <div class="col-lg-8">
                    <span style="font-family: Verdana; font-size: 12pt"><strong>NCRENB</strong></span>
                </div>
            </div>
        </div>
        <div class=" panel panel-body">
            <div class="row">
                <div class="col-lg-4">
                    Paper ID
                    <asp:TextBox ID="txt_papid" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-4">
                    Paper Name
                    <asp:TextBox ID="txt_papname" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-4">
                    Amount
                    <asp:TextBox ID="txt_amt" runat="server" Enabled="false" Text="500" CssClass="form-control"></asp:TextBox>
                </div>
                </div>
            <br />
            <div class="row">
                <div class="col-lg-3"></div>
                <div class="col-lg-3"><asp:Button ID="btn_pay" runat="server" Text="Payment" CssClass="btn btn-success" OnClick="btn_pay_Click" Width="100%" /></div>
                <div class="col-lg-3"><asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-success" Width="100%" /></div>
                <div class="col-lg-3"></div>
            </div>
            </div>
          </div>
          </form>
</asp:Content>

