using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class FulfillmentOrderASN
    {
        public string ARZM { get; set; }
        public string PO { get; set; }
        public string FO { get; set; }
        public string Line { get; set; }
        public string Model { get; set; }
        public string ProductDescription { get; set; }
        public int Qty { get; set; }
        public int Cartons { get; set; }
        public string DateShipped { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Carrier { get; set; }
        public string TrackingNumber { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierCity { get; set; }
        public string SupplierState { get; set; }
        public int Pallets { get; set; }
        public decimal Weight { get; set; }
        public string UEDF_FileName { get; set; }
        public string UEDF_DateTime { get; set; }



    }

}
