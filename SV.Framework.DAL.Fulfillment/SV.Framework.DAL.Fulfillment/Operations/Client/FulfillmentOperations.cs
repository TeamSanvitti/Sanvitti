using System.Collections.Generic;
using System.Data;
using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class FulfillmentOperations : BaseCreateInstance
    {
        
        public  List<FulfillmentComment> GetFulfillmentComments(int poID)
        {
            List<FulfillmentComment> commentsList = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                
                try
                {
                    objCompHash.Add("@POID", poID);

                    arrSpFieldSeq = new string[] { "@POID" };
                    dt = db.GetTableRecords(objCompHash, "av_Fulfillment_Comments_select", arrSpFieldSeq);
                    commentsList = PopulateFulfillmentComments(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
             //       db = null;
                }
            }
            return commentsList;
        }


        public  FulfillmentEsNInfo GetPurchaseOrderESNs(int POID)
        {
            FulfillmentEsNInfo fulfillmentEsNInfo = default;//null;// new FulfillmentEsNInfo();
            List<FulfillmentEsn> esnList = default;//null;//new List<FulfillmentEsn>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@PO_ID", POID);


                    arrSpFieldSeq = new string[] { "@PO_ID" };
                    ds = db.GetDataSet(objCompHash, "Av_PurchaseOrder_ESN_Select", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            fulfillmentEsNInfo = new FulfillmentEsNInfo();
                            fulfillmentEsNInfo.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                            fulfillmentEsNInfo.Qty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                            fulfillmentEsNInfo.ShippedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.Now, false)).ToString("yyyyMMdd");

                        }
                        esnList = PopulateFulfillmentESNs(ds.Tables[1]);
                        if (esnList != null)
                        {
                            fulfillmentEsNInfo.EsnList = esnList;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);
                }
                finally
                {
                    // db = null;

                }
            }


            return fulfillmentEsNInfo;
        }

        public  string UEDF_File_Update(int po_id, string fileName, DateTime currentCSTDateTime, DateTime currentUtcDateTime)
        {
            string errorMessage = default;//string.Empty;
            int recordCount = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@po_id", po_id);
                    objCompHash.Add("@UEDF_FileName", fileName);
                    objCompHash.Add("@CurrentCSTDateTime", currentCSTDateTime);
                    objCompHash.Add("@CurrentUTCDateTime", currentUtcDateTime);

                    arrSpFieldSeq = new string[] { "@po_id", "@UEDF_FileName", "@CurrentCSTDateTime", "@CurrentUTCDateTime" };
                    recordCount = db.ExecuteNonQuery(objCompHash, "av_PurchaseOrder_UEDF_FileNameUpdate", arrSpFieldSeq);
                    if (recordCount > 0)
                    {
                        errorMessage = "";// ResponseErrorCode.UpdatedSuccessfully.ToString();
                    }
                    else
                    {
                        errorMessage = clsGeneral.ResponseErrorCode.DataNotUpdated.ToString();
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
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

        public  List<FulfillmentDetailModel> GetPurchaseOrderTxtFile(int POID, out string ShipmentDate)
        {
            ShipmentDate = "";
            List<FulfillmentDetailModel> poDetails = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();
               try
                {
                    objCompHash.Add("@POID", POID);


                    arrSpFieldSeq = new string[] { "@POID" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderDetail_TXTFILE", arrSpFieldSeq);

                    poDetails = PopulateFulfillmentDetail(dt, out ShipmentDate);

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);
                }
                finally
                {
                   // db = null;

                }
            }

            return poDetails;
        }

        public string FulfillmentAddLineItems(List<FulfillmentItem> fulfillmentItems, int companyID, int POID, int userID, out int recordCount, out int returnCount)
        {
            string errorMessage = default;//"";
            recordCount = 0;
            returnCount = 0;
            string itemXML = clsGeneral.SerializeObject(fulfillmentItems);

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {

                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@POID", POID);
                    objCompHash.Add("@piXMLData", itemXML);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@POID", "@piXMLData" };

                    db.ExeCommand(objCompHash, "av_PurchaseOrder_AddLineItems", arrSpFieldSeq, "@poRecordCount", "@poReturnCount", out recordCount, out returnCount);


                }
                catch (Exception ex)
                {
                    errorMessage = ex.Message;
                    //throw ex;
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }

            return errorMessage;
        }




        private PurchaseOrderShipment PopulateShipmentNew(List<PurchaseOrderShipmentDB> shipmentDB, string trackingNumber, out int userID)
        {
            userID = 0;
            //List<PurchaseOrderShipmentDB> shipmentDB = Session["shipmentDB"] as List<PurchaseOrderShipmentDB>;
            var shipmentList = (from item in shipmentDB where item.TrackingNumber.Equals(trackingNumber) select item).ToList();

            //List<PurchaseOrderShipment> shipments = new List<PurchaseOrderShipment>();
            PurchaseOrderShipment shipment = new PurchaseOrderShipment();
            LineItem lineItem = null;
            List<LineItem> lineItems = null;
            var res = shipmentList.GroupBy(e => new { e.CustomerOrderNumber, e.PO_ID, e.PO_Date, e.Ship_Via, e.TrackingNumber, e.ContainerID, e.ShipDate, e.APIAddress });

            foreach (var val in res)
            {
                shipment = new PurchaseOrderShipment()
                {
                    PO_ID = val.Key.PO_ID,
                    PurchaseOrderNumber = val.Key.CustomerOrderNumber,
                    ShippingMethod = val.Key.Ship_Via,
                    ShippingTrackingNumber = val.Key.TrackingNumber,
                    ShippingDate = (val.Key.ShipDate != null ? val.Key.ShipDate.ToString() : null),
                };
                lineItems = new List<LineItem>();
                foreach (PurchaseOrderShipmentDB e in val)
                {
                    lineItem = new LineItem();
                    userID = e.UserID;
                    // lineItem.BatchNumber = e.BatchNumber;
                    lineItem.ESN = e.ESN;
                    lineItem.ICCID = e.ICC_ID;
                    lineItem.ItemCode = e.SKU;
                    lineItem.LineNo = (int)e.Line_no;
                    lineItem.Quantity = e.Qty;
                    lineItem.ContainerID = e.ContainerID;

                    lineItems.Add(lineItem);
                }

                shipment.LineItems = lineItems;
                // shipments.Add(shipment);
            }


            return shipment;
        }

        public List<PurchaseOrderShipmentDB> GetPurchaseOrderToBeSent(int companyID, string fromDate, string toDate, string poNum, bool ShipmentSentOnly, string TrackingNumber)
        {
            //List<FulfillmentTracking> shipments = null;
            List<PurchaseOrderShipmentDB> shipmentDB = null;
            System.Text.StringBuilder sbError = new System.Text.StringBuilder();
            // List<PurchaseOrderShipmentDB> shipments = null;
            int recordCount = 0, errornumber = 0;
            // string errorMessage = string.Empty;

            string errorMessage = null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
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


                    //arrSpFieldSeq = new string[] { "@piStatusID" };
                    arrSpFieldSeq = new string[] { "@piStatusID", "@piCompanyID", "@piPO_Num", "@piFromDate", "@piToDate", "@ShipmentSentOnly", "@TrackingNumber" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_TrackingToBeSent_CronJob", arrSpFieldSeq, "@poRowCount", "@poErrorNumber", "@poErrorMessage", out recordCount, out errornumber, out errorMessage);

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
                    Logger.LogMessage(ex, this);
                }
                finally
                {
                    //  db = null;
                    if (sbError.Length > 0)
                        errorMessage = string.Concat(errorMessage, sbError.ToString());
                }

            }
            return shipmentDB;
        }
        
        private List<PurchaseOrderShipmentDB> PopulateShipmentDB(DataTable dt)
        {
            List<PurchaseOrderShipmentDB> shipments = default;// null;// new List<PurchaseOrderShipmentDB>();
            PurchaseOrderShipmentDB shipment = default;//null;
            if (dt != null && dt.Rows.Count > 0)
            {
                shipments = new List<PurchaseOrderShipmentDB>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    shipment = new PurchaseOrderShipmentDB();
                    shipment.APIAddress = clsGeneral.getColumnData(dataRow, "APIAddress", string.Empty, false) as string;
                    shipment.BatchNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
                    shipment.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    shipment.ICC_ID = clsGeneral.getColumnData(dataRow, "ICC_ID", string.Empty, false) as string;
                    shipment.Line_no = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Line_no", 0, false));
                    shipment.PO_Date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", string.Empty, false)).ToShortDateString();
                    shipment.PO_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Po_ID", 0, false));
                    shipment.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));
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

        public  string AssignAcknowledgement(int po_id, string trackingNumber)
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
                        errorMessage = clsGeneral.ResponseErrorCode.DataNotUpdated.ToString();

                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
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

        private  List<FulfillmentDetailModel> PopulateFulfillmentDetail(DataTable dt, out string ShipmentDate)
        {
            ShipmentDate = "";
            List<FulfillmentDetailModel> poDetails = default;// null;// new List<PurchaseOrderShipmentDB>();
            FulfillmentDetailModel poDetail = default;// null;
            if (dt != null && dt.Rows.Count > 0)
            {
                poDetails = new List<FulfillmentDetailModel>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    poDetail = new FulfillmentDetailModel();

                    poDetail.BlankColumn = "";
                    ShipmentDate = clsGeneral.getColumnData(dataRow, "ShipmentDate", string.Empty, false) as string;
                    poDetail.MeidDec = clsGeneral.getColumnData(dataRow, "MeidDec", string.Empty, false) as string;
                    poDetail.MeidIMEI = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    poDetail.MeidHex = clsGeneral.getColumnData(dataRow, "MeidHex", string.Empty, false) as string;
                    poDetail.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    poDetail.ModelNumber = clsGeneral.getColumnData(dataRow, "ModelNumber", string.Empty, false) as string;
                    poDetail.Status = clsGeneral.getColumnData(dataRow, "STATUS", string.Empty, false) as string;
                    poDetail.PalletManDate = clsGeneral.getColumnData(dataRow, "PALLETMANDATE", string.Empty, false) as string;
                    poDetail.PalletID = clsGeneral.getColumnData(dataRow, "PalletID", string.Empty, false) as string;
                    poDetail.CartonID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    poDetail.CartonManDate = clsGeneral.getColumnData(dataRow, "CARTONMANDATE", string.Empty, false) as string;
                    poDetail.SerialNumber = clsGeneral.getColumnData(dataRow, "SerialNumber", string.Empty, false) as string;
                    poDetail.Destination = clsGeneral.getColumnData(dataRow, "Destination", string.Empty, false) as string;
                    poDetails.Add(poDetail);

                }
            }

            return poDetails;
        }

        private  List<FulfillmentEsn> PopulateFulfillmentESNs(DataTable dt)
        {
            
            List<FulfillmentEsn> esnList = default;//null;// new List<PurchaseOrderShipmentDB>();
            FulfillmentEsn poDetail = default;//null;
            if (dt != null && dt.Rows.Count > 0)
            {
                esnList = new List<FulfillmentEsn>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    poDetail = new FulfillmentEsn();

                    poDetail.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    esnList.Add(poDetail);

                }
            }

            return esnList;
        }
        private  List<FulfillmentComment> PopulateFulfillmentComments(DataTable dataTable)
        {
            List<FulfillmentComment> commentsList = default;//new List<FulfillmentComment>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                commentsList = new List<FulfillmentComment>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    FulfillmentComment objComments = new FulfillmentComment();
                    objComments.Comments = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                    objComments.CommentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CommentDate", 0, false));

                    commentsList.Add(objComments);
                }
            }
            return commentsList;

        }

    }
}
