using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace avii.Classes
{
                 
    #region Enums

    [Serializable]
    public enum AssignmentType
    {
        [XmlEnumAttribute(Name = "NewAssign")]
        NewAssign = 'N',
        [XmlEnumAttribute(Name = "Swap")]
        Swap = 'S'
    }

    [Serializable]
    [Flags]
    public enum TimeInterval
    {
        [XmlEnumAttribute(Name = "Today")]
        Today = 1,
        [XmlEnumAttribute(Name = "OneWeek")]
        OneWeek = 7,
        [XmlEnumAttribute(Name = "TwoWeek")]
        TwoWeek = 15,
        [XmlEnumAttribute(Name = "LastMonth")]
        LastMonth = 30,
        [XmlEnumAttribute(Name = "Quaterly")]
        Quaterly = 90,
        [XmlEnumAttribute(Name = "SixMonths")]
        SixMonths = 180,
        [XmlEnumAttribute(Name = "OneYear")]
        OneYear = 365

    }
    public enum PurchaseOrderFlag
    {
        A,
        W,
        U
    }
    public enum RMAFlag
    {
        A,
        W,
        U
    }
    [Serializable]
    public enum RMAStatus
    {
        None = 0,
        Pending = 1,
        Received = 2,
        PendingForRepair = 3,
        PendingForCredit = 4,
        PendingForReplacement = 5,
        Approved = 6,
        Returned = 7,
        Credited = 8,
        Denied = 9,
        Closed = 10,
        OutWithOEMForRepair=11,
        BackToStockNDF=12,
        BackToStockCredited = 13,
        BackToStockReplacedByOEM = 14,
        RepairedByOEM  = 15,
        ReplacedBYOEM = 16,
        ReplacedByAV = 17,
        RepairedByAV = 18,
        NDFNoDefectFound = 19,
        PreOwnedAstock = 20,
        PreOwnedBtock = 21,
        PreOwnedCtock = 22,
        Rejected = 23,
        RTSReturnToStock = 24,
        Incomplete = 25,
        Damaged = 26,
        Preowned = 27,
        ReturnToOEM = 28,
        ReturnedToStock = 29,
        Cancel = 30,
        ExternalESN = 31,
        PendingShipToOEM = 32,
        SentToOEM = 33,
        PendingShipToSupplier = 34,
        SentToSupplier = 35,
        ReturnedFromOEM=36,
        ShippingError = 37,
        ReplacedByOEMNew=38,
        ReplacedByOEMPreowned=39
    }
    
    [Serializable]
    [Flags]
    public enum ShipmentType
    {
        [XmlEnumAttribute(Name = "Ship")]
        Ship='S',
        [XmlEnumAttribute(Name = "Return")]
        Return='R'
    }

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
        FulfillmentOrderNotExists,
        PurchaseOrderAlreadyExists,
        ShipByIsNotCorrect,
        NoRecordsFound,
        NoItemFound,
        RMANotExists,
        UpdatedSuccessfully,
        QuantityIsNotCorrect,
        PurchaseOrderCannotBeCancelled,
        RMACannotBeCancelled,
        StateCodeIsNotCorrect,
        FulfillmentOrderAlreadyProcessed,
        AccountNumberNotExists,
        DataNotUpdated,
        DuplicateItemFound,
        StroreIDNotExists,
        SubmittedSuccessfully,
        CancelledSuccessfully
    }

    [Serializable]
    public enum ShippingMethod
    {
        FedEx,
        UPS
    }

    [Serializable]
    [Flags]
    public enum PurchaseOrderStatus
    {
        [XmlEnumAttribute(Name = "Pending")]
        Pending=1,
        [XmlEnumAttribute(Name = "Processed")]
        Processed=2,
        [XmlEnumAttribute(Name = "Shipped")]
        Shipped=3,
        [XmlEnumAttribute(Name = "Closed")]
        Closed=4,
        [XmlEnumAttribute(Name = "Return")]
        Return=5,
        [XmlEnumAttribute(Name = "OnHold")]
        OnHold = 6,
        [XmlEnumAttribute(Name = "OutofStock")]
        OutofStock=7,
        [XmlEnumAttribute(Name = "InProcess")]
        InProcess = 8,
        [XmlEnumAttribute(Name = "Cancelled")]
        Cancelled = 9,
        [XmlEnumAttribute(Name = "PartialProcessed")]
        PartialProcessed = 10,
        [XmlEnumAttribute(Name = "PartialShipped")]
        PartialShipped = 11
        
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

    [XmlRoot(ElementName = "PurchaseOrderChangeStatus", IsNullable = true)]
    public class PurchaseOrderChangeStatus
    {

        private string purchaseOrderNumber;
        private string companyAccountNumber;

        [XmlElement(ElementName = "PurchaseOrderNumber", IsNullable = true)]
        public string PurchaseOrderNumber
        {
            get { return purchaseOrderNumber; }
            set { purchaseOrderNumber = value; }
        }
        [XmlElement(ElementName = "companyAccountNumber", IsNullable = true)]
        public string CompanyAccountNumber
        {
            get { return companyAccountNumber; }
            set { companyAccountNumber = value; }
        }
        
    }


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
        private string simNumber = string.Empty;

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
        [XmlIgnore]
        [XmlElement(ElementName = "simnumber", IsNullable = true)]
        public string SimNumber
        {
            get
            {
                if (string.IsNullOrEmpty(simNumber))
                    return string.Empty;
                else
                    return simNumber;
            }
            set
            {
                simNumber = value;
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
        private bool customAttribute;
        private string lteICCID;
        private string lteIMSI;
        private string akey;
        private bool isSim;
        private string simNumber;
        private string trackingNumber;
        private string batchnumber;
        private string productName;

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


        [XmlIgnore]
        public string ProductName
        {
            get
            {
                return productName;
            }
            set
            {
                productName = value;
            }
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
        //[XmlIgnore]
        //public int EsnCount
        //{
        //    get
        //    {
        //        return esnCount;
        //    }
        //    set
        //    {
        //        esnCount = value;
        //    }
        //}

        [XmlIgnore]
        public bool CustomAttribute
        {
            get
            {
                return customAttribute;
            }
            set
            {
                customAttribute = value;
            }
        }

        [XmlIgnore]
        public bool IsSim
        {
            get
            {
                return isSim;
            }
            set
            {
                isSim = value;
            }
        }
        [XmlIgnore]
        public string Akey
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
        [XmlIgnore]
        public string LTEICCID
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
        [XmlIgnore]
        public string LTEIMSI
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
                else
                {
                    _qty = value;
                }
                //if (PurchaseOrderLineItem.Phone == _purchaseOrderLineItem && value > 1)
                //{
                //    throw new Exception("Only one phone can be ordered in one line item");

                //}
                //else
                //{
                //    _qty = value;
                //}
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
        [XmlElement(ElementName = "batchnumber", IsNullable = true)]
        public string BatchNumber
        {
            get
            {
                if (string.IsNullOrEmpty(batchnumber))
                    return string.Empty;
                else
                    return batchnumber;
            }
            set
            {
                batchnumber = value;
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
        [XmlElement(ElementName = "simnumber", IsNullable = true)]
        public string SimNumber
        {
            get
            {
                if (string.IsNullOrEmpty(simNumber))
                    return string.Empty;
                else
                    return simNumber;
            }
            set
            {
                simNumber = value;
            }
        }
        [XmlElement(ElementName = "trackingnumber", IsNullable = true)]
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
        private string otksl;
        private string akey;
        //private AssignmentType assignmentType;
        //private string hex;

        
        

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
        
        [XmlElement(ElementName = "otksl", IsNullable = true)]
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
        [XmlElement(ElementName = "akey", IsNullable = true)]
        public string Akey
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

         
    }

    public class PurchaseOrderProvisioningRequest
    {
        private PurchaseOrderProvisioning _ProvisionInfo;
        private clsAuthentication _auth;
        private AssignmentType assignmentType;
        public PurchaseOrderProvisioningRequest()
        {
            _auth = new clsAuthentication();
            _ProvisionInfo = new PurchaseOrderProvisioning();
        }

        public AssignmentType AssignmentType
        {
            get
            {
                return assignmentType;
            }
            set
            {
                assignmentType = value;
            }
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

    public class UsersRequest
    {
        private clsAuthentication _auth;
        public UsersRequest()
        {
            _auth = new clsAuthentication();

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
    public class UsersResponse
    {
        private avii.Classes.CompanyInformation companyInfo;
        private string returnCode;
        private string comment;
        public UsersResponse()
        {
            companyInfo = new CompanyInformation();
        }
        public avii.Classes.CompanyInformation CompanyInfo
        {
            get
            {
                return companyInfo;
            }
            set
            {
                companyInfo = value;
            }
        }

        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
    }


    public class RmaResponses
    {
        private avii.Classes.RMAInfo _rmaInfo;
        private string _errorCode;
        private string _comment;
        public RmaResponses()
        {
            _rmaInfo = new  RMAInfo();
        }
        public avii.Classes.RMAInfo RmaInformation
        {
            get
            {
                return _rmaInfo;
            }
            set
            {
                _rmaInfo = value;
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

    public class RMARequest
    {
        private RMANew _rmaNew;
        private clsAuthentication _auth;
        public RMARequest()
        {
            _auth = new clsAuthentication();
            _rmaNew = new RMANew();

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

        public RMANew RmaInformation
        {
            get
            {
                return _rmaNew;
            }
            set
            {
                _rmaNew = value;
            }
        }

    }
    public class StockRequest
    {

        private clsAuthentication _auth;
        public StockRequest()
        {
            _auth = new clsAuthentication();

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
    public class StockResponse
    {
        private List<avii.Classes.StockInHand> _stockList;
        private string _errorCode;
        private string _comment;
        public StockResponse()
        {
            _stockList = new List<StockInHand>();
        }
        public List<avii.Classes.StockInHand> StockinHandList
        {
            get
            {
                return _stockList;
            }
            set
            {
                _stockList = value;
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

    public class RMARequests
    {
        private string _rmaNumber;
        
        private clsAuthentication _auth;
        public RMARequests()
        {
            _auth = new clsAuthentication();
            
        }
        public string RMANumber
        {
            get
            {
                return _rmaNumber;
            }
            set
            {
                _rmaNumber = value;
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
    public class RmaResponse
    {
        private List<avii.Classes.RMAReport> _rmaList;
        private string _errorCode;
        private string _comment;
        public RmaResponse()
        {
            _rmaList = new List<RMAReport>();
        }
        public List<avii.Classes.RMAReport> RmaReportList
        {
            get
            {
                return _rmaList;
            }
            set
            {
                _rmaList = value;
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
    public class RMASearchCriteria
    {
        private string _rmaNumber;
        private RMAStatus _rmaStatus;
        private string _esn;

        private string _returnReason;
        private DateTime _rma_from_date;// = DateTime.Today.AddYears(-1);
        private DateTime _rma_to_date;// = DateTime.Today;

        private clsAuthentication _auth;
        public RMASearchCriteria()
        {
            _auth = new clsAuthentication();
            _rmaStatus = Classes.RMAStatus.None;
        }
        public string RMANumber
        {
            get
            {
                return _rmaNumber;
            }
            set
            {
                _rmaNumber = value;
            }
        }
        public Classes.RMAStatus RMAStatus
        {
            get
            {
                return _rmaStatus;
            }
            set
            {
                _rmaStatus = value;
            }
        }
        public string ESN
        {
            get
            {
                return _esn;
            }
            set
            {
                _esn = value;
            }
        }
        public string ReturnReason
        {
            get
            {
                return _returnReason;
            }
            set
            {
                _returnReason = value;
            }
        }
        public DateTime RMA_To_Date
        {
            get
            {
                return _rma_to_date;
            }
            set
            {
                _rma_to_date = value;
            }
        }
        public DateTime RMA_From_Date
        {
            get
            {
                return _rma_from_date;
            }
            set
            {
                _rma_from_date = value;
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
    
    public class FulfillmentOrderStatusRequest
    {
        private TimeInterval duration;
        private clsAuthentication auth;

        public FulfillmentOrderStatusRequest()
        {
            auth = new clsAuthentication();
        }


        public TimeInterval Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }

        //[XmlElement(ElementName = "Authentication", IsNullable = true)]
        public clsAuthentication Authentication
        {
            get
            {
                return auth;
            }
            set
            {
                auth = value;
            }
        }
    }
    public class FulfillmentOrderStatusResponse
    {
        private List<FulfillmentStatusReport> fulfillmentStatusList;
        private string returnCode;
        private string comment;
        public FulfillmentOrderStatusResponse()
        {
            fulfillmentStatusList = new List<FulfillmentStatusReport>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<FulfillmentStatusReport> FulfillmentStatusList
        {
            get { return fulfillmentStatusList; }
            set { fulfillmentStatusList = value; }
        }
    }
    public class RMAStatusRequest
    {
        private TimeInterval duration;
        private clsAuthentication auth;

        public RMAStatusRequest()
        {
            auth = new clsAuthentication();
        }


        public TimeInterval Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }

        //[XmlElement(ElementName = "Authentication", IsNullable = true)]
        public clsAuthentication Authentication
        {
            get
            {
                return auth;
            }
            set
            {
                auth = value;
            }
        }
    }
    public class RMAStatusResponse
    {
        private List<CompanyRmaStatuses> rmaStatusList;
        private string returnCode;
        private string comment;
        public RMAStatusResponse()
        {
            rmaStatusList = new List<CompanyRmaStatuses>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<CompanyRmaStatuses> RMAStatusList
        {
            get { return rmaStatusList; }
            set { rmaStatusList = value; }
        }
    }

    public class ProductRmaReasonsRequest
    {
        private TimeInterval duration;
        private clsAuthentication auth;

        public ProductRmaReasonsRequest()
        {
            auth = new clsAuthentication();
        }


        public TimeInterval Duration
        {
            get
            {
                return duration;
            }
            set
            {
                duration = value;
            }
        }

        //[XmlElement(ElementName = "Authentication", IsNullable = true)]
        public clsAuthentication Authentication
        {
            get
            {
                return auth;
            }
            set
            {
                auth = value;
            }
        }
    }
    public class ProductRmaReasonsResponse
    {
        private List<ProductRmaReason> productRmaReason;
        private string returnCode;
        private string comment;
        public ProductRmaReasonsResponse()
        {
            productRmaReason = new List<ProductRmaReason>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<ProductRmaReason> ProductRmaReason
        {
            get { return productRmaReason; }
            set { productRmaReason = value; }
        }
    }


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

    public class PurchaseOrderInfoRequest
    {
        private string _purchaseOrderNumber;
        private string esn;
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
    public class FulfillmentReportRequest
    {
        private string _fulfillmentNumber;
        private DateTime fromPODate = DateTime.MinValue;
        private DateTime toPODate = DateTime.MinValue;
        private DateTime fromShipDate = DateTime.MinValue;
        private DateTime toShipDate = DateTime.MinValue;
        private PurchaseOrderStatus _purchaseOrderStatus;
        private string trackingNumber;
         
        
        private clsAuthentication _auth;

        public FulfillmentReportRequest()
        {
            _auth = new clsAuthentication();

        }
        public string TrackingNumber
        {
            get
            {
                return trackingNumber;
            }
            set
            {
                trackingNumber = value;
            }
        }
        public PurchaseOrderStatus FulfillmentOrderStatus
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
        public DateTime ShipFromDate
        {
            get
            {
                return fromShipDate;
            }
            set
            {
                fromShipDate = value;
            }
        }
        public DateTime ShipToDate
        {
            get
            {
                return toShipDate;
            }
            set
            {
                toShipDate = value;
            }
        }
        
        public DateTime FulfillmentFromDate
        {
            get
            {
                return fromPODate;
            }
            set
            {
                fromPODate = value;
            }
        }
        public DateTime FulfillmentToDate
        {
            get
            {
                return toPODate;
            }
            set
            {
                toPODate = value;
            }
        }
        public string FulfillmentNumber
        {
            get
            {
                return _fulfillmentNumber;
            }
            set
            {
                _fulfillmentNumber = value;
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
    public class FulfillmentReportResponse
    {
        private List<FulfillmentOrdersProvisional> fulfillmentOrderList;
        private string returnCode;
        private string comment;
        public FulfillmentReportResponse()
        {
            fulfillmentOrderList = new List<FulfillmentOrdersProvisional>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<FulfillmentOrdersProvisional> FulfillmentOrderList
        {
            get { return fulfillmentOrderList; }
            set { fulfillmentOrderList = value; }
        }
    }

    public class FulfillmentOrderWithHeaderResponse
    {
        private List<FulfillmentOrdersListWithHeader> fulfillmentOrderList;
        private string returnCode;
        private string comment;
        public FulfillmentOrderWithHeaderResponse()
        {
            fulfillmentOrderList = new List<FulfillmentOrdersListWithHeader>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<FulfillmentOrdersListWithHeader> FulfillmentOrderList
        {
            get { return fulfillmentOrderList; }
            set { fulfillmentOrderList = value; }
        }
    }

    public class FulfillmentOrderListItemsResponse
    {
        private List<FulfillmentOrdersListItems> fulfillmentOrderList;
        private string returnCode;
        private string comment;
        public FulfillmentOrderListItemsResponse()
        {
            fulfillmentOrderList = new List<FulfillmentOrdersListItems>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<FulfillmentOrdersListItems> FulfillmentOrderList
        {
            get { return fulfillmentOrderList; }
            set { fulfillmentOrderList = value; }
        }
    }


    public class CancelPurchaseOrderResponse
    {
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

    public class CompanyStoreRequest
    {

        private clsAuthentication _auth;

        public CompanyStoreRequest()
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

    public class CompanyStoreResponse
    {
        private CompanyInfo _companyInfo;
        private string _errorCode;
        private string _comment;

        public CompanyStoreResponse()
        {
            _companyInfo = new CompanyInfo();
        }

        public CompanyInfo CompanyInformation
        {
            get
            {
                return _companyInfo;
            }
            set
            {
                _companyInfo = value;
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

    public class InventoryResponse
    {
        private List<InventorySKU> _inventoryList;
        private string _errorCode;
        private string _comment;

        public InventoryResponse()
        {
            _inventoryList = new List<InventorySKU>();
        }

        public List<InventorySKU> InventoryList
        {
            get
            {
                return _inventoryList;
            }
            set
            {
                _inventoryList = value;
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
        private string esn;
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

    [Serializable]
    [XmlRoot(ElementName = "FulfillmentOrdersProvisional", IsNullable = true)]
    public class FulfillmentOrdersProvisional 
    {
        #region Private Members
        private string aerovoiceSaleOrderNumber = string.Empty;
        private string fulfillmentOrderNumber = string.Empty;
        private string storeID = string.Empty;
        private string trackingNumber = string.Empty;
        private string productCode = string.Empty;
        private string esn = string.Empty;
        private string upc = string.Empty;
        private string mslNumber = string.Empty;
        private string lteICCID = string.Empty;
        private string lteIMSI = string.Empty;
        private string akey = string.Empty;
        private string otksl = string.Empty;
        private string simNumber = string.Empty;
        private string batchNumber = string.Empty;

        #endregion

        #region Constructor

        public FulfillmentOrdersProvisional()
        {
            
        }

        #endregion

        #region Properties
        
        [XmlElement(ElementName = "BatchNumber", IsNullable = true)]
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
        [XmlIgnore]
        [XmlElement(ElementName = "SimNumber", IsNullable = true)]
        public string SimNumber
        {
            get
            {
                return simNumber;
            }
            set
            {
                simNumber = value;
            }
        }
        [XmlElement(ElementName = "FulfillmentOrderNumber", IsNullable = true)]
        public string FulfillmentOrderNumber
        {
            get
            {
                return fulfillmentOrderNumber;
            }
            set
            {
                fulfillmentOrderNumber = value;
            }
        }
        [XmlElement(ElementName = "SaleOrderNumber", IsNullable = true)]
        public string AerovoiceSaleOrderNumber
        {
            get
            {
                return aerovoiceSaleOrderNumber;
            }
            set
            {
                aerovoiceSaleOrderNumber = value;
            }
        }
        [XmlElement(ElementName = "StoreID", IsNullable = true)]
        public string StoreID
        {
            get
            {
                return storeID;
            }
            set
            {
                storeID = value;
            }
        }
        [XmlElement(ElementName = "TrackingNumber", IsNullable = true)]
        public string TrackingNumber
        {
            get
            {
                return trackingNumber;
            }
            set
            {
                trackingNumber = value;
            }
        }
        [XmlElement(ElementName = "SKU", IsNullable = true)]
        public string ProductCode
        {
            get
            {
                return productCode;
            }
            set
            {
                productCode = value;
            }
        }
        [XmlElement(ElementName = "ESN", IsNullable = true)]
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
        [XmlElement(ElementName = "UPC", IsNullable = true)]
        public string UPC
        {
            get
            {
                return upc;
            }
            set
            {
                upc = value;
            }
        }
        [XmlIgnore]
        [XmlElement(ElementName = "MSLNumber", IsNullable = true)]
        public string MSLNumber
        {
            get
            {
                return mslNumber;
            }
            set
            {
                mslNumber = value;
            }
        }

        [XmlElement(ElementName = "ICCID", IsNullable = true)]
        public string LTEICCID
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
        [XmlIgnore]
        [XmlElement(ElementName = "LTEIMSI", IsNullable = true)]
        public string LTEIMSI
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
        [XmlIgnore]
        [XmlElement(ElementName = "AKEY", IsNullable = true)]
        public string Akey
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
        [XmlIgnore]
        [XmlElement(ElementName = "OTKSL", IsNullable = true)]
        public string Otksl
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

        #endregion





    }

    [Serializable]
    [XmlRoot(ElementName = "FulfillmentOrderHeader", IsNullable = true)]
    public class FulfillmentOrderHeader
    {
        #region Private Members
        private string aerovoiceSaleOrderNumber = string.Empty;
        private string fulfillmentOrderNumber = string.Empty;
        private string storeID = string.Empty;
        private string trackingNumber = string.Empty;
        private string customerAccountNumber = string.Empty;
        private string contactName = string.Empty;
        private string shipAddress = string.Empty;
        private string shipCity = string.Empty;
        private DateTime? fulfillmentOrderDate;
        private string shipState = string.Empty;
        private string shipZip = string.Empty;
        private string shipVia = string.Empty;
        private PurchaseOrderStatus poStatus;

        #endregion

        #region Constructor

        public FulfillmentOrderHeader()
        {

        }

        #endregion

        #region Properties

        public PurchaseOrderStatus FulfillmentStatus
        {
            get
            {
                return poStatus;
            }
            set
            {
                poStatus = value;
            }
        }
        [XmlElement(ElementName = "FulfillmentOrderDate", IsNullable = true)]
        public DateTime? FulfillmentOrderDate
        {
            get
            {
                return fulfillmentOrderDate;
            }
            set
            {
                fulfillmentOrderDate = value;
            }
        }
        [XmlElement(ElementName = "FulfillmentOrderNumber", IsNullable = true)]
        public string FulfillmentOrderNumber
        {
            get
            {
                return fulfillmentOrderNumber;
            }
            set
            {
                fulfillmentOrderNumber = value;
            }
        }
        [XmlElement(ElementName = "AerovoiceSaleOrderNumber", IsNullable = true)]
        public string AerovoiceSaleOrderNumber
        {
            get
            {
                return aerovoiceSaleOrderNumber;
            }
            set
            {
                aerovoiceSaleOrderNumber = value;
            }
        }
        [XmlElement(ElementName = "StoreID", IsNullable = true)]
        public string StoreID
        {
            get
            {
                return storeID;
            }
            set
            {
                storeID = value;
            }
        }
        [XmlElement(ElementName = "TrackingNumber", IsNullable = true)]
        public string TrackingNumber
        {
            get
            {
                return trackingNumber;
            }
            set
            {
                trackingNumber = value;
            }
        }
        [XmlElement(ElementName = "CustomerAccountNumber", IsNullable = true)]
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
        [XmlElement(ElementName = "ContactName", IsNullable = true)]
        public string ContactName
        {
            get
            {
                return contactName;
            }
            set
            {
                contactName = value;
            }
        }
        [XmlElement(ElementName = "ShipVia", IsNullable = true)]
        public string ShipVia
        {
            get
            {
                return shipVia;
            }
            set
            {
                shipVia = value;
            }
        }
        [XmlElement(ElementName = "ShipAddress", IsNullable = true)]
        public string ShipAddress
        {
            get
            {
                return shipAddress;
            }
            set
            {
                shipAddress = value;
            }
        }
        [XmlElement(ElementName = "ShipCity", IsNullable = true)]
        public string ShipCity
        {
            get
            {
                return shipCity;
            }
            set
            {
                shipCity = value;
            }
        }
        [XmlElement(ElementName = "ShipState", IsNullable = true)]
        public string ShipState
        {
            get
            {
                return shipState;
            }
            set
            {
                shipState = value;
            }
        }
        [XmlElement(ElementName = "ShipZip", IsNullable = true)]
        public string ShipZip
        {
            get
            {
                return shipZip;
            }
            set
            {
                shipZip = value;
            }
        }


        #endregion





    }
    [Serializable]
    [XmlRoot(ElementName = "FulfillmentOrdersListWithHeader", IsNullable = true)]
    public class FulfillmentOrdersListWithHeader
    {
        #region Private Members
        private string aerovoiceSaleOrderNumber = string.Empty;
        private string fulfillmentOrderNumber = string.Empty;
        private string storeID = string.Empty;
        private string trackingNumber = string.Empty;
        private string customerAccountNumber = string.Empty;
        private string contactName = string.Empty;
        private string shipAddress = string.Empty;
        private string shipCity = string.Empty;
        private DateTime? fulfillmentOrderDate;
        private string shipState = string.Empty;
        private string shipZip = string.Empty;
        private string shipBy = string.Empty;
        #endregion

        #region Constructor

        public FulfillmentOrdersListWithHeader()
        {

        }

        #endregion

        #region Properties
        [XmlElement(ElementName = "FulfillmentOrderDate", IsNullable = true)]
        public DateTime? FulfillmentOrderDate
        {
            get
            {
                return fulfillmentOrderDate;
            }
            set
            {
                fulfillmentOrderDate = value;
            }
        }
        [XmlElement(ElementName = "FulfillmentOrderNumber", IsNullable = true)]
        public string FulfillmentOrderNumber
        {
            get
            {
                return fulfillmentOrderNumber;
            }
            set
            {
                fulfillmentOrderNumber = value;
            }
        }
        [XmlElement(ElementName = "SaleOrderNumber", IsNullable = true)]
        public string AerovoiceSaleOrderNumber
        {
            get
            {
                return aerovoiceSaleOrderNumber;
            }
            set
            {
                aerovoiceSaleOrderNumber = value;
            }
        }
        [XmlElement(ElementName = "StoreID", IsNullable = true)]
        public string StoreID
        {
            get
            {
                return storeID;
            }
            set
            {
                storeID = value;
            }
        }
        [XmlElement(ElementName = "TrackingNumber", IsNullable = true)]
        public string TrackingNumber
        {
            get
            {
                return trackingNumber;
            }
            set
            {
                trackingNumber = value;
            }
        }
        [XmlElement(ElementName = "CustomerAccountNumber", IsNullable = true)]
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
        [XmlElement(ElementName = "ContactName", IsNullable = true)]
        public string ContactName
        {
            get
            {
                return contactName;
            }
            set
            {
                contactName = value;
            }
        }
        [XmlElement(ElementName = "ShipBy", IsNullable = true)]
        public string ShipBy
        {
            get
            {
                return shipBy;
            }
            set
            {
                shipBy = value;
            }
        }
        [XmlElement(ElementName = "ShipAddress", IsNullable = true)]
        public string ShipAddress
        {
            get
            {
                return shipAddress;
            }
            set
            {
                shipAddress = value;
            }
        }
        [XmlElement(ElementName = "ShipCity", IsNullable = true)]
        public string ShipCity
        {
            get
            {
                return shipCity;
            }
            set
            {
                shipCity = value;
            }
        }
        [XmlElement(ElementName = "ShipState", IsNullable = true)]
        public string ShipState
        {
            get
            {
                return shipState;
            }
            set
            {
                shipState = value;
            }
        }
        [XmlElement(ElementName = "ShipZip", IsNullable = true)]
        public string ShipZip
        {
            get
            {
                return shipZip;
            }
            set
            {
                shipZip = value;
            }
        }


        #endregion





    }

    [Serializable]
    [XmlRoot(ElementName = "FulfillmentOrdersListItems", IsNullable = true)]
    public class FulfillmentOrdersListItems
    {
        #region Private Members
        private string aerovoiceSaleOrderNumber = string.Empty;
        private string fulfillmentOrderNumber = string.Empty;
        private string storeID = string.Empty;
        private string trackingNumber = string.Empty;
        private string productCode = string.Empty;
        private string esn = string.Empty;
        private string upc = string.Empty;
        private string mslNumber = string.Empty;
        private string mdn = string.Empty;
        private string msid = string.Empty;
        private string passCode = string.Empty;
        private string lteICCID = string.Empty;
        private string lteIMSI = string.Empty;
        private string akey = string.Empty;
        private string otksl = string.Empty;
        private string simNumber = string.Empty;
        #endregion

        #region Constructor

        public FulfillmentOrdersListItems()
        {

        }

        #endregion

        #region Properties
        [XmlElement(ElementName = "SimNumber", IsNullable = true)]
        public string SimNumber
        {
            get
            {
                return simNumber;
            }
            set
            {
                simNumber = value;
            }
        }
        [XmlElement(ElementName = "PassCode", IsNullable = true)]
        public string PassCode
        {
            get
            {
                return passCode;
            }
            set
            {
                passCode = value;
            }
        }
        [XmlElement(ElementName = "MSID", IsNullable = true)]
        public string MSID
        {
            get
            {
                return msid;
            }
            set
            {
                msid = value;
            }
        }
        [XmlElement(ElementName = "MDN", IsNullable = true)]
        public string MDN
        {
            get
            {
                return mdn;
            }
            set
            {
                mdn = value;
            }
        }
        [XmlElement(ElementName = "FulfillmentOrderNumber", IsNullable = true)]
        public string FulfillmentOrderNumber
        {
            get
            {
                return fulfillmentOrderNumber;
            }
            set
            {
                fulfillmentOrderNumber = value;
            }
        }
        [XmlElement(ElementName = "AerovoiceSaleOrderNumber", IsNullable = true)]
        public string AerovoiceSaleOrderNumber
        {
            get
            {
                return aerovoiceSaleOrderNumber;
            }
            set
            {
                aerovoiceSaleOrderNumber = value;
            }
        }
        [XmlElement(ElementName = "StoreID", IsNullable = true)]
        public string StoreID
        {
            get
            {
                return storeID;
            }
            set
            {
                storeID = value;
            }
        }
        [XmlElement(ElementName = "TrackingNumber", IsNullable = true)]
        public string TrackingNumber
        {
            get
            {
                return trackingNumber;
            }
            set
            {
                trackingNumber = value;
            }
        }
        [XmlElement(ElementName = "ProductCode", IsNullable = true)]
        public string ProductCode
        {
            get
            {
                return productCode;
            }
            set
            {
                productCode = value;
            }
        }
        [XmlElement(ElementName = "ESN", IsNullable = true)]
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
        [XmlElement(ElementName = "UPC", IsNullable = true)]
        public string UPC
        {
            get
            {
                return upc;
            }
            set
            {
                upc = value;
            }
        }
        [XmlElement(ElementName = "MSLNumber", IsNullable = true)]
        public string MSLNumber
        {
            get
            {
                return mslNumber;
            }
            set
            {
                mslNumber = value;
            }
        }
        [XmlElement(ElementName = "LTEICCID", IsNullable = true)]
        public string LTEICCID
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
        [XmlElement(ElementName = "LTEIMSI", IsNullable = true)]
        public string LTEIMSI
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
        [XmlElement(ElementName = "AKEY", IsNullable = true)]
        public string Akey
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
        [XmlElement(ElementName = "Otksl", IsNullable = true)]
        public string Otksl
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


        #endregion





    }
    public class FulfillmentSKUs
    {
        public int POD_ID { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public int AssignedQty { get; set; }
        public bool IsDelete { get; set; }
    }

    }
