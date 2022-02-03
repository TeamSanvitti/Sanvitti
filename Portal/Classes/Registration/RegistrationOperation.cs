using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace avii.Classes
{
    public class RegistrationOperation
    {
        public static string ValidateUserName(string userName)
        {
            string returnMessage = string.Empty;
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", -1);
                objCompHash.Add("@UserName", userName);
                objCompHash.Add("@type", string.Empty);
                objCompHash.Add("@userid", -1);
                objCompHash.Add("@roleguid", -1);
                objCompHash.Add("@userflag", 1);
                
                arrSpFieldSeq = new string[] { "@CompanyID", "@UserName", "@type", "@userid", "@roleguid", "@userflag"  };

                dataTable = db.GetTableRecords(objCompHash, "Aero_UserInfo_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    returnMessage = dataTable.Rows[0]["UserName"].ToString();

                    
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

            return returnMessage;
        }
        public static int CreateUser(UserRegistration userObj)
        {
            int returnValue = 0;﻿
            //string userXml = clsGeneral.SerializeObjects(userObj);

            string addressXml = clsGeneral.SerializeObject(userObj.OfficeAndShippAddress);
            
            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@UserID", userObj.UserID);
                objCompHash.Add("@Username", userObj.UserName);
                objCompHash.Add("@Pwd", userObj.Password);
                objCompHash.Add("@Email", userObj.Email);
                objCompHash.Add("@CompanyID", userObj.CompanyID);
                objCompHash.Add("@AccountStatusID", userObj.AccountStatusID);
                objCompHash.Add("@AccountNumber", userObj.CompanyAccNo);

                objCompHash.Add("@active", userObj.Active);
                objCompHash.Add("@userType", userObj.UserType);
                objCompHash.Add("@userStores", userObj.Stores);
                objCompHash.Add("@addressXML", addressXml);
                objCompHash.Add("@CompanyShortName", userObj.CompanyShortName??"");

                arrSpFieldSeq = new string[] { "@UserID", "@Username", "@Pwd", "@Email", "@CompanyID", "@AccountStatusID", "@AccountNumber", 
                    "@active", "@userType", "@userStores", "@addressXML","@CompanyShortName" };
                returnValue = db.ExecCommand(objCompHash, "av_User_InsertUpdate", arrSpFieldSeq);

            }
            catch (Exception exp)
            {
                throw exp;
            }
            return returnValue;
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
                arrSpFieldSeq = new string[] { "@CompanyID","@UserID" };
                

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
        
        public static UserRegistration GetUserInfo(int userID)
        {
            UserRegistration objUser = new UserRegistration();

            DataSet dataTable = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try                                  
            {
                objCompHash.Add("@CompanyID", -1);
                objCompHash.Add("@UserName", string.Empty);
                objCompHash.Add("@type", string.Empty);
                objCompHash.Add("@userid", userID);
                objCompHash.Add("@roleguid", -1);
                objCompHash.Add("@userflag", 1);
                
                arrSpFieldSeq = new string[] { "@CompanyID", "@UserName", "@type", "@userid", "@roleguid", "@userflag" };


                dataTable = db.GetDataSet(objCompHash, "Aero_UserInfo_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Tables.Count > 0 && dataTable.Tables[0].Rows.Count > 0)
                {
                    if (dataTable.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dataTable.Tables[0].Rows)
                        {

                            objUser.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));
                            objUser.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                            objUser.AccountStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AccountStatusID", 0, false));
                            objUser.Email = clsGeneral.getColumnData(dataRow, "Email", string.Empty, false) as string;

                            objUser.Password = clsGeneral.getColumnData(dataRow, "pwd", string.Empty, false) as string;

                            objUser.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                            objUser.UserType = clsGeneral.getColumnData(dataRow, "UserType", string.Empty, false) as string;





                            if (dataTable.Tables[1].Rows.Count > 0)
                            {
                                objUser.OfficeAndShippAddress = GetStoreLocationList(dataTable.Tables[1]);
                            }
                            if (dataTable.Tables[2].Rows.Count > 0)
                            {
                                objUser.UserStores = GetUserStoreList(dataTable.Tables[2]);
                            }


                            //CompanyList.Add(objCompany);
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

            return objUser;
        }
        public static List<StoreLocation> GetUserStoreList(DataTable dataTable)
        {
            List<StoreLocation> stroeLocationList = new List<StoreLocation>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                StoreLocation objStoreLocation = new StoreLocation();
                objStoreLocation.CompanyAddressID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AddressID", 0, false));
                objStoreLocation.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", 0, false) as string;
                objStoreLocation.StoreAddress.Address1 = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                objStoreLocation.StoreAddress.Address2 = clsGeneral.getColumnData(dataRow, "Address2", string.Empty, false) as string;
                AddressType addressType = (AddressType)Enum.Parse(typeof(AddressType), dataRow["AddressType"].ToString());
                objStoreLocation.StoreAddress.AddressType = addressType;
                //objStoreLocation.StoreAddress.AddressType = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AddressType", 0, false));
                objStoreLocation.StoreAddress.City = clsGeneral.getColumnData(dataRow, "City", string.Empty, false) as string;
                objStoreLocation.StoreAddress.State = clsGeneral.getColumnData(dataRow, "State", string.Empty, false) as string;
                objStoreLocation.StoreAddress.Zip = clsGeneral.getColumnData(dataRow, "Zip", string.Empty, false) as string;
                //objStoreLocation.StoreAddress.City = clsGeneral.getColumnData(dataRow, "AddressType", string.Empty, false) as string;
                objStoreLocation.StoreContact.CellPhone = clsGeneral.getColumnData(dataRow, "CellPhone", string.Empty, false) as string;
                objStoreLocation.StoreContact.ContactName = clsGeneral.getColumnData(dataRow, "ContactName", string.Empty, false) as string;
                objStoreLocation.StoreContact.Email1 = clsGeneral.getColumnData(dataRow, "Email1", string.Empty, false) as string;
                objStoreLocation.StoreContact.Email2 = clsGeneral.getColumnData(dataRow, "Email2", string.Empty, false) as string;
                objStoreLocation.StoreContact.HomePhone = clsGeneral.getColumnData(dataRow, "HomePhone", string.Empty, false) as string;
                objStoreLocation.StoreContact.OfficePhone1 = clsGeneral.getColumnData(dataRow, "OfficePhone1", string.Empty, false) as string;
                objStoreLocation.StoreContact.OfficePhone2 = clsGeneral.getColumnData(dataRow, "OfficePhone2", string.Empty, false) as string;
                objStoreLocation.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));

                stroeLocationList.Add(objStoreLocation);
            }
            return stroeLocationList;
        }
        public static List<StoreLocation> GetStoreLocationList(DataTable dataTable)
        {
            List<StoreLocation> stroeLocationList = new List<StoreLocation>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                StoreLocation objStoreLocation = new StoreLocation();
                objStoreLocation.CompanyAddressID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AddressID", 0, false));
                objStoreLocation.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", 0, false) as string;
                objStoreLocation.StoreAddress.Address1 = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                objStoreLocation.StoreAddress.Address2 = clsGeneral.getColumnData(dataRow, "Address2", string.Empty, false) as string;
                AddressType addressType = (AddressType)Enum.Parse(typeof(AddressType), dataRow["AddressType"].ToString());
                objStoreLocation.StoreAddress.AddressType = addressType;
                //objStoreLocation.StoreAddress.AddressType = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AddressType", 0, false));
                objStoreLocation.StoreAddress.City = clsGeneral.getColumnData(dataRow, "City", string.Empty, false) as string;
                objStoreLocation.StoreAddress.State = clsGeneral.getColumnData(dataRow, "State", string.Empty, false) as string;
                objStoreLocation.StoreAddress.Zip = clsGeneral.getColumnData(dataRow, "Zip", string.Empty, false) as string;
                //objStoreLocation.StoreAddress.City = clsGeneral.getColumnData(dataRow, "AddressType", string.Empty, false) as string;
                objStoreLocation.StoreContact.CellPhone = clsGeneral.getColumnData(dataRow, "CellPhone", string.Empty, false) as string;
                objStoreLocation.StoreContact.ContactName = clsGeneral.getColumnData(dataRow, "ContactName", string.Empty, false) as string;
                objStoreLocation.StoreContact.Email1 = clsGeneral.getColumnData(dataRow, "Email1", string.Empty, false) as string;
                objStoreLocation.StoreContact.Email2 = clsGeneral.getColumnData(dataRow, "Email2", string.Empty, false) as string;
                objStoreLocation.StoreContact.HomePhone = clsGeneral.getColumnData(dataRow, "HomePhone", string.Empty, false) as string;
                objStoreLocation.StoreContact.OfficePhone1 = clsGeneral.getColumnData(dataRow, "OfficePhone1", string.Empty, false) as string;
                objStoreLocation.StoreContact.OfficePhone2 = clsGeneral.getColumnData(dataRow, "OfficePhone2", string.Empty, false) as string;
                objStoreLocation.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));

                stroeLocationList.Add(objStoreLocation);
            }
            return stroeLocationList;
        }
        public static List<UserRegistration> GetUserInfoList(int CompanyID, string userType, string sortBy, int statusID)
        {
            List<UserRegistration> objUserList = new List<UserRegistration>();


            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@UserName", string.Empty);
                objCompHash.Add("@type", userType);
                objCompHash.Add("@userid", -1);
                objCompHash.Add("@roleguid", -1);
                objCompHash.Add("@userflag", -1);
                objCompHash.Add("@StatusID", statusID);


                arrSpFieldSeq = new string[] { "@CompanyID", "@UserName", "@type", "@userid", "@roleguid", "@userflag", "@StatusID" };

                dataTable = db.GetTableRecords(objCompHash, "Aero_UserInfo_select", arrSpFieldSeq);


                //if (dataTable != null && dataTable.Tables.Count > 0 && dataTable.Tables[0].Rows.Count > 0)
                {
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            UserRegistration objUser = new UserRegistration();
                            objUser.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));
                            objUser.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                            objUser.AccountStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AccountStatusID", 0, false));
                            objUser.Email = clsGeneral.getColumnData(dataRow, "Email", string.Empty, false) as string;
                            objUser.CompanyName = clsGeneral.getColumnData(dataRow, "companyname", string.Empty, false) as string;
                            objUser.Password = clsGeneral.getColumnData(dataRow, "pwd", string.Empty, false) as string;
                            objUser.AccStatus = clsGeneral.getColumnData(dataRow, "AccStatus", string.Empty, false) as string;
                            objUser.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                            objUser.UserType = clsGeneral.getColumnData(dataRow, "UserType", string.Empty, false) as string;

                            objUserList.Add(objUser);
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

            if (sortBy != string.Empty)
            {
                GenericComparer<UserRegistration> cmp = new GenericComparer<UserRegistration>(sortBy);
                objUserList.Sort(cmp);
            }

            return objUserList;
        }

        public static void DeleteUser(int userID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                arrSpFieldSeq = new string[] { "@UserID" };
                objCompHash.Add("@UserID", userID);
                db.ExeCommand(objCompHash, "Av_User_Delete", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

    }
}