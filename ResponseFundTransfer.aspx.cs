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


public partial class ResponseFundTransfer : System.Web.UI.Page
{
    Class1 c1 = new Class1();
    protected void Page_Load(object sender, EventArgs e)
    {
        //try
        //{
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
                 //  string respHashKey = "KEYRESP123657234";
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
                            lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                        }
                        else if (postingf_code == "C")
                        {
                            //Response.Redirect("http://localhost:2394/student/form_final.aspx");
                            lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                        }
                        else if (postingf_code == "S")
                        {
                            lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                        }
                        else if (postingf_code == "Ok")
                        {
                            lblStatus.Text = "Signature matched..." + postinamount + " " + postingbank_txn + " " + postingdate + " " + postingprod + " " + postingf_code + " " + postingbank_name + " " + signature + " " + postingdiscriminator;
                        }
                    }
                    else
                    {
//                        lblStatus.Text = "Signature Mismatched...";Response.Redirect("http://203.192.254.34/STUDENT_ERP/FEE_RECIEPT_COPY.ASPX");
                        //lblStatus.Text = "Signature Mismatched..."; Response.Redirect("http://localhost:2394/student/form_final.aspx");

                    }
                   //Response.Redirect("http://203.192.254.34/STUDENT_ERP/FEE_RECIEPT_COPY.ASPX?postingmmp_txn="+postingmmp_txn+"&postingmer_txn="+postingmer_txn+"&postinamount="+postinamount+"&postingprod="+postingprod+"&postingdate="+postingdate+"&postingbank_txn="+postingbank_txn+"&postingf_code="+postingf_code+"&postingbank_name="+postingbank_name+"&postingdiscriminator="+postingdiscriminator+"&customername="+customername+"&customermail="+customermail+"&customerno="+customerno);
                   // Response.Redirect("http://vivacollege.in/student/fee_reciept_copy_pay.ASPX?postingmmp_txn=" + postingmmp_txn + "&postingmer_txn=" + postingmer_txn + "&postinamount=" + postinamount + "&postingprod=" + postingprod + "&postingdate=" + postingdate + "&postingbank_txn=" + postingbank_txn + "&postingf_code=" + postingf_code + "&postingbank_name=" + postingbank_name + "&postingdiscriminator=" + postingdiscriminator + "&customername=" + customername + "&customermail=" + customermail + "&customerno=" + customerno);


                    //try
                    //{
                        String[] val = new String[0];
                        string id = "";

                        id = postingmer_txn.Substring(0, 8);
                        //if (postingmer_txn.Contains("2018"))
                        //{
                        //    val = postingmer_txn.Split('2018');
                        //    id = val[0] + "A";
                        //}
                        //else if (postingmer_txn.Contains("J")) { val = postingmer_txn.Split('A'); id = val[0] + "J"; }
                        string str = "    update processing_fees set name='" + customername + "',mobile_no='" + customerno + "',email='" + customermail + "',postinamount='" + postinamount + "',postingmmp_txn='" + postingmmp_txn + "',postingprod='" + postingprod + "',postingdate='" + postingdate + "',postingbank_txn='" + postingbank_txn + "',postingf_code='" + postingf_code + "',postingbank_name='" + postingbank_name + "',signature='Matched',postingdiscriminator='" + postingdiscriminator + "',curr_dt=getdate() where form_no='" + id + "' and postingmer_txn='" + postingmer_txn + "'";
                        //  string str = "insert into processing_fees values('" + id + "','" + customername + "','" + customerno + "','" + customermail + "','" + postinamount + "','" + postingmmp_txn + "','" + postingmer_txn + "','" + postingprod + "','" + postingdate + "','" + postingbank_txn + "','" + postingf_code + "','" + postingbank_name + "','Matched','" + postingdiscriminator + "','Fees','" + Session["ayid"].ToString() + "',getdate())";



                        c1.update_data(str);
                        DataSet newds = ((DataSet)Session["stud_data"]);
                        string trans_id = "";
                        if (postingf_code == "S" || postingf_code == "Ok")
                        {
                            //    string str11 = "select Duration from m_academic where ayid='" + newds.Tables[3].Rows[0]["to_year"].ToString() + "'";
                            //    Session["ayid"] = newds.Tables[3].Rows[0]["to_year"].ToString();
                            //    DataSet dt11 = c1.fill_dataset(str11);

                            //    string sum = Convert.ToString(Convert.ToUInt32(newds.Tables[3].Rows[0]["to_year"].ToString().Substring(5, 2)) + 1);

                            //    string str1 = "select MAX(Recpt_no) as rec_no from m_FeeEntry where ayid=(select ayid from m_academic where IsCurrent=1)";
                            //    lblstudentid.Text = Session["UserName"].ToString();
                            //    DataSet dt2 = c1.fill_dataset(str1);
                            //    trans_id = "";
                            //    if (dt2.Tables[0].Rows.Count > 0)
                            //    {
                            //        string count = Convert.ToString((Convert.ToInt32(dt2.Tables[0].Rows[0][0].ToString()) + 1));
                            //        trans_id =  count;
                            //    }
                            //    else
                            //    {
                            //        trans_id = "1";

                            //    }
                            //    string update = "update  www_m_std_personaldetails_tbl set flag=1 where  stud_id='" + id + "' and ayid='" + Session["ayid"].ToString() + "'";

                            //    c1.update_data(update);
                            //    string subcourse = "select * from m_crs_subcourse_tbl where subcourse_name='" + Session["subcourse"] + "'";
                            //    DataSet dt = c1.fill_dataset(subcourse);
                            //      string str123 = "select * from m_std_studentacademic_tbl where stud_id='" + id + "' and ayid=(select ayid from m_academic where iscurrent=1)";
                            //    DataSet dt123 = c1.fill_dataset(str123);
                            //    string str1234 = "select * from www_eligibility where stud_id='" + id + "' and to_year='" + Session["ayid"].ToString() + "'";
                            //    DataSet dt1234 = c1.fill_dataset(str1234);
                            //    string frm_sub = "select * from m_crs_subjectgroup_tbl where group_id='" + dt1234.Tables[0].Rows[0]["group_id"].ToString() + "'";
                            //    DataSet frm_sub1 = c1.fill_dataset(frm_sub);
                            //    if (dt123.Tables[0].Rows.Count == 0)
                            //    {
                            //        string temp_str = "if exists (select * from Temp_Student_data where stud_id='" + id + "' and ayid= '" + Session["ayid"].ToString() + "') update Temp_Student_data set from_Subcourse_id='" + frm_sub1.Tables[0].Rows[0]["subcourse_id"].ToString() + "',Group_Id='" + Session["groupid"] + "',Ayid='" + Session["ayid"].ToString() + "',To_Subcourse_id='" + dt.Tables[0].Rows[0]["subcourse_id"].ToString() + "',Div='',Roll_no='',Marks_Obtained='100',Out_Of_Mks='100',Remark=''  where Stud_Id ='" + id + " ' and ayid= '" + Session["ayid"].ToString() + "' else insert into Temp_Student_data values('" + id + "','" + frm_sub1.Tables[0].Rows[0]["subcourse_id"].ToString() + "','" + Session["groupid"] + "','" + Session["ayid"].ToString() + "','" + dt.Tables[0].Rows[0]["subcourse_id"].ToString() + "','','','100','100','','0')";
                            //        c1.update_data(temp_str);
                            //    string cs = ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;
                            //    using (SqlConnection con = new SqlConnection(cs))
                            //    {
                            //        using (SqlCommand cmd = new SqlCommand("insert_update_admission_fees1", con))
                            //        {
                            //            con.Open();
                            //            cmd.CommandType = CommandType.StoredProcedure;

                            //            cmd.Parameters.AddWithValue("@stud_id", id);
                            //            cmd.Parameters.AddWithValue("@subcourse_Id", dt.Tables[0].Rows[0]["subcourse_id"].ToString());
                            //            cmd.Parameters.AddWithValue("@group_id", Session["groupid"]);
                            //            cmd.Parameters.AddWithValue("@Course_tot_fees", 0);
                            //            cmd.Parameters.AddWithValue("@Course_Fee_Paid", 0);


                            //            cmd.Parameters.AddWithValue("@Course_fee_Bal", 0);

                            //            cmd.Parameters.AddWithValue("@Ayid", Session["ayid"].ToString());
                            //            cmd.Parameters.AddWithValue("@user_id", "Admin");
                            //            string message = Convert.ToString(cmd.ExecuteScalar());
                            //            string st = "select * from online_payment_grant where stud_id='" + id + "'";
                            //            DataSet ds = c1.fill_dataset(st);
                            //            string ins_fee = "";
                            //            if (ds.Tables[0].Rows.Count == 0)
                            //            {
                            //                ins_fee = "insert into m_FeeEntry select stud_id,Amount,a.ayid,GETDATE(),Struct_name,'Online Pay','" + trans_id + "',null,null,null,null,'Clear','Fee',null,null,'Admin',GETDATE(),null,'0',null from m_std_studentacademic_tbl as a,m_FeeMaster as b where stud_id='" + id + "' and a.group_id=b.Group_id and a.ayid=(select ayid from m_academic where IsCurrent=1) and a.ayid=b.Ayid";

                            //            }
                            //            else
                            //            {
                            //                ins_fee = "insert into m_FeeEntry select stud_id,Amount,a.ayid,GETDATE(),Struct_name,'Online Pay','" + trans_id + "',null,null,null,null,'Clear','Fee','" + ds.Tables[0].Rows[0]["category"] + "','freeship/scholarship','Admin',GETDATE(),null,'0',null from m_std_studentacademic_tbl as a,m_FeeMaster as b where stud_id='" + id + "' and a.group_id=b.Group_id and a.ayid=(select ayid from m_academic where IsCurrent=1) and a.ayid=b.Ayid and  Struct_name not like 'TUT%' union all select a.stud_id,case when stud_Category='OBC' then (cast(Amount as int)/2) else '0' end,a.ayid,GETDATE(),Struct_name,'Online Pay','',null,null,null,null,'Clear','Fee','" + ds.Tables[0].Rows[0]["category"] + "','freeship/scholarship','Admin',GETDATE(),null,'0',null from m_std_studentacademic_tbl as a,m_FeeMaster as b,m_std_personaldetails_tbl as c where a.stud_id=c.stud_id and c.stud_id='" + id + "' and a.group_id=b.Group_id and a.ayid=(select ayid from m_academic where IsCurrent=1) and a.ayid=b.Ayid and  Struct_name like 'TUT%'";


                            //            }
                            //            c1.update_data(ins_fee);
                            //        }
                            //    }
                            //    }
                            //}
                            string str_op = "select * from online_payment_grant where ayid=(select max(ayid) from m_academic ) and stud_id='" + id + "'";
                            DataSet dt = c1.fill_dataset(str_op);
                            QRCodeEncoder encoder = new QRCodeEncoder();

                            encoder.QRCodeScale = 5;
                            //string path = "E:\\website\\staff\\images\\";
                            //string img_path = "E:\\website\\staff\\images\\";
                            //string path = "E:\\inetpub\\wwwroot\\student\\images\\";
                            //string img_path = "E:\\inetpub\\wwwroot\\student\\images\\";
                            string s = "";

                            s = "Student ID : " + id + Environment.NewLine;
                            s = s + "STUDENT NAME : " + dt.Tables[0].Rows[0]["name"].ToString() + Environment.NewLine;

                            string str_grp = "select * from m_crs_subjectgroup_tbl where group_id='" + dt.Tables[0].Rows[0]["course"].ToString() + "'";
                            DataSet dsGrpID = c1.fill_dataset(str_grp);

                            //Bitmap img = encoder.Encode(s);
                            //System.Drawing.Image logo = System.Drawing.Image.FromFile(path + "vivalogo.png");

                            //int left = (img.Width / 2) - (logo.Width / 2);

                            //int top = (img.Height / 2) - (logo.Height / 2);
                            //Graphics g = Graphics.FromImage(img);
                            //g.DrawImage(logo, new Point(left, top));
                            //img.Save(img_path + "img_" + id.ToString() + ".jpg", ImageFormat.Jpeg);


                            //qr_code.ImageUrl = "img.jpg";
                            //qr_code1.ImageUrl = "img.jpg";


                            //if (ds.Tables[0].Rows.Count > 0)
                            //{

                            //    if (ds.Tables[0].Rows[0]["STUD_CATEGORY"].ToString() != "OPEN")
                            //    {
                            //        // tab_other_desc.Visible = true;
                            //        // tab_other_desc2.Visible = true;
                            //    }
                            //}
                            long total1 = Convert.ToInt32(postinamount.Split('.')[0]);
                          
                            // lbl_date1.Text = ds.Tables[0].Rows[0]["max_pay"].ToString();
                            lbl_date1.Text = postingdate;
                            long total2 = Convert.ToInt32(postinamount.Split('.')[0]);
                            
                            //lbl_no_1.Text = Session["UserName"].ToString();
                            lbl_amount_1.Text = ConvertNumbertoWords(total2) + " ONLY";
                            lbl_name_1.Text = dt.Tables[0].Rows[0]["name"].ToString();
                            // lbl_course_1.Text = ds.Tables[0].Rows[0]["Group_title"].ToString();
                            lbl_no_1.Text = trans_id;
                            //lbl_stud_id_1.Text = ds.Tables[0].Rows[0]["id"].ToString();

                            lbltransaction_id.Text = postingmmp_txn;
                            lblstatus1.Text = "Payment Successful";
                            lblcourse.Text = dsGrpID.Tables[0].Rows[0]["Group_title"].ToString();

                            if (postingdiscriminator == "NB")
                            {
                                lblmode.Text = "Net Banking";
                            }
                            else if (postingdiscriminator == "CC")
                            {
                                lblmode.Text = "Credit Cards";
                            }
                            else if (postingdiscriminator == "DC")
                            {
                                lblmode.Text = "Debit Cards";
                            }
                            else if (postingdiscriminator == "IM")
                            {
                                lblmode.Text = "IMPS";
                            }
                            else if (postingdiscriminator == "MX")
                            {
                                lblmode.Text = "American Express Cards";
                            }
                            lblamountdigits.Text = total2.ToString();
                            lblvivatransction.Text = postingmer_txn;
                            lblbank.Text = postingbank_name;
                           
                        }
                        else
                        {
                            if (postingf_code == "C" || postingf_code == "F")
                            {
                                lblstatus1.Text = "Payment Unsuccessful";
                            }
                            lbltransaction_id.Text = postingmmp_txn;
                            lblvivatransction.Text = postingmer_txn;
                            lbl_date1.Text = postingdate;
                            receipt.Visible = false;
                            tab_category2.Visible = false;
                            received_date.Visible = false;
                            amount.Visible = false;
                            words.Visible = false;
                            mode.Visible = false;
                            bankname.Visible = false;
                            group.Visible = false;
                            lblstudentid.Visible = false;
                            qrcode.Visible = false;
                            // divmsg.InnerHtml = "Details not found";
                        }
                        //string ss = (string)HttpContext.Current.Session["id"];
                    //---------------new com---------//
                        //string ss = id + "|" + trans_id;
                        ////string ss = id + "|16020015A/17-18/1";
                        //if (ss.Contains("|") == true)
                        //{
                        //    if (ss.Contains("duplicate") == true)
                        //    {
                        //        //lbl_cc.Visible = true;
                        //        //lbl_sc.Visible = true;
                        //    }
                        //    string[] arr = ss.Split('|');
                        //    Session["id"] = arr[0].ToUpper().ToString();
                        //    ss = arr[0].ToUpper().ToString();
                        //    if (arr.Length == 2)
                        //    {
                        //        Session["Recpt_no"] = arr[1].ToUpper().ToString().Trim();
                        //    }
                        //    else
                        //    {
                        //        Session["Recpt_no"] = "";
                        //    }


                        //}

                        //else
                        //{
                        //    Session["Recpt_no"] = "";
                        //}
                        //---------------new com---------//e

                        //string ss = (string)HttpContext.Current.Session["id"];
                        //if (HttpContext.Current.Session["id"] != null)
                        //{
                        //     ss = (string)HttpContext.Current.Session["id"];

                        //}

                        //if (string.IsNullOrEmpty(Session["UserName"].ToString()))
                        //{
                        //    Response.Redirect("login.aspx", false);
                        //}
                        //else
                        //{
                        //if (Convert.ToBoolean(Session["li_fee"]) == false)
                        //{
                        //    Response.Redirect("profile.aspx");
                        //}
                        //else
                        //{
                        //tab_other_desc.Visible = false;
                        //tab_other_desc2.Visible = false;
                        //    //---------------new com---------//s
                        //DataSet dsGrpID = c1.fill_dataset("select a.group_id,g.Group_title from dbo.m_std_studentacademic_tbl a,dbo.m_crs_subjectgroup_tbl g where a.group_id=g.Group_id and  stud_id='" + id + "' and a.del_flag=0 and a.ayid=(select MAX(ayid) from dbo.m_academic)");

                        //if (dsGrpID.Tables[0].Rows.Count > 0)
                        //{
                            //---------------new com---------//e
                            //string qry = "SELECT [ROLL NO],[STUDENT NAME],TOTAL_COURSE_FEES,SUM(amount) AS PAID,BALANCE,isnull((SELECT SUBSTRING((SELECT ( ', ' + Remark) FROM";
                            //qry += " m_feeentry t2 WHERE m.id = t2.Stud_id  ORDER BY id, Remark FOR XML PATH('')),3,1000)  ),'') as 'REMARK', STUD_CATEGORY,ID,[Group_title],group_id ";
                            //qry += " FROM Cheque_Status M where Status='Clear'and AYID=(select MAX(ayid) from dbo.m_academic)  and group_id='" + dsGrpID.Tables[0].Rows[0]["group_id"].ToString() + "' and ID='" + Session["id"].ToString() + "'";
                            //qry += "  GROUP BY [Student Name],[Roll no],TOTAL_COURSE_FEES,ID,BALANCE,Remark,STUD_CATEGORY,[Group_title],group_id, DISCOUNT_GIVEN,";
                            //qry += "  REFUND_GIVEN ORDER BY [Group_title], case when  isnumeric(substring([Roll no],0,2))<>1 then cast (substring([Roll no],";
                            //qry += "(PATINDEX('%[0-9]%',[Roll no])),len([Roll no]))as int) else cast([Roll no] as int)end ";


                            //qry += "select field_type,value from dbo.www_receipt where Group_id='" + dsGrpID.Tables[0].Rows[0]["group_id"].ToString() + "' and ayid = (select MAX(ayid) from dbo.m_academic) and del_flag=0";
                            //string qry = "SELECT Recpt_no, (select CONVERT(varchar,max(max_pay_dt),103) from Cheque_Status where ID='" + ss + "'  and ayid=(select max(ayid) from m_Academic)) max_pay, replace (replace([Pay date],' ','') ,':','') as Pay_Date_Time,[STUDENT NAME],TOTAL_COURSE_FEES,SUM(amount) AS PAID,BALANCE,isnull((SELECT SUBSTRING((SELECT ( ', ' + Remark) FROM";
                            //qry += " m_feeentry t2 WHERE m.id = t2.Stud_id  ORDER BY id,Recpt_no, Remark FOR XML PATH('')),3,1000)  ),'') as 'REMARK', STUD_CATEGORY,ID,[Group_title],group_id ,'YEAR 20'+substring( (select MAX(ayid) from dbo.m_academic),LEN((select MAX(ayid) from dbo.m_academic))-1,2)+' - 20'+ cast((cast(substring( (select MAX(ayid) from dbo.m_academic),LEN((select MAX(ayid) from dbo.m_academic))-1,2) as int)+1) as varchar) ayid,convert(varchar,(CONVERT(date,m.[Pay date],103)),103)  as Curr_Date";
                            //qry += " FROM Cheque_Status M where Status='Clear'and AYID=(select MAX(ayid) from dbo.m_academic)  and group_id='" + dsGrpID.Tables[0].Rows[0]["group_id"].ToString() + "' and ID='" + Session["id"].ToString() + "'";
                            //qry += "  GROUP BY [Student Name],[Roll no],TOTAL_COURSE_FEES,ID,Recpt_no,[Pay date],BALANCE,Remark,STUD_CATEGORY,[Group_title],group_id, DISCOUNT_GIVEN,";
                            //qry += "  REFUND_GIVEN ORDER BY [Group_title], case when  isnumeric(substring([Roll no],0,2))<>1 then cast (substring([Roll no],";
                            //qry += "(PATINDEX('%[0-9]%',[Roll no])),len([Roll no]))as int) else cast([Roll no] as int)end ";
                            //string qry = "";

                            //if (Session["Recpt_no"].ToString() != "")
                            //{
                            //    qry = "SELECT Recpt_no, (select CONVERT(varchar,max(max_pay_dt),103) from Cheque_Status where ID='" + ss + "'  and ayid=(select max(ayid) from m_Academic)) max_pay, replace (replace([Pay date],' ','') ,':','') as Pay_Date_Time,[STUDENT NAME],TOTAL_COURSE_FEES,SUM(amount) AS PAID,BALANCE,isnull((SELECT SUBSTRING((SELECT ( ', ' + Remark) FROM";
                            //    qry += " m_feeentry t2 WHERE m.id = t2.Stud_id  ORDER BY id,Recpt_no, Remark FOR XML PATH('')),3,1000)  ),'') as 'REMARK', STUD_CATEGORY,ID,[Group_title],group_id ,'YEAR 20'+substring( (select MAX(ayid) from dbo.m_academic),LEN((select MAX(ayid) from dbo.m_academic))-1,2)+' - 20'+ cast((cast(substring( (select MAX(ayid) from dbo.m_academic),LEN((select MAX(ayid) from dbo.m_academic))-1,2) as int)+1) as varchar) ayid,convert(varchar,(CONVERT(date,m.[Pay date],103)),103)  as Curr_Date";
                            //    qry += " FROM Cheque_Status M where Status='Clear'and AYID=(select MAX(ayid) from dbo.m_academic)  and group_id='" + dsGrpID.Tables[0].Rows[0]["group_id"].ToString() + "' and ID='" + Session["id"].ToString() + "' and Recpt_no like '%" + Session["Recpt_no"].ToString() + "%'";
                            //    qry += "  GROUP BY [Student Name],[Roll no],TOTAL_COURSE_FEES,ID,Recpt_no,[Pay date],BALANCE,Remark,STUD_CATEGORY,[Group_title],group_id, DISCOUNT_GIVEN,";
                            //    qry += "  REFUND_GIVEN ORDER BY [Group_title], case when  isnumeric(substring([Roll no],0,2))<>1 then cast (substring([Roll no],";
                            //    qry += "(PATINDEX('%[0-9]%',[Roll no])),len([Roll no]))as int) else cast([Roll no] as int)end ";
                            //}
                            //else
                            //{
                            //    qry = "SELECT Recpt_no, (select CONVERT(varchar,max(max_pay_dt),103) from Cheque_Status where ID='" + ss + "'  and ayid=(select max(ayid) from m_Academic)) max_pay, replace (replace([Pay date],' ','') ,':','') as Pay_Date_Time,[STUDENT NAME],TOTAL_COURSE_FEES,SUM(amount) AS PAID,BALANCE,isnull((SELECT SUBSTRING((SELECT ( ', ' + Remark) FROM";
                            //    qry += " m_feeentry t2 WHERE m.id = t2.Stud_id  ORDER BY id,Recpt_no, Remark FOR XML PATH('')),3,1000)  ),'') as 'REMARK', STUD_CATEGORY,ID,[Group_title],group_id ,'YEAR 20'+substring( (select MAX(ayid) from dbo.m_academic),LEN((select MAX(ayid) from dbo.m_academic))-1,2)+' - 20'+ cast((cast(substring( (select MAX(ayid) from dbo.m_academic),LEN((select MAX(ayid) from dbo.m_academic))-1,2) as int)+1) as varchar) ayid,convert(varchar,(CONVERT(date,m.[Pay date],103)),103)  as Curr_Date";
                            //    qry += " FROM Cheque_Status M where Status='Clear'and AYID=(select MAX(ayid) from dbo.m_academic)  and group_id='" + dsGrpID.Tables[0].Rows[0]["group_id"].ToString() + "' and ID='" + Session["id"].ToString() + "'";
                            //    qry += "  GROUP BY [Student Name],[Roll no],TOTAL_COURSE_FEES,ID,Recpt_no,[Pay date],BALANCE,Remark,STUD_CATEGORY,[Group_title],group_id, DISCOUNT_GIVEN,";
                            //    qry += "  REFUND_GIVEN ORDER BY [Group_title], case when  isnumeric(substring([Roll no],0,2))<>1 then cast (substring([Roll no],";
                            //    qry += "(PATINDEX('%[0-9]%',[Roll no])),len([Roll no]))as int) else cast([Roll no] as int)end ";
                            //}
                            //qry += "select field_type,value,column1 from dbo.www_receipt where Group_id='" + dsGrpID.Tables[0].Rows[0]["group_id"].ToString() + "' and ayid = (select MAX(ayid) from dbo.m_academic) and del_flag=0 order by cast (column1  as int)";
                            //DataSet ds = c1.fill_dataset(qry);
                        //---------------new com---------//s
                            //string st = "select  a.stud_id,(stud_F_Name+' '+stud_M_Name+' '+stud_L_Name) as Name,d.Group_title as course,stud_Category as category,SUM(amount) as fees,b.Ayid from m_std_personaldetails_tbl as a,m_std_studentacademic_tbl as c,m_FeeEntry as b,m_crs_subjectgroup_tbl as d where a.stud_id='" + id + "' and a.stud_id=b.Stud_id and b.Ayid=(select Ayid from m_academic where IsCurrent=1) and b.Stud_id=c.stud_id and c.ayid=b.Ayid and c.group_id=d.group_id group by a.stud_id,stud_F_Name,stud_M_Name,stud_L_Name,Group_title,stud_Category,b.Ayid";
                            //DataSet ds5 = c1.fill_dataset(st);

                            //if (ds5.Tables[0].Rows.Count > 0)
                            //{
                                
                            //    tab_category2.Visible = true;
                            //    lbl_category_1.Text = ds5.Tables[0].Rows[0]["category"].ToString();
                              

                            //}
                            //else
                            //{
                                
                            //    tab_category2.Visible = false;
                            //    lbl_category_1.Text = "OPEN";
                            //}
                        //---------------new com---------//e

                            //string qry2 = "   select distinct 'Total Fees' 'Text',cast(TOTAL_COURSE_FEES as varchar(max))+'/-' 'value' from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 union all  select  CASE WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) IN ( 11, 12, 13 ) THEN CAST(row_number() over(partition by id order by convert(date,[Pay date],105)) AS VARCHAR(10)) + 'th'  WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) % 10 = 1 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10)) + 'st'  WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) % 10 = 2 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10)) + 'nd'  WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) % 10 = 3 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10)) + 'rd'  ELSE CAST(row_number() over(partition by id order by convert(date,[Pay date],105)) AS VARCHAR(10)) + 'th' END +' Installment Amount Dated on '+ cast(convert(date,[Pay date],105) as varchar(max))  ,cast(AMOUNT as varchar(max))+'/-' from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 and  convert(date,[Pay date],105)<>(select top 1 convert(date,[Pay date],105) from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 order by convert(date,[Pay date],105) desc ) union all select 'Received Fees' 'Text', cast(AMOUNT as varchar(max))+'/-' from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 and  convert(date,[Pay date],105)=(select top 1 convert(date,[Pay date],105) from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 order by convert(date,[Pay date],105) desc ) union all select 'Balance Fees' 'Text',case when BALANCE=0 then cast(BALANCE as varchar(MAX)) else  CAST(BALANCE as varchar(max))+'/-' end Balance from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 and    convert(date,[Pay date],105)=(select top 1 convert(date,[Pay date],105) from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 order by convert(date,[Pay date],105) desc ) ";

                            //-----------------------------------03/08/2017-----------------------------------
                            //string qry2 = "   select distinct 'Total Fees' 'Text',cast(TOTAL_COURSE_FEES as varchar(max))+'/-' 'value' from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 union all select  CASE WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) IN ( 11, 12, 13 ) THEN CAST(row_number() over(partition by id  order by convert(date,[Pay date],105)) AS VARCHAR(10)) + 'th'  WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) % 10 = 1 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10))  + 'st'  WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) % 10 = 2 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10)) + 'nd'  WHEN row_number() over(partition by id order by convert (date,[Pay date],105)) % 10 = 3 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10)) + 'rd'  ELSE CAST(row_number() over(partition by id order by convert(date,[Pay date],105)) AS VARCHAR(10)) + 'th' END +' Installment Amount Dated on '+ cast(convert(date,[Pay date],105) as varchar(max))  ,cast(AMOUNT as varchar(max))+'/-'  from Cheque_Status where ID='" + Session["id"].ToString() + "' and Status='clear' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0  and  [Pay date time]<>(select top 1 [Pay date time] from Cheque_Status where ID='" + Session["id"].ToString() + "'  and ayid=(select MAX(ayid) from dbo.m_academic) and Status='clear' and del_flag=0 order by [Pay date time] desc ) union all  select 'Received Fees' 'Text', cast(AMOUNT as varchar(max))+'/-' from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic)and Status='clear' and del_flag=0 and  [Pay date time]=(select top 1 [Pay date time] from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 and Status='clear' order by [Pay date time] desc ) union all select 'Balance Fees' 'Text',case when BALANCE=0 then cast(BALANCE as varchar(MAX)) else  CAST(BALANCE as varchar(max))+'/-' end Balance  from Cheque_Status where ID='" + Session["id"].ToString() + "' and Status='clear' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 and [Pay date time]=(select top 1 [Pay date time] from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and Status='clear' and del_flag=0 order by [Pay date time] desc ) ";

                            //DataSet dtpaid = c1.fill_dataset("SELECT SUM(AMOUNT) as Paid FROM m_feeentry where stud_id='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and chq_status='Clear' and del_flag=0");

                            //string qry2 = "";
                            ////  if (Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_COURSE_FEES"].ToString()) < Convert.ToInt32(dtpaid.Tables[0].Rows[0]["Paid"].ToString()))
                            ////  {
                            ////  qry2 = "   select distinct 'Total Fees' 'Text',cast(TOTAL_COURSE_FEES as varchar(max))+'/-' 'value' from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 union all select  CASE WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) IN ( 11, 12, 13 ) THEN CAST(row_number() over(partition by id  order by convert(date,[Pay date],105)) AS VARCHAR(10)) + 'th'  WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) % 10 = 1 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10))  + 'st'  WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) % 10 = 2 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10)) + 'nd'  WHEN row_number() over(partition by id order by convert (date,[Pay date],105)) % 10 = 3 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10)) + 'rd'  ELSE CAST(row_number() over(partition by id order by convert(date,[Pay date],105)) AS VARCHAR(10)) + 'th' END +' Installment Amount Dated on '+ cast(convert(date,[Pay date],105) as varchar(max))  ,cast(AMOUNT as varchar(max))+'/-'  from Cheque_Status where ID='" + Session["id"].ToString() + "' and Status='clear' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0  and  [Pay date time]<>(select top 1 [Pay date time] from Cheque_Status where ID='" + Session["id"].ToString() + "'  and ayid=(select MAX(ayid) from dbo.m_academic) and Status='clear' and del_flag=0 order by [Pay date time] desc ) union all  select 'Received Fees' 'Text', cast(AMOUNT as varchar(max))+'/-' from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic)and Status='clear' and del_flag=0 and  [Pay date time]=(select top 1 [Pay date time] from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 and Status='clear' order by [Pay date time] desc ) union all select 'Balance Fees' 'Text',case when BALANCE=0 then cast('0' as varchar(MAX)) else  CAST('0' as varchar(max))+'/-' end Balance  from Cheque_Status where ID='" + Session["id"].ToString() + "' and Status='clear' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 and [Pay date time]=(select top 1 [Pay date time] from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and Status='clear' and del_flag=0 order by [Pay date time] desc ) union all select distinct case when ((select sum(Amount) from m_feemaster where group_id=(select group_id from m_std_studentacademic_tbl where stud_id='" + Session["id"].ToString() + "' and ayid=(select max(ayid) from m_academic) and del_flag=0) and ayid=(select max(ayid) from m_academic) and del_flag=0)<(select sum(Amount) from m_feeentry where stud_id='" + Session["id"].ToString() + "' and ayid=(select max(ayid) from m_academic) and del_flag=0 and chq_status='Clear')) then case when (isnull((select sum(AMount) from m_feeentry where stud_id='" + Session["id"].ToString() + "' and ayid=(select max(ayid) from m_academic) and Remark='Refund' and del_flag=0),0)>0)then 'Refunded Amount'  else 'Refundable Amount' end else 'Refundable Amount' end 'Text', cast(((select sum(Amount) from m_feeentry where stud_id='" + Session["id"].ToString() + "' and ayid=(select max(ayid) from m_academic) and del_flag=0 and chq_status='Clear')-(select Amount from m_feemaster where group_id=(select group_id from m_std_studentacademic_tbl where stud_id='" + Session["id"].ToString() + "' and ayid=(select max(ayid) from m_academic) and del_flag=0) and ayid=(select max(ayid) from m_academic) and del_flag=0)) as varchar(max))+'/-' from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic)and del_flag=0 group by AMOUNT,TOTAL_COURSE_FEES";
                            //qry2 = "select text,case when value like '-%' then '0/-' else value end value from (select distinct 'Total Fees' 'Text',cast(TOTAL_COURSE_FEES as varchar(max))+'/-' 'value' from Cheque_Status where ID='" + Session["id"].ToString() + " ' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0"
                            //    + "    union all     select  CASE WHEN row_number() over(partition by id order by convert(date,[transaction_dt],105)) IN ( 11, 12, 13 ) THEN CAST(row_number() over(partition by id  order by convert(date,[Pay date],105)) AS VARCHAR(10)) + 'th'  WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) % 10 = 1 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10))  + 'st'  WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) % 10 = 2 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10)) + 'nd'  WHEN row_number() over(partition by id order by convert (date,[Pay date],105)) % 10 = 3 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10)) + 'rd'  ELSE CAST(row_number() over(partition by id order by convert(date,[Pay date],105)) AS VARCHAR(10)) + 'th' END +' Installment Amount Dated on '+ cast(convert(date,[Pay date],105) as varchar(max))  ,cast(AMOUNT as varchar(max))+'/-'  from Cheque_Status where ID='" + Session["id"].ToString() + " ' and Status='clear' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0  and  [transaction_dt]<>(select top 1 [transaction_dt] from Cheque_Status where ID='" + Session["id"].ToString() + " '  and ayid=(select MAX(ayid) from dbo.m_academic) and Status='clear' and del_flag=0 order by [transaction_dt] desc )     union all "
                            //    + "      select 'Received Fees' 'Text', cast(AMOUNT as varchar(max))+'/-' from Cheque_Status where ID='" + Session["id"].ToString() + " ' and ayid=(select MAX(ayid) from dbo.m_academic)and Status='clear' and del_flag=0 and  [transaction_dt]=(select top 1 [transaction_dt] from Cheque_Status where ID='" + Session["id"].ToString() + " ' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 and Status='clear' order by [transaction_dt] desc )      union all select 'Balance Fees' 'Text',case when BALANCE=0 then cast('0' as varchar(MAX)) else  CAST(balance as varchar(max))+'/-' end Balance  from Cheque_Status where ID='" + Session["id"].ToString() + " ' and Status='clear' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 and [Pay date time]=(select top 1 [Pay date time] from Cheque_Status where ID='" + Session["id"].ToString() + " ' and ayid=(select MAX(ayid) from dbo.m_academic) and Status='clear' and del_flag=0 order by [Pay date time] desc )"
                            //    + "      union all   select 'Refunded Amount'  'Text', cast((select sum(amount) from m_feeentry where Remark='refund' and stud_id='" + Session["id"].ToString() + "') as varchar(max))+'/-' union all select distinct 'Refundable Amount'  'Text', cast((((select sum(Amount) from m_feeentry where stud_id='" + Session["id"].ToString() + " ' and ayid=(select max(ayid) from m_academic) and del_flag=0 and chq_status='Clear')-(select Amount from m_feemaster where group_id=(select group_id from m_std_studentacademic_tbl where stud_id='" + Session["id"].ToString() + " ' and ayid=(select max(ayid) from m_academic) and del_flag=0) and ayid=(select max(ayid) from m_academic) and del_flag=0))-(isnull((select sum(amount) from m_feeentry where Remark='refund' and stud_id='" + Session["id"].ToString() + "'),0))) as varchar(max))+'/-' from Cheque_Status where ID='" + Session["id"].ToString() + " ' and ayid=(select MAX(ayid) from dbo.m_academic)and del_flag=0 group by AMOUNT,TOTAL_COURSE_FEES      ) a      where value is not null";

                            ////    }  //----------------------------end-------------------------
                            ////else
                            ////{
                            ////  qry2 = "   select distinct 'Total Fees' 'Text',cast(TOTAL_COURSE_FEES as varchar(max))+'/-' 'value' from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 union all select  CASE WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) IN ( 11, 12, 13 ) THEN CAST(row_number() over(partition by id  order by convert(date,[Pay date],105)) AS VARCHAR(10)) + 'th'  WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) % 10 = 1 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10))  + 'st'  WHEN row_number() over(partition by id order by convert(date,[Pay date],105)) % 10 = 2 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10)) + 'nd'  WHEN row_number() over(partition by id order by convert (date,[Pay date],105)) % 10 = 3 THEN CAST(row_number() over(partition by id order by id) AS VARCHAR(10)) + 'rd'  ELSE CAST(row_number() over(partition by id order by convert(date,[Pay date],105)) AS VARCHAR(10)) + 'th' END +' Installment Amount Dated on '+ cast(convert(date,[Pay date],105) as varchar(max))  ,cast(AMOUNT as varchar(max))+'/-'  from Cheque_Status where ID='" + Session["id"].ToString() + "' and Status='clear' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0  and  [Pay date time]<>(select top 1 [Pay date time] from Cheque_Status where ID='" + Session["id"].ToString() + "'  and ayid=(select MAX(ayid) from dbo.m_academic) and Status='clear' and del_flag=0 order by [Pay date time] desc ) union all  select 'Received Fees' 'Text', cast(AMOUNT as varchar(max))+'/-' from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic)and Status='clear' and del_flag=0 and  [Pay date time]=(select top 1 [Pay date time] from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 and Status='clear' order by [Pay date time] desc ) union all select 'Balance Fees' 'Text',case when BALANCE=0 then cast(BALANCE as varchar(MAX)) else  CAST(BALANCE as varchar(max))+'/-' end Balance  from Cheque_Status where ID='" + Session["id"].ToString() + "' and Status='clear' and ayid=(select MAX(ayid) from dbo.m_academic) and del_flag=0 and [Pay date time]=(select top 1 [Pay date time] from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from dbo.m_academic) and Status='clear' and del_flag=0 order by [Pay date time] desc ) ";
                            ////}
                            //DataSet ds1 = c1.fill_dataset(qry2);

                            ////string qry3 = "  select e.stud_id,per.stud_L_Name +' '+ per.stud_F_Name + ' '+ per.stud_M_Name as [Name] ,per.stud_Category,e.ayid as Chk_Ayid,Type,Recpt_mode,Struct_name,Amount,convert(varchar(10),Pay_date,103) as Pay_date,convert(varchar(23),  e.Curr_dt,121) as Curr_dt,Recpt_no,convert(varchar(10),Recpt_Chq_dt,103) as Recpt_Chq_dt,Recpt_Chq_No,Recpt_Bnk_Name,  Recpt_Bnk_Branch,Chq_status,Remark,Authorized_By , e.stud_id + ' | '+per.stud_L_Name+' | '+per.stud_F_Name +' | '  +per.stud_m_name+' | '+ cast(Recpt_no as varchar(max)) + ' | '+ e.ayid + ' | '+ cast(Amount as varchar(max)) + ' | '+ Recpt_Chq_No +' | '+convert(varchar(10),Recpt_Chq_dt,103) +' | '+   (case when stud_Gender=0 then 'Mr.' else 'Miss.' end)+ ' | '+ mrs.Group_title+ ' | '+ Recpt_Bnk_Name   AS Recipt  from m_feeentry  as e ,m_std_personaldetails_tbl as per ,m_crs_subjectgroup_tbl as mrs ,m_std_studentacademic_tbl as acd  where acd.ayid=e.Ayid and  acd.stud_id=e.Stud_id  and mrs.Group_id=acd.group_id and  per.stud_id=e.Stud_id and  e.del_flag = 0 and  Chq_status = 'clear' and e.Stud_id='" + Session["id"].ToString() + "'  and e.Ayid=(select MAX(ayid) from dbo.m_academic) and Recpt_mode like 'Cheque%'  order by stud_id,e.Ayid,Type,Recpt_mode ";

                            //string qry3 = "     select ayid,[Student Name] ,GROUP_TITLE,ID,TOTAL_COURSE_FEES,mode,amount,balance,[Pay date],[Pay date time],[Cheque No],[Bank Name],[Branch Name] from Cheque_Status where ID='" + Session["id"].ToString() + "' and ayid=(select MAX(ayid) from m_academic) and Status='clear' and mode in ('Cheque','DD')";
                            //DataSet ds4 = c1.fill_dataset(qry3);
                        //---------------new com---------//s
                            //QRCodeEncoder encoder = new QRCodeEncoder();

                            //encoder.QRCodeScale = 5;
                            ////string path = "E:\\website\\staff\\images\\";
                            ////string img_path = "E:\\website\\staff\\images\\";
                            //string path = "E:\\inetpub\\wwwroot\\student\\images\\";
                            //string img_path = "E:\\inetpub\\wwwroot\\student\\images\\";
                            //string s = "";

                            //s = "Student ID : " + id + Environment.NewLine;
                            //s = s + "STUDENT NAME : " + ds5.Tables[0].Rows[0]["name"].ToString() + Environment.NewLine;



                            //Bitmap img = encoder.Encode(s);
                            //System.Drawing.Image logo = System.Drawing.Image.FromFile(path + "vivalogo.png");

                            //int left = (img.Width / 2) - (logo.Width / 2);

                            //int top = (img.Height / 2) - (logo.Height / 2);
                            //Graphics g = Graphics.FromImage(img);
                            //g.DrawImage(logo, new Point(left, top));
                            //img.Save(img_path + "img_" + ss.ToString() + ".jpg", ImageFormat.Jpeg);


                            ////qr_code.ImageUrl = "img.jpg";
                            ////qr_code1.ImageUrl = "img.jpg";


                            ////if (ds.Tables[0].Rows.Count > 0)
                            ////{

                            ////    if (ds.Tables[0].Rows[0]["STUD_CATEGORY"].ToString() != "OPEN")
                            ////    {
                            ////        // tab_other_desc.Visible = true;
                            ////        // tab_other_desc2.Visible = true;
                            ////    }
                            ////}
                            //long total1 = Convert.ToInt32(ds5.Tables[0].Rows[0]["fees"].ToString());
                            //total1 = total1 + 100;
                            //// lbl_date1.Text = ds.Tables[0].Rows[0]["max_pay"].ToString();
                            //lbl_date1.Text = postingdate;
                            //long total2 = Convert.ToInt32(ds5.Tables[0].Rows[0]["fees"].ToString());
                            //total2 = total2 + 100;
                            ////lbl_no_1.Text = Session["UserName"].ToString();
                            //lbl_amount_1.Text = ConvertNumbertoWords(total2) + " ONLY";
                            //lbl_name_1.Text = ds5.Tables[0].Rows[0]["name"].ToString();
                            //// lbl_course_1.Text = ds.Tables[0].Rows[0]["Group_title"].ToString();
                            //lbl_no_1.Text = trans_id;
                            ////lbl_stud_id_1.Text = ds.Tables[0].Rows[0]["id"].ToString();
                       
                            //lbltransaction_id.Text = postingmmp_txn;
                            //lblstatus1.Text = "Payment Successful";
                            //lblcourse.Text = dsGrpID.Tables[0].Rows[0]["Group_title"].ToString();

                            //if (postingdiscriminator == "NB")
                            //{
                            //    lblmode.Text = "Net Banking";
                            //}
                            //else if (postingdiscriminator == "CC")
                            //{
                            //    lblmode.Text = "Credit Cards";
                            //}
                            //else if (postingdiscriminator == "DC")
                            //{
                            //    lblmode.Text = "Debit Cards";
                            //}
                            //else if (postingdiscriminator == "IM")
                            //{
                            //    lblmode.Text = "IMPS";
                            //}
                            //else if (postingdiscriminator == "MX")
                            //{
                            //    lblmode.Text = "American Express Cards";
                            //}
                            //lblamountdigits.Text = total2.ToString();
                            //lblvivatransction.Text = postingmer_txn;
                            //lblbank.Text = postingbank_name;
                            //img.Dispose();
                            //logo.Dispose();
                        //---------------new com---------//e
                            //if (ds.Tables[0].Rows.Count > 0)
                            //{
                            //    long feeTotal = Convert.ToInt32(ds.Tables[0].Rows[0]["TOTAL_COURSE_FEES"]);
                            //    long feePaid = Convert.ToInt32(ds.Tables[0].Rows[0]["PAID"]);
                            //    if (feeTotal == feePaid)
                            //    {
                            //        if (ds.Tables[1].Rows.Count > 0)
                            //        {
                            //            ds.Tables[1].Rows.Add(ds.Tables[1].NewRow());
                            //            long total = 0;
                            //            for (int i = 0; i < ds.Tables[1].Rows.Count; i++)
                            //            {
                            //                try
                            //                {
                            //                    total += Convert.ToInt32(ds.Tables[1].Rows[i]["value"].ToString());
                            //                }
                            //                catch (Exception ex2)
                            //                {
                            //                }
                            //            }
                            //            ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["field_type"] = "TOTAL FEES";
                            //            ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["value"] = total.ToString();
                            //            if (total == feeTotal)
                            //            {
                            //                Session["dsFee"] = ds;
                            //                //Response.Redirect("feeReceiptFull.aspx", false);
                            //                // viewReceipt.Visible = true;
                            //            }
                            //            else
                            //            {
                            //                //divmsg.InnerHtml = "Fee detail not proper";
                            //            }
                            //        }
                            //        else
                            //        {
                            //            //Fee details not found
                            //           // divmsg.InnerHtml = "Fee detail not found";
                            //        }
                            //    }
                            //    else
                            //    {
                            //        //divmsg.InnerHtml = "Full fees not paid";
                            //    }
                            //}
                            //else
                            //{
                            //    //full time fee detail not found
                            //    //divmsg.InnerHtml = "Admission detail not found";
                            //}

                            //DataSet ds1 = (DataSet)Session["dsFee"];
                            //GridView1.DataSource = ds.Tables[1];
                            //GridView1.DataBind();
                            //GridView1.Rows[ds.Tables[1].Rows.Count - 1].Font.Bold = true;
                            //GridView1.Rows[ds.Tables[1].Rows.Count - 1].Font.Size = 8;



                            //lblNo.Text = Session["UserName"].ToString();
                            //lblAmount.Text = ConvertNumbertoWords(total1) + " ONLY";
                            //lblName.Text = ds.Tables[0].Rows[0]["STUDENT NAME"].ToString();
                            //                    if (ds.Tables[0].Rows[0]["Group_title"].ToString() == "FIRST YEAR ARCHITECTURE")
                            //                    {
                            //                        //lblCourse.Text ="F.Y. Architecture";
                            //                        //lbl_course_1.Text = "F.Y. Architecture";


                            //                    }
                            //                    else if (ds.Tables[0].Rows[0]["Group_title"].ToString() == "SECOND YEAR ARCHITECTURE")
                            //                    {
                            //                        //lblCourse.Text = "S.Y. Architecture";

                            //                        //lbl_course_1.Text = "S.Y. Architecture";
                            //                    }
                            //                    else if (ds.Tables[0].Rows[0]["Group_title"].ToString() == "THIRD YEAR ARCHITECTURE")
                            //                    {
                            //                        //lblCourse.Text = "T.Y. Architecture";
                            //                        //lbl_course_1.Text = "T.Y. Architecture";

                            //                    }
                            //                    else if (ds.Tables[0].Rows[0]["Group_title"].ToString() == "FORTH YEAR ARCHITECTURE")
                            //                    {
                            //                        //lblCourse.Text = "FOTH Y. Architecture";
                            //                        //lbl_course_1.Text = "FOTH Y. Architecture";

                            //                    }
                            //                    else if (ds.Tables[0].Rows[0]["Group_title"].ToString() == "FIFTH YEAR ARCHITECTURE")
                            //                    {
                            //                       // lblCourse.Text = "FIFTH Y. Architecture";
                            //                       // lbl_course_1.Text = "FIFTH Y. Architecture";

                            //                    }
                            //                    else {

                            ////lblCourse.Text = ds.Tables[0].Rows[0]["Group_title"].ToString();
                            //                       // lbl_course_1.Text = ds.Tables[0].Rows[0]["Group_title"].ToString();

                            //                    }
                            // lblNo.Text = ds.Tables[0].Rows[0]["Recpt_no"].ToString();
                            //lbl_stud_id.Text = ds.Tables[0].Rows[0]["id"].ToString();
                            //lbl_category.Text = ds.Tables[0].Rows[0]["STUD_CATEGORY"].ToString();
                            //lbl_date.Text = ds.Tables[0].Rows[0]["max_pay"].ToString();

                            //lbl_year.Text = ds.Tables[0].Rows[0]["ayid"].ToString();
                            //lbl_year1.Text = ds.Tables[0].Rows[0]["ayid"].ToString();

                            //lblprintdt.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                            //lblprintdt1.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
                            //lbl_total_fees.Text = ds.Tables[0].Rows[0]["TOTAL_COURSE_FEES"].ToString();
                            //lbl_recievd_fees.Text = ds.Tables[0].Rows[0]["PAID"].ToString();
                            //lbl_balance_fees.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();


                            //string barcode_data = lblNo.Text  + ds.Tables[0].Rows[0]["Pay_Date_Time"].ToString();
                            //lbl_barcode.Text = lblNo.Text + ds.Tables[0].Rows[0]["Pay_Date_Time"].ToString();
                            //lblform.Text = "";
                            //lblform.Text = barcode_data;
                            //barcode(barcode_data);
                            //barcode1(barcode_data);
                            //grd_installment.DataSource = ds1.Tables[0];
                            //grd_installment.DataBind();

                            //grd_installment2.DataSource = ds1.Tables[0];
                            //grd_installment2.DataBind();

                            //grd_cheque_details.DataSource = ds4.Tables[0];
                            //grd_cheque_details.DataBind();

                            //grd_cheqye_2.DataSource = ds4.Tables[0];
                            //grd_cheqye_2.DataBind();

                            ////DataSet ds2 = (DataSet)Session["dsFee"];
                            //GridView2.DataSource = ds.Tables[1];
                            //GridView2.DataBind();
                            //GridView2.Rows[ds.Tables[1].Rows.Count - 1].Font.Bold = true;
                            //GridView2.Rows[ds.Tables[1].Rows.Count - 1].Font.Size = 8;


                            //lbl_total_fees_1.Text = ds.Tables[0].Rows[0]["TOTAL_COURSE_FEES"].ToString();
                            //lbl_recved_fees_1 .Text = ds.Tables[0].Rows[0]["PAID"].ToString();
                            //lbl_balance_fees_1.Text = ds.Tables[0].Rows[0]["BALANCE"].ToString();
                        //---------------new com---------//s
                        //}
                        //else
                        //{
                        //    if (postingf_code == "C" || postingf_code == "F")
                        //    {
                        //        lblstatus1.Text = "Payment Unsuccessful";
                        //    }
                        //    lbltransaction_id.Text = postingmmp_txn;
                        //    lblvivatransction.Text = postingmer_txn;
                        //    lbl_date1.Text = postingdate;
                        //    receipt.Visible = false;
                        //    tab_category2.Visible = false;
                        //    received_date.Visible = false;
                        //    amount.Visible = false;
                        //    words.Visible = false;
                        //    mode.Visible = false;
                        //    bankname.Visible = false;
                        //    group.Visible = false;
                        //    lblstudentid.Visible = false;
                        //    qrcode.Visible = false;
                        //    // divmsg.InnerHtml = "Details not found";
                        //}
                    //---------------new com---------//e
                        // }

                        //  HttpContext.Current.Session["id"] = null;
            //        }

            ////}
            //        catch (Exception ex1)
            //        {
            //            ScriptManager.RegisterClientScriptBlock(this, typeof(Page), "anything", "alert('" + ex1.Message + "');", true);

            //            // Response.Redirect("profile.aspx", false);
            //        }



                }
            }

     //   }

       // catch (Exception ex)
       // {
//Response.Redirect("http://203.192.254.34/STUDENT_ERP/FEE_RECIEPT_COPY.ASPX");
        //}
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

    public void barcode(string id)
    {
        string barCode = id;
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
        {
            using (Graphics graphics = Graphics.FromImage(bitMap))
            {

                Font oFont = new Font("IDAutomationHC39M", 16);
                //Font oFont = new Font("IDAutomationHC39M", 16);
                PointF point = new PointF(2f, 2f);
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush whiteBrush = new SolidBrush(Color.White);
                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();

                Convert.ToBase64String(byteImage);
                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
            //plBarCode2.Controls.Add(imgBarCode);

        }
    }
    public static byte[] ImageToByte2(System.Drawing.Image img)
    {
        byte[] byteArray = new byte[0];
        using (MemoryStream stream = new MemoryStream())
        {
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            stream.Close();

            byteArray = stream.ToArray();
        }
        return byteArray;
    }
    public void barcode1(string id)
    {
        string barCode = id;
        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
        using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
        {
            using (Graphics graphics = Graphics.FromImage(bitMap))
            {

                Font oFont = new Font("IDAutomationHC39M", 16);
                //Font oFont = new Font("IDAutomationHC39M", 16);
                PointF point = new PointF(2f, 2f);
                SolidBrush blackBrush = new SolidBrush(Color.Black);
                SolidBrush whiteBrush = new SolidBrush(Color.White);
                graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
            }
            using (MemoryStream ms = new MemoryStream())
            {
                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] byteImage = ms.ToArray();

                Convert.ToBase64String(byteImage);
                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
            }
            //plBarCode1.Controls.Add(imgBarCode);

        }
    }
}