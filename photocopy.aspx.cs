using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class photocopy : System.Web.UI.Page
{
    DataSet ds;
    DataTable dt;
    string stud_id = "", stud_name = "", stud_caste = "", stud_mobno = "", stud_email = "", stud_add = "", category = "";
    Class1 cls = new Class1();
    new_Class2 cls1 = new new_Class2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {


            ds = new DataSet();
            string s1 = "select stud_F_Name,stud_M_Name,stud_L_Name,stud_Mother_FName,stud_Gender,stud_BloodGroup,dbo.www_date_display_personal(stud_DOB) as DOB,stud_Nationality,stud_BirthPlace,stud_DomiciledIn,stud_PermanentAdd,stud_PermanentPhone,stud_NativePhone,stud_Category,stud_Caste,stud_Religion,stud_MotherTounge,stud_MartialStatus,stud_Email from dbo.m_std_personaldetails_tbl where stud_id='" + Session["UserName"] + "'";
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
                    category = ds.Tables[0].Rows[i]["stud_category"].ToString();
                    name_txt.Text = stud_name;
                    caste_txt.Text = stud_caste;
                    mobno_txt.Text = stud_mobno;
                    emailid_txt.Text = stud_email;
                    add_txt.Text = stud_add;
                    Session["cat"] = category;
                    Session["Student_Name"] = stud_name;
                    Session["Student_Address"] = stud_add;
                    Session["Student_Caste"] = stud_caste;
                    Session["Student_Mobno"] = stud_mobno;
                    Session["Student_Email"] = stud_email;

                }
            }
        }

    }

    protected void drp_dwn_sem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drp_dwn_sem.SelectedIndex > 0)
        {
            txt_seat.Text = "";
            DataSet ds1 = new DataSet();
            DataSet ds2 = new DataSet();
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            ds = new DataSet();
            print_btn.Enabled = true;
            Session["semester"] = drp_dwn_sem.SelectedValue;
            Session["photocopy_Sem"] = drp_dwn_sem.SelectedValue.ToString();

            dt.Columns.Add("subject_name");
            dt.Columns.Add("marks_obtained");
            dt.Columns.Add("paper_code");

            dt1.Columns.Add("subject_name");
            dt1.Columns.Add("marks_obtained");
            dt1.Columns.Add("paper_code");

            string stud_id = Session["UserName"].ToString();
            string crs_name = Session["course_name"].ToString();

            string course_qry = "select course_id from m_crs_course_tbl where course_name = '" + crs_name + "'";
            ds = cls.fill_dataset(course_qry);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string qry_subj = "select * from dbo.cre_subject where branch_id='" + ds.Tables[0].Rows[0]["course_id"].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue.ToString() + "' and Pattern='" + drp_dwn_pattern.SelectedValue.ToString() + "' and del_flag=0";
                ds1 = cls1.fill_dataset(qry_subj);

                string qry_subj_done = "select * from PR_Details where stud_id='" + stud_id + "' and ext2=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month)  and branch_id='" + ds.Tables[0].Rows[0]["course_id"].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue.ToString() + "' and Pattern='" + drp_dwn_pattern.SelectedValue.ToString() + "' and photocopy_flag=1 and ext2=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month) and ext3=(select max(ayid) from m_academic where Iscurrent=1)";
                ds2 = cls.fill_dataset(qry_subj_done);

                if (drp_dwn_sem.SelectedValue == "Sem-1" || drp_dwn_sem.SelectedValue == "Sem-2" || drp_dwn_sem.SelectedValue == "Sem-7" || drp_dwn_sem.SelectedValue == "Sem-8")
                {

                    university.Visible = true;
                    GridView1.DataSource = null;
                    GridView1.DataBind();

                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        txt_seat.Text = ds2.Tables[0].Rows[0]["seat_no"].ToString();
                        for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                        {
                            DataRow dr = dt.NewRow();


                            string subject = ds2.Tables[0].Rows[j]["subject_name"].ToString();
                            string marks_done = ds2.Tables[0].Rows[j]["marks_obtained"].ToString();
                            string ppr_code = ds2.Tables[0].Rows[j]["paper_code"].ToString();


                            dr["subject_name"] = subject;
                            dr["marks_obtained"] = marks_done;
                            dr["paper_code"] = ppr_code;
                            dt.Rows.Add(dr);
                        }
                    }

                    ViewState["dt"] = dt;
                    Session["MyTable_Photocopy"] = dt;
                    this.data();
                }
                else
                {
                    university.Visible = false;
                    grdfees.DataSource = null;
                    grdfees.DataBind();
                    if (ds2.Tables[0].Rows.Count > 0)
                    {
                        txt_seat.Text = ds2.Tables[0].Rows[0]["seat_no"].ToString();
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = dt.NewRow();
                            string subject = ds1.Tables[0].Rows[i]["subject_name"].ToString();
                            dr["subject_name"] = subject;

                            for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                            {

                                string subject_done = ds2.Tables[0].Rows[j]["subject_name"].ToString();
                                string marks_done = ds2.Tables[0].Rows[j]["marks_obtained"].ToString();
                                string ppr_code = ds2.Tables[0].Rows[j]["paper_code"].ToString();

                                if (subject == subject_done)
                                {


                                    dr["marks_obtained"] = marks_done;
                                    dr["paper_code"] = ppr_code;

                                }


                            }
                            dt.Rows.Add(dr);
                            //Session["marks_Table"] = dt;
                        }
                        Session["MyTable_Photocopy"] = dt;
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            Label subject = (Label)GridView1.Rows[i].FindControl("name_of_subj") as Label;
                            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("photo_chk") as CheckBox;

                            for (int j = 0; j < ds2.Tables[0].Rows.Count; j++)
                            {
                                string subject_done = ds2.Tables[0].Rows[j]["subject_name"].ToString();
                                if (subject.Text == subject_done)
                                {
                                    chk.Checked = true;
                                }

                            }
                        }
                    }
                    else
                    {
                        DataTable dt2 = new DataTable();
                        dt2.Columns.Add("subject_name");
                        dt2.Columns.Add("marks_obtained");
                        dt2.Columns.Add("paper_code");
                        for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
                        {
                            DataRow dr = dt2.NewRow();
                            dr["subject_name"] = ds1.Tables[0].Rows[i]["subject_name"].ToString();
                            dr["marks_obtained"] = "";
                            dr["paper_code"] = "";
                            dt2.Rows.Add(dr);
                        }
                        Session["MyTable_Photocopy"] = dt2;
                        GridView1.DataSource = dt2;
                        GridView1.DataBind();

                    }

                }

                string str11212 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and Status=('PExam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + drp_dwn_sem.SelectedValue + "') and postingf_code='ok';select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
                DataSet ds11212 = cls.fill_dataset(str11212);
                if (ds11212.Tables[0].Rows.Count > 0)
                {
                    if (drp_dwn_sem.SelectedValue == "Sem-1" || drp_dwn_sem.SelectedValue == "Sem-2" || drp_dwn_sem.SelectedValue == "Sem-7" || drp_dwn_sem.SelectedValue == "Sem-8")
                    {
                        grdfees.Enabled = false;
                        btn_add.Enabled = false;
                    }
                    else
                    {
                        GridView1.Enabled = false;
                    }
                }
                else
                {
                    grdfees.Enabled = true;
                    btn_add.Enabled = true;
                    GridView1.Enabled = true;
                }
            }
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
            grdfees.DataSource = null;
            grdfees.DataBind();
            university.Visible = false;
        }

    }


    protected void save_btn_Click(object sender, EventArgs e)
    {
        string str11212 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and Status=('PExam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + drp_dwn_sem.SelectedValue + "') and postingf_code='ok';select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
        DataSet ds11212 = cls.fill_dataset(str11212);
        if (ds11212.Tables[0].Rows.Count > 0)
        {
            dt = new DataTable();

            dt.Columns.Add("subject_name");
            dt.Columns.Add("marks_obtained");
            dt.Columns.Add("paper_code");
            if (drp_dwn_sem.SelectedValue == "Sem-1" || drp_dwn_sem.SelectedValue == "Sem-2" || drp_dwn_sem.SelectedValue == "Sem-7" || drp_dwn_sem.SelectedValue == "Sem-8")
            {
                if (grdfees.Rows.Count > 0)
                {
                    for (int i = 0; i < grdfees.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();

                        TextBox subj_name_lbl = (TextBox)grdfees.Rows[i].FindControl("txt_subject") as TextBox;
                        TextBox marks_txt = (TextBox)grdfees.Rows[i].FindControl("txt_marksobtained") as TextBox;
                        TextBox Q_paper_code = (TextBox)grdfees.Rows[i].FindControl("txt_questpaper") as TextBox;

                        dr["subject_name"] = subj_name_lbl.Text;
                        dr["paper_code"] = Q_paper_code.Text;
                        dr["marks_obtained"] = marks_txt.Text;
                        dt.Rows.Add(dr);
                    }
                }
            }
            else
            {
                if (GridView1.Rows.Count > 0)
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        Label subj_name_lbl = (Label)GridView1.Rows[i].FindControl("name_of_subj");
                        TextBox marks_txt = (TextBox)GridView1.Rows[i].FindControl("mrks_obt");
                        TextBox Q_paper_code = (TextBox)GridView1.Rows[i].FindControl("q_ppr_cd");
                        CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("photo_chk") as CheckBox;

                        if(chk.Checked==true)
                        {
                            dr["subject_name"] = subj_name_lbl.Text;
                            dr["paper_code"] = Q_paper_code.Text;
                            dr["marks_obtained"] = marks_txt.Text;
                            dt.Rows.Add(dr);
                        }
                    }
                }
            }
            Session["MyTable_Photocopy"] = dt;
            Button1.Enabled = true;
            Payment.Enabled = false;
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#fyModal').modal('show');</script>", false);

        }
        else
        {
            Button1.Enabled = false;
            Payment.Enabled = true;
            DataSet crs_ds = new DataSet();
            dt = new DataTable();

            dt.Columns.Add("subject_name");
            dt.Columns.Add("marks_obtained");
            dt.Columns.Add("paper_code");
            if (drp_dwn_sem.SelectedValue == "Sem-1" || drp_dwn_sem.SelectedValue == "Sem-2" || drp_dwn_sem.SelectedValue == "Sem-7" || drp_dwn_sem.SelectedValue == "Sem-8")
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                string subname = "";
                if (grdfees.Rows.Count > 0)
                {
                    int kh = 0;
                    for (int i = 0; i < grdfees.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();

                        TextBox subj_name_lbl = (TextBox)grdfees.Rows[i].FindControl("txt_subject") as TextBox;
                        TextBox marks_txt = (TextBox)grdfees.Rows[i].FindControl("txt_marksobtained") as TextBox;
                        TextBox Q_paper_code = (TextBox)grdfees.Rows[i].FindControl("txt_questpaper") as TextBox;


                        string subj_name = subj_name_lbl.Text;
                        string marks = marks_txt.Text;


                        if (subname == "")
                        {
                            subname = subj_name;
                        }
                        else
                        {
                            subname += "," + subj_name;
                        }

                        string crs_name = Session["course_name"].ToString();
                        string paper_code = Q_paper_code.Text;
                        stud_id = Session["UserName"].ToString();

                        string crs_id_qry = "select course_id from m_crs_course_tbl where course_name = '" + crs_name + "';select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
                        crs_ds = cls.fill_dataset(crs_id_qry);
                        Session["month"] = crs_ds.Tables[1].Rows[0]["month"].ToString();
                        dr["subject_name"] = subj_name;
                        dr["paper_code"] = paper_code;
                        dr["marks_obtained"] = marks;

                        DataSet subj_ds = new DataSet();
                        string present_subj = "select subject_name from PR_Details where subject_name='" + subj_name + "' and ext2=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month)  and stud_id='" + stud_id + "' and photocopy_flag=1 and ext2='" + crs_ds.Tables[1].Rows[0][0].ToString() + "' and ext3=(select max(ayid) from m_academic where Iscurrent=1) and sem_id='" + drp_dwn_sem.SelectedValue + "'";
                        subj_ds = cls.fill_dataset(present_subj);

                        kh++;


                        if (subj_ds.Tables[0].Rows.Count > 0)
                        {

                            string upd_subj = "update PR_Details set seat_no='" + txt_seat.Text + "', marks_obtained='" + marks + "',paper_code='" + paper_code + "' where subject_name='" + subj_name + "' and stud_id='" + stud_id + "' and photocopy_flag=1 and ext2='" + crs_ds.Tables[1].Rows[0][0].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ext3=(select max(ayid) from m_academic where Iscurrent=1)";
                            bool result_upd = false;
                            result_upd = cls.DMLqueries(upd_subj);


                        }
                        else
                        {
                            string updqry = "insert into PR_details values ('" + stud_id + "','" + subj_name + "','" + paper_code + "','" + marks + "','" + crs_ds.Tables[0].Rows[0]["course_id"].ToString() + "','" + drp_dwn_sem.SelectedValue.ToString() + "',getdate(),getdate(),'0','0','1','" + drp_dwn_pattern.SelectedValue.ToString() + "','" + crs_ds.Tables[1].Rows[0][0].ToString() + "',(select max(ayid) from m_academic where Iscurrent=1),'" + txt_seat.Text + "')";
                            bool result_ins = false;
                            result_ins = cls.DMLqueries(updqry);

                        }


                        dt.Rows.Add(dr);
                    }
                    string qry = "delete from PR_Details where  stud_id='" + stud_id + "' and photocopy_flag=1 and ext2='" + Session["month"].ToString() + "' and ext3=(select max(ayid) from m_academic where Iscurrent=1) and sem_id='" + drp_dwn_sem.SelectedValue + "' and subject_name not in ('" + subname.Replace(",", "','") + "') ";
                    bool result = false;
                    result = cls.DMLqueries(qry);

                    Session["cnt"] = kh;




                    Session["MyTable_Photocopy"] = dt;
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#fyModal').modal('show');</script>", false);
                }
                else
                {
                    string qry = "delete from PR_Details where  stud_id='" + Session["UserName"].ToString() + "' and photocopy_flag=1 and ext2=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end) and ext3=(select max(ayid) from m_academic where Iscurrent=1) and sem_id='" + drp_dwn_sem.SelectedValue + "'  ";
                    bool result = false;
                    result = cls.DMLqueries(qry);
                }
                //    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Data Saved Successfully!!')", true);
                

            }
            else
            {
                int kh = 0;
                if (GridView1.Rows.Count > 0)
                {

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        DataRow dr = dt.NewRow();
                        CheckBox chk_bx = (CheckBox)GridView1.Rows[i].FindControl("photo_chk");
                        Label subj_name_lbl = (Label)GridView1.Rows[i].FindControl("name_of_subj");
                        TextBox marks_txt = (TextBox)GridView1.Rows[i].FindControl("mrks_obt");
                        TextBox Q_paper_code = (TextBox)GridView1.Rows[i].FindControl("q_ppr_cd");

                        bool Checked_ = chk_bx.Checked;
                        string subj_name = subj_name_lbl.Text;
                        string marks = marks_txt.Text;
                        string crs_name = Session["course_name"].ToString();
                        string paper_code = Q_paper_code.Text;
                        stud_id = Session["UserName"].ToString();
                        string crs_id_qry = "select course_id from m_crs_course_tbl where course_name = '" + crs_name + "';select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
                        crs_ds = cls.fill_dataset(crs_id_qry);



                        DataSet subj_ds = new DataSet();
                        string present_subj = "select subject_name from PR_Details where subject_name='" + subj_name + "' and stud_id='" + stud_id + "' and photocopy_flag=1 and ext2='" + crs_ds.Tables[1].Rows[0][0].ToString() + "' and ext3=(select max(ayid) from m_academic where Iscurrent=1) and ext2=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month)  and sem_id='" + drp_dwn_sem.SelectedValue + "'";
                        subj_ds = cls.fill_dataset(present_subj);
                        if (Checked_ == true)
                        {
                            dr["subject_name"] = subj_name;
                            dr["paper_code"] = paper_code;
                            dr["marks_obtained"] = marks;
                            kh++;


                            if (subj_ds.Tables[0].Rows.Count > 0)
                            {

                                string upd_subj = "update PR_Details set seat_no='" + txt_seat.Text + "',marks_obtained='" + marks + "',paper_code='" + paper_code + "' where subject_name='" + subj_name + "' and stud_id='" + stud_id + "' and photocopy_flag=1 and ext2='" + crs_ds.Tables[1].Rows[0][0].ToString() + "' and sem_id='" + drp_dwn_sem.SelectedValue + "' and ext3=(select max(ayid) from m_academic where Iscurrent=1)";
                                bool result = false;
                                result = cls.DMLqueries(upd_subj);

                            }
                            else
                            {

                                string qry = "insert into PR_details values ('" + stud_id + "','" + subj_name + "','" + paper_code + "','" + marks + "','" + crs_ds.Tables[0].Rows[0]["course_id"].ToString() + "','" + drp_dwn_sem.SelectedValue.ToString() + "',getdate(),getdate(),'0','0','1','" + drp_dwn_pattern.SelectedValue.ToString() + "','" + crs_ds.Tables[1].Rows[0][0].ToString() + "',(select max(ayid) from m_academic where Iscurrent=1),'" + txt_seat.Text + "')";
                                bool result = false;
                                result = cls.DMLqueries(qry);

                            }

                        }
                        else
                        {
                            if (subj_ds.Tables[0].Rows.Count > 0)
                            {
                                string upd_subj = "delete from  PR_Details where subject_name='" + subj_name + "' and stud_id='" + stud_id + "' and photocopy_flag=1 and ext2='" + crs_ds.Tables[1].Rows[0][0].ToString() + "' and ext3=(select max(ayid) from m_academic where Iscurrent=1) and sem_id='" + drp_dwn_sem.SelectedValue + "'";
                                bool result = false;
                                result = cls.DMLqueries(upd_subj);
                            }
                        }
                        dt.Rows.Add(dr);
                        Session["MyTable_Photocopy"] = dt;
                        Session["cnt"] = kh;
                    }
                    //  ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Data Saved Successfully!!')", true);
                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#fyModal').modal('show');</script>", false);

                }

            }
        }
    }

  

    protected void drp_dwn_pattern_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (drp_dwn_pattern.SelectedIndex > 0)
        {
            drp_dwn_sem.Enabled = true;
        }

        Session["Pattern_photocopy"] = drp_dwn_pattern.SelectedValue.ToString();
    }

    protected void print_btn_Click(object sender, EventArgs e)
    {
        // Response.Redirect("photocopy_form.aspx");
    }
    protected void Payment_Click(object sender, EventArgs e)
    {
        try
        {
            string customer_acc_no = Session["Username"].ToString();
            int amount = 0;
            if (Session["cat"].ToString() == "OPEN" || Session["cat"].ToString() == "OBC" || Session["cat"].ToString() == "SEBC" || Session["cat"].ToString() == "SBC" || Session["cat"].ToString() == "TWFS" || Session["cat"].ToString() == "TFWS" || Session["cat"].ToString() == "EBC")
            {
                amount = (50 * Convert.ToInt32(Session["cnt"])) + 10;
            }
            else if (Session["cat"].ToString() == "SC" || Session["cat"].ToString() == "ST" || Session["cat"].ToString().Contains("NT") || Session["cat"].ToString().Contains("DT"))
            {
                amount = (25 * Convert.ToInt32(Session["cnt"])) + 10;
            }

            if (amount != 0 && Convert.ToInt32(Session["cnt"])>0)
            {
                string group = "";
                string name, category, year, ayid;
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
                DataSet payParameters = cls.fill_dataset("select group_title from dbo.m_crs_subjectgroup_tbl where group_id in (select group_id from m_std_studentacademic_tbl where stud_id='" + Session["Username"] + "' and del_flag=0); select upper(isnull(stud_L_Name,'')+' '+isnull(stud_F_Name,'')+' '+isnull(stud_M_Name,'')) as Name,stud_Category from m_std_personaldetails_tbl where stud_id='" + Session["Username"] + "' and del_flag=0; select (substring(Duration,9,4)+'-'+substring(Duration,21,4)) as Duration,ayid from m_academic where IsCurrent=1;");
                group += payParameters.Tables[0].Rows[0]["group_title"].ToString().Replace("\r\n", "").Replace("&","and"); 
                name = payParameters.Tables[1].Rows[0]["Name"].ToString();
                category = payParameters.Tables[1].Rows[0]["stud_Category"].ToString();
                year = payParameters.Tables[2].Rows[0]["Duration"].ToString();
                ayid = payParameters.Tables[2].Rows[0]["ayid"].ToString();
                string str112 = "select * from processing_fees where form_no='" + Session["UserName"].ToString() + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1)";
                DataSet dt2 = cls.fill_dataset(str112);
                string trans_id = "";
                if (dt2.Tables[0].Rows.Count > 0) { trans_id = Session["UserName"].ToString() + Convert.ToString(Convert.ToInt32(dt2.Tables[0].Rows.Count) + 1); }
                else
                { //trans_id = Session["UserName"].ToString() + '/' + ds.Tables[3].Rows[0]["to_year"].ToString().Substring(5, 2) + '-' + sum + "/1"; 
                    if (dt2.Tables[0].Rows.Count == 0)
                    {
                        trans_id = Session["UserName"].ToString() + "1";
                    }
                    else
                    {
                        trans_id = Session["UserName"].ToString() + Convert.ToString(Convert.ToInt32(dt2.Tables[0].Rows.Count) + 1);
                    }
                }
                string str12 = "insert into processing_fees values('" + Session["UserName"].ToString() + "','','','','" + amount + "','','" + trans_id + "','','','','','','','','PExam:'+ case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end+':" + drp_dwn_sem.SelectedValue + "',(select max(ayid) from m_academic where Iscurrent=1),getdate())";
                cls.update_data(str12);
                Response.Redirect("payment.aspx/" + amount + "/" + trans_id + "/" + customer_acc_no + "/PExam/" + name + "/" + group + "/" + category + "/" + year + "/" + group + "/" + ayid, false);
            }
            else
            {
                if (Convert.ToInt32(Session["cnt"]) > 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('No Subject Selected')", true);
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Cannot Redirect to Online Payment!!')", true);
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("Photocopy_form_off.aspx");
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


    protected void btn_add_Click(object sender, EventArgs e)
    {
        try
        {

            DataTable dt = ViewState["dt"] as DataTable;
            DataRow r = dt.NewRow();
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
    protected void txt_subject_TextChanged(object sender, EventArgs e)
    {

        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
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

    protected void txt_marksobtained_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            TextBox txt = sender as TextBox;
            GridViewRow gvrow = txt.NamingContainer as GridViewRow;
            GridViewRow row1 = grdfees.Rows[gvrow.RowIndex];
            DataTable dt = ViewState["dt"] as DataTable;

            string theory = ((TextBox)grdfees.Rows[row1.RowIndex].FindControl("txt_marksobtained")).Text;

            DataRow dr = dt.NewRow();

            dt.Rows[row1.RowIndex]["marks_obtained"] = theory.ToString();
            ViewState["dt"] = dt;
            this.data();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }

    protected void txt_questpaper_TextChanged(object sender, EventArgs e)
    {
        try
        {
            GridView1.DataSource = null;
            GridView1.DataBind();

            TextBox txt = sender as TextBox;
            GridViewRow gvrow = txt.NamingContainer as GridViewRow;
            GridViewRow row1 = grdfees.Rows[gvrow.RowIndex];
            DataTable dt = ViewState["dt"] as DataTable;

            string theory = ((TextBox)grdfees.Rows[row1.RowIndex].FindControl("txt_questpaper")).Text;

            DataRow dr = dt.NewRow();

            dt.Rows[row1.RowIndex]["paper_code"] = theory.ToString();
            ViewState["dt"] = dt;
            this.data();

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "$.notify('" + ex.Message.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0, timeout: 100 });", true);
        }
    }
}