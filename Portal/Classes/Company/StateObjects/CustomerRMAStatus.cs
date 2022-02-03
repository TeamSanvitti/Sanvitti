using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
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
}