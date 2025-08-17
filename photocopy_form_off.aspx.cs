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

public partial class photocopy_form_off : System.Web.UI.Page
{
    DataSet ds;
    DataTable dt;
    Class1 cls = new Class1();

    string stud_id;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string s1 = "select stud_F_Name,stud_M_Name,stud_L_Name,stud_Mother_FName,stud_Gender,stud_BloodGroup,dbo.www_date_display_personal(stud_DOB) as DOB,stud_Nationality,stud_BirthPlace,stud_DomiciledIn,stud_PermanentAdd,stud_PermanentPhone,stud_NativePhone,stud_Category,stud_Caste,stud_Religion,stud_MotherTounge,stud_MartialStatus,stud_Email from dbo.m_std_personaldetails_tbl where stud_id='" + Session["UserName"].ToString() + "';select *,(select SUBSTRING(Duration ,9,4) as year from m_academic where IsCurrent=1) as year,(select course_name from m_crs_course_tbl where course_id=a.branch_id) as branch  from PR_Details as a where ext3=(select max(ayid) from m_academic where iscurrent='1') and stud_id='" + Session["UserName"].ToString() + "' and photocopy_flag='1' and del_flag=0 and ext2=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month) AND SEM_ID='" + Session["semester"] + "'";
            ds = cls.fill_dataset(s1);
            string stud_id = "", stud_name = "", stud_caste = "", stud_mobno = "", stud_email = "", stud_add = "", category = "";

            if (ds.Tables[0].Rows.Count > 0)
            {
                stud_id = Session["UserName"].ToString();
                label1.Text = ds.Tables[0].Rows[0]["stud_L_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_F_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_M_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_Mother_FName"].ToString();
                label4.Text = ds.Tables[0].Rows[0]["stud_PermanentPhone"].ToString();
                label7.Text = ds.Tables[0].Rows[0]["stud_Email"].ToString();
                label2.Text = ds.Tables[0].Rows[0]["stud_PermanentAdd"].ToString();
            }


            if (ds.Tables[1].Rows.Count > 0)
            {
                txt_year.Text = ds.Tables[1].Rows[0]["year"].ToString();
                TextBox5.Text = ds.Tables[1].Rows[0]["branch"].ToString();

                pattern_txt.Text = ds.Tables[1].Rows[0]["Pattern"].ToString();
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
            }
            string str112 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and Status like 'PExam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + Session["semester"] + "' and status like 'P%'  and postingf_code in ('S','Ok')";
            DataSet dt2 = cls.fill_dataset(str112);
            if (dt2.Tables[0].Rows.Count > 0)
            {
                lbl_stat.Text = "Paid";
            }
            else
            {
                lbl_stat.Text = "UnPaid";
            }
            date_lbl.Text = DateTime.Now.Date.ToString("dd-MM-yyyy");
            label3.Text = Session["Username"].ToString();
        }
    }

    public string getWhileLoopData()
    {
        string htmlStr = "";
        ds = new DataSet();
        dt = new DataTable();
        stud_id = Session["UserName"].ToString();

        dt = (DataTable)Session["MyTable_Photocopy"];

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