<%@ Page Language="C#" AutoEventWireup="true" CodeFile="railwayIAgree.aspx.cs" Inherits="railwayIAgree" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Student</title>
    <!-- Bootstrap Core CSS -->
    <link href="css/bootstrap.min.css" rel="stylesheet">

    <!-- MetisMenu CSS -->
    <link href="css/plugins/metisMenu/metisMenu.min.css" rel="stylesheet">

    <!-- Custom CSS -->
    <link href="css/sb-admin-2.css" rel="stylesheet">

    <!-- Custom Fonts -->
    <link href="font-awesome-4.1.0/css/font-awesome.min.css" rel="stylesheet" type="text/css">
      <script type="text/javascript" language="javascript">

          function DisableBackButton() {
              window.history.forward()
          }
          DisableBackButton();
          window.onload = DisableBackButton;
          window.onpageshow = function (evt) { if (evt.persisted) DisableBackButton() }
          window.onunload = function () { void (0) }
</script>
</head>
<body>

    <div class="panel panel-default">
        <div class="panel-body">
                <div class="row" style="margin-top:15px">
                    <div class="col-lg-4 col-md-4 "></div>
                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                           <div class="panel panel-info">
                            <div class="panel-heading">
                             <div class="row" id="img" runat="server">
                              <div class="col-sm-6 col-md-10 col-lg-12">
                                <div class="thumbnail" style="align-content:center">
                                  <img data-src="holder.js/300x300" src="images/at.png" alt="...">
                                </div>
                              </div>
                             </div>
                                <br />
                                <div class="panel-title">Railway Concession Terms & Conditions</div>
                            </div>
                             <div class="panel-body">
                                <form id="Form2" role="form" runat="server">
                                        <div class="alert alert-danger">
                                           <ol>
                                               <li>
                                                   Once the request is approved, I will collect it within 2 days from the date of approval.
                                               </li>
                                              <%-- <li>
                                                   If in case I fail to collect the concession within the desired period then 
                                               </li>--%>
                                               <li>
                                                   I will not misuse the facility and will not apply for fake concession.
                                               </li>
                                           </ol>
                                        </div>                                        
                                        <div class="row">

                                            <div class="col-lg-12">
                                                <div class="form-group"><asp:CheckBox ID="chkIAgree" runat="server" /> I Agree to the terms and condition.</div>
                                            </div>
                                        </div>
                                    <div class="row">
                                        <div class="col-lg-6">
                                        <asp:Button ID="btnIAgree" runat="server" Text="Continue"   CssClass="btn btn-success btn-block" OnClick="btnIAgree_Click" />
                                            </div>
                                         <div class="col-lg-6">
                                        <asp:Button ID="btnBack" runat="server" Text="Cancel"   CssClass="btn btn-primary btn-block" OnClick="btnBack_Click" />
                                            </div>
                                        </div>
                                   
                                        </div>
                                    <br />
                                    <div class="row">
                                        <div class="col-lg-12">
                                        <div class="alert alert-danger" runat="server" id="errorMsg"></div>
                                        </div>
                                        </div>
                                </form>
                             </div>
                           </div>
                        </div>
                </div>
           
        </div>
    </div>

    <!-- jQuery -->
    <script src="js/jquery.js"></script>

    <!-- Bootstrap Core JavaScript -->
    <script src="js/bootstrap.min.js"></script>

    <!-- Metis Menu Plugin JavaScript -->
    <script src="js/plugins/metisMenu/metisMenu.min.js"></script>

    <!-- Custom Theme JavaScript -->
    <script src="js/sb-admin-2.js"></script>
</body>
</html>
