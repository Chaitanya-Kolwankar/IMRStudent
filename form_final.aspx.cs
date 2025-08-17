using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Web.Services;

public partial class form_final : System.Web.UI.Page
{
    SqlCommand cmd = new SqlCommand();
    DataSet ds;
    DataSet stud_marks = new DataSet();

    Class1 C1 = new Class1();
    String grpname;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"].ToString() != null)
            {
                string url = C1.urls() + "Login/" + Session["UserName"].ToString();

                HttpWebRequest request_data = (HttpWebRequest)WebRequest.Create(url);
                request_data.Method = "GET";
                HttpWebResponse response_data = (HttpWebResponse)request_data.GetResponse();

                String stringsize_data = response_data.ContentLength.ToString();
                StreamReader reader_data = new StreamReader(response_data.GetResponseStream());

                if (Convert.ToInt32(stringsize_data) > 77)
                {
                    JObject results = JObject.Parse(reader_data.ReadToEnd());
                    string name = "";

                    DataSet stud_ds = new DataSet();
                    stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["student_details"].ToString()));
                    stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["student_handicap"].ToString()));
                    stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["student_group"].ToString()));
                    stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["stud_nxt_class"].ToString()));
                    stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["stud_current_acad"].ToString()));
                    stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["stud_nxt_grps"].ToString()));
                    if (results.Count > 7)
                    {
                        stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["stud_mod_data"].ToString()));

                    }

                    ds = stud_ds;
                }
                else
                {
                    String msg = reader_data.ReadToEnd();
                    Session["admission_msg"] = msg;
                    Response.Redirect("admission_error.aspx", false);
                }




                if (ds.Tables.Count > 0)
                {

                    String full_name = ds.Tables[0].Rows[0]["stud_L_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_F_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_M_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_Mother_FName"].ToString().TrimEnd();

                    string sBaseUrl = C1.urls() + "auth/" + Session["UserName"].ToString() + "/" + full_name + "/" + ds.Tables[0].Rows[0]["stud_DOB"].ToString();

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sBaseUrl);
                    request.Method = "GET";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    String stringsize = response.ContentLength.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    if (Convert.ToInt32(stringsize) > 15)
                    {
                        JObject results = JObject.Parse(reader.ReadToEnd());
                        string name = "";
                        stud_marks.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["cre_earned"].ToString()));
                    }

                    else
                    {
                        String msg = reader.ReadToEnd();


                    }

                    DataTable dtyear = C1.fildatatable("select concat(substring(Duration,9,4),'-',substring(Duration,21,4)) as year from m_academic where ayid='" + ds.Tables[3].Rows[0]["to_year"].ToString() + "'");

                    string a = "", b = "", c = "", d = "";
                    lblstud_id.Text = Session["UserName"].ToString();
                    lbl_group_id.Text = ds.Tables[2].Rows[0]["Group_title"].ToString();
                    lbl_year.Text = dtyear.Rows[0]["year"].ToString();
                    lblfname.Text = full_name.ToUpper();


                    lblgender.Text = ds.Tables[0].Rows[0]["stud_Gender"].ToString().ToUpper();
                    String s_bld = ds.Tables[0].Rows[0]["stud_BloodGroup"].ToString();

                    if (s_bld == "0" || s_bld == "A +ve")
                    {
                        lblblood.Text = "A +ve";
                    }
                    else if (s_bld == "1" || s_bld == "A -ve")
                    {
                        lblblood.Text = "A -ve";
                    }
                    else if (s_bld == "2" || s_bld == "B +ve")
                    {
                        lblblood.Text = "B +ve";
                    }
                    else if (s_bld == "3" || s_bld == "B -ve")
                    {
                        lblblood.Text = "B -ve";
                    }
                    else if (s_bld == "4" || s_bld == "AB +ve")
                    {
                        lblblood.Text = "AB +ve";
                    }
                    else if (s_bld == "5" || s_bld == "AB -ve")
                    {
                        lblblood.Text = "AB -ve";
                    }
                    else if (s_bld == "6" || s_bld == "O +ve")
                    {
                        lblblood.Text = "O +ve";
                    }
                    else if (s_bld == "7" || s_bld == "O -ve")
                    {
                        lblblood.Text = "O -ve";
                    }
                    else if (s_bld == "")
                    {
                        lblblood.Text = "-";
                    }

                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["stud_caste"].ToString()))
                    {
                        lblcaste.Text = ds.Tables[0].Rows[0]["stud_caste"].ToString().ToUpper();
                    }
                    else
                    {
                        lblcaste.Text = "";
                    }
                    string strcaste = "select * from StudCast_tbl where stud_id='" + Session["UserName"].ToString() + "'";
                    DataSet dscst = C1.fill_dataset(strcaste);

                    if (dscst.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["stud_Category"].ToString() == "EBC" || ds.Tables[0].Rows[0]["stud_Category"].ToString() == "SEBC" || ds.Tables[0].Rows[0]["stud_Category"].ToString() == "EWS")
                        {
                            lblincdt.Text = dscst.Tables[0].Rows[0]["income_dt"].ToString();
                            lblincno.Text = dscst.Tables[0].Rows[0]["income_no"].ToString();
                            lblcsdt.Text = "-";
                            lblcstval.Text = "-";
                        }
                        else if (ds.Tables[0].Rows[0]["stud_Category"].ToString() != "EBC" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "OPEN" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "TFWS")
                        {
                            lblincdt.Text = dscst.Tables[0].Rows[0]["income_dt"].ToString();
                            lblincno.Text = dscst.Tables[0].Rows[0]["income_no"].ToString();
                            lblcsdt.Text = dscst.Tables[0].Rows[0]["cast_dt"].ToString();
                            lblcstval.Text = dscst.Tables[0].Rows[0]["Cast_no"].ToString();
                        }
                        else
                        {
                            lblincdt.Text = "-";
                            lblincno.Text = "-";
                            lblcsdt.Text = "-";
                            lblcstval.Text = "-";
                        }

                        if (dscst.Tables[0].Rows[0]["Aadhar_No"].ToString() != "")
                        {
                            lblaadhar.Text = dscst.Tables[0].Rows[0]["Aadhar_No"].ToString();
                        }
                        else
                        {
                            lblaadhar.Text = "-";
                        }

                        if (dscst.Tables[0].Rows[0]["extra2"].ToString() != "")
                        {
                            lblmahid.Text = dscst.Tables[0].Rows[0]["extra2"].ToString();
                        }
                        else
                        {
                            lblmahid.Text = "-";
                        }

                        if (dscst.Tables[0].Rows[0]["extra3"].ToString() != "")
                        {
                            lblmahpass.Text = dscst.Tables[0].Rows[0]["extra3"].ToString();
                        }
                        else
                        {
                            lblmahpass.Text = "-";
                        }
                    }
                    else
                    {
                        lblincdt.Text = "-";
                        lblincno.Text = "-";
                        lblcsdt.Text = "-";
                        lblcstval.Text = "-";
                        lblaadhar.Text = "-";
                        lblmahid.Text = "-";
                        lblmahpass.Text = "-";
                    }


                    lbldob.Text = ds.Tables[0].Rows[0]["stud_DOB"].ToString().ToUpper();
                    lblnat.Text = ds.Tables[0].Rows[0]["stud_Nationality"].ToString().ToUpper();
                    lblreligion.Text = ds.Tables[0].Rows[0]["stud_Religion"].ToString().ToUpper();
                    lblbirthplace.Text = ds.Tables[0].Rows[0]["stud_BirthPlace"].ToString().ToUpper();
                    lbldom.Text = ds.Tables[0].Rows[0]["stud_DomiciledIn"].ToString().ToUpper();
                    lbladdress.Text = Session["stud_PermanentAdd"].ToString().ToUpper();
                    lblemailadd.Text = Session["stud_Email"].ToString();
                    lblmobno.Text = Session["stud_PermanentPhone"].ToString();
                    lblphnno.Text = Session["stud_NativePhone"].ToString();
                    lblcat.Text = ds.Tables[0].Rows[0]["stud_Category"].ToString().ToUpper();
                    lblmar.Text = Session["stud_MartialStatus"].ToString().ToUpper();

                    grpname = ds.Tables[5].Rows[0]["Group_title"].ToString().Trim();


                    lblfaocc.Text = Session["stud_Father_Occupation"].ToString().ToUpper();
                    lblfatel.Text = Session["stud_Father_TelNo"].ToString();
                    lblfabuss.Text = Session["stud_Father_BusinessServiceAdd"].ToString().ToUpper();
                    lblmoocc.Text = Session["stud_Mother_Occupation"].ToString().ToUpper();
                    lblmotel.Text = Session["stud_Mother_TelNo"].ToString();
                    lblmobuss.Text = Session["stud_Mother_BusinessServiceAdd"].ToString().ToUpper();
                    lblnoofperson.Text = Session["stud_NoOfFamilyMembers"].ToString();
                    lblearn.Text = Session["stud_Earning"].ToString();
                    lblnonearn.Text = Session["stud_NonEarning"].ToString();
                    lblsannualincome.Text = Session["stud_YearlyIncome"].ToString();


                    lbldom.Text = ds.Tables[0].Rows[0]["stud_DomiciledIn"].ToString().ToUpper();

                    lblnssncc.Text = Session["member_of_ncc"].ToString().ToUpper();

                    lblscholarfree.Text = Session["propose_scholarship"].ToString().ToUpper();

                    lblphhan.Text = checkNullnew(Session["If_PHYSICALALLY_RESERVED"].ToString()).ToUpper();

                    lblciricular.Text = checkNullnew(Session["extra_activity"].ToString()).ToUpper();

                    Image1.ImageUrl = "data:image/png;base64," + Session["image"];

                    Session.Clear();
                }
                else
                {
                    Response.Redirect("Login.aspx", false);

                }

            }
            else
            {
                Session.Clear();

                Response.Redirect("Login.aspx", true);
            }

        }
        catch (Exception ex)
        {
            Session.Clear();

            Response.Redirect("Login.aspx", true);
        }
    }

    public String checkNull(string s1)
    {
        string stra = "select field_type ,value from dbo.www_m_std_personaldetails_tbl where field_type = '" + s1 + "' and stud_id = '" + Session["UserName"] + "'";
        DataSet dsnew = C1.fill_dataset(stra);
        if (dsnew.Tables[0].Rows.Count > 0)
        {
            return dsnew.Tables[0].Rows[0]["value"].ToString();
        }
        else
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][s1] == DBNull.Value)
                {
                    return "";
                }
                else
                {
                    return ds.Tables[0].Rows[0][s1].ToString();
                }
            }
            else
            {
                return "";
            }
        }

    }

    public String checkNullnew(string s1)
    {
        if (s1 == "")
        {
            return "-";
        }
        else
        {

            return s1;
        }


    }
}