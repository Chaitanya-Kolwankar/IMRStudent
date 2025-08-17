using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Generic;
using Microsoft.SqlServer.Server;

/// <summary>
/// Summary description for QueryClass
/// </summary>
public class QueryClass
{
    Class1 cls = new Class1();
    SqlCommand cmd1 = new SqlCommand();
    SqlDataAdapter da1;
    DataSet dss = new DataSet();
	public QueryClass()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public string splitGrp(string grp)
    {
        string[] grpId = grp.Split(',');
        string finalString = "";
        for (int i = 0; i < grpId.Length; i++)
        {
            if (i == 0)
            {
                finalString = "'" + grpId[i].ToString() + "'";
            }
            else
            {
                finalString += ",'" + grpId[i].ToString() + "'";
            }
        }
        return finalString;
    }


    public void getcourse(DropDownList ddl)
    {
        String qry = "Select * from m_crs_course_tbl where faculty_id='FAC00007'";
        DataSet dss = cls.fill_dataset(qry);
        ddl.DataSource = dss.Tables[0];
        ddl.DataTextField = "course_name";
        ddl.DataValueField = "course_id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("-- Select --", "0"));
    }

    public void getsubcourse(string courseID, DropDownList ddl)
    {
        String qry = "select * from m_crs_subcourse_tbl where course_id='" + courseID + "'";
        DataSet dss = cls.fill_dataset(qry);
        ddl.DataSource = dss.Tables[0];
        ddl.DataTextField = "subcourse_name";
        ddl.DataValueField = "subcourse_id";
        ddl.DataBind();
        ddl.Items.Insert(0, new ListItem("-- Select --", "0"));
    }


}
public class STUDENTFEES
{
    public string chqstatus { get; set; }
    public string balanceamt { get; set; }
    public string structname { get; set; }
    public string miscid { get; set; }
    public string STUDENTID { get; set; }
    public string STRUCTURE { get; set; }
    public string YEAR { get; set; }
    public string ayid { get; set; }
    public string AMOUNT { get; set; }
    public string CRSAMOUNT { get; set; }
    public string PAID { get; set; }
    public string DIFFERNCE { get; set; }
    public string REFUND { get; set; }
    public string REFUNDED { get; set; }
    public string PAYDATE { get; set; }
    public string AUTHORIZEDBY { get; set; }
    public string RECIPTMODE { get; set; }
    public string curr_dt { get; set; }
    public string task { get; set; }
    public string type { get; set; }
    public string Recpt_Chq_dt { get; set; }
    public string Recpt_Chq_No { get; set; }
    public string Recpt_Bnk_Name { get; set; }
    public string Recpt_Bnk_Branch { get; set; }
    public int flagchk { get; set; }
    public int STATUS12 { get; set; }
    public int REMARK12 { get; set; }
    public string structype { get; set; }
    public string REMARK { get; set; }
    public string RECIPTNO { get; set; }
    public string STATUS { get; set; }
    public string message { get; set; }
    public string feecount { get; set; }
    public string paymode { get; set; }
    public string Course_tot_fees { get; set; }
    public string course_fee_paid { get; set; }
    public string refdet { get; set; }
    public string flag { get; set; }
    public string authcaste { get; set; }
    public string PAY { get; set; }
    public string studid { get; set; }
    public string studname { get; set; }
    public string groupid { get; set; }
    public string narryear { get; set; }
    public string branch { get; set; }
}