using iTextSharp.tool.xml.css;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Util;

public partial class pendingFee : System.Web.UI.Page
{
    Class1 c1 = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserName"] != null)
        {
            if (!IsPostBack)
            {
                //string qry = "select AYID,(substring(Duration,9,4)+'-'+substring(Duration,21,4)) as Duration from m_academic where ayid in (select distinct ayid from m_std_studentacademic_tbl where stud_id='" + Session["UserName"].ToString() + "') order by AYID desc;";
                string qry = "select m.ayid,(substring(Duration, 9, 4) + '-' + substring(Duration, 21, 4)) as Durations,b.Group_id[Group Id],Group_title,(substring(Duration, 9, 4) + '-' + substring(Duration, 21, 4)) + ' (' + Group_title + ')' as Duration from m_academic m ,m_std_studentacademic_tbl st, m_crs_subjectgroup_tbl b where m.ayid = st.ayid and st.stud_id = '" + Session["UserName"].ToString() + "' and b.Group_id = st.group_id";

                DataSet dss = c1.fill_dataset(qry);
                //group_id.Value = dss.Tables[1].Rows[0].ToString();
                group_id.Value = dss.Tables[0].Rows[0]["Group id"].ToString();
                ddlyear.DataSource = dss.Tables[0];
                ddlyear.DataTextField = "Duration";
                ddlyear.DataValueField = "AYID";
                ddlyear.DataBind();
                ddlyear.Items.Insert(0, new ListItem("-- Select --", "0"));
                amtshw.Visible = false;
                feedetail.Visible = false;
                if (Convert.ToBoolean(Session["RequeryProcess"]) == false)
                {
                    Session["RequeryProcess"] = true;
                }
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }

    protected void btnpay_Click(object sender, EventArgs e)
    {
        try
        {
            string stud_id = Session["UserName"].ToString().Trim();
            string str_group_id = group_id.Value.Trim();
            string str_category = txtCategory.Text.Trim();
            string ayid = ddlyear.SelectedValue.Trim();
            string pay_date = DateTime.Now.ToString("yyyy-MM-dd");
            string receipt_no = load_recipt_no(stud_id);

            int main_amount = Convert.ToInt32(txtamount.Text.Trim());

            string qrys = "SELECT CASE WHEN ISNULL(Paid, 0) >= CAST(mst.Amount AS INT) THEN 1 ELSE 0 END AS flag, mst.Struct_type, mst.Struct_name, CAST(mst.Amount AS INT) AS TotalFees, ISNULL(Paid, 0) AS Paid, CAST(mst.Amount AS INT) - ISNULL(Paid, 0) AS Balance, mst.Struct_id, mst.Rank FROM " + Session["feemaster"].ToString() + " mst LEFT JOIN (SELECT fee.Struct_id, SUM(CAST(fee.Amount AS INT)) AS Paid FROM m_FeeEntry fee WHERE fee.Stud_id = '" + stud_id + "' AND fee.Ayid = '" + ayid + "' AND fee.del_flag = 0 AND fee.Chq_status = 'Clear' GROUP BY fee.Struct_id) fee ON fee.Struct_id = mst.Struct_id WHERE mst.Ayid = '" + ayid + "' AND mst.del_flag = 0 AND mst.Group_id = '" + str_group_id + "' AND mst.Gender = '" + Session["gender"].ToString().Trim() + "' AND mst.Category = '" + str_category + "' ORDER BY mst.Rank;";
            DataTable dt = c1.fillDataTable(qrys);
            if (dt.Rows.Count > 0)
            {
                string success_qry = "";
                foreach (DataRow row in dt.Rows)
                {
                    int balance = Convert.ToInt32(row["Balance"]);
                    string structId = row["Struct_ID"].ToString();
                    string structName = row["Struct_Name"].ToString().Replace("'", "''");
                    string structType = row["Struct_Type"].ToString();

                    int allocate = 0;

                    if (main_amount > 0 && balance > 0)
                    {
                        if (main_amount >= balance)
                        {
                            allocate = balance;
                            main_amount -= balance;
                        }
                        else
                        {
                            allocate = main_amount;
                            main_amount = 0;
                        }
                    }

                    if (allocate > 0)
                    {
                        success_qry += "INSERT INTO m_FeeEntry(Stud_id, Amount, Ayid, Pay_date, Struct_id, Install_id, Struct_name, Recpt_mode, Receipt_no,Chq_status, type, user_id) VALUES ('" + stud_id + "','" + allocate + "','" + ayid + "',(CAST('" + pay_date + "' AS datetime)),NULLIF('" + structId + "',''),'" + ddl_install.SelectedValue + "','" + structName + "','Online Pay','" + receipt_no + "','Clear','" + structType + "','" + stud_id + "');";
                    }
                }

                if (div_install.Visible)
                {
                    success_qry += "update m_FeeInstallment set balance_Amount='0',PaymentStatus=1 where Install_id='" + ddl_install.SelectedValue + "' and Del_flag=0;";
                }
                if (div_fine.Visible)
                {
                    success_qry += "INSERT INTO m_FeeEntry(Stud_id, Amount, Ayid, Pay_date, Struct_id, Install_id, Struct_name, Recpt_mode, Receipt_no,Chq_status, type, user_id,Fine_flag) VALUES ('" + stud_id + "','" + txt_fine.Text.Trim() + "','" + ayid + "',(CAST('" + pay_date + "' AS datetime)),'Fine','" + ddl_install.SelectedValue + "','Fine','Online Pay','" + receipt_no + "','Clear','Fine','" + stud_id + "',1);";
                }
                //----payment processing

                string query = "select AYID,Duration as year from m_academic where ayid='" + ayid + "';";
                query += "select * from processing_fees where stud_id='" + stud_id + "' and ayid='" + ayid + "'";
                DataSet ds = c1.fill_dataset(query);
                //--year
                string[] main = ds.Tables[0].Rows[0]["year"].ToString().Split('/');
                string[] curr_year = main[2].Split('-');
                string acdyear = ds.Tables[0].Rows[0]["AYID"].ToString();
                string amount = div_fine.Visible ? (Convert.ToInt32(txtamount.Text.Trim()) + Convert.ToInt32(txt_fine.Text.Trim())).ToString() : txtamount.Text.Trim();

                //--transaction id
                string txn_id = "";
                if (ds.Tables[1].Rows.Count > 0)
                {
                    txn_id = Session["UserName"].ToString() + curr_year[0] + (Convert.ToInt32(ds.Tables[1].Rows.Count) + 1);
                }
                else
                {
                    txn_id = Session["UserName"].ToString() + curr_year[0] + "1";
                }

                string[] parts = ddlyear.SelectedItem.Text.Split('(');
                string groupname = parts[1].TrimEnd(')').Trim();
                string status = Session["UserName"].ToString() + "|" + groupname + "|" + txtCategory.Text.Trim();
                string qry = "insert into processing_fees (stud_id,ayid,amount,txn_id,IPG_txn_id,process_date,bank_txn,status_code,status_date,bank_name,Status,curr_dt,mod_dt,del_date,del_flag,txn_src) values ('" + stud_id + "','" + acdyear + "','" + amount + "','" + txn_id + "',null,GETDATE(),null,null,null,null,'" + status + "',GETDATE(),null,null,0,'FEES');";

                qry += "INSERT INTO Process_fees_Transaction_Logs (txn_id, query, curr_dt) VALUES ('" + txn_id + "', '" + success_qry.Replace("'","''") + "', GETDATE());"; //--very important for success Insert

                if (c1.DMLqueries(qry))
                {

                    string returnUrl = "http://localhost:56335/Fees_Receipt.aspx"; //--testing
                    //string returnUrl = "http://localhost:56335/Fees_Receipt.aspx"; //--live
                    Payment payment = new Payment();
                    payment.ProcessPaymentForApplicant(Session["UserName"].ToString(), amount, txn_id, status, curr_year[0] + "-" + main[main.Length - 1], returnUrl);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error in processing payment. Please try again later.');", true);
                    return;
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error in processing payment. Please try again later.');", true);
                return;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Error in processing payment. Please try again later: " + ex + "');", true);
            return;
        }
    }

    public void load_ddl_install()
    {
        c1.SetdropdownForMember1(ddl_install, "m_FeeInstallment", "(cast(Install_no As varchar) +' ('+ cast(balance_Amount As varchar) + ')') [install_no]", "Install_id", "Stud_id='" + Session["Username"] + "' and Ayid='" + ddlyear.SelectedValue + "' and Group_id='" + group_id.Value + "' and PaymentStatus=0 and Del_flag=0");

        if (ddl_install.Items.Count != 0)
        {
            div_install.Visible = true;
            amtshw.Visible = true;
        }
        else if (ddl_install.Items.Count == 0)
        {
            amtshw.Visible = true;
            txtamount.Text = lblpaid.Text;
        }
    }


    public void clear()
    {
        lblfee.Text = "";
        lblpaid.Text = "";
        lblbal.Text = "";
        txtamount.Text = "";
        lblotherfees.Text = "";
        lblgroup_id.Text = "";
        ddl_install.DataSource = null;
        ddl_install.Items.Clear();
        ddl_install.Items.Add(new ListItem("--Select--", ""));
        div_fine.Visible = false;
        feedetail.Visible = false;
    }

    protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlyear.SelectedIndex > 0)
        {
            string qry = "select distinct stud_Category,UPPER(stud_Gender) [stud_Gender] from m_std_personaldetails_tbl where stud_id='" + Session["UserName"].ToString() + "' and del_flag=0";
            DataTable dt = c1.fildatatable(qry);
            if (dt.Rows.Count > 0)
            {
                string category = dt.Rows[0]["stud_Category"].ToString().Trim().ToUpper();
                string gender = dt.Rows[0]["stud_Gender"].ToString().Trim().ToUpper();
                gender = gender == "0" ? "FEMALE" : (gender == "1" ? "MALE" : gender);
                if (dt.Rows[0]["stud_Category"].ToString() == "OPEN")
                {
                    Session["feemaster"] = "m_FeeMaster";
                    Session["gender"] = "NON";
                }
                else
                {
                    Session["feemaster"] = "m_FeeMaster_category";
                    Session["gender"] = gender;
                }

                string student_details = "SELECT ISNULL(Paid.PaidAmount, 0) AS PaidAmount,ISNULL(Total.TotalAmount, 0) AS TotalAmount,ISNULL(Total.TotalAmount, 0) - ISNULL(Paid.PaidAmount, 0) AS Balance FROM (SELECT SUM(CAST(Amount AS INT)) AS PaidAmount FROM m_FeeEntry WHERE Stud_id = '" + Session["UserName"].ToString() + "' AND Ayid = '" + ddlyear.SelectedValue.ToString() + "' AND Chq_status = 'Clear' AND del_flag = 0 and fine_flag=0) AS Paid CROSS JOIN(SELECT SUM(CAST(Amount AS INT)) AS TotalAmount FROM " + Session["feemaster"].ToString() + " WHERE Ayid = '" + ddlyear.SelectedValue.ToString() + "' AND Group_id = '" + group_id.Value + "' AND del_flag = 0 and Gender='" + Session["gender"].ToString() + "' and Category='" + category + "') AS Total";

                DataTable dt2 = c1.fildatatable(student_details);
                if (dt2.Rows.Count > 0)
                {
                    lblfee.Text = dt2.Rows[0]["TotalAmount"].ToString();
                    lblpaid.Text = dt2.Rows[0]["PaidAmount"].ToString();
                    lblbal.Text = dt2.Rows[0]["Balance"].ToString();
                    txtCategory.Text = category;
                    feedetail.Visible = true;
                    load_ddl_install();
                }
            }
        }
        else
        {
            clear();
        }
    }

    protected void lnkFineInfo_Click(object sender, EventArgs e)
    {
        DataTable dt = c1.fildatatable("select Install_id,Install_no,CONVERT(varchar, Due_date, 103)[Due_date],Install_Amount,balance_Amount,(CASE WHEN GETDATE() > Due_date THEN DATEDIFF(DAY, Due_date, GETDATE()) * 10 ELSE 0 END) [Fine_Amount],(CASE WHEN GETDATE() > Due_date THEN DATEDIFF(DAY, Due_date, GETDATE()) ELSE 0 END) [Days_Past] from m_FeeInstallment where Stud_id='" + Session["Username"] + "' and Ayid='" + ddlyear.SelectedValue.ToString() + "' and Group_id='" + group_id.Value + "' and Del_flag=0  and Install_id='" + ddl_install.SelectedValue + "'");

        if (dt.Rows.Count > 0)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<table class='table table-bordered table-striped text-center'>");
            html.Append("<thead><tr>");
            html.Append("<th>Sr. No</th>");
            html.Append("<th>Installment No</th>");
            html.Append("<th>Due Date</th>");
            html.Append("<th>Installment Amount</th>");
            html.Append("<th>Balance Amount</th>");
            html.Append("<th>Fine Amount</th>");
            html.Append("<th>Days Past</th>");
            html.Append("</tr></thead>");
            html.Append("<tbody>");

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html.Append("<tr>");
                html.Append("<td>" + (i + 1) + "</td>");
                html.Append("<td>" + dt.Rows[i]["Install_no"] + "</td>");
                html.Append("<td>" + dt.Rows[i]["Due_date"] + "</td>");
                html.Append("<td>" + dt.Rows[i]["Install_Amount"] + "</td>");
                html.Append("<td>" + dt.Rows[i]["balance_Amount"] + "</td>");
                html.Append("<td>" + dt.Rows[i]["Fine_Amount"] + "</td>");
                html.Append("<td>" + dt.Rows[i]["Days_Past"] + "</td>");
                html.Append("</tr>");
            }

            html.Append("</tbody></table>");

            litFineTable.Text = html.ToString();
            fine_stud_id.Text = Session["UserName"].ToString();
            fine_name.Text = Session["name"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('modal_fine');", true);
        }
    }

    protected void ddl_install_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtamount.Text = string.Empty;
        div_fine.Visible = false;
        if (ddl_install.SelectedValue != "0")
        {
            string selected_valuve = ddl_install.SelectedItem.Text.Trim();
            string amount = Regex.Match(selected_valuve, @"\((.*?)\)").Groups[1].Value;
            txtamount.Text = amount;
            hidden_install_amount.Value = amount;
            string qry = "select  (CASE WHEN GETDATE() > Due_date THEN DATEDIFF(DAY, Due_date, GETDATE()) * 10 ELSE 0 END) [Fine],Fine_flag from m_FeeInstallment where Stud_id='" + Session["Username"] + "' and Ayid='" + ddlyear.SelectedValue + "' and Group_id='" + group_id.Value + "' and  Install_id='" + ddl_install.SelectedValue + "' and Del_flag=0;";

            DataTable dt = c1.fildatatable(qry);
            if (dt.Rows.Count > 0)
            {
                if (Convert.ToInt32(dt.Rows[0]["Fine"]) > 0)
                {
                    div_fine.Visible = true;
                    txt_fine.Text = dt.Rows[0]["Fine"].ToString().Trim();
                }
            }
        }
        else
        {
            txtamount.Text = "";
            txtamount.Text = null;
        }
    }

    public string load_recipt_no(string stud_id)
    {
        string year = ddlyear.SelectedValue.Split('-')[0];
        string prefix = stud_id + "-" + year + "-";
        int newIncrement = 1;

        string qry = "SELECT MAX(RIGHT(Receipt_No, 2)) FROM m_FeeEntry WHERE Receipt_No LIKE '" + prefix + "%'";
        DataTable dt = c1.fillDataTable(qry);

        if (dt.Rows.Count > 0 && dt.Rows[0][0] != DBNull.Value)
        {
            int maxVal = Convert.ToInt32(dt.Rows[0][0].ToString());
            if (maxVal >= 99)
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Maximum receipt count (99) reached for this student and year !!', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
                return null;
            }
            newIncrement = maxVal + 1;
        }

        string receiptNo = prefix + newIncrement.ToString("D2");
        return receiptNo;
    }
}