using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;


public partial class fee_recpt : System.Web.UI.Page
{
    Class1 cls1 = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //string qry = "select AYID,(substring(Duration,9,4)+'-'+substring(Duration,21,4)) as Duration from m_academic where ayid in (select distinct ayid from m_std_studentacademic_tbl where stud_id='" + Session["UserName"].ToString() + "') order by AYID desc";
            //DataSet dss = c1.fill_dataset(qry);
            //ddlyear.DataSource = dss.Tables[0];
            //ddlyear.DataTextField = "Duration";
            //ddlyear.DataValueField = "AYID";
            //ddlyear.DataBind();
            //ddlyear.Items.Insert(0, new ListItem("-- Select --", "0"));

            string ayd = "select ayid from m_academic where IsCurrent=1";
            DataSet dsayd = cls1.fill_dataset(ayd);

            Session["ayidd"] = dsayd.Tables[0].Rows[0]["ayid"].ToString();

        }
    }

    [WebMethod]
    public static STUDENTFEES[] StrudentFeeDetails(string ayid)
    {
        classWebMethods webCls = new classWebMethods();
        return webCls.StrudentFeeDetails(ayid);
    }

    [WebMethod]
    public static bool receipt_type(string stud_id, string year, string recipt_no)
    {
        classWebMethods webCls = new classWebMethods();
        return webCls.receipt_type(stud_id, year, recipt_no);
    }

    [System.Web.Services.WebMethod(enableSession: true)]
    public static void Setsession(string id)
    {
        HttpContext.Current.Session["id"] = id;
    }

    [WebMethod]
    public static List<ListItem> fillayid(string stud_id)
    {
        classWebMethods cls = new classWebMethods();
        return cls.fillayid(stud_id);

    }
   
}