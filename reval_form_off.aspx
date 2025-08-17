<%@ Page Language="C#" AutoEventWireup="true" CodeFile="reval_form_off.aspx.cs" Inherits="reval_form_off" %>

<!DOCTYPE html>

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
            padding: 0px;
            font-size:14px;
        }

        li {
            font-size: 12px;
        }
    </style>
</head>
<body style="font-family: 'Times New Roman'">
    <form id="form1" runat="server">
        <div>
            <div>
                <img src="images/VIT-banner.png" style="height: 150px; width: 100%" />
            </div>
            <div style="border-bottom: 1px dashed black; margin-bottom: 2px"></div>
            <div style="border-bottom: 1px dashed black"></div>
            <div style="font-size: 18px; margin-top: 5px;text-align:center">
                <span style="border-bottom: 1px solid black">APPLICATION FOR THE <span style="font-weight: bold">REVALUATION</span> OF ASSESSED ANSWER BOOKS.</span>
            </div>

            <table style="margin-bottom: 5px; text-align: center" class="table table-bordered">
                <tr style="text-align: center; font-weight:bold;">
                    <%--<td></td>--%>
                    <td>Branch
                    </td>
                    <td>Semester
                    </td>
                    <%-- <td></td>--%>
                    <td>Month
                    </td>
                    <td>Year
                    </td>
                    <td>Seat No.
                    </td>
                    <td>Pattern
                    </td>
                </tr>
                <tr>
                    <%-- <td>
                        <span style="font-size: 18px; font-weight: bold">at </span>
                    </td>--%>
                    <td>
                        <asp:Label ID="TextBox5" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="sem_txt" runat="server" Style="text-align: center; font-family: 'Times New Roman'"></asp:Label>
                    </td>
                    <%-- <td>
                        <span style="font-size: 18px; font-weight: bold">of&nbsp;</span>
                    </td>--%>
                    <td>
                        <asp:Label ID="txt_month" runat="server" Style="text-align: center; font-family: 'Times New Roman'"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="txt_year" runat="server" Style="text-align: center; font-family: 'Times New Roman'"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="TextBox9" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="pattern_txt" runat="server" Style="text-align: center; font-family: 'Times New Roman'"></asp:Label>
                    </td>
                </tr>

            </table>
            <p style="margin-bottom: 5px;">
                Name and Address of the candidate (in BLOCK LETTERS)
            </p>
               <table class="table table-bordered">
                <tr> 
                    <td style="width: 20%;padding-left: 5px;">Student ID: 
                    </td>
                    <td style="font-weight: bold; width: 70%;padding-left: 5px; ">
                        <asp:Label runat="server" ID="label3" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;padding-left: 5px;">Shri/Smt./Kum. 
                    </td>
                    <td style="font-weight: bold; width: 70%;padding-left: 5px; ">
                        <asp:Label runat="server" ID="label1" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;padding-left: 5px;">Address 
                    </td>
                    <td style="font-weight: bold; width: 70%;padding-left: 5px; ">
                        <asp:Label runat="server" ID="label2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;padding-left: 5px;">Mobile No
                    </td>
                    <td style="font-weight: bold; width: 70%;padding-left: 5px; ">
                        <asp:Label runat="server" ID="label4"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;padding-left: 5px;">Email Id 
                    </td>
                    <td style="font-weight: bold; width: 70%;padding-left: 5px; ">
                        <asp:Label runat="server" ID="label7"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;padding-left: 5px;">Seat No:
                    </td>
                    <td style="font-weight: bold; width: 70%;padding-left: 5px;">
                        <asp:Label runat="server" ID="lbl_seat"></asp:Label>
                    </td>
                </tr>
            </table>
            <p style="text-align: center; font-size: 13px; margin-top: 7px;">
                PARTICULARS OF THE SUBJECT(S) APPLIED FOR <span style="border-bottom: 1px solid black">REVALUATION</span> OF ASSESSED ANSWERBOOKS
            </p>
           <table class="table table-bordered" style="text-align: center">

                <tr style="font-weight:bold">
                    <td>Sr No.
                    </td>
                    <td>Name of The Subject
                    </td>
                    <td>Paper Code
                    </td>
                    <td>Marks Obtained
                    </td>
                </tr>


                <%=getWhileLoopData()%>
            </table>
               <div style="margin-top: 2px;">
                <span>Online Payment :-
                </span>
                <span style="font-weight: bold"><asp:Label ID="lbl_stat" runat="server" ></asp:Label>
                </span>
            </div>
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
                    <asp:Label runat="server" ID="date_lbl"></asp:Label>
                </span>
                <span style="float: right">Signature of the Candidate
                </span>
            </div>
       <%--     <div style="border-bottom: 1px dashed black; margin-bottom: 5px;"></div>--%>
            <p style="margin-bottom: 3px;">
                <span style="border-bottom: 1px solid black">Revaluation Rules :-</span>
            </p>
            <ol>
                <li>Any wrong information (i.e. Seat no.,Marks,/Subjects) fill-up by the 
                    candidate the form will be rejected and the fees will not be refundable
                </li>
                <li>Fees 10/- for Revaluation Form Process.
                </li>
                <li>Fees 125/- per subject for S.C.,S.T.,D.T./N.T. CategoryStudents.
                    (Caste Certificate if Compulsory)
                </li>
                <li>Fees 250/- per subject for OPEN / OBC Category Students
                </li>
            </ol>
            <table class="table table-bordered" style="text-align:center">
                
                <tr style="font-weight:bold">
                    <td>Sr No.
                    </td>
                    <td>Total Marks
                    </td>
                    <td>Minimum Passing Marks
                    </td>
                    <td>Required For Revaluation Marks
                    </td>
                </tr>
                
                <tr >
                    <td>
                        01
                    </td>
                    <td>80
                    </td>
                    <td>32
                    </td>
                    <td>13
                    </td>
                </tr>
                
                



            </table>

        </div>
    </form>
</body>
<script src="bootstrap-4.0.0-alpha.6-dist/js/bootstrap.min.js"></script>
<script src="jquery-1.9.0.min.js"></script>

<script>
    $(document).ready(function () {

        window.print();

    });
</script>
</html>
