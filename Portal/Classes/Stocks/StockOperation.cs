using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class StockOperation
    {

        public static List<StockStatus> GetStockStatus(int companyId, string StockCalculateDateFrom, string StockCalculateDateTo, string sku)
        {
            List<StockStatus> stockStatusList = new List<StockStatus>();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@piCompanyID", companyId);
                objCompHash.Add("@piStockCalculateDateFrom", string.IsNullOrWhiteSpace(StockCalculateDateFrom) ? null : Convert.ToDateTime(StockCalculateDateFrom).ToString("yyyy-MM-dd"));
                objCompHash.Add("@piStockCalculateDateTo", string.IsNullOrWhiteSpace(StockCalculateDateTo) ? null : Convert.ToDateTime(StockCalculateDateTo).ToString("yyyy-MM-dd"));
                objCompHash.Add("@piSKU", sku);

                arrSpFieldSeq = new string[] { "@piCompanyID", "@piStockCalculateDateFrom", "@piStockCalculateDateTo", "@piSKU" };
                ds = db.GetDataSet(objCompHash, "aV_StockStatus_SELECT", arrSpFieldSeq);

                stockStatusList = PopulateStockStatus(ds.Tables[0]);

            }
            catch (Exception objExp)
            {
                //serviceOrders = null;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                db = null;
            }

            return stockStatusList;

        }
        public RunningStockResponse GetRunningStock(RunningStockRequest serviceRequest)
        {
            SV.Framework.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.Inventory.InventoryReportOperation>();

            string requestXML = clsGeneral.SerializeObject(serviceRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetRunningStock";
            request.RequestTimeStamp = DateTime.Now;
            request.UserID = 0;
            request.CompanyID = 0;

            Exception ex = null;

            RunningStockResponse serviceResponse = new RunningStockResponse();
            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out ex);
                if (ex != null)
                {
                    serviceResponse.Comment = ex.Message;
                    serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                }
                else
                {
                    if (credentialValidation != null && credentialValidation.CompanyID > 0)
                    {
                        request.UserID = credentialValidation.UserID;
                        request.CompanyID = credentialValidation.CompanyID;

                        string DateFrom = serviceRequest.DateFrom.ToShortDateString() == "1/1/0001" ? null : serviceRequest.DateFrom.ToShortDateString();
                        string DateTo = serviceRequest.DateTo.ToShortDateString() == "1/1/0001" ? null : serviceRequest.DateTo.ToShortDateString();
                        //List<StockStatus> StockReceivals =  GetStockStatus(credentialValidation.CompanyID, DateFrom, DateTo, serviceRequest.SKU);
                        List<SV.Framework.Models.Inventory.StockCount> runningStock = inventoryReportOperation.GetStockCountSummary(credentialValidation.CompanyID, DateFrom, DateTo, serviceRequest.SKU, serviceRequest.IncludeDisabledSKU);


                        if (runningStock != null && runningStock.Count > 0)
                        {

                            serviceResponse.Comment = "Successfully Retrieved";
                            serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                            serviceResponse.RunningStock = runningStock;


                        }
                        else
                        {

                            serviceResponse.Comment = "No records found";
                            serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                            serviceResponse.RunningStock = null;
                        }

                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot Authenticate User";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                        serviceResponse.RunningStock = null;
                    }
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;
                    // LogOperations.ApiLogInsert(request);
                }
            }
            catch (Exception exc)
            {
                ex = exc;
                //  serviceResponse.Comment = exc.Message;
                //serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                // LogOperations.ApiLogInsert(request);
            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }

            return serviceResponse;
        }

        public StockReceivalResponse GetInventoryStockReceival(StockReceivalRequest serviceRequest)
        {
            string requestXML = clsGeneral.SerializeObject(serviceRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetInventoryStockFlow";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;
            request.UserID = 0;
            request.CompanyID = 0;


            StockReceivalResponse serviceResponse = new StockReceivalResponse();
            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out ex);
                if (ex != null)
                {
                    serviceResponse.Comment = ex.Message;
                    serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = true;
                    request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                    
                }
                else
                {
                    if (credentialValidation != null && credentialValidation.CompanyID > 0)
                    {
                        request.UserID = credentialValidation.UserID;
                        request.CompanyID = credentialValidation.CompanyID;

                        string DateFrom = serviceRequest.DateFrom.ToShortDateString() == "1/1/0001" ? null : serviceRequest.DateFrom.ToShortDateString();
                        string DateTo = serviceRequest.DateTo.ToShortDateString() == "1/1/0001" ? null : serviceRequest.DateTo.ToShortDateString();
                        List<StockStatus> StockReceivals = GetStockStatus(credentialValidation.CompanyID, DateFrom, DateTo, serviceRequest.SKU);


                        if (StockReceivals != null && StockReceivals.Count > 0)
                        {

                            serviceResponse.Comment = "Successfully Retrieved";
                            serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                            serviceResponse.StockReceivals = StockReceivals;


                        }
                        else
                        {

                            serviceResponse.Comment = "No records found";
                            serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                            serviceResponse.StockReceivals = null;
                        }

                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot Authenticate User";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                        serviceResponse.StockReceivals = null;
                    }
                    request.UserID = credentialValidation.UserID;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;
                   // LogOperations.ApiLogInsert(request);
                }
            }
            catch (Exception exc)
            {
                ex = exc;
                //  serviceResponse.Comment = exc.Message;
                //serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
               // LogOperations.ApiLogInsert(request);
            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }

            return serviceResponse;
        }

        public static List<CurrentStock> GetCurrentStock(int companyId, string SKU, bool IsDisable, bool IsKitted)
        {
            List<CurrentStock> stockStatusList = new List<CurrentStock>();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@companyID", companyId);
                objCompHash.Add("@piSKU", string.IsNullOrEmpty(SKU) ? null : SKU);
                objCompHash.Add("@piIsDisable", IsDisable);
                objCompHash.Add("@piIsKitted", IsKitted);


                arrSpFieldSeq = new string[] { "@companyID", "@piSKU", "@piIsDisable", "@piIsKitted" };
                dt = db.GetTableRecords(objCompHash, "av_StockStatus_Current", arrSpFieldSeq);
                stockStatusList = PopulateCurrentStock(dt);

            }
            catch (Exception objExp)
            {
                //serviceOrders = null;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                db = null;
            }

            return stockStatusList;

        }
        public CurrentStockResponse GetInventoryStockCurrent(CurrentStockRequest serviceRequest)
        {
            string requestXML = clsGeneral.SerializeObject(serviceRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetInventoryStockCurrent";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;
            request.UserID = 0;
            request.CompanyID = 0;

            CurrentStockResponse serviceResponse = new CurrentStockResponse();
            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out ex);
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
                else
                {
                    if (credentialValidation != null && credentialValidation.CompanyID > 0)
                    {
                        request.UserID = credentialValidation.UserID;
                        request.CompanyID = credentialValidation.CompanyID;

                        List<CurrentStock> CurrentStocks = GetCurrentStock(credentialValidation.CompanyID, serviceRequest.SKU, false, false);


                        if (CurrentStocks != null && CurrentStocks.Count > 0)
                        {

                            serviceResponse.Comment = "Successfully Retrieved";
                            serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                            serviceResponse.CurrentStocks = CurrentStocks;


                        }
                        else
                        {

                            serviceResponse.Comment = "No records found";
                            serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                            serviceResponse.CurrentStocks = null;
                        }

                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot Authenticate User";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                        serviceResponse.CurrentStocks = null;
                    }
                    request.UserID = credentialValidation.UserID;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.Comment;
                    

                }
            }
            catch (Exception exc)
            {
                ex = exc;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.CurrentStocks = null;
                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
           //     LogOperations.ApiLogInsert(request);


            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }
            return serviceResponse;
        }

        private static List<CurrentStock> PopulateCurrentStock(DataTable dt)
        {
            CurrentStock skuStock = null;
            List<CurrentStock> stockStatusList = new List<CurrentStock>();
            
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    skuStock = new CurrentStock();
                    skuStock.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    skuStock.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    //skuStock.OpeningBalance = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OpeningBalance", 0, false));
                    skuStock.StockInHand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_hand", 0, false));
                    skuStock.IsDisable = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsDisable", false, false));
                    //skuStock.StockAssignment = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StockAssignment", 0, false));
                    //skuStock.DefectiveESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "BadESN", 0, false));
                    //skuStock.StockReceived = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "NewReceival", 0, false));
                    //skuStock.PendingAssignment = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PendingAssignment", 0, false));
                    //skuStock.ClosingBalance = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ClosingBalance", 0, false));
                    skuStock.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    //skuStock.ModelNumber = clsGeneral.getColumnData(dataRow, "ModelNumber", string.Empty, false) as string;
                    stockStatusList.Add(skuStock);

                }
            }
            return stockStatusList;

        }
        private static List<StockStatus> PopulateStockStatus(DataTable dt)
        {
            StockStatus skuStock = null;
            List<StockStatus> stockStatusList = new List<StockStatus>();
            CompanyDetail companyDetail = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    skuStock = new StockStatus();
                    companyDetail = new CompanyDetail();
                    skuStock.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    skuStock.OpeningBalance = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OpeningBalance", 0, false));
                    //skuStock.StockInHand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_hand", 0, false));
                   // skuStock.StockAssignment = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StockAssignment", 0, false));
                   // skuStock.Reassignment = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Reassignment", 0, false));
                   // skuStock.DefectiveESN = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "BadESN", 0, false));
                    skuStock.StockReceived = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "NewReceival", 0, false));
                   // skuStock.PendingAssignment = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PendingAssignment", 0, false));
                    skuStock.ClosingBalance = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ClosingBalance", 0, false));
                    skuStock.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    //skuStock.ModelNumber = clsGeneral.getColumnData(dataRow, "ModelNumber", string.Empty, false) as string;
                    skuStock.OpeningBalanceDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "OpeningBalanceDate", DateTime.Now, false)).ToString("MM/dd/yyyy");
                    skuStock.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    //companyDetail.CompanyAccountNumber = clsGeneral.getColumnData(dataRow, "CompanyAccountNumber", string.Empty, false) as string;
                    //companyDetail.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                    //skuStock.CompanyInfo = companyDetail;
                    stockStatusList.Add(skuStock);

                }
            }
            return stockStatusList;

        }
        public static List<SKUStock> GetSKUStocks(int companyId, string StockCalculateDate)
        {
            List<SKUStock> SKUStocks = new List<SKUStock>();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@piCompanyID", companyId);
                objCompHash.Add("@piStockCalculateDate", string.IsNullOrWhiteSpace(StockCalculateDate) ? null : StockCalculateDate);
                
              
                arrSpFieldSeq = new string[] { "@piCompanyID", "@piStockCalculateDate" };
                dt = db.GetTableRecords(objCompHash, "aV_StockStatus_SELECT", arrSpFieldSeq);
                SKUStocks = PopulateSKUStocks(dt);

            }
            catch (Exception objExp)
            {
                //serviceOrders = null;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                db = null;
            }

            return SKUStocks;

        }
        private static List<SKUStock> PopulateSKUStocks(DataTable dt)
        {
            SKUStock skuStock = null;
            List<SKUStock> SKUStockList = new List<SKUStock>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    skuStock = new SKUStock();

                    skuStock.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    skuStock.OpeningBalance = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OpeningBalance", 0, false));
                    skuStock.StockInHand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_hand", 0, false));
                    skuStock.StockAssigned = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StockAssignment", 0, false));
                    skuStock.StockShipped = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StockShipped", 0, false));
                    skuStock.BadStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "BadESN", 0, false));
                    skuStock.StockReceived = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "NewReceival", 0, false));
                    skuStock.PendingAssignment = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PendingAssignment", 0, false));

                    SKUStockList.Add(skuStock);
                    
                }
            }
            return SKUStockList;

        }
    }
}