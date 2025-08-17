<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Complaint.aspx.cs" Inherits="Complaint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function allLetter(inputtxt) {
            var letters = /^[A-Za-z]+$/;
            if (inputtxt.value.match(letters)) {
                return true;
            }
            else {
                alert('Please input alphabet characters only');
                return false;
            }
        }
    </script>
    <div class="row" style="margin-top: 10px">
        <div class="col-lg-12">
            <div class="panel panel-primary">
                <div class="panel-heading">
                   Suggestion Box
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-2"></div>
                        <div class="col-lg-8">
                            <form id="Form1" role="form" runat="server">
                                <div class="well">

                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:Label ID="complaint" runat="server" Text="SUGGESTION TYPE :" ToolTip="SUGGESTION TYPE"></asp:Label>
                                                <asp:DropDownList ID="ddcomplaint" runat="server" CssClass="form-control">
                                                    <asp:ListItem>--SELECT--</asp:ListItem>
                                                    <asp:ListItem>Infrastructure</asp:ListItem>
                                                    <asp:ListItem>Lectures</asp:ListItem>
                                                    <asp:ListItem>Teaching Staff</asp:ListItem>
                                                    <asp:ListItem>Non-Teaching Staff</asp:ListItem>
                                                    <asp:ListItem>Administration</asp:ListItem>
                                                    <asp:ListItem>Office</asp:ListItem>
                                                    <asp:ListItem>Examination</asp:ListItem>
                                                    <asp:ListItem>Results</asp:ListItem>
                                                    <asp:ListItem>Grievances</asp:ListItem>
                                                    <asp:ListItem>Sexual Harassment</asp:ListItem>
                                                    <asp:ListItem>Ragging</asp:ListItem>
                                                    <asp:ListItem>Sports</asp:ListItem>
                                                    <asp:ListItem>Cultural</asp:ListItem>
                                                    <asp:ListItem>Any Other</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>

                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:Label ID="lblcomplaint_title" runat="server" Text="SUGGESTION TITLE :" ToolTip="SUGGESTION TITLE"></asp:Label>
                                                <asp:TextBox ID="txtcomplaint_title" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <asp:Label ID="lbldescription" Text="DESCRIPTION :" ToolTip="DESCRIPTION:" runat="server"></asp:Label>
                                                <asp:TextBox TextMode="MultiLine" ID="txtdescription" runat="server" CssClass="form-control" Rows="4"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="file_upload" style="margin-top: 10px">
                                        <div class="col-lg-12">Attach a File:
                                            <asp:FileUpload runat="server" ID="file_upload_info" class="btn btn-default" Style="margin-top: 5pt" />
                                        </div>
                                    </div>
                                </div>



                                <div class="alert alert-danger" id="diverror" runat="server" visible="false">
                                </div>
                                <div class="row" style="margin-top: 25px">
                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                        <asp:Button ID="btnsubmit" CssClass="btn btn-lg btn-red btn-primary" Text="Submit Suggestion" runat="server" ToolTip="Submit Suggestion" OnClick="btnsubmit_Click" />
                                        <asp:Button ID="btnCancel" CssClass="btn btn-lg btn-default" Text="Cancel" runat="server" ToolTip="Cancel" OnClick="btnCancel_Click"/>
                                    </div>
                                  
                                </div>
                                <div style="margin-top: 25px">
                                    <div class="panel panel-default">
                                        <div class="panel-heading">
                                            Your Total Suggestions
                                        </div>
                                        <div class="panel-body">
                                            <div class="table-responsive">
                                                <asp:GridView ID="GridView1" runat="server" Width="100%" CssClass="table table-bordered" AutoGenerateColumns="false" OnRowCommand="GridView1_RowCommand">
                                                    <Columns>
                                                        <asp:BoundField DataField="id" HeaderText="Suggestion ID" />
                                                        <asp:BoundField DataField="type" HeaderText="Type" />
                                                        <asp:BoundField DataField="title" HeaderText="Title" />
                                                        <asp:BoundField DataField="description" HeaderText="Description" />
                                                        <asp:BoundField DataField="submit_date" HeaderText="Suggestion Date" />
                                                        <asp:TemplateField HeaderText="Submitted File">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnsubmittedFile" runat="server" Text="Download" CommandName="submittedFile" OnClientClick="aspnetForm.target ='_blank'"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="status" HeaderText="Status" />
                                                        <asp:BoundField DataField="reply" HeaderText="Reply" />
                                                        <asp:TemplateField HeaderText="Replied File">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnrepliedFile" runat="server" Text="Download" CommandName="repliedFile" OnClientClick="aspnetForm.target ='_blank'"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div class="col-lg-2"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

