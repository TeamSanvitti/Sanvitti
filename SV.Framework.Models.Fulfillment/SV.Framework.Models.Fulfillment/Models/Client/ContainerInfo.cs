using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    public class ContainerInfo
    {
        public string Code { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public int PoQuantity { get; set; }
        public int ContainerQuantity { get; set; }
        public int ContainerRequired { get; set; }
        public int PalletRequired { get; set; }
        public int PalletQuantity { get; set; }
        public int ItemCompanyGUID { get; set; }
        public int POID { get; set; }
        public int StatusID { get; set; }
        public int CurrentStock { get; set; }
        [XmlIgnore]
        public string ErrorMessage { get; set; }
        [XmlIgnore]
        public string KitType { get; set; }
        [XmlIgnore]
        public bool IsKittedBox { get; set; }
        public List<FulfillmentContainer> Containers { get; set; }
    }
}
