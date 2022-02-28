using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class CustomerRmaReason
    {
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public int ActivationCoverage { get; set; }
        public int BatteryPower { get; set; }
        //public int BuyerRemorse { get; set; }
        //public int CoverageIssues { get; set; }
        //public int DOA { get; set; }
        //public int DropCalls { get; set; }
        public int HardwareIssues { get; set; }
        public int LiquidDamage { get; set; }
        //public int LoanerProgram { get; set; }
        public int MissingParts { get; set; }
        public int Others { get; set; }
        public int PhysicalAbuse { get; set; }
        //public int PowerIssues { get; set; }
        //public int ReturnToStock { get; set; }
        //public int ScreenIssues { get; set; }
        //public int ShippingError { get; set; }
        public int Software { get; set; }

    }
}
