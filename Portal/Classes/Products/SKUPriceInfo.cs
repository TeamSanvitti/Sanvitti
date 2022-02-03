using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class SKUPriceInfo
    {
        public string SKU { get; set; }
        public double SKUPrice { get; set; }
        public double SKULastPrice { get; set; }
        public string UserName { get; set; }
        public DateTime ChangeDate { get; set; }
        
    }
}