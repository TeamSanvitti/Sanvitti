using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    #region Enums
    public enum PurchaseOrderFlag
    {
        A,
        W,
        U
    }
    [Serializable]
    [Flags]
    public enum PurchaseOrderStatus
    {
        [XmlEnumAttribute(Name = "Pending")]
        Pending = 1,
        [XmlEnumAttribute(Name = "Processed")]
        Processed = 2,
        [XmlEnumAttribute(Name = "Shipped")]
        Shipped = 3,
        [XmlEnumAttribute(Name = "Closed")]
        Closed = 4,
        [XmlEnumAttribute(Name = "Return")]
        Return = 5,
        [XmlEnumAttribute(Name = "OnHold")]
        OnHold = 6,
        [XmlEnumAttribute(Name = "OutofStock")]
        OutofStock = 7,
        [XmlEnumAttribute(Name = "InProcess")]
        InProcess = 8,
        [XmlEnumAttribute(Name = "Cancelled")]
        Cancelled = 9,
        [XmlEnumAttribute(Name = "PartialProcessed")]
        PartialProcessed = 10,
        [XmlEnumAttribute(Name = "PartialShipped")]
        PartialShipped = 11

    }

    [Serializable]
    [Flags]
    public enum ShipmentType
    {
        [XmlEnumAttribute(Name = "Ship")]
        Ship = 'S',
        [XmlEnumAttribute(Name = "Return")]
        Return = 'R'
    }
    #endregion

    [XmlRoot(ElementName = "purchaseorder", IsNullable = true)]
    public class clsPurchaseOrder
    {
        #region Private Members
        private string _aerovoiceSaleOrderNumber;
        private string _purchaseOrderNumber;
        private string _customerOrderNumber;
        private int _purchaseOrderID;
        private DateTime _purchaseOrderDate;
        private DateTime _requestedShipDate;
        private string _customerNumber;
        private string _customerName;
        private string _poType;
        private string _storeID;
        private string _b2bStoreID;
        // private string _storeName;
        private bool? _b2b = false;
        private string _comments;
        private clsShipping _shipping;
        private string _shipThrough;
        private List<PurchaseOrderItem> _items;
        private PurchaseOrderStatus _purchaseOrderStatus;
        private ShipmentType shipmentType;

        private List<ReturnLabel> returnLabelItems;
        private List<FulfillmentComment> fulfillmentCommentList;
        #endregion

        #region Constructor

        public clsPurchaseOrder()
        {
            _shipping = new clsShipping();
            _items = new List<PurchaseOrderItem>();
            _purchaseOrderStatus = PurchaseOrderStatus.Pending;
            returnLabelItems = new List<ReturnLabel>();
            shipmentType = ShipmentType.Ship;
            fulfillmentCommentList = new List<FulfillmentComment>();
        }

        public clsPurchaseOrder(int purchaseOrderID)
        {
            _purchaseOrderID = purchaseOrderID;
            _purchaseOrderStatus = PurchaseOrderStatus.Pending;
            _shipping = new clsShipping();
            _items = new List<PurchaseOrderItem>();
            shipmentType = ShipmentType.Ship;
            returnLabelItems = new List<ReturnLabel>();
            fulfillmentCommentList = new List<FulfillmentComment>();
        }

        public clsPurchaseOrder(int purchaseOrderID, string aerovoiceSalesOrderNumber)
        {
            _purchaseOrderID = purchaseOrderID;
            _aerovoiceSaleOrderNumber = aerovoiceSalesOrderNumber;
            _purchaseOrderStatus = PurchaseOrderStatus.Pending;
            _shipping = new clsShipping();
            _items = new List<PurchaseOrderItem>();
            returnLabelItems = new List<ReturnLabel>();
            shipmentType = ShipmentType.Ship;
            fulfillmentCommentList = new List<FulfillmentComment>();
        }
        #endregion

        #region Properties


        [XmlIgnore]
        //[XmlElement(ElementName = "PurchaseOrderStatus", IsNullable = true)]

        [XmlElement(ElementName = "PurchaseOrderStatus")]
        public PurchaseOrderStatus PurchaseOrderStatus
        {
            get
            {
                return _purchaseOrderStatus;
            }
            set { _purchaseOrderStatus = value; }
        }
        [XmlIgnore]
        [XmlElement(ElementName = "salesordernumber", IsNullable = true)]
        public string AerovoiceSalesOrderNumber
        {
            get
            {
                return _aerovoiceSaleOrderNumber;
            }

        }

        [XmlElement(ElementName = "purchaseordernumber", IsNullable = true)]
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
        [XmlElement(ElementName = "customerordernumber", IsNullable = true)]
        public string CustomerOrderNumber
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_customerOrderNumber))
                    return "";
                else
                    return _customerOrderNumber;
            }
            set
            {
                _customerOrderNumber = value;
            }
        }
        [XmlIgnore]
        [XmlElement(ElementName = "purchaseorderID", IsNullable = true)]
        public int? PurchaseOrderID
        {
            get
            {
                return _purchaseOrderID;
            }
        }

        [XmlElement(ElementName = "purchaseorderdate", DataType = "date")]
        public DateTime PurchaseOrderDate
        {
            get
            {
                if ((_purchaseOrderDate != null) && (_purchaseOrderDate == DateTime.MinValue))
                {
                    return DateTime.Now;
                }
                else
                    return _purchaseOrderDate;
            }
            set
            {
                _purchaseOrderDate = value;
            }
        }
        [XmlElement(ElementName = "requestedshipdate", DataType = "date")]
        public DateTime RequestedShipDate
        {
            get
            {
                if ((_requestedShipDate == null) || (_requestedShipDate == DateTime.MinValue))
                {
                    return DateTime.Now;
                }
                else
                    return _requestedShipDate;
            }
            set
            {
                _requestedShipDate = value;
            }

        }
        [XmlElement(ElementName = "customernumber", IsNullable = true)]
        public string CustomerNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_customerNumber))
                    return string.Empty;
                else
                    return _customerNumber;
            }
            set
            {
                _customerNumber = value;
            }
        }

        [XmlElement(ElementName = "customername", IsNullable = true)]
        public string CustomerName
        {
            get
            {
                if (string.IsNullOrEmpty(_customerName))
                    return string.Empty;
                else
                    return _customerName;
            }
            set
            {
                _customerName = value;
            }
        }
        //_customerName
        [XmlElement(ElementName = "PurchaseOrderType", IsNullable = true)]
        public string POType
        {
            get
            {
                if (string.IsNullOrEmpty(_poType))
                    return string.Empty;
                else
                    return _poType;
            }
            set
            {
                _poType = value;
            }
        }
        [XmlElement(ElementName = "storeid", IsNullable = true)]
        public string StoreID
        {
            get
            {
                if (string.IsNullOrEmpty(_storeID))
                    return string.Empty;
                else
                    return _storeID;
            }
            set
            {
                _storeID = value;
            }
        }
        [XmlElement(ElementName = "b2bstoreid", IsNullable = true)]
        public string B2BStoreID
        {
            get
            {
                if (string.IsNullOrEmpty(_b2bStoreID))
                    return string.Empty;
                else
                    return _b2bStoreID;
            }
            set
            {
                _b2bStoreID = value;
            }
        }
        [XmlIgnore]
        [XmlElement(ElementName = "b2b", IsNullable = true)]
        public bool? B2B
        {
            get
            {
                if (_b2b == null)
                    return false;
                else
                    return _b2b;
            }
            set
            {

                _b2b = value;
            }

        }
        [XmlElement(ElementName = "comment", IsNullable = true)]
        public string Comments
        {
            get
            {
                if (string.IsNullOrEmpty(_comments))
                    return string.Empty;
                else
                    return _comments;
            }
            set
            {
                _comments = value;
            }
        }

        [XmlElement(ElementName = "shipping", IsNullable = true)]
        public clsShipping Shipping
        {
            get
            {
                return _shipping;
            }
            set
            {
                _shipping = value;
            }
        }

        [XmlElement(ElementName = "shipthrough", IsNullable = true)]
        public string ShipThrough
        {
            get
            {
                if (string.IsNullOrEmpty(_shipThrough))
                    return string.Empty;
                else
                    return _shipThrough;
            }
            set
            {
                _shipThrough = value;
            }
        }
        //[XmlIgnore]
        [XmlElement(ElementName = "shipmenttype")]
        public ShipmentType ShipmentType
        {
            get
            {
                return shipmentType;
            }
            set
            {
                shipmentType = value;
            }
        }
        [XmlArray("details"), XmlArrayItem("lineitem", IsNullable = true)]
        public List<PurchaseOrderItem> PurchaseOrderItems
        {
            get
            {
                return _items;
            }
            set
            {
                _items = value;
            }

        }
        [XmlIgnore]
        [XmlArray("returnlabels"), XmlArrayItem("returnlabelitem", IsNullable = true)]
        public List<ReturnLabel> ReturnLabelItems
        {
            get
            {
                return returnLabelItems;
            }
            set
            {
                returnLabelItems = value;
            }
        }
        [XmlIgnore]
        [XmlArray("fulfillmentcomments"), XmlArrayItem("fulfillmentcommentlist", IsNullable = true)]
        public List<FulfillmentComment> FulfillmentCommentList
        {
            get
            {
                return fulfillmentCommentList;
            }
            set
            {
                fulfillmentCommentList = value;
            }
        }


        #endregion

    }


    

    
    
}
