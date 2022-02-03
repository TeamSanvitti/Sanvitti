using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
//using Sv.Framework.DataMembers.StateObject;

namespace avii.Classes
{
    public class StoreOperations
    {
        public static List<StoreLocation> getStoreLocationList(int CompanyID)
        {
            List<StoreLocation> stroeLocationList = new List<StoreLocation>();

            DataSet dataTable = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@CompanyName", string.Empty);
                objCompHash.Add("@CompanyAccountNumber", string.Empty);
                objCompHash.Add("@StoreID", string.Empty);
                objCompHash.Add("@ContactName", string.Empty);
                //objCompHash.Add("@email", email);
                arrSpFieldSeq = new string[] { "@CompanyID", "@CompanyName", "@CompanyAccountNumber", "@StoreID", "@ContactName" };

                dataTable = db.GetDataSet(objCompHash, "av_Company_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Tables.Count > 0 && dataTable.Tables[2].Rows.Count > 0)
                {
                    if (dataTable.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dataTable.Tables[2].Rows)
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
                            
                            stroeLocationList.Add(objStoreLocation);
                        }
                    }
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

            return stroeLocationList;
        }
        public static List<StoreLocation> GetCompanyStoreList(int companyID, int userID)
        {
            List<StoreLocation> stroeLocationList = new List<StoreLocation>();


            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq = null;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {


                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@UserID", userID);
                arrSpFieldSeq = new string[] { "@CompanyID", "@UserID" };


                dataTable = db.GetTableRecords(objCompHash, "av_CompanyStore_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {

                        StoreLocation objStoreLocation = new StoreLocation();
                        objStoreLocation.CompanyAddressID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AddressID", 0, false));

                        objStoreLocation.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;
                        objStoreLocation.StoreName = clsGeneral.getColumnData(dataRow, "CompositStoreIDStoreName", string.Empty, false) as string;
                        objStoreLocation.StoreAddress.Address1 = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                        objStoreLocation.StoreAddress.Address2 = clsGeneral.getColumnData(dataRow, "Address2", string.Empty, false) as string;
                        //AddressType addressType = (AddressType)Enum.Parse(typeof(AddressType), dataRow["AddressType"].ToString());
                        //objStoreLocation.StoreAddress.AddressType = addressType;
                        objStoreLocation.StoreAddress.City = clsGeneral.getColumnData(dataRow, "City", string.Empty, false) as string;
                        objStoreLocation.StoreAddress.State = clsGeneral.getColumnData(dataRow, "State", string.Empty, false) as string;
                        objStoreLocation.StoreAddress.Zip = clsGeneral.getColumnData(dataRow, "Zip", string.Empty, false) as string;
                        ////objStoreLocation.StoreAddress.City = clsGeneral.getColumnData(dataRow, "AddressType", string.Empty, false) as string;
                        //objStoreLocation.StoreContact.CellPhone = clsGeneral.getColumnData(dataRow, "CellPhone", string.Empty, false) as string;
                        objStoreLocation.StoreContact.ContactName = clsGeneral.getColumnData(dataRow, "ContactName", string.Empty, false) as string;
                        objStoreLocation.StoreContact.Email1 = clsGeneral.getColumnData(dataRow, "Email1", string.Empty, false) as string;
                        //objStoreLocation.StoreContact.Email2 = clsGeneral.getColumnData(dataRow, "Email2", string.Empty, false) as string;
                        //objStoreLocation.StoreContact.HomePhone = clsGeneral.getColumnData(dataRow, "HomePhone", string.Empty, false) as string;
                        objStoreLocation.StoreContact.OfficePhone1 = clsGeneral.getColumnData(dataRow, "OfficePhone1", string.Empty, false) as string;
                        //objStoreLocation.StoreContact.OfficePhone2 = clsGeneral.getColumnData(dataRow, "OfficePhone2", string.Empty, false) as string;
                        //objStoreLocation.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));

                        stroeLocationList.Add(objStoreLocation);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return stroeLocationList;
        }
        
    }
}
