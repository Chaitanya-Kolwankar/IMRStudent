using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Cache;
using System.IO;
using System.Text.RegularExpressions;

public partial class Announcement : System.Web.UI.Page
{

    Class1 c1_portal = new Class1();
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da;
    Class1 c1 = new Class1();
    DataSet ds;
    string fn;
    String to_year;
    string handicap;
    string contentType = "", fileName = "";
    Byte[] data;

   
    public String checkNull_phy(string s)
    {

        string stra = "select field_type ,value from dbo.www_m_std_personaldetails_tbl where field_type = '" + s + "' and stud_id = '" + Session["UserName"] + "'";
        DataSet dsnew = c1.fill_dataset(stra);
        if (dsnew.Tables[0].Rows.Count > 0)
        {
            return dsnew.Tables[0].Rows[0]["value"].ToString().Trim();
        }
        else
        {
            return "";
        }
    }

    public void profile_fill_data()
    {
        try
        {
            string s1 = "select stud_F_Name,stud_M_Name,stud_L_Name,stud_Mother_FName,stud_Gender,stud_Email,stud_BloodGroup,dbo.www_date_display_personal(stud_DOB) as DOB,stud_Nationality,stud_BirthPlace,stud_DomiciledIn,stud_PermanentAdd,stud_PermanentPhone,stud_NativePhone,stud_Category,stud_Caste,stud_Religion,stud_MotherTounge,stud_MartialStatus from dbo.m_std_personaldetails_tbl where stud_id='" + Session["UserName"] + "'";
            string login_qry = "select * from www_login where stud_id = '" + Session["UserName"] + "'";
            DataSet ds_login = c1.fill_dataset(login_qry);
            DataSet ds1 = c1.fill_dataset(s1);
            Session["ds_final"] = ds1;
            ds = ds1;
            string uname = checkNull("stud_MartialStatus");
            txtfirst.Text = checkNull("stud_F_Name");
            txtfather.Text = checkNull("stud_M_Name");
            txtsurname.Text = checkNull("stud_L_Name");
            txtmother.Text = checkNull("stud_Mother_FName");
            string studGen = checkNull("stud_Gender");
            if (studGen == "1" || studGen.ToUpper() == "MALE")
            {
                ddgender.SelectedValue = "MALE";
            }
            else
            {
                ddgender.SelectedValue = "FEMALE";
            }
            ddblood.SelectedValue = checkNull("stud_BloodGroup");
            txtnationality.Text = checkNull("stud_Nationality");
            txtbirth.Text = checkNull("stud_BirthPlace");
            txtdomiciled.Text = checkNull("stud_DomiciledIn");
            txtadddress.Text = checkNull("stud_PermanentAdd");
            txtmobno.Text = checkNull("stud_PermanentPhone");
            txttelno.Text = checkNull("stud_NativePhone");
            if (Session["Email_ID"].ToString() == string.Empty)
            {
                txtemail.Text = checkNull("stud_Email"); 
            }
            else
            {
                txtemail.Text = checkNull("stud_Email"); 
            }
            ddcategory.Text = checkNull("stud_Category");
            txtcaste.Text = checkNull("stud_Caste");
            txtreligion.Text = checkNull("stud_Religion");
            txtmothertongue.Text = checkNull("stud_MotherTounge");
            ddmarried.SelectedValue = checkNull("stud_MartialStatus");
            //if (checkNull("stud_MartialStatus") == "True")
            //{
            //    ddmarried.SelectedValue = "MARRIED";
            //}
            //else
            //{
            //    ddmarried.SelectedValue = "UNMARRIED";
            //}
            //retrieve_image();
            // Image1.ImageUrl = "data:image/png;base64," + Session["image"];
            txtdate.Text = ds1.Tables[0].Rows[0]["DOB"].ToString();

            string s11 = checkNull_phy("If PHYSICALALLY RESERVED");
            if (s11 != "")
            {
                chkhandicap.Checked = true;

                if (s11 == "Visually Impaired" || s11 == "Speech" || s11 == "Hearing Impaired" || s11 == "Orthopedic Disorder" || s11 == "Mentally Retarded")
                {
                    panhandicap.Visible = true;
                    ddhandicap.Visible = true;
                    ddhandicap.SelectedValue = s11.Trim();
                }
                else
                {
                    panhandicap.Visible = true;
                    ddhandicap.Visible = true;
                    txtphysicalspecify.Visible = true;
                    ddhandicap.SelectedValue = "Others";
                    txtphysicalspecify.Text = s11;
                }

            }
            else
            {
                chkhandicap.Checked = false;
            }


        }
        catch (Exception ex)
        {
            string msg = "Error Code: 444 Main Profile";
            Response.Redirect("ErrorPage.aspx?x=" + msg + "", true);
        }




    }

    public void date_check_Comaparision()
    {
        //string s3 = "select dbo.www_date(stud_DOB) as stud_dob,mothers_name from www_login where  stud_id='" + Session["UserName"] + "'";
        string s3 = "select stud_DOB as stud_dob,mothers_name from www_login where  stud_id='" + Session["UserName"] + "'";
        c1.Conn();
        DataSet ds3 = c1.fill_dataset(s3);
        Session["date_check"] = ds3.Tables[0].Rows[0]["stud_dob"].ToString();
    }

    public void deassign()
    {
        //txtfirst.ReadOnly = false;
        //txtmother.ReadOnly = false;
        //txtfather.ReadOnly = false;
        //txtsurname.ReadOnly = false;
        chkhandicap.Enabled = true;
        ddgender.Enabled = true;
        ddblood.Enabled = true;
        //txtdate.Visible = false;
        //DatePicker1.Visible = true;
        txtnationality.ReadOnly = false;
        txtbirth.ReadOnly = false;
        txtdomiciled.ReadOnly = false;
        txtadddress.ReadOnly = false;
        txtmobno.ReadOnly = false;
        //txtemail.ReadOnly = false;
        //ddcategory.Enabled = true;
        txtcaste.ReadOnly = false;
        txtreligion.ReadOnly = false;
        txtmothertongue.ReadOnly = false;
        ddmarried.Enabled = true;
    }

    public String checkNull(string s)
    {

        string stra = "select field_type ,value from dbo.www_m_std_personaldetails_tbl where field_type = '" + s + "' and stud_id = '" + Session["UserName"] + "'";
        DataSet dsnew = c1.fill_dataset(stra);
        if (dsnew.Tables[0].Rows.Count > 0)
        {
            return dsnew.Tables[0].Rows[0]["value"].ToString();
        }
        else
        {

            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][s] == DBNull.Value)
                {
                    return "";
                }
                else
                {
                    return ds.Tables[0].Rows[0][s].ToString();
                }
            }
            else
            {
                return "";
            }

        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["UserName"].ToString() == string.Empty)
                {
                    Response.Redirect("login.aspx");
                }
                else
                {
                    //Session["admission_done"] = false;
                    //if (Convert.ToBoolean(Session["admission"]) == true)
                    //{
                    //    DataSet dsChk = c1.fill_dataset("select ayid from m_std_studentacademic_tbl where ayid=(select MAX(ayid) from dbo.m_academic) AND stud_id='" + Session["UserName"].ToString() + "'");
                    //    if (dsChk.Tables[0].Rows.Count > 0)
                    //    {
                    //        Session["admission_done"] = true;
                    //        div_admission.Visible = false;
                    //    }
                    //    else
                    //    {
                    //        div_admission.Visible = true;
                    //    }
                    //}
                    //else
                    //{
                    //    div_admission.Visible = false;
                    //}

                    lblstudid.Text = Session["UserName"].ToString();
                    ddmarried.SelectedValue = "UNMARRIED";
                    date_check_Comaparision();
                    Session["date_check"].ToString();
                    Session["Image_flag"] = "false";
                    // DatePicker1.Visible = false;
                    // lbldateformat.Visible = false;
                    if (Session["UserName"].ToString() == string.Empty)
                    {
                        Response.Redirect("Login.aspx");
                    }
                    else
                    {
                        deassign();
                        profile_fill_data();
                        //shr fill_grid();

                        if (Session["disp"].ToString() == "1")
                        {
                            butsubmit.Visible = false;
                        }
                        else
                        {
                            //butsubmit.Visible = true;
                        }

                    }
                    fill_data();
                    //notice();
                    //cls_announce();

                }
            }
            catch (Exception ex)
            {
                Response.Redirect("log_out.aspx");
            }
        }
    }

    public void fill_data()
    {

        //www_student_personal_imr is replaced to www_student_personal_vishal
        try
        {
            string s1 = "select PARMANENT_ADDRESS,dbo.www_date_display_personal(stud_dob) as DOB,stud_dob,MARTIAL_STATUS,PARMANENT_PHONE,course_name,Group_title,Roll_no,division from www_student_personal_imr where student_id='" + Session["UserName"] + "' and ayid=(select max(ayid) from www_student_personal_imr where student_id='" + Session["UserName"] + "') ";
            DataSet ds1 = c1.fill_dataset(s1);
            //lbladdressfromuser.Text = ds1.Tables[0].Rows[0]["PARMANENT_ADDRESS"].ToString();

            Session["DOB"] = ds1.Tables[0].Rows[0]["DOB"].ToString();
            Session["Datebirth"] = (DateTime)ds1.Tables[0].Rows[0]["stud_dob"];
            //lblmarriedfromuser.Text = ds1.Tables[0].Rows[0]["MARTIAL_STATUS"].ToString();
            //lblphonefromuser.Text = ds1.Tables[0].Rows[0]["PARMANENT_PHONE"].ToString();
            lblcoursefromuser.Text = ds1.Tables[0].Rows[0]["course_name"].ToString();
            lblsubcoursefromuser.Text = ds1.Tables[0].Rows[0]["Group_title"].ToString();
            Session["Group_title"] = ds1.Tables[0].Rows[0]["Group_title"].ToString();
            //lbldivfromuser.Text = ds1.Tables[0].Rows[0]["division"].ToString();
            lblrollfromuser.Text = ds1.Tables[0].Rows[0]["Roll_no"].ToString();
            Session["course_name"] = ds1.Tables[0].Rows[0]["course_name"].ToString();

        }
        catch (Exception ex)
        {
            Response.Write("Error Occured While Processing.");
        }

    }
    protected void chkhandicap_CheckedChanged(object sender, EventArgs e)
    {
        if (chkhandicap.Checked == true)
        {
            panhandicap.Visible = true;
        }


        if (chkhandicap.Checked == false)
        {
            panhandicap.Visible = false;
        }
    }
    protected void ddhandicap_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddhandicap.SelectedValue.ToString() == "Others")
        {
            //lblspecify.Visible = true;
            txtphysicalspecify.Visible = true;
        }
        else
        {
            //lblspecify.Visible = false;
            txtphysicalspecify.Visible = false;
        }
    }

    public string ret_string(string s1)
    {
        //string gender = string.Empty;
        string str_qry = "insert into  [www_m_std_personaldetails_tbl_temp] values('" + Session["UserName"] + "'";
        DataSet ds_before_insert = (DataSet)Session["ds_final"];


        if (s1 == "stud_Gender")
        {
            string gender = string.Empty;
            string gen_int = ds_before_insert.Tables[0].Rows[0]["stud_Gender"].ToString();
            if (gen_int == "1")
            {
                gender = "MALE";
            }
            else if (gen_int == "0")
            {
                gender = "FEMALE";
            }

            if (gender == ddgender.SelectedValue)
            {
                str_qry = string.Empty;

            }
            else
            {
                str_qry = ddgender.SelectedValue;

            }

            return str_qry;
        }
        if (s1 == "stud_BloodGroup")
        {
            string blood = ds_before_insert.Tables[0].Rows[0]["stud_BloodGroup"].ToString();
            if (blood == ddblood.SelectedValue)
            {
                str_qry = string.Empty;
            }

            else
            {
                str_qry = ddblood.SelectedValue;
            }

            return str_qry;
        }

        if (s1 == "stud_F_Name")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == txtfirst.Text.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txtfirst.Text.ToUpper().ToString();
            }
            return str_qry;
        }


        //if (s1 == "stud_DOB")
        //{

        //    if (DatePicker1.SelectedDate.ToShortDateString() == Session["date_check"].ToString())
        //    {
        //        str_qry = String.Empty;

        //    }
        //    else
        //    {
        //        str_qry = Convert.ToString(DatePicker1.SelectedDate);
        //    }
        //    return str_qry;
        //}


        if (s1 == "stud_M_Name")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == txtfather.Text.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txtfather.Text.ToUpper().ToString();
            }
            return str_qry;
        }
        if (s1 == "stud_L_Name")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == txtsurname.Text.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txtsurname.Text.ToUpper().ToString();
            }
            return str_qry;
        }
        if (s1 == "stud_Mother_FName")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString().ToUpper() == txtmother.Text.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txtmother.Text.ToUpper().ToString();
            }
            return str_qry;
        }

        if (s1 == "stud_Nationality")
        {
            string nat = ds_before_insert.Tables[0].Rows[0][s1].ToString();
            if (nat.ToUpper().ToString() == txtnationality.Text.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txtnationality.Text.ToUpper().ToString();
            }
            return str_qry;
        }

        if (s1 == "stud_BirthPlace")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == txtbirth.Text.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txtbirth.Text.ToUpper().ToString();
            }
            return str_qry;
        }

        if (s1 == "stud_DomiciledIn")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == txtdomiciled.Text.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txtdomiciled.Text.ToUpper().ToString();
            }
            return str_qry;
        }

        if (s1 == "stud_PermanentAdd")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == txtadddress.Text.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txtadddress.Text.ToUpper().ToString();
            }
            return str_qry;
        }
        if (s1 == "stud_PermanentPhone")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == txtmobno.Text.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txtmobno.Text.ToUpper().ToString();
            }
            return str_qry;
        }

        if (s1 == "stud_NativePhone")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == txttelno.Text.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txttelno.Text.ToUpper().ToString();
            }
            return str_qry;
        }
        if (s1 == "stud_Category")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == ddcategory.SelectedValue.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = ddcategory.SelectedValue.ToUpper().ToString();
            }
            return str_qry;
        }

        if (s1 == "stud_Caste")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == txtcaste.Text.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txtcaste.Text.ToUpper().ToString();
            }
            return str_qry;
        }
        if (s1 == "stud_Religion")
        {
            string nat = ds_before_insert.Tables[0].Rows[0][s1].ToString();
            if (nat.ToUpper().ToString() == txtreligion.Text.Trim().ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txtreligion.Text.ToUpper().ToString();
            }
            return str_qry;
        }
        if (s1 == "stud_MotherTounge")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == txtmothertongue.Text.ToUpper().ToString())
            {
                str_qry = String.Empty;

            }
            else
            {
                str_qry = txtmothertongue.Text.ToUpper().ToString();
            }
            return str_qry;
        }

        if (s1 == "stud_MartialStatus")
        {
            if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == "True")
            {
                str_qry = "MARRIED";

            }
            else if (ds_before_insert.Tables[0].Rows[0][s1].ToString() == "False")
            {
                str_qry = "UNMARRIED";
            }
            if (str_qry != ddmarried.SelectedItem.Text)
            {
                str_qry = ddmarried.SelectedItem.Text;

            }
            else
            {
                str_qry = String.Empty;
            }

            return str_qry;
        }
        return str_qry;
    }

    public string insert_data()
    {



        int gen;
        string final_query = string.Empty;
        string stud_F_Name = ret_string("stud_F_Name");
        if (stud_F_Name != string.Empty)
        {
            string new_str_dsp = firstname.Text.Substring(0, firstname.Text.LastIndexOf(':'));
            if (final_query.ToString() == string.Empty)
            {
                final_query = insert_data_store(new_str_dsp, stud_F_Name, "stud_F_Name", 32);
            }
            else
            {
                final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_F_Name, "stud_F_Name", 32);
            }


        }

        string stud_M_Name = ret_string("stud_M_Name");
        if (stud_M_Name != string.Empty)
        {
            string new_str_dsp = SURNAME.Text.Substring(0, SURNAME.Text.LastIndexOf(':'));
            if (final_query.ToString() == string.Empty)
            {
                final_query = insert_data_store(new_str_dsp, stud_M_Name, "stud_M_Name", 32);
            }
            else
            {
                final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_M_Name, "stud_M_Name", 32);
            }

        }

        string stud_L_Name = ret_string("stud_L_Name");
        if (stud_L_Name != string.Empty)
        {
            string new_str_dsp = LASTNAME.Text.Substring(0, LASTNAME.Text.LastIndexOf(':'));
            if (final_query.ToString() == string.Empty)
            {
                final_query = insert_data_store(new_str_dsp, stud_L_Name, "stud_L_Name", 32);
            }
            else
            {
                final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_L_Name, "stud_L_Name", 32);
            }

        }

        string stud_Mother_FName = ret_string("stud_Mother_FName");
        if (stud_Mother_FName != string.Empty)
        {
            string new_str_dsp = mothername.Text.Substring(0, mothername.Text.LastIndexOf(':'));
            if (final_query.ToString() == string.Empty)
            {
                final_query = insert_data_store(new_str_dsp, stud_Mother_FName, "stud_Mother_FName", 32);
            }
            else
            {
                final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_Mother_FName, "stud_Mother_FName", 32);
            }


        }
        //code by shraddha...........

        string stra = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_Gender' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(stra);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_Gender")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + ddgender.Text.ToString() + "' where field_type = 'stud_Gender' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {
            string stud_Gender = ret_string("stud_Gender");
            if (stud_Gender != string.Empty)
            {
                string new_str_dsp = gender.Text.Substring(0, gender.Text.LastIndexOf(':'));
                if (stud_Gender == "MALE")
                {
                    gen = 1;
                }
                else
                {
                    gen = 0;
                }

                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_Gender, "stud_Gender", 32); ;
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_Gender, "stud_Gender", 32); ;
                }


            }

        }


        string strb = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_BloodGroup' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(strb);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_BloodGroup")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + ddblood.SelectedItem.Text.ToString() + "' where field_type = 'stud_BloodGroup' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {
            string stud_BloodGroup = ret_string("stud_BloodGroup");
            if (stud_BloodGroup != string.Empty)
            {
                string new_str_dsp = blood.Text.Substring(0, blood.Text.LastIndexOf(':'));
                if (stud_BloodGroup == "A -ve")
                {
                    gen = 0;
                }
                else if (stud_BloodGroup == "A +ve")
                {
                    gen = 1;
                }
                else if (stud_BloodGroup == "B +ve")
                {
                    gen = 5;
                }
                else if (stud_BloodGroup == "B -ve")
                {
                    gen = 4;
                }
                else if (stud_BloodGroup == "AB +ve")
                {
                    gen = 3;
                }
                else if (stud_BloodGroup == "AB -ve")
                {
                    gen = 2;
                }
                else if (stud_BloodGroup == "O -ve")
                {
                    gen = 6;
                }
                else if (stud_BloodGroup == "O +ve")
                {
                    gen = 7;
                }
                else
                {
                    gen = 32;
                    stud_BloodGroup = null;
                }

                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_BloodGroup, "stud_BloodGroup", 32);
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_BloodGroup, "stud_BloodGroup", 32);
                }

            }
        }


        string strc = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_Nationality' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(strc);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_Nationality")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + txtnationality.Text.ToString() + "' where field_type = 'stud_Nationality' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {
            string stud_Nationality = ret_string("stud_Nationality");
            if (stud_Nationality != string.Empty)
            {
                string new_str_dsp = lblnat.Text.Substring(0, lblnat.Text.LastIndexOf(':'));
                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_Nationality, "stud_Nationality", 32);
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_Nationality, "stud_Nationality", 32);
                }

            }
        }

        string strd = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_BirthPlace' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(strd);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_BirthPlace")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + txtbirth.Text.ToString() + "' where field_type = 'stud_BirthPlace' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {

            string stud_BirthPlace = ret_string("stud_BirthPlace");
            if (stud_BirthPlace != string.Empty)
            {
                string new_str_dsp = lblday.Text.Substring(0, lblday.Text.LastIndexOf(':'));
                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_BirthPlace, "stud_BirthPlace", 32);
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_BirthPlace, "stud_BirthPlace", 32);
                }

            }
        }


        string stre = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_DomiciledIn' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(stre);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_DomiciledIn")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + txtdomiciled.Text.ToString() + "' where field_type = 'stud_DomiciledIn' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {



            string stud_DomiciledIn = ret_string("stud_DomiciledIn");
            if (stud_DomiciledIn != string.Empty)
            {
                string new_str_dsp = lbldomicile.Text.Substring(0, lbldomicile.Text.LastIndexOf(':'));
                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_DomiciledIn, "stud_DomiciledIn", 32);
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_DomiciledIn, "stud_DomiciledIn", 32);
                }
            }
        }



        string strf = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_PermanentAdd' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(strf);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_PermanentAdd")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + txtadddress.Text.ToString() + "' where field_type = 'stud_PermanentAdd' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {
            string stud_PermanentAdd = ret_string("stud_PermanentAdd");
            if (stud_PermanentAdd != string.Empty)
            {
                string new_str_dsp = lbladdress.Text.Substring(0, lbladdress.Text.LastIndexOf(':'));
                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_PermanentAdd, "stud_PermanentAdd", 32);
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_PermanentAdd, "stud_PermanentAdd", 32);
                }



            }
        }

        string strg = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_PermanentPhone' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(strg);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_PermanentPhone")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + txtmobno.Text.ToString() + "' where field_type = 'stud_PermanentPhone' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {

            string stud_PermanentPhone = ret_string("stud_PermanentPhone");
            if (stud_PermanentPhone != string.Empty)
            {
                string new_str_dsp = lblmobile.Text.Substring(0, lblmobile.Text.LastIndexOf(':'));
                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_PermanentPhone, "stud_PermanentPhone", 32);
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_PermanentPhone, "stud_PermanentPhone", 32);
                }


            }
        }

        string strh = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_NativePhone' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(strh);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_NativePhone")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + txttelno.Text.ToString() + "' where field_type = 'stud_NativePhone' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {

            string stud_NativePhone = ret_string("stud_NativePhone");
            if (stud_NativePhone != string.Empty)
            {
                string new_str_dsp = lbltelno.Text.Substring(0, lbltelno.Text.LastIndexOf(':'));
                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_NativePhone, "stud_NativePhone", 32);
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_NativePhone, "stud_NativePhone", 32);
                }


            }

        }

        string stri = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_Category' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(stri);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_Category")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + ddcategory.SelectedItem.Text.ToString() + "' where field_type = 'stud_Category' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {

            string stud_Category = ret_string("stud_Category");
            if (stud_Category != string.Empty)
            {
                string new_str_dsp = lblcategory.Text.Substring(0, lblcategory.Text.LastIndexOf(':'));
                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_Category, "stud_Category", 32);
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_Category, "stud_Category", 32);
                }

            }
        }


        string strj = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_Caste' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(strj);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_Caste")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + txtcaste.Text.ToString() + "' where field_type = 'stud_Caste' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {
            string stud_Caste = ret_string("stud_Caste");
            if (stud_Caste != string.Empty)
            {
                string new_str_dsp = lblcaste.Text.Substring(0, lblcaste.Text.LastIndexOf(':'));
                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_Caste, "stud_Caste", 32);
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_Caste, "stud_Caste", 32);
                }


            }
        }


        string strk = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_Religion' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(strk);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_Religion")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + txtreligion.Text.ToString() + "' where field_type = 'stud_Religion' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {
            string stud_Religion = ret_string("stud_Religion");
            if (stud_Religion != string.Empty)
            {
                string new_str_dsp = lblreligion.Text.Substring(0, lblreligion.Text.LastIndexOf(':'));
                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_Religion, "stud_Religion", 32);
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_Religion, "stud_Religion", 32);
                }


            }
        }

        string strl = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_MotherTounge' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(strl);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_MotherTounge")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + txtmothertongue.Text.ToString() + "' where field_type = 'stud_MotherTounge' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {
            string stud_MotherTounge = ret_string("stud_MotherTounge");
            if (stud_MotherTounge != string.Empty)
            {
                string new_str_dsp = lblmothertongue.Text.Substring(0, lblmothertongue.Text.LastIndexOf(':'));
                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_MotherTounge, "stud_MotherTounge", 32);
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_MotherTounge, "stud_MotherTounge", 32);
                }


            }
        }


        string strm = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_MartialStatus' and stud_id = '" + Session["UserName"] + "'";
        ds = c1.fill_dataset(strm);
        if (ds.Tables[0].Rows.Count > 0)
        {

            string str = ds.Tables[0].Rows[0][0].ToString();

            if (str == "stud_MartialStatus")
            {
                string strupdate = "update www_m_std_personaldetails_tbl set value='" + ddmarried.SelectedItem.Text.ToString() + "' where field_type = 'stud_MartialStatus' and stud_id = '" + Session["UserName"] + "'";
                c1.update_data(strupdate);

            }
        }
        else
        {
            string stud_MartialStatus = ret_string("stud_MartialStatus");
            if (stud_MartialStatus != string.Empty)
            {
                string new_str_dsp = lblmartial.Text.Substring(0, lblmartial.Text.LastIndexOf(':'));
                if (stud_MartialStatus == "UNMARRIED")
                {
                    gen = 0;
                }
                else if (stud_MartialStatus == "MARRIED")
                {
                    gen = 1;
                }
                else
                {
                    gen = 32;
                    stud_MartialStatus = null;
                }

                if (final_query.ToString() == string.Empty)
                {
                    final_query = insert_data_store(new_str_dsp, stud_MartialStatus, "stud_MartialStatus", 32);
                }
                else
                {
                    final_query = final_query + ";" + insert_data_store(new_str_dsp, stud_MartialStatus, "stud_MartialStatus", 32);
                }

            }

        }

        return final_query;


    }

    public string insert_data_store(string fielddisplay, string s1, string fieldtype, int a)
    {

        string s;
        if (a != 32)
        {
            s = "insert into www_m_std_personaldetails_tbl values('" + Session["UserName"].ToString() + "','" + fielddisplay + "','" + fieldtype + "'," + a + ",null,0,getdate(),getdate(),'" + to_year + "')";
            return s;
        }
        else
        {

            s = "insert into www_m_std_personaldetails_tbl values('" + Session["UserName"].ToString() + "','" + fielddisplay + "','" + fieldtype + "','" + s1 + "',null,0,getdate(),getdate(),'" + to_year + "')";
            return s;

        }

    }

    protected void btnsubmit_Click1(object sender, EventArgs e)
    {
        try
        {
            if (txtadddress.Text.Trim().ToString() == "" || txtmobno.Text.Trim().ToString() == "" || (txtcaste.Text.Trim().ToString() == "" && txtreligion.Text.Trim().ToString() == ""))
            {
                ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please fill all Mandatory fields')", true);
                //div_valid.InnerText = "Please fill all Mandatory fields";
            }
            else
            {
                Session["Address"] = txtadddress.Text;
                if (chkhandicap.Checked == true)
                {
                    string stra = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'If PHYSICALALLY RESERVED' and stud_id = '" + Session["UserName"] + "'";
                    ds = c1.fill_dataset(stra);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        string str = ds.Tables[0].Rows[0][0].ToString();
                        if (str == "If PHYSICALALLY RESERVED")
                        {
                            string strupdate;
                            if (ddhandicap.SelectedItem.Text.ToString() == "Others")
                            {
                                strupdate = "update www_m_std_personaldetails_tbl set value='" + txtphysicalspecify.Text.ToString() + "' where field_type = 'If PHYSICALALLY RESERVED' and stud_id = '" + Session["UserName"] + "'";
                            }
                            else
                            {
                                strupdate = "update www_m_std_personaldetails_tbl set value='" + ddhandicap.SelectedItem.Text.ToString() + "' where field_type = 'If PHYSICALALLY RESERVED' and stud_id = '" + Session["UserName"] + "'";
                            }

                            c1.update_data(strupdate);

                            string final_qry = insert_data();
                            string message = execute_data("datafield", final_qry, 32);

                            if (message == "TRANSACTION SUCCESSFUL")
                            {
                                String str12 = string.Empty;
                                str12 = "Data Saved Successfully..";
                                lblmainerror.Visible = true;
                                lblmainerror.InnerText = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + str12 + "'" + ")</script>";
                                Page.Controls.Add(lblmainerror);
                                //lblmainerror.Text = "Data Updated Successfully....";
                            }
                            //shr    fill_grid();
                            Response.Redirect("otherinfo.aspx?x=a", false);
                        }
                    }
                    else
                    {
                        if (ddhandicap.SelectedValue.ToString() != "--Select--")
                        {
                            if (ddhandicap.SelectedValue.ToString() == "Others")
                            {
                                handicap = txtphysicalspecify.Text.Trim().ToString();
                            }
                            else
                            {
                                handicap = ddhandicap.SelectedItem.Text.ToString();
                            }

                            string final_qry = insert_data();
                            final_qry = final_qry + "insert into www_m_std_personaldetails_tbl values('" + Session["UserName"].ToString() + "','If PHYSICALALLY RESERVED','If PHYSICALALLY RESERVED','" + handicap + "',null,0,getdate(),getdate(),'" + to_year + "')";
                            string message = execute_data("datafield", final_qry, 32);
                            if (message == "TRANSACTION SUCCESSFUL")
                            {
                                String str = string.Empty;
                                str = "Data Saved Successfully..";
                                lblmainerror.Visible = true;
                                lblmainerror.InnerText = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + str + "'" + ")</script>";
                                Page.Controls.Add(lblmainerror);
                                lblmainerror.InnerText = "Data Updated Successfully....";
                            }
                            txtdate.Visible = true;
                            //shr  fill_grid();
                            // shraddha
                            Response.Redirect("otherinfo.aspx?x=a", false);
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Data Saved Successfully..')", true);
                        }

                        else
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "", "alert('select type for handicap')", true);
                        }
                    }

                }


                else
                {
                    //handicap = "NO";

                    string stremail = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'stud_Email' and stud_id = '" + Session["UserName"] + "'";
                    ds = c1.fill_dataset(stremail);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        //string str = ds.Tables[0].Rows[0][0].ToString();
                        //string strdelete = "delete from www_m_std_personaldetails_tbl where field_type = 'If PHYSICALALLY RESERVED' and stud_id = '" + Session["UserName"] + "'";
                        //c1.delete_data(strdelete);

                    }
                    else
                    {

                        string insert = "insert into www_m_std_personaldetails_tbl values ('" + Session["UserName"] + "','EMAIL_ID:','stud_Email','" + txtemail.Text.ToString().Trim() + "',null,0,getdate(),getdate(),'" + to_year + "')";
                        c1.Conn();
                        //if (con.State == ConnectionState.Closed)
                        //{
                        //    con.Open();
                        //}
                        //else
                        //{
                        //    con.Close();
                        //    con.Open();
                        //}
                        cmd.Connection = c1.con;
                        cmd.CommandText = insert;
                        cmd.CommandType = CommandType.Text;

                        cmd.ExecuteNonQuery();
                        c1.con_close();

                    }

                    string stra = "select field_type from dbo.www_m_std_personaldetails_tbl where field_type = 'If PHYSICALALLY RESERVED' and stud_id = '" + Session["UserName"] + "'";
                    ds = c1.fill_dataset(stra);
                    if (ds.Tables[0].Rows.Count > 0)
                    {

                        string str = ds.Tables[0].Rows[0][0].ToString();
                        string strdelete = "delete from www_m_std_personaldetails_tbl where field_type = 'If PHYSICALALLY RESERVED' and stud_id = '" + Session["UserName"] + "'";
                        c1.delete_data(strdelete);

                    }

                    string final_qry = insert_data();
                    //final_qry = final_qry + "insert into www_m_std_personaldetails_tbl values('" + Session["UserName"].ToString() + "','If PHYSICALALLY RESERVED','If PHYSICALALLY RESERVED','" + handicap + "',null,0,getdate(),getdate(),'" + to_year + "')";

                    string message = execute_data("datafield", final_qry, 32);
                    if (message == "TRANSACTION SUCCESSFUL")
                    {
                        String str = string.Empty;
                        str = "Data Saved Successfully..";
                        lblmainerror.Visible = true;
                        lblmainerror.InnerText = "<script language='javascript'>" + Environment.NewLine + "window.alert(" + "'" + str + "'" + ")</script>";
                        Page.Controls.Add(lblmainerror);
                        lblmainerror.InnerText = "Data Updated Successfully....";
                    }

                    txtdate.Visible = true;
                    //shr   fill_grid();
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Data Saved Successfully..')", true);
                    Response.Redirect("otherinfo.aspx?x=a", false);

                }
            }
        }
        catch
        {
            //string msg = "Error Code: 555 Main Profile";
            //Response.Redirect("ErrorPage.aspx?x=" + msg + "", true);
            Response.Redirect("log_out.aspx",true);
        }
    }

    public string execute_data(string fieldtype, string s1, int a)
    {

        cmd = new SqlCommand();

        if (a == 1 || a == 0)
        {
            c1.Conn();
            cmd.Connection = c1.con;

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insert_www_m_std_personaldetails_tbl";
            cmd.Parameters.AddWithValue("@stud_id", Session["UserName"].ToString());
            cmd.Parameters.AddWithValue("@field_type", fieldtype);
            cmd.Parameters.AddWithValue("@value", a);
            string message = cmd.ExecuteScalar().ToString();
            return message;
        }
        else
        {
            c1.Conn();
            //if (con.State == ConnectionState.Closed)
            //{
            //    con.Open();
            //}
            //else
            //{
            //    con.Close();
            //    con.Open();
            //}
            cmd.Connection = c1.con;


            ///////////befor bulk insert
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandText = "insert_www_m_std_personaldetails_tbl";
            //cmd.Parameters.AddWithValue("@stud_id", Session["UserName"].ToString());
            //cmd.Parameters.AddWithValue("@field_type", fieldtype);
            //cmd.Parameters.AddWithValue("@value", s1);
            //cmd.ExecuteNonQuery();
            //con.Close();
            ///////////befor bulk insert

            ////////////////////////////for bulk insert
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insert_www_m_std_personaldetails_tbl_bulk";
            //string s = "insert into www_m_std_personaldetails_tbl values('" + Session["UserName"].ToString() + "','" + fieldtype + "','" + s1 + "',null,0,getdate(),getdate())";
            string q_type = "insert";
            cmd.Parameters.AddWithValue("@type", q_type);
            cmd.Parameters.AddWithValue("@ins_query", s1);
            string message = cmd.ExecuteScalar().ToString();
            c1.con_close();
            return message;
        }

    }
}