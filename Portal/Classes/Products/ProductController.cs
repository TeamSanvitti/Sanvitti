using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using avii.Classes;

namespace avii.Classes
{
    public class ProductType
    {
        public int ProductTypeID { get; set; }
        public string Code { get; set; }
    }
    public class ProductCondition
    {
        public int ConditionID { get; set; }
        public string Code { get; set; }
    }
    public class ItemCameraInfo
    {
        public int ItemCameraID { get; set; }
        public int ItemGUID { get; set; }
        public int CameraID { get; set; }
        public string CameraType { get; set; }
        public string Pixel { get; set; }
        public string Zoom { get; set; }
        
    }
    public class CameraConfig
    {
        
        public int CameraID { get; set; }
        public string Pixel { get; set; }
        public string Zoom { get; set; }
        public string CameraConfigs { get; set; }

    }
    [Serializable]
    public class CompanySKUno
    {
        private int? mappedItemCompanyGUID;
        private int itemcompanyGUID;
        private int itemGUID;
        private int companyID;
        private string sku;
        //private string massku;
        private string warehouseCode;
        private string companyname;
        private int poExists;
        private bool finishedSKU;
        private bool allowESN;
        private int categoryID;
        private int parentCategoryID;
        [XmlIgnore]
        public int CurrentStock { get; set; }

        [XmlIgnore]
        public bool IsDisable { get; set; }
        [XmlIgnore]
        public int MinimumStockLevel { get; set; }
        [XmlIgnore]
        public int MaximumStockLevel { get; set; }

        
        [XmlIgnore]
        public int ContainerQty { get; set; }
        [XmlIgnore]
        public int PalletQuantity { get; set; }
        [XmlIgnore]
        public string DPCI { get; set; }
        [XmlIgnore]
        public string SWVersion { get; set; }
        [XmlIgnore]
        public string HWVersion { get; set; }
        [XmlIgnore]
        public string BoxDesc { get; set; }

        ////private double price;
        [XmlIgnore]
        public int CategoryID
        {
            get
            {
                return categoryID;
            }
            set
            {
                categoryID = value;
            }
        }
        [XmlIgnore]
        public int ParentCategoryGUID
        {
            get
            {
                return parentCategoryID;
            }
            set
            {
                parentCategoryID = value;
            }
        }
        [XmlIgnore]
        public bool AllowESN
        {
            get
            {
                return allowESN;
            }
            set
            {
                allowESN = value;
            }
        }

        //public double Price
        //{
        //    get
        //    {
        //        return price;
        //    }
        //    set
        //    {
        //        price = value;
        //    }
        //}

        //public bool IsFinishedSKU
        //{
        //    get
        //    {
        //        return finishedSKU;
        //    }
        //    set
        //    {
        //        finishedSKU = value;
        //    }
        //}
        public int PoExists
        {
            get
            {
                return poExists;
            }
            set
            {
                poExists = value;
            }
        }
        
        public int ItemcompanyGUID
        {
            get
            {
                return itemcompanyGUID;
            }
            set
            {
                itemcompanyGUID = value;
            }
        }
        [XmlIgnore]
        public int? MappedItemCompanyGUID
        {
            get
            {
                return mappedItemCompanyGUID;
            }
            set
            {
                mappedItemCompanyGUID = value;
            }
        }
        
        public int ItemGUID
        {
            get
            {
                return itemGUID;
            }
            set
            {
                itemGUID = value;
            }
        }
        public int CompanyID
        {
            get
            {
                return companyID;
            }
            set
            {
                companyID = value;
            }
        }
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
        [XmlIgnore]
        public string MappedSKU { get; set; }
        //public string MASSKU
        //{
        //    get
        //    {
        //        return massku;
        //    }
        //    set
        //    {
        //        massku = value;
        //    }
        //}
        public string WarehouseCode
        {
            get
            {
                return warehouseCode;
            }
            set
            {
                warehouseCode = value;
            }
        }
        public string CompanyName
        {
            get
            {
                return companyname;
            }
            set
            {
                companyname = value;
            }
        }
    }
    public class CustomerLogin
    {
        private int customerLoginID;
        private int customerID;
        private string password;
        private string email;
        private DateTime dateStamp;
        private int active;


        public int CustomerLoginID
        {
            get
            {
                return customerLoginID;
            }
            set
            {
                customerLoginID = value;
            }
        }
        public int CustomerID
        {
            get
            {
                return customerID;
            }
            set
            {
                customerID = value;
            }
        }
        public int Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }
        public DateTime DateStamp
        {
            get
            {
                return dateStamp;
            }
            set
            {
                dateStamp = value;
            }
        }

    }
    public class Customer
    {
        private int customerGUID;
        private string customerName;
        private string customerAddress1;
        private string customerAddress2;
        private string customerCity;
        private string customerState;
        private string customerZip;
        private string shippingAddress1;
        private string shippingAddress2;
        private string shippingCity;
        private string shippingState;
        private string shippingZip;
        private string shippingContactName;

        public int CustomerGUID
        {
            get
            {
                return customerGUID;
            }
            set
            {
                customerGUID = value;
            }
        }
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
            }
        }
        public string CustomerAddress1
        {
            get
            {
                return customerAddress1;
            }
            set
            {
                customerAddress1 = value;
            }
        }
        public string CustomerAddress2
        {
            get
            {
                return customerAddress2;
            }
            set
            {
                customerAddress2 = value;
            }
        }
        public string CustomerCity
        {
            get
            {
                return customerCity;
            }
            set
            {
                customerCity = value;
            }
        }
        public string CustomerState
        {
            get
            {
                return customerState;
            }
            set
            {
                customerState = value;
            }
        }
        public string CustomerZip
        {
            get
            {
                return customerZip;
            }
            set
            {
                customerZip = value;
            }
        }
        public string ShippingAddress1
        {
            get
            {
                return shippingAddress1;
            }
            set
            {
                shippingAddress1 = value;
            }
        }
        public string ShippingAddress2
        {
            get
            {
                return shippingAddress2;
            }
            set
            {
                shippingAddress2 = value;
            }
        }
        public string ShippingCity
        {
            get
            {
                return shippingCity;
            }
            set
            {
                shippingCity = value;
            }
        }
        public string ShippingContactName
        {
            get
            {
                return shippingContactName;
            }
            set
            {
                shippingContactName = value;
            }
        }
        public string ShippingState
        {
            get
            {
                return shippingState;
            }
            set
            {
                shippingState = value;
            }
        }

        public string ShippingZip
        {
            get
            {
                return shippingZip;
            }
            set
            {
                shippingZip = value;
            }
        }

    }
    public class InventoryItemCSV
    {
        public string CustomerName { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string ModelNumber { get; set; }
        public string UPC { get; set; }

    }
    public class InventoryItems
    {
        #region "Private Attributes"
        private int itemGUID;
        private string itemCode;
        private string itemName;
        private InventoryItemType itemType;
        private int _itemType;
        private string technology;
        private int technologyID;
        private int categoryGUID;
        private int parentcategoryGUID;
        private string category;
        private DeviceTypeCategory deviceTypeCatg;
        private string deviceTypeName;
        private int deviceTypeGuid;
        private string modelNumber;
        private string itemDesc1;
        private string itemDesc2;
        private string upc;
        private string itemsize;
        private string itemprice;
        private string itemMinprice;
        private string makeritems;
        private string itemcolor;
        private int makerGUID;
        private string maker;
        private string gender;
        private bool active;
        private int companyGUID;
        private string sku;
        private string createdBy;
        private string modifiedBy;
        private DateTime createdDate;
        private DateTime modifiedDate;
        private List<InventoryItemUsage> itemUsage;
        private List<InventoryItemSpecifications> itemSpecs;
        private List<InventoryItemCarrier> itemCarriers;
        private List<InventoryItemImages> itemImages;
        private List<InventoryItemSizes> itemSizes;
        private bool allowRMA;
        private bool showunderCatalog;
        private string itemDocument;
        private bool allowLTE;
        private bool allowSIM;
        private double defaultPrice;

        private bool isKitted;


        #endregion

        #region "Public Constructors"

        public InventoryItems()
        {
            itemUsage = new List<InventoryItemUsage>();
            itemSpecs = new List<InventoryItemSpecifications>();
            itemCarriers = new List<InventoryItemCarrier>();
        }

        #endregion

        #region "Public Properties"

        public string OSType { get; set; }
        public string ProductType { get; set; }
        public string Condition { get; set; }
        public int ProductTypeID { get; set; }
        public int ConditionID { get; set; }
        public bool ReStock { get; set; }
        public int EsnLength { get; set; }
        public int MeidLength { get; set; }

        public bool IsDisplayName { get; set; }
        public string Storage { get; set; }
        public int StorageQty { get; set; }
        public string CategoryWithProductAllowed { get; set; }
        public int OperationSystem { get; set; }
        public string ScreenSize { get; set; }
         public int DisplayPriority { get; set; }
         public int SpintorLockTypeID { get; set; }
         public int SimCardTypeID { get; set; }
        public decimal Weight { get; set; }

        public bool AllowESN { get; set; }
        public int ItemCompanyGUID { get; set; }
        public bool IsDisable { get; set; }
        public string CompanyName { get; set; }
        public double DefaultPrice
        {
            get
            {
                return defaultPrice;
            }
            set
            {
                defaultPrice = value;
            }

        }
        public int ItemGUID
        {
            get
            {
                return itemGUID;
            }
            set
            {
                itemGUID = value;
            }

        }
        public int Item_Type
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
        public int CompanyGUID
        {
            get
            {
                return companyGUID;
            }
            set
            {
                companyGUID = value;
            }

        }
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
        public int TechnologyID
        {
            get
            {
                return technologyID;
            }
            set
            {
                technologyID = value;
            }
        }
        public string ItemName
        {
            get
            {
                return itemName;
            }
            set
            {
                itemName = value;
            }
        }
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                gender = value;
            }
        }

        public string MakerItems
        {
            get
            {
                return makeritems;
            }
            set
            {
                makeritems = value;
            }
        }
        public string ItemSize
        {
            get
            {
                return itemsize;
            }
            set
            {
                itemsize = value;
            }
        }

        public string ItemPrice
        {
            get
            {
                return itemprice;
            }
            set
            {
                itemprice = value;
            }
        }
        public string ItemMinPrice
        {
            get
            {
                return itemMinprice;
            }
            set
            {
                itemMinprice = value;
            }
        }

        public int DeviceTypeGuid
        {
            get
            {
                return deviceTypeGuid;
            }
            set
            {
                deviceTypeGuid = value;
            }
        }

        public string ItemCode
        {
            get
            {
                return itemCode;
            }
            set
            {
                itemCode = value;
            }
        }
        public string ItemColor
        {
            get
            {
                return itemcolor;
            }
            set
            {
                itemcolor = value;
            }
        }

        public InventoryItemType ItemType
        {
            get
            {
                return itemType;
            }
            set
            {
                itemType = value;
            }
        }

        public string ItemTechnology
        {
            get
            {
                return technology;
            }
            set
            {
                technology = value;
            }
        }

        public int ParentCategoryGUID
        {
            get
            {
                return parentcategoryGUID;
            }
            set
            {
                parentcategoryGUID = value;
            }
        }
        public int ItemCategoryGUID
        {
            get
            {
                return categoryGUID;
            }
            set
            {
                categoryGUID = value;
            }
        }
        public bool IsESNRequired { get; set; }
        public string ItemCategory
        {
            get
            {
                return category;
            }
            set
            {
                category = value;
            }
        }

        public string DeviceTypeName
        {
            get
            {
                return deviceTypeName;
            }
            set
            {
                deviceTypeName = value;
            }
        }

        public DeviceTypeCategory DeviceType
        {
            get
            {
                return deviceTypeCatg;
            }
            set
            {
                deviceTypeCatg = value;
            }
        }

        public string ModelNumber
        {
            get
            {
                return modelNumber;
            }
            set
            {
                modelNumber = value;
            }
        }

        public string ItemDesc1
        {
            get
            {
                return itemDesc1;
            }
            set
            {
                itemDesc1 = value;
            }
        }

        public string ItemDesc2
        {
            get
            {
                return itemDesc2;
            }
            set
            {
                itemDesc2 = value;
            }
        }

        public string Upc
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

        public int ItemMakerGUID
        {
            get
            {
                return makerGUID;
            }
            set
            {
                makerGUID = value;
            }
        }

        public string ItemMaker
        {
            get
            {
                return maker;
            }
            set
            {
                maker = value;
            }
        }

        public string CreatedBy
        {
            get
            {
                return createdBy;
            }
            set
            {
                createdBy = value;
            }
        }

        public string ModifiedBy
        {
            get
            {
                return modifiedBy;
            }
            set
            {
                modifiedBy = value;
            }
        }

        public bool Active
        {
            get
            {
                return active;
            }
            set
            {
                active = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return createdDate;
            }
            set
            {
                createdDate = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return modifiedDate;
            }
            set
            {
                modifiedDate = value;
            }
        }

        public List<InventoryItemUsage> ItemUsage
        {
            get
            {
                return itemUsage;
            }
            set
            {
                itemUsage = value;
            }
        }

        public List<InventoryItemSpecifications> ItemSpecifications
        {
            get
            {
                return itemSpecs;
            }
            set
            {
                itemSpecs = value;
            }
        }

        public List<InventoryItemCarrier> ItemCarriers
        {
            get
            {
                return itemCarriers;
            }
            set
            {
                itemCarriers = value;
            }
        }

        public List<InventoryItemImages> ItemImages
        {
            get
            {
                return itemImages;
            }
            set
            {
                itemImages = value;
            }
        }

        public List<InventoryItemSizes> ItemSizes
        {
            get
            {
                return itemSizes;
            }
            set
            {
                itemSizes = value;
            }
        }
        public bool AllowRMA
        {
            get
            {
                return allowRMA;
            }
            set
            {
                allowRMA = value;
            }
        }
        public bool AllowLTE
        {
            get
            {
                return allowLTE;
            }
            set
            {
                allowLTE = value;
            }
        }
        public bool AllowSIM
        {
            get
            {
                return allowSIM;
            }
            set
            {
                allowSIM = value;
            }
        }
        public bool IsKitted
        {
            get
            {
                return isKitted;
            }
            set
            {
                isKitted = value;
            }
        }
        
        public bool ShowunderCatalog
        {
            get
            {
                return showunderCatalog;
            }
            set
            {
                showunderCatalog = value;
            }
        }
        public string ItemDocument
        {
            get
            {
                return itemDocument;
            }
            set
            {
                itemDocument = value;
            }
        }
        
        #endregion

        #region "Public Methods"

        public InventoryItems LoadData(DataSet dataSet)
        {
            // Populate the object from the dataset
            InventoryItems invItem = new InventoryItems();

            return invItem;

        }

        public ReturnStatus SetData(InventoryItems inventoryItem)
        {
            // Call the Database object to save the data to the database. I advice you to use OPENXML to save the data to the SQLServer database. 
            // OPENXML will provide more control on the database transaction and data integrity.
            ReturnStatus returnStatus = ReturnStatus.NoActionTaken;
            InventoryItems invItem = new InventoryItems();

            return returnStatus;
        }

        #endregion
    }

    [Serializable]
    public class ItemCategory
    {
        private string _categoryDescription;
        private int _categoryGUID;
        private string _categoryImage;
        private string _categoryName;
        private string _parentcategoryName;
        private string _comments;
        private int _parentCategoryGUID;
        private int _active;
        private bool isESNRequired;
        public string CategoryWithProductAllowed { get; set; }
        public int CategoryGUID
        {
            get
            {
                return _categoryGUID;
            }
            set
            {
                _categoryGUID = value;
            }
        }

        public string ParentCategoryName
        {
            get
            {
                return _parentcategoryName;
            }
            set
            {
                _parentcategoryName = value;
            }
        }
        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                _categoryName = value;
            }
        }

        public string CategoryDescription
        {
            get
            {
                return _categoryDescription;
            }
            set
            {
                _categoryDescription = value;
            }
        }

        public string CategoryImage
        {
            get
            {
                return _categoryImage;
            }
            set
            {
                _categoryImage = value;
            }
        }

        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
            }
        }

        public int ParentCategoryGUID
        {
            get
            {
                return _parentCategoryGUID;
            }
            set
            {
                _parentCategoryGUID = value;
            }
        }

        public int isActive
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
        public bool IsESNRequired
        {
            get
            {
                return isESNRequired;
            }
            set
            {
                isESNRequired = value;
            }
        }
    }

    public class products
    {
        private int _itemGUID;
        private int  _categoryID;
        private string _itemCode;
        private string _modelNumber;
        private string _itemName;
        private string _itemDescription;
        private string _color;
        private int _createdBy;
        //private DateTime _createdDate;
        private int _modifiedBy; 
        //private DateTime _modifiedDate;
        private Boolean _active;

        public int ItemGUID
        {
            get
            {
                return _itemGUID;
            }
            set
            {
                _itemGUID = value;
            }
        }
        public int CategoryID
        {
            get
            {
                return _categoryID;
            }
            set
            {
                _categoryID = value;
            }
        }
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

        public string ModelNumber
        {
            get
            {
                return _modelNumber;
            }
            set
            {
                _modelNumber = value;
            }
        }
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

        public string ItemDescription
        {
            get
            {
                return _itemDescription;
            }
            set
            {
                _itemDescription = value;
            }
        }

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

        public int CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                _createdBy = value;
            }
        }

        public int ModifiedBy
        {
            get
            {
                return _modifiedBy;
            }
            set
            {
                _modifiedBy = value;
            }
        }

        public Boolean Active
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
    }

    public class Maker
    {
        private int _makerGUID;
        private string _makerName;
        private string _makerDescription;
        private string _makerImage;
        private string _makerComments;
        private int _parentGUID;
        private int _active;
        private int _isalphabet;

        public int MakerGUID
        {
            get
            {
                return _makerGUID;
            }
            set
            {
                _makerGUID = value;
            }
        }
        public int Isalphabet
        {
            get
            {
                return _isalphabet;
            }
            set
            {
                _isalphabet = value;
            }
        }

        public string MakerName
        {
            get
            {
                return _makerName;
            }
            set
            {
                _makerName = value;
            }
        }

        public string MakerDescription
        {
            get
            {
                return _makerDescription;
            }
            set
            {
                _makerDescription = value;
            }
        }

        public string MakerImage
        {
            get
            {
                return _makerImage;
            }
            set
            {
                _makerImage = value;
            }
        }

        public string MakerComments
        {
            get
            {
                return _makerComments;
            }
            set
            {
                _makerComments = value;
            }
        }

        public int ParentGUID
        {
            get
            {
                return _parentGUID;
            }
            set
            {
                _parentGUID = value;
            }
        }

        public int isActive
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
    }
}
    public class InventoryItemPricing
    {
        private int itemGUID;
        private int pricingGUID;
        private int custTypeGuid;
        private string custType;
        private double webPrice;
        private double retailPrice;
        private double wholeSalePrice;
        private ItemPricingType priceType;
        private string _itemSize;
        private string _itemSizeprice;
        private string _itempricensize;
        private double itemprice;
        private int _itemsizeguid;
        private int _sizeguid;

        public int ItemGUID
        {
            get
            {
                return itemGUID;
            }
            set
            {
                itemGUID = value;
            }
        }
        public int SizeGUID
        {
            get
            {
                return _sizeguid;
            }
            set
            {
                _sizeguid = value;
            }
        }
        public int ItemsizeGUID
        {
            get
            {
                return _itemsizeguid;
            }
            set
            {
                _itemsizeguid = value;
            }
        }
        public int PricingGUID
        {
            get
            {
                return pricingGUID;
            }
            set
            {
                pricingGUID = value;
            }
        }

        public int CustTypeGuid
        {
            get
            {
                return custTypeGuid;
            }
            set
            {
                custTypeGuid = value;
            }
        }

        public string CustType
        {
            get
            {
                return custType;
            }
            set
            {
                custType = value;
            }
        }
        public string ItemSizeprice
        {
            get
            {
                return _itemSizeprice;
            }
            set
            {
                _itemSizeprice = value;
            }
        }
        public string ItemPricenSize
        {
            get
            {
                return _itempricensize;
            }
            set
            {
                _itempricensize = value;
            }
        }


        public double Itemprice
        {
            get
            {
                return itemprice;
            }
            set
            {
                itemprice = value;
            }
        }

        public double WebPrice
        {
            get
            {
                return webPrice;
            }
            set
            {
                webPrice = value;
            }
        }

        public double RetailPrice
        {
            get
            {
                return retailPrice;
            }
            set
            {
                retailPrice = value;
            }
        }

        public double WholeSalePrice
        {
            get
            {
                return wholeSalePrice;
            }
            set
            {
                wholeSalePrice = value;
            }
        }

        public ItemPricingType PriceType
        {
            get
            {
                return priceType;
            }
            set
            {
                priceType = value;
            }
        }

        public string ItemSize
        {
            get
            {
                return _itemSize;
            }
            set
            {
                _itemSize = value;
            }
        }
    }

    public class InventoryItemUsage
    {
        private int companyGuid;
        private string companyName;
        private string sku;
        private List<InventoryItemPricing> itemPricing;

        public int CompanyGuid
        {
            get
            {
                return companyGuid;
            }
            set
            {
                companyGuid = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
            }
        }

        public string Sku
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

        public List<InventoryItemPricing> ItemPricing
        {
            get
            {
                return itemPricing;
            }
            set
            {
                itemPricing = value;
            }
        }

    }

    public class InventoryItemSpecifications
    {
        private int specGuid;
        private string spec;
        private int itemGUID;

        public int SpecificationGuid
        {
            get
            {
                return specGuid;
            }
            set
            {
                specGuid = value;
            }
        }

        public string Specificaiton
        {
            get
            {
                return spec;
            }
            set
            {
                spec = value;
            }
        }
        public int ItemGUID
        {
            get
            {
                return itemGUID;
            }
            set
            {
                itemGUID = value;
            }
        }

        //public int ItemGUID
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //    }
        //}
    }

    public class InventoryItemCarrier
    {
        private int carGuid;
        private string carrierName;
        private string carrierLogo;

        public int CarrierGuid
        {
            get
            {
                return carGuid;
            }
            set
            {
                carGuid = value;
            }
        }

        public string CarrierName
        {
            get
            {
                return carrierName;
            }
            set
            {
                carrierName = value;
            }
        }

        public string CarrierLogo
        {
            get
            {
                return carrierLogo;
            }
            set
            {
                carrierLogo = value;
            }
        }
    }

    //public class InventoryItemImages
    //{
    //    private int itemGUID;
    //    private int imageGUID;
    //    private ImageType imageType;
    //    private string imageName;
    //    private string imageURL;

    //    public int ImageGUID
    //    {
    //        get
    //        {
    //            return imageGUID;
    //        }
    //        set
    //        {
    //            imageGUID = value;
    //        }
    //    }


    //    public int ItemGUID
    //    {
    //        get
    //        {
    //            return itemGUID;
    //        }
    //        set
    //        {
    //            itemGUID = value;
    //        }
    //    }

    //    public ImageType ImageType
    //    {
    //        get
    //        {
    //            return imageType;
    //        }
    //        set
    //        {
    //            imageType = value;
    //        }
    //    }

    //    public string ImageName
    //    {
    //        get
    //        {
    //            return imageName;
    //        }
    //        set
    //        {
    //            imageName = value;
    //        }
    //    }

    //    public string ImageURL
    //    {
    //        get
    //        {
    //            return imageURL;
    //        }
    //        set
    //        {
    //            imageURL = value;
    //        }
    //    }
    //}

    //public class InventoryItemSizes
    //{
    //    private int itemGUID;
    //    private int sizeGUID;
    //    private AvailableType avail;
    //    private string sizeTxt;


    //    public int SizeGUID
    //    {
    //        get
    //        {
    //            return sizeGUID;
    //        }
    //        set
    //        {
    //            sizeGUID = value;
    //        }
    //    }


    //    public int ItemGUID
    //    {
    //        get
    //        {
    //            return itemGUID;
    //        }
    //        set
    //        {
    //            itemGUID = value;
    //        }
    //    }

    //    public AvailableType Availability
    //    {
    //        get
    //        {
    //            return avail;
    //        }
    //        set
    //        {
    //            avail = value;
    //        }
    //    }

    //    public string SizeText
    //    {
    //        get
    //        {
    //            return sizeTxt;
    //        }
    //        set
    //        {
    //            sizeTxt = value;
    //        }
    //    }
    //}
    public class InventoryItemColor
    {
        private int colorGUID;
        private string color;
        private int itemGUID;



        public int ColorGUID
        {
            get
            {
                return colorGUID;
            }
            set
            {
                colorGUID = value;
            }
        }

        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        public int ItemGUID
        {
            get
            {
                return itemGUID;
            }
            set
            {
                itemGUID = value;
            }
        }
    }

    #region inventorySizes
    [XmlRoot(ElementName = "ModulePermission", IsNullable = true)]
    public class InventoryItemSizes
    {
        private int? itemGUID;
        private int? sizeGUID;
        private int? size_GUID;
        private string size;
        private AvailableType? avail;
        private string comment;
        private string _itemsizetype;
        private string weight;
        private int? _itemimageGUID;
        //private int colorGUID;
        //private string color;

        //[XmlElement(ElementName = "colorGUID", IsNullable = true)]
        //public int ColorGUID
        //{
        //    get
        //    {
        //        return colorGUID;
        //    }
        //    set
        //    {
        //        colorGUID = value;
        //    }
        //}
        //[XmlElement(ElementName = "color", IsNullable = true)]
        public string ItemSizetype
        {
            get
            {
                return _itemsizetype;
            }
            set
            {
                _itemsizetype = value;
            }
        }
        public string Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        [XmlElement(ElementName = "itemGUID", IsNullable = true)]
        public int? ItemGUID
        {
            get
            {
                return itemGUID;
            }
            set
            {
                itemGUID = value;
            }
        }
        [XmlElement(ElementName = "itemGUID", IsNullable = true)]
        public int? ItemimageGUID
        {
            get
            {
                return _itemimageGUID;
            }
            set
            {
                _itemimageGUID = value;
            }
        }
        [XmlElement(ElementName = "size_GUID", IsNullable = true)]
        public int? Size_GUID
        {
            get
            {
                return size_GUID;
            }
            set
            {
                size_GUID = value;
            }
        }


        [XmlElement(ElementName = "sizeGUID", IsNullable = true)]
        public int? SizeGUID
        {
            get
            {
                return sizeGUID;
            }
            set
            {
                sizeGUID = value;
            }
        }
        [XmlElement(ElementName = "sizeGUID", IsNullable = true)]
        public string Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }
        [XmlElement(ElementName = "availableType", IsNullable = true)]
        public AvailableType? Available
        {
            get
            {
                return avail;
            }
            set
            {
                avail = value;
            }
        }

        [XmlElement(ElementName = "sizeText", IsNullable = true)]
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

    #endregion

    #region inventoryImages
    [XmlRoot(ElementName = "ItemImaes", IsNullable = true)]
    public class InventoryItemImages
    {
        private int? itemGUID;
        private int? imageGUID;
        private int? itemsizeGUID;
        private ImageType? imageType;
        private string imageName;
        private string imageURL;
        private string color;
        private int colorGuid;
        private string weight;
        [XmlElement(ElementName = "imageGUID", IsNullable = true)]
        public int? ImageGUID
        {
            get
            {
                return imageGUID;
            }
            set
            {
                imageGUID = value;
            }
        }
        [XmlElement(ElementName = "ItemSizeGUID", IsNullable = true)]
        public int? ItemSizeGUID
        {
            get
            {
                return itemsizeGUID;
            }
            set
            {
                itemsizeGUID = value;
            }
        }

        [XmlElement(ElementName = "weight", IsNullable = true)]
        public string Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        [XmlElement(ElementName = "itemGUID", IsNullable = true)]
        public int? ItemGUID
        {
            get
            {
                return itemGUID;
            }
            set
            {
                itemGUID = value;
            }
        }

        [XmlElement(ElementName = "imageType", IsNullable = true)]
        public ImageType? ImageType
        {
            get
            {
                return imageType;
            }
            set
            {
                imageType = value;
            }
        }

        [XmlElement(ElementName = "imageName", IsNullable = true)]
        public string ImageName
        {
            get
            {
                return imageName;
            }
            set
            {
                imageName = value;
            }
        }

        [XmlElement(ElementName = "imageURL", IsNullable = true)]
        public string ImageURL
        {
            get
            {
                return imageURL;
            }
            set
            {
                imageURL = value;
            }
        }
        [XmlElement(ElementName = "imagecolor", IsNullable = true)]
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        [XmlElement(ElementName = "imagecolorID", IsNullable = true)]
        public int ColorGUID
        {
            get
            {
                return colorGuid;
            }
            set
            {
                colorGuid = value;
            }
        }
    }

    #endregion

    public class InventoryItemSizess
    {
        private List<InventoryItemSizes> inventoryItemSizess;
        public List<InventoryItemSizes> InventoryItemSizeList
        {
            get
            {
                return inventoryItemSizess;
            }
            set
            {
                inventoryItemSizess = value;
            }
        }
    }
    public class InventoryItemImagess
    {
        private List<InventoryItemImages> inventoryItemImagess;
        public List<InventoryItemImages> InventoryItemImageList
        {
            get
            {
                return inventoryItemImagess;
            }
            set
            {
                inventoryItemImagess = value;
            }
        }
    }
    
    #region ENUM


    
    //public enum ScreenSize
    //{
    //    None = 0,
    //    4" = 1,
    //    Basic = 2,
    //    iOS = 3
    //}

    
    public enum AvailableType
    {
        Available = 0,
        NotAvailable = 1,
        Cancelled = 2,
        Other = 3
    }

    public enum ImageType
    {
        Main = 0,
        front = 1,
        Accessories = 2,
        Other = 3
    }
    public enum ReturnStatus
    {
        NoActionTaken = 0,
        SuccessfullyCompleted = 1,
        ApplicationExceptionOccured = 2,
        SystemExceptionOccured = 3,
        DatabaseConnectivityException = 4,
        DatabaseObjectCallException = 5
    }
    public enum ItemPricingType
    {
        Regular = 1,
        Special = 2

    }
    public enum InventoryItemType
    {
        New = 1,
        Refurbish = 2,
        PreOwned = 3
    }

    public enum InventoryItemTechnology
    {
        GSM = 1,
        CDMA = 2,
        NotApplicable = 0
    }

    public enum InventoryItemCategory
    {
        OEM = 1,
        Debrand = 2,
    }

    public enum DeviceTypeCategory
    {
        Phone = 1,
        Accessory = 2,
        Other = 0
    }
    #endregion
    public class ProductController
    {
        
        protected int _itemGUID;
        protected List<InventoryItemImages> objItemImagesList = new List<InventoryItemImages>();
        protected List<InventoryItemSizes> objItemSizesList = new List<InventoryItemSizes>();
        public ProductController()
        { }

        public int ItemGUID
        {
            get
            {
                return _itemGUID;
            }
            set
            {
                _itemGUID = value;
            }
        }



        #region Currently not in use
        public List<avii.Classes.ItemCategory> getItemCategoryList(int ctegoryGUID, string scatname
            , int hasitems, bool withparent, int active)
        {
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            List<avii.Classes.ItemCategory> lstItemCategoryList = new List<avii.Classes.ItemCategory>();
            avii.Classes.ItemCategory objItemCategory;
            
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@categoryGUID", ctegoryGUID);
                objCompHash.Add("@hasitems", hasitems);
                objCompHash.Add("@categoryName", scatname);
                objCompHash.Add("@active", active);

                arrSpFieldSeq = new string[] { "@categoryGUID", "@hasitems", "@categoryName", "@active" };
                ds = objDB.GetDataSet(objCompHash, "ItemCategory_Select", arrSpFieldSeq);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    int iteratefrom = 1;
                    if (withparent)
                        iteratefrom = 0;
                    for(int ictr=iteratefrom; ictr<ds.Tables[0].Rows.Count; ictr++)
                    {
                        DataRow dataRow = ds.Tables[0].Rows[ictr];

                        objItemCategory = new avii.Classes.ItemCategory();
                        objItemCategory.CategoryGUID = Convert.ToInt32(dataRow["categoryGUID"]);
                        objItemCategory.CategoryName = dataRow["categoryName"].ToString();
                        objItemCategory.CategoryDescription = dataRow["categoryDescription"].ToString();
                        objItemCategory.CategoryImage = dataRow["categoryImage"].ToString();
                        objItemCategory.Comments = dataRow["comments"].ToString();
                        objItemCategory.ParentCategoryGUID = Convert.ToInt32( dataRow["ParentCategoryGUID"]);
                        objItemCategory.isActive = Convert.ToInt32(dataRow["active"]);
                        
                        lstItemCategoryList.Add(objItemCategory);
                    }
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return lstItemCategoryList;
        }
        #endregion
        //#region to get the category tabs and their contents
        //public Hashtable getcategoryitemlist()
        //{

        //    List<avii.Classes.ItemCategory> categorylist = getItemsCategoryList(-1, "", 1, true, 1, -1, false, 1,"","");
            
            
        //    Hashtable objCompHash = new Hashtable();
        //    for (int i = 0; i < categorylist.Count; i++)
        //    {
        //        List<avii.Classes.InventoryItem> itemlist = getItemList(Convert.ToInt32(categorylist[i].CategoryGUID), -1, 1, Convert.ToInt32(categorylist[i].ParentCategoryGUID), 1, -1, "", "-1", -1, 1, categorylist[i].CategoryName.ToString(), "");
        //        objCompHash.Add(categorylist[i].ParentCategoryName, itemlist);
        //    }

        //    return objCompHash;

        //}
        //#endregion
        #region getItemsCategoryList --
        public List<avii.Classes.ItemCategory> getItemsCategoryList(int categoryGUID, string scatname
            , int hasitems, bool withparent, int active, int parentcategoryGUID, bool excludeKitted, bool OnlyNonEsn, bool IsEsnRequired)
        {
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            List<avii.Classes.ItemCategory> lstItemCategoryList = new List<avii.Classes.ItemCategory>();
            avii.Classes.ItemCategory objItemCategory;

            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
        try
        {
            int catid = categoryGUID;
            if (parentcategoryGUID > 0)
                catid = parentcategoryGUID;

            objCompHash.Add("@CategoryGUID", categoryGUID);
            //objCompHash.Add("@hasitems", hasitems);
            objCompHash.Add("@CategoryName", scatname);
            //objCompHash.Add("@active", active);
            //objCompHash.Add("@tabflag", tabflag);
            //objCompHash.Add("@categoryName1", categoryMen);
            //objCompHash.Add("@categoryName2", categoryWomen);
            objCompHash.Add("@excludeKitted", excludeKitted);
            objCompHash.Add("@OnlyNonESN", OnlyNonEsn);
            objCompHash.Add("@OnlyESN", IsEsnRequired);
            

            //arrSpFieldSeq = new string[] { "@categoryGUID", "@hasitems", "@categoryName", "@active", "@categoryName1", "@categoryName2" };
            arrSpFieldSeq = new string[] { "@CategoryGUID", "@CategoryName", "@excludeKitted", "@OnlyNonESN", "@OnlyESN" };
            ds = objDB.GetDataSet(objCompHash, "av_itemcategory_select", arrSpFieldSeq);

            //if (formenu)
            //{
            //    avii.Classes.ItemCategory itemViewAll = new avii.Classes.ItemCategory();
            //    itemViewAll.CategoryGUID = -1;
            //    itemViewAll.CategoryName = "View All";
            //    itemViewAll.CategoryDescription = "";
            //    itemViewAll.CategoryImage = "";
            //    itemViewAll.Comments = "";
            //    itemViewAll.ParentCategoryGUID = parentcategoryGUID;
            //    itemViewAll.ParentCategoryName = "";
            //    lstItemCategoryList.Add(itemViewAll);

            //    avii.Classes.ItemCategory itemNewArrival = new avii.Classes.ItemCategory();
            //    itemNewArrival.CategoryGUID = -2;
            //    itemNewArrival.CategoryName = "New Arrivals";
            //    itemNewArrival.CategoryDescription = "";
            //    itemNewArrival.CategoryImage = "";
            //    itemNewArrival.Comments = "";
            //    itemNewArrival.ParentCategoryGUID = parentcategoryGUID;
            //    itemNewArrival.ParentCategoryName = "";
            //    lstItemCategoryList.Add(itemNewArrival);
            //}
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int iteratefrom = 1;
                if (withparent)
                    iteratefrom = 0;
                for (int ictr = iteratefrom; ictr < ds.Tables[0].Rows.Count; ictr++)
                {
                    DataRow dataRow = ds.Tables[0].Rows[ictr];

                    objItemCategory = new avii.Classes.ItemCategory();
                    objItemCategory.CategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryGUID", 0, false));
                    objItemCategory.CategoryName = clsGeneral.getColumnData(dataRow, "categoryName", string.Empty, false) as string;
                    //objItemCategory.ParentCategoryName = clsGeneral.getColumnData(dataRow, "parentcatname", string.Empty, false) as string;
                    objItemCategory.CategoryDescription = clsGeneral.getColumnData(dataRow, "categoryDescription", string.Empty, false) as string;
                    objItemCategory.CategoryImage = clsGeneral.getColumnData(dataRow, "categoryImage", string.Empty, false) as string;
                    objItemCategory.Comments = clsGeneral.getColumnData(dataRow, "comments", string.Empty, false) as string;
                    objItemCategory.ParentCategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ParentCategoryGUID", 0, false));
                    //objItemCategory.isActive = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "active", 0, false)); 
                    objItemCategory.IsESNRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESNRequired", false, false));
                    objItemCategory.CategoryWithProductAllowed = Convert.ToString(clsGeneral.getColumnData(dataRow, "CategoryWithProductAllowed", string.Empty, false));

                    lstItemCategoryList.Add(objItemCategory);
                }
            }
        }
        catch (Exception objExp)
        {
            throw new Exception(objExp.Message.ToString());
        }
        finally
        {
            objDB.DBClose();
            objCompHash = null;
            arrSpFieldSeq = null;
        }
            return lstItemCategoryList;
        }
        #endregion

        #region getItemCategoryTree
        public List<avii.Classes.ItemCategory> getItemCategoryTree(int catID, int depth, int hasitems
            , bool withparent, int active, int parentcatID, bool withIndent, bool excludeKitted, bool OnlyNonEsn, bool IsEsnRequired)
        {
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            if (0 == catID)
                catID = -1;
            List<avii.Classes.ItemCategory> lstItemCategoryTree = new List<avii.Classes.ItemCategory>();

            traverseCategories(ref lstItemCategoryTree, getItemsCategoryList(catID, "", hasitems, withparent, active
                , parentcatID, excludeKitted, OnlyNonEsn, IsEsnRequired), 0, 0, withIndent);

            return lstItemCategoryTree;
        }

        #endregion

        #region traverseCategories
        public List<avii.Classes.ItemCategory> traverseCategories(ref List<avii.Classes.ItemCategory> itemcategoryListTree
            , List<avii.Classes.ItemCategory> itemcategoryList, int catid, int depth, bool withIndent)
        {
            for (int ictr = 0; ictr < itemcategoryList.Count; ictr++)
            {
                if (catid == itemcategoryList[ictr].ParentCategoryGUID)
                {
                    for (int j = 0; j < depth; j++)
                    {
                        if (withIndent)
                            itemcategoryList[ictr].CategoryName = "&nbsp;&nbsp;&nbsp;" + itemcategoryList[ictr].CategoryName;
                        else
                            itemcategoryList[ictr].CategoryName = itemcategoryList[ictr].CategoryName;
                    }

                    itemcategoryListTree.Add(itemcategoryList[ictr]);
                    itemcategoryListTree = traverseCategories(ref itemcategoryListTree, itemcategoryList, itemcategoryList[ictr].CategoryGUID, depth + 1, withIndent);
                }
            }
            return itemcategoryListTree;
        }
        #endregion

        #region getMakerList -- 
        public List<avii.Classes.Maker> getMakerList(int makerGUID, string makerName
            , int hasitems, int active, int alphalist,int categoryid, int parentid, int topmaker)
        {
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            List<avii.Classes.Maker> lstMaker = new List<avii.Classes.Maker>();
            avii.Classes.Maker objMaker;

            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@makerGUID", makerGUID);
                objCompHash.Add("@makerName", makerName);
                //objCompHash.Add("@hasitems", hasitems);
                //objCompHash.Add("@active", active);
                //objCompHash.Add("@alphalist", alphalist);
                //objCompHash.Add("@categoryGUID", categoryid);
                //objCompHash.Add("@parentcategoryGUID", parentid);
                //objCompHash.Add("@topmaker", topmaker);



                //arrSpFieldSeq = new string[] { "@makerGUID", "@makerName", "@hasitems", "@active", "@alphalist", "@categoryGUID", "@parentcategoryGUID", "@topmaker" };
                arrSpFieldSeq = new string[] { "@makerGUID", "@makerName"};
                ds = objDB.GetDataSet(objCompHash, "av_Maker_Select", arrSpFieldSeq);
                //if (alphalist < 1)
                {
                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        for (int ictr = 0; ictr < ds.Tables[0].Rows.Count; ictr++)
                        {
                            DataRow dataRow = ds.Tables[0].Rows[ictr];

                            objMaker = new avii.Classes.Maker();
                            objMaker.MakerGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "makerGUID", 0, false));
                            objMaker.MakerName = clsGeneral.getColumnData(dataRow, "makerName", string.Empty, false) as string;
                            //objMaker.MakerDescription = clsGeneral.getColumnData(dataRow, "makerDesc", string.Empty, false) as string;
                            //objMaker.MakerImage = clsGeneral.getColumnData(dataRow, "makerImage", string.Empty, false) as string;
                            //objMaker.MakerComments = clsGeneral.getColumnData(dataRow, "makerComment", string.Empty, false) as string;
                            //objMaker.ParentGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ParentGUID", 0, false));
                            //objMaker.isActive = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "active", 0, false));
                            

                            lstMaker.Add(objMaker);
                        }
                    }
                }
                //else
                //{
                //    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                //    {
                //        for (int ictr1 = 0; ictr1 < ds.Tables[0].Rows.Count; ictr1++)
                //        {
                //            DataRow dataRow1 = ds.Tables[0].Rows[ictr1];

                //            avii.Classes.Maker objMaker1 = new avii.Classes.Maker();
                //            objMaker1.MakerGUID = -1;
                //            objMaker1.MakerName = dataRow1["makerName"].ToString();
                //            objMaker1.MakerDescription = "";
                //            objMaker1.MakerImage = "";
                //            objMaker1.MakerComments = "";
                //            objMaker1.ParentGUID = 0;
                //            objMaker1.isActive = 1;
                //            if (topmaker < 0)
                //                objMaker1.Isalphabet = Convert.ToInt32(dataRow1["isalphabet"]);
                //            else
                //                objMaker1.Isalphabet = -1;
                //            lstMaker.Add(objMaker1);
                //        }
                //    }
                //}
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return lstMaker;
        }
    public static List<ProductType> GetProductTypes(int productTypeID)
    {
        avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        List<ProductType> productTypes = new List<ProductType>();
        avii.Classes.ProductType  productType;

        DataTable dt = new DataTable();
        Hashtable objCompHash = new Hashtable();
        string[] arrSpFieldSeq;
        try
        {
            objCompHash.Add("@ProductTypeID", productTypeID);
            arrSpFieldSeq = new string[] { "@ProductTypeID" };
            dt = objDB.GetTableRecords(objCompHash, "av_ProductType_Select", arrSpFieldSeq);
            //if (alphalist < 1)
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {

                        productType = new avii.Classes.ProductType();
                        productType.ProductTypeID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ProductTypeID", 0, false));
                        productType.Code = clsGeneral.getColumnData(dataRow, "Code", string.Empty, false) as string;
                        productTypes.Add(productType);
                    }
                }
            }

        }
        catch (Exception objExp)
        {
            throw new Exception(objExp.Message.ToString());
        }
        finally
        {
            objDB.DBClose();
            objCompHash = null;
            arrSpFieldSeq = null;
        }

        return productTypes;
    }
    public static List<ProductCondition> GetProductCondition(int conditionID)
    {
        avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        List<ProductCondition> productConditions = new List<ProductCondition>();
        avii.Classes.ProductCondition productCondition;

        DataTable dt = new DataTable();
        Hashtable objCompHash = new Hashtable();
        string[] arrSpFieldSeq;
        try
        {
            objCompHash.Add("@ConditionID", conditionID);
            arrSpFieldSeq = new string[] { "@ConditionID" };
            dt = objDB.GetTableRecords(objCompHash, "av_ProductCondition_Select", arrSpFieldSeq);
            //if (alphalist < 1)
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {

                        productCondition = new avii.Classes.ProductCondition();
                        productCondition.ConditionID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ConditionID", 0, false));
                        productCondition.Code = clsGeneral.getColumnData(dataRow, "Code", string.Empty, false) as string;
                        productConditions.Add(productCondition);
                    }
                }
            }

        }
        catch (Exception objExp)
        {
            throw new Exception(objExp.Message.ToString());
        }
        finally
        {
            objDB.DBClose();
            objCompHash = null;
            arrSpFieldSeq = null;
        }

        return productConditions;
    }

    #endregion

    //#region -- ItemCategory_update 
    //public void ItemCategory_update(int categoryGUID, string categoryName, string categoryDescription
    //    , string categoryImage, string categoryComments, int parentCategoryGUID)
    //{
    //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
    //    Hashtable objCompHash = new Hashtable();
    //    string[] arrSpFieldSeq;

    //    try
    //    {
    //        objCompHash.Add("@categoryGUID", categoryGUID);
    //        objCompHash.Add("@categoryName", categoryName);
    //        objCompHash.Add("@categoryDescription", categoryDescription);
    //        objCompHash.Add("@categoryImage", categoryImage);
    //        objCompHash.Add("@categoryComments", categoryComments);
    //        objCompHash.Add("@parentCategoryGUID", parentCategoryGUID);

    //        arrSpFieldSeq = new string[] { "@categoryGUID", "@categoryName", "@categoryDescription", "@categoryImage", "@categoryComments", "@parentCategoryGUID" };
    //        objDB.ExecuteNonQuery(objCompHash, "ItemCategory_Update", arrSpFieldSeq);
    //    }
    //    catch (Exception objExp)
    //    {
    //        throw new Exception(objExp.Message.ToString());
    //    }
    //    finally
    //    {
    //        objDB.DBClose();
    //        objCompHash = null;
    //        arrSpFieldSeq = null;
    //    }
    //}
    //#endregion

    //#region -- Maker_update
    //public void Maker_update(int makerGUID, string makerName, string makerDescription
    //    , string makerImage, string makerComment, int parentGUID)
    //{
    //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
    //    Hashtable objCompHash = new Hashtable();
    //    string[] arrSpFieldSeq;

    //    try
    //    {
    //        objCompHash.Add("@makerGUID", makerGUID);
    //        objCompHash.Add("@makerName", makerName);
    //        objCompHash.Add("@makerDescription", makerDescription);
    //        objCompHash.Add("@makerImage", makerImage);
    //        objCompHash.Add("@makerComment", makerComment);
    //        objCompHash.Add("@parentGUID", parentGUID);

    //        arrSpFieldSeq = new string[] { "@makerGUID", "@makerName", "@makerDescription", "@makerImage", "@makerComment", "@parentGUID" };
    //        objDB.ExecuteNonQuery(objCompHash, "Maker_Update", arrSpFieldSeq);
    //    }
    //    catch (Exception objExp)
    //    {
    //        throw new Exception(objExp.Message.ToString());
    //    }
    //    finally
    //    {
    //        objDB.DBClose();
    //        objCompHash = null;
    //        arrSpFieldSeq = null;
    //    }
    //}
    //#endregion
    //#region --categoryType--
    //public DataSet categoryType()
    //{
    //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
    //    try
    //    {
    //        string sql = "exec itemCategory_type";
    //        DataSet ds = objDB.GetDataSet(sql);
    //        return ds;
    //    }
    //    catch (Exception objExp)
    //    {
    //        throw new Exception(objExp.Message.ToString());
    //    }
    //    finally
    //    {
    //        objDB.DBClose();
    //    }
    //}

    //#endregion
    //# region createItemSizeList - creates the list of ItemSize objects to be passed to the stored proc as XML --
    //public void createItemSizeList(int itemGUID, string size, AvailableType available, string comment)
    //{
    //    InventoryItemSizes objItemSizesTemp = new InventoryItemSizes();
    //    objItemSizesTemp.ItemGUID = itemGUID;
    //    objItemSizesTemp.Size = size;
    //    //objItemSizesTemp.Available = available;
    //    objItemSizesTemp.Comment = comment;
    //    objItemSizesList.Add(objItemSizesTemp);

    //}
    //#endregion

    //# region createItemImageList - creates the list of ItemImage objects to be passed to the stored proc as XML --
    //public void createItemImageList(int itemGUID, int imageGuid, string ImagePath, ImageType ImageType, string ImageTxt)
    //{
    //    InventoryItemImages objItemImagesTemp = new InventoryItemImages();
    //    objItemImagesTemp.ItemGUID = itemGUID;
    //    objItemImagesTemp.ImageGUID = imageGuid;
    //    objItemImagesTemp.ImageURL = ImagePath;
    //    //objItemImagesTemp.ImageType= ImageType;
    //    objItemImagesTemp.ImageName = ImageTxt;
    //    objItemImagesList.Add(objItemImagesTemp);

    //}
    //#endregion

    #region serializeObjetToXMLString - generic function to serliaze an object to XML string
    public string serializeObjetToXMLString(object obj, string rootNodeName, string listName)
        {
            XmlSerializer objXMLSerializer = new XmlSerializer(obj.GetType());
            MemoryStream memstr = new MemoryStream();
            XmlTextWriter xmltxtwr = new XmlTextWriter(memstr, Encoding.UTF8);
            string sXML = "";

            objXMLSerializer.Serialize(xmltxtwr, obj);
            xmltxtwr.Close();
            memstr.Close();

            sXML = Encoding.UTF8.GetString(memstr.GetBuffer());
            sXML = "<" + rootNodeName + ">" + sXML.Substring(sXML.IndexOf("<" + listName + ">"));
            sXML = sXML.Substring(0, (sXML.LastIndexOf(Convert.ToChar(62)) + 1));

            return sXML;

        }

        
        #endregion

        # region createItem(createitemSize and createitemImages) - uses the list of ItemSize and ItemImage objects to creat the DB entries --
        public int createInventoryItem(int itemGuid, int categoryID, string modelNumber
            , string itemName, string itemDesc,string itemFullDesc, string UPCcode
            , int active, int MakerGUID, int companyid, string sku, bool allowRMA, bool showunderCatalog, string itemDocument,
            List<Carriers> carrierList, bool allowLTE, bool allowSIM, int userID, double Weight, bool IsKitted, bool allowESN, string storage, 
            int storageQty, bool IsDisplayName, int esnLength, int meidLength, int productTypeID, int conditionID, bool restock, string OSType)
        {
            ProductModel productModel = new ProductModel();
            productModel.ItemGuid = itemGuid;
            productModel.allowESN = allowESN;
            productModel.allowSIM = allowSIM;
            productModel.Active = active;
            productModel.AllowRMA = allowRMA;
            productModel.carrierList = carrierList;
            productModel.CategoryID = categoryID;
            productModel.Companyid = companyid;
            productModel.esnLength = esnLength;
            productModel.IsDisplayName = IsDisplayName;
            productModel.IsKitted = IsKitted;
            productModel.ItemDesc = itemDesc;
            productModel.ItemFullDesc = itemFullDesc;
            productModel.ItemName = itemName;
            productModel.MakerGUID = MakerGUID;
            productModel.meidLength = meidLength;
            productModel.ModelNumber = modelNumber;
            productModel.showunderCatalog = showunderCatalog;
            productModel.storage = storage;
            productModel.storageQty = storageQty;
            productModel.UPCcode = UPCcode;
            productModel.userID = userID;
            productModel.Weight = Weight;
            productModel.ProductTypeID = productTypeID;
            productModel.ConditionID = conditionID;
            productModel.ReStock = restock;
            productModel.OSType = OSType;

            string requestXML = clsGeneral.SerializeObject(productModel);

            SV.Framework.Admin.ItemLogModel logRequest = new SV.Framework.Admin.ItemLogModel();
            logRequest.ItemCompanyGUID = 0;
            logRequest.RequestData = requestXML;
            logRequest.CreateUserID = userID;
            logRequest.ItemGUID = itemGuid;
            if (itemGuid > 0)
                logRequest.ActionName = "Product Update";
            else
                logRequest.ActionName = "Product Create";
            if (active == 1)
                logRequest.Status = "Active";
            else
                logRequest.Status = "Inactive";

            logRequest.SKU = "";
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            int returnValue = 0;
            string carrierXML = clsGeneral.SerializeObject(carrierList);
            try
            {
                //FXml = memstr.ToString();
                //string sizeXml = serializeObjetToXMLString((object)this.objItemSizesList, "ArrayOfInventoryItemSizes", "InventoryItemSizes");
                // __________ end - serialise the itemSize list to pass to the stored proc ________

                // __________ begin - serialise the itemImage list to pass to the stored proc ________
                //string imageXml = serializeObjetToXMLString((object)this.objItemImagesList, "ArrayOfInventoryItemImages", "InventoryItemImages");
                // __________ end - serialise the itemImage list to pass to the stored proc ________
                //string upc=
                objCompHash.Add("@ItemGUID", itemGuid);
                objCompHash.Add("@CategoryID", categoryID);
              //  objCompHash.Add("@ItemCode", itemCode);
                objCompHash.Add("@ModelNumber", modelNumber);
                objCompHash.Add("@ItemName", itemName);
                objCompHash.Add("@ItemDescription", itemDesc);
                objCompHash.Add("@ItemFullDesc", itemFullDesc);
                
                objCompHash.Add("@UPC", UPCcode);
             //   objCompHash.Add("@Color", itemColor);
             //   objCompHash.Add("@ItemType", itemType);
                
                objCompHash.Add("@Active", active);
             //   objCompHash.Add("@Technology", technology);
                objCompHash.Add("@makerGUID", MakerGUID);
                objCompHash.Add("@companyid", companyid);
                objCompHash.Add("@sku", sku);
                
                objCompHash.Add("@AllowRMA", allowRMA);
                objCompHash.Add("@ShowunderCatalog", showunderCatalog);
                objCompHash.Add("@ItemDocument", itemDocument);
                objCompHash.Add("@CarrierXML", carrierXML);
                objCompHash.Add("@CustomAttribute", allowLTE);
                objCompHash.Add("@AllowSim", allowSIM);
            //    objCompHash.Add("@DefaultPrice", defaultPrice);
                objCompHash.Add("@UserID", userID);
            //    objCompHash.Add("@SimCardTypeID", SimCardTypeID);
            //    objCompHash.Add("@SpintorLockTypeID", SpintorLockTypeID);
            //    objCompHash.Add("@DisplayPriority", DisplayPriority);
                objCompHash.Add("@Weight", Weight);
            objCompHash.Add("@IsKitted", IsKitted);
            objCompHash.Add("@AllowESN", allowESN);
            objCompHash.Add("@Storage", storage);
            objCompHash.Add("@StorageQty", storageQty);
            objCompHash.Add("@IsDisplayName", IsDisplayName);
            objCompHash.Add("@EsnLength", esnLength);
            objCompHash.Add("@MeidLength", meidLength);
            objCompHash.Add("@ProductTypeID", productTypeID);
            objCompHash.Add("@ConditionID", conditionID);
            objCompHash.Add("@ReStock", restock);
            objCompHash.Add("@OSType", OSType);

            //     objCompHash.Add("@OperationSystem", operatingSystem);
            //    objCompHash.Add("@ScreenSize", screenSize);

            //objCompHash.Add("@Size_XML", sizeXml);
            //objCompHash.Add("@Image_XML", imageXml);

            //objCompHash.Add("@webPrice", webPrice);
            //objCompHash.Add("@retailPrice", retailPrice);
            //objCompHash.Add("@wholesalePrice", wholesalePrice);
            //objCompHash.Add("@priceType", priceType);
            //objCompHash.Add("@SpeficationTxt", specification);

            arrSpFieldSeq = new string[] { "@ItemGUID", "@CategoryID", "@ModelNumber", "@ItemName", "@ItemDescription", 
                    "@ItemFullDesc", "@UPC", "@Active", "@makerGUID", "@companyid", "@sku", "@AllowRMA",
                    "@ShowunderCatalog", "@ItemDocument", "@CarrierXML", "@CustomAttribute", "@AllowSim","@UserID", "@Weight", 
                "@IsKitted","@AllowESN", "@Storage", "@StorageQty", "@IsDisplayName","@EsnLength", "@MeidLength", "@ProductTypeID",
                "@ConditionID","@ReStock","@OSType"};
                returnValue = db.ExecCommand(objCompHash, "av_Item_Update", arrSpFieldSeq);
                if(returnValue < 0)
                {
                    logRequest.ItemGUID = (-1 * returnValue);
                    returnValue = 0;
                }
            if (ItemGUID > 0)
                logRequest.ResponseData = "Updated successfully";
            else
                logRequest.ResponseData = "Submitted successfully";
            //errorMessage = ResponseErrorCode.UploadedSuccessfully.ToString();
        }
            catch (Exception objExp)
            {
                logRequest.Comment = objExp.Message;
                //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                //throw objExp;
            }
            finally
            {
                SV.Framework.Admin.ItemLogOperation.ItemLogInsert(logRequest);
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return returnValue;
        }
        #endregion

        # region createItem(createitemSize and createitemImages) - uses the list of ItemSize and ItemImage objects to creat the DB entries --
        //public void createInventoryItem(int itemGuid, int categoryID, string itemCode, string modelNumber, string itemName, string itemDesc, string UPCcode, string itemColor, int active, string webPrice, string retailPrice, string wholesalePrice, int priceType, string specification)
        //{
        //    DBConnect db = new DBConnect();
        //    string[] arrSpFieldSeq;
        //    Hashtable objCompHash = new Hashtable();

        //    try
        //    {
        //        //FXml = memstr.ToString();
        //        string sizeXml = serializeObjetToXMLString((object)this.objItemSizesList, "ArrayOfInventoryItemSizes", "InventoryItemSizes");
        //        // __________ end - serialise the itemSize list to pass to the stored proc ________

        //        // __________ begin - serialise the itemImage list to pass to the stored proc ________
        //        string imageXml = serializeObjetToXMLString((object)this.objItemImagesList, "ArrayOfInventoryItemImages", "InventoryItemImages");
        //        // __________ end - serialise the itemImage list to pass to the stored proc ________
        //        //string upc=
        //        objCompHash.Add("@ItemGUID", itemGuid);
        //        objCompHash.Add("@CategoryID", categoryID);
        //        objCompHash.Add("@ItemCode", itemCode);
        //        objCompHash.Add("@ModelNumber", modelNumber);
        //        objCompHash.Add("@ItemName", itemName);
        //        objCompHash.Add("@ItemDescription", itemDesc);
        //        objCompHash.Add("@UPC", UPCcode);
        //        objCompHash.Add("@Color", itemColor);
        //        objCompHash.Add("@Active", active);
        //        objCompHash.Add("@Size_XML", sizeXml);
        //        objCompHash.Add("@Image_XML", imageXml);

        //        objCompHash.Add("@webPrice", webPrice);
        //        objCompHash.Add("@retailPrice", retailPrice);
        //        objCompHash.Add("@wholesalePrice", wholesalePrice);
        //        objCompHash.Add("@priceType", priceType);
        //        objCompHash.Add("@SpeficationTxt", specification);




        //        arrSpFieldSeq = new string[] { "@ItemGUID", "@CategoryID", "@ItemCode", "@ModelNumber", "@ItemName", "@ItemDescription", "@UPC", "@Color", "@Active", "@Size_XML", "@Image_XML", "@webPrice", "@retailPrice", "@wholesalePrice", "@priceType", "@SpeficationTxt" };
        //        db.ExeCommand(objCompHash, "Item_Update", arrSpFieldSeq);

        //        //errorMessage = ResponseErrorCode.UploadedSuccessfully.ToString();
        //    }
        //    catch (Exception objExp)
        //    {
        //        //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
        //        throw objExp;
        //    }
        //    finally
        //    {
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }
        //    //return errorMessage;
        //}
        #endregion

        # region createItem(Sizes) - uses the list of ItemSize and ItemImage objects to creat the DB entries --
        //old with XML
        //public void createItemSizes(int itemGuid)
        //{
        //    DBConnect db = new DBConnect();
        //    string[] arrSpFieldSeq;
        //    Hashtable objCompHash = new Hashtable();

        //    try
        //    {
        //        //FXml = memstr.ToString();
        //        string sizeXml = serializeObjetToXMLString((object)this.objItemSizesList, "ArrayOfInventoryItemSizes", "InventoryItemSizes");
        //        // __________ end - serialise the itemSize list to pass to the stored proc ________

        //        // __________ begin - serialise the itemImage list to pass to the stored proc ________
        //        //string imageXml = serializeObjetToXMLString((object)this.objItemImagesList, "ArrayOfInventoryItemImages", "InventoryItemImages");
        //        // __________ end - serialise the itemImage list to pass to the stored proc ________
        //        //string upc=
        //        objCompHash.Add("@ItemGUID", itemGuid);
        //        objCompHash.Add("@Size_XML", sizeXml);
                



        //        arrSpFieldSeq = new string[] { "@ItemGUID", "@Size_XML" };
        //        db.ExeCommand(objCompHash, "ItemSizes_Update", arrSpFieldSeq);

        //        //errorMessage = ResponseErrorCode.UploadedSuccessfully.ToString();
        //    }
        //    catch (Exception objExp)
        //    {
        //        //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
        //        throw objExp;
        //    }
        //    finally
        //    {
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }
        //    //return errorMessage;
        //}
        #endregion

        #region getItemList - get the list of items for a category / all categories / particular item
        public List<avii.Classes.InventoryItems> getItemList(int iCategoryGUID, int iItemGUID, int active, int makerGUID, int technologyGUID, string modelnumber, 
            int topnewitem, string sku, string upc, string color, int companyID, string itemCode, string warehouseCode, int showunderCatalog,
            int productTypeID,int conditionID, bool restock)
        {
            List<avii.Classes.InventoryItems> objItemList = new List<avii.Classes.InventoryItems>();
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            try
            {
                objCompHash.Add("@ItemGUID", iItemGUID);
                objCompHash.Add("@categoryGUID", iCategoryGUID);
                objCompHash.Add("@active", active);               
              //  objCompHash.Add("@makerGUID", makerGUID);
                objCompHash.Add("@itemName", string.Empty);
              //  objCompHash.Add("@technology", technologyGUID);
                objCompHash.Add("@modelnumber", modelnumber);
                objCompHash.Add("@topnewitem", topnewitem);
                objCompHash.Add("@sku", sku);
                objCompHash.Add("@upc", upc);
              //  objCompHash.Add("@color", color);
                objCompHash.Add("@companyID", companyID);
              //  objCompHash.Add("@itemCode", itemCode);
             //   objCompHash.Add("@WarehouseCode", warehouseCode);
                objCompHash.Add("@ShowunderCatalog", showunderCatalog);
                objCompHash.Add("@ProductTypeID", productTypeID);
                objCompHash.Add("@ConditionID", conditionID);
                objCompHash.Add("@ReStock", restock);


                arrSpFieldSeq = new string[] { "@ItemGUID", "@categoryGUID", "@active", "@itemName", "@modelnumber", "@topnewitem", "@sku", "@upc", "@companyID", 
                    "@ShowunderCatalog","@ProductTypeID","@ConditionID","@ReStock" };
                ds = objDB.GetDataSet(objCompHash, "av_item_Select", arrSpFieldSeq);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    avii.Classes.InventoryItems objItem = new avii.Classes.InventoryItems();

                    objItem.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemGUID", 0, false));
                    objItem.ProductTypeID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ProductTypeID", 0, false));
                    objItem.ConditionID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ConditionID", 0, false));
                    objItem.ReStock = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ReStock", false, false));
                    // objItem.ItemCode = clsGeneral.getColumnData(dataRow, "itemCode", string.Empty, false) as string; 
                    objItem.ModelNumber = clsGeneral.getColumnData(dataRow, "modelnumber", string.Empty, false) as string;
                    //      objItem.ItemColor = clsGeneral.getColumnData(dataRow, "color", string.Empty, false) as string; 
                    objItem.ItemName = clsGeneral.getColumnData(dataRow, "itemname", string.Empty, false) as string;
                    objItem.Upc = clsGeneral.getColumnData(dataRow, "upc", string.Empty, false) as string;
                    objItem.ItemCategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryID", 0, false));
                    objItem.ItemCategory = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    objItem.ItemMakerGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "makerGUID", 0, false));
                    //objItem.ItemMaker = clsGeneral.getColumnData(dataRow, "makerName", string.Empty, false) as string; 
                    objItem.ItemDesc1 = clsGeneral.getColumnData(dataRow, "itemDescription", string.Empty, false) as string;
                    objItem.ItemDesc2 = clsGeneral.getColumnData(dataRow, "ItemDescrition2", string.Empty, false) as string;
                    // objItem.MakerItems = clsGeneral.getColumnData(dataRow, "makeritems", string.Empty, false) as string;
                    //objItem.TechnologyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CarrierGUID", 0, false));                       
                    //InventoryItemType itemType = (InventoryItemType)Enum.Parse(typeof(InventoryItemType), dataRow["ItemType"].ToString());
                    //objItem.Item_Type = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemType", 0, false));
                    //objItem.ItemType = itemType;
                    objItem.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemGUID", 0, false));
                    //    objItem.ItemPrice = clsGeneral.getColumnData(dataRow, "webprice", string.Empty, false) as string;
                    objItem.ItemTechnology = clsGeneral.getColumnData(dataRow, "CarrierName", string.Empty, false) as string;
                    objItem.ParentCategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "parentCategoryGUID", 0, false));
                    objItem.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                    objItem.CompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "companyid", 0, false));
                    objItem.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                    objItem.AllowRMA = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowRMA", false, false));
                    objItem.ShowunderCatalog = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ShowunderCatalog", false, false));
                    //objItem.ItemDocument = clsGeneral.getColumnData(dataRow, "ItemDocument", string.Empty, false) as string;
                    objItem.AllowLTE = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "CustomAttribute", false, false));
                    objItem.AllowSIM = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsSim", false, false));
                    //objItem.DefaultPrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "DefaultPrice", 0, false));
                    objItem.IsKitted = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsKitted", false, false));
                    objItem.AllowESN = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowESN", false, false));
                    objItem.IsESNRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESNRequired", false, false));
                    objItem.CategoryWithProductAllowed = Convert.ToString(clsGeneral.getColumnData(dataRow, "CategoryWithProductAllowed", string.Empty, false));
                    objItem.Storage = Convert.ToString(clsGeneral.getColumnData(dataRow, "Storage", string.Empty, false));
                    objItem.StorageQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StorageQty", 0, true));
                    objItem.IsDisplayName = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsDisplayName", 0, true));
                    objItem.EsnLength = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "EsnLength", 0, true));
                    objItem.MeidLength = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MeidLength", 0, true));

                    objItem.OSType = Convert.ToString(clsGeneral.getColumnData(dataRow, "OSType", string.Empty, false));

                    //objItem.DisplayPriority = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DisplayPriority", 0, true));
                    objItem.Weight = Convert.ToDecimal(dataRow["Weight"]); //(float)clsGeneral.getColumnData(row2, "Weight", 0, false);
                                                                           //objItem.SimCardTypeID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "SimCardTypeID", 0, true));
                                                                           //objItem.SpintorLockTypeID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "SpintorLockTypeID", 0, true));
                                                                           //objItem.OperationSystem = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OperationSystem", 0, true));
                                                                           //objItem.ScreenSize = clsGeneral.getColumnData(dataRow, "ScreenSize", string.Empty, false) as string; 




                    objItemList.Add(objItem);
                }
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return objItemList;
        }

    public List<avii.Classes.InventoryItems> GetCustomerSKUList(int iCategoryGUID, int iItemGUID, string modelnumber, string sku, string upc, int companyID, bool Isdisable)
    {
        List<avii.Classes.InventoryItems> objItemList = new List<avii.Classes.InventoryItems>();
        avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        Hashtable objCompHash = new Hashtable();
        string[] arrSpFieldSeq;
        DataSet ds = new DataSet();
        try
        {
            objCompHash.Add("@ItemGUID", iItemGUID);
            objCompHash.Add("@categoryGUID", iCategoryGUID);
            objCompHash.Add("@modelnumber", modelnumber);
            objCompHash.Add("@sku", sku);
            objCompHash.Add("@upc", upc);
            objCompHash.Add("@companyID", companyID);
            objCompHash.Add("@IsDisable", Isdisable);


            arrSpFieldSeq = new string[] { "@ItemGUID", "@categoryGUID", "@modelnumber", "@sku", "@upc", "@companyID", "@IsDisable" };
            ds = objDB.GetDataSet(objCompHash, "av_item_sku_Select", arrSpFieldSeq);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    avii.Classes.InventoryItems objItem = new avii.Classes.InventoryItems();

                    objItem.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemGUID", 0, false));
                    //objItem.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string; 
                    objItem.ModelNumber = clsGeneral.getColumnData(dataRow, "modelnumber", string.Empty, false) as string;
                    objItem.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string; 
                    objItem.ItemName = clsGeneral.getColumnData(dataRow, "itemname", string.Empty, false) as string;
                    objItem.Upc = clsGeneral.getColumnData(dataRow, "upc", string.Empty, false) as string;
                    objItem.ItemCategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryID", 0, false));
                    objItem.ItemCategory = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    objItem.ItemMakerGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "makerGUID", 0, false));
                    //objItem.ItemMaker = clsGeneral.getColumnData(dataRow, "makerName", string.Empty, false) as string; 
                    objItem.ItemDesc1 = clsGeneral.getColumnData(dataRow, "itemDescription", string.Empty, false) as string;
                    objItem.ItemDesc2 = clsGeneral.getColumnData(dataRow, "ItemDescrition2", string.Empty, false) as string;
                    // objItem.MakerItems = clsGeneral.getColumnData(dataRow, "makeritems", string.Empty, false) as string;
                    //objItem.TechnologyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CarrierGUID", 0, false));                       
                    //InventoryItemType itemType = (InventoryItemType)Enum.Parse(typeof(InventoryItemType), dataRow["ItemType"].ToString());
                    //objItem.Item_Type = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemType", 0, false));
                    //objItem.ItemType = itemType;
                    objItem.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemGUID", 0, false));
                    //    objItem.ItemPrice = clsGeneral.getColumnData(dataRow, "webprice", string.Empty, false) as string;
                    objItem.ItemTechnology = clsGeneral.getColumnData(dataRow, "CarrierName", string.Empty, false) as string;
                    objItem.ParentCategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "parentCategoryGUID", 0, false));
                    objItem.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                    objItem.CompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "companyid", 0, false));
                    objItem.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                    objItem.AllowRMA = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowRMA", false, false));
                    objItem.ShowunderCatalog = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ShowunderCatalog", false, false));
                    //objItem.ItemDocument = clsGeneral.getColumnData(dataRow, "ItemDocument", string.Empty, false) as string;
                    objItem.AllowLTE = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "CustomAttribute", false, false));
                    objItem.AllowSIM = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsSim", false, false));
                    //objItem.DefaultPrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "DefaultPrice", 0, false));
                    objItem.IsKitted = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsKitted", false, false));
                    objItem.AllowESN = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowESN", false, false));

                    objItem.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    objItem.IsDisable = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsDisable", false, false));

                    //objItem.DisplayPriority = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DisplayPriority", 0, true));
                    objItem.Weight = Convert.ToDecimal(dataRow["Weight"]); //(float)clsGeneral.getColumnData(row2, "Weight", 0, false);
                                                                           //objItem.SimCardTypeID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "SimCardTypeID", 0, true));
                                                                           //objItem.SpintorLockTypeID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "SpintorLockTypeID", 0, true));
                                                                           //objItem.OperationSystem = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OperationSystem", 0, true));
                                                                           //objItem.ScreenSize = clsGeneral.getColumnData(dataRow, "ScreenSize", string.Empty, false) as string; 




                    objItemList.Add(objItem);
                }
            }
        }
        catch (Exception objExp)
        {
            throw new Exception(objExp.Message.ToString());
        }
        finally
        {
            objDB.DBClose();
            objCompHash = null;
            arrSpFieldSeq = null;
        }
        return objItemList;
    }

    public List<avii.Classes.InventoryItems> GetProductList(int iCategoryGUID, int iItemGUID, int active, int makerGUID, int technologyGUID, string modelnumber, int topnewitem, string sku, string upc, string color, int companyID, string itemCode, string searchText)
        {
            List<avii.Classes.InventoryItems> objItemList = new List<avii.Classes.InventoryItems>();
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            try
            {
                objCompHash.Add("@ItemGUID", iItemGUID);
                objCompHash.Add("@categoryGUID", iCategoryGUID);
                objCompHash.Add("@active", active);
                objCompHash.Add("@makerGUID", makerGUID);
                objCompHash.Add("@itemName", string.Empty);
                objCompHash.Add("@technology", technologyGUID);
                objCompHash.Add("@modelnumber", modelnumber);
                objCompHash.Add("@topnewitem", topnewitem);
                objCompHash.Add("@sku", sku);
                objCompHash.Add("@upc", upc);
                objCompHash.Add("@color", color);
                objCompHash.Add("@companyID", companyID);
                objCompHash.Add("@itemCode", itemCode);
                objCompHash.Add("@WarehouseCode", string.Empty);
                objCompHash.Add("@ShowunderCatalog", 1);
                objCompHash.Add("@SearchText", searchText);


                arrSpFieldSeq = new string[] { "@ItemGUID", "@categoryGUID", "@active", "@makerGUID", "@itemName", "@technology", "@modelnumber", "@topnewitem", "@sku", "@upc", "@color", "@companyID", "@itemCode", "@WarehouseCode", "@ShowunderCatalog", "@SearchText" };
                ds = objDB.GetDataSet(objCompHash, "av_item_Select", arrSpFieldSeq);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        avii.Classes.InventoryItems objItem = new avii.Classes.InventoryItems();

                        objItem.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemGUID", 0, false));
                        objItem.ItemCode = clsGeneral.getColumnData(dataRow, "itemCode", string.Empty, false) as string;
                        objItem.ModelNumber = clsGeneral.getColumnData(dataRow, "modelnumber", string.Empty, false) as string;
                        objItem.ItemColor = clsGeneral.getColumnData(dataRow, "color", string.Empty, false) as string;
                        objItem.ItemName = clsGeneral.getColumnData(dataRow, "itemname", string.Empty, false) as string;
                        objItem.Upc = clsGeneral.getColumnData(dataRow, "upc", string.Empty, false) as string;
                        objItem.ItemCategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryID", 0, false));
                        objItem.ItemCategory = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                        objItem.ItemMakerGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "makerGUID", 0, false));
                        objItem.ItemMaker = clsGeneral.getColumnData(dataRow, "makerName", string.Empty, false) as string;
                        objItem.ItemDesc1 = clsGeneral.getColumnData(dataRow, "itemDescription", string.Empty, false) as string;
                        objItem.ItemDesc2 = clsGeneral.getColumnData(dataRow, "ItemDescrition2", string.Empty, false) as string;
                        objItem.MakerItems = clsGeneral.getColumnData(dataRow, "makeritems", string.Empty, false) as string;
                        objItem.TechnologyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Technologyid", 0, false));
                        InventoryItemType itemType = (InventoryItemType)Enum.Parse(typeof(InventoryItemType), dataRow["ItemType"].ToString());
                        objItem.Item_Type = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemType", 0, false));
                        objItem.ItemType = itemType;
                        objItem.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemGUID", 0, false));
                        objItem.ItemPrice = clsGeneral.getColumnData(dataRow, "webprice", string.Empty, false) as string;
                        objItem.ItemTechnology = clsGeneral.getColumnData(dataRow, "Technology", string.Empty, false) as string;
                        objItem.ParentCategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "parentCategoryGUID", 0, false));
                        objItem.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                        objItem.CompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "companyid", 0, false));
                        objItem.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        objItem.ItemImages = getItemImageList(objItem.ItemGUID, -1, 1, 0); 
                        objItemList.Add(objItem);
                    }
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return objItemList;
        }
        #endregion

        #region getItemImageList - get the list of images for an items all images / particular image
        public List<InventoryItemImages> getItemImageList(int ItemGUID, int itemImageGUID, int itemFlag,int imageFlag)
        {
            List<InventoryItemImages> objImageList = new List<InventoryItemImages>();
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            try
            {
                objCompHash.Add("@ItemGUID", ItemGUID);
                objCompHash.Add("@imageGUID", itemImageGUID);
                objCompHash.Add("@itemFlag", itemFlag);
                objCompHash.Add("@imageFlag", imageFlag);

                arrSpFieldSeq = new string[] { "@ItemGUID", "@imageGUID", "@itemFlag", "@imageFlag" };
                ds = objDB.GetDataSet(objCompHash, "av_ItemImage_Select", arrSpFieldSeq);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        InventoryItemImages objItemImage = new InventoryItemImages();
                        objItemImage.ImageGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "imageGUID", 0, false)); 
                        objItemImage.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemGUID", 0, false)); 
                        ImageType imagesType = (ImageType)Enum.Parse(typeof(ImageType), dataRow["imageType"].ToString());

                        objItemImage.ImageType = imagesType;
                        objItemImage.ImageName = clsGeneral.getColumnData(dataRow, "ImageTxt", string.Empty, false) as string; 
                        objItemImage.ImageURL = clsGeneral.getColumnData(dataRow, "ImagePath", string.Empty, false) as string; 
                        objItemImage.Color = clsGeneral.getColumnData(dataRow, "Color", string.Empty, false) as string; 
                        //objItemImage.ColorGUID =  Convert.ToInt32(dataRow["ColorGUID"]);
                        //objItemImage.ItemSizeGUID = Convert.ToInt32(dataRow["itemsizeguid"]);
                        //objItemImage.Weight = clsGeneral.getColumnData(dataRow, "webprice", string.Empty, false) as string; dataRow["weight"].ToString();

                        objImageList.Add(objItemImage);
                    }
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return objImageList;
        }
        #endregion

        //#region getItemSizeList - get the list of sizes for the item all sizes / particular size
        //public List<InventoryItemSizes> getItemSizeList(int ItemGUID, int size)
        //{
        //    List<InventoryItemSizes> objSizeList = new List<InventoryItemSizes>();
        //        avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        //        Hashtable objCompHash = new Hashtable();
        //        string[] arrSpFieldSeq;
        //        DataSet ds = new DataSet();

        //        try
        //        {
        //            objCompHash.Add("@ItemGUID", ItemGUID);
        //            objCompHash.Add("@size", size);
        //            arrSpFieldSeq = new string[] { "@ItemGUID", "@size" };
        //            ds = objDB.GetDataSet(objCompHash, "ItemSize_Select", arrSpFieldSeq);

        //            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //            {
        //                foreach (DataRow dataRow in ds.Tables[0].Rows)
        //                {
        //                    InventoryItemSizes objImageSize = new InventoryItemSizes();
        //                    objImageSize.ItemGUID = Convert.ToInt32(dataRow["itemGUID"]);
        //                    objImageSize.Size = dataRow["size"].ToString();
        //                    objImageSize.SizeGUID = Convert.ToInt32(dataRow["itemsizeGUID"]);
        //                    objImageSize.Size_GUID = Convert.ToInt32(dataRow["sizeGUID"]);
        //                    //objImageSize.ColorGUID = Convert.ToInt32(dataRow["colorGUID"]);
        //                    //objImageSize.Color = dataRow["color"].ToString();
        //                    AvailableType available=(AvailableType)Enum.Parse(typeof(AvailableType),dataRow["available"].ToString());

        //                    //objImageSize.Available = available;
        //                    objImageSize.ItemSizetype = dataRow["ItemSizetype"].ToString();
        //                    objImageSize.Comment = dataRow["comment"].ToString();
        //                    objImageSize.ItemimageGUID = Convert.ToInt32(dataRow["itemimageGUID"]);
        //                    objImageSize.Weight = dataRow["weight"].ToString();

                            
        //                    objSizeList.Add(objImageSize);
        //                }
        //            }
        //        }
        //        catch (Exception objExp)
        //        {
        //            throw new Exception(objExp.Message.ToString());
        //        }
        //        finally
        //        {
        //            objDB.DBClose();
        //            objCompHash = null;
        //            arrSpFieldSeq = null;
        //        }

        //        return objSizeList;
        //}
        //#endregion

        //#region getItemColorList - get the list of color for the item / particular color
        //public List<InventoryItemColor> getItemColorList(int ItemGUID, int colorGUID)
        //{
        //    List<InventoryItemColor> objColorList = new List<InventoryItemColor>();
        //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        //    Hashtable objCompHash = new Hashtable();
        //    string[] arrSpFieldSeq;
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        objCompHash.Add("@ItemGUID", ItemGUID);
        //        objCompHash.Add("@ColorGUID", colorGUID);
        //        arrSpFieldSeq = new string[] { "@ItemGUID", "@ColorGUID" };
        //        ds = objDB.GetDataSet(objCompHash, "itemColor_select", arrSpFieldSeq);

        //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow dataRow in ds.Tables[0].Rows)
        //            {
        //                InventoryItemColor objItemColor = new InventoryItemColor();
        //                objItemColor.ItemGUID = Convert.ToInt32(dataRow["itemGUID"]);
        //                objItemColor.Color = dataRow["color"].ToString();
        //                objItemColor.ColorGUID = Convert.ToInt32(dataRow["ColorGUID"]);

        //                objColorList.Add(objItemColor);
        //            }
        //        }
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }

        //    return objColorList;
        //}
        //#endregion

        #region getItemPriceDetails - get the list of prices for the item all prices / particular price
        public List<InventoryItemPricing> getItemPriceList(int ItemGUID, int pricingGUID)
        {
            List<InventoryItemPricing> objItemPriceList = new List<InventoryItemPricing>();
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();

            try
            {
                objCompHash.Add("@ItemGUID", ItemGUID);
                objCompHash.Add("@pricingGUID", pricingGUID);
                objCompHash.Add("@customerType", "0");

                arrSpFieldSeq = new string[] { "@ItemGUID", "@pricingGUID", "@customerType" };
                ds = objDB.GetDataSet(objCompHash, "av_ItemPrice_Select", arrSpFieldSeq);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        InventoryItemPricing objItemPricing = new InventoryItemPricing();
                        objItemPricing.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemGUID", 0, false)); 
                        objItemPricing.PricingGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pricingGUID", 0, false));
                        objItemPricing.WebPrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "webprice", 0, false)); 
                        objItemPricing.RetailPrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "retailprice", 0, false));
                        objItemPricing.WholeSalePrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "wholesaleprice", 0, false));
                        //string pricetype = dataRow["priceType"].ToString();
                        ItemPricingType priceType = (ItemPricingType)Enum.Parse(typeof(ItemPricingType), dataRow["priceType"].ToString());
                        objItemPricing.PriceType = priceType;
                        //objItemPricing.ItemSize = dataRow["itemSizeName"].ToString();
                        objItemPricing.Itemprice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "itemprice", 0, false)); 
                       // objItemPricing.ItemSizeprice = dataRow["sizenprice"].ToString();
                       // objItemPricing.ItemPricenSize = dataRow["ItemPricenSize"].ToString();
                        // objItemPricing.ItemsizeGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemsizeGUID", 0, false)); 
                        // objItemPricing.SizeGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "sizeGUID", 0, false)); 
                        
                        

                        objItemPriceList.Add(objItemPricing);

                    }
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return objItemPriceList;
        }
    #endregion

    public static List<MappedSKUModel> GetMappedSKUs(int itemGUID)
    {
        List<MappedSKUModel> skuList = new List<MappedSKUModel>();
        MappedSKUModel mappedSKU = null;
        avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        Hashtable objCompHash = new Hashtable();
        string[] arrSpFieldSeq;
        DataTable dt = new DataTable();
        try
        {
            objCompHash.Add("@ItemGUID", itemGUID);
            //objCompHash.Add("@priceType", pricetype);
            arrSpFieldSeq = new string[] { "@ItemGUID" };
            dt = objDB.GetTableRecords(objCompHash, "av_Item_MappedSKU_Select", arrSpFieldSeq);

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    mappedSKU = new MappedSKUModel();
                    mappedSKU.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false)); //Convert.ToInt32(dataRow["ItemCompanyGUID"]);
                    mappedSKU.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string; //dataRow["SKU"].ToString(); ;
                    

                    skuList.Add(mappedSKU);
                }
            }
        }
        catch (Exception objExp)
        {
            throw new Exception(objExp.Message.ToString());
        }
        finally
        {
            objDB.DBClose();
            objCompHash = null;
            arrSpFieldSeq = null;
        }

        return skuList;
    }
    #region getItemSpecifications - get the list of Specifications for the item all Specifications / particular Specification
    public List<InventoryItemSpecifications> getItemSpecifications(int ItemGUID)
        {
            List<InventoryItemSpecifications> objItemSpecifications = new List<InventoryItemSpecifications>();
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            try
            {
                objCompHash.Add("@ItemGUID", ItemGUID);
                //objCompHash.Add("@priceType", pricetype);
                arrSpFieldSeq = new string[] { "@ItemGUID" };
                ds = objDB.GetDataSet(objCompHash, "av_ItemSpecification_Select", arrSpFieldSeq);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        InventoryItemSpecifications objItemSpecification = new InventoryItemSpecifications();
                        objItemSpecification.ItemGUID = Convert.ToInt32(dataRow["itemGUID"]);
                        objItemSpecification.Specificaiton = dataRow["SpeficationTxt"].ToString(); ;
                        objItemSpecification.SpecificationGuid = Convert.ToInt32(dataRow["SpecificationGUID"]);


                        objItemSpecifications.Add(objItemSpecification);
                    }
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return objItemSpecifications;
        }
        #endregion

        #region getCompanySKUno --
        public List<CompanySKUno> getCompanySKUnoList(int itemGUID, int companyid,string sku)
        {
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            List<CompanySKUno> lstCompanySKUno = new List<CompanySKUno>();
            CompanySKUno objCompanySKUno;

            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
        try
        {
            objCompHash.Add("@itemguid", itemGUID);
            objCompHash.Add("@companyID", companyid);
            objCompHash.Add("@sku", sku);

            arrSpFieldSeq = new string[] { "@itemguid", "@companyID", "@sku" };
            ds = objDB.GetDataSet(objCompHash, "av_ItemCompanyAssign_select", arrSpFieldSeq);

            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                for (int ictr = 0; ictr < ds.Tables[0].Rows.Count; ictr++)
                {
                    DataRow dataRow = ds.Tables[0].Rows[ictr];

                    objCompanySKUno = new CompanySKUno();
                    objCompanySKUno.PoExists = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PoExists", 0, false));
                    objCompanySKUno.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemcompanyguid", 0, false));
                    objCompanySKUno.MappedItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MappedItemCompanyGUID", 0, false));
                    objCompanySKUno.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemGUID", 0, false));
                    objCompanySKUno.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "companyID", 0, false));
                    objCompanySKUno.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                    objCompanySKUno.MappedSKU = clsGeneral.getColumnData(dataRow, "MappedSKU", string.Empty, false) as string;
                    objCompanySKUno.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                    //objCompanySKUno.MASSKU = clsGeneral.getColumnData(dataRow, "MASSKU", string.Empty, false) as string;
                    objCompanySKUno.WarehouseCode = clsGeneral.getColumnData(dataRow, "WarehouseCode", string.Empty, false) as string;
                    //objCompanySKUno.IsFinishedSKU = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsFinalSKU", false, false));
                    //objCompanySKUno.Price = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "SKUPRICE", 0, false));
                    objCompanySKUno.IsDisable = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsDisable", false, false));
                    objCompanySKUno.MinimumStockLevel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MinimumStockLevel", 0, false));
                    objCompanySKUno.MaximumStockLevel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MaximumStockLevel", 0, false));
                    objCompanySKUno.ContainerQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ContainerQuantity", 0, false));
                    objCompanySKUno.PalletQuantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PalletQuantity", 0, false));
                    objCompanySKUno.DPCI = clsGeneral.getColumnData(dataRow, "DPCI", string.Empty, false) as string;
                    objCompanySKUno.SWVersion = clsGeneral.getColumnData(dataRow, "SWVersion", string.Empty, false) as string;
                    objCompanySKUno.HWVersion = clsGeneral.getColumnData(dataRow, "HWVersion", string.Empty, false) as string;
                    objCompanySKUno.BoxDesc = clsGeneral.getColumnData(dataRow, "BoxDesc", string.Empty, false) as string;


                    lstCompanySKUno.Add(objCompanySKUno);
                }
            }
        }
        catch (Exception objExp)
        {
            throw new Exception(objExp.Message.ToString());
        }
        finally
        {
            objDB.DBClose();
            objCompHash = null;
            arrSpFieldSeq = null;
        }

            return lstCompanySKUno;
        }
    public static CompanySKUno GetCompanyItemSKUInfo(int itemCompanyGUID)
    {
        avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        CompanySKUno objCompanySKUno = null;

        DataTable datTable = new DataTable();
        Hashtable objCompHash = new Hashtable();
        string[] arrSpFieldSeq;
        try
        {
            objCompHash.Add("@itemguid", -1);
            objCompHash.Add("@companyID", -1);
            objCompHash.Add("@sku", string.Empty);
            objCompHash.Add("@ItemCompanyGUID", itemCompanyGUID);

            arrSpFieldSeq = new string[] { "@itemguid", "@companyID", "@sku", "@ItemCompanyGUID" };
            datTable = objDB.GetTableRecords(objCompHash, "av_ItemCompanyAssign_select", arrSpFieldSeq);

            if (datTable != null && datTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in datTable.Rows)
                {
                    objCompanySKUno = new CompanySKUno();
                    objCompanySKUno.MappedItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MappedItemCompanyGUID", 0, false));
                    objCompanySKUno.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemcompanyguid", 0, false));
                    objCompanySKUno.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemGUID", 0, false));
                    objCompanySKUno.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "companyID", 0, false));
                    objCompanySKUno.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                    objCompanySKUno.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                    //objCompanySKUno.MASSKU = clsGeneral.getColumnData(dataRow, "MASSKU", string.Empty, false) as string;
                    objCompanySKUno.WarehouseCode = clsGeneral.getColumnData(dataRow, "WarehouseCode", string.Empty, false) as string;
                    //objCompanySKUno.IsFinishedSKU = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsFinalSKU", false, false));
                    //objCompanySKUno.Price = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "SKUPRICE", 0, false));

                    objCompanySKUno.IsDisable = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsDisable", false, false));
                    objCompanySKUno.MinimumStockLevel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MinimumStockLevel", 0, false));
                    objCompanySKUno.MaximumStockLevel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MaximumStockLevel", 0, false));
                    objCompanySKUno.ContainerQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ContainerQuantity", 0, false));
                    objCompanySKUno.PalletQuantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PalletQuantity", 0, false));
                    objCompanySKUno.DPCI = clsGeneral.getColumnData(dataRow, "DPCI", string.Empty, false) as string;
                    objCompanySKUno.SWVersion = clsGeneral.getColumnData(dataRow, "SWVersion", string.Empty, false) as string;
                    objCompanySKUno.HWVersion = clsGeneral.getColumnData(dataRow, "HWVersion", string.Empty, false) as string;
                    objCompanySKUno.BoxDesc = clsGeneral.getColumnData(dataRow, "BoxDesc", string.Empty, false) as string;
                }
            }
        }
        catch (Exception objExp)
        {
            throw new Exception(objExp.Message.ToString());
        }
        finally
        {
            objDB.DBClose();
            objCompHash = null;
            arrSpFieldSeq = null;
        }

        return objCompanySKUno;
    }

        #endregion
        #region Item's Camera info--

        public static ItemCameraInfo GetItemCameraInfo(int itemCameraID)
        {
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            ItemCameraInfo itemCameraInfo = null;

            DataTable datTable = new DataTable();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@Itemguid", 0);
                objCompHash.Add("@ItemCameraId", itemCameraID);

                arrSpFieldSeq = new string[] { "@Itemguid", "@ItemCameraId" };
                datTable = objDB.GetTableRecords(objCompHash, "Av_itemCameraSelect", arrSpFieldSeq);

                if (datTable != null && datTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in datTable.Rows)
                    {


                        itemCameraInfo = new ItemCameraInfo();
                        itemCameraInfo.ItemCameraID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCameraID", 0, false));
                        itemCameraInfo.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemGUID", 0, false));
                        itemCameraInfo.CameraID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CameraID", 0, false));
                        itemCameraInfo.CameraType = clsGeneral.getColumnData(dataRow, "CameraType", string.Empty, false) as string;



                    }
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return itemCameraInfo;
        }
        public static List<ItemCameraInfo> GetItemCameraList(int Itemguid)
        {
            List<ItemCameraInfo> itemCameraList = itemCameraList = new List<ItemCameraInfo>();
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            ItemCameraInfo itemCameraInfo = null;

            DataTable datTable = new DataTable();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@Itemguid", Itemguid);
                objCompHash.Add("@ItemCameraId", 0);

                arrSpFieldSeq = new string[] { "@Itemguid", "@ItemCameraId" };
                datTable = objDB.GetTableRecords(objCompHash, "Av_itemCameraSelect", arrSpFieldSeq);

                if (datTable != null && datTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in datTable.Rows)
                    {


                        itemCameraInfo = new ItemCameraInfo();
                        itemCameraInfo.ItemCameraID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCameraID", 0, false));
                        itemCameraInfo.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemGUID", 0, false));
                        itemCameraInfo.CameraID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CameraID", 0, false));
                        itemCameraInfo.CameraType = clsGeneral.getColumnData(dataRow, "CameraType", string.Empty, false) as string;
                        itemCameraInfo.Pixel = clsGeneral.getColumnData(dataRow, "Pixel", string.Empty, false) as string;
                        itemCameraInfo.Zoom = clsGeneral.getColumnData(dataRow, "Zoom", string.Empty, false) as string;

                        itemCameraList.Add(itemCameraInfo);

                    }
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return itemCameraList;
        }
        public static List<CameraConfig> GetCameraConfigList()
        {
            List<CameraConfig> CameraConfigList =  new List<CameraConfig>();
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            CameraConfig cameraConfig = null;

            DataTable datTable = new DataTable();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@CameraID", 0);

                arrSpFieldSeq = new string[] { "@CameraID" };
                datTable = objDB.GetTableRecords(objCompHash, "av_CameraConfigSelect", arrSpFieldSeq);

                if (datTable != null && datTable.Rows.Count > 0)
                {
                    string zoom = string.Empty;
                    foreach (DataRow dataRow in datTable.Rows)
                    {

                        zoom = string.Empty;
                        cameraConfig = new CameraConfig();
                        cameraConfig.CameraID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CameraID", 0, false));
                        cameraConfig.Pixel = clsGeneral.getColumnData(dataRow, "Pixel", string.Empty, false) as string;
                        cameraConfig.Zoom = clsGeneral.getColumnData(dataRow, "Zoom", string.Empty, false) as string;
                        zoom = !string.IsNullOrEmpty(cameraConfig.Zoom) ? " - " + cameraConfig.Zoom : string.Empty;
                        cameraConfig.CameraConfigs = cameraConfig.Pixel + zoom;
                        CameraConfigList.Add(cameraConfig);

                    }
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return CameraConfigList;
        }

        public static void ItemCameraInsertUpdate(ItemCameraInfo itemCameraInfo)
        {
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@ItemCameraID", itemCameraInfo.ItemCameraID);
                objCompHash.Add("@ItemGUID", itemCameraInfo.ItemGUID);
                objCompHash.Add("@CameraID", itemCameraInfo.CameraID);
                objCompHash.Add("@CameraType", itemCameraInfo.CameraType);



                arrSpFieldSeq = new string[] { "@ItemCameraID", "@ItemGUID", "@CameraID", "@CameraType" };
                db.ExeCommand(objCompHash, "Av_itemCameraInsertUpdate", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {
                //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public void ItemCameraDelete(int ItemCameraID)
        {
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@ItemCameraID", ItemCameraID);




                arrSpFieldSeq = new string[] { "@ItemCameraID" };
                db.ExeCommand(objCompHash, "Av_itemCameraDelete", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {
                //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }



    #endregion

    #region Create Item company SKU the DB entries --
    public static void SKUEnableDisableUpdate(string itemcompanyguids, bool isDisable, int userId)
    {
        avii.Classes.DBConnect db = new avii.Classes.DBConnect();
        string[] arrSpFieldSeq;
        Hashtable objCompHash = new Hashtable();

        try
        {
            objCompHash.Add("@itemcompanyguids", itemcompanyguids);
            objCompHash.Add("@isDisable", isDisable);
            objCompHash.Add("@userId", userId);
            

            arrSpFieldSeq = new string[] { "@itemcompanyguids", "@isDisable", "@userId" };
            db.ExeCommand(objCompHash, "av_ItemCompanyAssign_DisableEnable", arrSpFieldSeq);


        }
        catch (Exception objExp)
        {
            //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
            throw objExp;
        }
        finally
        {
            objCompHash = null;
            arrSpFieldSeq = null;
        }
    }
    public void createItemcompanySKU(int itemcompanyguid, int itemGuid, int companyGUID, string sku,int editeAll)
        {
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@itemcompanyguid", itemcompanyguid);
                objCompHash.Add("@itemguid", itemGuid);
                objCompHash.Add("@companyID", companyGUID);
                objCompHash.Add("@sku", sku);
                objCompHash.Add("@editeAll", editeAll);


                arrSpFieldSeq = new string[] { "@itemcompanyguid", "@itemguid", "@companyID", "@sku", "@editeAll" };
                db.ExeCommand(objCompHash, "av_ItemCompanyAssign_insertupdate", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {
                //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

      //  public static int AssignItemCompanySKU(CompanySKUno itemSkuInfo, int userID, bool IsFinishedSKU, out string isDuplicate)
    public static int AssignItemCompanySKU(CompanySKUno itemSkuInfo, int userID, out string isDuplicate)
    {
            int returnValue = 0;
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
        string skuRequest = clsGeneral.SerializeObject(itemSkuInfo);

        SV.Framework.Admin.ItemLogModel logRequest = new SV.Framework.Admin.ItemLogModel();
        logRequest.ItemCompanyGUID = itemSkuInfo.ItemcompanyGUID;
        logRequest.RequestData = skuRequest;
        logRequest.CreateUserID = userID;
        logRequest.ItemGUID = itemSkuInfo.ItemGUID;
        if (itemSkuInfo.ItemcompanyGUID > 0)
            logRequest.ActionName = "SKU Update";
        else
            logRequest.ActionName = "SKU Create";
        if (itemSkuInfo.IsDisable)
            logRequest.Status = "Disable";
        else
            logRequest.Status = "Enable";

        logRequest.SKU = itemSkuInfo.SKU;



        try
        {
            objCompHash.Add("@ItemCompanyGUID", itemSkuInfo.ItemcompanyGUID);
            objCompHash.Add("@CompanyID", itemSkuInfo.CompanyID);
            objCompHash.Add("@SKU", itemSkuInfo.SKU);
            objCompHash.Add("@ItemGUID", itemSkuInfo.ItemGUID);
            // objCompHash.Add("@MASSKU", itemSkuInfo.MASSKU);
            objCompHash.Add("@WarehouseCode", itemSkuInfo.WarehouseCode);
            objCompHash.Add("@MaximumStockLevel", itemSkuInfo.MaximumStockLevel);
            objCompHash.Add("@MinimumStockLevel", itemSkuInfo.MinimumStockLevel);
            objCompHash.Add("@IsDisable", itemSkuInfo.IsDisable);
            objCompHash.Add("@ContainerQuantity", itemSkuInfo.ContainerQty);
            objCompHash.Add("@DPCI", itemSkuInfo.DPCI);
            objCompHash.Add("@MappedItemCompanyGUID", itemSkuInfo.MappedItemCompanyGUID ?? 0);
            objCompHash.Add("@PalletQuantity", itemSkuInfo.PalletQuantity);
            objCompHash.Add("@SWVersion", itemSkuInfo.SWVersion);
            objCompHash.Add("@HWVersion", itemSkuInfo.HWVersion);
            objCompHash.Add("@BoxDesc", itemSkuInfo.BoxDesc);

            //objCompHash.Add("@UserID", userID);


            arrSpFieldSeq = new string[] { "@ItemCompanyGUID", "@CompanyID", "@SKU", "@ItemGUID", "@WarehouseCode",
                "@MaximumStockLevel", "@MinimumStockLevel", "@IsDisable","@ContainerQuantity","@DPCI", "@MappedItemCompanyGUID", "@PalletQuantity",
            "@SWVersion", "@HWVersion", "@BoxDesc"};
            //arrSpFieldSeq = new string[] { "@ItemCompanyGUID", "@CompanyID", "@SKU", "@ItemGUID", "@WarehouseCode", "@IsFinishedSKU" };
            db.ExeCommand(objCompHash, "av_ItemCompanyAssign_InsertUpdate2", arrSpFieldSeq, "@ItemCount", out returnValue, "@IsDuplicate", out isDuplicate);
            if (itemSkuInfo.ItemcompanyGUID > 0)
                logRequest.ResponseData = "Updated successfully";
            else
                logRequest.ResponseData = "Submitted successfully";
        }
        catch (Exception objExp)
        {
            logRequest.Comment = objExp.Message;
            //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
            throw objExp;
        }
        finally
        {

            SV.Framework.Admin.ItemLogOperation.ItemLogInsert(logRequest);

            objCompHash = null;
            arrSpFieldSeq = null;
        }
            return returnValue;
        }
        #endregion
        # region Delete Item company SKU the DB entries --

        public void DeleteitemCompanysku(int itemcompskuGuid, int userID)
        {
            SV.Framework.Admin.ItemLogModel logRequest = new SV.Framework.Admin.ItemLogModel();
            logRequest.ItemCompanyGUID = itemcompskuGuid;
            logRequest.ItemGUID = 0;
            logRequest.RequestData = "ItemCompanyGUID: " + itemcompskuGuid;
            logRequest.CreateUserID = userID;
        logRequest.ActionName = "SKU Delete";
        logRequest.Status = "Deleted";

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                logRequest.ResponseData = "Deleted successfully";
                SV.Framework.Admin.ItemLogOperation.ItemLogInsert(logRequest);

                arrSpFieldSeq = new string[] { "@itemcompanyguid" };
                objCompHash.Add("@itemcompanyguid", itemcompskuGuid);
                db.ExeCommand(objCompHash, "av_ItemCompanyAssign_delete", arrSpFieldSeq);

                
            }
            catch (Exception exp)
            {
            logRequest.Comment = exp.Message;
            SV.Framework.Admin.ItemLogOperation.ItemLogInsert(logRequest);

            //throw exp;
        }
        finally
            {

               
            }
        }
        #endregion
        # region Create Item Pricing to creat the DB entries --
        public void createItemPricing(int itemGuid, int pricingGUID, string webPrice, string retailPrice
            , string wholesalePrice, int priceType)
        {
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@ItemGUID", itemGuid);
                objCompHash.Add("@PricingGUID", pricingGUID);
                objCompHash.Add("@webPrice", webPrice);
                objCompHash.Add("@retailPrice", retailPrice);
                objCompHash.Add("@wholesalePrice", wholesalePrice);
                objCompHash.Add("@priceType", priceType);
                //objCompHash.Add("@itemSizeGUID", itemSize);

                arrSpFieldSeq = new string[] { "@ItemGUID", "@PricingGUID", "@webPrice", "@retailPrice", "@wholesalePrice", "@priceType" };
                db.ExeCommand(objCompHash, "av_ItemPricing_update", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {
                //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        #endregion

        # region Create Item Specifications to creat the DB entries --
        public void createItemSpecifications(int itemGuid, int SpecificationGUID, string SpeficationTxt)
        {
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@ItemGUID", itemGuid);
                objCompHash.Add("@SpecificationGUID", SpecificationGUID);
                objCompHash.Add("@SpeficationTxt", SpeficationTxt);

                arrSpFieldSeq = new string[] { "@ItemGUID", "@SpecificationGUID", "@SpeficationTxt" };
                db.ExeCommand(objCompHash, "av_ItemSpecification_update", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {
                //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        #endregion

        # region Create Item Images to create the DB entries --itemGUID ,ImagePath ,ImageType ,ImageTxt
        public DataTable createItemImages(int itemGuid, int imageGUID, string ImagePath, int ImageType, string ImageTxt)
        {
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@ItemGUID", itemGuid);
                objCompHash.Add("@imageGUID", imageGUID);
                objCompHash.Add("@ImagePath", ImagePath);
                objCompHash.Add("@ImageType", ImageType);
                objCompHash.Add("@ImageTxt", ImageTxt);
                //objCompHash.Add("@colorGuid", colorGuid);
                //objCompHash.Add("@itemSizeGUID", itemsizeguid);



                arrSpFieldSeq = new string[] { "@ItemGUID", "@imageGUID", "@ImagePath", "@ImageType", "@ImageTxt" };
                dt = db.GetTableRecords(objCompHash, "av_ItemImage_Update", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {
                //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return dt;
        }
        #endregion
        
        //# region Create Item sizes to create the DB entries --itemGUID ,size ,available ,comment
        //public void createItemSizes(int sizesGUID, int itemGuid, int size, int available, string comment, int itemImageGuid,string weight)
        //{
        //    avii.Classes.DBConnect db = new avii.Classes.DBConnect();
        //    string[] arrSpFieldSeq;
        //    Hashtable objCompHash = new Hashtable();

        //    try
        //    {
        //        objCompHash.Add("@SizesGUID", sizesGUID);
        //        objCompHash.Add("@ItemGUID", itemGuid);
        //        objCompHash.Add("@Size", size);
        //        objCompHash.Add("@Available", available);
        //        objCompHash.Add("@Comment", comment);
        //        objCompHash.Add("@itemImageGUID", itemImageGuid);
        //        objCompHash.Add("@weight", weight);

        //        arrSpFieldSeq = new string[] { "@SizesGUID", "@ItemGUID", "@Size", "@Available", "@Comment", "@itemImageGUID", "@weight" };
        //        db.ExeCommand(objCompHash, "ItemSizes_Update", arrSpFieldSeq);
        //    }
        //    catch (Exception objExp)
        //    {
        //        //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
        //        throw objExp;
        //    }
        //    finally
        //    {
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }
        //}
        //#endregion

        //# region Create Item createItemColors to create the DB entries 
        //public void createItemColors(int colorGUID, int itemGuid, string color)
        //{
        //    avii.Classes.DBConnect db = new avii.Classes.DBConnect();
        //    string[] arrSpFieldSeq;
        //    Hashtable objCompHash = new Hashtable();

        //    try
        //    {
        //        objCompHash.Add("@ColorGUID", colorGUID);
        //        objCompHash.Add("@ItemGUID", itemGuid);
        //        objCompHash.Add("@Color", color);




        //        arrSpFieldSeq = new string[] { "@ColorGUID", "@ItemGUID", "@Color"};
        //        db.ExeCommand(objCompHash, "itemColor_update", arrSpFieldSeq);


        //    }
        //    catch (Exception objExp)
        //    {
        //        //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
        //        throw objExp;
        //    }
        //    finally
        //    {
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }
        //}
        //#endregion

        //#region ds(iteminfo, sizes, addsize, Itemimage, latestitem, price, color),delete item, getItemCategories



        //public DataSet itemInfo(int itemID, string contactType)
        //{ 
        //    avii.Classes.DBConnect objDB=new avii.Classes.DBConnect();
        //    try
        //    {
        //        string sql = "exec getItemDetails " + itemID + ", " + contactType + " ";
        //        DataSet ds = objDB.GetDataSet(sql);
        //        return ds;
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //    }
        //}

        //public DataSet Sizes(int itemID, int notitemsize)
        //{
        //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        //    try
        //    {
        //        string sql = "exec sizes_select " + itemID + ", " + notitemsize;
        //        DataSet ds = objDB.GetDataSet(sql);
        //        return ds;
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //    }
        //}

        //public DataSet AddSizes()
        //{
        //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        //    try
        //    {
        //        string sql = "select sizeGUID,waist from av_size order by waist";
        //        DataSet ds = objDB.GetDataSet(sql);
        //        return ds;
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //    }
        //}

        public static void DeleteItems(int itemGUID, int orderFlag, int userId)
        {
        SV.Framework.Admin.ItemLogModel logRequest = new SV.Framework.Admin.ItemLogModel();
        logRequest.ItemCompanyGUID = 0;
        logRequest.ItemGUID = itemGUID;
        logRequest.RequestData = "ItemGUID: " + itemGUID;
        logRequest.CreateUserID = userId;
        //logRequest.ItemGUID = 0;
        if (orderFlag == 0)
        {
            logRequest.ActionName = "Product Delete";
            logRequest.Status = "Deleted";
        }
        else
        {
            logRequest.ActionName = "Product Inactivate";
            logRequest.Status = "Inactive";
        }
        DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {

                objCompHash.Add("@ItemGUID", itemGUID);
                

                arrSpFieldSeq = new string[] { "@ItemGUID" };
            if (orderFlag == 0)
            {
                db.ExeCommand(objCompHash, "av_item_delete", arrSpFieldSeq);
                logRequest.ResponseData = "Deleted successfully";
            }
            else
            {
                db.ExeCommand(objCompHash, "av_item_deactivate", arrSpFieldSeq);
                logRequest.ResponseData = "Inactivated successfully";
            }
            

            }
            catch (Exception objExp)
            {
            logRequest.Comment = objExp.Message;
                //throw objExp;
            }
            finally
            {
                SV.Framework.Admin.ItemLogOperation.ItemLogInsert(logRequest);
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public void deleteItem(string sql)
        {
            //string sql = "exec item_delete " + itemGUID;
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            try
            {
                objDB.ExeCommand(sql);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
            }
        }

        public DataSet getItemImages(int itemID)
        {
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            try
            {
                string sql = "exec ItemImage_Select " + itemID + ",-1,0,0";
                DataSet ds = objDB.GetDataSet(sql);
                return ds;
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
            }
        }

        //public DataSet getLatestItems(int categoryID, int itemGUID ,string contactType)
        //{
        //    string sql = "exec newItem_Select " + categoryID + ", " + itemGUID + "," + contactType;
        //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        //    try
        //    {
        //        DataSet ds = objDB.GetDataSet(sql);
        //        return ds;
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //    }

        //}

        //public DataSet getItemPrice(int itemID, string itemPrice)
        //{
        //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        //    try
        //    {
        //        string sql = "exec ItemPrice_Select " + itemID + ",-1," + itemPrice;
        //        DataSet ds = objDB.GetDataSet(sql);
        //        return ds;
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //    }
        //}

        //public DataSet getItemColor(int itemID)
        //{
        //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        //    try
        //    {
        //        string sql = "exec ItemColor_Select " + itemID + ",-1";
        //        DataSet ds = objDB.GetDataSet(sql);
        //        return ds;
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //    }
        //}

        //public string getItemCategories()
        //{
        //    string itemCategory = "";
        //    string categoryID = "";
        //    string parentCategoryID = "";
        //    List<avii.Classes.ItemCategory> objCategoryList = new List<avii.Classes.ItemCategory>();
        //    objCategoryList = getItemCategoryTree(0, 0,1, true, 1, -1,true);

        //    foreach (avii.Classes.ItemCategory objItemCat in objCategoryList)
        //    {
        //        if (itemCategory == "")
        //        {
        //            itemCategory = objItemCat.CategoryName;
        //            categoryID = objItemCat.CategoryGUID.ToString();
        //            parentCategoryID = objItemCat.ParentCategoryGUID.ToString();
        //        }
        //        else
        //        {
        //            itemCategory += ", " + objItemCat.CategoryName;
        //            categoryID += ", " + objItemCat.CategoryGUID.ToString();
        //            parentCategoryID += ", " + objItemCat.ParentCategoryGUID.ToString();
        //        }
        //    }
        //    itemCategory = itemCategory + "$" + categoryID + "$" + parentCategoryID;

        //    return itemCategory;
        //}

        //#endregion

        //#region (now not in use) getSortedItemList - get the list of items for a category / all categories / particular item
        //public List<avii.Classes.InventoryItems> getSortedItemList(int iCategoryGUID, int size, int price)
        //{
        //    List<avii.Classes.InventoryItems> objItemList = new List<avii.Classes.InventoryItems>();
        //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        //    Hashtable objCompHash = new Hashtable();
        //    string[] arrSpFieldSeq;
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        //objCompHash.Add("@ItemGUID", iItemGUID);
        //        objCompHash.Add("@CategoryID", iCategoryGUID);
        //        objCompHash.Add("@size", size);
        //        objCompHash.Add("@Price", price);

        //        arrSpFieldSeq = new string[] { "@CategoryID", "@size", "@Price" };
        //        ds = objDB.GetDataSet(objCompHash, "av_sortedItem", arrSpFieldSeq);

        //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow dataRow in ds.Tables[0].Rows)
        //            {
        //                avii.Classes.InventoryItems objItem = new avii.Classes.InventoryItems();

        //                objItem.ItemGUID = Convert.ToInt32(dataRow["itemGUID"]);
        //                objItem.ItemCode = dataRow["itemCode"].ToString();
        //                objItem.ModelNumber = dataRow["modelnumber"].ToString();
        //                objItem.ItemColor = dataRow["color"].ToString();
        //                objItem.ItemName = dataRow["itemname"].ToString();
        //                objItem.Upc = dataRow["upc"].ToString();
        //                objItem.ItemCategoryGUID = Convert.ToInt32(dataRow["CategoryID"]);
        //                objItem.ItemCategory = dataRow["CategoryName"].ToString();
        //                objItem.ItemMakerGUID = Convert.ToInt32(dataRow["makerGUID"]);
        //                objItem.ItemMaker = dataRow["makerName"].ToString();
        //                objItem.ItemDesc1 = dataRow["itemDescription"].ToString();
        //                objItem.ItemDesc2 = dataRow["ItemDescrition2"].ToString();

        //                objItemList.Add(objItem);
        //            }
        //        }
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }
        //    return objItemList;
        //}
        //#endregion

        //#region getSearchItemList - get the particular itemname  or itemcode item
        //public List<avii.Classes.InventoryItems> getSearchItemList(string itemNameOrCode)
        //{
        //    List<avii.Classes.InventoryItems> objItemList = new List<avii.Classes.InventoryItems>();
        //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        //    Hashtable objCompHash = new Hashtable();
        //    string[] arrSpFieldSeq;
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        objCompHash.Add("@itemNameOrCode", itemNameOrCode.Trim());
        //        //objCompHash.Add("@categoryGUID", iCategoryGUID);
        //        arrSpFieldSeq = new string[] { "@itemNameOrCode" };
        //        ds = objDB.GetDataSet(objCompHash, "ItemSearch_Select", arrSpFieldSeq);

        //        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            foreach (DataRow dataRow in ds.Tables[0].Rows)
        //            {
        //                avii.Classes.InventoryItems objItem = new avii.Classes.InventoryItems();

        //                objItem.ItemGUID = Convert.ToInt32(dataRow["itemGUID"]);
        //                objItem.ItemCode = dataRow["itemCode"].ToString();
        //                objItem.ModelNumber = dataRow["modelnumber"].ToString();
        //                objItem.ItemColor = dataRow["color"].ToString();
        //                objItem.ItemName = dataRow["itemname"].ToString();
        //                objItem.Upc = dataRow["upc"].ToString();
        //                objItem.ItemCategoryGUID = Convert.ToInt32(dataRow["CategoryID"]);
        //                objItem.ItemCategory = dataRow["CategoryName"].ToString();
        //                objItem.ItemMakerGUID = Convert.ToInt32(dataRow["makerGUID"]);
        //                objItem.ItemMaker = dataRow["makerName"].ToString();
        //                objItem.ItemDesc1 = dataRow["itemDescription"].ToString();
        //                objItem.ItemDesc2 = dataRow["ItemDescrition2"].ToString();

        //                objItemList.Add(objItem);
        //            }
        //        }
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }
        //    return objItemList;
        //}
        //#endregion

        //#region getSizebyCategory - 
        //public DataSet getSizebyCategory(int CategoryID)
        //{
            
        //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        //    Hashtable objCompHash = new Hashtable();
        //    string[] arrSpFieldSeq;
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        objCompHash.Add("@CategoryID", CategoryID);
        //       // objCompHash.Add("@pricingGUID", pricingGUID);
        //       // objCompHash.Add("@customerType", "0");

        //        arrSpFieldSeq = new string[] { "@CategoryID"};
        //        ds = objDB.GetDataSet(objCompHash, "CategoryBySize_select", arrSpFieldSeq);

                
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }

        //    return ds;
        //}
        // #endregion

        //#region getCustomerIfo - 
        //public DataSet getCustomerIfo(int CustomerID)
        //{

        //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        //    Hashtable objCompHash = new Hashtable();
        //    string[] arrSpFieldSeq;
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        objCompHash.Add("@CustomerID", CustomerID);
        //        // objCompHash.Add("@pricingGUID", pricingGUID);
        //        // objCompHash.Add("@customerType", "0");

        //        arrSpFieldSeq = new string[] { "@CustomerID" };
        //        ds = objDB.GetDataSet(objCompHash, "av_customer_Select", arrSpFieldSeq);


        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }

        //    return ds;
        //}
        //#endregion

        //#region // currently not in use
        //public DataSet getmakerlabetlist(int itemGUID, int categoryid)
        //{
            
        //    avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
        //    Hashtable objCompHash = new Hashtable();
        //    string[] arrSpFieldSeq;
        //    DataSet ds = new DataSet();

        //    try
        //    {
        //        objCompHash.Add("@itemGUID", itemGUID);
        //        objCompHash.Add("@categoryid", categoryid);
        //        // objCompHash.Add("@pricingGUID", pricingGUID);
        //        // objCompHash.Add("@customerType", "0");

        //        arrSpFieldSeq = new string[] { "@itemGUID", "@categoryid" };
        //        ds = objDB.GetDataSet(objCompHash, "makeralphabet_select", arrSpFieldSeq);


        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }

        //    return ds;
        //}
        //#endregion

        public string fnReadText(string sEmailName)
        {
            StreamReader objStrmRdr;
            string sMsg;
            objStrmRdr = new StreamReader(sEmailName);
            sMsg = objStrmRdr.ReadToEnd();
            objStrmRdr.Close();
            return sMsg;
        }
        public string fnWriteText(string sfileName, string massege)
        {
            StreamWriter objStrmWrite;
            //string sMsg;
            objStrmWrite = new StreamWriter(sfileName);
            objStrmWrite.WriteLine(massege);
            objStrmWrite.Close();
            return massege;
        }

    }
