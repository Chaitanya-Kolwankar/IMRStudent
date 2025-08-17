using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Specialized;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Configuration;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Net;
using Newtonsoft.Json;

public partial class ResponsePendingFee : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Class1 c1 = new Class1();
    Fees fee = new Fees();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                NameValueCollection nvc = Request.Form;

                if (Request.Params["mer_txn"] != null || Request.Params["mer_txn"] != "")
                {
                    if (Request.Params["mmp_txn"] != null)
                    {
                        string postingmmp_txn = Request.Params["mmp_txn"].ToString();
                        string postingmer_txn = Request.Params["mer_txn"];
                        string postinamount = Request.Params["amt"].ToString();
                        string postingprod = Request.Params["prod"].ToString();
                        string postingdate = Request.Params["date"].ToString();
                        string postingbank_txn = Request.Params["bank_txn"].ToString();
                        string postingf_code = Request.Params["f_code"].ToString();
                        string postingbank_name = Request.Params["bank_name"].ToString();
                        string signature = Request.Params["signature"].ToString();
                        string postingdiscriminator = Request.Params["discriminator"].ToString();
                        string customername = "";
                        string customermail = "";
                        string customerno = "";
                        string group_name = Request.Params["udf10"].ToString();
                        string category = Request.Params["udf11"].ToString();
                        string year = Request.Params["udf12"].ToString();
                        string group_id = Request.Params["udf14"].ToString();
                        string ayid = Request.Params["udf15"].ToString();

                        string respHashKey = "13368ae603a8f909ff";
                        string ressignature = "";
                        string strsignature = postingmmp_txn + postingmer_txn + postingf_code + postingprod + postingdiscriminator + postinamount + postingbank_txn;
                        byte[] bytes = Encoding.UTF8.GetBytes(respHashKey);
                        byte[] b = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
                        ressignature = byteToHexString(b).ToLower();

                        if (signature == ressignature)
                        {
                            if (postingf_code == "F")
                            {
                                lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                            }
                            else if (postingf_code == "C")
                            {
                                lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                            }
                            else if (postingf_code == "S")
                            {
                                lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                            }
                            else if (postingf_code == "Ok")
                            {
                                lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                            }
                        }

                        String[] val = new String[0];
                        string id = "";

                        id = postingmer_txn.Substring(0, 8);

                        string str = "    update processing_fees set name='" + customername + "',mobile_no='" + customerno + "',email='" + customermail + "',postinamount='" + postinamount + "',postingmmp_txn='" + postingmmp_txn + "',postingprod='" + postingprod + "',postingdate='" + postingdate + "',postingbank_txn='" + postingbank_txn + "',postingf_code='" + postingf_code + "',postingbank_name='" + postingbank_name + "',signature='Matched',postingdiscriminator='" + postingdiscriminator + "' where form_no='" + id + "' and postingmer_txn='" + postingmer_txn + "'";
                        c1.update_data(str);

                        str = "";
                        str = "select Status from processing_fees where form_no='" + id + "' and postingmer_txn='" + postingmer_txn + "'";
                        DataTable dt = c1.fildatatable(str);

                        if (postingf_code == "S" || postingf_code == "Ok")
                        {
                            useless();
                            string urlalias = c1.urls();
                            string url = @urlalias + "Fees/";
                            if (dt.Rows[0][0].ToString().Contains("Pending") == true)
                            {
                                fee.type = "pendingfee_success";
                            }
                            else if(dt.Rows[0][0].ToString().Contains("Admission") == true)
                            {
                                fee.type = "inhouseAdmission_success";
                            }
                            fee.stud_id = id.ToString();
                            fee.ayid = ayid.ToString();
                            fee.amount = postinamount.ToString();
                            fee.group_id = group_id.ToString();
                            fee.bankname = postingbank_name.ToString();
                            fee.transaction_id = postingmer_txn.ToString();
                            fee.user_id = id.ToString();

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

                            QRCodeEncoder encoder = new QRCodeEncoder();
                            encoder.QRCodeScale = 5;

                            lbl_date1.Text = postingdate;
                            long total2 = Convert.ToInt32(postinamount.Split('.')[0]);
                            lbl_amount_1.Text = ds.Tables[0].Rows[0]["inwords"].ToString().ToUpper() + " ONLY";
                            lblstudentid.Text = ds.Tables[0].Rows[0]["stud_id"].ToString().ToUpper();
                            lbl_name_1.Text = ds.Tables[0].Rows[0]["name"].ToString().ToUpper();
                            lbl_no_1.Text = ds.Tables[0].Rows[0]["transaction_id"].ToString().ToUpper();
                            lbl_category_1.Text = ds.Tables[0].Rows[0]["stud_Category"].ToString().ToUpper();

                            lbltransaction_id.Text = postingmmp_txn;
                            lblstatus1.Text = "SUCCESSFUL";
                            lblcourse.Text = ds.Tables[1].Rows[0]["Group_title"].ToString().ToUpper();

                            if (postingdiscriminator == "NB")
                            {
                                lblmode.Text = "NET BANKING";
                            }
                            else if (postingdiscriminator == "CC")
                            {
                                lblmode.Text = "CREDIT CARD";
                            }
                            else if (postingdiscriminator == "DC")
                            {
                                lblmode.Text = "DEBIT CARD";
                            }
                            else if (postingdiscriminator == "IM")
                            {
                                lblmode.Text = "IMPS";
                            }
                            else if (postingdiscriminator == "MX")
                            {
                                lblmode.Text = "AMERICAN EXPRESS CARD";
                            }
                            else if (postingdiscriminator == "BQ")
                            {
                                lblmode.Text = "BHARAT QR";
                            }
                            lblamountdigits.Text = total2.ToString().ToUpper();
                            lblvivatransction.Text = postingmer_txn.ToUpper();
                            lblbank.Text = postingbank_name.ToUpper();
                        }
                        else
                        {
                            

                            if(postingf_code=="F")
                            {
                                if (postingdiscriminator == "NB")
                                {
                                    lblmode.Text = "NET BANKING";
                                }
                                else if (postingdiscriminator == "CC")
                                {
                                    lblmode.Text = "CREDIT CARD";
                                }
                                else if (postingdiscriminator == "DC")
                                {
                                    lblmode.Text = "DEBIT CARD";
                                }
                                else if (postingdiscriminator == "IM")
                                {
                                    lblmode.Text = "IMPS";
                                }
                                else if (postingdiscriminator == "MX")
                                {
                                    lblmode.Text = "AMERICAN EXPRESS CARD";
                                }
                                else if (postingdiscriminator == "BQ")
                                {
                                    lblmode.Text = "BHARAT QR";
                                }
                                else
                                {
                                    lblmode.Text = "--";
                                }
                                lblbank.Text= postingbank_name.ToUpper();
                                lblstatus1.Text = "UNSUCCESSFUL";
                                lbl_deductedinfo.Visible = true;
                            }
                            else
                            {
                                lblmode.Text = "--";
                                lblbank.Text = "--";
                                lblstatus1.Text = "CANCELLED";
                            }

                            useless();
                            string urlalias = c1.urls();
                            string url = @urlalias + "Fees/";

                            fee.type = "payment_failure";
                            fee.stud_id = id.ToString();
                            fee.ayid = ayid.ToString();
                            fee.amount = postinamount.ToString();
                            fee.group_id = group_id.ToString();

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

                            lbl_date1.Text = postingdate;
                            long total2 = Convert.ToInt32(postinamount.Split('.')[0]);
                            lbl_amount_1.Text = ds.Tables[0].Rows[0]["inwords"].ToString().ToUpper() + " ONLY";
                            lblstudentid.Text = ds.Tables[0].Rows[0]["stud_id"].ToString().ToUpper();
                            lbl_name_1.Text = ds.Tables[0].Rows[0]["name"].ToString().ToUpper();
                            lbl_no_1.Text = "--";
                            lbl_category_1.Text = ds.Tables[0].Rows[0]["stud_Category"].ToString().ToUpper();

                            lbltransaction_id.Text = postingmmp_txn;
                            lblcourse.Text = ds.Tables[1].Rows[0]["Group_title"].ToString().ToUpper();
                            
                            lblamountdigits.Text = total2.ToString().ToUpper();
                            lblvivatransction.Text = postingmer_txn.ToUpper();
                            
                        }
                    }
                    else
                    {
                        Response.Redirect("login.aspx");
                    }
                }
                else
                {
                    Response.Redirect("login.aspx");
                }
            }
        }
        catch (Exception ex)
        {
            c1.err_cls(ex.ToString());
            Response.Redirect("login.aspx");
        }
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

    public void barcode(string id)
    {
        string barCode = id;
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
        {
            using (Graphics graphics = Graphics.FromImage(bitMap))
            {

                Font oFont = new Font("IDAutomationHC39M", 16);
                PointF point = new PointF(2f, 2f);
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush whiteBrush = new SolidBrush(Color.White);
                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();

                Convert.ToBase64String(byteImage);
                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
        }
    }
    public static byte[] ImageToByte2(System.Drawing.Image img)
    {
        byte[] byteArray = new byte[0];
        using (MemoryStream stream = new MemoryStream())
        {
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Close();

            byteArray = stream.ToArray();
        }
        return byteArray;
    }
    public void barcode1(string id)
    {
        string barCode = id;
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
        {
            using (Graphics graphics = Graphics.FromImage(bitMap))
            {

                Font oFont = new Font("IDAutomationHC39M", 16);
                PointF point = new PointF(2f, 2f);
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush whiteBrush = new SolidBrush(Color.White);
                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();

                Convert.ToBase64String(byteImage);
                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
        }
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
}