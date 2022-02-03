using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;
using System.Collections;
using System.Data;

namespace SV.Framework.DAL.Inventory
{
    public class InventoryOperation : BaseCreateInstance
    {

        public  InventoryList GetInventoryItems(int userID, int sim, int CompanyID)
        {
            clsInventoryDB clsInventoryDB = clsInventoryDB.CreateInstance<clsInventoryDB>();

            return clsInventoryDB.GetInventoryItems(userID, sim, CompanyID);
        }
        public List<InventorySKU> GetInventorySKUList(int userID)
        {
            List<InventorySKU> inventorySKUList = default;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@UserID" };
                    dt = db.GetTableRecords(objCompHash, "av_CustomerInventoryList_Select", arrSpFieldSeq);
                    inventorySKUList = PopulateInventoryList(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //  db = null;
                }
            }
            return inventorySKUList;
        }
        private  List<InventorySKU> PopulateInventoryList(DataTable dataTable)
        {

            List<InventorySKU> inventoryItemList = default;// new List<InventorySKU>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                inventoryItemList = new List<InventorySKU>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    InventorySKU objInventoryItem = new InventorySKU();
                    objInventoryItem.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    objInventoryItem.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    objInventoryItem.ModelNumber = clsGeneral.getColumnData(dataRow, "ModelNumber", string.Empty, false) as string;
                    objInventoryItem.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    //objInventoryItem.Color = clsGeneral.getColumnData(dataRow, "color", string.Empty, false) as string;
                    objInventoryItem.UPC = clsGeneral.getColumnData(dataRow, "upc", string.Empty, false) as string;
                    objInventoryItem.OEM = clsGeneral.getColumnData(dataRow, "makername", string.Empty, false) as string;
                    //objInventoryItem.Carrier = clsGeneral.getColumnData(dataRow, "CarrierName", string.Empty, false) as string;
                    inventoryItemList.Add(objInventoryItem);
                }
            }
            return inventoryItemList;
        }


    }
}
