using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class adm_prv_recpt : System.Web.UI.Page
{
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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

                string customername = "";
                string customermail = "";
                string customerno = "";

                string respHashKey = "13368ae603a8f909ff";
                string ressignature = "";
                string strsignature = postingmmp_txn + postingmer_txn + postingf_code + postingprod + postingdiscriminator + postinamount + postingbank_txn;
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

                String[] val = new String[0];
                string id = postingmer_txn.Substring(0, 8);


                string str = "    update processing_fees set name='" + customername + "',mobile_no='" + customerno + "',email='" + customermail + "',postinamount='" + postinamount + "',postingmmp_txn='" + postingmmp_txn + "',postingprod='" + postingprod + "',postingdate='" + postingdate + "',postingbank_txn='" + postingbank_txn + "',postingf_code='" + postingf_code + "',postingbank_name='" + postingbank_name + "',signature='Matched',postingdiscriminator='" + postingdiscriminator + "',ayid=(select max(ayid) from m_academic where iscurrent='1'),curr_dt=getdate() where form_no='" + id + "' and postingmer_txn='" + postingmer_txn + "'";

                cls.update_data(str);
                DataSet ds = new DataSet();
                if (postingf_code == "S" || postingf_code == "Ok")
                {
                    string s1 = "select stud_F_Name+' '+stud_M_Name+' '+stud_L_Name as name,(select duration from m_academic where ayid=b.ayid) as yr from m_std_personaldetails_tbl as a,Adm_Provisional as b where  a.stud_id='" + id + "' and b.transaction_id='" + postingmer_txn + "'";

                    ds = cls.fill_dataset(s1);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txt_id.Text = id;
                        Label1.Text = ds.Tables[0].Rows[0]["name"].ToString();
                        Label4.Text = postinamount;
                        Label5.Text = postingmer_txn;
                        Label6.Text = ds.Tables[0].Rows[0]["yr"].ToString();
                    }
                    lbl_status.Text = " Successful";
                }
                else
                {
                    string s1 = "select stud_F_Name+' '+stud_M_Name+' '+stud_L_Name as name,(select duration from m_academic where ayid=b.ayid) as yr from m_std_personaldetails_tbl as a,Adm_Provisional as b where  a.stud_id='" + id + "' and b.transaction_id='" + postingmer_txn + "'";

                    ds = cls.fill_dataset(s1);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        txt_id.Text = id;
                        Label1.Text = ds.Tables[0].Rows[0]["name"].ToString();
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