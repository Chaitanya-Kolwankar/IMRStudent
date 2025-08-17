using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;
using Newtonsoft.Json;
using System.Xml;

public partial class FeeReceiptMiscFees : System.Web.UI.Page
{
    Class1 cls = new Class1();
    Fees fee = new Fees();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        string stud_id = "", year = "", recpt_no = "";

        stud_id = Request.QueryString["id"];
        recpt_no = Request.QueryString["recpt"];
        year = Request.QueryString["ayid"];
        if (stud_id != "" && year != "" && recpt_no != "" && stud_id != null && year != null && recpt_no != null)
        {
            if (!IsPostBack)
            {
                try
                {
                    useless();
                    string urlalias = cls.urls();
                    string url = @urlalias + "Fees/";

                    fee.type = "receipt";
                    fee.subtype = "Misc";
                    fee.stud_id = stud_id.ToString();
                    fee.transaction_id = recpt_no.ToString();
                    fee.ayid = year.ToString();

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
                    }
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables.Contains("Error") == true)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + ds.Tables["Error"].Rows[0][0].ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        }
                        else
                        {
                            string engg = "";
                            if (ds.Tables["Group"].Rows[0]["Group_id"].ToString() != "GRP042")
                            {
                                engg = " Engineering ";
                            }
                           
                            lblNo.Text = ds.Tables["Name"].Rows[0]["stud_id"].ToString() + "/" + ds.Tables["Name"].Rows[0]["Receipt_no"].ToString();
                            lbl_date.Text = ds.Tables["Structure"].Rows[0]["Date"].ToString();
                            lblName.Text = ds.Tables["Name"].Rows[0]["name"].ToString().ToUpper();
                            lblamount.Text = ds.Tables["Calculated"].Rows[0]["Inwords"].ToString();
                            lblcourse.Text = ds.Tables["Group"].Rows[0]["Group_title"].ToString().ToUpper() +engg+ "  " + ds.Tables["Group"].Rows[0]["Year"].ToString().ToUpper();
                            lblcategory.Text = ds.Tables["Name"].Rows[0]["stud_category"].ToString();
                            gridstructre.DataSource = ds.Tables["Structure"];
                            gridstructre.DataBind();
                            gridpayment.DataSource = ds.Tables["Payment"];
                            gridpayment.DataBind();
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + ex.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
            }
        }
        else
        {
            Response.Redirect("login.aspx", true);
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