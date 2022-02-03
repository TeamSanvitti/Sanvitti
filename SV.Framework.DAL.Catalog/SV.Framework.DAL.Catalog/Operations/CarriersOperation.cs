using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Catalog
{
    public class CarriersOperation : BaseCreateInstance
    {
        public  List<Carriers> GetCarriersList(int carriersGUID, bool active)
        {
            List<Carriers> carriersList = default;// new List<Carriers>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
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
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return carriersList;
        }
        public List<Carriers> GetCarriersList(int makerid, int carrierGUID)
        {
            List<Carriers> carriersList = default;// new List<Carriers>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
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
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return carriersList;
        }
        public List<Carriers> PopulateCarriers(DataTable dataTable)
        {
            List<Carriers> carriersList = default;// new List<Carriers>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    carriersList = new List<Carriers>();
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
                Logger.LogMessage(ex, this);   //throw ex;
            }
            return carriersList;
        }

        public int CreateUpdateCarrier(Carriers carriersObj)
        {
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
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
                    Logger.LogMessage(exp, this);   //throw exp;
                }
            }
            return returnValue;
        }
        public  int DeleteCarrier(int carrierGUID)
        {
            int returnValue = 0;

            using (DBConnect db = new DBConnect())
            {
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
                    Logger.LogMessage(exp, this);   //throw exp;
                }
            }
            return returnValue;
        }
    }
}
