<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="upload_lc.aspx.cs" Inherits="upload_lc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <form runat="server">
        <br/>
        <div class="panel panel-primary">
            <div class="panel-heading">
                <span style="font-family: Verdana; font-size: 18pt; padding: 10px;"><strong>Upload Leaving Certificate</strong></span>
            </div>
            <div class="panel panel-body">
                <div class="row" style="color:red;text-align:center">
                    <i><h5>NOTE:File must be in PDF format only (Maximum Size 200kb)</h5></i>
                    </div>
                <div class="row">
                    <div class="col-lg-6">
                        <asp:FileUpload ID="filephoto" TabIndex="1" CssClass="form-control" runat="server" accept="image/*" ToolTip="Upload Photo"></asp:FileUpload>
                    </div>
                    <div class="col-lg-6">
                        <asp:Button ID="btnuploadphoto" TabIndex="2" runat="server" OnClick="btnuploadphoto_Click" Text="Upload Photo" class="form-control btn-default"></asp:Button>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

