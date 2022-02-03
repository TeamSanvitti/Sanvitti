using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class LogOperations : BaseCreateInstance
    {
        SV.Framework.DAL.Fulfillment.LogOperations logOperations = SV.Framework.DAL.Fulfillment.LogOperations.CreateInstance<DAL.Fulfillment.LogOperations>();

        public void FulfillmentLogInsert(FulfillmentLogModel request)
        {
            SV.Framework.DAL.Fulfillment.LogOperations logOperations = SV.Framework.DAL.Fulfillment.LogOperations.CreateInstance<DAL.Fulfillment.LogOperations>();

            logOperations.FulfillmentLogInsert(request);
        }
        public  void FulfillmentLogInsert(ContainerLogModel request)
        {
            SV.Framework.DAL.Fulfillment.LogOperations logOperations = SV.Framework.DAL.Fulfillment.LogOperations.CreateInstance<DAL.Fulfillment.LogOperations>();

            logOperations.FulfillmentLogInsert(request);
           
        }
        public void ApiLogInsert(LogModel request)
        {
            SV.Framework.DAL.Fulfillment.LogOperations logOperations = SV.Framework.DAL.Fulfillment.LogOperations.CreateInstance<DAL.Fulfillment.LogOperations>();

            logOperations.ApiLogInsert(request);
            
        }
        public  List<LogModel> GetAPILogReport(string fromDate, string toDate, string requestData, string responseData, string modelName, string status)
        {
            SV.Framework.DAL.Fulfillment.LogOperations logOperations = SV.Framework.DAL.Fulfillment.LogOperations.CreateInstance<DAL.Fulfillment.LogOperations>();

            List<LogModel> logList = logOperations.GetAPILogReport(fromDate, toDate, requestData, responseData, modelName, status);
            
            return logList;
        }

        public  List<FulfillmentLogInfo> GetFulfillmentLog(int poid, int Pages)
        {
            SV.Framework.DAL.Fulfillment.LogOperations logOperations = SV.Framework.DAL.Fulfillment.LogOperations.CreateInstance<DAL.Fulfillment.LogOperations>();

            List<FulfillmentLogInfo> logList = logOperations.GetFulfillmentLog(poid, Pages);
            
            return logList;
        }

    }

}
