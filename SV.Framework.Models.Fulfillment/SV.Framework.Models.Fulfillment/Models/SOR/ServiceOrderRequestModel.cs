using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.SOR
{
    public class ServiceOrderRequestModel
    {
        public string SORNumber { get; set; }
        public int ServiceRequestID { get; set; }
        public int Quantity { get; set; }
        public int ItemCompanyGUID { get; set; }
        public int RequestedBy { get; set; }
        public int UserID { get; set; }
        [XmlIgnore]
        public int StatusID { get; set; }
        [XmlIgnore]
        public string Comments { get; set; }
        [XmlIgnore]
        public string Status { get; set; }
        [XmlIgnore]
        public string RequestedUserBy { get; set; }
        [XmlIgnore]
        public string CreatedUserBy { get; set; }
        [XmlIgnore]
        public string SORDate { get; set; }
        [XmlIgnore]
        public DateTime CreateDate { get; set; }
        [XmlIgnore]
        public string CategoryName { get; set; }
        [XmlIgnore]
        public string SKU { get; set; }
        [XmlIgnore]
        public string ProductName { get; set; }
        [XmlIgnore]
        public string RequestData { get; set; }
        [XmlIgnore]
        public string FromDate { get; set; }
        [XmlIgnore]
        public string ToDate { get; set; }
        [XmlIgnore]
        public int CompanyID { get; set; }

    }

}
