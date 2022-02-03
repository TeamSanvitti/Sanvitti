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

    public class CompanySKUno
    {
        private int itemcompanyGUID;
        private int itemGUID;
        private int companyID;
        private string sku;
        private string massku;
        private string warehouseCode;
        private string companyname;


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
        public string MASSKU
        {
            get
            {
                return massku;
            }
            set
            {
                massku = value;
            }
        }
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
            , int hasitems, bool withparent, int active, int parentcategoryGUID)
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

                //arrSpFieldSeq = new string[] { "@categoryGUID", "@hasitems", "@categoryName", "@active", "@categoryName1", "@categoryName2" };
                arrSpFieldSeq = new string[] { "@CategoryGUID", "@CategoryName" };
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
            , bool withparent, int active, int parentcatID, bool withIndent)
        {
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            if (0 == catID)
                catID = -1;
            List<avii.Classes.ItemCategory> lstItemCategoryTree = new List<avii.Classes.ItemCategory>();

            traverseCategories(ref lstItemCategoryTree, getItemsCategoryList(catID, "", hasitems, withparent, active
                , parentcatID), 0, 0, withIndent);

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
        public int createInventoryItem(int itemGuid, int categoryID, string itemCode, string modelNumber
            , string itemName, string itemDesc,string itemFullDesc, string UPCcode, string itemColor, int itemType
            , int active, int technology, int MakerGUID, int companyid, string sku, bool allowRMA, bool showunderCatalog, string itemDocument, List<Carriers> carrierList)
        {
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
                objCompHash.Add("@ItemCode", itemCode);
                objCompHash.Add("@ModelNumber", modelNumber);
                objCompHash.Add("@ItemName", itemName);
                objCompHash.Add("@ItemDescription", itemDesc);
                objCompHash.Add("@ItemFullDesc", itemFullDesc);
                
                objCompHash.Add("@UPC", UPCcode);
                objCompHash.Add("@Color", itemColor);
                objCompHash.Add("@ItemType", itemType);
                
                objCompHash.Add("@Active", active);
                objCompHash.Add("@Technology", technology);
                objCompHash.Add("@makerGUID", MakerGUID);
                objCompHash.Add("@companyid", companyid);
                objCompHash.Add("@sku", sku);
                
                objCompHash.Add("@AllowRMA", allowRMA);
                objCompHash.Add("@ShowunderCatalog", showunderCatalog);
                objCompHash.Add("@ItemDocument", itemDocument);
                objCompHash.Add("@CarrierXML", carrierXML);

                //objCompHash.Add("@Size_XML", sizeXml);
                //objCompHash.Add("@Image_XML", imageXml);

                //objCompHash.Add("@webPrice", webPrice);
                //objCompHash.Add("@retailPrice", retailPrice);
                //objCompHash.Add("@wholesalePrice", wholesalePrice);
                //objCompHash.Add("@priceType", priceType);
                //objCompHash.Add("@SpeficationTxt", specification);


                arrSpFieldSeq = new string[] { "@ItemGUID", "@CategoryID", "@ItemCode", "@ModelNumber", "@ItemName", "@ItemDescription", "@ItemFullDesc", "@UPC", "@Color", "@ItemType", "@Active", "@Technology", "@makerGUID", "@companyid", "@sku", "@AllowRMA", "@ShowunderCatalog", "@ItemDocument", "@CarrierXML" };
                returnValue = db.ExecCommand(objCompHash, "av_Item_Update", arrSpFieldSeq);

                //errorMessage = ResponseErrorCode.UploadedSuccessfully.ToString();
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
        public List<avii.Classes.InventoryItems> getItemList(int iCategoryGUID, int iItemGUID, int active, int makerGUID, int technologyGUID, string modelnumber, int topnewitem, string sku, string upc, string color, int companyID, string itemCode, string warehouseCode, int showunderCatalog)
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
                objCompHash.Add("@WarehouseCode", warehouseCode);
                objCompHash.Add("@ShowunderCatalog", showunderCatalog);


                arrSpFieldSeq = new string[] { "@ItemGUID", "@categoryGUID", "@active", "@makerGUID", "@itemName", "@technology", "@modelnumber", "@topnewitem", "@sku", "@upc", "@color", "@companyID", "@itemCode", "@WarehouseCode", "@ShowunderCatalog" };
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
                        objItem.TechnologyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CarrierGUID", 0, false));                       
                        InventoryItemType itemType = (InventoryItemType)Enum.Parse(typeof(InventoryItemType), dataRow["ItemType"].ToString());
                        objItem.Item_Type = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemType", 0, false));
                        objItem.ItemType = itemType;
                        objItem.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemGUID", 0, false));
                        objItem.ItemPrice = clsGeneral.getColumnData(dataRow, "webprice", string.Empty, false) as string;
                        objItem.ItemTechnology = clsGeneral.getColumnData(dataRow, "CarrierName", string.Empty, false) as string; 
                        objItem.ParentCategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "parentCategoryGUID", 0, false)); 
                        objItem.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                        objItem.CompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "companyid", 0, false));
                        objItem.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        objItem.AllowRMA = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowRMA", false, false));
                        objItem.ShowunderCatalog = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ShowunderCatalog", false, false));
                        objItem.ItemDocument = clsGeneral.getColumnData(dataRow, "ItemDocument", string.Empty, false) as string; 
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

                arrSpFieldSeq = new string[] { "@itemguid", "@companyID", "@sku"};
                ds = objDB.GetDataSet(objCompHash, "av_ItemCompanyAssign_select", arrSpFieldSeq);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    for (int ictr = 0; ictr < ds.Tables[0].Rows.Count; ictr++)
                    {
                        DataRow dataRow = ds.Tables[0].Rows[ictr];

                        objCompanySKUno = new CompanySKUno();
                        objCompanySKUno.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemcompanyguid", 0, false));
                        objCompanySKUno.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemGUID", 0, false));
                        objCompanySKUno.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "companyID", 0, false));
                        objCompanySKUno.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        objCompanySKUno.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objCompanySKUno.MASSKU = clsGeneral.getColumnData(dataRow, "MASSKU", string.Empty, false) as string;
                        objCompanySKUno.WarehouseCode = clsGeneral.getColumnData(dataRow, "WarehouseCode", string.Empty, false) as string;

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
                        objCompanySKUno.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemcompanyguid", 0, false));
                        objCompanySKUno.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemGUID", 0, false));
                        objCompanySKUno.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "companyID", 0, false));
                        objCompanySKUno.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        objCompanySKUno.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objCompanySKUno.MASSKU = clsGeneral.getColumnData(dataRow, "MASSKU", string.Empty, false) as string;
                        objCompanySKUno.WarehouseCode = clsGeneral.getColumnData(dataRow, "WarehouseCode", string.Empty, false) as string;

                        
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
        # region Create Item company SKU the DB entries --
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

        public static int AssignItemCompanySKU(CompanySKUno itemSkuInfo)
        {
            int returnValue = 0;
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@ItemCompanyGUID", itemSkuInfo.ItemcompanyGUID);
                objCompHash.Add("@CompanyID", itemSkuInfo.CompanyID);
                objCompHash.Add("@SKU", itemSkuInfo.SKU);
                objCompHash.Add("@ItemGUID", itemSkuInfo.ItemGUID);
                objCompHash.Add("@MASSKU", itemSkuInfo.MASSKU);
                objCompHash.Add("@WarehouseCode", itemSkuInfo.WarehouseCode);


                arrSpFieldSeq = new string[] { "@ItemCompanyGUID", "@CompanyID", "@SKU", "@ItemGUID", "@MASSKU", "@WarehouseCode" };
                db.ExeCommand(objCompHash, "av_ItemCompanyAssign_InsertUpdate2", arrSpFieldSeq, "@ItemCount", out returnValue);


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
            return returnValue;
        }
        #endregion
        # region Delete Item company SKU the DB entries --

        public void DeleteitemCompanysku(int itemcompskuGuid)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                arrSpFieldSeq = new string[] { "@itemcompanyguid" };
                objCompHash.Add("@itemcompanyguid", itemcompskuGuid);
                db.ExeCommand(objCompHash, "av_ItemCompanyAssign_delete", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
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

        public static void DeleteItems(int itemGUID, int orderFlag)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {

                objCompHash.Add("@ItemGUID", itemGUID);
                

                arrSpFieldSeq = new string[] { "@ItemGUID" };
                if (orderFlag == 0)
                    db.ExeCommand(objCompHash, "av_item_delete", arrSpFieldSeq);
                else
                    db.ExeCommand(objCompHash, "av_item_deactivate", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
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
