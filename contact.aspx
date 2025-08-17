<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="contact.aspx.cs" Inherits="contact" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
    .equal-height-row {
        display: flex;
        flex-wrap: wrap;
    }
    .equal-height-row > .col-lg-4 {
        display: flex;
        flex-direction: column;
    }
    .panel {
        flex: 1;
        width:100%;
    }
</style>


    <div class="row equal-height-row" style="margin-top: 10px">
        <div class="col-lg-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Address
                </div>
                <div class="panel-body">
                    <p>Institute Of Management and Research, Mumbai,  Dist-Mumbai, Maharashtra..</p>
                </div>
            </div>
        </div>
        <div class="col-lg-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Contact Numbers
                </div>
                <div class="panel-body">
                    <p>Tel:(0250)-000000 / 1111111 / 1111111.<apan>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</apan></p>
                </div>
            </div>
        </div>
        <div class="col-lg-4">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Email
                </div>
                <div class="panel-body">
                    <p>principalIMR@college.org, Contact@imr-college.org</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

