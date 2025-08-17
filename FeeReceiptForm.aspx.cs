using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FeeReceiptForm : System.Web.UI.Page
{
    Class1 c1 = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {        
        if (!IsPostBack)
        {
            try
            {
                if (string.IsNullOrEmpty(Session["UserName"].ToString()))
                {
                    Response.Redirect("login.aspx", false);
                }
                else
                {
                    if (Convert.ToBoolean(Session["li_fee"]) == false)
                    {
                        Response.Redirect("profile.aspx");
                    }
                    else
                    {
                          DataSet dsGrpID = c1.fill_dataset("select a.group_id,g.Group_title from dbo.m_std_studentacademic_tbl a,dbo.m_crs_subjectgroup_tbl g where a.group_id=g.Group_id and  stud_id='" + Session["UserName"].ToString() + "' and a.del_flag=0 and a.ayid=(select MAX(ayid) from dbo.m_academic)");

                          if (dsGrpID.Tables[0].Rows.Count > 0)
                          {
                              string qry = "SELECT [ROLL NO],[STUDENT NAME],TOTAL_COURSE_FEES,SUM(amount) AS PAID,BALANCE,isnull((SELECT SUBSTRING((SELECT ( ', ' + Remark) FROM";
                              qry += " m_feeentry t2 WHERE m.id = t2.Stud_id  ORDER BY id, Remark FOR XML PATH('')),3,1000)  ),'') as 'REMARK', STUD_CATEGORY,ID,[Group_title],group_id ";
                              qry += " FROM Cheque_Status M where Status='Clear'and AYID=(select MAX(ayid) from dbo.m_academic)  and group_id='" + dsGrpID.Tables[0].Rows[0]["group_id"].ToString() + "' and ID='" + Session["UserName"].ToString() + "'";
                              qry += "  GROUP BY [Student Name],[Roll no],TOTAL_COURSE_FEES,ID,BALANCE,Remark,STUD_CATEGORY,[Group_title],group_id, DISCOUNT_GIVEN,";
                              qry += "  REFUND_GIVEN ORDER BY [Group_title], case when  isnumeric(substring([Roll no],0,2))<>1 then cast (substring([Roll no],";
                              qry += "(PATINDEX('%[0-9]%',[Roll no])),len([Roll no]))as int) else cast([Roll no] as int)end ";


                              qry += "select field_type,value from dbo.www_receipt where Group_id='" + dsGrpID.Tables[0].Rows[0]["group_id"].ToString() + "' and ayid = (select MAX(ayid) from dbo.m_academic) and del_flag=0";
                              DataSet ds = c1.fill_dataset(qry);
                              if (ds.Tables[0].Rows.Count > 0)
                              {
                                  long feeTotal = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_COURSE_FEES"]);
                                  long feePaid = Convert.ToInt32(ds.Tables[0].Rows[0]["PAID"]);
                                  if (feeTotal == feePaid)
                                  {
                                      if (ds.Tables[1].Rows.Count > 0)
                                      {
                                          ds.Tables[1].Rows.Add(ds.Tables[1].NewRow());
                                          long total = 0;
                                          for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                                          {
                                              try
                                              {
                                                  total += Convert.ToInt32(ds.Tables[1].Rows[i]["value"].ToString());
                                              }
                                              catch (Exception ex2)
                                              {
                                              }
                                          }
                                          ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["field_type"] = "TOTAL FEES";
                                          ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["value"] = total.ToString();
                                          if (total == feeTotal)
                                          {
                                              Session["dsFee"] = ds;
                                              //Response.Redirect("feeReceiptFull.aspx", false);
                                              viewReceipt.Visible = true;
                                          }
                                          else
                                          {
                                              divmsg.InnerHtml = "Fee detail not proper";
                                          }
                                      }
                                      else
                                      {
                                          //Fee details not found
                                          divmsg.InnerHtml = "Fee detail not found";
                                      }
                                  }
                                  else
                                  {
                                      divmsg.InnerHtml = "Full fees not paid";
                                  }
                              }
                              else
                              { 
                                  //full time fee detail not found
                                  divmsg.InnerHtml = "Admission detail not found";
                              }
                          }
                          else
                          {
                              divmsg.InnerHtml = "Details not found";
                          }
                    }
                }
            }
            catch (Exception ex1)
            {
                Response.Redirect("profile.aspx", false);
            }
        }
    }
}