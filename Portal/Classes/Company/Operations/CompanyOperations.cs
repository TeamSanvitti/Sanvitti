using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
////using Sv.Framework.DataMembers.Enums;
//using Sv.Framework.DataMembers.StateObject;
//using Sv.Framework.Utility.Operations;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
{
    public class CompanyOperations
    {
        public static bool ValidateShipStationConnectionString(string APIAddress, string authString, out string errorMessage)
        {
            errorMessage = "";
            bool IsValid = true;
            

            if(string.IsNullOrEmpty(authString))
            {
                IsValid = false;
                errorMessage = "ShipStation authentication string is missing!";

            }
            if (string.IsNullOrEmpty(APIAddress))
            {
                errorMessage = "ShipStation API address is missing!";
                IsValid = false;
            }            
            return IsValid;
        }

        public static bool ValidateEndiciaConnectionString(string connectionString, out string errorMessage)
        {
            errorMessage = "";

            bool IsValid = false;
            bool IsAccountID = false, IsRequesterID = false, IsPassPhrase = false;
            //connectionString = connectionString;

            string AccountID = "accountid", RequesterID = "requesterid", PassPhrase = "passphrase";

            string[] array = connectionString.Split(',');
            //if (array.Length == 3)
            {
                if (array.Length > 0)
                {
                    if (!string.IsNullOrWhiteSpace(array[0]))
                    {
                        if (array[0].ToLower().Contains(AccountID))
                        {
                            string[] array1 = array[0].Split('=');
                            if (array1.Length == 2 && array1[0].ToLower() == AccountID && !string.IsNullOrWhiteSpace(array1[1]))
                            {
                                IsAccountID = true;
                            }
                            else
                            {
                                if (array1.Length == 2)
                                {
                                    if (array1[0].ToLower() != AccountID)
                                        errorMessage = "Endicia " + AccountID + " is not correct!";
                                    if (string.IsNullOrWhiteSpace(array1[1]))
                                        errorMessage = "Endicia " + AccountID + " value cannot be empty!";
                                }
                            }
                        }
                        if (array[0].ToLower().Contains(RequesterID))
                        {
                            string[] array1 = array[0].Split('=');
                            if (array1.Length == 2 && array1[0].ToLower() == RequesterID && !string.IsNullOrWhiteSpace(array1[1]))
                            {
                                IsRequesterID = true;
                            }
                            else
                            {
                                if (array1.Length == 2)
                                {
                                    if (array1[0].ToLower() != RequesterID)
                                        errorMessage = "Endicia " + RequesterID + " is not correct!";
                                    if (string.IsNullOrWhiteSpace(array1[1]))
                                        errorMessage = "Endicia " + RequesterID + " value cannot be empty!";
                                }
                            }
                        }
                        if (array[0].ToLower().Contains(PassPhrase))
                        {
                            string[] array1 = array[0].Split('=');
                            if (array1.Length == 2 && array1[0].ToLower() == PassPhrase && !string.IsNullOrWhiteSpace(array1[1]))
                            {
                                IsPassPhrase = true;
                            }
                            else
                            {
                                if (array1.Length == 2)
                                {
                                    if (array1[0].ToLower() != PassPhrase)
                                        errorMessage = "Endicia " + PassPhrase + " is not correct!";
                                    if (string.IsNullOrWhiteSpace(array1[1]))
                                        errorMessage = "Endicia " + PassPhrase + " value cannot be empty!";
                                }
                            }
                        }
                    }
                    else
                    {
                        errorMessage = "Endicia connectionstring cannot be empty!";
                    }
                }
                if (array.Length > 1)
                {
                    if (!string.IsNullOrWhiteSpace(array[1]))
                    {
                        if (array[1].ToLower().Contains(AccountID))
                        {
                            string[] array1 = array[1].Split('=');
                            if (array1.Length == 2 && array1[0].ToLower() == AccountID && !string.IsNullOrWhiteSpace(array1[1]))
                            {
                                IsAccountID = true;
                            }
                            else
                            {
                                if (array1.Length == 2)
                                {
                                    if (array1[0].ToLower() != AccountID)
                                        errorMessage = "Endicia " + AccountID + " is not correct!";
                                    if (string.IsNullOrWhiteSpace(array1[1]))
                                        errorMessage = "Endicia " + AccountID + " value cannot be empty!";
                                }
                            }
                        }
                        if (array[1].ToLower().Contains(RequesterID))
                        {
                            string[] array1 = array[1].Split('=');
                            if (array1.Length == 2 && array1[0].ToLower() == RequesterID && !string.IsNullOrWhiteSpace(array1[1]))
                            {
                                IsRequesterID = true;
                            }
                            else
                            {
                                if (array1.Length == 2)
                                {
                                    if (array1[0].ToLower() != RequesterID)
                                        errorMessage = "Endicia " + RequesterID + " is not correct!";
                                    if (string.IsNullOrWhiteSpace(array1[1]))
                                        errorMessage = "Endicia " + RequesterID + " value cannot be empty!";
                                }
                            }

                        }
                        if (array[1].ToLower().Contains(PassPhrase))
                        {
                            string[] array1 = array[1].Split('=');
                            if (array1.Length == 2 && array1[0].ToLower() == PassPhrase && !string.IsNullOrWhiteSpace(array1[1]))
                            {
                                IsPassPhrase = true;
                            }
                            else
                            {
                                if (array1.Length == 2)
                                {
                                    if (array1[0].ToLower() != PassPhrase)
                                        errorMessage = "Endicia " + PassPhrase + " is not correct!";
                                    if (string.IsNullOrWhiteSpace(array1[1]))
                                        errorMessage = "Endicia " + PassPhrase + " value cannot be empty!";
                                }
                            }
                        }
                    }
                    else
                    {
                        errorMessage = "Endicia connectionstring cannot be empty!";
                    }
                }
                if (array.Length > 2)
                {
                    if (!string.IsNullOrWhiteSpace(array[2]))
                    {
                        if (array[2].ToLower().Contains(AccountID))
                        {
                            string[] array1 = array[2].Split('=');
                            if (array1.Length == 2 && array1[0].ToLower() == AccountID && !string.IsNullOrWhiteSpace(array1[1]))
                            {
                                IsAccountID = true;
                            }
                            else
                            {
                                if (array1.Length == 2)
                                {
                                    if (array1[0].ToLower() != AccountID)
                                        errorMessage = "Endicia " + AccountID + " is not correct!";
                                    if (string.IsNullOrWhiteSpace(array1[1]))
                                        errorMessage = "Endicia " + AccountID + " value cannot be empty!";
                                }
                            }

                        }
                        if (array[2].ToLower().Contains(RequesterID))
                        {
                            string[] array1 = array[2].Split('=');
                            if (array1.Length == 2 && array1[0].ToLower() == RequesterID && !string.IsNullOrWhiteSpace(array1[1]))
                            {
                                IsRequesterID = true;
                            }
                            else
                            {
                                if (array1.Length == 2)
                                {
                                    if (array1[0].ToLower() != RequesterID)
                                        errorMessage = "Endicia " + RequesterID + " is not correct!";
                                    if (string.IsNullOrWhiteSpace(array1[1]))
                                        errorMessage = "Endicia " + RequesterID + " value cannot be empty!";
                                }
                            }
                        }
                        if (array[2].ToLower().Contains(PassPhrase))
                        {
                            string[] array1 = array[2].Split('=');
                            if (array1.Length == 2 && array1[0].ToLower() == PassPhrase && !string.IsNullOrWhiteSpace(array1[1]))
                            {
                                IsPassPhrase = true;
                            }
                            else
                            {
                                if (array1.Length == 2)
                                {
                                    if (array1[0].ToLower() != PassPhrase)
                                        errorMessage = "Endicia " + PassPhrase + " is not correct!";
                                    if (string.IsNullOrWhiteSpace(array1[1]))
                                        errorMessage = "Endicia " + PassPhrase + " value cannot be empty!";
                                }
                            }
                        }

                    }
                    else
                    {
                        errorMessage = "Endicia connectionstring cannot be empty!";
                    }
                }
            }

            if(!IsAccountID)
                errorMessage = "Endicia " + AccountID + " information is missing!";
            if (!IsRequesterID)
                errorMessage = "Endicia " + RequesterID + " information is missing!";
            if (!IsPassPhrase)
                errorMessage = "Endicia " + PassPhrase + " information is missing!";

            if (IsAccountID && IsRequesterID && IsPassPhrase)
                IsValid = true;

            return IsValid;
        }
        public static UsersResponse GetAssignedUsers(UsersRequest userRequest)
        {

            UsersResponse serviceResponse = new UsersResponse();
            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
            string requestXML = clsGeneral.SerializeObject(userRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetAssignedUsers";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            try
            {

                if (userRequest != null)
                {

                    int userId = PurchaseOrder.AuthenticateRequest(userRequest.Authentication, out ex);
                    if (ex != null)
                    {
                        serviceResponse.Comment = ex.Message;
                        serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                        request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = true;
                        request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                        //LogOperations.ApiLogInsert(request);
                    }

                    if (userId > 0)
                    {
                        serviceResponse = GetAssignedUsers(userId);
                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                    request.UserID = userId;
                    request.CompanyID = 0;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;

                }
            }
            catch (Exception exc)
            {
                ex = exc;
                serviceResponse.CompanyInfo = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();
                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();

            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }
            return serviceResponse;
        }
        public static List<CustomerPricingType> GetCustomerPriceType()
        {

            List<CustomerPricingType> priceTypeList = new List<CustomerPricingType>();
            
            try
            {
                priceTypeList = PriceTypeDB();
                

            }
            catch (Exception ex)
            {
                
            }
            //serviceResponse = DownloadPoHeader(pos);
            return priceTypeList;
        }
        private static UsersResponse GetAssignedUsers(int userID)
        {

            UsersResponse serviceResponse = new UsersResponse();
            try
            {
                CompanyInformation companyInfo = AssignedUsersDB(userID);
                if (companyInfo != null && companyInfo.CompanyName != string.Empty)
                {
                    serviceResponse.CompanyInfo = companyInfo;
                    serviceResponse.Comment = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                    serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                }
                else
                {
                    serviceResponse.CompanyInfo = null;
                    serviceResponse.Comment = ResponseErrorCode.NoRecordsFound.ToString();
                    serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                }


            }
            catch (Exception ex)
            {
                serviceResponse.CompanyInfo = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InternalError.ToString();
            }
            //serviceResponse = DownloadPoHeader(pos);
            return serviceResponse;
        }
        private static CompanyInformation AssignedUsersDB(int userID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            List<Users> comapnyUserList = new List<Users>();
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            StringBuilder stringBuilder = new StringBuilder();
            CompanyInformation companyInfo = null;
            try
            {
                objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@UserID" };


                ds = db.GetDataSet(objCompHash, "av_AssignedUsers_Select", arrSpFieldSeq);

                
                
                Users userInfo = null;
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        
                        companyInfo = new CompanyInformation();
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
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;

            }

            return companyInfo;
        }

        private static List<CustomerPricingType> PriceTypeDB()
        {
            List<CustomerPricingType> priceTypeList = new List<CustomerPricingType>();
            CustomerPricingType priceType = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
          
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            StringBuilder stringBuilder = new StringBuilder();
            
            try
            {
                objCompHash.Add("@PricingTypeID", 0);

                arrSpFieldSeq = new string[] { "@PricingTypeID" };
                
                dt = db.GetTableRecords(objCompHash, "av_CustomerPricingTypeSelect", arrSpFieldSeq);
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        priceType = new CustomerPricingType();

                        priceType.PricingTypeID = (int)clsGeneral.getColumnData(dataRow, "PricingTypeID", 0, false);
                        priceType.PricingTypeText = (string)clsGeneral.getColumnData(dataRow, "PricingTypeText", string.Empty, false);
                        priceTypeList.Add(priceType);
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
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;

            }

            return priceTypeList;
        }

        public static List<IntegrationModule> GetIntegrationModules()
        {
            List<IntegrationModule> moduleList = new List<IntegrationModule>();
            IntegrationModule model = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;

            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            StringBuilder stringBuilder = new StringBuilder();

            try
            {
                objCompHash.Add("@IntegrationModuleID", 0);

                arrSpFieldSeq = new string[] { "@IntegrationModuleID" };

                dt = db.GetTableRecords(objCompHash, "av_CustomerIntegrationModuleSelect", arrSpFieldSeq);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        model = new IntegrationModule();

                        model.IntegrationModuleID = (int)clsGeneral.getColumnData(dataRow, "IntegrationModuleID", 0, false);
                        model.IntegrationName = (string)clsGeneral.getColumnData(dataRow, "IntegrationName", string.Empty, false);
                        moduleList.Add(model);
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
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;

            }

            return moduleList;
        }

        private static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }
        public static string SerializeObject<T>(T obj)
        {
            StringWriter xmlstringVal = null;
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            try
            {
                xmlstringVal = new StringWriter();
                ///xmlSettings.NamespaceHandling = NamespaceHandling.OmitDuplicates;
                xmlSettings.OmitXmlDeclaration = true;
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlWriter xmlWriter = XmlWriter.Create(xmlstringVal, xmlSettings);
                xs.Serialize(xmlWriter, obj, ns);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xmlSettings = null;
                xmlstringVal.Dispose();
            }

            return xmlstringVal.ToString().Trim();
            //try
            //{
            //    string xmlString = null;
            //    MemoryStream memoryStream = new MemoryStream();
            //    XmlSerializer xs = new XmlSerializer(typeof(T));
            //    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            //    xs.Serialize(xmlTextWriter, obj);
            //    memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            //    xmlString = UTF8ByteArrayToString(memoryStream.ToArray()); return xmlString.Trim();
            //}
            //catch (Exception ex)
            //{
            //    return string.Empty;
            //}
        }
        public static int CreateCompany(Company CompanyObj, out string warehouseCode)
        {
            int returnValue = 0;
            warehouseCode = string.Empty;
            string StoreXml = SerializeObject(CompanyObj.Stores);
            string salesPersonXML = SerializeObject(CompanyObj.AssingedSalesPerson);
            string customerEmailXml = SerializeObject(CompanyObj.CustomerEmailList);
            string customerWarehouseXml = SerializeObject(CompanyObj.WarehouseCodeList);
            string customerIntegrationXml = SerializeObject(CompanyObj.IntegrationList);
            
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
                    objCompHash.Add("@CustomerEmailXML", customerEmailXml);
                    objCompHash.Add("@WarehouseCodeXML", customerWarehouseXml);
                    objCompHash.Add("@IntegrationXML", customerIntegrationXml);
                    objCompHash.Add("@Active", CompanyObj.Active);
                    objCompHash.Add("@IsEmail", CompanyObj.IsEmail);
                    objCompHash.Add("@PricingTypeID", CompanyObj.PricingTypeID);
                    objCompHash.Add("@logopath", CompanyObj.LogoPath);
                    objCompHash.Add("@UserID", CompanyObj.UserID);
                    //objCompHash.Add("@APIName", CompanyObj.APIName);
                    //objCompHash.Add("@APIAddress", CompanyObj.APIAddress);
                    //objCompHash.Add("@APIUserName", CompanyObj.APIUserName);
                    //objCompHash.Add("@APIPassword", CompanyObj.APIPassword);


                    arrSpFieldSeq = new string[] { "@CompanyID", "@BussinessType", "@Comment", "@CompanyAccountNumber", "@CompanyAccountStatus", 
                        "@CompanyName", "@CompanyShortName", "@CompanySType", "@Email", "@GroupEmail","@Website","@StoreXML","@salesPersonXML",
                        "@CustomerEmailXML","@WarehouseCodeXML","@IntegrationXML","@Active","@IsEmail","@PricingTypeID","@logopath","@UserID" };
                    returnValue = db.ExCommand(objCompHash, "av_Company_InsertUpdate", arrSpFieldSeq, "@Warehouse", out warehouseCode);
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
            return returnValue;
        }
        public static List<CustomerEmail> GetCustomerEmailList(DataTable dataTable)
        {
            List<CustomerEmail> customerEmailList = new List<CustomerEmail>();

            //DataTable dataTable = new DataTable();
            //DBConnect db = new DBConnect();
            //string[] arrSpFieldSeq;
            //Hashtable objCompHash = new Hashtable();
            try
            {
                
                if (dataTable != null && dataTable.Rows.Count > 0)
                {


                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CustomerEmail objCustomerEmail = new CustomerEmail();
                        objCustomerEmail.ModuleGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleGUID", 0, false));
                        objCustomerEmail.IsNotification = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsNotification", false, false));
                        objCustomerEmail.Email = clsGeneral.getColumnData(dataRow, "Email", string.Empty, false) as string;
                        objCustomerEmail.OverrideEmail = clsGeneral.getColumnData(dataRow, "OverrideEmail", string.Empty, false) as string;
                        objCustomerEmail.ModuleName = clsGeneral.getColumnData(dataRow, "title", string.Empty, false) as string;
                        customerEmailList.Add(objCustomerEmail);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            

            return customerEmailList;
        }
        public static List<IntegrationModel> GetIntegrationList(DataTable dataTable)
        {
            List<IntegrationModel> integrationList = new List<IntegrationModel>();
            IntegrationModel model = null;
            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        model = new IntegrationModel();
                        model.IntegrationID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "IntegrationID", 0, false));
                        model.IntegrationModuleID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "IntegrationModuleID", 0, false));
                        model.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Active", false, false));
                        model.APIAddress = clsGeneral.getColumnData(dataRow, "APIAddress", string.Empty, false) as string;
                        model.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                        model.Password = clsGeneral.getColumnData(dataRow, "Password", string.Empty, false) as string;
                        integrationList.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return integrationList;
        }
        public static List<StoreLocation> getStoreLocationList(DataTable dataTable)
        { 
            List<StoreLocation> stroeLocationList = new List<StoreLocation>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                StoreLocation objStoreLocation = new StoreLocation();
                objStoreLocation.CompanyAddressID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AddressID", 0, false));
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
                //objStoreLocation.StoreAddress.City = clsGeneral.getColumnData(dataRow, "AddressType", string.Empty, false) as string;
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
        public static List<CompanyStore> GetCompanyStoreList(DataTable dataTable)
        {
            List<CompanyStore> stroeLocationList = new List<CompanyStore>();
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
            return stroeLocationList;
        }
        public static List<CompanyAddresses> GetCompanyAddresses(DataTable dataTable)
        {
            List<CompanyAddresses> companyAddressList = new List<CompanyAddresses>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                CompanyAddresses objCompanyAddress = new CompanyAddresses();
                Address CompanyAddress = new Address();
                ContactInfo CompanyContactInfo = new ContactInfo();
                int addrType = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AddressType", 0, false));

                CompanyAddress.Address1 = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                CompanyAddress.Address2 = clsGeneral.getColumnData(dataRow, "Address2", string.Empty, false) as string;
                //AddressType addressType = (AddressType)Enum.Parse(typeof(AddressType), dataRow["AddressType"].ToString());

                if(addrType == 1)
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
            return companyAddressList;
        }

        public static CompanyStoreResponse GetCompanyStore(CompanyStoreRequest companyStoreRequest)
        {
            CompanyStoreResponse serviceResponse = new CompanyStoreResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            string requestXML = clsGeneral.SerializeObject(companyStoreRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetCompanyStores";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            try
            {

                if (companyStoreRequest != null)
                {

                    int userID = PurchaseOrder.AuthenticateRequest(companyStoreRequest.Authentication, out ex);
                    if (ex != null)
                    {
                        serviceResponse.Comment = ex.Message;
                        serviceResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                        request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = true;
                        request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                        //LogOperations.ApiLogInsert(request);
                    }
                    if (userID > 0)
                    {
                        CompanyInfo companyInfo = GetCompanyInfo(userID);
                        serviceResponse.CompanyInformation = companyInfo;
                        serviceResponse.ErrorCode = string.Empty;
                        serviceResponse.Comment = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                    request.UserID = userID;
                    request.CompanyID = 0;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;

                }
            }
            catch (Exception exc)
            {
                ex = exc;
                serviceResponse.CompanyInformation = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();

            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }

            return serviceResponse;
        }
        public static CompanyInfo GetCompanyInfo(int userID)
        {
            CompanyInfo objCompany = new CompanyInfo();

            DataSet ds = new DataSet();
            DBConnect db = new DBConnect();
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
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return objCompany;
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
                objCompHash.Add("@CompanyAccountStatus", 0);
                arrSpFieldSeq = new string[] { "@CompanyID", "@CompanyName", "@CompanyAccountNumber", "@StoreID", "@ContactName", "@CompanyAccountStatus" };

                dataTable = db.GetDataSet(objCompHash, "av_Company_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Tables.Count > 0 && dataTable.Tables[0].Rows.Count > 0)
                {
                    if (dataTable.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in dataTable.Tables[0].Rows)
                        {                            
                            objCompany.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                            objCompany.PricingTypeID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PricingTypeID", 0, false));
                            objCompany.BussinessType = clsGeneral.getColumnData(dataRow, "BussinessType", string.Empty, false) as string;
                            objCompany.Comment = clsGeneral.getColumnData(dataRow, "Comment", string.Empty, false) as string;
                            objCompany.CompanyAccountNumber = clsGeneral.getColumnData(dataRow, "CompanyAccountNumber", string.Empty, false) as string;
                            objCompany.CompanyAccountStatus = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyAccountStatus", 0, false));
                            objCompany.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                            objCompany.CompanyShortName = clsGeneral.getColumnData(dataRow, "CompanyShortName", string.Empty, false) as string;
                            objCompany.Email = clsGeneral.getColumnData(dataRow, "Email", string.Empty, false) as string;
                            objCompany.GroupEmail = clsGeneral.getColumnData(dataRow, "GroupEmail", string.Empty, false) as string;
                            objCompany.Website = clsGeneral.getColumnData(dataRow, "Website", string.Empty, false) as string;
                            objCompany.LogoPath = clsGeneral.getColumnData(dataRow, "LogoPath", string.Empty, false) as string;
                            objCompany.CompanySType = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyType", 0, false));
                            objCompany.Active = (bool)clsGeneral.getColumnData(dataRow, "Active", true, false);
                            objCompany.IsEmail = (bool)clsGeneral.getColumnData(dataRow, "IsEmail", false, false);
                            //if(CompanyID > 0)
                            //{
                            //    objCompany.APIName = clsGeneral.getColumnData(dataRow, "APIName", string.Empty, false) as string;
                            //    objCompany.APIAddress = clsGeneral.getColumnData(dataRow, "APIAddress", string.Empty, false) as string;
                            //    objCompany.APIUserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                            //    objCompany.APIPassword = clsGeneral.getColumnData(dataRow, "Password", string.Empty, false) as string;

                            //}
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
                                if (dataTable.Tables.Count > 4 && dataTable.Tables[4].Rows.Count > 0)
                                {
                                    objCompany.CustomerEmailList = GetCustomerEmailList(dataTable.Tables[4]);
                                }
                                if (dataTable.Tables.Count > 5 && dataTable.Tables[5].Rows.Count > 0)
                                {
                                    objCompany.WarehouseCodeList = WarehouseCodeOperations.PopulateWarehouseCode(dataTable.Tables[5]);
                                }
                                if (dataTable.Tables.Count > 6 && dataTable.Tables[6].Rows.Count > 0)
                                {
                                    objCompany.IntegrationList = GetIntegrationList(dataTable.Tables[6]);
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
        public static List<Company> getCompanyList(int CompanyID, string CompanyName, string CompanyAccountNumber, string StoreID, string ContactName, int companyAccountStatus)
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
                objCompHash.Add("@CompanyAccountStatus", companyAccountStatus);
                arrSpFieldSeq = new string[] { "@CompanyID", "@CompanyName", "@CompanyAccountNumber", "@StoreID", "@ContactName", "@CompanyAccountStatus" };

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

        public static void DeleteCompanyAndStore(int comapmyID, int addressID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                arrSpFieldSeq = new string[] { "@CompanyID", "@AddressID" };

                objCompHash.Add("@CompanyID", comapmyID);
                objCompHash.Add("@AddressID", addressID);

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
        public static DataTable GetImportStoreList(string filePath)
        {

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@filepath", filePath);

                arrSpFieldSeq = new string[] { "@filepath" };

                dataTable = db.GetTableRecords(objCompHash, "av_rma_import", arrSpFieldSeq);



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

            return dataTable;
        }
        public static void UploadCompanyStores(List<StoreLocation> listObj, int CompanyID)
        {
            string StoreXml = SerializeObject(listObj);

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@StoreXML", StoreXml);


                arrSpFieldSeq = new string[] { "@CompanyID", "@StoreXML" };
                db.GetTableRecords(objCompHash, "av_CompanyStores_Upload", arrSpFieldSeq);

            }
            catch (Exception exp)
            {
                throw exp;
            }
            //return userid;
        }

        public static List<CustomerRMAStatus> GetCustomerRMAStatusList(int CompanyID, bool rma)
        {
            List<CustomerRMAStatus> customerRMAStatusList = new List<CustomerRMAStatus>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@RMA", rma);

                arrSpFieldSeq = new string[] { "@CompanyID", "@RMA" };

                dataTable = db.GetTableRecords(objCompHash, "av_Customer_RMAStatus_Select", arrSpFieldSeq);

                customerRMAStatusList = PopulatecustomerRMAStatus(dataTable);

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

            return customerRMAStatusList;
        }
        public static List<CustomerRMAStatus> GetRmaDetailStatusList()
        {
            List<CustomerRMAStatus> rmaStatusList = new List<CustomerRMAStatus>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", 0);
                //objCompHash.Add("@RMA", rma);

                arrSpFieldSeq = new string[] { "@CompanyID" };

                dataTable = db.GetTableRecords(objCompHash, "av_RmaDetail_Status_Select", arrSpFieldSeq);

                rmaStatusList = PopulatecustomerRMAStatus(dataTable);
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

            return rmaStatusList;
        }
        public static List<RmaTriageStatus> GetTriageStatusList()
        {
            List<RmaTriageStatus> statusList = new List<RmaTriageStatus>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@TriageStatusID", 0);

                arrSpFieldSeq = new string[] { "@TriageStatusID" };

                dataTable = db.GetTableRecords(objCompHash, "av_RMA_Triage_Statuses_Select", arrSpFieldSeq);

                statusList = PopulateStatus(dataTable);

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

            return statusList;
        }
        private static List<RmaTriageStatus> PopulateStatus(DataTable dataTable)
        {
            List<RmaTriageStatus> statusList = new List<RmaTriageStatus>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RmaTriageStatus obj = new RmaTriageStatus();
                    obj.TriageStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TriageStatusID", 0, false));
                    obj.TriageStatus = clsGeneral.getColumnData(dataRow, "TriageStatus", string.Empty, false) as string;

                    statusList.Add(obj);
                }

            }
            return statusList;
        }
        public static List<RmaReceiveStatus> GetReceiveRMAStatusList()
        {
            List<RmaReceiveStatus> statusList = new List<RmaReceiveStatus>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ReceiveStatusID", 0);
                
                arrSpFieldSeq = new string[] { "@ReceiveStatusID" };

                dataTable = db.GetTableRecords(objCompHash, "av_RMA_Receive_Statuses_Select", arrSpFieldSeq);

                statusList = PopulateReceiveRMAStatus(dataTable);

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

            return statusList;
        }
        private static List<RmaReceiveStatus> PopulateReceiveRMAStatus(DataTable dataTable)
        {
            List<RmaReceiveStatus> statusList = new List<RmaReceiveStatus>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RmaReceiveStatus obj = new RmaReceiveStatus();
                    obj.ReceiveStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ReceiveStatusID", 0, false));
                    obj.ReceiveStatus = clsGeneral.getColumnData(dataRow, "ReceiveStatus", string.Empty, false) as string;

                    statusList.Add(obj);
                }

            }
            return statusList;
        }
        private static List<CustomerRMAStatus> PopulatecustomerRMAStatus(DataTable dataTable)
        {
            List<CustomerRMAStatus> custRMAStatusList = new List<CustomerRMAStatus>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    CustomerRMAStatus objCustomerShipBy = new CustomerRMAStatus();
                    objCustomerShipBy.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    objCustomerShipBy.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                    objCustomerShipBy.StatusDescription = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;

                    custRMAStatusList.Add(objCustomerShipBy);
                }

            }
            return custRMAStatusList;
        }


    }
}
