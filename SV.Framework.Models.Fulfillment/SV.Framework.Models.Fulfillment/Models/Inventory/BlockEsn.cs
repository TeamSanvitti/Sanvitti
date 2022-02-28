using System;
using System.Collections.Generic;
using System.Text;
//using System.Xml.Serialization;

namespace SV.Framework.Models.Inventory
{
    public class BlockEsn
    {
        public string ESN { get; set; }
       // [XmlIgnore]
        public string ErrorMessage { get; set; }
    }
    public class BlockESN
    {
        public int BlockID { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }
        
        public DateTime CreateDate { get; set; }
        public string CreateBy { get; set; }
        public string ReceiveBy { get; set; }
        public string ApprovedBy { get; set; }
        public string ESNDATA { get; set; }
    }
    public class BlockESNCSV
    {
        //public int BlockID { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string Action { get; set; }
        public string Status { get; set; }

        public DateTime CreateDate { get; set; }
        public string CreatedBy { get; set; }
        public string ReceivedBy { get; set; }
        public string ApprovedBy { get; set; }
        public string ESNDATA { get; set; }
    }

    public class BlockEsnInfo
    {
        public int UserID { get; set; }
        public int ReceivedBy { get; set; }
        public int ItemCompanyGUID { get; set; }
        public string Comment { get; set; }
        public string ESNData { get; set; }
    }
}
