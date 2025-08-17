using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class feeReceiptFull : System.Web.UI.Page
{
    Class1 c1 = new Class1();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                if (string.IsNullOrEmpty(Session["UserName"].ToString()))
                //if(1==0)
                {
                    Response.Redirect("login.aspx",false);
                }
                else
                {
                    if (Convert.ToBoolean(Session["li_fee"]) == false)
                    {
                        Response.Redirect("profile.aspx");
                    }
                    else
                    {
                        //string qry = " select field_type,value from dbo.www_receipt where Group_id='" + dsGrpID.Tables[0].Rows[0]["group_id"].ToString() + "' and ayid = (select MAX(ayid) from dbo.m_academic) and del_flag=0";
                        //DataSet ds = c1.fill_dataset(qry);
                        //if (ds.Tables[1].Rows.Count > 0)
                        //{
                        //    ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
                        //    long total = 0;
                        //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        //    {
                        //        try
                        //        {
                        //            total += Convert.ToInt32(ds.Tables[0].Rows[i]["value"].ToString());
                        //        }
                        //        catch (Exception ex2)
                        //        {
                        //        }
                        //    }

                        //    ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["field_type"] = "TOTAL FEES";
                        //    ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["value"] = total.ToString();

                        //    GridView1.DataSource = ds;
                        //    GridView1.DataBind();
                        //    GridView1.Rows[ds.Tables[0].Rows.Count - 1].Font.Bold = true;
                        //    GridView1.Rows[ds.Tables[0].Rows.Count - 1].Font.Size = 8;

                        //    lblNo.Text = Session["UserName"].ToString();
                        //    lblAmount.Text = ConvertNumbertoWords(total) + " ONLY";
                        //    lblName.Text = Session["studfullnm"].ToString();
                        //    lblCourse.Text = dsGrpID.Tables[0].Rows[0]["Group_title"].ToString();
                        //}
                        DataSet ds = (DataSet)Session["dsFee"];
                        GridView1.DataSource = ds.Tables[1];
                        GridView1.DataBind();
                        GridView1.Rows[ds.Tables[1].Rows.Count - 1].Font.Bold = true;
                        GridView1.Rows[ds.Tables[1].Rows.Count - 1].Font.Size = 8;
                        long total = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_COURSE_FEES"]);
                        lblNo.Text = Session["UserName"].ToString();
                        lblAmount.Text = ConvertNumbertoWords(total) + " ONLY";
                        lblName.Text = ds.Tables[0].Rows[0]["STUDENT NAME"].ToString();
                        lblCourse.Text = ds.Tables[0].Rows[0]["Group_title"].ToString();

                    }
                }
            }
            catch(Exception ex)
            {
                Response.Redirect("profile.aspx", false);
            }
        }
    }

    public string ConvertNumbertoWords(long number)
    {
        if (number == 0) return "ZERO";
        if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
        string words = "";
        if ((number / 1000000) > 0)
        {
            words += ConvertNumbertoWords(number / 100000) + " LAKHS ";
            number %= 1000000;
        }
        if ((number / 1000) > 0)
        {
            words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
            number %= 1000;
        }
        if ((number / 100) > 0)
        {
            words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
            number %= 100;
        }
        //if ((number / 10) > 0)  
        //{  
        // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
        // number %= 10;  
        //}  
        if (number > 0)
        {
            if (words != "") words += "AND ";
            var unitsMap = new[]   
        {  
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"  
        };
            var tensMap = new[]   
        {  
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"  
        };
            if (number < 20) words += unitsMap[number];
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0) words += " " + unitsMap[number % 10];
            }
        }
        return words;
    } 

}