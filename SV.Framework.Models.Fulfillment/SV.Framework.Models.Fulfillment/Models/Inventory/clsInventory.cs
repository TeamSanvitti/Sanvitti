using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Inventory
{

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
        public DateTime LastUpdated
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

}
