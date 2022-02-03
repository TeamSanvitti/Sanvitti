using System;
using System.Collections;
using System.Collections.Generic;

namespace avii.Classes
{
    public class MDNXmlOperations
    {
        public static void CreateMDNProvisioning(List<avii.Classes.MDNXml> MDNListObj)
        {
            string mdnXml = avii.Classes.clsGeneral.SerializeObject(MDNListObj);
            
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@MDNXml", mdnXml);
                arrSpFieldSeq = new string[] { "@MDNXml" };
                db.GetTableRecords(objCompHash, "av_MDNProvisioning_InsertUpdate", arrSpFieldSeq);


            }
            catch (Exception exp)
            {
                throw exp;
            }
            
        }
    }
}