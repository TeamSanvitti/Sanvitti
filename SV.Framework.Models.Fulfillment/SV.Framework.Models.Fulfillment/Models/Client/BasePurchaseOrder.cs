using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    public class BasePurchaseOrder
    {
        #region Private Members
        private string _customerOrderNumber = string.Empty;
        private string _aerovoiceSaleOrderNumber = string.Empty;
        private string _purchaseOrderNumber = string.Empty;
        private int _purchaseOrderID = 0;
        private DateTime _purchaseOrderDate;
        private string _storeID = string.Empty;
        private string _comments = string.Empty;
        private string _customerName = string.Empty;
        private int? companyId = 0;
        private clsShipping _shipping;
        private ShipTracking _shipTracking;
        private List<BasePurchaseOrderItem> _items;
        private PurchaseOrderStatus _purchaseOrderStatus;
        private string sentESN = string.Empty;
        private string sentASN = string.Empty;
        private string customerAccoutNumber = string.Empty;
        private string customerNumber = string.Empty;
        private int _purchaseOrderStatusID = 1;
        private string _modifiedDate;
        private string _statusColor = string.Empty;
        private Shipment shipment;
        private string returnShipVia = string.Empty;
        private string returnLabel = string.Empty;
        private List<FulfillmentComment> fulfillmentCommentList;
        private DateTime _RequestedShipDate;



        #endregion

        #region Constructor

        public BasePurchaseOrder()
        {
            _shipping = new clsShipping();
            _items = new List<BasePurchaseOrderItem>();
            _shipTracking = new ShipTracking();
            _purchaseOrderStatus = PurchaseOrderStatus.Pending;
            shipment = new Shipment();
            fulfillmentCommentList = new List<FulfillmentComment>();
        }

        public BasePurchaseOrder(int purchaseOrderID) : this()
        {
            _purchaseOrderID = purchaseOrderID;
        }

        public BasePurchaseOrder(int purchaseOrderID, string aerovoiceSalesOrderNumber)
            : this()
        {
            _purchaseOrderID = purchaseOrderID;
            _aerovoiceSaleOrderNumber = aerovoiceSalesOrderNumber;
        }
        #endregion

        #region Properties
        public DateTime RequestedShipDate
        {
            get
            {
                return _RequestedShipDate;
            }
            set
            {
                _RequestedShipDate = value;
            }
        }
        [XmlIgnore]
        public string POSource { get; set; }

        [XmlIgnore]
        public int LineItemCount { get; set; }
        [XmlIgnore]
        public int OrderSent { get; set; }
        [XmlIgnore]
        public bool IsShipmentRequired { get; set; }
        [XmlIgnore]
        public bool IsInterNational { get; set; }

        [XmlIgnore]
        public string CustomerOrderNumber
        {
            get
            {
                return _customerOrderNumber;
            }
            set
            {
                _customerOrderNumber = value;
            }
        }

        [XmlIgnore]
        public string StatusColor
        {
            get
            {
                return _statusColor;
            }
            set
            {
                _statusColor = value;
            }
        }

        [XmlIgnore]
        public string ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }
            set
            {
                _modifiedDate = value;
            }
        }
        [XmlIgnore]
        public PurchaseOrderStatus PurchaseOrderStatus
        {
            get
            {
                return _purchaseOrderStatus;
            }
            set
            {
                _purchaseOrderStatus = value;
            }
        }
        [XmlIgnore]
        public int PurchaseOrderStatusID
        {
            get
            {
                return _purchaseOrderStatusID;
            }
            set
            {
                _purchaseOrderStatusID = value;
            }
        }
        [XmlIgnore]
        public string CustomerAccountNumber
        {
            get
            {
                return customerAccoutNumber;
            }
            set
            {
                customerAccoutNumber = value;
            }
        }

        [XmlIgnore]
        public string CustomerNumber
        {
            get
            {
                return customerNumber;
            }
            set
            {
                customerNumber = value;
            }
        }
        [XmlIgnore]
        public string CompanyLogo { get; set; }
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

        [XmlIgnore]
        public string SentESN
        {
            get
            {
                return sentESN;
            }
            set
            {
                sentESN = value;
            }
        }


        [XmlIgnore]
        public string SentASN
        {
            get
            {
                return sentASN;
            }
            set
            {
                sentASN = value;
            }
        }

        [XmlIgnore]
        public string ReturnLabel
        {
            get
            {
                return returnLabel;
            }
            set
            {
                returnLabel = value;
            }
        }
        [XmlIgnore]
        public string ReturnShipVia
        {
            get
            {
                return returnShipVia;
            }
            set
            {
                returnShipVia = value;
            }
        }

        [XmlElement(ElementName = "aerovoicesalesordernumber", IsNullable = true)]
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
                return _purchaseOrderDate;
            }
            set
            {
                _purchaseOrderDate = value;
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
        [XmlIgnore]
        [XmlElement(ElementName = "companyid", IsNullable = true)]
        public int? CompanyID
        {
            get
            {
                return companyId;
            }
            set
            {
                companyId = value;
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

        [XmlIgnore]
        [XmlElement(ElementName = "shiptracking", IsNullable = true)]
        public ShipTracking Tracking
        {
            get
            {
                return _shipTracking;
            }
            set
            {
                _shipTracking = value;
            }
        }

        [XmlArray("details"), XmlArrayItem("lineitem", IsNullable = true)]
        public List<BasePurchaseOrderItem> PurchaseOrderItems
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

        [XmlElement(ElementName = "shipment", IsNullable = true)]
        public Shipment Shipment
        {
            get
            {
                return shipment;
            }
            set
            {
                shipment = value;
            }

        }


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
