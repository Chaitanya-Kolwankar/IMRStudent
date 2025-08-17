<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Result.aspx.cs" Inherits="Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="Form1" role="form" runat="server">
        <div id="message" runat="server" visible ="false" style="font-size:25px">THERE ARE NO RESULTS AVAILABLE FOR YOU</div>
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        
        <div id="result" runat="server" visible="false">
        <div class="row" style="margin-top:10px">
                                <div class="col-lg-2">
         <div class="form-group">
        <label>Semester</label>
             <asp:DropDownList ID="ddlSem" AutoPostBack="true" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlSem_SelectedIndexChanged"></asp:DropDownList>
         </div>
        </div>
        <div class="col-lg-2">
         <div class="form-group">
        <label>Exam</label>
             <asp:DropDownList ID="ddlExam" runat="server" CssClass="form-control"></asp:DropDownList>
         </div>
        </div>
                <div class="col-lg-2" style="margin-top:25px">
                    <asp:Button ID="btnOk" Text="Get Result" runat="server" cssClass="btn btn-default" OnClick="btnOk_Click" />
        </div>
    </div>
             <div class="row" style="margin-top:10px">
                 <div class="alert-danger" id="divmsg" runat="server">

                 </div>
             </div>
                                <div class="panel panel-success" id="divResult" runat="server" visible="false" style="margin-top:20px">
                        <div class="panel-heading">
                            Result
                        </div>
                        <div class="panel-body">
        <div class="row">
            <div class="col-lg-2">
                Name : 
            </div>
              <div class="col-lg-10">
               <asp:Label ID="lblStudName" runat="server" Font-Bold="true" Text=""></asp:Label>
            </div>
        </div>
                            <div class="row">
            <div class="col-lg-2">
                Student ID :
            </div>
             <div class="col-lg-10">
                <asp:Label ID="lblStudID" runat="server" Font-Bold="true" Text=""></asp:Label>
            </div>
        </div>
            <div class="row">
            <div class="col-lg-2">
                Programme : 
            </div>
            <div class="col-lg-10">
                <asp:Label ID="lblProgram" runat="server" Font-Bold="true" Text=""></asp:Label>
            </div>
        </div>
             <div class="row">
            <div class="col-lg-2">
                Examination Seat No. : 
            </div>
                 <div class="col-lg-10">
                <asp:Label ID="lblSeatNo" runat="server" Font-Bold="true" Text=""></asp:Label>
            </div>
        </div>
             <div class="row">
            <div class="col-lg-2">
                Month & Year of Examination :
            </div>
                 <div class="col-lg-10">
                <asp:Label ID="lblExmDate" runat="server" Font-Bold="true" Text=""></asp:Label>
            </div>
        </div>
                            <div id="divMarks" runat="server" class="row" style="margin-top:20px">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:Table class="table table-bordered watermark" id="tblResult" HorizontalAlign="Center" runat="server" style="text-align:center;vertical-align:middle">
                                            <asp:TableRow>
          <asp:TableCell CssClass="alignment">
         <div class="row">
             <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" ><b style="text-align:center">Course Code</b></div>
         </div></asp:TableCell><asp:TableCell CssClass="alignment">
              <div class="row">
<div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" ><b style="text-align:center">Course Title</b></div></div>
          </asp:TableCell><asp:TableCell ColumnSpan="3" CssClass="alignment"> 
              <div class="row">
              <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12"><b style="text-align:center">Internal Assessment</b></div>
                  </div>
              <div class="row">
              <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4"><b style="text-align:center;font-size:12px">Max</b></div>
                  <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4"><b style="text-align:center;font-size:12px">Min</b></div>
                  <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" ><b style="text-align:center;font-size:12px">Obt</b></div>
                  </div>
          </asp:TableCell><asp:TableCell ColumnSpan="3" CssClass="alignment">
              <div class="row">
              <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" ><b style="text-align:center">End Semester Examination</b></div>
                  </div>
                            <div class="row">
              <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4"><b style="text-align:center;font-size:12px">Max</b></div>
                  <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4"><b style="text-align:center;font-size:12px">Min</b></div>
                  <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" ><b style="text-align:center;font-size:12px">Obt</b></div>
                  </div>
          </asp:TableCell><asp:TableCell ColumnSpan="3" CssClass="alignment">
              <div class="row">
              <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" ><b style="text-align:center">Total Marks</b></div>
                  </div>
                            <div class="row">
              <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4"><b style="text-align:center;font-size:12px">Max</b></div>
                  <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4"><b style="text-align:center;font-size:12px">Min</b></div>
                  <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4" ><b style="text-align:center;font-size:12px">Obt</b></div>
                  </div>
          </asp:TableCell><asp:TableCell CssClass="alignment">
              <div class="row">
              <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" ><b style="text-align:center">Grade</b></div>
                  </div>
          </asp:TableCell><asp:TableCell CssClass="alignment">
              <div class="row">
              <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" ><b style="text-align:center">Grade Point</b></div>
                  </div>
          </asp:TableCell><asp:TableCell CssClass="alignment">
              <div class="row">
              <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" ><b style="text-align:center">Credit Point</b></div>
                  </div>
          </asp:TableCell><asp:TableCell CssClass="alignment">
              <div class="row">
              <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" ><b style="text-align:center">CG = C x G</b></div>
                  </div>
          </asp:TableCell><asp:TableCell CssClass="alignment">
              <div class="row">
              <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12" ><b style="text-align:center">SGPA = ΣCG/Σ C</b></div>
                  </div>
          </asp:TableCell></asp:TableRow><asp:TableRow>
           <asp:TableCell ColumnSpan="2"></asp:TableCell>
    <asp:TableCell ColumnSpan="6">Total</asp:TableCell><asp:TableCell><asp:Label ID="lblTotal" Text="" runat="server"></asp:Label></asp:TableCell>
              <asp:TableCell></asp:TableCell><asp:TableCell><asp:Label ID="lblTotalObt" Text="" runat="server"></asp:Label></asp:TableCell><asp:TableCell ColumnSpan="2"></asp:TableCell>
              <asp:TableCell><asp:Label ID="lblTotalCP" Text="" runat="server"></asp:Label></asp:TableCell>
              <asp:TableCell><asp:Label ID="lblTotalCXG" Text="" runat="server"></asp:Label></asp:TableCell>
              <asp:TableCell></asp:TableCell></asp:TableRow>
                                            <asp:TableRow>
           <asp:TableCell ColumnSpan="2">Remark: <asp:Label ID="lbl_Remark" Text="" runat="server"></asp:Label></asp:TableCell>
           <asp:TableCell ColumnSpan="9">Credits Earned : <asp:Label ID="lblCreditsEarned" Text="" runat="server"></asp:Label></asp:TableCell>
           <asp:TableCell ColumnSpan="2">Grade : <asp:Label ID="lblgrade" Text="" runat="server"></asp:Label></asp:TableCell>
           <asp:TableCell>ΣC = <asp:Label ID="lblSummision" Text="" runat="server"></asp:Label></asp:TableCell>
           <asp:TableCell>ΣCG = <asp:Label ID="lblSumCG" Text="" runat="server"></asp:Label></asp:TableCell>
           <asp:TableCell>SGPA = <asp:Label ID="lblSGPA" Text="" runat="server"></asp:Label></asp:TableCell></asp:TableRow><%--<asp:TableRow>
           
           <asp:TableCell align="left" ColumnSpan="16">
               <div class="row">
                   <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                       F in front of marks indicates failure @ 5042/5043/5044, *5045,# 0.229,EX exemption, + Carried Forward F: fail
                   </div>
               </div>
                <div class="row">
                   <div class="col-lg-4 col-md-4 col-sm-4 col-xs-4">
                      Place : Virar
                   </div>
               </div>
               <div class="row">
                   <div class="col-lg-3 col-md-3 col-sm-3 col-xs-3">
                     Date :
                   </div>
               </div>
           </asp:TableCell></asp:TableRow>--%></asp:Table></div></div></div><!-- /.row (nested) --></div><!-- /.col-lg-12 --></div>
            </div></ContentTemplate>
        </asp:UpdatePanel>
        </form>
</asp:Content>

