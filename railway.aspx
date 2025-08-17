<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="railway.aspx.cs" Inherits="railway" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <style>
        .container {
            text-align:center
        }
        .borders {
           
          border-radius:10px;
        
        }
        .border2 {
           
         
          border: 1px solid black;
        }
        .margin-text-drop { margin-left: 2.0em; }
         .margin { margin-top: 2.0em; }


         .auto-style1 {
             margin-left: 360px;
         }
    </style>
    

  <div class="row" id="img" runat="server">
  <div class="col-sm-6 col-md-10 col-lg-12">
    <div class="thumbnail" style="align-content:center">
      <img data-src="holder.js/300x300" src="images/at.png" alt="...">
      
    </div>
  </div>
      <%--<div class="caption col-lg-12">
        <h4>Application for Railway Concession will commence <strong>from 5th December 2014</strong></h4>             
      </div>--%>
</div>

 
     <form id="Form1" runat="server" >
         <div><h2> <span class="label label-primary">Apply for Railway Concession</span></h2></div>

         <div style="padding-top:15px">
            <div id="railAlert" runat="server" class="alert alert-danger"></div>
         </div>

<div id="labels" runat="server" class="panel panel-info">
    <div class="panel-body">
        <div class="row">
             <h4 class="borders col-xs-12 col-md-6 col-lg-6">From Station:<asp:DropDownList class="margin-text-drop btn btn-default dropdown-toggle " CssClass="form-control" type="button" data-toggle="dropdown" ID="ddto_stn" AutoPostBack="false" runat="server">
               <%--<asp:ListItem>--SELECT--</asp:ListItem>
                                     <asp:ListItem>Umar Gaon</asp:ListItem>
                                     <asp:ListItem>Dahanu Road</asp:ListItem>
                                     <asp:ListItem>Vangaon</asp:ListItem>
                                     <asp:ListItem>Boisar</asp:ListItem>
                                     <asp:ListItem>Umroli</asp:ListItem>
                                     <asp:ListItem>Palghar</asp:ListItem>
                                     <asp:ListItem>Kelve Road</asp:ListItem>
                                     <asp:ListItem>Saphale</asp:ListItem>
                                     <asp:ListItem>Vaitarna</asp:ListItem>  
                                     <asp:ListItem>Nala Sopara</asp:ListItem>
                                     <asp:ListItem>Vasai Road</asp:ListItem>
                                     <asp:ListItem>Diva Junction</asp:ListItem>
                                     <asp:ListItem>Kopar</asp:ListItem>
                                     <asp:ListItem>Bhiwandi</asp:ListItem>
                                     <asp:ListItem>Kharbao</asp:ListItem>
                                     <asp:ListItem>Kaman Road</asp:ListItem>
                                     <asp:ListItem>Juchandra</asp:ListItem>
                                     <asp:ListItem>Naigaon</asp:ListItem> 
                                     <asp:ListItem>Bhayander</asp:ListItem>
                                     <asp:ListItem>Mira Road</asp:ListItem> 
                                     <asp:ListItem>Dahisar</asp:ListItem>
                                     <asp:ListItem>Borivali</asp:ListItem> 
                                     <asp:ListItem>Kandivali</asp:ListItem>
                                     <asp:ListItem>Malad</asp:ListItem> 
                                     <asp:ListItem>Goregaon</asp:ListItem>
                                     <asp:ListItem>Jogeshwari</asp:ListItem> 
                                     <asp:ListItem>Andheri</asp:ListItem>
                                     <asp:ListItem>Vile Parle</asp:ListItem> 
                                     <asp:ListItem>Santacruz</asp:ListItem>
                                     <asp:ListItem>Khar Road</asp:ListItem> 
                                     <asp:ListItem>Bandra</asp:ListItem>
                                     <asp:ListItem>Mahim Junction</asp:ListItem> 
                                     <asp:ListItem>Matunga Road</asp:ListItem>
                                     <asp:ListItem>Dadar</asp:ListItem> 
                                     <asp:ListItem>Elphinston Road</asp:ListItem>
                                     <asp:ListItem>Lower Parel</asp:ListItem> 
                                     <asp:ListItem>Mahalaxmi</asp:ListItem>
                                     <asp:ListItem>Mumbai Central</asp:ListItem> 
                                     <asp:ListItem>Grant Road</asp:ListItem>
                                     <asp:ListItem>Charni Road</asp:ListItem> 
                                     <asp:ListItem>Marine Lines</asp:ListItem>
                                     <asp:ListItem>Churchgate</asp:ListItem> --%>
                                                            </asp:DropDownList>
             
            </h4>
         <h4 class="borders col-xs-12 col-md-6 col-lg-6" >
             To Station:<asp:Label class="margin-text-drop btn btn-default" type="label"  ID="lblfrom" Text="Virar" CssClass="form-control"  runat="server"></asp:Label>
            </h4>
        </div>
        <div class="row">
            <h4 class="borders col-xs-12 col-md-6 col-lg-6">Class:<asp:DropDownList  class="margin-text-drop btn btn-default dropdown-toggle" type="button" CssClass="form-control" data-toggle="dropdown"  ID="ddl_class" AutoPostBack="false" runat="server">
                                                         <asp:ListItem>--SELECT--</asp:ListItem>
                                                        <asp:ListItem>First Class</asp:ListItem>
                                                        <asp:ListItem>Second Class</asp:ListItem>
                                                                              </asp:DropDownList>
            </h4>
  


         <h4 class="borders col-xs-12 col-md-6 col-lg-6" >Period:<asp:DropDownList  class="margin-text-drop btn btn-default dropdown-toggle " CssClass="form-control" type="button" data-toggle="dropdown" ID="ddperoid"   AutoPostBack="false" runat="server">
                                         <asp:ListItem>--SELECT--</asp:ListItem> 
                                        <asp:ListItem >Monthly</asp:ListItem>
                                        <asp:ListItem>Quarterly</asp:ListItem>
                                                        </asp:DropDownList>
            </h4>
        </div>
        <div class="row">
            <h4 class="borders col-xs-12 col-lg-6">Mobile No:<asp:TextBox  class="margin-text-drop btn btn-default " type="text"  ID="txt_phno" onkeypress="CheckNumeric(event);" CssClass="form-control validate[required]" MaxLength="10"  runat="server"></asp:TextBox>
            </h4>
  


         <h4 class="borders col-xs-12 col-lg-6" >Email:<asp:TextBox  class="margin-text-drop btn btn-default" type="text"  ID="txt_mail" CssClass="form-control validate[required]" runat="server"></asp:TextBox> </h4>   

        </div>
        <div class="row">
              <div class="borders" style="align-content:center" >
         
              
              <%--<h4 class="borders col-xs-12 col-lg-3 ">  <asp:Button ID="btn_new" Width="120pt" runat="server" Text="New Application" CssClass="btn btn-default" OnClick="btn_new_Click"  />   </h4>--%>
             <asp:Button ID="btnapply" runat="server" Text="Submit Application" CssClass="btn btn-default" OnClick="btnapply_Click" />   
               <asp:Button ID="btn_update" runat="server" Text="Update" CssClass="btn btn-default" OnClick="btn_update_Click" Visible="false" ValidationGroup="vgSubmit"/>  
             <asp:Button ID="btncancel" runat="server" Text="Delete" CssClass="btn btn-default" OnClick="btncancel_Click2" Visible="false" />   
  
    </div>
        </div>
        <div class="row">
            <div style="padding-top:10px">
              
                <div id="lblmainerror" runat="server"></div>
             </div>
         </div>
    </div>
</div>

       

          <asp:GridView ID ="grddata" AutoGenerateColumns="false" runat ="server" CssClass="table table-bordered" 
              class="table table-hover table-striped" ForeColor="#333333"  DataKeyNames="stud_id"
               OnRowCancelingEdit="grddata_RowCancelingEdit" OnRowEditing="grddata_RowEditing" OnRowUpdating="grddata_RowUpdating"
              OnRowDeleting="grddata_RowDeleting">
                    <Columns>
                         <asp:TemplateField HeaderText="Requisition ID">
                               <ItemTemplate>
                                   <asp:Label ID="lblreq_id" runat="server" Text='<%#Eval("req_id") %>' />
                               </ItemTemplate>
                               </asp:TemplateField>
                         <asp:TemplateField HeaderText="Student ID">
                               <ItemTemplate>
                                   <asp:Label ID="lblstud_id" runat="server" Text='<%#Eval("stud_id") %>' />
                               </ItemTemplate>
                               </asp:TemplateField>

                        <asp:TemplateField HeaderText="From Station">
                                <ItemTemplate>
                                   <asp:Label ID="lblto_stn" runat="server" Text='<%#Eval("to_stn") %>' />
                               </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Period">
                              <ItemTemplate>
                                   <asp:Label ID="lblperiod" runat="server" Text='<%#Eval("period") %>' />
                               </ItemTemplate>
                             </asp:TemplateField>

                         <asp:TemplateField HeaderText="Class">
                             <ItemTemplate>
                                   <asp:Label ID="lblclass" runat="server" Text='<%#Eval("class") %>' />
                               </ItemTemplate>
                             </asp:TemplateField>

                        <asp:TemplateField HeaderText="Apply Date">
                             <ItemTemplate>
                                   <asp:Label ID="lbldt" runat="server" Text='<%#Eval("curr_dt") %>' />
                               </ItemTemplate>
                             </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Receipt No.">
                             <ItemTemplate>
                                   <asp:Label ID="lblReceipt" runat="server" Text='<%#Eval("receipt_no") %>' />
                               </ItemTemplate>
                             </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date of action">
                             <ItemTemplate>
                                   <asp:Label ID="lblapprove" runat="server" Text='<%#Eval("req_dt") %>' />
                               </ItemTemplate>
                             </asp:TemplateField>

                        <asp:TemplateField HeaderText="Pass No">
                           <%-- <EditItemTemplate>
                                <asp:TextBox ID="txtpass_no" runat="server" Text='<%#Eval("pass_no") %>' />
                            </EditItemTemplate>--%>
                            <ItemTemplate>
                                <asp:Label ID="lblpass_no" runat="server" Text='<%#Eval("pass_no") %>' />
                            </ItemTemplate>
                            </asp:TemplateField>

                        <asp:TemplateField HeaderText =" Edit/Delete" >
                            <%--<EditItemTemplate>
                                <asp:ImageButton ID="imgbtnUpdate2" CommandName="Update" runat="server" ImageUrl="~/Images/update.jpg" ToolTip="Update" Height="20px" Width="20px" />
                                <asp:ImageButton ID="imgbtnCancel2" runat="server" CommandName="Cancel" ImageUrl="~/Images/Cancel.jpg" ToolTip="Cancel" Height="20px" Width="20px" />
                            </EditItemTemplate>--%>
                            <ItemTemplate>
                                 <asp:Button ID="btn_edit" runat="server" Text="Edit" CommandName="edit" > </asp:Button>
                               <%--<asp:Button ID="btn_delete" runat="server" Text="Delete" CommandName="delete" ></asp:Button>--%>
                            </ItemTemplate>
                             </asp:TemplateField>
                    </Columns>
              <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></FooterStyle>
               <PagerStyle HorizontalAlign="Center" BackColor="#284775" ForeColor="White"></PagerStyle>
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>
                 <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White"></HeaderStyle>
                <EditRowStyle BackColor="#999999"></EditRowStyle>
              <AlternatingRowStyle BackColor="White" ForeColor="#284775"></AlternatingRowStyle>
                </asp:GridView>

                 
    
         </form>
   
</asp:Content>

