using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class ForecastInfo
    {
        public string ForecastNumber { get; set; }
        public DateTime ForecastDate { get; set; }
        //public string Comments { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public int ForecastID { get; set; }
        public string SKU { get; set; }
        public int? Quantity { get; set; }
        public double Price { get; set; }
        public double TotalPrice { get; set; }
        
       
    }
}