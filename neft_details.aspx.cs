using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class neft_details : System.Web.UI.Page
{
    Class1 cls = new Class1();
    FileUpload fup_pdf, fup_photo, fup_recpt;
    string qryins = ""; bool img_exist = false;
    classWebMethods qrye = new classWebMethods();
    QueryClass qrycls = new QueryClass();
    string id = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserName"].ToString() != string.Empty || Session["UserName"].ToString() != "")
            {
                fillgrid();
                string signpho = "~/NEFT_DOC/" + Session["UserName"].ToString() + "/DOC.jpg";
                if ((File.Exists(Server.MapPath(signpho))) == true)
                {
                    this.imgphoto.ImageUrl = ("~/NEFT_DOC/" + Session["UserName"].ToString() + "/DOC.jpg").Replace("\\", "/");
                    img_exist = true;
                }
                else
                {
                    img_exist = false;
                }
                if (!IsPostBack)
                {
                    string getcourse = "select * from m_crs_subcourse_tbl where subcourse_id in (select distinct subcourse_Id from m_std_studentacademic_tbl where stud_id='" + Session["UserName"].ToString() + "' )";
                    DataTable dtcrs = cls.fildatatable(getcourse);
                    qrye.getenggayid(ddlyear);
                    qrycls.getsubcourse(dtcrs.Rows[0]["course_id"].ToString(), ddl_subcourse);
                    fillgrid();
                }
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
            cls.err_cls(ex.ToString());
        }

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

        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The file extension " + ext.ToUpper() + " is not allowed!Allowed Extensions are jpg,jpeg,bmp,png,gif,tif,tiff')", true);
        return false;
    }

    public bool validate()
    {
        if (txt_bnk.Text == "" || txt_dt.Text == "" || txtamt.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please fill all the details')", true);
            return false;
        }
        else if (ddlyear.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select your academic year')", true);
            return false;
        }
        else if (ddl_subcourse.SelectedIndex == 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select your subcourse')", true);
            return false;
        }
        else if (img_exist == false)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please upload the receipt')", true);
            return false;
        }
        else if (filephoto2.HasFile == false)
        {
            if (img_exist == false)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please upload the receipt')", true);
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return true;
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string neft="";
            if (getid.Text != "")
            {
                id = getid.Text;
            }
            if (validate())
            {
              
                
                if (getid.Text != "")
                {
                    neft = "select * from neft_doc where stud_id='" + Session["UserName"].ToString() + "' and id='" + id + "'";
                }
                else
                {
                    neft = "select * from neft_doc where trans_no='" + txt_trans.Text + "' and stud_id='" + Session["UserName"].ToString() + "'";
                }
                
                DataSet dsnft = cls.fill_dataset(neft);
                if (dsnft.Tables[0].Rows.Count > 0)
                {
                    qryins = "update neft_doc set bank_name='" + txt_bnk.Text.Trim() + "',ayid='" + ddlyear.SelectedValue + "',trans_no='" + txt_trans.Text.Trim() + "',amount='" + txtamt.Text.Trim() + "',pay_dt=convert(date,'" + txt_dt.Text + "',103),mod_dt=getdate() where stud_id='" + Session["UserName"].ToString() + "' and id='" + id + "'";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Updated Successfully')", true);
                }
                else
                {
                    qryins = "insert into neft_doc values ('" + Session["UserName"].ToString() + "',getdate(),'" + ddl_subcourse.SelectedValue + "',0,'','" + txt_bnk.Text.Trim() + "','" + txtamt.Text.Trim() + "',convert(date,'" + txt_dt.Text + "',103),'" + txt_trans.Text.Trim() + "','" + ddlyear.SelectedValue + "')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Data Saved Successfully')", true);
                }

                cls.DMLqueries(qryins);
                fillgrid();
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Login.aspx");
            cls.err_cls(ex.ToString());
        }
      
    }

    public void fillgrid()
    {
        string qry = "select * from neft_doc where stud_id='" + Session["UserName"].ToString() + "'";
        DataSet ds = cls.fill_dataset(qry);
        if (ds.Tables[0].Rows.Count > 0)
        {
            grid_show.Visible = true;
            grd.DataSource = ds;
            grd.DataBind();
        }
        else
        {
            grid_show.Visible = false;
        }
    }

    protected void btnuploadphoto_Click(object sender, EventArgs e)
    {
        try
        {
            Session["FileUploadPhoto"] = filephoto2;
            if (checkFileExtension(filephoto2.FileName) == true)
            {
                fup_photo = (FileUpload)Session["FileUploadPhoto"];
                int size = (fup_photo.PostedFile.ContentLength);
                if (size > 200000)
                {
                    //ErrorMessageDisplay("File size not be exceed than 200 KB");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('File size not be exceed than 200 KB')", true);
                }
                else
                {
                    string root = Session["UserName"].ToString();
                    string uploadFolder = Request.PhysicalApplicationPath + "NEFT_DOC\\" + Session["UserName"].ToString() + "\\";
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(uploadFolder.Replace("\\", "/"));
                    }
                    string extension = Path.GetExtension(fup_photo.PostedFile.FileName);
                    fup_photo.SaveAs(uploadFolder.Replace("\\", "/") + "DOC.jpg");
                    this.imgphoto.ImageUrl = ("" + "~/NEFT_DOC/" + Session["UserName"].ToString() + "/DOC.jpg").Replace("\\", "/");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Receipt Uploaded Successfully')", true);
                }
            }
        }
        catch (Exception ex)
        {
            cls.err_cls(ex.ToString());
        }
    }
  
    protected void btnview_Click(object sender, EventArgs e)
    {
        string rbt = "";
        Button btn = (Button)sender;
        GridViewRow GridView1 = (GridViewRow)btn.NamingContainer;
        txtamt.Text = (GridView1.FindControl("lbl_amt") as Label).Text;
        txt_bnk.Text = (GridView1.FindControl("lbl_bnk") as Label).Text;
        txt_dt.Text = (GridView1.FindControl("lbl_paydate") as Label).Text;
        txt_trans.Text = (GridView1.FindControl("lbl_trans") as Label).Text;
        //ddl_subcourse.Items.FindByValue() = (GridView1.FindControl("lbl_subcrs") as Label).Text;
        getid.Text = (GridView1.FindControl("lbl_idd") as Label).Text;
        ddl_subcourse.SelectedIndex = ddl_subcourse.Items.IndexOf(ddl_subcourse.Items.FindByValue((GridView1.FindControl("lbl_subcrs") as Label).Text));
                
    }
}