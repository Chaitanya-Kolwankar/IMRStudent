using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class KT_exm_new : System.Web.UI.Page
{
    DataSet ds;
    DataTable dt;
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        string semid = "", exmcode="";
        if (Session["stud_det"] != null && Session["stud_det"].ToString() != "")
        {
            string[] arr = Session["stud_det"].ToString().Split(':');
             semid = arr[0].ToString();
             exmcode = arr[1].ToString();
        }
        string qry = "";
        if (semid == "Sem-1" || semid == "Sem-2" || semid == "Sem-7" || semid == "Sem-8")
        {
             qry = "select sem_id,receipt_no,status,amount,previous_stud_id from kt_exam_pay_details where stud_id='" + Session["username"].ToString() + "' and ayid=(select ayid from m_academic where iscurrent='1') and sem_id='" + semid + "' and exm_date in (select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month) and exm_type='" + exmcode + "' ;";
        }
        else
        {
             qry = "select sem_id,receipt_no,status,amount,previous_stud_id from kt_exam_pay_details where stud_id='" + Session["username"].ToString() + "' and ayid=(select ayid from m_academic where iscurrent='1') and sem_id='" + semid + "' and exm_date in (select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month) and exm_type='" + exmcode + "' ;select seat_no from  engg_viva.dbo.cre_marks_tbl where stud_id='" + Session["prev_id"].ToString() + "'";
        }
        DataSet ds = cls.fill_dataset(qry);
        lblname.Text = Session["Student_Name"].ToString();

        if (Session["Pattern_kt_exm1"].ToString().Contains("CS"))
        {
            lblprogram.Text = "Choice Base";
        }
        else
        {
            lblprogram.Text = "CBGS";
        }
        lblstud_id.Text = Session["UserName"].ToString();
        lblsem.Text = Session["Semester"].ToString() + " " + "(KT)";
        lbl_chalan.Text = ds.Tables[0].Rows[0]["receipt_no"].ToString();
      
        lbl_status.Text = ds.Tables[0].Rows[0]["status"].ToString();
        lblfee.Text = ds.Tables[0].Rows[0]["amount"].ToString();
        Label2.Text = Session["exam_val"].ToString();
        Label7.Text = Session["exam_val"].ToString();
        Label1.Text = Session["Exam_form_branch"].ToString();
        lblname2.Text = Session["Student_Name"].ToString();
        lblprogram2.Text = Session["Pattern_kt_exm1"].ToString();
        lblstud_id2.Text = Session["UserName"].ToString();
        lblsem2.Text = Session["Semester"].ToString() + " " + "(KT)";
        lbl_chalan2.Text = ds.Tables[0].Rows[0]["receipt_no"].ToString();
        
        lbl_status2.Text = ds.Tables[0].Rows[0]["status"].ToString();
        lblfee2.Text = ds.Tables[0].Rows[0]["amount"].ToString();

        if (semid != "Sem-1" && semid != "Sem-2" && semid != "Sem-7" && semid != "Sem-8")
        {
            lbl_seat_no.Text = ds.Tables[1].Rows[0]["seat_no"].ToString();
            lbl_seat_no2.Text = ds.Tables[1].Rows[0]["seat_no"].ToString();
        }
    }

    public string getWhileLoopData()
    {
        string htmlStr = "";
        ds = new DataSet();
        dt = new DataTable();
        string stud_id = Session["UserName"].ToString();

        dt = (DataTable)Session["KT_exm_grid"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string sr_no = (i + 1).ToString();
            string Subj_Name = dt.Rows[i]["subj_name"].ToString();
            string theory_mrks = dt.Rows[i]["theory_mrks"].ToString();
            string internal_mrks = dt.Rows[i]["internal_mrks"].ToString();
            string term_wrks = dt.Rows[i]["term_wrks"].ToString();
            string pract_mrks = dt.Rows[i]["pract_mrks"].ToString();
            htmlStr += "<tr style='font-size:14px;'><td>" + sr_no + "</td><td>" + Subj_Name + "</td><td>" + theory_mrks + "</td><td>" + internal_mrks + "</td><td>" + term_wrks + "</td><td>" + pract_mrks + "</td></tr>";
        }
        return htmlStr;
    }
}