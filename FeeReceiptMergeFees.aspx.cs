using System;
using System.Data;
using System.Web.UI;
using System.Globalization;

public partial class FeeReceiptMergeFees : System.Web.UI.Page
{
    Class1 cls = new Class1();

    protected void Page_Load(object sender, EventArgs e)
    {

       
        if (!IsPostBack)
        {
            if (Convert.ToString(Session["stud_id"]) != "")
            {
                try
                {
                    string stud_id = Session["stud_id"].ToString();
                    string recpt_no = Session["Receipt_no"].ToString();
                    string ayid = Session["ayid"].ToString() ;
                    string struct_type = Session["Type"].ToString() ;

                    DataSet ds = cls.fill_dataset("DECLARE @stud_id VARCHAR(10)='" + stud_id + "', @ayid VARCHAR(10)='" + ayid + "', @receipt_no VARCHAR(20)='" + recpt_no + "',@struct_type VARCHAR(20)='" + struct_type + "';SELECT Stud_id, CONVERT(VARCHAR, Pay_date, 103) AS [Pay_date], Recpt_mode,Type,Recpt_Bnk_Name,Struct_name,Amount FROM m_FeeEntry WHERE Stud_id=@stud_id AND Ayid=@ayid AND Receipt_no=@receipt_no AND Fine_flag=0 and del_flag=0 and Type=@struct_type;SELECT SUM(Amount) AS TotalAmount FROM m_FeeEntry WHERE Stud_id=@stud_id AND Ayid=@ayid AND Receipt_no=@receipt_no AND Fine_flag=0and del_flag=0 and Type=@struct_type;SELECT ISNULL(p.stud_F_Name, '') + ' ' + ISNULL(p.stud_M_Name, '') + ' ' + ISNULL(p.stud_L_Name, '') AS [Name],p.stud_Category, g.Group_title AS [Group],a.Roll_no FROM m_std_studentacademic_tbl a INNER JOIN m_std_personaldetails_tbl p ON a.stud_id = p.stud_id INNER JOIN m_crs_subjectgroup_tbl g ON a.group_id = g.Group_id WHERE a.stud_id = @stud_id AND a.AYID=@ayid and a.del_flag = 0 AND g.del_flag = 0 ORDER BY a.ayid;");
                    ds.Tables[0].TableName = "FeeInfo";
                    ds.Tables[1].TableName = "TotalFee";
                    ds.Tables[2].TableName = "StdInfo";
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables["FeeInfo"].Rows.Count == 0 || ds.Tables["TotalFee"].Rows.Count == 0 || ds.Tables["StdInfo"].Rows.Count == 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('Something Went Wrong !!!', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                            return;
                        }
                        else
                        {
                            lblNo.Text = recpt_no;
                            lbl_date.Text = ds.Tables["FeeInfo"].Rows[0]["Pay_date"].ToString();
                            lblName.Text = ds.Tables["StdInfo"].Rows[0]["Name"].ToString().ToUpper();
                            lbl_stud_id.Text = stud_id;
                            lbl_rollno.Text = ds.Tables["StdInfo"].Rows[0]["Roll_no"].ToString();
                            amt_no.Text = ds.Tables["TotalFee"].Rows[0]["TotalAmount"].ToString();
                            lbl_payment_mode.Text = ds.Tables["FeeInfo"].Rows[0]["Recpt_mode"].ToString().ToUpper();
                            lbl_feetype.Text = ds.Tables["FeeInfo"].Rows[0]["Type"].ToString().ToUpper();
                            long number = long.Parse(ds.Tables["TotalFee"].Rows[0]["TotalAmount"].ToString());
                            lblamount.Text = ConvertNumberToWords(number) + " Rupees Only";
                            lblcourse.Text = ds.Tables["StdInfo"].Rows[0]["Group"].ToString().ToUpper();
                            lblcategory.Text = ds.Tables["StdInfo"].Rows[0]["stud_category"].ToString();
                            gridstructre.DataSource = ds.Tables["FeeInfo"];
                            gridstructre.DataBind();

                            //lbl_admno.Text = "";
                            //lbl_section.Text = "";
                            //lblmedium.Text = "";
                            //lblSubjects.Text = "";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "print", "window.print();", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Notify", "$.notify('" + ex.ToString() + "', { color: '#fff', background: '#D44950', blur: 0.2, delay: 0 })", true);
                }
            }
        }
    }
    public string ConvertNumberToWords(long number)
    {

        if (number == 0)
            return "Zero Rupees Only";

        if (number < 0)
            return "Minus " + ConvertNumberToWords(Math.Abs(number));

        string words = "";

        if ((number / 10000000) > 0)
        {
            words += ConvertNumberToWords(number / 10000000) + " Crore ";
            number %= 10000000;
        }

        if ((number / 100000) > 0)
        {
            words += ConvertNumberToWords(number / 100000) + " Lakh ";
            number %= 100000;
        }

        if ((number / 1000) > 0)
        {
            words += ConvertNumberToWords(number / 1000) + " Thousand ";
            number %= 1000;
        }

        if ((number / 100) > 0)
        {
            words += ConvertNumberToWords(number / 100) + " Hundred ";
            number %= 100;
        }

        if (number > 0)
        {
            if (words != "")
                words += "and ";

            string[] unitsMap = new[]
            {
            "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine",
            "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen",
            "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };

            string[] tensMap = new[]
            {
            "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty",
            "Sixty", "Seventy", "Eighty", "Ninety"
        };

            if (number < 20)
            {
                words += unitsMap[number];
            }
            else
            {
                words += tensMap[number / 10];
                if ((number % 10) > 0)
                    words += " " + unitsMap[number % 10];
            }
        }

        return words.Trim();
    }
}