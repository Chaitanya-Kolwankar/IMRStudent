<%@ Page Language="C#" AutoEventWireup="true" CodeFile="adm_prv_recpt.aspx.cs" Inherits="adm_prv_recpt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Receipt</title>

     <link href="bootstrap-4.0.0-alpha.6-dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        table, th, td {
            border: 1px solid black;
        }

        td {
            width: 50%;
            padding:12px;
        }
        tr>td:first-child{
            font-weight:bold;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="border: 1px solid black; height: 50vh">
            <div>
                <div class="row" style="margin-top: 15px">
                    <img src="images/VIT-banner.png" style="height: 140px" class="mx-4" />
                </div>
            </div>
            <div class="container-fluid">
                <table style="width: 100%">
                    <tr>
                        <td>Student Id: </td>
                        <td>
                            <asp:Label ID="txt_id" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Student Name: </td>
                        <td>
                            <asp:Label ID="Label1" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Academic Year: </td>
                        <td>
                            <asp:Label ID="Label6" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Amount: </td>
                        <td>
                            <asp:Label ID="Label4" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Status: </td>
                        <td>
                            <asp:Label ID="lbl_status" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Transaction ID: </td>
                        <td>
                            <asp:Label ID="Label5" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
    <script>
        window.print();
    </script>
</html>
