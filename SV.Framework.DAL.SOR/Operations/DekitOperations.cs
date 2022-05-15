using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.SOR
{
    public class DekitOperations : BaseCreateInstance
    {
        public int DeKittingCronJob()
        {
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CreateDate", null);
                    arrSpFieldSeq = new string[] { "@CreateDate" };
                    returnValue = db.ExecuteNonQuery(objCompHash, "av_Item_DeKitting_CronJob", arrSpFieldSeq);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                }
                finally
                {
                    // db = null;
                }
            }
            return returnValue;
        }
        public  int DeKittingRequestInsertUpdate(DekitServiceOrder request, out string errorMessage)
        {
            int returnValue = 0;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                //DataTable esnDT = ESNData(serviceOrder.SODetail);
                string dekitDetailXML = clsGeneral.SerializeObject(request.DekitDetails);
                string esnXML = clsGeneral.SerializeObject(request.EsnList);
                try
                {
                    objCompHash.Add("@DeKittingID", request.DeKittingID);
                    objCompHash.Add("@DekitRequestNumber", request.DekitRequestNumber);
                    objCompHash.Add("@CustomerRequestNumber", request.CustomerRequestNumber);
                    objCompHash.Add("@RequestedBy", request.RequestedBy);
                    objCompHash.Add("@CreatedBy", request.CreatedBy);
                    objCompHash.Add("@DekitDetailXML", dekitDetailXML);
                    objCompHash.Add("@EsnXML", esnXML);
                    //objCompHash.Add("@RequestData", RequestData);
                    // objCompHash.Add("@StatusID", request.DeKitStatusID);

                    arrSpFieldSeq = new string[] { "@DeKittingID", "@DekitRequestNumber", "@CustomerRequestNumber", "@CreatedBy",
                    "@RequestedBy", "@DekitDetailXML","@EsnXML"};
                    returnValue = db.ExCommand(objCompHash, "av_ItemDeKitting_InsertUpdate", arrSpFieldSeq, "@soErrorMessage", out errorMessage);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //DeKitRequestLogInsert(request.ServiceRequestID, RequestData, request.UserID, objExp.Message);

                }
                finally
                {
                    // db = null;
                }
            }

            return returnValue;

        }
        public  int DeKittingStatusUpdate(Int64 deKittingID, int userID, string deKittingStatus)
        {
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
               // DataTable dt = default; // new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@DeKittingID", deKittingID);
                    objCompHash.Add("@DeKittingStatus", deKittingStatus);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@DeKittingID", "@DeKittingStatus", "@UserID" };
                    returnValue = db.ExecuteNonQuery(objCompHash, "av_Item_DeKitting_Status_Update", arrSpFieldSeq);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //DeKitRequestLogInsert(request.ServiceRequestID, RequestData, request.UserID, objExp.Message);

                }
                finally
                {
                    //db = null;
                }
            }
            return returnValue;

        }
        public  int DeKittingDelete(Int64 deKittingID)
        {
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
               // DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@DeKittingID", deKittingID);
                    // objCompHash.Add("@DeKittingStatus", deKittingStatus);

                    arrSpFieldSeq = new string[] { "@DeKittingID" };
                    returnValue = db.ExecuteNonQuery(objCompHash, "av_Item_DeKitting_Delete", arrSpFieldSeq);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //DeKitRequestLogInsert(request.ServiceRequestID, RequestData, request.UserID, objExp.Message);

                }
                finally
                {
                    //db = null;
                }
            }
            return returnValue;
        }

        private void DeKitRequestLogInsert(int ServiceRequestID, string RequestData, int UserID, string errorMessage)
        {
            int returnValue = 0;
            int StatusID = 1;
            if (ServiceRequestID > 0)
                StatusID = 2;

            //errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
               // DataTable dt = default;// new DataTable();
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
                   // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            //return returnValue;

        }
        
        public  List<DekitESN> ValidateDeKittingESN(DekitServiceOrder request, out string errorMessage, out bool IsValidate)
        {
            IsValidate = true;
            List<DekitESN> esnList = default;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default; // new DataTable();
                Hashtable objCompHash = new Hashtable();

                string esnXML = clsGeneral.SerializeObject(request.EsnList);
                string esns = "<ItemCompanyGUID p3:nil=\"true\" xmlns:p3=\"http://www.w3.org/2001/XMLSchema-instance\" />";
                string iccid = "<MappedItemCompanyGUID p3:nil=\"true\" xmlns:p3=\"http://www.w3.org/2001/XMLSchema-instance\" />";
                string str = "<ItemCompanyGUID>0</ItemCompanyGUID><MappedItemCompanyGUID>0</MappedItemCompanyGUID>";
                esnXML = esnXML.Replace(esns, "");
                esnXML = esnXML.Replace(iccid, "");
                esnXML = esnXML.Replace(str, "");

                try
                {
                    objCompHash.Add("@CompanyId", request.CompanyId);
                    objCompHash.Add("@CustomerRequestNumber", request.CustomerRequestNumber);
                    objCompHash.Add("@EsnXML", esnXML);
                    objCompHash.Add("@KittedSKUId", request.DekitDetails[0].ItemCompanyGUID);
                    objCompHash.Add("@NumberOfKits", request.DekitDetails[0].Quantity);

                    arrSpFieldSeq = new string[] { "@CompanyId", "@CustomerRequestNumber", "@EsnXML", "@KittedSKUId", "@NumberOfKits" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceOrder_Dekitting_Validate", arrSpFieldSeq, "@soErrorMessage", out errorMessage);
                    esnList = PopulateEsnInfo(dt, out IsValidate);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return esnList;
        }
        public  string GenerateServiceOrder()
        {
            string returnValue = "";

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@DeKitRequestNumber", 1);

                    arrSpFieldSeq = new string[] { "@DeKitRequestNumber" };
                    returnValue = Convert.ToString(db.ExecuteScalar(objCompHash, "av_GetDeKitRequestNumber", arrSpFieldSeq));
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                   // db = null;
                }
            }
            return returnValue;

        }
        public List<ServiceRequestStatus> GetDeKitStatusList()
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
                    objCompHash.Add("@DeKitStatusID", 0);

                    arrSpFieldSeq = new string[] { "@DeKitStatusID" };
                    dt = db.GetTableRecords(objCompHash, "av_Item_DeKitting_Status_Select", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sorStatusList = new List<ServiceRequestStatus>();
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            statusInfo = new ServiceRequestStatus();
                            statusInfo.Status = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                            statusInfo.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DeKitStatusID", 0, false));

                            sorStatusList.Add(statusInfo);
                        }
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //  db = null;
                }
            }
            return sorStatusList;
        }

        public  List<DekittingDetail> GetDekittingSearch(string dekitRquestNumber, string customerRequestNumber, int companyID, string fromDate, string toDate, string SKU,  string ESN, int DeKitStatusID)
        {
            List<DekittingDetail> dekittingList = default;// new List<DekittingDetail>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@DeKittingID", 0);
                    objCompHash.Add("@DekitRequestNumber", dekitRquestNumber);
                    objCompHash.Add("@CustomerRequestNumber", customerRequestNumber);
                    objCompHash.Add("@KittedSKU", SKU);
                    objCompHash.Add("@ESN", ESN);
                    objCompHash.Add("@DateFrom", string.IsNullOrWhiteSpace(fromDate) ? null : fromDate);
                    objCompHash.Add("@DateTo", string.IsNullOrWhiteSpace(toDate) ? null : toDate);
                    objCompHash.Add("@CompanyId", companyID);
                    objCompHash.Add("@DeKitStatusID", DeKitStatusID);

                    arrSpFieldSeq = new string[] { "@DeKittingID", "@DekitRequestNumber", "@CustomerRequestNumber", "@KittedSKU", "@ESN", "@DateFrom",
                    "@DateTo", "@CompanyId", "@DeKitStatusID"};
                    dt = db.GetTableRecords(objCompHash, "av_DekittingRequestSelect", arrSpFieldSeq);
                    dekittingList = PopulateDekitings(dt);

                }
                catch (Exception objExp)
                {
                    //serviceOrders = null;
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());

                }
                finally
                {
                    // db = null;
                }
            }
            return dekittingList;
        }

        public  DekittingDetail GetDekittingDetail(Int64 DeKittingID)
        {
            DekittingDetail dekittingDetail = default;// new DekittingDetail();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@DeKittingID", DeKittingID);
                    objCompHash.Add("@DekitRequestNumber", "");
                    objCompHash.Add("@CustomerRequestNumber", "");
                    objCompHash.Add("@KittedSKU", "");
                    objCompHash.Add("@ESN", "");
                    objCompHash.Add("@DateFrom", null);
                    objCompHash.Add("@DateTo", null);
                    objCompHash.Add("@CompanyId", 0);

                    arrSpFieldSeq = new string[] { "@DeKittingID", "@DekitRequestNumber", "@CustomerRequestNumber", "@KittedSKU", "@ESN", "@DateFrom", "@DateTo", "@CompanyId" };
                    ds = db.GetDataSet(objCompHash, "av_DekittingRequestSelect", arrSpFieldSeq);
                    dekittingDetail = PopulateDekitingDetail(ds);

                }
                catch (Exception objExp)
                {
                    //serviceOrders = null;
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());

                }
                finally
                {
                    //db = null;
                }
            }
            return dekittingDetail;
        }
        private  DekittingDetail PopulateDekitingDetail(DataSet ds)
        {
            DekittingDetail dekittingDetail = default;// new DekittingDetail();

            //List<DekittingDetail> dekittingList = new List<DekittingDetail>();
            List<DekittingRawSKU> RawSKUs = default;// new List<DekittingRawSKU>();
            List<DekittingSKUESN> EsnList = default;// new List<DekittingSKUESN>();

            if (ds != null && ds.Tables.Count > 0 & ds.Tables[0].Rows.Count > 0)
            {
                dekittingDetail = new DekittingDetail();
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    RawSKUs = new List<DekittingRawSKU>();
                    EsnList = new List<DekittingSKUESN>();

                    dekittingDetail.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    dekittingDetail.CreatedBy = clsGeneral.getColumnData(dataRow, "CreatedBy", string.Empty, false) as string;
                    dekittingDetail.CustomerRequestNumber = clsGeneral.getColumnData(dataRow, "CustomerRequestNumber", string.Empty, false) as string;
                    dekittingDetail.DekitRequestNumber = clsGeneral.getColumnData(dataRow, "DekitRequestNumber", string.Empty, false) as string;
                    dekittingDetail.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    dekittingDetail.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    dekittingDetail.ApprovedBy = clsGeneral.getColumnData(dataRow, "ApprovedBy", string.Empty, false) as string;
                    dekittingDetail.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                    dekittingDetail.DeKitStatus = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;

                    dekittingDetail.DeKitDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "DeKitDate", string.Empty, false));
                    dekittingDetail.DeKittingID = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "DeKittingID", 0, false));
                    dekittingDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                    //esnDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));

                    if(ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow2 in ds.Tables[1].Rows)
                        {
                            DekittingRawSKU rawSKU = new DekittingRawSKU();
                            rawSKU.SKU = clsGeneral.getColumnData(dataRow2, "SKU", string.Empty, false) as string;
                            rawSKU.WhLocation = clsGeneral.getColumnData(dataRow2, "WhLocation", string.Empty, false) as string;
                            rawSKU.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "Quantity", 0, false));
                            RawSKUs.Add(rawSKU);
                        }
                    }
                    dekittingDetail.RawSKUs = RawSKUs;
                    if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow3 in ds.Tables[2].Rows)
                        {
                            DekittingSKUESN skuESN = new DekittingSKUESN();
                            skuESN.SKU = clsGeneral.getColumnData(dataRow3, "SKU", string.Empty, false) as string;
                            skuESN.ESN = clsGeneral.getColumnData(dataRow3, "ESN", string.Empty, false) as string;
                            skuESN.WhLocation = clsGeneral.getColumnData(dataRow3, "WhLocation", string.Empty, false) as string;
                            EsnList.Add(skuESN);
                        }
                    }
                    dekittingDetail.EsnList = EsnList;
                }
            }
            return dekittingDetail;

        }

        private  List<DekittingDetail> PopulateDekitings(DataTable dt)
        {
            List<DekittingDetail> dekittingList = default;// new List<DekittingDetail>();
            if (dt != null && dt.Rows.Count > 0)
            {
                dekittingList = new List<DekittingDetail>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    DekittingDetail dekittingDetail = new DekittingDetail();

                    dekittingDetail.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    dekittingDetail.CreatedBy = clsGeneral.getColumnData(dataRow, "CreatedBy", string.Empty, false) as string;
                    dekittingDetail.ApprovedBy = clsGeneral.getColumnData(dataRow, "ApprovedBy", string.Empty, false) as string;
                    dekittingDetail.CustomerRequestNumber = clsGeneral.getColumnData(dataRow, "CustomerRequestNumber", string.Empty, false) as string;
                    dekittingDetail.DekitRequestNumber = clsGeneral.getColumnData(dataRow, "DekitRequestNumber", string.Empty, false) as string;
                    dekittingDetail.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    dekittingDetail.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    dekittingDetail.DeKitStatus = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;

                    dekittingDetail.DeKitDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "DeKitDate", string.Empty, false));
                    dekittingDetail.DeKittingID = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "DeKittingID", 0, false));
                    dekittingDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                    //esnDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));

                    dekittingList.Add(dekittingDetail);                    
                }
            }
            return dekittingList;

        }

        private  List<DekitESN> PopulateEsnInfo(DataTable dt, out bool IsValidate)
        {
            IsValidate = true;
            List<DekitESN> esnList = default;// new List<DekitESN>();
            if (dt != null && dt.Rows.Count > 0)
            {
                esnList = new List<DekitESN>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    DekitESN esnDetail = new DekitESN();

                    esnDetail.WhLocation = clsGeneral.getColumnData(dataRow, "WhLocation", string.Empty, false) as string;
                    esnDetail.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    esnDetail.ValidationMsg = clsGeneral.getColumnData(dataRow, "ValidationMsg", string.Empty, false) as string;
                    esnDetail.ICCIDValidationMsg = clsGeneral.getColumnData(dataRow, "ICCIDValidationMsg", string.Empty, false) as string;
                    esnDetail.ICCID = clsGeneral.getColumnData(dataRow, "ICCID", string.Empty, false) as string;
                    esnDetail.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    esnDetail.MappedSKU = clsGeneral.getColumnData(dataRow, "MappedSKU", string.Empty, false) as string;
                    esnDetail.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    esnDetail.MappedItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MappedItemCompanyGUID", 0, false));
                    //esnDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));

                    esnList.Add(esnDetail);
                    if (!string.IsNullOrEmpty(esnDetail.ValidationMsg) && IsValidate)// && string.IsNullOrEmpty(esnDetail.ICCIDValidationMsg))
                        IsValidate = false;
                    if (!string.IsNullOrEmpty(esnDetail.ICCIDValidationMsg) && IsValidate)
                        IsValidate = false;
                }
            }
            return esnList;

        }

    }
}
