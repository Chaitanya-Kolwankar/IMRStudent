<%@ Page Language="C#" AutoEventWireup="true" CodeFile="KT_exm_new.aspx.cs" Inherits="KT_exm_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />

    <title></title>
    <link href="bootstrap-4.0.0-alpha.6-dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        @page {
            margin-left: 0.35cm;
            margin-right: 0.35cm;
            margin-top: 0.50cm;
            margin-bottom: 0.50cm;
        }

        table {
            font-family: 'Times New Roman';
        }

        .bgtd {
            background: #A8A8A8;
        }

        span {
            font-family: 'Times New Roman';
        }

        table, tr, td {
            border: 1px solid black;
        }
       
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="width: 100%; height: 100%; top: 0; bottom: 0; right: 0; left: 0; border: none">
            <tr style="height: 49%; border: none">
                <td style="border: none">
                    <div>
                        <div style="font-size: 16px; text-align: center">
                            <span style="float: left">[College Copy]</span>
                            <span style="font-weight: bold; font-family: 'Times New Roman'"><u>Acknowledgment</u></span>
                            <span style="float: right">Student ID:
                              <asp:Label ID="lblstud_id" Style="font-weight: bold" runat="server" Text="Label"></asp:Label></span>
                        </div>

                        <table style="margin-bottom: 5px; border-color: black; width: 100%; font-size: 14px">
                            <tr>
                                <td style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">College</div>
                                </td>
                                <td colspan="3" style="font-weight: bold">&nbsp&nbsp 823 Viva Institute Of Technology</td>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Chalan No</div>
                                </td>
                                <td>
                                  &nbsp&nbsp   <asp:Label ID="lbl_chalan" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Program(Pattern)</div>
                                </td>
                                 <td colspan="3">
                                  &nbsp&nbsp   <asp:Label ID="lblprogram" Style="font-weight: bold" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Branch</div>
                                </td>
                                <td>
                                  &nbsp&nbsp   <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Semester (Student Type)</div>
                                </td>
                                <td>
                                   &nbsp&nbsp  <asp:Label ID="lblsem" runat="server" Style="font-weight: bold" Text="Label"></asp:Label>
                                </td>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Previous Seat No.</div>
                                </td>
                                <td>
                                  &nbsp&nbsp   <asp:Label ID="lbl_seat_no" runat="server" Style="font-weight: bold" Text="Label"></asp:Label>
                                </td>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Exam</div>
                                </td>
                                <td>
                                  &nbsp&nbsp   <asp:Label ID="Label2" runat="server" Style="font-weight: bold" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Name</div>
                                </td>
                                <td colspan="5">
                                  &nbsp&nbsp   <asp:Label ID="lblname" Style="font-weight: bold" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Online Payment Status</div>
                                </td>
                                <td colspan="2">
                                   &nbsp&nbsp  <asp:Label ID="lbl_status" Style="font-weight: bold" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Fees+Late Fee =</div>
                                </td>
                                <td colspan="2">
                                   &nbsp&nbsp  <asp:Label ID="lblfee" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <p style="margin-bottom: 5px; margin-top: 8px; font-weight: bold">
                            Application for the Examination for the Subjects.
                        </p>

                        <%-- <asp:GridView ID="grid1" runat="server" CssClass="table table-border-self" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <asp:TemplateField ItemStyle-BorderWidth="1px">
                        <HeaderTemplate>
                            <tr>                               
                                <td style="text-align: center; font-weight: bold; border: 1px solid black">Subject Code</td>
                                <td style="text-align: center; font-weight: bold; border: 1px solid black" rowspan="5">Subject Name</td>
                            </tr>
                        </HeaderTemplate>

                        <ItemTemplate>
                            <td style="text-align: center; font-weight: normal; border: 1px solid black">
                                <asp:Label ID="lblcoursecode" runat="server" Text='<%# Eval("subject_code") %>'></asp:Label>
                            </td>
                            <td style="text-align: left; font-weight: normal; border: 1px solid black">
                                <asp:Label ID="lblcourse_title" runat="server" Text='<%# Eval("subject_name") %>'></asp:Label>
                            </td>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle />
            </asp:GridView>--%>

                        <table style="text-align: center; margin-top: 7px; width: 100%">

                            <tr style="font-size: 14px">
                                <td style="padding: 8px">Sr No.
                                </td>
                                <td style="padding: 8px">Name of The Subject
                                </td>
                                <td style="padding: 8px">Theory Marks
                                </td>
                                <td style="padding: 8px">Internal Marks
                                </td>
                                <td style="padding: 8px">Term Works
                                </td>
                                <td style="padding: 8px">Practical/Oral
                                </td>
                            </tr>


                            <%=getWhileLoopData()%>
                        </table>

                        <div>
                            <span style="font-weight: bold; font-family: 'Times New Roman'">Signature of Student
                            </span>

                            <span style="float: right; font-weight: bold; font-family: 'Times New Roman'">College Seal and Sign of Authorized Staff
                            </span>
                        </div>

                        <div style="padding-top: 9px">
                            <span>-----------------------</span>
                            <span style="float: right;">-----------------------</span>
                        </div>
                    </div>
                </td>
            </tr>
            <tr style="height: 2%; border: none">
                <td style="border: none">
                    <div style="width: 100%; height: 5px; border-bottom: 2px dashed black"></div>
                </td>
            </tr>
            <tr style="height: 49%; border: none">
                <td style="border: none">
                    <div>
                        <div style="font-size: 16px; text-align: center">
                            <span style="float: left">[Student Copy]</span>
                            <span style="font-weight: bold; font-family: 'Times New Roman'"><u>Acknowledgment</u></span>
                            <span style="float: right">Student ID:
                    <asp:Label ID="lblstud_id2" Style="font-weight: bold" runat="server" Text="Label"></asp:Label></span>
                        </div>

                        <table style="margin-bottom: 5px; border-color: black; width: 100%; font-size: 14px">
                            <tr>
                                <td style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">College</div>
                                </td>
                                <td colspan="3" style="font-weight: bold">&nbsp&nbsp  823 Viva Institute Of Technology</td>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Chalan No</div>
                                </td>
                                <td>
                                  &nbsp&nbsp   <asp:Label ID="lbl_chalan2" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Program(Pattern)</div>
                                </td>
                                <td colspan="5">
                                  &nbsp&nbsp   <asp:Label ID="lblprogram2" Style="font-weight: bold" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Semester (Student Type)</div>
                                </td>
                                <td>
                                  &nbsp&nbsp   <asp:Label ID="lblsem2" runat="server" Style="font-weight: bold" Text="Label"></asp:Label>
                                </td>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Previous Seat No.</div>
                                </td>
                                <td>
                                  &nbsp&nbsp   <asp:Label ID="lbl_seat_no2" runat="server" Style="font-weight: bold" Text="Label"></asp:Label>
                                </td>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Exam</div>
                                </td>
                                <td>
                                  &nbsp&nbsp   <asp:Label ID="Label7" runat="server" Style="font-weight: bold" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Name</div>
                                </td>
                                <td colspan="5">
                                  &nbsp&nbsp   <asp:Label ID="lblname2" Style="font-weight: bold" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Online Payment Status</div>
                                </td>
                                <td colspan="2">
                                  &nbsp&nbsp   <asp:Label ID="lbl_status2" Style="font-weight: bold" runat="server" Text="Label"></asp:Label>
                                </td>
                                <td class="bgtd" style="padding: 0px">
                                    <div class="bgtd" style="padding: 0.15rem">Fees+Late Fee =</div>
                                </td>
                                <td colspan="2">
                                   &nbsp&nbsp  <asp:Label ID="lblfee2" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <p style="margin-bottom: 5px; margin-top: 8px; font-weight: bold">
                            Application for the Examination for the Subjects.
                        </p>

                        <table style="text-align: center; margin-top: 7px; width: 100%">

                            <tr style="font-size: 14px">
                                <td style="padding: 8px">Sr No.
                                </td>
                                <td style="padding: 8px">Name of The Subject
                                </td>
                                <td style="padding: 8px">Theory Marks
                                </td>
                                <td style="padding: 8px">Internal Marks
                                </td>
                                <td style="padding: 8px">Term Works
                                </td>
                                <td style="padding: 8px">Practical/Oral
                                </td>
                            </tr>


                            <%=getWhileLoopData()%>
                        </table>

                        <div>
                            <span style="font-weight: bold; font-family: 'Times New Roman'">Signature of Student
                            </span>

                            <span style="float: right; font-weight: bold; font-family: 'Times New Roman'">College Seal and Sign of Authorized Staff
                            </span>
                        </div>

                        <div style="padding-top: 9px">
                            <span>-----------------------</span>
                            <span style="float: right;">-----------------------</span>
                        </div>
                    </div>
                </td>
            </tr>
        </table>

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
