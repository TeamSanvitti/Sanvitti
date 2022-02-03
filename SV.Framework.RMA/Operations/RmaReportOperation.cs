using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.RMA;
using SV.Framework.Models.Common;

namespace SV.Framework.RMA
{
    public class RmaReportOperation : BaseCreateInstance
    {
        public List<RmaInfo> GetRMAReport(int companyID, string fromDate, string toDate, int rmaStatusID, int esnStatusID, int triageStatusID, int receiveStatusID)
        {
            SV.Framework.DAL.RMA.RmaReportOperation rmaReportOperation = SV.Framework.DAL.RMA.RmaReportOperation.CreateInstance<SV.Framework.DAL.RMA.RmaReportOperation>();
            List<RmaInfo> rmaList = rmaReportOperation.GetRMAReport(companyID, fromDate, toDate, rmaStatusID, esnStatusID, triageStatusID, receiveStatusID);// new List<RmaInfo>();
            return rmaList;
        }
        public List<RMAEsnDetail> GetRmaEsnOnlyReport(int companyID, string RMANumber, string ESN, DateTime fromDate, DateTime toDate, int esnStatusID, int rmaStatusID)
        {
            SV.Framework.DAL.RMA.RmaReportOperation rmaReportOperation = SV.Framework.DAL.RMA.RmaReportOperation.CreateInstance<SV.Framework.DAL.RMA.RmaReportOperation>();
            return rmaReportOperation.GetRmaEsnOnlyReport(companyID, RMANumber, ESN, fromDate, toDate, esnStatusID, rmaStatusID);
        }
        public List<CustomerRmaStatus> GetCustomerRmaStatusSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            SV.Framework.DAL.RMA.RmaReportOperation rmaReportOperation = SV.Framework.DAL.RMA.RmaReportOperation.CreateInstance<SV.Framework.DAL.RMA.RmaReportOperation>();
            return rmaReportOperation.GetCustomerRmaStatusSummary(companyID, fromDate, toDate, userID, summaryBy);
        }
        public List<CustomerRmaEsnStatus> GetCustomerRmaESNStatusSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            SV.Framework.DAL.RMA.RmaReportOperation rmaReportOperation = SV.Framework.DAL.RMA.RmaReportOperation.CreateInstance<SV.Framework.DAL.RMA.RmaReportOperation>();
            return rmaReportOperation.GetCustomerRmaESNStatusSummary(companyID, fromDate, toDate, userID, summaryBy);
        }
        public List<CustomerRmaTriageStatus> GetCustomerRmaTriageStatusSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            SV.Framework.DAL.RMA.RmaReportOperation rmaReportOperation = SV.Framework.DAL.RMA.RmaReportOperation.CreateInstance<SV.Framework.DAL.RMA.RmaReportOperation>();
            return rmaReportOperation.GetCustomerRmaTriageStatusSummary(companyID, fromDate, toDate, userID, summaryBy);
        }

        public List<CustomerRmaDisposition> GetCustomerRmaDispositionSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            SV.Framework.DAL.RMA.RmaReportOperation rmaReportOperation = SV.Framework.DAL.RMA.RmaReportOperation.CreateInstance<SV.Framework.DAL.RMA.RmaReportOperation>();
            return rmaReportOperation.GetCustomerRmaDispositionSummary(companyID, fromDate, toDate, userID, summaryBy);
        }
        public List<CustomerRmaShipmentPaidBy> GetCustomerRmaShipmentPaidBySummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            SV.Framework.DAL.RMA.RmaReportOperation rmaReportOperation = SV.Framework.DAL.RMA.RmaReportOperation.CreateInstance<SV.Framework.DAL.RMA.RmaReportOperation>();
            return rmaReportOperation.GetCustomerRmaShipmentPaidBySummary(companyID, fromDate, toDate, userID, summaryBy);
        }
        public List<CustomerRmaReason> GetCustomerRmaReasonSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            SV.Framework.DAL.RMA.RmaReportOperation rmaReportOperation = SV.Framework.DAL.RMA.RmaReportOperation.CreateInstance<SV.Framework.DAL.RMA.RmaReportOperation>();
            return rmaReportOperation.GetCustomerRmaReasonSummary(companyID, fromDate, toDate, userID, summaryBy);
        }



    }
}
