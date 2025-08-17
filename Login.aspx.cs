using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;

public partial class Login : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    DataSet ds;
    Class1 c1 = new Class1();

    protected void Page_Load(object sender, EventArgs e)
    {
        string url = Convert.ToString(HttpContext.Current.Request.Url);
        string[] ss1 = url.Split('/');
        if (ss1[2].ToString().StartsWith("103"))
        {
            Response.Redirect("https://vit.vivacollege.in/student/");
        }
        div_valid.Visible = false;
    }

    public void fun_login()
    {
        string str = "INSERT INTO USERLOG (stud_id,STATUS,DATE) VALUES ('" + txtUserName.Text + "','Login',GETDATE())";
        cmd.CommandText = str;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = c1.con;

        c1.Conn();

        cmd.ExecuteNonQuery();
        c1.con_close();
    }

    public void Clear()
    {
        txtUserName.Text = string.Empty;
        txtPassword.Text = string.Empty;
    }

    public bool check_admission()
    {
        try
        {
            DataSet ds = c1.fill_dataset("select flag from dbo.www_m_std_personaldetails_tbl where stud_id='" + txtUserName.Text + "' and field_type='Group_id'");
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["flag"].ToString()) == false)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        catch (Exception ex)
        {
            return false;
        }

    }

    public void chk_eligibility()
    {
        DataSet ds_eli = c1.fill_dataset("select * from www_Eligibility where stud_id = '" + Session["UserName"].ToString() + "' and is_eligible = 1 and to_year = (select MAX(ayid) from dbo.m_academic) and del_flag=0");
        if (ds_eli.Tables[0].Rows.Count > 0)
        {
            //li_admission.Visible = true;
            Session["admission"] = true;
        }
        else
        {
            Session["admission"] = false;
        }
    }

    protected void btnLogin_Click1(object sender, EventArgs e)
    {
        try
        {
            fun_login();
            if (txtUserName.Text.Trim().ToString() == string.Empty && txtPassword.Text.ToString() == string.Empty)
            {
                div_valid.InnerText = "Enter Student ID and Password";
                div_valid.Visible = true;
                txtUserName.BorderColor = Color.Red;
                txtPassword.BorderColor = Color.Red;
                txtUserName.Focus();

            }
            else if (txtUserName.Text.Trim().ToString() == string.Empty || txtPassword.Text.ToString() == string.Empty)
            {
                if (txtUserName.Text.Trim().ToString() == string.Empty)
                {
                    div_valid.InnerText = "Enter Student ID";
                    div_valid.Visible = true;
                    txtUserName.BorderColor = Color.Red;
                    txtUserName.Focus();
                }
                if (txtPassword.Text.Trim().ToString() == string.Empty)
                {

                    div_valid.InnerText = "Enter Password";
                    div_valid.Visible = true;
                    txtPassword.BorderColor = Color.Red;
                    txtPassword.Focus();
                }
            }
            else
            {
                Session["UserName"] = "";
                Session["to_year"] = string.Empty;
                Session["Email_ID"] = string.Empty;
                Session["DOB"] = string.Empty;
                Session["Datebirth"] = string.Empty;
                Session["FileUpload1"] = string.Empty;
                Session["Image_flag"] = string.Empty;
                Session["disp"] = string.Empty;
                Session["new_flag"] = string.Empty;
                string str = "select stud_id,password,stud_DOB,mothers_name,email_id,new_flag from www_Login where stud_id='" + txtUserName.Text + "' and password='" + txtPassword.Text + "' and del_flag=0";
                cmd.CommandText = str;
                cmd.CommandType = CommandType.Text;
                cmd.Connection = c1.con;
                c1.Conn();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                ds = new DataSet();
                sda.Fill(ds);
                c1.con_close();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    string uname = c1.checkNull("stud_id", ds);
                    string passwd = c1.checkNull("password", ds);
                    string email_id = c1.checkNull("email_id", ds);
                    if (uname == "" && passwd == "")
                    {
                        Clear();
                        div_valid.InnerText = "Incorrect Student ID and Password";
                        div_valid.Visible = true;
                        txtUserName.BorderColor = Color.Red;
                        txtPassword.BorderColor = Color.Red;
                        txtUserName.Focus();
                    }
                    else
                    {
                        if (txtPassword.Text.Trim() == passwd)
                        {
                            Session["UserName"] = uname.ToString();
                            Session["RequeryProcess"] = false;//chinmay 09/07/2022 --added for ATOM requery process
                            chk_eligibility();
                            Response.Redirect("home.aspx", false);
                        }
                        else
                        {
                            div_valid.InnerText = "Incorrect Password";
                            div_valid.Visible = true;
                            txtPassword.BorderColor = Color.Red;
                            txtPassword.ForeColor = Color.Gray;
                        }

                    }
                }
                else
                {
                    div_valid.InnerText = "Incorrect Student ID and Password";
                    div_valid.Visible = true;
                    txtUserName.BorderColor = Color.Red;
                    txtUserName.ForeColor = Color.Gray;
                    txtPassword.BorderColor = Color.Red;
                    txtPassword.ForeColor = Color.Gray;
                    txtUserName.Focus();
                    Session["UserName"] = "";
                }

            }
        }
        catch (Exception ex)
        {
            string msg = "Error Code: 100 Login";
            Response.Redirect("Login.aspx?x=" + msg + "", true);
        }
    }
}