using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;

namespace avii.Classes
{
    public class SKUOperations
    {
        public enum OperationalSystem
        {
            None = 0,
            Andriod = 1,
            Basic = 2,
            iOS = 3
        }
        public static List<CustomerSKUs> GetSKUs(string CompanyAccountNumber,out int returnValue)
        {
            returnValue = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            List<CustomerSKUs> skuList = null;
            try
            {
                objCompHash.Add("@CompanyAccountNumber", CompanyAccountNumber);

                arrSpFieldSeq = new string[] { "@CompanyAccountNumber" };
                ds = db.GetDataSet(objCompHash, "av_AssignedSKUs_Select", arrSpFieldSeq);
                skuList = PopulateCustomerSKUs(ds, out returnValue);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return skuList;
        }

        public static List<Brands> GetProducts()
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            List<Brands> BrandList = null;
            try
            {
                objCompHash.Add("@ItemGUID", 0);

                arrSpFieldSeq = new string[] { "@ItemGUID" };
                ds = db.GetDataSet(objCompHash, "av_Products_Select", arrSpFieldSeq);
                BrandList = PopulateProducts(ds);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return BrandList;
        }

        private static List<Brands> PopulateProducts(DataSet ds)
        {
           
            Brands brand = null;
            List<Brands> branList = null;
            List<ProductsCategory> categoryList = null;
            List<PublicProduct> productList = null;
            List<ProductsImage> imgList = null;
            List<ProductsAttribute> attributeList = null;
            List<ProductSpecification> specificationList = null;
            List<ProductCamera> cameraList = null;
            ProductsCategory categoryObj = null;
            PublicProduct productObj = null;
            ProductCamera cameraObj = null;
            int MakerGUID = 0, CategoryID = 0, ItemGUID = 0, cMakerGUID = 0, operatingSystem = 0;
            try
            {
                if (ds != null && ds.Tables.Count > 0)
                {
                    branList = new List<Brands>();
                    bool ShowunderCatalog = false;
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dRow in ds.Tables[0].Rows)
                        {
                            MakerGUID = Convert.ToInt32(dRow["MakerGUID"]);


                            brand = new Brands();
                            brand.BrandName = (string)clsGeneral.getColumnData(dRow, "MakerName", string.Empty, false);
                            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                            {
                                categoryList = new List<ProductsCategory>();
                                foreach (DataRow row1 in ds.Tables[1].Select("MakerGUID=" + MakerGUID))
                                {
                                    categoryObj = new ProductsCategory();
                                    categoryObj.CategoryName = (string)clsGeneral.getColumnData(row1, "CategoryName", string.Empty, false);
                                    CategoryID = Convert.ToInt32(row1["CategoryID"]);
                                    cMakerGUID = Convert.ToInt32(row1["MakerGUID"]);
                                    if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                                    {
                                        productList = new List<PublicProduct>();
                                        foreach (DataRow row2 in ds.Tables[2].Select("CategoryID=" + CategoryID.ToString() + " AND MakerGUID=" + cMakerGUID.ToString()))
                                        {
                                            productObj = new PublicProduct();
                                            ItemGUID = Convert.ToInt32(row2["ItemGUID"]);
                                            productObj.Color = (string)clsGeneral.getColumnData(row2, "Color", string.Empty, false);
                                            productObj.ProductCode = (string)clsGeneral.getColumnData(row2, "ItemCode", string.Empty, false);
                                            productObj.ModelNumber = (string)clsGeneral.getColumnData(row2, "ModelNumber", string.Empty, true);
                                            productObj.ProductName = (string)clsGeneral.getColumnData(row2, "ItemName", string.Empty, false);
                                            productObj.Description = (string)clsGeneral.getColumnData(row2, "ItemDescription", string.Empty, true);
                                            productObj.LongDescription = (string)clsGeneral.getColumnData(row2, "LongDescription", string.Empty, false);
                                            productObj.UPC = (string)clsGeneral.getColumnData(row2, "UPC", string.Empty, true);
                                            productObj.CarrierName = (string)clsGeneral.getColumnData(row2, "CarrierName", string.Empty, false);
                                            productObj.DocumentPath = (string)clsGeneral.getColumnData(row2, "DocumentPath", string.Empty, true);
                                            productObj.ProductCondition = (string)clsGeneral.getColumnData(row2, "ProductCondition", string.Empty, false);

                                            productObj.DisplayPriority = (int)clsGeneral.getColumnData(row2, "DisplayPriority", 0, true);
                                            productObj.Weight = Convert.ToDecimal(row2["Weight"]); //(float)clsGeneral.getColumnData(row2, "Weight", 0, false);
                                            productObj.SimCardTypeText = (string)clsGeneral.getColumnData(row2, "SimCardTypeText", string.Empty, true);
                                            productObj.SpintorLockTypeText = (string)clsGeneral.getColumnData(row2, "SpintorLockTypeText", string.Empty, false);
                                            productObj.ScreenSize = (string)clsGeneral.getColumnData(row2, "ScreenSize", string.Empty, false);

                                            operatingSystem = Convert.ToInt32(row2["OperationSystem"]);

                                            avii.Classes.SKUOperations.OperationalSystem op = (avii.Classes.SKUOperations.OperationalSystem)operatingSystem;
                                            productObj.OperationalSystem = op.ToString();

                                            ShowunderCatalog = (Boolean)clsGeneral.getColumnData(row2, "ShowunderCatalog", false, true);
                                            productObj.ShowunderCatalog = ShowunderCatalog;
                                            imgList = new List<ProductsImage>();
                                            ProductsImage productsImage = null;
                                            if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                                            {
                                                foreach (DataRow dRowItem in ds.Tables[3].Select("ItemGUID =" + ItemGUID.ToString()))
                                                {

                                                    productsImage = new ProductsImage();

                                                    productsImage.ImagePath = (string)clsGeneral.getColumnData(dRowItem, "ImagePath", string.Empty, false);
                                                    imgList.Add(productsImage);

                                                }
                                            }
                                            productObj.ImageList = imgList;

                                            attributeList = new List<ProductsAttribute>();
                                            ProductsAttribute productsAttribute = null;
                                            if (ds.Tables.Count > 4 && ds.Tables[4].Rows.Count > 0)
                                            {
                                                foreach (DataRow dRowItem in ds.Tables[4].Select("ItemGUID =" + ItemGUID.ToString()))
                                                {

                                                    productsAttribute = new ProductsAttribute();

                                                    productsAttribute.AttributeName = (string)clsGeneral.getColumnData(dRowItem, "AttributeName", string.Empty, false);
                                                    productsAttribute.AttributeValue = (string)clsGeneral.getColumnData(dRowItem, "AttributeValue", string.Empty, false);
                                                    attributeList.Add(productsAttribute);

                                                }
                                            }
                                            productObj.AttributeList = attributeList;

                                            specificationList = new List<ProductSpecification>();
                                            ProductSpecification productsSpecification = null;
                                            if (ds.Tables.Count > 5 && ds.Tables[5].Rows.Count > 0)
                                            {
                                                foreach (DataRow dRowItem in ds.Tables[5].Select("ItemGUID =" + ItemGUID.ToString()))
                                                {

                                                    productsSpecification = new ProductSpecification();

                                                    productsSpecification.Specification = (string)clsGeneral.getColumnData(dRowItem, "SpeficationTxt", string.Empty, false);
                                                    specificationList.Add(productsSpecification);

                                                }
                                            }
                                            productObj.SpecificationList = specificationList;


                                            cameraList = new List<ProductCamera>();
                                            if (ds.Tables.Count > 6 && ds.Tables[6].Rows.Count > 0)
                                            {
                                                foreach (DataRow dRowItem in ds.Tables[6].Select("ItemGUID =" + ItemGUID.ToString()))
                                                {

                                                    cameraObj = new ProductCamera();

                                                    cameraObj.CameraType = (string)clsGeneral.getColumnData(dRowItem, "CameraType", string.Empty, false);
                                                    cameraObj.Pixel = (string)clsGeneral.getColumnData(dRowItem, "Pixel", string.Empty, false);
                                                    cameraObj.Zoom = (string)clsGeneral.getColumnData(dRowItem, "Zoom", string.Empty, false);
                                                    cameraList.Add(cameraObj);

                                                }
                                            }
                                            productObj.CameraList = cameraList;
                                            productList.Add(productObj);
                                        }
                                    }
                                    categoryObj.ProductList = productList;
                                    categoryList.Add(categoryObj);
                                }
                            }
                            brand.CategoryList = categoryList;
                            branList.Add(brand);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulateProducts : " + ex.Message);
            }


            return branList;
        }
        
        private static List<CustomerSKUs> PopulateCustomerSKUs(DataSet ds, out int returnValue)
        {
            returnValue = 0;
            CustomerSKUs custSKU = null;
            List<CustomerSKUs> skuList = null;
            List<ProductsImage> imgList = null;
            List<ProductsAttribute> attributeList = null;
            int itemGUID = 0;
            try
            {
                if (ds != null && ds.Tables.Count > 1)
                {
                    skuList = new List<CustomerSKUs>();
                    bool ShowunderCatalog = false;
                    foreach (DataRow dRowPO in ds.Tables[0].Rows)
                    {
                        itemGUID = Convert.ToInt32(dRowPO["ItemGUID"]);
                        if (itemGUID == -1)
                        {
                            returnValue = itemGUID;
                            return skuList;
                        }
                        custSKU = new CustomerSKUs();
                        custSKU.SKU = (string)clsGeneral.getColumnData(dRowPO, "SKU", string.Empty, true);
                        custSKU.ProductCode = (string)clsGeneral.getColumnData(dRowPO, "ItemCode", string.Empty, false);
                        custSKU.ModelNumber = (string)clsGeneral.getColumnData(dRowPO, "ModelNumber", string.Empty, true);
                        custSKU.ProductName = (string)clsGeneral.getColumnData(dRowPO, "ItemName", string.Empty, false);
                        custSKU.Description = (string)clsGeneral.getColumnData(dRowPO, "ItemDescription", string.Empty, true);
                        custSKU.LongDescription = (string)clsGeneral.getColumnData(dRowPO, "LongDescription", string.Empty, false);
                        custSKU.UPC = (string)clsGeneral.getColumnData(dRowPO, "UPC", string.Empty, true);
                        custSKU.Brand = (string)clsGeneral.getColumnData(dRowPO, "MakerName", string.Empty, false);
                        custSKU.CategoryName = (string)clsGeneral.getColumnData(dRowPO, "CategoryName", string.Empty, true);
                        custSKU.CarrierName = (string)clsGeneral.getColumnData(dRowPO, "CarrierName", string.Empty, false);
                        custSKU.DocumentPath = (string)clsGeneral.getColumnData(dRowPO, "DocumentPath", string.Empty, true);
                        custSKU.ProductCondition = (string)clsGeneral.getColumnData(dRowPO, "ProductCondition", string.Empty, false);
                        ShowunderCatalog = (Boolean)clsGeneral.getColumnData(dRowPO, "ShowunderCatalog", false, true);
                        custSKU.ShowunderCatalog = ShowunderCatalog; //(string)clsGeneral.getColumnData(dRowPO, "ShowunderCatalog", string.Empty, true);
                        //custSKU.ProductCode = (string)clsGeneral.getColumnData(dRowPO, "ItemCode", string.Empty, false);
                        imgList = new List<ProductsImage>();
                        ProductsImage productsImage = null;
                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dRowItem in ds.Tables[1].Select("ItemGUID =" + dRowPO["ItemGUID"].ToString()))
                            {

                                productsImage = new ProductsImage();

                                productsImage.ImagePath = (string)clsGeneral.getColumnData(dRowItem, "ImagePath", string.Empty, false);
                                imgList.Add(productsImage);

                            }
                        }
                        custSKU.ImageList = imgList;

                        attributeList = new List<ProductsAttribute>();
                        ProductsAttribute productsAttribute = null;
                        if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                        {
                            foreach (DataRow dRowItem in ds.Tables[2].Select("ItemGUID =" + dRowPO["ItemGUID"].ToString()))
                            {

                                productsAttribute = new ProductsAttribute();

                                productsAttribute.AttributeName = (string)clsGeneral.getColumnData(dRowItem, "AttributeName", string.Empty, false);
                                productsAttribute.AttributeValue = (string)clsGeneral.getColumnData(dRowItem, "AttributeValue", string.Empty, false);
                                attributeList.Add(productsAttribute);

                            }
                        }
                        custSKU.AttributeList = attributeList;
                        skuList.Add(custSKU);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulatePurchaseOrderItems : " + ex.Message);
            }


            return skuList;
        }

    }
}