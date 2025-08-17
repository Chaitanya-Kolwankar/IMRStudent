<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="NewKT_Exam.aspx.cs" Inherits="NewKT_Exam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <script src="js/jquery-min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <style>
        .table-container {
            height: 500px;
        }

        table {
            display: flex;
            flex-flow: column;
            height: 100%;
            width: 100%;
        }

            table thead {
                flex: 0 0 auto;
                width: calc(100% - 0.9em);
            }

            table tbody {
                flex: 1 1 auto;
                display: block;
                overflow-y: scroll;
            }

                table tbody tr {
                    width: 100%;
                }

                table thead,
                table tbody tr {
                    display: table;
                    table-layout: fixed;
                }

            table td, table th {
                padding: 0.3em;
            }


        .header-cont {
            width: 100%;
            position: fixed;
            top: 0px;
        }

        body {
            font-family: sans-serif;
            font-size: 10pt;
        }

        td {
            cursor: pointer;
        }

        .hover_row {
            background-color: #A1DCF2;
        }
    </style>


</asp:Content>

