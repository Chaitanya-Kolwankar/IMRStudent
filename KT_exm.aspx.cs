using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KT_exm : System.Web.UI.Page
{
    DataSet ds;
    DataTable dt;
    string stud_id = "", stud_name = "", stud_caste = "", stud_mobno = "", stud_email = "", stud_add = "";
    //string pattern_exm;
    Class1 cls = new Class1();
    new_Class2 cls2 = new new_Class2();
    protected void Page_Load(object sender, EventArgs e)
    {
        ds = new DataSet();
        string s1 = "select stud_F_Name,stud_M_Name,stud_L_Name,stud_Mother_FName,stud_Gender,stud_BloodGroup,dbo.www_date_display_personal(stud_DOB) as DOB,stud_Nationality,stud_BirthPlace,stud_DomiciledIn,stud_PermanentAdd,stud_PermanentPhone,stud_NativePhone,stud_Category,stud_Caste,stud_Religion,stud_MotherTounge,stud_MartialStatus,stud_Email from dbo.m_std_personaldetails_tbl where stud_id='" + Session["UserName"] + "';"
        + " select a.course_name,b.course_id,Group_title from www_student_personal_Vishal a,m_crs_course_tbl b where student_id='" + Session["UserName"] + "' and a.ayid=(select max(ayid) from www_student_personal_Vishal where student_id='" + Session["UserName"] + "')  and a.course_name=b.course_name ";
        ds = cls.fill_dataset(s1);

        if (ds.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                stud_id = Session["UserName"].ToString();
                stud_name = ds.Tables[0].Rows[i]["stud_L_Name"].ToString() + " " + ds.Tables[0].Rows[i]["stud_F_Name"].ToString() + " " + ds.Tables[0].Rows[i]["stud_M_Name"].ToString() + " " + ds.Tables[0].Rows[i]["stud_Mother_FName"].ToString();
                stud_caste = ds.Tables[0].Rows[i]["stud_Caste"].ToString();
                stud_mobno = ds.Tables[0].Rows[i]["stud_PermanentPhone"].ToString();
                stud_email = ds.Tables[0].Rows[i]["stud_Email"].ToString();
                stud_add = ds.Tables[0].Rows[i]["stud_PermanentAdd"].ToString();

                name_txt.Text = stud_name;
                caste_txt.Text = stud_caste;
                mobno_txt.Text = stud_mobno;
                emailid_txt.Text = stud_email;
                add_txt.Text = stud_add;

                Session["Student_Name"] = stud_name;
                Session["Student_Address"] = stud_add;
                Session["Student_Caste"] = stud_caste;
                Session["Student_Mobno"] = stud_mobno;
                Session["Student_Email"] = stud_email;

                drp_dwn_branch.SelectedItem.Value = ds.Tables[1].Rows[i]["course_id"].ToString();
                drp_dwn_branch.SelectedItem.Text = ds.Tables[1].Rows[i]["course_name"].ToString().Replace("\r\n", "");
                Session["Exam_form_branch"] = drp_dwn_branch.SelectedItem.Text;

                Session["course_name"] = ds.Tables[1].Rows[i]["course_name"].ToString();

            }
        }
    }
    protected void drp_dwn_sem_SelectedIndexChanged(object sender, EventArgs e)
    {
        ViewState["dt"] = null;
        btn_add.Enabled = false;
        grdfees.DataSource = null;
        grdfees.DataBind();
        GridView1.DataSource = null;
        GridView1.DataBind();
        string qry = "";


        if (drp_dwn_sem.SelectedValue == "Sem-1" || drp_dwn_sem.SelectedValue == "Sem-2" || drp_dwn_sem.SelectedValue == "Sem-7" || drp_dwn_sem.SelectedValue == "Sem-8")
        {
            qry = "select '--select--' as exam_code,'--select--' as exam_date union all select exam_code,exam_date from cre_exam where branch_id='" + drp_dwn_branch.SelectedValue + "' and atkt_exam=1 and exam_code not like 'r%' and is_current=1 and ayid in (select ayid from m_academic where IsCurrent = 1) and del_flag = 0";
            DataSet dsexm = new DataSet();
            dsexm = cls2.fill_dataset(qry);
            ddl_exam.DataTextField = dsexm.Tables[0].Columns["exam_date"].ToString();
            ddl_exam.DataValueField = dsexm.Tables[0].Columns["exam_code"].ToString();
            ddl_exam.DataSource = dsexm;
            ddl_exam.DataBind();
            university.Visible = false;
            drp_dwn_pattern.Enabled = true;
            DataSet ds = new DataSet();
            DataTable dt = ds.Tables.Add("Table 1");
            ds.Tables[0].Columns.Add("Subject_Name");
            ds.Tables[0].Columns.Add("theory_mrks");
            ds.Tables[0].Columns.Add("internal_mrks");
            ds.Tables[0].Columns.Add("term_wrks");
            ds.Tables[0].Columns.Add("pract_mrks");

            ViewState["dt"] = ds.Tables[0];
            this.data();
        }
        else
        {
            if (txtstud.Text != "")
            {
                qry = "select '--select--' as exam_code,'--select--' as exam_date union all select exam_code,exam_date from cre_exam where branch_id='" + drp_dwn_branch.SelectedValue + "' and atkt_exam=1 and exam_code not like 'r%' and is_current=1 and ayid in (select ayid from m_academic where IsCurrent = 1) and del_flag = 0";
                DataSet dsexm = new DataSet();
                dsexm = cls2.fill_dataset(qry);
                ddl_exam.DataTextField = dsexm.Tables[0].Columns["exam_date"].ToString();
                ddl_exam.DataValueField = dsexm.Tables[0].Columns["exam_code"].ToString();
                ddl_exam.DataSource = dsexm;
                ddl_exam.DataBind();

                string qry2 = "";
                qry2 = "select distinct pattern from cre_marks_tbl where stud_id='" + txtstud.Text + "' and sem_id='" + drp_dwn_sem.SelectedValue + "'";
                DataSet dspatr = cls2.fill_dataset(qry2);

                if (dspatr.Tables[0].Rows.Count > 0)
                {
                    if (dspatr.Tables[0].Rows[0]["pattern"].ToString() != "")
                    {
                        drp_dwn_pattern.ClearSelection();
                        drp_dwn_pattern.Items.FindByText(dspatr.Tables[0].Rows[0]["pattern"].ToString()).Selected = true;


                        Session["Pattern_kt_exm1"] = dspatr.Tables[0].Rows[0]["pattern"].ToString();
                    }
                }
                Session["prev_id"] = txtstud.Text;
                university.Visible = false;
                drp_dwn_pattern.Enabled = false;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Previous Result Student ID');", true);
                drp_dwn_sem.SelectedIndex = 0;
            }
        }


    }
    protected void drp_dwn_pattern_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    protected void print_btn_Click(object sender, EventArgs e)
    {
        string str11212 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and Status=('Exam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + drp_dwn_sem.SelectedValue + ":" + ddl_exam.SelectedValue + "') and (postingf_code='Ok' or postingf_code='S');select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
        DataSet ds11212 = cls.fill_dataset(str11212);
        if (ds11212.Tables[0].Rows.Count > 0)
        {
            Button1.Enabled = true;
            Payment.Enabled = false;
        }
        else
        {
            Button1.Enabled = false;
            Payment.Enabled = true;

        }
        if (drp_dwn_sem.SelectedValue == "Sem-1" || drp_dwn_sem.SelectedValue == "Sem-2" || drp_dwn_sem.SelectedValue == "Sem-7" || drp_dwn_sem.SelectedValue == "Sem-8")
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#fyModal').modal('show');</script>", false);


        // Response.Redirect("KT_exm_form.aspx");
    }

    protected void drp_dwn_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Exam_form_branch"] = "";
        //if (drp_dwn_branch.SelectedIndex>0)
        //{
        Session["Exam_form_branch"] = drp_dwn_branch.SelectedItem.Text;
        // }
    }
    protected void Payment_Click(object sender, EventArgs e)
    {
        string ins_str = "", subject = "";
        try
        {
            if (drp_dwn_sem.SelectedValue == "Sem-1" || drp_dwn_sem.SelectedValue == "Sem-2" || drp_dwn_sem.SelectedValue == "Sem-7" || drp_dwn_sem.SelectedValue == "Sem-8")
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                DataTable dt = ViewState["dt"] as DataTable;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["subject_name"].ToString().Trim() == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Subject name cannot be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        return;
                    }


                }
            }
            string str11212 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and Status=('Exam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + drp_dwn_sem.SelectedValue + ":" + ddl_exam.SelectedValue + "') and (postingf_code='Ok' or postingf_code='S') ;select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
            DataSet ds11212 = cls.fill_dataset(str11212);
            if (ds11212.Tables[0].Rows.Count == 0)
            {

                DataTable dt = new DataTable();
                dt.Columns.Add("subj_name");
                dt.Columns.Add("theory_mrks");
                dt.Columns.Add("internal_mrks");
                dt.Columns.Add("term_wrks");
                dt.Columns.Add("pract_mrks");


                int kh = 0;

                if (drp_dwn_sem.SelectedValue == "Sem-1" || drp_dwn_sem.SelectedValue == "Sem-2" || drp_dwn_sem.SelectedValue == "Sem-7" || drp_dwn_sem.SelectedValue == "Sem-8")
                {
                    for (int i = 0; i < grdfees.Rows.Count; i++)
                    {
                        string subject_code = "";


                        TextBox subj_name = grdfees.Rows[i].FindControl("txt_subject") as TextBox;
                        TextBox theory_mrks = grdfees.Rows[i].FindControl("txttheory") as TextBox;
                        TextBox internal_mrks = grdfees.Rows[i].FindControl("txtinternal") as TextBox;
                        TextBox term_wrks = grdfees.Rows[i].FindControl("txttermwork") as TextBox;
                        TextBox pract_mrks = grdfees.Rows[i].FindControl("txtpractical") as TextBox;

                        if (subject == "")
                        {
                            subject = subj_name.Text.Replace("'", "''");
                        }
                        else
                        {
                            subject = subject + "," + subj_name.Text.Replace("'", "''");
                        }
                        //CheckBox chk_bx = GridView1.Rows[i].FindControl("kt_chk") as CheckBox;
                        string str = "select * from kt_exam where stud_id='" + Session["UserName"].ToString() + "' and semester='" + drp_dwn_sem.SelectedItem.Text + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and exam_code='" + ds11212.Tables[1].Rows[0][0].ToString() + "' and  subject_name='" + subj_name.Text.Replace("'", "''") + "' ";
                        DataSet ds_new = cls.fill_dataset(str);

                        kh++;



                        if (ds_new.Tables[0].Rows.Count == 0)
                        {
                            ins_str = "insert into kt_exam values ('" + Session["UserName"].ToString() + "','" + subject_code + "','" + subj_name.Text.Replace("'", "''") + "','" + drp_dwn_sem.SelectedItem.Text + "','" + drp_dwn_sem.SelectedValue + "','" + ds11212.Tables[1].Rows[0][0].ToString() + "','" + drp_dwn_pattern.SelectedItem.Text + "',getdate(),null,0,'" + theory_mrks.Text + "','" + internal_mrks.Text + "','" + term_wrks.Text + "','" + pract_mrks.Text + "','',(select max(ayid) from m_academic where IsCurrent=1),null)";

                        }
                        else
                        {
                            ins_str = "update kt_exam set ESE='" + theory_mrks.Text + "',IA='" + internal_mrks.Text + "',TW='" + term_wrks.Text + "',[PR/OR]='" + pract_mrks.Text + "',mod_dt=getdate() where stud_id='" + Session["UserName"].ToString() + "' and semester='" + drp_dwn_sem.SelectedItem.Text + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from  m_academic where Iscurrent=1)  and exam_code='" + ddl_exam.SelectedValue + "' and  subject_name='" + subj_name.Text.Replace("'", "''") + "' ";
                        }
                        cls.DMLqueries(ins_str);
                        DataRow dr = dt.NewRow();

                        dr["subj_name"] = subj_name.Text;
                        dr["theory_mrks"] = theory_mrks.Text;
                        dr["internal_mrks"] = internal_mrks.Text;
                        dr["term_wrks"] = term_wrks.Text;
                        dr["pract_mrks"] = pract_mrks.Text;
                        dt.Rows.Add(dr);




                    }
                    //delete
                    string del_str = "";

                    del_str = "delete from kt_exam where stud_id='" + Session["UserName"].ToString() + "' and semester='" + drp_dwn_sem.SelectedItem.Text + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from  m_academic where Iscurrent=1)  and exam_code='" + ds11212.Tables[1].Rows[0][0].ToString() + "' and subject_name not in ('" + subject.Replace(",", "','") + "') ;"
                        + "delete from kt_exam_pay_details where stud_id='" + Session["UserName"].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and exm_date='" + ds11212.Tables[1].Rows[0][0].ToString() + "'  ";
                    cls.DMLqueries(del_str);
                }

                else
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        Label subject_code = GridView1.Rows[i].FindControl("subject_code") as Label;
                        Label subj_name = GridView1.Rows[i].FindControl("name_of_subj") as Label;
                        TextBox theory_mrks = GridView1.Rows[i].FindControl("theory_mrks") as TextBox;
                        TextBox internal_mrks = GridView1.Rows[i].FindControl("internal_mrks") as TextBox;
                        TextBox term_wrks = GridView1.Rows[i].FindControl("term_wrks") as TextBox;
                        TextBox pract_mrks = GridView1.Rows[i].FindControl("pract_mrks") as TextBox;
                        CheckBox chk_bx = GridView1.Rows[i].FindControl("kt_chk") as CheckBox;
                        string str = "select * from kt_exam where stud_id='" + Session["UserName"].ToString() + "' and semester='" + drp_dwn_sem.SelectedItem.Text + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and subject_id='" + subject_code.Text + "' and exam_code='" + ds11212.Tables[1].Rows[0][0].ToString() + "' ";
                        DataSet ds_new = cls.fill_dataset(str);
                        if (chk_bx.Checked == true)
                        {
                            // ins_str = "";
                            kh++;



                            if (ds_new.Tables[0].Rows.Count == 0)
                            {
                                ins_str = "insert into kt_exam values ('" + Session["UserName"].ToString() + "','" + subject_code.Text + "','" + subj_name.Text + "','" + drp_dwn_sem.SelectedItem.Text + "','" + drp_dwn_sem.SelectedValue + "','" + ds11212.Tables[1].Rows[0][0].ToString() + "','" + drp_dwn_pattern.SelectedItem.Text + "',getdate(),null,0,'" + theory_mrks.Text + "','" + internal_mrks.Text + "','" + term_wrks.Text + "','" + pract_mrks.Text + "','',(select max(ayid) from m_academic where IsCurrent=1),null)";

                            }
                            else
                            {
                                ins_str = "update kt_exam set ESE='" + theory_mrks.Text + "',IA='" + internal_mrks.Text + "',TW='" + term_wrks.Text + "',[PR/OR]='" + pract_mrks.Text + "',mod_dt=getdate() where stud_id='" + Session["UserName"].ToString() + "' and semester='" + drp_dwn_sem.SelectedItem.Text + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from  m_academic where Iscurrent=1) and subject_id='" + subject_code.Text + "' and exam_code='" + ds11212.Tables[1].Rows[0][0].ToString() + "' ";
                            }
                            cls.DMLqueries(ins_str);
                            DataRow dr = dt.NewRow();

                            dr["subj_name"] = subj_name.Text;
                            dr["theory_mrks"] = theory_mrks.Text;
                            dr["internal_mrks"] = internal_mrks.Text;
                            dr["term_wrks"] = term_wrks.Text;
                            dr["pract_mrks"] = pract_mrks.Text;
                            dt.Rows.Add(dr);
                        }
                        else
                        {



                            if (ds_new.Tables[0].Rows.Count > 0)
                            {
                                ins_str = "delete from kt_exam where stud_id='" + Session["UserName"].ToString() + "' and semester='" + drp_dwn_sem.SelectedItem.Text + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from  m_academic where Iscurrent=1) and subject_id='" + subject_code.Text + "' and exam_code='" + ds11212.Tables[1].Rows[0][0].ToString() + "' ;"
                                + "delete from kt_exam_pay_details where stud_id='" + Session["UserName"].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and exm_date='" + ds11212.Tables[1].Rows[0][0].ToString() + "' ";
                                cls.DMLqueries(ins_str);
                            }
                        }
                    }
                }

                DateTime futurDate = Convert.ToDateTime("2021-06-30 05:00");
                DateTime TodayDate = Convert.ToDateTime(DateTime.Now);
                var numberOfDays = (futurDate.Date - TodayDate.Date).TotalDays;
                TimeSpan fromH = TimeSpan.FromHours(DateTime.Now.Hour);
                TimeSpan toH = TimeSpan.FromHours(futurDate.TimeOfDay.Hours);
                TimeSpan fromm = TimeSpan.FromMinutes(DateTime.Now.Minute);
                TimeSpan tom = TimeSpan.FromMinutes(futurDate.TimeOfDay.Minutes);

                Session["KT_exm_grid"] = dt;
                int amount = 0;
                if (kh == 1)
                {
                    amount = 304;
                }
                else if (kh == 2)
                {
                    amount = 546;
                }
                else if (kh > 2)
                {
                    amount = 1154;
                }

                //500 late fee code commented on 29-10-21 by rohit
                //if (Convert.ToInt32(numberOfDays) <= 0 )
                //{
                //    if (Convert.ToInt32(numberOfDays) == 0 && DateTime.Now.TimeOfDay <= futurDate.TimeOfDay)
                //    {
                //        amount = amount + 500;
                //    }
                //    else
                //    {
                //        amount = amount + 500;
                //    }
                //}
                if (amount != 0)
                {
                    string name, group = "", category, year, group_id, ayid;
                    if (drp_dwn_sem.SelectedValue == "Sem-1" || drp_dwn_sem.SelectedValue == "Sem-2")
                    {
                        group = "FE ";
                    }
                    else if (drp_dwn_sem.SelectedValue == "Sem-3" || drp_dwn_sem.SelectedValue == "Sem-4")
                    {
                        group = "SE ";
                    }
                    else if (drp_dwn_sem.SelectedValue == "Sem-5" || drp_dwn_sem.SelectedValue == "Sem-6")
                    {
                        group = "TE ";
                    }
                    else if (drp_dwn_sem.SelectedValue == "Sem-7" || drp_dwn_sem.SelectedValue == "Sem-8")
                    {
                        group = "BE ";
                    }
                    DataSet payParameters = cls.fill_dataset("select upper(isnull(stud_L_Name,'')+' '+isnull(stud_F_Name,'')+' '+isnull(stud_M_Name,'')) as Name,stud_Category from m_std_personaldetails_tbl where stud_id='" + Session["Username"] + "' and del_flag=0; select ayid,(substring(Duration,9,4)+'-'+substring(Duration,21,4)) as Duration from m_academic where IsCurrent=1;");
                    group += drp_dwn_branch.SelectedItem.ToString().Replace("COMPUTER SCIENCE & ENGINEERING(ARTIFICIAL INTELLIGENCE AND MACHINE LEARNING)", "CSE (AI and ML)");
                    group_id = drp_dwn_branch.SelectedValue.ToString();
                    name = payParameters.Tables[0].Rows[0]["Name"].ToString();
                    category = payParameters.Tables[0].Rows[0]["stud_Category"].ToString();
                    year = payParameters.Tables[1].Rows[0]["Duration"].ToString();
                    ayid = payParameters.Tables[1].Rows[0]["ayid"].ToString();

                    string str112 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) ";
                    DataSet dt2 = cls.fill_dataset(str112);
                    string trans_id = "";
                    if (dt2.Tables[0].Rows.Count > 0) { trans_id = Session["UserName"].ToString() + (Convert.ToInt32(dt2.Tables[0].Rows.Count) + 1); }
                    else
                    { //trans_id = Session["UserName"].ToString() + '/' + ds.Tables[3].Rows[0]["to_year"].ToString().Substring(5, 2) + '-' + sum + "/1"; 
                        trans_id = Session["UserName"].ToString() + "1";
                    }
                    string str12 = "insert into processing_fees values('" + Session["UserName"].ToString() + "','','','','" + amount + "','','" + trans_id + "','','','','','','','','Exam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + drp_dwn_sem.SelectedValue + ":" + ddl_exam.SelectedValue + "',(select max(ayid) from m_academic where Iscurrent=1),getdate())";
                    cls.update_data(str12);

                    string customer_acc_no = Session["UserName"].ToString();
                    string ins_kt_new = "";
                    string str1 = "select * from kt_exam_pay_details where stud_id='" + Session["UserName"].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and exm_date='" + ds11212.Tables[1].Rows[0][0].ToString() + "' ";
                    DataSet ds_new1 = cls.fill_dataset(str1);
                    if (ds_new1.Tables[0].Rows.Count == 0)
                    {
                        ins_kt_new = "insert into kt_exam_pay_details values('" + Session["UserName"].ToString() + "','" + trans_id + "','" + drp_dwn_sem.SelectedValue + "','" + ds11212.Tables[1].Rows[0][0].ToString() + "','" + ddl_exam.SelectedValue + "','" + amount + "',(select max(ayid) from m_academic where Iscurrent=1),'" + Session["crs_id"] + "','',getdate(),null,0,'Unpaid','Admin','" + txtstud.Text + "')";
                        ins_kt_new = ins_kt_new + "; update kt_exam set receipt_no='" + trans_id + "',mod_dt=getdate() where stud_id='" + Session["UserName"].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from  m_academic where Iscurrent=1) and exam_code='" + ds11212.Tables[1].Rows[0][0].ToString() + "'";
                    }
                    else
                    {
                        ins_kt_new = "update kt_exam_pay_details set receipt_no='" + trans_id + "',amount='" + amount + "',course_id='" + Session["crs_id"] + "' where stud_id='" + Session["UserName"].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and exm_type='" + ddl_exam.SelectedValue + "' and preious_stud_id='" + txtstud.Text + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and exm_date='" + ds11212.Tables[1].Rows[0][0].ToString() + "' ";
                        ins_kt_new = ins_kt_new + "; update kt_exam set receipt_no='" + trans_id + "',mod_dt=getdate() where stud_id='" + Session["UserName"].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and exam_code='" + ds11212.Tables[1].Rows[0][0].ToString() + "'";

                    }
                    cls.update_data(ins_kt_new);
                    Response.Redirect("payment.aspx/" + amount + "/" + trans_id + "/" + customer_acc_no + "/Exam/" + name + "/" + group + "/" + category + "/" + year + "/" + group + "/" + ayid + "_" + drp_dwn_sem.SelectedValue);
                }
            }
        }
        catch (Exception ex)
        {
            cls.err_cls(ex.ToString());
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Something Went Wrong.Try again later', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("subj_name");
            dt.Columns.Add("theory_mrks");
            dt.Columns.Add("internal_mrks");
            dt.Columns.Add("term_wrks");
            dt.Columns.Add("pract_mrks");
            if (drp_dwn_sem.SelectedValue == "Sem-1" || drp_dwn_sem.SelectedValue == "Sem-2" || drp_dwn_sem.SelectedValue == "Sem-7" || drp_dwn_sem.SelectedValue == "Sem-8")
            {
                for (int i = 0; i < grdfees.Rows.Count; i++)
                {
                    CheckBox chk_bx = grdfees.Rows[i].FindControl("kt_chk") as CheckBox;
                    Label subject_code = grdfees.Rows[i].FindControl("subject_code") as Label;
                    Label subj_name = grdfees.Rows[i].FindControl("name_of_subj") as Label;
                    TextBox theory_mrks = grdfees.Rows[i].FindControl("theory_mrks") as TextBox;
                    TextBox internal_mrks = grdfees.Rows[i].FindControl("internal_mrks") as TextBox;
                    TextBox term_wrks = grdfees.Rows[i].FindControl("term_wrks") as TextBox;
                    TextBox pract_mrks = grdfees.Rows[i].FindControl("pract_mrks") as TextBox;
                    if (chk_bx.Checked == true)
                    {
                        DataRow dr = dt.NewRow();

                        dr["subj_name"] = subj_name.Text;
                        dr["theory_mrks"] = theory_mrks.Text;
                        dr["internal_mrks"] = internal_mrks.Text;
                        dr["term_wrks"] = term_wrks.Text;
                        dr["pract_mrks"] = pract_mrks.Text;
                        dt.Rows.Add(dr);
                    }
                }
            }
            else
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    CheckBox chk_bx = GridView1.Rows[i].FindControl("kt_chk") as CheckBox;
                    Label subject_code = GridView1.Rows[i].FindControl("subject_code") as Label;
                    Label subj_name = GridView1.Rows[i].FindControl("name_of_subj") as Label;
                    TextBox theory_mrks = GridView1.Rows[i].FindControl("theory_mrks") as TextBox;
                    TextBox internal_mrks = GridView1.Rows[i].FindControl("internal_mrks") as TextBox;
                    TextBox term_wrks = GridView1.Rows[i].FindControl("term_wrks") as TextBox;
                    TextBox pract_mrks = GridView1.Rows[i].FindControl("pract_mrks") as TextBox;
                    if (chk_bx.Checked == true)
                    {
                        DataRow dr = dt.NewRow();

                        dr["subj_name"] = subj_name.Text;
                        dr["theory_mrks"] = theory_mrks.Text;
                        dr["internal_mrks"] = internal_mrks.Text;
                        dr["term_wrks"] = term_wrks.Text;
                        dr["pract_mrks"] = pract_mrks.Text;
                        dt.Rows.Add(dr);
                    }
                }
            }

            Session["KT_exm_grid"] = dt;
            Session["stud_det"] = drp_dwn_sem.SelectedValue+":"+ddl_exam.SelectedValue;
            //  Response.Redirect("KT_exm_form.aspx");
            Response.Redirect("KT_exm_new.aspx");
        }
        catch (Exception ex)
        {

            cls.err_cls(ex.ToString());
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Something Went Wrong.Try again later', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
        }

    }

    protected void ddl_exam_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_exam.SelectedIndex > 0)
        {
            grdfees.DataSource = null;
            grdfees.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            Payment.Enabled = false;
            print_btn.Enabled = false;
            Session["exam_val"] = ddl_exam.SelectedItem.Text;

            DataSet ds1 = new DataSet();
            DataTable dt1 = new DataTable();
            dt1.Columns.Add("Sr_No");
            dt1.Columns.Add("Subject_name");
            dt1.Columns.Add("theory_mrks");
            dt1.Columns.Add("internal_mrks");
            dt1.Columns.Add("term_wrks");
            dt1.Columns.Add("pract_mrks");
            dt1.Columns.Add("subject_code");

            DataTable dtuniversity = new DataTable();
            dtuniversity.Columns.Add("Subject_name");
            dtuniversity.Columns.Add("theory_mrks");
            dtuniversity.Columns.Add("internal_mrks");
            dtuniversity.Columns.Add("term_wrks");
            dtuniversity.Columns.Add("pract_mrks");
            dtuniversity.Columns.Add("type");

            ds = new DataSet();

            string stud_id = Session["UserName"].ToString();
            string crs_name = Session["course_name"].ToString();

            Session["Semester"] = drp_dwn_sem.SelectedValue.ToString();
            //  Session["crs_id"] = ds.Tables[0].Rows[0]["course_id"].ToString();
            string course_qry = "select course_id from m_crs_course_tbl where course_name = '" + crs_name + "'";
            ds = cls.fill_dataset(course_qry);
            Session["crs_id"] = ds.Tables[0].Rows[0]["course_id"].ToString();

            if (drp_dwn_sem.SelectedValue == "Sem-1" || drp_dwn_sem.SelectedValue == "Sem-2" || drp_dwn_sem.SelectedValue == "Sem-7" || drp_dwn_sem.SelectedValue == "Sem-8")
            {
                university.Visible = true;

                grdfees.Enabled = true;
                btn_add.Enabled = true;
                string str11212 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and Status=('Exam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + drp_dwn_sem.SelectedValue + ":" + ddl_exam.SelectedValue + "') and postingf_code='ok';select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
                DataSet ds11212 = cls.fill_dataset(str11212);
                if (ds11212.Tables[0].Rows.Count > 0)
                {
                    grdfees.Enabled = false;
                    btn_add.Enabled = false;
                }
                else
                {

                    //string str_new = "select subject_name,ESE,IA,TW,[PR/OR],'' as type from kt_exam where stud_id='" + stud_id + "' and exam_code=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month) and curr_exam_code='" + drp_dwn_pattern.SelectedItem.Text + "' and ayid=(select MAX(ayid) from m_academic where IsCurrent='1') and   sem_id='" + drp_dwn_sem.SelectedValue + "'";

                    string str_new = "select subject_name,ESE,IA,TW,[PR/OR] from kt_exam a,kt_exam_pay_details b where a.stud_id='" + stud_id + "'  and exam_code=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month) and curr_exam_code='" + drp_dwn_pattern.SelectedItem.Text + "' and a.ayid=(select MAX(ayid) from m_academic where IsCurrent='1')  and   a.sem_id='" + drp_dwn_sem.SelectedValue + "' and a.ayid=b.ayid and a.stud_id=b.stud_id and a.sem_id=b.sem_id and b.exm_type='" + ddl_exam.SelectedValue + "'";


                    DataSet ds_new = cls.fill_dataset(str_new);
                    if (ds_new.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ds_new.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = dtuniversity.NewRow();
                            dr["Subject_name"] = ds_new.Tables[0].Rows[i]["subject_name"].ToString();
                            dr["theory_mrks"] = ds_new.Tables[0].Rows[i]["ESE"].ToString();
                            dr["internal_mrks"] = ds_new.Tables[0].Rows[i]["IA"].ToString();
                            dr["term_wrks"] = ds_new.Tables[0].Rows[i]["TW"].ToString();
                            dr["pract_mrks"] = ds_new.Tables[0].Rows[i]["PR/OR"].ToString();
                            dtuniversity.Rows.Add(dr);
                        }
                        print_btn.Enabled = true;
                    }
                    
                }
            }
            else
            {

                string qry_subj = "select * from dbo.cre_subject where branch_id='" + ds.Tables[0].Rows[0]["course_id"].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue.ToString() + "' and Pattern='" + drp_dwn_pattern.SelectedValue.ToString() + "'";
                ds1 = cls2.fill_dataset(qry_subj);
                bool flag = false;
                string str11212 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and Status=('Exam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + drp_dwn_sem.SelectedValue + ":" + ddl_exam.SelectedValue + "') and postingf_code='ok';select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
                DataSet ds11212 = cls.fill_dataset(str11212);
                if (ds11212.Tables[0].Rows.Count > 0)
                {
                    GridView1.Enabled = false;
                }
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    print_btn.Enabled = true;
                    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                    {
                        string str_new = "select ESE,IA,TW,[PR/OR] from kt_exam where stud_id='" + stud_id + "' and exam_code=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month) and curr_exam_code='" + drp_dwn_pattern.SelectedItem.Text + "' and ayid=(select MAX(ayid) from m_academic where IsCurrent='1') and subject_id='" + ds1.Tables[0].Rows[i]["subject_id"].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue + "'";
                        DataSet ds_new = cls.fill_dataset(str_new);

                        DataRow dr = dt1.NewRow();

                        if (ds_new.Tables[0].Rows.Count > 0)
                        {
                            dr["Sr_No"] = (i + 1).ToString();
                            dr["Subject_name"] = ds1.Tables[0].Rows[i]["subject_name"].ToString();
                            dr["theory_mrks"] = ds_new.Tables[0].Rows[i]["ESE"].ToString();
                            dr["internal_mrks"] = ds_new.Tables[0].Rows[i]["IA"].ToString();
                            dr["term_wrks"] = ds_new.Tables[0].Rows[i]["TW"].ToString();
                            dr["pract_mrks"] = ds_new.Tables[0].Rows[i]["PR/OR"].ToString();
                        }
                        else
                        {
                            //string str_new1 = "select ESE,IA,TW,[PR/OR] from kt_exam where stud_id='" + stud_id + "' and exam_code=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then "
                            //+" 'Nov' else 'Jun' end as month) and curr_exam_code='" + drp_dwn_pattern.SelectedItem.Text + "' and ayid=(select MAX(ayid) from m_academic where IsCurrent='1') and "
                            //   +"  subject_id='" + ds1.Tables[0].Rows[i]["subject_id"].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue + "'";

                            //    string str_new1 = "select distinct case when b.h1_type='ESE' then a.h1 else '' end as ESE ,case when b.h2_type='IA' then a.h2 else '' end as IA,case when b.h2_type='TW' "
                            //   + " then a.h2 else '' end as TW,case when b.h1_type='PR' or b.h1_type='OR' then a.h1 else '' end as PROR  from cre_marks_tbl a,cre_credit_tbl b where a.subject_id=b.subject_id and a.stud_id='" + txtstud.Text + "'";

                            string str_new1 = "select distinct case when b.h1_type='ESE' then a.h1 else '' end as ESE ,"
        + " case when b.h2_type='IA' then a.h2 else '' end as IA,case when b.h2_type='TW' then a.h2 else '' end as TW,"
        + " case when b.h1_type='PR' or b.h1_type='OR' then a.h1 else '' end as PROR  from cre_marks_tbl a,cre_credit_tbl b where a.credit_sub_id=b.credit_sub_id and a.subject_id=b.subject_id "
        + " and a.exam_code=(select distinct exam_code from dbo.cre_marks_tbl a where stud_id = '" + txtstud.Text + "' and a.sem_id = '" + drp_dwn_sem.SelectedItem.Value.ToString() + "' and curr_date = (select distinct max(curr_date)from dbo.cre_marks_tbl a where"
        + " stud_id = '" + txtstud.Text + "' and a.sem_id = '" + drp_dwn_sem.SelectedItem.Value.ToString() + "' and del_flag = 0 and remark is not null and remark <> '' and remark = 'Unsuccessful')) and remark = 'Unsuccessful' and stud_id='" + txtstud.Text + "' "
        + " and a.del_flag=0 and b.del_flag=0 and pattern='" + drp_dwn_pattern.SelectedItem.Value.ToString() + "'  and a.subject_id='" + ds1.Tables[0].Rows[i]["subject_id"].ToString() + "'";
                            DataSet ds_new1 = cls2.fill_dataset(str_new1);
                            if (ds_new1.Tables[0].Rows.Count > 0)
                            {
                                dr["Sr_No"] = (i + 1).ToString();
                                dr["Subject_name"] = ds1.Tables[0].Rows[i]["subject_name"].ToString();
                                dr["theory_mrks"] = ds_new1.Tables[0].Rows[0]["ESE"].ToString();
                                dr["internal_mrks"] = ds_new1.Tables[0].Rows[0]["IA"].ToString();
                                dr["term_wrks"] = ds_new1.Tables[0].Rows[0]["TW"].ToString();

                                dr["pract_mrks"] = ds_new1.Tables[0].Rows[0]["PROR"].ToString();
                                dt1.Rows.Add(dr);
                            }



                            //    dr["theory_mrks"] = "";
                            //    dr["internal_mrks"] = "";
                            //    dr["term_wrks"] = "";

                            //    dr["pract_mrks"] = "";
                            //}
                            //dr["subject_code"] = ds1.Tables[0].Rows[i]["subject_id"].ToString();

                        }
                    }
                }
                else
                {
                    print_btn.Enabled = true;
                }
            }

            if (drp_dwn_sem.SelectedValue == "Sem-1" || drp_dwn_sem.SelectedValue == "Sem-2" || drp_dwn_sem.SelectedValue == "Sem-7" || drp_dwn_sem.SelectedValue == "Sem-8")
            {
                Session["MyTable_KT_exm"] = dtuniversity;
                grdfees.DataSource = dtuniversity;
                grdfees.DataBind();
                ViewState["dt"] = dtuniversity;
            }
            else
            {
                Session["MyTable_KT_exm"] = dt1;
                GridView1.DataSource = dt1;
                GridView1.DataBind();
            }
        }
        else
        {
            grdfees.DataSource = null;
            grdfees.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
            btn_add.Enabled = false;
            print_btn.Enabled = false;

        }

    }
    protected void data()
    {
        try
        {
            DataTable dt = ViewState["dt"] as DataTable;
            grdfees.DataSource = dt;
            grdfees.DataBind();


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }//done
    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = ViewState["dt"] as DataTable;
            DataRow r = dt.NewRow();
            r["type"] = "insert";
            dt.Rows.Add(r);

            ViewState["dt"] = dt;
            this.data();
            GridView1.DataSource = null;
            GridView1.DataBind();
            print_btn.Enabled = true;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void grdfees_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void grdfees_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int index = Convert.ToInt32(e.RowIndex);
        DataTable dt = ViewState["dt"] as DataTable;
        if (dt.Rows.Count > 1)
        {
            dt.Rows[index].Delete();
            ViewState["dt"] = dt;
            this.data();
        }
        else
        {
            dt.Rows[index].Delete();
            ViewState["dt"] = dt;
            this.data();
            print_btn.Enabled = false;

        }
        GridView1.DataSource = null;
        GridView1.DataBind();
    }

    protected void txt_subject_TextChanged(object sender, EventArgs e)
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        try
        {
            bool action = false;
            TextBox txt = sender as TextBox;
            GridViewRow gvrow = txt.NamingContainer as GridViewRow;
            GridViewRow row1 = grdfees.Rows[gvrow.RowIndex];
            DataTable dt = ViewState["dt"] as DataTable;

            string subject = ((TextBox)grdfees.Rows[row1.RowIndex].FindControl("txt_subject")).Text;

            DataRow dr = dt.NewRow();
            if (subject.ToString().ToUpper() == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject name should not be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
                ((TextBox)grdfees.Rows[row1.RowIndex].FindControl("txt_subject")).Text = "";
                grdfees.Rows[row1.RowIndex].FindControl("txt_subject").Focus();
                action = true;
                return;
            }

            if (action == false)
            {
                dt.Rows[row1.RowIndex]["Subject_Name"] = subject.ToString();
                ViewState["dt"] = dt;
                this.data();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }

    }

    protected void txttheory_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            //bool action = false;
            TextBox txt = sender as TextBox;
            GridViewRow gvrow = txt.NamingContainer as GridViewRow;
            GridViewRow row1 = grdfees.Rows[gvrow.RowIndex];
            DataTable dt = ViewState["dt"] as DataTable;

            string theory = ((TextBox)grdfees.Rows[row1.RowIndex].FindControl("txttheory")).Text;

            DataRow dr = dt.NewRow();
            //if (theory.ToString().ToUpper() == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject name should not be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
            //    ((TextBox)grdfees.Rows[row1.RowIndex].FindControl("txttheory")).Text = "";
            //    grdfees.Rows[row1.RowIndex].FindControl("txttheory").Focus();
            //    action = true;
            //    return;
            //}

            //if (action == false)
            //{
            dt.Rows[row1.RowIndex]["theory_mrks"] = theory.ToString();
            ViewState["dt"] = dt;
            this.data();
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void txtinternal_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            //bool action = false;
            TextBox txt = sender as TextBox;
            GridViewRow gvrow = txt.NamingContainer as GridViewRow;
            GridViewRow row1 = grdfees.Rows[gvrow.RowIndex];
            DataTable dt = ViewState["dt"] as DataTable;

            string theory = ((TextBox)grdfees.Rows[row1.RowIndex].FindControl("txtinternal")).Text;

            DataRow dr = dt.NewRow();
            //if (theory.ToString().ToUpper() == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject name should not be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
            //    ((TextBox)grdfees.Rows[row1.RowIndex].FindControl("txtinternal")).Text = "";
            //    grdfees.Rows[row1.RowIndex].FindControl("txtinternal").Focus();
            //    action = true;
            //    return;
            //}

            //if (action == false)
            //{
            dt.Rows[row1.RowIndex]["internal_mrks"] = theory.ToString();
            ViewState["dt"] = dt;
            this.data();
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void txttermwork_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            //bool action = false;
            TextBox txt = sender as TextBox;
            GridViewRow gvrow = txt.NamingContainer as GridViewRow;
            GridViewRow row1 = grdfees.Rows[gvrow.RowIndex];
            DataTable dt = ViewState["dt"] as DataTable;

            string theory = ((TextBox)grdfees.Rows[row1.RowIndex].FindControl("txttermwork")).Text;

            DataRow dr = dt.NewRow();
            //if (theory.ToString().ToUpper() == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject name should not be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
            //    ((TextBox)grdfees.Rows[row1.RowIndex].FindControl("txttermwork")).Text = "";
            //    grdfees.Rows[row1.RowIndex].FindControl("txttermwork").Focus();
            //    action = true;
            //    return;
            //}

            //if (action == false)
            //{
            dt.Rows[row1.RowIndex]["term_wrks"] = theory.ToString();
            ViewState["dt"] = dt;
            this.data();
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void txtpractical_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            //bool action = false;
            TextBox txt = sender as TextBox;
            GridViewRow gvrow = txt.NamingContainer as GridViewRow;
            GridViewRow row1 = grdfees.Rows[gvrow.RowIndex];
            DataTable dt = ViewState["dt"] as DataTable;

            string theory = ((TextBox)grdfees.Rows[row1.RowIndex].FindControl("txtpractical")).Text;

            DataRow dr = dt.NewRow();
            //if (theory.ToString().ToUpper() == "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('Subject name should not be empty', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 });", true);
            //    ((TextBox)grdfees.Rows[row1.RowIndex].FindControl("txtpractical")).Text = "";
            //    grdfees.Rows[row1.RowIndex].FindControl("txtpractical").Focus();
            //    action = true;
            //    return;
            //}

            //if (action == false)
            //{
            dt.Rows[row1.RowIndex]["pract_mrks"] = theory.ToString();
            ViewState["dt"] = dt;
            this.data();
            //}
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}