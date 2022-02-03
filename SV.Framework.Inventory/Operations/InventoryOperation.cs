using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class InventoryOperation :BaseCreateInstance
    {        
        public InventoryList GetInventoryItems(int userID, int sim, int CompanyID)
        {
            SV.Framework.DAL.Inventory.InventoryOperation inventoryOperation = SV.Framework.DAL.Inventory.InventoryOperation.CreateInstance<SV.Framework.DAL.Inventory.InventoryOperation>();

            return inventoryOperation.GetInventoryItems(userID, sim, CompanyID);
        }
        public List<InventorySKU> GetInventorySKUList(int userID)
        {
            SV.Framework.DAL.Inventory.InventoryOperation inventoryOperation = SV.Framework.DAL.Inventory.InventoryOperation.CreateInstance<SV.Framework.DAL.Inventory.InventoryOperation>();

            return inventoryOperation.GetInventorySKUList(userID);
        }

    }
}
