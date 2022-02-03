using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class ProductSKUsInfo
    {
        public string SKU { get; set; }
        public string CustomerName { get; set; }
        public int UsedEsnProcessed { get; set; }
        public int UsedEsnShipped { get; set; }
        public int UnusedESN { get; set; }
        public int RmaESN { get; set; }
        public int ItemCompanyGUID { get; set; }
    }
}