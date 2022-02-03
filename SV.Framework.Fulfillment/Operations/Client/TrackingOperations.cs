using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class TrackingOperations : BaseCreateInstance
    {
        
        public  List<TrackingDetail> FulfillmentTrackingDelete(int lineNumber, int poID, string poSource, int userID, out int returnValue)
        {
            SV.Framework.DAL.Fulfillment.TrackingOperations trackingOperations = SV.Framework.DAL.Fulfillment.TrackingOperations.CreateInstance<DAL.Fulfillment.TrackingOperations>();

            returnValue = 0;
            List<TrackingDetail> poList = trackingOperations.FulfillmentTrackingDelete(lineNumber, poID, poSource, userID, out returnValue);
                        
            return poList;
        }
        public  List<TrackingDetail> FulfillmentTrackingUpdate(List<TrackingDetail> trackingInfoList, int poID, string poSource, PurchaseOrderStatus poStatus, int userID, out int returnValue, out string returnMessage)
        {
            SV.Framework.DAL.Fulfillment.TrackingOperations trackingOperations = SV.Framework.DAL.Fulfillment.TrackingOperations.CreateInstance<DAL.Fulfillment.TrackingOperations>();

            returnValue = 0;
            returnMessage = default;
            List<TrackingDetail> poList = trackingOperations.FulfillmentTrackingUpdate(trackingInfoList, poID, poSource, poStatus, userID, out returnValue, out returnMessage);

            return poList;
        }
    }
}
