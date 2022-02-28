using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class RmaInfo
    {
        public int RmaGUID { get; set; }
        public string CustomerContactName { get; set; }
        public string CustomerContactNumber { get; set; }
        public string CustomerAddress1 { get; set; }
        //public string Address2 { get; set; }
        public string CustomerCity { get; set; }
        public string CustomerState { get; set; }
        public string CustomerZipCode { get; set; }
        public string CustomerCountry { get; set; }
        public string StoreID { get; set; }
        public string RmaNumber { get; set; }
        public string FulfillmentNumber { get; set; }
        public string RmaStatus { get; set; }
        public string CustomerName { get; set; }
        public string RmaDate { get; set; }
        public string Status { get; set; }
        public string CustomerRMANumber { get; set; }
        public string CustomerEmail { get; set; }
        public string TriageStatus { get; set; }
        public string ReceiveStatus { get; set; }
        public string RMADocuments { get; set; }
        public List<RMADetail> RMADetails { get; set; }
        public List<RmaTracking> RmaTrackings { get; set; }
        public List<RmaComment> CustomerComments { get; set; }
        public List<RmaComment> InternalComments { get; set; }
        public List<RMAReceive> ReceiveList { get; set; }

        //public string MaxShipmentDate { get; set; }

    }

}
