using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.SOR
{
    public class ServiceRequestDownload
    {
        public string ServiceRequestNumber { get; set; }
        public string Date { get; set; }
        public string CategoryName { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }
        public string RequestedBy { get; set; }
        public string CreatedBy { get; set; }
        public string Status { get; set; }


    }
}
