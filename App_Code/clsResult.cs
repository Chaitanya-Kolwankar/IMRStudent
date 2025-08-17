using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;

/// <summary>
/// Summary description for clsResult
/// </summary>
public class clsResult
{

    DataSet ds;
    System.Data.DataTable dt;
    SqlDataAdapter da;
    public SqlConnection sqlconnect = new SqlConnection(ConfigurationManager.ConnectionStrings["connect1"].ConnectionString);
    SqlDataReader sdr;
    SqlCommand cmd = new SqlCommand();

	public clsResult()
	{
         
	}



    public void addRows(Table tbl,DataTable dt)
    {
        string[] str_dtCol = new string[] { "subject_code", "subject_name", "h2_out", "h2_pass", "h2", "h1_out", "h1_pass", "h1", "outOf", "formula", "obtain", "overall", "gradePoints", "creditEarned", "CXG", "sgpa" };
        bool first = true;
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            TableRow new_row = new TableRow();
            tbl.Rows.AddAt(1 + i, new_row);
            TableCell[] cel = new TableCell[16];
            for (int k = 0; k < 16; k++)
            {
                cel[k] = new TableCell();
                cel[k].Text = dt.Rows[i][str_dtCol[k]].ToString();
                tbl.Rows[1 + i].Cells.AddAt(k, cel[k]);
                if (k == 11)
                {
                    if (dt.Rows[i][str_dtCol[k]].ToString().Trim().Equals("F"))
                    {
                        new_row.BackColor = ColorTranslator.FromHtml("#F7EBEB");
                    }
                }
                if (k == 15)
                {
                    if (first)
                    {
                        cel[k].RowSpan = dt.Rows.Count;
                        first = false;
                    }
                    else
                    {
                        tbl.Rows[1 + i].Cells.Remove(cel[k]);
                    }
                }
                if (k == 1)
                {
                    cel[k].HorizontalAlign = HorizontalAlign.Left;
                }
            }
        }
    }
    public string Total_Grade(string SGPI1, int re)
    {
        string Grade;
        if (Convert.ToDouble(SGPI1) >= 7 && re == 1)
        {
            Grade = "O";
        }
        else if (Convert.ToDouble(SGPI1) >= 6 && Convert.ToDouble(SGPI1) <= 6.99 && re == 1)
        {
            Grade = "A";
        }
        else if (Convert.ToDouble(SGPI1) >= 5 && Convert.ToDouble(SGPI1) <= 5.99 && re == 1)
        {
            Grade = "B";
        }
        else if (Convert.ToDouble(SGPI1) >= 4 && Convert.ToDouble(SGPI1) <= 4.99 && re == 1)
        {
            Grade = "C";
        }
        else if (Convert.ToDouble(SGPI1) >= 3 && Convert.ToDouble(SGPI1) <= 3.99 && re == 1)
        {
            Grade = "D";
        }
        else if (Convert.ToDouble(SGPI1) >= 2 && Convert.ToDouble(SGPI1) <= 2.99 && re == 1)
        {
            Grade = "E";
        }
        else
        {
            Grade = "F";
        }
        return Grade;
    }

    public void opencon()
    {
        try
        {
            
            if (this.sqlconnect.State == System.Data.ConnectionState.Open)
            {
                sqlconnect.Close();
            }
            sqlconnect.Open();
        }
        catch (Exception ex)
        {
            ex.ToString();
        }
    }

    public DataSet filldata(string qry)
    {
        //fills dataset
        opencon();
        cmd = new SqlCommand(qry, sqlconnect);
        ds = new DataSet();
        da = new SqlDataAdapter(cmd);

        da.Fill(ds);
        sqlconnect.Close();
        return ds;
    }

    public System.Data.DataTable filldatatable(string qry)
    {
        //fills datatable
        opencon();
        cmd = new SqlCommand(qry, sqlconnect);
        da = new SqlDataAdapter(cmd);
        dt = new System.Data.DataTable();
        da.Fill(dt);
        sqlconnect.Close();
        return dt;
    }

    public bool DMLquerries(string qry)
    {
        // for insert Update delete
        opencon();
        cmd = new SqlCommand(qry, sqlconnect);
        cmd.CommandType = CommandType.Text;
        int i;
        cmd.CommandTimeout = 10800;
        i = cmd.ExecuteNonQuery();
        if (i > 0)
        {
            sqlconnect.Close();

            return true;
        }
        else
        {
            sqlconnect.Close();
            //sqlconnect.Dispose();
            return false;
        }
    }

    public String[] get_GradeForDeg(int m_obt, int outOf, int passMrk)
    {
        String[] arr = new String[3];
        if (m_obt != null && outOf != null)
        {
            int percent = (m_obt * 100) / outOf;

            if (percent < 40)
            {
                arr[0] = "F";
                arr[1] = "1";
                arr[2] = "Fail";
            }
            else if (percent >= 40 && percent < 45)
            {
                arr[0] = "E";
                arr[1] = "2";
                arr[2] = "Pass";
            }
            else if (percent >= 45 && percent < 50)
            {
                arr[0] = "D";
                arr[1] = "3";
                arr[2] = "Average";
            }
            else if (percent >= 50 && percent < 55)
            {
                arr[0] = "C";
                arr[1] = "4";
                arr[2] = "Fair";
            }
            else if (percent >= 55 && percent < 60)
            {
                arr[0] = "B";
                arr[1] = "5";
                arr[2] = "Good";
            }
            else if (percent >= 60 && percent < 70)
            {
                arr[0] = "A";
                arr[1] = "6";
                arr[2] = "Excellent";
            }
            else if (percent >= 70)
            {
                arr[0] = "O";
                arr[1] = "7";
                arr[2] = "Outstanding";
            }
        }
        return arr;

    }

    public String getExamFullName_console(String exam, string branch)
    {
        String exNm = String.Empty;
        switch (exam.ToUpper())
        {
            case "SEM-1":
                return exNm = "Result Sheet For First Year,Sem I(CBGS)," + branch + ",Exam:";
            case "SEM-2":
                return exNm = "Result Sheet For First Year,Sem II(CBGS)," + branch + ",Exam:";
            case "SEM-3":
                return exNm = "Result Sheet For Second Year,Sem III(CBGS)," + branch + ",Exam:";
            case "SEM-4":
                return exNm = "Result Sheet For Second Year,Sem IV(CBGS)," + branch + ",Exam:";
            case "SEM-5":
                return exNm = "Result Sheet For Third Year,Sem V(CBGS)," + branch + ",Exam:";
            case "SEM-6":
                return exNm = "Result Sheet For Third Year,Sem VI(CBGS)," + branch + ",Exam:";
            //case "SEM-7":
            //    return exNm = "Result Sheet For Third Year,Sem VI(CBGS) " + branch + ",Exam:-";
            //    return exNm = "BACHELOR OF, " + branch + " (Sem VII)";
            //case "SEM-8":
            //    return exNm = "BACHELOR OF, " + branch + " (Sem VIII)";
            //case "SEM-9":
            //    return exNm = "BACHELOR OF, " + branch + " (Sem IX)";
            //case "SEM-10":
            //    return exNm = "BACHELOR OF, " + branch + " (Sem X)";
        }
        return exNm;
    }

    public string Add_GraceMrksResult(string grace_mrk, string original_mrk)
    {
        string total = "";
        int h1 = Convert.ToInt32(original_mrk), h1_grace = 0;

        String temp = Convert.ToString(grace_mrk);

        if ((temp.Contains("#")) || (temp.Contains("*")) || (temp.Contains(",")) || (temp.Contains("^")) || (temp.Contains("@")))
        {
            if ((temp.Contains(",")))
            {
                int f1 = 0, f2 = 0;
                string[] s = temp.Split(',');
                bool forGrStar = true, forRes = true, forGrHash = true, for42flag = true;

                for (int i = 0; i < s.Length; i++)
                {
                    if ((temp.Contains("#")) && forGrHash == true)
                    {
                        s[i] = s[i].Remove(s[i].Length - 1).Trim().ToString();
                        f1 = Convert.ToInt32(s[i]);
                        forGrHash = false;
                        //0, s[i].LastIndexOf('#')));
                    }
                    else if ((temp.Contains("^")) && forRes == true)
                    {
                        s[i] = s[i].Remove(s[i].Length - 1).Trim().ToString();
                        f1 = Convert.ToInt32(s[i]);
                        forRes = false;
                        //, temp.LastIndexOf('#')));
                    }
                    else if ((temp.Contains("*")) && forGrStar == true)
                    {
                        s[i] = s[i].Remove(s[i].Length - 1).Trim().ToString();
                        //if (Convert.ToInt32(s[i]) <= h1_t)
                        //{
                        f1 = Convert.ToInt32(s[i]);
                        forGrStar = false;
                        //, s[i].LastIndexOf('*')));
                        //h1_t = h1_t - Convert.ToInt32(f1);
                        //}
                        //else
                        //{
                        //   // MessageBox.Show("Can't enter marks more than " + h1_t);
                        //    return total = "";
                        //}
                    }
                    else if ((temp.Contains("@")) && for42flag == true)
                    {
                        s[i] = s[i].Remove(s[i].Length - 1).Trim().ToString();
                        f1 = Convert.ToInt32(s[i]);
                        for42flag = false;
                        //, temp.LastIndexOf('#')));
                    }
                    h1_grace = f1 + f2;
                    f1 = f2;
                    f2 = h1_grace;
                }
            }
            else
            {
                if ((temp.Contains("#")))
                {
                    temp = temp.Remove(temp.Length - 1).Trim().ToString();
                    h1_grace = Convert.ToInt32(temp);
                    //, temp.LastIndexOf('#')));
                }
                else if ((temp.Contains("^")))
                {
                    temp = temp.Remove(temp.Length - 1).Trim().ToString();
                    h1_grace = Convert.ToInt32(temp);
                    //, temp.LastIndexOf('#')));
                }
                else if ((temp.Contains("*")))
                {
                    temp = temp.Remove(temp.Length - 1).Trim().ToString();
                    //if (Convert.ToInt32(temp) <= h1_t)
                    //{
                    h1_grace = Convert.ToInt32(temp);
                    //, temp.LastIndexOf('*')));
                    //h1_t = h1_t - Convert.ToInt32(h1_grace);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("Can't enter marks more than " + h1_t);
                    //    return total="";
                    //}

                }
                else if ((temp.Contains("@")))
                {
                    temp = temp.Remove(temp.Length - 1).Trim().ToString();
                    h1_grace = Convert.ToInt32(temp);
                    //, temp.LastIndexOf('#')));
                }
            }
        }
        else
        {
            h1_grace = 0;
        }

        total = Convert.ToString(h1 + h1_grace);

        return total;

    }

    public string sem_value(string sem)
    {

        switch (sem.ToLower())
        {
            case "sem-1":
                return "SEMESTER I";
            case "sem-2":
                return "SEMESTER II";
            case "sem-3":
                return "SEMESTER III";
            case "sem-4":
                return "SEMESTER IV";
            case "sem-5":
                return "SEMESTER V";
            case "sem-6":
                return "SEMESTER VI";
            case "sem-7":
                return "SEMESTER VII";
            case "sem-8":
                return "SEMESTER VIII";
            case "sem-9":
                return "SEMESTER IX";
            case "sem-10":
                return "SEMESTER X";
            default:
                return "";
        }
    }

    public String[] imp_subjGrade(string mObt, string mOutOf, string[] grade)
    {
        string[] subj = grade;
        if (grade[0] == "A")
        {
            for (int newMrk = Convert.ToInt32(mObt); newMrk <= Convert.ToInt32(mObt) + 3; newMrk++)
            {
                subj = get_GradeForDeg(newMrk, Convert.ToInt32(mOutOf), 0);
                if (subj[0] == "O")
                {
                    //subj[2] = newMrk.ToString();
                    subj[2] = mObt.ToString();
                    break;
                }
            }
        }
        return subj;
    }

        public String[] imp_OverAllGrade(string mObt, string mOutOf, string[] grade)
        {
            string[] subj = grade;
            if (grade[0] == "A" || grade[0] == "B")
            {
                for (int newMrk = Convert.ToInt32(mObt); newMrk <= Convert.ToInt32(mObt) + 3; newMrk++)
                {
                    subj = get_GradeForDeg(newMrk, Convert.ToInt32(mOutOf), 0);
                    if (grade[0] == "A")
                    {
                        if (subj[0] == "O")
                        {
                            subj[2] = newMrk.ToString();
                            break;
                        }
                    }
                    else if (grade[0] == "B")
                    {
                        if (subj[0] == "A")
                        {
                            subj[2] = newMrk.ToString();
                            break;
                        }
                    }
                }
            }
            return subj;
        }

        public int getPtsFrmGrade(String grade)
        {
            int points = 11;
            switch (grade.ToUpper())
            {
                case "F":
                    return points = 1;


                case "E":
                    return points = 2;

                case "D":
                    return points = 3;

                case "C":
                    return points = 4;

                case "B":
                    return points = 5;

                case "A":
                    return points = 6;

                case "O":
                    return points = 7;

            }
            return points;
        }
}