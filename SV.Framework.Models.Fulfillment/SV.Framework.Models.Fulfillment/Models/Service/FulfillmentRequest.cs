using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Authenticate;
//using SV.Framework.Models.Fulfillment;

namespace SV.Framework.Models.Service
{

    public class FulfillmentRequest
    {
        private UserCredentials userCredentials;
        private SV.Framework.Models.Fulfillment.Fulfillment fulfillmentOrder;
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
            fulfillmentOrder = new SV.Framework.Models.Fulfillment.Fulfillment();
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
        public SV.Framework.Models.Fulfillment.Fulfillment FulfillmentOrder
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
}
