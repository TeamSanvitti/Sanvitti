using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
namespace avii.Classes
{
    [XmlRoot(ElementName = "inventorylastupdate", IsNullable = true)]
    public class InventoryLastUpdate
    {
        private DateTime _phoneUpdate;
        private DateTime _accessoryUpdate;

        [XmlElement(ElementName = "phoneupdate")]
        public DateTime PhoneUpdate
        {
            get
            {
                return _phoneUpdate;
            }
            set
            {
                _phoneUpdate = value;
            }
        }

        [XmlElement(ElementName = "accessoryupdate")]
        public DateTime AccessoryUpdate
        {
            get
            {
                return _accessoryUpdate;
            }
            set
            {
                _accessoryUpdate = value;
            }
        }
    }


    [XmlRoot(ElementName = "currentinventory", IsNullable = true)]
    [Serializable]
    public class InventoryList
    {
        private List<clsInventory> inventoryList;

        public InventoryList()
        {
            inventoryList = new List<clsInventory>();
        }

        [XmlElement(ElementName = "inventory", IsNullable = true)]
        public List<clsInventory> CurrentInventory
        {
            set
            {
                inventoryList = value;
            }
            get
            {
                return inventoryList;
            }
        }

        public  bool  Exists(string itemCode)
        {
            bool found = false;
            foreach (clsInventory item in CurrentInventory)
            {

                if (string.Compare(item.ItemCode, itemCode, true) == 0)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }
    }

    [XmlRoot(ElementName = "inventorysummary", IsNullable = true)]
    [Serializable]
    public class InventorySummary
    {
        private clsInventory clsInventory;
        private int pendingSale, processedSale, shippedSale, days30, days60, days90;

        public InventorySummary(int PendingSale, int ProcessedSale, int ShippedSale)
        {
            clsInventory = new clsInventory();
            if (PendingSale > 0)
            {
                pendingSale = PendingSale;
            }

            if (ProcessedSale > 0)
            {
                processedSale = ProcessedSale;
            }

            if (ShippedSale > 0)
            {
                shippedSale = ShippedSale;
            }
        }


        public InventorySummary(int PendingSale, int ProcessedSale, int ShippedSale, int Days30, int Days60, int Days90)
        {
            clsInventory = new clsInventory();
            if (PendingSale > 0)
            {
                pendingSale = PendingSale;
            }

            if (ProcessedSale > 0)
            {
                processedSale = ProcessedSale;
            }

            if (ShippedSale > 0)
            {
                shippedSale = ShippedSale;
            }

            if (Days30 > 0)
            {
                days30 = Days30;
            }

            if (Days60 > 0)
            {
                days60 = Days60;
            }

            if (Days90 > 0)
            {
                days90 = Days90;
            }
        }


        public clsInventory Phone
        {
            set
            {
                clsInventory = value;
            }
            get
            {
                return clsInventory;
            }
        }

        public int PendingSale
        {
            get
            {
                return pendingSale;
            }
        }

        public int ProcessedSale
        {
            get
            {
                return processedSale;
            }
        }

        public int ShippedSale
        {
            get
            {
                return shippedSale;
            }
        }

        public int Days30
        {
            get
            {
                return days30;
            }
        }

        public int Days60
        {
            get
            {
                return days60;
            }
        }

        public int Days90
        {
            get
            {
                return days90;
            }
        }
    }

    [XmlRoot(ElementName = "inventory", IsNullable = true)]
    [Serializable]
    public class clsInventory
    {
        private int? _itemID = 0;
        private string _technology = string.Empty;
        private string _itemCode = string.Empty;
        private string _itemName = string.Empty;
        private string _itemType = string.Empty;
        private string _ItemDescription = string.Empty;
        private double _price = 0;
        private int _currentStock = 0;
        private string _warehouseCode = string.Empty;
        private double _warehouseCost = 0;
        private DateTime _lastdateTime;
        private bool? _phone = true;
        private bool? _active = true;
        private string _companyName = string.Empty;
        private string _upc = string.Empty;
        private string _maker = string.Empty;
        private string _model = string.Empty;
        private string _color = string.Empty;
        private string _sku = string.Empty;
        private bool _closeout = false;
        private int? _companyID;
        private List<InventoryItem> _inventoryItem;

        [XmlIgnore]
        public int? ItemID
        {
            get
            {
                return _itemID;
            }
            set
            {
                _itemID = value;
            }
        }

        [XmlElement(ElementName = "companyid", IsNullable = true)]
        public int? CompanyID
        {
            get
            {
                return _companyID;
            }
            set
            {
                _companyID = value;
            }
        }

        [XmlElement(ElementName = "companyname", IsNullable = true)]
        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
            }
        }

        [XmlElement(ElementName = "phone", IsNullable = true)]
        public bool? Phone
        {
            get
            {
                return _phone;
            }
            set
            {
                _phone = value;
            }
        }

        [XmlElement(ElementName = "active", IsNullable = true)]
        public bool? Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }
        
        [XmlElement(ElementName = "itemcode", IsNullable = true)]
        public string ItemCode
        {
            get
            {
                return _itemCode;
            }
            set
            {
                _itemCode = value;
            }
        }

        [XmlElement(ElementName = "devicetype", IsNullable = true)]
        public string DeviceType
        {
            get
            {
                return _itemType;
            }
            set
            {
                _itemType = value;
            }
        }

        [XmlElement(ElementName = "sku", IsNullable = true)]
        public string SKU
        {
            get
            {
                return _sku;
            }
            set
            {
                _sku = value;
            }
        }
        [XmlElement(ElementName = "upc", IsNullable = true)]
        public string UPC
        {
            get
            {
                return _upc;
            }
            set
            {
                _upc = value;
            }
        }

        [XmlElement(ElementName = "phonemaker", IsNullable = true)]
        public string PhoneMaker
        {
            get
            {
                return _maker;
            }
            set
            {
                _maker = value;
            }
        }

        [XmlElement(ElementName = "phonemodel", IsNullable = true)]
        public string PhoneModel
        {
            get
            {
                return _model;
            }
            set
            {
                _model = value;
            }
        }

        [XmlElement(ElementName = "color", IsNullable = true)]
        public string Color
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
            }
        }

        [XmlElement(ElementName = "itemname", IsNullable = true)]
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                _itemName = value;
            }
        }

        [XmlElement(ElementName = "itemdescription", IsNullable = true)]
        public string ItemDescription
        {
            get
            {
                return _ItemDescription;
            }
            set
            {
                _ItemDescription = value;
            }
        }

        //[XmlElement(ElementName = "price", IsNullable = true)]
        public Double Price
        {
            get
            {
                if (_price > 0)
                    return _price;
                else
                {

                    return 0;
                }
            }
            set
            {
                _price = value;
            }
        }

        //[XmlElement(ElementName = "currentstock", IsNullable = true)]
        public int CurrentStock
        {
            get
            {
                if (_currentStock > 0)
                    return _currentStock;   
                else
                   return 0;
            }
            set
            {
                _currentStock = value;
            }
        }

        [XmlElement(ElementName = "warehousecode", IsNullable = true)]
        public string WarehouseCode
        {
            get
            {
                return _warehouseCode;
            }
            set
            {
                _warehouseCode = value;
            }
        }

        [XmlElement(ElementName = "closeout", IsNullable = true)]
        public bool Closeout
        {
            get
            {
                return _closeout;
            }
            set
            {
                _closeout = value;
            }
        }

        [XmlElement(ElementName = "technology", IsNullable = true)]
        public string Technology
        {
            get
            {
                return _technology;
            }
            set
            {
                _technology = value;
            }
        }

        [XmlElement(ElementName = "warehousecost", IsNullable = true)]
        public double WarehouseCost
        {
            get
            {
                return _warehouseCost;
            }
            set
            {
                _warehouseCost = value;
            }
        }

        [XmlElement(ElementName = "lastupdate")]
        public DateTime  LastUpdated
        {
            get
            {
                return _lastdateTime;
            }
            set
            {
                _lastdateTime = value;
            }
        }

        [XmlArray("inventoryitems"), XmlArrayItem("inventoryitem", IsNullable = true)]
        public List<InventoryItem> InventoryItem
        {
            get
            {
                return _inventoryItem;
            }
            set
            {
                _inventoryItem = value;
            }
        }

        public clsInventory()
        {
            _inventoryItem = new List<InventoryItem>();
        }
    }

    [XmlRoot(ElementName = "inventoryitem", IsNullable = true)]
    [Serializable]
    public class InventoryItem
    {
        private string _esn;
        private string _boxId;

        [XmlElement(ElementName = "esn", IsNullable = true)]
        public string Esn
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
        
        [XmlElement(ElementName = "boxid", IsNullable = true)]
        public string BoxId
        {
            get
            {
                if (!string.IsNullOrEmpty(_boxId))
                {
                    return _boxId;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                _boxId = value;
            }
        }
    }

    public class InventoryItemRequest
    {
        private clsAuthentication _auth;
        
        public InventoryItemRequest()
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

    public class InventoryBadItemRequest
    {
        private clsAuthentication _auth;
        private List<InventoryItem> _items;

        public InventoryBadItemRequest()
        {
            _auth = new clsAuthentication();
            _items = new List<InventoryItem>();
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

        public List<InventoryItem> InventoryItems
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

    }

    public class InventoryItemResponse
    {
        private List<InventoryItem> _items;

        public InventoryItemResponse()
        {
            _items = new List<InventoryItem>();
         }

        public List<InventoryItem> InventoryItems
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
    }

    public class ServiceResponse
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
}
