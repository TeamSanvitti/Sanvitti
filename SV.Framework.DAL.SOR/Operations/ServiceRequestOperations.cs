using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.SOR
{
    public class ServiceRequestOperations : BaseCreateInstance
    {
        public int ServiceOrderRequestInsertUpdate(ServiceOrderRequestModel request, out string errorMessage)
        {
            int returnValue = 0;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                //DataTable esnDT = ESNData(serviceOrder.SODetail);
                string RequestData = clsGeneral.SerializeObject(request);
                try
                {
                    objCompHash.Add("@ServiceRequestID", request.ServiceRequestID);
                    objCompHash.Add("@ServiceRequestNumber", request.SORNumber);
                    objCompHash.Add("@ItemCompanyGUID", request.ItemCompanyGUID);
                    objCompHash.Add("@Quantity", request.Quantity);
                    objCompHash.Add("@RequestedBy", request.RequestedBy);
                    objCompHash.Add("@Comments", request.Comments);
                    objCompHash.Add("@CreatedBy", request.UserID);
                    objCompHash.Add("@RequestData", RequestData);
                    objCompHash.Add("@StatusID", request.StatusID);

                    arrSpFieldSeq = new string[] { "@ServiceRequestID", "@ServiceRequestNumber", "@ItemCompanyGUID", "@Quantity", "@RequestedBy", "@Comments",
                "@CreatedBy", "@RequestData", "@StatusID"};
                    returnValue = db.ExCommand(objCompHash, "av_ServiceRequest_InsertUpdate", arrSpFieldSeq, "@ErrorMessage", out errorMessage);

                }
                catch (Exception objExp)
                {
                    ServiceOrderRequestLogInsert(request.ServiceRequestID, RequestData, request.UserID, objExp.Message);
                    Logger.LogMessage(objExp, this);
                    //errorMessage = "Technical error!";
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return returnValue;

        }
        public ServiceRequestDetail ServiceRequestNumberSearch(int companyID, string serviceRequestNumber)
        {
            ServiceRequestDetail serviceRequestDetail = default;// new ServiceRequestDetail();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                Hashtable objCompHash = new Hashtable();
                List<KittedRawSKU> skuList = default;
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@ServiceRequestNumber", serviceRequestNumber);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@ServiceRequestNumber" };
                    ds = db.GetDataSet(objCompHash, "av_ServiceRequest_Search", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            serviceRequestDetail = new ServiceRequestDetail();
                            serviceRequestDetail.Status = clsGeneral.getColumnData(dataRow, "StatusTEXT", string.Empty, false) as string;
                            serviceRequestDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                            serviceRequestDetail.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));

                        }
                        if (ds != null && ds.Tables.Count > 1)
                        {
                            skuList = PopulateKittedRawSKUs(ds.Tables[1]);
                            serviceRequestDetail.RawSKUs = skuList;
                        }
                    }

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return serviceRequestDetail;
        }
        
        public int ServiceOrderRequestDelete(int serviceRequestID, int userID)
        {
            int returnValue = 0;
            //errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
               // DataTable dt = default; // new DataTable();
                Hashtable objCompHash = new Hashtable();
                //DataTable esnDT = ESNData(serviceOrder.SODetail);
                //string RequestData = clsGeneral.SerializeObject(request);
                try
                {
                    objCompHash.Add("@ServiceRequestID", serviceRequestID);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@ServiceRequestID", "@UserID" };
                    returnValue = db.ExecuteNonQuery(objCompHash, "av_ServiceRequest_Delete", arrSpFieldSeq);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //ServiceOrderRequestLogInsert(request.ServiceRequestID, RequestData, request.UserID, objExp.Message);
                    //errorMessage = "Technical error!";
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return returnValue;

        }

        public  List<ServiceOrderRequestModel> GetServiceOrderRequests(ServiceOrderRequestModel serviceOrder)
        {   
            List<ServiceOrderRequestModel> sorList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CompanyID", serviceOrder.CompanyID);
                    objCompHash.Add("@ServiceRequestID", serviceOrder.ServiceRequestID);
                    objCompHash.Add("@ServiceRequestNumber", serviceOrder.SORNumber);
                    objCompHash.Add("@FromDate", serviceOrder.FromDate == string.Empty ? null : serviceOrder.FromDate);
                    objCompHash.Add("@ToDate", serviceOrder.ToDate == string.Empty ? null : serviceOrder.ToDate);
                    objCompHash.Add("@SKU", serviceOrder.SKU);
                    objCompHash.Add("@StatusID", serviceOrder.StatusID);
                    objCompHash.Add("@RequestedBy", serviceOrder.RequestedBy);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@ServiceRequestID", "@ServiceRequestNumber", "@FromDate", "@ToDate", "@SKU", "@StatusID", "@RequestedBy" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceRequest_Select", arrSpFieldSeq);
                    sorList = PopulateSORList(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return sorList;

        }
        public List<ServiceOrderRequestModel> GetServiceOrderRequestLog(int serviceRequestID)
        {
            List<ServiceOrderRequestModel> sorList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@ServiceRequestID", serviceRequestID);
                    arrSpFieldSeq = new string[] { "@ServiceRequestID" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceRequestLog_Select", arrSpFieldSeq);
                    sorList = PopulateSORLogList(dt);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return sorList;

        }

        public ServiceOrderRequestModel GetServiceOrderRequest(int ServiceRequestID)
        {
            ServiceOrderRequestModel sorInfo = default;// new ServiceOrderRequestModel();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@ServiceRequestID", ServiceRequestID);
                    //objCompHash.Add("@ServiceRequestNumber", serviceOrder.SORNumber);
                    //objCompHash.Add("@FromDate", serviceOrder.FromDate == string.Empty ? null : serviceOrder.FromDate);
                    //objCompHash.Add("@ToDate", serviceOrder.ToDate == string.Empty ? null : serviceOrder.ToDate);
                    //objCompHash.Add("@SKU", serviceOrder.SKU);
                    //objCompHash.Add("@StatusID", serviceOrder.StatusID);
                    //objCompHash.Add("@RequestedBy", serviceOrder.RequestedBy);

                    arrSpFieldSeq = new string[] { "@ServiceRequestID" };
                    //arrSpFieldSeq = new string[] { "@ServiceRequestID", "@ServiceRequestNumber", "@FromDate", "@ToDate", "@SKU", "@StatusID", "@RequestedBy" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceRequest_Select", arrSpFieldSeq);
                    sorInfo = PopulateSORInfo(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return sorInfo;

        }
        public List<ServiceRequestStatus> GetSoRStatusList()
        {
            List<ServiceRequestStatus> sorStatusList = default;// new List<ServiceRequestStatus>();
            ServiceRequestStatus statusInfo = default;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@StatusID", 0);

                    arrSpFieldSeq = new string[] { "@StatusID" };
                    dt = db.GetTableRecords(objCompHash, "av_SORStatus_Select", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sorStatusList = new List<ServiceRequestStatus>();
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            statusInfo = new ServiceRequestStatus();
                            statusInfo.Status = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                            statusInfo.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                            sorStatusList.Add(statusInfo);
                        }
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return sorStatusList;

        }

        public List<ServiceOrderRequestModel> GetServiceOrderRequestWidget(int companyID, string SKU, string status)
        {
            List<ServiceOrderRequestModel> sorList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@SKU", SKU);
                    objCompHash.Add("@Status", status);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@SKU", "@Status" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceRequest_Widget", arrSpFieldSeq);
                    sorList = PopulateSORWidget(dt);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return sorList;
        }
        private List<KittedRawSKU> PopulateKittedRawSKUs(DataTable dataTable)
        {
            List<KittedRawSKU> skuList = default;
            KittedRawSKU rawSKU = default;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                skuList = new List<KittedRawSKU>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    rawSKU = new KittedRawSKU();
                    rawSKU.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    rawSKU.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                    rawSKU.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    rawSKU.MappedItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MappedItemCompanyGUID", 0, false));
                    rawSKU.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;

                    rawSKU.IsESNRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESNRequired", false, false));
                    rawSKU.StockMsg = clsGeneral.getColumnData(dataRow, "Stockmsg", string.Empty, false) as string;
                    //rawSKU.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    //rawSKU.MappedCategoryName = clsGeneral.getColumnData(dataRow, "MappedCategoryName", string.Empty, false) as string;
                    rawSKU.MappedSKU = clsGeneral.getColumnData(dataRow, "MappedSKU", string.Empty, false) as string;
                    //rawSKU.MappedProductName = clsGeneral.getColumnData(dataRow, "MappedItemName", string.Empty, false) as string;

                    skuList.Add(rawSKU);
                }
            }
            return skuList;

        }

        private List<ServiceOrderRequestModel> PopulateSORWidget(DataTable dt)
        {
            ServiceOrderRequestModel sorModel = default;
            List<ServiceOrderRequestModel> sorList = default;// new List<ServiceOrderRequestModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                sorList = new List<ServiceOrderRequestModel>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    sorModel = new ServiceOrderRequestModel();

                    sorModel.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                    sorModel.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    sorModel.SORDate = clsGeneral.getColumnData(dataRow, "SORDate", string.Empty, false) as string;

                    sorList.Add(sorModel);
                }
            }
            return sorList;

        }

        private ServiceOrderRequestModel PopulateSORInfo(DataTable dt)
        {
            ServiceOrderRequestModel sorModel = default;// new ServiceOrderRequestModel();
           // List<ServiceOrderRequestModel> sorList = new List<ServiceOrderRequestModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    sorModel = new ServiceOrderRequestModel();

                    sorModel.SORNumber = clsGeneral.getColumnData(dataRow, "ServiceRequestNumber", string.Empty, false) as string;
                    sorModel.ServiceRequestID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ServiceRequestID", 0, false));
                    sorModel.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    sorModel.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                    sorModel.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                    sorModel.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    sorModel.RequestedBy = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RequestedBy", 0, false));
                    //sorModel.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    //sorModel.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    //sorModel.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    //sorModel.CreatedUserBy = clsGeneral.getColumnData(dataRow, "CreatedUserBy", string.Empty, false) as string;
                    //sorModel.RequestedUserBy = clsGeneral.getColumnData(dataRow, "RequestedUserBy", string.Empty, false) as string;
                    //sorModel.Status = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    sorModel.Comments = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                    sorModel.SORDate = clsGeneral.getColumnData(dataRow, "CreatedDate", string.Empty, false) as string;

                    //sorList.Add(sorModel);

                }
            }
            return sorModel;

        }

        private List<ServiceOrderRequestModel> PopulateSORList(DataTable dt)
        {            
            ServiceOrderRequestModel sorModel = default;
            List<ServiceOrderRequestModel> sorList = default;// new List<ServiceOrderRequestModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                sorList = new List<ServiceOrderRequestModel>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    sorModel = new ServiceOrderRequestModel();
                    sorModel.SORNumber = clsGeneral.getColumnData(dataRow, "ServiceRequestNumber", string.Empty, false) as string;
                    sorModel.ServiceRequestID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ServiceRequestID", 0, false));
                    sorModel.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    sorModel.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    sorModel.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                    sorModel.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                    sorModel.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    sorModel.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    sorModel.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    sorModel.CreatedUserBy = clsGeneral.getColumnData(dataRow, "CreatedUserBy", string.Empty, false) as string;
                    sorModel.RequestedUserBy = clsGeneral.getColumnData(dataRow, "RequestedUserBy", string.Empty, false) as string;
                    sorModel.Status = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    sorModel.Comments = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                    sorModel.SORDate = clsGeneral.getColumnData(dataRow, "CreatedDate", string.Empty, false) as string;
                    sorModel.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", DateTime.Now, false));

                    sorList.Add(sorModel);                    
                }
            }
            return sorList;

        }
        private List<ServiceOrderRequestModel> PopulateSORLogList(DataTable dt)
        {
            ServiceOrderRequestModel sorModel = default;
            List<ServiceOrderRequestModel> sorList = default;// new List<ServiceOrderRequestModel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                sorList = new List<ServiceOrderRequestModel>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    sorModel = new ServiceOrderRequestModel();

                    sorModel.SORNumber = clsGeneral.getColumnData(dataRow, "ServiceRequestNumber", string.Empty, false) as string;
                    sorModel.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                    sorModel.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    sorModel.RequestData = (clsGeneral.getColumnData(dataRow, "RequestData", string.Empty, false) as string).Replace("<", "&lt;").Replace(">", "&gt;"); ;
                    sorModel.CreatedUserBy = clsGeneral.getColumnData(dataRow, "CreatedByName", string.Empty, false) as string;
                    sorModel.RequestedUserBy = clsGeneral.getColumnData(dataRow, "RequestedByName", string.Empty, false) as string;
                    sorModel.Status = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    sorModel.SORDate = clsGeneral.getColumnData(dataRow, "CreatedDate", string.Empty, false) as string;

                    sorList.Add(sorModel);

                }
            }
            return sorList;

        }

        private  void ServiceOrderRequestLogInsert(int ServiceRequestID,string RequestData, int UserID, string errorMessage)
        {
            int returnValue = 0;
            int StatusID = 1;
            if (ServiceRequestID > 0)
                StatusID = 2;

            //errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                //DataTable esnDT = ESNData(serviceOrder.SODetail);
                //string RequestData = clsGeneral.SerializeObject(request);
                try
                {
                    objCompHash.Add("@ServiceRequestID", ServiceRequestID);
                    objCompHash.Add("@StatusID", StatusID);
                    objCompHash.Add("@ServiceResult", errorMessage);
                    objCompHash.Add("@CreatedBy", UserID);
                    objCompHash.Add("@RequestData", RequestData);

                    arrSpFieldSeq = new string[] { "@ServiceRequestID", "@StatusID", "@ServiceResult", "@CreatedBy", "@RequestData" };
                    returnValue = db.ExecCommand(objCompHash, "av_ServiceRequestLog_Insert", arrSpFieldSeq);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //errorMessage = "Technical error!";
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //  db = null;
                }
            }
            //return returnValue;

        }
    }
}

