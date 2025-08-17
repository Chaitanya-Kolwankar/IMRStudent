<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta http-equiv="imagetoolbar" content="no" />
    <link rel="shortcut icon" href="images/mu.png" />
    <style type="text/css">
        .info, .success, .warning, .error, .validation {
            border: 1px solid;
            margin: 10px 0px;
            padding: 15px 10px 15px 50px;
            background-repeat: no-repeat;
            background-position: 10px center;
        }

        .validation{
            padding: 15px 10px 15px 10px;
            display:flex;
            justify-content:center;
            font-weight:900;
            color:#ccc;
            border:none;
            padding:0;
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
    <style>
        body {
            /*background: url('images/backimg1.jpg') no-repeat center center fixed;*/
            background-color:#124a72 !important;
            background-size: cover;
            font-family: 'Segoe UI', sans-serif;
        }

        .login-card {
            background-color: rgba(255, 255, 255, 0.95);
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0px 5px 15px rgba(0,0,0,0.3);
        }

        .institute-header h5, .institute-header h4 {
            margin: 5px 0;
        }

        .footer-note {
            font-size: 14px;
            color: #e7e7e7;
        }

        .form-control{
            background-color:#e7e7e7 !important;
        }

        .form-control::placeholder{
            color:#1a5d8c;
            opacity:1;
        }

        #txtPassword::placeholder,#txtUserName::placeholder {
    color: #1a5d8c; 
}

    </style>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Login</title>

    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

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

</head>


<body>
    <div class="container vh-100 d-flex flex-column justify-content-center align-items-center" style="margin-top: 110px">
        <div class="text-center mb-4 institute-header">
            <img src="images/RGCMS.png" height="100" alt="Mu Logo">
            <h4 class="text-dark" style="color:#ccc;font-size:x-large;text-transform:uppercase;margin-top:15px">Rajeev Gandhi College of Management Studies</h4>
            <h4 class="text-secondary" style="color:#ccc;font-size:x-large">Student Portal</h4>
        </div>

        <div style="display: flex; justify-content: center; margin-top: 3pc">
            <div class="col-md-6 col-lg-4">
                <h3 class="text-center mb-4" style="color:#ccc;font-size:45px;font-family:'Source Sans 3';margin-bottom:24px"><strong>Student Login</strong></h3>
                <form method="post" runat="server">
                    <div class="mb-3 form-group">
                        <asp:TextBox class="form-control" ID="txtUserName" runat="server" placeholder="Student ID" autofocus></asp:TextBox>
                    </div>
                    <div class="mb-3 form-group">
                        <asp:TextBox class="form-control" placeholder="Password" runat="server" ID="txtPassword" type="password"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <div id="div_valid" visible="false" runat="server" class="validation"></div>
                    </div>
                    <div class="text-right" style="margin-bottom:10px">
                        <a href="Forgot_ID.aspx" style="color:#e7e7e7">Forgot ID / Password?</a>
                    </div>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-lg btn-success btn-block form-group" OnClick="btnLogin_Click1" Style="margin-bottom: 8px;background-color:#1a5d8c;border:none" />
                    <%--<div class="alert alert-light text-center footer-note" role="alert">
                        Help Line Number: <strong>8408931470</strong><br>
                        Timing: 10 AM to 5 PM Only
                    </div>--%>
                </form>
            </div>
        </div>
    </div>
</body>




<%--<body>

    <div class="panel panel-default">
        <div class="">
            <center>            
                <div style="margin-top:15px"> 
               <!--//logo-->
                       <center>  <img id="Img1" src="images/vivalogo.png" height="100" alt="Logo" />
                       <p style="font-family: 'Times New Roman';font-size:15px;text-align:center">
                           <b><h5>Shri. Vishnu Waman Thakur Charitable Trust's</h5></b>
                            <h4 style="box-sizing: border-box; font-family: &quot;Times New Roman&quot;; font-weight: 500; line-height: 1.1; color: rgb(51, 51, 51); margin-top: 10px; margin-bottom: 10px; font-size: 15pt; font-style: normal; font-variant: normal; letter-spacing: normal; orphans: auto; text-align: -webkit-center; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px; background-color: rgb(245, 245, 245);">VIVA INSTITUTE OF TECHNOLOGY</h4>
                           <h4 style="font-size:15pt;font-family:'Verdana';color:gray">Student Portal Login</h4>
                       </p>
                       </center>
                 </div>
            </center>
        </div>
        <div class="panel-body" style="background-image: url('images/background/background2.jpg');background-size: cover;background-repeat: no-repeat;background-attachment: fixed;height:100vh">
            <center>    
                <div class="row" style="margin-top:15px">
                    <div class="col-lg-4 col-md-4 "></div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                           <div class="panel panel-info">
                            <div class="panel-heading">
                            <h3 class="panel-title">Please Sign In</h3>
                            </div>
                             <div class="panel-body">
                        <form id="Form1" role="form" runat="server">
                                <div class="form-group">
                                    <asp:TextBox class="form-control" id="txtUserName" runat="server" placeholder="Student ID" autofocus></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <asp:TextBox class="form-control" placeholder="Password" runat="server" id="txtPassword" type="password"></asp:TextBox>
                                </div>
                                 <div class="form-group">
                                    <div id="div_valid" visible="false" runat="server" class="validation"></div>
                                </div>
                                <!-- Change this to a button or input when using this as a form -->
                            <asp:Button ID="btnLogin" runat="server" Text="Login"   CssClass="btn btn-lg btn-success btn-block" OnClick="btnLogin_Click1"/>
                                 <div style="margin-top:10px">
                                    <center><label id="lbl_reset">
                                        <a href="Forgot_ID.aspx">Forgot ID / Password?</a>
                                    </label>
                                        </center>
                                </div>
                            <div style="margin-top:10px">
                                <div class="well">
                                    <center>
                                    <label id="Label1">
                                        Help Line Number : 8408931470<br /> Timing 10 AM To 5 PM Only
                                    </label>
                                        </center>
                                </div>
                                </div>
                        </form>
                    </div>
                           </div>
                        </div>
                </div>
            </center>
        </div>
    </div>


    </body>--%>
<!-- jQuery -->
<script src="js/jquery.js"></script>

<!-- Bootstrap Core JavaScript -->
<script src="js/bootstrap.min.js"></script>

<!-- Metis Menu Plugin JavaScript -->
<script src="js/plugins/metisMenu/metisMenu.min.js"></script>

<!-- Custom Theme JavaScript -->
<script src="js/sb-admin-2.js"></script>



</html>

