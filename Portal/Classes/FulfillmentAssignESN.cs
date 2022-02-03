using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
{

    [Serializable]
    [XmlRootAttribute("FulfillmentAssign", Namespace = "", IsNullable = false)]
    public class FulfillmentAssignESN
    {
        private string fulfillmentNumber;
        private string customerAccountNumber;
        private string sku;
        private string esn;
        private string fmupc;
        private string msl;
        private string otksl;
        private string akey;
        private string lteICCID;
        private string lteIMSI;
        private string batchNumber;
        private string trackingNumber;
        private int? itemCompanyGUID;
        public FulfillmentAssignESN()
        { }


        [XmlElement(ElementName = "TrackingNumber", IsNullable = true)]
        public string TrackingNumber
        {
            get
            {
                if (string.IsNullOrEmpty(trackingNumber))
                    return string.Empty;
                else
                    return trackingNumber;
            }
            set
            {
                trackingNumber = value;
            }
        }
        [XmlIgnore]
        public bool IsSim { get; set; }

        [XmlIgnore]
        public string ErrorMessage { get; set; }
        [XmlIgnore]
        public string ProductName { get; set; }
        [XmlIgnore]
        public string CategoryName { get; set; }

        [XmlIgnore]
        public string BatchNumber
        {
            get
            {
                return batchNumber;
            }
            set
            {
                batchNumber = value;
            }
        }
        //[XmlElement(ElementName = "SKU", IsNullable = true)]
        [XmlIgnore]
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
        [XmlElement(ElementName = "ItemCompanyGUID", IsNullable = true)]
        //[XmlIgnore]
        public int? ItemCompanyGUID
        {
            get
            {
                return itemCompanyGUID;
            }
            set
            {
                itemCompanyGUID = value;
            }
        }
        [XmlElement(ElementName = "ESN", IsNullable = true)]
        public string ESN
        {
            get
            {
                if (string.IsNullOrEmpty(esn))
                    return string.Empty;
                else
                    return esn;
            }
            set
            {
                esn = value;
            }
        }
        [XmlIgnore]
        [XmlElement(ElementName = "FulfillmentNumber", IsNullable = true)]
        public string FulfillmentNumber
        {
            get
            {
                if (string.IsNullOrEmpty(fulfillmentNumber))
                    return string.Empty;
                else
                    return fulfillmentNumber;
            }
            set
            {
                fulfillmentNumber = value;
            }
        }
        //[XmlElement(ElementName = "CustomerAccountNumber", IsNullable = true)]
        [XmlIgnore]
        public string CustomerAccountNumber
        {
            get
            {
                return customerAccountNumber;
            }
            set
            {
                customerAccountNumber = value;
            }
        }

        [XmlElement(ElementName = "ICCID", IsNullable = true)]
        //[XmlIgnore]
        public string LteICCID
        {
            get
            {
                return lteICCID;
            }
            set
            {
                lteICCID = value;
            }
        }
        //[XmlElement(ElementName = "LteIMSI", IsNullable = true)]
        [XmlIgnore]
        public string LteIMSI
        {
            get
            {
                return lteIMSI;
            }
            set
            {
                lteIMSI = value;
            }
        }
        //[XmlElement(ElementName = "OTKSL", IsNullable = true)]
        [XmlIgnore]
        public string OTKSL
        {
            get
            {
                return otksl;
            }
            set
            {
                otksl = value;
            }
        }

        //[XmlElement(ElementName = "AKEY", IsNullable = true)]
        [XmlIgnore]
        public string AKEY
        {
            get
            {
                return akey;
            }
            set
            {
                akey = value;
            }
        }
        //[XmlElement(ElementName = "FMUPC", IsNullable = true)]
        [XmlIgnore]
        public string FMUPC
        {
            get
            {
                return fmupc;
            }
            set
            {
                fmupc = value;
            }
        }

        //[XmlElement(ElementName = "MSL", IsNullable = true)]
        [XmlIgnore]
        public string MSL
        {
            get
            {
                return msl;
            }
            set
            {
                msl = value;
            }
        }
    }

    public class POTracking
    {
        public string TrackingNumber { get; set; }
    }
        [Serializable]
    [XmlRootAttribute("FulfillmentAssignNonESN", Namespace = "", IsNullable = false)]
    public class FulfillmentAssignNonESN
    {

        private string sku;
        private int? itemCompanyGUID;
        private int? pod_id;
        private int? qty;
        private int currentStock;
        private bool isAssign;
        [XmlIgnore]
        public string ErrorMessage { get; set; }
        [XmlIgnore]
        public string ProductName { get; set; }
        [XmlIgnore]
        public string CategoryName { get; set; }
        [XmlIgnore]
        public string SKU
        {
            get { return sku; }
            set { sku = value; }
        }
        [XmlElement(ElementName = "ItemCompanyGUID", IsNullable = true)]
        public int? ItemCompanyGUID
        {
            get { return itemCompanyGUID; }
            set { itemCompanyGUID = value; }
        }
        [XmlElement(ElementName = "POD_ID", IsNullable = true)]
        public int? POD_ID
        {
            get { return pod_id; }
            set { pod_id = value; }
        }
        [XmlElement(ElementName = "Qty", IsNullable = true)]
        public int? Qty
        {
            get { return qty; }
            set { qty = value; }
        }
        [XmlIgnore]
        public int CurrentStock
        {
            get { return currentStock; }
            set { currentStock = value; }
        }
        [XmlIgnore]
        public bool IsAssign
        {
            get { return isAssign; }
            set { isAssign = value; }
        }

    }
}
