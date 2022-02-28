using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.SOR
{
    public class ServiceOrderRequestSummary
    {
        public string SORNumbers { get; set; }
        public string ServiceRequestIDs { get; set; }
        public int Quantity { get; set; }
        public string CategoryName { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }

        public int OrderCount { get; set; }

    }

}
