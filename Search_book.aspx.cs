using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class search_book : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Session["UserName"].ToString() == string.Empty)
                {
                    Response.Redirect("login.aspx");
                    keyword1.SelectedIndex = 0;
                    keyword2.SelectedIndex = 0;
                    keyword3.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                Response.Redirect("log_out.aspx");
            }
        }
    }
    Class1 c = new Class1();
    DataSet ds = new DataSet();
    
    protected void Search_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }


            string query = "", flagKey1 = "", flagKey2 = "", flagKey3 = "", flagLogic = "", flagLogic1 = "";

            if (keyword1.SelectedIndex == 1)
            {
                if (txtTitle.Text != "")
                {
                    flagKey1 = "book_title";
                }
                else
                {
                    keyword2.SelectedIndex = 0;
                }
            }
            else if (keyword1.SelectedIndex == 2)
            {
                if (txtTitle.Text != "")
                {
                    flagKey1 = "author_name";
                }
                else
                {
                    keyword2.SelectedIndex = 0;
                }
            }
            else if (keyword1.SelectedIndex == 3)
            {
                if (txtTitle.Text != "")
                {
                    flagKey1 = "general_name";
                }
                else
                {
                    keyword2.SelectedIndex = 0;
                }

            }
            else
            {
                flagKey1 = "";
            }

            if (keyword2.SelectedIndex == 1)
            {
                if (txtAuthor.Text != "")
                {
                    flagKey2 = "book_title";
                }
                else
                {
                    keyword3.SelectedIndex = 0;
                }

            }
            else if (keyword2.SelectedIndex == 2)
            {
                if (txtAuthor.Text != "")
                {
                    flagKey2 = "author_name";
                }
                else
                {
                    keyword3.SelectedIndex = 0;
                }

            }
            else if (keyword2.SelectedIndex == 3)
            {
                if (txtAuthor.Text != "")
                {
                    flagKey2 = "general_name";
                }
                else
                {
                    keyword3.SelectedIndex = 0;
                }

            }
            else if (keyword2.SelectedIndex == 4)
            {
                if (txtAuthor.Text != "")
                {
                    flagKey2 = "book_edition";
                }
                else
                {
                    keyword3.SelectedIndex = 0;
                }

            }
            else if (keyword2.SelectedIndex == 5)
            {
                if (txtAuthor.Text != "")
                {
                    flagKey2 = "isbn_no";
                }
                else
                {
                    keyword3.SelectedIndex = 0;
                }

            }
            else
            {
                flagKey2 = "";
            }

            if (keyword3.SelectedIndex == 1 && txtPublisher.Text != "")
            {
                flagKey3 = "book_title";
            }
            else if (keyword3.SelectedIndex == 2 && txtPublisher.Text != "")
            {
                flagKey3 = "author_name";
            }
            else if (keyword3.SelectedIndex == 3 && txtPublisher.Text != "")
            {
                flagKey3 = "general_name";
            }
            else if (keyword3.SelectedIndex == 4 && txtPublisher.Text != "")
            {
                flagKey3 = "book_edition";
            }
            else if (keyword3.SelectedIndex == 5 && txtPublisher.Text != "")
            {
                flagKey3 = "isbn_no";
            }
            else
            {
                flagKey3 = "";
            }

            if (rdbAND.Checked == true)
            {
                flagLogic = "AND";
            }
            else if (rdbOR.Checked == true)
            {
                flagLogic = "OR";
            }
            else if (rdbNOT.Checked == true)
            {
                flagLogic = "NOT";
            }

            if (rdbAND1.Checked == true)
            {
                flagLogic1 = "AND";
            }
            else if (rdbOR1.Checked == true)
            {
                flagLogic1 = "OR";
            }
            else if (rdbNOT1.Checked == true)
            {
                flagLogic1 = "NOT";
            }


            string str = "select book_id,book_title,book_classification_no,book_edition,BookCount,author_name,accession_no,general_name from dbo.vu_bookDetails_accession_publisher ";
            if (flagKey1 != "")
            {
                str += "where   " + flagKey1 + " like '%" + txtTitle.Text.Trim() + "%' ";
            }
            if (flagKey2 != "" && txtTitle.Enabled == true)
            {
                if (flagKey1 != flagKey2)
                {
                    if (flagLogic != "NOT")
                    {
                        str += " " + flagLogic + " " + flagKey2 + " like '%" + txtAuthor.Text.Trim() + "%' ";
                    }
                    else if (flagLogic == "NOT")
                    {
                        if (txtTitle.Text != "")
                        {

                            str += " and " + flagKey2 + " not like '%" + txtAuthor.Text.Trim() + "%' ";
                        }
                        else
                        {
                            str += " " + flagKey2 + " not like '%" + txtAuthor.Text.Trim() + "%' ";
                        }
                    }

                }
                else
                {
                    if (flagLogic != "NOT")
                    {
                        str += " or  " + flagKey2 + " like '%" + txtAuthor.Text.Trim() + "%' ";
                    }
                    else if (flagLogic == "NOT")
                    {
                        str += " and " + flagKey2 + " not like '%" + txtAuthor.Text.Trim() + "%' ";
                    }
                }

            }
            if (flagKey3 != "" && txtAuthor.Enabled == true)
            {
                if (flagKey3 != flagKey1 && flagKey3 != flagKey2)
                {
                    if (flagLogic1 != "NOT")
                    {
                        str += " " + flagLogic1 + " " + flagKey3 + " like '%" + txtPublisher.Text.Trim() + "%' ";
                    }
                    else if (flagLogic1 == "NOT")
                    {
                        str += " and " + flagKey3 + " not like '%" + txtPublisher.Text.Trim() + "%' ";
                    }
                }
                else
                {
                    if (flagLogic1 != "NOT")
                    {
                        str += " or  " + flagKey3 + " like '%" + txtPublisher.Text.Trim() + "%' ";
                    }
                    else if (flagLogic1 == "NOT")
                    {
                        str += " and " + flagKey3 + " not like '%" + txtPublisher.Text.Trim() + "%' ";
                    }
                }


            }
            str += " order by book_title";

            hide();
            clear();

            if (str != "")
            {
                ds = c.fill_dataset(str);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    txtClear();
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    lblcount1.Text = "Search Result: " + ds.Tables[0].Rows.Count;
                    GridView1.DataSource = ds;
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
        }
        catch (Exception ex)
        {
            Response.Redirect("Search_book.aspx");
        }

    }
    protected void btnDetails_Click(object sender, EventArgs e)
    {
     

    }
    protected void btnDetails_Click1(object sender, EventArgs e)
    {
       
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            string accession, accession1 = "", str1 = "", book_id = "", author_name = "", book_classification_no = "", book_edition = "", general_name = "", s_img;
            clear();
            if (e.CommandName == "details")
            {
                GridViewRow row = (GridViewRow)(((Button)e.CommandSource).NamingContainer);
                accession1 = ((GridView)sender).Rows[row.RowIndex].Cells[6].Text;
                if (((GridView)sender).Rows[row.RowIndex].Cells[0].Text != "")
                {
                    book_id = ((GridView)sender).Rows[row.RowIndex].Cells[0].Text.ToString();
                }

                if (((GridView)sender).Rows[row.RowIndex].Cells[4].Text != "")
                {
                    if (((GridView)sender).Rows[row.RowIndex].Cells[4].Text == "&nbsp;")
                    {
                        general_name = "";
                    }
                    else
                    {
                        general_name = ((GridView)sender).Rows[row.RowIndex].Cells[4].Text.ToString();
                    }
                }
                else
                {
                    general_name = "";
                }


                if (accession1.Contains(','))
                {
                    accession = accession1.Substring(0, accession1.IndexOf(',')).ToString();
                }
                else
                {
                    accession = accession1;
                }

                // str1 = "select BookTitle,Author,Publisher,Keyword,ISBNNO,Remark from V_REP_CATALOG where AccessionNo='"+accession+"'";
                //str1 = " select book_id,book_title,author_name,general_name,keywords,isbn_no,remark,cover_image from vu_bookDetails_accession_publisher where book_title='" + book_title + "' and author_name='" + author_name + "' and book_classification_no='" + book_classification_no + "' and book_edition='" + book_edition + "' and general_name='" + general_name + "'";
                if (general_name != "")
                {
                    str1 = " select book_id,book_title,author_name,general_name,keywords,isbn_no,remark,cover_image from vu_bookDetails_accession_publisher where book_id='" + book_id + "' and general_name='" + general_name + "'";
                }
                else
                {
                    str1 = " select book_id,book_title,author_name,general_name,keywords,isbn_no,remark,cover_image from vu_bookDetails_accession_publisher where book_id='" + book_id + "' and general_name is null";
                }
                ds = c.fill_dataset(str1);
                visible();
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lbltitle.Text = ds.Tables[0].Rows[0]["book_title"].ToString();
                    lblauthor.Text = ds.Tables[0].Rows[0]["author_name"].ToString();
                    lblDesc.Text = ds.Tables[0].Rows[0]["remark"].ToString();
                    lblisbn.Text = ds.Tables[0].Rows[0]["isbn_no"].ToString();
                    lblkey.Text = ds.Tables[0].Rows[0]["keywords"].ToString();
                    lblPub.Text = ds.Tables[0].Rows[0]["general_name"].ToString();
                    if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["cover_image"].ToString()))
                    {
                        Byte[] img = (Byte[])ds.Tables[0].Rows[0]["cover_image"];
                        s_img = Convert.ToBase64String(img);
                        Image1.ImageUrl = "data:image/png;base64," + s_img;
                    }

                }

                string[] words = accession1.Split(',');
                lblcount.InnerText = "";
                string issued = "";
                foreach (string word in words)
                {
                    str1 = "select issue_return from dbo.ll_issue_return where accession_id='" + word + "'";
                    ds = c.fill_dataset(str1);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0][0].ToString() == "True")
                        {
                            issued = "Available";
                            lblcount.InnerText = lblcount.InnerText + "\n" + word;
                        }
                        else
                        {
                            issued = "Issued";
                            lblcountIssued.InnerText = lblcountIssued.InnerText + "\n" + word;
                        }

                    }
                    else
                    {
                        issued = "Available";
                        lblcount.InnerText = lblcount.InnerText + "\n" + word;
                    }

                }

            }
        }
        catch (Exception ex)
        {
            Response.Redirect("Search_book.aspx");
        }
    }

    public void visible()
    {
        lbltitle.Visible = true;
       // lbll.Visible = true;
        lblauthor.Visible = true;
        lblDesc.Visible = true;
        lblisbn.Visible = true;
        lblkey.Visible = true;
        lblPub.Visible = true;
        lblcount.Visible = true;
    }

    public void hide()
    {
        lbltitle.Visible = false;
       // lbll.Visible = false;
        lblauthor.Visible = false;
        lblDesc.Visible = false;
        lblisbn.Visible = false;
        lblkey.Visible = false;
        lblPub.Visible = false;
        lblcount.Visible = false;

    }

    public void clear()
    {
        lbltitle.Text= "";
        //lbll.Text = "";
        lblauthor.Text = "";
        lblDesc.Text = "";
        lblisbn.Text = "";
        lblkey.Text = "";
        lblPub.Text = "";
        lblcount.InnerText = "";
        keyword1.SelectedIndex = 0;
        keyword2.SelectedIndex = 0;
        keyword3.SelectedIndex = 0;
    }
    public void txtClear()
    {
        txtTitle.Text = "";
        txtTitle.Enabled = false;
        txtAuthor.Text = "";
        txtAuthor.Enabled = false;
        txtbasic.Text = "";
        txtPublisher.Text = "";
        txtPublisher.Enabled = false;
        rdbAND.Checked = false;
        rdbAND1.Checked = false;
        rdbNOT.Checked = false;
        rdbNOT1.Checked = false;
        rdbOR.Checked = false;
        rdbOR1.Checked = false;
        
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
       
    }

    protected void btnBasic_Click(object sender, EventArgs e)
    {
        try
        {
            if (GridView1.Rows.Count > 0)
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            hide();
            clear();
            string str2 = "";
            if (txtbasic.Text != "")
            {

                str2 = "select book_id,book_title,book_classification_no,book_edition,BookCount,author_name,accession_no,general_name from dbo.vu_bookDetails_accession_publisher where book_title like '%" + txtbasic.Text.Trim() + "%' order by book_title ";
                if (str2 != "")
                {
                    ds = c.fill_dataset(str2);
                }
                if (ds.Tables[0].Rows.Count <= 0)
                {
                    str2 = "select book_id,book_title,book_classification_no,book_edition,BookCount,author_name,accession_no,general_name from dbo.vu_bookDetails_accession_publisher where author_name like  '%" + txtbasic.Text.Trim() + "%' order by book_title";
                    if (str2 != "")
                    {
                        ds = c.fill_dataset(str2);
                        if (ds.Tables[0].Rows.Count <= 0)
                        {
                            str2 = "select book_id,book_title,book_classification_no,book_edition,BookCount,author_name,accession_no,general_name from dbo.vu_bookDetails_accession_publisher where general_name like  '%" + txtbasic.Text.Trim() + "%' order by book_title";
                            ds = c.fill_dataset(str2);
                        }
                    }

                }
            }
            else
            {
                str2 = "select book_id,book_title,book_classification_no,book_edition,BookCount,author_name,accession_no,general_name from dbo.vu_bookDetails_accession_publisher order by book_title";
                ds = c.fill_dataset(str2);
            }
            if (ds.Tables[0].Rows.Count > 0)
            {
                txtClear();
                lblcount1.Text = "Search Result: " + ds.Tables[0].Rows.Count;
                GridView1.DataSource = ds;
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
        catch (Exception ex)
        {
            Response.Redirect("Search_book.aspx");
        }
    }
    protected void keyword1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (keyword1.SelectedIndex > 0)
        {
            txtTitle.Enabled = true;
        }
        else
        {
            txtTitle.Text = "";
            txtTitle.Enabled = false;
        }
    }
    protected void keyword2_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (keyword2.SelectedIndex > 0)
        {
            if (rdbAND.Checked == true || rdbNOT.Checked == true || rdbOR.Checked == true)
            {
                lblerr.Visible = false;
                txtAuthor.Enabled = true;
            }
            else
            {
                lblerr.Text = "Please select the correct option";
                lblerr.Visible = true;
                keyword2.SelectedIndex = 0;
               // ClientScript.RegisterStartupScript(this.GetType(), "alertmessage", "javascript:alert('Please select the correct option')", true);

                

                //refreshforkey2();

                //err.InnerHtml = "Please select the correct option";
                //ClientScript.RegisterStartupScript(this.GetType(), "", "alert('Please select the correct option')", true); 
            }
        }
        else
        {
            txtAuthor.Text = "";
            txtAuthor.Enabled = false;
        }
    }
    protected void keyword3_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (keyword2.SelectedIndex > 0)
        {
            if (rdbAND1.Checked == true || rdbNOT1.Checked == true || rdbOR1.Checked == true)
            {
                lblerr.Visible = false;
                txtPublisher.Enabled = true;
            }
            else
            {
                lblerr.Visible = true;
                lblerr.Text = "Please select the correct option";
                keyword3.SelectedIndex = 0;  
            }
        }
        else
        {
            txtPublisher.Text = "";
            txtPublisher.Enabled = false;
        }
    }

    public void queries()
    {
    

    }
    protected void rdbAND1_CheckedChanged(object sender, EventArgs e)
    {
        keyword3.SelectedIndex = 0;
        lblerr.Visible = false;
        txtPublisher.Enabled = false;
    }
    protected void rdbOR1_CheckedChanged(object sender, EventArgs e)
    {
        keyword3.SelectedIndex = 0;
        lblerr.Visible = false;
        txtPublisher.Enabled = false;
    }
    protected void rdbNOT1_CheckedChanged(object sender, EventArgs e)
    {
        keyword3.SelectedIndex = 0;
        lblerr.Visible = false;
        txtPublisher.Enabled = false;
    }
    protected void rdbAND_CheckedChanged(object sender, EventArgs e)
    {
        keyword2.SelectedIndex = 0;
        lblerr.Visible = false;
        txtAuthor.Enabled = false;
    }
    protected void rdbOR_CheckedChanged(object sender, EventArgs e)
    {
        keyword2.SelectedIndex = 0;
        lblerr.Visible = false;
        txtAuthor.Enabled = false;
    }
    protected void rdbNOT_CheckedChanged(object sender, EventArgs e)
    {
        keyword2.SelectedIndex = 0;
        lblerr.Visible = false;
        txtAuthor.Enabled = false;
    }

    public void refreshforkey3()
    {
        if (keyword3.SelectedIndex <= 0)
        {
            lblerr.Visible = true;
            lblerr.Text = "Please select the correct option";
            keyword3.SelectedIndex = 0;
        }
        
    }
    public void refreshforkey2()
    {
        if (keyword2.SelectedIndex <= 0)
        {
            lblerr.Visible = true;
            lblerr.Text = "Please select the correct option";
            keyword2.SelectedIndex = 0;
        }
    }

    
}