using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class ShippingLabelInfo
    {
        public byte[] ShippingLabel { get; set; }
        public string ShippingLabelImage { get; set; }
        public string FulfillmentNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string ShipmentType { get; set; }
        // public string ShipDate { get; set; }
        // public double Weight { get; set; }
        // public string Package { get; set; }
        //  public string ShipVia { get; set; }
        //  public string Comments { get; set; }
        //  public List<FulfillmentShippingLineItem> LineItems { get; set; }

    }
}
