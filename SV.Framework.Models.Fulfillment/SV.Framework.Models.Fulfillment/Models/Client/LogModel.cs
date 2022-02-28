using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class LogModel
    {
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        public string ModuleName { get; set; }
        public DateTime RequestTimeStamp { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public DateTime ResponseTimeStamp { get; set; }
        public string ReturnMessage { get; set; }
        public bool ExceptionOccured { get; set; }
        public string ShortRequestData { get; set; }
        public string ShortResponseData { get; set; }

        public Int64 LogID { get; set; }
        public Int64 TimeDifference { get; set; }
    }

}
