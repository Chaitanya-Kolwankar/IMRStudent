<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="~/Adm_provisional.aspx.cs" Inherits="Adm_provisional" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
      <form id="Form1" runat="server" style="margin-top: 15px">
      <div class="panel panel-primary">
        <div class="panel-heading">
            <div class="row">
                <div class="col-lg-8">
                    <span style="font-family: Verdana; font-size: 12pt"><strong>Provisional Admission</strong></span>
                </div>
            </div>
        </div>
        <div class=" panel panel-body">
            <div class="row">
                <div class="col-lg-3">
                    Student ID
                    <asp:TextBox ID="txt_studid" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-lg-3">
                    Student Name
                    <asp:TextBox ID="txt_studname" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                </div>
                <div class="col-lg-3">
                    Amount
                    <asp:TextBox ID="txt_amt" runat="server" Enabled="false" Text="1602" CssClass="form-control"></asp:TextBox>
                </div>
                <div class="col-lg-3">
                    <br />
                    <asp:Button ID="Button1" runat="server" Text="Payment" CssClass="btn btn-success" OnClick="btn_pay_Click" Width="100%" />

                </div>
                </div>
            <br />
            <div class="row" style="display:none;">
                <div class="col-lg-3"></div>
                <div class="col-lg-3"><asp:Button ID="btn_pay" runat="server" Text="Payment" CssClass="btn btn-success" OnClick="btn_pay_Click" Width="100%" /></div>
                <div class="col-lg-3"><asp:Button ID="btn_reset" runat="server" Text="Reset" CssClass="btn btn-success" Width="100%" /></div>
                <div class="col-lg-3"></div>
            </div>
            </div>
          </div>
          </form>
</asp:Content>

