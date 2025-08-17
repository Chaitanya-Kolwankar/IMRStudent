using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admission_error : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"].ToString() != null)
            {
                if (Session["admission_msg"].ToString() != null)
                {
                    msg.InnerHtml = Session["admission_msg"].ToString();
                }
                else
                {
                    Response.Redirect("log_out.aspx",false);

                }

            }
            else
            {
                Response.Redirect("log_out.aspx",false);

            }
        }
        catch(Exception ae)
        {
            Response.Redirect("log_out.aspx",false);

        }
    }
}