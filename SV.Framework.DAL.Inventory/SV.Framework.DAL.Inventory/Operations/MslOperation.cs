using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Inventory 
{
    public class MslOperation : BaseCreateInstance
    {
        public List<EsnMslDetail> AssignMSL2FulfillmentESN(string ESN, int userID, int companyID, out int poRecordCount, out string poErrorMessage)
        {
            poRecordCount = 0;
            poErrorMessage = default;
            List<EsnMslDetail> esnList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@ESN", ESN);
                    objCompHash.Add("@piUserID", userID);
                    objCompHash.Add("@piCompanyID", companyID);

                    arrSpFieldSeq = new string[] { "@ESN", "@piUserID", "@piCompanyID" };
                    dt = db.GetTableRecords(objCompHash, "Av_PurchaseOrder_MSL_UPDATE", arrSpFieldSeq, "@poRecordCount", "poErrorMessage", out poRecordCount, out poErrorMessage);
                    esnList = PopulateMSL(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return esnList;
        }
        public List<EsnMslDetail> GetMissingMSL(int companyID)
        {
            List<EsnMslDetail> esnList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();
                
                try
                {
                    objCompHash.Add("@piCompanyID", companyID);

                    arrSpFieldSeq = new string[] { "@piCompanyID" };
                    dt = db.GetTableRecords(objCompHash, "Av_Missing_MSL_Select", arrSpFieldSeq);
                    esnList = PopulateMSL(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                   // db = null;
                }
            }
            return esnList;

        }
        public  List<ESNLog> GetEsnLog(string esn)
        {
            List<ESNLog> esnLogDetail = default;//new List<ESNLog>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                arrSpFieldSeq = new string[] { "@ESN" };
                DataTable dataTable = default;//null;
                try
                {
                    objCompHash.Add("@ESN", esn);

                    dataTable = db.GetTableRecords(objCompHash, "av_ESNLog_Select", arrSpFieldSeq);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        esnLogDetail = new List<ESNLog>();
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            ESNLog esnLog = new ESNLog();
                            esnLog.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                            esnLog.UpdateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UpdateDate", 0, false));
                            esnLog.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                            esnLog.ModuleName = clsGeneral.getColumnData(dataRow, "Module", string.Empty, false) as string;
                            esnLogDetail.Add(esnLog);
                        }
                    }
                }
                catch (Exception exp)
                {
                    throw exp;
                }

            }
            return esnLogDetail;
        }
        public bool MslEsnInsertUpdate(List<EsnUpload> mslEsnList, int itemCompanyGUID, int userID, string fileName, string comment, 
            DateTime uploadDate, out int insertCout, out int updateCount, out string errorMessage)
        {
            insertCout = 0;
            updateCount = 0;
            errorMessage = default;
            bool returnValue = false;
            using (DBConnect db = new DBConnect())
            {
                string esnXML = clsGeneral.SerializeObject(mslEsnList);
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                //int retIntVal = 0;
                try
                {
                    objCompHash.Add("@piEsnXML", esnXML);
                    objCompHash.Add("@itemCompanyGUID", itemCompanyGUID);
                    objCompHash.Add("@UserID", userID);
                    objCompHash.Add("@FileName", fileName);
                    objCompHash.Add("@Comment", comment);
                    objCompHash.Add("@UploadDate", uploadDate);

                    arrSpFieldSeq = new string[] { "@piEsnXML", "@itemCompanyGUID", "@UserID", "@FileName", "@Comment", "@UploadDate" };
                    db.ExeCommand(objCompHash, "Av_MSL_ESN_InsertUpdate", arrSpFieldSeq, "@poInsertCount", out insertCout, "@poUpdateCount", out updateCount, "@poErrorMessage", out errorMessage);
                    //int.TryParse(retVal.ToString(), out retIntVal);
                    //if (retIntVal == 0)
                    if (errorMessage == string.Empty)
                        returnValue = true;
                    else
                        returnValue = false;
                }
                catch (Exception objExp)
                {
                    returnValue = false;
                    Logger.LogMessage(objExp, this);//  throw new Exception(objExp.Message.ToString());

                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnValue;
        }
        public bool MslEsnInsertUpdateNew(EsnHeaderUpload esnInfo, string fileName, Int64 orderTransferID, out int insertCout, out int updateCount, out string errorMessage)
        {
            insertCout = 0;
            updateCount = 0;
            errorMessage = default;

            List<EsnUploadNew> mslEsnList = esnInfo.ESNs;
            bool returnValue = false;

            using (DBConnect db = new DBConnect())
            {
                DataTable dt = ESNDataNEW(mslEsnList);
                //string esnXML = clsGeneral.SerializeObject(mslEsnList);
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                int retIntVal = 0;
                try
                {
                    objCompHash.Add("@CustomerAccountNumber", esnInfo.CustomerAccountNumber);
                    objCompHash.Add("@ESNHeaderId", esnInfo.ESNHeaderId);
                    objCompHash.Add("@OrderNumber", esnInfo.OrderNumber);
                    objCompHash.Add("@CustOrderNumber", esnInfo.CustomerOrderNumber);
                    objCompHash.Add("@OrderDate", Convert.ToDateTime(esnInfo.OrderDate).ToString("yyyy-MM-dd"));
                    objCompHash.Add("@ShipDate", Convert.ToDateTime(esnInfo.ShipDate).ToString("yyyy-MM-dd"));
                    objCompHash.Add("@ShipVia", esnInfo.Shipvia);
                    objCompHash.Add("@TrackingNumber", esnInfo.TrackingNumber);
                    objCompHash.Add("@SKU", esnInfo.SKU);
                    objCompHash.Add("@OrderQty", esnInfo.OrderQty);
                    objCompHash.Add("@ShipQty", esnInfo.ShipQty);
                    objCompHash.Add("@UnitPrice", esnInfo.UnitPrice);
                    //objCompHash.Add("@itemCompanyGUID", itemCompanyGUID);
                    objCompHash.Add("@UserID", esnInfo.UserId);
                    objCompHash.Add("@FileName", fileName);
                    objCompHash.Add("@Comment", esnInfo.Comments);
                    objCompHash.Add("@UploadDate", DateTime.Now);
                    objCompHash.Add("@IsESNRequired", esnInfo.IsESNRequired);
                    objCompHash.Add("@ReceivedAs", esnInfo.ReceivedAs);
                    objCompHash.Add("@IsInspection", esnInfo.IsInspection);
                    objCompHash.Add("@SupplierName", esnInfo.SupplierName);
                    objCompHash.Add("@OrderTransferID", orderTransferID);

                    objCompHash.Add("@av_ESNUpload", dt);

                    arrSpFieldSeq = new string[] { "@CustomerAccountNumber","@ESNHeaderId", "@OrderNumber", "@CustOrderNumber", "@OrderDate", "@ShipDate", "@ShipVia",
                    "@TrackingNumber", "@SKU", "@OrderQty", "@ShipQty", "@UnitPrice", "@UserID", "@FileName", "@Comment", "@UploadDate","@IsESNRequired",
                    "@ReceivedAs", "@IsInspection","@SupplierName","@OrderTransferID","@av_ESNUpload" };
                    db.ExecCommand(objCompHash, "Av_MSL_ESN_InsertUpdateNew", arrSpFieldSeq, "@poInsertCount", out insertCout, "@poUpdateCount", out updateCount, "@poErrorMessage", out errorMessage, "@InvalidStock", out retIntVal);
                    //int.TryParse(retVal.ToString(), out retIntVal);
                    if (retIntVal == 1)
                        errorMessage = "Received quantity cannot be greater than stock in hand";

                    if (errorMessage == string.Empty)
                        returnValue = true;
                    else
                        returnValue = false;
                }
                catch (Exception objExp)
                {
                    errorMessage = objExp.Message;
                    returnValue = false;
                    Logger.LogMessage(objExp, this);//   throw new Exception(objExp.Message);

                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnValue;
        }
        public string GenerateOrderNumber()
        {
            string returnValue = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //DataTable dt = new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {

                    objCompHash.Add("@OrderNo", 1);

                    arrSpFieldSeq = new string[] { "@OrderNo" };
                    returnValue = Convert.ToString(db.ExecuteScalar(objCompHash, "av_GetESNHeaderOrderNumber", arrSpFieldSeq));

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return returnValue;

        }
        public int ESNDelete(string ESN)
        {
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@ESN", ESN);
                    //objCompHash.Add("@IsSIM", isSIM);

                    arrSpFieldSeq = new string[] { "@ESN" };
                    object obj = db.ExecuteScalar(objCompHash, "av_MSL_ESN_SingleDelete", arrSpFieldSeq);
                    if (obj != null)
                        returnValue = Convert.ToInt32(obj);


                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                   // db = null;
                }
            }
            return returnValue;
        }
        
        public  int ESNDelete(List<EsnUploadNew> esnList)
        {
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {

                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                DataTable dt = ESNDataTable(esnList);
                try
                {
                    objCompHash.Add("@av_ESNType", dt);


                    arrSpFieldSeq = new string[] { "@av_ESNType" };
                    object obj = db.ExecuteScalar(objCompHash, "av_ESN_Delete", arrSpFieldSeq);
                    if (obj != null)
                        returnValue = Convert.ToInt32(obj);


                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return returnValue;
        }
        public List<CompanySKUno> GetCompanySKUList(int companyID, int isSIM)
        {
            List<CompanySKUno> skuList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@IsSIM", isSIM);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@IsSIM" };
                    dt = db.GetTableRecords(objCompHash, "av_Company_SKUs_select", arrSpFieldSeq);
                    skuList = PopulateSKU(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return skuList;
        }
        public  List<CompanySKUno> GetCompanySKUs(int companyID, int isSIM)
        {
            List<CompanySKUno> skuList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@IsSIM", isSIM);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@IsSIM" };
                    dt = db.GetTableRecords(objCompHash, "av_Company_SKUs_select", arrSpFieldSeq);
                    skuList = PopulateSKUs(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return skuList;
        }
        public List<CompanySKUno> GetCompanySKUsNew(int companyID, int isSIM, string ModelNumber)
        {
            List<CompanySKUno> skuList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@IsSIM", isSIM);
                    objCompHash.Add("@ModelNumber", ModelNumber);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@IsSIM", "@ModelNumber" };
                    dt = db.GetTableRecords(objCompHash, "av_Company_SKUs_select", arrSpFieldSeq);
                    skuList = PopulateSKUs(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return skuList;
        }

        public List<EsnUpload> MslESNs_Validate(List<EsnUpload> mslEsnList, int itemCompanyGUID, out string errorMessage, out string duplicateESN, out string simMessage, out bool isLTE, out bool isSim, out int returnValue, out string poEsnMessage)
        {
            List<EsnUpload> esnList = default;//null;
            poEsnMessage = string.Empty;
            errorMessage = string.Empty;
            duplicateESN = string.Empty;
            isLTE = false;
            simMessage = string.Empty;
            isSim = false;
            returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string esnXML = clsGeneral.SerializeObject(mslEsnList);
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@piEsnXML", esnXML);
                    objCompHash.Add("@itemCompanyGUID", itemCompanyGUID);

                    arrSpFieldSeq = new string[] { "@piEsnXML", "@itemCompanyGUID" };
                    dt = db.GetTableRecords(objCompHash, "Av_MSL_ESN_Validate", arrSpFieldSeq, "@poErrorMessage", "@poDuplicateESNMessage", "@poSimMessage", "@poLteAttribute", "@poIsSim", "@poEsnMessage", out errorMessage, out duplicateESN, out simMessage, out isLTE, out isSim, out returnValue, out poEsnMessage);
                    esnList = PopulateMslESN(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return esnList;
        }
        public List<EsnUploadNew> MslESNs_ValidateNew(List<EsnUploadNew> mslEsnList, int itemCompanyGUID, string OrderNumber, out string errorMessage, out string duplicateESN, out string simMessage, out bool isLTE, out bool isOrderNumber, out int returnValue, out string poEsnMessage)
        {
            poEsnMessage = string.Empty;
            errorMessage = string.Empty;
            duplicateESN = string.Empty;
            isLTE = false;
            simMessage = string.Empty;
            isOrderNumber = false;
            returnValue = 0;
            List<EsnUploadNew> esnList = default;
            using (DBConnect db = new DBConnect())
            {
                string esnXML = clsGeneral.SerializeObject(mslEsnList);
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();                
                try
                {
                    objCompHash.Add("@piEsnXML", esnXML);
                    objCompHash.Add("@itemCompanyGUID", itemCompanyGUID);
                    objCompHash.Add("@OrderNumber", OrderNumber);

                    arrSpFieldSeq = new string[] { "@piEsnXML", "@itemCompanyGUID", "@OrderNumber" };
                    dt = db.GetTableRecords(objCompHash, "Av_MSL_ESN_Validate", arrSpFieldSeq, "@poErrorMessage", "@poDuplicateESNMessage", "@poSimMessage", "@poLteAttribute", "@poOrderNumber", "@poEsnMessage", out errorMessage, out duplicateESN, out simMessage, out isLTE, out isOrderNumber, out returnValue, out poEsnMessage);
                    esnList = PopulateMslESNs(dt);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return esnList;
        }

        public List<EsnUploadNew> MslESNs_ValidateNew2(List<EsnUploadNew> mslEsnList, int itemCompanyGUID, string OrderNumber, out string errorMessage, out string duplicateESN, out string simMessage, out bool isLTE, out bool isOrderNumber, out int returnValue, out string poEsnMessage, out string poESNquarantine, out string poESNBoxIDs)
        {
            poESNBoxIDs = string.Empty;
            poEsnMessage = string.Empty;
            errorMessage = string.Empty;
            duplicateESN = string.Empty;
            poESNquarantine = string.Empty;
            isLTE = false;
            simMessage = string.Empty;
            isOrderNumber = false;
            returnValue = 0;
            List<EsnUploadNew> esnList = default;
            using (DBConnect db = new DBConnect())
            {
                DataTable esnTable = ESNData(mslEsnList);

                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@piEsnDT", esnTable);
                    objCompHash.Add("@itemCompanyGUID", itemCompanyGUID);
                    objCompHash.Add("@OrderNumber", OrderNumber);

                    arrSpFieldSeq = new string[] { "@piEsnDT", "@itemCompanyGUID", "@OrderNumber" };
                    // dt = db.GetTableRecords(objCompHash, "Av_MSL_ESN_ValidateNew", arrSpFieldSeq, "@poErrorMessage", "@poDuplicateESNMessage", "@poSimMessage", "@poLteAttribute", "@poOrderNumber", "@poEsnMessage", out errorMessage, out duplicateESN, out simMessage, out isLTE, out isOrderNumber, out returnValue, out poEsnMessage);
                    // dt = db.GetTableRecords(objCompHash, "Av_MSL_ESN_ValidateNew", arrSpFieldSeq, "@poErrorMessage", "@poMeidLengthMessage", "@poEsnLengthMessage", "@poLteAttribute", "@poOrderNumber", "@poEsnMessage", out errorMessage, out duplicateESN, out simMessage, out isLTE, out isOrderNumber, out returnValue, out poEsnMessage);
                    dt = db.GetTableRecords(objCompHash, "Av_MSL_ESN_ValidateNew", arrSpFieldSeq, "@poErrorMessage", "@poMeidLengthMessage", "@poEsnLengthMessage",
                        "@poLteAttribute", "@poOrderNumber", "@poEsnMessage", "@poESNquarantine", "@poESNBoxIDs", out errorMessage, out duplicateESN, out simMessage,
                        out isLTE, out isOrderNumber, out returnValue, out poEsnMessage, out poESNquarantine, out poESNBoxIDs);
                    esnList = PopulateMslESNs(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return esnList;
        }
        public List<EsnUploadNew> MslESNs_ValidateNew1(List<EsnUploadNew> mslEsnList, int itemCompanyGUID, string OrderNumber, Int64 OrderTransferID, out string errorMessage, 
            out string duplicateESN, out string simMessage, out bool isLTE, out bool isOrderNumber, out int returnValue, out string poEsnMessage, 
            out string poESNquarantine, out string poESNBoxIDs, out string poLocations)
        {
            poLocations = string.Empty;
            poESNBoxIDs = string.Empty;
            poEsnMessage = string.Empty;
            errorMessage = string.Empty;
            duplicateESN = string.Empty;
            poESNquarantine = string.Empty;
            isLTE = false;
            simMessage = string.Empty;
            isOrderNumber = false;
            returnValue = 0;
            List<EsnUploadNew> esnList = default;
            using (DBConnect db = new DBConnect())
            {
                DataTable esnTable = ESNDataNEW(mslEsnList);

                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@piEsnDT", esnTable);
                    objCompHash.Add("@itemCompanyGUID", itemCompanyGUID);
                    objCompHash.Add("@OrderNumber", OrderNumber);
                    objCompHash.Add("@OrderTransferID", OrderTransferID);

                    arrSpFieldSeq = new string[] { "@piEsnDT", "@itemCompanyGUID", "@OrderNumber", "@OrderTransferID" };
                    // dt = db.GetTableRecords(objCompHash, "Av_MSL_ESN_ValidateNew", arrSpFieldSeq, "@poErrorMessage", "@poDuplicateESNMessage", "@poSimMessage", "@poLteAttribute", "@poOrderNumber", "@poEsnMessage", out errorMessage, out duplicateESN, out simMessage, out isLTE, out isOrderNumber, out returnValue, out poEsnMessage);
                    // dt = db.GetTableRecords(objCompHash, "Av_MSL_ESN_ValidateNew", arrSpFieldSeq, "@poErrorMessage", "@poMeidLengthMessage", "@poEsnLengthMessage", "@poLteAttribute", "@poOrderNumber", "@poEsnMessage", out errorMessage, out duplicateESN, out simMessage, out isLTE, out isOrderNumber, out returnValue, out poEsnMessage);
                    dt = db.GetTableRecords(objCompHash, "Av_MSL_ESN_ValidateNew1", arrSpFieldSeq, "@poErrorMessage", "@poMeidLengthMessage", "@poEsnLengthMessage",
                        "@poLteAttribute", "@poOrderNumber", "@poEsnMessage", "@poESNquarantine", "@poESNBoxIDs", "@poLocations", out errorMessage, out duplicateESN, out simMessage,
                        out isLTE, out isOrderNumber, out returnValue, out poEsnMessage, out poESNquarantine, out poESNBoxIDs, out poLocations);
                    esnList = PopulateMslESNs(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return esnList;
        }

        public List<EsnHeaders> GetESNwithHeaderList(int companyID, string CustOrderNumber, string ShipFrom, string ShipTo, string ESN, string TrackingNumber, string SKU, int categoryID, string location)
        {
            List<EsnHeaders> headerList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@CustOrderNumber", CustOrderNumber);
                    objCompHash.Add("@ShipFrom", string.IsNullOrEmpty(ShipFrom) ? null : ShipFrom);
                    objCompHash.Add("@ShipTo", string.IsNullOrEmpty(ShipTo) ? null : ShipTo);
                    objCompHash.Add("@ESN", ESN);
                    objCompHash.Add("@TrackingNumber", TrackingNumber);
                    objCompHash.Add("@SKU", SKU);
                    objCompHash.Add("@CategoryID", categoryID);
                    objCompHash.Add("@Location", location);
                    // objCompHash.Add("@ESNHeaderId", 0);


                    arrSpFieldSeq = new string[] { "@CompanyID", "@CustOrderNumber", "@ShipFrom", "@ShipTo", "@ESN", "@TrackingNumber", "@SKU", 
                        "@CategoryID", "@Location" };
                    dt = db.GetTableRecords(objCompHash, "AV_EsnMslSelect", arrSpFieldSeq);
                    headerList = PopulateESNwithHeaders(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return headerList;
        }
        public EsnHeaders GetESNwithHeaderDetail(int ESNHeaderId)
        {
            EsnHeaders headerDetail = new EsnHeaders();
            using (DBConnect db = new DBConnect())
            {
                //DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;
                DataSet ds = default;//new DataSet();
                Hashtable objCompHash = new Hashtable();


                try
                {
                    //objCompHash.Add("@CompanyID", companyID);
                    //objCompHash.Add("@CustOrderNumber", CustOrderNumber);
                    //objCompHash.Add("@ShipFrom", string.IsNullOrEmpty(ShipFrom) ? null : ShipFrom);
                    //objCompHash.Add("@ShipTo", string.IsNullOrEmpty(ShipTo) ? null : ShipTo);
                    //objCompHash.Add("@ESN", ESN);
                    // objCompHash.Add("@TrackingNumber", TrackingNumber);
                    objCompHash.Add("@ESNHeaderId", ESNHeaderId);

                    arrSpFieldSeq = new string[] { "@ESNHeaderId" };
                    ds = db.GetDataSet(objCompHash, "AV_EsnMslSelect", arrSpFieldSeq);
                    headerDetail = PopulateESNwithHeaderDetail(ds);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);// throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //  db = null;
                }
            }
            return headerDetail;
        }
        private  List<EsnHeaders> PopulateESNwithHeaders(DataTable dt)
        {
            List<EsnHeaders> headerList = default;// new List<EsnHeaders>();
            EsnHeaders objESN = default;//null;


            if (dt != null && dt.Rows.Count > 0)
            {
                headerList = new List<EsnHeaders>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    objESN = new EsnHeaders();
                    objESN.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    objESN.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    objESN.OrderDate = clsGeneral.getColumnData(dataRow, "OrderDate", string.Empty, false) as string;
                    objESN.CustomerOrderNumber = clsGeneral.getColumnData(dataRow, "CustOrderNumber", string.Empty, false) as string;
                    objESN.ESNHeaderId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ESNHeaderId", 0, false));
                    objESN.OrderDate = clsGeneral.getColumnData(dataRow, "OrderDate", string.Empty, false) as string;
                    objESN.OrderNumber = clsGeneral.getColumnData(dataRow, "OrderNumber", string.Empty, false) as string;
                    objESN.OrderQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderQty", 0, false));
                    objESN.ShipQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShipQty", 0, false));
                    objESN.AssignedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AssignedQuantity", 0, false));

                    objESN.ShipDate = clsGeneral.getColumnData(dataRow, "ShipDate", string.Empty, false) as string;
                    //objESN.ICC_ID = clsGeneral.getColumnData(dataRow, "icc_id", string.Empty, false) as string;
                    objESN.Shipvia = clsGeneral.getColumnData(dataRow, "Shipvia", string.Empty, false) as string;
                    objESN.UnitPrice = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "UnitPrice", 0, false));

                    objESN.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    objESN.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    objESN.IsESN = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESN", 0, false));
                    objESN.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;

                    headerList.Add(objESN);
                }
            }
            return headerList;

        }
        private  EsnHeaders PopulateESNwithHeaderDetail(DataSet dataset)
        {
            // List<EsnHeaders> headerList = new List<EsnHeaders>();
            EsnHeaders headerDetail = default;//new EsnHeaders();
            EsnUploadNew esnInfo = default;//null;
            List<EsnUploadNew> esnList = default;//null;
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                headerDetail = new EsnHeaders();
                foreach (DataRow dataRow in dataset.Tables[0].Rows)
                {
                    esnList = new List<EsnUploadNew>();

                    //objESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    headerDetail.OrderDate = clsGeneral.getColumnData(dataRow, "OrderDate", string.Empty, false) as string;
                    headerDetail.CustomerOrderNumber = clsGeneral.getColumnData(dataRow, "CustOrderNumber", string.Empty, false) as string;
                    headerDetail.ESNHeaderId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ESNHeaderId", 0, false));
                    headerDetail.OrderDate = clsGeneral.getColumnData(dataRow, "OrderDate", string.Empty, false) as string;
                    headerDetail.OrderNumber = clsGeneral.getColumnData(dataRow, "OrderNumber", string.Empty, false) as string;
                    headerDetail.OrderQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderQty", 0, false));
                    headerDetail.ShipQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShipQty", 0, false));
                    headerDetail.CompanyAccountNumber = clsGeneral.getColumnData(dataRow, "CompanyAccountNumber", string.Empty, false) as string;
                    headerDetail.ShipDate = clsGeneral.getColumnData(dataRow, "ShipDate", string.Empty, false) as string;
                    //objESN.ICC_ID = clsGeneral.getColumnData(dataRow, "icc_id", string.Empty, false) as string;
                    headerDetail.Shipvia = clsGeneral.getColumnData(dataRow, "Shipvia", string.Empty, false) as string;
                    headerDetail.UnitPrice = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "UnitPrice", 0, false));
                    headerDetail.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    headerDetail.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    headerDetail.CategoryID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryID", 0, false));
                    headerDetail.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    headerDetail.ReceivedAs = clsGeneral.getColumnData(dataRow, "ReceivedAs", string.Empty, false) as string;
                    headerDetail.IsInspection = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsInspection", false, false));
                    headerDetail.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                    headerDetail.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    headerDetail.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    headerDetail.SupplierName = clsGeneral.getColumnData(dataRow, "SupplierName", string.Empty, false) as string;

                    if (dataset.Tables.Count > 1 && dataset.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataset.Tables[1].Rows)
                        {
                            esnInfo = new EsnUploadNew();
                            esnInfo.ESN = clsGeneral.getColumnData(row, "ESN", string.Empty, false) as string;
                            esnInfo.IMEI2 = clsGeneral.getColumnData(row, "IMEI2", string.Empty, false) as string;
                            esnInfo.MslNumber = clsGeneral.getColumnData(row, "BatchNumber", string.Empty, false) as string;
                            esnInfo.ICC_ID = clsGeneral.getColumnData(row, "icc_id", string.Empty, false) as string;
                            esnInfo.BoxID = clsGeneral.getColumnData(row, "BoxID", string.Empty, false) as string;
                            esnInfo.Location = clsGeneral.getColumnData(row, "Location", string.Empty, false) as string;
                            esnInfo.MeidDec = clsGeneral.getColumnData(row, "MeidDec", string.Empty, false) as string;
                            esnInfo.MeidHex = clsGeneral.getColumnData(row, "MeidHex", string.Empty, false) as string;
                            esnInfo.MSL = clsGeneral.getColumnData(row, "msl", string.Empty, false) as string;
                            esnInfo.OTKSL = clsGeneral.getColumnData(row, "OTKSL", string.Empty, false) as string;
                            esnInfo.SerialNumber = clsGeneral.getColumnData(row, "SerialNumber", string.Empty, false) as string;
                            esnInfo.InUse = Convert.ToBoolean(clsGeneral.getColumnData(row, "InUse", false, false));
                            esnInfo.EsnID = Convert.ToInt32(clsGeneral.getColumnData(row, "EsnID", 0, false));

                            esnList.Add(esnInfo);
                        }
                    }
                    headerDetail.EsnList = esnList;
                    // headerList.Add(objESN);
                }
            }
            return headerDetail;

        }
        private  List<EsnUploadNew> PopulateMslESNs(DataTable dataTable)
        {
            List<EsnUploadNew> esnList = default;// new List<EsnUploadNew>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                esnList = new List<EsnUploadNew>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    EsnUploadNew objESN = new EsnUploadNew();
                    objESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    objESN.MslNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
                    objESN.MeidDec = clsGeneral.getColumnData(dataRow, "MeidDec", string.Empty, false) as string;
                    objESN.MeidHex = clsGeneral.getColumnData(dataRow, "MeidHex", string.Empty, false) as string;
                    objESN.OTKSL = clsGeneral.getColumnData(dataRow, "OTKSL", string.Empty, false) as string;
                    objESN.Location = clsGeneral.getColumnData(dataRow, "Location", string.Empty, false) as string;
                    objESN.SerialNumber = clsGeneral.getColumnData(dataRow, "SerialNumber", string.Empty, false) as string;
                    //objESN.PALLETID = clsGeneral.getColumnData(dataRow, "PALLET", string.Empty, false) as string;
                    //objESN.CARTONID = clsGeneral.getColumnData(dataRow, "carton", string.Empty, false) as string;
                    //objESN.HEX = clsGeneral.getColumnData(dataRow, "HEX", string.Empty, false) as string;
                    objESN.ICC_ID = clsGeneral.getColumnData(dataRow, "icc_id", string.Empty, false) as string;
                    objESN.ErrorMessage = clsGeneral.getColumnData(dataRow, "ErrorMessage", string.Empty, false) as string;

                    objESN.EsnID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "esnID", 0, false));

                    esnList.Add(objESN);
                }
            }
            return esnList;

        }
        private  List<EsnUpload> PopulateMslESN(DataTable dataTable)
        {
            List<EsnUpload> esnList = default;// new List<EsnUpload>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                esnList = new List<EsnUpload>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    EsnUpload objESN = new EsnUpload();
                    objESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    objESN.MslNumber = clsGeneral.getColumnData(dataRow, "MSL", string.Empty, false) as string;
                    objESN.MEID = clsGeneral.getColumnData(dataRow, "MEID", string.Empty, false) as string;
                    //objESN.AKEY = clsGeneral.getColumnData(dataRow, "AKEY", string.Empty, false) as string;
                    //objESN.OTKSL = clsGeneral.getColumnData(dataRow, "OTKSL", string.Empty, false) as string;
                    //objESN.AVPO = clsGeneral.getColumnData(dataRow, "AVPO", string.Empty, false) as string;
                    //objESN.PRO = clsGeneral.getColumnData(dataRow, "PRO", string.Empty, false) as string;
                    //objESN.PALLETID = clsGeneral.getColumnData(dataRow, "PALLET", string.Empty, false) as string;
                    //objESN.CARTONID = clsGeneral.getColumnData(dataRow, "carton", string.Empty, false) as string;
                    //objESN.HEX = clsGeneral.getColumnData(dataRow, "HEX", string.Empty, false) as string;
                    objESN.ICC_ID = clsGeneral.getColumnData(dataRow, "icc_id", string.Empty, false) as string;
                    // objESN.LTE_IMSI = clsGeneral.getColumnData(dataRow, "lte_imsi", string.Empty, false) as string;

                    //objESN.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));

                    esnList.Add(objESN);
                }
            }
            return esnList;

        }
        private  List<CompanySKUno> PopulateSKU(DataTable dataTable)
        {
            List<CompanySKUno> skuList = default;//new List<CompanySKUno>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                skuList = new List<CompanySKUno>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    CompanySKUno objInventoryItem = new CompanySKUno();
                    objInventoryItem.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    //objInventoryItem.MASSKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    objInventoryItem.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    objInventoryItem.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_Hand", 0, false));

                    skuList.Add(objInventoryItem);
                }
            }
            return skuList;

        }
        private  List<CompanySKUno> PopulateSKUs(DataTable dataTable)
        {
            List<CompanySKUno> skuList = default;//new List<CompanySKUno>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                skuList = new List<CompanySKUno>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    CompanySKUno objInventoryItem = new CompanySKUno();
                    objInventoryItem.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    objInventoryItem.ProductName = clsGeneral.getColumnData(dataRow, "ITEMNAME", string.Empty, false) as string;
                    objInventoryItem.UPC = clsGeneral.getColumnData(dataRow, "upc", string.Empty, false) as string;
                    objInventoryItem.ModelNumber = clsGeneral.getColumnData(dataRow, "ModelNumber", string.Empty, false) as string;
                    //objInventoryItem.MASSKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    objInventoryItem.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    objInventoryItem.CategoryID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryID", 0, false));
                    objInventoryItem.ParentCategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ParentCategoryGUID", 0, false));
                    objInventoryItem.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_Hand", 0, false));

                    objInventoryItem.AllowESN = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowESN", false, false));
                    skuList.Add(objInventoryItem);
                }
            }
            return skuList;

        }
        private  List<EsnMslDetail> PopulateMSL(DataTable dt)
        {
            List<EsnMslDetail> esnList = default;//new List<EsnMslDetail>();
            if (dt != null && dt.Rows.Count > 0)
            {
                esnList = new List<EsnMslDetail>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    EsnMslDetail esnMslDetail = new EsnMslDetail();
                    esnMslDetail.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    esnMslDetail.FulfillmentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", 0, false));
                    esnMslDetail.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "po_num", string.Empty, false) as string;
                    esnMslDetail.MslNumber = clsGeneral.getColumnData(dataRow, "MslNumber", string.Empty, false) as string;
                    esnMslDetail.CustomerName = clsGeneral.getColumnData(dataRow, "CustomerName", string.Empty, false) as string;
                    //  esnMslDetail.AKEY = clsGeneral.getColumnData(dataRow, "akey", string.Empty, false) as string;
                    //   esnMslDetail.OTKSL = clsGeneral.getColumnData(dataRow, "otksl", string.Empty, false) as string;
                    //   esnMslDetail.LTE_IMSI = clsGeneral.getColumnData(dataRow, "LTEIMSI", string.Empty, false) as string;
                    esnMslDetail.ICC_ID = clsGeneral.getColumnData(dataRow, "LTEICCID", string.Empty, false) as string;
                    esnList.Add(esnMslDetail);
                }
            }
            return esnList;

        }
        private DataTable ESNDataNEW(List<EsnUploadNew> mslEsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ESN", typeof(System.String));
            dt.Columns.Add("BatchNumber", typeof(System.String));
            dt.Columns.Add("ICC_ID", typeof(System.String));
            dt.Columns.Add("MeidHex", typeof(System.String));
            dt.Columns.Add("MeidDec", typeof(System.String));
            dt.Columns.Add("Location", typeof(System.String));
            dt.Columns.Add("MSL", typeof(System.String));
            dt.Columns.Add("OTKSL", typeof(System.String));
            dt.Columns.Add("SerialNumber", typeof(System.String));
            dt.Columns.Add("BoxID", typeof(System.String));
            dt.Columns.Add("IMEI2", typeof(System.String));
            DataRow row;

            if (mslEsnList != null && mslEsnList.Count > 0)
            {
                foreach (EsnUploadNew item in mslEsnList)
                {
                    row = dt.NewRow();
                    row["ESN"] = item.ESN;
                    row["BatchNumber"] = item.MslNumber;
                    row["ICC_ID"] = item.ICC_ID;
                    row["MeidHex"] = item.MeidHex;
                    row["MeidDec"] = item.MeidDec;
                    row["Location"] = item.Location;
                    row["MSL"] = item.MSL;
                    row["OTKSL"] = item.OTKSL;
                    row["SerialNumber"] = item.SerialNumber;
                    row["BoxID"] = item.BoxID;
                    row["IMEI2"] = item.IMEI2;

                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        private DataTable ESNData(List<EsnUploadNew> mslEsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ESN", typeof(System.String));
            dt.Columns.Add("BatchNumber", typeof(System.String));
            dt.Columns.Add("ICC_ID", typeof(System.String));
            dt.Columns.Add("MeidHex", typeof(System.String));
            dt.Columns.Add("MeidDec", typeof(System.String));
            dt.Columns.Add("Location", typeof(System.String));
            dt.Columns.Add("MSL", typeof(System.String));
            dt.Columns.Add("OTKSL", typeof(System.String));
            dt.Columns.Add("SerialNumber", typeof(System.String));
            dt.Columns.Add("BoxID", typeof(System.String));
            DataRow row;

            if (mslEsnList != null && mslEsnList.Count > 0)
            {
                foreach (EsnUploadNew item in mslEsnList)
                {
                    row = dt.NewRow();
                    row["ESN"] = item.ESN;
                    row["BatchNumber"] = item.MslNumber;
                    row["ICC_ID"] = item.ICC_ID;
                    row["MeidHex"] = item.MeidHex;
                    row["MeidDec"] = item.MeidDec;
                    row["Location"] = item.Location;
                    row["MSL"] = item.MSL;
                    row["OTKSL"] = item.OTKSL;
                    row["SerialNumber"] = item.SerialNumber;
                    row["BoxID"] = item.BoxID;

                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        private DataTable ESNDataTable(List<EsnUploadNew> esnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ROWID", typeof(System.Int32));
            dt.Columns.Add("ESN", typeof(System.String));

            int ROWID = 1;

            DataRow row;

            if (esnList != null && esnList.Count > 0)
            {
                foreach (EsnUploadNew item in esnList)
                {
                    row = dt.NewRow();

                    row["ROWID"] = ROWID;
                    row["ESN"] = item.ESN;



                    dt.Rows.Add(row);
                    ROWID = ROWID + 1;
                }
            }
            return dt;
        }


    }
}
