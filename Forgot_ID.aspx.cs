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

public partial class Forgot_ID : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect1"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            pop_ddl_years();
        }
    }
    string userid = "";
    string passwd = "";

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
            string msg = "Dear Student Your User ID is " + userid + " and Password is " + userid+ " for Online Admission from VIVA INSTITUTE OF TECHNOLOGY";
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



    public void clear()
    {
        DropDownList1.SelectedIndex = 0;
        DropDownList2.SelectedIndex = 0;
        DropDownList3.SelectedIndex = 0;
        inp_mothers_name.Value = "";
        txtMobNo.Value = "";
        
        
        
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            //string dob=DropDownList3.SelectedValue +"-"+ DropDownList2.SelectedValue +"-"+ DropDownList1.SelectedValue;
            string dob = DropDownList3.SelectedValue + "-" + DropDownList1.SelectedValue + "-" + DropDownList2.SelectedValue;
             //string machine_name = "";

            if(DropDownList1.SelectedValue!="Month" && DropDownList2.SelectedValue !="Day" && DropDownList3.SelectedValue !="Year")
            {
                try
                {
                    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect1"].ConnectionString);
                    SqlCommand cmd;
                    SqlDataAdapter da;
                    DataSet ds;
                    Class1 c1=new Class1();
                    cmd = new SqlCommand("select stud_id,mothers_name, stud_DOB,password from www_login where mob_no = '" + txtMobNo.Value + "'", con);
                    c1.Conn();
                    da = new SqlDataAdapter(cmd);
                    ds = new DataSet();
                    da.Fill(ds);
                   // DateTime dt=(Convert.ToDateTime(ds.Tables[0].Rows[0]["stud_DOB"].ToString())).Date;

                    if(ds.Tables[0].Rows.Count>0)
                    {
                        if ((inp_mothers_name.Value.ToUpper() == ds.Tables[0].Rows[0]["mothers_name"].ToString().ToUpper()) && (dob == Convert.ToDateTime(ds.Tables[0].Rows[0]["stud_DOB"]).ToString("yyyy-MM-dd")))
                        {
                            userid = ds.Tables[0].Rows[0]["stud_id"].ToString();
                            passwd = ds.Tables[0].Rows[0]["passwordl"].ToString();
                            sendmessage();
                            lbl_id.Visible = true;
                            //lbl_passwd.Visible = true;
                            lbl_message.Visible = true;
                            lbl_id.InnerHtml = "Your user ID is: <strong>" + userid + "</strong> and password is: <strong>" + passwd + "</strong>";
                            //lbl_passwd.Text = passwd;

                            lbl_message.Visible = true;
                            lbl_message.ForeColor = System.Drawing.Color.Green;
                            lbl_message.Font.Bold = true;
                            lbl_message.Text = "Message Send Successfully".ToUpper();
                            clear();
                           
                        }

                       
                    }
                  
                }
                catch
                {
                }
            }
        }
        catch
        { 
        }
    }
}