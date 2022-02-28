using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Authenticate;
using SV.Framework.Models.Fulfillment;

namespace SV.Framework.Models.Service
{
    public class SetPurchaseOrderShippingRequest
    {
        private List<PurchaseOrderShipment> shipments;

        private UserCredentials userCredentials;
        public SetPurchaseOrderShippingRequest()
        {
            userCredentials = new UserCredentials();
            shipments = new List<PurchaseOrderShipment>();
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
        public List<PurchaseOrderShipment> Shipments
        {
            get
            {
                return shipments;
            }
            set
            {
                shipments = value;
            }
        }

    }

}
