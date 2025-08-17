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

public partial class reval_form : System.Web.UI.Page
{
    DataSet ds;
    DataTable dt;
    Class1 cls = new Class1();

    string stud_id;
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
                //string customername = Request.Params["udf1"].ToString();
                //string customermail = Request.Params["udf2 "].ToString();
                //string customerno = Request.Params["udf3 "].ToString();
                string customername = "";
                string customermail = "";
                string customerno = "";

                string respHashKey = "13368ae603a8f909ff";
                // string respHashKey = "KEYRESP123657234";
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
                string id = "";
                Session["transid"] = postingmer_txn.ToString();
                id = postingmer_txn.Substring(0, 8);
                //if (postingmer_txn.Contains("2018"))
                //{
                //    val = postingmer_txn.Split('2018');
                //    id = val[0] + "A";
                //}
                //else if (postingmer_txn.Contains("J")) { val = postingmer_txn.Split('A'); id = val[0] + "J"; }
                string str11212 = "select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
                DataSet ds11212 = cls.fill_dataset(str11212);
                string str = "    update processing_fees set name='" + customername + "',mobile_no='" + customerno + "',email='" + customermail + "',postinamount='" + postinamount + "',postingmmp_txn='" + postingmmp_txn + "',postingprod='" + postingprod + "',postingdate='" + postingdate + "',postingbank_txn='" + postingbank_txn + "',postingf_code='" + postingf_code + "',postingbank_name='" + postingbank_name + "',signature='Matched',postingdiscriminator='" + postingdiscriminator + "',ayid=(select max(ayid) from m_academic where iscurrent='1'),curr_dt=getdate() where form_no='" + id + "' and postingmer_txn='" + postingmer_txn + "' and Status like '%RExam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+'%'";
                //   + "update kt_exam_pay_details set Status='Paid' where stud_id='" + id + "' and exam like '%" + ds11212.Tables[0].Rows[0][0].ToString() + "%' and receipt_no='" + postingmer_txn + "'";
                //  string str = "insert into processing_fees values('" + id + "','" + customername + "','" + customerno + "','" + customermail + "','" + postinamount + "','" + postingmmp_txn + "','" + postingmer_txn + "','" + postingprod + "','" + postingdate + "','" + postingbank_txn + "','" + postingf_code + "','" + postingbank_name + "','Matched','" + postingdiscriminator + "','Fees','" + Session["ayid"].ToString() + "',getdate())";



                cls.update_data(str);


                string s1 = "select stud_F_Name,stud_M_Name,stud_L_Name,stud_Mother_FName,stud_Gender,stud_BloodGroup,dbo.www_date_display_personal(stud_DOB) as DOB,stud_Nationality,stud_BirthPlace,stud_DomiciledIn,stud_PermanentAdd,stud_PermanentPhone,stud_NativePhone,stud_Category,stud_Caste,stud_Religion,stud_MotherTounge,stud_MartialStatus,stud_Email from dbo.m_std_personaldetails_tbl where stud_id='" + id + "';select *,(select SUBSTRING(Duration ,9,4) as year from m_academic where IsCurrent=1) as year,(select course_name from m_crs_course_tbl where course_id=a.branch_id) as branch from PR_Details as a where ext3=(select max(ayid) from m_academic where iscurrent='1') and stud_id='" + id + "' and reval_flag='1' and del_flag=0 and ext2=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month) and sem_id=(select distinct SUBSTRING(SUBSTRING(Status, CHARINDEX(':', Status)  + 1, LEN(Status)), CHARINDEX(':', SUBSTRING(Status, CHARINDEX(':', Status)  + 1, LEN(Status)))  + 1, LEN(SUBSTRING(Status, CHARINDEX(':', Status)  + 1, LEN(Status)))) from processing_fees where form_no='" + id + "'  and status like 'R%' and postingmer_txn='" + postingmer_txn + "')";
                ds = cls.fill_dataset(s1);
                string stud_id = "", stud_name = "", stud_caste = "", stud_mobno = "", stud_email = "", stud_add = "", category = "";

                if (ds.Tables[0].Rows.Count > 0)
                {

                    //stud_id = id;
                    //stud_name = ds.Tables[0].Rows[0]["stud_L_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_F_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_M_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_Mother_FName"].ToString();
                    //stud_caste = ds.Tables[0].Rows[0]["stud_Caste"].ToString();
                    //stud_mobno = ds.Tables[0].Rows[0]["stud_PermanentPhone"].ToString();
                    //stud_email = ds.Tables[0].Rows[0]["stud_Email"].ToString();
                    //stud_add = ds.Tables[0].Rows[0]["stud_PermanentAdd"].ToString();
                    //category = ds.Tables[0].Rows[0]["stud_category"].ToString();

                    //    Session["cat"] = category;
                    //    Session["Student_Name"] = stud_name;
                    //    Session["Student_Address"] = stud_add;
                    //    Session["Student_Caste"] = stud_caste;
                    //    Session["Student_Mobno"] = stud_mobno;
                    //Session["Student_Email"] = stud_email;



                    label3.Text = id;
                    label1.Text = ds.Tables[0].Rows[0]["stud_L_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_F_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_M_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_Mother_FName"].ToString();
                    label2.Text = ds.Tables[0].Rows[0]["stud_PermanentAdd"].ToString();

                    label4.Text = ds.Tables[0].Rows[0]["stud_PermanentPhone"].ToString();
                    label7.Text = ds.Tables[0].Rows[0]["stud_Email"].ToString();


                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    pattern_txt.Text = ds.Tables[1].Rows[0]["Pattern"].ToString();
                    txt_year.Text = ds.Tables[1].Rows[0]["year"].ToString();
                    
                    TextBox5.Text = ds.Tables[1].Rows[0]["branch"].ToString();
                    sem_txt.Text = ds.Tables[1].Rows[0]["sem_id"].ToString();
                    sem_txt.Text = ds.Tables[1].Rows[0]["sem_id"].ToString();
                    lbl_seat.Text = ds.Tables[1].Rows[0]["seat_no"].ToString();
                    TextBox9.Text = ds.Tables[1].Rows[0]["seat_no"].ToString();
                    pattern_txt.Text = ds.Tables[1].Rows[0]["Pattern"].ToString();
                    if (ds.Tables[1].Rows[0]["ext3"].ToString() == "Nov")
                    {
                        txt_month.Text = "November";
                    }   
                    else
                    {
                        txt_month.Text = "June";
                    }
                    date_lbl.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
                }
                if (postingf_code == "S" || postingf_code == "Ok")
                {
                    lbl_stat.Text = "Paid";
                }
                else
                {
                    lbl_stat.Text = "Unpaid";
                }
            }
        }
    }
    public string getWhileLoopData()
    {
        
            string htmlStr = ""; string filltable = ""; string[] arr;
            DataTable dtpro = new DataTable();
            dt = new DataTable();
            string transid = Session["transid"].ToString();
            if (Session["transid"].ToString() != "" && Session["transid"].ToString() != null)
            {
                transid = Session["transid"].ToString();
            }
            string str = "select Status,ayid from processing_fees where postingmer_txn='" + transid + "'";
            dtpro = cls.fildatatable(str);
            arr = dtpro.Rows[0]["status"].ToString().Split(':');
            filltable = "select subject_name,paper_code,marks_obtained from PR_Details where stud_id='" + transid.Substring(0, 8) + "'  and sem_id='" + arr[2].ToString() + "' and ext2='" + arr[1].ToString() + "' and ext3='" + dtpro.Rows[0]["ayid"].ToString() + "' and reval_flag=1 and del_flag=0";


            dt = cls.fildatatable(filltable);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string sr_no = (i + 1).ToString();
                string Subj_Name = dt.Rows[i]["subject_name"].ToString();
                string Marks_obt = dt.Rows[i]["marks_obtained"].ToString();
                string paper_code = dt.Rows[i]["paper_code"].ToString();
                htmlStr += "<tr style='font-size:12px;'><td>" + sr_no + "</td><td>" + Subj_Name + "</td><td>" + paper_code + "</td><td>" + Marks_obt + "</td></tr>";
            }
            return htmlStr;
       
        
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