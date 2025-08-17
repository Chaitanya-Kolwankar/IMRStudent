using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Web; // Required for HttpContext

/// <summary>
/// A self-contained helper class to fetch Student data,
/// process the payment, and handle the redirect to the payment gateway.
/// </summary>
public class Payment
{
    private Class1 c1 = new Class1();

    public void ProcessPaymentForApplicant(string stud_id, string amount, string transactionId, string status, string Year,string returnUrl)
    {
        // --- 1. Fetch Data from Database ---
        string sqlQuery = "SELECT (F_name + ' ' + L_name) AS Name, Email_id, Mob_No, Address_line1, Address_line2, city, State, pincode FROM d_adm_applicant WHERE stud_id='" + stud_id + "' and Del_Flag=0";

        DataTable dt = c1.fillDataTable(sqlQuery);

        if (dt != null && dt.Rows.Count > 0)
        {
            // --- 2. Populate Payment Data ---
            Hashtable paymentData = new Hashtable
            {
                { "name", dt.Rows[0]["Name"].ToString() },
                { "email", dt.Rows[0]["Email_id"].ToString() },
                { "phone", dt.Rows[0]["Mob_No"].ToString() },
                { "order_id", transactionId },
                { "amount", amount },
                { "description", "ACADEMIC FEES: "+ Year },
                { "address_line_1", dt.Rows[0]["Address_line1"].ToString() },
                { "address_line_2", dt.Rows[0]["Address_line2"].ToString() },
                { "city", dt.Rows[0]["city"].ToString() },
                { "state", dt.Rows[0]["State"].ToString() },
                { "zip_code", dt.Rows[0]["pincode"].ToString() },
                { "udf1", status }
            };
            
            // --- 3. Initiate the Payment ---
            InitiatePaymentRedirect(paymentData, returnUrl);
        }
        else
        {
            // If no data is found, throw an error to be caught by the calling page.
            throw new Exception("Applicant details could not be found for Student ID: " + stud_id);
        }
    }

    /// <summary>
    /// Private method to handle the core payment processing and redirection.
    /// </summary>
    private void InitiatePaymentRedirect(Hashtable paymentData, string returnUrl)
    {
        // Get Configuration Values
        string apiKey = ConfigurationManager.AppSettings["API_KEY"];
        string mode = ConfigurationManager.AppSettings["MODE"];
        string salt = ConfigurationManager.AppSettings["SALT"];
        string paymentUrl = ConfigurationManager.AppSettings["UATPAYMENTS_URL"];

        paymentData.Add("api_key", apiKey);
        paymentData.Add("return_url", returnUrl);
        paymentData.Add("mode", mode);
        paymentData.Add("country", "IND");
        paymentData.Add("currency", "INR");
        paymentData.Add("SALT", salt);

        // Calculate Hash
        string[] hashVarsSeq = ConfigurationManager.AppSettings["hashSequence"].Split('|');
        StringBuilder hashStringBuilder = new StringBuilder();
        foreach (string hash_var in hashVarsSeq)
        {
            hashStringBuilder.Append(paymentData.ContainsKey(hash_var) ? paymentData[hash_var] : "");
            hashStringBuilder.Append("|");
        }
        hashStringBuilder.Remove(hashStringBuilder.Length - 1, 1);

        string hash = Generatehash512(hashStringBuilder.ToString()).ToUpper();

        // Prepare Form Data for Submission
        Hashtable dataToPost = new Hashtable();
        foreach (DictionaryEntry item in paymentData)
        {
            if (item.Key.ToString() != "SALT")
            {
                dataToPost.Add(item.Key, item.Value);
            }
        }
        dataToPost.Add("hash", hash);

        // Prepare and Post the Form using HttpContext
        string strForm = PreparePOSTForm(paymentUrl, dataToPost);

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write(strForm);
        HttpContext.Current.Response.End();
    }

    // --- Helper methods for hashing and form creation ---
    private string Generatehash512(string text)
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
            return hex;
        }
    }

    private string PreparePOSTForm(string url, Hashtable data)
    {
        string formID = "PostForm";
        StringBuilder strForm = new StringBuilder();
        strForm.Append(string.Format("<form id=\"{0}\" name=\"{0}\" action=\"{1}\" method=\"POST\">", formID, url));
        foreach (System.Collections.DictionaryEntry key in data)
        {
            strForm.Append(string.Format("<input type=\"hidden\" name=\"{0}\" value=\"{1}\">", key.Key, key.Value));
        }
        strForm.Append("</form>");
        StringBuilder strScript = new StringBuilder();
        strScript.Append("<script language='javascript'>");
        strScript.Append(string.Format("var v{0} = document.{0};", formID));
        strScript.Append(string.Format("v{0}.submit();", formID));
        strScript.Append("</script>");
        return strForm.ToString() + strScript.ToString();
    }
}