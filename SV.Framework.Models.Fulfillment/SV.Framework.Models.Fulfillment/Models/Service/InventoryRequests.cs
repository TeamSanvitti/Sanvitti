using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Authenticate;

namespace SV.Framework.Models.Service
{
    public class InventoryRequest
    {

        private clsAuthentication _auth;

        public InventoryRequest()
        {
            _auth = new clsAuthentication();
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


    public class CurrentStockRequest
    {

        private UserCredentials userCredentials;
        private string sku;


        public CurrentStockRequest()
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

    public class RunningStockRequest
    {

        private UserCredentials userCredentials;
        private string sku;
        private DateTime from_date;
        private DateTime to_date;
        private bool includeDisabledSKU;

        public RunningStockRequest()
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
        public bool IncludeDisabledSKU
        {
            get
            {
                return includeDisabledSKU;
            }
            set
            {
                includeDisabledSKU = value;
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
        public DateTime DateFrom
        {
            get
            {
                return from_date;
            }
            set
            {
                from_date = value;
            }
        }

        public DateTime DateTo
        {
            get
            {
                return to_date;
            }
            set
            {
                to_date = value;
            }
        }


    }

}
