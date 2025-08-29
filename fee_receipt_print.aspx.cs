using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fee_receipt_print : System.Web.UI.Page
{
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["UserName"].ToString() != "")
        {
            if (!IsPostBack)
            {
                fillyear(Session["UserName"].ToString());
            }
        }
    }

    public void fillyear(string stud_id)
    {
        string qry = "select m.ayid,(substring(Duration, 9, 4) + '-' + right(Duration, 4)) as Durations,b.Group_id[Group Id],Group_title,(substring(Duration, 9, 4) + '-' + right(Duration, 4)) + ' (' + Group_title + ')' as Duration from m_academic m ,m_std_studentacademic_tbl st, m_crs_subjectgroup_tbl b where m.ayid = st.ayid and st.stud_id = '" + Session["UserName"].ToString() + "' and b.Group_id = st.group_id";

        DataSet dss = cls.fill_dataset(qry);
        //group_id.Value = dss.Tables[1].Rows[0].ToString();
        group_id.Value = dss.Tables[0].Rows[0]["Group id"].ToString();
        ddlayid.DataSource = dss.Tables[0];
        ddlayid.DataTextField = "Duration";
        ddlayid.DataValueField = "AYID";
        ddlayid.DataBind();
        ddlayid.Items.Insert(0, new ListItem("-- Select --", "0"));
    }

    protected void ddlayid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlayid.SelectedIndex > 0)
            {
                string qry = "select distinct stud_Category,UPPER(stud_Gender) [stud_Gender] from m_std_personaldetails_tbl where stud_id='" + Session["UserName"].ToString() + "' and del_flag=0";
                DataTable dt = cls.fildatatable(qry);
                if (dt.Rows.Count > 0)
                {
                    string category = dt.Rows[0]["stud_Category"].ToString().Trim().ToUpper();
                    string gender = dt.Rows[0]["stud_Gender"].ToString().Trim().ToUpper();
                    gender = gender == "0" ? "FEMALE" : (gender == "1" ? "MALE" : gender);
                    if (dt.Rows[0]["stud_Category"].ToString() == "OPEN")
                    {
                        Session["feemaster"] = "m_FeeMaster";
                        Session["gender"] = "NON";
                    }
                    else
                    {
                        Session["feemaster"] = "m_FeeMaster_category";
                        Session["gender"] = gender;
                    }
                    load_grd();
                }
            }
            else
            {
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + ex.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
        }
    }

    public void load_grd()
    {
        try
        {
            DataTable dt = cls.fillDataTable("select Receipt_no,Chq_status,Type,SUM(CAST(Amount as int)) [Amount],Recpt_mode,Convert(varchar, Pay_date,103) [Pay_date],Install_id from m_FeeEntry where Stud_id='" + Session["UserName"].ToString() + "' and Ayid='" + ddlayid.SelectedValue.Trim() + "' and del_flag=0 and fine_flag=0 and Chq_status = 'Clear' group by Receipt_no,Chq_status,Type,Recpt_mode,Pay_date,Install_id ;");
            if (dt.Rows.Count > 0)
            {
                grdedit.DataSource = dt;
                grdedit.DataBind();
            }
            else
            {
                grdedit.DataSource = null;
                grdedit.DataBind();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void btnprint_Click(object sender, EventArgs e)
    {
        GridViewRow gvrow = (GridViewRow)(sender as Control).Parent.Parent;
        string Chq_status = ((Label)gvrow.FindControl("Chq_status")).Text.Trim();
        string Receipt_no = ((Label)gvrow.FindControl("Receipt_no")).Text.Trim();
        string Type = ((Label)gvrow.FindControl("Type")).Text.Trim();
        if (Chq_status != "Clear")
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Cheque/NEFT Status :" + Chq_status + "!!', { color: '#a94442', background: '#f2dede', blur: 0.2, delay: 0 });", true);
        }
        else
        {
            Session["stud_id"] = Session["UserName"].ToString();
            Session["ayid"] = ddlayid.SelectedValue.Trim();
            Session["Type"] = Type;
            Session["Receipt_no"] = Receipt_no;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "redirect('FeeReceiptMergeFees.aspx');", true);
        }
    }
}