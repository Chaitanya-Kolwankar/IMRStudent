using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class MasterPage : System.Web.UI.MasterPage
{

    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da;
    DataSet ds;
    DataSet ds1;
    Class1 c1 = new Class1();
    new_Class2 c2 = new new_Class2();
    string s_img;

    String a;

    public void retrieve_image()
    {
        string s1 = "select a.stud_id,i.STUD_PHOTO,v.stud_l_name+' '+ v.stud_f_name+' '+v.stud_m_name as stud_name from m_std_studentacademic_tbl a left join studentImage i on i.stud_id=a.stud_id left join m_std_personaldetails_tbl v on   a.stud_id=v.stud_id  where  a.stud_id='" + Session["UserName"].ToString() + "' and a.del_flag=0 ";
        DataSet ds1 = c1.fill_dataset(s1);
        if (ds1.Tables[0].Rows[0]["STUD_PHOTO"] != DBNull.Value)
        {
            Byte[] img = (Byte[])ds1.Tables[0].Rows[0]["STUD_PHOTO"];
            s_img = Convert.ToBase64String(img);
            Session["image"] = s_img;
            Image1.ImageUrl = "data:image/png;base64," + s_img;
        }
        lblname.Text = ds1.Tables[0].Rows[0]["stud_name"].ToString();
        Session["name"] = ds1.Tables[0].Rows[0]["stud_name"].ToString();
    }

    public void fill_data()
    {
        try
        {
            string s1 = "select PARMANENT_ADDRESS,dbo.www_date_display_personal(stud_dob) as DOB,stud_dob,MARTIAL_STATUS,PARMANENT_PHONE,course_name,SUBCOURSE_NAME,Roll_no,division from www_student_personal_imr where student_id='" + Session["UserName"] + "'";
            ds1 = c1.fill_dataset(s1);
            //lbladdressfromuser.Text = ds1.Tables[0].Rows[0]["PARMANENT_ADDRESS"].ToString();
            lbldobfromuser.Text = "DOB :" + ds1.Tables[0].Rows[0]["DOB"].ToString();
            Session["DOB"] = ds1.Tables[0].Rows[0]["DOB"].ToString();
            Session["Datebirth"] = (DateTime)ds1.Tables[0].Rows[0]["stud_dob"];
            //lblmarriedfromuser.Text = ds1.Tables[0].Rows[0]["MARTIAL_STATUS"].ToString();
            //lblphonefromuser.Text = ds1.Tables[0].Rows[0]["PARMANENT_PHONE"].ToString();

            //lbldivfromuser.Text = ds1.Tables[0].Rows[0]["division"].ToString();
            //a = "0";
            //Session["a"] = a;
        }
        catch (Exception ex)
        {
            Response.Write("Error Occured While Processing....");
            a = "1";
        }

    }

    private void setRailwayVisibility()
    {
        string qryChk = "select stud_id from dbo.m_std_studentacademic_tbl where stud_id='" + Session["UserName"].ToString() + "' and del_flag=0 ";
        qryChk += " and ayid=(select ayid from dbo.m_academic where IsCurrent=1)";

        DataSet dsNew = c1.fill_dataset(qryChk);

        if (dsNew.Tables[0].Rows.Count > 0)
        {
            li_railway.Visible = true;
            Session["li_railway"] = true;
        }
        else
        {
            li_railway.Visible = false;
            Session["li_railway"] = false;
        }
        //REMOVE
        li_railway.Visible = true;
        Session["li_railway"] = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            try
            {
                //Session["image"] = "";
                if (Session["UserName"].ToString() == string.Empty)
                {

                    Response.Redirect("login.aspx");
                }
                else
                {
                    docupd.Attributes.Add("href", "documents.aspx?id=" + Session["Username"] + "&userid=" + Session["Username"] + "");
                    DataSet ds_show = new DataSet();

                    string stryearqry = "select Concat(SUBSTRING(Duration,9,4),'-',SUBSTRING(Duration,21,4)) as year from m_academic where IsCurrent=1";
                    DataTable dtyear = c1.fildatatable(stryearqry);
                    Session["acdyear"] = dtyear.Rows[0]["year"].ToString();
                    lblacdyear.Text = dtyear.Rows[0]["year"].ToString();
                    //            string qry_display = "select validity from cre_exam as a,Online_KT_form_validity as b where a.exam_code=b.exam_code and a.exam_code not in (select sem_id from cre_marks_tbl where stud_id='" + Session["UserName"].ToString() + "' "
                    //+ " and remark like 'UN%' ) and atkt_exam=1 and is_current=1 and branch_id='" + Session["course_id"].ToString() + "' and a.ayid=b.ayid and a.ayid=(select ayid from m_academic where IScurrent=1) and getdate()<validity "
                    //+ " order by validity desc";

                    //                   string qry_display = "select validity from cre_exam as a,Online_KT_form_validity as b where a.exam_code=b.exam_code and a.exam_code not in (select distinct exam_code from cre_marks_tbl"
                    //+ " where stud_id in (select stud_id from cre_stud_personaldetails where stud_F_name  in (select stud_L_Name+' '+stud_F_Name+' '+stud_M_Name+' '+stud_Mother_FName as stud_F_name"
                    // + " from viva_engg.dbo.m_std_personaldetails_tbl where stud_id='" + Session["UserName"].ToString() + "' )) and remark like 'UN%' ) and atkt_exam=1 and is_current=1 and branch_id='" + Session["course_id"].ToString() + "'"
                    // + " and a.ayid=b.ayid and a.ayid=(select ayid from m_academic where ayid='ayd0018') and getdate()<validity  order by validity desc";
                    //                   ds_show = c2.fill_dataset(qry_display);

                    //                   if (ds_show.Tables[0].Rows.Count > 0) // && Session["course_id"].ToString() != "CRS002"
                    //                   {
                    //                       ktform.Visible = true;
                    //                   }
                    //                   else
                    //                   {
                    //                       ktform.Visible = false;
                    //                   }

                    lblstudid.Text = "STUDENT ID:" + Session["UserName"].ToString();

                    //if (Session["a"] != "0")
                    //{
                    setRailwayVisibility();
                        retrieve_image();
                        fill_data();
                    if (Convert.ToBoolean(Session["admission"]) == true)
                    {
                        if (Convert.ToBoolean(Session["admission_done"]) == true)
                        {
                            li_admission.Visible = false;
                        }
                        else
                        {
                            string str = "select * from processing_fees where form_no='" + Session["Username"] + "' and ayid=(Select max(ayid) from m_academic where iscurrent='1') and postingf_code='Ok' and status like '%installment%'";
                            DataSet ds_check = c1.fill_dataset(str);
                            if (ds_check.Tables.Count > 0)
                            {
                                if (ds_check.Tables[0].Rows.Count == 0)
                                {
                                    li_admission.Visible = true;
                                }
                                else
                                {
                                    li_admission.Visible = false;
                                }
                            }
                            else
                            {
                                if (ds_check.Tables.Count == 0)
                                {
                                    li_admission.Visible = true;
                                }
                                else
                                {
                                    li_admission.Visible = false;
                                }
                            }

                          

                        }
                    }
                    else
                    {
                        li_admission.Visible = false;
                    }
                    //}
                    //else
                    {
                        lbldobfromuser.Text = Session["DOB"].ToString();

                        //Session["a"]Image1.ImageUrl = "data:image/png;base64," + Session["image"];
                        lblname.Text = Session["name"].ToString();

                    }
                    fee_pending();
                    string qry_lc = "select * from m_std_studentacademic_tbl where group_id in (select Group_id from m_crs_subjectgroup_tbl where Group_title like 'be%') and ayid in (select ayid from m_academic where iscurrent=1) and stud_id='" + Session["UserName"].ToString() + "'";
                    DataTable dt = c1.fildatatable(qry_lc);
                    if (dt.Rows.Count > 0)
                    {
                        stud_lc.Visible = true;
                    }
                    else
                    {
                        stud_lc.Visible = false;
                    }


                }
            }
            catch (Exception ex)
            {
                Response.Redirect("login.aspx");
            }
        }




    }
    public void fee_pending()
    {
        //        string str = "select stud_id,SUM(fee_pay),SUM(fee_amt) from (select distinct  a.stud_id,c.Struct_name,sum(b.Amount) as fee_pay,case when (select count(stud_id) from grant_freeshipscholarship where stud_id=a.stud_id and ayid=a.ayid and del_flag=0) >0 then 0 "
        //+" else c.Amount end fee_amt,a.group_id from m_std_studentacademic_tbl as a,m_feeentry as b,m_FeeMaster as c where a.stud_id=b.stud_id and "
        //+"c.group_id=(select SUBSTRING(group_title,1,2) from m_crs_subjectgroup_tbl where group_id=a.group_id) and b.Struct_name=c.Struct_name "
        //+ "and a.ayid=(select max(ayid)from m_academic where IsCurrent=1)  and a.ayid=b.Ayid and c.ayid=b.ayid and a.stud_id='" + Session["UserName"].ToString() + "' and a.del_flag=0 and "
        //+"a.del_flag=b.del_flag and b.del_flag=c.del_flag group by a.stud_id,a.ayid,a.group_id,c.Struct_name,c.Amount)  k  group by stud_id";


        //--Here removed the matching with the grant_freeshipscholarship (--Nitesh)
        string str = "select stud_id,SUM(fee_pay) from (select distinct  a.stud_id,c.Struct_name,sum(b.Amount) as fee_pay,a.group_id from m_std_studentacademic_tbl as a,m_feeentry as b,m_FeeMaster as c where a.stud_id=b.stud_id and c.group_id=(select SUBSTRING(group_title,1,2) from m_crs_subjectgroup_tbl where group_id=a.group_id) and b.Struct_name=c.Struct_name and a.ayid=(select max(ayid)from m_academic where IsCurrent=1)  and a.ayid=b.Ayid and c.ayid=b.ayid and a.stud_id='" + Session["UserName"].ToString() + "' and a.del_flag=0 and a.del_flag=b.del_flag and b.del_flag=c.del_flag group by a.stud_id,a.ayid,a.group_id,c.Struct_name,c.Amount)  k  group by stud_id";

//        string str = "select * from (select a.stud_id,sum(b.amount) as fee_pay,case when (select count(stud_id) from grant_freeshipscholarship where stud_id=a.stud_id and ayid=a.ayid and del_flag=0) >0"
//+ " then 0 else sum(c.Amount) end fee_amt from m_std_studentacademic_tbl as a,m_feeentry as b,m_FeeMaster as c where a.stud_id=b.stud_id and a.group_id=c.Group_id and a.ayid=(select max(ayid)from m_academic where IsCurrent=1) "
//+ " and a.ayid=c.Ayid and a.ayid=b.ayid and a.stud_id='" + Session["UserName"].ToString() + "' and a.del_flag=0 and a.del_flag=b.del_flag and b.del_flag=c.del_flag group by a.stud_id,a.ayid)  k where fee_amt <> fee_pay and fee_amt <> '0'";
        DataSet dsNew = c1.fill_dataset(str);
        //if (dsNew.Tables[0].Rows.Count > 0)
        //{
            fee_pen.Visible = true;
            //  Session["li_railway"] = true;
        //}
        //else
        //{
        //    fee_pen.Visible = false;
        //    // Session["li_railway"] = false;
        //}
    }
}
