using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Inventory;
//using SV.Framework.Models.Fulfillment;


namespace SV.Framework.Models.Service
{

    public class RunningStockResponse
    {
        private List<StockCount> runningStock;
        private string returnCode;
        private string comment;
        public RunningStockResponse()
        {
            runningStock = new List<StockCount>();
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
        public List<StockCount> RunningStock
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


    public class CurrentStockResponse
    {
        private List<CurrentStock> currentStocks;
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
}
