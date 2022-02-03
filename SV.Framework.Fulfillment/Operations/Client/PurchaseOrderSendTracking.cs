using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class PurchaseOrderSendTracking: BaseCreateInstance
    {
        

        private const int COMPANY_ID = 464;
        //private const string API_URL = "http://partner.t.freedompop.com/langlobal/order/status";
        public List<PurchaseOrderShipmentDB> GetPurchaseOrderToBeSent(int companyID, string fromDate, string toDate, string poNum, bool ShipmentSentOnly, string TrackingNumber)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrderSendTracking purchaseOrderSendTracking = SV.Framework.DAL.Fulfillment.PurchaseOrderSendTracking.CreateInstance<DAL.Fulfillment.PurchaseOrderSendTracking>();
            List<PurchaseOrderShipmentDB> shipmentDB = purchaseOrderSendTracking.GetPurchaseOrderToBeSent(companyID, fromDate, toDate, poNum, ShipmentSentOnly, TrackingNumber);

            return shipmentDB;
        }
        public string AssignAcknowledgement(int po_id, string trackingNumber)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrderSendTracking purchaseOrderSendTracking = SV.Framework.DAL.Fulfillment.PurchaseOrderSendTracking.CreateInstance<DAL.Fulfillment.PurchaseOrderSendTracking>();
            string errorMessage = purchaseOrderSendTracking.AssignAcknowledgement(po_id, trackingNumber);
            
            return errorMessage;
        }

        public IntegrationModel GetShipmentTrackingAPIInfo(int companyID, string shipmentTracking)
        {
            SV.Framework.DAL.Fulfillment.PurchaseOrderSendTracking purchaseOrderSendTracking = SV.Framework.DAL.Fulfillment.PurchaseOrderSendTracking.CreateInstance<DAL.Fulfillment.PurchaseOrderSendTracking>();
            IntegrationModel model = purchaseOrderSendTracking.GetShipmentTrackingAPIInfo(companyID, shipmentTracking);            

            return model;
        }

    }
}
