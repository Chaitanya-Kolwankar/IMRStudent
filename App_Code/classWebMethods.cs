using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using System.IO;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;


/// <summary>
/// Summary description for classWebMethods
/// </summary>
public class classWebMethods
{
    QueryClass qryCls = new QueryClass();
    Class1 cls1 = new Class1();
    Fees fee = new Fees();
    SqlDataReader resultset;
    bool bolinsert;
    int intddno;
	public classWebMethods()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    public STUDENTFEES[] StrudentFeeDetails(string ayid)
    {

        List<STUDENTFEES> std_fees = new List<STUDENTFEES>();
        DataSet dst_fees_details = new DataSet();
        string addyear = "";
        int fee = 0;
        string stud_id = Convert.ToString(HttpContext.Current.Session["UserName"]);

        //it checks whether entry is present in temp table 
        try
        {

            string query = "";

            //query = query + "SELECT distinct a.Recpt_no ,stud_id,Recpt_mode,convert(varchar,[Pay_date],103) as [PAYDATE] ,b.amt FROM m_FeeEntry  a inner join (select sum(amount) amt,Recpt_no FROM m_FeeEntry where stud_id='" + stud_id + "' and ayid ='"+ayid+"' and del_flag=0 group by Recpt_no ) as b on a.Recpt_no=b.Recpt_no where stud_id='" + stud_id + "' and ayid ='"+ayid+"' and del_flag=0 group by a.Recpt_no,stud_id,Recpt_mode ,[Pay_date],Amount,b.amt";

            //dst_fees_details = cls1.fill_dataset(query);
            query = query + "SELECT distinct a.Recpt_no ,stud_id,Recpt_mode,convert(varchar,[Pay_date],103) as [PAYDATE] ,sum(b.amt) as amt,'OTHER FEES' AS struct FROM m_FeeEntry  a inner join (select sum(amount) amt,Recpt_no,Struct_name FROM m_FeeEntry where stud_id='" + stud_id + "' and amount <> '0' and ayid ='" + ayid + "' and del_flag=0 group by Recpt_no,Struct_name ) as b on a.Recpt_no=b.Recpt_no where stud_id='" + stud_id + "' and ayid ='" + ayid + "' and del_flag=0 and a.Struct_name=b.Struct_name and a.struct_name not like 'TU%' and a.struct_name not like 'Dev%'  group by a.Recpt_no,stud_id,Recpt_mode ,convert(varchar,[Pay_date],103)";
            query = query + " union all SELECT distinct a.Recpt_no ,stud_id,Recpt_mode,convert(varchar,[Pay_date],103) as [PAYDATE] ,sum(b.amt) as amt,'TUTION FEES' AS struct FROM m_FeeEntry  a inner join (select sum(amount) amt,Recpt_no,Struct_name FROM m_FeeEntry where stud_id='" + stud_id + "' and amount <> '0' and ayid ='" + ayid + "' and del_flag=0 group by Recpt_no,Struct_name ) as b on a.Recpt_no=b.Recpt_no where stud_id='" + stud_id + "' and ayid ='" + ayid + "' and del_flag=0 and a.Struct_name=b.Struct_name and (a.struct_name  like 'TU%'  or a.struct_name  like 'Dev%')  group by a.Recpt_no,stud_id,Recpt_mode ,convert(varchar,[Pay_date],103)";
            dst_fees_details = cls1.fill_dataset(query);

            int diff = 0;
            int chkdiff = 0;
            if (dst_fees_details.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i <= dst_fees_details.Tables[0].Rows.Count - 1; i++)
                {
                    STUDENTFEES stm = new STUDENTFEES();
                    int cnt = 0;

                    stm.RECIPTNO = (dst_fees_details.Tables[0].Rows[i]["Recpt_no"].ToString());
                    stm.RECIPTMODE = (dst_fees_details.Tables[0].Rows[i]["Recpt_mode"].ToString());
                    stm.PAYDATE = dst_fees_details.Tables[0].Rows[i]["PAYDATE"].ToString();
                    //stm.REMARK = dst_fees_details.Tables[0].Rows[i]["Remark"].ToString();
                    stm.AMOUNT = dst_fees_details.Tables[0].Rows[i]["amt"].ToString();
                    stm.structname = dst_fees_details.Tables[0].Rows[i]["struct"].ToString();
                    stm.message = "";
                    std_fees.Add(stm);
                }
            }



        }
        catch (Exception ex)
        {
            // Interaction.MsgBox(ex.Message);
        }
        return std_fees.ToArray();

    }
    public bool receipt_type(string stud_id, string year, string recipt_no)
    {
        string str = ""; 
        bool flag=true ;
        str = "select Struct_name from m_feeentry where stud_id='" + stud_id + "' and ayid='" + year + "' and recpt_no='" + recipt_no + "' and Struct_name not like '%Tuition%' and Struct_name not like '%Tution%' and Struct_name not like 'develop%' and del_flag=0";
        DataTable dt = cls1.fildatatable(str);
        //for (int i=0; i < dt.Rows.Count ; i++)
        //{

        //    if (dt.Rows[i]["Struct_name"].ToString().Contains("Tution Fees") == true || dt.Rows[i]["Struct_name"].ToString().Contains("Development Fees") == true)
        //    {
        //        flag = true;
        //        break;
        //    }
        //    else
        //    {
        //        flag = false;
               
        //    }
        //}
        //if (flag ==true)
        //{
        //    return false;
        //}
        //else
        //{
        //    return true;
        //}

        if (dt.Rows.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public List<ListItem> fillayid(string stud_id)
    {

        //string finalGrp = qryCls.splitGrp(Convert.ToString(HttpContext.Current.Session["group_ids"]));

        string qry = "select AYID,(substring(Duration,9,4)+'-'+substring(Duration,21,4)) as Duration from m_academic where ayid in (select distinct ayid from m_std_studentacademic_tbl where stud_id='" + stud_id + "') order by AYID desc";
       // String qry = "select AYID,(substring(Duration,9,4)+'-'+substring(Duration,21,4)) as Duration from m_academic order by ayid desc";

        string constr = ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> tname = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        tname.Add(new ListItem
                        {
                            Value = sdr["AYID"].ToString(),
                            Text = sdr["Duration"].ToString()
                        });
                    }
                }
                con.Close();
                return tname;
            }
        }
    }

    public void getenggayid(DropDownList ddl)
    {
        //String qry = "select AYID,(substring(Duration,9,4)+'-'+substring(Duration,21,4)) as Duration from m_academic";
        string qry = "select AYID,(substring(Duration,9,4)+'-'+substring(Duration,21,4)) as Duration from m_academic where ayid in (select distinct ayid from m_FeeEntry where del_flag = 0) order by AYID desc";
        DataSet dss = cls1.fill_dataset(qry);
        ddl.DataSource = dss.Tables[0];
        ddl.DataTextField = "Duration";
        ddl.DataValueField = "AYID";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("-- Select --", "0"));
    }

    //-------------------------------payment gateway work----------------------------------
    public bool ATOMREQUERY(string stud_id)
    {
        bool processed = false;
        string requery = "select * from processing_fees where form_no='" + stud_id.ToString() + "' and convert(date,curr_dt,103)>convert(date,GETDATE()-30,103) and (postingf_code='' or postingf_code is null) ";
        //remove this condition after one month of mentioned date, initial setup for requery implementation by chinmay
        //requery += " and convert(date,curr_dt,103)>='2022-07-08'";
        //--------------------------------------------------------------
        DataTable dt = cls1.fildatatable(requery);
        if (dt.Rows.Count > 0)
        {
            string MerchantLogin, encdata;
            MerchantLogin = "65805";//-------------------different for each institute
            string TransactionID, TransactionAmount, TransactionDate;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    TransactionID = dt.Rows[i]["postingmer_txn"].ToString();
                    TransactionAmount = dt.Rows[i]["postinamount"].ToString();
                    DateTime date1 = DateTime.Parse(dt.Rows[i]["curr_dt"].ToString());
                    TransactionDate = date1.ToString("yyyy-MM-dd");

                    encdata = "merchantid=" + MerchantLogin + "&merchanttxnid=" + TransactionID + "&amt=" + TransactionAmount + "&tdate=" + TransactionDate;

                    //-------------------different for each institute
                    string passphrase = "83D1E1EC3DEE483BB698935F9B365805"; // AES Request key
                    string salt = "83D1E1EC3DEE483BB698935F9B365805"; // AES Request Salt/IV Key
                    string passphrase1 = "19DE2650AF672D308C508346BDD65805"; // AES Response Key
                    string salt1 = "19DE2650AF672D308C508346BDD65805"; // AES Response Salt/IV Key
                    //-----------------------------------------------------------
                    byte[] iv = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
                    int iterations = 65536;
                    int keysize = 256;
                    string Encryptval = Encrypt(encdata, passphrase, salt, iv, iterations);
                    string Link = "https://payment.atomtech.in/paynetz/vftsv2?login=" + MerchantLogin + "&encdata=" + Encryptval;
                    ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Link);
                    request.ProtocolVersion = HttpVersion.Version11;
                    request.Method = "POST";
                    request.ContentType = "application/json";
                    ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
                    request.Proxy.Credentials = CredentialCache.DefaultCredentials;
                    Encoding encoding = new UTF8Encoding();
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    Stream resStream = response.GetResponseStream();
                    StreamReader reader = new StreamReader(resStream);
                    string responseFromServer = reader.ReadToEnd();
                    string Decryptval = decrypt(responseFromServer, passphrase1, salt1, iv, iterations);
                    var result = Decryptval.Replace("[", "");
                    result = result.Replace("]", "");
                    Payverify.Payverify objectres = new Payverify.Payverify();
                    objectres = new System.Web.Script.Serialization.JavaScriptSerializer().Deserialize<Payverify.Payverify>(result);
                    string merchantID = objectres.merchantID;
                    string merchantTxnID = objectres.merchantTxnID;
                    string amnt = objectres.amt;
                    string verified = objectres.verified;
                    string statusCode = objectres.statusCode;
                    string desc = objectres.desc;
                    string bid = objectres.bid;
                    string bankname = objectres.bankName;
                    string atomTxnId = objectres.atomTxnId;
                    string discriminator = objectres.discriminator;
                    string surcharge = objectres.surcharge;
                    string cardNumber = objectres.cardNumber;
                    string txnDate = objectres.txnDate;
                    string udf9 = objectres.udf9;
                    string reconStatus = objectres.reconStatus;
                    string sdt = objectres.sdt;
                    request.Abort();
                    useless();
                    string urlalias = cls1.urls();
                    string url = @urlalias + "Fees/";
                    DataTable details = new DataTable();
                    if (dt.Rows[i]["Status"].ToString().ToUpper() == "ADMISSION")
                    {
                        details = cls1.fildatatable("select * from www_m_std_personaldetails_tbl where stud_id='" + stud_id.ToString() + "' and ayid='" + dt.Rows[i]["ayid"].ToString() + "' and field_type='Group_id'");
                        fee.type = "inhouseAdmission_success";
                        fee.group_id = details.Rows[0]["value"].ToString();
                    }
                    else if (dt.Rows[i]["Status"].ToString().ToUpper() == "PENDING FEE")
                    {
                        details = cls1.fildatatable("select * from m_std_studentacademic_tbl where stud_id='" + stud_id.ToString() + "' and ayid='" + dt.Rows[i]["ayid"].ToString() + "' and del_flag=0");
                        fee.type = "pendingfee_success";
                        fee.group_id = details.Rows[0]["Group_id"].ToString();
                    }

                    if (details.Rows.Count > 0)
                    {
                        cls1.DMLqueries(" update processing_fees set postinamount='" + amnt.ToString() + "',postingmmp_txn='" + atomTxnId + "',postingprod='TECHNOLOGY',postingdate='" + txnDate + "',postingbank_txn='" + bid + "',postingbank_name='" + bankname + "',signature='Matched',postingdiscriminator='" + discriminator + "' where form_no='" + stud_id.ToString() + "' and postingmer_txn='" + TransactionID + "' and ayid='" + dt.Rows[i]["ayid"].ToString() + "' ");

                        if (verified.ToString().ToUpper() == "SUCCESS")
                        {

                            fee.stud_id = stud_id.ToString();
                            fee.ayid = dt.Rows[i]["ayid"].ToString().ToString();
                            fee.amount = amnt.ToString();
                            fee.bankname = bankname.ToString();
                            fee.transaction_id = merchantTxnID.ToString();
                            fee.transaction_date = txnDate.ToString();
                            fee.user_id = stud_id.ToString();

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
                                string APIresult = streamReader.ReadToEnd();
                                DataSet ds = JsonConvert.DeserializeObject<DataSet>(APIresult);
                                if (ds.Tables.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["transaction_id"].ToString() != "")
                                    {
                                        cls1.DMLqueries(" update processing_fees set postingf_code='Requery Ok' where form_no='" + stud_id + "' and postingmer_txn='" + TransactionID + "' and ayid='" + dt.Rows[i]["ayid"].ToString() + "' ");
                                    }
                                }
                            }
                        }
                        else if (verified.ToString().ToUpper() == "FAILED")
                        {
                            cls1.DMLqueries(" update processing_fees set postingf_code='Requery F' where form_no='" + stud_id + "' and postingmer_txn='" + TransactionID + "' and ayid='" + dt.Rows[i]["ayid"].ToString() + "' ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    i = -1;
                }
            }
            processed = true;
        }
        return processed;
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
    public static string byteToHexString(byte[] byData)
    {
        StringBuilder sb = new StringBuilder((byData.Length * 2));
        for (int i = 0; (i < byData.Length); i++)
        {
            int v = (byData[i] & 255);
            if ((v < 16))
            {
                sb.Append('0');
            }

            sb.Append(v.ToString("X"));

        }

        return sb.ToString();
    }
    public string Encrypt(string plainText, string passphrase, string salt, Byte[] iv, int iterations)
    {
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        string data = ByteArrayToHexString(Encrypt(plainBytes, GetSymmetricAlgorithm(passphrase, salt, iv, iterations))).ToUpper();


        return data;
    }
    public String decrypt(String plainText, String passphrase, String salt, Byte[] iv, int iterations)
    {
        byte[] str = HexStringToByte(plainText);

        string data1 = Encoding.UTF8.GetString(decrypt(str, GetSymmetricAlgorithm(passphrase, salt, iv, iterations)));
        return data1;
    }
    public byte[] Encrypt(byte[] plainBytes, SymmetricAlgorithm sa)
    {
        return sa.CreateEncryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);

    }
    public byte[] decrypt(byte[] plainBytes, SymmetricAlgorithm sa)
    {
        return sa.CreateDecryptor().TransformFinalBlock(plainBytes, 0, plainBytes.Length);
    }
    public SymmetricAlgorithm GetSymmetricAlgorithm(String passphrase, String salt, Byte[] iv, int iterations)
    {
        var saltBytes = new byte[16];
        var ivBytes = new byte[16];
        Rfc2898DeriveBytes rfcdb = new System.Security.Cryptography.Rfc2898DeriveBytes(passphrase, Encoding.UTF8.GetBytes(salt), iterations);
        saltBytes = rfcdb.GetBytes(32);
        var tempBytes = iv;
        Array.Copy(tempBytes, ivBytes, Math.Min(ivBytes.Length, tempBytes.Length));
        var rij = new RijndaelManaged(); //SymmetricAlgorithm.Create();
        rij.Mode = CipherMode.CBC;
        rij.Padding = PaddingMode.PKCS7;
        rij.FeedbackSize = 128;
        rij.KeySize = 128;

        rij.BlockSize = 128;
        rij.Key = saltBytes;
        rij.IV = ivBytes;
        return rij;
    }
    protected static byte[] HexStringToByte(string hexString)
    {
        try
        {
            int bytesCount = (hexString.Length) / 2;
            byte[] bytes = new byte[bytesCount];
            for (int x = 0; x < bytesCount; ++x)
            {
                bytes[x] = Convert.ToByte(hexString.Substring(x * 2, 2), 16);
            }
            return bytes;
        }
        catch
        {
            throw;
        }
    }
    public static string ByteArrayToHexString(byte[] ba)
    {
        StringBuilder hex = new StringBuilder(ba.Length * 2);
        foreach (byte b in ba)
            hex.AppendFormat("{0:x2}", b);
        return hex.ToString();
    }
    //-------------------------------payment gateway work----------------------------------
}