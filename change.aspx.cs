using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class change : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        message.Visible = false;
    }

    Class1 c1 = new Class1();
    protected void btnChange_Click(object sender, EventArgs e)
    {
        if (txtoldPass.Value.Trim().Equals("") || txtNewPass.Value.Trim().Equals("") || txtConfirm.Value.Trim().Equals(""))
        {
            message.InnerText = "All fields are compulsory";
            message.Visible = true;
        }
        else
        {
            if (!txtNewPass.Value.Trim().Equals(txtConfirm.Value.Trim()))
            {
                message.InnerText = "Password Does Not Match";
                message.Visible = true;
            }
            else
            {
                DataSet dsOld = c1.fill_dataset("select password from www_login where stud_id = '"+Session["UserName"]+"'");
                if (dsOld.Tables[0].Rows.Count > 0)
                {
                    if (txtoldPass.Value.Trim().Equals(dsOld.Tables[0].Rows[0]["password"].ToString()))
                    {
                        if (c1.update_data("update www_login set password = '" + txtNewPass.Value.Trim() + "', mod_dt = getdate() where stud_id = '" + Session["UserName"] + "'"))
                        {
                            message.Attributes.Add("class", "alert alert-success");
                            message.InnerText = "Password Changed Successfully";
                            message.Visible = true;
                        }
                    }
                    else
                    {
                        message.Attributes.Add("class", "alert alert-danger");
                        message.InnerText = "Incorrect Old Password";
                        message.Visible = true;
                    }
                }
                else
                {
                    message.Attributes.Add("class", "alert alert-danger");
                    message.InnerText = "No Data Found";
                    message.Visible = true;
                }
            }
        }
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        txtoldPass.Value = "";
        txtNewPass.Value = "";
        txtConfirm.Value = "";
        message.Visible = false;
    }
}