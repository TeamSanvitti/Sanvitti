using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Customer;
using SV.Framework.Models.Service;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;


namespace SV.Framework.DAL.Admin
{
    public class CustomerOperation : BaseCreateInstance
    {
        public  CompanyInfo GetCompanyInfo(int userID)
        {
            CompanyInfo objCompany = default;// new CompanyInfo();
            using (DBConnect db = new DBConnect())
            {
                DataSet ds = default;//
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", 0);
                    objCompHash.Add("@CompanyName", string.Empty);
                    objCompHash.Add("@CompanyAccountNumber", string.Empty);
                    objCompHash.Add("@StoreID", string.Empty);
                    objCompHash.Add("@ContactName", string.Empty);
                    objCompHash.Add("@CompanyAccountStatus", 0);
                    objCompHash.Add("@UserID", userID);
                    arrSpFieldSeq = new string[] { "@CompanyID", "@CompanyName", "@CompanyAccountNumber", "@StoreID", "@ContactName", "@CompanyAccountStatus", "@UserID" };

                    ds = db.GetDataSet(objCompHash, "av_Company_select", arrSpFieldSeq);

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        objCompany = new CompanyInfo();
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            foreach (DataRow dataRow in ds.Tables[0].Rows)
                            {
                                //objCompany.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                                objCompany.BussinessType = clsGeneral.getColumnData(dataRow, "BussinessType", string.Empty, false) as string;
                                objCompany.Comment = clsGeneral.getColumnData(dataRow, "Comment", string.Empty, false) as string;
                                objCompany.CompanyAccountNumber = clsGeneral.getColumnData(dataRow, "CompanyAccountNumber", string.Empty, false) as string;
                                //objCompany.CompanyAccountStatus = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyAccountStatus", 0, false));
                                objCompany.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                                objCompany.CompanyShortName = clsGeneral.getColumnData(dataRow, "CompanyShortName", string.Empty, false) as string;
                                objCompany.Email = clsGeneral.getColumnData(dataRow, "Email", string.Empty, false) as string;
                                objCompany.GroupEmail = clsGeneral.getColumnData(dataRow, "GroupEmail", string.Empty, false) as string;
                                objCompany.Website = clsGeneral.getColumnData(dataRow, "Website", string.Empty, false) as string;
                                //objCompany.CompanySType = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyType", 0, false));
                                ///objCompany.Active = (bool)clsGeneral.getColumnData(dataRow, "Active", true, false);
                                if (userID > 0)
                                {
                                    if (ds.Tables[1].Rows.Count > 0)
                                    {
                                        objCompany.CompanyAddresses = GetCompanyAddresses(ds.Tables[1]);
                                    }
                                    if (ds.Tables[2].Rows.Count > 0)
                                    {
                                        objCompany.Stores = GetCompanyStoreList(ds.Tables[2]);
                                    }

                                }

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
                    //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return objCompany;
        }

        public  CompanyInformation AssignedUsersDB(int userID)
        {
            CompanyInformation companyInfo = default;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                List<Users> comapnyUserList = default;// new List<Users>();
                DataSet ds = new DataSet();
                Hashtable objCompHash = new Hashtable();
                StringBuilder stringBuilder = new StringBuilder();
                try
                {
                    objCompHash.Add("@UserID", userID);
                    arrSpFieldSeq = new string[] { "@UserID" };
                    ds = db.GetDataSet(objCompHash, "av_AssignedUsers_Select", arrSpFieldSeq);

                    Users userInfo = default;
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            companyInfo = new CompanyInformation();
                            comapnyUserList = new List<Users>();
                            companyInfo.CompanyAccountNumber = (string)clsGeneral.getColumnData(dataRow, "CompanyAccountNumber", string.Empty, false);
                            companyInfo.CompanyName = (string)clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false);
                            companyInfo.CompanyShortName = (string)clsGeneral.getColumnData(dataRow, "CompanyShortName", string.Empty, false);
                            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                            {
                                foreach (DataRow dataRowItem in ds.Tables[1].Rows)
                                {
                                    userInfo = new Users();
                                    userInfo.Email = (string)clsGeneral.getColumnData(dataRowItem, "email", string.Empty, false);
                                    userInfo.UserName = (string)clsGeneral.getColumnData(dataRowItem, "UserName", string.Empty, false);
                                    userInfo.Status = (string)clsGeneral.getColumnData(dataRowItem, "AccStatus", string.Empty, false);
                                    userInfo.RolesAssigned = (string)clsGeneral.getColumnData(dataRowItem, "RoleAssigned", string.Empty, false);
                                    comapnyUserList.Add(userInfo);
                                }
                                companyInfo.Users = comapnyUserList;
                            }
                        }
                    }
                }
                catch (Exception objExp)
                {
                    throw objExp;
                }
                finally
                {
                    db.DBClose();
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return companyInfo;
        }

        private  List<CompanyStore> GetCompanyStoreList(DataTable dataTable)
        {
            List<CompanyStore> stroeLocationList = default; ; new List<CompanyStore>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                stroeLocationList = new List<CompanyStore>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    CompanyStore objStoreLocation = new CompanyStore();
                    //objStoreLocation.CompanyAddressID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AddressID", 0, false));
                    objStoreLocation.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;
                    objStoreLocation.StoreName = clsGeneral.getColumnData(dataRow, "StoreName", string.Empty, false) as string;
                    objStoreLocation.StoreAddress.Address1 = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                    objStoreLocation.StoreAddress.Address2 = clsGeneral.getColumnData(dataRow, "Address2", string.Empty, false) as string;
                    //AddressType addressType = (AddressType)Enum.Parse(typeof(AddressType), dataRow["AddressType"].ToString());
                    //if (addrType == 1)
                    //    objStoreLocation.StoreAddress.AddressType = AddressType.Office1;
                    //if (addrType == 4)
                    //    objStoreLocation.StoreAddress.AddressType = AddressType.Shipping;
                    //if (addrType == 8)
                    //    
                    objStoreLocation.StoreAddress.AddressType = AddressType.Store;

                    //objStoreLocation.StoreAddress.AddressType = addressType;
                    objStoreLocation.StoreAddress.City = clsGeneral.getColumnData(dataRow, "City", string.Empty, false) as string;
                    objStoreLocation.StoreAddress.State = clsGeneral.getColumnData(dataRow, "State", string.Empty, false) as string;
                    objStoreLocation.StoreAddress.Zip = clsGeneral.getColumnData(dataRow, "Zip", string.Empty, false) as string;
                    objStoreLocation.StoreAddress.Country = clsGeneral.getColumnData(dataRow, "country", string.Empty, false) as string;
                    //objStoreLocation.StoreAddress.City = clsGeneral.getColumnData(dataRow, "AddressType", string.Empty, false) as string;
                    objStoreLocation.StoreContact.CellPhone = clsGeneral.getColumnData(dataRow, "CellPhone", string.Empty, false) as string;
                    objStoreLocation.StoreContact.ContactName = clsGeneral.getColumnData(dataRow, "ContactName", string.Empty, false) as string;
                    objStoreLocation.StoreContact.Email1 = clsGeneral.getColumnData(dataRow, "Email1", string.Empty, false) as string;
                    objStoreLocation.StoreContact.Email2 = clsGeneral.getColumnData(dataRow, "Email2", string.Empty, false) as string;
                    objStoreLocation.StoreContact.HomePhone = clsGeneral.getColumnData(dataRow, "HomePhone", string.Empty, false) as string;
                    objStoreLocation.StoreContact.OfficePhone1 = clsGeneral.getColumnData(dataRow, "OfficePhone1", string.Empty, false) as string;
                    objStoreLocation.StoreContact.OfficePhone2 = clsGeneral.getColumnData(dataRow, "OfficePhone2", string.Empty, false) as string;
                    //objStoreLocation.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                    //objStoreLocation.StoreFlag = clsGeneral.getColumnData(dataRow, "StoreFlag", string.Empty, false) as string;

                    stroeLocationList.Add(objStoreLocation);
                }
            }
            return stroeLocationList;
        }
        private  List<CompanyAddresses> GetCompanyAddresses(DataTable dataTable)
        {
            List<CompanyAddresses> companyAddressList = default;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                companyAddressList = new List<CompanyAddresses>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    CompanyAddresses objCompanyAddress = new CompanyAddresses();
                    Address CompanyAddress = new Address();
                    ContactInfo CompanyContactInfo = new ContactInfo();
                    int addrType = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AddressType", 0, false));

                    CompanyAddress.Address1 = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                    CompanyAddress.Address2 = clsGeneral.getColumnData(dataRow, "Address2", string.Empty, false) as string;
                    //AddressType addressType = (AddressType)Enum.Parse(typeof(AddressType), dataRow["AddressType"].ToString());

                    if (addrType == 1)
                        CompanyAddress.AddressType = AddressType.Office1;
                    if (addrType == 4)
                        CompanyAddress.AddressType = AddressType.Shipping;
                    //if (addrType == 8)
                    //    CompanyAddress.AddressType = AddressType.Store;

                    CompanyAddress.City = clsGeneral.getColumnData(dataRow, "City", string.Empty, false) as string;
                    CompanyAddress.State = clsGeneral.getColumnData(dataRow, "State", string.Empty, false) as string;
                    CompanyAddress.Zip = clsGeneral.getColumnData(dataRow, "Zip", string.Empty, false) as string;
                    CompanyAddress.Country = clsGeneral.getColumnData(dataRow, "country", string.Empty, false) as string;
                    CompanyContactInfo.CellPhone = clsGeneral.getColumnData(dataRow, "CellPhone", string.Empty, false) as string;
                    CompanyContactInfo.ContactName = clsGeneral.getColumnData(dataRow, "ContactName", string.Empty, false) as string;
                    CompanyContactInfo.Email1 = clsGeneral.getColumnData(dataRow, "Email1", string.Empty, false) as string;
                    CompanyContactInfo.Email2 = clsGeneral.getColumnData(dataRow, "Email2", string.Empty, false) as string;
                    CompanyContactInfo.HomePhone = clsGeneral.getColumnData(dataRow, "HomePhone", string.Empty, false) as string;
                    CompanyContactInfo.OfficePhone1 = clsGeneral.getColumnData(dataRow, "OfficePhone1", string.Empty, false) as string;
                    CompanyContactInfo.OfficePhone2 = clsGeneral.getColumnData(dataRow, "OfficePhone2", string.Empty, false) as string;
                    objCompanyAddress.CompanyAddress = CompanyAddress;
                    objCompanyAddress.CompanyContactInfo = CompanyContactInfo;
                    companyAddressList.Add(objCompanyAddress);
                }
            }
            return companyAddressList;
        }

    }
}
