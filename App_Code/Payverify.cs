using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace Payverify
{

    public class Payverify
    {

        public string merchantID { get; set; }
        public string merchantTxnID { get; set; }
        public string amt { get; set; }
        public string verified { get; set; }
        public string statusCode { get; set; }
        public string desc { get; set; }
        public string bid { get; set; }
        public string bankName { get; set; }
        public string atomTxnId { get; set; }
        public string discriminator { get; set; }
        public string surcharge { get; set; }
        public string cardNumber { get; set; }
        public string txnDate { get; set; }
        public string udf9 { get; set; }
        public string reconStatus { get; set; }
        public string sdt { get; set; }
    }
}
