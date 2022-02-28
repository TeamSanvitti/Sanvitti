using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    [Serializable]
    [XmlRootAttribute("FulfillmentAssignNonESN", Namespace = "", IsNullable = false)]
    public class FulfillmentAssignNonESN
    {

        private string sku;
        private int? itemCompanyGUID;
        private int? pod_id;
        private int? qty;
        private int currentStock;
        private bool isAssign;
        [XmlIgnore]
        public string ErrorMessage { get; set; }
        [XmlIgnore]
        public string ProductName { get; set; }
        [XmlIgnore]
        public string CategoryName { get; set; }
        [XmlIgnore]
        public string SKU
        {
            get { return sku; }
            set { sku = value; }
        }
        [XmlElement(ElementName = "ItemCompanyGUID", IsNullable = true)]
        public int? ItemCompanyGUID
        {
            get { return itemCompanyGUID; }
            set { itemCompanyGUID = value; }
        }
        [XmlElement(ElementName = "POD_ID", IsNullable = true)]
        public int? POD_ID
        {
            get { return pod_id; }
            set { pod_id = value; }
        }
        [XmlElement(ElementName = "Qty", IsNullable = true)]
        public int? Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        [XmlIgnore]
        public int CurrentStock
        {
            get { return currentStock; }
            set { currentStock = value; }
        }
        [XmlIgnore]
        public bool IsAssign
        {
            get { return isAssign; }
            set { isAssign = value; }
        }

    }
}
