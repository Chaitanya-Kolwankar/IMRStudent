using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class KT_ExamNew : System.Web.UI.Page
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
                drp_dwn_branch.SelectedItem.Text = ds.Tables[1].Rows[i]["course_name"].ToString();
                Session["Exam_form_branch"] = drp_dwn_branch.SelectedItem.Text;

            }
        }
      //  SetInitialRow();
    }

    private void SetInitialRow()
    {
        DataTable dt = new DataTable();
        DataRow dr = null;
        dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
        dt.Columns.Add(new DataColumn("Sr_No", typeof(string)));
        dt.Columns.Add(new DataColumn("Subject_name", typeof(string)));
        dt.Columns.Add(new DataColumn("theory_mrks", typeof(string)));
        dt.Columns.Add(new DataColumn("internal_mrks", typeof(string)));
        dt.Columns.Add(new DataColumn("term_wrks", typeof(string)));
        dt.Columns.Add(new DataColumn("pract_mrks", typeof(string)));
        dr = dt.NewRow();
        dr["Sr_No"] = 1;
        dr["Subject_name"] = string.Empty;
        dr["theory_mrks"] = string.Empty;
        dr["internal_mrks"] = string.Empty;
        dr["term_wrks"] = string.Empty;
        dr["pract_mrks"] = string.Empty;
        dt.Rows.Add(dr);
        //dr = dt.NewRow();

        //Store the DataTable in ViewState
        ViewState["CurrentTable"] = dt;

        Gridview2.DataSource = dt;
        Gridview2.DataBind();
    }


    protected void drp_dwn_branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Exam_form_branch"] = "";
        //if (drp_dwn_branch.SelectedIndex>0)
        //{
        Session["Exam_form_branch"] = drp_dwn_branch.SelectedItem.Text;
        // }
    }

    protected void Gridview2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    protected void drp_dwn_sem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (txtstud.Text != "")
        {
        //string qry = "";

        //qry = "select '--select--' as exam_code,'--select--' as exam_date union all select exam_code,exam_date from cre_exam where branch_id='" + drp_dwn_branch.SelectedValue + "' and atkt_exam=1 and exam_code not like 'r%' and is_current=1 and ayid in (select ayid from m_academic where IsCurrent = 1) and del_flag = 0";
        //DataSet dsexm = new DataSet();
        //dsexm = cls2.fill_dataset(qry);
        //ddl_exam.DataTextField = dsexm.Tables[0].Columns["exam_date"].ToString();
        //ddl_exam.DataValueField = dsexm.Tables[0].Columns["exam_code"].ToString();
        //ddl_exam.DataSource = dsexm;
        //ddl_exam.DataBind();
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('Please Enter Student ID');", true);
           // drp_dwn_sem.SelectedIndex = 0;
        }
    }

    protected void ddl_exam_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["exam_val"] = ddl_exam.SelectedItem.Text;

        DataSet ds1 = new DataSet();
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("Sr_No");
        dt1.Columns.Add("Subject_name");
        dt1.Columns.Add("theory_mrks");
        dt1.Columns.Add("internal_mrks");
        dt1.Columns.Add("term_wrks");
        dt1.Columns.Add("pract_mrks");

        ds = new DataSet();
        string stud_id = Session["UserName"].ToString();
        string crs_name = Session["course_name"].ToString();

        Session["Semester"] = drp_dwn_sem.SelectedValue.ToString();

        string str11212 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and Status=('Exam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + drp_dwn_sem.SelectedValue + "') and postingf_code='ok';select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
        DataSet ds11212 = cls.fill_dataset(str11212);
        if (ds11212.Tables[0].Rows.Count > 0)
        {
            Gridview2.Enabled = false;
        }

        string str_new = "select ESE,IA,TW,[PR/OR],subject_name from kt_exam where stud_id='" + stud_id + "' and exam_code=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month) and curr_exam_code='" + drp_dwn_pattern.SelectedItem.Text + "' and ayid=(select MAX(ayid) from m_academic where IsCurrent='1') and sem_id='" + drp_dwn_sem.SelectedValue + "'";
        DataSet ds_new = cls.fill_dataset(str_new);

        if (ds_new.Tables[0].Rows.Count > 0)
        {
            print_btn.Enabled = true;
            for (int i = 0; i < ds_new.Tables[0].Rows.Count; i++)
            {
                DataRow dr = dt1.NewRow();
                dr["Sr_No"] = (i + 1).ToString();
                dr["Subject_name"] = ds_new.Tables[0].Rows[i]["subject_name"].ToString();
                dr["theory_mrks"] = ds_new.Tables[0].Rows[i]["ESE"].ToString();
                dr["internal_mrks"] = ds_new.Tables[0].Rows[i]["IA"].ToString();
                dr["term_wrks"] = ds_new.Tables[0].Rows[i]["TW"].ToString();
                dr["pract_mrks"] = ds_new.Tables[0].Rows[i]["PR/OR"].ToString();
                dt1.Rows.Add(dr);
            }
            Session["MyTable_KT_exm"] = dt1;
            ViewState["CurrentTable"] = dt1;


            Gridview2.DataSource = dt1;
            Gridview2.DataBind();

            for (int j = 0; j < Gridview2.Rows.Count; j++)
            {
                TextBox box1 = (TextBox)Gridview2.Rows[j].Cells[1].FindControl("txtsubname");
                if (box1.Text != "")
                {
                    box1.Enabled = false;
                }
            }
        }
        else
        {
            print_btn.Enabled = true;
            SetInitialRow();
        }
    }
    protected void ButtonAdd_Click(object sender, EventArgs e)
    {
        AddNewRowToGrid();  
    }

    public void AddNewRowToGrid()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            if (dtCurrentTable.Rows.Count > 0)
            {
                for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                {
                    //extract the TextBox values  
                    TextBox box1 = (TextBox)Gridview2.Rows[rowIndex].Cells[1].FindControl("txtsubname");
                    TextBox box2 = (TextBox)Gridview2.Rows[rowIndex].Cells[2].FindControl("txttheory");
                    TextBox box3 = (TextBox)Gridview2.Rows[rowIndex].Cells[3].FindControl("txtinternal");
                    TextBox box4 = (TextBox)Gridview2.Rows[rowIndex].Cells[1].FindControl("txtterm");
                    TextBox box5 = (TextBox)Gridview2.Rows[rowIndex].Cells[2].FindControl("txtpractical");
                    
                    drCurrentRow = dtCurrentTable.NewRow();
                    drCurrentRow["Sr_No"] = i + 1;
                    dtCurrentTable.Rows[i - 1]["Subject_name"] = box1.Text;
                    dtCurrentTable.Rows[i - 1]["theory_mrks"] = box2.Text;
                    dtCurrentTable.Rows[i - 1]["internal_mrks"] = box3.Text;
                    dtCurrentTable.Rows[i - 1]["term_wrks"] = box4.Text;
                    dtCurrentTable.Rows[i - 1]["pract_mrks"] = box5.Text;
                    rowIndex++;
                }
                dtCurrentTable.Rows.Add(drCurrentRow);
                ViewState["CurrentTable"] = dtCurrentTable;
                Gridview2.DataSource = dtCurrentTable;
                Gridview2.DataBind();
            }
        }
        else
        {
            //Response.Write("ViewState is null");
        }
        //Set Previous Data on Postbacks  
        SetPreviousData();
    }

    private void SetPreviousData()
    {
        int rowIndex = 0;
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    TextBox box1 = (TextBox)Gridview2.Rows[rowIndex].Cells[1].FindControl("txtsubname");
                    TextBox box2 = (TextBox)Gridview2.Rows[rowIndex].Cells[2].FindControl("txttheory");
                    TextBox box3 = (TextBox)Gridview2.Rows[rowIndex].Cells[3].FindControl("txtinternal");
                    TextBox box4 = (TextBox)Gridview2.Rows[rowIndex].Cells[1].FindControl("txtterm");
                    TextBox box5 = (TextBox)Gridview2.Rows[rowIndex].Cells[2].FindControl("txtpractical");
                    box1.Text = dt.Rows[i]["Subject_name"].ToString();
                    box2.Text = dt.Rows[i]["theory_mrks"].ToString();
                    box3.Text = dt.Rows[i]["internal_mrks"].ToString();
                    box4.Text = dt.Rows[i]["term_wrks"].ToString();
                    box5.Text = dt.Rows[i]["pract_mrks"].ToString();

                    rowIndex++;
                }
            }
        }
    }

    protected void Gridview2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (ViewState["CurrentTable"] != null)
        {
            DataTable dt = (DataTable)ViewState["CurrentTable"];
            DataRow drCurrentRow = null;
            int rowIndex = Convert.ToInt32(e.RowIndex);
            string str11212 = "select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
            DataSet ds11212 = cls.fill_dataset(str11212);
            if (dt.Rows.Count > 1)
            {
                TextBox box1 = (TextBox)Gridview2.Rows[rowIndex].Cells[1].FindControl("txtsubname");
                string str = "select * from kt_exam where stud_id='" + Session["UserName"].ToString() + "' and semester='" + drp_dwn_sem.SelectedItem.Text + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and subject_name='" + box1.Text + "' and exam_code='" + ds11212.Tables[0].Rows[0][0].ToString() + "'";
                DataSet dtfill = cls.fill_dataset(str);
                if (dtfill.Tables[0].Rows.Count > 0)
                {
                    str = "delete from kt_exam where stud_id='" + Session["UserName"].ToString() + "' and semester='" + drp_dwn_sem.SelectedItem.Text + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and subject_name='" + box1.Text + "' and exam_code='" + ds11212.Tables[0].Rows[0][0].ToString() + "'";
                    cls.DMLqueries(str);
                }
                else
                {

                }

                dt.Rows.Remove(dt.Rows[rowIndex]);
                drCurrentRow = dt.NewRow();
                ViewState["CurrentTable"] = dt;
                Gridview2.DataSource = dt;
                Gridview2.DataBind();
                for (int j = 0; j < Gridview2.Rows.Count; j++)
                {
                    TextBox box2 = (TextBox)Gridview2.Rows[j].Cells[1].FindControl("txtsubname");
                    if (box2.Text != "")
                    {
                        box2.Enabled = false;
                    }
                }

                for (int i = 0; i < Gridview2.Rows.Count - 1; i++)
                {
                    Gridview2.Rows[i].Cells[0].Text = Convert.ToString(i + 1);
                }
                SetPreviousData();
            }
        }
    }

    protected void print_btn_Click(object sender, EventArgs e)
    {
        string str1 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and Status=('Exam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + drp_dwn_sem.SelectedValue + "') and (postingf_code='Ok' or postingf_code='S');select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
        DataSet ds11212 = cls.fill_dataset(str1);

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

        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#fyModal').modal('show');</script>", false);
    }


    protected void Payment_Click(object sender, EventArgs e)
    {
        string str11212 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and Status=('Exam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + drp_dwn_sem.SelectedValue + "') and (postingf_code='Ok' or postingf_code='S') ;select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
        DataSet ds11212 = cls.fill_dataset(str11212);
        if (ds11212.Tables[0].Rows.Count == 0)
        {
            Session["Pattern_kt_exm1"] = drp_dwn_pattern.SelectedItem.Text;
            DataTable dt = new DataTable();
            dt.Columns.Add("subj_name");
            dt.Columns.Add("theory_mrks");
            dt.Columns.Add("internal_mrks");
            dt.Columns.Add("term_wrks");
            dt.Columns.Add("pract_mrks");
            int kh = 0;
            for (int i = 0; i < Gridview2.Rows.Count; i++)
            {
                TextBox subj_name = Gridview2.Rows[i].FindControl("txtsubname") as TextBox;
                TextBox theory_mrks = Gridview2.Rows[i].FindControl("txttheory") as TextBox;
                TextBox internal_mrks = Gridview2.Rows[i].FindControl("txtinternal") as TextBox;
                TextBox term_wrks = Gridview2.Rows[i].FindControl("txtterm") as TextBox;
                TextBox pract_mrks = Gridview2.Rows[i].FindControl("txtpractical") as TextBox;
                string str = "select * from kt_exam where stud_id='" + Session["UserName"].ToString() + "' and semester='" + drp_dwn_sem.SelectedItem.Text + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and subject_name='" + subj_name.Text + "' and exam_code='" + ds11212.Tables[1].Rows[0][0].ToString() + "' ";
                DataSet ds_new = cls.fill_dataset(str);
                if (subj_name.Text!="")
                {
                    // ins_str = "";
                    kh++;
                    string ins_str = "";


                    if (ds_new.Tables[0].Rows.Count == 0)
                    {
                        ins_str = "insert into kt_exam values ('" + Session["UserName"].ToString() + "','','" + subj_name.Text + "','" + drp_dwn_sem.SelectedItem.Text + "','" + drp_dwn_sem.SelectedValue + "','" + ds11212.Tables[1].Rows[0][0].ToString() + "','" + drp_dwn_pattern.SelectedItem.Text + "',getdate(),null,0,'" + theory_mrks.Text + "','" + internal_mrks.Text + "','" + term_wrks.Text + "','" + pract_mrks.Text + "','',(select max(ayid) from m_academic where IsCurrent=1),null)";
                    }
                    else
                    {
                        ins_str = "update kt_exam set ESE='" + theory_mrks.Text + "',IA='" + internal_mrks.Text + "',TW='" + term_wrks.Text + "',[PR/OR]='" + pract_mrks.Text + "',mod_dt=getdate() where stud_id='" + Session["UserName"].ToString() + "' and semester='" + drp_dwn_sem.SelectedItem.Text + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from  m_academic where Iscurrent=1) and subject_name='" + subj_name.Text + "' and exam_code='" + ds11212.Tables[1].Rows[0][0].ToString() + "'";
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
                    string ins_str = "";
                    
                    if (ds_new.Tables[0].Rows.Count > 0)
                    {
                        ins_str = "delete from kt_exam where stud_id='" + Session["UserName"].ToString() + "' and semester='" + drp_dwn_sem.SelectedItem.Text + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from  m_academic where Iscurrent=1) and subject_name='" + subj_name.Text + "' and exam_code='" + ds11212.Tables[1].Rows[0][0].ToString() + "' ;"
                        + "delete from kt_exam_pay_details where stud_id='" + Session["UserName"].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and exm_date='" + ds11212.Tables[1].Rows[0][0].ToString() + "' ";
                        cls.DMLqueries(ins_str);
                    }
                }
            }

            DateTime futurDate = Convert.ToDateTime("2020-03-31 05:00");
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
                amount = 281;
            }
            else if (kh == 2)
            {
                amount = 501;
            }
            else if (kh > 2)
            {
                amount = 1052;
            }
            //if (Convert.ToInt32(numberOfDays) <= 0)
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
                string name, group, category, year;
                DataSet payParameters = cls.fill_dataset("select group_title from dbo.m_crs_subjectgroup_tbl where group_id in (select group_id from m_std_studentacademic_tbl where stud_id='" + Session["Username"] + "' and del_flag=0 and ayid=(select max(ayid) from m_academic where Iscurrent=1)); select upper(isnull(stud_L_Name,'')+' '+isnull(stud_F_Name,'')+' '+isnull(stud_M_Name,'')) as Name,stud_Category from m_std_personaldetails_tbl where stud_id='" + Session["Username"] + "' and del_flag=0; select (substring(Duration,9,4)+'-'+substring(Duration,21,4)) as Duration from m_academic where IsCurrent=1;");
                group = payParameters.Tables[0].Rows[0]["group_title"].ToString();
                name = payParameters.Tables[1].Rows[0]["Name"].ToString();
                category = payParameters.Tables[1].Rows[0]["stud_Category"].ToString();
                year = payParameters.Tables[2].Rows[0]["Duration"].ToString();

                string str112 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and Status like '%Exam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+'%'";
                DataSet dt2 = cls.fill_dataset(str112);
                string trans_id = "";
                if (dt2.Tables[0].Rows.Count > 0) { trans_id = Session["UserName"].ToString() + dt2.Tables[0].Rows.Count; }
                else
                { //trans_id = Session["UserName"].ToString() + '/' + ds.Tables[3].Rows[0]["to_year"].ToString().Substring(5, 2) + '-' + sum + "/1"; 
                    trans_id = Session["UserName"].ToString() + "1";
                }
                string str12 = "insert into processing_fees values('" + Session["UserName"].ToString() + "','','','','" + amount + "','','" + trans_id + "','','','','','','','','Exam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + drp_dwn_sem.SelectedValue + "',(select max(ayid) from m_academic where Iscurrent=1),getdate())";
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
                Response.Redirect("payment.aspx/" + amount + "/" + trans_id + "/" + customer_acc_no + "/Exam/" + name + "/" + group + "/" + category + "/" + year);
            }
        }
     
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        
    }
}