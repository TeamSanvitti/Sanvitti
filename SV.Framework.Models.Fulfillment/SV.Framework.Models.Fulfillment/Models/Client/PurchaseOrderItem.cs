using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    public enum CanadaStates
    {
        AB,
        BC,
        MB,
        NB,
        NL,
        NS,
        NT,
        NU,
        ON,
        PE,
        QC,
        SK,
        YT
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
    public enum PhoneCategoryType
    {
        [XmlEnumAttribute(Name = "Cold")]
        Cold = 'C',
        [XmlEnumAttribute(Name = "Hot")]
        Hot = 'H'
    }
    [Serializable]
    public enum PurchaseOrderLineItem
    {
        [XmlEnumAttribute(Name = "Phone")]
        Phone = 'P',
        [XmlEnumAttribute(Name = "Accessory")]
        Accessory = 'A'
    }

    [XmlRoot(ElementName = "lineitem", IsNullable = true)]
    public class PurchaseOrderItem
    {
        private int _lineNo = 0;
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
}
