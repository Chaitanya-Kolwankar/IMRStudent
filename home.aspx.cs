using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class home : System.Web.UI.Page
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
                    Session["admission_done"] = false;
                    if (Convert.ToBoolean(Session["admission"]) == true)
                    {
                        DataSet dsChk = c1.fill_dataset("select ayid from m_std_studentacademic_tbl where ayid=(select MAX(ayid) from dbo.m_academic) AND stud_id='" + Session["UserName"].ToString() + "' and del_flag=0");
                        if (dsChk.Tables[0].Rows.Count > 0)
                        {
                            Session["admission_done"] = true;
                            div_admission.Visible = false;
                        }
                        else
                        {
                            div_admission.Visible = true;
                        }
                    }
                    else
                    {
                        div_admission.Visible = false;
                    }
                    getGroup();   //--Making change from vishal to Imr
                    notice();
                    noticePrincipal();
                    noticeOffice();
                    noticeStaff();
                    //cls_announce();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("log_out.aspx");
            }
        }
    }

    private void getGroup()
    {
        //string s1 = "select course_name,Group_title,Roll_no,division from www_student_personal_Vishal where student_id='" + Session["UserName"] + "' and ayid=(select max(ayid) from www_student_personal_Vishal where student_id='" + Session["UserName"] + "') ";
        //DataSet ds1 = c1.fill_dataset(s1);
        //Session["Group_title"] = ds1.Tables[0].Rows[0]["Group_title"].ToString();
        //Session["course_name"] = ds1.Tables[0].Rows[0]["course_name"].ToString();

        string s1 = "select a.course_name,b.course_id,Group_title,Roll_no,division from www_student_personal_imr a,m_crs_course_tbl b where "
  + " student_id='" + Session["UserName"] + "' and a.course_name=b.course_name and a.ayid=(select max(ayid) from www_student_personal_imr  where student_id='" + Session["UserName"] + "') ";
        DataSet ds1 = c1.fill_dataset(s1);
        Session["Group_title"] = ds1.Tables[0].Rows[0]["Group_title"].ToString();
        Session["course_name"] = ds1.Tables[0].Rows[0]["course_name"].ToString();
        Session["course_id"] = ds1.Tables[0].Rows[0]["course_id"].ToString();
    }

    public void notice()
    {

        DataSet ds_notice = c1.fill_dataset("select b.* from dbo.m_std_studentacademic_tbl a left join  dbo.Assignments b on a.group_id=b.group_id left join m_crs_subjectgroup_tbl c on c.Group_id = b.group_id where c.Group_title = '" + Session["Group_title"] + "' and stud_id='" + Session["UserName"] + "' and (b.user_id not in ('OFFICE','PRINCIPAL','STAFF') or b.user_id is null)     and b.ayid in (select MAX(ayid) from dbo.m_academic) and b.del_flag = 0");
        if (ds_notice.Tables[0].Rows.Count > 0)
        {
            DateTime now = DateTime.Now;
            string actual = "";
            TimeSpan time;
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("Id");
            dtNew.Columns.Add("title");
            dtNew.Columns.Add("time");

            for (int i = 0; i < ds_notice.Tables[0].Rows.Count; i++)
            {
                DateTime notice_time = Convert.ToDateTime(ds_notice.Tables[0].Rows[i]["curr_dt"]);
                time = now.Subtract(notice_time);
                if (time.Days <= 0)
                {
                    if (time.Hours <= 0)
                    {
                        if (time.Minutes <= 0)
                        {
                            if (time.Seconds <= 0)
                            {
                                actual = "Just Now";
                            }
                            else
                            {
                                actual = time.Seconds + " seconds ago";
                            }
                        }
                        else
                        {
                            actual = time.Minutes + " minutes ago";
                        }
                    }
                    else
                    {
                        actual = time.Hours + " hours ago";
                    }
                }
                else
                {
                    if (time.Days >= 10)
                    {
                        actual = notice_time.ToString("dd MMM");
                    }
                    else
                    {
                        actual = time.Days + " days ago";
                    }
                }

                dtNew.Rows.Add();
                dtNew.Rows[i]["Id"] = ds_notice.Tables[0].Rows[i]["Id"].ToString();
                dtNew.Rows[i]["title"] = ds_notice.Tables[0].Rows[i]["topic"].ToString();
                dtNew.Rows[i]["time"] = actual;


                //HtmlAnchor list_a = new HtmlAnchor();
                //list_a.Attributes.Add("class", "list-group-item");
                //list_a.HRef = "#";//notice links here

                //LinkButton lnk = new LinkButton();
                //lnk.ID = ds_notice.Tables[0].Rows[i]["id"].ToString();
                //lnk.Controls.Add(new LiteralControl("<div class=panel panel-default> <i class=" + "fa fa-envelope fa-fw" + "></i>" + ds_notice.Tables[0].Rows[i]["topic"].ToString()));
                //lnk.Controls.Add(new LiteralControl("<span class=" + "pull-right text-muted small" + "><em>"));
                //lnk.Controls.Add(new LiteralControl(actual + "</em></span></div>"));
                //lnk.CommandArgument = ds_notice.Tables[0].Rows[i]["id"].ToString();
                //lnk.CommandName += new CommandEventHandler(lnk_Command);

                //list_a.Controls.Add(new LiteralControl("<i class=" + "fa fa-envelope fa-fw" + "></i>" + ds_notice.Tables[0].Rows[i]["topic"].ToString()));
                //list_a.Controls.Add(new LiteralControl("<span class=" + "pull-right text-muted small" + "><em>"));
                //list_a.Controls.Add(new LiteralControl(actual + "</em></span> <br/>"));
                //list_notice.Controls.Add(list_a);
                ////list_notice.Controls.Add(lnk);
                //if (ds_notice.Tables[0].Rows[i]["topic"].ToString().Contains("Result Declared"))
                //{
                //    list_a.HRef = "Result.aspx";
                //}
                //if (!string.IsNullOrEmpty(Convert.ToString(ds_notice.Tables[0].Rows[i]["FileName"])))
                //{
                //    list_a.HRef = "Notice.aspx";
                //}
            }
            GridView1.DataSource = dtNew;
            GridView1.DataBind();
            //GridView1.Enabled = true;
        }
        else
        {
            HtmlAnchor list_a = new HtmlAnchor();
            list_a.Attributes.Add("class", "list-group-item");
            list_a.HRef = "#";//notice links here
            list_a.Controls.Add(new LiteralControl("<i class=" + "fa fa-envelope fa-fw" + "></i> No New Notifications Yet!"));
            list_a.Controls.Add(new LiteralControl("<span class=" + "pull-right text-muted small" + "><em>"));
            list_a.Controls.Add(new LiteralControl("</em></span>"));
            list_notice.Controls.Add(list_a);
        }
    }

    public void noticePrincipal()
    {

        DataSet ds_notice = c1.fill_dataset("select b.* from dbo.m_std_studentacademic_tbl a left join  dbo.Assignments b on a.group_id=b.group_id left join m_crs_subjectgroup_tbl c on c.Group_id = b.group_id where c.Group_title = '" + Session["Group_title"] + "' and stud_id='" + Session["UserName"] + "' and b.user_id = 'PRINCIPAL' and b.ayid in (select MAX(ayid) from dbo.m_academic) and b.del_flag = 0");
        if (ds_notice.Tables[0].Rows.Count > 0)
        {
            DateTime now = DateTime.Now;
            string actual = "";
            TimeSpan time;
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("Id");
            dtNew.Columns.Add("title");
            dtNew.Columns.Add("time");

            for (int i = 0; i < ds_notice.Tables[0].Rows.Count; i++)
            {
                DateTime notice_time = Convert.ToDateTime(ds_notice.Tables[0].Rows[i]["curr_dt"]);
                time = now.Subtract(notice_time);
                if (time.Days <= 0)
                {
                    if (time.Hours <= 0)
                    {
                        if (time.Minutes <= 0)
                        {
                            if (time.Seconds <= 0)
                            {
                                actual = "Just Now";
                            }
                            else
                            {
                                actual = time.Seconds + " seconds ago";
                            }
                        }
                        else
                        {
                            actual = time.Minutes + " minutes ago";
                        }
                    }
                    else
                    {
                        actual = time.Hours + " hours ago";
                    }
                }
                else
                {
                    if (time.Days >= 10)
                    {
                        actual = notice_time.ToString("dd MMM");
                    }
                    else
                    {
                        actual = time.Days + " days ago";
                    }
                }

                dtNew.Rows.Add();
                dtNew.Rows[i]["Id"] = ds_notice.Tables[0].Rows[i]["Id"].ToString();
                dtNew.Rows[i]["title"] = ds_notice.Tables[0].Rows[i]["topic"].ToString();
                dtNew.Rows[i]["time"] = actual;


                //HtmlAnchor list_a = new HtmlAnchor();
                //list_a.Attributes.Add("class", "list-group-item");
                //list_a.HRef = "#";//notice links here

                //LinkButton lnk = new LinkButton();
                //lnk.ID = ds_notice.Tables[0].Rows[i]["id"].ToString();
                //lnk.Controls.Add(new LiteralControl("<div class=panel panel-default> <i class=" + "fa fa-envelope fa-fw" + "></i>" + ds_notice.Tables[0].Rows[i]["topic"].ToString()));
                //lnk.Controls.Add(new LiteralControl("<span class=" + "pull-right text-muted small" + "><em>"));
                //lnk.Controls.Add(new LiteralControl(actual + "</em></span></div>"));
                //lnk.CommandArgument = ds_notice.Tables[0].Rows[i]["id"].ToString();
                //lnk.CommandName += new CommandEventHandler(lnk_Command);

                //list_a.Controls.Add(new LiteralControl("<i class=" + "fa fa-envelope fa-fw" + "></i>" + ds_notice.Tables[0].Rows[i]["topic"].ToString()));
                //list_a.Controls.Add(new LiteralControl("<span class=" + "pull-right text-muted small" + "><em>"));
                //list_a.Controls.Add(new LiteralControl(actual + "</em></span> <br/>"));
                //list_notice.Controls.Add(list_a);
                ////list_notice.Controls.Add(lnk);
                //if (ds_notice.Tables[0].Rows[i]["topic"].ToString().Contains("Result Declared"))
                //{
                //    list_a.HRef = "Result.aspx";
                //}
                //if (!string.IsNullOrEmpty(Convert.ToString(ds_notice.Tables[0].Rows[i]["FileName"])))
                //{
                //    list_a.HRef = "Notice.aspx";
                //}
            }
            GridView2.DataSource = dtNew;
            GridView2.DataBind();
            //GridView1.Enabled = true;
        }
        else
        {
            HtmlAnchor list_a = new HtmlAnchor();
            list_a.Attributes.Add("class", "list-group-item");
            list_a.HRef = "#";//notice links here
            list_a.Controls.Add(new LiteralControl("<i class=" + "fa fa-envelope fa-fw" + "></i> No New Notifications Yet!"));
            list_a.Controls.Add(new LiteralControl("<span class=" + "pull-right text-muted small" + "><em>"));
            list_a.Controls.Add(new LiteralControl("</em></span>"));
            listNoticePrincipal.Controls.Add(list_a);
        }
    }

    public void noticeOffice()
    {

        DataSet ds_notice = c1.fill_dataset("select b.* from dbo.m_std_studentacademic_tbl a left join  dbo.Assignments b on a.group_id=b.group_id left join m_crs_subjectgroup_tbl c on c.Group_id = b.group_id where c.Group_title = '" + Session["Group_title"] + "' and stud_id='" + Session["UserName"] + "' and b.user_id = 'OFFICE' and b.ayid in (select MAX(ayid) from dbo.m_academic) and b.del_flag = 0");
        if (ds_notice.Tables[0].Rows.Count > 0)
        {
            DateTime now = DateTime.Now;
            string actual = "";
            TimeSpan time;
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("Id");
            dtNew.Columns.Add("title");
            dtNew.Columns.Add("time");

            for (int i = 0; i < ds_notice.Tables[0].Rows.Count; i++)
            {
                DateTime notice_time = Convert.ToDateTime(ds_notice.Tables[0].Rows[i]["curr_dt"]);
                time = now.Subtract(notice_time);
                if (time.Days <= 0)
                {
                    if (time.Hours <= 0)
                    {
                        if (time.Minutes <= 0)
                        {
                            if (time.Seconds <= 0)
                            {
                                actual = "Just Now";
                            }
                            else
                            {
                                actual = time.Seconds + " seconds ago";
                            }
                        }
                        else
                        {
                            actual = time.Minutes + " minutes ago";
                        }
                    }
                    else
                    {
                        actual = time.Hours + " hours ago";
                    }
                }
                else
                {
                    if (time.Days >= 10)
                    {
                        actual = notice_time.ToString("dd MMM");
                    }
                    else
                    {
                        actual = time.Days + " days ago";
                    }
                }

                dtNew.Rows.Add();
                dtNew.Rows[i]["Id"] = ds_notice.Tables[0].Rows[i]["Id"].ToString();
                dtNew.Rows[i]["title"] = ds_notice.Tables[0].Rows[i]["topic"].ToString();
                dtNew.Rows[i]["time"] = actual;


                //HtmlAnchor list_a = new HtmlAnchor();
                //list_a.Attributes.Add("class", "list-group-item");
                //list_a.HRef = "#";//notice links here

                //LinkButton lnk = new LinkButton();
                //lnk.ID = ds_notice.Tables[0].Rows[i]["id"].ToString();
                //lnk.Controls.Add(new LiteralControl("<div class=panel panel-default> <i class=" + "fa fa-envelope fa-fw" + "></i>" + ds_notice.Tables[0].Rows[i]["topic"].ToString()));
                //lnk.Controls.Add(new LiteralControl("<span class=" + "pull-right text-muted small" + "><em>"));
                //lnk.Controls.Add(new LiteralControl(actual + "</em></span></div>"));
                //lnk.CommandArgument = ds_notice.Tables[0].Rows[i]["id"].ToString();
                //lnk.CommandName += new CommandEventHandler(lnk_Command);

                //list_a.Controls.Add(new LiteralControl("<i class=" + "fa fa-envelope fa-fw" + "></i>" + ds_notice.Tables[0].Rows[i]["topic"].ToString()));
                //list_a.Controls.Add(new LiteralControl("<span class=" + "pull-right text-muted small" + "><em>"));
                //list_a.Controls.Add(new LiteralControl(actual + "</em></span> <br/>"));
                //list_notice.Controls.Add(list_a);
                ////list_notice.Controls.Add(lnk);
                //if (ds_notice.Tables[0].Rows[i]["topic"].ToString().Contains("Result Declared"))
                //{
                //    list_a.HRef = "Result.aspx";
                //}
                //if (!string.IsNullOrEmpty(Convert.ToString(ds_notice.Tables[0].Rows[i]["FileName"])))
                //{
                //    list_a.HRef = "Notice.aspx";
                //}
            }
            GridView3.DataSource = dtNew;
            GridView3.DataBind();
            //GridView1.Enabled = true;
        }
        else
        {
            HtmlAnchor list_a = new HtmlAnchor();
            list_a.Attributes.Add("class", "list-group-item");
            list_a.HRef = "#";//notice links here
            list_a.Controls.Add(new LiteralControl("<i class=" + "fa fa-envelope fa-fw" + "></i> No New Notifications Yet!"));
            list_a.Controls.Add(new LiteralControl("<span class=" + "pull-right text-muted small" + "><em>"));
            list_a.Controls.Add(new LiteralControl("</em></span>"));
            listNoticeOffice.Controls.Add(list_a);
        }
    }

    public void noticeStaff()
    {

        DataSet ds_notice = c1.fill_dataset("select b.* from dbo.m_std_studentacademic_tbl a left join  dbo.Assignments b on a.group_id=b.group_id left join m_crs_subjectgroup_tbl c on c.Group_id = b.group_id where c.Group_title = '" + Session["Group_title"] + "' and stud_id='" + Session["UserName"] + "' and b.user_id = 'STAFF' and b.ayid in (select MAX(ayid) from dbo.m_academic) and b.del_flag = 0");
        if (ds_notice.Tables[0].Rows.Count > 0)
        {
            DateTime now = DateTime.Now;
            string actual = "";
            TimeSpan time;
            DataTable dtNew = new DataTable();
            dtNew.Columns.Add("Id");
            dtNew.Columns.Add("title");
            dtNew.Columns.Add("time");

            for (int i = 0; i < ds_notice.Tables[0].Rows.Count; i++)
            {
                DateTime notice_time = Convert.ToDateTime(ds_notice.Tables[0].Rows[i]["curr_dt"]);
                time = now.Subtract(notice_time);
                if (time.Days <= 0)
                {
                    if (time.Hours <= 0)
                    {
                        if (time.Minutes <= 0)
                        {
                            if (time.Seconds <= 0)
                            {
                                actual = "Just Now";
                            }
                            else
                            {
                                actual = time.Seconds + " seconds ago";
                            }
                        }
                        else
                        {
                            actual = time.Minutes + " minutes ago";
                        }
                    }
                    else
                    {
                        actual = time.Hours + " hours ago";
                    }
                }
                else
                {
                    if (time.Days >= 10)
                    {
                        actual = notice_time.ToString("dd MMM");
                    }
                    else
                    {
                        actual = time.Days + " days ago";
                    }
                }

                dtNew.Rows.Add();
                dtNew.Rows[i]["Id"] = ds_notice.Tables[0].Rows[i]["Id"].ToString();
                dtNew.Rows[i]["title"] = ds_notice.Tables[0].Rows[i]["topic"].ToString();
                dtNew.Rows[i]["time"] = actual;


                //HtmlAnchor list_a = new HtmlAnchor();
                //list_a.Attributes.Add("class", "list-group-item");
                //list_a.HRef = "#";//notice links here

                //LinkButton lnk = new LinkButton();
                //lnk.ID = ds_notice.Tables[0].Rows[i]["id"].ToString();
                //lnk.Controls.Add(new LiteralControl("<div class=panel panel-default> <i class=" + "fa fa-envelope fa-fw" + "></i>" + ds_notice.Tables[0].Rows[i]["topic"].ToString()));
                //lnk.Controls.Add(new LiteralControl("<span class=" + "pull-right text-muted small" + "><em>"));
                //lnk.Controls.Add(new LiteralControl(actual + "</em></span></div>"));
                //lnk.CommandArgument = ds_notice.Tables[0].Rows[i]["id"].ToString();
                //lnk.CommandName += new CommandEventHandler(lnk_Command);

                //list_a.Controls.Add(new LiteralControl("<i class=" + "fa fa-envelope fa-fw" + "></i>" + ds_notice.Tables[0].Rows[i]["topic"].ToString()));
                //list_a.Controls.Add(new LiteralControl("<span class=" + "pull-right text-muted small" + "><em>"));
                //list_a.Controls.Add(new LiteralControl(actual + "</em></span> <br/>"));
                //list_notice.Controls.Add(list_a);
                ////list_notice.Controls.Add(lnk);
                //if (ds_notice.Tables[0].Rows[i]["topic"].ToString().Contains("Result Declared"))
                //{
                //    list_a.HRef = "Result.aspx";
                //}
                //if (!string.IsNullOrEmpty(Convert.ToString(ds_notice.Tables[0].Rows[i]["FileName"])))
                //{
                //    list_a.HRef = "Notice.aspx";
                //}
            }
            GridView4.DataSource = dtNew;
            GridView4.DataBind();
            //GridView1.Enabled = true;
        }
        else
        {
            HtmlAnchor list_a = new HtmlAnchor();
            list_a.Attributes.Add("class", "list-group-item");
            list_a.HRef = "#";//notice links here
            list_a.Controls.Add(new LiteralControl("<i class=" + "fa fa-envelope fa-fw" + "></i> No New Notifications Yet!"));
            list_a.Controls.Add(new LiteralControl("<span class=" + "pull-right text-muted small" + "><em>"));
            list_a.Controls.Add(new LiteralControl("</em></span>"));
            listNoticeStaff.Controls.Add(list_a);
        }
    }

    protected void View(object sender, EventArgs e)
    {
        string id = ((sender as LinkButton).CommandArgument);
        DataSet ds_notice = c1.fill_dataset("select * from  dbo.Assignments where Id=" + id + "");
        if (ds_notice.Tables[0].Rows.Count > 0)
        {
            if (ds_notice.Tables[0].Rows[0]["topic"].ToString().Contains("Result Declared"))
            {
                Response.Redirect("Result.aspx");
            }
            else
            {
                if (string.IsNullOrEmpty(Convert.ToString(ds_notice.Tables[0].Rows[0]["FileName"])))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", "javascript:alert('No files to download.')", true);
                }
                else
                {
                    Byte[] bytes;
                    bytes = (byte[])(ds_notice.Tables[0].Rows[0]["FileData"]);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Charset = "";
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.ContentType = Convert.ToString(ds_notice.Tables[0].Rows[0]["FileType"]);
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + Convert.ToString(ds_notice.Tables[0].Rows[0]["FileName"]));
                    Response.BinaryWrite(bytes);
                    Response.Flush();
                    Response.End();
                }
            }

        }
        else
        {

        }
    }

    //protected void ViewPrincipal(object sender, EventArgs e)
    //{
    //    string id = ((sender as LinkButton).CommandArgument);
    //    DataSet ds_notice = c1.fill_dataset("select * from  dbo.Assignments where Id=" + id + "");
    //    if (ds_notice.Tables[0].Rows.Count > 0)
    //    {
    //        if (ds_notice.Tables[0].Rows[0]["topic"].ToString().Contains("Result Declared"))
    //        {
    //            Response.Redirect("Result.aspx");
    //        }
    //        else
    //        {
    //            if (string.IsNullOrEmpty(Convert.ToString(ds_notice.Tables[0].Rows[0]["FileName"])))
    //            {
    //                ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", "javascript:alert('No files to download.')", true);
    //            }
    //            else
    //            {
    //                Byte[] bytes;
    //                bytes = (byte[])(ds_notice.Tables[0].Rows[0]["FileData"]);
    //                Response.Clear();
    //                Response.Buffer = true;
    //                Response.Charset = "";
    //                Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //                Response.ContentType = Convert.ToString(ds_notice.Tables[0].Rows[0]["FileType"]);
    //                Response.AppendHeader("Content-Disposition", "attachment; filename=" + Convert.ToString(ds_notice.Tables[0].Rows[0]["FileName"]));
    //                Response.BinaryWrite(bytes);
    //                Response.Flush();
    //                Response.End();
    //            }
    //        }

    //    }
    //    else
    //    {

    //    }
    //}
}