using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;

namespace avii.Classes
{
    public class FinishSKUOperations
    {
        public static List<CompanySKUno> GetCompanyFinalOrRawSKUList(int companyID, bool IsFinalSKU)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<CompanySKUno> skuList = null;
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@IsFinalSKU", IsFinalSKU);

                arrSpFieldSeq = new string[] { "@CompanyID", "@IsFinalSKU" };
                dt = db.GetTableRecords(objCompHash, "av_Company_SKUs_Select_New", arrSpFieldSeq);
                skuList = MslOperation.PopulateSKU(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return skuList;
        }

        public static List<RawSKU> GetCompanyAssignedRawSKUList(int companyID, int itemCompanyGUID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<RawSKU> skuList = null;
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@ItemCompanyGUID", itemCompanyGUID);

                arrSpFieldSeq = new string[] { "@CompanyID", "@ItemCompanyGUID" };
                dt = db.GetTableRecords(objCompHash, "Av_FinishedSKU_AssignedRawSKU_Select", arrSpFieldSeq);
                skuList = PopulateRawSKU(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return skuList;
        }
        public static List<RawSKU> GetKittedAssignedRawSKUs(int companyID, int itemCompanyGUID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<RawSKU> skuList = null;
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@ItemCompanyGUID", itemCompanyGUID);

                arrSpFieldSeq = new string[] { "@CompanyID", "@ItemCompanyGUID" };
                dt = db.GetTableRecords(objCompHash, "Av_KittedSKU_AssignedKiitedSKU_Select", arrSpFieldSeq);
                skuList = PopulateKittedRawSKUs(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return skuList;
        }
        public static bool FinishedSKUInsertUpdate(DataTable skuDt, int itemCompanyGUID, int userId, SV.Framework.Admin.Kittedsku kittedsku, string ActionName)
        {
            //SV.Framework.Admin.Kittedsku kittedsku = new SV.Framework.Admin.Kittedsku();
            //kittedsku.ItemCompanyGUID = itemCompanyGUID;
            //List<SV.Framework.Admin.Kitrawsku> rawskulist = new List<SV.Framework.Admin.Kitrawsku>();
            //SV.Framework.Admin.Kitrawsku rawsku = new SV.Framework.Admin.Kitrawsku();
            string rawSKUs = clsGeneral.SerializeObject(kittedsku);
            SV.Framework.Admin.ItemLogModel logRequest = new SV.Framework.Admin.ItemLogModel();
            logRequest.ItemCompanyGUID = itemCompanyGUID;
            logRequest.RequestData = rawSKUs;
            logRequest.CreateUserID = userId;
            logRequest.ItemGUID = 0;
            logRequest.ActionName = ActionName;
            //if (active == 1)
            logRequest.Status = "Active";
           // else
             //   logRequest.Status = "Inactive";

            logRequest.SKU = "";


            bool returnValue = false;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            //int retIntVal = 0;
            try
            {
                objCompHash.Add("@FinishedSKUID", itemCompanyGUID);
                objCompHash.Add("@UserID", userId);
                objCompHash.Add("@av_FinishedRawSKUs", skuDt);
                
                arrSpFieldSeq = new string[] { "@FinishedSKUID", "@UserID", "@av_FinishedRawSKUs" };
                db.ExeCommand(objCompHash, "av_FinishedRawSKUsInsertUpdate", arrSpFieldSeq);
                returnValue = true;

                logRequest.ResponseData = "Assigned successfully";
                //if (errorMessage == string.Empty)
                //    returnValue = true;
                //else
                //    returnValue = false;
            }
            catch (Exception objExp)
            {
                returnValue = false;
                logRequest.Comment = objExp.Message;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                SV.Framework.Admin.ItemLogOperation.ItemLogInsert(logRequest);
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return returnValue;
        }

        public static List<RawSKU> PopulateRawSKU(DataTable dataTable)
        {
            List<RawSKU> skuList = new List<RawSKU>();
            RawSKU rawSKU = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    rawSKU  = new RawSKU();
                    rawSKU.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    rawSKU.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                    rawSKU.IsESNRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESNRequired", false, false));
                    rawSKU.IsDisplayName = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsDisplayName", false, false));
                    rawSKU.DisplayName = clsGeneral.getColumnData(dataRow, "DisplayName", string.Empty, false) as string;

                    rawSKU.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    rawSKU.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    rawSKU.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    rawSKU.MappedCategoryName = clsGeneral.getColumnData(dataRow, "MappedCategoryName", string.Empty, false) as string;
                    rawSKU.MappedSKU = clsGeneral.getColumnData(dataRow, "MappedSKU", string.Empty, false) as string;
                    rawSKU.MappedProductName = clsGeneral.getColumnData(dataRow, "MappedItemName", string.Empty, false) as string;

                    skuList.Add(rawSKU);
                }
            }
            return skuList;

        }
        public static List<RawSKU> PopulateKittedRawSKUs(DataTable dataTable)
        {
            List<RawSKU> skuList = new List<RawSKU>();
            RawSKU rawSKU = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    rawSKU = new RawSKU();
                    rawSKU.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    rawSKU.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                    rawSKU.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    rawSKU.MappedItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MappedItemCompanyGUID", 0, false));
                    rawSKU.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    //rawSKU.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    //rawSKU.MappedCategoryName = clsGeneral.getColumnData(dataRow, "MappedCategoryName", string.Empty, false) as string;
                    //rawSKU.MappedSKU = clsGeneral.getColumnData(dataRow, "MappedSKU", string.Empty, false) as string;
                    //rawSKU.MappedProductName = clsGeneral.getColumnData(dataRow, "MappedItemName", string.Empty, false) as string;

                    skuList.Add(rawSKU);
                }
            }
            return skuList;

        }

    }

    public class RawSKU
    {
        public bool IsDisplayName { get; set; }
        public bool IsESNRequired { get; set; }
        public string DisplayName { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public int Quantity { get; set; }
        public int ItemcompanyGUID { get; set; }
        public int MappedItemcompanyGUID { get; set; }

        public string MappedSKU { get; set; }
        public string MappedProductName { get; set; }
        public string MappedCategoryName { get; set; }
        public string StockMsg { get; set; }
    }
}