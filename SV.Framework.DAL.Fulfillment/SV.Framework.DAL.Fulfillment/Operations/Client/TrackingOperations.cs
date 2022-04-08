using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class TrackingOperations : BaseCreateInstance
    {
        public  List<TrackingDetail> FulfillmentTrackingDelete(int lineNumber, int poID, string poSource, int userID, out int returnValue)
        {
            returnValue = 0;
            FulfillmentLogModel logModel = new FulfillmentLogModel();
            string response = default;//"";

            logModel.ActionName = "Label Delete";
            logModel.CreateUserID = userID;
            logModel.StatusID = 0;
            logModel.PO_ID = poID;
            logModel.FulfillmentNumber = string.Empty;
            logModel.Comment = string.Empty;
            //string poXML = BaseAerovoice.SerializeObject<BasePurchaseOrder>(purchaseOrder);
            // poXML = "<PurchaseOrderResponse>" + poXML.Substring(poXML.IndexOf("<PurchaseOrderNumber>"));

            logModel.RequestData = "POID: " + poID.ToString() + ", TrackingID: " + lineNumber.ToString();

            //returnMessage = string.Empty;
            List<TrackingDetail> poList = new List<TrackingDetail>();

            //string trackingXML = clsGeneral.SerializeObject(trackingInfoList);


            using (DBConnect db = new DBConnect())
            {
                DataTable dt = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", poID);
                    objCompHash.Add("@LineNumber", lineNumber);
                    objCompHash.Add("@UserID", userID);
                    objCompHash.Add("@PoSource", poSource);

                    arrSpFieldSeq = new string[] { "@POID", "@LineNumber", "@UserID", "@PoSource" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_Tracking_Delete", arrSpFieldSeq, "@poRecordCount", out returnValue);
                    poList = PopulateTrackingList(dt);

                    if (returnValue == 1)
                    {
                        response = "Fulfillment Order Label can not be deleted";
                    }
                    else
                    {
                        response = "Fulfillment Order Label is deleted successfully";
                    }
                }

                catch (Exception exp)
                {
                    logModel.Comment = exp.Message;
                    response = exp.Message;
                    Logger.LogMessage(exp, this); //  throw exp;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;

                    logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);

                    SV.Framework.DAL.Fulfillment.LogOperations logOperations = new LogOperations();
                    logOperations.FulfillmentLogInsert(logModel);
                    //SV.Framework.DAL.Fulfillment.LogOperations.FulfillmentLogInsert(logModel);

                }
            }
            return poList;
        }
        public  List<TrackingDetail> FulfillmentTrackingUpdate(List<TrackingDetail> trackingInfoList, int poID, string poSource, PurchaseOrderStatus poStatus, int userID, out int returnValue, out string returnMessage)
        {
            returnValue = 0;
            returnMessage = default;//string.Empty;
            List<TrackingDetail> poList = default;//new List<TrackingDetail>();
            using (DBConnect db = new DBConnect())
            {
                string trackingXML = clsGeneral.SerializeObject(trackingInfoList);

                DataTable dt = default;//new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@piTrackingXML", trackingXML);
                    objCompHash.Add("@piPOID", poID);
                    objCompHash.Add("@piUserID", userID);
                    objCompHash.Add("@piStatusID", Convert.ToInt32(poStatus));
                    objCompHash.Add("@piPoSource", poSource);

                    arrSpFieldSeq = new string[] { "@piTrackingXML", "@piPOID", "@piUserID", "@piStatusID", "@piPoSource" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_Tracking_WEB_Update", arrSpFieldSeq, "@poRecordCount", "@poErrorMessage", out returnValue, out returnMessage);
                    poList = PopulateTrackingList(dt);
                }

                catch (Exception exp)
                {
                    Logger.LogMessage(exp, this); //  throw exp;
                }
            }
            return poList;
        }



        private  List<TrackingDetail> PopulateTrackingList(DataTable dt)
        {
            List<TrackingDetail> trackingList = default;//new List<TrackingDetail>();

            TrackingDetail tracking = default;//null;
            if (dt != null && dt.Rows.Count > 0)
            {
                trackingList = new List<TrackingDetail>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    tracking = new TrackingDetail();
                    tracking.TrackingNumber = clsGeneral.getColumnData(dataRow, "Trackingnumber", string.Empty, false) as string;
                    tracking.ShipByCode = clsGeneral.getColumnData(dataRow, "ShipByCode", string.Empty, false) as string;
                    tracking.ShipByID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShipByID", 0, false));
                    tracking.LineNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LineNumber", 0, false));
                    tracking.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipDate", DateTime.MinValue, false));

                    tracking.Comments = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                    tracking.ReturnValue = clsGeneral.getColumnData(dataRow, "ReturnLabel", string.Empty, false) as string;
                    tracking.EsnCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "EsnCount", 0, false));
                    trackingList.Add(tracking);
                }
            }
            return trackingList;
        }


    }
}
