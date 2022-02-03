using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Inventory
{
    public class clsInventoryDB : BaseCreateInstance
    {

        public  InventoryList GetInventoryItems(int userID, int sim, int CompanyID)
        {
            InventoryList inventory = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                arrSpFieldSeq = new string[] { "@userID", "@CompanyID", "@SIM" };

                try
                {
                    objCompHash.Add("@userID", userID);
                    objCompHash.Add("@CompanyID", CompanyID);
                    objCompHash.Add("@SIM", sim);
                    DataTable dataTable = db.GetTableRecords(objCompHash, "Aero_GetInventory", arrSpFieldSeq);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        inventory = PopulateInventoryList(dataTable);
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                    //db = null;
                }
            }
            return inventory;
        }
        public  bool ValidateEsnExists(string esn)
        {
            bool returnVal = true;
            using (DBConnect db = new DBConnect())
            {
                try
                {
                    if (!string.IsNullOrEmpty(esn))
                    {
                        string[] arrSpFieldSeq;
                        int returnValue=0;
                        Hashtable objCompHash = new Hashtable();
                        arrSpFieldSeq = new string[] { "@esn" };
                        try
                        {
                            objCompHash.Add("@esn", esn);
                            returnValue = (int)db.ExecuteScalar(objCompHash, "Aero_ValidateESN", arrSpFieldSeq);
                        }
                        catch (Exception objExp)
                        {
                            Logger.LogMessage(objExp, this);
                           // throw new Exception(objExp.Message.ToString());
                        }
                        finally
                        {
                            db.DBClose();
                           // db = null;
                            objCompHash = null;
                            arrSpFieldSeq = null;
                        }

                        if (returnValue == 0)
                        {
                            returnVal = false;
                        }
                        else
                        {
                            returnVal = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return returnVal;
        }

        private  InventoryList PopulateInventoryList(DataTable dataTable)
        {
            bool newItem = false;
            InventoryList inventoryList = new InventoryList();
            clsInventory inventory = default;//new clsInventory();

            foreach (DataRow dataRow in dataTable.Rows)
            {

                //if (inventory == null || inventory.ItemCode != (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false))
                //{
                inventory = new clsInventory();
                //    newItem = true;
                //}

                //if (newItem)
                //{
                inventory.SKU = (string)clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false);
                inventory.ItemCode = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                inventory.ItemID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemID", 0, true));
                inventory.ItemDescription = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                inventory.ItemName = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                inventory.LastUpdated = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "LastUpdate", "01/01/0001", false));
                //inventory.Price = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "whCost", 0, false));
                inventory.WarehouseCode = (string)clsGeneral.getColumnData(dataRow, "whCode", string.Empty, false);
                inventory.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock", 0, false));
                inventory.Phone = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Phone", string.Empty, false));
                inventory.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Active", string.Empty, false));
                inventoryList.CurrentInventory.Add(inventory);
                newItem = false;
                //}

            }

            return inventoryList;
        }


    }
}
