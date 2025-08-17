<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ResponseFundTransfer.aspx.cs" Inherits="ResponseFundTransfer" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="vendors/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-latest.min.js"></script>
    <script src="https://code.jquery.com/jquery-migrate-1.2.1.js"></script>
    <script src="vendors/bootstrap/dist/js/bootstrap.js"></script>
    <script src="js/jquery-min.js"></script>
    <script src="notify-master/js/jquery-1.11.0.js"></script>
    <script src="js/jquery.qrcode.min.js"></script>
    <script src="js/jquery-barcode.js"></script>
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
            margin-top: 220px;
            height: 180px;
            margin-bottom: 100px;
            margin-left: 80px;
            margin-right: 50px;
            z-index: 100;
            opacity: 0.2;
            display: none;
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
            size: landscape;
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
    <div>
        <div class="container-fluid" style="width: 100%;">
            <div class="row">
              
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="border:1px solid black">
                    <%--                    <div class="watermark">
                        <img src="images/watermark.gif" />
                    </div>--%>


                                <div class="row">
                                    <div class="col-lg-2 col-md-2 col-sm-1 col-xs-1"></div>
                                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                                        <img src="images/vivalogo.png" style="width: 70px; height: 70px; margin-top: 8px;" alt="" />
                                    </div>
                                    <div class="col-lg-5 col-md-5 col-sm-7 col-xs-7" style="text-align: center">
                                     <span>LATE SHRI VISHNU WAMAN THAKUR CHARITABLE TRUST'S</span><br />
                <span><b>VIVA INSTITUTE OF TECHNOLOGY</b></span><br />
                <span><b>Shirgaon, Virar (East)</b></span><br />
                <span style="padding-left: 30px; font-size: 12px">(Affliated to University of Mumbai)</span><br />
                <span style="font-size: 10px">Shirgaon, Veer Sawarkar road, Virar(East) Tal-Vasai, Dist-Thane, Maharashtra.</span>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-1 col-xs-1"></div>
                                </div>

                            <div class="row" style="border-top: 1px dotted black; margin-top: 2px; font-size: 10px">
                            </div>
                            <div class="upperFont">
                                <div class="row">

                                     <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                        <span style="font-size: 12px">VIVA Transaction ID:</span>
                                        <span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lblvivatransction" runat="server" Text=""></asp:Label></span>
                                    </div> 

                                     <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                        <span style="font-size: 12px">Bank Transaction ID:</span>
                                        <span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lbltransaction_id" runat="server" Text=""></asp:Label></span>
                                    </div>

                                     <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                        <span style="font-size: 12px">Payment Status:</span>
                                        <span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lblstatus1" runat="server" Text=""></asp:Label></span>
                                    </div>

                                    <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                        <span runat="server" id="receipt" style="font-size: 12px">Receipt No:</span>
                                        <span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lbl_no_1" runat="server" Text=""></asp:Label></span>
                                    </div>
                                 <%--   <div id="Div2" runat="server" class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                        <span style="font-size: 12px">Date:</span>
                                        <span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lbl_date1" runat="server" Text=""></asp:Label></span>
                                    </div>--%>
                                </div>
                                <div class="row">
                                    <%-- <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                                            <span style="font-size: 8px">Student ID:</span>
                                            <span style="margin-left: 10px; font-size: 10px">
                                                <asp:Label ID="lbl_stud_id_1" runat="server" Text=""></asp:Label></span>
                                        </div>--%>
                                    <div id="tab_category2" runat="server" class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                                        <span style="font-size: 12px">Category:</span>
                                        <span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lbl_category_1" runat="server" Text=""></asp:Label></span>
                                    </div>
                                </div>
                                 <div class="row">
                                 <div id="Div1" runat="server" class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <span style="font-size: 12px">Date:</span>
                                        <span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lbl_date1" runat="server" Text=""></asp:Label></span>
                                  </div>
                                     </div>
                                 <div class="row">
                                    <div id="student_id" runat="server" class  ="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <span style="font-size: 12px">Student ID:</span>
                                        <b><span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lblstudentid" runat="server" Text=""></asp:Label></span></b>
                                    </div>
                                </div>
                                <div class="row">
                                    <div id="received_date" runat="server" class  ="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <span style="font-size: 12px">Received from Shri/Smt./Kum.</span>
                                        <b><span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lbl_name_1" runat="server" Text=""></asp:Label></span></b>
                                    </div>
                                </div>
                                <div id="amount" runat="server" class="row" style="margin-bottom: 4px">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <span style="font-size: 12px">Amount Of Rs:</span>
                                        <span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lblamountdigits" runat="server" Text=""></asp:Label></span>
                                    </div>
                                </div>
                                <div id="words" runat="server" class="row" style="margin-bottom: 4px">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <span style="font-size: 12px">Amount In Words:</span>
                                        <span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lbl_amount_1" runat="server" Text=""></asp:Label></span>
                                    </div>
                                </div>

                                 <div id="mode" runat="server" class="row" style="margin-bottom: 4px">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <span style="font-size: 12px">Mode of Payment:</span>
                                        <span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lblmode" runat="server" Text=""></asp:Label></span>
                                    </div>
                                </div> 
                                <div id="bankname" runat="server" class="row" style="margin-bottom: 4px">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <span style="font-size: 12px">Bank Name:</span>
                                        <span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lblbank" runat="server" Text=""></asp:Label></span>
                                    </div>
                                </div> 

                                  <div id="group" runat="server" class="row" style="margin-bottom: 4px">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <span style="font-size: 12px">Course Name:</span>
                                        <span style="margin-left: 10px; font-size: 11px">
                                            <asp:Label ID="lblcourse" runat="server" Text=""></asp:Label></span>
                                    </div>
                                </div> 

                                 <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6" style="margin-left: 10px;margin-top:7px">

                <!--    QR CODE-->
                <center>
<%--                    <div style="margin-top: 12pt;" ng-controller="qrController"  >
                        <qrcode version="8" error-correction-level="M" size="90" id="qrcode" data="{{student}}" download><!--<center><img src="images/vivalogo.png" height="10pt" /></center>--></qrcode>
                    </div>--%>
                        <div id="qrcode" runat="server"></div>

                </center>

            </div>



                            </div>
                            <%--<div id="div1" runat="server" class="alert alert-danger">
                            </div>--%>
                    </div>
                <div class="col-lg-4 col-md-4 col-sm-2 col-xs-1"></div> 
        </div>
        </div>


     <asp:Label ID="lblStatus" runat="server" visible="false" Style="margin-top: 10px" Text="" />
    </div>
    </form>
</body>
</html>
