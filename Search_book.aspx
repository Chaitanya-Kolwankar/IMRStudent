<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="search_book.aspx.cs" Inherits="search_book" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form runat="server">
       
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <div class="panel panel-primary">
               <div class="panel-heading">BOOK SEARCH</div>
               <div class="panel panel-body"> 

                   <%--mayu--%>

  <ul class="nav nav-pills">
    <li class="active"><a data-toggle="tab" href="#home">Basic Search</a></li>
    <li><a data-toggle="tab" href="#menu1" runat="server">Advanced Search</a></li>
  </ul>
      
   <div class="tab-content">
    <div id="home" class="tab-pane fade in active">
     <div class="row" style="padding-top:10px">
         <div class="col-lg-6 col-sm-6">
             <asp:TextBox ID="txtbasic" runat="server" CssClass="form-control"/>
         </div>
         <div class="col-lg-2 col-sm-2">
             <asp:Button ID="btnBasic" runat="server" Text="SEARCH" CssClass="form-control btn-primary" OnClick="btnBasic_Click" />
         </div>
     </div>
       <div class="row">
         <div class="col-lg-3">
         <i><asp:Label ID="lblinfo" runat="server" Text="Search by Book Title or Book Author or publisher" Font-Size="X-Small"></asp:Label></i>
         </div>
      </div>
    </div>

      
    <div id="menu1" class="tab-pane fade">
        <asp:UpdatePanel ID="update12" runat="server">
            <ContentTemplate>
                <div class="row" style="padding-top:10px">
     <div class="col-sm-12 col-lg-12">
        <div class="col-sm-3 col-lg-3">
            <asp:TextBox ID="txtTitle" runat="server" CssClass="form-control" Enabled="false"/>
        </div>
            <div class="col-sm-3 col-lg-3">
                <asp:DropDownList ID="keyword1" runat="server"  CssClass="form-control" OnSelectedIndexChanged="keyword1_SelectedIndexChanged"  AutoPostBack="true">
                    <asp:ListItem Text="--Keyword--" Value=""></asp:ListItem>
                    <asp:ListItem Text="Title" Value="Title"></asp:ListItem>
                    <asp:ListItem Text="Author" Value="Author"></asp:ListItem>
                    <asp:ListItem Text="Publisher" Value="Publisher"></asp:ListItem>
                </asp:DropDownList>
        </div>
         </div>
    </div>

                   <div class="row">
 <div class="col-sm-12 col-lg-12">
    &nbsp;&nbsp;
   
     <asp:RadioButton ID="rdbAND" runat="server" Text="AND" GroupName="Logic1" OnCheckedChanged="rdbAND_CheckedChanged"  AutoPostBack="true"/>&nbsp;&nbsp;
     <asp:RadioButton ID="rdbOR" runat="server" Text="OR" GroupName="Logic1" OnCheckedChanged="rdbOR_CheckedChanged"  AutoPostBack="true"/>&nbsp;&nbsp;
     <asp:RadioButton ID="rdbNOT" runat="server" Text="NOT" GroupName="Logic1" OnCheckedChanged="rdbNOT_CheckedChanged"  AutoPostBack="true"/>&nbsp;&nbsp;
   
 </div>
                       </div>

<div class="row">
         <div class="col-sm-12 col-lg-12">
         <div class="col-sm-3 col-lg-3">
             <asp:TextBox ID="txtAuthor" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
        </div>
        <div class="col-sm-3 col-lg-3">
             <asp:DropDownList ID="keyword2" runat="server"  CssClass="form-control" OnSelectedIndexChanged="keyword2_SelectedIndexChanged"  AutoPostBack="true">
                 <asp:ListItem Text="--Keyword--" Value=""></asp:ListItem>
                    <asp:ListItem Text="Title" Value="Title"></asp:ListItem>
                    <asp:ListItem Text="Author" Value="Author"></asp:ListItem>
                    <asp:ListItem Text="Publisher" Value="Publisher"></asp:ListItem>
                    <asp:ListItem Text="Edition" Value="Edition"></asp:ListItem>
                    <asp:ListItem Text="ISBN" Value="Standard No"></asp:ListItem>
                </asp:DropDownList>
        </div>
        </div>
</div>

<div class="row">
  <div class="col-sm-12 col-lg-12">
      &nbsp;&nbsp;
     <asp:RadioButton ID="rdbAND1" runat="server" Text="AND" GroupName="Logic" OnCheckedChanged="rdbAND1_CheckedChanged"  AutoPostBack="true"/>&nbsp;&nbsp;
     <asp:RadioButton ID="rdbOR1" runat="server" Text="OR" GroupName="Logic" OnCheckedChanged="rdbOR1_CheckedChanged"  AutoPostBack="true"/>&nbsp;&nbsp;
     <asp:RadioButton ID="rdbNOT1" runat="server" Text="NOT" GroupName="Logic" OnCheckedChanged="rdbNOT1_CheckedChanged"  AutoPostBack="true"/>&nbsp;&nbsp;
     

 </div>
</div>

                   <div class="row">
      <div class="col-sm-12 col-lg-12">
        <div class="col-sm-3 col-lg-3">
             <asp:TextBox ID="txtPublisher" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
        </div>
       <div class="col-sm-3 col-lg-3">
          <asp:DropDownList ID="keyword3" runat="server" CssClass="form-control" OnSelectedIndexChanged="keyword3_SelectedIndexChanged" AutoPostBack="true">
                    <asp:ListItem Text="--Keyword--" Value=""></asp:ListItem>
                    <asp:ListItem Text="Title" Value="Title"></asp:ListItem>
                    <asp:ListItem Text="Author" Value="Author"></asp:ListItem>
                    <asp:ListItem Text="Publisher" Value="Publisher"></asp:ListItem>
                    <asp:ListItem Text="Edition" Value="Edition"></asp:ListItem>
                    <asp:ListItem Text="ISBN" Value="Standard No"></asp:ListItem>
           </asp:DropDownList>
        </div>
     </div>
</div>
                          </ContentTemplate>
            </asp:UpdatePanel>
           
                   <div class="row">
        <div class="col-sm-12 col-lg-12">
           <div class="col-sm-3 col-lg-3">
            &nbsp;&nbsp;<asp:Button ID="Search" Text="SEARCH" runat="server" CssClass="form-control btn-primary" OnClick="Search_Click" AutoPostBack="true"/>
        </div>
     </div>
                       </div>
           

      <div class="row">
        <div class="col-sm-12 col-lg-12">
           <div class="col-sm-3 col-lg-3">
            &nbsp;&nbsp;<asp:Label ID="lblerr" Text="error" runat="server" ForeColor="Red" Visible="false"/>
        </div>
     </div>
                       </div>
            
 <%--           <Triggers>
                  <asp:PostBackTrigger ControlID="keyword1" /> 
                  <asp:PostBackTrigger ControlID="keyword2"  /> 
                  <asp:PostBackTrigger ControlID="keyword3" />           
            </Triggers>--%>
        

 
    </div>
  </div>


  
                    <%--mayu--%>


                   </div>
                </div>
          
          <div class="row">
        <div class="col-sm-12 col-lg-12">
           <div class="col-sm-3 col-lg-3">
            &nbsp;&nbsp;<asp:Label ID="lblcount1" runat="server" />
        </div>
     </div>
                       </div>

           <div class="panel panel-primary">
               <div class="panel-body">      
                    <div class="table-responsive" style="height:500px">
           <asp:GridView ID="GridView1" runat="server"  Width="100%" AutoGenerateColumns="false"  CssClass="table table-bordered" OnRowCommand="GridView1_RowCommand" >
                <Columns>
                                                        <asp:BoundField DataField="book_id" HeaderText="Book Id" />
                                                        <asp:BoundField DataField="book_title" HeaderText="Book Title" />
                                                        <asp:BoundField DataField="author_name" HeaderText="Author Name" />
                                                         <asp:BoundField DataField="book_edition" HeaderText="Book Edition" />
                                                        <asp:BoundField DataField="general_name" HeaderText="Publisher Name" />
                                                        <asp:BoundField DataField="book_classification_no" HeaderText="Book Classification No"/>                                
                                                        <asp:BoundField DataField="accession_no" HeaderText="Accession No" />
                                                        <asp:BoundField DataField="BookCount" HeaderText="Book Count" />                         
                                                        <asp:TemplateField HeaderText="Details">
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnDetails" Text="Details" runat="server" OnClick="btnDetails_Click1" CommandName="details" OnClientClick="aspnetForm.target ='_blank'" class="btn btn-primary btn-m" data-toggle="modal" data-target="#myModal"/>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                    </Columns>
           </asp:GridView>
               </div>
                   </div>
           </div>


            <!-- Modal -->
  <div class="modal fade" id="myModal" role="dialog">
    <div class="modal-dialog">
            <!-- Modal content-->
      <div class="modal-content">
        
        <div class="modal-body">
              <asp:UpdatePanel ID="UpdatePanel2" runat="server">
	            <ContentTemplate>
                    <div class="panel panel-info">
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-3 col-sm-3">
                                    <asp:Image ID="Image1" runat="server" Height="100px" Width="100px" BackColor="#E0E0E0" BorderColor="Black" BorderStyle="Solid" BorderWidth="2px" />
                                </div>
                            </div>
                   <div class="row" style="padding-top:10px">
                       <div class="col-lg-12 col-sm-12">
                         <div class="col-lg-3">
                            <b><asp:Label ID="lblTtl" Text="TITLE:" runat="server"></asp:Label></b>
                          </div>
                         <div class="col-lg-8">
                            <asp:Label ID="lbltitle" runat="server" Visible="false"></asp:Label>
                        </div>          
                      </div>
                    </div>

                     <div class="row">
                       <div class="col-lg-12 col-sm-12">
                         <div class="col-lg-3">
                            <b><asp:Label ID="lblAuth" Text="AUTHOR:" runat="server"></asp:Label></b>
                          </div>
                         <div class="col-lg-8">
                             <asp:Label ID="lblauthor" runat="server" Visible="false"/>
                        </div>          
                      </div>
                    </div>
                     
                    <div class="row">    
                   <div class="col-lg-12  col-sm-12">
                       <div class="col-lg-3">
                         <b><asp:Label ID="lblPublication" Text="Publication:" runat="server"></asp:Label></b>
                       </div>
                       <div class="col-lg-8">
                           <asp:Label ID="lblPub" runat="server" Visible="false"></asp:Label>
                       </div>
                   </div>
                    </div>  

                    <div class="row">
                     <div class="col-lg-12 col-sm-12">
                       <div class="col-lg-3">
                         <b><asp:Label ID="lblDescription" Text="Description:" runat="server"></asp:Label></b>
                       </div>
                       <div class="col-lg-8">
                           <asp:Label ID="lblDesc" runat="server" Visible="false"></asp:Label>
                       </div>
                   </div>
                    </div>

                    <div class="row">
                     <div class="col-lg-12 col-sm-12">
                       <div class="col-lg-3">
                         <b><asp:Label ID="lblstandard" Text="Standard No:" runat="server"></asp:Label></b>
                       </div>
                       <div class="col-lg-8">
                           <asp:Label ID="lblisbn" runat="server" Visible="false"></asp:Label>
                       </div>
                   </div>
                   </div>

                    <div class="row">
                     <div class="col-lg-12 col-sm-12">
                       <div class="col-lg-3">
                         <b><asp:Label ID="lblKeyword" Text="Key Words:" runat="server"></asp:Label></b>
                       </div>
                       <div class="col-lg-8">
                           <asp:Label ID="lblkey" runat="server" Visible="false"></asp:Label>
                       </div>
                   </div>
                    </div>

                    <br /><br />

                    <div class="row">
                      <div class="col-lg-12 col-sm-12">
                          <div class="col-lg-3"><b>AVAILABLE:</b></div>
                          <div class="col-lg-8" id="lblcount" runat="server">                  
                               </div>  
                          
                         </div>
                    </div>

                    <div class="row">
                    <div class="col-lg-12 col-sm-12">
                          <div class="col-lg-3"><b>ISSUED:</b></div>
                         <div class="col-lg-8" id="lblcountIssued" runat="server">
                               </div>  

                    </div>
                     </div>
                        </div>
                    </div>

                  



                             </ContentTemplate>
                 <Triggers>
	               <asp:AsyncPostBackTrigger ControlID="GridView1"  EventName="RowCommand" /> 

	           </Triggers>
                </asp:UpdatePanel>          
        </div>
        <div class="modal-footer">
          <button type="button" class="btn btn-primary" data-dismiss="modal">Close</button>
        </div>
      </div>
      
    </div>
  </div>

        
                                        
    
    </form>
   
     <script>
         $(function () {
             // for bootstrap 3 use 'shown.bs.tab', for bootstrap 2 use 'shown' in the next line
             $('a[data-toggle="tab"]').on('shown.bs.tab', function (e) {
                 // save the latest tab; use cookies if you like 'em better:
                 localStorage.setItem('lastTab', $(this).attr('href'));
             });

             // go to the latest tab, if it exists:
             var lastTab = localStorage.getItem('lastTab');
             if (lastTab) {
                 $('[href="' + lastTab + '"]').tab('show');
             }
         });
    </script>

<%--        <script type="text/javascript">
            function OnSelectedIndexChanged() {
                __doPostBack($('div[id$="update12"]').attr('id'), 'keyword1_Changed_Or_Anything_Else_You_Might_Want_To_Key_Off_Of');
            }

            $('input[id$="keyword1"]').change(OnSelectedIndexChanged);
</script>--%>
</asp:Content>

