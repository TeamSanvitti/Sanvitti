using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Catalog
{

    public class CategoryOperation:BaseCreateInstance
    {
        public int CategoryDelete(int categoryGUID)
        {
            int returnValue = 0;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CategoryGUID", categoryGUID);

                    arrSpFieldSeq = new string[] { "@CategoryGUID" };
                    returnValue = db.ExecuteNonQuery(objCompHash, "av_ItemCategoryDelete", arrSpFieldSeq);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);//
                    //errorMessage = "Technical error!";
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                   // db = null;
                }
            }
            return returnValue;

        }

        public  int CategoryInsertUpdate(CategoryModel request, out string errorMessage)
        {
            int returnValue = 0;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CategoryGUID", request.CategoryGUID);
                    objCompHash.Add("@CategoryName", request.CategoryName);
                    objCompHash.Add("@CategoryDesciption", request.CategoryDesc);
                    objCompHash.Add("@ParentCategoryGUID", request.ParentCategoryGUID);
                    objCompHash.Add("@CateogoryImage", request.CategoryImage);
                    objCompHash.Add("@CategoryStatus", request.CategoryStatus);
                    objCompHash.Add("@IsESNRequired", request.ESNRequired);
                    objCompHash.Add("@IsKittedBox", request.KittedBox);
                    objCompHash.Add("@IsSIMRequired", request.SIMRequired);
                    objCompHash.Add("@IsSIM", request.IsSIM);
                    objCompHash.Add("@IsRMAAllowed", request.RMAAllowed);
                    objCompHash.Add("@IsMappingAllowed", request.SKUMapping);
                    objCompHash.Add("@IsReStockingAllowed", request.ReStocking);
                    objCompHash.Add("@IsForecastingAllowed", request.Forecasting);

                    arrSpFieldSeq = new string[] { "@CategoryGUID", "@CategoryName", "@CategoryDesciption", "@ParentCategoryGUID",
                "@CateogoryImage", "@CategoryStatus", "@IsESNRequired", "@IsKittedBox", "@IsSIMRequired", "@IsSIM", "@IsRMAAllowed",
                "@IsMappingAllowed", "@IsReStockingAllowed", "@IsForecastingAllowed"};
                    returnValue = db.ExCommand(objCompHash, "av_ItemCategoryInsertUpdate", arrSpFieldSeq, "@ErrorMessage", out errorMessage);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);//
                    //errorMessage = "Technical error!";
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return returnValue;

        }
        public  List<CategoryModel> GetCategoryList(CategoryModel model)
        {
            List<CategoryModel> categoryList = default;// new List<CategoryModel>();
            using (DBConnect db = new DBConnect())
            {
                CategoryModel categoryModel = default;
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                Hashtable objCompHash = new Hashtable();
                string category = string.Empty;
                try
                {
                    objCompHash.Add("@CategoryName", model.CategoryName);
                    objCompHash.Add("@ParentCategoryGUID", model.ParentCategoryGUID);
                    objCompHash.Add("@CategoryStatus", model.CategoryStatusID);
                    objCompHash.Add("@IsBaseOnly", model.BaseCategories);
                    objCompHash.Add("@IsLeafOnly", model.LeafCategories);

                    arrSpFieldSeq = new string[] { "@CategoryName", "@ParentCategoryGUID", "@CategoryStatus", "@IsBaseOnly", "@IsLeafOnly" };
                    ds = db.GetDataSet(objCompHash, "av_ItemCategorySelect", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        categoryList = new List<CategoryModel>();
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            categoryModel = new CategoryModel();
                            categoryModel.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                            categoryModel.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                            category = clsGeneral.getColumnData(dataRow, "P_CategoryName", string.Empty, false) as string;
                            string[] arr = category.Split('>');
                            if (arr.Length > 1)
                                categoryModel.P_CategoryName = arr[0] + "><strong>" + arr[1] + "</strong>";
                            else
                                categoryModel.P_CategoryName = "<strong>" + category + "</strong>";
                            categoryModel.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                            categoryModel.CategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryGUID", 0, false));
                            categoryModel.ParentCategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ParentCategoryGUID", 0, false));
                            categoryModel.IsDelete = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "IsDelete", 0, false));
                            categoryModel.IsEdit = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "IsEdit", 0, false));
                            categoryModel.IsSIM = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsSIM", false, false));
                            categoryModel.KittedBox = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsKittedBox", false, false));
                            categoryModel.ReStocking = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsReStockingAllowed", false, false));
                            categoryModel.RMAAllowed = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsRMAAllowed", false, false));
                            categoryModel.SIMRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsSIMRequired", false, false));
                            categoryModel.SKUMapping = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsMappingAllowed", false, false));
                            categoryModel.ESNRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESNRequired", false, false));
                            categoryModel.Forecasting = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsForecastingAllowed", false, false));
                            categoryModel.CategoryDesc = clsGeneral.getColumnData(dataRow, "CategoryDesciption", string.Empty, false) as string;

                            categoryList.Add(categoryModel);
                        }

                    }

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);////throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return categoryList;
        }

        public  CategoryModel GetCategoryInfo(int categoryGUID)
        {
            CategoryModel categoryModel = default;// new CategoryModel();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CategoryGUID", categoryGUID);

                    arrSpFieldSeq = new string[] { "@CategoryGUID" };
                    dt = db.GetTableRecords(objCompHash, "av_ItemCategorySelect", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            categoryModel = new CategoryModel();
                            categoryModel.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                            categoryModel.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                            categoryModel.CategoryDesc = clsGeneral.getColumnData(dataRow, "CategoryDesciption", string.Empty, false) as string;
                            categoryModel.CategoryImage = clsGeneral.getColumnData(dataRow, "CateogoryImage", string.Empty, false) as string;
                            categoryModel.P_CategoryName = clsGeneral.getColumnData(dataRow, "P_CategoryName", string.Empty, false) as string;
                            categoryModel.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                            categoryModel.CategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryGUID", 0, false));
                            categoryModel.ParentCategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ParentCategoryGUID", 0, false));
                            //categoryModel.IsDelete = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "IsDelete", 0, false));
                            categoryModel.IsEdit = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "IsEdit", 0, false));

                            categoryModel.IsSIM = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsSIM", false, false));
                            categoryModel.KittedBox = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsKittedBox", false, false));
                            categoryModel.ReStocking = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsReStockingAllowed", false, false));
                            categoryModel.RMAAllowed = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsRMAAllowed", false, false));
                            categoryModel.SIMRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsSIMRequired", false, false));
                            categoryModel.SKUMapping = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsMappingAllowed", false, false));
                            categoryModel.ESNRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESNRequired", false, false));
                            categoryModel.Forecasting = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsForecastingAllowed", false, false));

                        }

                    }

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return categoryModel;
        }
        
    }
}
