using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Specialized;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Configuration;
using System.IO;

public partial class NCRENB_receipt : System.Web.UI.Page
{
    Class1 cls=new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack){
          NameValueCollection nvc = Request.Form;

          if (Request.Params["mmp_txn"] != null)
          {
              string postingmmp_txn = Request.Params["mmp_txn"].ToString();
              string postingmer_txn = Request.Params["mer_txn"];
              string postinamount = Request.Params["amt"].ToString();
              string postingprod = Request.Params["prod"].ToString();
              string postingdate = Request.Params["date"].ToString();
              string postingbank_txn = Request.Params["bank_txn"].ToString();
              string postingf_code = Request.Params["f_code"].ToString();
              string postingbank_name = Request.Params["bank_name"].ToString();
              string signature = Request.Params["signature"].ToString();
              string postingdiscriminator = Request.Params["discriminator"].ToString();
              //string customername = Request.Params["udf1"].ToString();
              //string customermail = Request.Params["udf2 "].ToString();
              //string customerno = Request.Params["udf3 "].ToString();
              string customername = "";
              string customermail = "";
              string customerno = "";

              string respHashKey = "13368ae603a8f909ff";
              //string respHashKey = "KEYRESP123657234";
              string ressignature = "";
              string strsignature = postingmmp_txn + postingmer_txn + postingf_code + postingprod + postingdiscriminator + postinamount + postingbank_txn;
              //string strsignature = postingmmp_txn + postingmer_txn1 + postingf_code + postingprod + discriminator + postinamount + postingbank_txn;
              byte[] bytes = Encoding.UTF8.GetBytes(respHashKey);
              byte[] b = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
              ressignature = byteToHexString(b).ToLower();

              if (signature == ressignature)
              {
                  if (postingf_code == "F")
                  {
                      // Response.Redirect("http://localhost:2394/student/form_final.aspx");
                  //    lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                  }
                  else if (postingf_code == "C")
                  {
                      //Response.Redirect("http://localhost:2394/student/form_final.aspx");
                     // lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                  }
                  else if (postingf_code == "S")
                  {
                   //   lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                  }
                  else if (postingf_code == "Ok")
                  {
                    //  lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                  }
              }
              else
              {
                  //                        lblStatus.Text = "Signature Mismatched...";Response.Redirect("http://203.192.254.34/STUDENT_ERP/FEE_RECIEPT_COPY.ASPX");
                  //lblStatus.Text = "Signature Mismatched..."; Response.Redirect("http://localhost:2394/student/form_final.aspx");

              }
              //Response.Redirect("http://203.192.254.34/STUDENT_ERP/FEE_RECIEPT_COPY.ASPX?postingmmp_txn="+postingmmp_txn+"&postingmer_txn="+postingmer_txn+"&postinamount="+postinamount+"&postingprod="+postingprod+"&postingdate="+postingdate+"&postingbank_txn="+postingbank_txn+"&postingf_code="+postingf_code+"&postingbank_name="+postingbank_name+"&postingdiscriminator="+postingdiscriminator+"&customername="+customername+"&customermail="+customermail+"&customerno="+customerno);
              // Response.Redirect("http://vivacollege.in/student/fee_reciept_copy_pay.ASPX?postingmmp_txn=" + postingmmp_txn + "&postingmer_txn=" + postingmer_txn + "&postinamount=" + postinamount + "&postingprod=" + postingprod + "&postingdate=" + postingdate + "&postingbank_txn=" + postingbank_txn + "&postingf_code=" + postingf_code + "&postingbank_name=" + postingbank_name + "&postingdiscriminator=" + postingdiscriminator + "&customername=" + customername + "&customermail=" + customermail + "&customerno=" + customerno);


              //try
              //{
              String[] val = new String[0];
              string id = postingmer_txn.Substring(0,8);

              //id = Session["emp_id"].ToString();;
              //if (postingmer_txn.Contains("2018"))
              //{
              //    val = postingmer_txn.Split('2018');
              //    id = val[0] + "A";
              //}
              //else if (postingmer_txn.Contains("J")) { val = postingmer_txn.Split('A'); id = val[0] + "J"; }
              //string str11212 = "select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
              //DataSet ds11212 = cls.fill_dataset(str11212);
              string str = "    update processing_fees set name='" + customername + "',mobile_no='" + customerno + "',email='" + customermail + "',postinamount='" + postinamount + "',postingmmp_txn='" + postingmmp_txn + "',postingprod='" + postingprod + "',postingdate='" + postingdate + "',postingbank_txn='" + postingbank_txn + "',postingf_code='" + postingf_code + "',postingbank_name='" + postingbank_name + "',signature='Matched',postingdiscriminator='" + postingdiscriminator + "',ayid=(select max(ayid) from m_academic where iscurrent='1'),curr_dt=getdate() where form_no='" + id + "' and postingmer_txn='" + postingmer_txn + "'";
           //   + "update kt_exam_pay_details set Status='Paid' where stud_id='" + id + "' and exam like '%" + ds11212.Tables[0].Rows[0][0].ToString() + "%' and receipt_no='" + postingmer_txn + "'";
              //  string str = "insert into processing_fees values('" + id + "','" + customername + "','" + customerno + "','" + customermail + "','" + postinamount + "','" + postingmmp_txn + "','" + postingmer_txn + "','" + postingprod + "','" + postingdate + "','" + postingbank_txn + "','" + postingf_code + "','" + postingbank_name + "','Matched','" + postingdiscriminator + "','Fees','" + Session["ayid"].ToString() + "',getdate())";



              cls.update_data(str);
              DataSet ds=new DataSet();
              if (postingf_code == "S" || postingf_code == "Ok")
              {
                    string s1 = "select stud_F_Name+' '+stud_M_Name+' '+stud_L_Name as name,b.paper_id,b.paper_name,(select duration from m_academic where ayid=b.ayid) as yr from m_std_personaldetails_tbl as a,NCRENB as b where  a.stud_id='" + id + "' and b.transaction_id='" + postingmer_txn + "'";

                    ds =cls.fill_dataset(s1);
                  if (ds.Tables[0].Rows.Count > 0)
                  {
                     txt_id.Text=id;
                      Label1.Text=ds.Tables[0].Rows[0]["name"].ToString();
                      Label2.Text=ds.Tables[0].Rows[0]["paper_id"].ToString();
                      Label3.Text=ds.Tables[0].Rows[0]["paper_name"].ToString();
                      Label4.Text=postinamount;
                      Label5.Text=postingmer_txn;
                      Label6.Text=ds.Tables[0].Rows[0]["yr"].ToString();
                  }
                    lbl_status.Text = " Successful";
              }
              else
              {
                    string s1 = "select stud_F_Name+' '+stud_M_Name+' '+stud_L_Name as name,b.paper_id,b.paper_name,(select duration from m_academic where ayid=b.ayid) as yr from m_std_personaldetails_tbl as a,NCRENB as b where  a.stud_id='" + id + "' and b.transaction_id='" + postingmer_txn + "'";

                    ds = cls.fill_dataset(s1);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txt_id.Text = id;
                        Label1.Text = ds.Tables[0].Rows[0]["name"].ToString();
                        Label2.Text = ds.Tables[0].Rows[0]["paper_id"].ToString();
                        Label3.Text = ds.Tables[0].Rows[0]["paper_name"].ToString();
                        Label4.Text = postinamount;
                        Label5.Text = postingmer_txn;
                        Label6.Text = ds.Tables[0].Rows[0]["yr"].ToString();
                    }
                    if (postingf_code == "C")
                    {
                        lbl_status.Text = " Cancelled";
                    }
                    else if (postingf_code == "F")
                    {
                        lbl_status.Text = " Unsuccessful";
                    }
              }
          }
    }
    }
    public static string byteToHexString(byte[] byData)
    {
        StringBuilder sb = new StringBuilder((byData.Length * 2));
        for (int i = 0; (i < byData.Length); i++)
        {
            int v = (byData[i] & 255);
            if ((v < 16))
            {
                sb.Append('0');
            }

            sb.Append(v.ToString("X"));

        }

        return sb.ToString();
    }
}