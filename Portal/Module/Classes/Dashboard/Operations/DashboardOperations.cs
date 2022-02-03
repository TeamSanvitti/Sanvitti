using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace avii.Classes
{
    public class DashboardOperations
    {
        public static List<RMAStatusSummary> GetRMAStatusSummary(int companyID, int timeInterval)
        {
            List<RMAStatusSummary> rmaStatusSummaryList = new List<RMAStatusSummary>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@TimeInterval", timeInterval);


                arrSpFieldSeq = new string[] { "@CompanyID", "@TimeInterval" };

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
        public static List<ShipBySummary> GetShipBySummary(int companyID, int timeInterval)
        {
            List<ShipBySummary> shipBySummaryList = new List<ShipBySummary>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@TimeInterval", timeInterval);


                arrSpFieldSeq = new string[] { "@CompanyID", "@TimeInterval" };

                dataTable = db.GetTableRecords(objCompHash, "AV_ShipBy_Summary", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        ShipBySummary objShipBySummary = new ShipBySummary();

                        objShipBySummary.ShipByCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShipByCount", 0, false));
                        objShipBySummary.ShipByText = clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, false) as string;
                        objShipBySummary.ShipByCode = clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false) as string;
                        objShipBySummary.PO_Date = clsGeneral.getColumnData(dataRow, "PO_Date", string.Empty, false) as string;

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
        public static List<POStatusSummary> GetPOStatusSummary(int companyID, int timeInterval)
        {
            List<POStatusSummary> poStatusSummaryList = new List<POStatusSummary>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@TimeInterval", timeInterval);


                arrSpFieldSeq = new string[] { "@CompanyID", "@TimeInterval" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_POStatus_Summary", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        POStatusSummary objPOStatusSummary = new POStatusSummary();

                        objPOStatusSummary.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                        objPOStatusSummary.StatusCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusCount", 0, false));
                        objPOStatusSummary.StatusText = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;

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
    }
}