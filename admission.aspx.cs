using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admission : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    Class1 cls1 = new Class1();
    classWebMethods qrye = new classWebMethods();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            tab1.Visible = true;
            tab2.Visible = false;
            if (Session["UserName"].ToString() != null)
            {
                if (!IsPostBack)
                {
                    if (Convert.ToBoolean(Session["RequeryProcess"]) == false)
                    {
                        bool processed = qrye.ATOMREQUERY(Session["UserName"].ToString());
                        Session["RequeryProcess"] = true;
                    }
                    if (Convert.ToBoolean(Session["admission"]) == true)
                    {
                        DataSet dsChk = cls1.fill_dataset("select ayid from m_std_studentacademic_tbl where ayid=(select MAX(ayid) from dbo.m_academic) AND stud_id='" + Session["UserName"].ToString() + "' and del_flag=0");
                        if (dsChk.Tables[0].Rows.Count > 0)
                        {
                            Session["admission_done"] = true;
                            Response.Redirect("fee_receipt_print.aspx", false);
                        }
                        else
                        {
                            Session["admission_done"] = false;
                        }
                    }
                    else
                    {
                        Session["admission_done"] = false;
                    }
                    string sBaseUrl = "https://vit.vivacollege.in/StudentPortalApi_eng/Login/" + Session["UserName"].ToString();
                    //string sBaseUrl = "http://localhost:49856/StudentPortalApi_eng/Login/" + Session["UserName"].ToString();

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(sBaseUrl);
                    request.Method = "GET";
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    String stringsize = response.ContentLength.ToString();
                    StreamReader reader = new StreamReader(response.GetResponseStream());

                    //dvResult.InnerHtml = reader.ReadToEnd();
                    // far = reader.ReadToEnd(); 
                    //object far = reader.ReadToEnd();

                    if (Convert.ToInt32(stringsize) > 77)
                    {
                        JObject results = JObject.Parse(reader.ReadToEnd());
                        string name = "";
                        //DataTable dt = JsonConvert.DeserializeObject<DataTable>(results.ToString());
                        //dt_json = (DataTable)JsonConvert.DeserializeObject(results.ToString(), ));
                        //Object jObject = JsonConvert.DeserializeObject<JObject>(results.Content);                     

                        DataSet stud_ds = new DataSet();
                        stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["student_details"].ToString()));
                        //stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["student_photo"].ToString()));
                        stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["student_handicap"].ToString()));
                        stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["student_group"].ToString()));
                        stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["stud_nxt_class"].ToString()));
                        stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["stud_current_acad"].ToString()));
                        stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["stud_nxt_grps"].ToString()));
                        if (results.Count > 7)
                        {
                            stud_ds.Tables.Add(JsonConvert.DeserializeObject<DataTable>(results["stud_mod_data"].ToString()));
                        }

                        Session["stud_data"] = stud_ds;
                        //  Session["UserName"] = stud_ds.Tables[0].Rows[0]["stud_id"];
                        //  Session["image"] = stud_ds.Tables[1].Rows[0]["STUD_Photo"];
                        Session["to_year"] = string.Empty;
                        Session["Email_ID"] = string.Empty;
                        Session["DOB"] = string.Empty;
                        Session["Datebirth"] = string.Empty;
                        Session["FileUpload1"] = string.Empty;
                        Session["Image_flag"] = string.Empty;
                        Session["disp"] = string.Empty;
                        Session["new_flag"] = string.Empty;
                        Session["Aadhar"] = string.Empty;
                        Session["cast_no"] = string.Empty;
                        Session["cast_dt"] = string.Empty;
                        Session["income_no"] = string.Empty;
                        Session["income_dt"] = string.Empty;
                        getData();

                        // Response.Redirect("BasicDetails.aspx", false);
                    }
                    else
                    {
                        String msg = reader.ReadToEnd();
                        Session["admission_msg"] = msg;
                        Response.Redirect("admission_error.aspx", false);
                        //  lbl_msg.Visible = true;
                        //lbl_msg.Text = msg;
                        //lbl_msg.ForeColor = Color.Red;
                        //ScriptManager.RegisterStartupScript(msgDiv, msgDiv.GetType(), "blinkeffect", "blinkeffect('#divMsg');", true);
                        //txtUserName.BorderColor = Color.Red;
                        //txtUserName.ForeColor = Color.Gray;
                        //txtPassword.BorderColor = Color.Red;
                        //txtPassword.ForeColor = Color.Gray;
                        //txtUserName.Focus();                    
                    }
                }
            }
            else
            {
                Response.Redirect("log_out.aspx", false);
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("log_out.aspx", false);
        }
    }

    public void getData()
    {
        DataSet ds = new DataSet();

        ds = ((DataSet)Session["stud_data"]);
        if (ds.Tables.Count > 6)
        {
            f_name.Text = ds.Tables[0].Rows[0]["stud_F_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_M_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_L_Name"].ToString();
            Session["name"] = f_name.Text.ToString();
            dob.Text = ds.Tables[0].Rows[0]["stud_DOB"].ToString();
            address.Text = ds.Tables[6].Rows[0]["stud_PermanentAdd"].ToString();
            mob_no.Text = ds.Tables[6].Rows[0]["stud_PermanentPhone"].ToString().Trim();
            phn_no.Text = ds.Tables[6].Rows[0]["stud_NativePhone"].ToString();
            stud_Email.Text = ds.Tables[6].Rows[0]["stud_email"].ToString();
            drp_mar.SelectedValue = ds.Tables[6].Rows[0]["stud_MartialStatus"].ToString();
            txtfatheroccupation.SelectedItem.Text = ds.Tables[6].Rows[0]["stud_Father_Occupation"].ToString();
            father_mob.Text = ds.Tables[6].Rows[0]["stud_Father_TelNo"].ToString();
            fat_add.Text = ds.Tables[6].Rows[0]["stud_Father_BusinessServiceAdd"].ToString();
            drp_motheroccu.SelectedItem.Text = ds.Tables[6].Rows[0]["stud_Mother_occupation"].ToString();
            mot_add.Text = ds.Tables[6].Rows[0]["stud_Mother_BusinessServiceAdd"].ToString();
            mother_mob.Text = ds.Tables[6].Rows[0]["stud_Mother_TelNo"].ToString().Trim();
            person_fam.Text = ds.Tables[6].Rows[0]["stud_NoOfFamilyMembers"].ToString();

            earn.Text = ds.Tables[6].Rows[0]["stud_Earning"].ToString();
            NON_earn.Text = ds.Tables[6].Rows[0]["stud_NonEarning"].ToString();
            fam_income.Text = ds.Tables[6].Rows[0]["stud_YearlyIncome"].ToString();

            drp_scholarship.SelectedValue = ds.Tables[6].Rows[0]["propose_scholarship"].ToString();
            drp_ncc.SelectedValue = ds.Tables[6].Rows[0]["member_of_ncc"].ToString();
            txt_activity.Text = ds.Tables[6].Rows[0]["extra_activity"].ToString();

            string strcast = "select * from StudCast_tbl where Stud_id='" + Session["UserName"].ToString() + "'";
            DataSet dtcst = cls1.fill_dataset(strcast);
            if (dtcst.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["Aadhar_no"].ToString()))
                {
                    txtaadhar.Text = dtcst.Tables[0].Rows[0]["Aadhar_no"].ToString();
                    Session["Aadhar"] = dtcst.Tables[0].Rows[0]["Aadhar_no"].ToString();
                }
                else
                {
                    txtaadhar.Text = "";
                    Session["Aadhar"] = "";
                }

                if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["extra2"].ToString()) || !string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["extra3"].ToString()))
                {
                    txtmahaid.Text = dtcst.Tables[0].Rows[0]["extra2"].ToString();
                    txtmahapass.Text = dtcst.Tables[0].Rows[0]["extra3"].ToString();
                    Session["Mahadbtid"] = dtcst.Tables[0].Rows[0]["extra2"].ToString();
                    Session["Mahadbtpass"] = dtcst.Tables[0].Rows[0]["extra3"].ToString();
                }
                else
                {
                    Session["Mahadbtid"] = "";
                    Session["Mahadbtpass"] = "";
                    txtmahaid.Text = "";
                    txtmahapass.Text = "";
                }


                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["stud_Category"].ToString()))
                {
                    if (ds.Tables[0].Rows[0]["stud_Category"].ToString() != "OPEN" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "EBC" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "SEBC" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "EWS" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "TFWS")
                    {
                        if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["Cast_no"].ToString()))
                        {
                            txtcastno.Text = dtcst.Tables[0].Rows[0]["Cast_no"].ToString();
                            Session["cast_no"] = dtcst.Tables[0].Rows[0]["Cast_no"].ToString();
                        }
                        else
                        {
                            txtcastno.Text = "";
                            Session["cast_no"] = "";
                        }

                        if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["cast_dt"].ToString()))
                        {
                            setcastdd(dtcst.Tables[0].Rows[0]["Cast_dt"].ToString());
                            Session["cast_dt"] = dtcst.Tables[0].Rows[0]["Cast_dt"].ToString();
                        }
                        else
                        {
                            ddmonth.SelectedIndex = 0;
                            dddate.SelectedIndex = 0;
                            ddyear.SelectedIndex = 0;
                            Session["cast_dt"] = "";
                        }

                        if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["income_dt"].ToString()))
                        {
                            setincomedd(dtcst.Tables[0].Rows[0]["income_dt"].ToString());
                            Session["income_dt"] = dtcst.Tables[0].Rows[0]["income_dt"].ToString();
                        }
                        else
                        {
                            dincmonth.SelectedIndex = 0;
                            dincdate.SelectedIndex = 0;
                            dincyear.SelectedIndex = 0;
                            Session["income_dt"] = "";
                        }

                        if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["income_no"].ToString()))
                        {
                            txtincno.Text = dtcst.Tables[0].Rows[0]["income_no"].ToString();
                            Session["income_no"] = dtcst.Tables[0].Rows[0]["income_no"].ToString();
                        }
                        else
                        {
                            txtincno.Text = "";
                            Session["income_no"] = "";
                        }
                    }

                    if (ds.Tables[0].Rows[0]["stud_Category"].ToString() == "EBC" || ds.Tables[0].Rows[0]["stud_Category"].ToString() == "SEBC" || ds.Tables[0].Rows[0]["stud_Category"].ToString() == "EWS")
                    {
                        if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["income_dt"].ToString()))
                        {
                            setincomedd(dtcst.Tables[0].Rows[0]["income_dt"].ToString());
                            Session["income_dt"] = dtcst.Tables[0].Rows[0]["income_dt"].ToString();
                        }
                        else
                        {
                            dincmonth.SelectedIndex = 0;
                            dincdate.SelectedIndex = 0;
                            dincyear.SelectedIndex = 0;
                            Session["income_dt"] = "";
                        }

                        if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["income_no"].ToString()))
                        {
                            txtincno.Text = dtcst.Tables[0].Rows[0]["income_no"].ToString();
                            Session["income_no"] = dtcst.Tables[0].Rows[0]["income_no"].ToString();
                        }
                        else
                        {
                            txtincno.Text = "";
                            Session["income_no"] = "";
                        }
                    }
                    if (ds.Tables[0].Rows[0]["stud_Category"].ToString() == "TFWS")
                    {
                        Session["income_no"] = "";
                        Session["income_dt"] = "";
                        Session["cast_no"] = "";
                        Session["cast_dt"] = "";
                    }
                }
                else
                {
                    Session["Aadhar"] = "";
                    txtaadhar.Text = "";
                }
            }

            if (ds.Tables[6].Rows[0]["If_PHYSICALALLY_RESERVED"].ToString() != "")
            {
                phychk.Checked = true;
                //DropDown_phy.Visible = true;
                DropDown_phy.Style.Add("display", "block");
                //phyd.Visible = true;
                //phyd.Text = ds.Tables[6].Rows[0]["If_PHYSICALALLY_RESERVED"].ToString();
                DropDown_phy.SelectedValue = ds.Tables[6].Rows[0]["If_PHYSICALALLY_RESERVED"].ToString();
            }
            else
            {
                phychk.Checked = false;
                //phyd.Visible = false;
                //DropDown_phy.Visible = false;
                DropDown_phy.Style.Add("display", "none");
            }
            if (ds.Tables[0].Rows[0]["year_of_adm"].ToString() != "")
            {
                drp_joining.SelectedIndex = drp_joining.Items.IndexOf(drp_joining.Items.FindByText(ds.Tables[0].Rows[0]["year_of_adm"].ToString()));
            }
            else
            {
                drp_joining.SelectedIndex = 0;
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["stud_Category"].ToString()))
            {
                drop_category(ds.Tables[0].Rows[0]["stud_Category"].ToString());
                ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(ds.Tables[0].Rows[0]["stud_Category"].ToString()));
                if (ds.Tables[0].Rows[0]["stud_Category"].ToString() != "OPEN" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "EBC" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "SEBC" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "EWS" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "TFWS")
                {
                    castdiv.Visible = true;
                    incomdiv.Visible = true;
                }
                else if (ds.Tables[0].Rows[0]["stud_Category"].ToString() == "EBC" || ds.Tables[0].Rows[0]["stud_Category"].ToString() == "SEBC" || ds.Tables[0].Rows[0]["stud_Category"].ToString() == "EWS")
                {
                    castdiv.Visible = false;
                    incomdiv.Visible = true;
                }
                else
                {
                    castdiv.Visible = false;
                    incomdiv.Visible = false;
                }
            }
            else
            {
                ddlCategory.SelectedIndex = 0;
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["stud_Caste"].ToString()))
            {
                ddlCast.SelectedIndex = ddlCast.Items.IndexOf(ddlCast.Items.FindByText(ds.Tables[0].Rows[0]["stud_Caste"].ToString()));
            }
            else
            {
                ddlCast.SelectedIndex = 0;
            }
        }
        else
        {
            f_name.Text = ds.Tables[0].Rows[0]["stud_F_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_M_Name"].ToString() + " " + ds.Tables[0].Rows[0]["stud_L_Name"].ToString();
            Session["name"] = f_name.Text.ToString();
            dob.Text = ds.Tables[0].Rows[0]["stud_DOB"].ToString();
            address.Text = ds.Tables[0].Rows[0]["stud_PermanentAdd"].ToString();
            mob_no.Text = ds.Tables[0].Rows[0]["stud_PermanentPhone"].ToString();
            phn_no.Text = ds.Tables[0].Rows[0]["stud_NativePhone"].ToString();
            stud_Email.Text = ds.Tables[0].Rows[0]["stud_email"].ToString();
            drp_mar.SelectedValue = ds.Tables[0].Rows[0]["stud_MartialStatus"].ToString();
            txtfatheroccupation.SelectedItem.Text = ds.Tables[0].Rows[0]["stud_Father_Occupation"].ToString();
            father_mob.Text = ds.Tables[0].Rows[0]["stud_Father_TelNo"].ToString();
            fat_add.Text = ds.Tables[0].Rows[0]["stud_Father_BusinessServiceAdd"].ToString();
            drp_motheroccu.SelectedItem.Text = ds.Tables[0].Rows[0]["stud_Mother_occupation"].ToString();
            mot_add.Text = ds.Tables[0].Rows[0]["stud_Mother_ResidentAdd"].ToString();
            mother_mob.Text = ds.Tables[0].Rows[0]["stud_Mother_TelNo"].ToString();
            person_fam.Text = ds.Tables[0].Rows[0]["stud_NoOfFamilyMembers"].ToString();



            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["stud_Category"].ToString()))
            {
                drop_category(ds.Tables[0].Rows[0]["stud_Category"].ToString());
                ddlCategory.SelectedIndex = ddlCategory.Items.IndexOf(ddlCategory.Items.FindByText(ds.Tables[0].Rows[0]["stud_Category"].ToString()));
                if (ds.Tables[0].Rows[0]["stud_Category"].ToString() != "OPEN" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "EBC" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "SEBC" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "EWS" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "TFWS")
                {
                    castdiv.Visible = true;
                    incomdiv.Visible = true;
                }
                else if (ds.Tables[0].Rows[0]["stud_Category"].ToString() == "EBC" || ds.Tables[0].Rows[0]["stud_Category"].ToString() == "SEBC" || ds.Tables[0].Rows[0]["stud_Category"].ToString() == "EWS")
                {
                    castdiv.Visible = false;
                    incomdiv.Visible = true;
                }
                else
                {
                    castdiv.Visible = false;
                    incomdiv.Visible = false;
                }
            }
            else
            {
                ddlCategory.SelectedIndex = 0;
            }


            string strcast = "select * from StudCast_tbl where Stud_id='" + Session["UserName"].ToString() + "'";
            DataSet dtcst = cls1.fill_dataset(strcast);
            if (dtcst.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["Aadhar_no"].ToString()))
                {
                    txtaadhar.Text = dtcst.Tables[0].Rows[0]["Aadhar_no"].ToString();
                    Session["Aadhar"] = dtcst.Tables[0].Rows[0]["Aadhar_no"].ToString();
                }
                else
                {
                    txtaadhar.Text = "";
                    Session["Aadhar"] = "";
                }

                if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["extra2"].ToString()) || !string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["extra3"].ToString()))
                {
                    txtmahaid.Text = dtcst.Tables[0].Rows[0]["extra2"].ToString();
                    txtmahapass.Text = dtcst.Tables[0].Rows[0]["extra3"].ToString();
                    Session["Mahadbtid"] = dtcst.Tables[0].Rows[0]["extra2"].ToString();
                    Session["Mahadbtpass"] = dtcst.Tables[0].Rows[0]["extra3"].ToString();
                }
                else
                {
                    Session["Mahadbtid"] = "";
                    Session["Mahadbtpass"] = "";
                    txtmahaid.Text = "";
                    txtmahapass.Text = "";
                }

                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["stud_Category"].ToString()))
                {
                    if (ds.Tables[0].Rows[0]["stud_Category"].ToString() != "OPEN" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "EBC" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "SEBC" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "EWS" && ds.Tables[0].Rows[0]["stud_Category"].ToString() != "TFWS")
                    {
                        if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["Cast_no"].ToString()))
                        {
                            txtcastno.Text = dtcst.Tables[0].Rows[0]["Cast_no"].ToString();
                            Session["cast_no"] = dtcst.Tables[0].Rows[0]["Cast_no"].ToString();
                        }
                        else
                        {
                            txtcastno.Text = "";
                            Session["cast_no"] = "";
                        }

                        if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["cast_dt"].ToString()))
                        {
                            setcastdd(dtcst.Tables[0].Rows[0]["Cast_dt"].ToString());
                            Session["cast_dt"] = dtcst.Tables[0].Rows[0]["Cast_dt"].ToString();
                        }
                        else
                        {
                            ddmonth.SelectedIndex = 0;
                            dddate.SelectedIndex = 0;
                            ddyear.SelectedIndex = 0;
                            Session["cast_dt"] = "";
                        }

                        if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["income_dt"].ToString()))
                        {
                            setincomedd(dtcst.Tables[0].Rows[0]["income_dt"].ToString());
                            Session["income_dt"] = dtcst.Tables[0].Rows[0]["income_dt"].ToString();
                        }
                        else
                        {
                            dincmonth.SelectedIndex = 0;
                            dincdate.SelectedIndex = 0;
                            dincyear.SelectedIndex = 0;
                            Session["income_dt"] = "";
                        }

                        if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["income_no"].ToString()))
                        {
                            txtincno.Text = dtcst.Tables[0].Rows[0]["income_no"].ToString();
                            Session["income_no"] = dtcst.Tables[0].Rows[0]["income_no"].ToString();
                        }
                        else
                        {
                            txtincno.Text = "";
                            Session["income_no"] = "";
                        }
                    }

                    if (ds.Tables[0].Rows[0]["stud_Category"].ToString() == "EBC" || ds.Tables[0].Rows[0]["stud_Category"].ToString() == "SEBC" || ds.Tables[0].Rows[0]["stud_Category"].ToString() == "EWS")
                    {
                        if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["income_dt"].ToString()))
                        {
                            setincomedd(dtcst.Tables[0].Rows[0]["income_dt"].ToString());
                            Session["income_dt"] = dtcst.Tables[0].Rows[0]["income_dt"].ToString();
                        }
                        else
                        {
                            dincmonth.SelectedIndex = 0;
                            dincdate.SelectedIndex = 0;
                            dincyear.SelectedIndex = 0;
                            Session["income_dt"] = "";
                        }

                        if (!string.IsNullOrEmpty(dtcst.Tables[0].Rows[0]["income_no"].ToString()))
                        {
                            txtincno.Text = dtcst.Tables[0].Rows[0]["income_no"].ToString();
                            Session["income_no"] = dtcst.Tables[0].Rows[0]["income_no"].ToString();
                        }
                        else
                        {
                            txtincno.Text = "";
                            Session["income_no"] = "";
                        }
                    }

                    if (ds.Tables[0].Rows[0]["stud_Category"].ToString() == "TFWS")
                    {
                        Session["income_no"] = "";
                        Session["income_dt"] = "";
                        Session["cast_no"] = "";
                        Session["cast_dt"] = "";
                    }
                }
                else
                {
                    Session["Aadhar"] = "";
                    txtaadhar.Text = "";
                }
            }

            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["stud_Caste"].ToString()))
            {
                ddlCast.SelectedIndex = ddlCast.Items.IndexOf(ddlCast.Items.FindByText(ds.Tables[0].Rows[0]["stud_Caste"].ToString()));
            }
            else
            {
                ddlCast.SelectedIndex = 0;
            }
            earn.Text = ds.Tables[0].Rows[0]["stud_Earning"].ToString();
            NON_earn.Text = ds.Tables[0].Rows[0]["stud_NonEarning"].ToString();
            fam_income.Text = ds.Tables[0].Rows[0]["stud_YearlyIncome"].ToString();

            if (ds.Tables[0].Rows[0]["year_of_adm"].ToString() != "")
            {
                drp_joining.SelectedIndex = drp_joining.Items.IndexOf(drp_joining.Items.FindByText(ds.Tables[0].Rows[0]["year_of_adm"].ToString()));
            }
            else
            {
                drp_joining.SelectedIndex = 0;
            }
            //if (ds.Tables[1].Rows[0]["Phy_handicap"].ToString() != "")
            //{
            //    phychk.Checked = true;
            //   // phyd.Visible = true;
            //    //DropDown_phy.Visible = true;
            //    DropDown_phy.Style.Add("display", "block");
            //    DropDown_phy.SelectedValue = ds.Tables[1].Rows[0]["Phy_handicap_Description"].ToString();
            //}
            //else
            //{
            //    phychk.Checked = false;
            //    //phyd.Visible = false;
            //    //DropDown_phy.Visible = false;
            //    DropDown_phy.Style.Add("display", "none");
            //}
        }
        cntFamily();
    }

    protected void btn1_Click(object sender, EventArgs e)
    {
        //tab1.Style.Add("padding", "10pt");
        //tab1.Style.Add("visibility", "hidden");

        //tab2.Style.Add("padding", "10pt");
        //tab2.Style.Add("visibility", "visible");
        bool institue = false;
        string script = "alert('Complete Cast Details');";
        string str = "select case when dte_insti is null then 'False' when dte_insti=1 then 'True' else 'False' end as dte_insti from d_adm_applicant where stud_id='" + Session["UserName"].ToString() + "'";
        DataSet dtchk = cls1.fill_dataset(str);
        if (dtchk.Tables.Count > 0)
        {
            if (dtchk.Tables[0].Rows.Count > 0)
            {
                if (!string.IsNullOrEmpty(dtchk.Tables[0].Rows[0]["dte_insti"].ToString()))
                {
                    if (dtchk.Tables[0].Rows[0]["dte_insti"].ToString() == "True")
                    {
                        institue = true;
                    }
                    else
                    {
                        institue = false;
                    }
                }
            }
        }

        Session["cast_new"] = ddlCategory.SelectedItem.Text;
        bool flagchk = false;
        if (txtaadhar.Text != "")
        {
            if (txtaadhar.Text.Length == 12)
            {
                if (institue == false)
                {
                    if (ddlCategory.SelectedItem.Text == "EBC" || ddlCategory.SelectedItem.Text == "SEBC" || ddlCategory.SelectedItem.Text == "EWS")
                    {
                        if (txtincno.Text == "")
                        {
                            flagchk = true;
                            script = "alert('Enter Income Certificate/Receipt No.');";
                        }
                        else if ((dincmonth.SelectedIndex == 0) || (dincdate.SelectedIndex == 0) || (dincyear.SelectedIndex == 0))
                        {
                            flagchk = true;
                            script = "alert('Select Income Certificate Date');";
                        }
                        else
                        {
                            flagchk = false;
                            Session["income_no"] = txtincno.Text;
                            Session["income_dt"] = dincdate.Text.ToString() + '-' + dincmonth.Text.ToString() + '-' + dincyear.Text.ToString();
                        }

                        if (txtmahaid.Text == "")
                        {
                            flagchk = true;
                            script = "alert('Enter Mahadbt ID');";
                            Session["Mahadbtid"] = "";
                            Session["Mahadbtpass"] = "";
                        }
                        else if (txtmahapass.Text == "")
                        {
                            flagchk = true;
                            script = "alert('Enter Mahadbt Password');";
                            Session["Mahadbtid"] = "";
                            Session["Mahadbtpass"] = "";
                        }
                        else
                        {
                            Session["Mahadbtid"] = txtmahaid.Text;
                            Session["Mahadbtpass"] = txtmahapass.Text;
                        }
                    }

                    if (ddlCategory.SelectedItem.Text != "OPEN" && ddlCategory.SelectedItem.Text != "EBC" && ddlCategory.SelectedItem.Text != "SEBC" && ddlCategory.SelectedItem.Text != "EWS" && ddlCategory.SelectedItem.Text != "TFWS")
                    {
                        if (txtcastno.Text == "")
                        {
                            flagchk = true;
                            script = "alert('Enter Cast Validity/Receipt No.');";
                        }
                        else if ((ddmonth.SelectedIndex == 0 || dddate.SelectedIndex == 0 || ddyear.SelectedIndex == 0))
                        {
                            flagchk = true;
                            script = "alert('Select Cast Validity Date');";
                        }
                        else if (txtincno.Text == "")
                        {
                            flagchk = true;
                            script = "alert('Enter Income Certificate/Receipt No.');";
                        }
                        else if ((dincmonth.SelectedIndex == 0 || dincdate.SelectedIndex == 0 || dincyear.SelectedIndex == 0))
                        {
                            flagchk = true;
                            script = "alert('Select Income Certificate Date');";
                        }
                        else
                        {
                            flagchk = false;
                            Session["cast_no"] = txtcastno.Text;
                            Session["cast_dt"] = dddate.Text.ToString() + '-' + ddmonth.Text.ToString() + '-' + ddyear.Text.ToString();
                            Session["income_no"] = txtincno.Text;
                            Session["income_dt"] = dincdate.Text.ToString() + '-' + dincmonth.Text.ToString() + '-' + dincyear.Text.ToString();
                        }

                        if (txtmahaid.Text == "")
                        {
                            flagchk = true;
                            script = "alert('Enter Mahadbt ID');";
                            Session["Mahadbtid"] = "";
                            Session["Mahadbtpass"] = "";
                        }
                        else if (txtmahapass.Text == "")
                        {
                            flagchk = true;
                            script = "alert('Enter Mahadbt Password');";
                            Session["Mahadbtid"] = "";
                            Session["Mahadbtpass"] = "";
                        }
                        else
                        {
                            Session["Mahadbtid"] = txtmahaid.Text;
                            Session["Mahadbtpass"] = txtmahapass.Text;
                        }
                    }
                    if (ddlCategory.SelectedItem.Text != "OPEN")
                    {
                        if (ddlCast.SelectedIndex == 0)
                        {
                            flagchk = true;
                            script = "alert('Select Cast');";
                        }
                    }



                    if (flagchk == false)
                    {
                        tab1.Visible = false;
                        tab2.Visible = true;
                        Session["Aadhar"] = txtaadhar.Text;
                    }
                    else
                    {
                        System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Test", script, true);
                    }
                }
                else
                {
                    if (txtcastno.Text == "" || txtincno.Text == "" || dincmonth.SelectedIndex == 0 || dincdate.SelectedIndex == 0 || dincyear.SelectedIndex == 0 || ddmonth.SelectedIndex == 0 || dddate.SelectedIndex == 0 || ddyear.SelectedIndex == 0)
                    {
                        Session["cast_no"] = "";
                        Session["cast_dt"] = "";
                        Session["income_no"] = "";
                        Session["income_dt"] = "";
                        Session["Mahadbtid"] = "";
                        Session["Mahadbtpass"] = "";
                    }
                    else
                    {
                        //flagchk = false;
                        Session["cast_no"] = txtcastno.Text;
                        Session["cast_dt"] = dddate.Text.ToString() + '-' + ddmonth.Text.ToString() + '-' + ddyear.Text.ToString();
                        Session["income_no"] = txtincno.Text;
                        Session["income_dt"] = dincdate.Text.ToString() + '-' + dincmonth.Text.ToString() + '-' + dincyear.Text.ToString();
                    }

                    if (txtincno.Text == "" || dincmonth.SelectedIndex == 0 || dincdate.SelectedIndex == 0 || dincyear.SelectedIndex == 0)
                    {

                    }
                    else
                    {

                        Session["income_no"] = txtincno.Text;
                        Session["income_dt"] = dincdate.Text.ToString() + '-' + dincmonth.Text.ToString() + '-' + dincyear.Text.ToString();
                    }

                    if (txtmahaid.Text == "" || txtmahapass.Text == "")
                    {
                        Session["Mahadbtid"] = "";
                        Session["Mahadbtpass"] = "";
                    }
                    else
                    {
                        Session["Mahadbtid"] = txtmahaid.Text;
                        Session["Mahadbtpass"] = txtmahapass.Text;
                    }
                    tab1.Visible = false;
                    tab2.Visible = true;
                    Session["Aadhar"] = txtaadhar.Text;
                }
            }
            else
            {
                script = "alert('Please Enter 12 Digit Aadhar Number.');";
                System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Test", script, true);
            }
        }
        else
        {
            Session["Aadhar"] = "";
            script = "alert('Please Enter Aadhar Number.');";
            System.Web.UI.ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Test", script, true);
        }

        if (ddlCategory.SelectedItem.Text == "OPEN" || ddlCategory.SelectedItem.Text == "TFWS")
        {
            Session["cast_no"] = "";
            Session["cast_dt"] = "";
            Session["income_no"] = "";
            Session["income_dt"] = "";
            if (txtmahaid.Text == "" || txtmahapass.Text == "")
            {
                Session["Mahadbtid"] = "";
                Session["Mahadbtpass"] = "";
            }
            else
            {
                Session["Mahadbtid"] = txtmahaid.Text;
                Session["Mahadbtpass"] = txtmahapass.Text;
            }
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        //tab2.Style.Add("padding", "10pt");
        //tab2.Style.Add("visibility", "hidden");

        //tab1.Style.Add("padding", "10pt");
        //tab1.Style.Add("visibility", "visible");
        if (Session["Aadhar"].ToString() != "")
        {
            if (ddlCategory.SelectedItem.Text != "OPEN" && ddlCategory.SelectedItem.Text != "EBC" && ddlCategory.SelectedItem.Text != "SEBC" && ddlCategory.SelectedItem.Text != "EWS" && ddlCategory.SelectedItem.Text != "TFWS")
            {
                castdiv.Visible = true;
                incomdiv.Visible = true;
                if (Session["cast_no"].ToString() != "" && Session["cast_dt"].ToString() != "" && Session["income_no"].ToString() != "" && Session["income_dt"].ToString() != "")
                {
                    txtcastno.Text = Session["cast_no"].ToString();
                    if (Session["cast_dt"].ToString().Contains("-"))
                    {
                        setcastdd(Session["cast_dt"].ToString());
                    }
                    else
                    {
                        ddmonth.SelectedIndex = 0;
                        dddate.SelectedIndex = 0;
                        ddyear.SelectedIndex = 0;
                    }

                    txtincno.Text = Session["income_no"].ToString();
                    if (Session["income_dt"].ToString().Contains("-"))
                    {
                        setincomedd(Session["income_dt"].ToString());
                    }
                    else
                    {
                        dincmonth.SelectedIndex = 0;
                        dincdate.SelectedIndex = 0;
                        dincyear.SelectedIndex = 0;
                    }
                }
                else
                {
                    txtcastno.Text = "";
                    txtincno.Text = "";
                    ddmonth.SelectedIndex = 0;
                    dddate.SelectedIndex = 0;
                    ddyear.SelectedIndex = 0;
                    dincmonth.SelectedIndex = 0;
                    dincdate.SelectedIndex = 0;
                    dincyear.SelectedIndex = 0;
                }
                if (Session["Mahadbtid"].ToString() != "" || Session["Mahadbtpass"].ToString() != "")
                {
                    txtmahaid.Text = Session["Mahadbtid"].ToString();
                    txtmahapass.Text = Session["Mahadbtpass"].ToString();
                }
            }
            else if (ddlCategory.SelectedItem.Text == "EBC" || ddlCategory.SelectedItem.Text == "SEBC" || ddlCategory.SelectedItem.Text == "EWS")
            {
                incomdiv.Visible = true;
                castdiv.Visible = false;
                if (Session["income_no"].ToString() != "" && Session["income_dt"].ToString() != "")
                {
                    txtincno.Text = Session["income_no"].ToString();
                    if (Session["income_dt"].ToString().Contains("-"))
                    {
                        setincomedd(Session["income_dt"].ToString());
                    }
                    else
                    {
                        dincmonth.SelectedIndex = 0;
                        dincdate.SelectedIndex = 0;
                        dincyear.SelectedIndex = 0;
                    }
                }
                else
                {
                    txtincno.Text = "";
                    dincmonth.SelectedIndex = 0;
                    dincdate.SelectedIndex = 0;
                    dincyear.SelectedIndex = 0;
                }

                if (Session["Mahadbtid"].ToString() != "" || Session["Mahadbtpass"].ToString() != "")
                {
                    txtmahaid.Text = Session["Mahadbtid"].ToString();
                    txtmahapass.Text = Session["Mahadbtpass"].ToString();
                }
            }
            else
            {
                incomdiv.Visible = false;
                castdiv.Visible = false;
                txtcastno.Text = "";
                txtincno.Text = "";
                ddmonth.SelectedIndex = 0;
                dddate.SelectedIndex = 0;
                ddyear.SelectedIndex = 0;
                dincmonth.SelectedIndex = 0;
                dincdate.SelectedIndex = 0;
                dincyear.SelectedIndex = 0;
            }
            txtaadhar.Text = Session["Aadhar"].ToString();
            tab1.Visible = true;
            tab2.Visible = false;


        }
    }

    public void setcastdd(string dob)
    {
        string[] arr;
        arr = dob.Split('-');

        if (arr[1].ToString().Contains("Day") || arr[1].ToString().Contains("Month") || arr[1].ToString().Contains("Year"))
        {
            ddmonth.SelectedIndex = 0;
            ddyear.SelectedIndex = 0;
            dddate.SelectedIndex = 0;
        }
        else
        {
            dddate.SelectedIndex = Convert.ToInt32(arr[0].Trim().ToString());
            if (arr[1].Trim().ToString().Length <= 1)
            {
                ddmonth.Text = "0" + arr[1].Trim().ToString();
            }
            else
            {
                ddmonth.SelectedValue = arr[1].Trim().ToString();
            }
            ddyear.SelectedIndex = ddyear.Items.IndexOf(new ListItem(arr[2].Trim().ToString()));
        }
    }

    public void setincomedd(string dob)
    {
        string[] arr;
        arr = dob.Split('-');

        if (arr[1].ToString().Contains("Day") || arr[1].ToString().Contains("Month") || arr[1].ToString().Contains("Year"))
        {
            dincdate.SelectedIndex = 0;
            dincmonth.SelectedIndex = 0;
            dincyear.SelectedIndex = 0;
        }
        else
        {
            dincdate.SelectedIndex = Convert.ToInt32(arr[0].Trim().ToString());
            if (arr[1].Trim().ToString().Length <= 1)
            {
                dincmonth.Text = "0" + arr[1].Trim().ToString();
            }
            else
            {
                dincmonth.SelectedValue = arr[1].Trim().ToString();
            }
            dincyear.SelectedIndex = ddyear.Items.IndexOf(new ListItem(arr[2].Trim().ToString()));
        }

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string aadharno = "";
            ds = ((DataSet)Session["stud_data"]);

            // sy_model data_cls = new sy_model();
            // data_cls.stud_id = Session["UserName"].ToString();
            Session["stud_Category"] = ds.Tables[0].Rows[0]["stud_Category"].ToString();

            Session["stud_Grno"] = ds.Tables[0].Rows[0]["stud_Grno"].ToString();
            Session["stud_PermanentAdd"] = address.Text.ToString();
            Session["stud_PermanentPhone"] = mob_no.Text;
            Session["stud_NativePhone"] = phn_no.Text;
            Session["yearofjoin"] = txt_year_of_joining.Text;
            Session["stud_Email"] = stud_Email.Text;
            Session["stud_Gender"] = ds.Tables[0].Rows[0]["stud_Gender"].ToString();
            Session["stud_MartialStatus"] = drp_mar.SelectedValue;
            if (DropDown_phy.SelectedIndex == 0)
            {
                Session["If_PHYSICALALLY_RESERVED"] = "";

            }
            else
            {
                Session["If_PHYSICALALLY_RESERVED"] = DropDown_phy.SelectedValue;
            }

            Session["stud_Father_Occupation"] = txtfatheroccupation.SelectedValue;
            Session["stud_Father_TelNo"] = father_mob.Text;
            Session["stud_Father_BusinessServiceAdd"] = fat_add.Text;
            Session["stud_Mother_Occupation"] = drp_motheroccu.SelectedValue;
            Session["stud_Mother_TelNo"] = mother_mob.Text;
            Session["stud_Mother_BusinessServiceAdd"] = mot_add.Text;
            Session["stud_NoOfFamilyMembers"] = person_fam.Text;
            Session["stud_Earning"] = earn.Text;
            Session["stud_NonEarning"] = NON_earn.Text;
            Session["stud_YearlyIncome"] = fam_income.Text;
            Session["propose_scholarship"] = drp_scholarship.SelectedValue.ToString();
            Session["member_of_ncc"] = drp_ncc.SelectedValue.ToString();
            Session["extra_activity"] = txt_activity.Text;

            cls1.update_data("update m_std_personaldetails_tbl set year_of_adm='" + drp_joining.Text.Replace("'", "''") + "',stud_Category='" + ddlCategory.SelectedItem.Text + "',stud_caste='" + ddlCast.SelectedItem.Text + "' where stud_id='" + Session["UserName"].ToString() + "'");
            string strcast = "select * from StudCast_tbl where Stud_id='" + Session["UserName"].ToString() + "'";
            DataSet dtcst = cls1.fill_dataset(strcast);
            if (dtcst.Tables[0].Rows.Count == 0)
            {
                cls1.update_data("insert into StudCast_tbl values('" + Session["UserName"].ToString() + "','" + Session["Aadhar"].ToString() + "','" + Session["cast_no"].ToString().Replace("'", "''") + "','" + Session["cast_dt"].ToString() + "','" + Session["income_no"].ToString().Replace("'", "''") + "','" + Session["income_dt"].ToString() + "','" + ddlCategory.SelectedItem.Text + "','" + Session["Mahadbtid"].ToString().Replace("'", "''") + "','" + Session["Mahadbtpass"].ToString().Replace("'", "''") + "','',getdate(),NULL,0)");
            }
            else
            {
                cls1.update_data("update StudCast_tbl set Aadhar_No='" + Session["Aadhar"].ToString() + "',	Cast_no='" + Session["cast_no"].ToString().Replace("'", "''") + "',cast_dt='" + Session["cast_dt"].ToString() + "',income_no='" + Session["income_no"].ToString().Replace("'", "''") + "'	,income_dt='" + Session["income_dt"].ToString() + "',extra1='" + ddlCategory.SelectedItem.Text + "',extra2='" + Session["Mahadbtid"].ToString().Replace("'", "''") + "',extra3='" + Session["Mahadbtpass"].ToString().Replace("'", "''") + "',mod_dt=getdate() where stud_id='" + Session["UserName"].ToString() + "'");
            }
            Response.Redirect("Apply_Course.aspx", false);
        }
        catch (Exception ae)
        {
            Response.Redirect("log_out.aspx", false);
        }
    }

    protected void phychk_CheckedChanged(object sender, EventArgs e)
    {

        if (phychk.Checked == true)
        {
            phychk.Checked = false;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(@"<script type='text/javascript'>");

            sb.Append("$('#myModal').modal('show');");

            sb.Append(@"</script>");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),

                       "ModalScript", sb.ToString(), false);
            // phyd.Visible = true;
        }
        else
        {
            //phyd.Text = "";
            //phyd.Visible = false;
            //DropDown_phy.Visible = false;
            DropDown_phy.Style.Add("display", "none");
            DropDown_phy.SelectedIndex = 0;
        }

    }
    protected void earn_TextChanged(object sender, EventArgs e)
    {
        cntFamily();
        //int earning, non_earning;


        //if (earn.Text != "" || NON_earn.Text != "")
        //{
        //    if (earn.Text == "")
        //    {
        //        earning = 0;
        //    }
        //    else
        //    {
        //        earning = Convert.ToInt32(earn.Text);
        //    }
        //    if (NON_earn.Text == "")
        //    {
        //        non_earning = 0;
        //    }
        //    else
        //    {
        //        non_earning = Convert.ToInt32(NON_earn.Text);
        //    }
        //    int total = earning + non_earning;
        //    person_fam.Text = Convert.ToString(total);
        //}
        tab1.Visible = false;
        tab2.Visible = true;
    }
    protected void NON_earn_TextChanged(object sender, EventArgs e)
    {
        cntFamily();
        //int earning, non_earning;


        //if (earn.Text != "" || NON_earn.Text != "")
        //{
        //    if (earn.Text == "")
        //    {
        //        earning = 0;
        //    }
        //    else
        //    {
        //        earning = Convert.ToInt32(earn.Text);
        //    }
        //    if (NON_earn.Text == "")
        //    {
        //        non_earning = 0;
        //    }
        //    else
        //    {
        //        non_earning = Convert.ToInt32(NON_earn.Text);
        //    }
        //    int total = earning + non_earning;
        //    person_fam.Text = Convert.ToString(total);
        //}
        //else
        //{
        //    person_fam.Text = "";
        //}
        tab1.Visible = false;
        tab2.Visible = true;
    }

    private void cntFamily()
    {
        int earning, non_earning;


        if (earn.Text != "" || NON_earn.Text != "")
        {
            if (earn.Text == "")
            {
                earning = 0;
            }
            else
            {
                earning = Convert.ToInt32(earn.Text);
            }
            if (NON_earn.Text == "")
            {
                non_earning = 0;
            }
            else
            {
                non_earning = Convert.ToInt32(NON_earn.Text);
            }
            int total = earning + non_earning;
            person_fam.Text = Convert.ToString(total);
        }
        else
        {
            person_fam.Text = "";
        }
    }




    protected void drp_scholarship_SelectedIndexChanged(object sender, EventArgs e)
    {
        tab1.Visible = false;
        tab2.Visible = true;
        if (drp_scholarship.SelectedIndex > 0)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            sb.Append(@"<script type='text/javascript'>");

            sb.Append("$('#modal_scholar').modal('show');");

            sb.Append(@"</script>");

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(),

                       "ModalScript", sb.ToString(), false);
        }
    }
    protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        drop_category(ddlCategory.Text.Trim());

        if (ddlCategory.SelectedIndex != 0)
        {
            if (ddlCategory.SelectedItem.Text == "OPEN")
            {
                castdiv.Visible = false;
                incomdiv.Visible = false;
            }
            else
            {
                if (ddlCategory.SelectedItem.Text == "EBC" || ddlCategory.SelectedItem.Text == "SEBC" || ddlCategory.SelectedItem.Text == "EWS")
                {
                    castdiv.Visible = false;
                    incomdiv.Visible = true;
                }
                else
                {
                    castdiv.Visible = true;
                    incomdiv.Visible = true;
                }
            }
        }
        else
        {
            castdiv.Visible = false;
            incomdiv.Visible = false;
        }
    }

    public void drop_category(string Category)
    {
        if (Category == "OPEN")
        {
            ddlCast.Enabled = false;
            ddlCast.SelectedIndex = 0;
        }
        else
        {
            ddlCast.Enabled = true;
            DataSet state_board_name = cls1.fill_dataset("select  child from dbo.State_category_details where Main = 'Reserved Category' and parent = '" + Category + "'");
            ddlCast.DataSource = state_board_name.Tables[0];

            ddlCast.DataTextField = "child";
            ddlCast.DataBind();
            ddlCast.Items.Insert(0, new ListItem("-- Select --", "0"));
            ddlCast.Items.Insert(1, new ListItem("Others", "Others"));
        }
    }
}