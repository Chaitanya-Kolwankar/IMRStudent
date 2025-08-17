using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class studentdocument_bot : System.Web.UI.Page
{
    Class1 cls = new Class1();
    string user_id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string user_id = this.Request.QueryString["userid"];
            if (user_id != null)
            {
                string straca = "select Duration,AYID from m_academic ";
                DataTable acadt = cls.fildatatable(straca);
                if (acadt.Rows.Count > 0)
                {

                    ddl_ayid.DataSource = acadt;
                    ddl_ayid.DataTextField = "Duration";
                    ddl_ayid.DataValueField = "AYID";
                    ddl_ayid.DataBind();
                    ddl_ayid.Items.Insert(0, new ListItem("-- Select --", "0"));
                }
            }

        }

    }
    protected void btn_fetch_Click(object sender, EventArgs e)
    {
        string strstud = "select Form_no,stud_id,(select concat(SUBSTRING(Duration,9,4),'_',SUBSTRING(Duration,21,4)) from m_academic where AYID=ACDID) as ayid from d_adm_applicant where ACDID='" + ddl_ayid.SelectedValue+"' and stud_id  in (select distinct stud_id from m_std_studentacademic_tbl where del_flag=0 and group_id='" + ddl_course.SelectedValue + "'  and stud_id not in (select distinct stud_id from student_document));select doc_id,upper(Replace(doc_name,' ','_')) as doc_name from document_list WHERE del_flag=0";
        DataSet ds = cls.fill_dataset(strstud);
        string hostedlink = "";
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                    {

                        if (ddl_course.SelectedItem.Text.Contains("FE") == true || ddl_course.SelectedItem.Text.Contains("FY") == true)
                        {
                            hostedlink = "https://vit.vivacollege.in/engg_fy/" + ds.Tables[0].Rows[0]["ayid"].ToString() + "_DOC/" + ds.Tables[0].Rows[i]["Form_no"].ToString() + "/" + ds.Tables[1].Rows[j]["doc_name"].ToString() + ".jpg";
                        }
                        else if (ddl_course.SelectedItem.Text.Contains("SE") == true || ddl_course.SelectedItem.Text.Contains("SY") == true)
                        {
                            hostedlink = "https://vit.vivacollege.in/engg_admission/" + ds.Tables[0].Rows[0]["ayid"].ToString() + "_DOC/" + ds.Tables[0].Rows[i]["Form_no"].ToString() + "/" + ds.Tables[1].Rows[j]["doc_name"].ToString() + ".jpg";
                        }

                        HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(hostedlink);
                        HttpWebResponse httpRes = null;
                        try
                        {
                            httpRes = (HttpWebResponse)httpReq.GetResponse(); // Error 404 right here,
                            if (httpRes.StatusCode == HttpStatusCode.OK)
                            {


                                using (WebClient webClient = new WebClient())
                                {
                                    string uploadFolder = Request.PhysicalApplicationPath + "student";
                                    if (!Directory.Exists(uploadFolder))
                                    {

                                        Directory.CreateDirectory(uploadFolder);
                                    }

                                    if ((File.Exists(Server.MapPath("~/student/" + ds.Tables[0].Rows[i]["stud_id"].ToString() + "_" + ds.Tables[1].Rows[j]["doc_id"].ToString() + ".jpg"))) == false)
                                    {
                                        byte[] dataArr = webClient.DownloadData(hostedlink);

                                        File.WriteAllBytes(uploadFolder + "\\" + ds.Tables[0].Rows[i]["stud_id"].ToString() + "_" + ds.Tables[1].Rows[j]["doc_id"].ToString() + ".jpg", dataArr);
                                        string imgpho = "~/student/" + ds.Tables[0].Rows[i]["stud_id"].ToString() + "_" + ds.Tables[1].Rows[j]["doc_id"].ToString() + ".jpg";
                                        string updqry = "insert into student_document (stud_id,doc_name,doc_path,curr_dt,user_id,del_flag) values ('" + ds.Tables[0].Rows[i]["stud_id"].ToString() + "','" + ds.Tables[1].Rows[j]["doc_name"].ToString().Replace("_", " ") + "','" + imgpho + "',GETDATE(),'" + user_id + "',0)";
                                        cls.DMLqueries(updqry);
                                    }
                                    else
                                    {

                                    }

                                }
                            }
                            else
                            {
                            }
                        }
                        catch (WebException wec)
                        {
                        }
                    }
                }

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Document Imported Successfully', { color: '#fff', background: '#008E00', blur: 0.2, delay: 0 })", true);
            }
        }
        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'No Student Found For Selected Year', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
        }


    }
    protected void ddl_ayid_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_ayid.SelectedIndex > 0)
        {
            string strmulti = "select Group_id,Group_title from m_crs_subjectgroup_tbl where Group_id in (select distinct group_id from OLA_FY_adm_CourseSelection where stud_id in (select stud_id from d_adm_applicant where ACDID='" + ddl_ayid.SelectedValue + "' and del_flag=0 and stud_id not in (select distinct stud_id from student_document )) and del_flag=0)";
            DataTable multidt = cls.fildatatable(strmulti);
            if (multidt.Rows.Count > 0)
            {

                ddl_course.DataSource = multidt;
                ddl_course.DataTextField = "Group_title";
                ddl_course.DataValueField = "Group_id";
                ddl_course.DataBind();

            }
        }
        else
        {

        }
    }
}