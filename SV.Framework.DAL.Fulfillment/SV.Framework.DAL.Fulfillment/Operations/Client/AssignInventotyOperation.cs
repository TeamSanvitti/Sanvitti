using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class AssignInventotyOperation : BaseCreateInstance
    {
        public void AssignInventoryService()
        {
            //string errorMessage = default;//string.Empty;
            int recordCount = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@ItemCompanyGUID", 0);
                    
                    arrSpFieldSeq = new string[] { "@ItemCompanyGUID" };
                    recordCount = db.ExecuteNonQuery(objCompHash, "av_InventotyAssignedQty_CronJob", arrSpFieldSeq);
                    
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //errorMessage = objExp.Message.ToString();
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            //return errorMessage;
        }

    }
}
