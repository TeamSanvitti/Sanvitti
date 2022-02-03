using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class StockInDemand
    {
        public string CategoryName { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        
       // public string ProductName { get; set; }
        public int RequiredQunatity { get; set; }
        public int CurrentStock { get; set; }
        public int OrderCount { get; set; }
        public int CompanyID { get; set; }
        public int ItemCompanyGUID { get; set; }

    }
    public class StockInDemandCSV
    {
        public string CategoryName { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }

        // public string ProductName { get; set; }
        public int RequiredQunatity { get; set; }
        public int CurrentStock { get; set; }
        public int OrderCount { get; set; }
        //public int CompanyID { get; set; }

    }
}