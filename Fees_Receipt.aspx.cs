using System;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;

public partial class Fees_Receipt : System.Web.UI.Page
{
    Class1 c1 = new Class1();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                string salt = ConfigurationManager.AppSettings["SALT"];
                string received_hash = Request.Form["hash"];
                string hash_string = salt;

                string[] keys = Request.Form.AllKeys;
                Array.Sort(keys, StringComparer.Ordinal);

                foreach (string key in keys)
                {
                    if (!string.IsNullOrEmpty(Request.Form[key]) && key != "hash")
                    {
                        hash_string += "|" + Request.Form[key];
                    }
                }
                string generated_hash = Generatehash512(hash_string);
                if (generated_hash.Equals(received_hash, StringComparison.OrdinalIgnoreCase))
                {
                    string txn_id = Request.Form["order_id"].ToString();
                    string amount = Request.Form["amount"].ToString();
                    string IPG_txn_id = Request.Form["transaction_id"].ToString();
                    string process_date = Request.Form["payment_datetime"].ToString();
                    string status_code = return_status(Request.Form["response_message"].ToString().ToUpper());
                    string process_mode = Request.Form["payment_mode"].ToString();
                    string name = Request.Form["name"].ToString();
                    string Status = Request.Form["response_message"].ToString();
                    string Description = Request.Form["description"].ToString();
                    string udf1 = Request.Form["udf1"].ToString();
                    string stud_id = udf1.Split('|')[0];
                    double doubleValue = double.Parse(amount);
                    long longValue = (long)doubleValue;

                    string str_proc = "select * from processing_fees where stud_id='" + stud_id + "' and txn_id='" + txn_id + "' and status_code='SUCCESS'";
                    DataTable dt_proc = c1.fillDataTable(str_proc);
                    if (dt_proc.Rows.Count == 0)
                    {
                        //---------------update Processing fees
                        string str = "update processing_fees set amount='" + amount + "',IPG_txn_id='" + IPG_txn_id + "',process_mode='" + process_mode + "',process_date='" + process_date + "',status_code='" + status_code + "',mod_dt=GETDATE(),status_date=GETDATE() where stud_id='" + stud_id + "' and txn_id='" + txn_id + "'";
                        c1.DMLqueries(str);

                        //----------Fill Recipt With Data
                        lblstudentid.Text = stud_id;
                        lbl_name_1.Text = name;
                        lblcourse.Text = udf1.Split('|')[1];
                        lbl_no_1.Text = txn_id;
                        lbl_date1.Text = process_date;
                        lblamountdigits.Text = amount;
                        lbl_amount_1.Text = ConvertNumbertoWords(Convert.ToInt32(longValue)) + " ONLY";
                        lblvivatransction.Text = txn_id;
                        lbltransaction_id.Text = IPG_txn_id;
                        lblmode.Text = process_mode;
                        lblstatus1.Text = Status;
                        lbldescription.Text = Description;

                        //----------Payment StatusCheck
                        if (status_code == "SUCCESS")
                        {
                            //------UPDATE FOR SUCESSES
                           DataTable dt_sucess= c1.fillDataTable("SELECT query from Process_fees_Transaction_Logs where txn_id='" + txn_id + "'");
                            c1.DMLqueries(dt_sucess.Rows[0]["query"].ToString());

                        }
                        else
                        {
                            if (status_code == "FAILED")
                            {
                                lbl_deductedinfo.Visible = true;
                                lbl_no_1.Text = "--";
                            }
                            else if (status_code == "ABORTED")
                            {
                                lbl_no_1.Text = "--";
                                lblmode.Text = "--";
                            }
                            else
                            {
                                lbl_deductedinfo.Visible = true;
                                lbl_no_1.Text = "--";
                            }
                        }
                    }
                }
                else
                {
                    // HASH MISMATCH!
                    lblstatus1.Text = "FAILED";
                    lblStatus.Text = "Error: Transaction could not be verified (Hash Mismatch).";
                    lblStatus.Visible = true;
                }

            }
            catch (Exception ex)
            {
                lblStatus.Text = "An unexpected error occurred: " + ex.Message;
                lblStatus.Visible = true;
            }
        }
    }

    // --- Helper Methods ---

    public string Generatehash512(string text)
    {
        byte[] message = Encoding.UTF8.GetBytes(text);
        using (SHA512Managed hashString = new SHA512Managed())
        {
            byte[] hashValue = hashString.ComputeHash(message);
            string hex = "";
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex.ToUpper(); // Return as uppercase to match common practice
        }
    }

    public string ConvertNumbertoWords(long number)
    {
        if (number == 0) return "ZERO";
        if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
        string words = "";
        if ((number / 1000000) > 0)
        {
            words += ConvertNumbertoWords(number / 100000) + " LAKHS ";
            number %= 1000000;
        }
        if ((number / 1000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
            number %= 1000;
        }
        if ((number / 100) > 0)
        {
            words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
            number %= 100;
        }
        if (number > 0)
        {
            if (words != "") words += "AND ";
            var unitsMap = new[] { "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN" };
            var tensMap = new[] { "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY" };
            if (number < 20) words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0) words += " " + unitsMap[number % 10];
            }
        }
        return words;
    }

    public string return_status(string status)
    {
        if (status.ToUpper().Contains("SUCCESS"))
        {
            return "SUCCESS";
        }
        else if (status.ToUpper().Contains("FAILED"))
        {
            return "FAILED";
        }
        else if (status.ToUpper().Contains("CANCELLED"))
        {
            return "CANCELLED";
        }
        else
        {
            return "FAILED";
        }
    }
}