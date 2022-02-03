using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Inventory
{
    public class StockOperations : BaseCreateInstance
    {
        public int RunningStockInsertUpdate(string currentDate)
        {
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CurrentDate", currentDate);

                    arrSpFieldSeq = new string[] { "@CurrentDate" };
                    returnValue = db.ExecCommand(objCompHash, "Av_RunningStockInsertUpdate", arrSpFieldSeq);

                    // return returnValue;
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnValue;
        }
        
    }
}
