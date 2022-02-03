using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace avii.Classes
{
    public class FulfillmentOperations
    {

        public static List<RMAComments> GetRMAComments(int rmaGUID, string commentType)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<RMAComments> commentsList = null;
            try
            {
                objCompHash.Add("@RMAGUID", rmaGUID);
                objCompHash.Add("@CommentType", commentType);

                arrSpFieldSeq = new string[] { "@RMAGUID", "@CommentType" };
                dt = db.GetTableRecords(objCompHash, "av_RMA_Comments_select", arrSpFieldSeq);
                commentsList = PopulateRMAComments(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return commentsList;
        }

        public static List<FulfillmentComment> GetFulfillmentComments(int poID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<FulfillmentComment> commentsList = null;
            try
            {
                objCompHash.Add("@POID", poID);

                arrSpFieldSeq = new string[] { "@POID" };
                dt = db.GetTableRecords(objCompHash, "av_Fulfillment_Comments_select", arrSpFieldSeq);
                commentsList = PopulateFulfillmentComments(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return commentsList;
        }
        

        public static void FulfillmentAVSONumberUpdate(List<FulfillmentNumber> poList, int companyID, string avsoNumber, string poSource, int userID, string fileName, string comment, out int returnValue, out string returnMessage)
        {
            returnValue = 0;
            returnMessage = string.Empty;
            //List<FulfillmentNumber> poList = new List<FulfillmentNumber>();

            string poXML = clsGeneral.SerializeObject(poList);

            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piXMLData", poXML);
                objCompHash.Add("@piCompanyID", companyID);
                objCompHash.Add("@piUserID", userID);
                objCompHash.Add("@piAVSONumber", avsoNumber);
                objCompHash.Add("@piPoSource", poSource);
                objCompHash.Add("@piFileName", fileName);
                objCompHash.Add("@piComment", comment);


                arrSpFieldSeq = new string[] { "@piXMLData", "@piCompanyID", "@piUserID", "@piAVSONumber", "@piPoSource", "@piFileName", "@piComment" };
                dt = db.GetTableRecords(objCompHash, "Av_PurchaseOrder_AVSO_Update", arrSpFieldSeq, "@poRecordCount", "@poErrorMessage", out returnValue, out returnMessage);
                //poList = PopulateTrackingList(dt);
            }

            catch (Exception exp)
            {
                throw exp;
            }

            // return poList;
        }

        public static int ValidateAVSOFulfillmentList(List<FulfillmentNumber> poList, int companyID, string avsoNumber, out string poFulfillmentErrorMessage, out string poErrorMessage)
        {
            int returnValue = 0;
            poFulfillmentErrorMessage = string.Empty;
            poErrorMessage = string.Empty;
            string poXML = clsGeneral.SerializeObject(poList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            //List<FulfillmentNumber> poList = new List<FulfillmentNumber>();
            try
            {
                objCompHash.Add("@piXMLData", poXML);
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@AVSONumber", avsoNumber);

                arrSpFieldSeq = new string[] { "@piXMLData", "@CompanyID", "@AVSONumber" };

                db.GetTableRecords(objCompHash, "Av_PurchaseOrder_AVSO_Validation", arrSpFieldSeq, "@poFulfillmentMessage", "@poErrorMessage", out poFulfillmentErrorMessage, out poErrorMessage, out returnValue);


            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return returnValue;

        }
        
        public static List<EsnList> GetTrackingESNList(int poID, string trackingNumber)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<EsnList> esnList = new List<EsnList>();
            try
            {
                objCompHash.Add("@POID", poID);
                objCompHash.Add("@TrackingNumber", trackingNumber);

                arrSpFieldSeq = new string[] { "@POID", "@TrackingNumber" };

                dt = db.GetTableRecords(objCompHash, "Av_Fulfillment_Tracking_ESN_Select", arrSpFieldSeq);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        EsnList esn = new EsnList();
                        esn.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
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
        public static List<POEsn> GetSKUESNList(int podID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<POEsn> esnList = new List<POEsn>();
            try
            {
                objCompHash.Add("@POD_ID", podID);
             
                arrSpFieldSeq = new string[] { "@POD_ID" };

                dt = db.GetTableRecords(objCompHash, "Av_Fulfillment_SKU_ESN_Select", arrSpFieldSeq);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {
                        POEsn esn = new POEsn();
                        esn.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        esn.ICCID = clsGeneral.getColumnData(dataRow, "ICC_ID", string.Empty, false) as string;
                        esn.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                        
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

        public static int FulifillmentSKUsESNDelete(string podids)
        {
            int returnValue = 0;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<RMAComments> commentsList = null;
            try
            {
                objCompHash.Add("@POD_IDs", podids);

                arrSpFieldSeq = new string[] { "@POD_IDs" };
                db.ExeCommand(objCompHash, "Av_Fulfillment_SKUs_ESN_Delete", arrSpFieldSeq);
                returnValue = 1;

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return returnValue;
        }
        // Validate Ship BY Code from DB
        private static bool ValidateShipByCode(FulfillmentRequest serviceRequest)
        {
            bool errorValue = true;
            string errorMessage = string.Empty;
            if (!string.IsNullOrEmpty(serviceRequest.FulfillmentOrder.ShipVia.Trim()))
            {
                errorMessage = PurchaseOrder.ValidateShipBy(serviceRequest.FulfillmentOrder.ShipVia.Trim());
                if (!string.IsNullOrEmpty(errorMessage))
                    errorValue = true;
                else
                    errorValue = false;
            }
            //else
            //    errorMessage = " Ship By is required";

            return errorValue;
        }

        private bool ValidateRequiredFields(Fulfillment fulfillmentOrderInfo, out string returnMessage)
        {
            bool returnValue = true;
            returnMessage = string.Empty;
            Hashtable hshLineNumberDuplicateCheck = new Hashtable();
            if (fulfillmentOrderInfo != null)
            {
                if (string.IsNullOrEmpty(fulfillmentOrderInfo.FulfillmentNumber))
                {
                    returnValue = false;
                    returnMessage = "Fulfillment number is required.";
                    return returnValue;
                }
                else
                if (fulfillmentOrderInfo.FulfillmentItems != null && fulfillmentOrderInfo.FulfillmentItems.Count > 0)
                {
                    foreach (FulfillmentItem item in fulfillmentOrderInfo.FulfillmentItems)
                    {

                        if (hshLineNumberDuplicateCheck.ContainsKey(item.LineNumber))
                        {

                            returnValue = false;
                            returnMessage = string.Format("Duplicate Line number ({0}) is found", item.LineNumber);
                            return returnValue;
                        }
                        else
                        {
                            hshLineNumberDuplicateCheck.Add(item.LineNumber, item.LineNumber);
                        }
                        if (string.IsNullOrEmpty(item.SKU))
                        {
                            returnValue = false;
                            returnMessage = "SKU is required.";
                            return returnValue;
                    
                        }
                        if (item.LineNumber == 0)
                        {
                            returnValue = false;
                            returnMessage = "Line number can not be 0.";
                            return returnValue;

                        }
                        if (item.Quantity == 0)
                        {
                            returnValue = false;
                            returnMessage = "Quantity can not be 0.";
                            return returnValue;

                        }
                    }
                }

            }
            else
            {
                returnValue = false;
                returnMessage = "There in no data to update.";
                return returnValue;
                    
            }
            return returnValue;
        }
        private static bool ValidationItems(List<FulfillmentItem> fulfillmentitems, InventoryList inventoryList, out string skuList)
        {
            bool found = true;
            skuList = string.Empty;
            foreach (Classes.FulfillmentItem inv in fulfillmentitems)
            {
                if (!inventoryList.Exists(inv.SKU))
                {
                    skuList = inv.SKU;
                    found = false;
                    break;
                }
            }

            return found;
        }
        private static bool ValidateDuplicateSKU(List<FulfillmentItem> polist, out string errorMessage)
        {
            errorMessage = string.Empty;
            bool isNotDuplicate = true;
           // List<avii.Classes.PurchaseOrderItem> polist = purchaseOrderRequest.PurchaseOrder.PurchaseOrderItems;
            var duplicateItems = polist.GroupBy(x => x).Where(g => g.Count() > 1).Select(y => y.Key).ToList();

            // var duplicateItems = (from itemcodes in polist where itemcodes.ItemCode.ToUpper().Equals(itemCode.ToUpper()) select itemcodes).ToList();
            if (duplicateItems != null && duplicateItems.Count > 0 && !string.IsNullOrEmpty(duplicateItems[0].SKU))
            {
                errorMessage = "Duplicate SKU are  not allowed!";
                isNotDuplicate = false;
            }
            return isNotDuplicate;
        }
        public FulfillmentResponse FulfillmentOrderUpdate(FulfillmentRequest serviceRequest)
        {
            //string inactiveESNMsg = string.Empty;
            //string invalidESNMsg = string.Empty;
            //string esnAlreadyExists = string.Empty;
            string requestXML = clsGeneral.SerializeObject(serviceRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "UpdatePurchaseOrder";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            int recordCount = 0;
            int returnCount = 0;
            string errorMessage = string.Empty;

            //bool approval = false;

            FulfillmentResponse serviceResponse = new FulfillmentResponse();
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
                        if (ValidateRequiredFields(serviceRequest.FulfillmentOrder, out errorMessage))
                        {
                            InventoryList inventoryList = new InventoryList();

                            InventoryItemRequest inventoryItemRequest = new InventoryItemRequest();
                            inventoryItemRequest.Authentication.UserName = serviceRequest.UserCredentials.UserName;
                            inventoryItemRequest.Authentication.Password = serviceRequest.UserCredentials.Password;
                            inventoryList = avii.Classes.PurchaseOrder.GetInventoryItems(inventoryItemRequest);


                            if (inventoryList != null)
                            {
                                //errorMessage = ValidateDuplicateSKU(serviceRequest.FulfillmentOrder.FulfillmentItems);
                                if (ValidateDuplicateSKU(serviceRequest.FulfillmentOrder.FulfillmentItems, out errorMessage))
                                {
                                    if (ValidationItems(serviceRequest.FulfillmentOrder.FulfillmentItems, inventoryList, out errorMessage))
                                    {
                                        FulfillmentUpdate(serviceRequest.FulfillmentOrder, credentialValidation.CompanyID, credentialValidation.UserID, out recordCount, out returnCount);
                                        if (returnCount == 0)
                                        {
                                            serviceResponse.Comment = "Fulfillment Order item count is: " + recordCount;
                                            serviceResponse.ReturnCode = ResponseErrorCode.UpdatedSuccessfully.ToString();
                                        }
                                        else
                                        {
                                            if (returnCount == 1)
                                            {
                                                serviceResponse.Comment = serviceRequest.FulfillmentOrder.FulfillmentNumber + " fulfillment number does not exists."; ;
                                                serviceResponse.ReturnCode = ResponseErrorCode.FulfillmentOrderNotExists.ToString();
                                            }
                                            else if (returnCount == 2)
                                            {
                                                serviceResponse.Comment = serviceRequest.FulfillmentOrder.FulfillmentNumber + " fulfillment number already processed."; ;
                                                serviceResponse.ReturnCode = ResponseErrorCode.FulfillmentOrderAlreadyProcessed.ToString();
                                            }
                                            else if (returnCount == 3)
                                            {
                                                serviceResponse.Comment = serviceRequest.FulfillmentOrder.State + " is not a valid state code.";
                                                serviceResponse.ReturnCode = ResponseErrorCode.StateCodeIsNotCorrect.ToString();
                                            }
                                            else if (returnCount == 4)
                                            {
                                                serviceResponse.Comment = serviceRequest.FulfillmentOrder.ShipVia + " is not a valid shipvia code.";
                                                serviceResponse.ReturnCode = ResponseErrorCode.ShipByIsNotCorrect.ToString();
                                            }
                                            else if (returnCount == 5)
                                            {
                                                serviceResponse.Comment = "Duplicate SKU are  not allowed!";
                                                serviceResponse.ReturnCode = ResponseErrorCode.DuplicateItemFound.ToString();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        serviceResponse.Comment = "Fulfillment Order items (" + errorMessage + ") are not valid, please check the item code from catalog";
                                        serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();
                                    }
                                }
                                else
                                {
                                    serviceResponse.Comment = errorMessage;
                                    serviceResponse.ReturnCode = ResponseErrorCode.DuplicateItemFound.ToString();
                                }

                            }
                            else
                            {
                                serviceResponse.Comment = "Inventory is not assinged this user";
                                serviceResponse.ReturnCode = ResponseErrorCode.InconsistantData.ToString();

                            }
                        }
                        else
                        {
                            serviceResponse.Comment = errorMessage;
                            serviceResponse.ReturnCode = ResponseErrorCode.MissingParameter.ToString();
                        }



                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot Authenticate User";
                        serviceResponse.ReturnCode = ResponseErrorCode.CannotAuthenticateUser.ToString();

                    }
                    request.UserID = credentialValidation.UserID;
                    request.CompanyID = credentialValidation.CompanyID;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.ReturnCode;
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
                //LogOperations.ApiLogInsert(request);
            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }
            return serviceResponse;
        }

        public static string FulfillmentAddLineItems(List<FulfillmentItem> fulfillmentItems, int companyID, int POID, int userID, out int recordCount, out int returnCount)
        {
            string errorMessage="";
            recordCount = 0;
            returnCount = 0;
            string itemXML = clsGeneral.SerializeObject(fulfillmentItems);

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@POID", POID);
                objCompHash.Add("@piXMLData", itemXML);

                arrSpFieldSeq = new string[] { "@CompanyID", "@POID",  "@piXMLData" };

                db.ExeCommand(objCompHash, "av_PurchaseOrder_AddLineItems", arrSpFieldSeq, "@poRecordCount", "@poReturnCount", out recordCount, out returnCount);


            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                //throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return errorMessage;
        }

        public static void FulfillmentUpdate(Fulfillment fulfillmentInfo, int companyID, int userID, out int recordCount, out int returnCount)
        {
            recordCount = 0;
            returnCount = 0;
            string itemXML = clsGeneral.SerializeObject(fulfillmentInfo.FulfillmentItems);
            
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@PO_Num", fulfillmentInfo.FulfillmentNumber);
                objCompHash.Add("@Contact_Name", fulfillmentInfo.ContactName);
                objCompHash.Add("@Ship_Via", fulfillmentInfo.ShipVia);
                objCompHash.Add("@ShipTo_Address", fulfillmentInfo.Address1);
                objCompHash.Add("@ShipTo_Address2", fulfillmentInfo.Address2);
                objCompHash.Add("@ShipTo_City", fulfillmentInfo.City);
                objCompHash.Add("@ShipTo_State", fulfillmentInfo.State);
                objCompHash.Add("@ShipTo_Zip", fulfillmentInfo.Zip);
                objCompHash.Add("@Contact_Phone", fulfillmentInfo.ContactPhone);
                objCompHash.Add("@Comments", fulfillmentInfo.Comments);
                objCompHash.Add("@piXMLData", itemXML);

                arrSpFieldSeq = new string[] { "@CompanyID", "@UserID", "@PO_Num", "@Contact_Name", "@Ship_Via", "@ShipTo_Address", "@ShipTo_Address2", "@ShipTo_City", "@ShipTo_State", "@ShipTo_Zip", "@Contact_Phone", "@Comments", "@piXMLData" };

                db.ExeCommand(objCompHash, "av_FulfillmentOrder_API_Update", arrSpFieldSeq, "@poRecordCount", "@poReturnCount", out recordCount, out returnCount);


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
        private static List<FulfillmentComment> PopulateFulfillmentComments(DataTable dataTable)
        {
            List<FulfillmentComment> commentsList = new List<FulfillmentComment>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    FulfillmentComment objComments = new FulfillmentComment();
                    objComments.Comments = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                    objComments.CommentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CommentDate", 0, false));

                    commentsList.Add(objComments);
                }
            }
            return commentsList;

        }

        private static List<RMAComments> PopulateRMAComments(DataTable dataTable)
        {
            List<RMAComments> commentsList = new List<RMAComments>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RMAComments objComments = new RMAComments();
                    objComments.Comments = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                    objComments.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CommentDate", 0, false));

                    commentsList.Add(objComments);
                }
            }
            return commentsList;

        }

        
    }
}