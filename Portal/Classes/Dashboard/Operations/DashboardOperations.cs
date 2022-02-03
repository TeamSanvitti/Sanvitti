using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace avii.Classes
{
    public class DashboardOperations
    {
        public static List<POSKUStock> GetSKUStock(int companyId)
        {
            List<POSKUStock> skuStockList = new List<POSKUStock>();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@piCompanyID", companyId);
                //objCompHash.Add("@piSKU", string.IsNullOrEmpty(SKU) ? null : SKU);
                //objCompHash.Add("@piIsDisable", IsDisable);
                //objCompHash.Add("@piIsKitted", IsKitted);


                arrSpFieldSeq = new string[] { "@piCompanyID" };
                dt = db.GetTableRecords(objCompHash, "AV_PurchaseOrderSKUStock_SELECT", arrSpFieldSeq);
                skuStockList = PopulateSKUStock(dt);

            }
            catch (Exception objExp)
            {
                //serviceOrders = null;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                db = null;
            }

            return skuStockList;

        }

        public static List<AssignedSKUSummary> GetAssignedSKUSummary(int companyID, string fromDate, string toDate, string sku)
        {
            List<AssignedSKUSummary> assignedSKUsSummaryList = new List<AssignedSKUSummary>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", string.IsNullOrWhiteSpace(fromDate)?null: fromDate);
                objCompHash.Add("@ToDate", string.IsNullOrWhiteSpace(toDate) ? null : toDate);
                objCompHash.Add("@SKU", sku);


                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@SKU" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_AssignedSKU_Summary", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        AssignedSKUSummary objAssignedSKUsSummary = new AssignedSKUSummary();

                        objAssignedSKUsSummary.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                        objAssignedSKUsSummary.InProcess = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "In Process", 0, false));
                        objAssignedSKUsSummary.Processed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Processed", 0, false));
                        objAssignedSKUsSummary.Shipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Shipped", 0, false));
                        objAssignedSKUsSummary.Closed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Closed", 0, false));
                        objAssignedSKUsSummary.Return = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Return", 0, false));
                        objAssignedSKUsSummary.OnHold = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "On Hold", 0, false));
                        objAssignedSKUsSummary.OutofStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Out of Stock", 0, false));
                        objAssignedSKUsSummary.Cancel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancel", 0, false));
                        objAssignedSKUsSummary.PartialProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Partial Processed", 0, false));
                        objAssignedSKUsSummary.PartialShipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Partial Shipped", 0, false));
                        objAssignedSKUsSummary.UnusedESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UnusedEsnCOUNT", 0, false));
                        objAssignedSKUsSummary.IsAdmin = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "IsAdmin", 0, false));

                        objAssignedSKUsSummary.SKU = clsGeneral.getColumnData(dataRow, "Item_Code", string.Empty, false) as string;

                        assignedSKUsSummaryList.Add(objAssignedSKUsSummary);
                    }

                }
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

            return assignedSKUsSummaryList;
        }
        
        public static List<RMAStatusSummary> GetRMAStatusSummary(int companyID, string fromDate, string toDate)
        {
            List<RMAStatusSummary> rmaStatusSummaryList = new List<RMAStatusSummary>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", string.IsNullOrWhiteSpace(fromDate) ? null : fromDate);
                objCompHash.Add("@ToDate", string.IsNullOrWhiteSpace(toDate) ? null : toDate);

                //objCompHash.Add("@TimeInterval", timeInterval);


                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_RMAStatus_Summary", arrSpFieldSeq);

                
                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RMAStatusSummary objRMAStatusSummary = new RMAStatusSummary();

                        objRMAStatusSummary.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaStatusID", 0, false));
                        objRMAStatusSummary.StatusCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusCount", 0, false));
                        objRMAStatusSummary.StatusText = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;

                        rmaStatusSummaryList.Add(objRMAStatusSummary);
                    }
                }
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

            return rmaStatusSummaryList;
        }
        public static List<ShipBySummary> GetShipBySummary(int companyID, string fromDate, string toDate)
        {
            List<ShipBySummary> shipBySummaryList = new List<ShipBySummary>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", string.IsNullOrWhiteSpace(fromDate) ? null : Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd"));
                objCompHash.Add("@ToDate", string.IsNullOrWhiteSpace(toDate) ? null : Convert.ToDateTime(toDate).ToString("yyyy-MM-dd"));
                //objCompHash.Add("@TimeInterval", timeInterval);


                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate" };

                dataTable = db.GetTableRecords(objCompHash, "AV_ShipBy_Summary", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        ShipBySummary objShipBySummary = new ShipBySummary();

                        objShipBySummary.ShipByCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShipByCount", 0, false));
                        objShipBySummary.ShipByText = clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, false) as string;
                        objShipBySummary.ShipByCode = clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false) as string;
                        objShipBySummary.ShipPackage = clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false) as string;
                        objShipBySummary.Cost = String.Format("{0:F2}", Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "Cost", 0, false)));
                        objShipBySummary.PO_Date = "";// clsGeneral.getColumnData(dataRow, "PO_Date", string.Empty, false) as string;

                        shipBySummaryList.Add(objShipBySummary);
                    }
                }
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

            return shipBySummaryList;
        }
        public static List<POStatusSummary> GetPOStatusSummary(int companyID, string fromDate, string toDate)
        {
            List<POStatusSummary> poStatusSummaryList = new List<POStatusSummary>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", string.IsNullOrWhiteSpace(fromDate) ? null : fromDate);
                objCompHash.Add("@ToDate", string.IsNullOrWhiteSpace(toDate) ? null : toDate);


                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_POStatus_Summary", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        POStatusSummary objPOStatusSummary = new POStatusSummary();

                        objPOStatusSummary.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                        objPOStatusSummary.StatusCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusCount", 0, false));
                        objPOStatusSummary.StatusText = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                        objPOStatusSummary.POType = clsGeneral.getColumnData(dataRow, "POType", string.Empty, false) as string;
                        objPOStatusSummary.SKUQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                        poStatusSummaryList.Add(objPOStatusSummary);
                    }
                }
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

            return poStatusSummaryList;
        }
        public static List<AssignedSKUsSummary> GetAssignedSKUsSummary(int companyID, int timeInterval, string sku)
        {
            List<AssignedSKUsSummary> assignedSKUsSummaryList = new List<AssignedSKUsSummary>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@TimeInterval", timeInterval);
                objCompHash.Add("@SKU", sku);


                arrSpFieldSeq = new string[] { "@CompanyID", "@TimeInterval","@SKU" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_AssignedSKUs_Summary", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        AssignedSKUsSummary objAssignedSKUsSummary = new AssignedSKUsSummary();

                        //objAssignedSKUsSummary.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaStatusID", 0, false));
                        objAssignedSKUsSummary.OrderCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderCount", 0, false));
                        objAssignedSKUsSummary.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;

                        assignedSKUsSummaryList.Add(objAssignedSKUsSummary);
                    }
                }
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

            return assignedSKUsSummaryList;
        }
        
        public static List<SKUsPOStatus> GetAssignedSKUsPOStatusSummary(int companyID, string sku)
        {
            List<SKUsPOStatus> assignedSKUsSummaryList = new List<SKUsPOStatus>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@SKU", sku);
                objCompHash.Add("@CompanyID", companyID);
                


                arrSpFieldSeq = new string[] {  "@SKU", "@CompanyID" };

                dataTable = db.GetTableRecords(objCompHash, "av_SKUPOStatusDetail_Select", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        SKUsPOStatus objAssignedSKUsSummary = new SKUsPOStatus();

                        //objAssignedSKUsSummary.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaStatusID", 0, false));
                        objAssignedSKUsSummary.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objAssignedSKUsSummary.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                        objAssignedSKUsSummary.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                        objAssignedSKUsSummary.Processed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Processed", 0, false));

                        objAssignedSKUsSummary.Shipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Shipped", 0, false));

                        objAssignedSKUsSummary.Cancel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancel", 0, false));

                        objAssignedSKUsSummary.Closed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Closed", 0, false));

                        objAssignedSKUsSummary.Return = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Return", 0, false));
                        
                        assignedSKUsSummaryList.Add(objAssignedSKUsSummary);
                    }
                }
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

            return assignedSKUsSummaryList;
        }

        public static List<UserSignInLog> GetUserSignInLogSummary(int userID)
        {
            List<UserSignInLog> userSignInLogList = new List<UserSignInLog>();
            //DataTable dataTable = new DataTable();
            //DBConnect db = new DBConnect();
            //string[] arrSpFieldSeq;
            //Hashtable objCompHash = new Hashtable();
            //try
            //{

            //    objCompHash.Add("@UserID", userID);
               


            //    arrSpFieldSeq = new string[] { "@UserID" };

            //    dataTable = db.GetTableRecords(objCompHash, "sv_svUserSignInLog_SELECT", arrSpFieldSeq);


            //    if (dataTable != null && dataTable.Rows.Count > 0)
            //    {

            //        foreach (DataRow dataRow in dataTable.Rows)
            //        {
            //            UserSignInLog objUserSignInLog = new UserSignInLog();

            //            objUserSignInLog.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));
            //            objUserSignInLog.SessionStartDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "SessionStartDate", string.Empty, false));
            //            objUserSignInLog.SessionEndDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "SessionEndDate", string.Empty, false));

            //            userSignInLogList.Add(objUserSignInLog);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            //finally
            //{
            //    db = null;
            //    objCompHash = null;
            //    arrSpFieldSeq = null;
            //}

            return userSignInLogList;
        }
        private static List<POSKUStock> PopulateSKUStock(DataTable dt)
        {
            POSKUStock skuStock = null;
            List<POSKUStock> stockList = new List<POSKUStock>();

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    skuStock = new POSKUStock();
                    skuStock.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    skuStock.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    skuStock.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    skuStock.Qty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                    skuStock.StockInHand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_hand", 0, false));
                    stockList.Add(skuStock);

                }
            }
            return stockList;

        }


    }
}