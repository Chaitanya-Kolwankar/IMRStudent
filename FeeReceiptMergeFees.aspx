<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FeeReceiptMergeFees.aspx.cs" Inherits="FeeReceiptMergeFees" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RGCMS | Staff Receipt</title>
    <link href="<%= ResolveUrl("~/images/mu.png") %>" rel="icon" />
    <link href="<%= ResolveUrl("~/images/mu.png") %>" rel="apple-touch-icon" />
    <script src="https://code.jquery.com/jquery-latest.min.js"></script>
    <%--<link href="<%= ResolveUrl("~/Assets/notify-master/css/notify.css") %>" rel="stylesheet" />
    <script src="<%= ResolveUrl("~/Assets/notify-master/js/notify.js") %>"></script>--%>
    <style>
        @page {
            size: A4;
        }

        .watermark {
            color: #d0d0d0;
            position: absolute;
            margin-top: 330px;
            height: 50px;
            margin-left: 180px;
            z-index: 100;
            opacity: 0.1;
            text-align: center;
        }

        .watermark img{
            height:550px;
            width:100%;
            margin-top:40px
        }

        .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
            padding: 0.3em;
            /*margin-bottom: -20px;*/
            border: 1px solid gray;
        }

        .upperFont {
            font-size: 15px;
        }

        table th {
            text-align: center;
        }

        .table td:first-child {
            padding: 0 0 0 4px;
        }

        .itemCss {
            margin-left: 5px;
            font-size: 8px;
        }

        .p-5 {
            padding: 5px;
        }
    </style>
    <style>
        @media print {
            @page {
                size: A4 portrait;
                margin: 3mm;
            }

            body {
                margin: 0;
                -webkit-print-color-adjust: exact;
                print-color-adjust: exact;
                font-family: Arial, sans-serif;
            }

            .print-wrapper {
                display: flex;
                justify-content: space-between;
                align-items: flex-start;
                width: 100%;
                page-break-inside: avoid;
            }

            .receipt-copy {
                flex: 1;
                border: 1px solid #000;
                padding: 0 8px;
                margin: 2px 10px;
                box-sizing: border-box;
                font-size: 14px;
            }

            table {
                width: 100%;
                table-layout: fixed;
                word-wrap: break-word;
            }
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row">

                <div class="print-wrapper" id="receipt-original">
                    <div class="receipt-copy">
                        <div class="col-lg-12 col-sm-12">
                            <div class="watermark">
                                <img src="/images/mu.png" style="height: 400px" />
                            </div>
                            <div class="container" style="width: 100%">
                                <div class="row">
                                    <div class="row" style="margin-top: 8px;">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <img src="/images/RJC.png" style="width: 790px;" />
                                        </div>
                                    </div>
                                </div>
                                <hr style="border-color: black;" />

                                <div style="width: 100%; max-width: 800px; margin: auto; border: 2px solid #000; font-family: Arial, sans-serif; font-size: 14px; padding: 15px; box-sizing: border-box;">

                                    <div style="display: grid; grid-template-columns: 2fr 1fr 1fr; gap: 8px 16px; font-size: 16px;">
                                        <div>
                                            <b>Rec No:</b>
                                            <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                        </div>
                                        <%-- <div><b>Adm. No:</b>
                                            <asp:Label ID="lbl_admno" runat="server" Text=""></asp:Label></div>--%>
                                        <div>
                                            <b>Date:</b>
                                            <asp:Label ID="lbl_date" runat="server" Text=""></asp:Label>
                                        </div>

                                        <div>
                                            <b>Class:</b>
                                            <asp:Label ID="lblcourse" runat="server" Text=""></asp:Label>
                                        </div>
                                        <%--<div><b>Section:</b>
                                            <asp:Label ID="lbl_section" runat="server" Text=""></asp:Label></div>--%>
                                        <div>
                                            <b>Student ID:</b>
                                            <asp:Label ID="lbl_stud_id" runat="server" Text=""></asp:Label>
                                        </div>

                                        <div>
                                            <b>Category:</b>
                                            <asp:Label ID="lblcategory" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div>
                                            <b>Roll No:</b>
                                            <asp:Label ID="lbl_rollno" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div>
                                            <b>Fee Type:</b>
                                            <asp:Label ID="lbl_feetype" runat="server" Text=""></asp:Label>
                                        </div>
                                        <div style="grid-column: span 3;">
                                            <b>Name:</b>
                                            <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                                        </div>

                                    </div>
                                    <table style="width: 100%; border-top: 2px solid #000; border-bottom: 1px solid #000; border-collapse: collapse; margin-top: 5px; font-size: 16px;">
                                        <tr style="background-color: #f2f2f2;">
                                            <th style="text-align: left; padding: 5px;">Received the following</th>
                                            <th style="text-align: right; padding: 5px;">(₹) Amount</th>
                                        </tr>
                                    </table>
                                    <div class="row" style="height: 540px; padding: 0 2px 0 2px">
                                        <div class="table-responsive">
                                            <br />
                                            <asp:GridView ID="gridstructre" CssClass="table" AutoGenerateColumns="false" runat="server">
                                                <Columns>
                                                    <asp:BoundField DataField="Struct_name" ItemStyle-Font-Size="16px" ItemStyle-CssClass="itemCss" ItemStyle-VerticalAlign="Middle" HeaderText="PARTICULARS" HeaderStyle-HorizontalAlign="left" ControlStyle-Font-Size="18px" />
                                                    <asp:BoundField DataField="Amount" ItemStyle-Font-Size="16px" ItemStyle-VerticalAlign="Middle" ItemStyle-CssClass="itemCss" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" HeaderText="AMOUNT (IN Rs.)" ControlStyle-Font-Size="18px" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <table style="width: 100%; border-top: 2px solid #000; border-bottom: 1px solid #000; border-collapse: collapse; margin-top: 5px; font-size: 16px;">
                                        <tr>
                                            <td style="padding: 8px;"><b>Total:</b></td>
                                            <td style="text-align: right; padding: 8px;">
                                                <b>₹ 
                                                <asp:Label ID="amt_no" runat="server" Text=""></asp:Label>
                                                </b>
                                            </td>
                                        </tr>
                                    </table>

                                    <div style="margin-top: 15px; font-size: 16px; line-height: 1.5; padding: 12px; border: 1px solid #ccc; border-radius: 6px;">

                                        <p style="margin-bottom: 6px;">
                                            <b>In Words:</b>
                                            <asp:Label ID="lblamount" runat="server" Text=""></asp:Label>
                                        </p>
                                        <table style="width: 100%; border-collapse: collapse;">
                                            <%-- <tr>
                                                <td><b>Medium:</b>
                                                    <asp:Label ID="lblmedium" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td><b>Subject:</b> 
                                                    <asp:Label ID="lblSubjects" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>--%>
                                            <tr>
                                                <td><b>Payment Mode:</b>
                                                    <asp:Label ID="lbl_payment_mode" runat="server" Text=""></asp:Label>
                                                </td>
                                                <%--<td><b>Transaction ID:</b>
                                                    <asp:Label ID="lbltrans_id" runat="server" Text=""></asp:Label>
                                                </td>--%>
                                            </tr>
                                            <tr>
                                                <td><b>Bank Name: </b>
                                                    <asp:Label ID="lbl_bank_name" runat="server" Text=""></asp:Label>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                <td colspan="2"><b>Remarks:</b>
                                                    <asp:Label ID="lblremark" runat="server" Text="Payment for Academic Term 2025-2026"></asp:Label>
                                                </td>
                                            </tr>--%>
                                        </table>
                                        <div style="text-align: right; margin-bottom: 15px; font-size: 16px;">
                                            <div style="display: inline-block; border-top: 1px solid #000; padding-top: 5px;">
                                                <span class="copy-label"><b>RECEIVER'S SIGNATURE</b></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <br />


                            </div>
                        </div>
                    </div>
                    <div class="receipt-copy" id="receipt-clone">
                    </div>
                </div>
            </div>
        </div>
    </form>

    <script>
        window.onload = function () {
            //document.getElementById("receipt-clone").innerHTML =
            //    document.querySelector("#receipt-original .receipt-copy").innerHTML;

            //var cloneLabel = document.querySelector("#receipt-clone .copy-label b");
            //if (cloneLabel) {
            //    cloneLabel.textContent = "OFFICE STAFF SIGNATURE";
            //}
        };
    </script>
</body>
</html>
