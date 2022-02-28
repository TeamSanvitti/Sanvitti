using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Fulfillment;
using System.Xml.Serialization;

namespace SV.Framework.Models.Service
{
    public class PurchaseOrderInfoRequest
    {
        private string _purchaseOrderNumber;
        private string esn;
        private clsAuthentication _auth;
        [XmlIgnore]
        public int UserId { get; set; }
        public PurchaseOrderInfoRequest()
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
        public string ESN
        {
            get
            {
                return esn;
            }
            set
            {
                esn = value;
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
