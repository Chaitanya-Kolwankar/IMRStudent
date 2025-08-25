<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html>
<html lang="en">

<head>
    <meta http-equiv="imagetoolbar" content="no" />
    <link rel="shortcut icon" href="images/mu.png" />
    <link href="css/plugins/bootstrap5.min.css" rel="stylesheet" />
    
    <style>
        body {
            background-image: url("images/backimg5.jpg ");
            background-repeat: no-repeat;
            background-size: cover;
        }

        .row > * {
            padding-right: 0;
        }
    </style>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <meta name="description" content="">
    <meta name="author" content="">

    <title>Login</title>

    <!-- Bootstrap Core CSS -->
    <%--<link href="css/bootstrap.min.css" rel="stylesheet">--%>

    <!-- MetisMenu CSS -->
    <link href="css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/sb-admin-2.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css">

</head>


<body>
    <%--<div class="container vh-100 d-flex flex-column justify-content-center align-items-center" style="margin-top: 110px">

        <div class="card"></div>

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
                    <div class="alert alert-light text-center footer-note" role="alert">
                        Help Line Number: <strong>8408931470</strong><br>
                        Timing: 10 AM to 5 PM Only
                    </div>
                </form>
            </div>
        </div>
    </div>--%>






    <div class="container">

        <section class="section register min-vh-100 d-flex flex-column align-items-center justify-content-center py-1">


            <div class="container">
                <div class="row justify-content-center">
                    <div class="col-lg-4 col-md-6 d-flex flex-column align-items-center justify-content-center" style="background-color: rgba(0, 0, 0, .5); color: white; border-radius: 10px">
                        <div class="d-flex justify-content-center">

                            <span class="d-lg-block">
                                <br />
                                <img src="images/RGCMS.png" alt="" style="max-height: 100PX !IMPORTANT;" /></span>


                        </div>
                        <!-- End Logo -->
                        <div class="d-flex justify-content-center">
                            <%--<span class="d-none d-lg-block"><img src="assets/img/logo.jpg" alt="" style="max-height:50PX !IMPORTANT"/></span>--%>
                            <div class="logo d-flex align-items-center w-auto">

                                <span class="d-lg-block pt-2" style="color: white; font-weight: 600;text-align:center">Rajeev Gandhi College of Management Studies</span>
                            </div>
                        </div>
                        <!-- End Logo -->

                        <%--<div class="card mb-3">--%>

                        <div class="card-body">

                            <div class="">
                                <h5 class="card-title text-center pb-0 fs-4" style="color: white">Login to Student Account</h5>

                            </div>
                            <form id="Form1" runat="server" class="row g-3 needs-validation" novalidate>

                                <div class="col-12">
                                    <label for="yourUsername" class="form-label">Student ID</label>
                                    <div class="input-group has-validation">
                                        <asp:TextBox runat="server" type="text" name="username" MaxLength="8" autocomplete="off" class="form-control" ID="txtUserName" Style="text-transform: uppercase;" onkeyPress="return alphaandnum(event)" required oncopy="return false"
                                            oncut="return false" />
                                        <div class="invalid-feedback">Please enter your Student ID.</div>
                                    </div>
                                </div>

                                <div class="col-12">
                                    <label for="yourPassword" class="form-label">Password</label>
                                    <asp:TextBox runat="server" MaxLength="20" type="password" name="password" class="form-control" ID="txtPassword" oncopy="return false" oncut="return paste" required />
                                    <div class="invalid-feedback">Please enter your password!</div>
                                </div>
                                <div class="col-12">
                                    <div id="div_valid" visible="false" runat="server" class="validation text-center" style="font-weight:700"></div>
                                </div>
                                <div class="text-right" style="margin-bottom: 10px">
                                    <a href="Forgot_ID.aspx" style="color: #e7e7e7;float:right">Forgot ID / Password?</a>
                                </div>
                                <div class="col-12">
                                    <%--<asp:Button runat="server" ID="btnLogin" class="btn btn-primary w-100" OnClick="btnLogin_Click" Text="Submit" />--%>
                                    <asp:Button ID="btnLogin" runat="server" Text="Login" CssClass="btn btn-lg btn-success btn-block form-group" OnClick="btnLogin_Click1" Style="margin-bottom: 8px; background-color: #1a5d8c; border: none;width:100%" />
                                </div>
                                <div class="credits container" style="text-align: center;">
                                </div>
                            </form>

                        </div>
                    </div>



                </div>
            </div>


        </section>

    </div>



</body>


<!-- jQuery -->
<script src="js/jquery.js"></script>

<!-- Bootstrap Core JavaScript -->
<script src="js/bootstrap.min.js"></script>

<!-- Metis Menu Plugin JavaScript -->
<script src="js/plugins/metisMenu/metisMenu.min.js"></script>

<!-- Custom Theme JavaScript -->
<script src="js/sb-admin-2.js"></script>



</html>

