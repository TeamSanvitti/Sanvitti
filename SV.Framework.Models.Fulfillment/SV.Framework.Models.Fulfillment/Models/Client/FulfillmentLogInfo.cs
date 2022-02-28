using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class FulfillmentLogInfo
    {
        public int POLogID { get; set; }
        public DateTime CreateDate { get; set; }
        public string ActionName { get; set; }
        public string UserName { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public string Status { get; set; }
        public string Comment { get; set; }
        // 
    }
}
