using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Authenticate;

namespace SV.Framework.Models.Service
{
    public class PurchaseOrderShipmentRequest
    {
        private UserCredentials userCredentials;
        private DateTime dateFrom;
        private DateTime dateTo;
        private string _purchaseOrderNumber;

        public PurchaseOrderShipmentRequest()
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

        public string PurchaseOrderNumber
        {
            get
            {
                return _purchaseOrderNumber;
            }
            set
            {
                _purchaseOrderNumber = value;
            }
        }
        public DateTime DateFrom
        {
            get
            {
                return dateFrom;
            }
            set
            {
                dateFrom = value;
            }
        }
        public DateTime DateTo
        {
            get
            {
                return dateTo;
            }
            set
            {
                dateTo = value;
            }
        }

    }

}
