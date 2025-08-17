using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

public partial class Result : System.Web.UI.Page
{
    clsResult objcon = new clsResult();
    DataTable dt_creditEarned;
    DataTable dtReChk;
    Class1 c1 = new Class1();

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                divmsg.Visible = false;
                divResult.Visible = false;
                message.Visible = false; //change it
                if (string.IsNullOrEmpty(Session["UserName"].ToString()))
                {

                    Response.Redirect("login.aspx");
                }
                else
                {
                    dtReChk = objcon.filldatatable("select * from cre_webresult where stud_id = '" + Session["UserName"].ToString() + "' and col1<>''");
                    if (dtReChk.Rows.Count > 0)
                    {
                        result.Visible = true;
                        message.Visible = false;
                        ddlSem.Items.Clear();
                        ddlSem.Items.Add(new ListItem("--SELECT--", ""));
                        for (int i = 0; i < dtReChk.Rows.Count; i++)
                        {
                            if (dtReChk.Rows[i]["sem_id"].ToString().Equals("Sem-1"))
                            {
                                if (!ddlSem.Items.Contains(new ListItem("SEMESTER I", dtReChk.Rows[i]["sem_id"].ToString())))
                                {
                                    ddlSem.Items.Add(new ListItem("SEMESTER I", dtReChk.Rows[i]["sem_id"].ToString()));
                                }
                            }
                            if (dtReChk.Rows[i]["sem_id"].ToString().Equals("Sem-2"))
                            {
                                if (!ddlSem.Items.Contains(new ListItem("SEMESTER II", dtReChk.Rows[i]["sem_id"].ToString())))
                                {
                                    ddlSem.Items.Add(new ListItem("SEMESTER II", dtReChk.Rows[i]["sem_id"].ToString()));
                                }
                            }
                            if (dtReChk.Rows[i]["sem_id"].ToString().Equals("Sem-3"))
                            {
                                if (!ddlSem.Items.Contains(new ListItem("SEMESTER III", dtReChk.Rows[i]["sem_id"].ToString())))
                                {
                                    ddlSem.Items.Add(new ListItem("SEMESTER III", dtReChk.Rows[i]["sem_id"].ToString()));
                                }
                            }
                            if (dtReChk.Rows[i]["sem_id"].ToString().Equals("Sem-4"))
                            {
                                if (!ddlSem.Items.Contains(new ListItem("SEMESTER IV", dtReChk.Rows[i]["sem_id"].ToString())))
                                {
                                    ddlSem.Items.Add(new ListItem("SEMESTER IV", dtReChk.Rows[i]["sem_id"].ToString()));
                                }
                            }

                        }
                        ddlSem.SelectedIndex = 0;
                    }
                    else
                    {
                        result.Visible = false;
                        message.Visible = true;
                    }
                }
            }
        }
        catch
        {
            Response.Redirect("log_out.aspx");
        }
    }

    protected void btnOk_Click(object sender, EventArgs e)
    {
        try
        {
            if (ddlExam.SelectedIndex > 0)
            {
                DataTable dtCheck = objcon.filldatatable("select col1 from cre_webresult where stud_id = '" + Session["UserName"].ToString() + "' and sem_id='" + ddlSem.SelectedValue.ToString() + "' and exam_code='" + ddlExam.SelectedValue.ToString() + "' and col1<>'' and declare_flag = 0");
                if (dtCheck.Rows.Count > 0)
                {
                    divmsg.Visible = true;
                    divmsg.InnerHtml = "<b> Contact to Degree Office, Room No. 217 (New Campus) </b>";
                    dtCheck.Dispose();
                }
                else
                {
                    bool dt_grace_flag = false;
                    if (dt_grace_flag == false)
                    {
                        DataTable dt_grace = objcon.filldatatable("select h1_grace,h2_grace from dbo.cre_marks_tbl where h1_grace='' or h2_grace=''");
                        dt_grace_flag = true;
                    }
                    //DataTable dt_req = objcon.filldatatable("select distinct a.exam_code,b.group_id, a.sem_id from dbo.V_credit_tbls a, m_std_studentacademic_tbl b where a.stud_id = b.stud_id and a.stud_id = '" + Session["UserName"].ToString() + "' and a.sem_id = (select MAX(sem_id) from dbo.cre_marks_tbl where stud_id = '" + Session["UserName"].ToString() + "') and b.ayid = (select max(ayid) from V_credit_tbls where stud_id='" + Session["UserName"].ToString() + "' and exam_code='" + ddlExam.SelectedValue.ToString() + "' and sem_id='" + ddlSem.SelectedValue.ToString() + "')");
                    // DataTable dt_req = objcon.filldatatable("select distinct a.exam_code,b.group_id, a.sem_id from dbo.V_credit_tbls a, m_std_studentacademic_tbl b where a.stud_id = b.stud_id and a.stud_id = '" + Session["UserName"].ToString() + "' and  a.ayid = (select max(ayid) from V_credit_tbls where stud_id='" + Session["UserName"].ToString() + "' and b.ayid = (select max(ayid) from V_credit_tbls where stud_id='" + Session["UserName"].ToString() + "' and exam_code='" + ddlExam.SelectedValue.ToString() + "' and sem_id='" + ddlSem.SelectedValue.ToString() + "'))");
                    //DataTable dt_req = objcon.filldatatable("select distinct exam_code,sem_id from V_credit_tbls where stud_id = '" + Session["UserName"].ToString() + "' and sem_id=(select MAX(sem_id) from dbo.cre_marks_tbl where stud_id = '" + Session["UserName"].ToString() + "')  and  exam_code='" + ddlExam.SelectedValue.ToString() + "' ");
                    // NEED TO CHANGE 20-JAN-2016 
                    DataTable dt_req = objcon.filldatatable("select distinct exam_code,sem_id,max(mac.group_id) as group_id from V_credit_tbls as v,m_std_studentacademic_tbl as mac where v.stud_id=mac.stud_id and v.stud_id  = '" + Session["UserName"].ToString() + "' and  exam_code='" + ddlExam.SelectedValue.ToString() + "' group by exam_code,sem_id ");

                    DataTable dtWeb = objcon.filldatatable("select * from cre_webresult where stud_id = '" + Session["UserName"].ToString() + "' and sem_id = '" + ddlSem.SelectedValue.ToString() + "' and exam_code = '" + ddlExam.SelectedValue.ToString() + "'  and del_flag = 0");
                    if (dtWeb.Rows.Count > 0)
                    {
                        if (dtWeb.Rows[0]["declare_flag"].ToString().Trim().Equals("True"))
                        {
                            if (dt_req.Rows.Count > 0)
                            {
                                DataTable dt;
                                dt_creditEarned = objcon.filldatatable("select sem1_credit_earn,sem2_credit_earn,sem3_credit_earn,sem4_credit_earn from dbo.cre_stud_academic where stud_id='" + Session["UserName"].ToString() + "'");
                                //dt = objcon.filldatatable("select distinct stud_id,case stud_Gender when 0 then '/ '+ upper(Student_Name) else upper(Student_Name) end as student_name,seat_no,subject_code,subject_name,credit,h1,h1_grace,h2,h2_grace,h1_out, h1_pass,h2_out,h2_pass,h1_type,h2_type,formula,remark,sem_id,exam_date,subject_id,b.course_name as branch_name,credit_sub_id from dbo.V_credit_tbls a left join dbo.m_crs_course_tbl b on a.branch_id=b.course_id where stud_id='" + Session["UserName"].ToString() + "' and exam_code='" + ddlExam.SelectedValue.ToString() + "' and sem_id='" + ddlSem.SelectedValue.ToString() + "'  and group_id like '%" + dt_req.Rows[0]["group_id"].ToString() + "%'    order by subject_id,credit_sub_id");
                                dt = objcon.filldatatable("select distinct stud_id,case stud_Gender when 0 then '/ '+ upper(Student_Name) else upper(Student_Name) end as student_name,seat_no,subject_code,subject_name,credit,h1,h1_grace,h2,h2_grace,h1_out, h1_pass,h2_out,h2_pass,h1_type,h2_type,formula,remark,sem_id,exam_date,subject_id,b.course_name as branch_name,credit_sub_id from dbo.V_credit_tbls a left join dbo.m_crs_course_tbl b on a.branch_id=b.course_id where stud_id='" + Session["UserName"].ToString() + "' and exam_code='" + ddlExam.SelectedValue.ToString() + "' and sem_id='" + ddlSem.SelectedValue.ToString() + "'    order by subject_id,credit_sub_id");
                                DataTable dtGroup = objcon.filldatatable("select Group_title from m_crs_subjectgroup_tbl where group_id like '%" + dt_req.Rows[0]["group_id"].ToString() + "%'");
                                lblProgram.Text = dt.Rows[0]["branch_name"].ToString() + " (" + dtGroup.Rows[0]["Group_title"].ToString() + ") " + objcon.sem_value(dt.Rows[0]["sem_id"].ToString());
                                lblSeatNo.Text = dt.Rows[0]["seat_no"].ToString();
                                lblStudName.Text = dt.Rows[0]["student_name"].ToString();
                                lblStudID.Text = dt.Rows[0]["stud_id"].ToString();
                                lblExmDate.Text = dt.Rows[0]["exam_date"].ToString();
                                if (dt.Rows.Count > 0)
                                {
                                    string newtotal_obtain = "0";
                                    int changedVal = 0;
                                    String[] arr1 = new String[3];
                                    bool twFail = false;
                                    string student_id = "";
                                    int Th_Kt = 0, Oth_Kt = 0, total_obtain = 0, outOf_new = 0;
                                    double cre_total = 0, creEarn_total = 0, CXG_total1 = 0, SGPA = 0, credit = 0, creEarn_total_new = 0;
                                    string CXG_total = "";
                                    int outOf = 0, pass = 0, obtain = 0;
                                    string remark = "";
                                    bool remark_flag = false;
                                    DataTable dtClone = new DataTable();
                                    dtClone = dt.Copy();
                                    //dtClone.Columns.Remove("h1");
                                    //dtClone.Columns.Remove("h2");
                                    dtClone.Columns.Add("h1_grade");
                                    dtClone.Columns.Add("h2_grade");
                                    dtClone.Columns.Add("overall");
                                    dtClone.Columns.Add("creditEarned");
                                    dtClone.Columns.Add("gradePoints");
                                    dtClone.Columns.Add("CXG");
                                    dtClone.Columns.Add("cre_total");
                                    dtClone.Columns.Add("creEarn_total");
                                    dtClone.Columns.Add("CXG_total");
                                    dtClone.Columns.Add("sgpa");
                                    dtClone.Columns.Add("cgpi");
                                    dtClone.Columns.Add("result_declare_on");
                                    dtClone.Columns.Add("outOf");
                                    dtClone.Columns.Add("pass");
                                    dtClone.Columns.Add("obtain");
                                    //dtClone.Columns.Add("stud_id");
                                    DataColumn col = new DataColumn("pic");
                                    DataColumn col1 = new DataColumn("sem1_credit_earn");
                                    DataColumn col2 = new DataColumn("sem2_credit_earn");
                                    DataColumn col3 = new DataColumn("sem3_credit_earn");
                                    DataColumn col4 = new DataColumn("sem4_credit_earn");
                                    DataColumn col5 = new DataColumn("Credit_earn");
                                    DataColumn col6 = new DataColumn("sgpa_new");
                                    DataColumn col7 = new DataColumn("total_obtain");
                                    DataColumn col8 = new DataColumn("grade_sgpa");
                                    DataColumn col9 = new DataColumn("outOf_new");
                                    DataColumn col10 = new DataColumn("creEarn_total_new");
                                    DataColumn col11 = new DataColumn("CXG_total_new");
                                    DataColumn col12 = new DataColumn("branchName_new");
                                    // DataColumn col13 = new DataColumn("semester_name");
                                    //DataColumn col14 = new DataColumn("student_id");


                                    col.DataType = typeof(Byte[]);
                                    dtClone.Columns.Add(col);
                                    dtClone.Columns.Add(col1);
                                    dtClone.Columns.Add(col2);
                                    dtClone.Columns.Add(col3);
                                    dtClone.Columns.Add(col4);
                                    dtClone.Columns.Add(col5);
                                    dtClone.Columns.Add(col6);
                                    dtClone.Columns.Add(col7);
                                    dtClone.Columns.Add(col8);
                                    dtClone.Columns.Add(col9);
                                    dtClone.Columns.Add(col10);
                                    dtClone.Columns.Add(col11);
                                    dtClone.Columns.Add(col12);

                                    DataTable dtclone2 = dtClone.Copy();

                                    int subGrade = 0;
                                    bool GotGrace = false, isNss = false, isfail = false;

                                    DataTable dtRemark = objcon.filldatatable("select remark from dbo.cre_marks_tbl m, dbo.m_std_personaldetails_tbl p  where m.stud_id=p.stud_id and sem_id='" + ddlSem.SelectedValue.ToString() + "' and m.exam_code='" + ddlExam.SelectedValue.ToString() + "' and m.stud_id='" + Session["UserName"].ToString() + "' and remark like 'un%' and  m.del_flag=0 and p.del_flag=0 ");
                                    if (dtRemark.Rows.Count > 0)
                                    {
                                        isfail = true;
                                    }

                                    DataTable dt42 = objcon.filldatatable("select sum(convert(int,isnull(h3_grace,'0'))) from dbo.cre_credit_tbl as c,dbo.cre_marks_tbl as m where c.credit_sub_id=m.credit_sub_id and m.exam_code='" + ddlExam.SelectedValue.ToString() + "' and m.stud_id='" + Session["UserName"].ToString() + "' and m.sem_id='" + ddlSem.SelectedValue.ToString() + "' and ((h1_grace like '%*%' or h1_grace like '%@%') or (h2_grace like '%*%' or h2_grace like '%@%')) and m.del_flag=0 and c.del_flag=0");


                                    if (dt42.Rows.Count > 0)
                                    {
                                        if (!string.IsNullOrEmpty(Convert.ToString(dt42.Rows[0][0])))
                                        {
                                            if (Convert.ToInt32(dt42.Rows[0][0]) > 0)
                                            {
                                                GotGrace = true;
                                            }
                                        }
                                    }

                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        int check_flg;
                                        check_flg = i;

                                        if (check_flg == 0)
                                        {
                                            dtClone.Rows[i]["credit"] = dtClone.Rows[i]["credit"].ToString();

                                        }
                                        else
                                        {
                                            if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                            {
                                                dtClone.Rows[i - 1]["credit"] = dtClone.Rows[i - 1]["credit"] + Environment.NewLine + dtClone.Rows[i]["credit"].ToString();

                                            }
                                            else
                                            {
                                                dtClone.Rows[i]["credit"] = dtClone.Rows[i]["credit"].ToString();
                                            }
                                        }

                                        dtClone.Rows[i]["result_declare_on"] = "";
                                        ////////////////////Examination Name/////////////
                                        //dtClone.Rows[i]["sem_id"] = objcon.getExamFullName(dt.Rows[i]["sem_id"].ToString());

                                        if ((!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h1_out"]))) && string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h1"])))
                                        {
                                            if (check_flg == 0)
                                            {
                                                dtclone2.Rows[i]["h1_grade"] = " ";
                                                dtclone2.Rows[i]["h2_grade"] = " ";
                                                dtclone2.Rows[i]["overall"] = " ";
                                                dtclone2.Rows[i]["creditEarned"] = " ";
                                                dtclone2.Rows[i]["gradePoints"] = " ";
                                                dtclone2.Rows[i]["CXG"] = " ";
                                                dtclone2.Rows[i]["cre_total"] = " ";
                                                dtclone2.Rows[i]["creEarn_total"] = " ";
                                                //dtclone2.Rows[i]["creEarn_total_new"] = " ";
                                                dtclone2.Rows[i]["CXG_total"] = " ";
                                                dtclone2.Rows[i]["sgpa"] = " ";
                                                dtclone2.Rows[i]["outOf"] = " ";
                                                dtclone2.Rows[i]["pass"] = " ";
                                                dtclone2.Rows[i]["obtain"] = " ";
                                                dtclone2.Rows[i]["sgpa_new"] = " ";


                                                dtClone.Rows[i]["h1_grade"] = " ";
                                                dtClone.Rows[i]["h2_grade"] = " ";
                                                dtClone.Rows[i]["overall"] = " ";
                                                dtClone.Rows[i]["creditEarned"] = " ";
                                                dtClone.Rows[i]["gradePoints"] = " ";
                                                dtClone.Rows[i]["CXG"] = " ";
                                                dtClone.Rows[i]["cre_total"] = " ";
                                                dtClone.Rows[i]["creEarn_total"] = " ";
                                                //dtClone.Rows[i]["creEarn_total_new"] = " ";
                                                dtClone.Rows[i]["CXG_total"] = " ";
                                                dtClone.Rows[i]["sgpa"] = " ";
                                                dtClone.Rows[i]["outOf"] = " ";
                                                dtClone.Rows[i]["pass"] = " ";
                                                dtClone.Rows[i]["obtain"] = " ";
                                                dtClone.Rows[i]["sgpa_new"] = " ";


                                                remark_flag = true;
                                                break;
                                            }
                                            else
                                            {
                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {
                                                    dtClone.Rows[i - 1]["h1_grade"] = dtClone.Rows[i - 1]["h1_grade"] + Environment.NewLine + " ";
                                                    dtClone.Rows[i - 1]["h2_grade"] = dtClone.Rows[i - 1]["h2_grade"] + Environment.NewLine + " ";
                                                    dtClone.Rows[i - 1]["overall"] = dtClone.Rows[i - 1]["overall"] + Environment.NewLine + " ";
                                                    dtClone.Rows[i - 1]["creditEarned"] = dtClone.Rows[i - 1]["creditEarned"] + Environment.NewLine + " ";
                                                    dtClone.Rows[i - 1]["gradePoints"] = dtClone.Rows[i - 1]["gradePoints"] + Environment.NewLine + " ";
                                                    dtClone.Rows[i - 1]["CXG"] = dtClone.Rows[i - 1]["CXG"] + Environment.NewLine + " ";
                                                    dtClone.Rows[i - 1]["cre_total"] = dtClone.Rows[i - 1]["cre_total"] + Environment.NewLine + " ";
                                                    dtClone.Rows[i - 1]["creEarn_total"] = dtClone.Rows[i - 1]["creEarn_total"] + Environment.NewLine + " ";
                                                    //dtClone.Rows[i - 1]["creEarn_total_new"] = dtClone.Rows[i - 1]["creEarn_total_new"] + Environment.NewLine + " ";
                                                    dtClone.Rows[i - 1]["CXG_total"] = dtClone.Rows[i - 1]["CXG_total"] + Environment.NewLine + " ";
                                                    // dtClone.Rows[i - 1]["sgpa"] = dtClone.Rows[i - 1]["sgpa"] + Environment.NewLine + " ";
                                                    dtClone.Rows[i - 1]["outOf"] = dtClone.Rows[i - 1]["outOf"] + Environment.NewLine + " ";
                                                    dtClone.Rows[i - 1]["pass"] = dtClone.Rows[i - 1]["pass"] + Environment.NewLine + " ";
                                                    dtClone.Rows[i - 1]["obtain"] = dtClone.Rows[i - 1]["obtain"] + Environment.NewLine + " ";


                                                    remark_flag = true;
                                                    break;
                                                }
                                                else
                                                {

                                                    dtclone2.Rows[i]["h1_grade"] = " ";
                                                    dtclone2.Rows[i]["h2_grade"] = " ";
                                                    dtclone2.Rows[i]["overall"] = " ";
                                                    dtclone2.Rows[i]["creditEarned"] = " ";
                                                    dtclone2.Rows[i]["gradePoints"] = " ";
                                                    dtclone2.Rows[i]["CXG"] = " ";
                                                    dtclone2.Rows[i]["cre_total"] = " ";
                                                    dtclone2.Rows[i]["creEarn_total"] = " ";
                                                    //dtclone2.Rows[i]["creEarn_total_new"] = " ";
                                                    dtclone2.Rows[i]["CXG_total"] = " ";
                                                    dtclone2.Rows[i]["sgpa"] = " ";
                                                    dtclone2.Rows[i]["outOf"] = " ";
                                                    dtclone2.Rows[i]["pass"] = " ";
                                                    dtclone2.Rows[i]["obtain"] = " ";
                                                    dtclone2.Rows[i]["sgpa_new"] = " ";



                                                    dtClone.Rows[i]["h1_grade"] = " ";
                                                    dtClone.Rows[i]["h2_grade"] = " ";
                                                    dtClone.Rows[i]["overall"] = " ";
                                                    dtClone.Rows[i]["creditEarned"] = " ";
                                                    dtClone.Rows[i]["gradePoints"] = " ";
                                                    dtClone.Rows[i]["CXG"] = " ";
                                                    dtClone.Rows[i]["cre_total"] = " ";
                                                    dtClone.Rows[i]["creEarn_total"] = " ";
                                                    //dtClone.Rows[i]["creEarn_total_new"] = " ";
                                                    dtClone.Rows[i]["CXG_total"] = " ";
                                                    dtClone.Rows[i]["sgpa"] = " ";
                                                    dtClone.Rows[i]["outOf"] = " ";
                                                    dtClone.Rows[i]["pass"] = " ";
                                                    dtClone.Rows[i]["obtain"] = " ";
                                                    dtClone.Rows[i]["sgpa_new"] = " ";



                                                    remark_flag = true;
                                                    break;

                                                }
                                            }
                                        }

                                        //if (dt.Rows[i]["h2_out"].ToString() != "" && dt.Rows[i]["h2"].ToString() == "")
                                        else if ((!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h2_out"]))) && string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h2"])))
                                        {
                                            if (check_flg == 0)
                                            {
                                                dtclone2.Rows[i]["h1_grade"] = " ";
                                                dtclone2.Rows[i]["h2_grade"] = " ";
                                                dtclone2.Rows[i]["overall"] = " ";
                                                dtclone2.Rows[i]["creditEarned"] = " ";
                                                dtclone2.Rows[i]["gradePoints"] = " ";
                                                dtclone2.Rows[i]["CXG"] = " ";
                                                dtclone2.Rows[i]["cre_total"] = " ";
                                                dtclone2.Rows[i]["creEarn_total"] = " ";
                                                //dtclone2.Rows[i]["creEarn_total_new"] = " ";
                                                dtclone2.Rows[i]["CXG_total"] = " ";
                                                dtclone2.Rows[i]["sgpa"] = " ";
                                                dtclone2.Rows[i]["outOf"] = " ";
                                                dtclone2.Rows[i]["pass"] = " ";
                                                dtclone2.Rows[i]["obtain"] = " ";
                                                dtclone2.Rows[i]["sgpa_new"] = " ";




                                                dtClone.Rows[i]["h1_grade"] = " ";
                                                dtClone.Rows[i]["h2_grade"] = " ";
                                                dtClone.Rows[i]["overall"] = " ";
                                                dtClone.Rows[i]["creditEarned"] = " ";
                                                dtClone.Rows[i]["gradePoints"] = " ";
                                                dtClone.Rows[i]["CXG"] = " ";
                                                dtClone.Rows[i]["cre_total"] = " ";
                                                dtClone.Rows[i]["creEarn_total"] = " ";
                                                //dtClone.Rows[i]["creEarn_total_new"] = " ";
                                                dtClone.Rows[i]["CXG_total"] = " ";
                                                dtClone.Rows[i]["sgpa"] = " ";
                                                dtClone.Rows[i]["outOf"] = " ";
                                                dtClone.Rows[i]["pass"] = " ";
                                                dtClone.Rows[i]["obtain"] = " ";
                                                dtClone.Rows[i]["sgpa_new"] = " ";


                                                remark_flag = true;
                                                break;
                                            }
                                            else
                                            {
                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {
                                                    dtClone.Rows[i - 1]["h1_grade"] = " ";
                                                    dtClone.Rows[i - 1]["h2_grade"] = " ";
                                                    dtClone.Rows[i - 1]["overall"] = " ";
                                                    dtClone.Rows[i - 1]["creditEarned"] = " ";
                                                    dtClone.Rows[i - 1]["gradePoints"] = " ";
                                                    dtClone.Rows[i - 1]["CXG"] = " ";
                                                    dtClone.Rows[i - 1]["cre_total"] = " ";
                                                    dtClone.Rows[i - 1]["creEarn_total"] = " ";
                                                    //dtClone.Rows[i - 1]["creEarn_total_new"] = " ";

                                                    dtClone.Rows[i - 1]["CXG_total"] = " ";
                                                    dtClone.Rows[i - 1]["sgpa"] = " ";
                                                    dtClone.Rows[i - 1]["outOf"] = " ";
                                                    dtClone.Rows[i - 1]["pass"] = " ";
                                                    dtClone.Rows[i - 1]["obtain"] = " ";
                                                    dtClone.Rows[i - 1]["sgpa_new"] = " ";

                                                    remark_flag = true;
                                                    break;

                                                }
                                                else
                                                {

                                                    dtclone2.Rows[i]["h1_grade"] = " ";
                                                    dtclone2.Rows[i]["h2_grade"] = " ";
                                                    dtclone2.Rows[i]["overall"] = " ";
                                                    dtclone2.Rows[i]["creditEarned"] = " ";
                                                    dtclone2.Rows[i]["gradePoints"] = " ";
                                                    dtclone2.Rows[i]["CXG"] = " ";
                                                    dtclone2.Rows[i]["cre_total"] = " ";
                                                    dtclone2.Rows[i]["creEarn_total"] = " ";
                                                    dtclone2.Rows[i]["CXG_total"] = " ";
                                                    dtclone2.Rows[i]["sgpa"] = " ";
                                                    dtclone2.Rows[i]["outOf"] = " ";
                                                    dtclone2.Rows[i]["pass"] = " ";
                                                    dtclone2.Rows[i]["obtain"] = " ";
                                                    dtclone2.Rows[i]["sgpa_new"] = " ";


                                                    dtClone.Rows[i]["h1_grade"] = " ";
                                                    dtClone.Rows[i]["h2_grade"] = " ";
                                                    dtClone.Rows[i]["overall"] = " ";
                                                    dtClone.Rows[i]["creditEarned"] = " ";
                                                    dtClone.Rows[i]["gradePoints"] = " ";
                                                    dtClone.Rows[i]["CXG"] = " ";
                                                    dtClone.Rows[i]["cre_total"] = " ";
                                                    dtClone.Rows[i]["creEarn_total"] = " ";
                                                    dtClone.Rows[i]["CXG_total"] = " ";
                                                    dtClone.Rows[i]["sgpa"] = " ";
                                                    dtClone.Rows[i]["outOf"] = " ";
                                                    dtClone.Rows[i]["pass"] = " ";
                                                    dtClone.Rows[i]["obtain"] = " ";
                                                    dtClone.Rows[i]["sgpa_new"] = " ";

                                                    remark_flag = true;
                                                    break;
                                                }
                                            }
                                        }
                                        /////////////////////////Check for + sign in h1 and h2///////////////////
                                        int check_flg_new;//for appending data into one column
                                        check_flg_new = i;

                                        if (dt.Rows[i]["h1"].ToString().Contains("+"))
                                        {
                                            string h1Val = dt.Rows[i]["h1"].ToString().Trim();
                                            dt.Rows[i]["h1"] = h1Val.Substring(0, h1Val.Length - 1);
                                        }

                                        if (dt.Rows[i]["h2"].ToString().Contains("+"))
                                        {
                                            string h2Val = dt.Rows[i]["h2"].ToString().Trim();
                                            dt.Rows[i]["h2"] = h2Val.Substring(0, h2Val.Length - 1);
                                        }



                                        /////////////////////////GET H1 Grade/////////////////////////////


                                        //if (dt.Rows[i]["h1_grace"] != DBNull.Value && dt.Rows[i]["h1_grace"].ToString().Trim().ToString() != "")
                                        // if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h1_grace"])))
                                        //vidula no grace
                                        //if (Convert.ToString(dt.Rows[i]["h1_grace"]).Contains("^"))
                                        //{
                                        //    dt.Rows[i]["h1"] = objcon.Add_GraceMrksResult(dt.Rows[i]["h1_grace"].ToString(), dt.Rows[i]["h1"].ToString());
                                        //}
                                        //else if (Convert.ToString(dt.Rows[i]["h1_grace"]).Contains("*"))
                                        //{

                                        //    dt.Rows[i]["h1"] = dt.Rows[i]["h1"].ToString() + Environment.NewLine + "*";
                                        //}

                                        //dt.Rows[i]["h1"] = dt.Rows[i]["h1"] + dt.Rows[i]["h1_grace"].ToString();

                                        //if (dt.Rows[i]["h1"] != DBNull.Value && dt.Rows[i]["h1"].ToString().Trim().ToString() != "")
                                        if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h1"])))
                                        {

                                            if (dt.Rows[i]["h1"].ToString().Trim() == "Ab")
                                            {
                                                arr1 = objcon.get_GradeForDeg(0, Convert.ToInt32(dt.Rows[i]["h1_out"]), Convert.ToInt32(dt.Rows[i]["h1_pass"]));
                                            }

                                            ////////Mayu
                                            //else if (dt.Rows[i]["h1"].ToString().Contains("*"))
                                            //{

                                            //    arr = objcon.get_GradeForDeg(Convert.ToInt32(dt.Rows[i]["h1"]), Convert.ToInt32(dt.Rows[i]["h1_out"]), Convert.ToInt32(dt.Rows[i]["h1_pass"]));
                                            //}
                                            else
                                            {
                                                arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dt.Rows[i]["h1"]), Convert.ToInt32(dt.Rows[i]["h1_out"]), Convert.ToInt32(dt.Rows[i]["h1_pass"]));
                                            }


                                            ///////Mayu
                                            if (check_flg == 0)
                                            {
                                                dtClone.Rows[i]["h1_grade"] = arr1[0];
                                                dtclone2.Rows[i]["h1_grade"] = arr1[0];
                                            }
                                            else
                                            {
                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {
                                                    dtClone.Rows[i - 1]["h1_grade"] = dtClone.Rows[i - 1]["h1_grade"].ToString() + Environment.NewLine + arr1[0];
                                                    dtclone2.Rows[i]["h1_grade"] = arr1[0];
                                                }
                                                else
                                                {
                                                    dtClone.Rows[i]["h1_grade"] = arr1[0];
                                                    dtclone2.Rows[i]["h1_grade"] = arr1[0];
                                                }
                                            }
                                            //=====================Th_Kt & Oth_kt count (cre_stud_academic)
                                            if (Convert.ToString(dt.Rows[i]["h1_type"]).Contains("ESE") && Convert.ToString(dtclone2.Rows[i]["h1_grade"]) == "F")
                                            { Th_Kt++; }
                                            else if (!Convert.ToString(dt.Rows[i]["h1_type"]).Contains("ESE") && Convert.ToString(dtclone2.Rows[i]["h1_grade"]) == "F")
                                            { Oth_Kt++; }
                                            //=================================
                                        }
                                        else
                                        {
                                            if (check_flg == 0)
                                            {
                                                dtClone.Rows[i]["h1_grade"] = "- -";
                                                dtclone2.Rows[i]["h1_grade"] = "- -";
                                            }
                                            else
                                            {
                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {
                                                    dtClone.Rows[i - 1]["h1_grade"] = dtClone.Rows[i - 1]["h1_grade"] + Environment.NewLine + "- -";
                                                    dtclone2.Rows[i]["h1_grade"] = "- -";
                                                }
                                                else
                                                {
                                                    dtClone.Rows[i]["h1_grade"] = "- -";
                                                    dtclone2.Rows[i]["h1_grade"] = "- -";
                                                }
                                            }

                                        }


                                        //--------mayuri------
                                        if ((dt.Rows[i]["h1"]).ToString() == "Ab")
                                        {
                                            dtClone.Rows[i]["h1"] = "AA";
                                            //dtClone.Rows[i]["h1_out"] = "AA";
                                            //dtClone.Rows[i]["h1_pass"] = "AA";
                                        }

                                        //////////////////////////GET H2 GRADE///////////////////////////
                                        //  if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h2_grace"])))
                                        //  dt.Rows[i]["h2"] = objcon.Add_GraceMrksResult(dt.Rows[i]["h2_grace"].ToString(), dt.Rows[i]["h2"].ToString());
                                        //if (dt.Rows[i]["h2"] != DBNull.Value && dt.Rows[i]["h2"].ToString().Trim().ToString() != "")
                                        if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h2"])))
                                        {
                                            //String[] arr1;
                                            if (dt.Rows[i]["h2"].ToString().Trim() == "Ab")
                                            {
                                                arr1 = objcon.get_GradeForDeg(0, Convert.ToInt32(dt.Rows[i]["h2_out"]), Convert.ToInt32(dt.Rows[i]["h2_pass"]));
                                            }
                                            else
                                            {
                                                arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dt.Rows[i]["h2"]), Convert.ToInt32(dt.Rows[i]["h2_out"]), Convert.ToInt32(dt.Rows[i]["h2_pass"]));
                                            }

                                            if (check_flg == 0)
                                            {
                                                dtClone.Rows[i]["h2_grade"] = arr1[0];
                                                dtclone2.Rows[i]["h2_grade"] = arr1[0];
                                            }
                                            else
                                            {
                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {
                                                    dtClone.Rows[i - 1]["h2_grade"] = dtClone.Rows[i - 1]["h2_grade"].ToString() + Environment.NewLine + arr1[0];
                                                    dtclone2.Rows[i]["h2_grade"] = arr1[0];
                                                }
                                                else
                                                {
                                                    dtClone.Rows[i]["h2_grade"] = arr1[0];
                                                    dtclone2.Rows[i]["h2_grade"] = arr1[0];
                                                }
                                            }
                                            //=====================Th_Kt & Oth_kt count (cre_stud_academic)
                                            if (Convert.ToString(dtclone2.Rows[i]["h2_grade"]) == "F")
                                            { Oth_Kt++; }
                                            //=================================
                                        }
                                        else
                                        {
                                            if (check_flg == 0)
                                            {
                                                dtClone.Rows[i]["h2_grade"] = "- -";
                                                dtclone2.Rows[i]["h2_grade"] = "- -";
                                            }
                                            else
                                            {
                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {
                                                    dtClone.Rows[i - 1]["h2_grade"] = dtClone.Rows[i - 1]["h2_grade"] + Environment.NewLine + "- -";
                                                    dtclone2.Rows[i]["h2_grade"] = "- -";
                                                }
                                                else
                                                {
                                                    dtClone.Rows[i]["h2_grade"] = "- -";
                                                    dtclone2.Rows[i]["h2_grade"] = "- -";
                                                }
                                            }
                                            //----mayuri----------
                                            if ((dt.Rows[i]["h2"]).ToString() == "")
                                            {
                                                dtClone.Rows[i]["h2"] = "NA";
                                                dtClone.Rows[i]["h2_out"] = "NA";
                                                dtClone.Rows[i]["h2_pass"] = "NA";
                                            }

                                        }
                                        //-------mayuri--------
                                        if ((dt.Rows[i]["h2"]).ToString() == "Ab")
                                        {
                                            dtClone.Rows[i]["h2"] = "AA";
                                            // dtClone.Rows[i]["h2_out"] = "AA";
                                            //dtClone.Rows[i]["h2_pass"] = "AA";
                                        }




                                        //////////////////////////////////////vidula_grade_end/////////////////////////////////////////////////////



                                        //if (dt.Rows[i]["h2_out"] == DBNull.Value || dt.Rows[i]["h2_out"].ToString() == "")
                                        if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h2_out"])))
                                        {
                                            dt.Rows[i]["h2_out"] = 0;
                                        }

                                        //if (dt.Rows[i]["h1_out"] == DBNull.Value || dt.Rows[i]["h1_out"].ToString() == "")
                                        if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h1_out"])))
                                        {
                                            dt.Rows[i]["h1_out"] = 0;
                                        }

                                        //if (dt.Rows[i]["h1_pass"] == DBNull.Value || dt.Rows[i]["h1_pass"].ToString() == "")
                                        if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h1_pass"])))
                                        {
                                            dt.Rows[i]["h1_pass"] = 0;
                                        }

                                        //if (dt.Rows[i]["h2_pass"] == DBNull.Value || dt.Rows[i]["h2_pass"].ToString() == "")
                                        if (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h2_pass"])))
                                        {
                                            dt.Rows[i]["h2_pass"] = 0;
                                        }


                                        ////////////////////GET OVERALL GRADE////////////////////
                                        ///////////------------mayuri---------
                                        if (Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("*"))
                                        {
                                            dtClone.Rows[i]["h1"] = dtClone.Rows[i]["h1"] + "*";
                                            dtClone.Rows[i]["obtain"] = dtClone.Rows[i]["obtain"] + "*";
                                        }
                                        else if (Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("@"))
                                        {
                                            dtClone.Rows[i]["h1"] = dtClone.Rows[i]["h1"] + "@";
                                            dtClone.Rows[i]["obtain"] = dtClone.Rows[i]["obtain"] + "@";
                                        }
                                        else if (Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("^"))
                                        {

                                            dtClone.Rows[i]["h1"] = 30;
                                            dtclone2.Rows[i]["h1"] = 30;
                                            dt.Rows[i]["h1"] = 30;


                                        }
                                        else
                                        {
                                        }
                                        string h2obt = dt.Rows[i]["h2"].ToString();
                                        string h1obt = dt.Rows[i]["h1"].ToString();


                                        int newh2;
                                        int newh1;
                                        ///-------------/////////////////////for h1 Ab---
                                        if (h1obt == "")
                                        {
                                            newh1 = 0;
                                        }
                                        else if (h1obt == "Ab")
                                        {
                                            newh1 = 0;
                                        }
                                        else
                                        {
                                            //dtClone.Rows[i]["h1"].ToString();
                                            newh1 = Convert.ToInt32(dt.Rows[i]["h1"]);
                                            // newh1 = dt.Rows[i]["h1"].ToString();
                                        }
                                        //-----------------------------for h2 ab-----------
                                        if (h2obt == "")
                                        {
                                            newh2 = 0;
                                        }
                                        else if (h2obt == "Ab")
                                        {
                                            newh2 = 0;
                                        }
                                        else
                                        {
                                            newh2 = Convert.ToInt32(dt.Rows[i]["h2"]);
                                        }




                                        outOf = Convert.ToInt32(dt.Rows[i]["h2_out"]) + Convert.ToInt32(dt.Rows[i]["h1_out"]);
                                        pass = Convert.ToInt32(dt.Rows[i]["h1_pass"]) + Convert.ToInt32(dt.Rows[i]["h2_pass"]);
                                        // obtain = Convert.ToInt32(dt.Rows[i]["h1"]) + newh2;
                                        obtain = newh1 + newh2;

                                        dtClone.Rows[i]["outOf"] = outOf;
                                        dtClone.Rows[i]["pass"] = pass;
                                        dtClone.Rows[i]["obtain"] = obtain;


                                        //////--------------student_id----------
                                        //===============student_id============

                                        student_id = (dtClone.Rows[i]["stud_id"]).ToString();


                                        //total_obtain
                                        int obt;
                                        total_obtain = total_obtain + Convert.ToInt32(dtClone.Rows[i]["obtain"]);
                                        newtotal_obtain = Convert.ToString(total_obtain);
                                        outOf_new = outOf_new + Convert.ToInt32(dtClone.Rows[i]["outOf"]);
                                        //aziz
                                        if (h1obt == "Ab" && dtClone.Rows[i]["h2"] == "NA")
                                        {
                                            dtClone.Rows[i]["obtain"] = "AA";

                                        }
                                        ////////mayuri----- For * and @ grace------///////

                                        string h1Ab, h2Ab;

                                        h1Ab = (dt.Rows[i]["h1"]).ToString();
                                        h2Ab = (dt.Rows[i]["h2"]).ToString();
                                        if (obtain < pass)
                                        {
                                            if ((Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("*")) || (Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("@")))
                                            {
                                                if ((h1Ab != "Ab") && (h2Ab != "Ab"))
                                                {
                                                    if ((Convert.ToInt32(dt.Rows[i]["h1"]) < Convert.ToInt32(dt.Rows[i]["h1_pass"])))
                                                    {
                                                        //(dtClone.Rows[i]["h1"]) = 30;
                                                        (dtclone2.Rows[i]["h1"]) = dt.Rows[i]["h1_pass"];
                                                        (dt.Rows[i]["h1"]) = dt.Rows[i]["h1_pass"];

                                                    }
                                                    if ((Convert.ToInt32(dt.Rows[i]["h2"]) < Convert.ToInt32(dt.Rows[i]["h2_pass"])))
                                                    {
                                                        //(dtClone.Rows[i]["h2"]) = 10;
                                                        (dtclone2.Rows[i]["h2"]) = dt.Rows[i]["h2_pass"];
                                                        (dt.Rows[i]["h2"]) = dt.Rows[i]["h2_pass"];
                                                    }
                                                }
                                            }

                                        }
                                        else
                                        {
                                        }

                                        /////---for h1---/////


                                        if (Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("*"))
                                        {
                                            //  dtClone.Rows[i]["h1"] = dtClone.Rows[i]["h1"] + "*";
                                            dtClone.Rows[i]["obtain"] = dtClone.Rows[i]["obtain"] + "*";
                                        }
                                        else if (Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("@"))
                                        {
                                            //dtClone.Rows[i]["h1"] = dtClone.Rows[i]["h1"] + "@";
                                            dtClone.Rows[i]["obtain"] = dtClone.Rows[i]["obtain"] + "@";
                                        }
                                        else if (Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("^"))
                                        {

                                            dtClone.Rows[i]["h1"] = 30;
                                            dtclone2.Rows[i]["h1"] = 30;
                                            dt.Rows[i]["h1"] = 30;


                                        }
                                        else
                                        {
                                        }



                                        /////---for h2---/////
                                        if (Convert.ToString(dtClone.Rows[i]["h2_grace"]).Contains("*"))
                                        {
                                            dtClone.Rows[i]["h2"] = dtClone.Rows[i]["h2"] + "*";
                                            dtClone.Rows[i]["obtain"] = dtClone.Rows[i]["obtain"] + "*";
                                        }
                                        else if (Convert.ToString(dtClone.Rows[i]["h2_grace"]).Contains("@"))
                                        {
                                            dtClone.Rows[i]["h2"] = dtClone.Rows[i]["h2"] + "@";
                                            dtClone.Rows[i]["obtain"] = dtClone.Rows[i]["obtain"] + "@";
                                        }
                                        else
                                        {
                                        }
                                        ///////////-------mayuri------------////////

                                        ///////////////overall grade

                                        if ((Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("*")) || (Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("@")))
                                        {
                                            if ((h1Ab != "Ab") && (h2Ab != "Ab"))
                                            {
                                                if ((Convert.ToInt32(dt.Rows[i]["h1"]) < Convert.ToInt32(dt.Rows[i]["h1_pass"])))
                                                {
                                                    //(dtClone.Rows[i]["h1"]) = 30;
                                                    (dtclone2.Rows[i]["h1"]) = dt.Rows[i]["h1_pass"];
                                                    (dt.Rows[i]["h1"]) = dt.Rows[i]["h1_pass"];

                                                }
                                                if ((Convert.ToInt32(dt.Rows[i]["h2"]) < Convert.ToInt32(dt.Rows[i]["h2_pass"])))
                                                {
                                                    //(dtClone.Rows[i]["h2"]) = 10;
                                                    (dtclone2.Rows[i]["h2"]) = dt.Rows[i]["h2_pass"];
                                                    (dt.Rows[i]["h2"]) = dt.Rows[i]["h2_pass"];
                                                }
                                            }
                                        }




                                        if ((!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h1"]))) && (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h2"]))))
                                        {
                                            if (check_flg == 0)
                                            {
                                                dtClone.Rows[i]["overall"] = dtClone.Rows[i]["h1_grade"];
                                                dtclone2.Rows[i]["overall"] = dtclone2.Rows[i]["h1_grade"];
                                            }
                                            else
                                            {
                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {
                                                    dtclone2.Rows[i]["overall"] = dtClone.Rows[i]["h1_grade"];
                                                    dtClone.Rows[i - 1]["overall"] = dtClone.Rows[i - 1]["overall"] + Environment.NewLine + dtClone.Rows[i]["h1_grade"];
                                                }
                                                else
                                                {
                                                    dtclone2.Rows[i]["overall"] = dtclone2.Rows[i]["h1_grade"];
                                                    dtClone.Rows[i]["overall"] = dtClone.Rows[i]["h1_grade"];
                                                }
                                            }
                                        }
                                        else if ((!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h2"]))) && (string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h1"]))))
                                        {
                                            if (check_flg == 0)
                                            {
                                                dtClone.Rows[i]["overall"] = dtClone.Rows[i]["h2_grade"];
                                                dtclone2.Rows[i]["overall"] = dtclone2.Rows[i]["h12_grade"];
                                            }
                                            else
                                            {
                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {
                                                    dtclone2.Rows[i]["overall"] = dtclone2.Rows[i]["h2_grade"];
                                                    dtClone.Rows[i - 1]["overall"] = dtClone.Rows[i - 1]["overall"] + Environment.NewLine + dtclone2.Rows[i]["h2_grade"];
                                                }
                                                else
                                                {
                                                    dtClone.Rows[i]["overall"] = dtClone.Rows[i]["h2_grade"];
                                                    dtclone2.Rows[i]["overall"] = dtclone2.Rows[i]["h2_grade"];
                                                }
                                            }
                                        }
                                        //else if (dt.Rows[i]["h1"] != DBNull.Value && dt.Rows[i]["h1"].ToString().Trim().ToString() != "" && dt.Rows[i]["h2"] != DBNull.Value && dt.Rows[i]["h1"].ToString().Trim().ToString() != "")
                                        //{
                                        else if (!string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h1"])) && !string.IsNullOrEmpty(Convert.ToString(dt.Rows[i]["h2"])))
                                        {
                                            if (Convert.ToString(dt.Rows[i]["h1"]).Contains("Ab"))
                                            {
                                                if (check_flg == 0)
                                                {
                                                    dtClone.Rows[i]["overall"] = dtClone.Rows[i]["h2"];
                                                    dtclone2.Rows[i]["overall"] = dtclone2.Rows[i]["h2"];
                                                }
                                                else
                                                {
                                                    if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                    {
                                                        dtclone2.Rows[i]["overall"] = dtclone2.Rows[i]["h2"];
                                                        dtClone.Rows[i - 1]["overall"] = dtClone.Rows[i - 1]["overall"] + Environment.NewLine + dtclone2.Rows[i]["h2"];
                                                    }
                                                    else
                                                    {
                                                        dtClone.Rows[i]["overall"] = dtClone.Rows[i]["h2"];
                                                        dtclone2.Rows[i]["overall"] = dtclone2.Rows[i]["h2"];
                                                    }
                                                }
                                            }
                                            else if (Convert.ToString(dt.Rows[i]["h2"]).Contains("Ab"))
                                            {
                                                if (check_flg == 0)
                                                {
                                                    dtClone.Rows[i]["overall"] = "F";
                                                    dtclone2.Rows[i]["overall"] = "F";
                                                }
                                                else
                                                {
                                                    if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                    {
                                                        dtclone2.Rows[i]["overall"] = "- -";
                                                        dtClone.Rows[i - 1]["overall"] = dtClone.Rows[i - 1]["overall"] + Environment.NewLine + "- -";
                                                    }
                                                    else
                                                    {
                                                        dtClone.Rows[i]["overall"] = "- -";
                                                        dtclone2.Rows[i]["overall"] = "- -";
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (check_flg == 0)
                                                {
                                                    // dtClone.Rows[i]["overall"] = Convert.ToInt32(dt.Rows[i]["h1"]) + Convert.ToInt32(dt.Rows[i]["h2"]);
                                                    dtclone2.Rows[i]["overall"] = Convert.ToInt32(dt.Rows[i]["h1"]) + Convert.ToInt32(dt.Rows[i]["h2"]);
                                                }
                                                else
                                                {
                                                    if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                    {
                                                        dtclone2.Rows[i]["overall"] = Convert.ToInt32(dt.Rows[i]["h1"]) + Convert.ToInt32(dt.Rows[i]["h2"]);
                                                        //dtClone.Rows[i - 1]["overall"] = dtClone.Rows[i - 1]["overall"].ToString() + Environment.NewLine + Convert.ToInt32(Convert.ToInt32(dt.Rows[i]["h1"]) + Convert.ToInt32(dt.Rows[i]["h2"]));
                                                    }
                                                    else
                                                    {
                                                        dtclone2.Rows[i]["overall"] = Convert.ToInt32(dt.Rows[i]["h1"]) + Convert.ToInt32(dt.Rows[i]["h2"]);
                                                        dtClone.Rows[i]["overall"] = Convert.ToInt32(dt.Rows[i]["h1"]) + Convert.ToInt32(dt.Rows[i]["h2"]);
                                                    }
                                                }
                                            }

                                            //-------------------------------------------Check for TW Fail--------------------------------------------
                                            if (dtClone.Rows[i]["h2"].ToString() == "F" && dt.Rows[i]["h2_type"].ToString().Contains("TW"))
                                            {
                                                twFail = true;
                                            }






                                            if (dtClone.Rows[i]["h2_grade"].ToString() == "F" && Convert.ToString(dt.Rows[i]["h2"]) != "Ab" && dtClone.Rows[i]["h1_grade"].ToString() != "F")
                                            {

                                                ///////check overall grade
                                                if (check_flg == 0)
                                                {
                                                    arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dtclone2.Rows[i]["overall"]), outOf, pass);
                                                    dtclone2.Rows[i]["overall"] = arr1[0];
                                                    dtClone.Rows[i]["overall"] = arr1[0];
                                                }
                                                else
                                                {
                                                    if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                    {
                                                        if (dtclone2.Rows[i]["overall"].ToString() != "- -")
                                                        {
                                                            arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dtclone2.Rows[i]["overall"]), outOf, pass);
                                                            dtclone2.Rows[i]["overall"] = arr1[0];
                                                            dtClone.Rows[i - 1]["overall"] = dtClone.Rows[i - 1]["overall"] + Environment.NewLine + arr1[0];
                                                        }
                                                    }

                                                    else
                                                    {
                                                        arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dtclone2.Rows[i]["overall"]), outOf, pass);
                                                        dtclone2.Rows[i]["overall"] = arr1[0];
                                                        dtClone.Rows[i]["overall"] = arr1[0];
                                                    }
                                                }


                                            }
                                            else if (dtclone2.Rows[i]["h2_grade"].ToString() == "F" && dtclone2.Rows[i]["h1_grade"].ToString() != "F" && dtclone2.Rows[i]["overall"].ToString() != "Ab")
                                            {
                                                if (check_flg == 0)
                                                {
                                                    //dtclone2.Rows[i]["h2"] = "RL";
                                                    //dtClone.Rows[i]["h2"] = "RL";
                                                    ////vidula_h2_fail//////
                                                    if (dtclone2.Rows[i]["overall"].ToString() != "F" && dtclone2.Rows[i]["overall"].ToString() != "Ab")
                                                    {
                                                        arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dtclone2.Rows[i]["overall"]), outOf, pass);

                                                        dtclone2.Rows[i]["overall"] = arr1[0];
                                                        dtClone.Rows[i]["overall"] = arr1[0];
                                                    }
                                                }
                                                else if (dtClone.Rows[i]["h1"] == "AA" && dtClone.Rows[i]["h2"] == "NA")
                                                {
                                                    //14040038
                                                    dtclone2.Rows[i]["overall"] = "F";
                                                    //arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dtclone2.Rows[i]["overall"]), outOf, pass);
                                                    // dtclone2.Rows[i]["overall"] = arr1[0];
                                                    dtClone.Rows[i]["overall"] = "F"; //arr1[0];


                                                }
                                                else if (dtclone2.Rows[i]["overall"].ToString() == "- -" && (dtClone.Rows[i]["h1"] == "AA" || dtClone.Rows[i]["h2"] == "AA"))
                                                {
                                                    dtclone2.Rows[i]["overall"] = "F";
                                                    //arr1 = fun.get_GradeForDeg(Convert.ToInt32(dtclone2.Rows[i]["overall"]), outOf, pass);
                                                    // dtclone2.Rows[i]["overall"] = arr1[0];
                                                    dtClone.Rows[i]["overall"] = "F"; //arr1[0];
                                                }
                                                else
                                                {//mayu
                                                    if (dtclone2.Rows[i]["overall"].ToString() != "Ab")
                                                    {
                                                        if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                        {
                                                            if (dtclone2.Rows[i]["overall"].ToString() != "- -")
                                                            {
                                                                arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dtclone2.Rows[i]["overall"]), outOf, pass);
                                                                dtclone2.Rows[i]["overall"] = arr1[0];
                                                                dtClone.Rows[i - 1]["overall"] = dtClone.Rows[i - 1]["overall"] + Environment.NewLine + arr1[0];
                                                            }

                                                            //dtclone2.Rows[i]["overall"] = "F";
                                                            //dtClone.Rows[i - 1]["overall"] = dtClone.Rows[i - 1]["overall"] + Environment.NewLine + "RL";
                                                        }
                                                        else
                                                        {


                                                            arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dtclone2.Rows[i]["overall"]), outOf, pass);
                                                            dtclone2.Rows[i]["overall"] = arr1[0];
                                                            dtClone.Rows[i]["overall"] = arr1[0];
                                                            //dtclone2.Rows[i]["overall"] = "F";
                                                            //dtClone.Rows[i]["overall"] = "RL";

                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {///////check overall grade
                                                //---mayuri
                                                if (dtclone2.Rows[i]["overall"].ToString() != "Ab")
                                                {
                                                    if (check_flg == 0)
                                                    {
                                                        if (dtclone2.Rows[i]["overall"].ToString() != "F" && dtclone2.Rows[i]["overall"].ToString() != "Ab")
                                                        {
                                                            arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dtclone2.Rows[i]["overall"]), outOf, pass);
                                                            dtclone2.Rows[i]["overall"] = arr1[0];
                                                            dtClone.Rows[i]["overall"] = arr1[0];
                                                        }

                                                    }
                                                    ////============================mauu-kru======================
                                                    //else if ((dtClone.Rows[i]["h2_grade"].ToString() == "F" && dtClone.Rows[i]["h1_grade"].ToString() != "F") || (dtClone.Rows[i]["h2_grade"].ToString() != "F" && dtClone.Rows[i]["h1_grade"].ToString() == "F"))
                                                    //{
                                                    //    dtclone2.Rows[i]["overall"] = "F";
                                                    //    dtClone.Rows[i]["overall"] = "F";
                                                    //}
                                                    else if (dtClone.Rows[i]["h1"] == "AA" || dtclone2.Rows[i]["overall"] == "- -")
                                                    {
                                                        //14040038
                                                        dtclone2.Rows[i]["overall"] = "F";
                                                        //arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dtclone2.Rows[i]["overall"]), outOf, pass);
                                                        // dtclone2.Rows[i]["overall"] = arr1[0];
                                                        dtClone.Rows[i]["overall"] = "F"; //arr1[0];


                                                    }
                                                    else
                                                    {
                                                        if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                        {
                                                            if (dtclone2.Rows[i]["overall"].ToString() != "- -")
                                                            {
                                                                arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dtclone2.Rows[i]["overall"]), outOf, pass);
                                                                dtclone2.Rows[i]["overall"] = arr1[0];
                                                                dtClone.Rows[i - 1]["overall"] = dtClone.Rows[i - 1]["overall"] + Environment.NewLine + arr1[0];
                                                            }
                                                        }
                                                        else
                                                        {
                                                            // dtclone2.Rows[i]["overall"] = "F";
                                                            arr1 = objcon.get_GradeForDeg(Convert.ToInt32(dtclone2.Rows[i]["overall"]), outOf, pass);
                                                            dtclone2.Rows[i]["overall"] = arr1[0];
                                                            dtClone.Rows[i]["overall"] = arr1[0];
                                                        }
                                                    }
                                                }
                                                if (dtclone2.Rows[i]["overall"].ToString() == "Ab")
                                                {
                                                    dtClone.Rows[i]["overall"] = "F";
                                                }

                                            }
                                        }
                                        else
                                        {
                                            if (check_flg == 0)
                                            {
                                                dtclone2.Rows[i]["overall"] = "- -";
                                                dtClone.Rows[i]["overall"] = "- -";
                                            }
                                            else
                                            {
                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {
                                                    dtclone2.Rows[i]["overall"] = "- -";
                                                    dtClone.Rows[i - 1]["overall"] = dtClone.Rows[i - 1]["overall"] + Environment.NewLine + "- -";
                                                }
                                                else
                                                {
                                                    dtclone2.Rows[i]["overall"] = "- -";
                                                    dtClone.Rows[i]["overall"] = "- -";
                                                }
                                            }
                                        }


                                        //mayuri
                                        //if (Convert.ToString(dt.Rows[i]["h1_grace"]).Contains("^"))
                                        //{
                                        //dt.Rows[i]["h1"] = objcon.Add_GraceMrksResult(dt.Rows[i]["h1_grace"].ToString(), dt.Rows[i]["h1"].ToString());
                                        //}



                                        ////-------2mauu-kru------------------
                                        if (Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("*") || Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("@") || Convert.ToString(dtClone.Rows[i]["h1_grace"]).Contains("^"))
                                        {
                                        }
                                        else
                                        {
                                            if ((dtClone.Rows[i]["h2_grade"].ToString() == "F" && dtClone.Rows[i]["h1_grade"].ToString() != "F") || (dtClone.Rows[i]["h2_grade"].ToString() != "F" && dtClone.Rows[i]["h1_grade"].ToString() == "F") || (dtClone.Rows[i]["h2_grade"].ToString() == "F" && dtClone.Rows[i]["h1_grade"].ToString() == "F"))
                                            {
                                                dtclone2.Rows[i]["overall"] = "F";
                                                dtClone.Rows[i]["overall"] = "F";
                                            }
                                            else
                                            {
                                                if ((dtClone.Rows[i]["h2_grade"].ToString() == "F" && dtClone.Rows[i]["h1_grade"].ToString() != "F") || (dtClone.Rows[i]["h2_grade"].ToString() != "F" && dtClone.Rows[i]["h1_grade"].ToString() == "F") || (dtClone.Rows[i]["h2_grade"].ToString() == "F" && dtClone.Rows[i]["h1_grade"].ToString() == "F"))
                                                {
                                                    dtclone2.Rows[i]["overall"] = "F";
                                                    dtClone.Rows[i]["overall"] = "F";
                                                }
                                                else
                                                {
                                                    //===========================Subject Grade improvement=========
                                                    string[] arrCopy = arr1;
                                                    if (isfail == false && GotGrace == false && subGrade <= 2)
                                                    {
                                                        arr1 = objcon.imp_subjGrade(dtClone.Rows[i]["obtain"].ToString(), dtClone.Rows[i]["outOf"].ToString(), arr1);
                                                        if (arrCopy[0] != arr1[0])
                                                        {
                                                            dtClone.Rows[i]["obtain"] = dtClone.Rows[i]["obtain"].ToString() + "@";
                                                            dtclone2.Rows[i]["overall"] = arr1[0];
                                                            dtClone.Rows[i]["overall"] = arr1[0];
                                                            subGrade++;
                                                            //improv=true;
                                                        }
                                                    }
                                                }
                                            }
                                        }


                                        if (dtClone.Rows[i]["h1"].ToString().Contains("+"))
                                        {
                                            string h1Val = dtClone.Rows[i]["h1"].ToString().Trim();
                                            dtClone.Rows[i]["h1"] = h1Val.Substring(0, h1Val.Length - 1);
                                        }

                                        if (dtClone.Rows[i]["h2"].ToString().Contains("+"))
                                        {
                                            string h2Val = dtClone.Rows[i]["h2"].ToString().Trim();
                                            dtClone.Rows[i]["h2"] = h2Val.Substring(0, h2Val.Length - 1);
                                        }

                                        //////////////////CREDITS EARNED//////////////////
                                        //if (dtClone.Rows[i]["h2"].ToString() == "F" || dtClone.Rows[i]["h1"].ToString() == "F")
                                        if (dtclone2.Rows[i]["overall"].ToString() != "- -" || dtclone2.Rows[i]["overall"].ToString() != "F")
                                        {
                                            if (check_flg == 0)
                                            {
                                                dtclone2.Rows[i]["creditEarned"] = Convert.ToDouble(dtclone2.Rows[i]["credit"]);
                                                dtClone.Rows[i]["creditEarned"] = Convert.ToDouble(dtclone2.Rows[i]["credit"]);
                                            }
                                            else
                                            {



                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {
                                                    dtclone2.Rows[i]["creditEarned"] = Convert.ToDouble(dtClone.Rows[i]["credit"]);
                                                    dtClone.Rows[i - 1]["creditEarned"] = dtClone.Rows[i - 1]["creditEarned"] + Environment.NewLine + Convert.ToDouble(dtclone2.Rows[i]["credit"]);
                                                }
                                                else
                                                {
                                                    dtclone2.Rows[i]["creditEarned"] = Convert.ToDouble(dtclone2.Rows[i]["credit"]);
                                                    dtClone.Rows[i]["creditEarned"] = Convert.ToDouble(dtclone2.Rows[i]["credit"]);
                                                }
                                            }

                                        }

                                        ////////////////GRADE POINTS////////////////////
                                        if (dtclone2.Rows[i]["creditEarned"].ToString() == "- -")
                                        {
                                            if (check_flg == 0)
                                            {
                                                dtclone2.Rows[i]["gradePoints"] = "- -";
                                                dtClone.Rows[i]["gradePoints"] = "- -";
                                            }
                                            else
                                            {
                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {
                                                    dtclone2.Rows[i]["gradePoints"] = "- -";
                                                    dtClone.Rows[i - 1]["gradePoints"] = dtClone.Rows[i - 1]["gradePoints"] + Environment.NewLine + "- -";
                                                }
                                                else
                                                {
                                                    dtclone2.Rows[i]["gradePoints"] = "- -";
                                                    dtClone.Rows[i]["gradePoints"] = "- -";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (check_flg == 0)
                                            {
                                                dtclone2.Rows[i]["gradePoints"] = objcon.getPtsFrmGrade(dtclone2.Rows[i]["overall"].ToString());
                                                dtClone.Rows[i]["gradePoints"] = objcon.getPtsFrmGrade(dtClone.Rows[i]["overall"].ToString());
                                            }
                                            else
                                            {
                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {

                                                    dtclone2.Rows[i]["gradePoints"] = objcon.getPtsFrmGrade(dtclone2.Rows[i]["overall"].ToString());
                                                    dtClone.Rows[i - 1]["gradePoints"] = dtClone.Rows[i - 1]["gradePoints"] + Environment.NewLine + objcon.getPtsFrmGrade(dtclone2.Rows[i]["overall"].ToString());

                                                }
                                                else
                                                {
                                                    dtclone2.Rows[i]["gradePoints"] = objcon.getPtsFrmGrade(dtclone2.Rows[i]["overall"].ToString());
                                                    dtClone.Rows[i]["gradePoints"] = objcon.getPtsFrmGrade(dtClone.Rows[i]["overall"].ToString());
                                                }
                                            }
                                        }
                                        //////////////////C X G///////////////////////////
                                        if (dtclone2.Rows[i]["gradePoints"].ToString() == "- -" && dtclone2.Rows[i]["creditEarned"].ToString() == "- -")
                                        {
                                            if (check_flg == 0)
                                            {
                                                dtclone2.Rows[i]["CXG"] = "- -";
                                                dtClone.Rows[i]["CXG"] = "- -";
                                            }
                                            else
                                            {
                                                if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                {
                                                    dtclone2.Rows[i]["CXG"] = "- -";
                                                    dtClone.Rows[i - 1]["CXG"] = dtClone.Rows[i - 1]["CXG"] + Environment.NewLine + "- -";
                                                }
                                                else
                                                {
                                                    dtclone2.Rows[i]["CXG"] = "- -";
                                                    dtClone.Rows[i]["CXG"] = "- -";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //sandeep 27Dec
                                            if (dtclone2.Rows[i]["overall"].ToString() == "F")
                                            {
                                                dtclone2.Rows[i]["CXG"] = "F";
                                                dtClone.Rows[i]["CXG"] = "F";
                                            }
                                            else
                                            {
                                                if (check_flg == 0)
                                                {
                                                    dtclone2.Rows[i]["CXG"] = Convert.ToDouble(dtclone2.Rows[i]["creditEarned"]) * Convert.ToDouble(dtclone2.Rows[i]["gradePoints"]);
                                                    dtClone.Rows[i]["CXG"] = Convert.ToDouble(dtClone.Rows[i]["creditEarned"]) * Convert.ToDouble(dtClone.Rows[i]["gradePoints"]);
                                                }
                                                else
                                                {
                                                    if (dtClone.Rows[i - 1]["subject_name"].ToString() == dtClone.Rows[i]["subject_name"].ToString())
                                                    {
                                                        // dtClone.Rows[i-1]["CXG"] =dtClone.Rows[i-1]["CXG"] + Environment.NewLine + Convert.ToDouble(dtClone.Rows[i-1]["creditEarned"]) * Convert.ToDouble(dtClone.Rows[i-1]["gradePoints"]);
                                                        dtclone2.Rows[i]["CXG"] = Convert.ToDouble(dtclone2.Rows[i]["creditEarned"]) * Convert.ToDouble(dtclone2.Rows[i]["gradePoints"]);
                                                        dtClone.Rows[i - 1]["CXG"] = dtClone.Rows[i - 1]["CXG"] + Environment.NewLine + Convert.ToDouble(dtclone2.Rows[i]["creditEarned"]) * Convert.ToDouble(dtclone2.Rows[i]["gradePoints"]);
                                                    }
                                                    else
                                                    {
                                                        dtclone2.Rows[i]["CXG"] = Convert.ToDouble(dtclone2.Rows[i]["creditEarned"]) * Convert.ToDouble(dtclone2.Rows[i]["gradePoints"]);
                                                        dtClone.Rows[i]["CXG"] = Convert.ToDouble(dtClone.Rows[i]["creditEarned"]) * Convert.ToDouble(dtClone.Rows[i]["gradePoints"]);
                                                    }
                                                }
                                            }
                                        }


                                        ////////////////////credit total/////////////////
                                        if (dtclone2.Rows[i]["credit"].ToString() == "- -")
                                        {
                                            changedVal = 0;
                                            cre_total = cre_total + changedVal;
                                        }
                                        else
                                        {
                                            cre_total = cre_total + Convert.ToDouble(dtclone2.Rows[i]["credit"]);
                                        }
                                        ///////////////////creditEarned total////////////////
                                        if (dtclone2.Rows[i]["creditEarned"].ToString() == "- -")
                                        {
                                            changedVal = 0;
                                            creEarn_total = creEarn_total + changedVal;
                                            //creEarn_total_new = creEarn_total + changedVal;
                                        }
                                        else if (dtclone2.Rows[i]["overall"].ToString() == "F")
                                        {
                                            changedVal = 0;
                                            creEarn_total = creEarn_total + changedVal;
                                        }
                                        else if (dtclone2.Rows[i]["h2_grade"].ToString() == "F")
                                        {
                                            changedVal = 0;
                                            creEarn_total = creEarn_total + changedVal;
                                        }
                                        else
                                        {
                                            creEarn_total = creEarn_total + Convert.ToDouble(dtclone2.Rows[i]["creditEarned"]);
                                            //creEarn_total_new = creEarn_total + Convert.ToDouble(dtclone2.Rows[i]["creditEarned"]);
                                        }

                                        ////sandeep 27Dec
                                        //if (dtclone2.Rows[i]["overall"].ToString() != "F")
                                        //{
                                        //    changedVal = 0;
                                        //    if (CXG_total == "")
                                        //    {
                                        //        CXG_total = Convert.ToString(Convert.ToDouble(dtclone2.Rows[i]["CXG"]));
                                        //    }
                                        //    else
                                        //    {
                                        //        CXG_total = Convert.ToString(Convert.ToDouble(CXG_total) + Convert.ToDouble(dtclone2.Rows[i]["CXG"]));
                                        //    }
                                        //}




                                        //////////////////CXG_total////////////////////////
                                        if (dtclone2.Rows[i]["overall"].ToString() == "F")
                                        {

                                            changedVal = 0;
                                            CXG_total1 = CXG_total1 + changedVal;
                                            CXG_total = CXG_total1.ToString();
                                            //objcon.DMLquerries("Update cre_marks_tbl set h3=0 where stud_id='" + dt_all.Rows[k]["stud_id"].ToString() + "' and ayid='" + cmbYear.SelectedValue.ToString() + "' and subject_id='" + dt.Rows[i]["subject_id"] + "' and exam_code='" + cmbExam.SelectedValue.ToString() + "' and credit_sub_id='" + dt.Rows[i]["credit_sub_id"] + "'");

                                        }
                                        else if (dtclone2.Rows[i]["CXG"].ToString() == "F")
                                        {
                                            changedVal = 0;
                                            CXG_total1 = CXG_total1 + changedVal;
                                            CXG_total = CXG_total1.ToString();
                                        }
                                        else
                                        {

                                            CXG_total1 = CXG_total1 + Convert.ToDouble(dtclone2.Rows[i]["CXG"]);
                                            CXG_total = CXG_total1.ToString();
                                            //objcon.DMLquerries("Update cre_marks_tbl set h3='" + dtclone2.Rows[i]["CXG"] + "' where stud_id='" + dt_all.Rows[k]["stud_id"].ToString() + "' and ayid='" + cmbYear.SelectedValue.ToString() + "' and subject_id='" + dt.Rows[i]["subject_id"] + "' and exam_code='" + cmbExam.SelectedValue.ToString() + "' and credit_sub_id='" + dt.Rows[i]["credit_sub_id"] + "'");

                                        }



                                        ////////////////////Remark////////////////////
                                        //if (dtClone.Rows[i]["remark"].ToString().ToUpper() == "UNSUCCESSFUL")
                                        if (dtclone2.Rows[i]["overall"].ToString() == "F" || dtclone2.Rows[i]["overall"].ToString() == "- -")
                                        {
                                            remark_flag = true;
                                        }

                                        credit = credit + Convert.ToDouble(dtClone.Rows[i]["credit"]);
                                    }
                                    int dtclone_count = dtClone.Rows.Count;
                                    for (int v = 0; v < dtclone_count; v++)
                                    {
                                        if (v != 0)
                                        {
                                            if (dtClone.Rows[v - 1]["subject_name"].ToString() == dtClone.Rows[v]["subject_name"].ToString())
                                            {
                                                dtClone.Rows.RemoveAt(v);
                                                dtclone_count = dtclone_count - 1;
                                            }
                                            else
                                            {
                                            }
                                        }
                                    }

                                    SGPA = CXG_total1 / creEarn_total;
                                    if (SGPA.ToString() == "NaN")
                                    {
                                        SGPA = 0.0;
                                    }

                                    Decimal SGPA1 = 0;
                                    string cgpi2 = "", sgpa = "";
                                    if (remark_flag == true)
                                    {
                                        remark = "Unsuccessful";
                                        sgpa = "NA";
                                        //string cgpi2 = "";
                                        //if (SGPA > 0)
                                        //{
                                        //    SGPA1 = Math.Round((decimal)SGPA, 2);//--------for others
                                        //    sgpa = SGPA1.ToString();

                                        //}
                                        //else
                                        //{
                                        //    SGPA1 = 0;
                                        //    sgpa = "NA";
                                        //}

                                    }
                                    else
                                    {
                                        remark = "Successful";

                                        if (SGPA > 0)
                                        {
                                            SGPA1 = Math.Round((decimal)SGPA, 2);//--------for others
                                            sgpa = SGPA1.ToString();
                                        }
                                        else
                                        {
                                            SGPA1 = 0;
                                        }

                                    }

                                    string s1 = "NA", s2 = "NA", s3 = "NA", s4 = "NA";
                                    string a1 = "";
                                    if (dt_creditEarned.Rows.Count > 0)
                                    {
                                        if (dt_req.Rows[0]["sem_id"].ToString() == "Sem-1")
                                        {
                                            if (!string.IsNullOrEmpty(Convert.ToString(dt_creditEarned.Rows[0]["sem1_credit_earn"])))
                                            {
                                                s1 = Convert.ToString(dt_creditEarned.Rows[0]["sem1_credit_earn"]);
                                                a1 = Convert.ToString(dt_creditEarned.Rows[0]["sem1_credit_earn"]);
                                            }
                                            else
                                            {
                                                s1 = "";
                                                a1 = "";
                                            }
                                        }
                                        else if (dt_req.Rows[0]["sem_id"].ToString() == "Sem-2")
                                        {
                                            if (!string.IsNullOrEmpty(Convert.ToString(dt_creditEarned.Rows[0]["sem1_credit_earn"])))
                                            {
                                                s1 = Convert.ToString(dt_creditEarned.Rows[0]["sem1_credit_earn"]);
                                            }
                                            else
                                            {
                                                s1 = "";
                                            }
                                            if (!string.IsNullOrEmpty(Convert.ToString(dt_creditEarned.Rows[0]["sem2_credit_earn"])))
                                            {
                                                s2 = Convert.ToString(dt_creditEarned.Rows[0]["sem2_credit_earn"]);
                                                a1 = Convert.ToString(dt_creditEarned.Rows[0]["sem2_credit_earn"]);
                                            }
                                            else
                                            {
                                                s2 = "";
                                                a1 = "";
                                            }
                                        }
                                        else if (dt_req.Rows[0]["sem_id"].ToString() == "Sem-3")
                                        {
                                            if (!string.IsNullOrEmpty(Convert.ToString(dt_creditEarned.Rows[0]["sem1_credit_earn"])))
                                            {
                                                s1 = Convert.ToString(dt_creditEarned.Rows[0]["sem1_credit_earn"]);
                                            }
                                            else
                                            {
                                                s1 = "";
                                            }
                                            if (!string.IsNullOrEmpty(Convert.ToString(dt_creditEarned.Rows[0]["sem2_credit_earn"])))
                                            {
                                                s2 = Convert.ToString(dt_creditEarned.Rows[0]["sem2_credit_earn"]);
                                            }
                                            else
                                            {
                                                s2 = "";
                                            }
                                            if (!string.IsNullOrEmpty(Convert.ToString(dt_creditEarned.Rows[0]["sem3_credit_earn"])))
                                            {
                                                s3 = Convert.ToString(dt_creditEarned.Rows[0]["sem3_credit_earn"]);
                                                a1 = Convert.ToString(dt_creditEarned.Rows[0]["sem3_credit_earn"]);
                                            }
                                            else
                                            {
                                                s3 = "";
                                                a1 = "";
                                            }
                                        }
                                        else if (dt_req.Rows[0]["sem_id"].ToString() == "Sem-4")
                                        {
                                            if (!string.IsNullOrEmpty(Convert.ToString(dt_creditEarned.Rows[0]["sem1_credit_earn"])))
                                            {
                                                s1 = Convert.ToString(dt_creditEarned.Rows[0]["sem1_credit_earn"]);
                                            }
                                            else
                                            {
                                                s1 = "";
                                            }
                                            if (!string.IsNullOrEmpty(Convert.ToString(dt_creditEarned.Rows[0]["sem2_credit_earn"])))
                                            {
                                                s2 = Convert.ToString(dt_creditEarned.Rows[0]["sem2_credit_earn"]);
                                            }
                                            else
                                            {
                                                s2 = "";
                                            }
                                            if (!string.IsNullOrEmpty(Convert.ToString(dt_creditEarned.Rows[0]["sem3_credit_earn"])))
                                            {
                                                s3 = Convert.ToString(dt_creditEarned.Rows[0]["sem3_credit_earn"]);
                                            }
                                            else
                                            {
                                                s3 = "";
                                            }
                                            if (!string.IsNullOrEmpty(Convert.ToString(dt_creditEarned.Rows[0]["sem4_credit_earn"])))
                                            {
                                                s4 = Convert.ToString(dt_creditEarned.Rows[0]["sem4_credit_earn"]);
                                                a1 = Convert.ToString(dt_creditEarned.Rows[0]["sem4_credit_earn"]);
                                            }
                                            else
                                            {
                                                s4 = "";
                                                a1 = "";
                                            }
                                        }
                                    }
                                    //----------------grade_sgpa--------------//
                                    string grade_sgpa;
                                    if (sgpa != "NA")
                                    {

                                        if (Convert.ToDouble(sgpa) >= 1 && Convert.ToDouble(sgpa) <= 1.99)
                                        {
                                            grade_sgpa = "F";
                                        }
                                        else if (Convert.ToDouble(sgpa) >= 2 && Convert.ToDouble(sgpa) <= 2.99 && remark == "Successful")
                                        {
                                            grade_sgpa = "E";
                                        }
                                        else if (Convert.ToDouble(sgpa) >= 3 && Convert.ToDouble(sgpa) <= 3.99 && remark == "Successful")
                                        {
                                            grade_sgpa = "D";
                                        }
                                        else if (Convert.ToDouble(sgpa) >= 4 && Convert.ToDouble(sgpa) <= 4.99 && remark == "Successful")
                                        {
                                            grade_sgpa = "C";
                                        }
                                        else if (Convert.ToDouble(sgpa) >= 5 && Convert.ToDouble(sgpa) <= 5.99 && remark == "Successful")
                                        {
                                            grade_sgpa = "B";
                                        }
                                        else if (Convert.ToDouble(sgpa) >= 6 && Convert.ToDouble(sgpa) <= 6.99 && remark == "Successful")
                                        {
                                            grade_sgpa = "A";
                                        }
                                        else if (Convert.ToDouble(sgpa) >= 7 && remark == "Successful")
                                        {
                                            grade_sgpa = "O";
                                        }
                                        else
                                        {
                                            grade_sgpa = "F";
                                        }
                                    }
                                    else
                                    {
                                        grade_sgpa = "NA";
                                    }



                                    ////////////////////overall grade improment
                                    int flag = 1;

                                    if ((grade_sgpa == "B" || grade_sgpa == "A")) //&& )
                                    {
                                        string newSGPI = Convert.ToString(Math.Round(((CXG_total1 + 1.0) / cre_total), 2));
                                        string newGrade = objcon.Total_Grade(newSGPI, flag);

                                        if (newGrade != grade_sgpa)
                                        {
                                            sgpa = newSGPI;
                                            grade_sgpa = newGrade;
                                            //Totgp = cxgTotal + 1;
                                            // Totgp = cxgTotal;
                                            //if (gotNss)
                                            //{

                                            CXG_total = CXG_total1.ToString() + "@1"; //===========Total      
                                            //}
                                            //else
                                            //{
                                            //===========Total      
                                            //}
                                        }
                                    }





                                    //====================================
                                    for (int j = 0; j < dtClone.Rows.Count; j++)
                                    {
                                        dtClone.Rows[j]["cre_total"] = cre_total;
                                        dtClone.Rows[j]["creEarn_total"] = creEarn_total;
                                        dtClone.Rows[j]["creEarn_total_new"] = creEarn_total;
                                        dtClone.Rows[j]["CXG_total"] = CXG_total;
                                        dtClone.Rows[j]["CXG_total_new"] = CXG_total;
                                        // dtClone.Rows[j]["sgpa"] = sgpa;
                                        dtClone.Rows[j]["remark"] = remark.ToUpper();
                                        dtClone.Rows[j]["cgpi"] = cgpi2;
                                        //dtClone.Rows[j]["outOf"] = outOf;
                                        //dtClone.Rows[j]["pass"] = pass;
                                        //dtClone.Rows[j]["obtain"] = obtain;
                                        dtClone.Rows[j]["sem1_credit_earn"] = s1;
                                        dtClone.Rows[j]["sem2_credit_earn"] = s2;
                                        dtClone.Rows[j]["sem3_credit_earn"] = s3;
                                        dtClone.Rows[j]["sem4_credit_earn"] = s4;
                                        dtClone.Rows[j]["Credit_earn"] = creEarn_total;
                                        dtClone.Rows[j]["sgpa_new"] = sgpa;
                                        dtClone.Rows[j]["total_obtain"] = total_obtain;
                                        // dtClone.Rows[j]["total_obtain"] = newtotal_obtain;
                                        dtClone.Rows[j]["grade_sgpa"] = grade_sgpa;
                                        dtClone.Rows[j]["outOf_new"] = outOf_new;
                                        dtClone.Rows[j]["stud_id"] = student_id;



                                    }
                                    dtClone.Rows[0]["sgpa"] = sgpa;
                                    objcon.addRows(tblResult, dtClone);
                                    lblgrade.Text = dtClone.Rows[0]["grade_sgpa"].ToString();
                                    lblTotalCXG.Text = dtClone.Rows[0]["CXG_total_new"].ToString();
                                    lblTotal.Text = dtClone.Rows[0]["outOf_new"].ToString();
                                    lblTotalObt.Text = dtClone.Rows[0]["total_obtain"].ToString();
                                    lbl_Remark.Text = dtClone.Rows[0]["remark"].ToString();
                                    lblCreditsEarned.Text = dtClone.Rows[0]["Credit_earn"].ToString();
                                    lblSummision.Text = dtClone.Rows[0]["creEarn_total"].ToString();
                                    lblSumCG.Text = dtClone.Rows[0]["CXG_total"].ToString();
                                    lblSGPA.Text = dtClone.Rows[0]["sgpa_new"].ToString();
                                    if (lbl_Remark.Text.ToUpper().Equals("UNSUCCESSFUL"))
                                    {
                                        divResult.Attributes.Add("class", "panel panel-danger");
                                    }
                                    divResult.Visible = true; //change it

                                }
                            }
                        }
                        else
                        {
                            divMarks.Visible = false;
                            message.InnerText = "Your Result is Not Declared Yet!";
                            message.Visible = true;
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("log_out.aspx");
        }
    }
    protected void ddlSem_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSem.SelectedIndex > 0)
        {
            string qry = "SELECT distinct exam_date+' '+ case atkt_exam when 1 then case when e.exam_code like 'E%' then '(A.T.K.T)' else '(Reval A.T.K.T)' end when 2 then case when e.exam_code like 'E%' then '(Additional)' else '(Reval Additional)' end  else  case  when e.exam_code like 'E%' then '(Regular)' else '(Reval Regular)' end  end as a1,e.exam_code,e.curr_date FROM cre_exam e left join dbo.cre_marks_tbl m on e.exam_code=m.exam_code LEFT JOIN cre_webresult a on m.exam_code = a.exam_code where m.stud_id='" + Session["UserName"].ToString() + "' and a.declare_flag = 1 and m.sem_id='" + ddlSem.SelectedValue.ToString() + "' and a.sem_id='" + ddlSem.SelectedValue.ToString() + "'  order by e.curr_date";
            c1.filterExam(ddlExam, qry);
        }
    }
}