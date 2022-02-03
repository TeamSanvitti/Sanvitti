using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

namespace avii.Classes
{
    public class LogModel
    {
        public int CompanyID { get; set; }
        public int UserID { get; set; }
        //public string UserName { get; set; }
        public string ModuleName { get; set; }
        public DateTime RequestTimeStamp { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public DateTime ResponseTimeStamp { get; set; }
        public string ReturnMessage { get; set; }
        public bool ExceptionOccured { get; set; }
        public string ShortRequestData { get; set; }
        public string ShortResponseData { get; set; }

        public Int64 LogID { get; set; }
        public Int64 TimeDifference { get; set; }
    }
    public class LogOperations
    {
        public static void ApiLogInsert(LogModel request)
        {
            DBConnect db = new DBConnect();
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
                objCompHash.Add("@CompanyID", request.CompanyID);

                arrSpFieldSeq = new string[] { "@ModuleName", "@RequestTimeStamp", "@RequestData", "@ResponseData", "@ResponseTimeStamp", 
                    "@ReturnMessage", "@ExceptionOccured", "@UserID", "@CompanyID"};
                db.ExeCommand(objCompHash, "sv_APILogInsert", arrSpFieldSeq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }


        }

        public static List<LogModel> GetAPILogReport(string fromDate, string toDate, string requestData, string responseData, string modelName, string status, int companyID)
        {
            DBConnect db = new DBConnect();
            List<LogModel> logList = null;
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {
                //objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", string.IsNullOrWhiteSpace(fromDate) ? null : Convert.ToDateTime(fromDate).ToString("yyyy-MM-dd"));
                objParams.Add("@ToDate", string.IsNullOrWhiteSpace(toDate) ? null : Convert.ToDateTime(toDate).ToString("yyyy-MM-dd"));
                objParams.Add("@RequestData", requestData);
                objParams.Add("@ResponseData", responseData);
                objParams.Add("@ModuleName", modelName);
                objParams.Add("@Status", status);
                objParams.Add("@CompanyID", companyID);

                arrSpFieldSeq =
                new string[] { "@FromDate", "@ToDate", "@RequestData", "@ResponseData", "@ModuleName", "@Status", "@CompanyID" };


                dataTable = db.GetTableRecords(objParams, "av_APILog_Report", arrSpFieldSeq);
                logList = PopulateAPILog(dataTable);

            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return logList;
        }

        private static List<LogModel> PopulateAPILog(DataTable dataTable)
        {
            List<LogModel> logList = new List<LogModel>();
            LogModel log = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    log = new LogModel();
                   // log.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                    log.ModuleName = clsGeneral.getColumnData(dataRow, "ModuleName", string.Empty, false) as string;
                    log.RequestData = (clsGeneral.getColumnData(dataRow, "RequestData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;"); 
                    log.ResponseData = (clsGeneral.getColumnData(dataRow, "ResponseData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;");
                    log.ReturnMessage = clsGeneral.getColumnData(dataRow, "ReturnMessage", string.Empty, false) as string;
                    log.RequestTimeStamp = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RequestTimeStamp", DateTime.MinValue, false));
                    log.ResponseTimeStamp = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ResponseTimeStamp", DateTime.MinValue, false));
                    log.ExceptionOccured = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ExceptionOccured", 0, false));
                    log.ShortRequestData = (clsGeneral.getColumnData(dataRow, "ShortRequestData", string.Empty, false) as string).Replace("<","&lt;").Replace(">", "&gt;"); 
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