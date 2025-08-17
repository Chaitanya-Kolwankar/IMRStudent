<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Apply_Course.aspx.cs" Inherits="Apply_Course" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="js/jquery-min.js"></script>
    <script src="js/bootstrap.min.js"></script>
   <style>
            blink {
                color: red;
                font-size: 10px;
                font-weight: bold;
            }
              @keyframes blink {
            50% {
                border-color: #fff;
            }
        }
        </style>
    <form runat="server">
        <br />
        <div class="panel panel-primary">

            <div class="panel-heading">
                <div class="row">
                    <span style="font-family: Georgia; padding: 10pt;"><strong>Apply Course</strong></span>
                    <div class="hidden-lg hidden-md">
                        <a class="btn btn-sm btn-success pull-right" href="admission.aspx"><i class="fa fa-chevron-left" aria-hidden="true"></i> Previous Page</a>
                    </div>
                </div>
            </div>
            <div class="row" style="padding: 15px 15px 0px">
                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    <div class="panel panel-danger" style="min-height: 202px;">
                        <div class="panel-heading">
                            <div>Previous Year Admission Details</div>
                        </div>
                        <div class="panel-body">
                            <div class="row" style="border-bottom: 1px solid black">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    Welcome, 
                                        <label id="lblname" runat="server"></label>
                                </div>
                            </div>
                            <div class="row" style="margin-top: 10px">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <span>Current Group:</span>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <asp:Label ID="curr_grp" runat="server"></asp:Label>
                                </div>
                            </div>

                            <div class="row" style="margin-top: 5pt">
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <span>Current Roll No:</span>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-6 col-xs-6">
                                    <asp:Label ID="curr_roll" runat="server"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>

                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                    <div class="panel panel-danger">
                        <div class="panel-heading">
                            <div>New Year Admission Details</div>
                        </div>
                        <div class="panel-body">
                            <div class="row" style="margin-top: 5px">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <span>Select Next Year Group:</span>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <asp:DropDownList ID="ddlNextGrp" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlNextGrp_SelectedIndexChanged"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="row" style="padding: 5px;">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <span>Total Course Fees:</span>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <asp:Label runat="server" ID="lbl_amt_final" CssClass="form-control" />
                                </div>
                            </div>
                            <div class="row" style="padding: 5px;">
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <span>Amount To Pay:</span>
                                </div>
                                <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                    <asp:TextBox ID="txt_amt_final" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                </div>
                            </div>
                            <div class="row" runat="server" id="disc_show" visible="false">
                                <div class="">
                                    <i>
                                        <asp:Label runat="server" ID="amt_disc" CssClass="form-control" /></i>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                 
            </div>
   <%--          <label>--%>
           <span class="blink_me" style="color: red;margin-left: 225px;">Note.. Dropout students are hereby informed to contact college office immediately for fees structure. Fees indicated on students portal is for regular students.</span>
       
                                  
              
                            <%--    </label>--%>

            <div class="row" style="padding-left: 15px;padding-right:15px">
                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" id="declaration" runat="server" visible="false">
                    <div class="panel panel-success">
                        <div class="panel-heading">
                            <div><strong>Declaration</strong></div>
                        </div>
                        <div class="panel-body">
                            <span id="lbldec" runat="server">शैक्षणिक वर्ष सन २०२३-२४ करिता सामाजीक न्याय विभाग/ एकात्मिक आदिवासी प्रकल्प/ उच्च शिक्षण संचानालय मार्फत राबविण्यात येणाऱ्या शिष्यवृत्ती योजनेसाठी लागू असलेली नियमावली मी वाचली आहे.
        <br />
                                सामाजिक न्याय विभाग/ एकात्मिक आदिवासी प्रकल्प/ उच्च शिक्षण संचानालय मार्फत शिष्यवृत्ती अर्ज https://mahadbtmahait.gov.in या संकेत स्थळावर सुरु होताच मी ऑनलाईन शिष्यवृत्ती अर्ज भरण्याची प्रक्रिया पूर्ण करेन तसेच शिष्यवृत्ती योजनेसाठी लागणारी सर्व कागदपत्रे ऑनलाईन शिष्यवृत्ती अर्जा मध्ये अपलोड करणार याची हमी देत आहे. सदर प्रक्रिया पूर्ण केली नाही तर मी पूर्ण शुल्क भरण्यास पात्र असेल व मी पूर्ण शुल्क भरेन.</span>

                            <br />
                        </div>
                        <div class="panel-footer" style="background-color: #D9534F">
                            <div class="checkbox">
                                <label>
                                    <span style="color: white">Check the box, if you abide by the above declaration</span>
                                </label>
                            </div>
                            <asp:CheckBox ID="chkAgree" TabIndex="5" runat="server" Text=" &nbsp I AGREE" CssClass="form-control" AutoPostBack="false" ToolTip="I AGREE"></asp:CheckBox>

                        </div>
                    </div>
                </div>
            </div>

            <div class="row" style="margin-bottom: 10px">
                <div class="col-md-4"></div>
                <div class="col-md-4">
                    <asp:Button ID="btnsubmit" runat="server" Text="Apply" class="btn btn-success btn-block" TabIndex="16" OnClick="btnsubmit_Click"></asp:Button>
                </div>
                <div class="col-md-4"></div>
            </div>

            <div id="fyModal" class="modal fade" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">

                            <h4 class="modal-title">Payment Mode</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-3"></div>
                                <div class="col-md-3">
                                    <asp:Button ID="Payment" CssClass="btn btn-success" Text="Online Payment" runat="server" Width="100%" OnClick="Payment_Click" />
                                </div>
                                <div class="col-md-3">

                                    <asp:Button ID="Button1" CssClass="btn btn-success" Text="Offline" runat="server" Width="100%" OnClick="cash_Click" />
                                </div>
                                <div class="col-md-3"></div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57)) {
                return false;
            }
            return true;
        }
    </script>
</asp:Content>

