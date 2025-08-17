using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
public partial class log_out : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (Session["UserName"] != null)
            {
                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect1"].ConnectionString);
                //SqlCommand com = new SqlCommand("UPDATE USERLOG SET LOGOUT = GETDATE() WHERE stud_id = @stud_id", con);
                SqlCommand com = new SqlCommand("INSERT INTO USERLOG (stud_id,STATUS,DATE) VALUES (@stud_id,'Logout',GETDATE())", con);
                con.Open();
                com.CommandType = System.Data.CommandType.Text;
                com.Parameters.AddWithValue("@stud_id", Session["UserName"].ToString());
                com.Parameters.AddWithValue("@logout", DateTime.Now);
                com.ExecuteNonQuery();
                con.Close();

                //Session["UserName"] = "";
                //Session["to_year"] = string.Empty;
                //Session["Email_ID"] = string.Empty;
                //Session["DOB"] = string.Empty;
                //Session["Datebirth"] = string.Empty;
                //Session["FileUpload1"] = string.Empty;
                //Session["Image_flag"] = string.Empty;
                //Session["disp"] = string.Empty;
                Session.Clear();
                Session.Abandon();
                Response.Redirect("login.aspx");
            }
            else
            {
                Response.Redirect("login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("login.aspx");
        }

    }
}