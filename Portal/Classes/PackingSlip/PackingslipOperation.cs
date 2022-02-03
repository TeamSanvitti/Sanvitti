using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class PackingslipOperation
    {
        public static void PackingSlipInsertUpdate(int poid, string packingSlip)
        {
            DBConnect db = new DBConnect();
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

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

    }
}