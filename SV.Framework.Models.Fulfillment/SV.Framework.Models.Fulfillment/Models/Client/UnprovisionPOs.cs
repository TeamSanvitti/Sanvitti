using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class UnprovisionPOs
    {
        public int POID { get; set; }
        public string FulfillmentNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string CompanyName { get; set; }
        public DateTime PO_Date { get; set; }
        public string Contact_Name { get; set; }
        public string Contact_Phone { get; set; }
        public DateTime ShipTo_Date { get; set; }
        public string ShipTo_Address { get; set; }
        public string ShipTo_Address2 { get; set; }
        public string ShipTo_City { get; set; }
        public string ShipTo_State { get; set; }
        public string ShipTo_Zip { get; set; }
        public string Ship_Via { get; set; }
        public string Store_ID { get; set; }
        public DateTime requestedshipdate { get; set; }
        public string POSource { get; set; }
        public int LineItemCount { get; set; }
        public string POType { get; set; }
        public string StatusText { get; set; }
    }
    public class UnprovisionPORequest
    {
        public int UnprovisioningID { get; set; }
        public int POID { get; set; }
        public int CreatedBy { get; set; }
        public int RequestedBy { get; set; }
        public int ApprovedBy { get; set; }
        public string CustomerComment { get; set; }
        public string AdminComment { get; set; }
        public string Status { get; set; }

    }

    public class UnprovisionStatus
    {
        public int StatusID { get; set; }
        public string StatusText { get; set; }

    }
    public class POESNInfo
    {
        public string ContainerID { get; set; }
        public string ESN { get; set; }
        public int? ItemCompanyGUID { get; set; }
        public string BatchNumber { get; set; }
        public string ICCID { get; set; }
        public string TrackingNumber { get; set; }
        public string Location { get; set; }
       
    }
}
