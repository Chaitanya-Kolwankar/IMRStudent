using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class fee_receipt_print : System.Web.UI.Page
{
    Fees fee = new Fees();
    Class1 cls = new Class1();
    DataSet ds = new DataSet();
    classWebMethods qrye = new classWebMethods();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null && Session["UserName"].ToString() != "")
        {
            if (!IsPostBack)
            {
                fillyear(Session["UserName"].ToString());
                //if (Convert.ToBoolean(Session["RequeryProcess"]) == false)
                //{
                //    bool processed = qrye.ATOMREQUERY(Session["UserName"].ToString());
                //    Session["RequeryProcess"] = true;
                //}
            }
        }
    }

    public void fillyear(string stud_id)
    {
        string qry = "select m.ayid,(substring(Duration, 9, 4) + '-' + substring(Duration, 21, 4)) as Durations,b.Group_id[Group Id],Group_title,(substring(Duration, 9, 4) + '-' + substring(Duration, 21, 4)) + ' (' + Group_title + ')' as Duration from m_academic m ,m_std_studentacademic_tbl st, m_crs_subjectgroup_tbl b where m.ayid = st.ayid and st.stud_id = '" + Session["UserName"].ToString() + "' and b.Group_id = st.group_id";

        DataSet dss = cls.fill_dataset(qry);
        //group_id.Value = dss.Tables[1].Rows[0].ToString();
        //group_id.Value = dss.Tables[0].Rows[0]["Group id"].ToString();
        ddlayid.DataSource = dss.Tables[0];
        ddlayid.DataTextField = "Duration";
        ddlayid.DataValueField = "AYID";
        ddlayid.DataBind();
        ddlayid.Items.Insert(0, new ListItem("-- Select --", "0"));
    }

    protected void grdtransaction_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Print")
        {
            int RowIndex = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grdtransaction.Rows[RowIndex];
            Label lblrecptno = (Label)row.FindControl("lblrecptno");
            Label lbltype= (Label)row.FindControl("lblstruct");
            Label lblstatus= (Label)row.FindControl("lblstatus");
            if (lblstatus.Text == "Clear")
            {
                string str = "id=" + Session["UserName"].ToString() + "&recpt=" + lblrecptno.Text + "&ayid=" + ddlayid.SelectedValue.ToString() + "";

                if (lbltype.Text == "OTHER FEES")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "redirect('FeeReceiptOtherFees.aspx?" + str + "');", true);
                }
                else if(lbltype.Text== "TUTION FEES")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "redirect('FeeReceiptTutionFees.aspx?" + str + "');", true);
                }
                else if (lbltype.Text == "FEES")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "redirect('FeeReceiptMergeFees.aspx?" + str + "');", true);
                }
                else if(lbltype.Text== "MISCELLANEOUS FEES")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "redirect('FeeReceiptMiscFees.aspx?" + str + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Payment for the following transaction is not cleared yet', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
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

    protected void ddlayid_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlayid.SelectedIndex > 0)
            {
                useless();
                string urlalias = cls.urls();
                string url = @urlalias + "Fees/";

                fee.type = "receipt";
                fee.subtype = "studentlistingmerge";      // subtype change for merge of receipt old subtype(studentlisting)
                fee.stud_id = Session["UserName"].ToString();
                fee.ayid = ddlayid.SelectedValue.ToString();

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
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            grdtransaction.DataSource = ds.Tables[0];
                            grdtransaction.DataBind();
                        }
                        else
                        {
                            grdtransaction.DataSource = null;
                            grdtransaction.DataBind();
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Fees not paid for selected year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        }
                    }
                }
                else
                {
                    grdtransaction.DataSource = null;
                    grdtransaction.DataBind();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Fees not paid for selected year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
            }
            else
            {
                grdtransaction.DataSource = null;
                grdtransaction.DataBind();
            }
        }
        catch(Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + ex.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
        }
    }
}