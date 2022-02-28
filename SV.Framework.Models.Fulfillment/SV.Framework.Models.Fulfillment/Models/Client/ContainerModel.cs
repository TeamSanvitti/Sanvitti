using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class ContainerModel
    {
        public String PostalCode { get; set; }
        public String Carrier { get; set; }
        public String PoNumber { get; set; }
        public String DPCI { get; set; }
        public String Casepack { get; set; }
        public String ContainerNumber { get; set; }
        public String Text { get; set; }
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
        public String ContainerCount { get; set; }
        public String ESNCount { get; set; }

    }
}
