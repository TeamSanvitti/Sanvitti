using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Fulfillment;

namespace SV.Framework.Models.Service
{
    public class PurchaseOrderCancelRequest
    {
        private string _purchaseOrderNumber;
        //private string esn;
        private clsAuthentication _auth;

        public PurchaseOrderCancelRequest()
        {
            _auth = new clsAuthentication();
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
