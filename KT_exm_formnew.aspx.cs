using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Specialized;
using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Data.SqlClient;
using System.Security.Policy;
using System.Configuration;
using System.IO;

public partial class KT_exm_formnew : System.Web.UI.Page
{
    string currexam = "";
    DataSet ds;
    DataTable dt;
    Class1 cls = new Class1();
    new_Class2 cls2 = new new_Class2();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            NameValueCollection nvc = Request.Form;

            if (Request.Params["mmp_txn"] != null)
            {
                string postingmmp_txn = Request.Params["mmp_txn"].ToString();
                string postingmer_txn = Request.Params["mer_txn"];
                string postinamount = Request.Params["amt"].ToString();
                string postingprod = Request.Params["prod"].ToString();
                string postingdate = Request.Params["date"].ToString();
                string postingbank_txn = Request.Params["bank_txn"].ToString();
                string postingf_code = Request.Params["f_code"].ToString();
                string postingbank_name = Request.Params["bank_name"].ToString();
                string signature = Request.Params["signature"].ToString();
                string postingdiscriminator = Request.Params["discriminator"].ToString();
                //string customername = Request.Params["udf1"].ToString();
                //string customermail = Request.Params["udf2 "].ToString();
                //string customerno = Request.Params["udf3 "].ToString();
                string customername = "";
                string customermail = "";
                string customerno = "";

               string respHashKey = "13368ae603a8f909ff";
               //string respHashKey = "KEYRESP123657234";
                string ressignature = "";
                string strsignature = postingmmp_txn + postingmer_txn + postingf_code + postingprod + postingdiscriminator + postinamount + postingbank_txn;
                //string strsignature = postingmmp_txn + postingmer_txn1 + postingf_code + postingprod + discriminator + postinamount + postingbank_txn;
                byte[] bytes = Encoding.UTF8.GetBytes(respHashKey);
                byte[] b = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(strsignature));
                ressignature = byteToHexString(b).ToLower();

                if (signature == ressignature)
                {
                    if (postingf_code == "F")
                    {
                        // Response.Redirect("http://localhost:2394/student/form_final.aspx");
                        //  lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                    }
                    else if (postingf_code == "C")
                    {
                        //Response.Redirect("http://localhost:2394/student/form_final.aspx");
                        //  lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                    }
                    else if (postingf_code == "S")
                    {
                        ///  lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                    }
                    else if (postingf_code == "Ok")
                    {
                        //lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                    }
                }
                else
                {
                    //lblStatus.Text = "Signature Mismatched...";Response.Redirect("http://203.192.254.34/STUDENT_ERP/FEE_RECIEPT_COPY.ASPX");
                    //lblStatus.Text = "Signature Mismatched..."; Response.Redirect("http://localhost:2394/student/form_final.aspx");

                }
                //Response.Redirect("http://203.192.254.34/STUDENT_ERP/FEE_RECIEPT_COPY.ASPX?postingmmp_txn="+postingmmp_txn+"&postingmer_txn="+postingmer_txn+"&postinamount="+postinamount+"&postingprod="+postingprod+"&postingdate="+postingdate+"&postingbank_txn="+postingbank_txn+"&postingf_code="+postingf_code+"&postingbank_name="+postingbank_name+"&postingdiscriminator="+postingdiscriminator+"&customername="+customername+"&customermail="+customermail+"&customerno="+customerno);
                // Response.Redirect("http://vivacollege.in/student/fee_reciept_copy_pay.ASPX?postingmmp_txn=" + postingmmp_txn + "&postingmer_txn=" + postingmer_txn + "&postinamount=" + postinamount + "&postingprod=" + postingprod + "&postingdate=" + postingdate + "&postingbank_txn=" + postingbank_txn + "&postingf_code=" + postingf_code + "&postingbank_name=" + postingbank_name + "&postingdiscriminator=" + postingdiscriminator + "&customername=" + customername + "&customermail=" + customermail + "&customerno=" + customerno);


                //try
                //{
                String[] val = new String[0];
                string id = "";

                id = postingmer_txn.Substring(0, 8);
                Session["UserName"] = id;
                Session["postingmertxn"] = postingmer_txn;
                //if (postingmer_txn.Contains("2018"))
                //{
                //    val = postingmer_txn.Split('2018');
                //    id = val[0] + "A";
                //}
                //else if (postingmer_txn.Contains("J")) { val = postingmer_txn.Split('A'); id = val[0] + "J"; }
                string str11212 = "select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month";
                DataSet ds11212 = cls.fill_dataset(str11212);
                string str = "    update processing_fees set name='" + customername + "',mobile_no='" + customerno + "',email='" + customermail + "',postinamount='" + postinamount + "',postingmmp_txn='" + postingmmp_txn + "',postingprod='" + postingprod + "',postingdate='" + postingdate + "',postingbank_txn='" + postingbank_txn + "',postingf_code='" + postingf_code + "',postingbank_name='" + postingbank_name + "',signature='Matched',postingdiscriminator='" + postingdiscriminator + "' where form_no='" + id + "' and postingmer_txn='" + postingmer_txn + "' ";
                string[] arr;string semid = "", exmcode = "", exmmonth = "";
                string strgetpro = "select status from processing_fees where postingmer_txn='"+ postingmer_txn + "'";
                DataTable dtprodetails = cls.fildatatable(strgetpro);
                if(dtprodetails.Rows.Count >0)
                {
                    arr = dtprodetails.Rows[0]["status"].ToString().Split(':');
                    semid = arr[2].ToString();
                    exmcode = arr[3].ToString();
                    exmmonth = arr[1].ToString();
                }
                if (postingf_code == "S" || postingf_code == "Ok")
                {
                    //string qry12 = "select sem_id,receipt_no,status,amount,previous_stud_id,exm_type from kt_exam_pay_details where stud_id='" + id + "'  ";
                    //DataSet ds12 = cls.fill_dataset(qry12);
                    
                    string qry = "select sem_id,receipt_no,status,amount,previous_stud_id,exm_type from kt_exam_pay_details where stud_id='" + id + "' and ayid=(select ayid from m_academic where iscurrent='1') and sem_id='" + semid + "' and exm_date='" + exmmonth + "' and exm_type='" + exmcode + "'";//;select seat_no from  engg_viva.dbo.cre_marks_tbl where stud_id='" + Session["prev_id"].ToString() + "'";/--------commented due to engg_viva is offline
                    DataSet ds = cls.fill_dataset(qry);
                    Session["prev_id"] = ds.Tables[0].Rows[0]["previous_stud_id"].ToString();
                    str = str + "update kt_exam_pay_details set Status='Paid' where stud_id='" + id + "' and exm_date like '%" + ds11212.Tables[0].Rows[0][0].ToString() + "%'  and sem_id='" + ds.Tables[0].Rows[0]["sem_id"].ToString()  + "' and ayid=(select ayid from m_academic where iscurrent='1')";
                    DataSet dt_new_ch = new DataSet();
                    if (ds.Tables[0].Rows[0]["sem_id"].ToString() != "Sem-1" && ds.Tables[0].Rows[0]["sem_id"].ToString() != "Sem-2" && ds.Tables[0].Rows[0]["sem_id"].ToString() != "Sem-7" && ds.Tables[0].Rows[0]["sem_id"].ToString() != "Sem-8")
                    {
                        //string new_ch = "select * from cre_marks_tbl where stud_id='" + Session["prev_id"].ToString() + "' and exam_code='" + ds.Tables[0].Rows[0]["exm_type"].ToString() + "' and sem_id='" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "' and ayid=(select ayid from m_academic where Iscurrent='1')";
                        string new_ch = "select * from cre_marks_tbl where stud_id='" + Session["prev_id"].ToString() + "'  and sem_id='" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "' ";
                         dt_new_ch = cls2.fill_dataset(new_ch);
                        //string str1 = "insert into cre_marks_tbl select distinct a.subject_id,stud_id,case when ((a.h1<>'' or a.h1 is not null) and a.h1 like '%+') and remark like 'S%' and a.h1_grace like '%^%' then cast(cast(replace(a.h1,'+','') as int)+cast(replace(h1_grace,'^','') as int) as varchar)+'+' when ((a.h1<>'' and a.h1 is not null) and remark like 'S%') then replace(a.h1,'+','')+'+' else a.h1  end  ,'',case when ((a.h2<>'' or a.h2 is not null) and a.h2 like '%+') and remark like 'S%' and a.h2_grace like '%^%' then cast(cast(replace(a.h2,'+','') as int)+cast(replace(h2_grace,'^','') as int) as varchar)+'+' when ((a.h2<>'' and a.h2 is not null) and remark like 'S%') then replace(a.h2,'+','')+'+' else a.h2  end  ,'',h3,h3_grace,a.credit_sub_id,'" + ds.Tables[0].Rows[0]["exm_type"].ToString() + "' as a,a.sem_id,(select ayid from m_academic where Iscurrent='1') as a1,null,'','OP','','','',getdate() as a3,null as a4,0 as a5,a.pattern from dbo.cre_marks_tbl a, dbo.cre_credit_tbl b, dbo.cre_subject c where a.credit_sub_id=b.credit_sub_id and b.ayid=(select ayid from m_academic where Iscurrent='1') and b.subject_id=c.subject_id and stud_id='" + ds.Tables[0].Rows[0]["previous_stud_id"].ToString() + "' and a.del_flag=0 and b.del_flag=0 and a.sem_id='" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "'  and exam_code = (select distinct exam_code from dbo.cre_marks_tbl a where stud_id ='" + ds.Tables[0].Rows[0]["previous_stud_id"].ToString() + "'  and a.sem_id = '" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "' and curr_date = (select distinct max(curr_date)from dbo.cre_marks_tbl a where stud_id = '" + ds.Tables[0].Rows[0]["previous_stud_id"].ToString() + "'  and  a.sem_id = '" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "' and del_flag = 0))";
                      
                        //if (dt_new_ch.Tables[0].Rows.Count == 0)
                        //{
                        //    cls2.update_data(str1);
                        //}
                    }
                    else
                    {
                        string strkt = "select distinct curr_exam_code as Pattern from kt_exam where stud_id = '" + id + "' and sem_id = '" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "' and receipt_no = '" + ds.Tables[0].Rows[0]["receipt_no"].ToString() + "' and ayid in (select ayid from m_academic where iscurrent='1')";
                        dt_new_ch = cls.fill_dataset(strkt);
                    }
                    //string new_ch = "select * from cre_marks_tbl where stud_id='" + Session["prev_id"].ToString() + "' and exam_code='" + ds.Tables[0].Rows[0]["exm_type"].ToString() + "' and sem_id='" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "' and ayid=(select ayid from m_academic where Iscurrent='1')";
                    //dt_new_ch = cls2.fill_dataset(new_ch);
                    cls.update_data(str);
                    string qry_name = "select ISNULL(stud_F_Name,'')+' '+ISNULL(stud_M_Name,'')+' '+ISNULL(stud_L_Name,'')+' '+ISNULL(stud_Mother_FName,'') as Name  ,course_name from m_std_personaldetails_tbl as a,m_crs_course_tbl as b ,m_crs_subcourse_tbl as c ,m_std_studentacademic_tbl as d where a.stud_id=d.stud_id and d.subcourse_Id=c.subcourse_id and c.course_id=b.course_id and a.stud_id='" + id + "' ";
                    DataSet st_name = cls.fill_dataset(qry_name);
                    lblname.Text = st_name.Tables[0].Rows[0]["Name"].ToString();
                    if (dt_new_ch.Tables[0].Rows.Count > 0)
                    {
                        if (dt_new_ch.Tables[0].Rows[0]["Pattern"].ToString().Contains("CS"))
                        {
                            lblprogram.Text = "Choice Base";
                            lblprogram2.Text = "Choice Base";
                        }
                        else
                        {
                            lblprogram.Text = dt_new_ch.Tables[0].Rows[0]["Pattern"].ToString();
                            lblprogram2.Text = dt_new_ch.Tables[0].Rows[0]["Pattern"].ToString();
                        }
                        currexam = dt_new_ch.Tables[0].Rows[0]["Pattern"].ToString();
                    }
                    string qry_ex = "select exam_code,exam_date from cre_exam where exam_code='" + ds.Tables[0].Rows[0]["exm_type"].ToString() + "'";
                    DataSet dsexm = cls2.fill_dataset(qry_ex);
                    lblstud_id.Text = id;
                    lblsem.Text = ds.Tables[0].Rows[0]["sem_id"].ToString() + " " + "(KT)";
                    lbl_chalan.Text = ds.Tables[0].Rows[0]["receipt_no"].ToString();
                    lbl_seat_no.Text = "--"; //ds.Tables[1].Rows[0]["seat_no"].ToString();
                    lbl_status.Text = "Paid";
                    lblfee.Text = ds.Tables[0].Rows[0]["amount"].ToString();
                    Label2.Text = dsexm.Tables[0].Rows[0]["exam_date"].ToString();
                    Label7.Text = dsexm.Tables[0].Rows[0]["exam_date"].ToString();
                    Label1.Text = st_name.Tables[0].Rows[0]["course_name"].ToString();
                    
                    lblname2.Text = st_name.Tables[0].Rows[0]["Name"].ToString();
                    
                    lblstud_id2.Text = id;
                    lblsem2.Text = ds.Tables[0].Rows[0]["sem_id"].ToString() + " " + "(KT)";
                    lbl_chalan2.Text = ds.Tables[0].Rows[0]["receipt_no"].ToString();//ds.Tables[0].Rows[0]["previous_stud_id"].ToString()
                    lbl_seat_no2.Text = "--";//; ds.Tables[1].Rows[0]["seat_no"].ToString();
                    lbl_status2.Text = "Paid";
                    lblfee2.Text = ds.Tables[0].Rows[0]["amount"].ToString();
                }
                if (postingf_code == "C" ||postingf_code == "F" )
                {
                    //string qry12 = "select sem_id,receipt_no,status,amount,previous_stud_id,exm_type from kt_exam_pay_details where stud_id='" + id + "' ";
                    //DataSet ds12 = cls.fill_dataset(qry12);
                    
                    string qry = "select sem_id,receipt_no,status,amount,previous_stud_id,exm_type from kt_exam_pay_details where stud_id='" + id + "'  and ayid=(select ayid from m_academic where iscurrent='1') and sem_id='" + semid + "' and exm_date='" + exmmonth + "' and exm_type='" + exmcode + "' ";//;select seat_no from  engg_viva.dbo.cre_marks_tbl where stud_id='" + Session["prev_id"].ToString() + "'";/--------commented due to engg_viva is offline
                    DataSet ds = cls.fill_dataset(qry);
                    Session["prev_id"] = ds.Tables[0].Rows[0]["previous_stud_id"].ToString();
                    str = str + "update kt_exam_pay_details set Status='Unpaid' where stud_id='" + id + "' and exm_date like '%" + ds11212.Tables[0].Rows[0][0].ToString() + "%'  and sem_id='" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "' and ayid=(select ayid from m_academic where iscurrent='1')";
                    DataSet dt_new_ch = new DataSet();
                    if (ds.Tables[0].Rows[0]["sem_id"].ToString() != "Sem-1" && ds.Tables[0].Rows[0]["sem_id"].ToString() != "Sem-2" && ds.Tables[0].Rows[0]["sem_id"].ToString() != "Sem-7" && ds.Tables[0].Rows[0]["sem_id"].ToString() != "Sem-8")
                    {
                        string new_ch = "select * from cre_marks_tbl where stud_id='" + Session["prev_id"].ToString() + "' and sem_id='" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "' ";
                        dt_new_ch = cls2.fill_dataset(new_ch);
                        //string str1 = "insert into cre_marks_tbl select distinct a.subject_id,stud_id,case when ((a.h1<>'' or a.h1 is not null) and a.h1 like '%+') and remark like 'S%' and a.h1_grace like '%^%' then cast(cast(replace(a.h1,'+','') as int)+cast(replace(h1_grace,'^','') as int) as varchar)+'+' when ((a.h1<>'' and a.h1 is not null) and remark like 'S%') then replace(a.h1,'+','')+'+' else a.h1  end  ,'',case when ((a.h2<>'' or a.h2 is not null) and a.h2 like '%+') and remark like 'S%' and a.h2_grace like '%^%' then cast(cast(replace(a.h2,'+','') as int)+cast(replace(h2_grace,'^','') as int) as varchar)+'+' when ((a.h2<>'' and a.h2 is not null) and remark like 'S%') then replace(a.h2,'+','')+'+' else a.h2  end  ,'',h3,h3_grace,a.credit_sub_id,'" + ds.Tables[0].Rows[0]["exm_type"].ToString() + "' as a,a.sem_id,(select ayid from m_academic where Iscurrent='1') as a1,null,'','OP','','','',getdate() as a3,null as a4,0 as a5,a.pattern from dbo.cre_marks_tbl a, dbo.cre_credit_tbl b, dbo.cre_subject c where a.credit_sub_id=b.credit_sub_id and b.ayid=(select ayid from m_academic where Iscurrent='1') and b.subject_id=c.subject_id and stud_id='" + ds.Tables[0].Rows[0]["previous_stud_id"].ToString() + "' and a.del_flag=0 and b.del_flag=0 and a.sem_id='" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "'  and exam_code = (select distinct exam_code from dbo.cre_marks_tbl a where stud_id ='" + ds.Tables[0].Rows[0]["previous_stud_id"].ToString() + "'  and a.sem_id = '" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "' and curr_date = (select distinct max(curr_date)from dbo.cre_marks_tbl a where stud_id = '" + ds.Tables[0].Rows[0]["previous_stud_id"].ToString() + "'  and  a.sem_id = '" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "' and del_flag = 0))";

                        //if (dt_new_ch.Tables[0].Rows.Count == 0)
                        //{
                        //    cls2.update_data(str1);
                        //}
                    }
                    else
                    {
                        string strkt = "select distinct curr_exam_code as Pattern from kt_exam where stud_id = '" + id+"' and sem_id = '"+ ds.Tables[0].Rows[0]["sem_id"].ToString() + "' and receipt_no = '"+ ds.Tables[0].Rows[0]["receipt_no"].ToString() + "' and ayid in (select ayid from m_academic where iscurrent='1')";
                        dt_new_ch = cls.fill_dataset(strkt);
                    }
                    //string new_ch = "select * from cre_marks_tbl where stud_id='" + Session["prev_id"].ToString() + "' and exam_code='" + ds.Tables[0].Rows[0]["exm_type"].ToString() + "' and sem_id='" + ds.Tables[0].Rows[0]["sem_id"].ToString() + "' and ayid=(select ayid from m_academic where Iscurrent='1')";
                    //dt_new_ch = cls2.fill_dataset(new_ch);
                    cls.update_data(str);
                    string qry_name = "select ISNULL(stud_F_Name,'')+' '+ISNULL(stud_M_Name,'')+' '+ISNULL(stud_L_Name,'')+' '+ISNULL(stud_Mother_FName,'') as Name  ,course_name from m_std_personaldetails_tbl as a,m_crs_course_tbl as b ,m_crs_subcourse_tbl as c ,m_std_studentacademic_tbl as d where a.stud_id=d.stud_id and d.subcourse_Id=c.subcourse_id and c.course_id=b.course_id and a.stud_id='" + id + "' ";
                    DataSet st_name = cls.fill_dataset(qry_name);
                    lblname.Text = st_name.Tables[0].Rows[0]["Name"].ToString();
                    //if (ds.Tables[0].Rows[0]["sem_id"].ToString()=="Sem-3"|| ds.Tables[0].Rows[0]["sem_id"].ToString() == "Sem-4"|| ds.Tables[0].Rows[0]["sem_id"].ToString() == "Sem-5"|| ds.Tables[0].Rows[0]["sem_id"].ToString() == "Sem-6")
                    //{
                        if (dt_new_ch.Tables[0].Rows.Count > 0)
                        {
                            if (dt_new_ch.Tables[0].Rows[0]["Pattern"].ToString().Contains("CS"))
                            {
                                lblprogram.Text = "Choice Base";
                                lblprogram2.Text = "Choice Base";
                            }

                            else
                            {
                                lblprogram.Text = dt_new_ch.Tables[0].Rows[0]["Pattern"].ToString();
                                lblprogram2.Text = dt_new_ch.Tables[0].Rows[0]["Pattern"].ToString();
                            }
                            currexam = dt_new_ch.Tables[0].Rows[0]["Pattern"].ToString();

                        }
                    //}
                    string qry_ex = "select exam_code,exam_date from cre_exam where exam_code='" + ds.Tables[0].Rows[0]["exm_type"].ToString() + "'";
                    DataSet dsexm = cls2.fill_dataset(qry_ex);
                    lblstud_id.Text = id;
                    lblsem.Text = ds.Tables[0].Rows[0]["sem_id"].ToString() + " " + "(KT)";
                    lbl_chalan.Text = ds.Tables[0].Rows[0]["receipt_no"].ToString();
                    lbl_seat_no.Text = "--"; //ds.Tables[1].Rows[0]["seat_no"].ToString();
                    lbl_status.Text = "Unpaid";
                    lblfee.Text = ds.Tables[0].Rows[0]["amount"].ToString();
                    Label2.Text = dsexm.Tables[0].Rows[0]["exam_date"].ToString();
                    Label7.Text = dsexm.Tables[0].Rows[0]["exam_date"].ToString();
                    Label1.Text = st_name.Tables[0].Rows[0]["course_name"].ToString();
                    lblname2.Text = st_name.Tables[0].Rows[0]["Name"].ToString();

                    lblstud_id2.Text = id;
                    lblsem2.Text = ds.Tables[0].Rows[0]["sem_id"].ToString() + " " + "(KT)";
                    lbl_chalan2.Text = ds.Tables[0].Rows[0]["receipt_no"].ToString();//ds.Tables[0].Rows[0]["previous_stud_id"].ToString()
                    lbl_seat_no2.Text = "--";//; ds.Tables[1].Rows[0]["seat_no"].ToString();
                    lbl_status2.Text = "Unpaid";
                    lblfee2.Text = ds.Tables[0].Rows[0]["amount"].ToString();

                }
              
            }
        }
    }

    public string getWhileLoopData()
    {
        string htmlStr = "";
        ds = new DataSet();
        dt = new DataTable();
        string stud_id = Session["UserName"].ToString();
        string str = "select  (Status) as status  from processing_fees where form_no='" + stud_id + "' and ayid=(select max(ayid) from m_academic where Iscurrent=1) and  postingmer_txn='" + Session["postingmertxn"].ToString() + "' ";
       DataTable dtprocess = cls.fildatatable(str);
        string[] semno = dtprocess.Rows[0]["status"].ToString().Split(':');


        string ktexm = "select subject_name,ESE,IA,TW,[PR/OR] as prac from kt_exam where stud_id='" + stud_id + "' and exam_code=(select case when  MONTH(getdate()) IN('7','8','9','10','11','12') then 'Nov' else 'Jun' end as month) and curr_exam_code='" + currexam + "' and ayid=(select MAX(ayid) from m_academic where IsCurrent='1')  and sem_id='" + semno[2].ToString() + "'  and exam_code='" + semno[1].ToString() + "'";
        dt = cls.fildatatable(ktexm);

       // dt = (DataTable)Session["KT_exm_grid"];

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string sr_no = (i + 1).ToString();
            string Subj_Name = dt.Rows[i]["subject_name"].ToString();
            string theory_mrks = dt.Rows[i]["ESE"].ToString();
            string internal_mrks = dt.Rows[i]["IA"].ToString();
            string term_wrks = dt.Rows[i]["TW"].ToString();
            string pract_mrks = dt.Rows[i]["prac"].ToString();
            htmlStr += "<tr style='font-size:14px;'><td>" + sr_no + "</td><td>" + Subj_Name + "</td><td>" + theory_mrks + "</td><td>" + internal_mrks + "</td><td>" + term_wrks + "</td><td>" + pract_mrks + "</td></tr>";
        }
        return htmlStr;
    }
    public static string byteToHexString(byte[] byData)
    {
        StringBuilder sb = new StringBuilder((byData.Length * 2));
        for (int i = 0; (i < byData.Length); i++)
        {
            int v = (byData[i] & 255);
            if ((v < 16))
            {
                sb.Append('0');
            }

            sb.Append(v.ToString("X"));

        }

        return sb.ToString();
    }
}