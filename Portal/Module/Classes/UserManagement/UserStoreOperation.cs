using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
namespace avii.Classes
{
	public class UserStoreOperation
	{
        //public static DataTable CompanyUserStores(int companyID, int userID)
        //{
        //    DBConnect db = new DBConnect();
        //    string[] arrSpFieldSeq;
        //    Hashtable objParams = new Hashtable();
        //    DataTable dataTable = new DataTable();
        //    try
        //    {
        //        objParams.Add("@CompanyID", companyID);
        //        objParams.Add("@userID", userID);
        //        arrSpFieldSeq =
        //        new string[] { "@CompanyID", "@userID" };
        //        dataTable = db.GetTableRecords(objParams, "av_companyStore_select", arrSpFieldSeq);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    finally
        //    {
        //        db.DBClose();
        //        objParams = null;
        //        arrSpFieldSeq = null;
        //    }
        //    return dataTable;
        //}

        private static List<StoreLocation> PopulateStores(DataTable dataTable)
        {
            List<StoreLocation> storeLocationList = new List<StoreLocation>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                StoreLocation objStoreLocation = new StoreLocation();
                objStoreLocation.CompanyAddressID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AddressID", 0, false));
                objStoreLocation.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                objStoreLocation.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;
                objStoreLocation.StoreName = clsGeneral.getColumnData(dataRow, "StoreName", string.Empty, false) as string;
                objStoreLocation.StoreAddress.Address1 = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                objStoreLocation.StoreAddress.Address2 = clsGeneral.getColumnData(dataRow, "Address2", string.Empty, false) as string;
                AddressType addressType = (AddressType)Enum.Parse(typeof(AddressType), dataRow["AddressType"].ToString());
                objStoreLocation.StoreAddress.AddressType = addressType;
                objStoreLocation.StoreAddress.City = clsGeneral.getColumnData(dataRow, "City", string.Empty, false) as string;
                objStoreLocation.StoreAddress.State = clsGeneral.getColumnData(dataRow, "State", string.Empty, false) as string;
                objStoreLocation.StoreAddress.Zip = clsGeneral.getColumnData(dataRow, "Zip", string.Empty, false) as string;
                objStoreLocation.StoreAddress.Country = clsGeneral.getColumnData(dataRow, "country", string.Empty, false) as string;
                objStoreLocation.StoreContact.CellPhone = clsGeneral.getColumnData(dataRow, "CellPhone", string.Empty, false) as string;
                objStoreLocation.StoreContact.ContactName = clsGeneral.getColumnData(dataRow, "ContactName", string.Empty, false) as string;
                objStoreLocation.StoreContact.Email1 = clsGeneral.getColumnData(dataRow, "Email1", string.Empty, false) as string;
                objStoreLocation.StoreContact.Email2 = clsGeneral.getColumnData(dataRow, "Email2", string.Empty, false) as string;
                objStoreLocation.StoreContact.HomePhone = clsGeneral.getColumnData(dataRow, "HomePhone", string.Empty, false) as string;
                objStoreLocation.StoreContact.OfficePhone1 = clsGeneral.getColumnData(dataRow, "OfficePhone1", string.Empty, false) as string;
                objStoreLocation.StoreContact.OfficePhone2 = clsGeneral.getColumnData(dataRow, "OfficePhone2", string.Empty, false) as string;
                objStoreLocation.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                objStoreLocation.StoreFlag = clsGeneral.getColumnData(dataRow, "StoreFlag", string.Empty, false) as string;
                objStoreLocation.CompositeKeyStoreIdStoreName = clsGeneral.getColumnData(dataRow, "CompositStoreIDStoreName", string.Empty, false) as string;
                objStoreLocation.UserStoreFlag = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "userID", 0, false));


                storeLocationList.Add(objStoreLocation);


            }
            return storeLocationList;
        }

        public static List<StoreLocation> GetStoreLocationList(int CompanyID, int userID)
        {
            List<StoreLocation> storeLocationList = new List<StoreLocation>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@UserID", userID);
                arrSpFieldSeq = new string[] { "@CompanyID", "@UserID" };


                dataTable = db.GetTableRecords(objCompHash, "av_CompanyStore_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    storeLocationList = PopulateStores(dataTable);
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

            return storeLocationList;
        }
        public static List<StoreLocation> GetUserStoreLocationList(int CompanyID, int userID)
        {
            List<StoreLocation> storeLocationList = new List<StoreLocation>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@UserID", userID);
                arrSpFieldSeq = new string[] { "@CompanyID", "@UserID" };

                if (CompanyID > 0)
                    dataTable = db.GetTableRecords(objCompHash, "av_CompanyStore_Select", arrSpFieldSeq);
                else
                    dataTable = db.GetTableRecords(objCompHash, "av_CompanyUserStore_Select", arrSpFieldSeq);
                    

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    storeLocationList = PopulateStores(dataTable);
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

            return storeLocationList;
        }

	}
}