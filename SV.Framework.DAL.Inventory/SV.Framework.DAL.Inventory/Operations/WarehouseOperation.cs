using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Inventory
{
    public class WarehouseOperation : BaseCreateInstance
    {
        public  List<WarehouseInfo> GetWarehouse(int warehouseID)
        {
            List<WarehouseInfo> warehouseList = default;// new List<Carriers>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@WarehouseID", warehouseID);
                    
                    arrSpFieldSeq = new string[] { "@WarehouseID" };

                    dataTable = db.GetTableRecords(objCompHash, "av_Warehouse_Select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        warehouseList = PopulateWarehouse(dataTable);
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return warehouseList;
        }

        public List<WarehouseStorage> GetWarehouseStorage(string warehouseCity, string warehouseLocation, int companyID)
        {
            List<WarehouseStorage> warehouseList = default;// new List<Carriers>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@WarehouseCity", warehouseCity);
                    objCompHash.Add("@WarehouseLocation", warehouseLocation);
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@WarehouseStorageID", 0);

                    arrSpFieldSeq = new string[] { "@WarehouseCity", "@WarehouseLocation", "@CompanyID", "@WarehouseStorageID" };

                    dataTable = db.GetTableRecords(objCompHash, "av_WarehouseStorage_Select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        warehouseList = PopulateWarehouseStorage(dataTable);
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return warehouseList;
        }

        public WarehouseStorage GetWarehouseLocationInfo(string warehouseLocation)
        {
            WarehouseStorage warehouseLocationInfo = default;// new List<Carriers>();
            using (DBConnect db = new DBConnect())
            {
                DataSet ds = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@WarehouseLocation", warehouseLocation);                    
                    arrSpFieldSeq = new string[] { "@WarehouseLocation" };
                    ds = db.GetDataSet(objCompHash, "av_WarehouseLocationAllowcation_Select", arrSpFieldSeq);
                    //ds = db.GetDataSet(objCompHash, "av_ESNByWarehouseLocation_Select", arrSpFieldSeq);

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        warehouseLocationInfo = PopulateWarehouseLocation(ds);
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return warehouseLocationInfo;
        }

        public WarehouseStorage GetWarehouseStorageInfo(int warehouseStorageID)
        {
            WarehouseStorage warehouseStorage = default;// new List<Carriers>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@WarehouseCity", "");
                    objCompHash.Add("@WarehouseLocation", "");
                    objCompHash.Add("@CompanyID", 0);
                    objCompHash.Add("@WarehouseStorageID", warehouseStorageID);

                    arrSpFieldSeq = new string[] { "@WarehouseCity", "@WarehouseLocation", "@CompanyID", "@WarehouseStorageID" };

                    dataTable = db.GetTableRecords(objCompHash, "av_WarehouseStorage_Select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        warehouseStorage = PopulateWarehouseStorageInfo(dataTable);
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return warehouseStorage;
        }

        public string CreateWarehouseStorage(WarehouseStorage request)
        {
            string errorMessage = default;
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@WarehouseStorageID", request.WarehouseStorageID);
                    objCompHash.Add("@WarehouseID", request.WarehouseID);
                    objCompHash.Add("@WarehouseLocation", request.WarehouseLocation);
                    objCompHash.Add("@Aisle", request.Aisle);
                    objCompHash.Add("@Bay", request.Bay);
                    objCompHash.Add("@RowLevel", request.RowLevel);
                    objCompHash.Add("@CompanyID", request.CompanyID);
                    objCompHash.Add("@UserID", request.UserID);
                    objCompHash.Add("@Specialinstructions", request.Specialinstructions);
                    
                    arrSpFieldSeq = new string[] { "@WarehouseStorageID", "@WarehouseID", "@WarehouseLocation", "@Aisle","@Bay", 
                        "@RowLevel","@CompanyID", "@UserID","@Specialinstructions" };
                    db.ExeCommand(objCompHash, "av_WarehouseStorage_InsertUpdate", arrSpFieldSeq, "@ErrorMessage", out errorMessage);
                }
                catch (Exception exp)
                {
                    errorMessage = exp.Message;
                    Logger.LogMessage(exp, this);   //throw exp;
                }
            }
            return errorMessage;
        }
        public  string DeleteWarehouseStorage(int warehouseStorageID, int userID)
        {
            string returnMessage = "Not deleted!";

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@WarehouseStorageID", warehouseStorageID);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@WarehouseStorageID", "@UserID" };
                    int returnValue = db.ExecuteNonQuery(objCompHash, "av_WarehouseStorage_Delete", arrSpFieldSeq);
                    if (returnValue > 0)
                        returnMessage = "Deleted successfully";
                    else
                        returnMessage = "Warehouse location in use you cannot delete!";
                }
                catch (Exception exp)
                {
                    returnMessage = exp.Message;
                    Logger.LogMessage(exp, this);   //throw exp;
                }
            }
            return returnMessage;
        }

        public List<WhLocationInfo> GetWarehouseLocationReport(string warehouseCity, string warehouseLocation, int companyID, string SKU, string ReceiveFromDate, string ReceiveToDate)
        {
            List<WhLocationInfo> warehouseList = default;
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@WarehouseCity", warehouseCity);
                    objCompHash.Add("@WarehouseLocation", warehouseLocation);
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@SKU", SKU);
                    objCompHash.Add("@FromDate", string.IsNullOrEmpty(ReceiveFromDate) ? null : ReceiveFromDate);
                    objCompHash.Add("@ToDate", string.IsNullOrEmpty(ReceiveToDate) ? null : ReceiveToDate);
                    

                    arrSpFieldSeq = new string[] { "@WarehouseCity", "@WarehouseLocation", "@CompanyID", "@SKU", "@FromDate", "@ToDate" };

                    dataTable = db.GetTableRecords(objCompHash, "av_WarehouseLocation_Report", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        warehouseList = PopulateWhLocationReport(dataTable);
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return warehouseList;
        }
        
        public List<EsnInfo> GetWarehouseLocationDetail(int ItemCompanyGUID, string warehouseLocation, string ReceiveFromDate, string ReceiveToDate, out List<NonEsnStorage> accessoryLoactionList)
        {
            List<EsnInfo> esnWarehouseList = default;
            accessoryLoactionList = default;
            using (DBConnect db = new DBConnect())
            {
                DataSet ds = default;
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@WarehouseLocation", warehouseLocation);
                    objCompHash.Add("@ItemCompanyGUID", ItemCompanyGUID);
                    objCompHash.Add("@FromDate", string.IsNullOrEmpty(ReceiveFromDate) ? null : ReceiveFromDate);
                    objCompHash.Add("@ToDate", string.IsNullOrEmpty(ReceiveToDate) ? null : ReceiveToDate);

                    arrSpFieldSeq = new string[] { "@WarehouseLocation", "@ItemCompanyGUID", "@FromDate", "@ToDate" };

                    ds = db.GetDataSet(objCompHash, "av_WhLocation_EsnOrNonEsn_Select", arrSpFieldSeq);

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        esnWarehouseList = PopulateWhLocationDetail(ds, out accessoryLoactionList); 
                    }
                    //{
                    //    warehouseList = PopulateWhLocationReport(dataTable);
                    //}

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return esnWarehouseList;
        }

        public List<WhLocationInfo> GetWarehouseLocationHistory(string warehouseLocation, int companyID)
        {
            List<WhLocationInfo> warehouseList = default;
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@WarehouseLocation", warehouseLocation);
                    objCompHash.Add("@CompanyID", companyID);
                    

                    arrSpFieldSeq = new string[] { "@WarehouseLocation", "@CompanyID" };

                    dataTable = db.GetTableRecords(objCompHash, "av_WharehouseLocation_History", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        warehouseList = PopulateWhLocationHistory(dataTable);
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return warehouseList;
        }

        public List<WhLocationInfo> GetWarehouseLocationReplacement(string warehouseLocation, int companyID, string SKU)
        {
            List<WhLocationInfo> warehouseList = default;
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@WarehouseLocation", warehouseLocation);
                    objCompHash.Add("@SKU", SKU);                    

                    arrSpFieldSeq = new string[] { "@CompanyID", "@WarehouseLocation", "@SKU" };

                    dataTable = db.GetTableRecords(objCompHash, "av_WarehouseLocation_Replacement", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        warehouseList = PopulateWhLocationReport(dataTable);
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return warehouseList;
        }

        public List<EsnInfo> WarehouseLocationEsnValidate(List<EsnInfo> ESNs, int companyID, string whLocation)
        {
            List<EsnInfo> esnList = default;
            using (DBConnect db = new DBConnect())
            {
                
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();
                DataTable esnTable = ESNData(ESNs);
                //int rowID = 1;
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@WarehouseLocation", whLocation);
                    objCompHash.Add("@ESNTable", esnTable);


                    arrSpFieldSeq = new string[] { "@CompanyID", "@WarehouseLocation", "@ESNTable" };
                    dt = db.GetTableRecords(objCompHash, "av_WhLocationESN_Validate", arrSpFieldSeq);
                    esnList = populateESNList(dt);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return esnList;
        }

        public int WarehouseLocationESNUpdate(List<EsnInfo> ESNs,int userId, int ItemCompanyGUID, string whLocationOld, string whLocationNew, int qty, int loginUserID, string Comment, out string errorMessage)
        {
            errorMessage = "";
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                DataTable esnTable = ESNData(ESNs);
                
                try
                {
                    objCompHash.Add("@UserID", userId);
                    objCompHash.Add("@ItemCompanyGUID", ItemCompanyGUID);
                    objCompHash.Add("@WarehouseLocationOld", whLocationOld);
                    objCompHash.Add("@WarehouseLocationNew", whLocationNew);
                    objCompHash.Add("@Qty", qty);
                    objCompHash.Add("@loginUserID", loginUserID);
                    objCompHash.Add("@Comment", Comment);
                    objCompHash.Add("@ESNTable", esnTable);


                    arrSpFieldSeq = new string[] { "@UserID", "@ItemCompanyGUID", "@WarehouseLocationOld", "@WarehouseLocationNew", "@Qty", "@loginUserID", "@Comment", "@ESNTable" };
                    returnValue = db.ExecuteNonQuery(objCompHash, "av_WhLocationESN_Update", arrSpFieldSeq);

                    
                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;
                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnValue;
        }
        public int WarehouseLocationNonEsnUpdate(int storageID, int userId, int ItemCompanyGUID, string whLocationOld, string whLocationNew, int qty, int oldQty, int loginUserID, string Comment, out string errorMessage)
        {
            errorMessage = "";
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {

                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@StorageID", storageID);
                    objCompHash.Add("@UserID", userId);
                    objCompHash.Add("@ItemCompanyGUID", ItemCompanyGUID);
                    objCompHash.Add("@WarehouseLocationOld", whLocationOld);
                    objCompHash.Add("@WarehouseLocationNew", whLocationNew);
                    objCompHash.Add("@Qty", qty);
                    objCompHash.Add("@OldQty", oldQty);
                    objCompHash.Add("@loginUserID", loginUserID);
                    objCompHash.Add("@Comment", Comment);


                    arrSpFieldSeq = new string[] { "@StorageID", "@UserID", "@ItemCompanyGUID", "@WarehouseLocationOld", "@WarehouseLocationNew", "@Qty", 
                        "@OldQty", "@loginUserID","Comment" };
                    returnValue = db.ExecuteNonQuery(objCompHash, "av_WhLocationNonESN_Update", arrSpFieldSeq);

                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;

                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnValue;
        }

        private List<EsnInfo> populateESNList(DataTable dt)
        {
            List<EsnInfo> esnList = default;
            EsnInfo esnInfo = default;
            if (dt != null && dt.Rows.Count > 0)
            {
                esnList = new List<EsnInfo>();
                foreach (DataRow row in dt.Rows)
                {
                    esnInfo = new EsnInfo();
                    esnInfo.Location = Convert.ToString(clsGeneral.getColumnData(row, "Location", string.Empty, false));
                    esnInfo.ESN = Convert.ToString(clsGeneral.getColumnData(row, "ESN", string.Empty, false));
                    esnInfo.ErrorMessage = Convert.ToString(clsGeneral.getColumnData(row, "ErrorMessage", string.Empty, false));
                    esnList.Add(esnInfo);
                }

            }
            return esnList;
        }
        private List<EsnInfo> PopulateWhLocationDetail(DataSet ds, out List<NonEsnStorage> accessoryLocationList)
        {
            List<EsnInfo> esnWarehouseList = default;// new List<Carriers>();
            accessoryLocationList = default;
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        esnWarehouseList = new List<EsnInfo>();
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            EsnInfo warehouseInfo = new EsnInfo();
                            warehouseInfo.Location = clsGeneral.getColumnData(dataRow, "Location", string.Empty, false) as string;

                            warehouseInfo.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                            warehouseInfo.MEID = clsGeneral.getColumnData(dataRow, "MeidDec", string.Empty, false) as string;
                            warehouseInfo.HEX = clsGeneral.getColumnData(dataRow, "MeidHex", string.Empty, false) as string;
                            warehouseInfo.BoxID = clsGeneral.getColumnData(dataRow, "BoxID", string.Empty, false) as string;
                            warehouseInfo.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                            warehouseInfo.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                            warehouseInfo.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                            warehouseInfo.Aisle = clsGeneral.getColumnData(dataRow, "Aisle", string.Empty, false) as string;
                            warehouseInfo.Bay = clsGeneral.getColumnData(dataRow, "Bay", string.Empty, false) as string;
                            warehouseInfo.RowLevel = clsGeneral.getColumnData(dataRow, "RowLevel", string.Empty, false) as string;

                            warehouseInfo.BatchNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
                            //warehouseInfo.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                            warehouseInfo.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UploadDate", DateTime.Now, false));

                            esnWarehouseList.Add(warehouseInfo);
                        }
                    }
                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        accessoryLocationList = new List<NonEsnStorage>();
                        foreach (DataRow dataRow in ds.Tables[1].Rows)
                        {
                            NonEsnStorage nonEsnStorage = new NonEsnStorage();
                            nonEsnStorage.WareHouseLocation = clsGeneral.getColumnData(dataRow, "WareHouseLocation", string.Empty, false) as string;
                            nonEsnStorage.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                            nonEsnStorage.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                            nonEsnStorage.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                            nonEsnStorage.Aisle = clsGeneral.getColumnData(dataRow, "Aisle", string.Empty, false) as string;
                            nonEsnStorage.Bay = clsGeneral.getColumnData(dataRow, "Bay", string.Empty, false) as string;
                            nonEsnStorage.RowLevel = clsGeneral.getColumnData(dataRow, "RowLevel", string.Empty, false) as string;

                            nonEsnStorage.BoxID = clsGeneral.getColumnData(dataRow, "BoxID", string.Empty, false) as string;
                            nonEsnStorage.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                            nonEsnStorage.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UploadDate", DateTime.Now, false));

                            accessoryLocationList.Add(nonEsnStorage);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex, this);   //throw ex;
            }
            return esnWarehouseList;
        }

        private List<WhLocationInfo> PopulateWhLocationHistory(DataTable dataTable)
        {
            List<WhLocationInfo> warehouseList = default;// new List<Carriers>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    warehouseList = new List<WhLocationInfo>();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        WhLocationInfo warehouseInfo = new WhLocationInfo();
                        warehouseInfo.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                        warehouseInfo.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        //warehouseInfo.WarehouseStorageID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WarehouseStorageID", 0, false));
                        warehouseInfo.WarehouseLocation = clsGeneral.getColumnData(dataRow, "WhLocation", string.Empty, false) as string;
                     //   warehouseInfo.WarehouseCity = clsGeneral.getColumnData(dataRow, "WarehouseCity", string.Empty, false) as string;
                        warehouseInfo.Aisle = clsGeneral.getColumnData(dataRow, "Aisle", string.Empty, false) as string;
                        warehouseInfo.Bay = clsGeneral.getColumnData(dataRow, "Bay", string.Empty, false) as string;
                        warehouseInfo.RowLevel = clsGeneral.getColumnData(dataRow, "RowLevel", string.Empty, false) as string;
                        warehouseInfo.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                        warehouseInfo.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        warehouseInfo.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                        warehouseInfo.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                        warehouseInfo.LogSource = clsGeneral.getColumnData(dataRow, "LogSource", string.Empty, false) as string;
                        warehouseInfo.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                        warehouseInfo.LastReceivedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", DateTime.Now, false));


                        warehouseList.Add(warehouseInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex, this);   //throw ex;
            }
            return warehouseList;
        }
        private DataTable ESNData(List<EsnInfo> EsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ROWID", typeof(System.Int32));
            dt.Columns.Add("ESN", typeof(System.String));
            int rowID = 1;
            DataRow row;

            if (EsnList != null && EsnList.Count > 0)
            {
                foreach (EsnInfo item in EsnList)
                {
                    row = dt.NewRow();
                    row["ROWID"] = rowID;
                    row["ESN"] = item.ESN;
                    dt.Rows.Add(row);
                    rowID = rowID + 1;
                }
            }
            return dt;
        }

        private List<WhLocationInfo> PopulateWhLocationReport(DataTable dataTable)
        {
            List<WhLocationInfo> warehouseList = default;// new List<Carriers>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    warehouseList = new List<WhLocationInfo>();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        WhLocationInfo warehouseInfo = new WhLocationInfo();
                        warehouseInfo.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                        warehouseInfo.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        warehouseInfo.WarehouseStorageID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WarehouseStorageID", 0, false));
                        warehouseInfo.StorageID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StorageID", 0, false));
                        warehouseInfo.WarehouseLocation = clsGeneral.getColumnData(dataRow, "WarehouseLocation", string.Empty, false) as string;
                        warehouseInfo.InventoryType = clsGeneral.getColumnData(dataRow, "InventoryType", string.Empty, false) as string;
                        warehouseInfo.WarehouseCity = clsGeneral.getColumnData(dataRow, "WarehouseCity", string.Empty, false) as string;
                        warehouseInfo.Aisle = clsGeneral.getColumnData(dataRow, "Aisle", string.Empty, false) as string;
                        warehouseInfo.Bay = clsGeneral.getColumnData(dataRow, "Bay", string.Empty, false) as string;
                        warehouseInfo.RowLevel = clsGeneral.getColumnData(dataRow, "RowLevel", string.Empty, false) as string;
                        warehouseInfo.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                        warehouseInfo.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        warehouseInfo.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                        warehouseInfo.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                        warehouseInfo.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                        warehouseInfo.LastReceivedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "LastReceivedDate", DateTime.Now, false));

                        warehouseList.Add(warehouseInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex, this);   //throw ex;
            }
            return warehouseList;
        }


        private List<WarehouseInfo> PopulateWarehouse(DataTable dataTable)
        {
            List<WarehouseInfo> warehouseList = default;// new List<Carriers>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    warehouseList = new List<WarehouseInfo>();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        WarehouseInfo warehouseInfo = new WarehouseInfo();
                        warehouseInfo.WarehouseID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WarehouseID", 0, false));
                        warehouseInfo.WarehouseCode = clsGeneral.getColumnData(dataRow, "WarehouseCode", string.Empty, false) as string;
                        warehouseInfo.WarehouseCity = clsGeneral.getColumnData(dataRow, "WarehouseCity", string.Empty, false) as string;


                        warehouseList.Add(warehouseInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex, this);   //throw ex;
            }
            return warehouseList;
        }
        private List<WarehouseStorage> PopulateWarehouseStorage(DataTable dataTable)
        {
            List<WarehouseStorage> warehouseList = default;// new List<Carriers>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    warehouseList = new List<WarehouseStorage>();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        WarehouseStorage warehouseInfo = new WarehouseStorage();
                        warehouseInfo.WarehouseID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WarehouseID", 0, false));
                        warehouseInfo.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        warehouseInfo.WarehouseStorageID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WarehouseStorageID", 0, false));
                        warehouseInfo.WarehouseLocation = clsGeneral.getColumnData(dataRow, "WarehouseLocation", string.Empty, false) as string;
                        warehouseInfo.WarehouseCity = clsGeneral.getColumnData(dataRow, "WarehouseCity", string.Empty, false) as string;
                        warehouseInfo.Aisle = clsGeneral.getColumnData(dataRow, "Aisle", string.Empty, false) as string;
                        warehouseInfo.Bay = clsGeneral.getColumnData(dataRow, "Bay", string.Empty, false) as string;
                        warehouseInfo.RowLevel = clsGeneral.getColumnData(dataRow, "RowLevel", string.Empty, false) as string;
                        warehouseInfo.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        warehouseInfo.CreateDate = clsGeneral.getColumnData(dataRow, "CreateDate", string.Empty, false) as string;
                        warehouseInfo.CreatedDateTime = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreatedDateTime", DateTime.Now, false));
                        warehouseInfo.CreatedBy = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                       // warehouseInfo.Specialinstructions = clsGeneral.getColumnData(dataRow, "Specialinstructions", string.Empty, false) as string;


                        warehouseList.Add(warehouseInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex, this);   //throw ex;
            }
            return warehouseList;
        }
        
        private WarehouseStorage PopulateWarehouseLocation(DataSet ds)
        {
            WarehouseStorage warehouseInfo = default;
            List<LocationEsnInfo> ESNs = default;
            LocationEsnInfo esnInfo = default; 
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        ESNs = new List<LocationEsnInfo>();
                        warehouseInfo = new WarehouseStorage();
                        //warehouseInfo.WarehouseID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WarehouseID", 0, false));
                        //warehouseInfo.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        //warehouseInfo.WarehouseStorageID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WarehouseStorageID", 0, false));
                        warehouseInfo.WarehouseLocation = clsGeneral.getColumnData(dataRow, "WarehouseLocation", string.Empty, false) as string;
                        warehouseInfo.WarehouseCity = clsGeneral.getColumnData(dataRow, "WarehouseCity", string.Empty, false) as string;
                        warehouseInfo.Aisle = clsGeneral.getColumnData(dataRow, "Aisle", string.Empty, false) as string;
                        warehouseInfo.Bay = clsGeneral.getColumnData(dataRow, "Bay", string.Empty, false) as string;
                        warehouseInfo.RowLevel = clsGeneral.getColumnData(dataRow, "RowLevel", string.Empty, false) as string;
                       // warehouseInfo.Specialinstructions = clsGeneral.getColumnData(dataRow, "Specialinstructions", string.Empty, false) as string;
                        warehouseInfo.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dataRow1 in ds.Tables[1].Rows)
                            {
                                esnInfo = new LocationEsnInfo();
                                esnInfo.EsnCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow1, "EsnCount", 0, false));
                                esnInfo.SKU = clsGeneral.getColumnData(dataRow1, "SKU", string.Empty, false) as string;
                                esnInfo.ReceivedBy = clsGeneral.getColumnData(dataRow1, "UserName", string.Empty, false) as string;
                                esnInfo.CustomerName = clsGeneral.getColumnData(dataRow1, "CompanyName", string.Empty, false) as string;
                                esnInfo.ReceivedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow1, "UploadDate", DateTime.Now, false));

                                //esnInfo.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow1, "PO_ID", 0, false));
                                //esnInfo.ESN = clsGeneral.getColumnData(dataRow1, "ESN", string.Empty, false) as string;
                                //esnInfo.DEC = clsGeneral.getColumnData(dataRow1, "MeidDec", string.Empty, false) as string;
                                //esnInfo.HEX = clsGeneral.getColumnData(dataRow1, "MeidHex", string.Empty, false) as string;
                                //esnInfo.BOXID = clsGeneral.getColumnData(dataRow1, "BOXID", string.Empty, false) as string;
                                //esnInfo.SerialNumber = clsGeneral.getColumnData(dataRow1, "SerialNumber", string.Empty, false) as string;
                                //esnInfo.FulfillmentNumber = clsGeneral.getColumnData(dataRow1, "PO_Num", string.Empty, false) as string;
                                //esnInfo.RmaNumber = clsGeneral.getColumnData(dataRow1, "RmaNumber", string.Empty, false) as string;
                                
                                ESNs.Add(esnInfo);
                            }
                        }
                        warehouseInfo.ESNs = ESNs;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex, this);   //throw ex;
            }
            return warehouseInfo;
        }

        private WarehouseStorage PopulateWarehouseStorageInfo(DataTable dataTable)
        {
            WarehouseStorage warehouseInfo = default;

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        warehouseInfo = new WarehouseStorage();
                        warehouseInfo.WarehouseID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WarehouseID", 0, false));
                        warehouseInfo.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        warehouseInfo.WarehouseStorageID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WarehouseStorageID", 0, false));
                        warehouseInfo.WarehouseLocation = clsGeneral.getColumnData(dataRow, "WarehouseLocation", string.Empty, false) as string;
                        warehouseInfo.WarehouseCity = clsGeneral.getColumnData(dataRow, "WarehouseCity", string.Empty, false) as string;
                        warehouseInfo.Aisle = clsGeneral.getColumnData(dataRow, "Aisle", string.Empty, false) as string;
                        warehouseInfo.Bay = clsGeneral.getColumnData(dataRow, "Bay", string.Empty, false) as string;
                        warehouseInfo.RowLevel = clsGeneral.getColumnData(dataRow, "RowLevel", string.Empty, false) as string;
                        warehouseInfo.Specialinstructions = clsGeneral.getColumnData(dataRow, "Specialinstructions", string.Empty, false) as string;
                                                
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex, this);   //throw ex;
            }
            return warehouseInfo;
        }

    }
}
