using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Configuration;
using System.Text.RegularExpressions;


public partial class railway : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect2"].ConnectionString);

    Class1 c1 = new Class1();
    SqlCommand cmd = new SqlCommand();
    DataSet ds, ds_year, grd_ds, ds_gen;
    private SqlConnection con;
    string stud_id_new = "", req_no = "", qry = "",phn_no="",email="";
    int flag_phn = 0,flag_email=0;
    
    public void data()
    {
        //con.Open();
        string id = Session["UserName"].ToString();
        if (Session["UserName"].ToString() != string.Empty || Session["UserName"].ToString() != "")
        {
            ds_year = new DataSet();
            ds_year = c1.fill_dataset("select ayid from dbo.m_academic where iscurrent=1");
            string qry = "";
            qry = "select stud_id,to_stn,case period when 1 then 'Monthly' when 3 then 'Quarterly' end as [period],case class when 1 then 'First Class' when 2 then 'Second Class' end as [class],convert(varchar,curr_dt,110) as [curr_dt],receipt_no,convert(varchar,req_dt,110) as req_dt,pass_no from dbo.requition_tbl where ayid='" + ds_year.Tables[0].Rows[0][0].ToString() + "' and stud_id='" + id + "' and del_flag=0";
            grd_ds = new DataSet();
            grd_ds = c1.fill_dataset(qry);
            grddata.DataSource = grd_ds.Tables[0];
            grddata.DataBind();
            //con.Close();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            try
            {
                if (Session["UserName"].ToString() != string.Empty || Session["UserName"].ToString() != "")
                {
                    if (Convert.ToBoolean(Session["li_railway"]) == false)
                    {
                        Response.Redirect("profile.aspx");
                    }
                    else
                    {
                        grddata.DataSource = null;
                        grddata.DataBind();
                        chkStudConcession();
                        fillStation();
                    }
                }
                else
                {
                    Response.Redirect("log_out.aspx");
                }
            }
            catch(Exception ex)
            {
                Response.Redirect("log_out.aspx");
            }
        }
    }

    void fillStation()
    {
        string qry1 = "select station from dbo.web_tp_station order by station";
        DataSet dsStn = new DataSet();
        dsStn = c1.fill_dataset(qry1);
        ddto_stn.Items.Clear();
        ddto_stn.Items.Add(new ListItem("--SELECT--", ""));
        for (int i = 0; i < dsStn.Tables[0].Rows.Count; i++)
        {
            ddto_stn.Items.Add(new ListItem(dsStn.Tables[0].Rows[i][0].ToString()));

        }
        ddto_stn.SelectedIndex = 0;
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        ddperoid.SelectedIndex = 0;
        ddperoid.Enabled = false;
        ddto_stn.SelectedIndex = 0;
        ddto_stn.Enabled = false;
        ddl_class.SelectedIndex = 0;
        ddl_class.Enabled = false;
        txt_phno.Text = string.Empty;
        txt_phno.Enabled = false;
        //btn_new.Enabled = true;
        txt_mail.Text = string.Empty;
        txt_mail.Enabled = false;
    }

    protected void btnapply_Click(object sender, EventArgs e)
    {
        if (ddto_stn.SelectedIndex == 0 || ddperoid.SelectedIndex == 0 || ddl_class.SelectedIndex == 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please select all Mandatory fields')", true);
        }
        else
        {
            string c = "";
            if (ddl_class.SelectedIndex == 1)
            {
                c = "1";
            }
            else if (ddl_class.SelectedIndex == 2)
            {
                c = "2";
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please select Class')", true);
                return;
            }
            string period = "";
            if (ddperoid.SelectedValue.ToString() == "Monthly")
            {
                period = "1";
            }
            else if (ddperoid.SelectedValue.ToString() == "Quarterly")
            {
                period = "3";
            }
            else if (ddperoid.SelectedValue.ToString() == "--SELECT--")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please Select Period For Pass')", true);
                return;
            }

            if (txt_phno.Text != "")
            {

                try
                {
                    long phn_no = Convert.ToInt64(txt_phno.Text);
                }
                catch (Exception ex)
                {
                    flag_phn = 1;
                }
           
            }

            if (txt_mail.Text != "")
            {
                string email = txt_mail.Text;
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(email);
                if (match.Success)
                {
                    flag_email = 0;
                    email = txt_mail.Text;
                }
                else
                {
                    flag_email = 1;
                }
            }


            if (flag_email == 0 && flag_phn == 0)
            {
                ds = new DataSet();
                ds = c1.fill_dataset("select ayid from dbo.m_academic where iscurrent=1;");
                cmd = new SqlCommand();
                c1.Conn();
                cmd.Connection = c1.con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insert_req";
                cmd.Parameters.AddWithValue("@STUD_ID", Session["UserName"].ToString());
                cmd.Parameters.AddWithValue("@TO_STN", ddto_stn.SelectedValue.ToString());
                cmd.Parameters.AddWithValue("@CLASS", c);
                cmd.Parameters.AddWithValue("@PERIOD", period);
                cmd.Parameters.AddWithValue("@PH_NO", txt_phno.Text);
                cmd.Parameters.AddWithValue("@EMAIL", txt_mail.Text);
                cmd.Parameters.AddWithValue("@AYID", ds.Tables[0].Rows[0][0].ToString());
                cmd.Parameters.AddWithValue("@REQ_ID", "");
                cmd.Parameters.AddWithValue("@ACTION", "insert");
                string message = cmd.ExecuteScalar().ToString();
                if (message == "TRANSACTION SUCCESSFUL")
                {
                    //String str = string.Empty;
                    //str = "Data Saved Successfully.";
                    //lblmainerror.Visible = true;
                    //lblmainerror.Text = str;
                    lblmainerror.Attributes.Add("class", "alert alert-success");
                    lblmainerror.InnerHtml = "<strong>Success:</strong> Data Saved Successfully.";
                    ddperoid.SelectedIndex = 0;
                    ddto_stn.SelectedIndex = 0;
                    ddl_class.SelectedIndex = 0;
                    btnapply.Enabled = false;
                    btn_update.Enabled = false;
                    //btn_new.Enabled = true;
                    txt_mail.Text = string.Empty;
                    txt_mail.Enabled = false;
                    txt_phno.Text = string.Empty;
                    chkStudConcession();

                }
            }
            else if (flag_phn == 1)
            {
                railAlert.InnerHtml = "Please enter numeric value";
            }
            else if (flag_email == 1)
            {
                railAlert.InnerHtml = "Please enter correct email id";
            }
        }
    }

    protected void btn_update_Click(object sender, EventArgs e)
    {
        txt_mail.Enabled = true;
        if ((ddto_stn.SelectedIndex == 0 || ddperoid.SelectedIndex == 0 || ddl_class.SelectedIndex == 0))
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please select all Mandatory fields')", true);
        }
        else
        {
            ds = new DataSet();
            ds = c1.fill_dataset("select * from requition_tbl where stud_id='" + Session["UserName"] + "' and req_flag=1");
            if (ds.Tables[0].Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('You can't update Concession application.')", true);
            }
            else
            {
                string c = "";
                if (ddl_class.SelectedIndex == 1)
                {
                    c = "1";
                }
                else if (ddl_class.SelectedIndex == 2)
                {
                    c = "2";
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please select Class')", true);
                    return;
                }


                string period = "";


                if (ddperoid.SelectedValue.ToString() == "Monthly")
                {
                    period = "1";
                }
                else if (ddperoid.SelectedValue.ToString() == "Quarterly")
                {
                    period = "3";
                }
                else if (ddperoid.SelectedValue.ToString() == "--SELECT--")
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please Select Period For Pass')", true);
                    return;
                }

                if (txt_phno.Text != "")
                {
                    try
                    {
                        long phn_no = Convert.ToInt64(txt_phno.Text);
                    }
                    catch (Exception ex)
                    {
                        flag_phn = 1;
                    }

                }

                if (txt_mail.Text != "")
                {
                    string email = txt_mail.Text;
                    Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                    Match match = regex.Match(email);
                    if (match.Success)
                    {
                        flag_email = 0;
                        email = txt_mail.Text;
                    }
                    else
                    {
                        flag_email = 1;
                    }

                    //string pattern = null;
                    //pattern = "^([0-9a-zA-Z]([-\\.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$";

                   
                }

                
        bool val = false;
        string message = "";

        if (flag_phn == 0 && flag_email==0)
        {
            try
            {
                cmd.Connection = c1.con;
                c1.con.Open();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insert_req";
                cmd.Parameters.AddWithValue("@STUD_ID", Session["UserName"].ToString());
                cmd.Parameters.AddWithValue("@TO_STN", ddto_stn.Text.ToString());
                cmd.Parameters.AddWithValue("@PERIOD", period);
                cmd.Parameters.AddWithValue("@CLASS", c);
                cmd.Parameters.AddWithValue("@PH_NO", txt_phno.Text);
                cmd.Parameters.AddWithValue("@EMAIL", txt_mail.Text);
                cmd.Parameters.AddWithValue("@AYID", "");
                cmd.Parameters.AddWithValue("@REQ_ID", Session[req_no].ToString());
                cmd.Parameters.AddWithValue("@ACTION", "Update");
                cmd.ExecuteNonQuery();
                message = cmd.ExecuteScalar().ToString();
                c1.con.Close();
                val = true;
            }
            catch (Exception ex)
            {
                val = false;
            }

            if (val == true)
            {
                if (message == "UPDATED SUCCESSFULLY")
                {
                    grddata.DataSource = null;
                    grddata.DataBind();
                    chkStudConcession();
                }
                else
                {
                    // railAlert.InnerHtml = "You selected address should be same as your ";
                }

            }
            else
            {

            }
        }
        else if (flag_phn == 1)
        {
            railAlert.InnerHtml = "Please enter numeric value";
        }
        else if (flag_email == 1)
        {
            railAlert.InnerHtml = "Please enter correct email id";
        }
                
            }
        }
    }

    protected void btn_new_Click(object sender, EventArgs e)
    {
        
    }

    void chkStudConcession()
    {
        grddata.Visible = true;
        ds = new DataSet();
        labels.Visible = false;
        ds = c1.fill_dataset("select * from dbo.requition_tbl where stud_id='" + Session["UserName"] + "' and req_id = (select MAX(req_id) from dbo.requition_tbl where stud_id='" + Session["UserName"] + "') and del_flag=0");
        ds_gen = c1.fill_dataset("select stud_Gender from m_std_personaldetails_tbl where stud_id='" + Session["UserName"] + "' and del_flag=0");

        if (ds.Tables[0].Rows.Count > 0)
        {
            if (Convert.ToBoolean(ds.Tables[0].Rows[0]["req_flag"]))
            {
                string msg = "";
                msg = chkDate(Convert.ToDateTime(ds.Tables[0].Rows[0]["req_dt"]), Convert.ToInt32(ds.Tables[0].Rows[0]["period"]));
                if (msg == "")
                {
                    //can apply
                    labels.Visible = true;
                }
                else
                {
                    grddata.DataSource = ds.Tables[0];
                    grddata.DataBind();
                    railAlert.InnerHtml = "<strong>Note:</strong> " + msg;
                }
            }
            else
            {
                //can edit

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["receipt_no"].ToString()))
                {
                    string msg = "";
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["req_flag"].ToString()))
                    {
                        if (ds.Tables[0].Rows[0]["req_flag"].ToString() == "False")
                        {
                            labels.Visible = true;
                            railAlert.InnerHtml = "<strong>Note:</strong> Your previous application is disapproved. Please fill proper details and re-submit new application";
                            grddata.Visible = false;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["req_dt"].ToString()))
                        {
                            msg = chkDate(Convert.ToDateTime(ds.Tables[0].Rows[0]["req_dt"]), Convert.ToInt32(ds.Tables[0].Rows[0]["period"]));
                        }
                        railAlert.InnerHtml = msg + " <br/>Your application is processed. You can not edit information.";
                        grddata.DataSource = ds.Tables[0];
                        grddata.DataBind();
                    }
                }
                else
                {
                    railAlert.InnerHtml = "<strong>Note:</strong> You have already applied for concession. Your requisition is pending. You can edit.";
                    grddata.DataSource = ds.Tables[0];
                    grddata.DataBind();
                }
            }
        }
        else
        {
            string str = "select stud_PermanentAdd from m_std_personaldetails_tbl where stud_id='" + Session["UserName"].ToString() + "' and del_flag=0";
            ds = c1.fill_dataset(str);
            if (ds.Tables.Count > 0)
            {
                railAlert.InnerHtml = "Note: Your current address is: " + ds.Tables[0].Rows[0][0].ToString() + ". ";
                railAlert.InnerHtml += "<br/>Your From Station address should be same as your current address otherwise your application might get disapproved.";
                railAlert.InnerHtml += "<br/>You will get message on mobile number and email_id you are entering. So please enter correct details.";
                labels.Visible = true;
            }

        }

        //if (ds_gen.Tables[0].Rows.Count > 0)
        //{
        //    if (ds_gen.Tables[0].Rows[0][0].ToString() == "1")
        //    {
        //        ddperoid.SelectedIndex = 2;
        //        ddperoid.Enabled = false;
        //    }
        //    else
        //    {
        //        ddperoid.Enabled = true;
        //    }
        //}

       // ddperoid.SelectedIndex = 1;
        //ddperoid.Enabled = false;

    }

    void chkRailStatus()
    {
        ds = new DataSet();
        ds = c1.fill_dataset("select * from requition_tbl where stud_id='" + Session["UserName"] + "' and req_flag=0; select * from requition_tbl where stud_id='" + Session["UserName"] + "' and req_flag=1");
        if (ds.Tables[0].Rows.Count > 0)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('You have already apply for Concession. Your Request is Pending. You can update Your Requisition.')", true);
            btn_update.Enabled = true;
            ds_gen = c1.fill_dataset("select stud_Gender from m_std_personaldetails_tbl where stud_id='" + Session["UserName"] + "' and del_flag=0");
            //btn_new.Enabled = false;
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ph_no"].ToString()))
            {
                txt_phno.Text = ds.Tables[0].Rows[0]["ph_no"].ToString();
                txt_phno.Enabled = true;
            }
            if (ds.Tables[0].Rows[0]["class"].ToString() == "1")
            {
                ddl_class.SelectedIndex = 1;
                ddl_class.Enabled = true;
            }
            else if (ds.Tables[0].Rows[0]["class"].ToString() == "2")
            {
                ddl_class.SelectedIndex = 2;
                ddl_class.Enabled = true;
            }

            if (ds_gen.Tables[0].Rows.Count > 0)
            {
                if (ds_gen.Tables[0].Rows[0][0].ToString() == "0")
                {
                    if (ds.Tables[0].Rows[0]["period"].ToString() == "1")
                    {
                        ddperoid.SelectedIndex = 1;
                        ddperoid.Enabled = true;
                    }
                    else if (ds.Tables[0].Rows[0]["period"].ToString() == "3")
                    {
                        ddperoid.SelectedIndex = 2;
                        ddperoid.Enabled = true;
                    }
                }
                else
                {
                    ddperoid.SelectedIndex = 2;
                    ddperoid.Enabled = false;
                }
            }

           
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["to_stn"].ToString()))
            {
                ddto_stn.Text = ds.Tables[0].Rows[0]["to_stn"].ToString();
                ddto_stn.Enabled = true;
            }
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["email_id"].ToString()))
            {
                txt_mail.Text = ds.Tables[0].Rows[0]["email_id"].ToString();
                txt_mail.Enabled = true;
            }
        }
        else if (ds.Tables[1].Rows.Count > 0)
        {
            
        }
        else
        {
            btnapply.Enabled = true;
            ddl_class.Enabled = true;
            if (ds_gen.Tables[0].Rows.Count > 0)
            {
                if (ds_gen.Tables[0].Rows[0][0].ToString() == "0")
                {
                    ddperoid.Enabled = true;
                }
                else
                {
                    ddperoid.Enabled = false;
                    ddperoid.SelectedIndex = 2;
                }
            }
            
            ddto_stn.Enabled = true;
            txt_phno.Enabled = true;
            txt_mail.Enabled = true;
        }
    }

    string chkDate(DateTime req_date, int p)
    {
        string msg = "";
        bool canApply = true;
        DateTime next = DateTime.MinValue;
        req_date.AddDays(-5);
        //req_date.Subtract(TimeSpan.FromDays(3));
        next = req_date.AddMonths(p);
        if (next < DateTime.Now)
        {

        }
        else
        {
            canApply = false;
            msg = "You can apply for Railway Concession after " + next.ToString("dd/MM/yyyy") + ".";
            msg += " Please collect your application within 3 days.";

        }
        return msg;
    }


    protected void grddata_RowEditing(object sender, GridViewEditEventArgs e)
    {
       req_no = "";
       stud_id_new = (grddata.Rows[e.NewEditIndex].FindControl("lblstud_id") as Label).Text;
       Session[req_no] = (grddata.Rows[e.NewEditIndex].FindControl("lblreq_id") as Label).Text;
       lblmainerror.Visible = false;
       DataSet ds_mult; 

       ds = c1.fill_dataset("select * from dbo.requition_tbl where stud_id='" + Session["UserName"] + "' and req_id='" + Session[req_no].ToString() + "' and del_flag=0");
       ds_gen = c1.fill_dataset("select stud_Gender from m_std_personaldetails_tbl where stud_id='" + Session["UserName"] + "' and del_flag=0");


        if (ds.Tables[0].Rows.Count > 0)
        {
            if (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["receipt_no"].ToString()))
            {
                ds_mult = c1.fill_dataset("select to_stn from dbo.requition_tbl where stud_id='" + Session["UserName"] + "' and del_flag=0");
                if (ds_mult.Tables.Count > 0)
                {
                    if (ds_mult.Tables[0].Rows.Count > 1)
                    {
                        //////////////
                        if (!string.IsNullOrEmpty(ds_mult.Tables[0].Rows[0]["to_stn"].ToString()))
                        {
                            for (int i = 0; i < ddto_stn.Items.Count; i++)
                            {
                                if (ds_mult.Tables[0].Rows[0]["to_stn"].ToString() == ddto_stn.Items[i].Text)
                                {
                                    ddto_stn.SelectedIndex = i;
                                    ddto_stn.Enabled = false;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            ddto_stn.SelectedIndex = 0;
                        }
                        //////////////


                    }
                    else
                    {
                        //////////////
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["to_stn"].ToString()))
                        {
                            for (int i = 0; i < ddto_stn.Items.Count; i++)
                            {
                                if (ds.Tables[0].Rows[0]["to_stn"].ToString() == ddto_stn.Items[i].Text)
                                {
                                    ddto_stn.SelectedIndex = i;
                                    ddto_stn.Enabled = true;
                                    break;
                                }
                            }
                        }
                        else
                        {
                            ddto_stn.SelectedIndex = 0;
                        }
                        //////////////
                    }
                }

              

                /////////////////////////////

                if (ds_gen.Tables[0].Rows.Count > 0)
                {
                    if (ds_gen.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["period"].ToString()))
                        {
                            if (ds.Tables[0].Rows[0]["period"].ToString() == "1")
                            {
                                ddperoid.SelectedIndex = 1;
                                ddperoid.Enabled = true;
                            }
                            else if (ds.Tables[0].Rows[0]["period"].ToString() == "3")
                            {
                                ddperoid.SelectedIndex = 2;
                                ddperoid.Enabled = true;
                            }
                        }
                        else
                        {
                            ddperoid.SelectedIndex = 0;
                        }

                    }
                    else
                    {
                        ddperoid.SelectedIndex = 2;
                        ddperoid.Enabled = false;
                    }
                  
                }


                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["class"].ToString()))
                {
                    if (ds.Tables[0].Rows[0]["class"].ToString() == "1")
                    {
                        ddl_class.SelectedIndex = 1;
                        ddl_class.Enabled = true;
                    }
                    else if (ds.Tables[0].Rows[0]["class"].ToString() == "2")
                    {
                        ddl_class.SelectedIndex = 2;
                        ddl_class.Enabled = true;
                    }
                }
                else
                {
                    ddl_class.SelectedIndex = 0;
                }
                ////////////////////////////////////////
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["ph_no"].ToString()))
                {
                    txt_phno.Text = ds.Tables[0].Rows[0]["ph_no"].ToString();
                }
                else
                {
                    txt_phno.Text = "";
                }
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["email_id"].ToString()))
                {
                    txt_mail.Text = ds.Tables[0].Rows[0]["email_id"].ToString();
                }
                else
                {
                    txt_mail.Text = "";
                }

                btn_update.Visible = true;
                btn_update.Enabled = true;
                btncancel.Enabled = true;
                btncancel.Visible = true;
                labels.Visible = true;
                btnapply.Visible = false;
                txt_mail.Enabled = true;
            }
            else
            {
                string msg = "";
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["req_dt"].ToString()))
                {
                    msg = chkDate(Convert.ToDateTime(ds.Tables[0].Rows[0]["req_dt"]), Convert.ToInt32(ds.Tables[0].Rows[0]["period"]));
                }
                railAlert.InnerHtml = msg +"<br/>Your application is in process. You can not edit information.";
            }
        }
    
    }

    protected void grddata_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grddata.EditIndex = -1;
        data();
    }

    protected void grddata_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    
    }
   
    protected void btn_new_Click1(object sender, EventArgs e)
    {
            labels.Visible = true;
            btn_update.Visible = true;
            btnapply.Visible = true;
            btncancel.Visible = true;
    }

    protected void btnapply_Click1(object sender, EventArgs e)
    {

    }

   
    protected void grddata_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
     

      

    }

    public object stud_id { get; set; }

    public object to_stn { get; set; }

    protected void btncancel_Click2(object sender, EventArgs e)
    {
        bool val = false;

        try
        {
            cmd.Connection = c1.con;
            c1.con.Open();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insert_req";
            cmd.Parameters.AddWithValue("@STUD_ID", Session["UserName"].ToString());
            cmd.Parameters.AddWithValue("@TO_STN", "");
            cmd.Parameters.AddWithValue("@PERIOD", "");
            cmd.Parameters.AddWithValue("@CLASS", "");
            cmd.Parameters.AddWithValue("@PH_NO", "");
            cmd.Parameters.AddWithValue("@EMAIL", "");
            cmd.Parameters.AddWithValue("@AYID", "");
            cmd.Parameters.AddWithValue("@REQ_ID", Session[req_no].ToString());
            cmd.Parameters.AddWithValue("@ACTION", "Delete");
            cmd.ExecuteNonQuery();
            c1.con.Close();
            val = true;
        }
        catch (Exception ex)
        {
            val = false;
        }


        if (val == true)
        {

            railAlert.InnerText = "";
            grddata.DataSource = null;
            grddata.DataBind();
            lblmainerror.Visible = true;
            lblmainerror.Attributes.Add("class", "alert alert-success");
            lblmainerror.InnerHtml = "<strong>Success:</strong> Data Deleted.";
            ddperoid.SelectedIndex = 0;
            ddto_stn.SelectedIndex = 0;
            ddl_class.SelectedIndex = 0;
            btnapply.Visible = true;
            btn_update.Visible = false;
            btncancel.Visible = false;
            txt_mail.Text = string.Empty;
            txt_mail.Enabled = true;
            txt_phno.Text = string.Empty;
            btnapply.Enabled = true;
            chkStudConcession();
        }
        else
        {
            lblmainerror.Attributes.Add("class", "alert alert-danger");
            lblmainerror.InnerHtml = "<strong>Success:</strong> Data Not Deleted.";
        }
    }
}