using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class FulfillmentStatus
    {
        private PurchaseOrderStatus fulfillmentStatus;
        private int statusCount;

        public PurchaseOrderStatus FulfillmentOrderStatus
        {
            get { return fulfillmentStatus; }
            set { fulfillmentStatus = value; }
        }

        public int StatusCount
        {
            get { return statusCount; }
            set { statusCount = value; }
        }

    }
}