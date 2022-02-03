using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Inventory
{
    public class EsnReverseOperation : BaseCreateInstance
    {
        public string ESNReverseUpdate(List<ESNReverse> esns, int userID)
        {
            string errorMessage = string.Empty;
            int recordCount = 0;
            using (DBConnect db = new DBConnect())
            {
                DataTable esnDT = ESNReverseData(esns);
                string[] arrSpFieldSeq;
                //string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@av_ESNReverseType", esnDT);

                    arrSpFieldSeq = new string[] { "@av_ESNReverseType" };
                    recordCount = db.ExecuteNonQuery(objCompHash, "av_ESNReverse_Update", arrSpFieldSeq);
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

        public  List<ESNReverse> ESNValidate(List<ESNReverse> esns, int CompanyID, out List<ESNskuReverse> skuList)
        {
            skuList = default;//new List<ESNskuReverse>();
            List<ESNReverse> esnList = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;//new DataSet();
                DataTable esnDT = ESNData(esns);
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", CompanyID);
                    objCompHash.Add("@ESNTable", esnDT);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@ESNTable" };
                    ds = db.GetDataSet(objCompHash, "av_PurchaseOrder_ESN_Validate", arrSpFieldSeq);

                    esnList = PopulateESNs(ds.Tables[0]);
                    skuList = PopulateSKUs(ds.Tables[1]);

                }
                catch (Exception ex)
                {

                }
                finally
                {
                    //db = null;

                }
            }
            return esnList;
        }
        private  DataTable ESNReverseData(List<ESNReverse> EsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ROWID", typeof(System.Int32));
            dt.Columns.Add("ESN", typeof(System.String));
            dt.Columns.Add("EsnID", typeof(System.Int32));
            dt.Columns.Add("POAssignID", typeof(System.Int32));
            dt.Columns.Add("OrderDetailId", typeof(System.Int32));
            int rowID = 1;
            DataRow row;

            if (EsnList != null && EsnList.Count > 0)
            {
                foreach (ESNReverse item in EsnList)
                {
                    row = dt.NewRow();
                    row["ROWID"] = rowID;
                    row["ESN"] = item.ESN;
                    row["EsnID"] = item.ESNID;
                    row["POAssignID"] = item.POAssignID;
                    row["OrderDetailId"] = item.OrderDetailId;
                    dt.Rows.Add(row);
                    rowID = rowID + 1;
                }
            }
            return dt;
        }

        private  DataTable ESNData(List<ESNReverse> EsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ROWID", typeof(System.Int32));
            dt.Columns.Add("ESN", typeof(System.String));
            int rowID = 1;
            DataRow row;

            if (EsnList != null && EsnList.Count > 0)
            {
                foreach (ESNReverse item in EsnList)
                {
                    row = dt.NewRow();
                    row["ROWID"] = rowID;
                    row["ESN"] = item.ESN;
                    dt.Rows.Add(row);
                    rowID = rowID + 1;
                }
            }
            return dt;
        }

        private  List<ESNReverse> PopulateESNs(DataTable dt)
        {

            List<ESNReverse> esnList = default;//null;
            ESNReverse esnInfo = default;//null;
            if (dt != null && dt.Rows.Count > 0)
            {
                esnList = new List<ESNReverse>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    esnInfo = new ESNReverse();

                    esnInfo.KittedSKU = clsGeneral.getColumnData(dataRow, "KittedSKU", string.Empty, false) as string;
                    esnInfo.RawSKU = clsGeneral.getColumnData(dataRow, "RawSKU", string.Empty, false) as string;
                    esnInfo.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    esnInfo.ErrorMessage = clsGeneral.getColumnData(dataRow, "ErrorMessage", string.Empty, false) as string;
                    //kitSummary.BoxID = clsGeneral.getColumnData(dataRow, "BoxNumber", string.Empty, false) as string;
                    //kitSummary.Username = clsGeneral.getColumnData(dataRow, "Username", string.Empty, false) as string;
                    esnInfo.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    esnInfo.ESNID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ESNID", 0, false));
                    esnInfo.OrderDetailId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderDetailId", 0, false));
                    esnInfo.POAssignID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "POAssignID", 0, false));
                    esnInfo.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));
                    //kitSummary.KittedCSTDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "KittedCSTDate", DateTime.Now, false));

                    esnList.Add(esnInfo);

                }
            }

            return esnList;
        }
        private  List<ESNskuReverse> PopulateSKUs(DataTable dt)
        {
            List<ESNskuReverse> skuList = default;//null;
            ESNskuReverse skuInfo = default;//null;
            if (dt != null && dt.Rows.Count > 0)
            {
                skuList = new List<ESNskuReverse>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    skuInfo = new ESNskuReverse();

                    skuInfo.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    skuInfo.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    skuInfo.RawSKU = clsGeneral.getColumnData(dataRow, "RawSKU", string.Empty, false) as string;
                    skuInfo.ESNCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ESNCount", 0, false));
                    skuInfo.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_Hand", 0, false));
                    skuInfo.ProposedStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ProposedStock", 0, false));
                   
                    skuList.Add(skuInfo);

                }
            }

            return skuList;
        }


    }
}
