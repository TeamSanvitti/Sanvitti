using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;
namespace SV.Framework.DAL.Inventory
{
   public class BlockEsnOperation: BaseCreateInstance
    {
       
        //private MslOperation mslOperation = MslOperation.CreateInstance<MslOperation>();

        public List<BlockESN> BlockEsnSearch(int CompanyID, string FromDate, string ToDate, string SKU, string esn, string action, string status)
        {
            List<BlockESN> EsnList = default;// new List<BlockESN>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                //DataSet ds = new DataSet();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CompanyID", CompanyID);
                    objCompHash.Add("@FromDate", string.IsNullOrEmpty(FromDate) ? null : FromDate);
                    objCompHash.Add("@ToDate", string.IsNullOrEmpty(ToDate) ? null : ToDate);
                    objCompHash.Add("@SKU", SKU);
                    objCompHash.Add("@ESN", esn);
                    objCompHash.Add("@Action", action);
                    objCompHash.Add("@Status", status);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@SKU", "@ESN", "@Action", "@Status" };
                    dt = db.GetTableRecords(objCompHash, "av_stockblock_Select", arrSpFieldSeq);

                    EsnList = PopulateBlockESNs(dt);

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
            return EsnList;

        }

        public  List<BlockEsn> BlockEsnValidate(int ItemCompanyGUID, string esnData)
        {
            List<BlockEsn> EsnList = default;//new List<BlockEsn>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                //DataSet ds = new DataSet();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@ItemCompanyGUID", ItemCompanyGUID);
                    objCompHash.Add("@ESNDATA", esnData);

                    arrSpFieldSeq = new string[] { "@ItemCompanyGUID", "@ESNDATA" };
                    dt = db.GetTableRecords(objCompHash, "av_stockblock_Validate", arrSpFieldSeq);

                    EsnList = PopulateBlockEsn(dt);

                }
                catch (Exception objExp)
                {
                    //serviceOrders = null;
                    Logger.LogMessage(objExp, this); throw new Exception(objExp.Message.ToString());

                }
                finally
                {
                    //   db = null;
                }
            }

            return EsnList;

        }

        public string ValidateRequiredFields(BlockEsnInfo request)
        {
            string returnMessage = string.Empty;
            System.Text.StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(request.Comment))
                sb.Append("Comment required!, ");
            if (string.IsNullOrEmpty(request.ESNData))
                sb.Append("ESNs required!, ");
            if(request.ItemCompanyGUID == 0)
                sb.Append("SKU required!, ");
            if (request.ReceivedBy == 0)
                sb.Append("Received by required!, ");
            returnMessage = sb.ToString();
            return returnMessage;
        }
        public int StockBlockInsert(BlockEsnInfo request)
        {
            int returnValue = 0;
            //errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                string RequestData = clsGeneral.SerializeObject(request);
                try
                {
                    objCompHash.Add("@BlockBy", request.UserID);
                    objCompHash.Add("@ReceivedBy", request.ReceivedBy);
                    objCompHash.Add("@ItemCompanyGUID", request.ItemCompanyGUID);
                    objCompHash.Add("@Comment", request.Comment);
                    objCompHash.Add("@ESNDATA", request.ESNData);

                    arrSpFieldSeq = new string[] { "@BlockBy", "@ReceivedBy", "@ItemCompanyGUID", "@Comment", "@ESNDATA" };
                    object obj = db.ExecuteScalar(objCompHash, "av_stockblock_Insert", arrSpFieldSeq);
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
        public  int StockBlockUpdate(int blockID, string status, int UserID)
        {
            int returnValue = 0;
            //errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@BlockID", blockID);
                    objCompHash.Add("@Status", status);
                    objCompHash.Add("@UserID", UserID);

                    arrSpFieldSeq = new string[] { "@BlockID", "@Status", "@UserID" };
                    object obj = db.ExecuteScalar(objCompHash, "av_stockblock_Update", arrSpFieldSeq);
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
                   // db = null;
                }
            }
            return returnValue;
        }



        private  List<BlockESN> PopulateBlockESNs(DataTable dt)
        {
            BlockESN blockEsn = default;
            string esnData = default;
            List<BlockESN> EsnList = new List<BlockESN>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    blockEsn = new BlockESN();
                    blockEsn.BlockID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "BlockID", 0, false));
                    blockEsn.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    blockEsn.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    blockEsn.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    blockEsn.Action = clsGeneral.getColumnData(dataRow, "Action", string.Empty, false) as string;
                    blockEsn.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                    blockEsn.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", DateTime.Now, false));
                    blockEsn.CreateBy = clsGeneral.getColumnData(dataRow, "BlockBy", string.Empty, false) as string;
                    blockEsn.ReceiveBy = clsGeneral.getColumnData(dataRow, "ReceivedBy", string.Empty, false) as string;
                    blockEsn.ApprovedBy = clsGeneral.getColumnData(dataRow, "ApprovedBy", string.Empty, false) as string;
                    esnData = clsGeneral.getColumnData(dataRow, "ESNDATA", string.Empty, false) as string;
                    blockEsn.ESNDATA = esnData;//.Replace(",", "|"); //clsGeneral.getColumnData(dataRow, "ESNDATA", string.Empty, false) as string;
                    EsnList.Add(blockEsn);

                }
            }
            return EsnList;

        }

        private  List<BlockEsn> PopulateBlockEsn(DataTable dt)
        {
            BlockEsn blockEsn = default;
            List<BlockEsn> EsnList = new List<BlockEsn>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    blockEsn = new BlockEsn();
                    blockEsn.ErrorMessage = clsGeneral.getColumnData(dataRow, "ErrorMessage", string.Empty, false) as string;
                    blockEsn.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    EsnList.Add(blockEsn);

                }
            }
            return EsnList;

        }

    }
}
 