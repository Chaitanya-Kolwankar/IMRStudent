using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class resetPAss : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect1"].ConnectionString);

  //  string connect = "Data Source=172.16.10.42;Initial Catalog=LMS_ERP_DJunior;User ID=sa;Password=passwd@12";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            pop_ddl_years();
            alert_apply.Visible = false;
            alert_danger.Visible = false;
            alert_pswd.Visible = false;
            alert_incorrect.Visible = false;
        }
        

    }

    public void pop_ddl_years()
    {
        int _currentyear;
        const int _year = 1970;
        _currentyear = DateTime.Now.Year;
        for (int s = _year; s <= _currentyear; s++)
        {
            DropDownList3.Items.Add(new ListItem((s).ToString(), (s).ToString()));
        }
        DropDownList3.DataBind();


        //for (int i = 1; i <= 31; i++)
        //{
        //    DropDownList2.Items.Add(new ListItem((i).ToString(), (i).ToString()));
        //}
        //DropDownList2.DataBind();



     
    }

    public bool sendmessage()
    {
        try
        {
            
            string uid = "vivavit347422";
            string pwd = "24034";
            string gsmsenderid = "VIVACL";

            string mob = txtMobNo.Value.Trim();
            string msg = "Dear Student Your User ID is " + Session["Formno"].ToString() + " and Password is " + Session["passwd"].ToString() + " for Online Admission from VIVA College";
            string strRequest = "username=" + uid + "&password=" + pwd + "&sender=" + gsmsenderid + "&to=" + mob + "&message=" + msg + "&priority=0&dnd=1&unicode=0";
            string url = "http://www.kit19.com/ComposeSMS.aspx?";
            string Result_FromSMS = "";
            StreamWriter myWriter = null;
            HttpWebRequest objRequest = (HttpWebRequest)WebRequest.Create(url + strRequest);
            objRequest.Method = "POST";
            objRequest.ContentLength = strRequest.Length;
            objRequest.ContentType = "application/x-www-form-urlencoded";
            myWriter = new StreamWriter(objRequest.GetRequestStream());
            myWriter.Write(strRequest);
            myWriter.Close();
            HttpWebResponse objResponse = (HttpWebResponse)objRequest.GetResponse();
            using (StreamReader sr = new StreamReader(objResponse.GetResponseStream()))
            {
                Result_FromSMS = sr.ReadToEnd();
                sr.Close();
            }
            return true;
        }
        catch (Exception ex)
        {

            return false;
        }

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            string dob = DropDownList3.SelectedValue + "-" + DropDownList1.SelectedValue + "-" + DropDownList2.SelectedValue;
            //    string clientMachineName;
            //   clientMachineName = (Dns.GetHostEntry(Request.ServerVariables["remote_addr"]).HostName);

            string machine_name = "";


            if (DropDownList1.SelectedValue != "Month" && DropDownList2.SelectedValue != "Day" && DropDownList3.SelectedValue != "Year")
            {

                    try
                    {

                        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect1"].ConnectionString);
                        SqlCommand cmd;
                        SqlDataAdapter da;
                        DataSet ds;
                        Class1 c1 = new Class1();
                        cmd = new SqlCommand("select mothers_name , stud_DOB from www_login where stud_id = '" + inp_mothers_name.Value + "'", con);
                        c1.Conn();

                        da = new SqlDataAdapter(cmd);
                        ds = new DataSet();
                        da.Fill(ds);




                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            if (inp_mothers_name.Value.Trim().ToUpper() == ds.Tables[0].Rows[0]["mothers_name"].ToString().ToUpper().Trim() && dob == Convert.ToDateTime(ds.Tables[0].Rows[0]["stud_DOB"]).ToString("yyyy-MM-dd"))
                            {

                                string qryadd = "insert into www_pswd_reset_tbl values('" + inp_mothers_name.Value + "',null,'','" + machine_name + "','',0)";
                                con.Open();
                                cmd.CommandText = qryadd;
                                cmd.Connection = con;
                                cmd.ExecuteNonQuery();
                                con.Close();

                                string qryadd2 = "update www_login set password='null' where mothers_name='" + inp_mothers_name.Value + "'";
                                con.Open();
                                cmd.CommandText = qryadd2;
                                cmd.Connection = con;
                                cmd.ExecuteNonQuery();
                                con.Close();


                                sendmessage();
                                alert_apply.Visible = true;
                                alert_danger.Visible = false;
                                alert_pswd.Visible = false;
                                alert_incorrect.Visible = false;



                            }
                            else
                            {
                                alert_apply.Visible = false;
                                alert_danger.Visible = false;
                                alert_pswd.Visible = false;
                                alert_incorrect.Visible = true;
                            }




                        }
                        else
                        {
                            alert_apply.Visible = false;
                            alert_danger.Visible = false;
                            alert_pswd.Visible = false;
                            alert_incorrect.Visible = true;
                        }

                    }


                    catch (Exception exc)
                    {
                        alert_apply.Visible = false;
                        alert_danger.Visible = false;
                        alert_pswd.Visible = false;
                        alert_incorrect.Visible = true;

                    }


               
                {
                    alert_apply.Visible = false;
                    alert_danger.Visible = false;
                    alert_pswd.Visible = true;
                    alert_incorrect.Visible = false;
                }

            }
            else
            {
                alert_apply.Visible = false;
                alert_danger.Visible = true;
                alert_pswd.Visible = false;
                alert_incorrect.Visible = false;
            }
        }
        catch
        {
            Response.Redirect("log_out.aspx", true);
        }
    }
    
}