using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Common;
using SV.Framework.Models.Inventory;

namespace SV.Framework.DAL.Inventory
{
    public class KittingOperations : BaseCreateInstance
    {

        public  string ESNKittingInsert(string ESN, DateTime currentCSTDateTime, int userID)
        {
            string errorMessage = default;
            int recordCount = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@ESN", ESN);
                    objCompHash.Add("@UserID", userID);
                    objCompHash.Add("@KittedCSTDate", currentCSTDateTime);

                    arrSpFieldSeq = new string[] { "@ESN", "@UserID", "@KittedCSTDate" };
                    recordCount = db.ExecuteNonQuery(objCompHash, "svESNKitted_Insert", arrSpFieldSeq);
                    if (recordCount > 0)
                    {
                        errorMessage = ""; // "Submitted successfully";// ResponseErrorCode.UpdatedSuccessfully.ToString();
                    }
                    else
                    {
                        errorMessage = clsGeneral.ResponseErrorCode.DataNotUpdated.ToString();
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    errorMessage = objExp.Message.ToString();
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return errorMessage;
        }

        public string PurchaseOrderKittingUpdate(string poAssignIDs, DateTime currentCSTDateTime, int userID)
        {
            string errorMessage = default;
            int recordCount = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POAssignIDs", poAssignIDs);
                    objCompHash.Add("@UserID", userID);
                    objCompHash.Add("@KittedCSTDate", currentCSTDateTime);

                    arrSpFieldSeq = new string[] { "@POAssignIDs", "@UserID", "@KittedCSTDate" };
                    recordCount = db.ExecuteNonQuery(objCompHash, "av_PurchaseOrder_Assign_Update", arrSpFieldSeq);
                    if (recordCount > 0)
                    {
                        errorMessage = "";// ResponseErrorCode.UpdatedSuccessfully.ToString();
                    }
                    else
                    {
                        errorMessage = clsGeneral.ResponseErrorCode.DataNotUpdated.ToString();
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    errorMessage = objExp.Message.ToString();
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return errorMessage;
        }
        public  KittingInfo GetKittingInfoByESN(string ESN, int ItemCompanyGUID)
        {
            KittingInfo kittingInfo = new KittingInfo();
            List<PurchaseOrderKitting> poDetails = default;// new List<PurchaseOrderKitting>();
            List<KitRawSKU> skus = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@ESN", ESN);
                    //objCompHash.Add("@ItemCompanyGUID", ItemCompanyGUID);

                    //arrSpFieldSeq = new string[] { "@ESN", "@ItemCompanyGUID" };
                    arrSpFieldSeq = new string[] { "@ESN" };
                    ds = db.GetDataSet(objCompHash, "av_KittingByESN_Select", arrSpFieldSeq);
                    poDetails = PopulateFulfillmentDetail(ds.Tables[0]);
                    skus = PopulateRawSKUs(ds.Tables[1]);
                    kittingInfo.PurchaseOrderKits = poDetails;
                    kittingInfo.RawSKUs = skus;


                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);
                }
                finally
                {
                    // db = null;

                }
            }
            return kittingInfo;
        }

        public KittingInfo GetPurchaseOrderKittingInfo(string poNumber, string ESN, string boxID, string palletID)
        {
            KittingInfo kittingInfo =  new KittingInfo();
            List<PurchaseOrderKitting> poDetails = default;
            List<KitRawSKU> skus = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@FulfillmentNumber", poNumber);
                    objCompHash.Add("@PalletID", palletID);
                    objCompHash.Add("@ESN", ESN);
                    objCompHash.Add("@BOXID", boxID);


                    arrSpFieldSeq = new string[] { "@FulfillmentNumber", "@PalletID", "@ESN", "@BOXID" };
                    ds = db.GetDataSet(objCompHash, "av_PurchaseOrderKitting_Select", arrSpFieldSeq);
                    poDetails = PopulateFulfillmentDetail(ds.Tables[0]);
                    skus = PopulateRawSKUs(ds.Tables[1]);
                    kittingInfo.PurchaseOrderKits = poDetails;
                    kittingInfo.RawSKUs = skus;


                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);
                }
                finally
                {
                    //db = null;

                }
            }
            return kittingInfo;
        }
        public List<PurchaseOrderKittingSummary> GetPurchaseOrderKittingSummary(string poNumber, string ESN, string boxID, int userID, string fromDate, string toDate)
        {
            List<PurchaseOrderKittingSummary> kittingSummary = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@FromDate", !string.IsNullOrEmpty(fromDate) ? fromDate : null);
                    objCompHash.Add("@ToDate", !string.IsNullOrEmpty(toDate) ? toDate : null);
                    objCompHash.Add("@UserID", userID);
                    objCompHash.Add("@FulfillmentNumber", poNumber);
                    objCompHash.Add("@BOXID", boxID);
                    objCompHash.Add("@IMEI", ESN);

                    arrSpFieldSeq = new string[] { "@FromDate", "@ToDate", "@UserID", "@FulfillmentNumber", "@BOXID", "@IMEI" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderKitting_Summary", arrSpFieldSeq);
                    kittingSummary = PopulateKittingSummary(dt);

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);
                }
                finally
                {
                   // db = null;

                }
            }
            return kittingSummary;
        }
        public List<POPallet> GetPurchaseOrderPallets(string poNumber)
        {
            List<POPallet> pallets = default;
            POPallet pOPallet;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@FulfillmentNumber", poNumber);

                    arrSpFieldSeq = new string[] { "@FulfillmentNumber" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseorderPalletByPO", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        pallets = new List<POPallet>();
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            pOPallet = new POPallet();

                            pOPallet.PalletID = clsGeneral.getColumnData(dataRow, "PalletID", string.Empty, false) as string;
                            pOPallet.PoPalletId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PoPalletId", 0, false));

                            pallets.Add(pOPallet);

                        }
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);
                }
                finally
                {
                    //db = null;

                }
            }
            return pallets;
        }
        public List<SKUiNFO> GetKittedSKUs(string ESN)
        {
            List<SKUiNFO> SKUs = default;
            SKUiNFO skuInfo = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@ESN", ESN);

                    arrSpFieldSeq = new string[] { "@ESN" };
                    dt = db.GetTableRecords(objCompHash, "av_KittedSKUByESN_Select", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        SKUs = new List<SKUiNFO>();
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            skuInfo = new SKUiNFO();

                            skuInfo.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                            skuInfo.IsKittedSKU = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsKittedSKU", false, false));
                            skuInfo.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));

                            SKUs.Add(skuInfo);

                        }
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);
                }
                finally
                {
                   // db = null;

                }
            }
            return SKUs;
        }

        private  List<PurchaseOrderKittingSummary> PopulateKittingSummary(DataTable dt)
        {

            List<PurchaseOrderKittingSummary> kittingSummary = default;
            PurchaseOrderKittingSummary kitSummary = default;
            if (dt != null && dt.Rows.Count > 0)
            {
                kittingSummary = new List<PurchaseOrderKittingSummary>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    kitSummary = new PurchaseOrderKittingSummary();

                    kitSummary.PalletID = clsGeneral.getColumnData(dataRow, "PalletID", string.Empty, false) as string;
                    kitSummary.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    kitSummary.SKU = clsGeneral.getColumnData(dataRow, "Item_Code", string.Empty, false) as string;
                    kitSummary.BoxID = clsGeneral.getColumnData(dataRow, "BoxNumber", string.Empty, false) as string;
                    kitSummary.Username = clsGeneral.getColumnData(dataRow, "Username", string.Empty, false) as string;
                    kitSummary.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    kitSummary.EsnCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "EsnCount", 0, false));
                    kitSummary.KittedCSTDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "KittedCSTDate", DateTime.Now, false));

                    kittingSummary.Add(kitSummary);

                }
            }

            return kittingSummary;
        }

        private  List<PurchaseOrderKitting> PopulateFulfillmentDetail(DataTable dt)
        {
            
            List<PurchaseOrderKitting> poDetails = default;// new List<PurchaseOrderShipmentDB>();
            PurchaseOrderKitting poDetail = default;
            if (dt != null && dt.Rows.Count > 0)
            {
                poDetails = new List<PurchaseOrderKitting>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    poDetail = new PurchaseOrderKitting();

                    poDetail.EsnSource = clsGeneral.getColumnData(dataRow, "ESNSource", string.Empty, false) as string;
                    poDetail.ShipToDate = clsGeneral.getColumnData(dataRow, "ShipTo_Date", string.Empty, false) as string;
                    poDetail.ErrorMessage = clsGeneral.getColumnData(dataRow, "ErrorMessage", string.Empty, false) as string;
                    poDetail.KittedSKU = clsGeneral.getColumnData(dataRow, "KittedSKU", string.Empty, false) as string;
                    poDetail.BoxNumber = clsGeneral.getColumnData(dataRow, "BoxNumber", string.Empty, false) as string;
                    poDetail.DEC = clsGeneral.getColumnData(dataRow, "MeidDec", string.Empty, false) as string;
                    poDetail.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    poDetail.HEX = clsGeneral.getColumnData(dataRow, "MeidHex", string.Empty, false) as string;
                    poDetail.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    poDetail.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    poDetail.PalletID = clsGeneral.getColumnData(dataRow, "PalletID", string.Empty, false) as string;
                    poDetail.POStatus = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    poDetail.QuantityCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                    poDetail.POAssignId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "POAssignId", 0, false));
                    poDetail.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));
                    poDetail.PO_Date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, false));

                    poDetails.Add(poDetail);

                }
            }

            return poDetails;
        }

        private  List<KitRawSKU> PopulateRawSKUs(DataTable dt)
        {
            List<KitRawSKU> skus = default;// new List<PurchaseOrderShipmentDB>();
            KitRawSKU skuInfo = default;
            if (dt != null && dt.Rows.Count > 0)
            {
                skus = new List<KitRawSKU>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    skuInfo = new KitRawSKU();

                    skuInfo.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    skuInfo.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    skuInfo.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    skuInfo.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));

                    skus.Add(skuInfo);
                }
            }
            return skus;
        }

    }
}
