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
/// Summary description for Class1
/// </summary>
public class Class1
{
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter da;
    DataSet ds = new DataSet();

    public SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connect1"].ConnectionString);

    public Class1()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string urls()
    {
        // return "https://vit.vivacollege.in/StudentPortalApi_eng/";
        return "http://localhost:17769/";
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
    public DataTable fildatatable(string query)
    {

        cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        Conn();
        da = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();

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

    public void err_cls(string ex)
    {
        string Path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
        System.IO.FileInfo Info = new System.IO.FileInfo(Path);
        string pageName = Info.Name;
        string qry, qrychk;
        qrychk = "Select * from  error_log where exception='" + ex.Replace("'", "''") + "' ";
        DataSet dschk = fill_dataset(qrychk);
        if (dschk.Tables[0].Rows.Count > 0)
        {
            qry = "update error_log set ondate=getdate(),url='" + pageName + "' where exception='" + ex.Replace("'", "''") + "' ";
        }
        else
        {
            qry = "insert into error_log values('" + ex.Replace("'", "''") + "',getdate(),'" + pageName + "','',null,null,null)";
        }
        DMLqueries(qry);
    }
    public DataSet fill_dataset_engg(string query)
    {
        SqlConnection con_engg = new SqlConnection(ConfigurationManager.ConnectionStrings["connect_new"].ConnectionString);
        cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con_engg;
        if (con_engg.State == ConnectionState.Open)
        {
            con_engg.Close();
            con_engg.Open();
        }
        else
        {
            con_engg.Open();
        }
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();

        da.Fill(ds);
        con_engg.Close();
        return ds;

    }




    public object SetdropdownForMember1(DropDownList ddl, string TABLE_NAME, string DATA_COLUMN, string VALUE_COLUMN, string CONDITION)
    {
        string Query;
        try
        {

            if (VALUE_COLUMN.Length > 0)
            {
                VALUE_COLUMN = "," + VALUE_COLUMN;
            }
            if (string.IsNullOrEmpty(CONDITION))
            {
                Query = "SELECT " + DATA_COLUMN + VALUE_COLUMN + " FROM " + TABLE_NAME;
            }
            else
            {
                //=============
                Query = "SELECT " + DATA_COLUMN + VALUE_COLUMN + " FROM " + TABLE_NAME + " where " + CONDITION;
            }
            ds = fill_dataset(Query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow drr = ds.Tables[0].NewRow();
                drr[0] = "--SELECT--";
                drr[1] = "0";
                ds.Tables[0].Rows.InsertAt(drr, 0);
                ddl.DataSource = ds.Tables[0];
                ddl.DataTextField = ds.Tables[0].Columns[0].ColumnName;
                ddl.DataValueField = ds.Tables[0].Columns[1].ColumnName;
                ddl.DataSource = null;
                ddl.DataBind();
                ddl.DataSource = ds.Tables[0];
                ddl.DataBind();
            }
        }
        catch (Exception ex)
        {
            return null;
        }
        return 0;
    }

    public DataTable fillDataTable(string query)
    {
        cmd = new SqlCommand();
        cmd.CommandText = query;
        cmd.CommandType = CommandType.Text;
        cmd.Connection = con;
        Conn();
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();

        da.Fill(ds);

        con.Close();
        return ds.Tables[0];
    }
}