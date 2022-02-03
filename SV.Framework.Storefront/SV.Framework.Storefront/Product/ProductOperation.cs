using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SV.Framework.Storefront.Product
{
    public class ProductOperation
    {
        public static async Task<GeteBayProductResponse> GeteBayInventory(string url, GetProductRequest request)
        {
            GeteBayProductResponse response = new GeteBayProductResponse();

            response = await ApiHelper.PostAsync<GeteBayProductResponse, GetProductRequest>(request, url);

            return response;
        }
        public static async Task<AddProductResponse> AddeBayInventory(string url, StorefrontProduct request)
        {
            AddProductResponse response = new AddProductResponse();

            response = await ApiHelper.PostAsync<AddProductResponse, StorefrontProduct>(request, url);
            
            return response;
        }
        public static List<ProductRequestModel> GetInventoty(string productSource, int CompanyID, string SKU, string Title, string Condition, string UPC, string status)
        {
            List<ProductRequestModel> products = new List<ProductRequestModel>();
            DBConnect db = new DBConnect();            
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            //try
            //{
                objCompHash.Add("@ProductSource", productSource);
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@SKU", SKU);
                objCompHash.Add("@Title", Title);
                objCompHash.Add("@Condition", Condition);
                objCompHash.Add("@UPC", UPC);
                objCompHash.Add("@Status", status);

                arrSpFieldSeq = new string[] { "@ProductSource", "@CompanyID", "@SKU", "@Title", "@Condition", "@UPC", "@Status" };
                dt = db.GetTableRecords(objCompHash, "svStoreFront_Select", arrSpFieldSeq);
                products = PopulateInventory(dt);
            //}
            //catch (Exception ex)
            //{
            //}
            //finally
            //{
                db = null;

            //}
            return products;
        }
        private static List<ProductRequestModel> PopulateInventory(DataTable dt)
        {
            List<ProductRequestModel> products = new List<ProductRequestModel>();
            ProductRequestModel productModel = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    productModel = new ProductRequestModel();
                    productModel.Condition = Convert.ToString(clsGeneral.getColumnData(row, "Condition", string.Empty, false));
                    productModel.CategoryName = Convert.ToString(clsGeneral.getColumnData(row, "CategoryName", string.Empty, false));
                    productModel.Active = Convert.ToBoolean(clsGeneral.getColumnData(row, "Active", false, false));
                    //productModel.Condition = Convert.ToString(clsGeneral.getColumnData(row, "Condition", string.Empty, false));
                    productModel.CountryOrRegion = Convert.ToString(clsGeneral.getColumnData(row, "CountryOrRegion", string.Empty, false));
                    productModel.ItemDescription = Convert.ToString(clsGeneral.getColumnData(row, "ItemDescription", string.Empty, false));
                    productModel.ItemName = Convert.ToString(clsGeneral.getColumnData(row, "ItemName", string.Empty, false));
                    productModel.Locale = Convert.ToString(clsGeneral.getColumnData(row, "Locale", string.Empty, false));
                    productModel.SerialNumber = Convert.ToString(clsGeneral.getColumnData(row, "SerialNumber", string.Empty, false));
                    productModel.ConditionDesc = Convert.ToString(clsGeneral.getColumnData(row, "ConditionDesc", string.Empty, false));
                    productModel.WeightDimension = Convert.ToString(clsGeneral.getColumnData(row, "WeightDimension", string.Empty, false));
                    productModel.Location = Convert.ToString(clsGeneral.getColumnData(row, "Location", string.Empty, false));
                    productModel.SKU = Convert.ToString(clsGeneral.getColumnData(row, "SKU", string.Empty, false));
                    productModel.UPC = Convert.ToString(clsGeneral.getColumnData(row, "UPC", string.Empty, false));
                    productModel.WarehouseCode = Convert.ToString(clsGeneral.getColumnData(row, "WarehouseCode", string.Empty, false));
                    productModel.MaximumStock = Convert.ToInt32(clsGeneral.getColumnData(row, "MaximumStock", 0, false));
                    productModel.MinimumStock = Convert.ToInt32(clsGeneral.getColumnData(row, "MinimumStock", 0, false));
                    productModel.OpeningStock = Convert.ToInt32(clsGeneral.getColumnData(row, "OpeningStock", 0, false));
                    productModel.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(row, "ItemGUID", 0, false));
                    productModel.Weight = Convert.ToDecimal(clsGeneral.getColumnData(row, "Weight", 0, false));
                    productModel.IsSync = Convert.ToBoolean(clsGeneral.getColumnData(row, "IsSync", false, false));

                    productModel.MakerName = Convert.ToString(clsGeneral.getColumnData(row, "MakerName", string.Empty, false));
                    productModel.ModifiedDate = Convert.ToString(clsGeneral.getColumnData(row, "ModifiedDate", string.Empty, false));
                    productModel.CreatedDate = Convert.ToString(clsGeneral.getColumnData(row, "CreatedDate", string.Empty, false));
                    productModel.UserName = Convert.ToString(clsGeneral.getColumnData(row, "UserName", string.Empty, false));
                    products.Add(productModel);



                }
            }
            return products;
        }
        public static int InventorySyncUpdate(AddProductResponse requestData, int UserID, int CompanID, out string errorMessage)
        {
            int returnValue = 0;

            errorMessage = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable syncdt = new DataTable();
            
            Hashtable objCompHash = new Hashtable();
            syncdt = SKUTable(requestData);
            try
            {
                //objCompHash.Add("@LogID", logID);
                objCompHash.Add("@UserID", UserID);
                objCompHash.Add("@CompanyID", CompanID);
                objCompHash.Add("@svSKU", syncdt);                

                arrSpFieldSeq = new string[] { "@UserID", "@CompanyID", "@svSKU" };
                object obj = db.ExecuteScalar(objCompHash, "svItem_Extension_Sync", arrSpFieldSeq);
                if (obj != null)
                {
                    returnValue = Convert.ToInt32(obj);
                    if (returnValue > 0)
                        errorMessage = returnValue + " record(s) synced successfully";
                }
            }
            catch (Exception objExp)
            {
                errorMessage = objExp.Message;
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return returnValue;

        }
        public static int InventorySyncFromStorefrontUpdate(DataTable syncdt, int UserID, int CompanyID, out string errorMessage)
        {
            int returnValue = 0;
            errorMessage = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
          //  DataTable syncdt = new DataTable();
            Hashtable objCompHash = new Hashtable();
           // syncdt = SyncFromStorefrontTable(productList);

            try
            {
                //objCompHash.Add("@LogID", logID);
                objCompHash.Add("@UserID", UserID);
                objCompHash.Add("@CompanyID", CompanyID);                
                objCompHash.Add("@svItemSyncType", syncdt);

                arrSpFieldSeq = new string[] { "@UserID", "@CompanyID", "@svItemSyncType" };
                object obj = db.ExecuteScalar(objCompHash, "svItem_Extension_SyncFromStorefront", arrSpFieldSeq);
                if (obj != null)
                {
                    returnValue = Convert.ToInt32(obj);
                    if (returnValue > 0)
                        errorMessage = returnValue + " record(s) synced successfully";
                }
            }
            catch (Exception objExp)
            {
                errorMessage = objExp.Message;
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return returnValue;

        }

        private static DataTable SyncFromStorefrontTable(List<ProductRequestModel> productList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SKU", typeof(System.String));
            dt.Columns.Add("UPC", typeof(System.String));
            dt.Columns.Add("ItemName", typeof(System.String));
            dt.Columns.Add("ItemDescription", typeof(System.String));
            dt.Columns.Add("OpeningStock", typeof(System.Int32));
            dt.Columns.Add("CountryOrRegion", typeof(System.String));
            dt.Columns.Add("Condition", typeof(System.String));
            dt.Columns.Add("ConditionDesc", typeof(System.String));
            dt.Columns.Add("Locale", typeof(System.String));        

            DataRow row;

            if (productList != null && productList.Count > 0)
            {
                //add returncode validation
                foreach (ProductRequestModel item in productList)
                {
                    row = dt.NewRow();
                    row["SKU"] = item.SKU;
                    row["UPC"] = item.UPC;
                    row["ItemName"] = item.ItemName;
                    row["ItemDescription"] = item.ItemDescription;
                    row["OpeningStock"] = item.OpeningStock;
                    row["CountryOrRegion"] = item.CountryOrRegion;
                    row["Condition"] = item.Condition;
                    row["ConditionDesc"] = item.ConditionDesc;
                    row["Locale"] = item.Locale;
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        private static DataTable SKUTable(AddProductResponse response)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SKU", typeof(System.String));
            
            DataRow row;

            if (response != null && response.Responses != null && response.Responses.Count > 0)
            {
                //add returncode validation
                foreach (ProductResponse item in response.Responses)
                {
                    row = dt.NewRow();
                    row["SKU"] = item.SKU;
                    dt.Rows.Add(row);                    
                }
            }
            return dt;
        }
        public static string ValidateProduct(ProductRequestModel productRequest)
        {
            string returnMessage = "";


            return returnMessage;
        }
        public static string InventoryInsertUpdate(ProductRequestModel productRequest)
        {
            string returnMessage = "Could not save product";
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            int returnValue = 0;
            
            try
            {
                objCompHash.Add("@ItemGUID", productRequest.ItemGUID);
                objCompHash.Add("@ProductSource", productRequest.ProductSource);
                objCompHash.Add("@CompanyID", productRequest.CompanyID);
                objCompHash.Add("@CategoryID", productRequest.CategoryID);
                objCompHash.Add("@MakerGUID", productRequest.MakerGUID);
                objCompHash.Add("@SKU", productRequest.SKU);
                objCompHash.Add("@UPC", productRequest.UPC);
                objCompHash.Add("@ModelNumber", productRequest.ModelNumber);
                objCompHash.Add("@ItemName", productRequest.ItemName);
                objCompHash.Add("@ItemDescription", productRequest.ItemDescription);
                objCompHash.Add("@Weight", productRequest.Weight);
                objCompHash.Add("@OpeningStock", productRequest.OpeningStock);
                objCompHash.Add("@CountryOrRegion", productRequest.CountryOrRegion);
                objCompHash.Add("@Locale", productRequest.Locale);
                objCompHash.Add("@Condition", productRequest.Condition);
                objCompHash.Add("@SerialNumber", productRequest.SerialNumber);
                objCompHash.Add("@ConditionDesc", productRequest.ConditionDesc);
                objCompHash.Add("@WeightDimension", productRequest.WeightDimension);
                objCompHash.Add("@Location", productRequest.Location);
                objCompHash.Add("@WarehouseCode", productRequest.WarehouseCode);
                objCompHash.Add("@MinimumStock", productRequest.MinimumStock);
                objCompHash.Add("@MaximumStock", productRequest.MaximumStock);
                objCompHash.Add("@Active", productRequest.Active);
                objCompHash.Add("@UserID", productRequest.UserID);               
                objCompHash.Add("@ProductTypeID", 0);               
                objCompHash.Add("@ConditionID", 0);               

                arrSpFieldSeq = new string[] { "@ItemGUID", "@ProductSource","@CompanyID", "@CategoryID", "@MakerGUID","@SKU", "@UPC",
                    "@ModelNumber", "@ItemName", "@ItemDescription", "@Weight", "@OpeningStock", "@CountryOrRegion", "@Locale",
                    "@Condition", "@SerialNumber", "@ConditionDesc", "@WeightDimension", "@Location", "@WarehouseCode", "@MinimumStock",
                    "@MaximumStock", "@Active", "@UserID", "@ProductTypeID", "@ConditionID"};
                returnValue = db.ExecCommand(objCompHash, "av_Item_InsertUpdate", arrSpFieldSeq);
                if (returnValue < 0)
                {
                    returnMessage = "Submitted successfully";
                }
                else
                {
                    if (returnValue == 1)
                        returnMessage = "ModelNumber already exist";
                    else
                           if (returnValue == 2)
                        returnMessage = "SKU already exist";
                    else
                               if (returnValue == 3)
                        returnMessage = "ModelNumber and SKU already exist";
                }
            }
            catch (Exception objExp)
            {
                returnMessage = objExp.Message;
                //logRequest.Comment = objExp.Message;
                //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                //throw objExp;
            }
            finally
            {
               // SV.Framework.Admin.ItemLogOperation.ItemLogInsert(logRequest);
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return returnMessage;
        }


        public enum StorefrontEnum
        { 
            eBay //,
            //Shopify
        }
        public enum ProductStatusEnum
        {
            Enable,
            Disable
        }
        public enum ConditionEnum
        {
            NEW, 
            LIKE_NEW, 
            NEW_OTHER, 
            NEW_WITH_DEFECTS, 
            MANUFACTURER_REFURBISHED, 
            CERTIFIED_REFURBISHED, 
            SELLER_REFURBISHED, 
            USED_EXCELLENT, 
            USED_VERY_GOOD, 
            USED_GOOD, 
            USED_ACCEPTABLE, 
            FOR_PARTS_OR_NOT_WORKING
        }
        public enum LocaleEnum
        {
            en_US, 
            en_CA, 
            fr_CA, 
            en_GB, 
            en_AU, 
            en_IN, 
            de_AT, 
            fr_BE, 
            fr_FR, 
            de_DE, 
            it_IT, 
            nl_BE, 
            nl_NL, 
            es_ES, 
            de_CH, 
            fi_FI, 
            zh_HK, 
            hu_HU, 
            en_PH, 
            pl_PL, 
            pt_PT, 
            ru_RU, 
            en_SG, 
            en_IE, 
            en_MY
        }
    }
}
