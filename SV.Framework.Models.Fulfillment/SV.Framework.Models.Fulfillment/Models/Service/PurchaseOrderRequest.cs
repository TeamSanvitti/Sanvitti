using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SV.Framework.Models.Fulfillment;

namespace SV.Framework.Models.Service
{
    public class PurchaseOrderRequest
    {
        private clsPurchaseOrder _purchaseOrder;
        private clsAuthentication _auth;

        public PurchaseOrderRequest()
        {
            _purchaseOrder = new clsPurchaseOrder();
            _auth = new clsAuthentication();
        }


        public clsPurchaseOrder PurchaseOrder
        {
            get
            {
                return _purchaseOrder;
            }
            set
            {
                _purchaseOrder = value;
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