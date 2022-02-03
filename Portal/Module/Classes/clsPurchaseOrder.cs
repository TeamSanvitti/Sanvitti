using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace avii.Classes
{
                 

    #region Enums
    [Serializable]
    public enum PhoneCategoryType
    {
        [XmlEnumAttribute(Name = "Cold")]
        Cold='C',
        [XmlEnumAttribute(Name = "Hot")]
        Hot='H'
    }

    [Serializable]
    public enum PurchaseOrderLineItem
    {
        [XmlEnumAttribute(Name = "Phone")]
        Phone = 'P',
        [XmlEnumAttribute(Name = "Accessory")]
        Accessory = 'A'
    }
   
    [Serializable]
    public enum ResponseErrorCode
    {
        None,
        MissingParameter,
        UploadedSuccessfully,
        SuccessfullyRetrieved,
        InternalError,
        InconsistantData,
        ErrowWhileLoadingData,
        PurchaseOrderNotShipped,
        PurchaseOrderShipped,
        CannotAuthenticateUser,
        PurchaseOrderItemNotAssigned,
        PurchaseOrderNotExists,
        PurchaseOrderAlreadyExists,
        ShipByIsNotCorrect
    }

    [Serializable]
    public enum ShippingMethod
    {
        FedEx,
        UPS
    }
    
    [Serializable]
    public enum PurchaseOrderStatus
    {
        Pending=1,
        Processed=2,
        Shipped=3,
        Closed=4,
        Return=5,
        OnHold = 6,
        OutofStock=7,
        Cancelled = 9
    }
    #endregion


    #region Shipping

    [XmlType("shippinginfo")]
    [XmlRoot(ElementName = "shippinginfo", IsNullable = true)]
    public class ShipTracking
    {
        private string _purchaseOrderNumber;
        private DateTime _shipToDate;
        private string _shipToTackingNumber;
        private string _shipToBy;

        [XmlIgnore]
        [XmlElement(ElementName = "purchaseordernumber", IsNullable = true)]
        public string PurchaseOrderNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_purchaseOrderNumber))
                    return string.Empty;
                else
                    return _purchaseOrderNumber;
            }
            set
            {
                _purchaseOrderNumber = value;
            }
        }

     
        [XmlElement(ElementName = "shiptodate", DataType = "date")]
        public DateTime ShipToDate
        {
            get
            {
                return _shipToDate;
            }
            set
            {
                _shipToDate = value;
            }
        }

        [XmlElement(ElementName = "shiptoby", IsNullable = true)]
        public string ShipToBy
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToBy))
                    return string.Empty;
                else
                    return _shipToBy;
            }
            set
            {
                _shipToBy = value;
            }
        }

        [XmlElement(ElementName = "trackingnumber", IsNullable = true)]
        public string ShipToTrackingNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToTackingNumber))
                    return string.Empty;
                else
                    return _shipToTackingNumber;
            }
            set
            {
                _shipToTackingNumber = value;
            }
        }

    }

    [XmlRoot(ElementName = "shipping", IsNullable = true)]
    public class clsShipping
    {
        private string _shipToAddress;
        private string _shipToAddress2;
        private string _shipToCity;
        private string _shipToState;
        private string _shipToZip;
        private string _contactName;
        private string _contactPhone;
        private string _shipToAttn;

        [XmlElement(ElementName = "contactname",  IsNullable = true)]
        public string ContactName
        {
            get
            {
                if (string.IsNullOrEmpty(_contactName))
                    return string.Empty;
                else
                    return _contactName;
            }
            set
            {
                _contactName = value;
            }
        }

        [XmlElement(ElementName = "contactphone", IsNullable = true)]
        public string ContactPhone
        {
            get
            {
                if (string.IsNullOrEmpty(_contactPhone))
                    return string.Empty;
                else
                    return _contactPhone;
            }
            set
            {
                _contactPhone = value;
            }
        }

        [XmlElement(ElementName = "shiptoattn", IsNullable = true)]
        public string ShipToAttn
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToAttn))
                    return string.Empty;
                else
                    return _shipToAttn;
            }
            set
            {
                _shipToAttn = value;
            }
        }

        [XmlElement(ElementName = "shiptoaddress1", IsNullable = true)]
        public string ShipToAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToAddress))
                    return string.Empty;
                else
                    return _shipToAddress;
            }
            set
            {
                _shipToAddress = value;
            }
        }

        [XmlElement(ElementName = "shiptoaddress2", IsNullable = true)]
        public string ShipToAddress2
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToAddress2))
                    return string.Empty;
                else
                    return _shipToAddress2;
            }
            set
            {
                _shipToAddress2 = value;
            }
        }

        [XmlElement(ElementName = "shiptocity", IsNullable = true)]
        public string ShipToCity
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToCity))
                    return string.Empty;
                else
                    return _shipToCity;
            }
            set
            {
                _shipToCity = value;
            }
        }

        [XmlElement(ElementName = "shiptostate", IsNullable = true)]
        public string ShipToState
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToState))
                    return string.Empty;
                else
                    return _shipToState;
            }
            set
            {
                _shipToState = value;
            }
        }

        [XmlElement(ElementName = "shiptozip", IsNullable = true)]
        public string ShipToZip
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToZip))
                    return string.Empty;
                else
                    return _shipToZip;
            }
            set
            {
                _shipToZip = value;
            }
        }
    }

    public class PurchaseOrderShipRequest
    {
        private string _purchaseOrderNumber;
        private clsAuthentication _auth;

        public PurchaseOrderShipRequest()
        {
            _purchaseOrderNumber = string.Empty;
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

    public class PurchaseOrderShipResponse
    {
        private ShipTracking _ShipInfo;
        private string _errorCode;
        private string _comment;

        public ShipTracking ShipInfo
        {
            get
            {
                return _ShipInfo;
            }
            set
            {
                _ShipInfo = value;
            }
        }

        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }
    }
    #endregion


    [XmlRoot(ElementName = "lineitem", IsNullable = true)]
    public class PurchaseOrderItem
    {
        private int _lineNo =0;
        private int _podID;
        private int _itemId;
        private string _itemCode = string.Empty;
        private int? _qty = 1;
        private int? _forecastGUID = 0;
        private string _esn = string.Empty;
        private string _mdnNumber = string.Empty;

        private PhoneCategoryType _phoneCategory = PhoneCategoryType.Cold;
        private PurchaseOrderLineItem _purchaseOrderLineItem = PurchaseOrderLineItem.Phone;
        public PurchaseOrderItem()
        {
        }

        public PurchaseOrderItem(int PurchaseOrderDetailID)
        {
            _podID = PurchaseOrderDetailID;
        }

        public PurchaseOrderItem(PurchaseOrderLineItem purchaseOrderLineItem)
        {
            _purchaseOrderLineItem = purchaseOrderLineItem;
        }

        [XmlElement(ElementName = "lineno")]
        public int LineNo
        {
            get
            {
                return _lineNo;
            }
            set
            {
                _lineNo = value;
            }
        }

        [XmlElement(ElementName = "itemid", IsNullable = true)]
        [XmlIgnore]
        public int ItemID
        {
            get
            {
                return _itemId;
            }
            set
            {
                _itemId = value;
            }
        }
        [XmlElement(ElementName = "ForecastGUID", IsNullable = true)]
        [XmlIgnore]
        public int? ForecastGUID
        {
            get
            {
                return _forecastGUID;
            }
            set
            {
                _forecastGUID = value;
            }
        }

        [XmlElement(ElementName = "itemcode")]
        public string ItemCode
        {
            get
            {
                if (string.IsNullOrEmpty(_itemCode))
                    return string.Empty;
                else
                    return _itemCode;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Item code should not be null or Empty");
                }
                else
                {
                    _itemCode = value;
                }
            }
        }

        [XmlElement(ElementName = "quantity")]
        public int? Quantity
        {
            get
            {
                if (_qty == null && _qty == 0)
                    return 1;
                else
                    return _qty;
            }
            set
            {
                if (value == null && value < 1)
                {
                    throw new Exception("Quantity should not be 0 or less than 0");
                }
                else
                {
                    _qty = value;
                }
            }
        }

        [XmlElement(ElementName = "esn", IsNullable = true)]
        [XmlIgnore]
        public string ESN
        {
            get
            {
                if (string.IsNullOrEmpty(_esn))
                    return string.Empty;
                else
                    return _esn;
            }
            set
            {
                _esn = value;
            }
        }

        [XmlElement(ElementName = "mdnnumber", IsNullable = true)]
        [XmlIgnore]
        public string MdnNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_mdnNumber))
                    return string.Empty;
                else
                    return _mdnNumber;
            }
            set
            {
                _mdnNumber = value;
            }
        }

 
        [XmlElement(ElementName = "phonecategory")]
        [XmlIgnore]
        public PhoneCategoryType PhoneCategory
        {
            get
            {
                    return _phoneCategory;
            }
            set
            {
                _phoneCategory = value;
            }
        }

    }

    public class BasePurchaseOrderItem
    {
        private int _lineNo = 0;
        private string _itemCode;
        private int? _qty;
        private string _warehouseCode;
        private double? _whCost;
        private string _upc;
        private string _fmupc;
        private string _esn;
        private string _mdnNumber;
        private string _mslNumber;
        private string _otksl;
        private string _passCode;
        private string _boxId;
        private int _podID;
        private string _msID;
        private string _tilaDocument;
        private string _v2Document;
        private int _statusID;
        private PurchaseOrderStatus _purchaseOrderStatus;

        private PurchaseOrderLineItem _purchaseOrderLineItem = PurchaseOrderLineItem.Phone;
        private PhoneCategoryType _phoneCategory = PhoneCategoryType.Cold;

        public BasePurchaseOrderItem()
        {
            _purchaseOrderStatus = PurchaseOrderStatus.Pending;
        }

        public BasePurchaseOrderItem(int PurchaseOrderDetailID)
        {
            _podID = PurchaseOrderDetailID;
            _purchaseOrderStatus = PurchaseOrderStatus.Pending;
        }

        public BasePurchaseOrderItem(PurchaseOrderLineItem purchaseOrderLineItem)
        {
            _purchaseOrderLineItem = purchaseOrderLineItem;
            _purchaseOrderStatus = PurchaseOrderStatus.Pending;
        }

        [XmlElement(ElementName = "lineno")]
        public int LineNo
        {
            get
            {
                return _lineNo;
            }
            set
            {
                _lineNo = value;
            }
        }
        [XmlIgnore]
        public PurchaseOrderStatus PODStatus
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
        public int StatusID
        {
            get
            {
                return _statusID;
            }
            set
            {
                _statusID = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "podid")]
        public int PodID
        {
            get
            {
                return _podID;
            }
            set
            {
                _podID = value;
            }
        }

        [XmlElement(ElementName = "itemcode", IsNullable = true)]
        public string ItemCode
        {
            get
            {
                if (string.IsNullOrEmpty(_itemCode))
                    return string.Empty;
                else
                    return _itemCode;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new Exception("Item code should not be null");
                }
                else
                {
                    _itemCode = value;
                }
            }
        }

        [XmlElement(ElementName = "quantity", IsNullable = true)]
        public int? Quantity
        {
            get
            {
                if (_qty == null && _qty == 0)
                    return 1;
                else
                    return _qty;
            }
            set
            {
                if (value == null && value < 1)
                {
                    throw new Exception("Quantity should not be 0 or less than 0");
                }

                if (PurchaseOrderLineItem.Phone == _purchaseOrderLineItem && value > 1)
                {
                    throw new Exception("Only one phone can be ordered in one line item");

                }
                else
                {
                    _qty = value;
                }
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "warehousecode", IsNullable = true)]
        public string WarehouseCode
        {
            get
            {
                if (string.IsNullOrEmpty(_warehouseCode))
                    return string.Empty;
                else
                    return _warehouseCode;
            }
            set
            {
                _warehouseCode = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "whcost", IsNullable = true)]
        public double? WholesaleCost
        {
            get
            {
                if (_whCost == null)
                    return 0;
                else
                    return _whCost;
            }
            set
            {
                _whCost = value;
            }
        }

        [XmlElement(ElementName = "upc", IsNullable = true)]
        public string UPC
        {
            get
            {
                if (string.IsNullOrEmpty(_upc))
                    return string.Empty;
                else
                    return _upc;
            }
            set
            {
                _upc = value;
            }
        }

        [XmlElement(ElementName = "fmupc", IsNullable = true)]
        public string FmUPC
        {
            get
            {
                if (string.IsNullOrEmpty(_fmupc))
                    return string.Empty;
                else
                    return _fmupc;
            }
            set
            {
                _fmupc = value;
            }
        }

        [XmlElement(ElementName = "esn", IsNullable = true)]
        public string ESN
        {
            get
            {
                if (string.IsNullOrEmpty(_esn))
                    return string.Empty;
                else
                    return _esn;
            }
            set
            {
                _esn = value;
            }
        }

        [XmlElement(ElementName = "mdnnumber", IsNullable = true)]
        public string MdnNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_mdnNumber))
                    return string.Empty;
                else
                    return _mdnNumber;
            }
            set
            {
                _mdnNumber = value;
            }
        }

        [XmlElement(ElementName = "mslnumber", IsNullable = true)]
        public string MslNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_mslNumber))
                    return string.Empty;
                else
                    return _mslNumber;
            }
            set
            {
                _mslNumber = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "mslnumber", IsNullable = true)]
        public string MsID
        {
            get
            {
                if (string.IsNullOrEmpty(_msID))
                    return string.Empty;
                else
                    return _msID;
            }
            set
            {
                _msID = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "passcode", IsNullable = true)]
        public string PassCode
        {
            get
            {
                if (string.IsNullOrEmpty(_passCode))
                    return string.Empty;
                else
                    return _passCode;
            }
            set
            {
                _passCode = value;
            }
        }

        [XmlElement(ElementName = "otksl", IsNullable = true)]
        public string Otksal
        {
            get
            {
                if (string.IsNullOrEmpty(_otksl))
                    return string.Empty;
                else
                    return _otksl;
            }
            set
            {
                _otksl = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "boxid", IsNullable = true)]
        public string BoxId
        {
            get
            {
                if (string.IsNullOrEmpty(_warehouseCode))
                    return string.Empty;
                else
                    return _boxId;
            }
            set
            {
                _boxId = value;
            }
        }

        [XmlElement(ElementName = "phonecategory")]
        public PhoneCategoryType PhoneCategory
        {
            get
            {
                if (_phoneCategory == null)
                    return PhoneCategoryType.Cold;
                else
                    return _phoneCategory;
            }
            set
            {
                _phoneCategory = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "tiladocument", IsNullable = true)]
        public string TilaDocument
        {
            get
            {
                if (!string.IsNullOrEmpty(_tilaDocument))
                    return _tilaDocument;
                else
                    return string.Empty;
            }
            set
            {
                _tilaDocument = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "v2document", IsNullable = true)]
        public string V2Document
        {
            get
            {
                if (!string.IsNullOrEmpty(_v2Document))
                    return _v2Document;
                else
                    return string.Empty;
            }
            set
            {
                _v2Document = value;
            }
        }

    }

    #region Provisioning 
    public class PurchaseOrderProvisioning
    {
        private string _esn;
        private string _mdnNumber;
        private string _msID;
        private string _passCode;
        private string _mslNumber;
        private string _fmupc;
        

        [XmlElement(ElementName = "esn", IsNullable = true)]
        public string ESN
        {
            get
            {
                if (string.IsNullOrEmpty(_esn))
                    return string.Empty;
                else
                    return _esn;
            }
            set
            {
                _esn = value;
            }
        }

        [XmlElement(ElementName = "mdnnumber", IsNullable = true)]
        public string MdnNumber
        {
            get
            {
                return _mdnNumber;
            }
            set
            {
                _mdnNumber = value;
            }
        }

        [XmlElement(ElementName = "msid", IsNullable = true)]
        public string MsID
        {
            get
            {
                return _msID;
            }
            set
            {
                _msID = value;
            }
        }

        [XmlElement(ElementName = "mslnumber", IsNullable = true)]
        public string MslNumber
        {
            get
            {
                return _mslNumber;
            }
            set
            {
                _mslNumber = value;
            }
        }

        [XmlElement(ElementName = "passcode", IsNullable = true)]
        public string PassCode
        {
            get
            {
                return _passCode;
            }
            set
            {
                _passCode = value;
            }
        }

        [XmlElement(ElementName = "fmupc", IsNullable = true)]
        public string FmUPC
        {
            get
            {
                return _fmupc;
            }
            set
            {
                _fmupc = value;
            }
        }
    }

    public class PurchaseOrderProvisioningRequest
    {
        private PurchaseOrderProvisioning _ProvisionInfo;
        private clsAuthentication _auth;

        public PurchaseOrderProvisioningRequest()
        {
            _auth = new clsAuthentication();
            _ProvisionInfo = new PurchaseOrderProvisioning();
        }

        public PurchaseOrderProvisioning ProvisioningInfo
        {
            get
            {
                return _ProvisionInfo;
            }
            set
            {
                _ProvisionInfo = value;
            }
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

    public class PurchaseOrderProvisioningResponse
    {
       // private PurchaseOrderProvisioning _ProvisionInfo;
        private string _errorCode;
        private string _comment;

       
        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }
    }
    #endregion
   
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

    public class PurchaseOrderResponse
    {
        private string _purchaseOrderNumber;
        private string _errorCode;
        private string _comment;

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

        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }
    }

    public class PurchaseOrderInfoRequest
    {
        private string _purchaseOrderNumber;
        private clsAuthentication _auth;

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


    public class PurchaseOrderInfoResponse
    {
        private clsPurchaseOrder _purchaseOrder;
        private string _errorCode;
        private string _comment;
 
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

        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }
    }

    public class ShippByResponse
    {
        private List<ShipBy> _shipBy;
        private string _errorCode;
        private string _comment;
        public ShippByResponse()
        {
            _shipBy = new List<ShipBy>();
        }
        public List<ShipBy> ShipByList
        {
            get
            {
                return _shipBy;
            }
            set
            {
                _shipBy = value;
            }
        }

        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }
    }

    public class FulfillmentOrderRequest
    {
        private string _purchaseOrderNumber;
        private clsAuthentication _auth = new clsAuthentication();

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

    public class FulfillmentOrderResponse
    {
        private avii.Classes.FulfillmentOrder _purchaseOrder;
        private string _errorCode;
        private string _comment;

        public avii.Classes.FulfillmentOrder PurchaseOrder
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

        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }
    }

    [XmlRoot(ElementName = "purchaseorder", IsNullable = true)]
    public class clsPurchaseOrder
    {
        #region Private Members
        private string _aerovoiceSaleOrderNumber;
        private string _purchaseOrderNumber;
        private int _purchaseOrderID;
        private DateTime _purchaseOrderDate;
        private string _customerNumber;
        private string _customerName;
        private string _storeID;
        private string _comments;
        private clsShipping _shipping;
        private string _shipThrough;
        private List<PurchaseOrderItem> _items;
        private PurchaseOrderStatus _purchaseOrderStatus;
        #endregion

        #region Constructor

        public clsPurchaseOrder()
        {
            _shipping = new clsShipping();
            _items = new List<PurchaseOrderItem>();
            _purchaseOrderStatus = PurchaseOrderStatus.Pending;
        }

        public clsPurchaseOrder(int purchaseOrderID)
        {
            _purchaseOrderID = purchaseOrderID;
            _purchaseOrderStatus = PurchaseOrderStatus.Pending;
            _shipping = new clsShipping();
            _items = new List<PurchaseOrderItem>();
        }

        public clsPurchaseOrder(int purchaseOrderID, string aerovoiceSalesOrderNumber)
        {
            _purchaseOrderID = purchaseOrderID;
            _aerovoiceSaleOrderNumber = aerovoiceSalesOrderNumber;
            _purchaseOrderStatus = PurchaseOrderStatus.Pending;
            _shipping = new clsShipping();
            _items = new List<PurchaseOrderItem>();
        }
        #endregion

        #region Properties
        [XmlIgnore]
        public PurchaseOrderStatus PurchaseOrderStatus
        {
            get
            {
                return _purchaseOrderStatus;
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
        #endregion
    
    }

    
    [XmlRoot(ElementName = "purchaseorders", IsNullable = true)]
    public class PurchaseOrders
    {
        public PurchaseOrders()
        {
            _purchaseOrders = new List<BasePurchaseOrder>();
        }

        private List<BasePurchaseOrder> _purchaseOrders;

        public List<BasePurchaseOrder> PurchaseOrderList
        {
            get
            {
                return _purchaseOrders;
            }
            set
            {
                _purchaseOrders = value;
            }
        }

        public BasePurchaseOrder FindPurchaseOrder(int PurchaseOrderID)
        {
            BasePurchaseOrder purchaseOrder = null;
            if (_purchaseOrders != null)
            {
                foreach (BasePurchaseOrder po in _purchaseOrders)
                {
                    if (po.PurchaseOrderID == PurchaseOrderID)
                    {
                        purchaseOrder = po;
                    }
                }
            }

            return purchaseOrder;
        }

        public BasePurchaseOrder FindPurchaseOrderbyPodID(int PurchaseOrderItemID)
        {
            BasePurchaseOrder purchaseOrder = null;
            if (_purchaseOrders != null)
            {
                foreach (BasePurchaseOrder po in _purchaseOrders)
                {
                    foreach (BasePurchaseOrderItem poItem in po.PurchaseOrderItems)
                    {
                        if (poItem.PodID == PurchaseOrderItemID)
                        {
                            purchaseOrder = po;
                        }
                    }
                }
            }
            return purchaseOrder;
        }

        public BasePurchaseOrder FindPurchaseOrderbyNumber(string PurchaseOrderNumber)
        {
            BasePurchaseOrder purchaseOrder = null;
            if (_purchaseOrders != null)
            {
                foreach (BasePurchaseOrder po in _purchaseOrders)
                {    
                    if (po.PurchaseOrderNumber.Equals(PurchaseOrderNumber))
                    {
                        purchaseOrder = po;
                    }
                }
            }
            return purchaseOrder;
        }
          
    }

    public class BasePurchaseOrder
    {
        #region Private Members
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

        #endregion

        #region Constructor

        public BasePurchaseOrder()
        {
            _shipping = new clsShipping();
            _items = new List<BasePurchaseOrderItem>();
            _shipTracking = new ShipTracking();
            _purchaseOrderStatus = PurchaseOrderStatus.Pending;
        }

        public BasePurchaseOrder(int purchaseOrderID): this()
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
       

    #endregion





    }


    }
