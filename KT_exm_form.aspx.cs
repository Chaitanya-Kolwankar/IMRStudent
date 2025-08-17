using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KT_exm_form : System.Web.UI.Page
{
    DataSet ds;
    DataTable dt;
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
     
        string picture = Session["image"].ToString();
        std_img.ImageUrl = "data:image/png;base64," + picture;

        TextBox5.Text = Session["Exam_form_branch"].ToString();
        TextBox9.Text = Session["UserName"].ToString();
        TextBox6.Text = Session["Semester"].ToString();
        label1.Text = Session["Student_Name"].ToString();
        label2.Text = Session["Student_Address"].ToString();
        label3.Text = Session["Student_Caste"].ToString();
        label4.Text = Session["Student_Mobno"].ToString();
        label5.Text = Session["Student_Email"].ToString();
        pattern_txt.Text = Session["Pattern_kt_exm"].ToString();
        DateTime dateTime = DateTime.UtcNow.Date;
        date_lbl.Text = dateTime.ToString("dd/MM/yyyy");
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