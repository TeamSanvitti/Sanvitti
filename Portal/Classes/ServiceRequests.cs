using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    
    public class InventoryEsnRequest
    {
        private UserCredentials userCredentials;
        private string reason;

        private List<avii.Classes.EsnList> esnList;
        public InventoryEsnRequest()
        {
            userCredentials = new UserCredentials();
            esnList = new List<EsnList>();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }

        public List<avii.Classes.EsnList> EsnList
        {
            get 
            { 
                return esnList; 
            }
            set 
            {
                esnList = value;
            }
        }
        public string Reason
        {
            get
            {
                return reason;
            }
            set
            {
                reason = value;
            }
        }
    }

    public class RmaEsnListingRequest
    {

        private UserCredentials userCredentials;
        private DateTime fromDate= DateTime.MinValue;
        private DateTime toDate = DateTime.MinValue;
        private RMAStatus esnStatus = RMAStatus.None;
        private RMAStatus rmaStatus = RMAStatus.None;
        
        public RmaEsnListingRequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }


        public DateTime FromDate
        {
            get
            {
                return fromDate;
            }
            set
            {
                fromDate = value;
            }
        }
        public DateTime ToDate
        {
            get
            {
                return toDate;
            }
            set
            {
                toDate = value;
            }
        }
        public RMAStatus EsnStatus
        {
            get
            {
                return esnStatus;
            }
            set
            {
                esnStatus = value;
            }
        }
        public RMAStatus RmaStatus
        {
            get
            {
                return rmaStatus;
            }
            set
            {
                rmaStatus = value;
            }
        }

    }
    public class EsnRepositoryDetailRequest
    {

        private UserCredentials userCredentials;
        private DateTime fromDate = DateTime.MinValue;
        private DateTime toDate = DateTime.MinValue;

        private bool unusedOnly = false;
        private bool showAllUnusedEsn = false;
        public EsnRepositoryDetailRequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }

        public bool ShowAllUnusedEsn
        {
            get
            {
                return showAllUnusedEsn;
            }
            set
            {
                showAllUnusedEsn = value;
            }
        }
        public DateTime FromDate
        {
            get
            {
                return fromDate;
            }
            set
            {
                fromDate = value;
            }
        }
        public DateTime ToDate
        {
            get
            {
                return toDate;
            }
            set
            {
                toDate = value;
            }
        }

        public bool UnusedOnly
        {
            get
            {
                return unusedOnly;
            }
            set
            {
                unusedOnly = value;
            }
        }

    }

    public class BadESNRequest
    {

        private UserCredentials userCredentials;

        public BadESNRequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }
    }

    public class ReassignSKURequest
    {

        private UserCredentials userCredentials;
        private DateTime fromDate = DateTime.MinValue;
        private DateTime toDate = DateTime.MinValue;
        private string esn;
        private string sku;
        
        public ReassignSKURequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }


        public DateTime FromDate
        {
            get
            {
                return fromDate;
            }
            set
            {
                fromDate = value;
            }
        }
        public DateTime ToDate
        {
            get
            {
                return toDate;
            }
            set
            {
                toDate = value;
            }
        }
        public string ESN
        {
            get
            {
                return esn;
            }
            set
            {
                esn = value;
            }
        }
        public string SKU
        {
            get
            {
                return sku;
            }
            set
            {
                sku = value;
            }
        }

    }

    public class FulfillmentRequest
    {
        private UserCredentials userCredentials;
        private Fulfillment fulfillmentOrder;
        //private string fulfillmentNumber;
        //private string contactName;
        //private string contactPhone;
        //private string address1;
        //private string address2;
        //private string city;
        //private string state;
        //private string zip;
        //private string shipVia;
        //private List<FulfillmentItem> fulfillmentItems;

        public FulfillmentRequest()
        {
            userCredentials = new UserCredentials();
            fulfillmentOrder = new Fulfillment();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }
        public Fulfillment FulfillmentOrder
        {
            get
            {
                return fulfillmentOrder;
            }
            set
            {
                fulfillmentOrder = value;
            }
        }

        //public string FulfillmentNumber
        //{
        //    get
        //    {
        //        return fulfillmentNumber;
        //    }
        //    set
        //    {
        //        fulfillmentNumber = value;
        //    }
        //}
        //public string ContactName
        //{
        //    get
        //    {
        //        return contactName;
        //    }
        //    set
        //    {
        //        contactName = value;
        //    }
        //}
        //public string ContactPhone
        //{
        //    get
        //    {
        //        return contactPhone;
        //    }
        //    set
        //    {
        //        contactPhone = value;
        //    }
        //}
        //public string City
        //{
        //    get
        //    {
        //        return city;
        //    }
        //    set
        //    {
        //        city = value;
        //    }
        //}
        //public string State
        //{
        //    get
        //    {
        //        return state;
        //    }
        //    set
        //    {
        //        state = value;
        //    }
        //}
        //public string Zip
        //{
        //    get
        //    {
        //        return zip;
        //    }
        //    set
        //    {
        //        zip = value;
        //    }
        //}
        //public string ShipVia
        //{
        //    get
        //    {
        //        return shipVia;
        //    }
        //    set
        //    {
        //        shipVia = value;
        //    }
        //}
        //public string Address1
        //{
        //    get
        //    {
        //        return address1;
        //    }
        //    set
        //    {
        //        address1 = value;
        //    }
        //}
        //public string Address2
        //{
        //    get
        //    {
        //        return address2;
        //    }
        //    set
        //    {
        //        address2 = value;
        //    }
        //}




    }
    public class EsnInventoryRequest
    {

        private UserCredentials userCredentials;
        private DateTime fromDate = DateTime.MinValue;
        private DateTime toDate = DateTime.MinValue;
        private bool showAllUnusedEsn = false;
        
        public EsnInventoryRequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }


        public DateTime FromDate
        {
            get
            {
                return fromDate;
            }
            set
            {
                fromDate = value;
            }
        }
        public DateTime ToDate
        {
            get
            {
                return toDate;
            }
            set
            {
                toDate = value;
            }
        }
        public bool ShowAllUnusedEsn
        {
            get
            {
                return showAllUnusedEsn;
            }
            set
            {
                showAllUnusedEsn = value;
            }
        }
        
    }

    public class CustomerShipViaRequest
    {

        private UserCredentials userCredentials;
        private DateTime fromDate = DateTime.MinValue;
        private DateTime toDate = DateTime.MinValue;

        public CustomerShipViaRequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }


        public DateTime FromDate
        {
            get
            {
                return fromDate;
            }
            set
            {
                fromDate = value;
            }
        }
        public DateTime ToDate
        {
            get
            {
                return toDate;
            }
            set
            {
                toDate = value;
            }
        }

    }

    public class EsnRepositoryRequest
    {

        private UserCredentials userCredentials;
        private DateTime fromDate = DateTime.MinValue;
        private DateTime toDate = DateTime.MinValue;

        private bool unusedOnly = false;
        private bool showAllUnusedEsn = false;

        public EsnRepositoryRequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }


        public DateTime FromDate
        {
            get
            {
                return fromDate;
            }
            set
            {
                fromDate = value;
            }
        }
        public DateTime ToDate
        {
            get
            {
                return toDate;
            }
            set
            {
                toDate = value;
            }
        }

        public bool UnusedOnly
        {
            get
            {
                return unusedOnly;
            }
            set
            {
                unusedOnly = value;
            }
        }
        public bool ShowAllUnusedEsn
        {
            get
            {
                return showAllUnusedEsn;
            }
            set
            {
                showAllUnusedEsn = value;
            }
        }

    }
    public class FulfillmentTrackingRequest
    {

        private UserCredentials userCredentials;
        private string trackingNumber;
        private string fulfillmentNumber;
        private string shipViaCode;
        private DateTime shipmentfromDate = DateTime.MinValue;
        private DateTime shipmentToDate = DateTime.MinValue;

        public FulfillmentTrackingRequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
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
        public string FulfillmentNumber
        {
            get
            {
                return fulfillmentNumber;
            }
            set
            {
                fulfillmentNumber = value;
            }
        }
        public DateTime ShipmentFromDate
        {
            get
            {
                return shipmentfromDate;
            }
            set
            {
                shipmentfromDate = value;
            }
        }
        public DateTime ShipmentToDate
        {
            get
            {
                return shipmentToDate;
            }
            set
            {
                shipmentToDate = value;
            }
        }

        public string ShipViaCode
        {
            get
            {
                return shipViaCode;
            }
            set
            {
                shipViaCode = value;
            }
        }

        
    }

    
}