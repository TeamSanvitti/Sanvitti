using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
namespace avii.Classes
{
    public enum ESNSearch
    {
        ESN,
        PHONE_MODEL,
        ITEM_CODE
    }

    public class clsInventoryDB
    {

        public static DataTable GetMslList(ESNSearch SearchType, string Esn, int multiESN, string meid, string akey)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@SearchType", "@ESN", "@MultiESN", "@MEID", "@AKEY" };
            DataTable dataTable = null;
            try
            {
                objCompHash.Add("@SearchType", SearchType.ToString());
                objCompHash.Add("@ESN", Esn);
                objCompHash.Add("@MultiESN", multiESN);
                objCompHash.Add("@MEID", meid);
                objCompHash.Add("@AKEY", akey);
                

                dataTable = db.GetTableRecords(objCompHash, "Aero_Inventory_MSL_SELECT", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return dataTable;
        }
        public static DataTable GetESNList(List<avii.Classes.clsEsnxml> esnList)
        {
            string esnXML = EsnManagementDB.SerializeObjetToXML(esnList, "ArrayOfClsEsnxml", "clsEsnxml");
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@esnXML", esnXML);

                arrSpFieldSeq = new string[] { "@esnXML" };

                dataTable = db.GetTableRecords(objCompHash, "Aero_Inventory_MSL_ESN_SELECT", arrSpFieldSeq);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return dataTable;
        }
        
        public static List<EsnInfo> GetESNCleanUpList(List<avii.Classes.EsnList> esnList)
        {
            List<EsnInfo> esnInfoList = new List<EsnInfo>();
            string esnXML = clsGeneral.SerializeObject(esnList);
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@EsnXML", esnXML);

                arrSpFieldSeq = new string[] { "@EsnXML" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Inventory_Unused_ESN_MSL_SELECT", arrSpFieldSeq);


                esnInfoList = PopulateEsnInfoList(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return esnInfoList;
        }
        public static int RepositoryESNCleanUp(List<avii.Classes.EsnList> esnList, int userID, string fileName, string comment)
        {
            int returnValue = 0;
            //List<EsnInfo> esnInfoList = new List<EsnInfo>();
            string esnXML = clsGeneral.SerializeObject(esnList);
            //DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@EsnXML", esnXML);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@FileName", fileName);
                objCompHash.Add("@Comment", comment);

                arrSpFieldSeq = new string[] { "@EsnXML", "@UserID", "@FileName", "@Comment" };

                returnValue = db.ExecCommand(objCompHash, "Av_Inventory_MSL_Delete", arrSpFieldSeq);


                //esnInfoList = PopulateEsnInfoList(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return returnValue;
        }

        public static List<EsnInfo> GetESNQuery(List<avii.Classes.EsnList> esnList)
        {
            List<EsnInfo> esnInfoList = new List<EsnInfo>();
            string esnXML = clsGeneral.SerializeObject(esnList);
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@esnsearch_xml", esnXML);

                arrSpFieldSeq = new string[] { "@esnsearch_xml" };

                dataTable = db.GetTableRecords(objCompHash, "Aero_Inventory_MSL_SELECT", arrSpFieldSeq);


                esnInfoList = PopulateEsnDetailList(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return esnInfoList;
        }
        public static List<EsnInfo> GetESNQueryWithRepository(List<avii.Classes.EsnList> esnList)
        {
            List<EsnInfo> esnInfoList = new List<EsnInfo>();
            string esnXML = clsGeneral.SerializeObject(esnList);
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@esnsearch_xml", esnXML);

                arrSpFieldSeq = new string[] { "@esnsearch_xml" };

                dataTable = db.GetTableRecords(objCompHash, "Av_Inventory_MSL_SELECT", arrSpFieldSeq);


                esnInfoList = PopulateEsnDetailList(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return esnInfoList;
        }
        private static List<EsnInfo> PopulateEsnDetailList(DataTable dataTable)
        {
            List<EsnInfo> esnInfoList = new List<EsnInfo>();
            EsnInfo esnInfo = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    esnInfo = new EsnInfo();
                    esnInfo.Item_Code = (string)clsGeneral.getColumnData(dataRow, "Item_Code", string.Empty, false);
                    esnInfo.ESN = (string)clsGeneral.getColumnData(dataRow, "esn", string.Empty, false);
                    esnInfo.SKU = (string)clsGeneral.getColumnData(dataRow, "sku", string.Empty, false);
                    esnInfo.MslNumber = (string)clsGeneral.getColumnData(dataRow, "MSLNumber", string.Empty, false);
                    esnInfo.BatchNumber = (string)clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false);
                    esnInfo.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                    esnInfo.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PurchaseOrderNumber", string.Empty, false);
                    esnInfo.AKEY = (string)clsGeneral.getColumnData(dataRow, "Akey", string.Empty, false);
                    esnInfo.OTKSL = (string)clsGeneral.getColumnData(dataRow, "Otksl", string.Empty, false);
                    esnInfo.MEID = (string)clsGeneral.getColumnData(dataRow, "Meid", string.Empty, false);
                    esnInfo.AVPO = (string)clsGeneral.getColumnData(dataRow, "AVPO", string.Empty, false);
                    esnInfo.ICC_ID = (string)clsGeneral.getColumnData(dataRow, "icc_id", string.Empty, false);
                    esnInfo.LTE_IMSI = (string)clsGeneral.getColumnData(dataRow, "lte_imsi", string.Empty, false);

                    esnInfo.HEX = (string)clsGeneral.getColumnData(dataRow, "Hex", string.Empty, false);
                    esnInfo.RmaNumber = (string)clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false);
                    //esnInfo.LTEIMSI = (string)clsGeneral.getColumnData(dataRow, "lte_imsi", string.Empty, false);

                    esnInfo.PO_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true));
                    esnInfo.RmaGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGuid", 0, true));
                    esnInfo.IsESN = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "BadESN", false, true));

                    esnInfo.SimNumber = (string)clsGeneral.getColumnData(dataRow, "SimNumber", string.Empty, false);
                    esnInfo.CustomerName = (string)clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false);
                    esnInfo.NewSKU = (string)clsGeneral.getColumnData(dataRow, "newsku", string.Empty, false);
                    esnInfo.KITID = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "KITID", 0, false));
                    esnInfo.Location = (string)clsGeneral.getColumnData(dataRow, "Location", string.Empty, false);
                    esnInfo.ContainerID = (string)clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false);

                    esnInfoList.Add(esnInfo);

                }
            }

            return esnInfoList;

        }
        
        private static List<EsnInfo> PopulateEsnInfoList(DataTable dataTable)
        {
            List<EsnInfo> esnInfoList = new List<EsnInfo>();
            EsnInfo esnInfo = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    esnInfo = new EsnInfo();
                    esnInfo.Item_Code = (string)clsGeneral.getColumnData(dataRow, "Item_Code", string.Empty, false);
                    esnInfo.ESN = (string)clsGeneral.getColumnData(dataRow, "esn", string.Empty, false);
                    esnInfo.SKU = (string)clsGeneral.getColumnData(dataRow, "sku", string.Empty, false);
                    esnInfo.MslNumber = (string)clsGeneral.getColumnData(dataRow, "MSLNumber", string.Empty, false);
                    esnInfo.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                    esnInfo.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PurchaseOrderNumber", string.Empty, false);
                    esnInfo.AKEY = (string)clsGeneral.getColumnData(dataRow, "Akey", string.Empty, false);
                    esnInfo.OTKSL = (string)clsGeneral.getColumnData(dataRow, "Otksl", string.Empty, false);
                    esnInfo.MEID = (string)clsGeneral.getColumnData(dataRow, "Meid", string.Empty, false);
                    esnInfo.AVPO = (string)clsGeneral.getColumnData(dataRow, "AVPO", string.Empty, false);
                    esnInfo.ICC_ID = (string)clsGeneral.getColumnData(dataRow, "icc_id", string.Empty, false);
                    esnInfo.LTE_IMSI = (string)clsGeneral.getColumnData(dataRow, "lte_imsi", string.Empty, false);

                    esnInfo.HEX = (string)clsGeneral.getColumnData(dataRow, "Hex", string.Empty, false);
                    esnInfo.RmaNumber = (string)clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false);
                    //esnInfo.LTEIMSI = (string)clsGeneral.getColumnData(dataRow, "lte_imsi", string.Empty, false);

                    esnInfo.PO_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true));
                    esnInfo.RmaGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGuid", 0, true));
                    esnInfo.IsESN = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "BadESN", false, true));

                    esnInfo.SimNumber = (string)clsGeneral.getColumnData(dataRow, "SimNumber", string.Empty, false);
                    esnInfo.CustomerName = (string)clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false);
                    esnInfo.SKU = (string)clsGeneral.getColumnData(dataRow, "sku", string.Empty, false);

                    esnInfoList.Add(esnInfo);

                }
            }

            return esnInfoList;

        }
        public static DataTable GetESN_XML(List<avii.Classes.EsnList> esnList)
        {
            string esnXML = clsGeneral.SerializeObject(esnList);
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@esnsearch_xml", esnXML);

                arrSpFieldSeq = new string[] { "@esnsearch_xml" };

                dataTable = db.GetTableRecords(objCompHash, "Aero_Inventory_MSL_SELECT", arrSpFieldSeq);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return dataTable;
        }
        public static DataTable GetESN_Repository(List<avii.Classes.EsnSearch> esnList)
        {
            string esnXML = clsGeneral.SerializeObject(esnList);
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@esnsearch_xml", esnXML);

                arrSpFieldSeq = new string[] { "@esnsearch_xml" };

                dataTable = db.GetTableRecords(objCompHash, "Av_Inventory_MSL_SELECT", arrSpFieldSeq);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return dataTable;
        }



        public static int UploadMSL(string MslXML)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@Po_Xml", "@paramout" };
            try
            {
                if (MslXML != null)
                {
                    objCompHash.Add("@Po_Xml", MslXML);
                    objCompHash.Add("@paramout", "0");

                    db.ExecuteScalar(objCompHash, "Aero_Inventory_MSL_Insert", arrSpFieldSeq);
                    return 1;
                }
                else
                    return 0;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static bool ValidateEsnExists(string esn)
        {
            bool returnVal = true;
            try
            {
                if (!string.IsNullOrEmpty(esn))
                {
                    DBConnect db = new DBConnect();
                    string[] arrSpFieldSeq;
                    int returnValue;
                    Hashtable objCompHash = new Hashtable();
                    arrSpFieldSeq = new string[] { "@esn" };
                    try
                    {
                        objCompHash.Add("@esn", esn);
                        returnValue = (int)db.ExecuteScalar(objCompHash, "Aero_ValidateESN", arrSpFieldSeq);
                    }
                    catch (Exception objExp)
                    {
                        throw new Exception(objExp.Message.ToString());
                    }
                    finally
                    {
                        db.DBClose();
                        db = null;
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
            return returnVal;
        }

        public static void SetInventory(clsInventory inventory) //InventoryList
        {
            try
            {
                string inventoryXml = BaseAerovoice.SerializeObject<clsInventory>(inventory);
                if (!string.IsNullOrEmpty(inventoryXml))
                {
                    DBConnect db = new DBConnect();
                    string[] arrSpFieldSeq;
                    Hashtable objCompHash = new Hashtable();
                    arrSpFieldSeq = new string[] { "@itemid", "@invXML" };
                    try
                    {
                        inventoryXml = "<inventory>" + inventoryXml.Substring(inventoryXml.IndexOf("<phone>"));
                        //inventoryXml = inventoryXml.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?><inventory xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">", "<inventory>");

                        objCompHash.Add("@itemid", inventory.ItemID);
                        objCompHash.Add("@invXML", inventoryXml);
                        db.ExeCommand(objCompHash, "Aero_Inventory_Insert", arrSpFieldSeq);
                    }
                    catch (Exception objExp)
                    {
                        throw new Exception(objExp.Message.ToString());
                    }
                    finally
                    {
                        db.DBClose();
                        db = null;
                        objCompHash = null;
                        arrSpFieldSeq = null;
                    }
                }
                else
                {
                    throw new Exception("Could not upload Inventory");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable DeleteEsns(string Esns)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@esns" };
            DataTable dataTable = new DataTable();
            try
            {
                objCompHash.Add("@esns", Esns);
                dataTable = db.GetTableRecords(objCompHash, "av_MSL_ESN_Delete", arrSpFieldSeq);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return dataTable;
        }

        public static int DeleteEsnsWithXls(List<avii.Classes.clsEsnxml> esnList)
        {
            string esnXML = EsnManagementDB.SerializeObjetToXML(esnList, "ArrayOfClsEsnxml", "clsEsnxml");
            int returnValue = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@esnXML" };
            try
            {
                objCompHash.Add("@esnXML", esnXML);
                returnValue = db.ExecuteNonQuery(objCompHash, "av_MSL_ESN_Delete", arrSpFieldSeq);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return returnValue;
        }

        public static DataTable GetPhoneEsns(int PhoneID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@itemID" };
            DataTable dataTable = new DataTable();
            try
            {
                objCompHash.Add("@itemID", PhoneID);
                dataTable = db.GetTableRecords(objCompHash, "Aero_GetPhonesEsns", arrSpFieldSeq);
               
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return dataTable;
        }

        public static clsInventory GetInventoryInformation(int ItemID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@itemID" };
            DataTable dataTable = new DataTable();
            try
            {
                objCompHash.Add("@itemID", ItemID);
                dataTable = db.GetTableRecords(objCompHash, "Aero_GetInventoryInfo", arrSpFieldSeq);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return PopulateInventory(dataTable);
        }


        public static DataTable GetAssignedEsnList(DateTime StartDate, DateTime EndDate, string ItemCode,
                            int UserID, bool AddHistory, int CompanyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@StartDate", "@EndDate", "@ItemCode", "@UserID", "@CompanyID" };
            DataTable dataTable = new DataTable();
            try
            {
                if (StartDate.Year != 1)
                    objCompHash.Add("@StartDate", StartDate);
                if (EndDate.Year != 1)
                    objCompHash.Add("@EndDate", EndDate);
                objCompHash.Add("@ItemCode", ItemCode);
                objCompHash.Add("@UserID", UserID);
                //objCompHash.Add("@AddHistory", AddHistory);
                objCompHash.Add("@CompanyID", CompanyID);

                dataTable = db.GetTableRecords(objCompHash, "Av_EsnSummary_Select", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return dataTable;
        }
     

        public static List<InventorySummary> GetPhonesSummary(DateTime StartDate, DateTime EndDate, string ItemCode,
                                    int UserID, bool AddHistory, int CompanyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@StartDate", "@EndDate", "@ItemCode", "@UserID",  "@CompanyID" };
            DataTable dataTable = new DataTable();
            try
            {
                if (StartDate.Year != 1)
                    objCompHash.Add("@StartDate", StartDate);
                if (EndDate.Year != 1)
                    objCompHash.Add("@EndDate", EndDate);
                objCompHash.Add("@ItemCode", ItemCode);
                objCompHash.Add("@UserID", UserID);
                //objCompHash.Add("@AddHistory", AddHistory);
                objCompHash.Add("@CompanyID", CompanyID);

                dataTable = db.GetTableRecords(objCompHash, "Aero_GetPhonesSummary", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return PopulateInventorySummary(dataTable);
        }
     
        private static InventoryList PopulateAccesoryInventoryList(DataTable dataTable)
        {
            InventoryList inventoryList = new InventoryList();
            clsInventory inventory = null;          

            foreach (DataRow dataRow in dataTable.Rows)
            {
                inventory = new clsInventory();
                inventory.SKU = (string)clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false);
                inventory.ItemCode = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                inventory.ItemID = (Int32?)clsGeneral.getColumnData(dataRow, "ItemID", string.Empty, false);
                inventory.ItemDescription = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                inventory.ItemName = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                inventory.LastUpdated = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "LastUpdate", "01/01/0001", false));
                inventory.Price = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "whCost", 0, false));
                inventory.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock", 0, false));
                inventory.WarehouseCode = (string)clsGeneral.getColumnData(dataRow, "whCode", string.Empty, false);
                inventory.Phone = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Phone", string.Empty, false));
                inventory.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Active", string.Empty, false));
                inventoryList.CurrentInventory.Add(inventory);
            }

            return inventoryList;
        }

        private static InventoryList PopulateInventoryList(DataTable dataTable)
        {
            bool newItem = false;
            InventoryList inventoryList = new InventoryList();
            clsInventory inventory;// = new clsInventory();

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

        private static List<InventorySummary> PopulateInventorySummary(DataTable dataTable)
        {
            int pendingSale = 0;
            int processedSale = 0;
            int shippedSale = 0;
            int days30, days60, days90;
            days30 = days60 =  days90 = 0;
            List<InventorySummary> inventoryList = new List<InventorySummary>();
            InventorySummary inventorySummary;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                pendingSale = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                processedSale = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Processed", 0, false));
                shippedSale = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Shipped", 0, false));
                //days30 = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "days30", 0, false));
                //days60 = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "days60", 0, false));
                //days90 = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "days90", 0, false));
                inventorySummary = new InventorySummary(pendingSale, processedSale, shippedSale, days30, days60, days90);

                inventorySummary.Phone.ItemCode = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                inventorySummary.Phone.SKU = (string)clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false);
                inventorySummary.Phone.Technology = (string)clsGeneral.getColumnData(dataRow, "Technology", string.Empty, false);
                inventorySummary.Phone.DeviceType = (string)clsGeneral.getColumnData(dataRow, "DeviceType", string.Empty, false);
                inventorySummary.Phone.UPC = (string)clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false);
                inventorySummary.Phone.PhoneMaker = (string)clsGeneral.getColumnData(dataRow, "PhoneMaker", string.Empty, false);
                inventorySummary.Phone.ItemID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemID", 0, true));
                inventorySummary.Phone.ItemDescription = (string)clsGeneral.getColumnData(dataRow, "ItemDescription", string.Empty, false);
                inventorySummary.Phone.ItemName = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                inventorySummary.Phone.LastUpdated = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "LastUpdate", "01/01/0001", false));
                inventorySummary.Phone.Price = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "whCost", 0, false));
                inventorySummary.Phone.WarehouseCode = (string)clsGeneral.getColumnData(dataRow, "whCode", string.Empty, false);
                inventorySummary.Phone.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock", 0, false));
                inventorySummary.Phone.Phone = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Phone", false, false));
                inventorySummary.Phone.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Active", false, false));
                 
                inventoryList.Add(inventorySummary);
            
            }

            return inventoryList;
        }

        private static clsInventory PopulateInventory(DataTable dataTable)
        {
            clsInventory inventory = null;//= new clsInventory();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                inventory = new clsInventory();
                DataRow dataRow = dataTable.Rows[0];
                inventory.ItemCode = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                inventory.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, true));
                inventory.ItemID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemID", 0, true));
                inventory.ItemDescription = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                inventory.ItemName = (string)clsGeneral.getColumnData(dataRow, "PhoneModel", string.Empty, false);
                inventory.LastUpdated = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "LastUpdate", "01/01/0001", false));
                inventory.Price = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "whCost", 0, false));
                inventory.WarehouseCode = (string)clsGeneral.getColumnData(dataRow, "whCode", string.Empty, false);
                inventory.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock", 0, false));
                inventory.Phone = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Phone", 1, false));
                inventory.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Active", 1, false));
                inventory.UPC = (string)clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false);
                inventory.PhoneMaker = (string)clsGeneral.getColumnData(dataRow, "PhoneMaker", string.Empty, false);
                inventory.PhoneModel = (string)clsGeneral.getColumnData(dataRow, "PhoneModel", string.Empty, false);
                inventory.WarehouseCost = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "whCost", 0, false));
                inventory.Technology = (string)clsGeneral.getColumnData(dataRow, "Technology", string.Empty, false);
                inventory.Color = (string)clsGeneral.getColumnData(dataRow, "Color", string.Empty, false);
                inventory.SKU = (string)clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false);
                inventory.DeviceType = (string)clsGeneral.getColumnData(dataRow, "DeviceType", string.Empty, false);
                inventory.Closeout = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "closeout", false, false));
            }

            return inventory;
        }

        private static InventoryList PopulatePhoneInventoryList(DataTable dataTable)
        {
            bool newItem = false;
            InventoryList inventoryList = new InventoryList();
            clsInventory inventory = null;//= new clsInventory();
            InventoryItem inventoryItem;// = new InventoryItem();

            foreach (DataRow dataRow in dataTable.Rows)
            {

                if (inventory == null || inventory.ItemCode != (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false))
                {
                    inventory = new clsInventory();
                    newItem = true;
                }

                if (newItem)
                {
                    inventory.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, true));
                    inventory.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, true) as string;
                    inventory.SKU = (string)clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false);
                    inventory.ItemCode = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                    inventory.PhoneModel = (string)clsGeneral.getColumnData(dataRow, "PhoneModel", string.Empty, false);
                    inventory.UPC = (string)clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                    inventory.ItemID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemID", 0, true));
                    inventory.ItemDescription = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                    inventory.ItemName = (string)clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false);
                    inventory.LastUpdated = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "LastUpdate", "01/01/0001", false));
                    inventory.Price = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "whCost", 0, false));
                    inventory.WarehouseCode = (string)clsGeneral.getColumnData(dataRow, "whCode", string.Empty, false);
                    inventory.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock", 0, false));
                    inventory.Phone = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Phone", 1, false));
                    inventory.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Active", 1, false));
                    inventoryList.CurrentInventory.Add(inventory);
                    newItem = false;
                }
                if (dataTable.Columns.Contains("ESN"))
                {
                    if ((string)clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) != string.Empty)
                    {
                        inventoryItem = new InventoryItem();
                        inventoryItem.BoxId = (string)clsGeneral.getColumnData(dataRow, "BoxID", string.Empty, false);
                        inventoryItem.Esn = (string)clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false);
                        inventory.InventoryItem.Add(inventoryItem);
                    }
                }
            }

            return inventoryList;
        }

        public static InventoryList GetPhoneInventory(int userID)
        {
            InventoryList inventory = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@userID" };
            try
            {
                objCompHash.Add("@userID", userID);
                DataTable dataTable = db.GetTableRecords(objCompHash, "Aero_GetPhonesInventory", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    inventory = PopulatePhoneInventoryList(dataTable);
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return inventory;
        }

        public static InventoryList GetInventory(int userID)
        {
            // UserID = 0 : for all inventory item
            InventoryList inventory = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@userID" };
            try
            {
                objCompHash.Add("@userID", userID);
                DataTable dataTable = db.GetTableRecords(objCompHash, "Aero_GetInventory", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    inventory = PopulatePhoneInventoryList(dataTable);
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return inventory;
        }

        public static InventoryList GetInventory(int userID, int companyid)
        {
            // UserID = 0 : for all inventory item
            InventoryList inventory = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@userID", "@CompanyID" };
            try
            {
                objCompHash.Add("@userID", userID);
                objCompHash.Add("@CompanyID", companyid);
                DataTable dataTable = db.GetTableRecords(objCompHash, "Aero_GetInventory", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    inventory = PopulatePhoneInventoryList(dataTable);
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return inventory;
        }

        public static InventoryList GetInventory()
        {
            InventoryList inventory = null;
            DBConnect db = new DBConnect();
            try
            {
                DataTable dataTable = db.GetTableRecords("Aero_GetInventory","Inventory");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    inventory = PopulatePhoneInventoryList(dataTable);
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return inventory;
        }

        public static InventoryList  GetAccessoryInventory(int userID)
        {
            InventoryList inventory = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@userID" };
            try
            {
                objCompHash.Add("@userID", userID);
                DataTable dataTable = db.GetTableRecords(objCompHash, "Aero_GetAccessoryInventory", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    inventory = PopulatePhoneInventoryList(dataTable);
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return inventory;
        }

        public static InventoryList GetInventoryItems(int userID, int sim, int CompanyID)
        {
            InventoryList inventory = null;
            DBConnect db = new DBConnect();
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return inventory;
        }

        public static InventoryLastUpdate GetInventoryLastUpdate()
        {
            InventoryLastUpdate inventoryLastUpdate = new InventoryLastUpdate(); 
            string returnDate = string.Empty;
            DBConnect db = new DBConnect();
            try
            {
                DataTable dataTable = db.GetTableRecords("Aero_GetLatestInventory", "InvTable");
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    inventoryLastUpdate = PopulateInventoryDates(dataTable, inventoryLastUpdate);
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return inventoryLastUpdate;
        }

        public static int UpdateInventoryItem(clsInventory inventoryItem)
        {
            int result = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@ItemID", "@ItemCode", "@ItemDescription", "@CompanyID", "@whCode", "@whCost", "@Stock", 
                "@Phone", "@Active", "@PhoneModel", "@PhoneMaker", "@UPC", "@Color", "@Technology", "@DeviceType", "@SKU", "@Closeout" };
            try
            {
                objCompHash.Add("@ItemID", inventoryItem.ItemID);
                objCompHash.Add("@ItemCode", inventoryItem.ItemCode);
                objCompHash.Add("@ItemDescription", inventoryItem.ItemDescription);
                objCompHash.Add("@CompanyID", inventoryItem.CompanyID);
                objCompHash.Add("@whCode", inventoryItem.WarehouseCode);
                objCompHash.Add("@whCost", inventoryItem.WarehouseCost);
                objCompHash.Add("@Stock", inventoryItem.CurrentStock);
                objCompHash.Add("@Phone", inventoryItem.Phone);
                objCompHash.Add("@Active", inventoryItem.Active);
                objCompHash.Add("@PhoneModel", inventoryItem.PhoneModel);
                objCompHash.Add("@PhoneMaker", inventoryItem.PhoneMaker);
                objCompHash.Add("@UPC", inventoryItem.UPC);
                objCompHash.Add("@Color", inventoryItem.Color);
                objCompHash.Add("@Technology", inventoryItem.Technology);
                objCompHash.Add("@DeviceType", inventoryItem.DeviceType);
                objCompHash.Add("@sku", inventoryItem.SKU);
                objCompHash.Add("@Closeout", inventoryItem.Closeout);

                result = db.ExecuteNonQuery(objCompHash, "Aero_InventoryItem_Update", arrSpFieldSeq);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return result;
        }


        public static int SetInventoryItem(clsInventory inventoryItem)
        {
            int result = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@ItemCode", "@ItemDescription", "@CompanyID", "@whCode", "@whCost", "@Stock", "@Phone", 
                "@Active", "@PhoneModel", "@PhoneMaker", "@UPC", "@Color", "@Technology", "@DeviceType", "@SKU", "@Closeout" };
            try
            {
                objCompHash.Add("@ItemCode", inventoryItem.ItemCode);
                objCompHash.Add("@ItemDescription", inventoryItem.ItemDescription);
                objCompHash.Add("@CompanyID", inventoryItem.CompanyID);
                objCompHash.Add("@whCode", inventoryItem.WarehouseCode);
                objCompHash.Add("@whCost", inventoryItem.WarehouseCost);
                objCompHash.Add("@Stock", inventoryItem.CurrentStock);
                objCompHash.Add("@Phone", inventoryItem.Phone);
                objCompHash.Add("@Active", inventoryItem.Active);
                objCompHash.Add("@PhoneModel", inventoryItem.PhoneModel);
                objCompHash.Add("@PhoneMaker", inventoryItem.PhoneMaker);
                objCompHash.Add("@UPC", inventoryItem.UPC);
                objCompHash.Add("@Color", inventoryItem.Color);
                objCompHash.Add("@Technology", inventoryItem.Technology);
                objCompHash.Add("@DeviceType", inventoryItem.DeviceType);
                objCompHash.Add("@SKU", inventoryItem.SKU);
                objCompHash.Add("@Closeout", inventoryItem.Closeout);

                result = db.ExecuteNonQuery(objCompHash, "Aero_InventoryItem_Insert", arrSpFieldSeq);
                
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return result;
        }


        private static InventoryLastUpdate PopulateInventoryDates(DataTable dataTable, InventoryLastUpdate inventoryLastUpdate)
        {
            DateTime phoneUpdate, accUpdate;            
            DataRow dataRow = dataTable.Rows[0];
            DateTime.TryParse(dataRow["PhoneDate"].ToString(), out phoneUpdate);
            DateTime.TryParse(dataRow["AccessoryDate"].ToString(), out accUpdate);

            inventoryLastUpdate.PhoneUpdate = phoneUpdate;
            inventoryLastUpdate.AccessoryUpdate = accUpdate;
            return inventoryLastUpdate;
        }

        public static DataTable GetEsnLogList(string Esn)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@ESN" };
            DataTable dataTable = null;
            try
            {
                objCompHash.Add("@ESN", Esn);

                dataTable = db.GetTableRecords(objCompHash, "av_ESNLog_Select", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return dataTable;
        }

        public static List<ESNLog> GetEsnLogs(string Esn)
        {
            List<ESNLog> esnLogs = new List<ESNLog>();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            arrSpFieldSeq = new string[] { "@ESN" };
            DataTable dataTable = null;
            try
            {
                objCompHash.Add("@ESN", Esn);

                dataTable = db.GetTableRecords(objCompHash, "av_ESNLog_Select", arrSpFieldSeq);
                esnLogs = PopulateESNLog(dataTable);
            }
            catch (Exception exp)
            {
                throw exp;
            }

            return esnLogs;
        }

        private static List<ESNLog> PopulateESNLog(DataTable dataTable)
        {
            List<ESNLog> esnLogs = new List<ESNLog>();
            ESNLog eSNLog = null;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                eSNLog = new ESNLog();
                
                eSNLog.ESN = (string)clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false);
                eSNLog.ModuleName = (string)clsGeneral.getColumnData(dataRow, "Module", string.Empty, false);
                eSNLog.Status = (string)clsGeneral.getColumnData(dataRow, "Status", string.Empty, false);
                eSNLog.UpdateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UpdateDate", "01/01/0001", false));
                esnLogs.Add(eSNLog);
               

            }
            return esnLogs;
        }


    }
}
