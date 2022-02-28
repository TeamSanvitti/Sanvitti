using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Authenticate;

namespace SV.Framework.Models.Service
{

    public class PurchaseOrderShippingRequest
    {
        private UserCredentials userCredentials;
        public PurchaseOrderShippingRequest()
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
}
