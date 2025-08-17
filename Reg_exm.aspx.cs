using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reg_exm : System.Web.UI.Page
{
    DataSet ds;
    DataTable dt;
    string stud_id = "", stud_name = "", stud_caste = "", stud_mobno = "", stud_email = "", stud_add = "";
    //string pattern_exm;
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        ds = new DataSet();
        string s1 = "select stud_F_Name,stud_M_Name,stud_L_Name,stud_Mother_FName,stud_Gender,stud_BloodGroup,dbo.www_date_display_personal(stud_DOB) as DOB,stud_Nationality,stud_BirthPlace,stud_DomiciledIn,stud_PermanentAdd,stud_PermanentPhone,stud_NativePhone,stud_Category,stud_Caste,stud_Religion,stud_MotherTounge,stud_MartialStatus,stud_Email from dbo.m_std_personaldetails_tbl where stud_id='" + Session["UserName"] + "'";
        ds = cls.fill_dataset(s1);

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                stud_id = Session["UserName"].ToString();
                stud_name = ds.Tables[0].Rows[i]["stud_L_Name"].ToString() + " " + ds.Tables[0].Rows[i]["stud_F_Name"].ToString() + " " + ds.Tables[0].Rows[i]["stud_M_Name"].ToString() + " " + ds.Tables[0].Rows[i]["stud_Mother_FName"].ToString();
                stud_caste = ds.Tables[0].Rows[i]["stud_Caste"].ToString();
                stud_mobno = ds.Tables[0].Rows[i]["stud_PermanentPhone"].ToString();
                stud_email = ds.Tables[0].Rows[i]["stud_Email"].ToString();
                stud_add = ds.Tables[0].Rows[i]["stud_PermanentAdd"].ToString().ToUpper();

                name_txt.Text = stud_name;
                caste_txt.Text = stud_caste;
                mobno_txt.Text = stud_mobno;
                emailid_txt.Text = stud_email;
                add_txt.Text = stud_add;

                Session["Student_Name"] = stud_name;
                Session["Student_Address"] = stud_add;
                Session["Student_Caste"] = stud_caste;
                Session["Student_Mobno"] = stud_mobno;
                Session["Student_Email"] = stud_email;

            }
        }
    }
    protected void drp_dwn_sem_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataSet ds1 = new DataSet();
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("Sr_No");
        dt1.Columns.Add("Subject_name");
        ds = new DataSet();
        string stud_id = Session["UserName"].ToString();
        string crs_name = Session["course_name"].ToString().Replace("COMPUTER SCIENCE & ENGINEERING\r\n(ARTIFICIAL INTELLIGENCE AND MACHINE LEARNING)", "CSE (AI & ML)");

        Session["Semester"] = drp_dwn_sem.SelectedValue.ToString();

        string course_qry = "select course_id from m_crs_course_tbl where course_name = '" + crs_name + "'";
        ds = cls.fill_dataset_engg(course_qry);
        string qry_subj = "select s.* from dbo.cre_subject s, cre_credit_tbl c where s.branch_id='" + ds.Tables[0].Rows[0]["course_id"].ToString() + "' and s.sem_id='" + drp_dwn_sem.SelectedValue.ToString() + "' and s.Pattern='" + drp_dwn_pattern.SelectedValue.ToString() + "' and s.del_flag=0 and s.del_flag=c.del_flag and c.subject_id=s.subject_id and c.ayid in (select max(ayid) from m_academic) and c.h1_type = 'ESE '";
        ds1 = cls.fill_dataset_engg(qry_subj);

        if (ds1.Tables[0].Rows.Count > 0)
        {
            print_btn.Enabled = true;
            for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                dr["Sr_No"] = (i + 1).ToString();
                dr["Subject_name"] = ds1.Tables[0].Rows[i]["subject_name"].ToString();
                dt1.Rows.Add(dr);
            }
            Session["MyTable_reg_exm"] = dt1;
            GridView1.DataSource = dt1;
            GridView1.DataBind();
        }
        else
        {
            print_btn.Enabled = false;
        }
    }

    protected void drp_dwn_pattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        //pattern_exm = "";
        if (drp_dwn_pattern.SelectedIndex > 0)
        {
            drp_dwn_sem.Enabled = true;
            drp_dwn_sem.SelectedIndex = 0;
            Session["Pattern_reg_exm"] = drp_dwn_pattern.SelectedValue.ToString();
        }
        else
        {
            drp_dwn_sem.Enabled = false;
            drp_dwn_sem.SelectedIndex = 0;
            Session["Pattern_reg_exm"] = "";
        }
        
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void print_btn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Reg_exm_form.aspx");
    }
    protected void drp_dwn_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Exam_form_branch"] = "";
        if (drp_dwn_branch.SelectedIndex > 0)
        {
            Session["Exam_form_branch"] = drp_dwn_branch.SelectedItem.Text;
        }
    }

}