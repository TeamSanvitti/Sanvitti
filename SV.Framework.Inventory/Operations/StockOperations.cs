using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class StockOperations : BaseCreateInstance
    {   
        public  int RunningStockInsertUpdate(string currentDate)
        {
            SV.Framework.DAL.Inventory.StockOperations stockOperations = SV.Framework.DAL.Inventory.StockOperations.CreateInstance<SV.Framework.DAL.Inventory.StockOperations>();

            int returnValue = stockOperations.RunningStockInsertUpdate(currentDate);
            
            return returnValue;
        }
        
        
    }
}
