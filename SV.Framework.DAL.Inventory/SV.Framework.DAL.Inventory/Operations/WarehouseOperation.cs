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
