<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Fees_Receipt.aspx.cs" Inherits="Fees_Receipt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="<%= ResolveUrl("~/images/mu.png") %>" rel="icon" />
    <link href="<%= ResolveUrl("~/images/mu.png") %>" rel="icon" />
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>RGCMS | STUDENT FEES</title>
    <%----------------------------------------------- Plugin Start ------------------------------------------%>
    <script src="http://code.jquery.com/jquery-latest.min.js"></script>
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="vendors/bootstrap/dist/js/bootstrap.js"></script>
    <%------------------------------------------------ Plugin End -------------------------------------------%>

    <style>
        .font-bold {
            font-weight: bold;
        }

        .upperFont {
            font-weight: bold;
            font-size: 11px;
        }

        .watermark {
            color: #d0d0d0;
            position: absolute;
            margin-top: 43px;
            height: 180px;
            margin-bottom: 100px;
            margin-left: 120px;
            margin-right: 50px;
            z-index: 100;
            opacity: 0.2;
        }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 0.156em;
            border: 1px solid black;
            margin-bottom: -20px;
        }

        .itemCss {
            margin-left: 5px;
            font-size: 8px;
        }

        .table {
            border-collapse: collapse;
            margin-bottom: 10px;
        }

        @page {
            size: portrait;
        }

        .table > tbody > tr > td > span, .table > tbody > tr > th > span {
            padding-left: 5px;
        }
    </style>

    <style>
        .vertical_dotted_line {
            border-right: 1px solid black;
            height: 100%;
        }

        #GridView1 > tbody > tr:last-child {
            font-weight: normal !important;
        }

        #GridView2 > tbody > tr:last-child {
            font-weight: normal !important;
        }

        #grd_installment2 > tbody > tr {
            font-weight: bold !important;
        }

        #grd_installment > tbody > tr {
            font-weight: bold !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid" style="width: 100%; padding-top: 5px">
            <br />
            <div class="row">

                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="border: 1px solid black">



                    <div class="row">

                        <div class="col-md-3"></div>
                        <div class="col-md-6">

                            <img src="/images/RJC.png" style="margin-top: 8px; width: -webkit-fill-available;" alt="" />
                        </div>


                    </div>

                    <div class="row" style="border-top: 1px dotted black; margin-top: 8px; font-size: 10px">
                    </div>

                    <div>
                        <div class="watermark">
                            <img src="/images/mu.png" width="440px" height="380px" style="opacity: 0.4" />
                        </div>


                        <div class="row">
                            <center>
                                <span style="margin-bottom: 3px; font-size: 18px; font-family: 'Times New Roman', Times, serif; font-weight: bold">PAYMENT RECEIPT</span>
                            </center>
                        </div>
                        <div class="row">

                            <span style="float: right; font-size: 15px; font-family: 'Times New Roman', Times, serif; padding-right: 20px"><b>Date : </b>
                                <asp:Label ID="lbl_date1" runat="server" Text=""></asp:Label></span>
                        </div>
                        <table class="table">
                            <tr>

                                <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <span class="data"><b>Student ID:</b></span>
                                </th>
                                <td>
                                    <span class="data">
                                        <asp:Label ID="lblstudentid" runat="server" Text=""></asp:Label></span>
                                </td>
                            </tr>
                            <tr>

                                <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <span class="data"><b>Received From:</b></span>
                                </th>
                                <td>
                                    <span class="data">
                                        <asp:Label ID="lbl_name_1" runat="server" Text=""></asp:Label></span>
                                </td>
                            </tr>
                            <tr>

                                <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <span class="data"><b>Course:</b></span>
                                </th>
                                <td>
                                    <span class="data">
                                        <asp:Label ID="lblcourse" runat="server" Text=""></asp:Label></span>
                                </td>
                            </tr>
                            <tr>

                                <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <span class="data"><b>Total Amount In ₹:</b></span>
                                </th>
                                <td>
                                    <span class="data">
                                        <asp:Label ID="lblamountdigits" runat="server" Text=""></asp:Label></span>
                                </td>
                            </tr>
                            <tr>

                                <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <span class="data"><b>Total Amount In Words:</b></span>
                                </th>
                                <td>
                                    <span class="data">
                                        <asp:Label ID="lbl_amount_1" runat="server" Text=""></asp:Label></span>
                                </td>
                            </tr>
                            <tr>

                                <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <span class="data"><b>Transaction ID:</b></span>
                                </th>
                                <td>
                                    <span class="data">
                                        <asp:Label ID="lblvivatransction" runat="server" Text=""></asp:Label></span>
                                </td>
                            </tr>
                            <tr>

                                <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <span class="data"><b>Bank Transaction ID:</b></span>
                                </th>
                                <td>
                                    <span class="data">
                                        <asp:Label ID="lbltransaction_id" runat="server" Text=""></asp:Label></span>
                                </td>
                            </tr>
                            <tr>

                                <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <span class="data"><b>Payment Status:</b></span>
                                </th>
                                <td>
                                    <span class="data">
                                        <asp:Label ID="lblstatus1" runat="server" Text=""></asp:Label></span>
                                </td>
                            </tr>
                            <tr id="receipt" runat="server">

                                <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <span class="data"><b>Receipt No:</b></span>
                                </th>
                                <td>
                                    <span class="data">
                                        <asp:Label ID="lbl_no_1" runat="server" Text=""></asp:Label></span>
                                </td>
                            </tr>
                            <tr>

                                <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <span class="data"><b>Mode of Payment:</b></span>
                                </th>
                                <td>
                                    <span class="data">
                                        <asp:Label ID="lblmode" runat="server" Text=""></asp:Label></span>
                                </td>
                            </tr>
                            <tr>
                                <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <span class="data"><b>Description:</b></span>
                                </th>
                                <td>
                                    <span class="data">
                                        <asp:Label ID="lbldescription" runat="server" Text=""></asp:Label></span>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <asp:Label ID="lblStatus" runat="server" Visible="false" Style="margin-top: 10px" Text="" />
            <asp:Label ID="lbl_deductedinfo" runat="server" Visible="false" Style="margin-top: 10px; margin-left: 10px" Text="(Note* : If an amount has been deducted, we recommended waiting for few hours, after logging back into  the application and checking the status of the deducted amount.)" />
            <div style="text-align: center;">
            </div>
        </div>
    </form>
</body>

<%--------------------------------------------------- JS Plugins Start -------------------------------------%>
<!-- Library Bundle Script -->
<script src="assets/js/core/libs.min.js"></script>

<!-- External Library Bundle Script -->
<script src="assets/js/core/external.min.js"></script>

<!-- App Script -->
<script src="assets/js/hope-ui.js"></script>


<%--------------------------------------------------- JS Plugins End -------------------------------------%>

<script>
    $(document).ready(function () {
        window.print();
    });
</script>
</html>
