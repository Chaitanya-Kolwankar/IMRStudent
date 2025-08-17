<%@ Page Language="C#" AutoEventWireup="true" CodeFile="feeReceiptFull.aspx.cs" Inherits="feeReceiptFull" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Receipt</title>
    <link href="css/bootstrap.css" rel="stylesheet" />

    <script src="js/jquery-min.js"></script>

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

        #watermark {
            color: #d0d0d0;
            position: absolute;
            margin-top: 280px;
            height: 180px;
            margin-bottom: 100px;
            margin-left: 80px;
            margin-right: 50px;
            z-index: 100;
            opacity: 0.2;
        }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 0.156em;
             border: 1px solid black;
        }
        .itemCss {
            margin-left:5px;
        }
       
    </style>


</head>
<body>
    <%-- <div id="watermark">
        <img src="images/watermark.gif" />
    </div>--%>
    <div class="row">
         <div class="col-lg-1 col-md-1 col-sm-1 col-xs-1">
        </div>
        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8" style="border:1px solid black">
            <div class="container" style="width: 100%">
                <div class="row" style="font-size: 10px">
                    <center>
            <span style="font-weight:bolder ; height: 40px;font-size:medium" >
               <u>RECEIPT</u>
            </span>
            </center>
                </div>

                <div class="row" style="margin-top: 2px;">
                    <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="margin-top: 2px">
                        <center>
                     <img src="images/vivalogo.png" style="width: 60px; height: 60px" alt="" />
                </center>
                    </div>
                    <div class="col-lg-9 col-md-9 col-sm-9 col-xs-9" style="font-size: 10px; text-align: center; margin-top: 5px">
                        <div style="margin-right: 15px">
                            <span>Late Shri. Vishnu Waman Thakur Charitable Trust's</span><br />
                            <span style="font-size: larger"><b>Bhaskar Waman Thakur College of Science,</b></span><br />
                            <span style="font-size: larger"><b>Yashwant Keshav Patil College of Commerce,</b></span><br />
                            <strong style="font-size: larger"><b>Vidya Dayanand Patil College of Arts,</b></strong><br/>
                        </div>
                    </div>

                </div>
                <div class="row">
                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="border-bottom: 1px dotted black;border-top: 1px dotted black; margin-bottom: 2px; font-size: 10px">
                        <span style="margin-top: 5px">
                            <center>Viva College Road,Virar(West), Tal. Vasai, Dist. Palghar - 401303</center>
                        </span>
                    </div>
                </div>
                <div class="upperFont">
                    <div class="row" style="margin-bottom:8px">
                        <div class="col-lg-8 col-md-8 col-sm-8 col-xs-8">
                            <span>No.</span>
                            <span style="margin-left:10px;font-weight:normal"><asp:Label ID="lblNo" runat="server" Text=""></asp:Label></span>
                        </div>
                        <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">Date:</div>
                    </div>
                    <div class="row" style="margin-bottom:8px">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <span>Received from Shri/Smt./Kum.</span>
                            <span style="border-bottom:1px solid black;margin-left:10px;font-weight:normal"><asp:Label ID="lblName" runat="server" Text=""></asp:Label></span>
                        </div>
                    </div>
                    <div class="row" style="margin-bottom:8px">
                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                            <span>the amount of Rs.</span>
                            <span style="border-bottom:1px solid black;margin-left:10px; font-weight:normal"><asp:Label ID="lblAmount" runat="server" Text=""></asp:Label></span>
                        </div>
                    </div>
                    <div class="row" style="border-top:1px solid black">
                            <center> <span>FEES STRUCTURE FOR THE YEAR 2018-2019</span><br />
                            <span>Course - <asp:Label ID="lblCourse" runat="server" Text=""></asp:Label></span> <br />
                            </center>
                        <span style="margin-left:15px">as detailed below:</span>
                    </div>
                </div>

                <div class="row" style="font-size: 10px">
                    <div>
                        <form runat="server">
                            <asp:GridView ID="GridView1" CssClass="table table-bordered" AutoGenerateColumns="false" runat="server">
                                <Columns>
                                    <%--<asp:BoundField DataField="field_type" ControlStyle-BorderColor="Black" FooterStyle-BorderStyle="Solid" HeaderText="PARTICULARS">
                                        <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="value" ControlStyle-BorderColor="Black" FooterStyle-BorderStyle="Solid" HeaderText="AMOUNT Rs.">
                                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    </asp:BoundField>--%>
                                      <asp:BoundField DataField="field_type" ItemStyle-CssClass="itemCss" HeaderText="PARTICULARS" />
                                        <asp:BoundField DataField="value" ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center" HeaderText="AMOUNT Rs." />
                                </Columns>
                            </asp:GridView>
                        </form>
                    </div>
                </div>
              <%--  <div class="row">
                    <center>
                        <span style="font-size:9pt;font-weight:bold" >Accounts Clerk</span>
                    </center>
                </div>--%>
            </div>
        </div>
        <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
        </div>
    </div>
</body>
</html>
