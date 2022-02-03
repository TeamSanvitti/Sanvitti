using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Catalog
{
    public class ProductController : BaseCreateInstance
    {
        public List<ItemCategory> GetItemsCategoryList(int categoryGUID, string scatname
           , int hasitems, bool withparent, int active, int parentcategoryGUID, bool excludeKitted, bool OnlyNonEsn, bool IsEsnRequired)
        {            
            List<ItemCategory> lstItemCategoryList = default;// new List<ItemCategory>();
            using (DBConnect objDB = new DBConnect())
            {
                ItemCategory objItemCategory= default;//;

                DataSet ds = default;//new DataSet();
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

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lstItemCategoryList = new List<ItemCategory>();
                        int iteratefrom = 1;
                        if (withparent)
                            iteratefrom = 0;
                        for (int ictr = iteratefrom; ictr < ds.Tables[0].Rows.Count; ictr++)
                        {
                            DataRow dataRow = ds.Tables[0].Rows[ictr];

                            objItemCategory = new ItemCategory();
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
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return lstItemCategoryList;
        }



        #region getMakerList -- 
        public List<Maker> getMakerList(int makerGUID, string makerName
            , int hasitems, int active, int alphalist, int categoryid, int parentid, int topmaker)
        {
            List<Maker> lstMaker = default;// new List<Maker>();
            using (DBConnect objDB = new DBConnect())
            {
                Maker objMaker;

                DataSet ds = new DataSet();
                Hashtable objCompHash = new Hashtable();
                string[] arrSpFieldSeq;
                try
                {
                    objCompHash.Add("@makerGUID", makerGUID);
                    objCompHash.Add("@makerName", makerName);
                    arrSpFieldSeq = new string[] { "@makerGUID", "@makerName" };
                    ds = objDB.GetDataSet(objCompHash, "av_Maker_Select", arrSpFieldSeq);
                    //if (alphalist < 1)
                    {
                        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                        {
                            lstMaker = new List<Maker>();
                            for (int ictr = 0; ictr < ds.Tables[0].Rows.Count; ictr++)
                            {
                                DataRow dataRow = ds.Tables[0].Rows[ictr];

                                objMaker = new Maker();
                                objMaker.MakerGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "makerGUID", 0, false));
                                objMaker.MakerName = clsGeneral.getColumnData(dataRow, "makerName", string.Empty, false) as string;

                                lstMaker.Add(objMaker);
                            }
                        }
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        
            return lstMaker;
        }
        public List<ProductType> GetProductTypes(int productTypeID)
        {
            List<ProductType> productTypes = default;// new List<ProductType>();
            using (DBConnect objDB = new DBConnect())
            {
                ProductType productType;
                DataTable dt = default;// new DataTable();
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
                            productTypes = new List<ProductType>();
                            foreach (DataRow dataRow in dt.Rows)
                            {
                                productType = new ProductType();
                                productType.ProductTypeID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ProductTypeID", 0, false));
                                productType.Code = clsGeneral.getColumnData(dataRow, "Code", string.Empty, false) as string;
                                productTypes.Add(productType);
                            }
                        }
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return productTypes;
        }
        public List<ProductCondition> GetProductCondition(int conditionID)
        {
            List<ProductCondition> productConditions = default;// new List<ProductCondition>();
            using (DBConnect objDB = new DBConnect())
            {
                ProductCondition productCondition;

                DataTable dt = default;// new DataTable();
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
                            productConditions = new List<ProductCondition>();
                            foreach (DataRow dataRow in dt.Rows)
                            {

                                productCondition = new ProductCondition();
                                productCondition.ConditionID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ConditionID", 0, false));
                                productCondition.Code = clsGeneral.getColumnData(dataRow, "Code", string.Empty, false) as string;
                                productConditions.Add(productCondition);
                            }
                        }
                    }

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
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
            int returnValue = 0;
            SV.Framework.DAL.Catalog.ItemLogOperation ItemLogOperation = SV.Framework.DAL.Catalog.ItemLogOperation.CreateInstance<SV.Framework.DAL.Catalog.ItemLogOperation>();

            using (DBConnect db = new DBConnect())
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

                ItemLogModel logRequest = new ItemLogModel();
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
                //DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
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
                    if (returnValue < 0)
                    {
                        logRequest.ItemGUID = (-1 * returnValue);
                        returnValue = 0;
                    }
                    if (itemGuid > 0)
                        logRequest.ResponseData = "Updated successfully";
                    else
                        logRequest.ResponseData = "Submitted successfully";
                    //errorMessage = ResponseErrorCode.UploadedSuccessfully.ToString();
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //
                    logRequest.Comment = objExp.Message;
                    //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                    //throw objExp;
                }
                finally
                {
                    ItemLogOperation.ItemLogInsert(logRequest);
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnValue;
        }
        #endregion
        #region getItemList - get the list of items for a category / all categories / particular item
        public List<InventoryItems> getItemList(int iCategoryGUID, int iItemGUID, int active, int makerGUID, int technologyGUID, string modelnumber,
            int topnewitem, string sku, string upc, string color, int companyID, string itemCode, string warehouseCode, int showunderCatalog,
            int productTypeID, int conditionID, bool restock)
        {
            List<InventoryItems> objItemList = default;// new List<InventoryItems>();
            using (DBConnect objDB = new DBConnect())
            {
                Hashtable objCompHash = new Hashtable();
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
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
                        objItemList = new List<InventoryItems>();
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            InventoryItems objItem = new InventoryItems();

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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return objItemList;
        }

        public List<InventoryItems> GetCustomerSKUList(int iCategoryGUID, int iItemGUID, string modelnumber, string sku, string upc, int companyID, bool Isdisable)
        {
            List<InventoryItems> objItemList = default; // new List<InventoryItems>();
            using (DBConnect objDB = new DBConnect())
            {
                Hashtable objCompHash = new Hashtable();
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
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
                        objItemList = new List<InventoryItems>();
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            InventoryItems objItem = new InventoryItems();

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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return objItemList;
        }

        public List<InventoryItems> GetProductList(int iCategoryGUID, int iItemGUID, int active, int makerGUID, int technologyGUID, string modelnumber, int topnewitem, string sku, string upc, string color, int companyID, string itemCode, string searchText)
        {
            List<InventoryItems> objItemList = default;// new List<InventoryItems>();
            using (DBConnect objDB = new DBConnect())
            {
                Hashtable objCompHash = new Hashtable();
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
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
                        objItemList = new List<InventoryItems>();
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            InventoryItems objItem = new InventoryItems();

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
                            CatalogEnums.InventoryItemType itemType = (CatalogEnums.InventoryItemType)Enum.Parse(typeof(CatalogEnums.InventoryItemType), dataRow["ItemType"].ToString());
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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return objItemList;
        }
        #endregion
        #region getItemImageList - get the list of images for an items all images / particular image
        public List<InventoryItemImages> getItemImageList(int ItemGUID, int itemImageGUID, int itemFlag, int imageFlag)
        {
            List<InventoryItemImages> objImageList = default;// new List<InventoryItemImages>();
            using (DBConnect objDB = new DBConnect())
            {
                Hashtable objCompHash = new Hashtable();
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();

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
                        objImageList = new List<InventoryItemImages>();
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            InventoryItemImages objItemImage = new InventoryItemImages();
                            objItemImage.ImageGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "imageGUID", 0, false));
                            objItemImage.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemGUID", 0, false));
                            CatalogEnums.ImageType imagesType = (CatalogEnums.ImageType)Enum.Parse(typeof(CatalogEnums.ImageType), dataRow["imageType"].ToString());

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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return objImageList;
        }
        #endregion

        #region getItemPriceDetails - get the list of prices for the item all prices / particular price
        public List<InventoryItemPricing> getItemPriceList(int ItemGUID, int pricingGUID)
        {
            List<InventoryItemPricing> objItemPriceList = default;// new List<InventoryItemPricing>();
            using (DBConnect objDB = new DBConnect())
            {
                Hashtable objCompHash = new Hashtable();
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();

                try
                {
                    objCompHash.Add("@ItemGUID", ItemGUID);
                    objCompHash.Add("@pricingGUID", pricingGUID);
                    objCompHash.Add("@customerType", "0");

                    arrSpFieldSeq = new string[] { "@ItemGUID", "@pricingGUID", "@customerType" };
                    ds = objDB.GetDataSet(objCompHash, "av_ItemPrice_Select", arrSpFieldSeq);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        objItemPriceList = new List<InventoryItemPricing>();
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            InventoryItemPricing objItemPricing = new InventoryItemPricing();
                            objItemPricing.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemGUID", 0, false));
                            objItemPricing.PricingGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pricingGUID", 0, false));
                            objItemPricing.WebPrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "webprice", 0, false));
                            objItemPricing.RetailPrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "retailprice", 0, false));
                            objItemPricing.WholeSalePrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "wholesaleprice", 0, false));
                            //string pricetype = dataRow["priceType"].ToString();

                            CatalogEnums.ItemPricingType priceType = (CatalogEnums.ItemPricingType)Enum.Parse(typeof(CatalogEnums.ItemPricingType), dataRow["priceType"].ToString());
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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return objItemPriceList;
        }
        #endregion
        public List<MappedSKUModel> GetMappedSKUs(int itemGUID)
        {
            List<MappedSKUModel> skuList = default;// new List<MappedSKUModel>();
            using (DBConnect objDB = new DBConnect())
            {
                MappedSKUModel mappedSKU = default;
                Hashtable objCompHash = new Hashtable();
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                try
                {
                    objCompHash.Add("@ItemGUID", itemGUID);
                    //objCompHash.Add("@priceType", pricetype);
                    arrSpFieldSeq = new string[] { "@ItemGUID" };
                    dt = objDB.GetTableRecords(objCompHash, "av_Item_MappedSKU_Select", arrSpFieldSeq);

                    if (dt != null && dt.Rows.Count > 0)
                    {
                        skuList = new List<MappedSKUModel>();
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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return skuList;
        }
        #region getItemSpecifications - get the list of Specifications for the item all Specifications / particular Specification
        public List<InventoryItemSpecifications> getItemSpecifications(int ItemGUID)
        {
            List<InventoryItemSpecifications> objItemSpecifications = default;// new List<InventoryItemSpecifications>();
            using (DBConnect objDB = new DBConnect())
            {
                Hashtable objCompHash = new Hashtable();
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                try
                {
                    objCompHash.Add("@ItemGUID", ItemGUID);
                    //objCompHash.Add("@priceType", pricetype);
                    arrSpFieldSeq = new string[] { "@ItemGUID" };
                    ds = objDB.GetDataSet(objCompHash, "av_ItemSpecification_Select", arrSpFieldSeq);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        objItemSpecifications = new List<InventoryItemSpecifications>();
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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return objItemSpecifications;
        }
        #endregion

        #region getCompanySKUno --
        public List<CompanySKUno> getCompanySKUnoList(int itemGUID, int companyid, string sku)
        {
            List<CompanySKUno> lstCompanySKUno = default;// new List<CompanySKUno>();
            using (DBConnect objDB = new DBConnect())
            {
                CompanySKUno objCompanySKUno = default;

                DataSet ds = default;// new DataSet();
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
                        lstCompanySKUno = new List<CompanySKUno>();
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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return lstCompanySKUno;
        }
        public  CompanySKUno GetCompanyItemSKUInfo(int itemCompanyGUID)
        {
            CompanySKUno objCompanySKUno = default;
            using (DBConnect objDB = new DBConnect())
            {
                DataTable datTable = default;// new DataTable();
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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return objCompanySKUno;
        }

        #endregion
        public  ItemCameraInfo GetItemCameraInfo(int itemCameraID)
        {            
            ItemCameraInfo itemCameraInfo = default;
            using (DBConnect objDB = new DBConnect())
            {
                DataTable datTable = default;// new DataTable();
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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return itemCameraInfo;
        }
        public  List<ItemCameraInfo> GetItemCameraList(int Itemguid)
        {
            List<ItemCameraInfo> itemCameraList = default;// new List<ItemCameraInfo>();

            using (DBConnect objDB = new DBConnect())
            {
                ItemCameraInfo itemCameraInfo = default;

                DataTable datTable = default; // new DataTable();
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
                        itemCameraList = new List<ItemCameraInfo>();
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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return itemCameraList;
        }

        public  List<CameraConfig> GetCameraConfigList()
        {
            List<CameraConfig> CameraConfigList = default;// new List<CameraConfig>();
            using (DBConnect objDB = new DBConnect())
            {
                CameraConfig cameraConfig = default;

                DataTable datTable = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                string[] arrSpFieldSeq;
                try
                {
                    objCompHash.Add("@CameraID", 0);

                    arrSpFieldSeq = new string[] { "@CameraID" };
                    datTable = objDB.GetTableRecords(objCompHash, "av_CameraConfigSelect", arrSpFieldSeq);

                    if (datTable != null && datTable.Rows.Count > 0)
                    {
                        CameraConfigList = new List<CameraConfig>();
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
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return CameraConfigList;
        }

        public  void ItemCameraInsertUpdate(ItemCameraInfo itemCameraInfo)
        {
            using (DBConnect db = new DBConnect())
            {
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
                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }
        public void ItemCameraDelete(int ItemCameraID)
        {
            using (DBConnect db = new DBConnect())
            {
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
                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }

        #region Create Item company SKU the DB entries --
        public  void SKUEnableDisableUpdate(string itemcompanyguids, bool isDisable, int userId)
        {
            using (DBConnect db = new DBConnect())
            {
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
                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }
        public void createItemcompanySKU(int itemcompanyguid, int itemGuid, int companyGUID, string sku, int editeAll)
        {
            using (DBConnect db = new DBConnect())
            {
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
                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }

        //  public static int AssignItemCompanySKU(CompanySKUno itemSkuInfo, int userID, bool IsFinishedSKU, out string isDuplicate)
        public  int AssignItemCompanySKU(CompanySKUno itemSkuInfo, int userID, out string isDuplicate)
        {
            SV.Framework.DAL.Catalog.ItemLogOperation ItemLogOperation = SV.Framework.DAL.Catalog.ItemLogOperation.CreateInstance<SV.Framework.DAL.Catalog.ItemLogOperation>();

            isDuplicate = default;
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                string skuRequest = clsGeneral.SerializeObject(itemSkuInfo);

                ItemLogModel logRequest = new ItemLogModel();
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
                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {

                    ItemLogOperation.ItemLogInsert(logRequest);

                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnValue;
        }
        #endregion
        # region Delete Item company SKU the DB entries --

        public void DeleteitemCompanysku(int itemcompskuGuid, int userID)
        {
            SV.Framework.DAL.Catalog.ItemLogOperation ItemLogOperation = SV.Framework.DAL.Catalog.ItemLogOperation.CreateInstance<SV.Framework.DAL.Catalog.ItemLogOperation>();

            using (DBConnect db = new DBConnect())
            {
                ItemLogModel logRequest = new ItemLogModel();
                logRequest.ItemCompanyGUID = itemcompskuGuid;
                logRequest.ItemGUID = 0;
                logRequest.RequestData = "ItemCompanyGUID: " + itemcompskuGuid;
                logRequest.CreateUserID = userID;
                logRequest.ActionName = "SKU Delete";
                logRequest.Status = "Deleted";

                //DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    logRequest.ResponseData = "Deleted successfully";
                    ItemLogOperation.ItemLogInsert(logRequest);

                    arrSpFieldSeq = new string[] { "@itemcompanyguid" };
                    objCompHash.Add("@itemcompanyguid", itemcompskuGuid);
                    db.ExeCommand(objCompHash, "av_ItemCompanyAssign_delete", arrSpFieldSeq);


                }
                catch (Exception exp)
                {
                    logRequest.Comment = exp.Message;
                    ItemLogOperation.ItemLogInsert(logRequest);
                    Logger.LogMessage(exp, this); //
                    //throw exp;
                }
                finally
                {


                }
            }
        }
        #endregion
        # region Create Item Pricing to creat the DB entries --
        public void createItemPricing(int itemGuid, int pricingGUID, string webPrice, string retailPrice
            , string wholesalePrice, int priceType)
        {
            using (DBConnect db = new DBConnect())
            {
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
                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }
        #endregion

        # region Create Item Specifications to creat the DB entries --
        public void createItemSpecifications(int itemGuid, int SpecificationGUID, string SpeficationTxt)
        {
            using (DBConnect db = new DBConnect())
            {
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
                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }
        #endregion

        # region Create Item Images to create the DB entries --itemGUID ,ImagePath ,ImageType ,ImageTxt
        public DataTable createItemImages(int itemGuid, int imageGUID, string ImagePath, int ImageType, string ImageTxt)
        {
            DataTable dt = default;// new DataTable();

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
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
                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return dt;
        }
        #endregion

        public  void DeleteItems(int itemGUID, int orderFlag, int userId)
        {
            SV.Framework.DAL.Catalog.ItemLogOperation ItemLogOperation = SV.Framework.DAL.Catalog.ItemLogOperation.CreateInstance<SV.Framework.DAL.Catalog.ItemLogOperation>();

            using (DBConnect db = new DBConnect())
            {
                ItemLogModel logRequest = new ItemLogModel();
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
                    Logger.LogMessage(objExp, this); //
                    logRequest.Comment = objExp.Message;
                    //throw objExp;
                }
                finally
                {
                    ItemLogOperation.ItemLogInsert(logRequest);
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }
        public void deleteItem(string sql)
        {
            //string sql = "exec item_delete " + itemGUID;
            using (DBConnect objDB = new DBConnect())
            {
                try
                {
                    objDB.ExeCommand(sql);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                }
            }
        }
        public List<CustomerWarehouseCode> GetCompanyWarehouseCode(int CompanyID, string warehouseCode, bool active)
        {
            List<CustomerWarehouseCode> warehouseCodeList = default;// new List<CustomerWarehouseCode>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@WarehouseCodeGUID", 0);
                    objCompHash.Add("@CompanyID", CompanyID);
                    objCompHash.Add("@WarehouseCode", warehouseCode);
                    objCompHash.Add("@Active", active);

                    arrSpFieldSeq = new string[] { "@WarehouseCodeGUID", "@CompanyID", "@WarehouseCode", "@Active" };

                    dataTable = db.GetTableRecords(objCompHash, "av_CompanyWarehouseCode_Select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        warehouseCodeList = PopulateWarehouseCode(dataTable);
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //  throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return warehouseCodeList;
        }
        private List<CustomerWarehouseCode> PopulateWarehouseCode(DataTable dataTable)
        {
            List<CustomerWarehouseCode> warehouseCodeList = new List<CustomerWarehouseCode>();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    CustomerWarehouseCode warehouseCode = new CustomerWarehouseCode();
                    warehouseCode.WarehouseCodeGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WarehouseCodeGUID", 0, false));
                    warehouseCode.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    warehouseCode.WarehouseCode = clsGeneral.getColumnData(dataRow, "WarehouseCode", string.Empty, false) as string;
                    warehouseCode.CompanyName = clsGeneral.getColumnData(dataRow, "companyname", string.Empty, false) as string;
                    warehouseCode.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Active", false, false));
                    warehouseCode.WHCodecompanyName = clsGeneral.getColumnData(dataRow, "WHCodecompanyName", string.Empty, false) as string;

                    warehouseCodeList.Add(warehouseCode);
                }
            }
            return warehouseCodeList;
        }

    }
}
