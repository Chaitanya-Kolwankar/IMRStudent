using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Cache;
using System.IO;
using System.Text.RegularExpressions;
using System.Configuration;

public partial class Complaint : System.Web.UI.Page
{
    Class1 c1 = new Class1();

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
                    fillGrid();
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("log_out.aspx");
            }
        }
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        diverror.Visible = false;
        if (ddcomplaint.SelectedIndex == 0)
        {
            displayError("Select Suggestion type");
        }
        else if (txtcomplaint_title.Text.Trim()=="")
        {
            displayError("Please enter Suggestion title");
        }
        else if (txtdescription.Text.Trim() == "")
        {
            displayError("Please enter Suggestion description");
        }
        else
        {
            //string filetype = Path.GetFileName(file_upload_info.PostedFile.ContentType);
            string filetype ="";
            string filename1 ="";
            Stream str = file_upload_info.PostedFile.InputStream;
            BinaryReader br = new BinaryReader(str);
            Byte[] size = br.ReadBytes((int)str.Length);
            bool hasFile = false;
            if (!file_upload_info.HasFile)
            {
                filetype = "";
            }
            else
            {
                string filePath = file_upload_info.PostedFile.FileName;
                filename1 = Path.GetFileName(filePath);
                string ext = Path.GetExtension(filename1);
                hasFile = true;
                //string type = "";
                switch (ext)
                {
                    case ".pdf":
                        filetype = "application/pdf";
                        break;
                }
            }

            if (hasFile == true && filetype == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", "javascript:alert('Only pdf Files can be attached.')", true);
                displayError("Only pdf Files can be attached.");
            }
            else
            {
                if (c1.execSp(Session["UserName"].ToString(), ddcomplaint.SelectedItem.Text, txtcomplaint_title.Text.Trim(), txtdescription.Text.Trim(), size, filetype, filename1))
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", "javascript:alert('Suggestion Registered succesfully.')", true);
                    ddcomplaint.SelectedIndex = 0;
                    txtcomplaint_title.Text = "";
                    txtdescription.Text = "";
                    fillGrid();
                }
                else
                {
                    displayError("Something went wrong. Please Retry");
                }
            }
        }
    }

    private void displayError(string text)
    {
        diverror.Visible = true;
        diverror.InnerText = text;
    }


    private void fillGrid()
    {
        DataSet dsNew = c1.fill_dataset("select id,type,title,description,submit_date,status,reply from dbo.stud_complain where stud_id='" + Session["UserName"].ToString() + "' and del_flag=0 order by submit_date desc");
        if (dsNew.Tables[0].Rows.Count > 0)
        {
            GridView1.DataSource = dsNew.Tables[0];
            GridView1.DataBind();
        }
        else
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        diverror.Visible = false;
        ddcomplaint.SelectedIndex = 0;
        txtcomplaint_title.Text = "";
        txtdescription.Text = "";
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "repliedFile")
        {
            diverror.Visible = false;
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            string id = ((GridView)sender).Rows[row.RowIndex].Cells[0].Text;

            //int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;

            DataSet dsNew = c1.fill_dataset("select * from dbo.stud_complain where stud_id='" + Session["UserName"].ToString() + "' and id='" + id + "' and del_flag=0");
           
            contentType = dsNew.Tables[0].Rows[0]["replied_file_type"].ToString();
            fileName = dsNew.Tables[0].Rows[0]["replied_file_name"].ToString();

            if (contentType == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", "javascript:alert('No file attached.')", true);
                displayError("No file attached.");
            }
            else
            {
                bytes = (byte[])dsNew.Tables[0].Rows[0]["replied_file"];
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }
        if (e.CommandName == "submittedFile")
        {
            diverror.Visible = false;
            GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
            //string id = GridView1.DataKeys[row.RowIndex].Values[0].ToString();
            string id = ((GridView)sender).Rows[row.RowIndex].Cells[0].Text;
            //int id = int.Parse((sender as LinkButton).CommandArgument);
            byte[] bytes;
            string fileName, contentType;

            DataSet dsNew = c1.fill_dataset("select * from dbo.stud_complain where stud_id='" + Session["UserName"].ToString() + "' and id='" + id + "' and del_flag=0");
           
            contentType = dsNew.Tables[0].Rows[0]["submitted_file_type"].ToString();
            fileName = dsNew.Tables[0].Rows[0]["submitted_file_name"].ToString();

            if (contentType == "")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", "javascript:alert('No file attached.')", true);
                displayError("No file attached.");
            }
            else
            {
                bytes = (byte[])dsNew.Tables[0].Rows[0]["submitted_file"];
                Response.Clear();
                Response.Buffer = true;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + fileName);
                Response.BinaryWrite(bytes);
                Response.Flush();
                Response.End();
            }
        }
    }
}