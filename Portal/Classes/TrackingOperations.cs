using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
using System.Text.RegularExpressions;

namespace avii.Classes
{
    public class TrackingOperations
    {
        public static List<TrackingDetail> FulfillmentTrackingDelete(int lineNumber, int poID, string poSource, int userID, out int returnValue)
        {
            returnValue = 0;
            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            string response = "";

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

            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
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

                if(returnValue == 1)
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
                throw exp;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;

                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                //SV.Framework.Fulfillment.LogOperations.FulfillmentLogInsert(logModel);

            }
            return poList;
        }

        public static List<TrackingDetail> FulfillmentTrackingUpdate(List<TrackingDetail> trackingInfoList, int poID, string poSource, PurchaseOrderStatus poStatus, int userID, out int returnValue, out string returnMessage)
        {
            returnValue = 0;
            returnMessage = string.Empty;
            List<TrackingDetail> poList = new List<TrackingDetail>();

            string trackingXML = clsGeneral.SerializeObject(trackingInfoList);

            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
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
                throw exp;
            }

            return poList;
        }
        private static string getHourOffset(Match m)
        {
            // Need to also account for Daylights Savings 
            // Time when calculating UTC offset value
            DateTime dtLocal = DateTime.Parse(m.Result("${date}"));
            DateTime dtUTC = dtLocal.ToUniversalTime();
            int hourLocalOffset = dtUTC.Hour - dtLocal.Hour;
            int hourServer = int.Parse(m.Result("${hour}"));
            string newHour = (hourServer + (hourLocalOffset -
                hourServer)).ToString("0#");
            string retString = m.Result("${date}" + "${time}" +
               newHour + "${last}");

            return retString;
        }
        private static readonly Regex DTCheck = new Regex(@"(\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{7})([\+|-]\d{2}:\d{2})");
        private static readonly Regex DTCheck2 = new Regex(@"(\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{6})([\+|-]\d{2}:\d{2})");

        public static List<Trackings> UpdateFulfillmentTracking(List<Trackings> trackingInfoList, string companyAccountNumber, string poSource, PurchaseOrderStatus poStatus, int userID, bool isDelete, string fileName, string comment, out int returnValue, out string returnMessage)
        {
            returnValue = 0;
            returnMessage = string.Empty;
            List<Trackings> poList = new List<Trackings>();

            string trackingXML = clsGeneral.SerializeObject(trackingInfoList);

            //trackingXML = "<ArrayOfTrackings><Trackings><ShipDate>2014-09-25T17:37:13.152923-07:00</ShipDate><FulfillmentNumber>ESNREASSIGN1</FulfillmentNumber><Tracking>shipdate1</Tracking><AvOrderNumber /><ShippingVia /><ShipmentType>S</ShipmentType><Comments /></Trackings></ArrayOfTrackings>";
            // Search for datetime values of the format 
           // --> 2004-08-22T00:00:00.0000000-05:00
           //string rp = @"(?<DATE>\d{4}-\d{2}-\d{2})(?<TIME>T\d{2}:\d{2}:\d{2}.\d{7}-)(?<HOUR>\d{2})(?<LAST>:\d{2})";
           // Replace UTC offset value
           //string fixedXML = Regex.Replace( trackingXML, rp, 
           //     new MatchEvaluator( getHourOffset ) );


            // remove time zone
            if (DTCheck.IsMatch(trackingXML))
            {
                trackingXML = DTCheck.Replace(trackingXML, "$1");
                //trackingXML = DTCheck.Replace(trackingXML, "$1");
            }
            if (DTCheck2.IsMatch(trackingXML))
            {
                trackingXML = DTCheck2.Replace(trackingXML, "$1");
                //trackingXML = DTCheck.Replace(trackingXML, "$1");
            }
            //trackingXML = DTCheck.Replace(trackingXML, "$1");
   

            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piTrackingXML", trackingXML);
                //objCompHash.Add("@piSalesOrderNumber", salesOrderNumber);
                //objCompHash.Add("@piShipByID", shipByID);
                objCompHash.Add("@piCompanyAccountNumber", companyAccountNumber);
                objCompHash.Add("@piUserID", userID);
                //objCompHash.Add("@piComments", Comments);
                objCompHash.Add("@piStatusID", Convert.ToInt32(poStatus));
                objCompHash.Add("@piPoSource", poSource);
                objCompHash.Add("@piIsDelete", isDelete);
                objCompHash.Add("@FileName", fileName);
                objCompHash.Add("@Comment", comment);


                arrSpFieldSeq = new string[] { "@piTrackingXML", "@piCompanyAccountNumber", "@piUserID", "@piStatusID", "@piPoSource", "@piIsDelete", "@FileName", "@Comment" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_Tracking_Update", arrSpFieldSeq, "@poRecordCount", "@poErrorMessage", out returnValue, out returnMessage);
                //poList = PopulateTrackingInfo(dt);
            }

            catch (Exception exp)
            {
                throw exp;
            }

            return poList;
        }
        //private static readonly Regex DTCheck = new Regex(@"(\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}.\d{7})([\+|-]\d{2}:\d{2})");

        public static void FulfillmentMultiTrackingInsert(FulfillmentMultiTracking trackingInfo, string poSource, int userID, bool isDelete, string fileName, string comment, out int returnValue, out string returnMessage)
        {
            returnValue = 0;
            returnMessage = string.Empty;
            List<MultiTracking> poList = new List<MultiTracking>();

            string trackingXML = clsGeneral.SerializeObject(trackingInfo.Trackings);
            // remove time zone
            if (DTCheck.IsMatch(trackingXML))
                trackingXML = DTCheck.Replace(trackingXML, "$1");

            if (DTCheck2.IsMatch(trackingXML))
            {
                trackingXML = DTCheck2.Replace(trackingXML, "$1");
                //trackingXML = DTCheck.Replace(trackingXML, "$1");
            }
            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piXMLData", trackingXML);
                objCompHash.Add("@CompanyAccountNumber", trackingInfo.CustomerAccountNumber);
                objCompHash.Add("@ShipViaCode", trackingInfo.ShipViaCode);
                objCompHash.Add("@ShipDate", trackingInfo.ShipDate);
                objCompHash.Add("@piUserID", userID);
                objCompHash.Add("@piPoSource", poSource);
                objCompHash.Add("@piIsDelete", isDelete);
                objCompHash.Add("@piFileName", fileName);
                objCompHash.Add("@piComment", trackingInfo.Comments);


                arrSpFieldSeq = new string[] { "@piXMLData", "@CompanyAccountNumber", "@ShipViaCode", "@ShipDate", "@piUserID", "@piPoSource", "@piIsDelete", "@piFileName", "@piComment" };
                dt = db.GetTableRecords(objCompHash, "Av_PurchaseOrder_MultiTracking_Insert", arrSpFieldSeq, "@poRecordCount", "@poErrorMessage", out returnValue, out returnMessage);
                //poList = PopulateTrackingList(dt);
            }

            catch (Exception exp)
            {
                throw exp;
            }

           // return poList;
        }


        public static List<MultiTracking> ValidateFulfillmentMultiTracking(FulfillmentMultiTracking trackingInfo, bool isDelete, out string poFulfillmentMessage, out string poTrackingExistsMessage, out string poEsnMessage, out string poEsnExistsMessage)
        {
            //returnValue = 0;
            poFulfillmentMessage = string.Empty;
            poTrackingExistsMessage = string.Empty;
            poEsnMessage = string.Empty;
            poEsnExistsMessage = string.Empty;
            List<MultiTracking> poList = new List<MultiTracking>();

            string trackingXML = clsGeneral.SerializeObject(trackingInfo.Trackings);

            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piXMLData", trackingXML);
                objCompHash.Add("@CompanyAccountNumber", trackingInfo.CustomerAccountNumber);
                objCompHash.Add("@piIsDelete", isDelete);

                arrSpFieldSeq = new string[] { "@piXMLData", "@CompanyAccountNumber", "@piIsDelete" };
                dt = db.GetTableRecords(objCompHash, "Av_PurchaseOrder_Tracking_Validation", arrSpFieldSeq, "@poFulfillmentMessage", "@poTrackingExistsMessage", "@poEsnMessage", "@poEsnExistsMessage", out poFulfillmentMessage, out poTrackingExistsMessage, out poEsnMessage, out poEsnExistsMessage);


                poList = PopulateMultiTracking(dt);
            }

            catch (Exception exp)
            {
                throw exp;
            }

            return poList;
        }



        private static List<Trackings> PopulateTrackingInfo(DataTable dt)
        {
            List<Trackings> trackingList = new List<Trackings>();

            Trackings tracking = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    tracking = new Trackings();
                    tracking.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PurchaseOrderNumber", string.Empty, false) as string;
                    tracking.Tracking = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    //tracking.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "linenumber", 0, false));


                    trackingList.Add(tracking);
                }
            }
            return trackingList;
        }
        private static List<TrackingDetail> PopulateTrackingList(DataTable dt)
        {
            List<TrackingDetail> trackingList = new List<TrackingDetail>();

            TrackingDetail tracking = null;
            if (dt != null && dt.Rows.Count > 0)
            {
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
        private static List<MultiTracking> PopulateMultiTracking(DataTable dt)
        {
            List<MultiTracking> trackingList = new List<MultiTracking>();

            MultiTracking tracking = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    tracking = new MultiTracking();
                    tracking.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "FulfillmentNumber", string.Empty, false) as string;
                    tracking.Tracking = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    //tracking.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    //tracking.ShipViaCode = clsGeneral.getColumnData(dataRow, "ShipViaCode", string.Empty, false) as string;

                    
                    //tracking.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "linenumber", 0, false));


                    trackingList.Add(tracking);
                }
            }
            return trackingList;
        }
        
        
    }
}