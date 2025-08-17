<%@ Page Language="C#" AutoEventWireup="true" CodeFile="studentdocument_bot.aspx.cs" Inherits="studentdocument_bot" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="bootstrap-4.0.0-alpha.6-dist/css/bootstrap.min.css" rel="stylesheet" />
    <link href="font-awesome-4.5.0/css/font-awesome.min.css" rel="stylesheet" />
    <link href="notify-master/css/notify.css" rel="stylesheet" />
    <link href="css/bootstrap-multiselect.css" rel="stylesheet" />
    <script src="js/jquery-min.js"></script>
    <script src="js/bootstrap-multiselect.js"></script>
    <script src="notify-master/js/notify.js"></script>
    <script src="bootstrap-4.0.0-alpha.6-dist/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scrrr"></asp:ScriptManager>
        <div class="container" style="padding-top: 40px">
            <div class="card">
                <div class="card-header card-primary">
                    <div class="row">
                        <div class="col-md-6">
                            <h3 style="color: white">Document Upload Bot</h3>
                        </div>


                    </div>

                </div>
                <div class="card-body">

                    <div class="row" style="padding-top: 10px">


                        <div class="col-md-3">
                            <span>Academic Year</span>
                            <asp:DropDownList ID="ddl_ayid" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddl_ayid_SelectedIndexChanged" AutoPostBack="true"/>

                        </div>
                       <%-- <div class="col-md-3">
                            <asp:ListBox ID="ddl_course" runat="server" CssClass="select2_multiple form-control" SelectionMode="Multiple"></asp:ListBox>
                        </div>--%>
                         <div class="col-md-3">
                              <span>Course</span>
                            <asp:DropDownList ID="ddl_course" runat="server" CssClass="form-control" ></asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <asp:Button ID="btn_fetch" runat="server" CssClass="btn btn-success" Text="Upload" OnClick="btn_fetch_Click" />
                        </div>


                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
<%--<script type="text/javascript">
    $(function () {
        $('[id*=ddl_course]').multiselect({
            includeSelectAllOption: true
        });
    });
</script>--%>
</html>
