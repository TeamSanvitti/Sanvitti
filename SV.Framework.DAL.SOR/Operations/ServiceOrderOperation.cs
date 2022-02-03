using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.SOR
{
    public class ServiceOrderOperation:BaseCreateInstance
    {
        public List<ServiceOrderDetail> ValidateServiceOrderEsnRange(int qty, string ESNs, string SKUs, out string errorMessage, out int IsValidQty)
        {
            IsValidQty = 0;
            //IsValidate = true;
            List<ServiceOrderDetail> esnList = default;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@Qty", qty);
                    objCompHash.Add("@ESNs", ESNs);
                    objCompHash.Add("@SKUs", SKUs);
                    arrSpFieldSeq = new string[] { "@Qty", "@ESNs", "@SKUs" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceOrderValidateESNQuantity", arrSpFieldSeq, "@InvalidQty", "@InvalidESNRange", out IsValidQty, out errorMessage);
                    esnList = PopulateEsns(dt);
                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //  db = null;
                }
            }
            return esnList;

        }
        public List<ServiceOrderDetail> ValidateServiceOrderEsnRange(List<SOQtyValidate> sOQtyValidates, out string errorMessage, out int IsValidQty)
        {
            IsValidQty = 0;
            //IsValidate = true;
            List<ServiceOrderDetail> esnList = default;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string esnXML = clsGeneral.SerializeObject(sOQtyValidates);
                //DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@piEsnXML", esnXML);
                    arrSpFieldSeq = new string[] { "@piEsnXML" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceOrderValidateESNQuantity", arrSpFieldSeq, "@InvalidQty", "@InvalidESNRange", out IsValidQty, out errorMessage);
                    esnList = PopulateEsns(dt);
                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;
                    Logger.LogMessage(objExp, this);
                    // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return esnList;

        }

        public  List<ServiceOrderDetail> ValidateServiceOrder(ServiceOrders serviceOrder, out string errorMessage, out bool IsValidate)
        {
            IsValidate = true;
            List<ServiceOrderDetail> esnList = default;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                DataTable esnDT = ESNData(serviceOrder.SODetail);
                try
                {
                    objCompHash.Add("@KittedSKUId", serviceOrder.KittedSKUId);
                    objCompHash.Add("@ServiceOrderId", serviceOrder.ServiceOrderId);
                    objCompHash.Add("@CustOrderNo", serviceOrder.CustomerOrderNumber);
                    objCompHash.Add("@av_ServiceOrderDetailType", esnDT);


                    arrSpFieldSeq = new string[] { "@KittedSKUId", "@ServiceOrderId", "@CustOrderNo", "@av_ServiceOrderDetailType" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceOrderValidateNew", arrSpFieldSeq, "@soErrorMessage", out errorMessage);
                    esnList = PopulateEsnInfo(dt, out IsValidate);

                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return esnList;

        }

        public List<ServiceOrderDetail> Validate_ServiceOrder(ServiceOrders serviceOrder, out string errorMessage, out bool IsValidate)
        {
            IsValidate = true;
            List<ServiceOrderDetail> esnList = default;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                string esnXML = clsGeneral.SerializeObject(serviceOrder.SODetail);
                string esns = "<ItemCompanyGUID p3:nil=\"true\" xmlns:p3=\"http://www.w3.org/2001/XMLSchema-instance\" />";
                string iccid = "<MappedItemCompanyGUID p3:nil=\"true\" xmlns:p3=\"http://www.w3.org/2001/XMLSchema-instance\" />";
                string str = "<ItemCompanyGUID>0</ItemCompanyGUID><MappedItemCompanyGUID>0</MappedItemCompanyGUID>";
                // string esnXML = clsGeneral.SerializeObject(esnAssignment.EsnList);
                esnXML = esnXML.Replace(esns, "");
                esnXML = esnXML.Replace(iccid, "");
                esnXML = esnXML.Replace(str, "");

                try
                {
                    objCompHash.Add("@CompanyId", serviceOrder.CompanyId);
                    objCompHash.Add("@CustOrderNo", serviceOrder.CustomerOrderNumber);
                    objCompHash.Add("@EsnXML", esnXML);
                    objCompHash.Add("@KittedSKUId", serviceOrder.KittedSKUId);
                    objCompHash.Add("@NumberOfKits", serviceOrder.Quantity);

                    arrSpFieldSeq = new string[] { "@CompanyId", "@CustOrderNo", "@EsnXML", "@KittedSKUId", "@NumberOfKits" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceOrder_Validate", arrSpFieldSeq, "@soErrorMessage", out errorMessage);
                    esnList = PopulateEsnInfo(dt, out IsValidate);

                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return esnList;

        }
        private void ServiceOrderLogInsert(int ServiceOrderID, string RequestData, int UserID, string errorMessage)
        {
            int returnValue = 0;
            int StatusID = 1;
            
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
                    objCompHash.Add("@ServiceOrderID", ServiceOrderID);
                    objCompHash.Add("@ServiceResult", errorMessage);
                    objCompHash.Add("@CreatedBy", UserID);
                    objCompHash.Add("@RequestData", RequestData);

                    arrSpFieldSeq = new string[] { "@ServiceOrderID",  "@ServiceResult", "@CreatedBy", "@RequestData" };
                    returnValue = db.ExecCommand(objCompHash, "sv_ServiceOrderDetailLog_Insert", arrSpFieldSeq);

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

        public int ServiceOrderInsertUpdate(ServiceOrders serviceOrder, int userId, out string errorMessage)
        {
            int returnValue = 0;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                //DataTable esnDT = ESNData(serviceOrder.SODetail);
                string esnXML = clsGeneral.SerializeObject(serviceOrder.SODetail);
                try
                {
                    objCompHash.Add("@ServiceOrderId", serviceOrder.ServiceOrderId);
                    objCompHash.Add("@ServiceOrderNo", serviceOrder.ServiceOrderNumber);
                    objCompHash.Add("@CustOrderNo", serviceOrder.CustomerOrderNumber);
                    objCompHash.Add("@OrderDate", DateTime.Now);
                    objCompHash.Add("@KittedSKUId", serviceOrder.KittedSKUId);
                    objCompHash.Add("@Qty", serviceOrder.Quantity);
                    objCompHash.Add("@UserId", userId);
                    objCompHash.Add("@EsnXML", esnXML);

                    arrSpFieldSeq = new string[] { "@ServiceOrderId", "@ServiceOrderNo", "@CustOrderNo", "@OrderDate", "@KittedSKUId", "@Qty",
                "@UserId", "@EsnXML"};
                    returnValue = db.ExCommand(objCompHash, "av_ServiceOrder_InsertUpdate", arrSpFieldSeq, "@soErrorMessage", out errorMessage);

                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;// "Technical error!";
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return returnValue;

        }
        private  DataTable ESNData2(List<ServiceOrderDetail> mslEsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("RowID", typeof(System.Int32));
            dt.Columns.Add("SKU", typeof(System.String));
            dt.Columns.Add("ESN", typeof(System.String));
            dt.Columns.Add("ICCID", typeof(System.String));


            DataRow row;
            int rowID = 1;
            if (mslEsnList != null && mslEsnList.Count > 0)
            {
                foreach (ServiceOrderDetail item in mslEsnList)
                {
                    row = dt.NewRow();
                    row["RowID"] = rowID;
                    row["SKU"] = item.SKU;
                    row["ESN"] = item.ESN;
                    row["ICCID"] = item.ICCID;
                   
                    dt.Rows.Add(row);
                    rowID += rowID;
                }
            }
            return dt;
        }
        public  List<ServiceOrderDetail> Validate_ServiceOrder_New2(ServiceOrders serviceOrder, out string errorMessage, out bool IsValidate)
        {
            IsValidate = true;
            List<ServiceOrderDetail> esnList = default;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                DataTable esnDT = ESNData2(serviceOrder.SODetail);



                try
                {
                    objCompHash.Add("@CompanyId", serviceOrder.CompanyId);
                    objCompHash.Add("@CustOrderNo", serviceOrder.CustomerOrderNumber);
                    //objCompHash.Add("@EsnXML", esnXML);
                    objCompHash.Add("@KittedSKUId", serviceOrder.KittedSKUId);
                    objCompHash.Add("@NumberOfKits", serviceOrder.Quantity);
                    objCompHash.Add("@esnTable", esnDT);

                    arrSpFieldSeq = new string[] { "@CompanyId", "@CustOrderNo", "@KittedSKUId", "@NumberOfKits", "@esnTable" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceOrder_Validate_New2", arrSpFieldSeq, "@soErrorMessage", out errorMessage);
                    esnList = PopulateEsnInfo(dt, out IsValidate);

                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return esnList;

        }

        private  DataTable ESNData3(List<ServiceOrderDetail> mslEsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("RowID", typeof(System.Int32));
            dt.Columns.Add("SKU", typeof(System.String));
            dt.Columns.Add("ESN", typeof(System.String));
            dt.Columns.Add("ICCID", typeof(System.String));


            DataRow row;
            int rowID = 1;
            if (mslEsnList != null && mslEsnList.Count > 0)
            {
                foreach (ServiceOrderDetail item in mslEsnList)
                {
                    row = dt.NewRow();
                    row["RowID"] = item.RowNumber;
                    row["SKU"] = item.SKU.Replace("ESN(","").Replace(")","");
                    row["ESN"] = item.ESN;
                    row["ICCID"] = item.ICCID.Replace("ICCID(", "").Replace(")", "");

                    dt.Rows.Add(row);
                    rowID += rowID;
                }
            }
            return dt;
        }

        public  List<ServiceOrderDetail> Validate_ServiceOrder_New3(ServiceOrders serviceOrder, out string errorMessage, out bool IsValidate)
        {
            IsValidate = true;
            List<ServiceOrderDetail> esnList = default;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                DataTable esnDT = ESNData3(serviceOrder.SODetail);



                try
                {
                    objCompHash.Add("@CompanyId", serviceOrder.CompanyId);
                    objCompHash.Add("@CustOrderNo", serviceOrder.CustomerOrderNumber);
                    //objCompHash.Add("@EsnXML", esnXML);
                    objCompHash.Add("@KittedSKUId", serviceOrder.KittedSKUId);
                    objCompHash.Add("@NumberOfKits", serviceOrder.Quantity);
                    objCompHash.Add("@esnTable", esnDT);

                    arrSpFieldSeq = new string[] { "@CompanyId", "@CustOrderNo", "@KittedSKUId", "@NumberOfKits", "@esnTable" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceOrder_Validate_New3", arrSpFieldSeq, "@soErrorMessage", out errorMessage);
                    esnList = PopulateEsnInfo(dt, out IsValidate);

                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return esnList;

        }
        public  List<ServiceOrderDetail> Validate_ServiceOrder_New(ServiceOrders serviceOrder, out string errorMessage, out bool IsValidate)
        {
            IsValidate = true;
            List<ServiceOrderDetail> esnList = default;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                string esnXML = clsGeneral.SerializeObject(serviceOrder.SODetail);
                string esns = "<ItemCompanyGUID p3:nil=\"true\" xmlns:p3=\"http://www.w3.org/2001/XMLSchema-instance\" />";
                string iccid = "<MappedItemCompanyGUID p3:nil=\"true\" xmlns:p3=\"http://www.w3.org/2001/XMLSchema-instance\" />";
                string str = "<ItemCompanyGUID>0</ItemCompanyGUID><MappedItemCompanyGUID>0</MappedItemCompanyGUID>";
                // string esnXML = clsGeneral.SerializeObject(esnAssignment.EsnList);
                esnXML = esnXML.Replace(esns, "");
                esnXML = esnXML.Replace(iccid, "");
                esnXML = esnXML.Replace(str, "");

                try
                {
                    objCompHash.Add("@CompanyId", serviceOrder.CompanyId);
                    objCompHash.Add("@CustOrderNo", serviceOrder.CustomerOrderNumber);
                    objCompHash.Add("@EsnXML", esnXML);
                    objCompHash.Add("@KittedSKUId", serviceOrder.KittedSKUId);
                    objCompHash.Add("@NumberOfKits", serviceOrder.Quantity);

                    arrSpFieldSeq = new string[] { "@CompanyId", "@CustOrderNo", "@EsnXML", "@KittedSKUId", "@NumberOfKits" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceOrder_Validate_New", arrSpFieldSeq, "@soErrorMessage", out errorMessage);
                    esnList = PopulateEsnInfo(dt, out IsValidate);

                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return esnList;

        }
        public  int ServiceOrder_InsertUpdate_New(ServiceOrders serviceOrder, int userId, out string errorMessage)
        {
            int returnValue = 0;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                DataTable esnDT = ESNData(serviceOrder.SODetail);
                //string esnXML = clsGeneral.SerializeObject(serviceOrder.SODetail);
                string reqestData = clsGeneral.SerializeObject(serviceOrder);
                try
                {
                    objCompHash.Add("@ServiceOrderId", serviceOrder.ServiceOrderId);
                    objCompHash.Add("@ServiceOrderNo", serviceOrder.ServiceOrderNumber);
                    objCompHash.Add("@CustOrderNo", serviceOrder.CustomerOrderNumber);
                    objCompHash.Add("@OrderDate", DateTime.Now);
                    objCompHash.Add("@KittedSKUId", serviceOrder.KittedSKUId);
                    objCompHash.Add("@Qty", serviceOrder.Quantity);
                    objCompHash.Add("@UserId", userId);
                    // objCompHash.Add("@EsnXML", esnXML);
                    objCompHash.Add("@av_ServiceOrderDetail", esnDT);

                    arrSpFieldSeq = new string[] { "@ServiceOrderId", "@ServiceOrderNo", "@CustOrderNo", "@OrderDate", "@KittedSKUId", "@Qty", "@UserId", "@av_ServiceOrderDetail" };
                    returnValue = db.ExCommand(objCompHash, "av_ServiceOrder_InsertUpdate_New", arrSpFieldSeq, "@soErrorMessage", out errorMessage);
                    ServiceOrderLogInsert(returnValue, reqestData, userId, errorMessage);

                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;// "Technical error!";
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return returnValue;

        }
        public int ServiceOrder_NonESN_InsertUpdate(ServiceOrders serviceOrder, int userId, out string errorMessage)
        {
            int returnValue = 0;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                //DataTable esnDT = ESNData(serviceOrder.SODetail);
                //string esnXML = clsGeneral.SerializeObject(serviceOrder.SODetail);
                string reqestData = clsGeneral.SerializeObject(serviceOrder);
                try
                {
                    objCompHash.Add("@ServiceOrderId", serviceOrder.ServiceOrderId);
                    objCompHash.Add("@ServiceOrderNo", serviceOrder.ServiceOrderNumber);
                    objCompHash.Add("@CustOrderNo", serviceOrder.CustomerOrderNumber);
                    objCompHash.Add("@OrderDate", DateTime.Now);
                    objCompHash.Add("@KittedSKUId", serviceOrder.KittedSKUId);
                    objCompHash.Add("@Qty", serviceOrder.Quantity);
                    objCompHash.Add("@UserId", userId);
                    
                    arrSpFieldSeq = new string[] { "@ServiceOrderId", "@ServiceOrderNo", "@CustOrderNo", "@OrderDate", "@KittedSKUId", "@Qty", "@UserId" };
                    returnValue = db.ExCommand(objCompHash, "av_ServiceOrder_NonESN_InsertUpdate", arrSpFieldSeq, "@soErrorMessage", out errorMessage);
                    ServiceOrderLogInsert(returnValue, reqestData, userId, errorMessage);

                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;// "Technical error!";
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return returnValue;

        }

        public int GenerateServiceOrder()
        {
            int returnValue = 0;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
               // DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@ServiceOrderNo", 1);

                    arrSpFieldSeq = new string[] { "@ServiceOrderNo" };
                    returnValue = Convert.ToInt32(db.ExecuteScalar(objCompHash, "av_GetServiceOrder", arrSpFieldSeq));
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
            return returnValue;
        }
        public  List<ServiceOrders> GetServiceOrders(ServiceOrders serviceOrder)
        {
            List<ServiceOrders> serviceOrders = default;// new List<ServiceOrders>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@ServiceOrderId", serviceOrder.ServiceOrderId);
                    objCompHash.Add("@ServiceOrderNo", serviceOrder.ServiceOrderNumber);
                    objCompHash.Add("@CustOrderNo", serviceOrder.CustomerOrderNumber);
                    objCompHash.Add("@KittedSKU", serviceOrder.SKU);
                    objCompHash.Add("@ESN", serviceOrder.ESN);
                    objCompHash.Add("@DateFrom", string.IsNullOrWhiteSpace(serviceOrder.DateFrom) ? null : serviceOrder.DateFrom);
                    objCompHash.Add("@DateTo", string.IsNullOrWhiteSpace(serviceOrder.DateTo) ? null : serviceOrder.DateTo);
                    objCompHash.Add("@CompanyId", serviceOrder.CompanyId);

                    arrSpFieldSeq = new string[] { "@ServiceOrderId", "@ServiceOrderNo", "@CustOrderNo", "@KittedSKU", "@ESN", "@DateFrom", "@DateTo", "@CompanyId" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceOrderSelect", arrSpFieldSeq);
                    serviceOrders = PopulateServiceOrders(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //serviceOrders = null;
                    //throw new Exception(objExp.Message.ToString());

                }
                finally
                {
                    //db = null;
                }
            }
            return serviceOrders;

        }
        public  List<SOSKUSummary> GetServiceOrderSummary(ServiceOrders serviceOrder)
        {
            List<SOSKUSummary> serviceOrders = default;// new List<SOSKUSummary>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@ServiceOrderNo", serviceOrder.ServiceOrderNumber);
                    objCompHash.Add("@CustOrderNo", serviceOrder.CustomerOrderNumber);
                    objCompHash.Add("@KittedSKU", serviceOrder.SKU);
                    objCompHash.Add("@ESN", serviceOrder.ESN);
                    objCompHash.Add("@DateFrom", string.IsNullOrWhiteSpace(serviceOrder.DateFrom) ? null : serviceOrder.DateFrom);
                    objCompHash.Add("@DateTo", string.IsNullOrWhiteSpace(serviceOrder.DateTo) ? null : serviceOrder.DateTo);
                    objCompHash.Add("@CompanyId", serviceOrder.CompanyId);

                    arrSpFieldSeq = new string[] { "@ServiceOrderNo", "@CustOrderNo", "@KittedSKU", "@ESN", "@DateFrom", "@DateTo", "@CompanyId" };
                    dt = db.GetTableRecords(objCompHash, "av_ServiceOrderSKU_Summary", arrSpFieldSeq);
                    serviceOrders = PopulateSKUSummary(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //serviceOrders = null;
                    //throw new Exception(objExp.Message.ToString());

                }
                finally
                {
                    //db = null;
                }
            }
            return serviceOrders;

        }
        public  ServiceOrders GetServiceOrderDetail(int serviceOrderId)
        {
            ServiceOrders serviceOrders = default;// new ServiceOrders();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default; // new DataSet();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@ServiceOrderId", serviceOrderId);


                    arrSpFieldSeq = new string[] { "@ServiceOrderId" };
                    ds = db.GetDataSet(objCompHash, "av_ServiceOrderSelect", arrSpFieldSeq);
                    serviceOrders = PopulateServiceOrderDetail(ds);

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
            return serviceOrders;

        }


        private static DataTable ESNData(List<ServiceOrderDetail> EsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ItemCompanyGUID", typeof(System.Int32));
            dt.Columns.Add("Esn", typeof(System.String));
            dt.Columns.Add("IsPrinted", typeof(System.Boolean));
            dt.Columns.Add("MappedItemCompanyGUID", typeof(System.Int32));
            dt.Columns.Add("ICCID", typeof(System.String));
            dt.Columns.Add("RowNumber", typeof(System.Int32));

            DataRow row;

            if (EsnList != null && EsnList.Count > 0)
            {
                foreach (ServiceOrderDetail item in EsnList)
                {
                    row = dt.NewRow();
                    row["ItemCompanyGUID"] = item.ItemCompanyGUID;
                    row["Esn"] = item.ESN;
                    row["IsPrinted"] = item.IsPrint;
                    row["MappedItemCompanyGUID"] = item.MappedItemCompanyGUID;
                    row["ICCID"] = item.ICCID;
                    row["RowNumber"] = item.RowNumber;

                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        private  List<ServiceOrderDetail> PopulateEsnInfo(DataTable dt, out bool IsValidate)
        {
            IsValidate = true;
            List<ServiceOrderDetail> esnList = new List<ServiceOrderDetail>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    ServiceOrderDetail esnDetail = new ServiceOrderDetail();

                    esnDetail.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    esnDetail.IsPrint = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsPrint", false, false));
                    esnDetail.ValidationMsg = clsGeneral.getColumnData(dataRow, "ValidationMsg", string.Empty, false) as string;
                    esnDetail.ICCIDValidationMsg = clsGeneral.getColumnData(dataRow, "ICCIDValidationMsg", string.Empty, false) as string;
                    esnDetail.BatchNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
                    esnDetail.ICCID = clsGeneral.getColumnData(dataRow, "ICCID", string.Empty, false) as string;
                    esnDetail.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    esnDetail.MappedSKU = clsGeneral.getColumnData(dataRow, "MappedSKU", string.Empty, false) as string;
                    esnDetail.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                    esnDetail.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    esnDetail.MappedItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MappedItemCompanyGUID", 0, false));
                    esnDetail.OrderDetailId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderDetailId", 0, false));
                    esnDetail.Id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ROWID", 0, false));
                    esnDetail.IsSim = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsSim", false, false));
                    esnDetail.Qty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                    esnDetail.RowNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RowNumber", 0, false));
                    esnDetail.KitID = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "KitID", 0, false));
                    esnList.Add(esnDetail);
                    if (!string.IsNullOrEmpty(esnDetail.ValidationMsg) && IsValidate)// && string.IsNullOrEmpty(esnDetail.ICCIDValidationMsg))
                        IsValidate = false;
                    if (!string.IsNullOrEmpty(esnDetail.ICCIDValidationMsg) && IsValidate)
                        IsValidate = false;
                }
            }
            return esnList;

        }
        private  ServiceOrders PopulateServiceOrderDetail(DataSet ds)
        {
            List<ServiceOrderDetail> esnList = new List<ServiceOrderDetail>();
            ServiceOrders serviceOrder = new ServiceOrders();
            bool IsValidate = false;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    //serviceOrder = new ServiceOrders();
                    serviceOrder.ServiceOrderNumber = clsGeneral.getColumnData(dataRow, "ServiceOrderNo", string.Empty, false) as string;
                    //serviceOrder.IsPrint = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsPrinted", false, false));
                    serviceOrder.CustomerOrderNumber = clsGeneral.getColumnData(dataRow, "CustOrderNo", string.Empty, false) as string;
                    serviceOrder.OrderDate = clsGeneral.getColumnData(dataRow, "OrderDate", string.Empty, false) as string;
                    serviceOrder.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    serviceOrder.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                    serviceOrder.ServiceOrderId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ServiceOrderId", 0, false));
                    serviceOrder.KittedSKUId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "KittedSKUId", 0, false));
                    serviceOrder.KittedSKUId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "KittedSKUId", 0, false));
                    serviceOrder.CompanyId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyId", 0, false));
                    serviceOrder.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;



                }
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    esnList = PopulateEsnInfo(ds.Tables[1], out IsValidate);
                    
                }
                serviceOrder.SODetail = esnList;
            }
            return serviceOrder;

        }
        private List<ServiceOrders> PopulateServiceOrders(DataTable dt)
        {
            List<ServiceOrders> soList = new List<ServiceOrders>();
            ServiceOrders serviceOrder = null;

            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    serviceOrder = new ServiceOrders();
                    serviceOrder.ServiceOrderNumber = clsGeneral.getColumnData(dataRow, "ServiceOrderNo", string.Empty, false) as string;
                    //serviceOrder.IsPrint = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsPrinted", false, false));
                    serviceOrder.CustomerOrderNumber = clsGeneral.getColumnData(dataRow, "CustOrderNo", string.Empty, false) as string;
                    serviceOrder.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    serviceOrder.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    serviceOrder.OrderDate = clsGeneral.getColumnData(dataRow, "OrderDate", string.Empty, false) as string;
                    serviceOrder.OrderDate2 = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "OrderDate2", DateTime.Now, false) );
                    serviceOrder.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    serviceOrder.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                    serviceOrder.ServiceOrderId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ServiceOrderId", 0, false));
                    soList.Add(serviceOrder);
                    
                }
            }
            return soList;

        }
        private  List<ServiceOrderDetail> PopulateEsns(DataTable dt)
        {
            List<ServiceOrderDetail> esnList = new List<ServiceOrderDetail>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    ServiceOrderDetail esnDetail = new ServiceOrderDetail();
                    esnDetail.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    esnDetail.Id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ID", 0, false));
                    esnList.Add(esnDetail);
                    
                }
            }
            return esnList;

        }
        private List<SOSKUSummary> PopulateSKUSummary(DataTable dt)
        {
            SOSKUSummary skuDetail = null;
            List<SOSKUSummary> skuList = new List<SOSKUSummary>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt.Rows)
                {
                    skuDetail = new SOSKUSummary();
                    skuDetail.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    skuDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                    skuList.Add(skuDetail);

                }
            }
            return skuList;

        }

    }
}