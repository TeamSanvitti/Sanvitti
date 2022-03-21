using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment 
{

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
        public int ItemsPerContainer { get; set; }
        [XmlIgnore]
        public int ContainersPerPallet { get; set; }

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

}
