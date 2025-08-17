using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Security;
using System.Web.UI.WebControls;
/// <summary>
/// Summary description for new_Class2
/// </summary>
public class new_Class2
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da;
    DataSet ds = new DataSet();

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect_new"].ConnectionString);
	public new_Class2()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public void Conn()
    {
        if (con.State == ConnectionState.Open)
        {
            con.Close();
            con.Open();
        }
        else
        {
            con.Open();
        }

    }
    public void con_close()
    {
        con.Close();
    }
    public string Encryptdata(string password)
    {
        string strmsg = string.Empty;
        byte[] encode = new byte[password.Length];
        encode = Encoding.UTF8.GetBytes(password);
        strmsg = Convert.ToBase64String(encode);
        return strmsg;
    }

    public string Decryptdata(string encryptpwd)
    {
        string decryptpwd = string.Empty;
        UTF8Encoding encodepwd = new UTF8Encoding();
        Decoder Decode = encodepwd.GetDecoder();
        byte[] todecode_byte = Convert.FromBase64String(encryptpwd);
        int charCount = Decode.GetCharCount(todecode_byte, 0, todecode_byte.Length);
        char[] decoded_char = new char[charCount];
        Decode.GetChars(todecode_byte, 0, todecode_byte.Length, decoded_char, 0);
        decryptpwd = new String(decoded_char);
        return decryptpwd;
    }

    public List<ListItem> fillexam(string branch, string year)
    {
        string qry = "";
        //    qry = "SELECT exam_date+' '+ case atkt_exam when 1 then case when exam_code like 'E%' then '(A.T.K.T)' else '(Reval A.T.K.T)' end else case atkt_exam when 2 then case when exam_code like 'E%' then '(Additional)'  else '(Reval Additional)' end else case  when exam_code like 'E%' then '(Regular)' else '(Reval Regular)' end end end as a1,exam_code FROM cre_exam WHERE ayid='" + year + "' and branch_id='" + branch + "'  and           del_flag= 0 and exam_code not like '%RE%' and exam_code not like '%EXM360%' and exam_code not like '%EXM385%' and exam_code not like '%EXM389%' and exam_code not like '%EXM432%' and exam_code not like '%EXM399%' and is_lock=0 and exam_code in (select distinct exam_code from cre_marks_tbl where del_flag=0)";

        qry = " SELECT exam_date+' '+ case atkt_exam when 1 then case when exam_code like 'E%' then '(A.T.K.T)' else '(Reval A.T.K.T)' end else "
    + " case atkt_exam when 2 then case when exam_code like 'E%' then '(Additional)'  else '(Reval Additional)' end else case  when exam_code "
    + " like 'E%' then '(Regular)' else '(Reval Regular)' end end end as a1,exam_code FROM cre_exam WHERE ayid='" + year + "' and branch_id='" + branch + "'  and     "
    + " del_flag= 0  and exam_date not like '%2018%'"
    + " and is_lock=0 and exam_code in (select distinct exam_code from cre_marks_tbl where del_flag=0 and ayid='" + year + "') ";

        string constr = ConfigurationManager.ConnectionStrings["connect1"].ConnectionString;
        using (SqlConnection con = new SqlConnection(constr))
        {
            using (SqlCommand cmd = new SqlCommand(qry))
            {
                List<ListItem> course = new List<ListItem>();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        course.Add(new ListItem
                        {
                            Value = sdr["exam_code"].ToString(),
                            Text = sdr["a1"].ToString()
                        });
                    }
                }
                con.Close();
                return course;
            }
        }
    }

    public DataSet fill_dataset(string query)
    {

        cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        Conn();
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();

        da.Fill(ds);
        con_close();
        return ds;

    }

    public bool insert_data(string query)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = query;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insert_www_m_std_personaldetails_tbl";

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    public bool delete_data(string qry)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = qry;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            Conn();
            cmd.ExecuteNonQuery();
            con_close();
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public String checkNull(string s, DataSet ds1)
    {
        if (ds1.Tables[0].Rows.Count > 0)
        {
            if (ds1.Tables[0].Rows[0][s] == DBNull.Value)
            {
                return "";
            }
            else
            {
                return ds1.Tables[0].Rows[0][s].ToString();
            }
        }
        else
        {
            return "";
        }

    }

    public void filterExam(DropDownList cbo, string Query)
    {
        try
        {

            DataSet dsNew = fill_dataset(Query);
            if (dsNew.Tables[0].Rows.Count > 0)
            {
                dsNew.Tables[0].Columns.Remove("curr_date");
            }
            cbo.Items.Clear();
            cbo.Items.Add(new ListItem("--SELECT--", ""));
            for (int i = 0; i < dsNew.Tables[0].Rows.Count; i++)
            {
                cbo.Items.Add(new ListItem(dsNew.Tables[0].Rows[i][0].ToString(), dsNew.Tables[0].Rows[i][1].ToString()));

            }
            cbo.SelectedIndex = 0;

        }
        catch (Exception ex)
        {

        }

    }

    public bool DMLqueries(string qry)
    {
        // for insert Update delete
        con.Open();
        cmd = new SqlCommand(qry, con);
        cmd.CommandType = CommandType.Text;
        int i = 0;
        cmd.CommandTimeout = 10800;
        i = cmd.ExecuteNonQuery();
        if (i > 0)
        {
            con.Close();
            //con.Dispose();
            return true;
        }
        else
        {
            con.Close();
            //con.Dispose();
            return false;
        }
    }

    public bool execSp(string stud_id, string type, string title, string description, Byte[] size, string filetype, string filename)
    {
        try
        {
            SqlCommand cmd = new SqlCommand();
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            else
            {
                con.Close();
                con.Open();
            }
            cmd.Connection = con;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "proc_insrt_complain";
            cmd.Parameters.AddWithValue("@stud_id", stud_id);
            cmd.Parameters.AddWithValue("@type", type);
            cmd.Parameters.AddWithValue("@title", title);
            cmd.Parameters.AddWithValue("@description", description);
            cmd.Parameters.AddWithValue("@file", size);
            cmd.Parameters.AddWithValue("@filetype", filetype);
            cmd.Parameters.AddWithValue("@filename", filename);
            cmd.ExecuteScalar();
            con.Close();
            return true;
        }
        catch (Exception ex1)
        {
            return false;
        }
    }

    public bool update_data(string qry)
    {
        try
        {
            cmd = new SqlCommand();
            cmd.CommandText = qry;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            Conn();
            cmd.ExecuteNonQuery();
            con_close();
            return true;

        }
        catch (Exception ex)
        {
            return false;
        }
    }
}