using System;
using System.Collections.Generic;
using System.Text;

using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;
using System.Collections;
using System.Data;

namespace SV.Framework.DAL.Inventory
{
    public class DiscardSKUOperation : BaseCreateInstance
    {
        public int DiscartedSKUInsert(DiscartedSKU request)
        {
            int returnValue = 0;
            //errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                //string RequestData = clsGeneral.SerializeObject(request);
                try
                {
                    objCompHash.Add("@DiscardedSKUID", request.DiscardedSKUID);
                    objCompHash.Add("@ItemCompanyGUID", request.ItemCompanyGUID);
                    objCompHash.Add("@ModuleName", request.ModuleName);
                    objCompHash.Add("@Qty", request.Qty);
                    objCompHash.Add("@CreatedBy", request.UserID);
                    objCompHash.Add("@RequestedBy", request.RequestedBy);
                    objCompHash.Add("@Comments", request.Comments);
                    objCompHash.Add("@ApproveLan", request.ApproveLan);

                    arrSpFieldSeq = new string[] { "@DiscardedSKUID", "@ItemCompanyGUID", "@ModuleName", "@Qty", "@CreatedBy", "@RequestedBy", "@Comments", "@ApproveLan" };
                    object obj = db.ExecuteNonQuery(objCompHash, "av_DiscardedSKUsInsert", arrSpFieldSeq);
                    if (obj != null)
                        returnValue = Convert.ToInt32(obj);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    // ServiceOrderRequestLogInsert(request.ServiceRequestID, RequestData, request.UserID, objExp.Message);
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

        public List<DiscartedSKU> DiscartedSKUSearch(int CompanyID, string FromDate, string ToDate, string SKU)
        {
            List<DiscartedSKU> DiscartedSKUList = default;// new List<BlockESN>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CompanyID", CompanyID);
                    objCompHash.Add("@FromDate", string.IsNullOrEmpty(FromDate) ? null : FromDate);
                    objCompHash.Add("@ToDate", string.IsNullOrEmpty(ToDate) ? null : ToDate);
                    objCompHash.Add("@SKU", SKU);
                    
                    arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@SKU" };
                    dt = db.GetTableRecords(objCompHash, "av_DiscardedSKUsSelect", arrSpFieldSeq);

                    DiscartedSKUList = PopulateSKUs(dt);

                }
                catch (Exception objExp)
                {
                    //serviceOrders = null;
                    Logger.LogMessage(objExp, this); throw new Exception(objExp.Message.ToString());

                }
                finally
                {
                    //  db = null;
                }
            }
            return DiscartedSKUList;

        }
        public SkuInfo SKUInfoBySKUId(int ItemCompanyGUID)
        {
            SkuInfo skuInfo = default;// new List<BlockESN>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@ItemCompanyGUID", ItemCompanyGUID);
                    
                    arrSpFieldSeq = new string[] { "@ItemCompanyGUID" };
                    dt = db.GetTableRecords(objCompHash, "AV_GetDefaultSKUInfo_Select", arrSpFieldSeq);

                    skuInfo = PopulateSkuInfo(dt);

                }
                catch (Exception objExp)
                {
                    //serviceOrders = null;
                    Logger.LogMessage(objExp, this); throw new Exception(objExp.Message.ToString());

                }
                finally
                {
                    //  db = null;
                }
            }
            return skuInfo;

        }
        private SkuInfo PopulateSkuInfo(DataTable dt)
        {
            SkuInfo skuInfo = default;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    skuInfo = new SkuInfo();
                    skuInfo.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    skuInfo.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_Hand", 0, false));
                    //discartedSKU.ModuleName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    skuInfo.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    skuInfo.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    skuInfo.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                   
                }
            }
            return skuInfo;
        }
        private List<DiscartedSKU> PopulateSKUs(DataTable dt)
        {
            DiscartedSKU discartedSKU = default;
            List<DiscartedSKU> skuList = new List<DiscartedSKU>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    discartedSKU = new DiscartedSKU();
                    discartedSKU.DiscardedSKUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DiscardedSKUID", 0, false));
                    discartedSKU.Qty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                    //discartedSKU.ModuleName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    discartedSKU.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                    discartedSKU.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    discartedSKU.ModuleName = clsGeneral.getColumnData(dataRow, "ModuleName", string.Empty, false) as string;
                    discartedSKU.DiscardDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", DateTime.Now, false));
                    discartedSKU.DiscardedBy = clsGeneral.getColumnData(dataRow, "DiscardedBy", string.Empty, false) as string;
                    discartedSKU.RequestedByName = clsGeneral.getColumnData(dataRow, "RequestedBy", string.Empty, false) as string;
                    discartedSKU.ApprovedBy = clsGeneral.getColumnData(dataRow, "ApprovedBy", string.Empty, false) as string;
                    skuList.Add(discartedSKU);
                }
            }
            return skuList;
        }

    }

    
}
