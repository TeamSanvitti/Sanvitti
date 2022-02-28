using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class PalletModel
    {
        public String FO { get; set; }
        public String PoNumber { get; set; }
        public String ShipDate { get; set; }
        public String PalletID { get; set; }
        public String SKU { get; set; }
        public String ItemCount { get; set; }
        public String CartonCount { get; set; }
        public String TotalPallet { get; set; }
        public String Comments { get; set; }
        public String SNo { get; set; }
        public String CompanyName { get; set; }
        public String AddressLine1 { get; set; }
        public String AddressLine2 { get; set; }
        public String State { get; set; }
        public String City { get; set; }
        public String ZipCode { get; set; }
        public String Country { get; set; }

        public String CustomerName { get; set; }
        public String ShippingAddressLine1 { get; set; }
        public String ShippingAddressLine2 { get; set; }
        public String ShippingCity { get; set; }
        public String ShippingState { get; set; }
        public String ShippingZipCode { get; set; }
        public String ShippingCountry { get; set; }
        public String ProductType { get; set; }
        public String OSType { get; set; }

    }

}
