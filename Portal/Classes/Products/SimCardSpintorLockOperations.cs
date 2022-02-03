using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class SimCardSpintorLockOperations
    {
        public static void GetSimSpintorLockTypesList(out List<SimCardType> SimCardTypeList, out List<SpintorLockType> SpintorLockTypeList)
        {
            SimCardTypeList = new List<SimCardType>();
            SpintorLockTypeList = new List<SpintorLockType>();

            DataSet dataSet = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@SimCardTypeID", 0);
               

                arrSpFieldSeq = new string[] { "@SimCardTypeID" };

                dataSet = db.GetDataSet(objCompHash, "av_SimCard_SpintorLock_Types_Select", arrSpFieldSeq);

                if (dataSet != null && dataSet.Tables.Count > 0)
                {
                    SimCardTypeList = PopulateSimCardTypes(dataSet.Tables[0]);
                    SpintorLockTypeList = PopulateSpintorLockTypes(dataSet.Tables[1]);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

           // return carriersList;
        }
        public static List<SimCardType> PopulateSimCardTypes(DataTable dataTable)
        {
            List<SimCardType> SimCardTypeList = new List<SimCardType>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        SimCardType simCardTypeObj = new SimCardType();
                        simCardTypeObj.SimCardTypeID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "SimCardTypeID", 0, false));
                        simCardTypeObj.SimCardTypeText = clsGeneral.getColumnData(dataRow, "SimCardTypeText", string.Empty, false) as string;

                        SimCardTypeList.Add(simCardTypeObj);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return SimCardTypeList;
        }
        public static List<SpintorLockType> PopulateSpintorLockTypes(DataTable dataTable)
        {
            List<SpintorLockType> SpintorLockTypeList = new List<SpintorLockType>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        SpintorLockType spintorLockTypeObj = new SpintorLockType();
                        spintorLockTypeObj.SpintorLockTypeID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "SpintorLockTypeID", 0, false));
                        spintorLockTypeObj.SpintorLockTypeText = clsGeneral.getColumnData(dataRow, "SpintorLockTypeText", string.Empty, false) as string;

                        SpintorLockTypeList.Add(spintorLockTypeObj);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return SpintorLockTypeList;
        }

    }

   // [Serializable]
   // [XmlRoot(ElementName = "Carriers", IsNullable = true)]
    public class SimCardType
    {

        //[XmlElement(ElementName = "CarrierGUID", IsNullable = true)]
        public int SimCardTypeID { get; set; }
       // [XmlIgnore]
        public string SimCardTypeText { get; set; }
        
    }
    public class SpintorLockType
    {

       
        public int SpintorLockTypeID    { get; set; }
      
        public string SpintorLockTypeText { get; set; }
       

    }

}