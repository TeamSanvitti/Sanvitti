using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class LogOperations : BaseCreateInstance
    {
        public  void FulfillmentLogInsert(FulfillmentLogModel request)
        {
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@PO_ID", request.PO_ID);
                    objCompHash.Add("@ActionName", request.ActionName);
                    objCompHash.Add("@RequestData", request.RequestData);
                    objCompHash.Add("@ResponseData", request.ResponseData);
                    objCompHash.Add("@PO_StatusID", request.StatusID);
                    objCompHash.Add("@CreateUserID", request.CreateUserID);
                    objCompHash.Add("@Comment", request.Comment);
                    objCompHash.Add("@FulfillmentNumber", request.FulfillmentNumber);


                    arrSpFieldSeq = new string[] { "@PO_ID", "@ActionName", "@RequestData", "@ResponseData", "@PO_StatusID", "@CreateUserID", "@Comment", "@FulfillmentNumber" };
                    db.ExeCommand(objCompHash, "av_FulfillmentServiceLogInsert", arrSpFieldSeq);
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //ex ex;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }


        }
        public  void FulfillmentLogInsert(ContainerLogModel request)
        {
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@PO_ID", request.PO_ID);
                    objCompHash.Add("@ActionName", request.ActionName);
                    objCompHash.Add("@RequestData", request.RequestData);
                    objCompHash.Add("@ResponseData", request.ResponseData);
                    objCompHash.Add("@PO_StatusID", request.StatusID);
                    objCompHash.Add("@CreateUserID", request.CreateUserID);
                    objCompHash.Add("@Comment", request.Comment);
                    objCompHash.Add("@FulfillmentNumber", request.FulfillmentNumber);


                    arrSpFieldSeq = new string[] { "@PO_ID", "@ActionName", "@RequestData", "@ResponseData", "@PO_StatusID", "@CreateUserID", "@Comment", "@FulfillmentNumber" };
                    db.ExeCommand(objCompHash, "av_FulfillmentServiceLogInsert", arrSpFieldSeq);
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }

        }
        public  void ApiLogInsert(LogModel request)
        {
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@ModuleName", request.ModuleName);
                    objCompHash.Add("@RequestTimeStamp", request.RequestTimeStamp);
                    objCompHash.Add("@RequestData", request.RequestData);
                    objCompHash.Add("@ResponseData", request.ResponseData);
                    objCompHash.Add("@ResponseTimeStamp", request.ResponseTimeStamp);
                    objCompHash.Add("@ReturnMessage", request.ReturnMessage);
                    objCompHash.Add("@ExceptionOccured", request.ExceptionOccured);
                    objCompHash.Add("@UserID", request.UserID);


                    arrSpFieldSeq = new string[] { "@ModuleName", "@RequestTimeStamp", "@RequestData", "@ResponseData", "@ResponseTimeStamp",
                    "@ReturnMessage", "@ExceptionOccured","@UserID" };
                    db.ExeCommand(objCompHash, "sv_APILogInsert", arrSpFieldSeq);
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }

        }
        public  List<LogModel> GetAPILogReport(string fromDate, string toDate, string requestData, string responseData, string modelName, string status)
        {
            List<LogModel> logList = default;//null;
            using (DBConnect db = new DBConnect())
            {

                string[] arrSpFieldSeq;

                Hashtable objParams = new Hashtable();

                DataTable dataTable = default;// new DataTable();

                try
                {

                    // objParams.Add("@CompanyID", companyID);
                    objParams.Add("@FromDate", string.IsNullOrWhiteSpace(fromDate) ? null : Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd"));
                    objParams.Add("@ToDate", string.IsNullOrWhiteSpace(toDate) ? null : Convert.ToDateTime(toDate).ToString("yyyy-MM-dd"));
                    objParams.Add("@RequestData", requestData);
                    objParams.Add("@ResponseData", responseData);
                    objParams.Add("@ModuleName", modelName);
                    objParams.Add("@Status", status);

                    arrSpFieldSeq =
                    new string[] { "@FromDate", "@ToDate", "@RequestData", "@ResponseData", "@ModuleName", "@Status" };


                    dataTable = db.GetTableRecords(objParams, "av_APILog_Report", arrSpFieldSeq);
                    logList = PopulateAPILog(dataTable);

                }

                catch (Exception ex)
                {

                    Logger.LogMessage(ex, this); // throw ex;

                }
                finally
                {

                  //  db.DBClose();
                    objParams = null;
                    arrSpFieldSeq = null;

                }
            }
            return logList;
        }

        public  List<FulfillmentLogInfo> GetFulfillmentLog(int poid, int Pages)
        {            
            List<FulfillmentLogInfo> logList = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objParams = new Hashtable();
                DataTable dataTable = default;// new DataTable();
                try
                {
                    objParams.Add("@PO_ID", poid);
                    objParams.Add("@Pages", Pages);

                    arrSpFieldSeq =
                    new string[] { "@PO_ID", "@Pages" };

                    dataTable = db.GetTableRecords(objParams, "av_FulfillmentLog_Select", arrSpFieldSeq);
                    logList = PopulatePOLog(dataTable);
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
            return logList;
        }
        private  List<FulfillmentLogInfo> PopulatePOLog(DataTable dataTable)
        {
            List<FulfillmentLogInfo> logList = default;//new List<FulfillmentLogInfo>();
            FulfillmentLogInfo log = default;//null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                logList = new List<FulfillmentLogInfo>();

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    log = new FulfillmentLogInfo();
                    log.ActionName = clsGeneral.getColumnData(dataRow, "ActionName", string.Empty, false) as string;
                    log.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                    log.Status = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    log.RequestData = (clsGeneral.getColumnData(dataRow, "RequestData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;");
                    log.ResponseData = (clsGeneral.getColumnData(dataRow, "ResponseData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;");
                    //log.ReturnMessage = clsGeneral.getColumnData(dataRow, "ReturnMessage", string.Empty, false) as string;
                    log.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", DateTime.MinValue, false));
                    //log.ResponseTimeStamp = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ResponseTimeStamp", DateTime.MinValue, false));
                    //log.ExceptionOccured = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ExceptionOccured", 0, false));
                    //log.ShortRequestData = (clsGeneral.getColumnData(dataRow, "ShortRequestData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;");
                    //log.ShortResponseData = (clsGeneral.getColumnData(dataRow, "ShortResponseData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;");
                     log.POLogID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "POLogID", 0, false));
                    //log.ExceptionOccured = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ExceptionOccured", false, false));
                    // log.TimeDifference = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "TimeDifference", 0, false));
                    logList.Add(log);


                }
            }
            return logList;
        }
        private  List<LogModel> PopulateAPILog(DataTable dataTable)
        {
            List<LogModel> logList = default;// new List<LogModel>();
            LogModel log = default;//null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                logList = new List<LogModel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    log = new LogModel();
                    log.ModuleName = clsGeneral.getColumnData(dataRow, "ModuleName", string.Empty, false) as string;
                    log.RequestData = (clsGeneral.getColumnData(dataRow, "RequestData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;");
                    log.ResponseData = (clsGeneral.getColumnData(dataRow, "ResponseData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;");
                    log.ReturnMessage = clsGeneral.getColumnData(dataRow, "ReturnMessage", string.Empty, false) as string;
                    log.RequestTimeStamp = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RequestTimeStamp", DateTime.MinValue, false));
                    log.ResponseTimeStamp = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ResponseTimeStamp", DateTime.MinValue, false));
                    log.ExceptionOccured = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ExceptionOccured", 0, false));
                    log.ShortRequestData = (clsGeneral.getColumnData(dataRow, "ShortRequestData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;");
                    log.ShortResponseData = (clsGeneral.getColumnData(dataRow, "ShortResponseData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;");
                    log.LogID = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "LogID", 0, false));
                    log.ExceptionOccured = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ExceptionOccured", false, false));
                    log.TimeDifference = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "TimeDifference", 0, false));
                    logList.Add(log);


                }
            }
            return logList;
        }

    }

}
