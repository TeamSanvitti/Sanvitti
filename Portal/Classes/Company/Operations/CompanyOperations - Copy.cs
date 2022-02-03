using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
////using Sv.Framework.DataMembers.Enums;
//using Sv.Framework.DataMembers.StateObject;
//using Sv.Framework.Utility.Operations;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

namespace avii.Classes
{
    public class CompanyOperations
    {
        private static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }
        public static string SerializeObject<T>(T obj)
        {
            try
            {
                string xmlString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, obj);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                xmlString = UTF8ByteArrayToString(memoryStream.ToArray()); return xmlString.Trim();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
        public static void CreateCompany(Company CompanyObj)
        {
            string StoreXml = SerializeObject(CompanyObj.Stores);
            string salesPersonXML = SerializeObject(CompanyObj.AssingedSalesPerson);
      
            //DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                //if (!string.IsNullOrEmpty(userName))
                {
                    objCompHash.Add("@CompanyID", CompanyObj.CompanyID);
                    objCompHash.Add("@BussinessType", CompanyObj.BussinessType);
                    objCompHash.Add("@Comment", CompanyObj.Comment);
                    objCompHash.Add("@CompanyAccountNumber", CompanyObj.CompanyAccountNumber);
                    objCompHash.Add("@CompanyAccountStatus", CompanyObj.CompanyAccountStatus);
                    objCompHash.Add("@CompanyName", CompanyObj.CompanyName);
                    objCompHash.Add("@CompanyShortName", CompanyObj.CompanyShortName);
                    objCompHash.Add("@CompanySType", CompanyObj.CompanySType);
                    objCompHash.Add("@Email", CompanyObj.Email);
                    objCompHash.Add("@GroupEmail", CompanyObj.GroupEmail);
                    objCompHash.Add("@Website", CompanyObj.Website);
                    objCompHash.Add("@StoreXML", StoreXml);
                    objCompHash.Add("@salesPersonXML", salesPersonXML);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@BussinessType", "@Comment", "@CompanyAccountNumber", "@CompanyAccountStatus", 
                        "@CompanyName", "@CompanyShortName", "@CompanySType", "@Email", "@GroupEmail","@Website","@StoreXML","@salesPersonXML" };
                    db.GetTableRecords(objCompHash, "av_Company_InsertUpdate", arrSpFieldSeq);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    userid = Convert.ToInt32(dt.Rows[0]["userguid"]);
                    //}
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            //return userid;
        }

        public static List<StoreLocation> getStoreLocationList(DataTable dataTable)
        { 
            List<StoreLocation> stroeLocationList = new List<StoreLocation>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                StoreLocation objStoreLocation = new StoreLocation();
                objStoreLocation.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;
                objStoreLocation.StoreName = clsGeneral.getColumnData(dataRow, "StoreName", string.Empty, false) as string;
                objStoreLocation.StoreAddress.Address1 = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                objStoreLocation.StoreAddress.Address2 = clsGeneral.getColumnData(dataRow, "Address2", string.Empty, false) as string;
                AddressType addressType = (AddressType)Enum.Parse(typeof(AddressType), dataRow["AddressType"].ToString());
                objStoreLocation.StoreAddress.AddressType = addressType;
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
        public static List<SalesPerson> getSalesPersonList(DataTable dataTable)
        {
            List<SalesPerson> SalesPersonList = new List<SalesPerson>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                SalesPerson objSalesPerson = new SalesPerson();
                objSalesPerson.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));
                objSalesPerson.EmployeeNumber = clsGeneral.getColumnData(dataRow, "username", string.Empty, false) as string;

                SalesPersonList.Add(objSalesPerson);
            }
            return SalesPersonList;
        }
        public static Company getCompanyInfo(int CompanyID, string CompanyName, string StoreID)
        {
            Company objCompany = new Company();

            DataSet dataTable = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@CompanyName", CompanyName);
                objCompHash.Add("@CompanyAccountNumber", string.Empty);
                objCompHash.Add("@StoreID", StoreID);
                objCompHash.Add("@ContactName", string.Empty);
                //objCompHash.Add("@email", email);
                arrSpFieldSeq = new string[] { "@CompanyID", "@CompanyName", "@CompanyAccountNumber", "@StoreID", "@ContactName" };

                dataTable = db.GetDataSet(objCompHash, "av_Company_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Tables.Count > 0 && dataTable.Tables[0].Rows.Count > 0)
                {
                    if (dataTable.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dataTable.Tables[0].Rows)
                        {                            
                            objCompany.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                            objCompany.BussinessType = clsGeneral.getColumnData(dataRow, "BussinessType", string.Empty, false) as string;
                            objCompany.Comment = clsGeneral.getColumnData(dataRow, "Comment", string.Empty, false) as string;
                            objCompany.CompanyAccountNumber = clsGeneral.getColumnData(dataRow, "CompanyAccountNumber", string.Empty, false) as string;
                            objCompany.CompanyAccountStatus = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyAccountStatus", 0, false));
                            objCompany.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                            objCompany.CompanyShortName = clsGeneral.getColumnData(dataRow, "CompanyShortName", string.Empty, false) as string;
                            objCompany.Email = clsGeneral.getColumnData(dataRow, "Email", string.Empty, false) as string;
                            objCompany.GroupEmail = clsGeneral.getColumnData(dataRow, "GroupEmail", string.Empty, false) as string;
                            objCompany.Website = clsGeneral.getColumnData(dataRow, "Website", string.Empty, false) as string;
                            objCompany.CompanySType = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyType", 0, false));
                            objCompany.Active = (bool)clsGeneral.getColumnData(dataRow, "Active", true, false);
                            if (CompanyID > 0 && StoreID==string.Empty)
                            {
                                if (dataTable.Tables[1].Rows.Count > 0)
                                {
                                    objCompany.officeAndShippAddress = getStoreLocationList(dataTable.Tables[1]);
                                }

                                if (dataTable.Tables[2].Rows.Count > 0)
                                {
                                    objCompany.Stores = getStoreLocationList(dataTable.Tables[2]);
                                }
                                
                                if (dataTable.Tables[3].Rows.Count > 0)
                                {
                                    objCompany.AssingedSalesPerson = getSalesPersonList(dataTable.Tables[3]);
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
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return objCompany;
        }
        public static List<Company> getCompanyList(int CompanyID, string CompanyName, string CompanyAccountNumber, string StoreID, string ContactName)
        {
            List<Company> CompanyList = new List<Company>();

            DataSet dataTable = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@CompanyName", CompanyName);
                objCompHash.Add("@CompanyAccountNumber", CompanyAccountNumber);
                objCompHash.Add("@StoreID", StoreID);
                objCompHash.Add("@ContactName", ContactName);
                //objCompHash.Add("@email", string.Empty);
                arrSpFieldSeq = new string[] { "@CompanyID", "@CompanyName", "@CompanyAccountNumber", "@StoreID", "@ContactName" };

                dataTable = db.GetDataSet(objCompHash, "av_Company_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Tables.Count>0 && dataTable.Tables[0].Rows.Count > 0)
                {
                    if (dataTable.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dataTable.Tables[0].Rows)
                        {
                            Company objCompany = new Company();
                            objCompany.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                            objCompany.BussinessType = clsGeneral.getColumnData(dataRow, "BussinessType", string.Empty, false) as string;
                            objCompany.Comment = clsGeneral.getColumnData(dataRow, "Comment", string.Empty, false) as string;
                            objCompany.CompanyAccountNumber = clsGeneral.getColumnData(dataRow, "CompanyAccountNumber", string.Empty, false) as string;
                            objCompany.CompanyAccountStatus = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyAccountStatus", 0, false));
                            objCompany.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                            objCompany.CompanyShortName = clsGeneral.getColumnData(dataRow, "CompanyShortName", string.Empty, false) as string;
                            objCompany.Email = clsGeneral.getColumnData(dataRow, "Email", string.Empty, false) as string;
                            objCompany.GroupEmail = clsGeneral.getColumnData(dataRow, "GroupEmail", string.Empty, false) as string;
                            objCompany.Website = clsGeneral.getColumnData(dataRow, "Website", string.Empty, false) as string;
                            objCompany.CompanySType = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyType", 0, false));
                            objCompany.Active = (bool)clsGeneral.getColumnData(dataRow, "Active", true, false);
                            
                            //if (dataTable.Tables[1].Rows.Count > 0)
                            //{
                            //    objCompany.officeAndShippAddress = getStoreLocationList(dataTable.Tables[1]);
                            //}
                            //if (dataTable.Tables[2].Rows.Count > 0)
                            //{
                            //    objCompany.Stores = getStoreLocationList(dataTable.Tables[2]);
                            //}
                            //if (dataTable.Tables[3].Rows.Count > 0)
                            //{
                            //    objCompany.AssingedSalesPerson = getSalesPersonList(dataTable.Tables[3]);
                            //}
                            CompanyList.Add(objCompany);
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

            return CompanyList;
        }

        public static void DeleteCompanyAndStore(int comapmyID, int CompanyAddressID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                arrSpFieldSeq = new string[] { "@CompanyID", "@CompanyAddressID" };

                objCompHash.Add("@CompanyID", comapmyID);
                objCompHash.Add("@CompanyAddressID", CompanyAddressID);

                db.ExeCommand(objCompHash, "av_company_delete", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }

        }
        public static string checkForDuplicateCompanyNemail(string companyName, string email, int companyID)
        {
            string returnMsg = string.Empty;
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyName", companyName);
                objCompHash.Add("@email", email);
                objCompHash.Add("@companyID", companyID);

                arrSpFieldSeq = new string[] { "@CompanyName", "@email", "@companyID" };

                dataTable = db.GetTableRecords(objCompHash, "av_companyNemail_select", arrSpFieldSeq);
                if (dataTable.Rows.Count > 0)
                    returnMsg = dataTable.Rows[0]["returnMsg"].ToString();
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

            return returnMsg;
        }
    }
}
