using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Fulfillment;

namespace SV.Framework.Models.Service
{
    public class InventoryItemRequest
    {
        private clsAuthentication _auth;

        public InventoryItemRequest()
        {
            _auth = new clsAuthentication();
        }

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
