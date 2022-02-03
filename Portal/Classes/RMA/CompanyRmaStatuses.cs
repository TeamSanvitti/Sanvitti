using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class CompanyRmaStatuses
    {
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public int Pending { get; set; }
        public int Received { get; set; }
        public int PendingforRepair { get; set; }
        public int PendingforCredit { get; set; }
        public int PendingforReplacement { get; set; }
        public int Approved { get; set; }
        public int Returned { get; set; }
        public int Credited { get; set; }
        public int Denied { get; set; }
        public int Closed { get; set; }
        public int OutwithOEMforrepair { get; set; }
        public int BacktoStockNDF { get; set; }
        public int BacktoStockCredited { get; set; }
        public int BacktoStockReplacedbyOEM { get; set; }
        public int RepairedbyOEM { get; set; }
        public int ReplacedBYOEM { get; set; }
        public int ReplacedBYAV { get; set; }
        public int RepairedByAV { get; set; }
        public int NDFNoDefectFound { get; set; }
        public int PREOWNEDAstock { get; set; }
        public int PREOWENDBStock { get; set; }
        public int PREOWENDCStock { get; set; }
        public int Rejected { get; set; }
        public int RTSReturnToStock { get; set; }
        public int Incomplete { get; set; }
        public int Damaged { get; set; }
        public int Preowned { get; set; }
        public int ReturntoOEM { get; set; }
        public int ReturnedtoStock { get; set; }
        public int Cancel { get; set; }
        public int ExternalESN { get; set; }
        public int PendingshiptoOEM { get; set; }
        public int SenttoOEM { get; set; }
        public int PendingshiptoSupplier { get; set; }
        public int SenttoSupplier { get; set; }
        public int ReturnedfromOEM { get; set; }
    }

    public class CustomerRmaStatus
    {
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public int Pending { get; set; }
        public int Completed { get; set; }
        public int Cancelled { get; set; }
        //public int PendingforCredit { get; set; }

    }
    public class CustomerRmaEsnStatus
    {
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public int Pending { get; set; }
        public int Completed { get; set; }
        public int Cancelled { get; set; }
        public int ExternalESN { get; set; }
        public int ReturnToStock { get; set; }
        public int SentToSupplier { get; set; }

    }
    public class CustomerRmaTriageStatus
    {
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public int Pending { get; set; }
        public int Complete { get; set; }
        public int InProcess { get; set; }
        public int NotRequired { get; set; }
        
    }
    public class CustomerRmaShipmentPaidBy
    {
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public int Customer { get; set; }
        public int Internal { get; set; }

    }
    public class CustomerRmaDisposition
    {
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public int Credit { get; set; }
        public int Discarded { get; set; }
        public int Repair { get; set; }
        public int Replaced { get; set; }

    }
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