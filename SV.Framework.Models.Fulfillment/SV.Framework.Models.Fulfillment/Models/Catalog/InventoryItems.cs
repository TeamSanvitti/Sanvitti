using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{
    public class InventoryItems
    {
        #region "Private Attributes"
        private int itemGUID;
        private string itemCode;
        private string itemName;
        private CatalogEnums.InventoryItemType itemType;
        private int _itemType;
        private string technology;
        private int technologyID;
        private int categoryGUID;
        private int parentcategoryGUID;
        private string category;
        private CatalogEnums.DeviceTypeCategory deviceTypeCatg;
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

        public CatalogEnums.InventoryItemType ItemType
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

        public CatalogEnums.DeviceTypeCategory DeviceType
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

        //#region "Public Methods"

        //public InventoryItems LoadData(DataSet dataSet)
        //{
        //    // Populate the object from the dataset
        //    InventoryItems invItem = new InventoryItems();

        //    return invItem;

        //}

        //public ReturnStatus SetData(InventoryItems inventoryItem)
        //{
        //    // Call the Database object to save the data to the database. I advice you to use OPENXML to save the data to the SQLServer database. 
        //    // OPENXML will provide more control on the database transaction and data integrity.
        //    ReturnStatus returnStatus = ReturnStatus.NoActionTaken;
        //    InventoryItems invItem = new InventoryItems();

        //    return returnStatus;
        //}

        //#endregion
    }
}
