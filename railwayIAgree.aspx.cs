using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class railwayIAgree : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                errorMsg.Visible = false;
                chkIAgree.Checked = false;
                if (Session["UserName"].ToString() == string.Empty)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("log_out.aspx");
            }
        }
    }
    protected void btnIAgree_Click(object sender, EventArgs e)
    {
        if(chkIAgree.Checked==false)
        {
            errorMsg.Visible = true;
            errorMsg.InnerHtml = "Please select I Agree and then continue";
        }
        else
        {
            errorMsg.Visible = false;
            Response.Redirect("railway.aspx");
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        try
        {
            
            if (Session["UserName"].ToString() == string.Empty)
            {
                Response.Redirect("login.aspx");
            }
            else
            {
                Response.Redirect("home.aspx",false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("log_out.aspx");
        }
    }
}