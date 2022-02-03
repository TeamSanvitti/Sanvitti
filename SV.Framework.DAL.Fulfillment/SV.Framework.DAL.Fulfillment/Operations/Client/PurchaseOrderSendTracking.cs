using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class PurchaseOrderSendTracking : BaseCreateInstance
    {
        private const int COMPANY_ID = 464;
        private const string API_URL = "http://partner.t.freedompop.com/langlobal/order/status";
        public List<PurchaseOrderShipmentDB> GetPurchaseOrderToBeSent(int companyID, string fromDate, string toDate, string poNum, bool ShipmentSentOnly, string TrackingNumber)
        {
            //List<FulfillmentTracking> shipments = null;
            List<PurchaseOrderShipmentDB> shipmentDB = default;//null;
            System.Text.StringBuilder sbError = new System.Text.StringBuilder();
            // List<PurchaseOrderShipmentDB> shipments = null;
            int recordCount = 0, errornumber = 0;
            // string errorMessage = string.Empty;

            string errorMessage = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@piStatusID", 3);
                    objCompHash.Add("@piCompanyID", companyID);
                    objCompHash.Add("@piPO_Num", poNum);
                    objCompHash.Add("@piFromDate", string.IsNullOrWhiteSpace(fromDate) ? null : fromDate);
                    objCompHash.Add("@piToDate", string.IsNullOrWhiteSpace(toDate) ? null : toDate);
                    objCompHash.Add("@ShipmentSentOnly", ShipmentSentOnly);
                    objCompHash.Add("@TrackingNumber", TrackingNumber);


                    arrSpFieldSeq = new string[] { "@piStatusID", "@piCompanyID", "@piPO_Num", "@piFromDate", "@piToDate", "@ShipmentSentOnly", "@TrackingNumber" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_TrackingToBeSent", arrSpFieldSeq, "@poRowCount", "@poErrorNumber", "@poErrorMessage", out recordCount, out errornumber, out errorMessage);

                    shipmentDB = PopulateShipmentDB(dt);
                    //HttpContext.Current.Session["shipmentDB"] = shipmentDB;
                    // shipments = PopulateShipmentdb(shipmentDB);

                }


                catch (Exception ex)
                {
                    if (string.IsNullOrWhiteSpace(errorMessage))
                        errorMessage = ex.ToString();
                    else
                        errorMessage = string.Concat(errorMessage, ex.ToString());
                    sbError.Append(errorMessage);
                    Logger.LogMessage(ex, this); //
                }
                finally
                {
                    // db = null;
                    if (sbError.Length > 0)
                        errorMessage = string.Concat(errorMessage, sbError.ToString());
                }

            }
            return shipmentDB;
        }
        public string AssignAcknowledgement(int po_id, string trackingNumber)
        {
            string errorMessage = default;//string.Empty;
            int recordCount = 0, errornumber = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@po_id", po_id);
                    objCompHash.Add("@TrackingNumber", trackingNumber);

                    arrSpFieldSeq = new string[] { "@po_id", "@TrackingNumber" };
                    db.ExeCommand(objCompHash, "av_purchaseorder_AckSent_Insert", arrSpFieldSeq, "@poRowCount", "@poErrorNumber", "@poErrorMessage", out recordCount, out errornumber, out errorMessage);
                    if (recordCount > 0)
                    {
                        errorMessage = "";// ResponseErrorCode.UpdatedSuccessfully.ToString();

                    }
                    //else if (!string.IsNullOrEmpty(errorMessage))
                    //{
                    //   // errorMessage = ResponseErrorCode.PurchaseOrderNotExists.ToString();

                    //}
                    else
                    {
                        errorMessage = ResponseErrorCode.DataNotUpdated.ToString();

                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //
                    errorMessage = objExp.Message.ToString();
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return errorMessage;
        }

        public IntegrationModel GetShipmentTrackingAPIInfo(int companyID, string shipmentTracking)
        {
            IntegrationModel model = new IntegrationModel();
            string errorMessage = default;//string.Empty;
            //   int recordCount = 0, errornumber = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                DataTable dt = default;// new DataTable();
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@ShipmentTracking", shipmentTracking);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@ShipmentTracking" };
                    dt = db.GetTableRecords(objCompHash, "av_Company_APIAddressInfo", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            model.APIAddress = clsGeneral.getColumnData(dataRow, "APIAddress", string.Empty, false) as string;
                            model.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                            model.Password = clsGeneral.getColumnData(dataRow, "Password", string.Empty, false) as string;
                            model.APIName = clsGeneral.getColumnData(dataRow, "IntegrationName", string.Empty, false) as string;

                        }
                        errorMessage = "";// ResponseErrorCode.UpdatedSuccessfully.ToString();

                    }
                    else
                    {
                        errorMessage = ResponseErrorCode.DataNotUpdated.ToString();

                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //
                    errorMessage = objExp.Message.ToString();
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return model;
        }

        private List<PurchaseOrderShipmentDB> PopulateShipmentDB(DataTable dt)
        {
            List<PurchaseOrderShipmentDB> shipments = default;//null;// new List<PurchaseOrderShipmentDB>();
            PurchaseOrderShipmentDB shipment = default;//null;
            if (dt != null && dt.Rows.Count > 0)
            {
                shipments = new List<PurchaseOrderShipmentDB>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    shipment = new PurchaseOrderShipmentDB();
                    shipment.BatchNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
                    shipment.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    shipment.ICC_ID = clsGeneral.getColumnData(dataRow, "ICC_ID", string.Empty, false) as string;
                    shipment.Line_no = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Line_no", 0, false));
                    shipment.PO_Date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", string.Empty, false)).ToShortDateString();
                    shipment.PO_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Po_ID", 0, false));
                    shipment.Qty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                    shipment.PO_NUM = clsGeneral.getColumnData(dataRow, "PO_NUM", string.Empty, false) as string;
                    shipment.CustomerOrderNumber = clsGeneral.getColumnData(dataRow, "CustomerOrderNumber", string.Empty, false) as string;

                    shipment.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipDate", string.Empty, false)).ToString("yyyy-MM-dd");
                    shipment.Ship_Via = clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false) as string;
                    shipment.SKU = clsGeneral.getColumnData(dataRow, "item_code", string.Empty, false) as string;
                    shipment.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    shipment.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    shipment.AcknowledgmentSent = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "TrackingSentDateTime", DateTime.MinValue, false)).ToString("yyyy-MM-dd"); //clsGeneral.getColumnData(dataRow, "AcknowledgmentSent", string.Empty, false) as string;

                    //  shipment.ShipmentType = clsGeneral.getColumnData(dataRow, "ShipmentType", string.Empty, false) as string;
                    shipment.PO_Status = clsGeneral.getColumnData(dataRow, "PO_Status", string.Empty, false) as string;
                    shipments.Add(shipment);
                }
            }

            return shipments;
        }


    }
}
