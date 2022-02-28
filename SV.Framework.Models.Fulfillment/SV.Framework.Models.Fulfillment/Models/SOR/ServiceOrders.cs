using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.SOR
{
    public class ServiceOrders
    {
        public int ServiceOrderId { get; set; }
        public string ServiceOrderNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string SKU { get; set; }
        public string ESN { get; set; }
        public int CompanyId { get; set; }
        public string OrderDate { get; set; }
        public DateTime OrderDate2 { get; set; }
        public int KittedSKUId { get; set; }
        public int Quantity { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public List<ServiceOrderDetail> SODetail { get; set; }

    }

}
