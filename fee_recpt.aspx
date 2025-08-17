<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="fee_recpt.aspx.cs" Inherits="fee_recpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <script src="js/jquery-min.js"></script>

    <form id="Form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"></asp:ScriptManager>
        <br />
        <div class="container-fluid">
            <div class="panel panel-primary">
                <div class="panel-heading">Student Receipt</div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4 ">
                            Academic Year   
                    <select id="ddlayid" class="form-control"></select>
                        </div>
                    </div>
                    <div class="row" id="transaction" style="display: none; margin-top: 10px">
                        <div class="col-lg-12">
                            <div class="table-responsive" style="height: 100%; WIDTH: 100%; OVERFLOW-X: scroll;">
                                <table id="tbltransaction" class="table table-condensed table-bordered table-striped">
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </div>
    </form>

    <script src="JsForms/fee_recpt.js"></script>
    <script src="notify-master/js/notify.js"></script>
    <script type="text/javascript">
        var stud_id = '<%=Session["UserName"] %>'
       
    </script>
</asp:Content>

