using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;
using System.Linq;

namespace SV.Framework.DAL.Fulfillment
{
    public class FulfillmentReportOperation : BaseCreateInstance
    {
        public  List<FulfillmentBilling> GetFulfillmentBillingReport(int companyID, string DateFrom, string DateTo, string ShipFromDate, string ShipToDate, string FulfillmentNumber, string FulfillmentType, string ContactName)
        {
            List<FulfillmentBilling> poBillingList = default;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;
                Hashtable objCompHash = new Hashtable();
                //ShipmentInfo shipmentInfo = null;
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@FromDate", string.IsNullOrEmpty(DateFrom) ? null : DateFrom);
                    objCompHash.Add("@ToDate", string.IsNullOrEmpty(DateTo) ? null : DateTo);
                    objCompHash.Add("@ShipFromDate", string.IsNullOrEmpty(ShipFromDate) ? null : ShipFromDate);
                    objCompHash.Add("@ShipToDate", string.IsNullOrEmpty(ShipToDate) ? null : ShipToDate);
                    objCompHash.Add("@FulfillmentNumber", FulfillmentNumber);
                    objCompHash.Add("@FulfillmentType", FulfillmentType);
                    objCompHash.Add("@ContactName", ContactName);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@ShipFromDate", "@ShipToDate", "@FulfillmentNumber", "@FulfillmentType", "@ContactName" };

                    dt = db.GetTableRecords(objCompHash, "av_FulfillmentBilling_Report", arrSpFieldSeq);

                    poBillingList = PopulatePOBilling(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                   // db = null;
                }
            }
            return poBillingList;

        }
        
        public  List<ShipmentInfo> GetShipmentSummary(int companyID, string labelType, string shipmentDateFrom, string shipmentDateTo, string shipVia, string shipPackage)
        {
            List<ShipmentInfo> shipmentList = default;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;
                Hashtable objCompHash = new Hashtable();
                //ShipmentInfo shipmentInfo = null;
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@LabelType", labelType);
                    objCompHash.Add("@ShipmentDateFrom", string.IsNullOrEmpty(shipmentDateFrom) ? null : shipmentDateFrom);
                    objCompHash.Add("@ShipmentDateTo", string.IsNullOrEmpty(shipmentDateTo) ? null : shipmentDateTo);
                    objCompHash.Add("@ShipVia", shipVia);
                    objCompHash.Add("@ShipPackage", shipPackage);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@LabelType", "@ShipmentDateFrom", "@ShipmentDateTo", "@ShipVia", "@ShipPackage" };

                    dt = db.GetTableRecords(objCompHash, "av_Shipment_Summary", arrSpFieldSeq);

                    shipmentList = PopulateShipments(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return shipmentList;

        }

        public List<UnusedLabel> GetUnusedLabels(int CompanyID, string FromDate, string ToDate, string TrackingNumber, bool IsCancel)
        {
            List<UnusedLabel> unusedLabelList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CompanyID", CompanyID);
                    objCompHash.Add("@FromDate", FromDate == "" ? null : FromDate);
                    objCompHash.Add("@ToDate", ToDate == "" ? null : ToDate);
                    objCompHash.Add("@TrackingNumber", TrackingNumber);
                    objCompHash.Add("@IsCancel", IsCancel);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@TrackingNumber", "@IsCancel" };
                    dt = db.GetTableRecords(objCompHash, "av_ShippingLabelUnusedSelect", arrSpFieldSeq);
                    unusedLabelList = PopulateUnusedLabels(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return unusedLabelList;

        }

        public string UnusedLabelInsertUpdate(UnusedLabelInfo request)
        {
            string returnMessage = "Could not cancel label";

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                int returnValue = 0;

                try
                {
                    objCompHash.Add("@UserID", request.UserID);
                    objCompHash.Add("@svLabelIdsType", request.dataTable);


                    arrSpFieldSeq = new string[] { "@UserID", "@svLabelIdsType" };
                    returnValue = db.ExecuteNonQuery(objCompHash, "svUnusedLabel_Cancel", arrSpFieldSeq);
                    if (returnValue > 0)
                    {
                        returnMessage = "";
                    }
                    else
                    {
                        // returnMessage = "ModelNumber and SKU already exist";
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //
                    returnMessage = objExp.Message;
                    //logRequest.Comment = objExp.Message;
                    //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                    //throw objExp;
                }
                finally
                {
                    // SV.Framework.Admin.ItemLogOperation.ItemLogInsert(logRequest);
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnMessage;
        }

        public List<PurchaseOrderShipmentDB> GetPurchaseOrderShipmentReport(string PurchaseOrderNumber, DateTime DateFrom, DateTime DateTo, int companyId, out decimal totalShipPrice, out List<PurchaseOrderShipmentDB> shipmentDB)
        {
            totalShipPrice = 0;
            List<PurchaseOrderShipmentDB> shipments = default;
            shipmentDB = default;
            using (DBConnect db = new DBConnect())
            {
                System.Text.StringBuilder sbError = new System.Text.StringBuilder();
                // List<PurchaseOrderShipmentDB> shipments = null;

                string dateFrom = DateFrom.ToShortDateString();
                string dateTo = DateTo.ToShortDateString();

                string errorMessage = null;
                string[] arrSpFieldSeq;
                DataTable dt = new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@piCompanyID", companyId);
                    objCompHash.Add("@PO_Num", PurchaseOrderNumber);
                    objCompHash.Add("@DateFrom", dateFrom == "1/1/0001" ? null : dateFrom == "01/01/0001" ? null : dateFrom == "01-01-0001" ? null : dateFrom); //
                    objCompHash.Add("@DateTo", dateTo == "1/1/0001" ? null : dateTo == "01/01/0001" ? null : dateTo == "01-01-0001" ? null : dateTo);

                    arrSpFieldSeq = new string[] { "@piCompanyID", "@PO_Num", "@DateFrom", "@DateTo" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_ShipmentTracking_Select", arrSpFieldSeq);
                    shipmentDB = PopulateShipmentDB(dt);
                    if (shipmentDB != null && shipmentDB.Count > 0)
                    {
                        var res = shipmentDB.GroupBy(e => new { e.ShipPrice, e.TrackingNumber });

                        totalShipPrice = res.Sum(x => x.Key.ShipPrice);
                    }
                    
                    shipments = PopulateShipmentdb(shipmentDB);

                }


                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //
                    if (string.IsNullOrWhiteSpace(errorMessage))
                        errorMessage = ex.ToString();
                    else
                        errorMessage = string.Concat(errorMessage, ex.ToString());
                    sbError.Append(errorMessage);
                }
                finally
                {
                    //db = null;
                    if (sbError.Length > 0)
                        errorMessage = string.Concat(errorMessage, sbError.ToString());
                }

            }
            return shipments;
        }
        public  List<PurchaseOrderShipping> GetPurchaseOrderShipmentAPI(SV.Framework.Models.Service.PurchaseOrderShipmentRequest request, int companyId)
        {
            List<PurchaseOrderShipping> shipments = null;
            List<PurchaseOrderShipmentDB> shipmentDB = null;
            System.Text.StringBuilder sbError = new System.Text.StringBuilder();
            // List<PurchaseOrderShipmentDB> shipments = null;


            string errorMessage = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyID", companyId);
                objCompHash.Add("@PO_Num", request.PurchaseOrderNumber);
                objCompHash.Add("@DateFrom", request.DateFrom.ToShortDateString() == "1/1/0001" ? null : request.DateFrom.ToShortDateString()); //
                objCompHash.Add("@DateTo", request.DateTo.ToShortDateString() == "1/1/0001" ? null : request.DateTo.ToShortDateString());

                arrSpFieldSeq = new string[] { "@piCompanyID", "@PO_Num", "@DateFrom", "@DateTo" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_ShipmentTracking_Select", arrSpFieldSeq);
                shipmentDB = PopulateShipmentDB(dt);
                // HttpContext.Current.Session["shipmentDB"] = shipmentDB;
                shipments = PopulateShipmentAPI(shipmentDB);

            }


            catch (Exception ex)
            {
                if (string.IsNullOrWhiteSpace(errorMessage))
                    errorMessage = ex.ToString();
                else
                    errorMessage = string.Concat(errorMessage, ex.ToString());
                sbError.Append(errorMessage);
            }
            finally
            {
                db = null;
                if (sbError.Length > 0)
                    errorMessage = string.Concat(errorMessage, sbError.ToString());
            }


            return shipments;
        }

        public List<FulfillmentStatusReport> GetCustomerFulfillmentStatusReport(int companyID, int timeInterval, int userID)
        {
            List<FulfillmentStatusReport> fulfillmentStatusReportList = default;
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();

                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {

                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@TimeInterval", timeInterval);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@TimeInterval", "@UserID" };

                    dataTable = db.GetTableRecords(objCompHash, "AV_Customer_Fulfillment_Report", arrSpFieldSeq);


                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        fulfillmentStatusReportList = new List<FulfillmentStatusReport>();
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            FulfillmentStatusReport objFulfillmentStatusReport = new FulfillmentStatusReport();

                            objFulfillmentStatusReport.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                            objFulfillmentStatusReport.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                            objFulfillmentStatusReport.InProcess = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "In Process", 0, false));
                            objFulfillmentStatusReport.Processed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Processed", 0, false));
                            objFulfillmentStatusReport.Shipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Shipped", 0, false));
                            objFulfillmentStatusReport.Closed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Closed", 0, false));
                            objFulfillmentStatusReport.Return = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Return", 0, false));
                            objFulfillmentStatusReport.OnHold = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "On Hold", 0, false));
                            objFulfillmentStatusReport.OutofStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Out of Stock", 0, false));
                            objFulfillmentStatusReport.Cancel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancel", 0, false));
                            objFulfillmentStatusReport.PartialProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Partial Processed", 0, false));

                            objFulfillmentStatusReport.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;

                            fulfillmentStatusReportList.Add(objFulfillmentStatusReport);
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
            return fulfillmentStatusReportList;
        }
        public  List<StoreFulfillmentStatus> GetCustomerStoreFulfillmentStatusReport(int companyID, string companyName, int timeInterval, int userID)
        {
            List<StoreFulfillmentStatus> fulfillmentStatusReportList = default;
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();

                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {

                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@CompanyName", companyName);
                    objCompHash.Add("@TimeInterval", timeInterval);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@CompanyName", "@TimeInterval", "@UserID" };

                    dataTable = db.GetTableRecords(objCompHash, "AV_Storewise_Fulfillment_Report", arrSpFieldSeq);


                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        fulfillmentStatusReportList = new List<StoreFulfillmentStatus>();
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            StoreFulfillmentStatus objFulfillmentStatusReport = new StoreFulfillmentStatus();

                            objFulfillmentStatusReport.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                            objFulfillmentStatusReport.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                            objFulfillmentStatusReport.InProcess = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "In Process", 0, false));
                            objFulfillmentStatusReport.Processed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Processed", 0, false));
                            objFulfillmentStatusReport.Shipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Shipped", 0, false));
                            objFulfillmentStatusReport.Closed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Closed", 0, false));
                            objFulfillmentStatusReport.Return = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Return", 0, false));
                            objFulfillmentStatusReport.OnHold = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "On Hold", 0, false));
                            objFulfillmentStatusReport.OutofStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Out of Stock", 0, false));
                            objFulfillmentStatusReport.Cancel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancel", 0, false));
                            objFulfillmentStatusReport.PartialProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Partial Processed", 0, false));

                            objFulfillmentStatusReport.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;

                            fulfillmentStatusReportList.Add(objFulfillmentStatusReport);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return fulfillmentStatusReportList;
        }

        public  List<FulfillmentSKUStatus> GetCustomerFulfillmentSKUStatusReport(int companyID, int timeInterval, int userID)
        {
            List<FulfillmentSKUStatus> fulfillmentStatusReportList = default;// new List<FulfillmentSKUStatus>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();

                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {

                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@TimeInterval", timeInterval);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@TimeInterval", "@UserID" };

                    dataTable = db.GetTableRecords(objCompHash, "AV_Customer_Fulfillment_SKU_Report", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        fulfillmentStatusReportList = new List<FulfillmentSKUStatus>();
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            FulfillmentSKUStatus objFulfillmentStatusReport = new FulfillmentSKUStatus();

                            objFulfillmentStatusReport.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                            objFulfillmentStatusReport.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                            objFulfillmentStatusReport.InProcess = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "In Process", 0, false));
                            objFulfillmentStatusReport.Processed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Processed", 0, false));
                            objFulfillmentStatusReport.Shipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Shipped", 0, false));
                            objFulfillmentStatusReport.Closed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Closed", 0, false));
                            objFulfillmentStatusReport.Return = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Return", 0, false));
                            objFulfillmentStatusReport.OnHold = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "On Hold", 0, false));
                            objFulfillmentStatusReport.OutofStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Out of Stock", 0, false));
                            objFulfillmentStatusReport.Cancel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancel", 0, false));
                            objFulfillmentStatusReport.PartialProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Partial Processed", 0, false));

                            objFulfillmentStatusReport.SKU = clsGeneral.getColumnData(dataRow, "Item_Code", string.Empty, false) as string;

                            fulfillmentStatusReportList.Add(objFulfillmentStatusReport);
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
            return fulfillmentStatusReportList;
        }

        
        private List<PurchaseOrderShipmentDB> PopulateShipmentdb(List<PurchaseOrderShipmentDB> shipmentDB)
        {

            List<PurchaseOrderShipmentDB> shipments = default;// new List<PurchaseOrderShipmentDB>();
            PurchaseOrderShipmentDB shipment = default;// new PurchaseOrderShipmentDB();
            var res = shipmentDB.GroupBy(e => new { e.PO_NUM, e.PO_ID, e.PO_Date, e.Ship_Via, e.PoType, e.ContactName });
            //e.ShipPackage, e.ShipPrice, e.ShipWeight, 
            if (res != null)
            {
                shipments = new List<PurchaseOrderShipmentDB>();
                foreach (var val in res)
                {
                    shipment = new PurchaseOrderShipmentDB()
                    {
                        PO_NUM = val.Key.PO_NUM,
                        Ship_Via = val.Key.Ship_Via,
                        PO_ID = val.Key.PO_ID,
                        PO_Date = val.Key.PO_Date,
                        ContactName = val.Key.ContactName,
                        PoType = val.Key.PoType
                        // ShipWeight = val.Key.ShipWeight,
                        // ShipPrice = val.Key.ShipPrice,
                        // ShipPackage = val.Key.ShipPackage
                        //                    ShipDate = (val.Key.ShipDate != null ? val.Key.ShipDate.ToString() : null),
                        // AcknowledgmentSent = (val.Key.AcknowledgmentSent != null ? val.Key.AcknowledgmentSent.ToString() : ""),
                    };

                    shipments.Add(shipment);
                }
            }

            return shipments;
        }

        private List<PurchaseOrderShipmentDB> PopulateShipmentDB(DataTable dt)
        {
            List<PurchaseOrderShipmentDB> shipments = default;// new List<PurchaseOrderShipmentDB>();
            PurchaseOrderShipmentDB shipment = default;
            if (dt != null && dt.Rows.Count > 0)
            {
                shipments = new List<PurchaseOrderShipmentDB>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    shipment = new PurchaseOrderShipmentDB();
                    shipment.BatchNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
                    shipment.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    shipment.ICC_ID = clsGeneral.getColumnData(dataRow, "ICC_ID", string.Empty, false) as string;
                    shipment.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;

                    shipment.Line_no = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Line_no", 0, false));
                    shipment.PO_Date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.MinValue, false)).ToShortDateString();
                    shipment.PO_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Po_ID", 0, false));
                    shipment.Qty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                    shipment.PO_NUM = clsGeneral.getColumnData(dataRow, "PO_NUM", string.Empty, false) as string;
                    shipment.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipDate", DateTime.MinValue, false)).ToShortDateString();
                    shipment.Ship_Via = clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false) as string;
                    shipment.SKU = clsGeneral.getColumnData(dataRow, "item_code", string.Empty, false) as string;
                    shipment.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    shipment.AcknowledgmentSent = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "AcknowledgmentSent", DateTime.MinValue, false)).ToShortDateString();//clsGeneral.getColumnData(dataRow, "AcknowledgmentSent", string.Empty, false) as string;
                    shipment.LineNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LineNumber", 0, false));

                    shipment.PoType = clsGeneral.getColumnData(dataRow, "PoType", string.Empty, false) as string;
                    shipment.ContactName = clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, false) as string;
                    shipment.ShipmentType = clsGeneral.getColumnData(dataRow, "ShipmentType", string.Empty, false) as string;
                    shipment.ShipPackage = clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false) as string;
                    shipment.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    shipment.ShipMethod = clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, false) as string;
                    shipment.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    shipment.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;

                    shipment.ShipPrice = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "FinalPostage", 0, false));
                    shipment.ShipWeight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));
                    shipments.Add(shipment);
                }
            }

            return shipments;
        }

        private List<UnusedLabel> PopulateUnusedLabels(DataTable dt)
        {
            UnusedLabel unusedLabel = default;
            List<UnusedLabel> unusedLabelList = default; ;
            if (dt != null && dt.Rows.Count > 0)
            {
                unusedLabelList = new List<UnusedLabel>();
                foreach (DataRow row in dt.Rows)
                {

                    unusedLabel = new UnusedLabel();

                    unusedLabel.TrackingNumber = clsGeneral.getColumnData(row, "TrackingNumber", string.Empty, false) as string;
                    unusedLabel.AssignedTo = clsGeneral.getColumnData(row, "AssignedTo", string.Empty, false) as string;
                    unusedLabel.AssignToNumber = clsGeneral.getColumnData(row, "AssignToNumber", string.Empty, false) as string;
                    unusedLabel.LabelGenerationDate = clsGeneral.getColumnData(row, "LabelGenerationDate", string.Empty, false) as string;
                    unusedLabel.LabelType = clsGeneral.getColumnData(row, "LabelType", string.Empty, false) as string;
                    unusedLabel.ShipmentMethod = clsGeneral.getColumnData(row, "ShipmentMethod", string.Empty, false) as string;
                    unusedLabel.ShipPackage = clsGeneral.getColumnData(row, "ShipPackage", string.Empty, false) as string;
                    // unusedLabel.TrackingNumber = clsGeneral.getColumnData(row, "TrackingNumber", string.Empty, false) as string;
                    unusedLabel.FinalPostage = Convert.ToDecimal(clsGeneral.getColumnData(row, "FinalPostage", 0, false));
                    unusedLabel.ShippingWeight = Convert.ToDecimal(clsGeneral.getColumnData(row, "ShippingWeight", 0, false));
                    unusedLabel.POID = Convert.ToInt32(clsGeneral.getColumnData(row, "PO_ID", 0, false));
                    unusedLabel.ID = Convert.ToInt32(clsGeneral.getColumnData(row, "ID", 0, false));
                    unusedLabel.LabelSource = clsGeneral.getColumnData(row, "LabelSource", string.Empty, false) as string;
                    unusedLabelList.Add(unusedLabel);


                }
            }
            return unusedLabelList;

        }
        private List<FulfillmentBilling> PopulatePOBilling(DataTable dt)
        {
            List<FulfillmentBilling> poBillingList = default;
            FulfillmentBilling po = default;

            if (dt != null && dt.Rows.Count > 0)
            {
                poBillingList = new List<FulfillmentBilling>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    po = new FulfillmentBilling();
                    po.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    po.FulfillmentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.MinValue, false));
                    po.Price = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "FinalPostage", 0, false));
                    po.Weight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));

                    po.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipDate", DateTime.MinValue, false));
                    po.ShipVia = clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, false) as string;
                    po.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    po.ShipmentType = clsGeneral.getColumnData(dataRow, "ReturnLabel", string.Empty, false) as string;
                    po.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    po.ICC_ID = clsGeneral.getColumnData(dataRow, "ICC_ID", string.Empty, false) as string;
                    po.BatchNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
                    po.ShipPackage = clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false) as string;
                    po.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    po.ContactName = clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, false) as string;
                    po.FulfillmentType = clsGeneral.getColumnData(dataRow, "POType", string.Empty, false) as string;


                    poBillingList.Add(po);
                }
            }

            return poBillingList;
        }

        private List<ShipmentInfo> PopulateShipments(DataTable dt)
        {
            List<ShipmentInfo> shipmentList = default;
            ShipmentInfo shipmentInfo = default;

            if (dt != null && dt.Rows.Count > 0)
            {
                shipmentList = new List<ShipmentInfo>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    shipmentInfo = new ShipmentInfo();
                    shipmentInfo.AssignmentNumber = clsGeneral.getColumnData(dataRow, "AssignmentNumber", string.Empty, false) as string;
                    shipmentInfo.FinalPostage = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "FinalPostage", 0, false));
                    shipmentInfo.ShipWeight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));
                    shipmentInfo.ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ID", 0, false));
                    shipmentInfo.IsPrint = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsPrint", false, false));

                    shipmentInfo.LabelGenerationDate = clsGeneral.getColumnData(dataRow, "CreatedDate", string.Empty, false) as string;
                    shipmentInfo.LabelType = clsGeneral.getColumnData(dataRow, "LabelType", string.Empty, false) as string;
                    shipmentInfo.LabelUsedDate = clsGeneral.getColumnData(dataRow, "LabelUsedDate", string.Empty, false) as string;
                    shipmentInfo.ShipMethod = clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, false) as string;
                    shipmentInfo.ShipPackage = clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false) as string;
                    shipmentInfo.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;

                    shipmentList.Add(shipmentInfo);
                }
            }

            return shipmentList;
        }

        private  List<PurchaseOrderShipping> PopulateShipmentAPI(List<PurchaseOrderShipmentDB> shipmentDB)
        {

            List<PurchaseOrderShipping> shipments = new List<PurchaseOrderShipping>();
            PurchaseOrderShipping shipment = new PurchaseOrderShipping();
            var res = shipmentDB.GroupBy(e => new { e.PO_NUM, e.PO_ID, e.PO_Date, e.Ship_Via });

            List<PurchaseOrderTracking> trackings = null;
            PurchaseOrderTracking tracking = null;
            List<LineItems> lineItems = null;
            LineItems lineItem = null;
            int qty = 1;
            foreach (var val in res)
            {
                shipment = new PurchaseOrderShipping()
                {
                    PurchaseOrderNumber = val.Key.PO_NUM,
                    PODate = val.Key.PO_Date,
                    ShippingMethod = val.Key.Ship_Via,

                };
                List<PurchaseOrderShipmentDB> shipmentList = val.ToList();
                var trackingList = val.ToList().GroupBy(e => new { e.TrackingNumber, e.AcknowledgmentSent, e.ShipDate, e.ShipmentType });
                trackings = new List<PurchaseOrderTracking>();

                foreach (var e in trackingList)
                {
                    tracking = new PurchaseOrderTracking();

                    tracking.TrackingNumber = e.Key.TrackingNumber;
                    tracking.AcknowledgmentSent = e.Key.AcknowledgmentSent;
                    tracking.ShipDate = e.Key.ShipDate;
                    tracking.ShipmentType = e.Key.ShipmentType;

                    lineItems = new List<LineItems>();
                    var poLineItemList = (from item in shipmentDB where item.TrackingNumber.Equals(tracking.TrackingNumber) select item).ToList();
                    foreach (PurchaseOrderShipmentDB item in poLineItemList)
                    {
                        lineItem = new LineItems();
                        lineItem.BatchNumber = item.BatchNumber;
                        lineItem.ESN = item.ESN;
                        lineItem.ICCID = item.ICC_ID;
                        lineItem.SKU = item.SKU;
                        if (string.IsNullOrWhiteSpace(lineItem.ESN))
                            qty = item.Qty;
                        lineItem.Qty = qty;
                        lineItems.Add(lineItem);

                    }
                    tracking.LineItems = lineItems;
                    trackings.Add(tracking);
                }

                shipment.Trackings = trackings;
                shipments.Add(shipment);
            }


            return shipments;
        }

        //private static List<PurchaseOrderShipmentDB> PopulateShipmentDB(DataTable dt)
        //{
        //    List<PurchaseOrderShipmentDB> shipments = null;// new List<PurchaseOrderShipmentDB>();
        //    PurchaseOrderShipmentDB shipment = null;
        //    if (dt != null && dt.Rows.Count > 0)
        //    {
        //        shipments = new List<PurchaseOrderShipmentDB>();
        //        foreach (DataRow dataRow in dt.Rows)
        //        {
        //            shipment = new PurchaseOrderShipmentDB();
        //            shipment.BatchNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
        //            shipment.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
        //            shipment.ICC_ID = clsGeneral.getColumnData(dataRow, "ICC_ID", string.Empty, false) as string;
        //            shipment.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;

        //            shipment.Line_no = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Line_no", 0, false));
        //            shipment.PO_Date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.MinValue, false)).ToShortDateString();
        //            shipment.PO_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Po_ID", 0, false));
        //            shipment.Qty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
        //            shipment.PO_NUM = clsGeneral.getColumnData(dataRow, "PO_NUM", string.Empty, false) as string;
        //            shipment.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipDate", DateTime.MinValue, false)).ToShortDateString();
        //            shipment.Ship_Via = clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false) as string;
        //            shipment.SKU = clsGeneral.getColumnData(dataRow, "item_code", string.Empty, false) as string;
        //            shipment.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
        //            shipment.AcknowledgmentSent = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "AcknowledgmentSent", DateTime.MinValue, false)).ToShortDateString();//clsGeneral.getColumnData(dataRow, "AcknowledgmentSent", string.Empty, false) as string;
        //            shipment.LineNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LineNumber", 0, false));

        //            shipment.PoType = clsGeneral.getColumnData(dataRow, "PoType", string.Empty, false) as string;
        //            shipment.ContactName = clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, false) as string;
        //            shipment.ShipmentType = clsGeneral.getColumnData(dataRow, "ShipmentType", string.Empty, false) as string;
        //            shipment.ShipPackage = clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false) as string;
        //            shipment.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
        //            shipment.ShipMethod = clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, false) as string;
        //            shipment.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
        //            shipment.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;

        //            shipment.ShipPrice = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "FinalPostage", 0, false));
        //            shipment.ShipWeight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));
        //            shipments.Add(shipment);
        //        }
        //    }

        //    return shipments;
        //}


    }
}
