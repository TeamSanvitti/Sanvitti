using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class ShippingLabelsDetails
    {

        public string ShippingLabelImage { get; set; }
        public string FulfillmentNumber { get; set; }
        public string TrackingNumber { get; set; }
        public string ShipmentType { get; set; }
        public string ShipDate { get; set; }
        public decimal Weight { get; set; }
        public decimal FinalPostage { get; set; }
        public string Package { get; set; }
        public string ShipVia { get; set; }
        public string Comments { get; set; }
        public bool IsManualTracking { get; set; }
        //  public List<FulfillmentShippingLineItem> LineItems { get; set; }
        public List<CustomValues> customValues { get; set; }

    }
}
