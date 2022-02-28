using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.RMA
{
    public class CustomerRMAStatus
    {
        [XmlIgnore]
        public int CompanyID { get; set; }
        [XmlIgnore]
        public string StatusDescription { get; set; }
        public int StatusID { get; set; }
        //public bool? Active { get; set; }
    }
    public class CustomerRmaStatus
    {
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public int Pending { get; set; }
        public int Completed { get; set; }
        public int Cancelled { get; set; }
        //public int PendingforCredit { get; set; }

    }

}
