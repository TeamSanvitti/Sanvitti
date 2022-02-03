using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SV.Framework.Admin
{
    public class ItemLogOperation
    {
        public static void ItemLogInsert(ItemLogModel request)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ItemGUID", request.ItemGUID);
                objCompHash.Add("@ItemCompanyGUID", request.ItemCompanyGUID);
                objCompHash.Add("@ActionName", request.ActionName);
                objCompHash.Add("@RequestData", request.RequestData);
                objCompHash.Add("@ResponseData", request.ResponseData);
                objCompHash.Add("@Status", request.Status);
                objCompHash.Add("@CreateUserID", request.CreateUserID);
                objCompHash.Add("@Comment", request.Comment);
                objCompHash.Add("@SKU", request.SKU??"");


                arrSpFieldSeq = new string[] { "@ItemGUID", "@ItemCompanyGUID", "@ActionName", "@RequestData", "@ResponseData", "@Status", 
                    "@CreateUserID", "@Comment", "@SKU" };
                db.ExeCommand(objCompHash, "av_ItemLogInsert", arrSpFieldSeq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }


        }

        public static List<ItemLog> GetProductLog(int itemGUID, int itemCompanyGUID)
        {
            List<ItemLog> logList = new List<ItemLog>();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@ItemGUID", itemGUID);
                objCompHash.Add("@ItemCompanyGUID", itemCompanyGUID);
               
                arrSpFieldSeq = new string[] { "@ItemGUID", "@ItemCompanyGUID" };
                ds = db.GetDataSet(objCompHash, "av_ItemlogSelect", arrSpFieldSeq);

                logList = PopulateItemLog(ds.Tables[0]);

            }
            catch (Exception objExp)
            {
                //serviceOrders = null;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                db = null;
            }

            return logList;

        }
        private static List<ItemLog> PopulateItemLog(DataTable dt)
        {
            ItemLog itemLog = null;
            List<ItemLog> logList = new List<ItemLog>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    itemLog = new ItemLog();
                    itemLog.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    itemLog.ActionName = clsGeneral.getColumnData(dataRow, "ActionName", string.Empty, false) as string;
                    itemLog.UserName = clsGeneral.getColumnData(dataRow, "Username", string.Empty, false) as string;
                  //  itemLog.RequestData = clsGeneral.getColumnData(dataRow, "RequestData", string.Empty, false) as string;
                    //itemLog.ResponseData = clsGeneral.getColumnData(dataRow, "ResponseData", string.Empty, false) as string;
                    itemLog.RequestData = (clsGeneral.getColumnData(dataRow, "RequestData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;");
                    itemLog.ResponseData = (clsGeneral.getColumnData(dataRow, "ResponseData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;");

                    itemLog.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                    itemLog.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    itemLog.ModelNumber = clsGeneral.getColumnData(dataRow, "ModelNumber", string.Empty, false) as string;
                    itemLog.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", DateTime.Now, false));
                    itemLog.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    logList.Add(itemLog);

                }
            }
            return logList;

        }

    }
}
