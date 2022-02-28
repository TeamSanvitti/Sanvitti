using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.RMA
{
    public class RmaDetailModel
    {
        [XmlIgnore]
        public string ErrorMessage { get; set; }
        public int? RmaDetGUID { get; set; }
        public int? Warranty { get; set; }
        public int? Reason { get; set; }
        public string Notes { get; set; }
        public string ESN { get; set; }
        [XmlIgnore]
        public string SKU { get; set; }
        [XmlIgnore]
        public string Status { get; set; }
        [XmlIgnore]
        public int SKUQty { get; set; }
        public int? PO_ID { get; set; }
        public int? POD_ID { get; set; }
        public int? StatusID { get; set; }
        public int? ItemCompanyGUID { get; set; }
        public int? Quantity { get; set; }
        [XmlIgnore]
        public string RmaNumber { get; set; }
        [XmlIgnore]
        public string ProductName { get; set; }
        public string TriageNotes { get; set; }
        [XmlIgnore]
        public int RmaGUID { get; set; }
        //public int? RmaDelGUID { get; set; }
        public int? DispositionID { get; set; }
        public int? TriageStatusID { get; set; }

    }

}
