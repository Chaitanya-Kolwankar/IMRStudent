<%@ Page Language="C#" AutoEventWireup="true" CodeFile="form_final.aspx.cs" Inherits="form_final" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Admission Form</title>
    <link href="css/bootstrap.css" rel="stylesheet" />
    <script src="js/jquery-min.js"></script>
    <script src="js/jquery.qrcode.min.js"></script>
    <script src="js/jquery-barcode.js"></script>
    <style>
        @page {
            size: A4;
        }
        .font-bold {
            font-weight: bold;
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
    </style>
</head>
<body>
    <div id="watermark">
        <img src="images/watermark.gif" />
    </div>
    <div class="container" style="width: 100%">
        <div class="row" style="font-size: 10px">
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2" style="border: 1px solid black; height: 40px">
                Teaching Staff:
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="border: 1px solid black; height: 40px">
                Non-Teaching Staff:
            </div>
            <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3" style="border: 1px solid black; height: 40px">
                Student ID:
                <asp:Label ID="lblstud_id" runat="server" Font-Bold="true"></asp:Label>

                <center> <div id="bcTarget"></div></center>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2" style="border: 1px solid black; height: 40px">
                Roll No:
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2" style="border: 1px solid black; height: 40px">
                Fees Paid:
            </div>
        </div>
        <div class="row" style="margin-top: 10px; border: 1px solid black;">
            <div class="col-lg-10 col-md-10 col-sm-10 col-xs-10" style="margin-top:1px">
                <center>
                <asp:Image ID="Imagelogo" runat="server" ImageUrl="images/VIT-banner.png"/>
                    </center>
                <%--<center>
                        <div id="qrcode"></div>
                </center>--%>
            </div>
            <div class="col-lg-2 col-md-2 col-sm-2 col-xs-2">
                <asp:Image ID="Image1" runat="server" Style="height: 120PX; width: 230px;margin-top:5px" ImageUrl='<%# "Handler.ashx?Imid=" + Eval("stud_id") %>' />
            </div>
            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" style="border-top: 1px solid black; margin-bottom: 2px; font-size: 13px; font-weight: bold;">
                <span style="margin-top: 5px">
                    <center>ADMISSION FORM FOR <asp:Label ID="lbl_group_id" runat="server"></asp:Label>&nbsp;<asp:Label ID="lbl_year" runat="server" ></asp:Label></center>
                </span>
            </div>
        </div>
        <div class="row" style="border: 0px solid black; font-size: 13px">
            <div>
                <table class="table" id="dataTables-example" style="margin-top: 5px">
                    <tbody>
                        <tr class="odd gradeX">
                            <td style="width: 20%">
                                <div class="font-bold">Full Name:</div>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblfname" runat="server" Style="font-style: normal;"></asp:Label>
                        </tr>

                        <tr class="odd gradeX">
                            <td style="width: 20%">
                                <div class="font-bold">Address:</div>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lbladdress" runat="server" Style="font-style: normal;"></asp:Label>
                            </td>

                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Mobile No:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblmobno" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Phone No:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblphnno" runat="server" Style="font-style: normal;"></asp:Label>


                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Email ID:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblemailadd" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Date of Birth:</div>
                            </td>
                            <td>
                                <asp:Label ID="lbldob" runat="server" Style="font-style: normal;"></asp:Label>

                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Birth Place:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblbirthplace" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Bloodgroup:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblblood" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Category:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblcat" runat="server" Style="font-style: normal;"></asp:Label>


                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Caste:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblcaste" runat="server" Style="font-style: normal;" />


                            </td>
                        </tr>
                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Caste Validity/Receipt No.:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblcstval" runat="server" Style="font-style: normal;"></asp:Label>


                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Caste Validity Date:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblcsdt" runat="server" Style="font-style: normal;" />


                            </td>
                        </tr>
                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Income Certificate/Receipt No.:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblincno" runat="server" Style="font-style: normal;"></asp:Label>


                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Income Certificate Date:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblincdt" runat="server" Style="font-style: normal;" />


                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Nationality:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblnat" runat="server" Style="font-style: normal"></asp:Label>

                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Domicile:</div>
                            </td>
                            <td>
                                <asp:Label ID="lbldom" runat="server" Style="font-style: normal"></asp:Label>

                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Religion:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblreligion" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Gender:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblgender" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Married/Unmarried:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblmar" runat="server" Style="font-style: normal"></asp:Label>
                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Physically Challanged:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblphhan" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Father Occupation:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblfaocc" runat="server" Style="font-style: normal"></asp:Label>

                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Tel No:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblfatel" runat="server" Width="250px" Font-Names="Times New Roman" Font-Size="Medium" Height="20px"></asp:Label>

                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Business/Service Address:</div>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblfabuss" runat="server" Style="font-style: normal"></asp:Label>

                            </td>

                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Mother Occupation:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblmoocc" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Tel No:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblmotel" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Business/Service Address:</div>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblmobuss" runat="server" Style="font-style: normal"></asp:Label>


                            </td>

                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">No.of Persons in Family:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblnoofperson" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Annual income of the family:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblsannualincome" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td>
                                <div class="font-bold">Earning:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblearn" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                            <td style="width: 20%">
                                <div class="font-bold">Non-Earning:</div>
                            </td>
                            <td>
                                <asp:Label ID="lblnonearn" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td colspan="1">
                                <div class="font-bold">Have You Applied for Scholarship/Freeship:</div>
                            </td>
                            <td colspan="1">
                                <asp:Label ID="lblscholarfree" runat="server" Style="font-style: normal"></asp:Label>


                            </td>
                            <td colspan="1">
                                <div class="font-bold">Whether a Member of NCC/NSS:</div>
                            </td>
                            <td colspan="1">
                                <asp:Label ID="lblnssncc" runat="server" Style="font-style: normal"></asp:Label>


                            </td>


                        </tr>



                        <tr class="odd gradeX">
                            <td colspan="2">
                                <div class="font-bold">Proficiency acquired in Extra curricular Activities:</div>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblciricular" runat="server" Style="font-style: normal"></asp:Label>


                            </td>

                        </tr>
                        <tr class="odd gradeX">
                            <td colspan="2">
                                <div class="font-bold">Aadhar No:</div>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="lblaadhar" runat="server" Style="font-style: normal"></asp:Label>
                            </td>

                        </tr>
                        <tr>
                            <td colspan="1">
                                <div class="font-bold">MahaDBT ID:</div>
                            </td>
                            <td colspan="1">
                                <asp:Label ID="lblmahid" runat="server" Style="font-style: normal"></asp:Label>
                            </td>
                            <td colspan="1">
                                <div class="font-bold">MahaDBT Password:</div>
                            </td>
                            <td colspan="1">
                                <asp:Label ID="lblmahpass" runat="server" Style="font-style: normal"></asp:Label>
                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td colspan="4">
                                <div style="font-size: 10px">
                                    <span><strong>Declaration by Student:</strong></span>I hereby declare that, I have read the rules related to admission and the information filled by me in this form is accurate and true to the best of my
                            knowledge. I will be responsible for any discrepency, arising out of the form signed by me and I undertake that, without necessary documents the final admission will not be granted and/or admission
                            will stand cancelled. will not be granted and/or admission will stand cancelled admission will stand cancelled.
                                </div>
                            </td>

                        </tr>

                        <tr class="odd gradeX">
                            <td colspan="4">
                                <div style="font-size: 10px">
                                    <span><strong>Note:</strong></span>Students from Backward class may please note that their freeship/Schollarship form is official sanctioning from government.<br />
                                    They will not be allowed change of stream.<br />
                                    Students writing any negative and offensive remark about any staff member or institution on any social networking website may be booked under cyber
                            crime law, Indian I.T Act 2000.
                                </div>
                            </td>
                        </tr>

                        <tr class="odd gradeX">
                            <td colspan="2" style="margin-top: 15px">
                                <div class="font-bold" style="margin-top: 15px; text-align: left; vertical-align: middle">Student's Signature</div>
                            </td>
                            <td colspan="2">

                                <div class="font-bold" style="margin-top: 15px; text-align: left; vertical-align: middle">Parent's Signature</div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    </div>

    <script src="js/bootstrap.min.js"></script>
    <script>
        $(document).ready(function () {
            var stud_data = "Student Id:" + $("#lblstud_id").text() + "\n Name:" + $("#lblfname").text() + "\n Class:" + $('#lbl_group_id').text();
            $('#qrcode').qrcode({
                "width": 100,
                "height": 100,
                "color": "#3a3",
                "text": stud_data
            });
            $("#bcTarget").barcode($("#lblstud_id").text(), "codabar");
            window.print();
        });
    </script>
</body>
</html>

