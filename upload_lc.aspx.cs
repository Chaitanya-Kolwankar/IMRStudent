using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class upload_lc : System.Web.UI.Page
{
    FileUpload fup_Photo;
    string chk_group = "", grp = "";
    Class1 cls = new Class1();
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (!IsPostBack)
                {
                    chk_group = "select * from m_std_studentacademic_tbl where group_id in (select Group_id from m_crs_subjectgroup_tbl where Group_title like 'be%') and ayid in (select ayid from m_academic where iscurrent=1) and stud_id='" + Session["UserName"].ToString() + "'";
                    ds = cls.fill_dataset(chk_group);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (Session["UserName"].ToString() != null || Session["UserName"].ToString() != "")
                        {
                        }
                        else
                        {
                            Response.Redirect("Login.aspx", false);
                        }
                    }
                    else
                    {
                        Response.Redirect("Login.aspx", false);
                    }
                }
            }
            catch (Exception ex1)
            {
                Response.Redirect("Login.aspx", false);                
            }
        }
    }

    public bool checkFileExtension1(string filename)
    {
        if (filename.Contains(".") != true)
            return false;


        string[] validExtensions = new string[7];
        string ext = filename.Substring(filename.LastIndexOf('.') + 1).ToLower();


        validExtensions[0] = "pdf";




        for (int i = 0; i < validExtensions.Length - 1; i++)
        {
            if (ext == validExtensions[i])
                return true;
        }


        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('The file extension " + ext.ToUpper() + " is not allowed!Allowed Extensions are pdf');", true);
        return false;
    }

    protected void btnuploadphoto_Click(object sender, EventArgs e)
    {
        try
        {
            string qrychk = "", qry = "";
            if (filephoto.HasFile == false)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Select an Photo to upload');", true);
            }
            else
            {

                //access santosh raut and shilpa keni
                qrychk = "select * from lc_doc where stud_id='" + Session["UserName"].ToString() + "' and del_flag=0";
                DataSet ds = cls.fill_dataset(qrychk);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    qry = "update lc_doc set group_id='" + ds.Tables[0].Rows[0]["group_id"].ToString() + "',mod_dt=getdate() where stud_id='" + Session["UserName"].ToString() + "'";
                    
                }
                else
                {
                    qry = "insert into lc_doc values ('" + Session["UserName"].ToString() + "','" + ds.Tables[0].Rows[0]["group_id"].ToString() + "',getdate(),null,0,'','','')";
                    Session["FileUploadPhoto"] = filephoto;
                    if (checkFileExtension1(filephoto.FileName) != true)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Select an Photo to upload');", true);
                    }
                    else
                    {
                        fup_Photo = (FileUpload)Session["FileUploadPhoto"];
                        int size1 = fup_Photo.PostedFile.ContentLength;
                        if (size1 > 200000)
                        {
                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('File size not be exceed than 200 KB');", true);

                        }
                        else
                        {

                            string root = Session["UserName"].ToString();
                            string uploadFolder = Request.PhysicalApplicationPath + "LC\\" + Session["UserName"].ToString() + "\\";
                            if (!Directory.Exists(root))
                            {

                                Directory.CreateDirectory(uploadFolder.Replace("\\", "/"));
                            }

                            string extension = Path.GetExtension(fup_Photo.PostedFile.FileName);
                            fup_Photo.SaveAs(uploadFolder.Replace("\\", "/") + "" + Session["UserName"].ToString() + "_LC.pdf");

                            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('Submitted Sucessfully');", true);
                        }
                    }
                }
                cls.DMLqueries(qry);
               
            }
        }
        catch (Exception EX)
        {
            cls.err_cls(EX.ToString());
        }
    }
}