<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KT_exm_form.aspx.cs" Inherits="KT_exm_form" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title></title>
    <link href="bootstrap-4.0.0-alpha.6-dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .table-border-1 th td {
            border: 1px solid black;
        }

        .table td, .table th {
            padding: 4px;
        }
    </style>
</head>
<body style="font-family: 'Times New Roman'">
    <form id="form1" runat="server">
        <div>
            <table style="margin-bottom: 5px;">
                <tr style="line-height:25px">
                    <td style="width: 15%"></td>
                    <td style="width: 13%">
                        <img src="vivalogo.png" style="width: 60px; height: 60px; margin-top: 5px;" />
                    </td>
                    <td style="width: 75%;">
                        <div style="font-size: 15px;">
                            Late Shri Vishnu Waman Thakur Charitable Trust's
                        </div>
                        <div style="font-size: 18px; font-weight: bold">
                            VIVA INSTITUTE OF TECHNOLOGY
                        </div>
                        <div style="font-size: 12px; font-weight: bold">
                            AT :- SHIRGAON, POST :- VIRAR, TAL. :- VASAI, DIST:- PALGHAR.
                        </div>
                    </td>
                    <td style="width: 5%"></td>
                </tr>
            </table>
            <div style="border-bottom: 1px dashed black; margin-bottom: 2px"></div>
            <div style="border-bottom: 1px dashed black"></div>
            <div style="font-size: 18px; margin-top: 10px; margin-bottom: 10px; text-align: center">
                <span><span style="font-weight: bold">KT EXAMINATION FORM</span></span>
            </div>

            <table style="margin-bottom: 5px">
                <tr>
                    <td>
                        <span style="font-size: 18px; font-weight: bold">at </span>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox5" runat="server" Style="width: 140px; border: 1px solid grey; text-align: center; font-family: 'Times New Roman';padding:4px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox6" runat="server" Style="width: 70px; border: 1px solid grey; text-align: center; font-family: 'Times New Roman';padding:4px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <span style="font-size: 18px; font-weight: bold">of&nbsp;</span>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox7" runat="server" Style="width: 100px; border: 1px solid grey; text-align: center; font-family: 'Times New Roman';padding:4px" Text="May" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox8" runat="server" Style="width: 80px; border: 1px solid grey; text-align: center; font-family: 'Times New Roman';padding:4px" Text="2022" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="pattern_txt" runat="server" Style="width: 80px; border: 1px solid grey; text-align: center; font-family: 'Times New Roman';padding:4px" Enabled="false"></asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox9" runat="server" Style="width: 130px; border: 1px solid grey; margin-left: 15px; text-align: center; font-family: 'Times New Roman';padding:4px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr style="text-align: center; font-size: 13px;">
                    <td></td>
                    <td>Name of the Examination
                    </td>
                    <td>Semester
                    </td>
                    <td></td>
                    <td>Month
                    </td>
                    <td>Year
                    </td>
                    <td>Pattern
                    </td>
                    <td>
                        <asp:Label ID="stud_id" runat="server" Text="Student ID" Style="margin-left: 15px" />
                    </td>
                </tr>
            </table>

            <p style="margin-bottom: 5px; margin-top: 15px">
                Name and Address of the candidate (in BLOCK LETTERS)
            </p>

            <table>
                <tr>
                    <td style="width: 85%">
                        <table>
                            <tr style="line-height:25px">
                                <td style="width: 125px">Shri/Smt./Kum. <span style="float: right">:- &nbsp</span>
                                </td>
                                <td style="font-weight: bold; font-size: 15px;">
                                    <asp:Label runat="server" ID="label1" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr style="line-height:25px">
                                <td>Address <span style="float: right">:- &nbsp</span>
                                </td>
                                <td style="font-weight: bold; font-size: 15px;">
                                    <asp:Label runat="server" ID="label2" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr style="line-height:25px">
                                <td>Caste <span style="float: right">:- &nbsp</span>
                                </td>
                                <td style="font-weight: bold; font-size: 15px;">
                                    <asp:Label runat="server" ID="label3" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr style="line-height:25px">
                                <td>Mobile No <span style="float: right">:- &nbsp</span>
                                </td>
                                <td style="font-weight: bold; font-size: 15px;">
                                    <asp:Label runat="server" ID="label4" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr style="line-height:25px">
                                <td>Email Id <span style="float: right">:- &nbsp</span>
                                </td>
                                <td style="font-weight: bold; font-size: 15px;">
                                    <asp:Label runat="server" ID="label5" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 15%">
                        <asp:Image ID="std_img" runat="server" Width="120px" Height="130px" Style="border: 1px solid black" />
                    </td>
                </tr>
            </table>

            <p style="text-align: center; font-size: 14px; margin-top: 7px;">
                <%--PARTICULARS OF THE SUBJECT(S) APPLIED FOR <span style="border-bottom: 1px solid black">PHOTOCOPY</span> OF ASSESSED ANSWERBOOKS--%>
            </p>
            <table class="table table-bordered" style="text-align: center; margin-top: 7px">

                <tr style="font-size: 16px">
                    <td style="padding:8px">Sr No.
                    </td>
                    <td style="padding:8px">Name of The Subject
                    </td>
                    <td style="padding:8px">Theory Marks
                    </td>
                    <td style="padding:8px">Internal Marks
                    </td>
                    <td style="padding:8px">Term Works
                    </td>
                    <td style="padding:8px">Practical/Oral
                    </td>
                </tr>


                <%=getWhileLoopData()%>
            </table>
            
            <div style="margin-top: 2px;">
                <span>Place :-
                </span>
                <span style="font-weight: bold">VIT (Virar - E)
                </span>
            </div>
            <div>
                <span>Date :-
                </span>
                <span style="font-weight: bold">
                    <asp:Label runat="server" ID="date_lbl" Text="10/11/15"></asp:Label>
                </span>
                <span style="float: right">Signature of the Candidate
                </span>
            </div>
            <div style="border-bottom: 1px dashed black; margin-bottom: 5px;"></div>

            <p style="margin-bottom: 3px;">
                <span style="border-bottom: 1px solid black">NOTE:-</span>
            </p>
            <ol>
                <li>Attach Photo Copies of all previous Marksheets.
                </li>
                <li>
                    Examination fee for KT students.
                </li>
            </ol>
            <ul style="margin-left:10px;margin-top:-13px">
                <li>one subject Rs. 260/-</li>
                <li>Two subjects Rs. 470/-</li>
                <li>Three or More subjects Rs. 995/-</li>
            </ul>

        </div>
    </form>
</body>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.js"></script>
<script src="https://stackpath.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
<script>
    $(document).ready(function () {

        window.print();

    });
</script>
</html>
