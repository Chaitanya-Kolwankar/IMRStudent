using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class document : System.Web.UI.Page
{
    Class1 cls = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {

                fetchdoc();
                string stud_id = this.Request.QueryString["id"];
                string user_id = this.Request.QueryString["userid"];
                if (stud_id != null && user_id != null)
                {
                    this.imgdoc.ImageUrl = "~/images/defaultdocument.jpg";
                    Session["stud_id"] = stud_id;
                    Session["user_id"] = user_id;


                    string query = "select * from studentImage where stud_id='" + stud_id + "';select * from student_document where stud_id='" + stud_id + "' and Del_Flag=0";

                    DataSet ds = cls.fill_dataset(query);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (!Convert.IsDBNull(ds.Tables[0].Rows[0]["Stud_photo"]))
                        {
                            Byte[] byte_photo = (Byte[])ds.Tables[0].Rows[0]["Stud_photo"];
                            string str_photo = Convert.ToBase64String(byte_photo, 0, byte_photo.Length);

                            imgphoto.ImageUrl = "data:image/png;base64," + str_photo;
                        }
                        if (!Convert.IsDBNull(ds.Tables[0].Rows[0]["Stud_sing"]))
                        {
                            Byte[] byte_sign = (Byte[])ds.Tables[0].Rows[0]["Stud_sing"];
                            string str_sign = Convert.ToBase64String(byte_sign, 0, byte_sign.Length);
                            imgsign.ImageUrl = "data:image/png;base64," + str_sign;
                        }

                        grid.DataSource = ds.Tables[1];
                        grid.DataBind();
                    }
                    else
                    {

                    }
                    string straca = "select doc_id,doc_name from document_list WHERE del_flag=0";
                    DataTable acadt = cls.fildatatable(straca);
                    if (acadt.Rows.Count > 0)
                    {

                        ddl_doc.DataSource = acadt;
                        ddl_doc.DataTextField = "doc_name";
                        ddl_doc.DataValueField = "doc_id";
                        ddl_doc.DataBind();
                        ddl_doc.Items.Insert(0, new ListItem("-- Select --", "0"));
                    }
                }
                else
                {
                    Response.Redirect("login.aspx", false);
                }

            }
        }
        catch (Exception ex)
        {
           // Response.Redirect("login.aspx", false);
        }
    }
    public void fetchdoc()
    {
        string strstud = "select * from student_document where stud_id='" + Session["UserName"].ToString() + "';select doc_id,upper(Replace(doc_name,' ','_')) as doc_name from document_list WHERE del_flag=0;select  Form_no,case when (select COUNT(*) from adm_applicant_registration where formno=Form_no)>0 then 'FE' when (select COUNT(*) from adm_applicant_entry_otheryear where formno=Form_no)>0 then 'SE'  end  as app_type  , (select concat(SUBSTRING(Duration,9,4),'_',SUBSTRING(Duration,21,4)) from m_academic where AYID=ACDID) as ayid   from d_adm_applicant where stud_id='" + Session["UserName"].ToString() + "'";
        DataSet ds = cls.fill_dataset(strstud);
        string hostedlink = "";
        if (ds.Tables[0].Rows.Count == 0)
        {
            if (ds.Tables[1].Rows.Count > 0)
            {

                for (int j = 0; j < ds.Tables[1].Rows.Count; j++)
                {

                    if (ds.Tables[2].Rows[0]["app_type"].ToString() == "FE")
                    {
                        hostedlink = "https://vit.vivacollege.in/engg_fy/" + ds.Tables[2].Rows[0]["ayid"].ToString() + "_DOC/" + ds.Tables[2].Rows[0]["Form_no"].ToString() + "/" + ds.Tables[1].Rows[j]["doc_name"].ToString() + ".jpg";
                    }
                    else if (ds.Tables[2].Rows[0]["app_type"].ToString() == "SE")
                    {
                        hostedlink = "https://vit.vivacollege.in/engg_admission/" + ds.Tables[2].Rows[0]["ayid"].ToString() + "_DOC/" + ds.Tables[2].Rows[0]["Form_no"].ToString() + "/" + ds.Tables[1].Rows[j]["doc_name"].ToString() + ".jpg";
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

                                if ((File.Exists(Server.MapPath("~/student/" + Session["UserName"].ToString() + "_" + ds.Tables[1].Rows[j]["doc_id"].ToString() + ".jpg"))) == false)
                                {
                                    byte[] dataArr = webClient.DownloadData(hostedlink);

                                    File.WriteAllBytes(uploadFolder + "\\" + Session["UserName"].ToString() + "_" + ds.Tables[1].Rows[j]["doc_id"].ToString() + ".jpg", dataArr);
                                    string imgpho = "~/student/" + Session["UserName"].ToString() + "_" + ds.Tables[1].Rows[j]["doc_id"].ToString() + ".jpg";
                                    string updqry = "insert into student_document (stud_id,doc_name,doc_path,curr_dt,user_id,del_flag) values ('" + Session["UserName"].ToString() + "','" + ds.Tables[1].Rows[j]["doc_name"].ToString().Replace("_", " ") + "','" + imgpho + "',GETDATE(),'" + Session["UserName"].ToString() + "',0)";
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
        }
    }

    protected void btnmodal_Click(object sender, EventArgs e)
    {
        ddl_doc.SelectedIndex = 0;
        imgdoc.ImageUrl = "~/images/defaultdocument.jpg";
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#myModal').modal({backdrop: 'static'});</script>", false);
    }

    public bool checkFileExtension(string filename)
    {
        if (filename.Contains(".") != true)
            return false;

        string[] validExtensions = new string[7];
        string ext = filename.Substring(filename.LastIndexOf('.') + 1).ToLower();


        validExtensions[0] = "jpg";
        validExtensions[1] = "jpeg";
        validExtensions[2] = "bmp";
        validExtensions[3] = "png";
        validExtensions[4] = "gif";
        validExtensions[5] = "tif";
        validExtensions[6] = "tiff";



        for (int i = 0; i < validExtensions.Length - 1; i++)
        {
            if (ext == validExtensions[i])
                return true;
        }

        return false;
    }

    FileUpload file_doc;
    protected void btn_save_Click(object sender, EventArgs e)
    {
        if (ddl_doc.SelectedIndex > 0)
        {

            string updqry = "";

            string imgpho = "~/student/" + Session["stud_id"].ToString() + "_" + ddl_doc.SelectedValue + ".jpg";

            if (filedoc.HasFile == false)
            {

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Select " + ddl_doc.SelectedItem.Text + " to Upload', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
            }
            else
            {
                Session["FileUploaddoc"] = filedoc;
                if (checkFileExtension(filedoc.FileName) != true)
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Select Image File Only ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);

                }
                else
                {
                    try
                    {
                        file_doc = (FileUpload)Session["FileUploaddoc"];
                        int size1 = file_doc.PostedFile.ContentLength;
                        if (size1 > 200000)
                        {

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'File size not be exceed than 200 KB  ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                        }
                        else
                        {

                            string uploadFolder = Request.PhysicalApplicationPath + "student\\";
                            if (!Directory.Exists(uploadFolder.Replace("\\", "/")))
                            {

                                Directory.CreateDirectory(uploadFolder.Replace("\\", "/"));
                            }

                            string extension = Path.GetExtension(file_doc.PostedFile.FileName);
                            file_doc.SaveAs(uploadFolder.Replace("\\", "/") + Session["stud_id"].ToString().Trim() + "_" + ddl_doc.SelectedValue.Trim() + ".jpg");
                            string query = "select * from student_document where stud_id='" + Session["stud_id"].ToString() + "' and doc_name='" + ddl_doc.SelectedItem.Text + "' ";
                            DataTable ds = cls.fildatatable(query);
                            if (ds.Rows.Count > 0)
                            {
                                if (ds.Rows[0]["del_flag"].ToString() == "True")
                                {
                                    updqry = "update student_document set doc_path='" + imgpho + "' ,del_flag=0,mod_dt=getdate()  where stud_id='" + Session["stud_id"].ToString() + "' and  doc_name='" + ddl_doc.SelectedItem.Text + "'";
                                }
                                else
                                {
                                    updqry = "update student_document set doc_path='" + imgpho + "' ,mod_dt=getdate() where stud_id='" + Session["stud_id"].ToString() + "' and  doc_name='" + ddl_doc.SelectedItem.Text + "'";
                                }
                            }
                            else
                            {
                                updqry = "insert into student_document (stud_id,doc_name,doc_path,curr_dt,user_id,del_flag) values ('" + Session["stud_id"].ToString() + "','" + ddl_doc.SelectedItem.Text + "','" + imgpho + "',GETDATE(),'" + Session["user_id"].ToString() + "',0)";
                            }

                            this.imgdoc.ImageUrl = ("~/student/" + Session["stud_id"].ToString().Trim() + "_" + ddl_doc.SelectedValue.Trim() + ".jpg");

                            if (cls.DMLqueries(updqry) == true)
                            {
                                Session["FileUploaddoc"] = null;

                                fillgrid();

                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  '" + ddl_doc.SelectedItem.Text + " Submitted Sucessfully', { color: '#fff', background: '#008E00', blur: 0.2, delay: 0 })", true);
                            }
                            else
                            {
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  '" + ddl_doc.SelectedItem.Text + " Not Submitted ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                            }
                        }
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                        string msg = "Insert Error:";
                        msg += ex.Message;
                        throw new Exception(msg);
                    }
                    finally
                    {
                        cls.con.Close();
                    }
                }
            }

        }
        else
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify(  'Select Document for Upload ', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
        }

        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#myModal').modal({backdrop: 'static'});</script>", false);
    }

    public void fillgrid()
    {
        string query1 = "select * from student_document where stud_id='" + Session["stud_id"].ToString() + "' and Del_Flag=0";

        DataSet ds1 = cls.fill_dataset(query1);
        grid.DataSource = ds1.Tables[0];
        grid.DataBind();
    }

    protected void ddl_doc_SelectedIndexChanged(object sender, EventArgs e)
    {

        DataTable dt = cls.fildatatable("select * from student_document where stud_id='" + Session["stud_id"].ToString() + "' and doc_name='" + ddl_doc.SelectedItem.Text + "' and Del_Flag=0");
        if (dt.Rows.Count > 0)
        {
            string imgpho = "~/student/" + Session["stud_id"].ToString() + "_" + ddl_doc.SelectedValue + ".jpg";
            if ((File.Exists(Server.MapPath(imgpho))) == true)
            {

                this.imgdoc.ImageUrl = ("~/student/" + Session["stud_id"].ToString() + "_" + ddl_doc.SelectedValue + ".jpg");
            }
            else
            {

                this.imgdoc.ImageUrl = "~/images/defaultdocument.jpg";
            }
        }
        else
        {

            this.imgdoc.ImageUrl = "~/images/defaultdocument.jpg";
        }
        if (filedoc.HasFile == true)
        {
            filedoc.Dispose();
        }
        //lblmsg.Text = "";
        //lblsuccess.Text = "";
        ////lblmsg.Visible = false;
        ////lblsuccess.Visible = false;

        //divmsg.Attributes.Add("Style", "display:none");
        //divsuccess.Attributes.Add("Style", "display:none");

        //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#myModal').modal({backdrop: 'static'});</script>", false);

    }
    protected void grid_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

            string link = ((Label)row.FindControl("lbllink")).Text.ToString().Trim();
            string docname = ((Label)row.FindControl("lblfilename")).Text.ToString().Trim();

            imgdocviewer.ImageUrl = link;
            btn_download.HRef = link;
            //btn_print.Attributes.Add("onclick", "PrintImage('" + link + "'); return false;");
            lbldocname.Text = docname + ".jpg";
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#photoviewer').modal({backdrop: true});</script>", false);

        }
        if (e.CommandName == "delflag")
        {
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);

            string doc_name = ((Label)row.Cells[2].FindControl("lblfilename")).Text;
            if (doc_name != "")
            {
                string querydelete = "update  student_document set del_flag=1,user_id='" + Session["user_id"] + "',del_dt=getdate() where doc_name='" + doc_name + "' and stud_id='" + Session["stud_id"] + "'";
                if (cls.DMLqueries(querydelete))
                {
                    fillgrid();
                }
            }

        }
    }

    //protected void btn_download_Click(object sender, EventArgs e)
    //{
    //    using (WebClient webClient = new WebClient())
    //    {
    //        byte[] dataArr = webClient.DownloadData(imgdocviewer.ImageUrl);

    //        string uploadFolder = Request.PhysicalApplicationPath + "2021_2022_DOC";
    //        if (!Directory.Exists(uploadFolder))
    //        {

    //            Directory.CreateDirectory(uploadFolder);
    //        }

    //        if ((File.Exists(Server.MapPath("~/2021_2022_DOC/222222_photo.jpg"))) == false)
    //        {

    //            File.WriteAllBytes(uploadFolder + "\\222222_photo.jpg", dataArr);
    //        }
    //        else
    //        {

    //        }

    //    }
    //}

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        ddl_doc.SelectedIndex = 0;
        imgdoc.ImageUrl = "~/images/defaultdocument.jpg";

        DataTable dt = cls.fildatatable("select * from student_document where stud_id='" + Session["stud_id"].ToString() + "' and doc_name='" + ddl_doc.SelectedItem.Text + "' and Del_Flag=0");
        if (dt.Rows.Count > 0)
        {
            string imgpho = "~/student/" + Session["stud_id"].ToString() + "_" + ddl_doc.SelectedValue + ".jpg";
            if ((File.Exists(Server.MapPath(imgpho))) == true)
            {

                this.imgdoc.ImageUrl = ("~/student/" + Session["stud_id"].ToString() + "_" + ddl_doc.SelectedValue + ".jpg");
            }
            else
            {

                this.imgdoc.ImageUrl = "~/images/defaultdocument.jpg";
            }
        }
        else
        {

            this.imgdoc.ImageUrl = "~/images/defaultdocument.jpg";
        }

        if (filedoc.HasFile == true)
        {
            filedoc.Dispose();
        }
        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#myModal').modal({backdrop: 'static'});</script>", false);
    }
    protected void btn_close_Click(object sender, EventArgs e)
    {

    }
}



