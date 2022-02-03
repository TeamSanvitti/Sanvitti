using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class PackingslipOperation : BaseCreateInstance
    {
        public  void PackingSlipInsertUpdate(int poid, string packingSlip)
        {
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@PO_ID", poid);
                    objCompHash.Add("@PackingSlip", packingSlip);
                    arrSpFieldSeq = new string[] { "@PO_ID", "@PackingSlip" };
                    db.ExeCommand(objCompHash, "av_PackingSlipInsert", arrSpFieldSeq);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); // throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }

    }
}
