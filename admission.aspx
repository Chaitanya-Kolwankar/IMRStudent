<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="admission.aspx.cs" Inherits="admission" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function CheckNumeric(e) {

            if (window.event) // IE
            {
                if ((e.keyCode < 48 || e.keyCode > 57) & e.keyCode != 8) {
                    event.returnValue = false;
                    return false;

                }
            }
            else { // Fire Fox
                if ((e.which < 48 || e.which > 57) & e.which != 8) {
                    e.preventDefault();
                    return false;

                }
            }
        }
    </script>
    <form runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br />
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <div class="row">
                            <span style="font-family: Georgia; padding: 10pt;"><strong>Basic Details</strong></span>
                            <%--<div class="hidden-lg hidden-md">
                                <a class="btn btn-sm btn-success pull-right" href="#"><i class="fa fa-plus"></i>Previous Page</a>
                            </div>--%>
                        </div>
                    </div>
                </div>

                <%-- basic Tab1--%>
                <div id="tab1" runat="server" class="panel panel-success" style="padding: 10pt">
                    <div class="row" style="margin-top: 10px">
                        <%--<div class="col-lg-1"></div>--%>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Full Name:</label>
                                <asp:TextBox ID="f_name" class="form-control" runat="server" disabled></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Date Of Birth:</label>
                                <asp:TextBox ID="dob" runat="server" class="form-control" disabled></asp:TextBox>
                            </div>
                        </div>
                        <%--<div class="col-lg-1">
                    <div class="form-group">
                        <label>Address:</label>
                        <asp:TextBox ID="address" TextMode="MultiLine" class="form-control" runat="server" disabled></asp:TextBox>
                    </div>
                </div>--%>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>Address:</label>
                                <asp:TextBox ID="address" TextMode="MultiLine" class="form-control" runat="server" MaxLength="150"></asp:TextBox>
                            </div>
                        </div>
                        <%--<div class="col-lg-6">
                       </div>--%>
                    </div>
                    <div class="row">
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Mobile No:<span style="color: red; font-weight: 200"><b>*</b></span></label>
                                <asp:TextBox ID="mob_no" runat="server" MaxLength="10" minlength="10" class="form-control" onkeypress="CheckNumeric(event);"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Mahadbt ID:</label>
                                <asp:TextBox ID="txtmahaid" runat="server" class="form-control" MaxLength="40"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <label>Mahadbt Password:</label>
                                <asp:TextBox ID="txtmahapass" runat="server" class="form-control" MaxLength="40"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Phone No:</label>
                                <asp:TextBox ID="phn_no" runat="server" MaxLength="12" class="form-control" onkeypress="CheckNumeric(event);" disabled> </asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Aadhar No:<span style="color: red;"><b>*</b></span></label>
                                <asp:TextBox ID="txtaadhar" runat="server" class="form-control" onkeypress="CheckNumeric(event);" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Email Id:<span style="color: red;"><b>*</b></span></label>
                                <asp:TextBox ID="stud_Email" runat="server" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Category:<span style="color: red;"><b>*</b></span></label>
                                <asp:DropDownList onblur="OnBlur(this);" ID="ddlCategory" onfocus="OnFocus(this);" Enabled="false" TabIndex="1" runat="server" ToolTip="CATEGORY" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>OPEN</asp:ListItem>
                                    <asp:ListItem>SC</asp:ListItem>
                                    <asp:ListItem>ST</asp:ListItem>
                                    <asp:ListItem>DT/VT</asp:ListItem>
                                    <asp:ListItem>OBC</asp:ListItem>
                                    <asp:ListItem>NT-1 (NT-B)</asp:ListItem>
                                    <asp:ListItem>NT-2 (NT-C)</asp:ListItem>
                                    <asp:ListItem>NT-3 (NT-D)</asp:ListItem>
                                    <asp:ListItem>SBC</asp:ListItem>
                                    <asp:ListItem>TFWS</asp:ListItem>
                                    <asp:ListItem>VJ/DT(A)</asp:ListItem>
                                    <asp:ListItem>EBC</asp:ListItem>
                                    <asp:ListItem>SEBC</asp:ListItem>
                                    <asp:ListItem>EWS</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Caste:<span style="color: red; font-weight: 200"><b>*</b></span></label>
                                <asp:DropDownList onblur="OnBlur(this);" ID="ddlCast" onfocus="OnFocus(this);"
                                    TabIndex="2" runat="server" ToolTip="Caste" CssClass="uppercase form-control" Font-Names="Verdana"
                                    AutoPostBack="True">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>Others</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                <label>Married/Unmarried:</label>
                                <asp:DropDownList ID="drp_mar" class="form-control" runat="server">
                                    <asp:ListItem>Unmarried</asp:ListItem>
                                    <asp:ListItem>Married</asp:ListItem>
                                </asp:DropDownList>
                                </select>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="row" id="castdiv" runat="server" visible="false">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>Cast Validity/Receipt No.:<span style="color: red;"><b>*</b></span></label>
                                        <asp:TextBox ID="txtcastno" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-inline">
                                        <span style="font-family: Verdana;">Cast Validity<span style="color: #ff3333">*</span></span>
                                        <br />
                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddmonth" onfocus="OnFocus(this);" CssClass="form-control" TabIndex="11" runat="server" AutoPostBack="true">
                                            <asp:ListItem>Month</asp:ListItem>
                                            <asp:ListItem Value="01">Jan</asp:ListItem>
                                            <asp:ListItem Value="02">Feb</asp:ListItem>
                                            <asp:ListItem Value="03">Mar</asp:ListItem>
                                            <asp:ListItem Value="04">Apr</asp:ListItem>
                                            <asp:ListItem Value="05">May</asp:ListItem>
                                            <asp:ListItem Value="06">Jun</asp:ListItem>
                                            <asp:ListItem Value="07">Jul</asp:ListItem>
                                            <asp:ListItem Value="08">Aug</asp:ListItem>
                                            <asp:ListItem Value="09">Sept</asp:ListItem>
                                            <asp:ListItem Value="10">Oct</asp:ListItem>
                                            <asp:ListItem Value="11">Nov</asp:ListItem>
                                            <asp:ListItem Value="12">Dec</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList onblur="OnBlur(this);" ID="dddate" onfocus="OnFocus(this);" CssClass="form-control" TabIndex="10" runat="server">
                                            <asp:ListItem>Day</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList onblur="OnBlur(this);" ID="ddyear" onfocus="OnFocus(this);" CssClass="form-control" TabIndex="12" runat="server">
                                            <asp:ListItem>Year</asp:ListItem>
                                            <asp:ListItem>2025</asp:ListItem>
                                            <asp:ListItem>2024</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2018</asp:ListItem>
                                            <asp:ListItem>2017</asp:ListItem>
                                            <asp:ListItem>2016</asp:ListItem>
                                            <asp:ListItem>2015</asp:ListItem>
                                            <asp:ListItem>2014</asp:ListItem>
                                            <asp:ListItem>2013</asp:ListItem>
                                            <asp:ListItem>2012</asp:ListItem>
                                            <asp:ListItem>2011</asp:ListItem>
                                            <asp:ListItem>2010</asp:ListItem>
                                            <asp:ListItem>2009</asp:ListItem>
                                            <asp:ListItem>2008</asp:ListItem>
                                            <asp:ListItem>2007</asp:ListItem>
                                            <asp:ListItem>2006</asp:ListItem>
                                            <asp:ListItem>2005</asp:ListItem>
                                            <asp:ListItem>2004</asp:ListItem>
                                            <asp:ListItem>2003</asp:ListItem>
                                            <asp:ListItem>2002</asp:ListItem>
                                            <asp:ListItem>2001</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="row" id="incomdiv" runat="server" visible="false">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>Income Certificate/Receipt No.:<span style="color: red;"><b>*</b></span></label>
                                        <asp:TextBox ID="txtincno" runat="server" class="form-control" MaxLength="50"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-inline">
                                        <span style="font-family: Verdana;">Income Certificate Date<span style="color: #ff3333">*</span></span>
                                        <br />
                                        <asp:DropDownList onblur="OnBlur(this);" ID="dincmonth" onfocus="OnFocus(this);" CssClass="form-control" TabIndex="11" runat="server" AutoPostBack="true">
                                            <asp:ListItem>Month</asp:ListItem>
                                            <asp:ListItem Value="01">Jan</asp:ListItem>
                                            <asp:ListItem Value="02">Feb</asp:ListItem>
                                            <asp:ListItem Value="03">Mar</asp:ListItem>
                                            <asp:ListItem Value="04">Apr</asp:ListItem>
                                            <asp:ListItem Value="05">May</asp:ListItem>
                                            <asp:ListItem Value="06">Jun</asp:ListItem>
                                            <asp:ListItem Value="07">Jul</asp:ListItem>
                                            <asp:ListItem Value="08">Aug</asp:ListItem>
                                            <asp:ListItem Value="09">Sept</asp:ListItem>
                                            <asp:ListItem Value="10">Oct</asp:ListItem>
                                            <asp:ListItem Value="11">Nov</asp:ListItem>
                                            <asp:ListItem Value="12">Dec</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList onblur="OnBlur(this);" ID="dincdate" onfocus="OnFocus(this);" CssClass="form-control" TabIndex="10" runat="server">
                                            <asp:ListItem>Day</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>31</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList onblur="OnBlur(this);" ID="dincyear" onfocus="OnFocus(this);" CssClass="form-control" TabIndex="12" runat="server">
                                            <asp:ListItem>Year</asp:ListItem>
                                            <asp:ListItem>2025</asp:ListItem>
                                            <asp:ListItem>2024</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem>
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2018</asp:ListItem>
                                            <asp:ListItem>2017</asp:ListItem>
                                            <asp:ListItem>2016</asp:ListItem>
                                            <asp:ListItem>2015</asp:ListItem>
                                            <asp:ListItem>2014</asp:ListItem>
                                            <asp:ListItem>2013</asp:ListItem>
                                            <asp:ListItem>2012</asp:ListItem>
                                            <asp:ListItem>2011</asp:ListItem>
                                            <asp:ListItem>2010</asp:ListItem>
                                            <asp:ListItem>2009</asp:ListItem>
                                            <asp:ListItem>2008</asp:ListItem>
                                            <asp:ListItem>2007</asp:ListItem>
                                            <asp:ListItem>2006</asp:ListItem>
                                            <asp:ListItem>2005</asp:ListItem>
                                            <asp:ListItem>2004</asp:ListItem>
                                            <asp:ListItem>2003</asp:ListItem>
                                            <asp:ListItem>2002</asp:ListItem>
                                            <asp:ListItem>2001</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <%-- <div class="row">
                            <!--<div class="col-lg-1"></div>-->
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Birth Place:</label>
                                    <asp:TextBox ID="birth_place" runat="server"  class="form-control" disabled></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Blood Group:</label>
                                    <asp:DropDownList ID="drp_blood" class="form-control" runat="server">
                                        <asp:ListItem>--Select--</asp:ListItem>
                                        <asp:ListItem>A+</asp:ListItem>
                                        <asp:ListItem>A-</asp:ListItem>
                                        <asp:ListItem>B+</asp:ListItem>
                                        <asp:ListItem>B-</asp:ListItem>
                                        <asp:ListItem>Ab+</asp:ListItem>
                                        <asp:ListItem>Ab</asp:ListItem>
                                        <asp:ListItem>O+</asp:ListItem>
                                        <asp:ListItem>O-</asp:ListItem>
                                    </asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Nationality:</label>
                                    <asp:TextBox ID="nationality" runat="server"  class="form-control" disabled></asp:TextBox>
                                </div>
                            </div>
                              </div>--%>
                    <%--                        <div class="row">
                     <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Category:</label>
                                    <asp:TextBox ID="category" runat="server"  class="form-control" disabled ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Caste:</label>
                                    <asp:TextBox ID="cast" runat="server"  class="form-control" disabled></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Domicile:</label>
                                    <asp:TextBox ID="domicile" runat="server"  class="form-control" disabled></asp:TextBox>
                                </div>
                            </div>
                             </div>--%>
                    <%--<div class="row">
                 <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Religion:</label>
                            <asp:TextBox ID="religion" runat="server"  class="form-control" disabled ></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-4">
                                <div class="form-group">
                                    <label>Gender:</label>
                                    <asp:TextBox ID="gender" runat="server"  class="form-control" disabled ></asp:TextBox>
                                </div>
                            </div>
                            
                            </div>--%>
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <asp:CheckBox ID="phychk" runat="server" OnCheckedChanged="phychk_CheckedChanged" AutoPostBack="true" />
                                <label>Physically Challenged</label>

                                <%--                            <div id="popup_msg" style="display: none;">Age is something</div>--%>
                                <%--<asp:TextBox ID="phyd" runat="server" class="form-control" ></asp:TextBox>--%>
                                <asp:DropDownList ID="DropDown_phy" class="form-control" runat="server" Style="display: none">
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>Visually Impaired</asp:ListItem>
                                    <asp:ListItem>Speech and/or Hearing Impaired</asp:ListItem>
                                    <asp:ListItem>Orthopedic Disorder</asp:ListItem>
                                    <asp:ListItem>Mentally Retired</asp:ListItem>
                                    <asp:ListItem>Learning Disability</asp:ListItem>
                                    <asp:ListItem>Dyslexia</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <%--   <div class="col-lg-4" id="phy_details_div" style="visibility: hidden">
                        <div class="form-group">
                            <label>Details:</label>
                            <div id="my_div"></div>
                        </div>
                    </div>
                    <div class="col-lg-4" id="phy_others" style="visibility: hidden">
                        <div class="form-group">
                            <label>Other:</label>
                            <asp:TextBox ID="other" runat="server" class="form-control"></asp:TextBox>
                        </div>
                    </div>--%>
                    </div>
                    <div class="row" style="padding: 10pt;">
                        <div class="col-lg-6"></div>
                        <div class="col-lg-4 col-xs-12 col-md-12 col-lg-pull-2">
                            <asp:Button ID="btn1" runat="server" Text="Next" class="btn btn-success btn-block topMargin" OnClientClick="return ValidateEmail();" OnClick="btn1_Click"></asp:Button>
                        </div>
                    </div>
                </div>

                <%--family Tab2  --%>
                <div id="tab2" class="panel panel-success" runat="server" style="padding: 10pt">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Father's / Guardian's Occupation:</label>
                                <asp:DropDownList ID="txtfatheroccupation" runat="server" CssClass="form-control">
                                    <asp:ListItem>--SELECT--</asp:ListItem>
                                    <asp:ListItem>Service</asp:ListItem>
                                    <asp:ListItem>Business</asp:ListItem>
                                    <asp:ListItem>Professional</asp:ListItem>
                                    <asp:ListItem>Farmer</asp:ListItem>
                                    <asp:ListItem>Laborer</asp:ListItem>
                                    <asp:ListItem>Retired</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem>
                                </asp:DropDownList>

                            </div>
                        </div>

                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Tel No:</label>
                                <asp:TextBox ID="father_mob" class="form-control" onkeypress="CheckNumeric(event);" runat="server" MaxLength="10"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>Business/Service Address:</label>
                                <asp:TextBox ID="fat_add" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Mother's Occupation:</label>
                                <asp:DropDownList ID="drp_motheroccu" runat="server" CssClass="form-control">
                                    <asp:ListItem>--SELECT--</asp:ListItem>
                                    <asp:ListItem>House Wife</asp:ListItem>
                                    <asp:ListItem>Service</asp:ListItem>
                                    <asp:ListItem>Business</asp:ListItem>
                                    <asp:ListItem>Professional</asp:ListItem>
                                    <asp:ListItem>Farmer</asp:ListItem>
                                    <asp:ListItem>Laborer</asp:ListItem>
                                    <asp:ListItem>Retired</asp:ListItem>
                                    <asp:ListItem>Other</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Tel No:</label>
                                <asp:TextBox ID="mother_mob" class="form-control" onkeypress="CheckNumeric(event);" runat="server" MaxLength="10"></asp:TextBox>

                            </div>
                        </div>
                    </div>
                    <hr />
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>Business/Service Address:</label>
                                <asp:TextBox ID="mot_add" TextMode="MultiLine" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Earning:</label>
                                <asp:TextBox ID="earn" runat="server" class="form-control" onkeypress="CheckNumeric(event);" OnTextChanged="earn_TextChanged" AutoPostBack="true" MaxLength="5"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Non-Earning:</label>
                                <asp:TextBox ID="NON_earn" runat="server" onkeypress="CheckNumeric(event);" class="form-control" OnTextChanged="NON_earn_TextChanged" AutoPostBack="true" MaxLength="5"></asp:TextBox>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>No.Of Persons in family:</label>
                                <asp:TextBox ID="person_fam" runat="server" class="form-control" disabled></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Family Income (Yearly):</label>
                                <asp:TextBox ID="fam_income" runat="server" onkeypress="CheckNumeric(event);" class="form-control" MaxLength="12"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Applied for Government Scholarship/Freeship (Current year):</label>
                                <asp:DropDownList ID="drp_scholarship" runat="server" CssClass="form-control" OnSelectedIndexChanged="drp_scholarship_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem>Yes</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Whether a Member of NCC/NSS:</label>
                                <asp:DropDownList ID="drp_ncc" runat="server" CssClass="form-control" onchange="changeCursor()">
                                    <asp:ListItem>No</asp:ListItem>
                                    <asp:ListItem>NCC</asp:ListItem>
                                    <asp:ListItem>NSS</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <div class="row">

                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Extra Cirricular Activities (if any):</label>
                                <asp:TextBox ID="txt_activity" runat="server" class="form-control"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Year of Joining Institute:</label>
                                <!--<asp:TextBox ID="txt_year_of_joining" runat="server" class="form-control" required></asp:TextBox>-->
                                <asp:DropDownList ID="drp_joining" runat="server" CssClass="form-control" required>
                                    <asp:ListItem>--Select--</asp:ListItem>
                                    <asp:ListItem>2008-2009</asp:ListItem>
                                    <asp:ListItem>2009-2010</asp:ListItem>
                                    <asp:ListItem>2010-2011</asp:ListItem>
                                    <asp:ListItem>2011-2012</asp:ListItem>
                                    <asp:ListItem>2012-2013 </asp:ListItem>
                                    <asp:ListItem>2013-2014</asp:ListItem>
                                    <asp:ListItem>2014-2015</asp:ListItem>
                                    <asp:ListItem>2015-2016</asp:ListItem>
                                    <asp:ListItem>2016-2017</asp:ListItem>
                                    <asp:ListItem>2017-2018</asp:ListItem>
                                    <asp:ListItem>2018-2019</asp:ListItem>
                                    <asp:ListItem>2019-2020</asp:ListItem>
                                    <asp:ListItem>2020-2021</asp:ListItem>
                                    <asp:ListItem>2021-2022</asp:ListItem>
                                    <asp:ListItem>2022-2023</asp:ListItem>

                                </asp:DropDownList><asp:RequiredFieldValidator
                                    ID="RequiredFieldValidator1"
                                    runat="server"
                                    ControlToValidate="drp_joining"
                                    InitialValue="Choose One"
                                    ErrorMessage="* Please select Your Year."
                                    ForeColor="Red"
                                    Font-Names="Impact">
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row" style="padding: 10pt;">
                        <div class="col-lg-6">
                            <asp:Button ID="btnBack" runat="server" Text="Back" class="btn btn-success btn-block topMargin" TabIndex="16" OnClick="btnBack_Click"></asp:Button>
                        </div>
                        <div class="col-lg-6 col-xs-12 col-md-12">
                            <asp:Button ID="btnsubmit" runat="server" Text="CONTINUE" class="btn btn-success btn-block topMargin" TabIndex="17" OnClick="btnsubmit_Click" OnClientClick="return validate();"></asp:Button>
                        </div>
                    </div>
                </div>
            </ContentTemplate>

        </asp:UpdatePanel>
        <div class="row">
            <div class="container">

                <div class="modal fade" id="myModal" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                                <h4 class="modal-title">Note</h4>
                            </div>

                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <span>

                                            <b>Do you have medical certificate from competent authority ?</b>

                                        </span>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-11 col-lg-offset-1 form-inline">
                                            Yes
                                            <asp:RadioButton ID="r_yes" CssClass="form-control " runat="server" GroupName="note" />&nbsp;&nbsp;
   No
                                            <asp:RadioButton ID="r_no" CssClass="form-control" runat="server" GroupName="note" />


                                        </div>
                                    </div>
                                    <br />
                                </div>
                                <div class="row" id="msg_content" runat="server" style="display: none">
                                    <div class="col-lg-9">
                                        <span><b>Attach xerox copy of the same along with admission form.</b></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <%--          <button type="button" id="agree" runat="server" class="btn btn-default" data-dismiss="modal">I Agree</button>--%>
                            <asp:Button ID="agree" runat="server" CssClass="btn btn-default" Text="Confirm" data-dismiss="modal" />
                            <%--          <button type="button" id="disagree" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                            <asp:Button ID="disagree" CssClass="btn btn-default" runat="server" Text="Close" data-dismiss="modal" />
                        </div>


                    </div>

                </div>
            </div>

        </div>


        <div class="row">
            <div class="container">

                <div class="modal fade" id="modal_scholar" role="dialog">
                    <div class="modal-dialog">

                        <!-- Modal content-->
                        <div class="modal-content">
                            <div class="modal-header">
                                <%--<button type="button" class="close" data-dismiss="modal">&times;</button>--%>
                                <h4 class="modal-title">Note</h4>
                            </div>

                            <div class="modal-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <span>

                                            <b>Do you have Caste certificate ?</b>

                                        </span>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-11 col-lg-offset-1 form-inline">
                                            Yes
                                            <asp:RadioButton ID="r_Ycaste" CssClass="form-control " runat="server" GroupName="note_caste" />&nbsp;&nbsp;
   No
                                            <asp:RadioButton ID="r_Ncaste" CssClass="form-control" runat="server" GroupName="note_caste" />


                                        </div>
                                    </div>
                                    <br />
                                </div>
                                <div class="row" id="msg_caste" runat="server" style="display: none">
                                    <div class="col-lg-9">
                                        <span><b>Attach xerox copy of the same along with admission form.</b></span><br />
                                        <span><a href="pdf/NOTICE SCHOLARSHIP.pdf" download>Click Here to Download Hamipatra</a></span>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <%--          <button type="button" id="agree" runat="server" class="btn btn-default" data-dismiss="modal">I Agree</button>--%>
                            <asp:Button ID="agree_caste" runat="server" CssClass="btn btn-default" Text="Confirm" data-dismiss="modal" />
                            <%--          <button type="button" id="disagree" class="btn btn-default" data-dismiss="modal">Close</button>--%>
                            <asp:Button ID="close_caste" CssClass="btn btn-default" runat="server" Text="Close" data-dismiss="modal" />
                        </div>


                    </div>

                </div>
            </div>

        </div>

    </form>
    <script type="text/javascript">


        function ValidateEmail() {
            var x = document.getElementById('<%= stud_Email.ClientID %>').value;

            var atpos = x.indexOf("@");
            var dotpos = x.lastIndexOf(".");
            if (atpos < 1 || dotpos < atpos + 2 || dotpos + 2 >= x.length) {
                alert("Not a valid e-mail address");
                //document.getElementById("lbl_msg").value = "Invalid Email ID";
                //document.getElementById("lbl_msg").style.display = 'visible';
                //blinkeffect('#divMsg');
                return false;
            }
            var fld = document.getElementById('<%= mob_no.ClientID %>').value;

            if (fld == "") {
                alert("Please Enter Mobile Number.");

                return false;
            }

            else if (fld.length != 10) {
                alert("The mobile number is the wrong length. \nPlease enter 10 digits mobile no.");

                return false;
            }
            var stud_add = document.getElementById('<%= address.ClientID %>').value;
            if (stud_add == "") {
                alert("Please Enter Address.");

                return false;
            }
        }


        function validate() {

            var f_mob = document.getElementById('<%= father_mob.ClientID %>').value;
            var earn = document.getElementById('<%= earn.ClientID %>').value;
            var non_earn = document.getElementById('<%= NON_earn.ClientID %>').value;
            var income = document.getElementById('<%= fam_income.ClientID %>').value;

            var drp_joining = document.getElementById("<%=drp_joining.ClientID %>");
            if (drp_joining.value == "--Select--") {
                //If the "Please Select" option is selected display error.
                alert("Please select Your Year!");
                return false;
            }
            return true;



            if (f_mob == "") {
                alert("Please Enter Father Mobile Number.");

                return false;
            }

            else if (f_mob.length != 10) {
                alert("Please enter 10 digits mobile no.");

                return false;
            }

            if (earn == "") {
                alert("Please Enter Earning Person.");

                return false;
            }

            if (non_earn == "") {
                alert("Please Enter Non-Earning Person.");

                return false;
            }
            if (income == "") {
                alert("Please Enter Family Income.");

                return false;
            }


        }


    </script>
    <script src="js/jquery-min.js"></script>
    <script>

        $('#<%=agree.ClientID%>').click(function () {

            //  alert("ok");
            if ($('#<%=r_yes.ClientID%>').is(":checked")) {


                $('#<%=phychk.ClientID%>').prop('checked', true);
                document.getElementById('<%=DropDown_phy.ClientID %>').style.display = 'block';

            }
        });

        $('#<%=disagree.ClientID%>').click(function () {

            //  alert("ok");
            $('#<%=phychk.ClientID%>').prop('checked', false);
            $('#<%=DropDown_phy.ClientID%>').attr("Display", "none");
        });

        $('#<%=r_yes.ClientID%>').change(function () {
            if ($(this).is(":checked")) {
                //  alert('ok');
                document.getElementById('<%=msg_content.ClientID %>').style.display = 'block';


            }
            else {
                document.getElementById('<%=msg_content.ClientID %>').style.display = 'none';

            }
        });
        $('#<%=r_no.ClientID%>').change(function () {
            if ($(this).is(":checked")) {
                //  alert('ok');
                document.getElementById('<%=msg_content.ClientID %>').style.display = 'none';


            }

        });


        //caste certificate
        $('#<%=agree_caste.ClientID%>').click(function () {

            //  alert("ok");
            if ($('#<%=r_Ycaste.ClientID%>').is(":checked")) {

                // $('#<%=drp_scholarship.ClientID%>').selectedIndex = 0;


            }
            else {
                $('#<%=drp_scholarship.ClientID%>')[0].selectedIndex = 0;
            }
        });

        $('#<%=close_caste.ClientID%>').click(function () {

            //  alert("ok");
            $('#<%=drp_scholarship.ClientID%>')[0].selectedIndex = 0;

        });

        $('#<%=r_Ycaste.ClientID%>').change(function () {
            if ($(this).is(":checked")) {
                //  alert('ok');
                document.getElementById('<%=msg_caste.ClientID %>').style.display = 'block';


            }
            else {
                document.getElementById('<%=msg_caste.ClientID %>').style.display = 'none';

            }
        });

        $('#<%=r_Ncaste.ClientID%>').change(function () {
            if ($(this).is(":checked")) {
                //  alert('ok');
                document.getElementById('<%=msg_caste.ClientID %>').style.display = 'none';


            }

        });



    </script>




</asp:Content>

