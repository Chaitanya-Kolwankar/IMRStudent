using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class NCRENB_FF_P : System.Web.UI.Page
{
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Convert.ToString(Session["username"]) == "")
                {
                    Response.Redirect("Login.aspx");
                }

            }
            catch (Exception ex)
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
    protected void btn_pay_Click(object sender, EventArgs e)
    {
        try
        {
            if (txt_papid.Text != "" && txt_papname.Text != "" && txt_amt.Text != "")
            {
                string t_id = "";

                string str11212 = "select * from NCRENB";
                DataSet ds11212 = cls.fill_dataset(str11212);
                if (ds11212.Tables[0].Rows.Count == 0)
                {
                    t_id = Session["username"] + txt_amt.Text + Session["username"] + "1";
                    // ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#fyModal').modal('show');</script>", false);
                }
                else
                {
                    t_id = Session["username"] + txt_amt.Text + Session["username"] + Convert.ToString((Convert.ToInt32(ds11212.Tables[0].Rows.Count) + 1));
                }
                string str = "insert into NCRENB values('" + txt_papid.Text + "','" + txt_papname.Text + "','" + txt_amt.Text + "','S',(select max(ayid) from m_academic where iscurrent=1),'" + t_id + "','" + Session["username"] + "',getdate(),null,'0')";
                if (cls.DMLqueries(str) == true)
                {
                    string name, group, category, year, group_id, ayid;
                    DataSet payParameters = cls.fill_dataset("select group_title,Group_id from dbo.m_crs_subjectgroup_tbl where group_id in (select group_id from m_std_studentacademic_tbl where stud_id='" + Session["Username"] + "' and del_flag=0); select upper(isnull(stud_L_Name,'')+' '+isnull(stud_F_Name,'')+' '+isnull(stud_M_Name,'')) as Name,stud_Category from m_std_personaldetails_tbl where stud_id='" + Session["Username"] + "' and del_flag=0; select (substring(Duration,9,4)+'-'+substring(Duration,21,4)) as Duration,AYID from m_academic where IsCurrent=1;");
                    group = payParameters.Tables[0].Rows[0]["group_title"].ToString().Replace("COMPUTER SCIENCE & ENGINEERING (AI&ML)", "CSE (AI and ML)");
                    group_id = payParameters.Tables[0].Rows[0]["Group_id"].ToString();
                    name = payParameters.Tables[1].Rows[0]["Name"].ToString();
                    category = payParameters.Tables[1].Rows[0]["stud_Category"].ToString();
                    year = payParameters.Tables[2].Rows[0]["Duration"].ToString();
                    ayid = payParameters.Tables[2].Rows[0]["AYID"].ToString();

                    string str12 = "insert into processing_fees values('" + Session["username"] + "','','','','" + txt_amt.Text + "','','" + t_id + "','','','','','','','','" + Session["username"] + "',(select max(ayid) from m_academic where Iscurrent=1),getdate())";
                    cls.update_data(str12);
                    Response.Redirect("payment.aspx/" + txt_amt.Text + "/" + t_id + "/123/SolarPowerWorkshop_EE/" + name + "/" + group + "/" + category + "/" + year + "/" + group_id + "/" + ayid, false);
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "Fill All Details", true);

            }
        }
        catch (Exception ex)
        {
        }
    }
}