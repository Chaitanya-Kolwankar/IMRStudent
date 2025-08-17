<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FeeReceiptForm.aspx.cs" Inherits="FeeReceiptForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="panel panel-default" style="margin-top:20px">
        <div class="panel-heading">
            Application Forms
        </div>
        <div class="panel-body" style="font-size:12pt;font-weight:bold">
            <br />
            <a id="viewReceipt" runat="server" visible="false" href="feeReceiptFull.aspx" class="btn btn-outline btn-primary btn-lg btn-block" >View Fee Receipt</a>
            <br />
            <div id="divmsg" runat="server" class="alert alert-danger">

            </div>
        </div>
    </div>
</asp:Content>

