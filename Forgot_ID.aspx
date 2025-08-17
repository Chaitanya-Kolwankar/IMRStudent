<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Forgot_ID.aspx.cs" Inherits="Forgot_ID" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>


    <meta http-equiv="imagetoolbar" content="no" />
    <link rel="shortcut icon" href="images/vivalogo.png" />
    <style type="text/css">
        .info, .success, .warning, .error, .validation {
            border: 1px solid;
            margin: 10px 0px;
            padding: 15px 10px 15px 50px;
            background-repeat: no-repeat;
            background-position: 10px center;
        }

        .info {
            color: #00529B;
            background-color: #BDE5F8;
            background-image: url('images/info.png');
        }

        .success {
            color: #4F8A10;
            background-color: #DFF2BF;
            background-image: url('images/success.png');
        }

        .warning {
            color: #9F6000;
            background-color: #FEEFB3;
            background-image: url('images/warning.png');
        }

        .error {
            color: #D8000C;
            background-color: #FFBABA;
            background-image: url('images/error.png');
        }
    </style>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Reset Password</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">
    <link href="css/datepicker.css" rel="stylesheet" />
    <!-- MetisMenu CSS -->
    <link href="css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/sb-admin-2.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css">

    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <script src="//oss.maxcdn.com/jquery.mask/1.6.0/jquery.mask.min.js"></script>
    <%-- <script>
        $(document).ready(function () {
            $('#maskForm')
                .bootstrapValidator({
                    feedbackIcons: {
                        valid: 'glyphicon glyphicon-ok',
                        invalid: 'glyphicon glyphicon-remove',
                        validating: 'glyphicon glyphicon-refresh'
                    },
                    fields: {
                        dob: {
                            validators: {
                                ip: {
                                    message: 'Date Of birth is not valid. Please enter in DD/MM/YYYY format'
                                }
                            }
                        }
                    }
                })
                .find('[name="dob"]').mask('09/09/0099');
        });
    </script>--%>

    <script type="text/javascript">
        $(function () {
            $('#datetimepicker5').datetimepicker({
                pickTime: false
            });
        });
    </script>
    <script type="text/javascript">
        $(function () {
            $('#datetimepicker1').datetimepicker();
        });
    </script>


    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-35865948-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>


</head>
<body style="background-color:#124a72">
    <div class="container">
        <div class="row">
            <div class="col-md-4 col-md-offset-4" style="margin-top:60px">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title">Forgot ID / password?</h3>
                    </div>
                    <div class="panel-body">
                        <form id="Form1" role="form" runat="server">
                            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                            <%--<div class="form-group">
                                    <input class="form-control" id="inp_stud_id" placeholder="Student ID" runat="server" autofocus>
                                </div>--%>
                            <div id="maskForm" method="post" class="form-horizontal">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                        <div class="form-group">

                                            <h5>Date of Birth:</h5>
                                            <div class="dropdown">
                                                <asp:DropDownList CssClass="form-control" class="col-lg-4 margin-text-drop btn btn-default dropdown-toggle " type="button" data-toggle="dropdown" ID="DropDownList2" AutoPostBack="true" runat="server">
                                                    <asp:ListItem>Day</asp:ListItem>
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                    <asp:ListItem>08</asp:ListItem>
                                                    <asp:ListItem>09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>

                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>
                                                    <asp:ListItem>13</asp:ListItem>
                                                    <asp:ListItem>14</asp:ListItem>
                                                    <asp:ListItem>15</asp:ListItem>
                                                    <asp:ListItem>16</asp:ListItem>
                                                    <asp:ListItem>17</asp:ListItem>
                                                    <asp:ListItem>18</asp:ListItem>
                                                    <asp:ListItem>19</asp:ListItem>
                                                    <asp:ListItem>20</asp:ListItem>

                                                    <asp:ListItem>21</asp:ListItem>
                                                    <asp:ListItem>22</asp:ListItem>
                                                    <asp:ListItem>23</asp:ListItem>
                                                    <asp:ListItem>24</asp:ListItem>
                                                    <asp:ListItem>25</asp:ListItem>
                                                    <asp:ListItem>26</asp:ListItem>
                                                    <asp:ListItem>27</asp:ListItem>
                                                    <asp:ListItem>28</asp:ListItem>
                                                    <asp:ListItem>29</asp:ListItem>
                                                    <asp:ListItem>30</asp:ListItem>

                                                    <asp:ListItem>31</asp:ListItem>




                                                </asp:DropDownList>
                                                <asp:DropDownList CssClass="form-control" class="col-lg-4 margin-text-drop btn btn-default dropdown-toggle " type="button" data-toggle="dropdown" ID="DropDownList1" AutoPostBack="true" runat="server">
                                                    <asp:ListItem>Month</asp:ListItem>
                                                    <asp:ListItem>01</asp:ListItem>
                                                    <asp:ListItem>02</asp:ListItem>
                                                    <asp:ListItem>03</asp:ListItem>
                                                    <asp:ListItem>04</asp:ListItem>
                                                    <asp:ListItem>05</asp:ListItem>
                                                    <asp:ListItem>06</asp:ListItem>
                                                    <asp:ListItem>07</asp:ListItem>
                                                    <asp:ListItem>08</asp:ListItem>
                                                    <asp:ListItem>09</asp:ListItem>
                                                    <asp:ListItem>10</asp:ListItem>
                                                    <asp:ListItem>11</asp:ListItem>
                                                    <asp:ListItem>12</asp:ListItem>



                                                </asp:DropDownList>
                                                <asp:DropDownList CssClass="form-control" class="col-lg-4 margin-text-drop btn btn-default dropdown-toggle " type="button" data-toggle="dropdown" ID="DropDownList3" AutoPostBack="true" runat="server">
                                                    <asp:ListItem>Year</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                            <%-- <input data-provide="datepicker" type="text" placeholder="Date of Birth"  data-date-format="mm/dd/yyyy" class="form-control" name="dob " /--%>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <div class="form-group">
                                    <input id="inp_mothers_name" runat="server" class="form-control" placeholder="Enter Mother's Name" name="mother's name" type="text" value="" />
                                </div>
                                 <div class="form-group">
                                    <input id="txtMobNo" runat="server" class="form-control" placeholder="Mobile No" name="mother's name" type="text" value="" />
                                </div>

                                <%--<div class="form-group">
                                    <input id="inp_new_pswd" runat="server" class="form-control" placeholder="Enter New Password" name="password" type="password" value="">
                                </div>--%>
                                <%--<div class="form-group">
                                    <input id="inp_cnf_new_pswd" runat="server" class="form-control" placeholder="Confirm New Password" name="password" type="password" value="">
                                </div>--%>
                                <!-- Change this to a button or input when using this as a form -->
                                <asp:Button ID="btnLogin" runat="server" Text="Submit" CssClass="btn btn-lg btn-success btn-block" OnClick="btnLogin_Click" />
                                <div class="form-group">
                                    <%--<asp:Label ID="lbl_id" runat="server" Text="" Visible="false"></asp:Label>--%>
                                    <div id="lbl_id" runat="server" class="alert alert-danger" visible="false">

                                    </div>
                                      <%--<asp:Label ID="lbl_passwd" runat="server" Text="" CssClass="form-control" Visible="false"></asp:Label><br />--%>
                                     <asp:Label ID="lbl_message" runat="server" Text="" CssClass="form-control" Visible="false"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <div class="form-control">
                                        <center> <a style="font-weight:700" href="Login.aspx">To Login Click Here</a></center>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>



        <div id="alert_apply" class="borders margin col-xs-12 col-lg-12 alert alert-success" visible="false" runat="server">
            <a href="#" class="close" data-dismiss="alert">&times;
            </a>Your New Password has been set
   <strong>Successfully.</strong>
            <%--<a href="Login.aspx">Back to Login</a>--%>
        </div>




        <div id="alert_danger" class="borders margin col-xs-12 col-lg-12 alert alert-danger" role="alert" visible="false" runat="server">
            <a href="#" class="close" data-dismiss="alert">&times;
            </a>
            <strong id="strong_msg" runat="server">Each field is Compulsory</strong> <%--<a href="Login.aspx">Back to Login</a>--%>
        </div>

        <div id="alert_pswd" class="borders margin col-xs-12 col-lg-12 alert alert-danger" role="alert" visible="false" runat="server">
            <a href="#" class="close" data-dismiss="alert">&times;
            </a>
            <strong>Password does not Match</strong> <%--<a href="Login.aspx">Back to Login</a>--%>
        </div>

        <div id="alert_incorrect" class="borders margin col-xs-12 col-lg-12 alert alert-danger" role="alert" visible="false" runat="server">
            <a href="#" class="close" data-dismiss="alert">&times;
            </a>
            <strong>Invalid input. Please enter correct data.</strong> <%--<a href="Login.aspx">Back to Login</a>--%>
        </div>



    </div>
</body>
</html>
