using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Catalog
{
    public class FinishSKUOperations : BaseCreateInstance
    {
        public List<CompanySKUno> GetCompanyFinalOrRawSKUList(int companyID, bool IsFinalSKU)
        {
            List<CompanySKUno> skuList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@IsFinalSKU", IsFinalSKU);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@IsFinalSKU" };
                    dt = db.GetTableRecords(objCompHash, "av_Company_SKUs_Select_New", arrSpFieldSeq);
                    skuList = PopulateSKU(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return skuList;
        }

        public List<RawSKU> GetCompanyAssignedRawSKUList(int companyID, int itemCompanyGUID, bool IsCustomer, bool IsESNRequired)
        {
            List<RawSKU> skuList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@ItemCompanyGUID", itemCompanyGUID);
                    objCompHash.Add("@IsCustomer", IsCustomer);
                    objCompHash.Add("@IsESNRequired", IsESNRequired);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@ItemCompanyGUID", "@IsCustomer", "@IsESNRequired" };
                    dt = db.GetTableRecords(objCompHash, "Av_FinishedSKU_AssignedRawSKU_Select", arrSpFieldSeq);
                    skuList = PopulateRawSKU(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return skuList;
        }
        public  List<RawSKU> GetKittedAssignedRawSKUs(int companyID, int itemCompanyGUID)
        {
            List<RawSKU> skuList = default;
            using (DBConnect db = new DBConnect())
            {

                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                
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
                    Logger.LogMessage(objExp, this);//throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return skuList;
        }
        public  bool FinishedSKUInsertUpdate(DataTable skuDt, int itemCompanyGUID, int userId, Kittedsku kittedsku, string ActionName)
        {
            SV.Framework.DAL.Catalog.ItemLogOperation ItemLogOperation = SV.Framework.DAL.Catalog.ItemLogOperation.CreateInstance<SV.Framework.DAL.Catalog.ItemLogOperation>();

            bool returnValue = false;
            using (DBConnect db = new DBConnect())
            {

                string rawSKUs = clsGeneral.SerializeObject(kittedsku);
                ItemLogModel logRequest = new ItemLogModel();
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
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());

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

        private List<RawSKU> PopulateRawSKU(DataTable dataTable)
        {
            List<RawSKU> skuList = default;// new List<RawSKU>();

            RawSKU rawSKU = default;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                skuList = new List<RawSKU>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    rawSKU = new RawSKU();
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
        private List<RawSKU> PopulateKittedRawSKUs(DataTable dataTable)
        {
            List<RawSKU> skuList = default;// new List<RawSKU>();
            RawSKU rawSKU = default;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                skuList = new List<RawSKU>();
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
        private List<CompanySKUno> PopulateSKU(DataTable dataTable)
        {
            List<CompanySKUno> skuList = default;// new List<CompanySKUno>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                skuList = new List<CompanySKUno>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    CompanySKUno objInventoryItem = new CompanySKUno();
                    objInventoryItem.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    //objInventoryItem.MASSKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    objInventoryItem.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    objInventoryItem.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_Hand", 0, false));
                    objInventoryItem.IsKittedBox = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsKittedBox", 0, false));
                    objInventoryItem.IsESNRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESNRequired", 0, false));

                    skuList.Add(objInventoryItem);
                }
            }
            return skuList;

        }


    }
}
