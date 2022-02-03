using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.RMA;
using SV.Framework.Models.Common;
using System.Data;
using System.Collections;

namespace SV.Framework.DAL.RMA
{
    public class RmaReportOperation : BaseCreateInstance
    {
        public List<RmaInfo> GetRMAReport(int companyID, string fromDate, string toDate, int rmaStatusID, int esnStatusID, int triageStatusID, int receiveStatusID)
        {
            List<RmaInfo> rmaList = default;// new List<RmaInfo>();
            using (DBConnect db = new DBConnect())
            {
                //RmaInfo rmaInfo = default;// new RmaInfo();
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@FromDate", fromDate);
                    objCompHash.Add("@ToDate", toDate);
                    objCompHash.Add("@RmaStatusID", rmaStatusID);
                    objCompHash.Add("@EsnStatusID", esnStatusID);
                    objCompHash.Add("@TriageStatusID", triageStatusID);
                    objCompHash.Add("@ReceiveStatusID", receiveStatusID);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@RmaStatusID", "@EsnStatusID", "@TriageStatusID", "@ReceiveStatusID" };
                    ds = db.GetDataSet(objCompHash, "AV_RMADetail_Report", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        rmaList = PopulateRmaReport(ds);
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); ////throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return rmaList;
        }

        public  List<RMAEsnDetail> GetRmaEsnOnlyReport(int companyID, string RMANumber, string ESN, DateTime fromDate, DateTime toDate, int esnStatusID, int rmaStatusID)
        {
            List<RMAEsnDetail> rmaList = default;// new List<RMAEsnDetail>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default; new DataTable();

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
                        rmaList = new List<RMAEsnDetail>();

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
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return rmaList;
        }

        public List<CustomerRmaStatus> GetCustomerRmaStatusSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            List<CustomerRmaStatus> rmaStatusList = default;// new List<CustomerRmaStatus>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();

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
                        rmaStatusList = new List<CustomerRmaStatus>();
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
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                   // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return rmaStatusList;
        }

        public List<CustomerRmaEsnStatus> GetCustomerRmaESNStatusSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            List<CustomerRmaEsnStatus> rmaStatusList = default;
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();

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
                    Logger.LogMessage(ex, this); // throw ex;
                }
                finally
                {
                    //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return rmaStatusList;
        }
        public List<CustomerRmaTriageStatus> GetCustomerRmaTriageStatusSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            List<CustomerRmaTriageStatus> rmaStatusList = default;
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();

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
                        rmaStatusList = new List<CustomerRmaTriageStatus>();
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
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return rmaStatusList;
        }
        public  List<CustomerRmaDisposition> GetCustomerRmaDispositionSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            List<CustomerRmaDisposition> rmaStatusList = default;// new List<CustomerRmaDisposition>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();

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
                        rmaStatusList = new List<CustomerRmaDisposition>();
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
                    Logger.LogMessage(ex, this); // throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return rmaStatusList;
        }
        public  List<CustomerRmaShipmentPaidBy> GetCustomerRmaShipmentPaidBySummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            List<CustomerRmaShipmentPaidBy> rmaStatusList = default;// new List<CustomerRmaShipmentPaidBy>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();

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
                        rmaStatusList = new List<CustomerRmaShipmentPaidBy>();
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
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return rmaStatusList;
        }

        public List<CustomerRmaReason> GetCustomerRmaReasonSummary(int companyID, string fromDate, string toDate, int userID, string summaryBy)
        {
            List<CustomerRmaReason> rmaStatusList = default;
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();

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
                        rmaStatusList = new List<CustomerRmaReason>();
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
                    Logger.LogMessage(ex, this); // throw ex;
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return rmaStatusList;
        }
        





        private List<RmaInfo> PopulateRmaReport(DataSet ds)
        {
            List<RmaInfo> rmaList = default;

            RmaInfo rmaInfo = default;
            List<RMADetail> rmaDetails = default;
            RMADetail rmaDetail = default;
            int RMAReceiveGUID = 0;
            string dispostion = string.Empty, warranty = string.Empty;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                rmaList = new List<RmaInfo>();
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    rmaInfo = new RmaInfo();
                    rmaDetails = new List<RMADetail>();

                    rmaInfo.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                    //rmaInfo.CustomerAddress1 = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                    rmaInfo.CustomerCity = clsGeneral.getColumnData(dataRow, "ContactCity", string.Empty, false) as string;
                    rmaInfo.CustomerContactName = clsGeneral.getColumnData(dataRow, "RmaContactName", string.Empty, false) as string;
                    //rmaInfo.CustomerContactNumber = clsGeneral.getColumnData(dataRow, "ContactPhone", string.Empty, false) as string;
                    //rmaInfo.CustomerCountry = "US"; //clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                    //rmaInfo.CustomerEmail = clsGeneral.getColumnData(dataRow, "ContactEmail", string.Empty, false) as string;
                    //rmaInfo.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                    rmaInfo.CustomerRMANumber = clsGeneral.getColumnData(dataRow, "CustomerRmaNumber", string.Empty, false) as string;
                    rmaInfo.CustomerState = clsGeneral.getColumnData(dataRow, "ContactState", string.Empty, false) as string;
                    rmaInfo.CustomerZipCode = clsGeneral.getColumnData(dataRow, "ContactZip", string.Empty, false) as string;
                    rmaInfo.RmaDate = clsGeneral.getColumnData(dataRow, "RmaDate", string.Empty, false) as string;
                    rmaInfo.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                    rmaInfo.RmaStatus = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    //rmaInfo.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;
                    rmaInfo.TriageStatus = clsGeneral.getColumnData(dataRow, "TriageStatus", string.Empty, false) as string;
                    rmaInfo.ReceiveStatus = clsGeneral.getColumnData(dataRow, "ReceiveStatus", string.Empty, false) as string;
                    rmaInfo.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_NUM", string.Empty, false) as string;
                    //rmaInfo.RmaDate = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;


                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[1].Select("RmaGUID = " + rmaInfo.RmaGUID))
                        {
                            rmaDetail = new RMADetail();
                            rmaDetail.RmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(row, "RmaDetGUID", 0, false));
                            rmaDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(row, "Quantity", 0, false));
                            Disposition enumDispostion = (Disposition)Convert.ToInt32(clsGeneral.getColumnData(row, "Disposition", 0, false));
                            dispostion = enumDispostion.ToString();
                            Warranty enumWarranty = (Warranty)Convert.ToInt32(clsGeneral.getColumnData(row, "Warranty", 0, false));
                            warranty = enumWarranty.ToString();

                            rmaDetail.Disposition = dispostion; // clsGeneral.getColumnData(row, "Disposition", string.Empty, false) as string;
                            rmaDetail.ESN = clsGeneral.getColumnData(row, "ESN", string.Empty, false) as string;
                            rmaDetail.CategoryName = clsGeneral.getColumnData(row, "CategoryName", string.Empty, false) as string;
                            rmaDetail.ProductName = clsGeneral.getColumnData(row, "ItemName", string.Empty, false) as string;
                            rmaDetail.Reason = clsGeneral.getColumnData(row, "RMAReason", string.Empty, false) as string;
                            rmaDetail.SKU = clsGeneral.getColumnData(row, "SKU", string.Empty, false) as string;
                            rmaDetail.Status = clsGeneral.getColumnData(row, "EsnStatus", string.Empty, false) as string;
                            rmaDetail.Warranty = warranty;//clsGeneral.getColumnData(row, "Warranty", string.Empty, false) as string;
                            rmaDetail.TriageNotes = clsGeneral.getColumnData(row, "TriageNotes", string.Empty, false) as string;
                            rmaDetail.TriageStatus = clsGeneral.getColumnData(row, "TriageStatus", string.Empty, false) as string;
                            //rmaDetail.a = clsGeneral.getColumnData(row, "Reason", string.Empty, false) as string;
                            rmaDetails.Add(rmaDetail);
                        }
                    }

                    rmaInfo.RMADetails = rmaDetails;

                    rmaList.Add(rmaInfo);
                }
            }
            return rmaList;
        }

    }
}
