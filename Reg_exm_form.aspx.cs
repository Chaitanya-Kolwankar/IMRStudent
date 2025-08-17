using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reg_exm_form : System.Web.UI.Page
{
    DataSet ds;
    DataTable dt;
    Class1 cls = new Class1();
    
    protected void Page_Load(object sender, EventArgs e)
    {

        string picture = Session["image"].ToString();
        std_img.ImageUrl = "data:image/png;base64," + picture;

        TextBox5.Text = Session["Exam_form_branch"].ToString();
        TextBox9.Text= Session["UserName"].ToString();
        TextBox6.Text = Session["Semester"].ToString();
        label1.Text = Session["Student_Name"].ToString();
        label2.Text = Session["Student_Address"].ToString();
        label3.Text = Session["Student_Caste"].ToString();
        label4.Text = Session["Student_Mobno"].ToString();
        label5.Text = Session["Student_Email"].ToString();
        pattern_txt.Text = Session["Pattern_reg_exm"].ToString();
        DateTime dateTime = DateTime.UtcNow.Date;
        date_lbl.Text = dateTime.ToString("dd/MM/yyyy");
    }

    public string getWhileLoopData()
    {
        string htmlStr = "";
        ds = new DataSet();
        dt = new DataTable();
        string stud_id = Session["UserName"].ToString();

        dt = (DataTable)Session["MyTable_reg_exm"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string sr_no = (i + 1).ToString();
            string Subj_Name = dt.Rows[i]["subject_name"].ToString();
            htmlStr += "<tr style='font-size:12px;'><td>" + sr_no + "</td><td>" + Subj_Name + "</td></tr>";
        }
        return htmlStr;
    }
}