using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.RMA;
using SV.Framework.Models.Common;
using System.Data;
using System.Collections;
using SV.Framework.Models.Fulfillment;

namespace SV.Framework.RMA
{
    public class RMATrackingOperation : BaseCreateInstance
    {
        public ShippingLabelResponse ShippingLabelUpdate(RMATrackning request, int userId)
        {
            SV.Framework.DAL.RMA.RMATrackingOperation rmaTrackingOperation = SV.Framework.DAL.RMA.RMATrackingOperation.CreateInstance<SV.Framework.DAL.RMA.RMATrackingOperation>();

            ShippingLabelResponse serviceResponse = rmaTrackingOperation.ShippingLabelUpdate(request, userId);
            return serviceResponse;

        }

        public string GetLabelBase64(int TrackingId, out string shipMethod, out string ShipPackage, out bool IsManualTracking)
        {
            SV.Framework.DAL.RMA.RMATrackingOperation rmaTrackingOperation = SV.Framework.DAL.RMA.RMATrackingOperation.CreateInstance<SV.Framework.DAL.RMA.RMATrackingOperation>();

            string labelBase64 = string.Empty;
            shipMethod = string.Empty;
            ShipPackage = string.Empty;
            IsManualTracking = false;
            labelBase64 = rmaTrackingOperation.GetLabelBase64(TrackingId, out shipMethod, out ShipPackage, out IsManualTracking);
            return labelBase64;
        }        
    }
}
