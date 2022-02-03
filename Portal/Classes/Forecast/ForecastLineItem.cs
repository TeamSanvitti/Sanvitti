using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    public class ForecastLineItem
    {
        public string SKU { get; set; }
        public int? Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        public ForecastStatuses LineItemStatus { get; set; }
        public string Comments { get; set; }     

    }
    [Serializable]
    [Flags]
    public enum ForecastStatuses
    {
        [XmlEnumAttribute(Name = "Pending")]
        Pending = 1,
        [XmlEnumAttribute(Name = "Received")]
        Received = 2,
        [XmlEnumAttribute(Name = "Cancelled")]
        Cancelled = 3,
        [XmlEnumAttribute(Name = "Fulfilled")]
        Fulfilled = 4,
        [XmlEnumAttribute(Name = "OutOfOrder")]
        OutOfOder = 5,
        [XmlEnumAttribute(Name = "PartialFulfilled")]
        PartialFulfilled = 6,
        [XmlEnumAttribute(Name = "Deleted")]
        Deleted = 7
    }

}