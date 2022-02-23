using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Inventory
{
    public class InventoryReportOperation:BaseCreateInstance
    {

        public List<EsnInfo> GetESNQueryWithRepository(List<SV.Framework.Models.Fulfillment.EsnList> esnList)
        {
            List<EsnInfo> esnInfoList = default;
            using (DBConnect db = new DBConnect())
            {
                string esnXML = clsGeneral.SerializeObject(esnList);
                DataTable dataTable = default;// new DataTable();
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
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return esnInfoList;
        }
        public List<EsnInfo> GetESNQuery(List<SV.Framework.Models.Fulfillment.EsnList> esnList)
        {
            List<EsnInfo> esnInfoList = default;// new List<EsnInfo>();
            using (DBConnect db = new DBConnect())
            {
                string esnXML = clsGeneral.SerializeObject(esnList);
                DataTable dataTable = default;// new DataTable();
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
                    Logger.LogMessage(ex, this); //   throw ex;
                }
                finally
                {
                   // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return esnInfoList;
        }
        public int DeleteEsnsWithXls(List<clsEsnxml> esnList)
        {
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string esnXML = clsGeneral.SerializeObjetToXML(esnList, "ArrayOfClsEsnxml", "clsEsnxml");
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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                    // db = null;
                }
            }
            return returnValue;
        }
        public  List<EsnRepositoryDetail> GetCustomerEsnRepositoryDetail(int companyID, int itemCompanyGUID, DateTime fromDate, DateTime toDate, bool unUsedESN, bool showAllUnusedESN)
        {
            List<EsnRepositoryDetail> esnList = default;// new List<EsnRepositoryDetail>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@ItemCompanyGUID", itemCompanyGUID);
                    objCompHash.Add("@FromDate", fromDate.ToShortDateString() == "1/1/0001" ? DateTime.Now.AddDays(-365).ToShortDateString() : fromDate.ToShortDateString());
                    objCompHash.Add("@ToDate", toDate.ToShortDateString() == "1/1/0001" ? DateTime.Now.ToShortDateString() : toDate.ToShortDateString());
                    objCompHash.Add("@UnUsedESN", unUsedESN);
                    objCompHash.Add("@ShowAllUnusedESN", showAllUnusedESN);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@ItemCompanyGUID", "@FromDate", "@ToDate", "@UnUsedESN", "@ShowAllUnusedESN" };

                    dataTable = db.GetTableRecords(objCompHash, "AV_ESN_Repository_Detail", arrSpFieldSeq);


                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        esnList = new List<EsnRepositoryDetail>();
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            EsnRepositoryDetail esnObj = new EsnRepositoryDetail();

                            esnObj.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                            esnObj.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                            esnObj.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UploadDate", DateTime.MinValue, false));
                            esnObj.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                            esnObj.FulfillmentStatus = clsGeneral.getColumnData(dataRow, "PoStatus", string.Empty, false) as string;
                            esnObj.FulfillmentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.MinValue, false));
                            esnObj.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.MinValue, false));

                            esnObj.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false));
                            esnObj.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                            esnObj.RmaStatus = clsGeneral.getColumnData(dataRow, "RmaStatus", string.Empty, false) as string;
                            esnObj.RmaEsnStatus = clsGeneral.getColumnData(dataRow, "RmaEsnStatus", string.Empty, false) as string;
                            esnList.Add(esnObj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //throw ex;
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

        public  List<EsnRepository> GetCustomerEsnRepositoryDownload(int companyID, int itemCompanyGUID, DateTime fromDate, DateTime toDate, bool unUsedESN, bool showAllUnusedESN)
        {
            List<EsnRepository> esnList = default; ; new List<EsnRepository>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@ItemCompanyGUID", itemCompanyGUID);
                    objCompHash.Add("@FromDate", fromDate.ToShortDateString() == "1/1/0001" ? null : fromDate.ToShortDateString());
                    objCompHash.Add("@ToDate", toDate.ToShortDateString() == "1/1/0001" ? null : toDate.ToShortDateString());
                    objCompHash.Add("@UnusedESN", unUsedESN);
                    objCompHash.Add("@ShowAllUnusedESN", showAllUnusedESN);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@ItemCompanyGUID", "@FromDate", "@ToDate", "@UnusedESN", "@ShowAllUnusedESN" };


                    dataTable = db.GetTableRecords(objCompHash, "av_EsnRepository_Download", arrSpFieldSeq);


                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        esnList = new List<EsnRepository>();
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            EsnRepository esnObj = new EsnRepository();

                            esnObj.UnusedESN = clsGeneral.getColumnData(dataRow, "UnusedESN", string.Empty, false) as string;
                            esnObj.InPorocessESN = clsGeneral.getColumnData(dataRow, "InprocessedESN", string.Empty, false) as string;
                            esnObj.ShippedESN = clsGeneral.getColumnData(dataRow, "ShippedESN", string.Empty, false) as string;
                            esnObj.RmaESN = clsGeneral.getColumnData(dataRow, "RmaESN", string.Empty, false) as string;

                            esnList.Add(esnObj);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //throw ex;
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

        public DataTable GetESNList(List<clsEsnxml> esnList)
        {
            DataTable dataTable = default;// new DataTable();

            using (DBConnect db = new DBConnect())
            {
                string esnXML = clsGeneral.SerializeObjetToXML(esnList, "ArrayOfClsEsnxml", "clsEsnxml");
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
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return dataTable;
        }
        public  List<ReassignSKU> GetReassignSkuReport(int companyID, DateTime fromDate, DateTime toDate, string esn, string sku)
        {
            List<ReassignSKU> reassignSKUList = default;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;

                Hashtable objParams = new Hashtable();

                DataTable dataTable = default;// new DataTable();

                try
                {

                    objParams.Add("@CompanyID", companyID);
                    objParams.Add("@FromDate", fromDate.ToShortDateString());
                    objParams.Add("@ToDate", toDate.ToShortDateString());
                    objParams.Add("@ESN", esn);
                    objParams.Add("@SKU", sku);

                    arrSpFieldSeq =
                    new string[] { "@CompanyID", "@FromDate", "@ToDate", "@ESN", "@SKU" };

                    dataTable = db.GetTableRecords(objParams, "AV_ReassignSKU_SELECT", arrSpFieldSeq);
                    reassignSKUList = PopulateReassignSKU(dataTable);
                }
                catch (Exception ex)
                {

                    Logger.LogMessage(ex, this); // throw ex;

                }

                finally
                {
                    db.DBClose();
                    objParams = null;
                    arrSpFieldSeq = null;
                }
            }
            return reassignSKUList;
        }
        public List<StockInDemand> GetStockInDemandList(int companyID, string SKU, string fromDate, string toDate)
        {
            List<StockInDemand> stockList = default;// new List<StockInDemand>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;

                Hashtable objParams = new Hashtable();
                DataTable dataTable = default;// new DataTable();

                try
                {


                    objParams.Add("@CompanyID", companyID);
                    objParams.Add("@SKU", SKU);
                    objParams.Add("@FromDate", fromDate == "" ? null : fromDate);
                    objParams.Add("@ToDate", toDate == "" ? null : toDate);

                    arrSpFieldSeq =
                    new string[] { "@CompanyID", "@SKU", "@FromDate", "@ToDate" };

                    dataTable = db.GetTableRecords(objParams, "av_Stock_In_Demand_Select", arrSpFieldSeq);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        stockList = new List<StockInDemand>();
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            StockInDemand stockInDemand = new StockInDemand();

                            stockInDemand.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                            stockInDemand.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                            stockInDemand.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                            stockInDemand.RequiredQunatity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                            stockInDemand.OrderCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderCount", 0, false));
                            stockInDemand.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_hand", 0, false));
                            stockInDemand.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                            stockInDemand.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                            stockList.Add(stockInDemand);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {

                    db.DBClose();
                    objParams = null;
                    arrSpFieldSeq = null;

                }
            }
            return stockList;
        }
        
        public List<CurrentStock> GetCurrentStock(int companyId, string SKU, bool IsDisable, bool IsKitted)
        {
            List<CurrentStock> stockStatusList = default;// new List<CurrentStock>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@companyID", companyId);
                    objCompHash.Add("@piSKU", string.IsNullOrEmpty(SKU) ? null : SKU);
                    objCompHash.Add("@piIsDisable", IsDisable);
                    objCompHash.Add("@piIsKitted", IsKitted);


                    arrSpFieldSeq = new string[] { "@companyID", "@piSKU", "@piIsDisable", "@piIsKitted" };
                    dt = db.GetTableRecords(objCompHash, "av_StockStatus_Current", arrSpFieldSeq);
                    stockStatusList = PopulateCurrentStock(dt);

                }
                catch (Exception objExp)
                {
                    //serviceOrders = null;
                    Logger.LogMessage(objExp, this);//  throw new Exception(objExp.Message.ToString());

                }
                finally
                {
                    //db = null;
                }
            }
            return stockStatusList;

        }

        public List<StockCount> GetStockCountSummary(int companyId, string StockDateFrom, string StockDateTo, string sku, bool includeDisabledSKU)
        {
            List<StockCount> stockList = default;//new List<StockCount>();
            using (DBConnect db = new DBConnect())
            {
                //DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;

                DataSet ds = default;//new DataSet();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CompanyID", companyId);
                    objCompHash.Add("@FromDate", string.IsNullOrWhiteSpace(StockDateFrom) ? null : StockDateFrom);
                    objCompHash.Add("@ToDate", string.IsNullOrWhiteSpace(StockDateTo) ? null : StockDateTo);
                    objCompHash.Add("@SKU", sku);
                    objCompHash.Add("@IncludeDisabledSKU", includeDisabledSKU);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@SKU", "@IncludeDisabledSKU" };
                    ds = db.GetDataSet(objCompHash, "av_StockCount_Select", arrSpFieldSeq);

                    stockList = PopulateStockCount(ds.Tables[0]);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //serviceOrders = null;
                    //throw new Exception(objExp.Message.ToString());

                }
                finally
                {
                    // db = null;
                }
            }
            return stockList;

        }
        private static List<StockCount> PopulateStockCount(DataTable dt)
        {
            StockCount skuStock = default;//null;
            List<StockCount> stockList = default;//new List<StockCount>();
            if (dt != null && dt.Rows.Count > 0)
            {
                stockList = new List<StockCount>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    skuStock = new StockCount();
                    skuStock.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    skuStock.OpeningBalance = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OpeningBalance", 0, false));
                    skuStock.StockAssigned = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AssignedCount", 0, false));
                    skuStock.StockReassignment = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Reassignment", 0, false));
                    skuStock.StockReceived = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ReceiveCount", 0, false));
                    skuStock.DekitCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DekitCount", 0, false));
                    skuStock.UnProvisioningCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UnProvisioningCount", 0, false));
                    skuStock.ClosingBalance = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ClosingBalance", 0, false));
                    skuStock.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    skuStock.StockDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "StockDate", DateTime.Now, false));
                    skuStock.RefreshDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RefreshDate", DateTime.Now, false));
                    skuStock.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    stockList.Add(skuStock);

                }
            }
            return stockList;

        }

        private List<ReassignSKU> PopulateReassignSKU(DataTable dataTable)
        {
            List<ReassignSKU> reassignSKUList = default;// new List<ReassignSKU>();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                reassignSKUList = new List<ReassignSKU>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ReassignSKU reassignSKU = new ReassignSKU();
                    reassignSKU.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    reassignSKU.OldSKUCategory = clsGeneral.getColumnData(dataRow, "OldSKUCategory", string.Empty, false) as string;
                    reassignSKU.OldSKU = clsGeneral.getColumnData(dataRow, "OLDSKU", string.Empty, false) as string;
                    reassignSKU.NewSKUCategory = clsGeneral.getColumnData(dataRow, "NewSKUCategory", string.Empty, false) as string;
                    reassignSKU.NewSKU = clsGeneral.getColumnData(dataRow, "NEWSKU", string.Empty, false) as string;
                    //skuESN.TotalESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TotalEsncount", 0, false));
                    reassignSKU.ChangeDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ChangeDate", 0, false));
                    reassignSKU.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                    reassignSKU.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    reassignSKU.RMANumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;

                    reassignSKUList.Add(reassignSKU);


                }
            }
            return reassignSKUList;
        }

        private  List<CurrentStock> PopulateCurrentStock(DataTable dt)
        {
            CurrentStock skuStock = default;
            List<CurrentStock> stockStatusList = default;// new List<CurrentStock>();

            if (dt != null && dt.Rows.Count > 0)
            {
                stockStatusList = new List<CurrentStock>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    skuStock = new CurrentStock();
                    skuStock.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    skuStock.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    //skuStock.OpeningBalance = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OpeningBalance", 0, false));
                    skuStock.StockInHand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_hand", 0, false));
                    skuStock.IsDisable = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsDisable", false, false));
                    //skuStock.StockAssignment = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StockAssignment", 0, false));
                    //skuStock.DefectiveESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "BadESN", 0, false));
                    //skuStock.StockReceived = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "NewReceival", 0, false));
                    //skuStock.PendingAssignment = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PendingAssignment", 0, false));
                    //skuStock.ClosingBalance = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ClosingBalance", 0, false));
                    skuStock.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    //skuStock.ModelNumber = clsGeneral.getColumnData(dataRow, "ModelNumber", string.Empty, false) as string;
                    stockStatusList.Add(skuStock);

                }
            }
            return stockStatusList;

        }

        private List<EsnInfo> PopulateEsnDetailList(DataTable dataTable)
        {
            List<EsnInfo> esnInfoList = default;// new List<EsnInfo>();
            EsnInfo esnInfo = default;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                esnInfoList = new List<EsnInfo>();
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
                    esnInfo.BoxID = (string)clsGeneral.getColumnData(dataRow, "BoxID", string.Empty, false);

                    esnInfoList.Add(esnInfo);

                }
            }

            return esnInfoList;

        }


    }
}
