using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.SOR
{
    public class ServiceOrderDetail
    {
        public Int64 KitID { get; set; }

        [XmlIgnore]
        public bool IsSim { get; set; }
        [XmlIgnore]
        public int Qty { get; set; }

        [XmlIgnore]
        public int Id { get; set; }
        [XmlIgnore]
        public int OrderDetailId { get; set; }
        public string ESN { get; set; }
        //[XmlIgnore]
        public int ItemCompanyGUID { get; set; }
        [XmlIgnore]
        public bool IsPrint { get; set; }
        [XmlIgnore]
        public string ValidationMsg { get; set; }
        [XmlIgnore]
        public string ICCIDValidationMsg { get; set; }
        [XmlIgnore]
        public string BatchNumber { get; set; }
        //[XmlIgnore]
        public int MappedItemCompanyGUID { get; set; }
        public string ICCID { get; set; }
        //[XmlIgnore]
        public string SKU { get; set; }
        [XmlIgnore]
        public string MappedSKU { get; set; }
        [XmlIgnore]
        public string UPC { get; set; }
        public int RowNumber { get; set; }



    }

}
