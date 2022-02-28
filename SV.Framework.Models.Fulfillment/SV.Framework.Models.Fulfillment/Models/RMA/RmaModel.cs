using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class RmaModel
    {
        public int POID { get; set; }
        public int ReceiveStatusID { get; set; }
        public string ReceiveStatus { get; set; }
        public int TriageStatusID { get; set; }
        //public string FulfillemtNumber { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string ContactCountry { get; set; }
        public string StoreID { get; set; }
        public int ItemCompanyGUID { get; set; }
        public int Quantity { get; set; }
        public string RmaNumber { get; set; }
        public string FulfillmentNumber { get; set; }
        public string PoStatus { get; set; }
        public string Comment { get; set; }
        public string LanComment { get; set; }
        public string Status { get; set; }
        public string CustomerRMANumber { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public int RmaGUID { get; set; }
        public int CompanyID { get; set; }
        public int RmaStatusID { get; set; }
        public int UserID { get; set; }
        public int LoginUserID { get; set; }
        public string RMANUMBERAPI { get; set; }
        public bool IsAPI { get; set; }
        public bool DoNotSendShippingLabel { get; set; }
        public bool AllowShippingLabel { get; set; }
        public bool DocRePrint { get; set; }
        public string RMASource { get; set; }
        public DateTime RmaDate { get; set; }
        public DateTime MaxShipmentDate { get; set; }
        //[XmlIgnore]
        public List<RmaDetailModel> ExistingRmaDetail { get; set; }
        public List<RmaDetailModel> RmaDetail { get; set; }
        public List<RmaDocument> RmaDocumentList { get; set; }

    }

}
