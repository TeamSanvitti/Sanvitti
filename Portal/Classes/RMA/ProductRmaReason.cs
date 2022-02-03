using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class ProductRmaReason
    {
        public string ProductName { get; set; }
        public int DOA { get; set; }
        public int AudioIssues { get; set; }
        public int ScreenIssues { get; set; }
        public int PowerIssues { get; set; }
        public int Others { get; set; }
        public int MissingParts { get; set; }
        public int ReturnToStock { get; set; }
        public int BuyerRemorse { get; set; }
        public int PhysicalAbuse { get; set; }
        public int LiquidDamage { get; set; }
        public int DropCalls { get; set; }
        public int Software { get; set; }
        public int ActivationIssues { get; set; }
        public int CoverageIssues { get; set; }
        public int LoanerProgram { get; set; }
        public int ShippingError { get; set; }
        public int HardwareIssues { get; set; }
        public int Total { get; set; }

    }
}