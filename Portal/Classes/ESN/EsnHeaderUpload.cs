using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    public class EsnHeaderUpload
    {
        public int ESNHeaderId { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string ShipDate { get; set; }
        public string Shipvia { get; set; }
        public string TrackingNumber { get; set; }
        public int OrderQty { get; set; }
        public int ShipQty { get; set; }
        public string CustomerAccountNumber { get; set; }
        public decimal UnitPrice { get; set; }
        public string SKU { get; set; }
        public string Comments { get; set; }
        public bool IsESNRequired { get; set; }
        public string ReceivedAs { get; set; }
        public bool IsInspection { get; set; }
        public int UserId { get; set; }
        public List<EsnUploadNew> ESNs { get; set; }
    }
    public class EsnUploadNew
    {
        [XmlIgnore]
        public string ErrorMessage { get; set; }
        public string ESN { get; set; }
        public string MslNumber { get; set; }
        public string ICC_ID { get; set; }
        public string MeidHex { get; set; }
        public string MeidDec { get; set; }
        public string Location { get; set; }
        public string MSL { get; set; }
        public string OTKSL { get; set; }
        public string SerialNumber { get; set; }
        public string BoxID { get; set; }
        public string SNo { get; set; }
        [XmlIgnore]
        public bool InUse { get; set; }
        [XmlIgnore]
        public int EsnID { get; set; }

    }
    public class EsnHeaders
    {
        public string UserName { get; set; }
        public bool IsESN { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public int CompanyID { get; set; }
        public int ESNHeaderId { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string ShipDate { get; set; }
        public string Shipvia { get; set; }
        public string TrackingNumber { get; set; }
        public int OrderQty { get; set; }
        public int ShipQty { get; set; }
        public int ItemCompanyGUID { get; set; }
        public decimal UnitPrice { get; set; }
        public string SKU { get; set; }
        public String CompanyAccountNumber { get; set; }
        public List<EsnUploadNew> EsnList { get; set; }
        public string ProductName { get; set; }
        public string ReceivedAs { get; set; }
        public bool IsInspection { get; set; }
        //public string ICC_ID { get; set; }
    }
}