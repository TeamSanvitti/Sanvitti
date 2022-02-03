using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Fulfillment
{
    public class FulfillmentModel
    {
        public int POID { get; set; }
        public string FulfillmentNumber { get; set; }
        public string FulfillmentDate { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string ContactName { get; set; }
        public string CompanyName { get; set; }
        public string ConnectionString { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string ValidationStatus { get; set; }
    }
}
