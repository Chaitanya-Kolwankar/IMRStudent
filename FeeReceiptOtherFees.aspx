<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeeReceiptOtherFees.aspx.cs" Inherits="FeeReceiptOtherFees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-latest.min.js"></script>
    <script src="https://code.jquery.com/jquery-migrate-1.2.1.js"></script>
    <script src="vendors/bootstrap/dist/js/bootstrap.js"></script>
    <script src="js/jquery-min.js"></script>
    <script src="notify-master/js/jquery-1.11.0.js"></script>
    <script src="js/jquery.qrcode.min.js"></script>
    <script src="js/jquery-barcode.js"></script>
    <style>
        @page {
            size: A4;
        }

        .watermark {
            color: #d0d0d0;
            position: absolute;
            margin-top: 260px;
            height: 100px;
            margin-bottom: 190px;
            margin-left: 110px;
            margin-right: 110px;
            z-index: 100;
            opacity: 0.2;
        }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 0.156em;
            border: 1px solid black;
            margin-bottom: -20px;
            border: 1px solid;
        }

        .upperFont {
            font-size: 15px;
        }

        #gridstructre > tbody > tr:last-child, tr:nth-last-child(2) {
            font-weight: bold !important;
            text-align: center !important;
        }

        /*#gridpayment > tbody > tr:last-child {
            font-weight: bold !important;
        }*/

        table th {
            text-align: center;
        }


        .itemCss {
            margin-left: 5px;
            font-size: 8px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-12 col-sm-12" style="border: 1px solid black;">
                    <div class="watermark">
                        <img src="images/watermark.gif" />
                    </div>
                    <div class="container" style="width: 100%">
                        <div class="row">
                            <div class="row" style="margin-top: 1px;">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <img src="images/VIT-banner.png" style="width: 100%" />
                                </div>
                            </div>
                        </div>
                        <hr style="border-color: black;margin-top:1px;margin-bottom:1px" />
                        <div class="upperFont" style="margin: 15px">
                            <div class="row">
                                <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                    <span>Receipt No:</span>
                                    <b>
                                        <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                    </b>
                                </div>
                                <div id="Div1" runat="server" class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                    <span style="float: right;">Date:
                                    <b>
                                        <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label>
                                    </b>
                                    </span>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <span style="padding-left: 55px;">Received from Shri/Smt./Kum.</span>
                                    <b>
                                        <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                    </b>
                                    <span>the sum of rupees</span>
                                    <b>
                                        <asp:Label ID="lblamount" runat="server" Text=""></asp:Label>
                                    </b>
                                    for the course 
                                        <b>
                                            <asp:Label ID="lblcourse" runat="server" Text=""></asp:Label>
                                        </b>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <span>Category:
                                        <b>
                                            <asp:Label ID="lblcategory" runat="server" Text=""></asp:Label>
                                        </b>
                                    </span>
                                </div>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gridstructre" CssClass="table" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:BoundField DataField="Struct_name" ItemStyle-Font-Size="15px" ItemStyle-CssClass="itemCss" ItemStyle-VerticalAlign="Middle" HeaderText="PARTICULARS" HeaderStyle-HorizontalAlign="left" ControlStyle-Font-Size="15px" />
                                        <asp:BoundField DataField="Amount" ItemStyle-Font-Size="15px" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="itemCss" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="AMOUNT (IN Rs.)" ControlStyle-Font-Size="15px" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="row">
                                <div class="col-lg-12" style="border: 1px solid;border-bottom: none;">
                                    <center><b>PAYMENT DETAILS</b></center>
                                </div>
                                <asp:GridView ID="gridpayment" CssClass="table" AutoGenerateColumns="false" runat="server">
                                    <Columns>
                                        <asp:BoundField DataField="Recpt_mode" ItemStyle-Font-Size="15px" ItemStyle-CssClass="itemCss" ItemStyle-VerticalAlign="Middle" HeaderText="PAYMENT MODE" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="left" ControlStyle-Font-Size="15px" />
                                        <asp:BoundField DataField="Recpt_Bnk_Name" ItemStyle-Font-Size="15px" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="itemCss" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="BANK NAME" ControlStyle-Font-Size="15px" />
                                        <asp:BoundField DataField="Recpt_Chq_No" ItemStyle-Font-Size="15px" ItemStyle-CssClass="itemCss" ItemStyle-VerticalAlign="Middle" HeaderText="INSTRUMENT NO." ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="left" ControlStyle-Font-Size="15px" />
                                        <asp:BoundField DataField="Recpt_Chq_dt" ItemStyle-Font-Size="15px" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="itemCss" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="DATE" ControlStyle-Font-Size="15px" />
                                        <asp:BoundField DataField="Amount" ItemStyle-Font-Size="15px" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="itemCss" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="AMOUNT" ControlStyle-Font-Size="15px" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                        <br />
                       
                        <div class="upperFont">
                            <div class="col-lg-12">
                                <span style="float: right;">
                                    <b>FOR VIVA INSTITUTE OF TECHNOLOGY</b>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
                <div>
                    <div class="col-lg-12" style="font-size: 12px;">
                        * Computer generated report does not required signature.
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        window.onload = function () {
            window.print();
        }
    </script>
</body>
</html>
