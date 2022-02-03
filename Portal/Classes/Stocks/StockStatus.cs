using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;


namespace avii.Classes
{
    public class CompanyDetail
    {
        public string CompanyAccountNumber { get; set; }
        public string CompanyName { get; set; }
    }
    public class StockStatus
    {
        public string OpeningBalanceDate { get; set; }
        public string SKU { get; set; }
        public string ItemName { get; set; }
        //public string ModelNumber { get; set; }
        public string CategoryName { get; set; }
        public int OpeningBalance { get; set; }
        //public int StockInHand { get; set; }
        //public int StockAssignment { get; set; }
        //public int DefectiveESN { get; set; }
        public int StockReceived { get; set; }
        //public int PendingAssignment { get; set; }
        public int ClosingBalance { get; set; }
        //public int Reassignment { get; set; }
        //public List<ESNInfo> ESNLIst { get; set; }
    }
    public class CurrentStock
    {
        public string SKU { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; } 

        public int StockInHand { get; set; }
        [XmlIgnore]
        public bool IsDisable { get; set; }
        
    }
    public class ESNInfo
    {
        public string ESN { get; set; }
        public string MSL { get; set; }
        public string Icc_Id { get; set; }
    }
    public class CurrentStockRequest
    {

        private UserCredentials userCredentials;
        private string sku;
        

        public CurrentStockRequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }


        public string SKU
        {
            get
            {
                return sku;
            }
            set
            {
                sku = value;
            }
        }
        

    }
    public class CurrentStockResponse
    {
        private List<avii.Classes.CurrentStock> currentStocks;
        private string returnCode;
        private string comment;
        public CurrentStockResponse()
        {
            currentStocks = new List<CurrentStock>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<CurrentStock> CurrentStocks
        {
            get
            {
                return currentStocks;
            }
            set
            {
                currentStocks = value;
            }
        }
    }


    public class StockReceivalRequest
    {

        private UserCredentials userCredentials;
        private string sku;
        private DateTime from_date;
        private DateTime to_date;

        public StockReceivalRequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }
        public string SKU
        {
            get
            {
                return sku;
            }
            set
            {
                sku = value;
            }
        }
        public DateTime DateFrom
        {
            get
            {
                return from_date;
            }
            set
            {
                from_date = value;
            }
        }
        
        public DateTime DateTo
        {
            get
            {
                return to_date;
            }
            set
            {
                to_date = value;
            }
        }


    }
    public class StockReceivalResponse
    {
        private List<avii.Classes.StockStatus> stockReceivals;
        private string returnCode;
        private string comment;
        public StockReceivalResponse()
        {
            stockReceivals = new List<StockStatus>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<StockStatus> StockReceivals
        {
            get
            {
                return stockReceivals;
            }
            set
            {
                stockReceivals = value;
            }
        }
    }
    public class RunningStockResponse
    {
        private List<SV.Framework.Models.Inventory.StockCount> runningStock;
        private string returnCode;
        private string comment;
        public RunningStockResponse()
        {
            runningStock = new List<SV.Framework.Models.Inventory.StockCount>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<SV.Framework.Models.Inventory.StockCount> RunningStock
        {
            get
            {
                return runningStock;
            }
            set
            {
                runningStock = value;
            }
        }
    }



    public class RunningStockRequest
    {

        private UserCredentials userCredentials;
        private string sku;
        private DateTime from_date;
        private DateTime to_date;
        private bool includeDisabledSKU;

        public RunningStockRequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }
        public bool IncludeDisabledSKU
        {
            get
            {
                return includeDisabledSKU;
            }
            set
            {
                includeDisabledSKU = value;
            }
        }
        public string SKU
        {
            get
            {
                return sku;
            }
            set
            {
                sku = value;
            }
        }
        public DateTime DateFrom
        {
            get
            {
                return from_date;
            }
            set
            {
                from_date = value;
            }
        }

        public DateTime DateTo
        {
            get
            {
                return to_date;
            }
            set
            {
                to_date = value;
            }
        }


    }

}