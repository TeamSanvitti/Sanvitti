using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;

namespace avii.Classes
{
    public class CarriersOperation
    {
        public static List<Carriers> GetCarriersList(int carriersGUID, bool active)
        {
            List<Carriers> carriersList = new List<Carriers>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CarrierGUID", 0);
                objCompHash.Add("@Active", active);

                arrSpFieldSeq = new string[] { "@CarrierGUID", "@Active" };

                dataTable = db.GetTableRecords(objCompHash, "av_Carrier_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    carriersList = PopulateCarriers(dataTable);
                }

            }
            catch (Exception ex)
            {
                //throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return carriersList;
        }
        public static List<Carriers> GetCarriersList(int makerid, int carrierGUID)
        {
            List<Carriers> carriersList = new List<Carriers>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@MakerGUID", makerid);
                objCompHash.Add("@CarrierGUID", carrierGUID);

                arrSpFieldSeq = new string[] { "@MakerGUID", "@CarrierGUID" };

                dataTable = db.GetTableRecords(objCompHash, "av_Carriers_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    carriersList = PopulateCarriers(dataTable);
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

            return carriersList;
        }
        public static List<Carriers> PopulateCarriers(DataTable dataTable)
        {
            List<Carriers> carriersList = new List<Carriers>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        Carriers carriersObj = new Carriers();
                        carriersObj.CarrierGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CarrierGUID", 0, false));
                        carriersObj.CarrierName = clsGeneral.getColumnData(dataRow, "CarrierName", string.Empty, false) as string;
                        carriersObj.CarrierLogo = clsGeneral.getColumnData(dataRow, "CarrierLogo", string.Empty, false) as string;
                        carriersObj.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Active", false, false));

                        carriersList.Add(carriersObj);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return carriersList;
        }

        public static int CreateUpdateCarrier(Carriers carriersObj)
        {
            int returnValue = 0;
            
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CarrierGUID", carriersObj.CarrierGUID);
                objCompHash.Add("@CarrierName", carriersObj.CarrierName);
                objCompHash.Add("@CarrierLogo", carriersObj.CarrierLogo);
                objCompHash.Add("@Active", carriersObj.Active);



                arrSpFieldSeq = new string[] { "@CarrierGUID", "@CarrierName", "@CarrierLogo", "@Active" };
                returnValue = db.ExecCommand(objCompHash, "av_Carrier_InsertUpdate", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return returnValue;
        }
        public static int DeleteCarrier(int carrierGUID)
        {
            int returnValue = 0;

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CarrierGUID", carrierGUID);
                


                arrSpFieldSeq = new string[] { "@CarrierGUID" };
                returnValue = db.ExecCommand(objCompHash, "av_Carrier_Delete", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return returnValue;
        }
    }
}