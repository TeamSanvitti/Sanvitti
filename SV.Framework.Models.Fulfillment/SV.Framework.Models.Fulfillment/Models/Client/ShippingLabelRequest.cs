using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class ShippingLabelRequest
    {

        private clsAuthentication _auth;
        private ShippingLabelInfo shippingLabelInfo;

        public ShippingLabelRequest()
        {

            _auth = new clsAuthentication();
            shippingLabelInfo = new ShippingLabelInfo();
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

        //[XmlElement(ElementName = "Authentication", IsNullable = true)]
        public ShippingLabelInfo ShippingLabelDetail
        {
            get
            {
                return shippingLabelInfo;
            }
            set
            {
                shippingLabelInfo = value;
            }
        }
    }
}
