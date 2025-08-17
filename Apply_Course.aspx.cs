using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Runtime.Serialization.Json;
using System.Text;

public partial class Apply_Course : System.Web.UI.Page
{
    Class1 cls1 = new Class1();
    Fees fee = new Fees();
    string str = "", prev_check = "", curr_group = "";
    int amt_val, amt_val1 = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["UserName"].ToString() != null || Session["UserName"].ToString() != "")
                {
                    if (!IsPostBack)
                    {
                        if (Session["stud_Category"].ToString().ToUpper() != "OPEN")
                        {
                            declaration.Visible = true;
                        }
                        fillCourse();
                    }
                }
                else
                {
                    Response.Redirect("Login.aspx", false);
                }
            }
            catch (Exception ex1)
            {
                Response.Redirect("Login.aspx", false);
                cls1.err_cls(ex1.ToString());
            }
        }
    }
    private void fillCourse()
    {
        lblname.InnerText = Convert.ToString(Session["name"]);
        DataSet ds = ((DataSet)Session["stud_data"]);
        curr_grp.Text = ds.Tables[4].Rows[0]["Group_title"].ToString();
        curr_roll.Text = ds.Tables[4].Rows[0]["roll_no"].ToString();
        Session["subcourse"] = ds.Tables[3].Rows[0]["subcourse_name"].ToString();
        ddlNextGrp.Items.Clear();
        ddlNextGrp.Items.Add("--Select--");
        for (int i = 0; i < ds.Tables[5].Rows.Count; i++)
        {
            ddlNextGrp.Items.Add(new ListItem(ds.Tables[5].Rows[i]["Group_title"].ToString(), ds.Tables[5].Rows[i]["Group_id"].ToString(), true));
        }
    }
    protected void ddlNextGrp_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlNextGrp.SelectedIndex > 0)
            {
                DataSet ds = ((DataSet)Session["stud_data"]);
                useless();
                string urlalias = cls1.urls();
                string url = @urlalias + "Fees/";

                fee.type = "select";
                fee.subtype = "Admission";
                fee.stud_id = Session["Username"].ToString();
                fee.ayid = ds.Tables[3].Rows[0]["to_year"].ToString();
                fee.group_id = ddlNextGrp.SelectedValue.ToString();

                string jsonString = JsonHelper.JsonSerializer<Fees>(fee);
                var httprequest = (HttpWebRequest)WebRequest.Create(url);
                httprequest.ContentType = "application/json";
                httprequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httprequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonString);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                var httpresponse = (HttpWebResponse)httprequest.GetResponse();
                using (var streamReader = new StreamReader(httpresponse.GetResponseStream()))
                {
                    string result = streamReader.ReadToEnd();
                    ds = JsonConvert.DeserializeObject<DataSet>(result);
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables.Contains("Error") == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + ds.Tables["Error"].Rows[0][0].ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        }
                        else
                        {
                            lbl_amt_final.Text = ds.Tables["Calculated"].Rows[0]["Total"].ToString();
                            txt_amt_final.Text = ds.Tables["Calculated"].Rows[0]["Total"].ToString();
                            txt_amt_final.Focus();
                        }
                    }
                }
            }
            else
            {
                lbl_amt_final.Text = "";
                txt_amt_final.Text = "";
            }
        }
        catch (Exception ex1)
        {
            cls1.err_cls(ex1.ToString());
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlNextGrp.SelectedIndex == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select Next Year Group')", true);

            }
            else
            {
                DataSet ds = ((DataSet)Session["stud_data"]);
                bool flag = false;
                if (txt_amt_final.Text.Trim() == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Enter Amount ')", true);
                    flag = false;
                    return;
                }
                if (Convert.ToDouble(txt_amt_final.Text) > Convert.ToDouble(lbl_amt_final.Text))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your fees amount is Rs." + lbl_amt_final.Text + "')", true);
                    txt_amt_final.Text = "";
                    flag = false;
                    return;
                }

                //Part Payment allowed amount check
                DataTable allowed = cls1.fildatatable("select * from Part_pay_auth where stud_id='" + Session["Username"].ToString() + "' and ayid='" + ds.Tables[3].Rows[0]["to_year"].ToString() + "' and group_id='" + ddlNextGrp.SelectedValue.ToString() + "' and del_flag=0 and convert(date,GETDATE(),103)>=CONVERT(date,from_dt,103) and convert(date,GETDATE(),103)<=CONVERT(date,to_date,103)");
                if (allowed.Rows.Count > 0)
                {
                    if (Convert.ToDouble(txt_amt_final.Text) < Convert.ToDouble(allowed.Rows[0]["amount"].ToString()))
                    {
                        DateTime d = Convert.ToDateTime(allowed.Rows[0]["to_date"]);
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Your authorized part payment minimum amount is Rs." + allowed.Rows[0]["amount"].ToString() + " and last date for payment " + d.ToString("dd/MM/yyyy") + "')", true);
                        txt_amt_final.Text = "";
                        flag = false;
                        return;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                else
                {
                    if (Convert.ToDouble(txt_amt_final.Text) < Convert.ToDouble(lbl_amt_final.Text))
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Entire fees need to be paid')", true);
                        txt_amt_final.Text = "";
                        flag = false;
                        return;
                    }
                    else
                    {
                        flag = true;
                    }
                }
                if (flag == true)
                {
                    if (Session["stud_Category"].ToString().ToUpper() != "OPEN")
                    {
                        if (chkAgree.Checked)
                        {
                            if (ddlNextGrp.SelectedIndex == 0)
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select one Group to proceed')", true);
                            }
                            else
                            {
                                sy_model data_cls = new sy_model();

                                data_cls.stud_id = Session["UserName"].ToString();
                                data_cls.stud_grno = ds.Tables[0].Rows[0]["stud_Grno"].ToString();
                                data_cls.stud_PermanentAdd = replacequote(Session["stud_PermanentAdd"].ToString());
                                data_cls.stud_PermanentPhone = Session["stud_PermanentPhone"].ToString();
                                data_cls.stud_NativePhone = Session["stud_NativePhone"].ToString();
                                data_cls.stud_Email = replacequote(Session["stud_Email"].ToString());
                                data_cls.stud_Gender = Session["stud_Gender"].ToString();
                                data_cls.stud_MartialStatus = Session["stud_MartialStatus"].ToString();
                                data_cls.If_PHYSICALALLY_RESERVED = replacequote(Session["If_PHYSICALALLY_RESERVED"].ToString());
                                data_cls.stud_Father_Occupation = Session["stud_Father_Occupation"].ToString();
                                data_cls.stud_Father_TelNo = Session["stud_Father_TelNo"].ToString();
                                data_cls.stud_Father_BusinessServiceAdd = replacequote(Session["stud_Father_BusinessServiceAdd"].ToString());
                                data_cls.stud_Mother_Occupation = Session["stud_Mother_Occupation"].ToString();
                                data_cls.stud_Mother_TelNo = Session["stud_Mother_TelNo"].ToString();
                                data_cls.stud_Mother_BusinessServiceAdd = replacequote(Session["stud_Mother_BusinessServiceAdd"].ToString());
                                data_cls.stud_NoOfFamilyMembers = Session["stud_NoOfFamilyMembers"].ToString();
                                data_cls.stud_Earning = Session["stud_Earning"].ToString();
                                data_cls.stud_NonEarning = Session["stud_NonEarning"].ToString();
                                data_cls.stud_YearlyIncome = Session["stud_YearlyIncome"].ToString();
                                data_cls.propose_scholarship = Session["propose_scholarship"].ToString();
                                data_cls.member_of_ncc = Session["member_of_ncc"].ToString();
                                data_cls.extra_activity = replacequote(Session["extra_activity"].ToString());
                                data_cls.ayid = ds.Tables[3].Rows[0]["to_year"].ToString();
                                data_cls.group_id = ddlNextGrp.SelectedValue.ToString();
                                data_cls.is_new_Stud = ds.Tables[3].Rows[0]["is_new_Stud"].ToString();
                                data_cls.stud_BloodGroup = ds.Tables[0].Rows[0]["stud_BloodGroup"].ToString();

                                string json = Newtonsoft.Json.JsonConvert.SerializeObject(data_cls);

                                var httpWebRequest = (HttpWebRequest)WebRequest.Create(cls1.urls() + "Login/");
                                httpWebRequest.ContentType = "text/json";
                                httpWebRequest.Method = "POST";

                                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                                {
                                    streamWriter.Write(json);
                                    streamWriter.Flush();
                                    streamWriter.Close();
                                }

                                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                                {
                                    var result = streamReader.ReadToEnd();
                                }
                                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#fyModal').modal('show');</script>", false);
                            }
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Check on I Agree to continue')", true);
                        }
                    }
                    else
                    {
                        if (ddlNextGrp.SelectedIndex == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Select one Group to proceed')", true);
                        }
                        else
                        {
                            sy_model data_cls = new sy_model();

                            data_cls.stud_id = Session["UserName"].ToString();
                            data_cls.stud_grno = ds.Tables[0].Rows[0]["stud_Grno"].ToString();
                            data_cls.stud_PermanentAdd = replacequote(Session["stud_PermanentAdd"].ToString());
                            data_cls.stud_PermanentPhone = Session["stud_PermanentPhone"].ToString();
                            data_cls.stud_NativePhone = Session["stud_NativePhone"].ToString();
                            data_cls.stud_Email = replacequote(Session["stud_Email"].ToString());
                            data_cls.stud_Gender = Session["stud_Gender"].ToString();
                            data_cls.stud_MartialStatus = Session["stud_MartialStatus"].ToString();
                            data_cls.If_PHYSICALALLY_RESERVED = replacequote(Session["If_PHYSICALALLY_RESERVED"].ToString());
                            data_cls.stud_Father_Occupation = Session["stud_Father_Occupation"].ToString();
                            data_cls.stud_Father_TelNo = Session["stud_Father_TelNo"].ToString();
                            data_cls.stud_Father_BusinessServiceAdd = replacequote(Session["stud_Father_BusinessServiceAdd"].ToString());
                            data_cls.stud_Mother_Occupation = Session["stud_Mother_Occupation"].ToString();
                            data_cls.stud_Mother_TelNo = Session["stud_Mother_TelNo"].ToString();
                            data_cls.stud_Mother_BusinessServiceAdd = replacequote(Session["stud_Mother_BusinessServiceAdd"].ToString());
                            data_cls.stud_NoOfFamilyMembers = Session["stud_NoOfFamilyMembers"].ToString();
                            data_cls.stud_Earning = Session["stud_Earning"].ToString();
                            data_cls.stud_NonEarning = Session["stud_NonEarning"].ToString();
                            data_cls.stud_YearlyIncome = Session["stud_YearlyIncome"].ToString();
                            data_cls.propose_scholarship = Session["propose_scholarship"].ToString();
                            data_cls.member_of_ncc = Session["member_of_ncc"].ToString();
                            data_cls.extra_activity = replacequote(Session["extra_activity"].ToString());
                            data_cls.ayid = ds.Tables[3].Rows[0]["to_year"].ToString();
                            data_cls.group_id = ddlNextGrp.SelectedValue.ToString();
                            data_cls.is_new_Stud = ds.Tables[3].Rows[0]["is_new_Stud"].ToString();
                            data_cls.stud_BloodGroup = ds.Tables[0].Rows[0]["stud_BloodGroup"].ToString();

                            string json = Newtonsoft.Json.JsonConvert.SerializeObject(data_cls);

                            var httpWebRequest = (HttpWebRequest)WebRequest.Create(cls1.urls() + "Login/");
                            httpWebRequest.ContentType = "text/json";
                            httpWebRequest.Method = "POST";

                            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                            {
                                streamWriter.Write(json);
                                streamWriter.Flush();
                                streamWriter.Close();
                            }

                            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var result = streamReader.ReadToEnd();
                            }
                            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#fyModal').modal('show');</script>", false);
                        }
                    }
                }
            }
        }
        catch (Exception ex1)
        {
            Response.Redirect("log_out.aspx", false);
            cls1.err_cls(ex1.ToString());
        }
    }
    protected void Payment_Click(object sender, EventArgs e)
    {
        DataSet ds = ((DataSet)Session["stud_data"]);

        int amt_val = 0;
        string name, group, category, year, group_id;
        string group_title = "select REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(group_title, '(', ''), ')', ''), '.', ''),'-',''),' ',''),'&','') as group_title  from dbo.m_crs_subjectgroup_tbl where group_id='" + ddlNextGrp.SelectedValue + "';";
        DataSet group_title_name = cls1.fill_dataset(group_title);
        string customer_acc_no = Session["UserName"].ToString() + group_title_name.Tables[0].Rows[0]["group_title"].ToString();

        DataSet payParameters = cls1.fill_dataset("select group_id,group_title from dbo.m_crs_subjectgroup_tbl where group_id='" + ddlNextGrp.SelectedValue + "'; select upper(isnull(stud_L_Name,'')+' '+isnull(stud_F_Name,'')+' '+isnull(stud_M_Name,'')) as Name,stud_Category from m_std_personaldetails_tbl where stud_id='" + Session["Username"] + "' and del_flag=0; select (substring(Duration,9,4)+'-'+substring(Duration,21,4)) as Duration from m_academic where Ayid='" + ds.Tables[3].Rows[0]["to_year"].ToString() + "';");
        group = payParameters.Tables[0].Rows[0]["group_title"].ToString();
        group_id = payParameters.Tables[0].Rows[0]["group_id"].ToString();
        name = payParameters.Tables[1].Rows[0]["Name"].ToString();
        category = payParameters.Tables[1].Rows[0]["stud_Category"].ToString();
        year = payParameters.Tables[2].Rows[0]["Duration"].ToString();

        int i = 0;
        i = Convert.ToInt32(txt_amt_final.Text);

        amt_val = Convert.ToInt32(txt_amt_final.Text);
        string str11 = "select Duration from m_academic where ayid='" + ds.Tables[3].Rows[0]["to_year"].ToString() + "'";
        Session["ayid"] = ds.Tables[3].Rows[0]["to_year"].ToString();
        DataSet dt11 = cls1.fill_dataset(str11);
        string[] sum = dt11.Tables[0].Rows[0][0].ToString().Split('/');
        sum = sum[2].Split('-');

        string str112 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid='" + ds.Tables[3].Rows[0]["to_year"].ToString() + "'";
        DataSet dt2 = cls1.fill_dataset(str112);
        string trans_id = "";
        if (dt2.Tables[0].Rows.Count > 0) { trans_id = Session["UserName"].ToString() + sum[0] + Convert.ToInt32(dt2.Tables[0].Rows.Count) + 1; }
        else
        {
            trans_id = Session["UserName"].ToString() + sum[0] + "1";
        }
        if (Convert.ToString(amt_val) != "")
        {
            string str12 = "insert into processing_fees(form_no,postinamount,postingmer_txn,Status,ayid,curr_dt) values('" + Session["UserName"].ToString() + "','" + amt_val + "','" + trans_id + "','Admission','" + ds.Tables[3].Rows[0]["to_year"].ToString() + "',getdate())";
            cls1.update_data(str12);
            group = group.Replace("COMPUTER SCIENCE & ENGINEERING (AI&ML)", "AIML");
            Response.Redirect("payment.aspx/" + amt_val + "/" + trans_id + "/" + customer_acc_no + "/student/" + name + "/" + group + "/" + category + "/" + year + "/" + group_id + "/" + Session["ayid"].ToString());

        }
    }
    protected void cash_Click(object sender, EventArgs e)
    {
        Response.Redirect("form_final.aspx");
    }

    public string replacequote(string a)
    {
        if (a.Contains("'"))
        {
            return a.Replace("'", "''");
        }
        else
        {
            return a;
        }
    }
    public void useless()
    {
        fee.stud_id = null;
        fee.Structure = null;
        fee.amount = null;
        fee.transaction_id = null;
        fee.transaction_date = null;
        fee.bankname = null;
        fee.ayid = null;
        fee.prev_ayid = null;
        fee.category = null;
        fee.group_title = null;
        fee.group_id = null;
        fee.prev_group_id = null;
        fee.subtype = null;
        fee.type = null;
        fee.user_id = null;
        fee.pay_date = null;
        fee.recpt_mode = null;
        fee.recpt_chq_no = null;
        fee.recpt_chq_dt = null;
        fee.recpt_bank_branch = null;
        fee.chq_status = null;
        fee.fee_type = null;
        fee.remark = null;
        fee.strarray = null;
    }
    public class JsonHelper
    {
        public static string JsonSerializer<T>(T t)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            ser.WriteObject(ms, t);
            string jsonString = Encoding.UTF8.GetString(ms.ToArray());
            ms.Close();
            return jsonString;
        }
    }
}