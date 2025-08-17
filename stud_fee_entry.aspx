<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="stud_fee_entry.aspx.cs" Inherits="stud_fee_entry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form id="form" runat="server">
        <div class="col-md-2" runat="server" id="stud_id">
            <asp:TextBox ID="txt_studid" runat="server" CssClass="form-control" placeholder="Student ID" MaxLength="8" onkeypress="return isNumber(event)" Text=""></asp:TextBox>
        </div>
    </form>


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

