using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.DAL.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class clsInventoryDB : BaseCreateInstance
    {
        public InventoryList GetInventoryItems(int userID, int sim, int CompanyID)
        {
            SV.Framework.DAL.Inventory.clsInventoryDB clsInventorydb = SV.Framework.DAL.Inventory.clsInventoryDB.CreateInstance<SV.Framework.DAL.Inventory.clsInventoryDB>();
            InventoryList inventory = clsInventorydb.GetInventoryItems(userID, sim, CompanyID);
           
            
            return inventory;
        }
        //public static InventoryList GetInventoryItems(InventoryItemRequest inventoryItemRequest)
        //{
        //    InventoryList inventoryList = new InventoryList();
        //    Exception exc = null;
        //    int userId = AuthenticateRequest(inventoryItemRequest.Authentication, out exc);
        //    if (userId > 0)
        //    {
        //        inventoryList = clsInventoryDB.GetInventoryItems(userId, -1, 0);
        //    }
        //    return inventoryList;
        //}
        public  bool ValidateEsnExists(string esn)
        {
            SV.Framework.DAL.Inventory.clsInventoryDB clsInventorydb = SV.Framework.DAL.Inventory.clsInventoryDB.CreateInstance<SV.Framework.DAL.Inventory.clsInventoryDB>();
            bool returnVal = clsInventorydb.ValidateEsnExists(esn);
           
            return returnVal;
        }

    }
}
