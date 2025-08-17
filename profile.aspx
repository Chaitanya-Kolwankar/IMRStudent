<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="profile.aspx.cs" Inherits="Announcement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .text1 {
            color: #009999;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 25px;
            font-weight: bold;
            -webkit-transition-property: color;
            -webkit-transition-duration: 1s, 1s;
            -webkit-transition-timing-function: linear, ease-in;
            -moz-transition-property: color;
            -moz-transition-duration: 1s;
            -moz-transition-timing-function: linear, ease-in;
            -o-transition-property: color;
            -o-transition-duration: 1s;
            -o-transition-timing-function: linear, ease-in;
        }

            .text1:hover {
                color: #003399;
            }
    </style>
    <form runat="server">
        <div class="row" style="margin-top: 10px" id="div_admission" runat="server" visible="false">
            <div class="col-lg-6">
                <marquee behavior="alternate"><a href="#" class="text1 blink_me">Online Admission</a></marquee>
            </div>
        </div>
        <div class="row" style="margin-top: 10px">
            <div class="col-lg-12 col-md-12">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        General Information
                    </div>
                    <div class="table-responsive">

                        <table class="table table-hover">
                            <thead>
                                <tr>
                                    <td>Student ID :</td>
                                    <td>Course Name :</td>
                                    <td>Sub Course Name :</td>
                                    <td>Roll No. :</td>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblstudid" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblcoursefromuser" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblsubcoursefromuser" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblrollfromuser" runat="server" Font-Bold="true" Text=""></asp:Label></td>
                                </tr>
                            </tbody>
                        </table>

                    </div>
                </div>
            </div>
            <%-- <div class="col-lg-6">
            <div class="panel panel-red">
                        <div class="panel-heading">
                            Notices
                        </div>
                        <div class="panel-body">
                              <div class="list-group" id="list_notice" runat="server">
                                       
                            </div>
                             <div class="table-responsive">
                           
                                 <asp:GridView ID="GridView1" runat="server" Width="100%" Height="100px" CssClass="table table-bordered" AutoGenerateColumns="false">
                                                    <Columns>
                                                        <asp:BoundField DataField="Id"  HeaderText="Id" Visible="false"/>
                                                        <asp:BoundField DataField="title" HeaderText="Title" />
                                                        
                <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkView" runat="server" Text="View / Download" OnClick="View" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="time" HeaderText="Time" />
                                                    </Columns>
                                                </asp:GridView>
                                  </div>  
                        </div>

            </div>
        </div>--%>
        </div>
        <div class="row" style="margin-top: 10px">
            <div class="col-lg-12">
                <div class="panel panel-info">
                    <div class="panel-heading">
                        Profile
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="well-lg">
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <asp:Label ID="firstname" runat="server" Text="FIRST NAME* :"></asp:Label>
                                                <asp:TextBox ID="txtfirst" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <asp:Label ID="LASTNAME" runat="server" Text="LAST NAME* :" ToolTip="LAST NAME"></asp:Label>
                                                <asp:TextBox ID="txtsurname" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <asp:Label ID="mothername" runat="server" Text="MOTHER NAME*" ToolTip="MOTHER's NAME"></asp:Label>
                                                <asp:TextBox ID="txtmother" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <asp:Label ID="SURNAME" runat="server" Text="MIDDLE NAME* :"></asp:Label>
                                                <asp:TextBox ID="txtfather" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <asp:Label ID="gender" runat="server" Text="GENDER :" ToolTip="GENDER"></asp:Label>
                                                <asp:DropDownList ID="ddgender" runat="server" CssClass="form-control" disabled>
                                                    <asp:ListItem>--SELECT--</asp:ListItem>
                                                    <asp:ListItem>MALE</asp:ListItem>
                                                    <asp:ListItem>FEMALE</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <asp:Label ID="blood" runat="server" Text="BLOOD GROUP :" ToolTip="SELECT BLOOD GROUP"></asp:Label>
                                                <asp:DropDownList ID="ddblood" runat="server" CssClass="form-control" disabled>
                                                    <asp:ListItem>--SELECT--</asp:ListItem>
                                                    <asp:ListItem>A +ve</asp:ListItem>
                                                    <asp:ListItem>A -ve</asp:ListItem>
                                                    <asp:ListItem>B +ve</asp:ListItem>
                                                    <asp:ListItem>B -ve</asp:ListItem>
                                                    <asp:ListItem>AB +ve</asp:ListItem>
                                                    <asp:ListItem>AB -ve</asp:ListItem>
                                                    <asp:ListItem>O +ve</asp:ListItem>
                                                    <asp:ListItem>O -ve</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <asp:Label ID="lblnat" runat="server" Text="NATIONALITY :" ToolTip="NATIONALITY"></asp:Label>
                                                <asp:TextBox ID="txtnationality" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <asp:Label ID="lblmartial" Text="MARTIAL STATUS :" ToolTip="MARTIAL STATUS :" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddmarried" runat="server" CssClass="form-control" disabled>
                                                    <asp:ListItem>--SELECT--</asp:ListItem>
                                                    <asp:ListItem>MARRIED</asp:ListItem>
                                                    <asp:ListItem>UNMARRIED</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <asp:Label ID="lblmothertongue" Text="MOTHER TONGUE :" ToolTip="MOTHER TONGUE" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtmothertongue" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <asp:Label ID="lblday" Text="BIRTH PLACE:" ToolTip="BIRTH PLACE" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtbirth" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <label>Date Of Birth</label>
                                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-3">
                                            <div class="form-group">
                                                <asp:Label ID="lbldomicile" Text="DOMICILED IN:" ToolTip="BIRTH PLACE" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtdomiciled" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="well-sm">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <asp:Label ID="lbladdress" Text="ADDRESS:" ToolTip="ADDRESS" runat="server"></asp:Label>
                                                <asp:TextBox TextMode="MultiLine" ID="txtadddress" runat="server" CssClass="form-control" Rows="4" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group" style="margin-top: 0px">
                                                <asp:Label ID="lbltelno" Text="CONTACT NUMBER:" ToolTip="Contact Number" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtmobno" ToolTip="REGISTERED MOBILE NUMBER CANNOT BE EDITED" runat="server" placeholder="Number 2" CssClass="form-control" disabled></asp:TextBox><br />
                                                <asp:Label ID="lblmobile" Text="MOBILE NO.:" ToolTip="MOBILE NUMBER" runat="server"></asp:Label>
                                                <asp:TextBox ID="txttelno" runat="server" placeholder="Number 1" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <label>Email ID</label>
                                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="well-sm">
                                    <div class="row">
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblcategory" Text="CATEGORY:" ToolTip="CATEGORY" runat="server"></asp:Label>
                                                <asp:DropDownList ID="ddcategory" CssClass="form-control" ToolTip="CATEGORY" runat="server" AutoPostBack="false" Font-Names="Georgia" Font-Size="10pt" Enabled="False">
                                                    <asp:ListItem>OPEN</asp:ListItem>
                                                    <asp:ListItem>SC</asp:ListItem>
                                                    <asp:ListItem>ST</asp:ListItem>
                                                    <asp:ListItem>DT(A)</asp:ListItem>
                                                    <asp:ListItem>NT(A)</asp:ListItem>
                                                    <asp:ListItem>NT(B)</asp:ListItem>
                                                    <asp:ListItem>NT(C)</asp:ListItem>
                                                    <asp:ListItem>NT(D)</asp:ListItem>
                                                    <asp:ListItem>OBC</asp:ListItem>
                                                    <asp:ListItem>SBC</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group" style="margin-top: 0px">
                                                <asp:Label ID="lblcaste" Text="Caste:" ToolTip="Caste" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtcaste" runat="server" CssClass="form-control" disabled> </asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-4">
                                            <div class="form-group">
                                                <asp:Label ID="lblreligion" Text="RELIGION:" ToolTip="RELIGION" runat="server"></asp:Label>
                                                <asp:TextBox ID="txtreligion" runat="server" CssClass="form-control" disabled></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="well-sm">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <h4>Physically Reserved</h4>
                                            <div class="row">
                                                <div class="col-lg-6">
                                                    <label>
                                                        <asp:CheckBox ID="chkhandicap" Enabled="false" runat="server" CssClass="form-control" OnCheckedChanged="chkhandicap_CheckedChanged" AutoPostBack="True" Text="Click If Yes, Submit Your Medical Certificate From The Authority" disabled /></label>
                                                    <div id="panhandicap" runat="server" visible="false">
                                                        <div class="form-group">

                                                            <asp:DropDownList ID="ddhandicap" CssClass="form-control" runat="server" EnableViewState="true" AutoPostBack="True" OnSelectedIndexChanged="ddhandicap_SelectedIndexChanged" disabled>
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                                <asp:ListItem>Visually Impaired</asp:ListItem>
                                                                <asp:ListItem>Speech</asp:ListItem>
                                                                <asp:ListItem>Hearing Impaired</asp:ListItem>
                                                                <asp:ListItem>Orthopedic Disorder</asp:ListItem>
                                                                <asp:ListItem>Mentally Retarded</asp:ListItem>
                                                                <asp:ListItem>Others</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="form-group">

                                                            <asp:TextBox ID="txtphysicalspecify" runat="server" CssClass="form-control" Text="" Visible="false" disabled></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="alert alert-danger" id="lblmainerror" runat="server" visible="false">
                                </div>
                                <asp:Button ID="butsubmit" CssClass="btn btn-default" Text="Save" runat="server" ToolTip="Submit Data" OnClick="btnsubmit_Click1" Visible="false" />

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</asp:Content>

