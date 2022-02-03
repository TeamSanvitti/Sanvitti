using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Inventory
{
    public class InvalidEsnOperation : BaseCreateInstance
    {
        public  List<InvalidEsn> GetInvalidEsnList(int companyId, int categoryID, string sku, int IsAssignedFlag)
        {
            List<InvalidEsn> InvalidEsnList = default;//new List<InvalidEsn>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                                       //DataSet ds = new DataSet();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CompanyID", companyId);
                    objCompHash.Add("@SKU", sku);
                    objCompHash.Add("@CategoryID", categoryID);
                    objCompHash.Add("@IsAssigned", IsAssignedFlag);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@SKU", "@CategoryID", "@IsAssigned" };
                    dt = db.GetTableRecords(objCompHash, "av_InvalidEsnMeid_Select", arrSpFieldSeq);

                    InvalidEsnList = PopulateInvalidEsn(dt);

                }
                catch (Exception objExp)
                {
                    //serviceOrders = null;
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());

                }
                finally
                {
                    //db = null;
                }
            }
            return InvalidEsnList;

        }
        private static List<InvalidEsn> PopulateInvalidEsn(DataTable dt)
        {
            InvalidEsn invalidEsn = default;//null;
            List<InvalidEsn> InvalidEsnList = default;//new List<InvalidEsn>();
            if (dt != null && dt.Rows.Count > 0)
            {
                InvalidEsnList = new List<InvalidEsn>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    invalidEsn = new InvalidEsn();
                    invalidEsn.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    invalidEsn.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    invalidEsn.MEID = clsGeneral.getColumnData(dataRow, "MeidDec", string.Empty, false) as string;
                    invalidEsn.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    invalidEsn.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    invalidEsn.ESNColor = Convert.ToString(clsGeneral.getColumnData(dataRow, "ESNColor", string.Empty, false));
                    invalidEsn.MeidColor = Convert.ToString(clsGeneral.getColumnData(dataRow, "MeidColor", string.Empty, false));
                    invalidEsn.FulfillmentNumber = Convert.ToString(clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false));
                    InvalidEsnList.Add(invalidEsn);

                }
            }
            return InvalidEsnList;

        }

    }
}
