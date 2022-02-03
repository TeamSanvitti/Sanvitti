using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace avii.Classes
{
    public class ReportOperations
    {
        public static List<FulfillmentBilling> GetFulfillmentBillingReport(int companyID, string DateFrom, string DateTo, string ShipFromDate, string ShipToDate, string FulfillmentNumber, string FulfillmentType, string ContactName)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<FulfillmentBilling> poBillingList = new List<FulfillmentBilling>();
            //ShipmentInfo shipmentInfo = null;
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", string.IsNullOrEmpty(DateFrom) ? null : DateFrom);
                objCompHash.Add("@ToDate", string.IsNullOrEmpty(DateTo) ? null : DateTo);
                objCompHash.Add("@ShipFromDate", string.IsNullOrEmpty(ShipFromDate) ? null : ShipFromDate);
                objCompHash.Add("@ShipToDate", string.IsNullOrEmpty(ShipToDate) ? null : ShipToDate);
                objCompHash.Add("@FulfillmentNumber", FulfillmentNumber);
                objCompHash.Add("@FulfillmentType", FulfillmentType);
                objCompHash.Add("@ContactName", ContactName);
                
                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@ShipFromDate", "@ShipToDate", "@FulfillmentNumber", "@FulfillmentType", "@ContactName" };

                dt = db.GetTableRecords(objCompHash, "av_FulfillmentBilling_Report", arrSpFieldSeq);

                poBillingList = PopulatePOBilling(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return poBillingList;

        }
        private static List<FulfillmentBilling> PopulatePOBilling(DataTable dt)
        {
            List<FulfillmentBilling> poBillingList = new List<FulfillmentBilling>();
            FulfillmentBilling po = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    po = new FulfillmentBilling();
                    po.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    po.FulfillmentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.MinValue, false));
                    po.Price = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "FinalPostage", 0, false));
                    po.Weight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));
                    
                    po.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipDate", DateTime.MinValue, false));
                    po.ShipVia = clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, false) as string;
                    po.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    po.ShipmentType = clsGeneral.getColumnData(dataRow, "ReturnLabel", string.Empty, false) as string;
                    po.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    po.ICC_ID = clsGeneral.getColumnData(dataRow, "ICC_ID", string.Empty, false) as string;
                    po.BatchNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
                    po.ShipPackage = clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false) as string;
                    po.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    po.ContactName = clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, false) as string;
                    po.FulfillmentType = clsGeneral.getColumnData(dataRow, "POType", string.Empty, false) as string;


                    poBillingList.Add(po);
                }
            }

            return poBillingList;
        }

        public static List<ShipmentInfo> GetShipmentSummary(int companyID, string labelType, string shipmentDateFrom, string shipmentDateTo, string shipVia, string shipPackage)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<ShipmentInfo> shipmentList = new List<ShipmentInfo>();
            //ShipmentInfo shipmentInfo = null;
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@LabelType", labelType);
                objCompHash.Add("@ShipmentDateFrom", string.IsNullOrEmpty(shipmentDateFrom) ? null: shipmentDateFrom);
                objCompHash.Add("@ShipmentDateTo", string.IsNullOrEmpty(shipmentDateTo) ? null : shipmentDateTo);
                objCompHash.Add("@ShipVia", shipVia);
                objCompHash.Add("@ShipPackage", shipPackage);

                arrSpFieldSeq = new string[] { "@CompanyID", "@LabelType", "@ShipmentDateFrom", "@ShipmentDateTo", "@ShipVia", "@ShipPackage" };

                dt = db.GetTableRecords(objCompHash, "av_Shipment_Summary", arrSpFieldSeq);

                shipmentList = PopulateShipments(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return shipmentList;

        }
        private static List<ShipmentInfo> PopulateShipments(DataTable dt)
        {
            List<ShipmentInfo> shipmentList = new List<ShipmentInfo>();
            ShipmentInfo shipmentInfo = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    shipmentInfo = new ShipmentInfo();
                    shipmentInfo.AssignmentNumber = clsGeneral.getColumnData(dataRow, "AssignmentNumber", string.Empty, false) as string;
                    shipmentInfo.FinalPostage = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "FinalPostage", 0, false));
                    shipmentInfo.ShipWeight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));
                    shipmentInfo.ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ID", 0, false));
                    shipmentInfo.IsPrint = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsPrint", false, false));

                    shipmentInfo.LabelGenerationDate = clsGeneral.getColumnData(dataRow, "CreatedDate", string.Empty, false) as string;
                    shipmentInfo.LabelType = clsGeneral.getColumnData(dataRow, "LabelType", string.Empty, false) as string;
                    shipmentInfo.LabelUsedDate = clsGeneral.getColumnData(dataRow, "LabelUsedDate", string.Empty, false) as string;
                    shipmentInfo.ShipMethod = clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, false) as string;
                    shipmentInfo.ShipPackage = clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false) as string;
                    shipmentInfo.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;

                    shipmentList.Add(shipmentInfo);
                }
            }

            return shipmentList;
        }

        public static List<EsnList> GetFileUploadMappingList(int uploadedFileID, int moduleID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<EsnList> esnList = new List<EsnList>();
            try
            {
                objCompHash.Add("@UploadedFileID", uploadedFileID);
                objCompHash.Add("@ModuleID", moduleID);

                arrSpFieldSeq = new string[] { "@UploadedFileID", "@ModuleID" };

                dt = db.GetTableRecords(objCompHash, "av_FileUpload_Mapping_Select", arrSpFieldSeq);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        EsnList esn = new EsnList();
                        esn.ESN = clsGeneral.getColumnData(dataRow, "CONTENT", string.Empty, false) as string;
                        esnList.Add(esn);
                    }
                }

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return esnList;

        }
        
        public static List<ModulesName> GetModuleNameList(int moduleID)
        {
            DBConnect db = new DBConnect();
            List<ModulesName> moduleList = new List<ModulesName>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                
                objParams.Add("@ModuleID", moduleID);
                
                arrSpFieldSeq =
                new string[] { "@ModuleID" };


                dataTable = db.GetTableRecords(objParams, "AV_ModuleName_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        ModulesName module = new ModulesName();
                        module.ModuleName = clsGeneral.getColumnData(dataRow, "ModuleName", string.Empty, false) as string;
                        module.ModuleID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ModuleID", 0, false));
                        moduleList.Add(module);


                    }
                }
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
            return moduleList;
        }

        public static List<Fileupload> GetFileUploadReport(int companyID, int moduleID, DateTime fromDate, DateTime toDate)
        {
            DBConnect db = new DBConnect();
            List<Fileupload> fileUploadList = new List<Fileupload>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@ModuleID", moduleID);
                objParams.Add("@FromDate", fromDate.ToShortDateString() == "1/1/0001" ? null : fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString() == "1/1/0001" ? null : toDate.ToShortDateString());
                
                arrSpFieldSeq =
                new string[] { "@CompanyID", "@ModuleID", "@FromDate", "@ToDate" };


                dataTable = db.GetTableRecords(objParams, "av_FileUpload_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        Fileupload fileUpload = new Fileupload();
                        fileUpload.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        fileUpload.ModuleName = clsGeneral.getColumnData(dataRow, "ModuleName", string.Empty, false) as string;
                        fileUpload.UploadedBy = clsGeneral.getColumnData(dataRow, "Username", string.Empty, false) as string;
                        fileUpload.FileName = clsGeneral.getColumnData(dataRow, "UploadedFilename", string.Empty, false) as string;
                        fileUpload.ModuleID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ModuleID", 0, false));
                        fileUpload.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UploadedDate", DateTime.MinValue, false));
                        fileUpload.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        fileUpload.UploadedFileID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UploadedFileID", 0, false));
                        fileUpload.Comments = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                        fileUpload.Resource = clsGeneral.getColumnData(dataRow, "Resource", string.Empty, false) as string;
                        
                        fileUploadList.Add(fileUpload);


                    }
                }
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
            return fileUploadList;
        }

        public FulfillmentTrackingResponse GetFulfillmentTracking(FulfillmentTrackingRequest serviceRequest)
        {

            FulfillmentTrackingResponse serviceResponse = new FulfillmentTrackingResponse();
            Exception exc = null;

            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out exc);
                if (credentialValidation != null && credentialValidation.CompanyID > 0)
                {

                    List<FulfillmentTracking> trackingList = ReportOperations.GetCustomerFulfillmentTrackingReport(credentialValidation.CompanyID, serviceRequest.ShipmentFromDate, serviceRequest.ShipmentToDate, serviceRequest.FulfillmentNumber, serviceRequest.TrackingNumber, serviceRequest.ShipViaCode);


                    if (trackingList != null && trackingList.Count > 0)
                    {

                        serviceResponse.Comment = "Successfully Retrieved";
                        serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                        serviceResponse.TrackingList = trackingList;


                    }
                    else
                    {

                        serviceResponse.Comment = "No records found";
                        serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                        serviceResponse.TrackingList = null;
                    }

                }
                else
                {
                    serviceResponse.Comment = "Cannot Authenticate User";
                    serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    serviceResponse.TrackingList = null;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.TrackingList = null;

            }

            return serviceResponse;
        }


        public static List<FulfillmentTracking> GetCustomerFulfillmentTrackingReport(int companyID, DateTime fromDate, DateTime toDate, string fulfillmentNumber, string trackingNumber, string shipViaCode)
        {
            DBConnect db = new DBConnect();
            List<FulfillmentTracking> trackingList = new List<FulfillmentTracking>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate.ToShortDateString() == "1/1/0001" ? null : fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString() == "1/1/0001" ? null : toDate.ToShortDateString());
                objParams.Add("@FulfillmentNumber", fulfillmentNumber);
                objParams.Add("@TrackingNumber", trackingNumber);
                objParams.Add("@ShipViaCode", shipViaCode);

                arrSpFieldSeq =
                new string[] { "@CompanyID", "@FromDate", "@ToDate", "@FulfillmentNumber", "@TrackingNumber", "@ShipViaCode" };


                dataTable = db.GetTableRecords(objParams, "AV_CustomerFulfillmentTacking_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        FulfillmentTracking tracking = new FulfillmentTracking();
                        tracking.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                        tracking.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "po_num", string.Empty, false) as string;
                        tracking.ShipByCode = clsGeneral.getColumnData(dataRow, "ShipByCode", string.Empty, false) as string;
                        tracking.ShipmentType = clsGeneral.getColumnData(dataRow, "RETURNLABLE", string.Empty, false) as string;
                        tracking.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "po_id", 0, false));
                        tracking.FulfillmentDate =  Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "po_date", "", false));
                        tracking.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipDate", "1/1/0001", false));
                        tracking.EsnCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "EsnCount", 0, false));
                        trackingList.Add(tracking);


                    }
                }
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
            return trackingList;
        }

        public EsnRepositoryResponse GetEsnRepository(EsnRepositoryRequest serviceRequest)
        {
            string requestXML = clsGeneral.SerializeObject(serviceRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetEsnRepositoryList";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            EsnRepositoryResponse serviceResponse = new EsnRepositoryResponse();
            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out ex);
                if (ex != null)
                {
                    request.CompanyID = 0;
                    request.UserID = 0;
                    serviceResponse.Comment = ex.Message;
                    serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    LogOperations.ApiLogInsert(request);
                }
                else
                {

                    if (credentialValidation != null && credentialValidation.CompanyID > 0)
                    {
                        request.UserID = credentialValidation.UserID;
                        request.CompanyID = credentialValidation.CompanyID;

                        List<EsnRepository> esnList = ReportOperations.GetCustomerEsnRepositoryDownload(credentialValidation.CompanyID, 0, serviceRequest.FromDate, serviceRequest.ToDate, serviceRequest.UnusedOnly, serviceRequest.ShowAllUnusedEsn);


                        //esnInfoList = avii.Classes.BabEsnOperation.ValidateBadESN(serviceRequest.EsnList, credentialValidation.CompanyID, out recordCount, out inactiveESNMsg, out invalidESNMsg, out esnAlreadyExists);
                        if (esnList != null && esnList.Count > 0)
                        {

                            serviceResponse.Comment = "Successfully Retrieved.  Record Count: " + esnList.Count;
                            serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                            serviceResponse.EsnList = esnList;


                        }
                        else
                        {

                            serviceResponse.Comment = "No records found";
                            serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                            serviceResponse.EsnList = null;
                        }

                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot Authenticate User";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                        serviceResponse.EsnList = null;
                    }
                    
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;
                    LogOperations.ApiLogInsert(request);
                }
            }
            catch (Exception exc)
            {
                ex = exc;
                //  serviceResponse.Comment = exc.Message;
                //serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.EsnList = null;

                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                LogOperations.ApiLogInsert(request);
            }


            return serviceResponse;
        }

        public CustomerShipViaResponse GetCustomerShipViaSummary(CustomerShipViaRequest serviceRequest)
        {

            CustomerShipViaResponse serviceResponse = new CustomerShipViaResponse();
            Exception exc = null;

            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out exc);
                if (credentialValidation != null && credentialValidation.CompanyID > 0)
                {

                    Customers customerInfo = ReportOperations.GetCustomerShipViaSummary(credentialValidation.CompanyID, serviceRequest.FromDate, serviceRequest.ToDate);


                    if (customerInfo != null && !string.IsNullOrEmpty(customerInfo.CustomerName))
                    {

                        serviceResponse.Comment = "Successfully Retrieved";
                        serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                        serviceResponse.Customer = customerInfo;


                    }
                    else
                    {

                        serviceResponse.Comment = "No records found";
                        serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                        serviceResponse.Customer = null;
                    }

                }
                else
                {
                    serviceResponse.Comment = "Cannot Authenticate User";
                    serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    serviceResponse.Customer = null;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.Customer = null;

            }

            return serviceResponse;
        }

        public EsnInventoryResponse GetEsnInventorySummary(EsnInventoryRequest serviceRequest)
        {

            EsnInventoryResponse serviceResponse = new EsnInventoryResponse();
            Exception exc = null;

            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out exc);
                if (credentialValidation != null && credentialValidation.CompanyID > 0)
                {

                    List<SkusEsnList> esnList = ReportOperations.GetCustomerSkuEsnSummary(credentialValidation.CompanyID, 0, serviceRequest.FromDate, serviceRequest.ToDate, serviceRequest.ShowAllUnusedEsn);


                    if (esnList != null && esnList.Count > 0)
                    {

                        serviceResponse.Comment = "Successfully Retrieved.  Record Count: " + esnList.Count;
                        serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                        serviceResponse.EsnInventory = esnList;


                    }
                    else
                    {

                        serviceResponse.Comment = "No records found";
                        serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                        serviceResponse.EsnInventory = null;
                    }

                }
                else
                {
                    serviceResponse.Comment = "Cannot Authenticate User";
                    serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    serviceResponse.EsnInventory = null;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.EsnInventory = null;

            }

            return serviceResponse;
        }

        public EsnRepositoryDetailResponse GetEsnRepositoryDetail(EsnRepositoryDetailRequest serviceRequest)
        {

            EsnRepositoryDetailResponse serviceResponse = new EsnRepositoryDetailResponse();
            Exception exc = null;

            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out exc);
                if (credentialValidation != null && credentialValidation.CompanyID > 0)
                {

                    List<EsnRepositoryDetail> esnList = ReportOperations.GetCustomerEsnRepositoryDetail(credentialValidation.CompanyID, 0, serviceRequest.FromDate, serviceRequest.ToDate, serviceRequest.UnusedOnly, serviceRequest.ShowAllUnusedEsn);


                    //esnInfoList = avii.Classes.BabEsnOperation.ValidateBadESN(serviceRequest.EsnList, credentialValidation.CompanyID, out recordCount, out inactiveESNMsg, out invalidESNMsg, out esnAlreadyExists);
                    if (esnList != null && esnList.Count > 0)
                    {

                        serviceResponse.Comment = "Successfully Retrieved.  Record Count: " + esnList.Count;
                        serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                        serviceResponse.ESNDetail = esnList;


                    }
                    else
                    {

                        serviceResponse.Comment = "No records found";
                        serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                        serviceResponse.ESNDetail = null;
                    }

                }
                else
                {
                    serviceResponse.Comment = "Cannot Authenticate User";
                    serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    serviceResponse.ESNDetail = null;
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.ESNDetail = null;

            }

            return serviceResponse;
        }

        public RmaEsnListingResponse GetRmaEsnListing(RmaEsnListingRequest serviceRequest)
        {
            string requestXML = clsGeneral.SerializeObject(serviceRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetRmaEsnListing";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            RmaEsnListingResponse serviceResponse = new RmaEsnListingResponse();
            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out ex);
                if (ex != null)
                {

                    request.UserID = 0;
                    request.CompanyID = 0;

                    serviceResponse.Comment = ex.Message;
                    serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    LogOperations.ApiLogInsert(request);
                }
                else
                {

                    if (credentialValidation != null && credentialValidation.CompanyID > 0)
                    {

                        request.UserID = credentialValidation.UserID;
                        request.CompanyID = credentialValidation.CompanyID;

                        List<RmaEsnStatuses> rmaStatusList = ReportOperations.GetCustomerRmaEsnStatusReport(credentialValidation.CompanyID, serviceRequest.FromDate, serviceRequest.ToDate, Convert.ToInt32(serviceRequest.EsnStatus), Convert.ToInt32(serviceRequest.RmaStatus));


                        //esnInfoList = avii.Classes.BabEsnOperation.ValidateBadESN(serviceRequest.EsnList, credentialValidation.CompanyID, out recordCount, out inactiveESNMsg, out invalidESNMsg, out esnAlreadyExists);
                        if (rmaStatusList != null && rmaStatusList.Count > 0)
                        {

                            serviceResponse.Comment = "Successfully Retrieved.  Record Count: " + rmaStatusList.Count;
                            serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                            serviceResponse.RmaEsnListing = rmaStatusList;


                        }
                        else
                        {

                            serviceResponse.Comment = "No records found";
                            serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                            serviceResponse.RmaEsnListing = null;
                        }

                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot Authenticate User";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                        serviceResponse.RmaEsnListing = null;
                    }
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;
                    LogOperations.ApiLogInsert(request);
                }
            }
            catch (Exception exc)
            {
                ex = exc;
                //  serviceResponse.Comment = exc.Message;
                //serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.RmaEsnListing = null;
                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                LogOperations.ApiLogInsert(request);
            }

            return serviceResponse;
        }

        public RmaEsnDetailResponse GetRmaEsnDetail(RmaEsnListingRequest serviceRequest)
        {
            string requestXML = clsGeneral.SerializeObject(serviceRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetRmaEsnDetail";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            RmaEsnDetailResponse serviceResponse = new RmaEsnDetailResponse();
            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out ex);

                if (ex != null)
                {

                    request.UserID = 0;
                    request.CompanyID = 0;

                    serviceResponse.Comment = ex.Message;
                    serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    LogOperations.ApiLogInsert(request);
                }
                else
                {

                    if (credentialValidation != null && credentialValidation.CompanyID > 0)
                    {
                        request.UserID = credentialValidation.UserID;
                        request.CompanyID = credentialValidation.CompanyID;


                        List<RMAESNDetail> rmaDetailList = ReportOperations.GetRmaEsnDetailReport(credentialValidation.CompanyID, "", "", serviceRequest.FromDate, serviceRequest.ToDate, Convert.ToInt32(serviceRequest.EsnStatus), Convert.ToInt32(serviceRequest.RmaStatus));


                        //esnInfoList = avii.Classes.BabEsnOperation.ValidateBadESN(serviceRequest.EsnList, credentialValidation.CompanyID, out recordCount, out inactiveESNMsg, out invalidESNMsg, out esnAlreadyExists);
                        if (rmaDetailList != null && rmaDetailList.Count > 0)
                        {

                            serviceResponse.Comment = "Successfully Retrieved.  Record Count: " + rmaDetailList.Count;
                            serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                            serviceResponse.RmaEsnDetail = rmaDetailList;


                        }
                        else
                        {

                            serviceResponse.Comment = "No records found";
                            serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                            serviceResponse.RmaEsnDetail = null;
                        }

                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot Authenticate User";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                        serviceResponse.RmaEsnDetail = null;
                    }
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;
                    LogOperations.ApiLogInsert(request);
                }
            }
            catch (Exception exc)
            {
                ex = exc;
                //  serviceResponse.Comment = exc.Message;
                //serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.RmaEsnDetail = null;
                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                LogOperations.ApiLogInsert(request);
            }

            return serviceResponse;
        }

        public static List<EsnRepository> GetCustomerEsnRepositoryDownload(int companyID, int itemCompanyGUID, DateTime fromDate, DateTime toDate, bool unUsedESN, bool showAllUnusedESN)
        {
            List<EsnRepository> esnList = new List<EsnRepository>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@ItemCompanyGUID", itemCompanyGUID);
                objCompHash.Add("@FromDate", fromDate.ToShortDateString() == "1/1/0001" ? null : fromDate.ToShortDateString());
                objCompHash.Add("@ToDate", toDate.ToShortDateString() == "1/1/0001" ? null : toDate.ToShortDateString());
                objCompHash.Add("@UnusedESN", unUsedESN);
                objCompHash.Add("@ShowAllUnusedESN", showAllUnusedESN);

                arrSpFieldSeq = new string[] { "@CompanyID", "@ItemCompanyGUID", "@FromDate", "@ToDate", "@UnusedESN", "@ShowAllUnusedESN" };


                dataTable = db.GetTableRecords(objCompHash, "av_EsnRepository_Download", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        EsnRepository esnObj = new EsnRepository();

                        esnObj.UnusedESN = clsGeneral.getColumnData(dataRow, "UnusedESN", string.Empty, false) as string;
                        esnObj.InPorocessESN = clsGeneral.getColumnData(dataRow, "InprocessedESN", string.Empty, false) as string;
                        esnObj.ShippedESN = clsGeneral.getColumnData(dataRow, "ShippedESN", string.Empty, false) as string;
                        esnObj.RmaESN = clsGeneral.getColumnData(dataRow, "RmaESN", string.Empty, false) as string;

                        esnList.Add(esnObj);
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

            return esnList;
        }

        public static List<ReassignSKU> GetReassignSkuReport(int companyID, DateTime fromDate, DateTime toDate, string esn, string sku)
        {
            DBConnect db = new DBConnect();
            List<ReassignSKU> reassignSKUList = null;
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString());
                objParams.Add("@ESN", esn);
                objParams.Add("@SKU", sku);

                arrSpFieldSeq =
                new string[] { "@CompanyID", "@FromDate", "@ToDate", "@ESN", "@SKU" };


                dataTable = db.GetTableRecords(objParams, "AV_ReassignSKU_SELECT", arrSpFieldSeq);
                reassignSKUList = PopulateReassignSKU(dataTable);

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
            return reassignSKUList;
        }

        private static List<ReassignSKU> PopulateReassignSKU(DataTable dataTable)
        {
            List<ReassignSKU> reassignSKUList = new List<ReassignSKU>();

            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ReassignSKU reassignSKU = new ReassignSKU();
                    reassignSKU.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    reassignSKU.OldSKUCategory = clsGeneral.getColumnData(dataRow, "OldSKUCategory", string.Empty, false) as string;
                    reassignSKU.OldSKU = clsGeneral.getColumnData(dataRow, "OLDSKU", string.Empty, false) as string;
                    reassignSKU.NewSKUCategory = clsGeneral.getColumnData(dataRow, "NewSKUCategory", string.Empty, false) as string;
                    reassignSKU.NewSKU = clsGeneral.getColumnData(dataRow, "NEWSKU", string.Empty, false) as string;
                    //skuESN.TotalESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TotalEsncount", 0, false));
                    reassignSKU.ChangeDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ChangeDate", 0, false));
                    reassignSKU.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                    reassignSKU.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    reassignSKU.RMANumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;

                    reassignSKUList.Add(reassignSKU);


                }
            }
            return reassignSKUList;
        }

        public static List<ProductSKUsInfo> GetProductWiseSKUsEsnSummary(int itemGUID, DateTime fromDate, DateTime toDate)
        {
            DBConnect db = new DBConnect();
            List<ProductSKUsInfo> skuEsnList = new List<ProductSKUsInfo>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@ItemGUID", itemGUID);
                objParams.Add("@FromDate", fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString());

                arrSpFieldSeq =
                new string[] { "@ItemGUID", "@FromDate", "@ToDate" };


                dataTable = db.GetTableRecords(objParams, "av_ProductWise_SKU_N_ESN_Detail_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        ProductSKUsInfo skuESN = new ProductSKUsInfo();
                        skuESN.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        skuESN.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        skuESN.UnusedESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UnusedESN", 0, false));
                        skuESN.UsedEsnProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UsedEsnProcessed", 0, false));
                        skuESN.UsedEsnShipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UsedEsnShipped", 0, false));
                        skuESN.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                        skuESN.RmaESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RMACount", 0, false));

                        skuEsnList.Add(skuESN);


                    }
                }
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
            return skuEsnList;
        }

        public static Customers GetCustomerShipViaSummary(int companyID, DateTime fromDate, DateTime toDate)
        {
            DBConnect db = new DBConnect();
            Customers customer = new Customers();
            List<ShipViaList> shipViaList = new List<ShipViaList>();
            string[] arrSpFieldSeq;
            Hashtable objParams = new Hashtable();
            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate.ToShortDateString() == "1/1/0001" ? null : fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString() == "1/1/0001" ? null : toDate.ToShortDateString());
                objParams.Add("@API", true);

                arrSpFieldSeq =
                new string[] { "@CompanyID", "@FromDate", "@ToDate", "@API" };


                dataTable = db.GetTableRecords(objParams, "AV_Customer_ShipVia_Report", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        ShipViaList shipVia = new ShipViaList();
                        customer.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;


                        shipVia.ShipByCode = clsGeneral.getColumnData(dataRow, "ShipByCode", string.Empty, false) as string;
                        shipVia.Count = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShipViaCount", 0, false));

                        shipViaList.Add(shipVia);


                    }
                    customer.ShipVia = shipViaList;


                }
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
            return customer;
        }

        public static List<CustomerShipViaCodes> GetCustomersShipViaSummary(int companyID, DateTime fromDate, DateTime toDate)
        {
            DBConnect db = new DBConnect();
            List<CustomerShipViaCodes> CustomerShipViaCodeList = new List<CustomerShipViaCodes>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate.ToShortDateString() == "1/1/0001" ? null : fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString() == "1/1/0001" ? null : toDate.ToShortDateString());
                objParams.Add("@API", false);

                arrSpFieldSeq =
                new string[] { "@CompanyID", "@FromDate", "@ToDate", "@API" };


                dataTable = db.GetTableRecords(objParams, "AV_Customer_ShipVia_Report", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CustomerShipViaCodes customerShipViaCode = new CustomerShipViaCodes();
                        customerShipViaCode.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        customerShipViaCode.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                        customerShipViaCode.FDGE = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDGE", 0, false));
                        customerShipViaCode.FDX2DASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDX2DASR", 0, false));
                        customerShipViaCode.FDX2DCOD = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDX2DCOD", 0, false));
                        customerShipViaCode.FDX2DDSR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDX2DDSR", 0, false));
                        customerShipViaCode.FDX2DHASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDX2DHASR", 0, false));
                        customerShipViaCode.FDX2DHDSR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDX2DHDSR", 0, false));
                        customerShipViaCode.FDX2DHNOS = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDX2DHNOS", 0, false));
                        customerShipViaCode.FDX2DNOS = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDX2DNOS", 0, false));
                        customerShipViaCode.FDX3DSAV = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDX3DSAV", 0, false));
                        customerShipViaCode.FDXEXASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXEXASR", 0, false));
                        customerShipViaCode.FDX2DHASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDX2DHASR", 0, false));
                        customerShipViaCode.FDXEXCOD = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXEXCOD", 0, false));
                        customerShipViaCode.FDXEXDSR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXEXDSR", 0, false));
                        customerShipViaCode.FDXEXHASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXEXHASR", 0, false));
                        customerShipViaCode.FDXEXHDSR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXEXHDSR", 0, false));
                        customerShipViaCode.FDXEXHNOS = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXEXHNOS", 0, false));
                        customerShipViaCode.FDXEXNOS = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXEXNOS", 0, false));
                        customerShipViaCode.FDXGRASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXGRASR", 0, false));
                        customerShipViaCode.FDXGRCOD = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXGRCOD", 0, false));
                        customerShipViaCode.FDXGRDSR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXGRDSR", 0, false));
                        customerShipViaCode.FDX2DCOD = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDX2DCOD", 0, false));
                        customerShipViaCode.FDXHDASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXHDASR", 0, false));
                        customerShipViaCode.FDX2DHASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDX2DHASR", 0, false));
                        customerShipViaCode.FDXHDDSR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXHDDSR", 0, false));
                        customerShipViaCode.FDXHDNOS = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXHDNOS", 0, false));
                        customerShipViaCode.FDXINEC = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXINEC", 0, false));

                        customerShipViaCode.FDXINPR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXINPR", 0, false));
                        customerShipViaCode.FDXPOASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXPOASR", 0, false));
                        customerShipViaCode.FDXPOCOD = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXPOCOD", 0, false));
                        customerShipViaCode.FDXPODSR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXPODSR", 0, false));
                        customerShipViaCode.FDXPOHASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXPOHASR", 0, false));
                        customerShipViaCode.FDXPOHDSR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXPOHDSR", 0, false));
                        customerShipViaCode.FDXPOHNOS = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXPOHNOS", 0, false));
                        customerShipViaCode.FDXPONOS = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSDASR", 0, false));
                        customerShipViaCode.FDXSDASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDX2DHASR", 0, false));
                        customerShipViaCode.FDXSDDSR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSDDSR", 0, false));
                        customerShipViaCode.FDXSDHASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSDHASR", 0, false));
                        customerShipViaCode.FDXSDHDSR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSDHDSR", 0, false));
                        customerShipViaCode.FDXSDHNOS = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSDHNOS", 0, false));
                        customerShipViaCode.FDXSDNOS = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSDNOS", 0, false));
                        customerShipViaCode.FDXSOASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSOASR", 0, false));
                        customerShipViaCode.FDXSOCOD = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSOCOD", 0, false));
                        customerShipViaCode.FDXSODSR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSODSR", 0, false));
                        customerShipViaCode.FDXSOHASR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSOHASR", 0, false));
                        customerShipViaCode.FDXSOHDSR = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSOHDSR", 0, false));
                        customerShipViaCode.FDXSOHNOS = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSOHNOS", 0, false));
                        customerShipViaCode.FDXSONOS = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FDXSONOS", 0, false));
                        //customerShipViaCode.FED1DPM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "[FED 1D PM]", 0, false));
                        //customerShipViaCode.FED2DPM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "[FED 2D PM]", 0, false));
                        customerShipViaCode.FED1DPM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FED1DPM", 0, false));
                        customerShipViaCode.FED2DPM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FED2DPM", 0, false));
                        customerShipViaCode.FedExSaturdayDeliver = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "[FedEx Saturday Deliver]", 0, false));
                        customerShipViaCode.FedExSaver3day = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "[FedEx Saver 3 day]", 0, false));
                        customerShipViaCode.IndirectSign = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "[Indirect Sign]", 0, false));
                        customerShipViaCode.PriorityOvernite = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PriorityOvernite", 0, false));
                        customerShipViaCode.UPSBlue = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "[UPS Blue]", 0, false));
                        customerShipViaCode.UPSGround = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "[UPS Ground]", 0, false));
                        customerShipViaCode.UPSRed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "[UPS Red]", 0, false));


                        CustomerShipViaCodeList.Add(customerShipViaCode);


                    }
                }
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
            return CustomerShipViaCodeList;
        }
        public static List<SkusEsnList> GetCustomerSkuEsnSummary(int companyID, int itemCompanyGUID, DateTime fromDate, DateTime toDate, bool showAllUnusedESN)
        {
            DBConnect db = new DBConnect();
            List<SkusEsnList> skuEsnList = new List<SkusEsnList>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString());
                objParams.Add("@ItemCompanyGUID", itemCompanyGUID);
                objParams.Add("@ShowAllEsn", showAllUnusedESN);

                arrSpFieldSeq =
                new string[] { "@CompanyID", "@FromDate", "@ToDate", "@ItemCompanyGUID", "@ShowAllEsn" };


                dataTable = db.GetTableRecords(objParams, "AV_CustomerSkuEsnSummary_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        SkusEsnList skuESN = new SkusEsnList();
                        skuESN.ProductCode = clsGeneral.getColumnData(dataRow, "ModelNumber", string.Empty, false) as string;
                        skuESN.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                        skuESN.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        //skuESN.TotalESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TotalEsncount", 0, false));
                        skuESN.UnusedESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UnusedESN", 0, false));
                        skuESN.UsedEsnProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UsedEsnProcessed", 0, false));
                        skuESN.UsedEsnShipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UsedEsnShipped", 0, false));
                        skuESN.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                        skuESN.RmaESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "esnCount", 0, false));
                        skuESN.OpeningBalance = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "EsnOpeningBalance", 0, false));
                        skuESN.NewESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "NewEsnCount", 0, false));

                        skuESN.AvailableESN = skuESN.NewESN + skuESN.OpeningBalance;
                        
                        skuEsnList.Add(skuESN);


                    }
                }
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
            return skuEsnList;
        }
        public static List<SkusEsnList> GetCustomerSkuSimSummary(int companyID, int itemCompanyGUID, DateTime fromDate, DateTime toDate, bool showAllUnusedESN)
        {
            DBConnect db = new DBConnect();
            List<SkusEsnList> skuEsnList = new List<SkusEsnList>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString());
                objParams.Add("@ItemCompanyGUID", itemCompanyGUID);
                objParams.Add("@ShowAllEsn", showAllUnusedESN);

                arrSpFieldSeq =
                new string[] { "@CompanyID", "@FromDate", "@ToDate", "@ItemCompanyGUID", "@ShowAllEsn" };


                dataTable = db.GetTableRecords(objParams, "AV_CustomerSkuSIMSummary_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        SkusEsnList skuESN = new SkusEsnList();
                        skuESN.ProductCode = clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false) as string;
                        skuESN.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                        skuESN.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        //skuESN.TotalESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TotalEsncount", 0, false));
                        skuESN.UnusedESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UnusedESN", 0, false));
                        skuESN.UsedEsnProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UsedEsnProcessed", 0, false));
                        skuESN.UsedEsnShipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UsedEsnShipped", 0, false));
                        skuESN.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                        skuESN.RmaESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "esnCount", 0, false));
                        skuESN.OpeningBalance = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "EsnOpeningBalance", 0, false));
                        skuESN.NewESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "NewEsnCount", 0, false));

                        skuESN.AvailableESN = skuESN.NewESN + skuESN.OpeningBalance;

                        skuEsnList.Add(skuESN);


                    }
                }
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
            return skuEsnList;
        }

        //public static List<SkusEsnList> GetCustomerSkuEsnSummary(int companyID, DateTime fromDate, DateTime toDate)
        //{
        //    DBConnect db = new DBConnect();
        //    List<SkusEsnList> skuEsnList = new List<SkusEsnList>();
        //    string[] arrSpFieldSeq;

        //    Hashtable objParams = new Hashtable();

        //    DataTable dataTable = new DataTable();

        //    try
        //    {

        //        objParams.Add("@CompanyID", companyID);
        //        objParams.Add("@FromDate", fromDate.ToShortDateString());
        //        objParams.Add("@ToDate", toDate.ToShortDateString());

        //        arrSpFieldSeq =
        //        new string[] { "@CompanyID", "@FromDate", "@ToDate" };


        //        dataTable = db.GetTableRecords(objParams, "AV_CustomerSkuEsnSummary_Select", arrSpFieldSeq);
        //        if (dataTable != null && dataTable.Rows.Count > 0)
        //        {
        //            foreach (DataRow dataRow in dataTable.Rows)
        //            {
        //                SkusEsnList skuESN = new SkusEsnList();
        //                skuESN.ProductCode = clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false) as string;
        //                skuESN.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
        //                skuESN.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
        //                //skuESN.TotalESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TotalEsncount", 0, false));
        //                skuESN.UnusedESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UnusedESN", 0, false));
        //                skuESN.UsedEsnProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UsedEsnProcessed", 0, false));
        //                skuESN.UsedEsnShipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UsedEsnShipped", 0, false));
        //                skuESN.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
        //                skuESN.RmaESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "esnCount", 0, false));

        //                skuEsnList.Add(skuESN);


        //            }
        //        }
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
        //    return skuEsnList;
        //}

        public static List<SkusEsnList> GetCustomerSkuEsnSummary(int companyID, int duration)
        {
            DBConnect db = new DBConnect();
            List<SkusEsnList> skuEsnList = new List<SkusEsnList>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@Duration", duration);

                arrSpFieldSeq =
                new string[] { "@CompanyID", "@Duration" };


                dataTable = db.GetTableRecords(objParams, "AV_CustomerSkuEsnSummary_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        SkusEsnList skuESN = new SkusEsnList();
                        skuESN.ProductCode = clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false) as string;
                        skuESN.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                        skuESN.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        //skuESN.TotalESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TotalEsncount", 0, false));
                        skuESN.UnusedESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UnusedESN", 0, false));
                        skuESN.UsedEsnProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UsedEsnProcessed", 0, false));
                        skuESN.UsedEsnShipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UsedEsnShipped", 0, false));
                        skuESN.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                        skuESN.RmaESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "esnCount", 0, false));

                        skuEsnList.Add(skuESN);


                    }
                }
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
            return skuEsnList;
        }

        public static List<SkuEsnList> GetSkuEsnSummary(int companyID)
        {
            DBConnect db = new DBConnect();
            List<SkuEsnList> skuEsnList = new List<SkuEsnList>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);

                arrSpFieldSeq =
                new string[] { "@CompanyID" };

                dataTable = db.GetTableRecords(objParams, "AV_SkuEsnSummary_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        SkuEsnList skuESN = new SkuEsnList();
                        skuESN.ProductCode = clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false) as string;
                        skuESN.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                        skuESN.CarrierName = clsGeneral.getColumnData(dataRow, "CarrierName", string.Empty, false) as string;
                        skuESN.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                        skuESN.MakerName = clsGeneral.getColumnData(dataRow, "MakerName", string.Empty, false) as string;
                        skuESN.TotalESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TotalEsncount", 0, false));
                        skuESN.UnusedESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UnusedESN", 0, false));
                        skuESN.UsedESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UsedESN", 0, false));
                        skuESN.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemGUID", 0, false));
                        skuESN.RmaESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "esnCount", 0, false));


                        skuEsnList.Add(skuESN);


                    }
                }
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
            return skuEsnList;
        }
        public static List<MslESN> GetCustomerUnusedEsnList(int companyID, DateTime fromDate, DateTime toDate, bool showAllEsn)
        {
            DBConnect db = new DBConnect();
            List<MslESN> esnList = new List<MslESN>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();
            DataTable dataTable = new DataTable();

            try
            {
                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString());
                objParams.Add("@ShowAllEsn", showAllEsn);

                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@ShowAllEsn" };

                dataTable = db.GetTableRecords(objParams, "av_CustomerUnsedESN_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        MslESN esnObj = new MslESN();
                        esnObj.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        esnObj.ESN = clsGeneral.getColumnData(dataRow, "esn", string.Empty, false) as string;
                        esnObj.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UploadDate", DateTime.MinValue, false));

                        esnList.Add(esnObj);


                    }
                }
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
            return esnList;
        }
        public static List<MslESN> GetCustomerUnusedSIMList(int companyID, DateTime fromDate, DateTime toDate, bool showAllEsn)
        {
            DBConnect db = new DBConnect();
            List<MslESN> esnList = new List<MslESN>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();
            DataTable dataTable = new DataTable();

            try
            {
                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString());
                objParams.Add("@ShowAllEsn", showAllEsn);

                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@ShowAllEsn" };

                dataTable = db.GetTableRecords(objParams, "av_CustomerUnsedSIM_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        MslESN esnObj = new MslESN();
                        esnObj.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        esnObj.ESN = clsGeneral.getColumnData(dataRow, "SIM", string.Empty, false) as string;
                        esnObj.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "SIMUploadDate", DateTime.MinValue, false));

                        esnList.Add(esnObj);


                    }
                }
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
            return esnList;
        }
        public static List<MslESN> GetCustomerUsedSIMList(int companyID, DateTime fromDate, DateTime toDate)
        {
            DBConnect db = new DBConnect();
            List<MslESN> esnList = new List<MslESN>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString());

                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate" };

                dataTable = db.GetTableRecords(objParams, "av_CustomerUsedsim_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        MslESN esnObj = new MslESN();
                        esnObj.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        esnObj.ESN = clsGeneral.getColumnData(dataRow, "SIM", string.Empty, false) as string;
                        esnObj.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "simUploadDate", DateTime.MinValue, false));

                        esnList.Add(esnObj);


                    }
                }
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
            return esnList;
        }
        
        public static List<MslESN> GetCustomerUsedEsnList(int companyID, DateTime fromDate, DateTime toDate)
        {
            DBConnect db = new DBConnect();
            List<MslESN> esnList = new List<MslESN>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString());
                
                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate" };

                dataTable = db.GetTableRecords(objParams, "av_CustomerUsedESN_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        MslESN esnObj = new MslESN();
                        esnObj.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        esnObj.ESN = clsGeneral.getColumnData(dataRow, "esn", string.Empty, false) as string;
                        esnObj.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UploadDate", DateTime.MinValue, false));

                        esnList.Add(esnObj);


                    }
                }
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
            return esnList;
        }
        public static List<MslESN> GetSkusEsnList(int statusID, int itemCompanyGUID, DateTime fromDate, DateTime toDate, bool isESN)
        {
            DBConnect db = new DBConnect();
            List<MslESN> esnList = new List<MslESN>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {


                objParams.Add("@ItemCompanyGUID", itemCompanyGUID);
                objParams.Add("@FromDate", fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString());
                objParams.Add("@StatusID", statusID);


                arrSpFieldSeq = new string[] { "@ItemCompanyGUID", "@FromDate", "@ToDate", "@StatusID" };
                if (isESN)
                    dataTable = db.GetTableRecords(objParams, "av_SkuUsedESN_Select", arrSpFieldSeq);
                else
                    dataTable = db.GetTableRecords(objParams, "av_SkuUsedSIM_Select", arrSpFieldSeq);
                
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        MslESN esnObj = new MslESN();
                        if (isESN)
                        {
                            esnObj.ESN = clsGeneral.getColumnData(dataRow, "esn", string.Empty, false) as string;
                            esnObj.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UploadDate", DateTime.MinValue, false));
                        }
                        else
                        {
                            esnObj.ESN = clsGeneral.getColumnData(dataRow, "sim", string.Empty, false) as string;
                            esnObj.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "simUploadDate", DateTime.MinValue, false));
                        }
                        esnList.Add(esnObj);


                    }
                }
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
            return esnList;
        }

        public static List<MslESN> GetSkusRmaEsnList(int statusID, int itemCompanyGUID, DateTime fromDate, DateTime toDate, bool isESN)
        {
            DBConnect db = new DBConnect();
            List<MslESN> esnList = new List<MslESN>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {


                objParams.Add("@ItemCompanyGUID", itemCompanyGUID);
                objParams.Add("@FromDate", fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString());
                objParams.Add("@StatusID", statusID);


                arrSpFieldSeq = new string[] { "@ItemCompanyGUID", "@FromDate", "@ToDate", "@StatusID" };
                if (isESN)
                    dataTable = db.GetTableRecords(objParams, "av_SkuRMAESN_Select", arrSpFieldSeq);
                else
                    dataTable = db.GetTableRecords(objParams, "av_SkuRmaSIM_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        MslESN esnObj = new MslESN();
                        if (isESN)
                        {
                            esnObj.ESN = clsGeneral.getColumnData(dataRow, "esn", string.Empty, false) as string;
                            esnObj.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UploadDate", DateTime.MinValue, false));
                        }
                        else
                        {
                            esnObj.ESN = clsGeneral.getColumnData(dataRow, "SIM", string.Empty, false) as string;
                            esnObj.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "SIMUploadDate", DateTime.MinValue, false));
                        }
                        esnList.Add(esnObj);


                    }
                }
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
            return esnList;
        }

        public static List<MslESN> GetEsnList(int itemGUID, int itemCompanyGUID, DateTime fromDate, DateTime toDate)
        {
            DBConnect db = new DBConnect();
            List<MslESN> esnList = new List<MslESN>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@ItemGUID", itemGUID);
                objParams.Add("@ItemCompanyGUID", itemCompanyGUID);
                objParams.Add("@FromDate", fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString());
                //objParams.Add("@ShowAllUnusedESN", showAllUnusedESN);


                arrSpFieldSeq = new string[] { "@ItemGUID", "@ItemCompanyGUID", "@FromDate", "@ToDate" };

                dataTable = db.GetTableRecords(objParams, "av_ProductUnsedESN_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        MslESN esnObj = new MslESN();
                        esnObj.ESN = clsGeneral.getColumnData(dataRow, "esn", string.Empty, false) as string;
                        esnObj.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UploadDate", DateTime.MinValue, false));

                        esnList.Add(esnObj);


                    }
                }
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
            return esnList;
        }


        public FulfillmentOrderStatusResponse GetCustomerFulfillmentStatuses(FulfillmentOrderStatusRequest serviceRequest)
        {
            int companyID, timeInterval;
            companyID = timeInterval = 0;

            timeInterval = Convert.ToInt32(serviceRequest.Duration);
            FulfillmentOrderStatusResponse serviceResponse = new FulfillmentOrderStatusResponse();
            Exception exc = null;

            try
            {
                int userID = avii.Classes.PurchaseOrder.AuthenticateRequest(serviceRequest.Authentication, out exc);


                if (userID > 0 && timeInterval > 0)
                {

                    List<FulfillmentStatusReport> fulfillmentStatusReportList = GetCustomerFulfillmentStatusReport(companyID, timeInterval, userID);
                    if (fulfillmentStatusReportList != null)
                    {
                        serviceResponse.FulfillmentStatusList = fulfillmentStatusReportList;
                        serviceResponse.Comment = string.Empty;
                        serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();

                    }
                    else
                    {
                        serviceResponse.FulfillmentStatusList = null;
                        serviceResponse.Comment = string.Empty;
                        serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();

                    }


                }
                else
                {
                    if (userID == 0)
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                    else
                    {
                        if (timeInterval == 0)
                        {
                            serviceResponse.Comment = "Duration cannot be zero";
                            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                serviceResponse.FulfillmentStatusList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();

            }

            return serviceResponse;
        }

        public RMAStatusResponse GetCustomerRmaStatuses(RMAStatusRequest serviceRequest)
        {
            RMAStatusResponse serviceResponse = new RMAStatusResponse();
            int companyID, timeInterval;
            companyID = timeInterval = 0;
            Exception exc = null;

            timeInterval = Convert.ToInt32(serviceRequest.Duration);

            try
            {
                int userID = avii.Classes.PurchaseOrder.AuthenticateRequest(serviceRequest.Authentication, out exc);


                if (userID > 0 && timeInterval > 0)
                {

                    List<CompanyRmaStatuses> rmaStatusList = GetCustomerRmaStatusReport(companyID, timeInterval, userID);
                    if (rmaStatusList != null && rmaStatusList.Count > 0)
                    {
                        serviceResponse.RMAStatusList = rmaStatusList;
                        serviceResponse.Comment = string.Empty;
                        serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();

                    }
                    else
                    {
                        serviceResponse.RMAStatusList = null;
                        serviceResponse.Comment = string.Empty;
                        serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();

                    }


                }
                else
                {
                    if (userID == 0)
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                    else
                    {
                        if (timeInterval == 0)
                        {
                            serviceResponse.Comment = "Duration cannot be zero";
                            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                serviceResponse.RMAStatusList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();

            }

            return serviceResponse;
        }

        public ProductRmaReasonsResponse GetProductRmaReasons(ProductRmaReasonsRequest serviceRequest)
        {
            ProductRmaReasonsResponse serviceResponse = new ProductRmaReasonsResponse();
            int timeInterval = 0;
            string companyAccountNumber = string.Empty;
            Exception exc = null;


            try
            {
                timeInterval = Convert.ToInt32(serviceRequest.Duration);
                int userID = avii.Classes.PurchaseOrder.AuthenticateRequest(serviceRequest.Authentication, out exc);


                if (userID > 0 && timeInterval > 0)
                {

                    List<ProductRmaReason> productRmaReason = GetProductRMAReasonSummary(companyAccountNumber, timeInterval, userID);
                    if (productRmaReason != null && productRmaReason.Count > 0)
                    {
                        serviceResponse.ProductRmaReason = productRmaReason;
                        serviceResponse.Comment = string.Empty;
                        serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();

                    }
                    else
                    {
                        serviceResponse.ProductRmaReason = null;
                        serviceResponse.Comment = string.Empty;
                        serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();

                    }


                }
                else
                {
                    if (userID == 0)
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                    else
                    {
                        if (timeInterval == 0)
                        {
                            serviceResponse.Comment = "Duration cannot be zero";
                            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                serviceResponse.ProductRmaReason = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();

            }

            return serviceResponse;
        }


        public static List<RmaStatus> GetRmaStatusesSummary(int reasonID, string productName, string companyAccountNumber, int timeInterval)
        {
            List<RmaStatus> rmaSummaryList = new List<RmaStatus>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@ProductName", productName);
                objCompHash.Add("@ReasonID", reasonID);
                objCompHash.Add("@CompanyAccountNumber", companyAccountNumber);
                objCompHash.Add("@TimeInterval", timeInterval);


                arrSpFieldSeq = new string[] { "@ProductName", "@ReasonID", "@CompanyAccountNumber", "@TimeInterval" };

                dataTable = db.GetTableRecords(objCompHash, "av_RMA_Statuses_Summary", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RmaStatus rmaSummary = new RmaStatus();

                        rmaSummary.Count = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaCount", 0, false));
                        rmaSummary.StatusName = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;

                        rmaSummaryList.Add(rmaSummary);
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

            return rmaSummaryList;
        }

        public static List<ProductRmaReason> GetProductRMAReasonSummary(string companyAccountNumber, int timeInterval, int userID)
        {
            List<ProductRmaReason> rmaSummaryList = new List<ProductRmaReason>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyAccountNumber", companyAccountNumber);
                objCompHash.Add("@TimeInterval", timeInterval);
                objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@CompanyAccountNumber", "@TimeInterval", "@UserID" };

                dataTable = db.GetTableRecords(objCompHash, "av_Customer_RMA_Reason_Summary", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        ProductRmaReason rmaSummary = new ProductRmaReason();

                        rmaSummary.ActivationIssues = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ActivationIssues", 0, false));
                        rmaSummary.AudioIssues = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AudioIssues", 0, false));
                        rmaSummary.BuyerRemorse = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "BuyerRemorse", 0, false));
                        rmaSummary.CoverageIssues = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CoverageIssues", 0, false));
                        rmaSummary.DOA = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DOA", 0, false));
                        rmaSummary.DropCalls = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DropCalls", 0, false));
                        rmaSummary.HardwareIssues = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "HardwareIssues", 0, false));
                        rmaSummary.LiquidDamage = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LiquidDamage", 0, false));
                        rmaSummary.LoanerProgram = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LoanerProgram", 0, false));
                        rmaSummary.MissingParts = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MissingParts", 0, false));

                        rmaSummary.Others = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Others", 0, false));
                        rmaSummary.PhysicalAbuse = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PhysicalAbuse", 0, false));
                        rmaSummary.PowerIssues = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PowerIssues", 0, false));
                        rmaSummary.ReturnToStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ReturnToStock", 0, false));
                        rmaSummary.ShippingError = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShippingError", 0, false));
                        rmaSummary.ScreenIssues = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ScreenIssues", 0, false));
                        rmaSummary.Software = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Software", 0, false));
                        rmaSummary.ProductName = clsGeneral.getColumnData(dataRow, "Product Name", string.Empty, false) as string; //+ "(" + clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false) as string +")";
                        //rmaSummary.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string + "(" + clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false) as string + ")";
                        rmaSummary.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));

                        rmaSummaryList.Add(rmaSummary);
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

            return rmaSummaryList;
        }
        public static List<StoreFulfillmentStatus> GetCustomerStoreFulfillmentStatusReport(int companyID, string companyName, int timeInterval, int userID)
        {
            List<StoreFulfillmentStatus> fulfillmentStatusReportList = new List<StoreFulfillmentStatus>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@CompanyName", companyName);
                objCompHash.Add("@TimeInterval", timeInterval);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@CompanyID", "@CompanyName", "@TimeInterval", "@UserID" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Storewise_Fulfillment_Report", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        StoreFulfillmentStatus objFulfillmentStatusReport = new StoreFulfillmentStatus();

                        objFulfillmentStatusReport.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                        objFulfillmentStatusReport.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                        objFulfillmentStatusReport.InProcess = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "In Process", 0, false));
                        objFulfillmentStatusReport.Processed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Processed", 0, false));
                        objFulfillmentStatusReport.Shipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Shipped", 0, false));
                        objFulfillmentStatusReport.Closed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Closed", 0, false));
                        objFulfillmentStatusReport.Return = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Return", 0, false));
                        objFulfillmentStatusReport.OnHold = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "On Hold", 0, false));
                        objFulfillmentStatusReport.OutofStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Out of Stock", 0, false));
                        objFulfillmentStatusReport.Cancel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancel", 0, false));
                        objFulfillmentStatusReport.PartialProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Partial Processed", 0, false));

                        objFulfillmentStatusReport.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;

                        fulfillmentStatusReportList.Add(objFulfillmentStatusReport);
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

            return fulfillmentStatusReportList;
        }

        public static List<FulfillmentStatusReport> GetCustomerFulfillmentStatusReport(int companyID, int timeInterval, int userID)
        {
            List<FulfillmentStatusReport> fulfillmentStatusReportList = new List<FulfillmentStatusReport>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@TimeInterval", timeInterval);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@CompanyID", "@TimeInterval", "@UserID" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_Fulfillment_Report", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        FulfillmentStatusReport objFulfillmentStatusReport = new FulfillmentStatusReport();

                        objFulfillmentStatusReport.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                        objFulfillmentStatusReport.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                        objFulfillmentStatusReport.InProcess = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "In Process", 0, false));
                        objFulfillmentStatusReport.Processed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Processed", 0, false));
                        objFulfillmentStatusReport.Shipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Shipped", 0, false));
                        objFulfillmentStatusReport.Closed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Closed", 0, false));
                        objFulfillmentStatusReport.Return = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Return", 0, false));
                        objFulfillmentStatusReport.OnHold = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "On Hold", 0, false));
                        objFulfillmentStatusReport.OutofStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Out of Stock", 0, false));
                        objFulfillmentStatusReport.Cancel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancel", 0, false));
                        objFulfillmentStatusReport.PartialProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Partial Processed", 0, false));

                        objFulfillmentStatusReport.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;

                        fulfillmentStatusReportList.Add(objFulfillmentStatusReport);
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

            return fulfillmentStatusReportList;
        }
        public static List<FulfillmentStatusReport> GetCustomerFulfillmentStatusSummary(int companyID, string fromDate, string toDate, int userID)
        {
            List<FulfillmentStatusReport> fulfillmentStatusReportList = new List<FulfillmentStatusReport>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", fromDate);
                objCompHash.Add("@ToDate", toDate);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@UserID" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_Fulfillment_Summary", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        FulfillmentStatusReport objFulfillmentStatusReport = new FulfillmentStatusReport();

                        objFulfillmentStatusReport.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                        objFulfillmentStatusReport.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                        objFulfillmentStatusReport.InProcess = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "In Process", 0, false));
                        objFulfillmentStatusReport.Processed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Processed", 0, false));
                        objFulfillmentStatusReport.Shipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Shipped", 0, false));
                        objFulfillmentStatusReport.Closed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Closed", 0, false));
                        objFulfillmentStatusReport.Return = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Return", 0, false));
                        objFulfillmentStatusReport.OnHold = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "On Hold", 0, false));
                        objFulfillmentStatusReport.OutofStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Out of Stock", 0, false));
                        objFulfillmentStatusReport.Cancel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancel", 0, false));
                        objFulfillmentStatusReport.PartialProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Partial Processed", 0, false));

                        objFulfillmentStatusReport.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;

                        fulfillmentStatusReportList.Add(objFulfillmentStatusReport);
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

            return fulfillmentStatusReportList;
        }

        public static List<FulfillmentSKUStatus> GetCustomerFulfillmentSKUStatusReport(int companyID, int timeInterval, int userID)
        {
            List<FulfillmentSKUStatus> fulfillmentStatusReportList = new List<FulfillmentSKUStatus>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@TimeInterval", timeInterval);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@CompanyID", "@TimeInterval", "@UserID" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_Fulfillment_SKU_Report", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        FulfillmentSKUStatus objFulfillmentStatusReport = new FulfillmentSKUStatus();

                        objFulfillmentStatusReport.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                        objFulfillmentStatusReport.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                        objFulfillmentStatusReport.InProcess = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "In Process", 0, false));
                        objFulfillmentStatusReport.Processed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Processed", 0, false));
                        objFulfillmentStatusReport.Shipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Shipped", 0, false));
                        objFulfillmentStatusReport.Closed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Closed", 0, false));
                        objFulfillmentStatusReport.Return = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Return", 0, false));
                        objFulfillmentStatusReport.OnHold = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "On Hold", 0, false));
                        objFulfillmentStatusReport.OutofStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Out of Stock", 0, false));
                        objFulfillmentStatusReport.Cancel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancel", 0, false));
                        objFulfillmentStatusReport.PartialProcessed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Partial Processed", 0, false));

                        objFulfillmentStatusReport.SKU = clsGeneral.getColumnData(dataRow, "Item_Code", string.Empty, false) as string;

                        fulfillmentStatusReportList.Add(objFulfillmentStatusReport);
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

            return fulfillmentStatusReportList;
        }

        public static List<RmaEsn> GetCustomerRmaEsn(int statusID, int companyID, int timeInterval)
        {
            List<RmaEsn> rmaEsnList = new List<RmaEsn>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@StatusID", statusID);
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@TimeInterval", timeInterval);


                arrSpFieldSeq = new string[] { "@StatusID", "@CompanyID", "@TimeInterval" };


                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_RMA_ESN", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RmaEsn rmaESN = new RmaEsn();


                        rmaESN.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        rmaESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;

                        rmaEsnList.Add(rmaESN);
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

            return rmaEsnList;
        }

        public static List<RmaEsnStatuses> GetCustomerRmaEsnStatusReport(int companyID, int timeInterval, int esnStatusID, int rmaStatusID)
        {
            List<RmaEsnStatuses> rmaStatusList = new List<RmaEsnStatuses>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@TimeInterval", timeInterval);
                objCompHash.Add("@RmaStatusID", rmaStatusID);
                objCompHash.Add("@EsnStatusID", esnStatusID);
                //objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@CompanyID", "@TimeInterval", "@RmaStatusID", "@EsnStatusID" };


                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_RMA_ESN_Report", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RmaEsnStatuses rmaESN = new RmaEsnStatuses();

                        rmaESN.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false));

                        rmaESN.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        rmaESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        rmaESN.RmaStatus = clsGeneral.getColumnData(dataRow, "RmaStatus", string.Empty, false) as string;
                        rmaESN.EsnStatus = clsGeneral.getColumnData(dataRow, "EsnStatus", string.Empty, false) as string;

                        rmaStatusList.Add(rmaESN);
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

            return rmaStatusList;
        }
        public static List<RmaEsnStatuses> GetCustomerRmaEsnStatusReport(int companyID, DateTime fromDate, DateTime toDate, int esnStatusID, int rmaStatusID)
        {
            List<RmaEsnStatuses> rmaStatusList = new List<RmaEsnStatuses>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", fromDate.ToShortDateString());
                objCompHash.Add("@ToDate", toDate.ToShortDateString());
                objCompHash.Add("@RmaStatusID", rmaStatusID);
                objCompHash.Add("@EsnStatusID", esnStatusID);
                //objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@RmaStatusID", "@EsnStatusID" };


                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_RMA_ESN_Report", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RmaEsnStatuses rmaESN = new RmaEsnStatuses();

                        rmaESN.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false));

                        rmaESN.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        rmaESN.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                        rmaESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        rmaESN.RmaStatus = clsGeneral.getColumnData(dataRow, "RmaStatus", string.Empty, false) as string;
                        rmaESN.EsnStatus = clsGeneral.getColumnData(dataRow, "EsnStatus", string.Empty, false) as string;
                        rmaESN.AVPO = clsGeneral.getColumnData(dataRow, "AVPO", string.Empty, false) as string;
                        
                        rmaStatusList.Add(rmaESN);
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

            return rmaStatusList;
        }
        public static List<RMAEsnDetail> GetRmaEsnOnlyReport(int companyID, string RMANumber, string ESN, DateTime fromDate, DateTime toDate, int esnStatusID, int rmaStatusID)
        {
            List<RMAEsnDetail> rmaList = new List<RMAEsnDetail>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@RmaNumber", RMANumber);
                objCompHash.Add("@ESN", ESN);

                objCompHash.Add("@FromDate", fromDate.ToShortDateString());
                objCompHash.Add("@ToDate", toDate.ToShortDateString());
                objCompHash.Add("@RmaStatusID", rmaStatusID);
                objCompHash.Add("@EsnStatusID", esnStatusID);
                //objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@CompanyID", "@RmaNumber", "@ESN", "@FromDate", "@ToDate", "@RmaStatusID", "@EsnStatusID" };


                dataTable = db.GetTableRecords(objCompHash, "AV_RMA_ESN_Detail_Report", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RMAEsnDetail rmaESN = new RMAEsnDetail();

                        rmaESN.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false)).ToString("MM/dd/yyyy");
                        rmaESN.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_NUM", string.Empty, false) as string;
                        rmaESN.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;

                        rmaESN.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        rmaESN.BatchNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
                        rmaESN.ICCID = clsGeneral.getColumnData(dataRow, "ICCID", string.Empty, false) as string;
                        rmaESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        rmaESN.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                        rmaESN.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;

                        rmaESN.RmaStatus = clsGeneral.getColumnData(dataRow, "RmaStatus", string.Empty, false) as string;
                        rmaESN.EsnStatus = clsGeneral.getColumnData(dataRow, "EsnStatus", string.Empty, false) as string;

                        rmaESN.TriageDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "TriageDate", DateTime.MinValue, false)).ToString("MM/dd/yyyy");
                        if (rmaESN.TriageDate == "01-01-0001" || rmaESN.TriageDate == "01/01/0001")
                            rmaESN.TriageDate = "";

                        rmaESN.TriageStatus = clsGeneral.getColumnData(dataRow, "TriageStatus", string.Empty, false) as string;
                        rmaESN.Reason = clsGeneral.getColumnData(dataRow, "RMAReason", string.Empty, false) as string;
                        rmaESN.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;

                        rmaList.Add(rmaESN);
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

            return rmaList;
        }
        public static List<RMAESNDetail> GetRmaEsnDetailReport(int companyID, string RMANumber, string ESN, DateTime fromDate, DateTime toDate, int esnStatusID, int rmaStatusID)
        {
            List<RMAESNDetail> rmaList = new List<RMAESNDetail>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@RmaNumber", RMANumber);
                objCompHash.Add("@ESN", ESN);

                objCompHash.Add("@FromDate", fromDate.ToShortDateString());
                objCompHash.Add("@ToDate", toDate.ToShortDateString());
                objCompHash.Add("@RmaStatusID", rmaStatusID);
                objCompHash.Add("@EsnStatusID", esnStatusID);
                //objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@CompanyID", "@RmaNumber", "@ESN", "@FromDate", "@ToDate", "@RmaStatusID", "@EsnStatusID" };


                dataTable = db.GetTableRecords(objCompHash, "AV_RMA_ESN_Detail_Report", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RMAESNDetail rmaESN = new RMAESNDetail();

                        rmaESN.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false)).ToString("MM/dd/yyyy");

                        rmaESN.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        rmaESN.BatchNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
                        rmaESN.ICCID = clsGeneral.getColumnData(dataRow, "ICCID", string.Empty, false) as string;
                        rmaESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        rmaESN.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                        rmaESN.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                        

                        rmaESN.RmaStatus = clsGeneral.getColumnData(dataRow, "RmaStatus", string.Empty, false) as string;

                        rmaESN.TriageDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "TriageDate", DateTime.MinValue, false)).ToString("MM/dd/yyyy");
                        if (rmaESN.TriageDate == "01-01-0001" || rmaESN.TriageDate == "01/010001")
                            rmaESN.TriageDate = "";

                        rmaESN.TriageStatus = clsGeneral.getColumnData(dataRow, "TriageStatus", string.Empty, false) as string;
                        rmaESN.Reason = clsGeneral.getColumnData(dataRow, "RMAReason", string.Empty, false) as string;
                        rmaESN.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;

                        rmaESN.CustomerName = clsGeneral.getColumnData(dataRow, "RmaContactName", string.Empty, false) as string;
                        rmaESN.Address = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                        rmaESN.City = clsGeneral.getColumnData(dataRow, "ContactCity", string.Empty, false) as string;
                        rmaESN.State = clsGeneral.getColumnData(dataRow, "ContactState", string.Empty, false) as string;
                        rmaESN.Zip = clsGeneral.getColumnData(dataRow, "ContactZip", string.Empty, false) as string;
                       // rmaESN.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;

                        rmaList.Add(rmaESN);
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

            return rmaList;
        }

        public static List<EsnRepositoryDetail> GetCustomerEsnRepositoryDetail(int companyID, int itemCompanyGUID, DateTime fromDate, DateTime toDate, bool unUsedESN, bool showAllUnusedESN)
        {
            List<EsnRepositoryDetail> esnList = new List<EsnRepositoryDetail>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@ItemCompanyGUID", itemCompanyGUID);
                objCompHash.Add("@FromDate", fromDate.ToShortDateString() == "1/1/0001" ? DateTime.Now.AddDays(-365).ToShortDateString() : fromDate.ToShortDateString());
                objCompHash.Add("@ToDate", toDate.ToShortDateString() == "1/1/0001" ? DateTime.Now.ToShortDateString() : toDate.ToShortDateString());
                objCompHash.Add("@UnUsedESN", unUsedESN);
                objCompHash.Add("@ShowAllUnusedESN", showAllUnusedESN);

                arrSpFieldSeq = new string[] { "@CompanyID", "@ItemCompanyGUID", "@FromDate", "@ToDate", "@UnUsedESN", "@ShowAllUnusedESN" };

                dataTable = db.GetTableRecords(objCompHash, "AV_ESN_Repository_Detail", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        EsnRepositoryDetail esnObj = new EsnRepositoryDetail();

                        esnObj.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        esnObj.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                        esnObj.UploadDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UploadDate", DateTime.MinValue, false));
                        esnObj.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                        esnObj.FulfillmentStatus = clsGeneral.getColumnData(dataRow, "PoStatus", string.Empty, false) as string;
                        esnObj.FulfillmentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.MinValue, false));
                        esnObj.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.MinValue, false));                        

                        esnObj.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false));
                        esnObj.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        esnObj.RmaStatus = clsGeneral.getColumnData(dataRow, "RmaStatus", string.Empty, false) as string;
                        esnObj.RmaEsnStatus = clsGeneral.getColumnData(dataRow, "RmaEsnStatus", string.Empty, false) as string;
                        esnList.Add(esnObj);
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

            return esnList;
        }

        public static List<CompanyRmaStatuses> GetCustomerRmaStatusReport(int companyID, int timeInterval, int userID)
        {
            List<CompanyRmaStatuses> rmaStatusList = new List<CompanyRmaStatuses>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@TimeInterval", timeInterval);
                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@CompanyID", "@TimeInterval", "@UserID" };


                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_RMA_Report", arrSpFieldSeq);


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CompanyRmaStatuses objFulfillmentStatusReport = new CompanyRmaStatuses();

                        objFulfillmentStatusReport.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                        objFulfillmentStatusReport.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                        objFulfillmentStatusReport.Received = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Received", 0, false));
                        objFulfillmentStatusReport.PendingforRepair = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending for Repair", 0, false));
                        objFulfillmentStatusReport.PendingforCredit = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending for Credit", 0, false));
                        objFulfillmentStatusReport.PendingforReplacement = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending for Replacement", 0, false));
                        objFulfillmentStatusReport.Approved = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Approved", 0, false));
                        objFulfillmentStatusReport.Returned = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Returned", 0, false));
                        objFulfillmentStatusReport.Credited = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Credited", 0, false));
                        objFulfillmentStatusReport.Denied = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Denied", 0, false));
                        objFulfillmentStatusReport.Closed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Closed", 0, false));

                        objFulfillmentStatusReport.OutwithOEMforrepair = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Out with OEM for repair", 0, false));
                        objFulfillmentStatusReport.BacktoStockNDF = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Back to Stock -NDF", 0, false));
                        objFulfillmentStatusReport.BacktoStockCredited = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Back to Stock- Credited", 0, false));
                        objFulfillmentStatusReport.BacktoStockReplacedbyOEM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Back to Stock – Replaced by OEM", 0, false));
                        objFulfillmentStatusReport.RepairedbyOEM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Repaired by OEM", 0, false));
                        objFulfillmentStatusReport.RepairedByAV = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Repaired By AV", 0, false));
                        objFulfillmentStatusReport.ReplacedBYAV = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Replaced BY AV", 0, false));
                        objFulfillmentStatusReport.ReplacedBYOEM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Replaced BY OEM", 0, false));
                        objFulfillmentStatusReport.NDFNoDefectFound = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "NDF (No Defect Found)", 0, false));
                        objFulfillmentStatusReport.PREOWNEDAstock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PRE-OWNED – A stock", 0, false));
                        objFulfillmentStatusReport.PREOWENDBStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PRE-OWEND - B Stock", 0, false));

                        //objFulfillmentStatusReport.OutwithOEMforrepair = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        //objFulfillmentStatusReport.BacktoStockNDF = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                        //objFulfillmentStatusReport.BacktoStockCredited = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "In Process", 0, false));
                        //objFulfillmentStatusReport.BacktoStockReplacedbyOEM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Processed", 0, false));
                        //objFulfillmentStatusReport.RepairedbyOEM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Shipped", 0, false));
                        //objFulfillmentStatusReport.RepairedByAV = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Closed", 0, false));
                        //objFulfillmentStatusReport.ReplacedBYAV = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Return", 0, false));
                        //objFulfillmentStatusReport.ReplacedBYOEM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "On Hold", 0, false));
                        //objFulfillmentStatusReport.NDFNoDefectFound = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Out of Stock", 0, false));
                        //objFulfillmentStatusReport.PREOWNEDAstock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancel", 0, false));
                        //objFulfillmentStatusReport.PREOWENDBStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Partial Processed", 0, false));

                        objFulfillmentStatusReport.PREOWENDCStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PRE-OWEND – C Stock", 0, false));
                        objFulfillmentStatusReport.Rejected = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Rejected", 0, false));
                        objFulfillmentStatusReport.RTSReturnToStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RTS (Return To Stock)", 0, false));
                        objFulfillmentStatusReport.Incomplete = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Incomplete", 0, false));
                        objFulfillmentStatusReport.Damaged = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Damaged", 0, false));
                        objFulfillmentStatusReport.Preowned = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Preowned", 0, false));
                        objFulfillmentStatusReport.ReturntoOEM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Return to OEM", 0, false));
                        objFulfillmentStatusReport.ReturnedtoStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Returned to Stock", 0, false));
                        objFulfillmentStatusReport.Cancel = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancel", 0, false));
                        objFulfillmentStatusReport.ExternalESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "External ESN", 0, false));
                        objFulfillmentStatusReport.PendingshiptoOEM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending ship to OEM", 0, false));
                        objFulfillmentStatusReport.SenttoOEM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Sent to OEM", 0, false));
                        objFulfillmentStatusReport.PendingshiptoSupplier = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending ship to Supplier", 0, false));
                        objFulfillmentStatusReport.SenttoSupplier = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Sent to Supplier", 0, false));
                        objFulfillmentStatusReport.ReturnedfromOEM = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Returned from OEM", 0, false));

                        objFulfillmentStatusReport.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;

                        rmaStatusList.Add(objFulfillmentStatusReport);
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

            return rmaStatusList;
        }

        public static List<CustomerRmaStatus> GetCustomerRmaStatusSummary(int companyID, string fromDate, string  toDate, int userID, string summaryBy)
        {
            List<CustomerRmaStatus> rmaStatusList = new List<CustomerRmaStatus>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", string.IsNullOrEmpty(fromDate) ? null : fromDate);
                objCompHash.Add("@ToDate", string.IsNullOrEmpty(toDate) ? null : toDate);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@SummaryBy", summaryBy);

                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@UserID", "@SummaryBy" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_RMA_ReportNew", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CustomerRmaStatus customerRmaStatus = new CustomerRmaStatus();

                        customerRmaStatus.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                        customerRmaStatus.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                        customerRmaStatus.Cancelled = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancelled", 0, false));
                        customerRmaStatus.Completed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Completed", 0, false));
                        customerRmaStatus.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        rmaStatusList.Add(customerRmaStatus);

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

            return rmaStatusList;
        }
        public static List<CustomerRmaEsnStatus> GetCustomerRmaESNStatusSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            List<CustomerRmaEsnStatus> rmaStatusList = new List<CustomerRmaEsnStatus>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", string.IsNullOrEmpty(fromDate) ? null : fromDate);
                objCompHash.Add("@ToDate", string.IsNullOrEmpty(toDate) ? null : toDate);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@SummaryBy", summaryBy);


                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@UserID", "@SummaryBy" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_RMA_ReportNew", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CustomerRmaEsnStatus customerRmaStatus = new CustomerRmaEsnStatus();

                        customerRmaStatus.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                        customerRmaStatus.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                        customerRmaStatus.Cancelled = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Cancelled", 0, false));
                        customerRmaStatus.Completed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Completed", 0, false));
                        customerRmaStatus.ReturnToStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RTS (Return To Stock)", 0, false));
                        customerRmaStatus.SentToSupplier = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Sent to Supplier", 0, false));
                        customerRmaStatus.ExternalESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "External ESN", 0, false));
                        customerRmaStatus.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        rmaStatusList.Add(customerRmaStatus);

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

            return rmaStatusList;
        }
        public static List<CustomerRmaTriageStatus> GetCustomerRmaTriageStatusSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            List<CustomerRmaTriageStatus> rmaStatusList = new List<CustomerRmaTriageStatus>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", string.IsNullOrEmpty(fromDate) ? null : fromDate);
                objCompHash.Add("@ToDate", string.IsNullOrEmpty(toDate) ? null : toDate);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@SummaryBy", summaryBy);


                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@UserID", "@SummaryBy" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_RMA_ReportNew", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CustomerRmaTriageStatus customerRmaStatus = new CustomerRmaTriageStatus();

                        customerRmaStatus.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                        customerRmaStatus.Pending = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pending", 0, false));
                        customerRmaStatus.NotRequired = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Not Required", 0, false));
                        customerRmaStatus.Complete = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Complete", 0, false));
                        customerRmaStatus.InProcess = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "In-Process", 0, false));
                        customerRmaStatus.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        rmaStatusList.Add(customerRmaStatus);

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

            return rmaStatusList;
        }
        public static List<CustomerRmaDisposition> GetCustomerRmaDispositionSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            List<CustomerRmaDisposition> rmaStatusList = new List<CustomerRmaDisposition>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", string.IsNullOrEmpty(fromDate) ? null : fromDate);
                objCompHash.Add("@ToDate", string.IsNullOrEmpty(toDate) ? null : toDate);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@SummaryBy", summaryBy);


                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@UserID", "@SummaryBy" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_RMA_ReportNew", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CustomerRmaDisposition customerRmaStatus = new CustomerRmaDisposition();

                        customerRmaStatus.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                        customerRmaStatus.Credit = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Credit", 0, false));
                        customerRmaStatus.Discarded = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Discarded", 0, false));
                        customerRmaStatus.Repair = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Repair", 0, false));
                        customerRmaStatus.Replaced = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Replaced", 0, false));
                        customerRmaStatus.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        rmaStatusList.Add(customerRmaStatus);

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

            return rmaStatusList;
        }
        public static List<CustomerRmaShipmentPaidBy> GetCustomerRmaShipmentPaidBySummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            List<CustomerRmaShipmentPaidBy> rmaStatusList = new List<CustomerRmaShipmentPaidBy>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", string.IsNullOrEmpty(fromDate) ? null : fromDate);
                objCompHash.Add("@ToDate", string.IsNullOrEmpty(toDate) ? null : toDate);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@SummaryBy", summaryBy);


                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@UserID", "@SummaryBy" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_RMA_ReportNew", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CustomerRmaShipmentPaidBy customerRmaStatus = new CustomerRmaShipmentPaidBy();

                        customerRmaStatus.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                        customerRmaStatus.Customer = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Customer", 0, false));
                        customerRmaStatus.Internal = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Internal", 0, false));
                        customerRmaStatus.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        rmaStatusList.Add(customerRmaStatus);

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

            return rmaStatusList;
        }

        public static List<CustomerRmaReason> GetCustomerRmaReasonSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            List<CustomerRmaReason> rmaStatusList = new List<CustomerRmaReason>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FromDate", string.IsNullOrEmpty(fromDate) ? null : fromDate);
                objCompHash.Add("@ToDate", string.IsNullOrEmpty(toDate) ? null : toDate);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@SummaryBy", summaryBy);


                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@UserID", "@SummaryBy" };

                dataTable = db.GetTableRecords(objCompHash, "AV_Customer_RMA_ReportNew", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CustomerRmaReason customerRmaStatus = new CustomerRmaReason();

                        customerRmaStatus.Total = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "zzTotal", 0, false));
                        customerRmaStatus.ActivationCoverage = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "[Activation/Coverage]", 0, false));
                        customerRmaStatus.BatteryPower = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "[Battery/Power]", 0, false));
                        //customerRmaStatus.BuyerRemorse = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "BuyerRemorse", 0, false));
                        //customerRmaStatus.CoverageIssues = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CoverageIssues", 0, false));
                        //customerRmaStatus.DOA = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DOA", 0, false));
                        //customerRmaStatus.DropCalls = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DropCalls", 0, false));
                        customerRmaStatus.HardwareIssues = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "HardwareIssues", 0, false));
                        customerRmaStatus.LiquidDamage = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LiquidDamage", 0, false));
                        //customerRmaStatus.LoanerProgram = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LoanerProgram", 0, false));
                        customerRmaStatus.MissingParts = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MissingParts", 0, false));
                        customerRmaStatus.Others = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Others", 0, false));
                        customerRmaStatus.PhysicalAbuse = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PhysicalAbuse", 0, false));
                        //customerRmaStatus.PowerIssues = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PowerIssues", 0, false));
                        //customerRmaStatus.ReturnToStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ReturnToStock", 0, false));

                        //customerRmaStatus.ScreenIssues = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ScreenIssues", 0, false));
                        //customerRmaStatus.ShippingError = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShippingError", 0, false));
                        customerRmaStatus.Software = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Software", 0, false));
                        customerRmaStatus.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        rmaStatusList.Add(customerRmaStatus);

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

            return rmaStatusList;
        }

        public static DataTable GetCustomerRmaStatusReportdt(int companyID, int timeInterval)
        {

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@TimeInterval", timeInterval);


                arrSpFieldSeq = new string[] { "@CompanyID", "@TimeInterval" };


                dt = db.GetTableRecords(objCompHash, "AV_Customer_RMA_Report", arrSpFieldSeq);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return dt;
        }

        public static DataTable GetCompanyList(int companyID)
        {
            DataTable dataTable = new DataTable();
            if (!(dataTable != null && dataTable.Rows.Count > 0))
            {
                dataTable = clsCompany.GetCompany(companyID, 0);
            }

            return dataTable;
        }

        public static DataTable GetFulfillmentLogReport(string poNumber, string sku, string fromDate, string toDate, int companyID)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@PoNum", poNumber);
                objParams.Add("@SKU", sku);
                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);
                objParams.Add("@CompanyID", companyID);


                arrSpFieldSeq =
                new string[] { "@PoNum", "@SKU", "@FromDate", "@ToDate", "@CompanyID" };

                logTable = db.GetTableRecords(objParams, "av_FulfillmentLog_SELECT", arrSpFieldSeq);

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
            return logTable;
        }
        public static void DeleteFulfillmentReport(int fulfillmentLogID)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@fulfillmentLogID", fulfillmentLogID);



                arrSpFieldSeq = new string[] { "@fulfillmentLogID" };
                objDB.ExeCommand(objCompHash, "av_FulfillmentLog_DELETE", arrSpFieldSeq);
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
        public static DataTable GetUploadLogReport(string fromDate, string toDate, string moduleName, int statusID)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@UploadDateFrom", fromDate);
                objParams.Add("@UploadDateTo", toDate);
                objParams.Add("@ModuleName", moduleName);
                objParams.Add("@StatusID", statusID);


                arrSpFieldSeq =
                new string[] { "@UploadDateFrom", "@UploadDateTo", "@ModuleName", "@StatusID" };

                logTable = db.GetTableRecords(objParams, "av_UploadTasks_Select", arrSpFieldSeq);

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
            return logTable;
        }
        public static void DeleteUploadReport(int uploadID)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@UploadID", uploadID);



                arrSpFieldSeq = new string[] { "@UploadID" };
                objDB.ExeCommand(objCompHash, "av_UploadTasks_Delete", arrSpFieldSeq);
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

        public static List<LoginUser> GetUserLoginLogReport(string userName, int companyID, string fromDate, string toDate)
        {
            List<LoginUser> userList = new List<LoginUser>();
            LoginUser loginUser = null;
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@UserName", userName);
                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);


                arrSpFieldSeq =
                new string[] { "@UserName", "@CompanyID", "@FromDate", "@ToDate" };

                logTable = db.GetTableRecords(objParams, "av_UserSignIn_SELECT", arrSpFieldSeq);
                if(logTable != null && logTable.Rows.Count > 0)
                {
                    foreach(DataRow row in logTable.Rows)
                    {
                        loginUser = new LoginUser();
                        loginUser.SignInID = Convert.ToInt32(clsGeneral.getColumnData(row, "SignInID", 0, false));
                        loginUser.CompanyName = Convert.ToString(clsGeneral.getColumnData(row, "CompanyName", string.Empty, false));
                        loginUser.UserName = Convert.ToString(clsGeneral.getColumnData(row, "UserName", string.Empty, false));
                        loginUser.SessionStartDate = Convert.ToString(clsGeneral.getColumnData(row, "SessionStartDate", string.Empty, false));
                        loginUser.SessionEndDate = Convert.ToString(clsGeneral.getColumnData(row, "SessionEndDate", string.Empty, false));

                        // loginUser.SessionStartDate = Convert.ToString(clsGeneral.getColumnData(row, "CompanyName", string.Empty, false));

                        //loginuserUser.SessionEndDate = Convert.ToString(clsGeneral.getColumnData(row, "SessionEndDate", string.Empty, false));
                        userList.Add(loginUser);

                    }
                }
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
            return userList;
        }
        public static void DeleteLoginReport(int signInID)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@SignInID", signInID);



                arrSpFieldSeq = new string[] { "@SignInID" };
                objDB.ExeCommand(objCompHash, "av_loginLog_DELETE", arrSpFieldSeq);
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


        //
        public static DataTable GetAuditESNLogReport(string esn, string module, string fromDate, string toDate)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@ESN", esn);
                objParams.Add("@Module", module);
                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);



                arrSpFieldSeq =
                new string[] { "@ESN", "@Module", "@FromDate", "@ToDate" };

                logTable = db.GetTableRecords(objParams, "av_AuditESNLog_SELECT", arrSpFieldSeq);

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
            return logTable;
        }

        public static void DeleteAuditESNReport(int esnLogGUID)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@EsnLogGUID", esnLogGUID);



                arrSpFieldSeq = new string[] { "@EsnLogGUID" };
                objDB.ExeCommand(objCompHash, "av_EsnMslLog_DELETE", arrSpFieldSeq);
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
        public static DataTable GetMASLogReport(string fromDate, string toDate)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);



                arrSpFieldSeq =
                new string[] { "@FromDate", "@ToDate" };

                logTable = db.GetTableRecords(objParams, "ToMAS_SO_SalesOrderHeader_Select", arrSpFieldSeq);

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
            return logTable;
        }
        public static DataTable ChildDataSource(string salesNumber, string OrderDate)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@SalesOrderNo", salesNumber);
                objParams.Add("@OrderDate", OrderDate);



                arrSpFieldSeq =
                new string[] { "@SalesOrderNo", "@OrderDate" };

                logTable = db.GetTableRecords(objParams, "ToMas_SO_SalesOrderDetail_Select", arrSpFieldSeq);

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
            return logTable;
        }

        public static DataTable GetPOFromMASReport(string fromDate, string toDate, string poNum, string salesOrderNum)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);
                objParams.Add("@PONum", poNum);
                objParams.Add("@SalesOrderNum", salesOrderNum);



                arrSpFieldSeq =
                new string[] { "@FromDate", "@ToDate", "@PONum", "@SalesOrderNum" };

                logTable = db.GetTableRecords(objParams, "av_FromMAS_SO_SalesOrderHeader_Select", arrSpFieldSeq);

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
            return logTable;
        }
        public static DataTable ChildDataSourceFromMas(string salesNumber, string OrderDate)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@SalesOrderNo", salesNumber);
                //objParams.Add("@OrderDate", OrderDate);



                arrSpFieldSeq =
                new string[] { "@SalesOrderNo" };

                logTable = db.GetTableRecords(objParams, "av_FromMas_SO_SalesOrderDetail_Select", arrSpFieldSeq);

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
            return logTable;
        }

        public static void DeleteMasReport(string salesOrderNo)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@SalesOrderNo", salesOrderNo);



                arrSpFieldSeq = new string[] { "@SalesOrderNo" };
                objDB.ExeCommand3(objCompHash, "ToMas_SO_SalesOrder_Delete", arrSpFieldSeq);
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
        public static DataTable CustomerInventoryReport(int companyID, string itemCode)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@ItemCode", itemCode);



                arrSpFieldSeq =
                new string[] { "@CompanyID", "@ItemCode" };

                dataTable = db.GetTableRecords(objParams, "AV_IM1_InventoryMasterfile_Select", arrSpFieldSeq);

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

        public static DataTable GetEmailLogReport(string fromDate, string toDate, string moduleName)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable ds = new DataTable();

            try
            {

                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);
                objParams.Add("@moduleName", moduleName);


                arrSpFieldSeq =
                new string[] { "@FromDate", "@ToDate", "@moduleName" };

                ds = db.GetTableRecords(objParams, "av_EmailLog_SELECT", arrSpFieldSeq);

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
            return ds;
        }
        public static void DeleteEmailReport(int emailLogID)
        {
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@EmailLogID", emailLogID);


                arrSpFieldSeq = new string[] { "@EmailLogID" };
                objDB.ExeCommand(objCompHash, "sv_EmailLog_DELETE", arrSpFieldSeq);
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
        public static DataTable GetItemCode_ESN_Summary()
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@ESN", string.Empty);




                arrSpFieldSeq =
                new string[] { "@ESN" };

                logTable = db.GetTableRecords(objParams, "av_ItemCode_ESN_Summary", arrSpFieldSeq);

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
            return logTable;
        }
        public static DataTable GetcustomErrorReport(string fromDate, string toDate, string searchText)
        {
            DBConnect db = new DBConnect();

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable logTable = new DataTable();

            try
            {

                objParams.Add("@FromDate", fromDate);
                objParams.Add("@ToDate", toDate);
                objParams.Add("@SearchText", searchText);



                arrSpFieldSeq =
                new string[] { "@FromDate", "@ToDate", "@SearchText" };

                logTable = db.GetTableRecords(objParams, "sv_CustomerError_Select", arrSpFieldSeq);

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
            return logTable;
        }


        public static StockResponse GetStockInHandList(StockRequest stockRequest)
        {
            StockResponse serviceResponse = new StockResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            Exception exc = null;
            try
            {

                if (stockRequest != null)
                {

                    int userId = PurchaseOrder.AuthenticateRequest(stockRequest.Authentication, out exc);
                    if (userId > 0)
                    {
                        List<StockInHand> stockList = GetStockInHandReport(0, userId);
                        if (stockList != null && stockList.Count > 0)
                        {
                            serviceResponse.StockinHandList = stockList;
                            serviceResponse.ErrorCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                        }
                        else
                            serviceResponse.ErrorCode = ResponseErrorCode.NoRecordsFound.ToString();
                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                serviceResponse.StockinHandList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
            }

            return serviceResponse;
        }
        public static List<StockInHand> GetStockInHandReport(int companyID, int userID)
        {
            DBConnect db = new DBConnect();
            List<StockInHand> stockInHandList = new List<StockInHand>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@UserID", userID);

                arrSpFieldSeq =
                new string[] { "@CompanyID", "@UserID" };

                dataTable = db.GetTableRecords(objParams, "AV_StockInHand_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        StockInHand stockInHandObj = new StockInHand();
                        stockInHandObj.ItemCode = clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false) as string;
                        stockInHandObj.ItemDescription = clsGeneral.getColumnData(dataRow, "Itemname", string.Empty, false) as string;
                        stockInHandObj.WarehouseCode = clsGeneral.getColumnData(dataRow, "WarehouseCode", string.Empty, false) as string;
                        stockInHandObj.TotalQtyInHand = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "QtyOnHand", 0, false));
                        stockInHandObj.QtyOnPurchaseOrder = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "QtyOnPurchaseOrder", 0, false));
                        stockInHandObj.QtyOnBackOrder = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "QtyOnBackOrder", 0, false));
                        stockInHandObj.QtyOnSalesOrder = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "QtyOnSalesOrder", 0, false));

                        stockInHandList.Add(stockInHandObj);


                    }
                }
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
            return stockInHandList;
        }


    }
}
