using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;

namespace avii.Classes
{
    public class BabEsnOperation
    {
        public BadESNResponse GetBadESN(BadESNRequest serviceRequest)
        {
            Exception exc = null;
            BadESNResponse serviceResponse = new BadESNResponse();
            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out exc);
                if (credentialValidation != null && credentialValidation.CompanyID > 0)
                {
                    List<EsnList> esnInfoList = null;
                    esnInfoList = avii.Classes.BabEsnOperation.GetBadESNList(credentialValidation.CompanyID);
                    if (esnInfoList != null && esnInfoList.Count > 0)
                    {

                        serviceResponse.Comment = "Successfully Retrieved.  Record Count: " + esnInfoList.Count;
                        serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                        serviceResponse.ESNList = esnInfoList;

                        
                    }
                    else
                    {

                        serviceResponse.Comment = "No Records Found";
                        serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                        serviceResponse.ESNList = null;
                    }

                }
                else
                {
                    serviceResponse.Comment = "Cannot Authenticate User";
                    serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    serviceResponse.ESNList = null;

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.ESNList = null;
            }

            return serviceResponse;
        }
        public ReassignSKUResponse GetReassignSkuList(ReassignSKURequest serviceRequest)
        {

            ReassignSKUResponse serviceResponse = new ReassignSKUResponse();
            Exception exc = null;

            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out exc);
                if (credentialValidation != null && credentialValidation.CompanyID > 0)
                {
                    List<ReassignSKU> reassignSKUList = null;
                    reassignSKUList = avii.Classes.ReportOperations.GetReassignSkuReport(credentialValidation.CompanyID, serviceRequest.FromDate, serviceRequest.ToDate, serviceRequest.ESN, serviceRequest.SKU);
                    if (reassignSKUList != null && reassignSKUList.Count > 0)
                    {

                        serviceResponse.Comment = "Successfully Retrieved.  Record Count: " + reassignSKUList.Count;
                        serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                        serviceResponse.ReassignSKUList = reassignSKUList;


                    }
                    else
                    {

                        serviceResponse.Comment = "No Records Found";
                        serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                        serviceResponse.ReassignSKUList = null;
                    }

                }
                else
                {
                    serviceResponse.Comment = "Cannot Authenticate User";
                    serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    serviceResponse.ReassignSKUList = null;

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                serviceResponse.ReassignSKUList = null;
            }

            return serviceResponse;
        }
        
        public InventoryEsnResponse BadEsnUpdateAPI(InventoryEsnRequest serviceRequest)
        {
            string inactiveESNMsg = string.Empty;
            string invalidESNMsg = string.Empty;
            string esnAlreadyExists = string.Empty;
            int recordCount = 0;
            string errorMessage = string.Empty;
            bool approval = false;
            Exception exc = null;

            InventoryEsnResponse serviceResponse = new InventoryEsnResponse();
            try
            {
                CredentialValidation credentialValidation = AuthenticationOperation.AuthenticateRequest(serviceRequest.UserCredentials, out exc);
                if (credentialValidation != null && credentialValidation.CompanyID > 0)
                {
                    List<EsnInfo> esnInfoList = null;
                    bool isDelete = false;
                    esnInfoList = avii.Classes.BabEsnOperation.ValidateBadESN(serviceRequest.EsnList, credentialValidation.CompanyID, isDelete, out recordCount, out inactiveESNMsg, out invalidESNMsg, out esnAlreadyExists);
                    if (string.IsNullOrEmpty(errorMessage) && string.IsNullOrEmpty(inactiveESNMsg) && string.IsNullOrEmpty(invalidESNMsg) && string.IsNullOrEmpty(esnAlreadyExists))
                    {
                        BabEsnOperation.BadEsnUpload(serviceRequest.EsnList, credentialValidation.CompanyID, serviceRequest.Reason, approval, isDelete, 0, string.Empty, string.Empty, out recordCount);
                        if (recordCount > 0)
                        {
                            serviceResponse.Comment = "Submitted successfully.  Record Count: " + recordCount;
                            serviceResponse.ReturnCode = ResponseErrorCode.UpdatedSuccessfully.ToString();

                        }
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(errorMessage))
                        {
                            if (!string.IsNullOrEmpty(inactiveESNMsg))
                            {
                                errorMessage = inactiveESNMsg + " ESN(s) product are not active";
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(inactiveESNMsg))
                            {
                                errorMessage = errorMessage + " and " + inactiveESNMsg + " ESN(s) product are not active";
                            }
                        }
                        if (string.IsNullOrEmpty(errorMessage))
                        {
                            if (!string.IsNullOrEmpty(invalidESNMsg))
                            {
                                errorMessage = invalidESNMsg + " ESN(s) are not valid";
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(invalidESNMsg))
                            {
                                errorMessage = errorMessage + " and " + invalidESNMsg + " ESN(s) are not valid";
                            }
                        }
                        if (string.IsNullOrEmpty(errorMessage))
                        {
                            if (!string.IsNullOrEmpty(esnAlreadyExists))
                            {
                                errorMessage = esnAlreadyExists + " ESN(s) are already exists";
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(esnAlreadyExists))
                            {
                                errorMessage = errorMessage + " and " + esnAlreadyExists + " ESN(s) are already exists";
                            }
                        }
                        serviceResponse.Comment = errorMessage;
                        serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();

                    }

                }
                else
                {
                    serviceResponse.Comment = "Cannot Authenticate User";
                    serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();

                }
            }
            catch (Exception ex)
            {
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

            }

            return serviceResponse;
        }
        public static List<EsnInfo> ValidateBadESN(List<avii.Classes.EsnList> esnList, int companyID, bool isDelete, out int poRecordCount, out string poErrorMessage, out string poInvalidESNMessage, out string poEsnExistsMessage)
        {
            poRecordCount = 0;
            poErrorMessage = string.Empty;
            poInvalidESNMessage = string.Empty;
            poEsnExistsMessage = string.Empty;

            List<EsnInfo> esnInfoList = new List<EsnInfo>();
            string esnXML = clsGeneral.SerializeObject(esnList);
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piXMLData", esnXML);
                objCompHash.Add("@piCompanyID", companyID);
                objCompHash.Add("@piIsDelete", isDelete);


                arrSpFieldSeq = new string[] { "@piXMLData", "@piCompanyID", "@piIsDelete" };

                dataTable = db.GetTableRecords(objCompHash, "av_BadEsn_Validate", arrSpFieldSeq, "@poRecordCount", "@poErrorMessage", "@poInvalidESNMessage", "@poEsnExistsMessage", out poRecordCount, out poErrorMessage, out poInvalidESNMessage, out poEsnExistsMessage);
                esnInfoList = PopulateBadEsnInfoList(dataTable);

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

            return esnInfoList;
        }

        public static List<EsnList> GetBadESNList(int companyID)
        {
            List<EsnList> esnInfoList = new List<EsnList>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyID", companyID);

                arrSpFieldSeq = new string[] { "@piCompanyID" };

                dataTable = db.GetTableRecords(objCompHash, "Av_BadESN_Select", arrSpFieldSeq);
                esnInfoList = PopulateBadEsnList(dataTable);

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

            return esnInfoList;
            
        }
        public static List<EsnInfo> ValidateBadEsnApproval(List<avii.Classes.EsnList> esnList, int companyID, out int poRecordCount, out string poErrorMessage, out string poInvalidESNMessage)
        {
            poRecordCount = 0;
            poErrorMessage = string.Empty;
            poInvalidESNMessage = string.Empty;

            List<EsnInfo> esnInfoList = new List<EsnInfo>();
            string esnXML = clsGeneral.SerializeObject(esnList);
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piXMLData", esnXML);
                objCompHash.Add("@piCompanyID", companyID);

                arrSpFieldSeq = new string[] { "@piXMLData", "@piCompanyID" };

                dataTable = db.GetTableRecords(objCompHash, "av_BadEsnApproval_Validate", arrSpFieldSeq, "@poRecordCount", "@poErrorMessage", "@poInvalidESNMessage", out poRecordCount, out poErrorMessage, out poInvalidESNMessage);
                esnInfoList = PopulateBadEsnInfoList(dataTable);

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

            return esnInfoList;
        }
        public static void BadEsnApprovalUpdate(List<avii.Classes.EsnList> esnList, int companyID, out int recordCount)
        {
            recordCount = 0;
            //List<EsnInfo> esnInfoList = new List<EsnInfo>();
            string esnXML = clsGeneral.SerializeObject(esnList);
            //DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piXMLData", esnXML);
                objCompHash.Add("@piCompanyID", companyID);
                //objCompHash.Add("@piReason", reason);
                //objCompHash.Add("@piApproval", approval);

                arrSpFieldSeq = new string[] { "@piXMLData", "@piCompanyID" };

                db.ExeCommand(objCompHash, "av_BadEsn_Approval", arrSpFieldSeq, "@poRecordCount", out recordCount);


                //esnInfoList = PopulateEsnInfoList(dataTable);
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

            //return returnValue;
        }

        public static void BadEsnUpload(List<avii.Classes.EsnList> esnList, int companyID, string reason, bool approval, bool isDelete, int userID, string fileName, string comment, out int recordCount)
        {
            recordCount = 0;
            //List<EsnInfo> esnInfoList = new List<EsnInfo>();
            string esnXML = clsGeneral.SerializeObject(esnList);
            //DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piXMLData", esnXML);
                objCompHash.Add("@piCompanyID", companyID);
                objCompHash.Add("@piReason", reason);
                objCompHash.Add("@piApproval", approval);
                objCompHash.Add("@piIsDelete", isDelete);
                objCompHash.Add("@piUserID", userID);
                objCompHash.Add("@FileName", fileName);
                objCompHash.Add("@Comment", comment);

                arrSpFieldSeq = new string[] { "@piXMLData", "@piCompanyID", "@piReason", "@piApproval", "@piIsDelete", "@piUserID", "@FileName", "@Comment" };

                db.ExeCommand(objCompHash, "av_BadEsn_Insert", arrSpFieldSeq, "@poRecordCount", out recordCount);


                //esnInfoList = PopulateEsnInfoList(dataTable);
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

            //return returnValue;
        }
        
        private static List<EsnInfo> PopulateBadEsnInfoList(DataTable dataTable)
        {
            List<EsnInfo> esnInfoList = new List<EsnInfo>();
            EsnInfo esnInfo = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    esnInfo = new EsnInfo();
                    esnInfo.Item_Code = (string)clsGeneral.getColumnData(dataRow, "Item_Code", string.Empty, false);
                    esnInfo.ESN = (string)clsGeneral.getColumnData(dataRow, "esn", string.Empty, false);
                    esnInfo.SKU = (string)clsGeneral.getColumnData(dataRow, "sku", string.Empty, false);
                    esnInfo.MslNumber = (string)clsGeneral.getColumnData(dataRow, "MSLNumber", string.Empty, false);
                    // esnInfo.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                    // esnInfo.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PurchaseOrderNumber", string.Empty, false);
                    //esnInfo.AKEY = (string)clsGeneral.getColumnData(dataRow, "Akey", string.Empty, false);
                    //esnInfo.OTKSL = (string)clsGeneral.getColumnData(dataRow, "Otksl", string.Empty, false);
                    //esnInfo.MEID = (string)clsGeneral.getColumnData(dataRow, "Meid", string.Empty, false);
                    //esnInfo.AVPO = (string)clsGeneral.getColumnData(dataRow, "AVPO", string.Empty, false);
                    esnInfo.ICC_ID = (string)clsGeneral.getColumnData(dataRow, "icc_id", string.Empty, false);
                    //esnInfo.LTE_IMSI = (string)clsGeneral.getColumnData(dataRow, "lte_imsi", string.Empty, false);

                    //esnInfo.HEX = (string)clsGeneral.getColumnData(dataRow, "Hex", string.Empty, false);
                    //esnInfo.RmaNumber = (string)clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false);
                    //esnInfo.LTEIMSI = (string)clsGeneral.getColumnData(dataRow, "lte_imsi", string.Empty, false);

                    //esnInfo.PO_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true));
                    //esnInfo.RmaGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGuid", 0, true));
                    esnInfo.IsESN = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESN", false, true));

                    esnInfoList.Add(esnInfo);

                }
            }

            return esnInfoList;

        }

        private static List<EsnList> PopulateBadEsnList(DataTable dataTable)
        {
            List<EsnList> esnInfoList = new List<EsnList>();
            EsnList esnInfo = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    esnInfo = new EsnList();
                    esnInfo.ESN = (string)clsGeneral.getColumnData(dataRow, "esn", string.Empty, false);
                    
                    esnInfoList.Add(esnInfo);

                }
            }

            return esnInfoList;

        }

        
    }
}