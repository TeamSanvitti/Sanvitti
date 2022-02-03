using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class FulfillmentReportOperation : BaseCreateInstance
    {
        public  List<FulfillmentBilling> GetFulfillmentBillingReport(int companyID, string DateFrom, string DateTo, string ShipFromDate, string ShipToDate, string FulfillmentNumber, string FulfillmentType, string ContactName)
        {
            SV.Framework.DAL.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.DAL.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.DAL.Fulfillment.FulfillmentReportOperation>();

            return fulfillmentReportOperation.GetFulfillmentBillingReport(companyID, DateFrom, DateTo, ShipFromDate, ShipToDate, FulfillmentNumber, FulfillmentType, ContactName);
            
        }
        
        public  List<ShipmentInfo> GetShipmentSummary(int companyID, string labelType, string shipmentDateFrom, string shipmentDateTo, string shipVia, string shipPackage)
        {
            SV.Framework.DAL.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.DAL.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.DAL.Fulfillment.FulfillmentReportOperation>();

            return fulfillmentReportOperation.GetShipmentSummary(companyID, labelType, shipmentDateFrom, shipmentDateTo, shipVia, shipPackage);

        }
        public List<UnusedLabel> GetUnusedLabels(int CompanyID, string FromDate, string ToDate, string TrackingNumber, bool IsCancel)
        {
            SV.Framework.DAL.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.DAL.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.DAL.Fulfillment.FulfillmentReportOperation>();
            return fulfillmentReportOperation.GetUnusedLabels(CompanyID, FromDate, ToDate, TrackingNumber, IsCancel);
        }
        public string UnusedLabelInsertUpdate(UnusedLabelInfo request)
        {
            SV.Framework.DAL.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.DAL.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.DAL.Fulfillment.FulfillmentReportOperation>();
            return fulfillmentReportOperation.UnusedLabelInsertUpdate(request);
        }
        public List<PurchaseOrderShipmentDB> GetPurchaseOrderShipmentReport(string PurchaseOrderNumber, DateTime DateFrom, DateTime DateTo,
            int companyId, out decimal totalShipPrice, out List<PurchaseOrderShipmentDB> shipmentDB)
        {
            totalShipPrice = 0;
            shipmentDB = default;
            SV.Framework.DAL.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.DAL.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.DAL.Fulfillment.FulfillmentReportOperation>();
            return fulfillmentReportOperation.GetPurchaseOrderShipmentReport(PurchaseOrderNumber, DateFrom, DateTo,companyId, out totalShipPrice, out shipmentDB);

        }
        public List<FulfillmentStatusReport> GetCustomerFulfillmentStatusReport(int companyID, int timeInterval, int userID)
        {
            SV.Framework.DAL.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.DAL.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.DAL.Fulfillment.FulfillmentReportOperation>();
            return fulfillmentReportOperation.GetCustomerFulfillmentStatusReport(companyID, timeInterval, userID);
        }
        public List<StoreFulfillmentStatus> GetCustomerStoreFulfillmentStatusReport(int companyID, string companyName, int timeInterval, int userID)
        {
            SV.Framework.DAL.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.DAL.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.DAL.Fulfillment.FulfillmentReportOperation>();
            return fulfillmentReportOperation.GetCustomerStoreFulfillmentStatusReport(companyID, companyName, timeInterval, userID);
        }
        public List<FulfillmentSKUStatus> GetCustomerFulfillmentSKUStatusReport(int companyID, int timeInterval, int userID)
        {
            SV.Framework.DAL.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.DAL.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.DAL.Fulfillment.FulfillmentReportOperation>();
            return fulfillmentReportOperation.GetCustomerFulfillmentSKUStatusReport(companyID, timeInterval, userID);
        }

        public List<PurchaseOrderShipping> GetPurchaseOrderShipmentAPI(SV.Framework.Models.Service.PurchaseOrderShipmentRequest request, int companyId)
        {
            SV.Framework.DAL.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.DAL.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.DAL.Fulfillment.FulfillmentReportOperation>();
            return fulfillmentReportOperation.GetPurchaseOrderShipmentAPI(request, companyId);
        }






    }
}
