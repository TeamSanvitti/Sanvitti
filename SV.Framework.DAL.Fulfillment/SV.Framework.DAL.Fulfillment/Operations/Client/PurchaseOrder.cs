using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;
//using System.IO;
using System.Linq;
//using System.Configuration;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;
using SV.Framework.Models.Service;

namespace SV.Framework.DAL.Fulfillment
{   
    public class PurchaseOrder : BaseCreateInstance
    {
        public  List<ShipBy> GetShipByList()
        {
            List<ShipBy> shipBylist = default;//;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@ShipByID", 0);
                    objCompHash.Add("@ShipByCode", string.Empty);
                    // objCompHash.Add("@Nationality", string.Empty);
                    arrSpFieldSeq = new string[] { "@ShipByID", "@ShipByCode" };
                    dt = db.GetTableRecords(objCompHash, "av_ShipBy_Select", arrSpFieldSeq);
                    shipBylist = PopulateShipBy(dt);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                   // db = null;
                }
            }
            return shipBylist;
        }

        public string ValidateShipByText(string shipByText)
        {
            string returnMsg = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@ShipByID", 0);
                    objCompHash.Add("@ShipByCode", "");
                    objCompHash.Add("@ShipByText", shipByText);

                    arrSpFieldSeq = new string[] { "@ShipByID", "@ShipByCode", "@ShipByText" };
                    dt = db.GetTableRecords(objCompHash, "av_ShipBy_Select", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        returnMsg = dt.Rows[0]["ShipByText"].ToString();
                    }
                }
                catch (Exception objExp)
                {
                    //returnMsg = objExp.Message;
                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message);
                }
                finally
                {
                    db.DBClose();
                    //db = null;
                }
            }
            return returnMsg;
        }

        public PurchaseOrders GerPurchaseOrdersNew(string PONumber, string ContactName, string POFromDate, string POToDate, int UserID,
                                                       string statusID, int CompanyID, string esn, string AvOrder, string MslNumber, string PhoneCategory,
                                                       string ItemCode, string StoreID, string fmUPC, string zoneGUID, string shipFrom, string shipTo,
                                                       int PO_ID, string trackingNumber, string customerOrderNumber, string POType, int StockInDemand)
        {
            PurchaseOrders po = default;//new PurchaseOrders();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;//new DataSet();

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@Po_Num", PONumber);
                    objCompHash.Add("@Contact_Name", ContactName);
                    objCompHash.Add("@From_Date", string.IsNullOrWhiteSpace(POFromDate) ? null : POFromDate);
                    objCompHash.Add("@To_Date", string.IsNullOrWhiteSpace(POToDate) ? null : POToDate);
                    objCompHash.Add("@StatusID", statusID);
                    objCompHash.Add("@UserID", UserID);
                    objCompHash.Add("@CompanyID", CompanyID);
                    objCompHash.Add("@esn", esn);
                    objCompHash.Add("@AvOrderNumber", AvOrder);
                    objCompHash.Add("@MslNumber", MslNumber);
                    objCompHash.Add("@PhoneCategory", PhoneCategory);
                    objCompHash.Add("@ItemCode", ItemCode);
                    objCompHash.Add("@StoreID", StoreID);
                    objCompHash.Add("@fmUPC", fmUPC);
                    objCompHash.Add("@ShipFrom", string.IsNullOrWhiteSpace(shipFrom) ? null : shipFrom); //shipFrom);
                    objCompHash.Add("@ShipTo", string.IsNullOrWhiteSpace(shipTo) ? null : shipTo); //shipTo);

                    objCompHash.Add("@POID", PO_ID);
                    objCompHash.Add("@TrackingNumber", trackingNumber);
                    objCompHash.Add("@CustomerOrderNumber", customerOrderNumber);
                    objCompHash.Add("@POType", POType);
                    objCompHash.Add("@StockInDemand", StockInDemand);

                    if (!string.IsNullOrEmpty(zoneGUID))
                    {
                        objCompHash.Add("@ZoneGUID", zoneGUID);
                    }

                    arrSpFieldSeq = new string[] { "@Po_Num", "@Contact_Name", "@From_Date", "@To_Date", "@StatusID", "@UserID", "@CompanyID", "@esn",
                    "@AvOrderNumber", "@MslNumber", "@PhoneCategory", "@ItemCode", "@StoreID", "@fmUPC", "@ZoneGUID", "@ShipFrom", "@ShipTo",
                    "@TrackingNumber","@CustomerOrderNumber","@POType", "@POID","@StockInDemand" };
                    ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_Select_New", arrSpFieldSeq);

                    po =  PopulatePurchaseOrdersNew(ds);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                   // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return po;
        }





        public  PurchaseOrders GerPurchaseOrders(string PONumber, string ContactName, string POFromDate, string POToDate, int UserID,
                                                       string statusID, int CompanyID, string esn, string AvOrder, string MslNumber,
                                                       string PhoneCategory, string ItemCode, string StoreID, string fmUPC, string zoneGUID,
                                                       string shipFrom, string shipTo, int PO_ID)
        {
            PurchaseOrders po = default;//new PurchaseOrders();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@Po_Num", PONumber);
                    objCompHash.Add("@Contact_Name", ContactName);
                    objCompHash.Add("@From_Date", POFromDate);
                    objCompHash.Add("@To_Date", POToDate);
                    objCompHash.Add("@StatusID", statusID);
                    objCompHash.Add("@UserID", UserID);
                    objCompHash.Add("@CompanyID", CompanyID);
                    objCompHash.Add("@esn", esn);
                    objCompHash.Add("@AvOrderNumber", AvOrder);
                    objCompHash.Add("@MslNumber", MslNumber);
                    objCompHash.Add("@PhoneCategory", PhoneCategory);
                    objCompHash.Add("@ItemCode", ItemCode);
                    objCompHash.Add("@StoreID", StoreID);
                    objCompHash.Add("@fmUPC", fmUPC);
                    objCompHash.Add("@ShipFrom", shipFrom);
                    objCompHash.Add("@ShipTo", shipTo);
                    objCompHash.Add("@POID", PO_ID);
                    if (!string.IsNullOrEmpty(zoneGUID))
                    {
                        objCompHash.Add("@ZoneGUID", zoneGUID);
                    }

                    arrSpFieldSeq = new string[] { "@Po_Num", "@Contact_Name", "@From_Date", "@To_Date", "@StatusID", "@UserID", "@CompanyID", "@esn", "@AvOrderNumber", "@MslNumber", "@PhoneCategory", "@ItemCode", "@StoreID", "@fmUPC", "@ZoneGUID", "@ShipFrom", "@ShipTo", "@POID" };
                    ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_Select", arrSpFieldSeq);

                    po=  PopulatePurchaseOrders(ds);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                  //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return po;
        }

        public  PurchaseOrders GerSelectedPurchaseOrders(string POIds, int downloadFlag)
        {
            PurchaseOrders po = default;//new PurchaseOrders(); 
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@Po_Ids", POIds);
                    objCompHash.Add("@DownloadFlag", downloadFlag);

                    arrSpFieldSeq = new string[] { "@Po_Ids", "@DownloadFlag" };
                    ds = db.GetDataSet(objCompHash, "AV_PurchaseOrder_Download", arrSpFieldSeq);

                     po = PopulatePurchaseOrdersDownload(ds);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                  //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return po;
        }
        public  void FulfillmentUpdate(SV.Framework.Models.Fulfillment.Fulfillment  fulfillmentInfo, int companyID, int userID, out int recordCount, out int returnCount)
        {
            recordCount = 0;
            returnCount = 0;
            using (DBConnect db = new DBConnect())
            {
                string itemXML = clsGeneral.SerializeObject(fulfillmentInfo.FulfillmentItems);

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
                    Logger.LogMessage(ex, this);// throw ex;
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            //return returnValue;
        }

        public CancelPurchaseOrderResponse CancelFulfillment(string purchaseOrderNumber, int userID)
        {
            CancelPurchaseOrderResponse fulfillmentResponse = new CancelPurchaseOrderResponse();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //DataTable dataTable = new DataTable();
                Hashtable objCompHash = new Hashtable();
                string outParam = string.Empty;
                int statusID = 0;
                try
                {
                    objCompHash.Add("@PO_Num", purchaseOrderNumber);
                    objCompHash.Add("@UserID", userID);
                    arrSpFieldSeq = new string[] { "@PO_Num", "@UserID" };

                    statusID = db.ExCommand(objCompHash, "Av_PurchaseOrder_Cancel", arrSpFieldSeq, "@outparam", out outParam);

                    if (outParam == purchaseOrderNumber)
                    {
                        fulfillmentResponse.ErrorCode = string.Empty;
                        fulfillmentResponse.Comment = ResponseErrorCode.CancelledSuccessfully.ToString();
                    }
                    else if (outParam == string.Empty && statusID == 0)
                    {
                        fulfillmentResponse.ErrorCode = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                        fulfillmentResponse.Comment = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                    }
                    else
                    {
                        fulfillmentResponse.ErrorCode = ResponseErrorCode.PurchaseOrderCannotBeCancelled.ToString();
                        fulfillmentResponse.Comment = "PurchaseOrder should be in pending or processed state for cancellation";
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //serviceResponse.Comment = ex.Message;
                    fulfillmentResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
                    fulfillmentResponse.Comment = objExp.Message.ToString();
                    //throw objExp;
                }
                finally
                {
                    db.DBClose();
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return fulfillmentResponse;
        }

        public PurchaseOrderInfoResponse GerPurchaseOrderAPI(PurchaseOrderInfoRequest purchaseOrderRequest)
        {
            PurchaseOrderInfoResponse serviceResponse = new PurchaseOrderInfoResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.None.ToString();           

            if (purchaseOrderRequest.UserId > 0)
            {
                if (!string.IsNullOrEmpty(purchaseOrderRequest.PurchaseOrderNumber) || !string.IsNullOrEmpty(purchaseOrderRequest.ESN))
                {
                    using (DBConnect db = new DBConnect())
                    {
                        string[] arrSpFieldSeq;
                        DataSet ds = default;// new DataSet();

                        Hashtable objCompHash = new Hashtable();
                        try
                        {
                            objCompHash.Add("@Po_Num", purchaseOrderRequest.PurchaseOrderNumber);
                            objCompHash.Add("@UserID", purchaseOrderRequest.UserId);
                            objCompHash.Add("@esn", purchaseOrderRequest.ESN ?? null);

                            arrSpFieldSeq = new string[] { "@Po_Num", "@UserID", "@esn" };
                            ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_API_Select", arrSpFieldSeq);

                            clsPurchaseOrder po = PopulatePurchaseOrder(ds);
                            if (po != null)
                            {
                                serviceResponse.PurchaseOrder = po;

                                serviceResponse.Comment = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                            }
                            else
                            {
                                serviceResponse.ErrorCode = ResponseErrorCode.PurchaseOrderNotExists.ToString();
                            }


                        }
                        catch (Exception ex)
                        {
                            Logger.LogMessage(ex, this);
                            serviceResponse.Comment = ex.Message;
                            serviceResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                            serviceResponse.PurchaseOrder = null;
                            
                        }
                        finally
                        {
                            db.DBClose();
                            objCompHash = null;
                            arrSpFieldSeq = null;
                        }
                    }
                }
                else
                {
                    serviceResponse.Comment = "Search criteria required";
                    serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
                }
            }
            else
            {
                serviceResponse.Comment = "Cannot authenticate user";
                serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
            }
            return serviceResponse;
        }

        public List<BasePurchaseOrder> GetPurchaseOrderHistory(int poID)
        {
            List<BasePurchaseOrder> poStatusHistory = default;//new List<BasePurchaseOrder>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@PO_ID", poID);
                    arrSpFieldSeq = new string[] { "@PO_ID" };
                    dt = db.GetTableRecords(objCompHash, "AV_PurchaseOrder_History", arrSpFieldSeq);

                    BasePurchaseOrder purchaseOrder = null;
                    foreach (DataRow dataRow in dt.Rows)
                    {

                        purchaseOrder = new BasePurchaseOrder(Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true)),
                            string.Empty);
                        //purchaseOrder.StatusColor = (string)clsGeneral.getColumnData(dataRow, "StatusColor", string.Empty, false);

                        purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, true));
                        purchaseOrder.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                        purchaseOrder.SentASN = (string)clsGeneral.getColumnData(dataRow, "PoDataXML", string.Empty, false);
                        purchaseOrder.Comments = (string)clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false);

                        purchaseOrder.PurchaseOrderStatus = (PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "POStatus", 1, false));
                        //purchaseOrder.PurchaseOrderStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "POStatus", 0, false));
                        purchaseOrder.ModifiedDate = (string)clsGeneral.getColumnData(dataRow, "ModifiedDate", string.Empty, false);
                        purchaseOrder.CustomerName = (string)clsGeneral.getColumnData(dataRow, "Username", string.Empty, false);
                        purchaseOrder.SentESN = (string)clsGeneral.getColumnData(dataRow, "POFlag", string.Empty, false);
                        poStatusHistory.Add(purchaseOrder);
                    }
                    //return PopulatePurchaseOrders(ds);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return poStatusHistory;
        }

        private DataTable ItemsTable(List<BasePurchaseOrderItem> items)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("PODID", typeof(System.Int32));
            dt.Columns.Add("ItemsPerContainer", typeof(System.Int32));
            dt.Columns.Add("ContainerPerPallet", typeof(System.Int32));

            DataRow row;
            int SNo = 1;
            if (items != null && items.Count > 0)
            {
                foreach (BasePurchaseOrderItem item in items)
                {
                    row = dt.NewRow();
                    row["PODID"] = item.PodID;
                    row["ItemsPerContainer"] = item.ItemsPerContainer;
                    row["ContainerPerPallet"] = item.ContainersPerPallet;
                    dt.Rows.Add(row);
                    SNo = SNo + 1;
                }
            }
            return dt;
        }

        public string UpdatePurchaseOrder(BasePurchaseOrder purchaseOrder, int userID)
        {
            string response = "";

            using (DBConnect db = new DBConnect())
            {
                FulfillmentLogModel logModel = new FulfillmentLogModel();
                
                logModel.ActionName = "Fulfillment Update";
                logModel.CreateUserID = userID;
                logModel.StatusID = purchaseOrder.PurchaseOrderStatusID;
                logModel.PO_ID = Convert.ToInt32(purchaseOrder.PurchaseOrderID);
                logModel.FulfillmentNumber = string.Empty;
                logModel.Comment = string.Empty;
                string poXML = BaseAerovoice.SerializeObject<BasePurchaseOrder>(purchaseOrder);
                poXML = "<BasePurchaseOrder>" + poXML.Substring(poXML.IndexOf("<RequestedShipDate>"));
                DataTable itemsTable = ItemsTable(purchaseOrder.PurchaseOrderItems);
                logModel.RequestData = poXML;
                // DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@PO_ID", purchaseOrder.PurchaseOrderID);
                    objCompHash.Add("@PO_Num", purchaseOrder.PurchaseOrderNumber);
                    objCompHash.Add("@Contact_Name", purchaseOrder.Shipping.ContactName);
                    objCompHash.Add("@ShipTo_Attn", purchaseOrder.Shipping.ShipToAttn);
                    objCompHash.Add("@Ship_Via", purchaseOrder.Tracking.ShipToBy);
                    objCompHash.Add("@Tracking", purchaseOrder.Tracking.ShipToTrackingNumber);
                    //objCompHash.Add("@ShipDate", shipTrack.ShipToDate);
                    objCompHash.Add("@StatusID", purchaseOrder.PurchaseOrderStatusID);
                    objCompHash.Add("@ShipTo_Address", purchaseOrder.Shipping.ShipToAddress);
                    objCompHash.Add("@ShipTo_Address2", purchaseOrder.Shipping.ShipToAddress2);
                    objCompHash.Add("@ShipTo_City", purchaseOrder.Shipping.ShipToCity);
                    objCompHash.Add("@ShipTo_State", purchaseOrder.Shipping.ShipToState);
                    objCompHash.Add("@ShipTo_Zip", purchaseOrder.Shipping.ShipToZip);
                    objCompHash.Add("@Contact_Phone", purchaseOrder.Shipping.ContactPhone);
                    objCompHash.Add("@UserID", userID);
                    objCompHash.Add("@Comments", purchaseOrder.Comments);

                    objCompHash.Add("@PoDate", purchaseOrder.PurchaseOrderDate);
                    objCompHash.Add("@IsShipmentRequired", purchaseOrder.IsShipmentRequired);
                    objCompHash.Add("@RequestedShipDate", purchaseOrder.RequestedShipDate);
                    objCompHash.Add("@CustomerOrderNumber", purchaseOrder.CustomerAccountNumber);
                    objCompHash.Add("@LineItems", itemsTable);


                    arrSpFieldSeq = new string[] { "@PO_ID", "@PO_Num", "@Contact_Name", "@ShipTo_Attn", "@Ship_Via",
                "@Tracking", "@StatusID", "@ShipTo_Address","@ShipTo_Address2", "@ShipTo_City", "@ShipTo_State", "@ShipTo_Zip",
                "@Contact_Phone","@UserID", "@Comments", "@PoDate","@IsShipmentRequired","@RequestedShipDate","@CustomerOrderNumber","@LineItems" };
                 object objResult =   db.ExecuteScalar(objCompHash, "Aero_PurchaseOrder_Update", arrSpFieldSeq);
                    if (objResult != null)
                    {
                        if(Convert.ToInt32(objResult)==999)
                        {
                            response = "Customer order number already exists!";
                        }
                        else if (Convert.ToInt32(objResult) == 1)
                        {
                            response = "Updated successfully";

                        }
                    }
                    else
                        response = "Updated successfully";


                }
                catch (Exception objExp)
                {
                    logModel.Comment = objExp.Message;
                    response = objExp.Message;
                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                    //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;

                    logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                    LogOperations logOperations = new LogOperations();
                    logOperations.FulfillmentLogInsert(logModel);
                    //LogOperations.FulfillmentLogInsert(logModel);

                }
            }
            return response;
        }


        public  void DeletePurchaseOrder(int poID, int userID)
        {
            using (DBConnect db = new DBConnect())
            {
                FulfillmentLogModel logModel = new FulfillmentLogModel();
                string response = "";

                logModel.ActionName = "Fulfillment Delete";
                logModel.CreateUserID = userID;
                logModel.StatusID = 0;
                logModel.PO_ID = poID;
                logModel.FulfillmentNumber = string.Empty;
                logModel.Comment = string.Empty;
                string poXML = "POID: " + poID;

                logModel.RequestData = poXML;

                //DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@Po_ID", poID);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@Po_ID", "@UserID" };
                    db.ExeCommand(objCompHash, "Aero_PurchaseOrder_Delete", arrSpFieldSeq);

                    response = "Deleted successfuly.";
                }
                catch (Exception objExp)
                {
                    logModel.Comment = objExp.Message;
                    response = objExp.Message;
                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message.ToString());
                }
                finally
                {

                  //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                    logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                    LogOperations logOperations = new LogOperations();
                    logOperations.FulfillmentLogInsert(logModel);
                    //LogOperations.FulfillmentLogInsert(logModel);

                }
            }
        }

        public void PurchaseOrderUpdateDetail(int podID, string esn, string msl, string msid, string mdn, string passCode, string fmupc, int statusID, int userID, string lteICCiD, string lteIMSI, string akey, string otksl, string simNumber, out string returnMessage)
        {
            returnMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POD_ID", podID);
                    objCompHash.Add("@ESN", esn);
                    objCompHash.Add("@msl", msl);
                    objCompHash.Add("@MSID", msid);
                    objCompHash.Add("@mdn", mdn);
                    objCompHash.Add("@Pass_Code", passCode);
                    objCompHash.Add("@fmupc", fmupc);
                    objCompHash.Add("@StatusID", statusID);
                    objCompHash.Add("@LTEICCID", lteICCiD);
                    objCompHash.Add("@LTEIMSI", lteIMSI);
                    objCompHash.Add("@Akey", akey);
                    objCompHash.Add("@Otksl", otksl);
                    objCompHash.Add("@SimNumber", simNumber);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@POD_ID", "@ESN", "@msl", "@MSID", "@mdn", "@Pass_Code", "@fmupc", "@StatusID",
                    "@LTEICCID", "@LTEIMSI", "@Akey", "@Otksl","@SimNumber", "@UserID" };
                    db.ExCommand(objCompHash, "Aero_PurchaseOrderDetail_Update", arrSpFieldSeq, "@ReturnMessage", out returnMessage);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }

        public  int DeletePurchaseOrderDetail(int podID, int userID)
        {
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POD_ID", podID);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@POD_ID", "@UserID" };
                    db.ExeCommand(objCompHash, "Aero_PurchaseOrderDetail_Delete", arrSpFieldSeq, "@ItemCount", out returnValue);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                  //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnValue;
        }

        public  List<BasePurchaseOrderItem> GetPurchaseOrderItemList(int PO_ID)
        {
            List<BasePurchaseOrderItem> poLIst = default;// new List<BasePurchaseOrderItem>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", PO_ID);

                    arrSpFieldSeq = new string[] { "@POID" };
                    dt = db.GetTableRecords(objCompHash, "Av_PurchaseOrder_Assign_Select", arrSpFieldSeq);

                    poLIst = PopulatePurchaseOrderItemList(dt);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return poLIst;
        }
        public  int ValidateFulfillmentOrder(List<FulfillmentNumber> poList, List<FulfillmentSKU> skuList, string companyAccountNumber, out int poRecordCount, out string poErrorMessage, out string poStoreIDErrorMessage, out string poShipViaErrorMessage, out string poSKUsErrorMessage, out string poStateErrorMessage, out string errorMessage)
        {
            string poXML = clsGeneral.SerializeObject(poList);

            string storeXML = string.Empty;//clsGeneral.SerializeObject(storeList);

            string shipviaXML = string.Empty;//clsGeneral.SerializeObject(shipViaList);
            string skuXML = clsGeneral.SerializeObject(skuList);
            string stateXML = string.Empty;//clsGeneral.SerializeObject(stateList);

            int returnValue = 0;
            poRecordCount = 0;
            poErrorMessage = string.Empty;
            poStoreIDErrorMessage = string.Empty;
            poShipViaErrorMessage = string.Empty;
            poSKUsErrorMessage = string.Empty;
            errorMessage = string.Empty;
            poStateErrorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@piCompanyAccountNumber", companyAccountNumber);
                    objCompHash.Add("@piXMLData", poXML);
                    objCompHash.Add("@piStoreXMLData", storeXML);
                    objCompHash.Add("@piSKUsXMLData", skuXML);
                    objCompHash.Add("@piShipViaXMLData", shipviaXML);
                    objCompHash.Add("@piStateXMLData", stateXML);
                    arrSpFieldSeq = new string[] { "@piCompanyAccountNumber", "@piXMLData", "@piStoreXMLData", "@piSKUsXMLData", "@piShipViaXMLData", "@piStateXMLData" };
                    returnValue = db.ExCommand(objCompHash, "av_Fulfillment_Validations", arrSpFieldSeq, "@poRecordCount", "@poErrorMessage", "@poStoreIDErrorMessage", "@poSKUsErrorMessage", "@poShipViaErrorMessage", "@poStateErrorMessage", out poRecordCount, out poErrorMessage, out poStoreIDErrorMessage, out poSKUsErrorMessage, out poShipViaErrorMessage, out poStateErrorMessage);

                }
                catch (Exception objExp)
                {
                    errorMessage = "av_Fulfillment_Validations: " + objExp.Message.ToString();
                    Logger.LogMessage(objExp, this); // 
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }

            }
            return returnValue;
        }

        public  int SetPurchaseOrderChangeStatusDB(List<PurchaseOrderChangeStatus> poList, string status, int userID, int conditionalStatus)
        {
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string poXML = clsGeneral.SerializeObject(poList);
                //DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@piPoXML", poXML);
                    objCompHash.Add("@piStatus", status);
                    objCompHash.Add("@piUserID", userID);
                    objCompHash.Add("@piConditionalStatus", conditionalStatus);

                    arrSpFieldSeq = new string[] { "@piPoXML", "@piStatus", "@piUserID", "@piConditionalStatus" };
                    returnValue = db.ExecCommand(objCompHash, "Av_PurchaseOrder_ChangeStatus", arrSpFieldSeq);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //  throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnValue;
        }
        public  List<clsPurchaseOrder> SaveBulkPurchaseOrderDB(List<clsPurchaseOrder> poList, string companyAccountNumber, int userID, string poSource, string fileName, string comment, out int poRecordCount, out int poReturnCount, out string errorMessage)
        {
            //int returnValue = 0;
            poRecordCount = 0;
            poReturnCount = 0;
            errorMessage = string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string poXML = clsGeneral.SerializeObject(poList);

                //DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;
                string sCode = string.Empty;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@piCompanyAccountNumber", companyAccountNumber);
                    objCompHash.Add("@piXMLData", poXML);
                    objCompHash.Add("@piUserID", userID);
                    objCompHash.Add("@piPOSource", poSource);
                    objCompHash.Add("@FileName", fileName);
                    objCompHash.Add("@Comment", comment);

                    arrSpFieldSeq = new string[] { "@piCompanyAccountNumber", "@piXMLData", "@piUserID", "@piPOSource", "@FileName", "@Comment" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_Bulk_Create", arrSpFieldSeq, "@poRecordCount", "@poReturnCount", out poRecordCount, out poReturnCount);
                    int statusID = 0;
                    string poNum = string.Empty;
                    string comments = "None";

                    foreach (DataRow dataRowItem in dt.Rows)
                    {
                        statusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "statusID", 0, false));
                        if (statusID > 0)
                            comments = "Uploaded";
                        poNum = clsGeneral.getColumnData(dataRowItem, "ponum", string.Empty, false) as string;
                        //update po status
                        poList
                           .Where(x => x.PurchaseOrderNumber == poNum)
                           .ToList()
                           .ForEach(x => { x.Comments = comments; });

                    }
                }
                catch (Exception objExp)
                {
                    errorMessage = "av_PurchaseOrder_Bulk_Create: " + objExp.Message.ToString();
                    Logger.LogMessage(objExp, this); // 
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }

            return poList;
        }

        public  List<BasePurchaseOrderItem> GetPurchaseOrderItemsAndTrackingList(int PO_ID, out List<TrackingDetail> trackingList)
        {
            List<BasePurchaseOrderItem> poList = default;// new List<BasePurchaseOrderItem>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;//new DataSet();
                trackingList = new List<TrackingDetail>();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", PO_ID);

                    arrSpFieldSeq = new string[] { "@POID" };
                    ds = db.GetDataSet(objCompHash, "Aero_PurchaseOrder_Detail_Select", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 1)
                    {
                        trackingList = PopulateTrackingList(ds.Tables[1]);
                    }
                    poList= PopulatePurchaseOrderItemList(ds.Tables[0]);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //  throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return poList;
        }

        public  void PurchaseOrderUpdateDetailNew(int podID, int qty, int userID, out string returnMessage)
        {
            returnMessage = default;//string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POD_ID", podID);
                    objCompHash.Add("@Qty", qty);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@POD_ID", "@Qty", "@UserID" };
                    db.ExCommand(objCompHash, "av_PurchaseOrderDetail_Update", arrSpFieldSeq, "@ReturnMessage", out returnMessage);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //   throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }
        public  List<ShipBy> GetShipByList(string Nationality)
        {
            List<ShipBy> shipBylist = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();
                
                try
                {
                    objCompHash.Add("@ShipByID", 0);
                    objCompHash.Add("@ShipByCode", string.Empty);
                    objCompHash.Add("@Nationality", Nationality);
                    arrSpFieldSeq = new string[] { "@ShipByID", "@ShipByCode", "@Nationality" };
                    dt = db.GetTableRecords(objCompHash, "av_ShipBy_Select", arrSpFieldSeq);
                    shipBylist = PopulateShipBy(dt);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //   throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                    //db = null;
                }
            }
            return shipBylist;
        }

        //public  bool AuthenticateRequestNew(clsAuthentication AuthenticateUser, out int userid, out int companyID)
        //{
        //    userid = companyID = 0;
        //    bool returnValue = false;
        //    returnValue = clsUser.ValidateUserNew(AuthenticateUser, out userid, out companyID);
        //    //int userId = 0;
        //    //userId = clsUser.ValidateUser(AuthenticateUser);
        //    return returnValue;
        //}
        public string CreatePurchaseOrderDB(string POXml, int UserID, int forecastGUID, string poFlag, bool IsInterNational, out string poNumber)
        {
            FulfillmentLogModel logModel = new FulfillmentLogModel();
            PurchaseOrderResponse response = new PurchaseOrderResponse();

            logModel.ActionName = "Fulfillment Create";
            logModel.CreateUserID = UserID;
            logModel.StatusID = 1;
            logModel.PO_ID = 0;
            logModel.FulfillmentNumber = string.Empty;

            string errorMessage = string.Empty;
            poNumber = string.Empty;
            int returnValue = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                string sCode2 = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {

                    POXml = "<purchaseorder>" + POXml.Substring(POXml.IndexOf("<purchaseordernumber>"));
                    logModel.RequestData = POXml;
                    objCompHash.Add("@Po_Xml", POXml);
                    objCompHash.Add("@UserID", UserID);
                    objCompHash.Add("@ForecastGUID", forecastGUID);
                    objCompHash.Add("@POFlag", poFlag);
                    objCompHash.Add("@IsInternational", IsInterNational);
                    arrSpFieldSeq = new string[] { "@Po_Xml", "@UserID", "@ForecastGUID", "@POFlag", "@IsInternational" };
                    //returnValue = db.ExecCommand(objCompHash, "Aero_PurchaseOrder_Create", arrSpFieldSeq);
                    returnValue = db.ExCommand(objCompHash, "Aero_PurchaseOrder_Create", arrSpFieldSeq, "@poNumber", "@outparam", out poNumber, out sCode2);
                    if (returnValue == 0 && string.IsNullOrEmpty(sCode2))
                    {
                        errorMessage = ResponseErrorCode.UploadedSuccessfully.ToString();

                        logModel.PO_ID = returnValue;
                        logModel.FulfillmentNumber = poNumber;
                        response.ErrorCode = string.Empty;// ResponseErrorCode.UploadedSuccessfully.ToString();
                        response.PurchaseOrderNumber = poNumber;
                        response.Comment = errorMessage;

                    }
                    else
                    {
                        if (returnValue > 0)
                        {
                            if (!string.IsNullOrEmpty(sCode2))
                            {
                                errorMessage = sCode2;//ResponseErrorCode.StroreIDNotExists.ToString();
                                response.PurchaseOrderNumber = poNumber;
                                response.ErrorCode = errorMessage;
                                response.Comment = errorMessage;

                            }
                            else
                            {
                                errorMessage = ResponseErrorCode.PurchaseOrderAlreadyExists.ToString();
                                response.PurchaseOrderNumber = poNumber;
                                response.ErrorCode = errorMessage;
                                response.Comment = errorMessage;
                            }
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(sCode2))
                            {
                                errorMessage = ResponseErrorCode.StroreIDNotExists.ToString();
                                response.PurchaseOrderNumber = poNumber;
                                response.ErrorCode = errorMessage;
                                response.Comment = errorMessage;

                            }
                        }
                    }
                }
                catch (Exception objExp)
                {
                    errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                    response.PurchaseOrderNumber = string.Empty;
                    response.ErrorCode = errorMessage;
                    response.Comment = errorMessage;
                    logModel.Comment = errorMessage;
                    Logger.LogMessage(objExp, this); //  
                }
                finally
                {
                    string POreposponseXml = BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                    POreposponseXml = "<PurchaseOrderResponse>" + POreposponseXml.Substring(POreposponseXml.IndexOf("<PurchaseOrderNumber>"));

                    logModel.ResponseData = POreposponseXml;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                    LogOperations logOperations = new LogOperations();
                    logOperations.FulfillmentLogInsert(logModel);
                    //LogOperations.FulfillmentLogInsert(logModel);
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return errorMessage;
        }

        public  List<FulfillmentSKUs> GetPurchaseSKUList(int PO_ID, out List<POEsn> esnList)
        {
            List<FulfillmentSKUs> fulfillmentSKUs = default;//new List<FulfillmentSKUs>();
            esnList = default;//new List<POEsn>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", PO_ID);

                    arrSpFieldSeq = new string[] { "@POID" };
                    ds = db.GetDataSet(objCompHash, "Av_PurchaseOrder_AssignedESN_Select", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        fulfillmentSKUs = PopulateFufillmentSKUs(ds.Tables[0]);
                        if (ds != null && ds.Tables.Count > 1)
                            esnList = PopulateESNs(ds.Tables[1]);

                    }
                    return fulfillmentSKUs;
                }
                catch (Exception objExp)
                {
                    throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }

        public List<POEsn> GetUnProvisionEsnList(int POID )
        {
            List<POEsn> esnList  = default;//new List<POEsn>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", POID);

                    arrSpFieldSeq = new string[] { "@POID" };
                    ds = db.GetDataSet(objCompHash, "Av_PurchaseOrder_UnProvisionedESN_Select", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                         esnList = PopulateESNs(ds.Tables[0]);

                    }
                   
                }
                catch (Exception objExp)
                {
                    throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return esnList;
        }

        public PurchaseOrders GerPurchaseOrdersView(string PONumber, string companyAccountNumber)
        {
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@piPoNum", PONumber);
                    objCompHash.Add("@piCompanyAccountNumber", companyAccountNumber);


                    arrSpFieldSeq = new string[] { "@piPoNum", "@piCompanyAccountNumber" };
                    ds = db.GetDataSet(objCompHash, "Av_PurchaseOrder_View", arrSpFieldSeq);

                    return PopulatePurchaseOrdersNew(ds);
                }
                catch (Exception objExp)
                {
                    throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    db.DBClose();
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }

        public  List<BasePurchaseOrderItem> GetPurchaseOrderInventorySummary(string PONumber, string ContactName, string POFromDate, string POToDate, int UserID,
                                                       string statusID, int CompanyID, string esn, string trackingNumber, string MslNumber,
                                                       string PhoneCategory, string ItemCode, string StoreID, string POType, string customerOrderNumber,
                                                       string shipFrom, string shipTo, int PO_ID, out List<FulfillmentStatus> statusList)
        {
            List<BasePurchaseOrderItem> itemList = null;// = new List<BasePurchaseOrderItem>();
            statusList = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            //List<FulfillmentStatus> statusList = new List<FulfillmentStatus>();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", PONumber);
                objCompHash.Add("@Contact_Name", ContactName);
                objCompHash.Add("@From_Date", POFromDate);
                objCompHash.Add("@To_Date", POToDate);
                objCompHash.Add("@StatusID", statusID);
                objCompHash.Add("@UserID", UserID);
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@esn", esn);
                objCompHash.Add("@TrackingNumber", trackingNumber);
                objCompHash.Add("@MslNumber", MslNumber);
                objCompHash.Add("@PhoneCategory", PhoneCategory);
                objCompHash.Add("@ItemCode", ItemCode);
                objCompHash.Add("@StoreID", StoreID);
                objCompHash.Add("@POType", POType);
                objCompHash.Add("@customerOrderNumber", customerOrderNumber);
                objCompHash.Add("@ShipFrom", shipFrom);
                objCompHash.Add("@ShipTo", shipTo);
                objCompHash.Add("@POID", PO_ID);

                //if (!string.IsNullOrEmpty(zoneGUID))
                // {
                //    objCompHash.Add("@ZoneGUID", zoneGUID);
                //}

                arrSpFieldSeq = new string[] { "@Po_Num", "@Contact_Name", "@From_Date", "@To_Date", "@StatusID", "@UserID", "@CompanyID", "@esn", "@TrackingNumber", "@MslNumber",
                    "@PhoneCategory", "@ItemCode", "@StoreID", "@POType", "@customerOrderNumber", "@ShipFrom", "@ShipTo", "@POID" };

                ds = db.GetDataSet(objCompHash, "AV_PurchaseOrderInventory_Sumarry", arrSpFieldSeq);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    itemList = PopulatePurchaseOrderItemSummary(ds.Tables[0]);
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    statusList = PopulateFulfillmentStatusList(ds.Tables[1]);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return itemList;
        }


        #region private methods
        private  List<FulfillmentStatus> PopulateFulfillmentStatusList(DataTable dt)
        {
            List<FulfillmentStatus> statusList = new List<FulfillmentStatus>();
            if (dt != null && dt.Rows.Count > 0)
            {
                FulfillmentStatus fulfillmentStatus;
                foreach (DataRow dataRowItem in dt.Rows)
                {
                    fulfillmentStatus = new FulfillmentStatus();
                    fulfillmentStatus.FulfillmentOrderStatus = (PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                    fulfillmentStatus.StatusCount = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusCount", 0, false));
                    statusList.Add(fulfillmentStatus);


                }
            }

            return statusList;
        }

        private List<BasePurchaseOrderItem> PopulatePurchaseOrderItemSummary(DataTable dt)
        {
            List<BasePurchaseOrderItem> purchaseOrderItemList = null;

            //BasePurchaseOrder purchaseOrder = null;


            if (dt != null && dt.Rows.Count > 0)
            {
                int itemCount = 0;
                purchaseOrderItemList = new List<BasePurchaseOrderItem>();
                BasePurchaseOrderItem purchaseOrderItem;
                foreach (DataRow dataRowItem in dt.Rows)
                {
                    purchaseOrderItem = new BasePurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                    purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "ItemCount", 0, false));
                    purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                    purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                    purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                    purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                    //itemCount = (int)clsGeneral.getColumnData(dataRowItem, "ItemCount", 0, false);
                    //purchaseOrderItem.BoxId = itemCount.ToString();
                    purchaseOrderItem.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                    purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "fmupc", string.Empty, false);
                    //purchaseOrderItem.WarehouseCode = (string)clsGeneral.getColumnData(dataRowItem, "whCode", string.Empty, false);
                    //purchaseOrderItem.WholesaleCost = Convert.ToDouble(clsGeneral.getColumnData(dataRowItem, "cost", 0, false));
                    //purchaseOrderItem.TilaDocument = (string)clsGeneral.getColumnData(dataRowItem, "tilaDocument", string.Empty, false);
                    //purchaseOrderItem.V2Document = (string)clsGeneral.getColumnData(dataRowItem, "v2document", string.Empty, false);

                    //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));
                    purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "mslNumber", string.Empty, false);
                    purchaseOrderItem.MsID = (string)clsGeneral.getColumnData(dataRowItem, "MSID", string.Empty, false);
                    purchaseOrderItem.PassCode = (string)clsGeneral.getColumnData(dataRowItem, "pass_code", string.Empty, false);
                    purchaseOrderItem.PODStatus = (PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                    purchaseOrderItem.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                    purchaseOrderItem.CustomAttribute = Convert.ToBoolean(clsGeneral.getColumnData(dataRowItem, "CustomAttribute", false, false));
                    purchaseOrderItem.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "LTEICCID", string.Empty, false);
                    purchaseOrderItem.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                    purchaseOrderItem.Otksal = (string)clsGeneral.getColumnData(dataRowItem, "otksl", string.Empty, false);
                    purchaseOrderItem.Akey = (string)clsGeneral.getColumnData(dataRowItem, "akey", string.Empty, false);

                    purchaseOrderItemList.Add(purchaseOrderItem);
                }
            }





            return purchaseOrderItemList;
        }

        private List<FulfillmentSKUs> PopulateFufillmentSKUs(DataTable dt)
        {
            List<FulfillmentSKUs> purchaseOrderItemList = default;// null;

            //BasePurchaseOrder purchaseOrder = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                purchaseOrderItemList = new List<FulfillmentSKUs>();
                FulfillmentSKUs purchaseOrderItem;
                foreach (DataRow dataRowItem in dt.Rows)
                {
                    purchaseOrderItem = new FulfillmentSKUs();
                    purchaseOrderItem.POD_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false));
                    purchaseOrderItem.SKU = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                    purchaseOrderItem.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Qty", 0, false));
                    purchaseOrderItem.AssignedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "AssignedQty", 0, false));
                    purchaseOrderItem.IsDelete = Convert.ToBoolean(clsGeneral.getColumnData(dataRowItem, "IsDelete", 0, false));

                    purchaseOrderItemList.Add(purchaseOrderItem);
                }
            }
            return purchaseOrderItemList;
        }
        private  List<POEsn> PopulateESNs(DataTable dt)
        {
            List<POEsn> esnList = default;//new List<POEsn>();
            if (dt != null && dt.Rows.Count > 0)
            {
                esnList = new List<POEsn>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    POEsn esn = new POEsn();
                    esn.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    esn.ICCID = "";// clsGeneral.getColumnData(dataRow, "ICC_ID", string.Empty, false) as string;
                    esn.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                    esn.PalletID = clsGeneral.getColumnData(dataRow, "PalletID", string.Empty, false) as string;
                    esn.BoxID = clsGeneral.getColumnData(dataRow, "BoxID", string.Empty, false) as string;
                    esn.Hex = clsGeneral.getColumnData(dataRow, "MeidHex", string.Empty, false) as string;
                    esn.Dec = clsGeneral.getColumnData(dataRow, "MeidDec", string.Empty, false) as string;
                    esn.SerialNumber = clsGeneral.getColumnData(dataRow, "SerialNumber", string.Empty, false) as string;
                    esn.Location = clsGeneral.getColumnData(dataRow, "Location", string.Empty, false) as string;

                    esnList.Add(esn);
                }
            }
            return esnList;
        }

        private List<ShipBy> PopulateShipBy(DataTable dataTable)
        {
            List<ShipBy> shipBylist = default;// new List<ShipBy>();
            ShipBy shipByInfo = default;//null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                shipBylist = new List<ShipBy>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    shipByInfo = new ShipBy();
                    shipByInfo.ShipByCode = Convert.ToString(clsGeneral.getColumnData(dataRow, "ShipByCode", string.Empty, true));
                    shipByInfo.ShipByText = (string)clsGeneral.getColumnData(dataRow, "ShipByText", string.Empty, true);
                    shipByInfo.ShipCodeNText = (string)clsGeneral.getColumnData(dataRow, "ShipCodeNText", string.Empty, true);
                    shipByInfo.ShipByID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShipByID", 0, true));

                    shipBylist.Add(shipByInfo);
                }
            }
            return shipBylist;
        }

        private  PurchaseOrders PopulatePurchaseOrdersNew(DataSet ds)
        {
            DateTime shipDateCheck;
            PurchaseOrders purchaseOrders = default;//null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                purchaseOrders = new PurchaseOrders();
                BasePurchaseOrder purchaseOrder = default;//null;
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {

                    purchaseOrder = new BasePurchaseOrder(Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true)),
                        (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, true));
                    purchaseOrder.FactOrderNumber = (string)clsGeneral.getColumnData(dataRow, "FactOrderNumber", string.Empty, false);
                    purchaseOrder.POSource = (string)clsGeneral.getColumnData(dataRow, "POSource", string.Empty, false);
                    purchaseOrder.POType = (string)clsGeneral.getColumnData(dataRow, "POType", string.Empty, false);

                    purchaseOrder.IsShipmentRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsShipmentRequired", false, false));
                    purchaseOrder.IsInterNational = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsInterNational", false, false));

                    purchaseOrder.LineItemCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LineItemCount", 0, false));
                    purchaseOrder.OrderSent = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderSent", 0, false));
                    purchaseOrder.StatusColor = (string)clsGeneral.getColumnData(dataRow, "StatusColor", string.Empty, false);
                    purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, true));
                    purchaseOrder.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                    purchaseOrder.CustomerOrderNumber = (string)clsGeneral.getColumnData(dataRow, "CustomerOrderNumber", string.Empty, false);
                    purchaseOrder.RequestedShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "Requestedshipdate", DateTime.Now, true));

                    purchaseOrder.CustomerAccountNumber = (string)clsGeneral.getColumnData(dataRow, "CustomerAccountNumber", string.Empty, false);
                    purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                    purchaseOrder.Comments = (string)clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false);
                    //purchaseOrder.Shipping.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                    purchaseOrder.Shipping.ContactName = (string)clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, true);
                    purchaseOrder.Shipping.ContactPhone = (string)clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress2 = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAttn = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Attn", string.Empty, false);
                    purchaseOrder.Shipping.ShipToCity = (string)clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, true);
                    purchaseOrder.Shipping.ShipToState = (string)clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false);
                    purchaseOrder.Shipping.ShipToZip = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, true);
                    purchaseOrder.Tracking.ShipToTrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, true);
                    purchaseOrder.CustomerNumber = (string)clsGeneral.getColumnData(dataRow, "customernumber", string.Empty, true);
                    purchaseOrder.CustomerName = (string)clsGeneral.getColumnData(dataRow, "companyname", string.Empty, false);
                    purchaseOrder.CompanyLogo = (string)clsGeneral.getColumnData(dataRow, "LogoPath", string.Empty, false);

                    //purchaseOrder.ShipThrough = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, true);
                    shipDateCheck = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.MinValue, false));
                    if (shipDateCheck != DateTime.MinValue)
                        purchaseOrder.Tracking.ShipToDate = shipDateCheck;

                    purchaseOrder.Tracking.ShipToBy = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false);
                    purchaseOrder.Tracking.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                    //purchaseOrder.SentASN = (string)clsGeneral.getColumnData(dataRow, "SentASN", string.Empty, false);
                    //purchaseOrder.SentESN = (string)clsGeneral.getColumnData(dataRow, "SentESN", string.Empty, false);
                    purchaseOrder.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    purchaseOrder.PurchaseOrderStatus = (PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "Status", 1, false));
                    purchaseOrder.PurchaseOrderStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Status", 0, false));
                    purchaseOrder.ModifiedDate = (string)clsGeneral.getColumnData(dataRow, "LastUpdateDate", string.Empty, false);

                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        BasePurchaseOrderItem purchaseOrderItem;
                        foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                        {
                            purchaseOrderItem = new BasePurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                            purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_no", 0, false));
                            purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                            purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                            //  purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                            purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                            // purchaseOrderItem.BoxId = (string)clsGeneral.getColumnData(dataRowItem, "Box_id", string.Empty, false);
                            purchaseOrderItem.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                            //  purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "fmupc", string.Empty, false);
                            purchaseOrderItem.WarehouseCode = (string)clsGeneral.getColumnData(dataRowItem, "whCode", string.Empty, false);
                            purchaseOrderItem.WholesaleCost = Convert.ToDouble(clsGeneral.getColumnData(dataRowItem, "cost", 0, false));
                            //purchaseOrderItem.TilaDocument = (string)clsGeneral.getColumnData(dataRowItem, "tilaDocument", string.Empty, false);
                            //purchaseOrderItem.V2Document = (string)clsGeneral.getColumnData(dataRowItem, "v2document", string.Empty, false);

                            //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));
                            purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "mslNumber", string.Empty, false);
                            // purchaseOrderItem.MsID = (string)clsGeneral.getColumnData(dataRowItem, "MSID", string.Empty, false);
                            purchaseOrderItem.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "ICC_ID", string.Empty, false);
                            purchaseOrderItem.PODStatus = (PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 1, false));
                            purchaseOrderItem.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                            purchaseOrderItem.ItemsPerContainer = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "ItemsPerContainer", 0, false));
                            purchaseOrderItem.ContainersPerPallet = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "ContainerPerPallet", 0, false));

                            purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);
                        }
                    }

                    purchaseOrders.PurchaseOrderList.Add(purchaseOrder);
                }
            }

            return purchaseOrders;
        }

        private  PurchaseOrders PopulatePurchaseOrders(DataSet ds)
        {
            DateTime shipDateCheck;
            PurchaseOrders purchaseOrders = default;//null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                purchaseOrders = new PurchaseOrders();
                BasePurchaseOrder purchaseOrder = default;//null;
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {

                    purchaseOrder = new BasePurchaseOrder(Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true)),
                        (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, true));
                    purchaseOrder.StatusColor = (string)clsGeneral.getColumnData(dataRow, "StatusColor", string.Empty, false);

                    purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, true));
                    purchaseOrder.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                    purchaseOrder.CustomerAccountNumber = (string)clsGeneral.getColumnData(dataRow, "CustomerAccountNumber", string.Empty, false);
                    purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                    purchaseOrder.Comments = (string)clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false);
                    //purchaseOrder.Shipping.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                    purchaseOrder.Shipping.ContactName = (string)clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, true);
                    purchaseOrder.Shipping.ContactPhone = (string)clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress2 = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAttn = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Attn", string.Empty, false);
                    purchaseOrder.Shipping.ShipToCity = (string)clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, true);
                    purchaseOrder.Shipping.ShipToState = (string)clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false);
                    purchaseOrder.Shipping.ShipToZip = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, true);
                    purchaseOrder.Tracking.ShipToTrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, true);
                    purchaseOrder.CustomerNumber = (string)clsGeneral.getColumnData(dataRow, "customernumber", string.Empty, true);
                    purchaseOrder.CustomerName = (string)clsGeneral.getColumnData(dataRow, "companyname", string.Empty, false);

                    //purchaseOrder.ShipThrough = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, true);
                    shipDateCheck = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.MinValue, false));
                    if (shipDateCheck != DateTime.MinValue)
                        purchaseOrder.Tracking.ShipToDate = shipDateCheck;

                    purchaseOrder.Tracking.ShipToBy = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false);
                    purchaseOrder.Tracking.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                    purchaseOrder.SentASN = (string)clsGeneral.getColumnData(dataRow, "SentASN", string.Empty, false);
                    purchaseOrder.SentESN = (string)clsGeneral.getColumnData(dataRow, "SentESN", string.Empty, false);
                    purchaseOrder.CompanyID = Convert.ToInt16(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    purchaseOrder.PurchaseOrderStatus = (PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "Status", 1, false));
                    purchaseOrder.PurchaseOrderStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Status", 0, false));
                    purchaseOrder.ModifiedDate = (string)clsGeneral.getColumnData(dataRow, "LastUpdateDate", string.Empty, false);
                    purchaseOrder.ReturnLabel = (string)clsGeneral.getColumnData(dataRow, "ReturnLabel", string.Empty, false);
                    if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        BasePurchaseOrderItem purchaseOrderItem;
                        foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                        {
                            purchaseOrderItem = new BasePurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                            purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_no", 0, false));
                            purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                            purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                            purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                            purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                            purchaseOrderItem.BoxId = (string)clsGeneral.getColumnData(dataRowItem, "Box_id", string.Empty, false);
                            purchaseOrderItem.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                            purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "fmupc", string.Empty, false);
                            purchaseOrderItem.WarehouseCode = (string)clsGeneral.getColumnData(dataRowItem, "whCode", string.Empty, false);
                            purchaseOrderItem.WholesaleCost = Convert.ToDouble(clsGeneral.getColumnData(dataRowItem, "cost", 0, false));
                            //purchaseOrderItem.TilaDocument = (string)clsGeneral.getColumnData(dataRowItem, "tilaDocument", string.Empty, false);
                            //purchaseOrderItem.V2Document = (string)clsGeneral.getColumnData(dataRowItem, "v2document", string.Empty, false);

                            //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));
                            purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "mslNumber", string.Empty, false);
                            purchaseOrderItem.MsID = (string)clsGeneral.getColumnData(dataRowItem, "MSID", string.Empty, false);
                            purchaseOrderItem.PassCode = (string)clsGeneral.getColumnData(dataRowItem, "pass_code", string.Empty, false);
                            purchaseOrderItem.PODStatus = (PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 1, false));
                            purchaseOrderItem.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                            purchaseOrderItem.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "LTEICCID", string.Empty, false);
                            purchaseOrderItem.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                            purchaseOrderItem.Otksal = (string)clsGeneral.getColumnData(dataRowItem, "otksl", string.Empty, false);
                            purchaseOrderItem.Akey = (string)clsGeneral.getColumnData(dataRowItem, "akey", string.Empty, false);
                            purchaseOrderItem.SimNumber = (string)clsGeneral.getColumnData(dataRowItem, "SimNumber", string.Empty, false);
                            purchaseOrderItem.BatchNumber = (string)clsGeneral.getColumnData(dataRowItem, "BatchNumber", string.Empty, false);

                            purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);
                        }
                    }

                    purchaseOrders.PurchaseOrderList.Add(purchaseOrder);
                }
            }

            return purchaseOrders;
        }
        private  PurchaseOrders PopulatePurchaseOrdersDownload(DataSet ds)
        {
            DateTime shipDateCheck;
            PurchaseOrders purchaseOrders = default;//null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                purchaseOrders = new PurchaseOrders();
                BasePurchaseOrder purchaseOrder = default;//null;
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {

                    purchaseOrder = new BasePurchaseOrder(Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true)),
                        (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, true));
                    purchaseOrder.StatusColor = (string)clsGeneral.getColumnData(dataRow, "StatusColor", string.Empty, false);

                    purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, true));
                    purchaseOrder.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                    purchaseOrder.CustomerAccountNumber = (string)clsGeneral.getColumnData(dataRow, "CustomerAccountNumber", string.Empty, false);
                    purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                    purchaseOrder.Comments = (string)clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false);
                    //purchaseOrder.Shipping.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                    purchaseOrder.Shipping.ContactName = (string)clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, true);
                    purchaseOrder.Shipping.ContactPhone = (string)clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAddress2 = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false);
                    purchaseOrder.Shipping.ShipToAttn = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Attn", string.Empty, false);
                    purchaseOrder.Shipping.ShipToCity = (string)clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, true);
                    purchaseOrder.Shipping.ShipToState = (string)clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false);
                    purchaseOrder.Shipping.ShipToZip = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, true);
                    purchaseOrder.Tracking.ShipToTrackingNumber = (string)clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, true);
                    purchaseOrder.CustomerNumber = (string)clsGeneral.getColumnData(dataRow, "customernumber", string.Empty, true);
                    purchaseOrder.CustomerName = (string)clsGeneral.getColumnData(dataRow, "companyname", string.Empty, false);

                    //purchaseOrder.ShipThrough = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, true);
                    shipDateCheck = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.MinValue, false));
                    if (shipDateCheck != DateTime.MinValue)
                        purchaseOrder.Tracking.ShipToDate = shipDateCheck;

                    purchaseOrder.Tracking.ShipToBy = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false);
                    purchaseOrder.Tracking.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                    purchaseOrder.SentASN = (string)clsGeneral.getColumnData(dataRow, "SentASN", string.Empty, false);
                    purchaseOrder.SentESN = (string)clsGeneral.getColumnData(dataRow, "SentESN", string.Empty, false);
                    purchaseOrder.CompanyID = Convert.ToInt16(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    purchaseOrder.PurchaseOrderStatus = (PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "Status", 1, false));
                    purchaseOrder.PurchaseOrderStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Status", 0, false));
                    purchaseOrder.ModifiedDate = (string)clsGeneral.getColumnData(dataRow, "LastUpdateDate", string.Empty, false);
                    purchaseOrder.ReturnLabel = (string)clsGeneral.getColumnData(dataRow, "ReturnLabel", string.Empty, false);
                    purchaseOrder.ReturnShipVia = (string)clsGeneral.getColumnData(dataRow, "ReturnShipVia", string.Empty, false);
                    if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                    {
                        BasePurchaseOrderItem purchaseOrderItem;
                        foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                        {
                            purchaseOrderItem = new BasePurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                            purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_no", 0, false));
                            purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                            purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                            purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                            purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                            purchaseOrderItem.BoxId = (string)clsGeneral.getColumnData(dataRowItem, "Box_id", string.Empty, false);
                            purchaseOrderItem.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                            purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "fmupc", string.Empty, false);
                            purchaseOrderItem.WarehouseCode = (string)clsGeneral.getColumnData(dataRowItem, "whCode", string.Empty, false);
                            purchaseOrderItem.WholesaleCost = Convert.ToDouble(clsGeneral.getColumnData(dataRowItem, "cost", 0, false));
                            //purchaseOrderItem.TilaDocument = (string)clsGeneral.getColumnData(dataRowItem, "tilaDocument", string.Empty, false);
                            //purchaseOrderItem.V2Document = (string)clsGeneral.getColumnData(dataRowItem, "v2document", string.Empty, false);

                            //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));
                            purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "mslNumber", string.Empty, false);
                            purchaseOrderItem.MsID = (string)clsGeneral.getColumnData(dataRowItem, "MSID", string.Empty, false);
                            purchaseOrderItem.PassCode = (string)clsGeneral.getColumnData(dataRowItem, "pass_code", string.Empty, false);
                            purchaseOrderItem.PODStatus = (PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 1, false));
                            purchaseOrderItem.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                            purchaseOrderItem.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "LTEICCID", string.Empty, false);
                            purchaseOrderItem.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                            purchaseOrderItem.Otksal = (string)clsGeneral.getColumnData(dataRowItem, "otksl", string.Empty, false);
                            purchaseOrderItem.Akey = (string)clsGeneral.getColumnData(dataRowItem, "akey", string.Empty, false);
                            purchaseOrderItem.SimNumber = (string)clsGeneral.getColumnData(dataRowItem, "SimNumber", string.Empty, false);
                            purchaseOrderItem.TrackingNumber = (string)clsGeneral.getColumnData(dataRowItem, "TrackingNumber", string.Empty, false);

                            purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);
                        }
                    }

                    purchaseOrders.PurchaseOrderList.Add(purchaseOrder);
                }
            }

            return purchaseOrders;
        }


        private  List<BasePurchaseOrderItem> PopulatePurchaseOrderItemList(DataTable dt)
        {
            List<BasePurchaseOrderItem> purchaseOrderItemList = default;//null;

            //BasePurchaseOrder purchaseOrder = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                int itemCount = 0;
                purchaseOrderItemList = new List<BasePurchaseOrderItem>();
                BasePurchaseOrderItem purchaseOrderItem;
                foreach (DataRow dataRowItem in dt.Rows)
                {
                    purchaseOrderItem = new BasePurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                    purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_No", 0, false));
                    purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                    purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                    //purchaseOrderItem.BatchNumber = (string)clsGeneral.getColumnData(dataRowItem, "BatchNumber", string.Empty, false);
                    purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);
                    //itemCount = (int)clsGeneral.getColumnData(dataRowItem, "ItemCount", 0, false);
                    //purchaseOrderItem.BoxId = itemCount.ToString();
                    purchaseOrderItem.UPC = (string)clsGeneral.getColumnData(dataRowItem, "UPC", string.Empty, false);
                    // purchaseOrderItem.FmUPC = (string)clsGeneral.getColumnData(dataRowItem, "fmupc", string.Empty, false);
                    //purchaseOrderItem.WarehouseCode = (string)clsGeneral.getColumnData(dataRowItem, "whCode", string.Empty, false);
                    //purchaseOrderItem.WholesaleCost = Convert.ToDouble(clsGeneral.getColumnData(dataRowItem, "cost", 0, false));
                    //purchaseOrderItem.TilaDocument = (string)clsGeneral.getColumnData(dataRowItem, "tilaDocument", string.Empty, false);
                    //purchaseOrderItem.V2Document = (string)clsGeneral.getColumnData(dataRowItem, "v2document", string.Empty, false);

                    //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));
                    //purchaseOrderItem.MslNumber = (string)clsGeneral.getColumnData(dataRowItem, "mslNumber", string.Empty, false);
                    //purchaseOrderItem.MsID = (string)clsGeneral.getColumnData(dataRowItem, "MSID", string.Empty, false);
                    //purchaseOrderItem.PassCode = (string)clsGeneral.getColumnData(dataRowItem, "pass_code", string.Empty, false);
                    purchaseOrderItem.PODStatus = (PurchaseOrderStatus)Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                    purchaseOrderItem.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "StatusID", 1, false));
                    purchaseOrderItem.CustomAttribute = Convert.ToBoolean(clsGeneral.getColumnData(dataRowItem, "CustomAttribute", false, false));
                    purchaseOrderItem.LTEICCID = (string)clsGeneral.getColumnData(dataRowItem, "ICC_ID", string.Empty, false);
                    //purchaseOrderItem.LTEIMSI = (string)clsGeneral.getColumnData(dataRowItem, "LTEIMSI", string.Empty, false);
                    //purchaseOrderItem.Otksal = (string)clsGeneral.getColumnData(dataRowItem, "otksl", string.Empty, false);
                    //purchaseOrderItem.Akey = (string)clsGeneral.getColumnData(dataRowItem, "akey", string.Empty, false);
                    // purchaseOrderItem.IsSim = Convert.ToBoolean(clsGeneral.getColumnData(dataRowItem, "IsSim", false, false));
                    // purchaseOrderItem.SimNumber = (string)clsGeneral.getColumnData(dataRowItem, "SimNumber", string.Empty, false);
                    purchaseOrderItem.TrackingNumber = (string)clsGeneral.getColumnData(dataRowItem, "TrackingNumber", string.Empty, false);
                    purchaseOrderItem.ProductName = (string)clsGeneral.getColumnData(dataRowItem, "ItemName", string.Empty, false);
                    //purchaseOrderItem.EsnCount = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "ESNCOUNT", 0, false));
                    purchaseOrderItem.ItemsPerContainer = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "ItemsPerContainer", 0, false));
                    purchaseOrderItem.ContainersPerPallet = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "ContainerPerPallet", 0, false));

                    purchaseOrderItemList.Add(purchaseOrderItem);
                }
            }
            return purchaseOrderItemList;
        }

        private  List<TrackingDetail> PopulateTrackingList(DataTable dt)
        {
            List<TrackingDetail> trackingList = default;//new List<TrackingDetail>();

            TrackingDetail tracking = default;//null;
            if (dt != null && dt.Rows.Count > 0)
            {
                trackingList = new List<TrackingDetail>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    tracking = new TrackingDetail();
                    tracking.TrackingNumber = clsGeneral.getColumnData(dataRow, "Trackingnumber", string.Empty, false) as string;
                    tracking.ShipByCode = clsGeneral.getColumnData(dataRow, "ShipByCode", string.Empty, false) as string;
                    tracking.ShipByID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShipByID", 0, false));
                    tracking.LineNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LineNumber", 0, false));
                    tracking.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipDate", DateTime.MinValue, false));

                    tracking.Comments = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                    tracking.ReturnValue = clsGeneral.getColumnData(dataRow, "ReturnLabel", string.Empty, false) as string;
                    tracking.EsnCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "EsnCount", 0, false));
                    tracking.TrackingSentDateTime = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "TrackingSentDateTime", DateTime.MinValue, false));
                    tracking.ShipPackage = Convert.ToString(clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false));
                    tracking.ShipPrice = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "FinalPostage", 0, false));
                    tracking.ShipWeight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));
                    trackingList.Add(tracking);
                }
            }
            return trackingList;
        }
        private  clsPurchaseOrder PopulatePurchaseOrder(DataSet ds)
        {
            clsPurchaseOrder purchaseOrder = default;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow dataRow = ds.Tables[0].Rows[0];

                purchaseOrder = new clsPurchaseOrder(Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, true)),
                    (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, true));
                purchaseOrder.PurchaseOrderDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, true));
                purchaseOrder.PurchaseOrderNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                purchaseOrder.StoreID = (string)clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false);
                purchaseOrder.Comments = (string)clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false);
                //purchaseOrder.AerovoiceSalesOrderNumber = (string)clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false);
                purchaseOrder.Shipping.ContactName = (string)clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, true);
                purchaseOrder.Shipping.ContactPhone = (string)clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false);
                purchaseOrder.Shipping.ShipToAddress = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false);
                purchaseOrder.Shipping.ShipToAddress2 = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false);
                purchaseOrder.Shipping.ShipToAttn = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Attn", string.Empty, false);
                purchaseOrder.ShipThrough = (string)clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false);
                purchaseOrder.Shipping.ShipToCity = (string)clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, true);
                //purchaseOrder.Shipping.ShipToDate = (DateTime)clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.Now, false);
                purchaseOrder.Shipping.ShipToState = (string)clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false);
                purchaseOrder.Shipping.ShipToZip = (string)clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, true);
                purchaseOrder.PurchaseOrderStatus = (PurchaseOrderStatus)Convert.ToInt16(clsGeneral.getColumnData(dataRow, "Status", 1, false));

                if (ds.Tables[1].Rows.Count > 0)
                {
                    PurchaseOrderItem purchaseOrderItem;
                    foreach (DataRow dataRowItem in ds.Tables[1].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                    {
                        purchaseOrderItem = new PurchaseOrderItem(Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "POD_ID", 0, false)));
                        purchaseOrderItem.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "Line_no", 0, false));
                        purchaseOrderItem.ESN = (string)clsGeneral.getColumnData(dataRowItem, "ESN", string.Empty, false);
                        purchaseOrderItem.ItemCode = (string)clsGeneral.getColumnData(dataRowItem, "Item_Code", string.Empty, false);
                        //purchaseOrderItem.MdnNumber = (string)clsGeneral.getColumnData(dataRowItem, "MDN", string.Empty, false);
                        //purchaseOrderItem.SimNumber = (string)clsGeneral.getColumnData(dataRowItem, "SimNumber", string.Empty, false);
                        purchaseOrderItem.Quantity = (int?)clsGeneral.getColumnData(dataRowItem, "Qty", 0, false);

                        //purchaseOrderItem.PhoneCategory = (PhoneCategoryType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "PhoneCategory", "C", false));

                        purchaseOrder.PurchaseOrderItems.Add(purchaseOrderItem);
                    }
                }
                if (ds.Tables.Count > 2 && ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    ReturnLabel returnLabelItem;

                    foreach (DataRow dataRowItem in ds.Tables[2].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                    {
                        returnLabelItem = new ReturnLabel();
                        returnLabelItem.TrackingNumber = (string)clsGeneral.getColumnData(dataRowItem, "TrackingNumber", string.Empty, false);
                        returnLabelItem.ShipmentType = (string)clsGeneral.getColumnData(dataRowItem, "ReturnLabel", string.Empty, false);
                        //ShipmentType shipmentType = (ShipmentType)Convert.ToChar(clsGeneral.getColumnData(dataRowItem, "ReturnLabels", string.Empty, false));

                        //returnLabelItem.ShipmentType = shipmentType;
                        returnLabelItem.ShipByCode = (string)clsGeneral.getColumnData(dataRowItem, "ShipByCode", string.Empty, false);
                        purchaseOrder.ReturnLabelItems.Add(returnLabelItem);
                    }
                }
                if (ds.Tables.Count > 3 && ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    List<FulfillmentComment> fulfillmentCommentList = new List<FulfillmentComment>();

                    FulfillmentComment fulfillmentComment;

                    foreach (DataRow dataRowItem in ds.Tables[3].Select("POID = " + purchaseOrder.PurchaseOrderID.ToString()))
                    {
                        fulfillmentComment = new FulfillmentComment();
                        fulfillmentComment.CommentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRowItem, "CommentDate", string.Empty, false));
                        fulfillmentComment.Comments = (string)clsGeneral.getColumnData(dataRowItem, "Comments", string.Empty, false);
                        fulfillmentCommentList.Add(fulfillmentComment);


                    }
                    purchaseOrder.FulfillmentCommentList = fulfillmentCommentList;
                }
            }

            return purchaseOrder;
        }

        #endregion
    }

}
