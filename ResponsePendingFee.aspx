<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResponsePendingFee.aspx.cs" Inherits="ResponsePendingFee" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <style>
        @page {
            size: A4;
        }
        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 5px;
            border: 1px solid black;
            font-family:'Times New Roman';
            font-size:15px;
        }
    </style>
</head>
<body onkeydown="return (event.keyCode !=116)">
    <form id="form1" runat="server">
        <div>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="border: 1px solid black">
                        <div class="row" style="margin-top: 8px;margin-bottom:8px;">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <img src="images/VIT-banner.png" style="width:100%"/>
                            </div>
                        </div>

                        <div class="row" style="border-top: 1px dotted black; margin-top: 2px;margin-bottom:5px; font-size: 10px">
                        </div>
                        <div>
                            <div class="row">
                                <center>
                                    <span style="margin-bottom:3px;font-size:15px;font-family:'Times New Roman', Times, serif;font-weight:bold">PAYMENT RECEIPT</span>
                                </center>
                            </div>
                            <div class="row">
                                <span style="float:right;font-size:15px;font-family:'Times New Roman', Times, serif;padding-right:20px"><b>Date : </b><asp:Label ID="lbl_date1" runat="server" Text=""></asp:Label></span>
                            </div>
                            <table class="table">
                                <tr>
                                    <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4"">
                                        <span class="data"><b>Student ID:</b></span>
                                    </th>
                                    <td>
                                        <span class="data"><asp:Label ID="lblstudentid" runat="server" Text=""></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4"">
                                        <span class="data"><b>Received from Shri/Smt./Kum.</b></span>
                                    </th>
                                    <td>
                                        <span class="data"><asp:Label ID="lbl_name_1" runat="server" Text=""></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4"">
                                        <span class="data"><b>Category:</b></span>
                                    </th>
                                    <td>
                                        <span class="data"><asp:Label ID="lbl_category_1" runat="server" Text=""></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4"">
                                        <span class="data"><b>Course:</b></span>
                                    </th>
                                    <td>
                                        <span class="data"><asp:Label ID="lblcourse" runat="server" Text=""></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4"">
                                        <span class="data"><b>Amount Of Rs:</b></span>
                                    </th>
                                    <td>
                                        <span class="data"><asp:Label ID="lblamountdigits" runat="server" Text=""></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4"">
                                        <span class="data"><b>Amount In Words:</b></span>
                                    </th>
                                    <td>
                                        <span class="data"><asp:Label ID="lbl_amount_1" runat="server" Text=""></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4"">
                                        <span class="data"><b>VIVA Transaction ID:</b></span>
                                    </th>
                                    <td>
                                        <span class="data"><asp:Label ID="lblvivatransction" runat="server" Text=""></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4"">
                                        <span class="data"><b>Bank Transaction ID:</b></span>
                                    </th>
                                    <td>
                                        <span class="data"><asp:Label ID="lbltransaction_id" runat="server" Text=""></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4"">
                                        <span class="data"><b>Payment Status:</b></span>
                                    </th>
                                    <td>
                                        <span class="data"><asp:Label ID="lblstatus1" runat="server" Text=""></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4"">
                                        <span class="data"><b>Receipt No:</b></span>
                                    </th>
                                    <td>
                                        <span class="data"><asp:Label ID="lbl_no_1" runat="server" Text=""></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4"">
                                        <span class="data"><b>Bank Name:</b></span>
                                    </th>
                                    <td>
                                        <span class="data"><asp:Label ID="lblbank" runat="server" Text=""></asp:Label></span>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-lg-4 col-md-4 col-sm-4 col-xs-4"">
                                        <span class="data"><b>Mode of Payment:</b></span>
                                    </th>
                                    <td>
                                        <span class="data"><asp:Label ID="lblmode" runat="server" Text=""></asp:Label></span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <div class="col-lg-4 col-md-4 col-sm-2 col-xs-1"></div>
                </div>
            </div>
            <asp:Label ID="lblStatus" runat="server" Visible="false" Style="margin-top: 10px" Text="" />
            <asp:Label ID="lbl_deductedinfo" runat="server" Visible="false" Style="margin-top: 10px; margin-left: 20px" Text="(Note* : If an amount has been deducted, we recommended waiting for 42 hours, after logging back into <br> &nbsp&nbsp&nbsp&nbsp&nbsp the application and checking the status of the deducted amount and admission confirmation.)" />
        </div>
    </form>
</body>
</html>
<script>
    $(document).ready(function () {
        window.print();
    });
    if (window.history.replaceState) {
        window.history.replaceState(null, null, window.location.href);
    }
</script>