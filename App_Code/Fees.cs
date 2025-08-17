using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Fees
/// </summary>
public class Fees
{
    public string stud_id { get; set; }
    public string Structure { get; set; }
    public string amount { get; set; }
    public string transaction_id { get; set; }
    public string transaction_date { get; set; }
    public string bankname { get; set; }
    public string ayid { get; set; }
    public string prev_ayid { get; set; }
    public string category { get; set; }
    public string group_title { get; set; }
    public string group_id { get; set; }
    public string prev_group_id { get; set; }
    public string subtype { get; set; }
    public string type { get; set; }
    public string user_id { get; set; }
    public string pay_date { get; set; }
    public string recpt_mode { get; set; }
    public string recpt_chq_no { get; set; }
    public string recpt_chq_dt { get; set; }
    public string recpt_bank_branch { get; set; }
    public string chq_status { get; set; }
    public string fee_type { get; set; }
    public string remark { get; set; }
    public structure[] strarray { get; set; }
}

public class structure
{
    public string amount { get; set; }
    public string Structure { get; set; }
}