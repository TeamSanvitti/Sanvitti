using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Inventory
{
    public class EsnBoxOperation : BaseCreateInstance
    {
        public  string BoxIDUpdate(int esnID, string boxID, string ESN)
        {
            string returnMessage = "Could not update";
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                int returnValue = 0;
                try
                {
                    objCompHash.Add("@EsnID", esnID);
                    objCompHash.Add("@ESN", ESN);
                    objCompHash.Add("@BoxID", boxID);

                    arrSpFieldSeq = new string[] { "@EsnID", "@ESN", "@BoxID" };
                    returnValue = db.ExecuteNonQuery(objCompHash, "svESNBoxID_Update", arrSpFieldSeq);
                    if (returnValue > 0)
                    {
                        returnMessage = "";
                    }
                }
                catch (Exception objExp)
                {
                    returnMessage = objExp.Message;
                    Logger.LogMessage(objExp, this); //
                }
                finally
                {
                    // SV.Framework.Admin.ItemLogOperation.ItemLogInsert(logRequest);
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnMessage;
        }
        public  EsnBoxIDInfo GetEsnBoxIDInfo(string ESN)
        {
            EsnBoxIDInfo esnBoxIDInfo = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@ESN", ESN);

                    arrSpFieldSeq = new string[] { "@ESN" };
                    dt = db.GetTableRecords(objCompHash, "svESNBoxIDInfo_Select", arrSpFieldSeq);
                    esnBoxIDInfo = PopulateESNwithHeaders(dt);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return esnBoxIDInfo;
        }
        private  EsnBoxIDInfo PopulateESNwithHeaders(DataTable dt)
        {
            
            EsnBoxIDInfo objESN = default;


            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    objESN = new EsnBoxIDInfo();
                    objESN.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    objESN.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    objESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    objESN.ReceiveDate = clsGeneral.getColumnData(dataRow, "ReceiveDate", string.Empty, false) as string;
                    objESN.HEX = clsGeneral.getColumnData(dataRow, "MeidHex", string.Empty, false) as string;
                    objESN.DEC = clsGeneral.getColumnData(dataRow, "MeidDec", string.Empty, false) as string;
                    objESN.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));
                    objESN.EsnID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "EsnID", 0, false));
                    objESN.ErrorMessage = Convert.ToString(clsGeneral.getColumnData(dataRow, "ErrorMessage", "", false));
                    objESN.FulfillmentNumber = Convert.ToString(clsGeneral.getColumnData(dataRow, "PO_Num", "", false));
                   
                    objESN.BoxID = clsGeneral.getColumnData(dataRow, "BoxID", string.Empty, false) as string;
                    objESN.SerialNumber = clsGeneral.getColumnData(dataRow, "SerialNumber", string.Empty, false) as string;
                    objESN.BoxItems = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ContainerQuantity", 0, false));
                    objESN.AssignedBoxItems = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AssignedCount", 0, false));
                    

                }
            }
            return objESN;

        }
    }

}
