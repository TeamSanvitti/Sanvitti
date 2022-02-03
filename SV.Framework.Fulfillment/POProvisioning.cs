using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Fulfillment
{
    public class POProvisioning
    {
        public string FulfillmentNumber { get; set; }
        public DateTime PODate { get; set; }
        public string POStatus { get; set; }
        public DateTime ProvisioningDate { get; set; }
        public DateTime ShipDate { get; set; }
        public DateTime RequestedShipDate { get; set; }
        public int Quantity { get; set; }
        public int POID { get; set; }
    }
    public class POProvisioningInfo
    {
        public List<FulfillmentHeader> FulfillmentHeaders { get; set; }
        public List<ProvisioningDetail> ProvisioningDetails { get; set; }
        
    }
    public class FulfillmentHeader
    {
        public string FulfillmentNumber { get; set; }
        public string PODate { get; set; }
        public string ShipTo_Date { get; set; }
        public string POStatus { get; set; }
        public string CategoryName { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
      //  public int POID { get; set; }
    }
    public class ProvisioningDetail    {
        public string ContainerID { get; set; }
        public string ESN { get; set; }
        public string HEX { get; set; }
        public string DEC { get; set; }
        public string Location { get; set; }
        public string BoxID { get; set; }
        public string SerialNumber { get; set; }
        //  public int POID { get; set; }
    }
}
