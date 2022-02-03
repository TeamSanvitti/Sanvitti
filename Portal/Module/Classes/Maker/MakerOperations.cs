using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace avii.Classes
{
    public class MakerOperations
    {
        public static DataTable GetStates()
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();


            try
            {

                objParams.Add("@countryID", 0);

                arrSpFieldSeq = new string[] { "@countryID" };

                dataTable = db.GetTableRecords(objParams, "av_State_Select", arrSpFieldSeq);



            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return dataTable;
        }
        public static int CreateMaker(ItemMaker itemMakerObj)
        {
            int returnValue = 0;
            string makerXml = clsGeneral.SerializeObject(itemMakerObj);
            

            //DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@MakerXml", makerXml);


                arrSpFieldSeq = new string[] { "@MakerXml" };
                returnValue = db.ExecCommand(objCompHash, "av_Maker_InsertUpdate", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return returnValue;
        }
        #region GetMakerList --
        public static List<avii.Classes.ItemMaker> GetMakerList(int makerGUID, string makerName, string shortName, int active, int showUnderCatalog)
        {
            DBConnect objDB = new DBConnect();
            List<avii.Classes.ItemMaker> lstMaker = new List<avii.Classes.ItemMaker>();
            avii.Classes.ItemMaker objMaker;

            DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@MakerGUID", makerGUID);
                objCompHash.Add("@MakerName", makerName);
                objCompHash.Add("@ShortName", shortName);
                objCompHash.Add("@Active", active);
                objCompHash.Add("@ShowunderCatalog", showUnderCatalog);

                arrSpFieldSeq = new string[] { "@MakerGUID", "@MakerName", "@ShortName", "@Active", "@ShowunderCatalog" };
                dataTable = objDB.GetTableRecords(objCompHash, "av_ItemMaker_SELECT", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            
                            Address address = new Address();
                            ContactInfo contactInfo = new ContactInfo();
                            objMaker = new avii.Classes.ItemMaker();
                            objMaker.MakerGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "makerGUID", 0, false));
                            objMaker.AddressID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "addressID", 0, false));
                            objMaker.MakerName = clsGeneral.getColumnData(dataRow, "makerName", string.Empty, false) as string;
                            objMaker.ShortName = clsGeneral.getColumnData(dataRow, "ShortName", string.Empty, false) as string;
                            objMaker.MakerImage = clsGeneral.getColumnData(dataRow, "makerImage", string.Empty, false) as string;
                            objMaker.MakerDescription = clsGeneral.getColumnData(dataRow, "makerdesc", string.Empty, false) as string;
                            objMaker.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                            objMaker.ShowunderCatalog = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ShowunderCatalog", false, false));
                        
                            AddressType addressType = (AddressType)Enum.Parse(typeof(AddressType), dataRow["AddressType"].ToString());
                            address.AddressType = addressType;
                            address.Address1 = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                            address.Address2 = clsGeneral.getColumnData(dataRow, "Address2", string.Empty, false) as string;
                            address.City = clsGeneral.getColumnData(dataRow, "City", string.Empty, false) as string;
                            address.Country = clsGeneral.getColumnData(dataRow, "Country", string.Empty, false) as string;
                            address.State = clsGeneral.getColumnData(dataRow, "State", string.Empty, false) as string;
                            address.Zip = clsGeneral.getColumnData(dataRow, "zip", string.Empty, false) as string;

                            objMaker.MakerAddresses = address;

                            contactInfo.CellPhone = clsGeneral.getColumnData(dataRow, "CellPhone", string.Empty, false) as string;
                            contactInfo.ContactName = clsGeneral.getColumnData(dataRow, "ContactName", string.Empty, false) as string;
                            contactInfo.Email1 = clsGeneral.getColumnData(dataRow, "Email1", string.Empty, false) as string;
                            contactInfo.Email2 = clsGeneral.getColumnData(dataRow, "Email2", string.Empty, false) as string;
                            contactInfo.HomePhone = clsGeneral.getColumnData(dataRow, "HomePhone", string.Empty, false) as string;
                            contactInfo.OfficePhone1 = clsGeneral.getColumnData(dataRow, "OfficePhone1", string.Empty, false) as string;
                            contactInfo.OfficePhone2 = clsGeneral.getColumnData(dataRow, "OfficePhone2", string.Empty, false) as string;
                            objMaker.MakerContactInfo = contactInfo;

                            objMaker.MakerCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MakerCount", 0, false));

                            lstMaker.Add(objMaker);
                        }
                    }
                
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return lstMaker;
        }
        public static List<avii.Classes.ItemMaker> GetMakerList(int carrierID)
        {
            DBConnect objDB = new DBConnect();
            List<avii.Classes.ItemMaker> lstMaker = new List<avii.Classes.ItemMaker>();
            avii.Classes.ItemMaker objMaker;

            DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@MakerGUID", 0);
                objCompHash.Add("@MakerName", null);
                objCompHash.Add("@ShortName", null);
                objCompHash.Add("@Active", 1);
                objCompHash.Add("@ShowunderCatalog", 1);
                objCompHash.Add("@CarrierID", carrierID);


                arrSpFieldSeq = new string[] { "@MakerGUID", "@MakerName", "@ShortName", "@Active", "@ShowunderCatalog", "@CarrierID" };
                dataTable = objDB.GetTableRecords(objCompHash, "av_ItemMaker_SELECT", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {

                        Address address = new Address();
                        ContactInfo contactInfo = new ContactInfo();
                        objMaker = new avii.Classes.ItemMaker();
                        objMaker.MakerGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "makerGUID", 0, false));
                        objMaker.AddressID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "addressID", 0, false));
                        objMaker.MakerName = clsGeneral.getColumnData(dataRow, "makerName", string.Empty, false) as string;
                        objMaker.ShortName = clsGeneral.getColumnData(dataRow, "ShortName", string.Empty, false) as string;
                        objMaker.MakerImage = clsGeneral.getColumnData(dataRow, "makerImage", string.Empty, false) as string;
                        objMaker.MakerDescription = clsGeneral.getColumnData(dataRow, "makerdesc", string.Empty, false) as string;
                        objMaker.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                        AddressType addressType = (AddressType)Enum.Parse(typeof(AddressType), dataRow["AddressType"].ToString());
                        address.AddressType = addressType;
                        address.Address1 = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                        address.Address2 = clsGeneral.getColumnData(dataRow, "Address2", string.Empty, false) as string;
                        address.City = clsGeneral.getColumnData(dataRow, "City", string.Empty, false) as string;
                        address.Country = clsGeneral.getColumnData(dataRow, "Country", string.Empty, false) as string;
                        address.State = clsGeneral.getColumnData(dataRow, "State", string.Empty, false) as string;
                        address.Zip = clsGeneral.getColumnData(dataRow, "zip", string.Empty, false) as string;

                        objMaker.MakerAddresses = address;

                        contactInfo.CellPhone = clsGeneral.getColumnData(dataRow, "CellPhone", string.Empty, false) as string;
                        contactInfo.ContactName = clsGeneral.getColumnData(dataRow, "ContactName", string.Empty, false) as string;
                        contactInfo.Email1 = clsGeneral.getColumnData(dataRow, "Email1", string.Empty, false) as string;
                        contactInfo.Email2 = clsGeneral.getColumnData(dataRow, "Email2", string.Empty, false) as string;
                        contactInfo.HomePhone = clsGeneral.getColumnData(dataRow, "HomePhone", string.Empty, false) as string;
                        contactInfo.OfficePhone1 = clsGeneral.getColumnData(dataRow, "OfficePhone1", string.Empty, false) as string;
                        contactInfo.OfficePhone2 = clsGeneral.getColumnData(dataRow, "OfficePhone2", string.Empty, false) as string;
                        objMaker.MakerContactInfo = contactInfo;

                        objMaker.MakerCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MakerCount", 0, false));

                        lstMaker.Add(objMaker);
                    }
                }

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return lstMaker;
        }
        #endregion

        public static ItemMaker GetMakerInfo(int makerGUID)
        {
            avii.Classes.DBConnect objDB = new avii.Classes.DBConnect();
            
            avii.Classes.ItemMaker objMaker = new avii.Classes.ItemMaker(); 

            DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@MakerGUID", makerGUID);
                objCompHash.Add("@MakerName", null);

                objCompHash.Add("@ShortNamee", null);

                arrSpFieldSeq = new string[] { "@MakerGUID", "@MakerName", "@ShortName" };
                dataTable = objDB.GetTableRecords(objCompHash, "av_ItemMaker_SELECT", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {

                        Address address = new Address();
                        ContactInfo contactInfo = new ContactInfo();
                        
                        objMaker.MakerGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "makerGUID", 0, false));
                        objMaker.AddressID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "addressID", 0, false));
                        objMaker.MakerName = clsGeneral.getColumnData(dataRow, "makerName", string.Empty, false) as string;
                        objMaker.ShortName = clsGeneral.getColumnData(dataRow, "ShortName", string.Empty, false) as string;
                        objMaker.MakerImage = clsGeneral.getColumnData(dataRow, "makerImage", string.Empty, false) as string;
                        objMaker.MakerDescription = clsGeneral.getColumnData(dataRow, "makerdesc", string.Empty, false) as string;
                        objMaker.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                        objMaker.ShowunderCatalog = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ShowunderCatalog", false, false));
                        AddressType addressType = (AddressType)Enum.Parse(typeof(AddressType), dataRow["AddressType"].ToString());
                        address.AddressType = addressType;
                        address.Address1 = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                        address.Address2 = clsGeneral.getColumnData(dataRow, "Address2", string.Empty, false) as string;
                        address.City = clsGeneral.getColumnData(dataRow, "City", string.Empty, false) as string;
                        address.Country = clsGeneral.getColumnData(dataRow, "Country", string.Empty, false) as string;
                        address.State = clsGeneral.getColumnData(dataRow, "State", string.Empty, false) as string;
                        address.Zip = clsGeneral.getColumnData(dataRow, "zip", string.Empty, false) as string;

                        objMaker.MakerAddresses = address;

                        contactInfo.CellPhone = clsGeneral.getColumnData(dataRow, "CellPhone", string.Empty, false) as string;
                        contactInfo.ContactName = clsGeneral.getColumnData(dataRow, "ContactName", string.Empty, false) as string;
                        contactInfo.Email1 = clsGeneral.getColumnData(dataRow, "Email1", string.Empty, false) as string;
                        contactInfo.Email2 = clsGeneral.getColumnData(dataRow, "Email2", string.Empty, false) as string;
                        contactInfo.HomePhone = clsGeneral.getColumnData(dataRow, "HomePhone", string.Empty, false) as string;
                        contactInfo.OfficePhone1 = clsGeneral.getColumnData(dataRow, "OfficePhone1", string.Empty, false) as string;
                        contactInfo.OfficePhone2 = clsGeneral.getColumnData(dataRow, "OfficePhone2", string.Empty, false) as string;
                        objMaker.MakerContactInfo = contactInfo;

                        
                    }
                }

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return objMaker;
        }

        public static void DeleteMaker(int makerGUID)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@MakerGUID", makerGUID);

                arrSpFieldSeq = new string[] { "@MakerGUID" };
                objDB.ExeCommand(objCompHash, "av_Maker_Delete", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
    }
}
