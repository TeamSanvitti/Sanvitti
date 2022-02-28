using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Fulfillment;

namespace SV.Framework.Models.Service
{

    public class FulfillmentReportRequest
    {
        private string _fulfillmentNumber;
        private DateTime fromPODate = DateTime.MinValue;
        private DateTime toPODate = DateTime.MinValue;
        private DateTime fromShipDate = DateTime.MinValue;
        private DateTime toShipDate = DateTime.MinValue;
        private PurchaseOrderStatus _purchaseOrderStatus;
        private string trackingNumber;


        private clsAuthentication _auth;

        public FulfillmentReportRequest()
        {
            _auth = new clsAuthentication();

        }
        public string TrackingNumber
        {
            get
            {
                return trackingNumber;
            }
            set
            {
                trackingNumber = value;
            }
        }
        public PurchaseOrderStatus FulfillmentOrderStatus
        {
            get
            {
                return _purchaseOrderStatus;
            }
            set
            {
                _purchaseOrderStatus = value;
            }
        }
        public DateTime ShipFromDate
        {
            get
            {
                return fromShipDate;
            }
            set
            {
                fromShipDate = value;
            }
        }
        public DateTime ShipToDate
        {
            get
            {
                return toShipDate;
            }
            set
            {
                toShipDate = value;
            }
        }

        public DateTime FulfillmentFromDate
        {
            get
            {
                return fromPODate;
            }
            set
            {
                fromPODate = value;
            }
        }
        public DateTime FulfillmentToDate
        {
            get
            {
                return toPODate;
            }
            set
            {
                toPODate = value;
            }
        }
        public string FulfillmentNumber
        {
            get
            {
                return _fulfillmentNumber;
            }
            set
            {
                _fulfillmentNumber = value;
            }
        }

        //[XmlElement(ElementName = "Authentication", IsNullable = true)]
        public clsAuthentication Authentication
        {
            get
            {
                return _auth;
            }
            set
            {
                _auth = value;
            }
        }
    }
}
