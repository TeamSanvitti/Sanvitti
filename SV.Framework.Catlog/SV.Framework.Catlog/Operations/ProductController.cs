using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace SV.Framework.Catalog
{
    public class ProductController : BaseCreateInstance
    {
        public List<ItemCategory> GetItemCategoryTree(int catID, int depth, int hasitems
            , bool withparent, int active, int parentcatID, bool withIndent, bool excludeKitted, bool OnlyNonEsn, bool IsEsnRequired)
        {

            if (0 == catID)
                catID = -1;
            List<ItemCategory> lstItemCategoryTree = new List<ItemCategory>();

            TraverseCategories(ref lstItemCategoryTree, GetItemsCategoryList(catID, "", hasitems, withparent, active
                , parentcatID, excludeKitted, OnlyNonEsn, IsEsnRequired), 0, 0, withIndent);

            return lstItemCategoryTree;
        }
        public List<ItemCategory> TraverseCategories(ref List<ItemCategory> itemcategoryListTree
            , List<ItemCategory> itemcategoryList, int catid, int depth, bool withIndent)
        {
            if (itemcategoryList != null && itemcategoryList.Count > 0)
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
                        itemcategoryListTree = TraverseCategories(ref itemcategoryListTree, itemcategoryList, itemcategoryList[ictr].CategoryGUID, depth + 1, withIndent);
                    }
                }
            }
            return itemcategoryListTree;
        }

        public List<ItemCategory> GetItemsCategoryList(int categoryGUID, string scatname
           , int hasitems, bool withparent, int active, int parentcategoryGUID, bool excludeKitted, bool OnlyNonEsn, bool IsEsnRequired)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();
            List<ItemCategory> lstItemCategoryList = productController.GetItemsCategoryList(categoryGUID, scatname
           ,  hasitems,  withparent, active,  parentcategoryGUID, excludeKitted,  OnlyNonEsn,  IsEsnRequired);// new List<ItemCategory>();
            
            return lstItemCategoryList;
        }



        #region getMakerList -- 
        public List<Maker> getMakerList(int makerGUID, string makerName
            , int hasitems, int active, int alphalist, int categoryid, int parentid, int topmaker)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<Maker> lstMaker = productController.getMakerList(makerGUID, makerName
            , hasitems, active, alphalist, categoryid, parentid, topmaker);// new List<Maker>();
            
            return lstMaker;
        }
        public List<ProductType> GetProductTypes(int productTypeID)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<ProductType> productTypes = productController.GetProductTypes(productTypeID);// new List<ProductType>();
            return productTypes;
        }
        public List<ProductCondition> GetProductCondition(int conditionID)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<ProductCondition> productConditions = productController.GetProductCondition(conditionID);// new List<ProductCondition>();
            return productConditions;
        }

        #endregion

        #region createItem(createitemSize and createitemImages) - uses the list of ItemSize and ItemImage objects to creat the DB entries --
        public int createInventoryItem(int itemGuid, int categoryID, string modelNumber
            , string itemName, string itemDesc, string itemFullDesc, string UPCcode
            , int active, int MakerGUID, int companyid, string sku, bool allowRMA, bool showunderCatalog, string itemDocument,
            List<Carriers> carrierList, bool allowLTE, bool allowSIM, int userID, double Weight, bool IsKitted, bool allowESN, string storage,
            int storageQty, bool IsDisplayName, int esnLength, int meidLength, int productTypeID, int conditionID, bool restock, string OSType)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            int returnValue = productController.createInventoryItem( itemGuid,  categoryID,  modelNumber
            ,  itemName,  itemDesc,  itemFullDesc,  UPCcode
            ,  active,  MakerGUID,  companyid,  sku,  allowRMA,  showunderCatalog,  itemDocument,
            carrierList,  allowLTE,  allowSIM,  userID,  Weight,  IsKitted,  allowESN,  storage,
             storageQty,  IsDisplayName,  esnLength,  meidLength,  productTypeID,  conditionID,  restock, OSType);
            return returnValue;
        }
        #endregion
        #region getItemList - get the list of items for a category / all categories / particular item
        public List<InventoryItems> getItemList(int iCategoryGUID, int iItemGUID, int active, int makerGUID, int technologyGUID, string modelnumber,
            int topnewitem, string sku, string upc, string color, int companyID, string itemCode, string warehouseCode, int showunderCatalog,
            int productTypeID, int conditionID, bool restock)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<InventoryItems> objItemList = productController.getItemList(iCategoryGUID,  iItemGUID,  active,  makerGUID,  technologyGUID,  modelnumber,
             topnewitem,  sku,  upc,  color,  companyID,  itemCode,  warehouseCode,  showunderCatalog,
             productTypeID,  conditionID,  restock);// new List<InventoryItems>();
            return objItemList;
        }

        public List<InventoryItems> GetCustomerSKUList(int iCategoryGUID, int iItemGUID, string modelnumber, string sku, string upc, int companyID, bool Isdisable)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<InventoryItems> objItemList = productController.GetCustomerSKUList(iCategoryGUID, iItemGUID, modelnumber,  sku,  upc,  companyID,  Isdisable); // new List<InventoryItems>();
            return objItemList;
        }

        public List<InventoryItems> GetProductList(int iCategoryGUID, int iItemGUID, int active, int makerGUID, int technologyGUID, string modelnumber, int topnewitem, string sku, string upc, string color, int companyID, string itemCode, string searchText)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<InventoryItems> objItemList = productController.GetProductList(iCategoryGUID, iItemGUID, active, makerGUID, technologyGUID, modelnumber, topnewitem, sku, upc, color, companyID, itemCode, searchText);// new List<InventoryItems>();
            return objItemList;
        }
        #endregion
        #region getItemImageList - get the list of images for an items all images / particular image
        public List<InventoryItemImages> getItemImageList(int ItemGUID, int itemImageGUID, int itemFlag, int imageFlag)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<InventoryItemImages> objImageList = productController.getItemImageList(ItemGUID, itemImageGUID, itemFlag, imageFlag);// new List<InventoryItemImages>();
            return objImageList;
        }
        #endregion

        #region getItemPriceDetails - get the list of prices for the item all prices / particular price
        public List<InventoryItemPricing> getItemPriceList(int ItemGUID, int pricingGUID)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<InventoryItemPricing> objItemPriceList = productController.getItemPriceList(ItemGUID, pricingGUID);// new List<InventoryItemPricing>();
            return objItemPriceList;
        }
        #endregion
        public List<MappedSKUModel> GetMappedSKUs(int itemGUID)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<MappedSKUModel> skuList = productController.GetMappedSKUs(itemGUID);// new List<MappedSKUModel>();
            
            return skuList;
        }
        #region getItemSpecifications - get the list of Specifications for the item all Specifications / particular Specification
        public List<InventoryItemSpecifications> getItemSpecifications(int ItemGUID)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<InventoryItemSpecifications> objItemSpecifications = productController.getItemSpecifications(ItemGUID);// new List<InventoryItemSpecifications>();
            
            return objItemSpecifications;
        }
        #endregion

        #region getCompanySKUno --
        public List<CompanySKUno> getCompanySKUnoList(int itemGUID, int companyid, string sku)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<CompanySKUno> lstCompanySKUno = productController.getCompanySKUnoList(itemGUID, companyid, sku);
            return lstCompanySKUno;
        }
        public  CompanySKUno GetCompanyItemSKUInfo(int itemCompanyGUID)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            CompanySKUno objCompanySKUno = productController.GetCompanyItemSKUInfo(itemCompanyGUID);
            return objCompanySKUno;
        }

        #endregion
        public  ItemCameraInfo GetItemCameraInfo(int itemCameraID)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            ItemCameraInfo itemCameraInfo = productController.GetItemCameraInfo(itemCameraID);
            return itemCameraInfo;
        }
        public  List<ItemCameraInfo> GetItemCameraList(int Itemguid)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<ItemCameraInfo> itemCameraList = productController.GetItemCameraList(Itemguid);// new List<ItemCameraInfo>();

            return itemCameraList;
        }

        public  List<CameraConfig> GetCameraConfigList()
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<CameraConfig> CameraConfigList = productController.GetCameraConfigList();// new List<CameraConfig>();
            return CameraConfigList;
        }

        public  void ItemCameraInsertUpdate(ItemCameraInfo itemCameraInfo)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();
            productController.ItemCameraInsertUpdate(itemCameraInfo);
        }
        public void ItemCameraDelete(int ItemCameraID)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();
            productController.ItemCameraDelete(ItemCameraID);
        }

        #region Create Item company SKU the DB entries --
        public  void SKUEnableDisableUpdate(string itemcompanyguids, bool isDisable, int userId)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();
            productController.SKUEnableDisableUpdate( itemcompanyguids,  isDisable,  userId);
        }
        public void createItemcompanySKU(int itemcompanyguid, int itemGuid, int companyGUID, string sku, int editeAll)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();
            productController.createItemcompanySKU(itemcompanyguid, itemGuid,  companyGUID,  sku,  editeAll);
        }

        //  public static int AssignItemCompanySKU(CompanySKUno itemSkuInfo, int userID, bool IsFinishedSKU, out string isDuplicate)
        public  int AssignItemCompanySKU(CompanySKUno itemSkuInfo, int userID, out string isDuplicate)
        {
            isDuplicate = default;
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            int returnValue = productController.AssignItemCompanySKU(itemSkuInfo, userID, out isDuplicate);
            return returnValue;
        }
        #endregion
        # region Delete Item company SKU the DB entries --

        public void DeleteitemCompanysku(int itemcompskuGuid, int userID)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();
            productController.DeleteitemCompanysku(itemcompskuGuid, userID);
        }
        #endregion
        # region Create Item Pricing to creat the DB entries --
        public void createItemPricing(int itemGuid, int pricingGUID, string webPrice, string retailPrice
            , string wholesalePrice, int priceType)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();
            productController.createItemPricing( itemGuid,  pricingGUID,  webPrice,  retailPrice,  wholesalePrice,  priceType);
        }
        #endregion

        # region Create Item Specifications to creat the DB entries --
        public void createItemSpecifications(int itemGuid, int SpecificationGUID, string SpeficationTxt)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();
            productController.createItemSpecifications(itemGuid, SpecificationGUID, SpeficationTxt);
        }
        #endregion

        # region Create Item Images to create the DB entries --itemGUID ,ImagePath ,ImageType ,ImageTxt
        public DataTable createItemImages(int itemGuid, int imageGUID, string ImagePath, int ImageType, string ImageTxt)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            DataTable dt = productController.createItemImages(itemGuid, imageGUID, ImagePath, ImageType, ImageTxt);// new DataTable();

            return dt;
        }
        #endregion

        public  void DeleteItems(int itemGUID, int orderFlag, int userId)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();
            productController.DeleteItems(itemGUID, orderFlag, userId);
        }
        public void deleteItem(string sql)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();
            productController.deleteItem(sql);
        }

        public List<CustomerWarehouseCode> GetCompanyWarehouseCode(int CompanyID, string warehouseCode, bool active)
        {
            SV.Framework.DAL.Catalog.ProductController productController = SV.Framework.DAL.Catalog.ProductController.CreateInstance<SV.Framework.DAL.Catalog.ProductController>();

            List<CustomerWarehouseCode> warehouseCodeList = productController.GetCompanyWarehouseCode(CompanyID, warehouseCode, active);// new List<CustomerWarehouseCode>();
            
            return warehouseCodeList;
        }


    }
}
