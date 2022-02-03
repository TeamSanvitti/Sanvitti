using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.RMA;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.RMA
{
    public class RmaOperation : BaseCreateInstance
    {
        
        public RmaInfo GetRMA(int rmaGUID)
        {
            RmaInfo rmaInfo = default;// new RmaInfo();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@RmaGUID", rmaGUID);

                    arrSpFieldSeq = new string[] { "@RmaGUID" };
                    ds = db.GetDataSet(objCompHash, "av_RMA_View", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        rmaInfo = PopulateRma(ds);
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return rmaInfo;
        }

        public  int RMAReceiveInsert(RmaReceive rmaInfo)
        {
            int ReturnCode = 0;
            using (DBConnect db = new DBConnect())
            {
                string ESNXml = clsGeneral.SerializeObject(rmaInfo.ReceiveList);
                string[] arrSpFieldSeq;

                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@RMAReceiveGUID", rmaInfo.RMAReceiveGUID);
                    objCompHash.Add("@RmaGUID", rmaInfo.RMAGUID);
                    objCompHash.Add("@Comments", rmaInfo.Comment);
                    objCompHash.Add("@ApprovedByID", rmaInfo.ApprovedID);
                    objCompHash.Add("@UserID", rmaInfo.UserID);
                    objCompHash.Add("@RmaReceiveXML", ESNXml);
                    objCompHash.Add("@ReceivedByID", rmaInfo.ReceivedByID);

                    arrSpFieldSeq = new string[] { "@RMAReceiveGUID", "@RmaGUID", "@Comments", "@ApprovedByID", "@UserID", "@RmaReceiveXML", "@ReceivedByID" };

                    ReturnCode = db.ExecuteNonQuery(objCompHash, "av_RMA_ReceiveInsert", arrSpFieldSeq);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return ReturnCode;
        }
        public int RMAReceiveDelete(int rmaReceiveDetailGUID, int userID)
        {
            int ReturnCode = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;

                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@RMAReceiveDetailGUID", rmaReceiveDetailGUID);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@RMAReceiveDetailGUID", "@UserID" };

                    ReturnCode = db.ExecuteNonQuery(objCompHash, "av_RMA_ReceiveDetail_Delete", arrSpFieldSeq);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return ReturnCode;
        }

        public  RmaReceive GetRmaReceiveSearch(int companyID, string rmaNumber, string trackingNumber)
        {
            RmaReceive rmaReceive = default;// new RmaReceive();
            using (DBConnect db = new DBConnect())
            {
                List<RmaReceiveDetail> rmaList = default;// new List<RmaReceiveDetail>();
                List<RmaReceiveDetail> receivedList = default;// new List<RmaReceiveDetail>();
                List<RmaReceiveTracking> rmaTrackingList = default;// new List<RmaReceiveTracking>();
                                                                   //RmaReceiveDetail rmaModel = null; // new RmaModel();
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@RmaNumber", rmaNumber);
                    objCompHash.Add("@TrackingNumber", trackingNumber);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@RmaNumber", "@TrackingNumber" };
                    ds = db.GetDataSet(objCompHash, "av_RmaReceive_Search", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        rmaReceive = new RmaReceive();
                        rmaList = PopulateRmaReceive(ds.Tables[0]);
                        receivedList = PopulateRmaReceive(ds.Tables[2]);
                        rmaTrackingList = PopulateRmaTracking(ds.Tables[1]);
                        //rma HEADER
                        rmaReceive.RmaNumber = ds.Tables[0].Rows[0]["RmaNumber"].ToString();
                        rmaReceive.RmaDate = ds.Tables[0].Rows[0]["RmaDate"].ToString();
                        rmaReceive.CustomerRmaNumber = ds.Tables[0].Rows[0]["CustomerRmaNumber"].ToString();
                        rmaReceive.RmaStatus = ds.Tables[0].Rows[0]["RmaStatus"].ToString();
                    }

                    rmaReceive.ReceiveList = rmaList;
                    rmaReceive.ReceivedList = receivedList;
                    rmaReceive.TrackingList = rmaTrackingList;



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
            return rmaReceive;
        }

        public static string ValidateRmaFields(RmaModel rmaInfo)
        {
            string returnResult = string.Empty;
            if(string.IsNullOrEmpty(rmaInfo.Address1))
            {
                returnResult = "Address is required!";
            }
            if (string.IsNullOrEmpty(rmaInfo.City))
            {
                if (string.IsNullOrEmpty(returnResult))
                    returnResult = "City is required!";
                else
                {
                    returnResult = returnResult + "\n City is required!";
                }
            }
            if (string.IsNullOrEmpty(rmaInfo.ContactName))
            {
                if (string.IsNullOrEmpty(returnResult))
                    returnResult = "Contact name is required!";
                else
                {
                    returnResult = returnResult + "\n name is required!";
                }
            }
            if (string.IsNullOrEmpty(rmaInfo.ContactNumber))
            {
                if (string.IsNullOrEmpty(returnResult))
                    returnResult = "Contact number is required!";
                else
                {
                    returnResult = returnResult + "\n Contact number is required!";
                }
            }
            if (string.IsNullOrEmpty(rmaInfo.Email))
            {
                if (string.IsNullOrEmpty(returnResult))
                    returnResult = "Email is required!";
                else
                {
                    returnResult = returnResult + "\n Email is required!";
                }
            }
            if (string.IsNullOrEmpty(rmaInfo.State))
            {
                if (string.IsNullOrEmpty(returnResult))
                    returnResult = "State is required!";
                else
                {
                    returnResult = returnResult + "\n State is required!";
                }
            }
            if (string.IsNullOrEmpty(rmaInfo.ZipCode))
            {
                if (string.IsNullOrEmpty(returnResult))
                    returnResult = "Zip code is required!";
                else
                {
                    returnResult = returnResult + "\n Zip code is required!";
                }
            }


            return returnResult;
        }
        public static string ValidateRmaStatusWithLineItemsStatuses(RmaModel rmaInfo)
        {
            string returnResult = string.Empty;
            if(rmaInfo.Status.ToLower() == "completed")
            {
                if(rmaInfo.ReceiveStatus.ToLower() == "received")
                {

                }
                else
                {
                    returnResult = "RMA status cannot be completed without receive!";
                }
            }
            return returnResult;
        }
        public RmaResponse RMAInsertUpdate(RmaModel rmaInfo)
        {
            RmaResponse response = new RmaResponse();
            response.RmaNumber = string.Empty;
            response.CustomerRMANumberExists = string.Empty;
            response.RMANumberExists = string.Empty;
            response.ReturnCode = 0;
            //int ReturnCode = 0;
            using (DBConnect db = new DBConnect())
            {
                //string CustomerRMANumberExists = string.Empty, RMANumberExists = string.Empty, RmaNumber = string.Empty;
                string ESNXml = clsGeneral.SerializeObject(rmaInfo.RmaDetail);
                string rmaDocXML = clsGeneral.SerializeObject(rmaInfo.RmaDocumentList);
                string[] arrSpFieldSeq;
                DataTable dataTable = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@RmaGUID", rmaInfo.RmaGUID);
                    objCompHash.Add("@rmadate", rmaInfo.RmaDate);
                    objCompHash.Add("@RmaStatusID", rmaInfo.RmaStatusID);
                    objCompHash.Add("@ponum", rmaInfo.FulfillmentNumber);
                    objCompHash.Add("@rmaCustomername", rmaInfo.ContactName);
                    objCompHash.Add("@ContactAddress", rmaInfo.Address1);
                    objCompHash.Add("@ContactState", rmaInfo.State);
                    objCompHash.Add("@ContactCity", rmaInfo.City);
                    objCompHash.Add("@ContactZip", rmaInfo.ZipCode);
                    objCompHash.Add("@ContactPhone", rmaInfo.ContactNumber);
                    objCompHash.Add("@ContactEmail", rmaInfo.Email);
                    objCompHash.Add("@Comment", rmaInfo.Comment);
                    objCompHash.Add("@AVComments", rmaInfo.LanComment);
                    objCompHash.Add("@rmaxml", ESNXml);
                    objCompHash.Add("@UserID", rmaInfo.UserID);
                    objCompHash.Add("@CreatedBy", rmaInfo.LoginUserID);
                    objCompHash.Add("@ModifiedBy", rmaInfo.LoginUserID);
                    objCompHash.Add("@LocationCode", "");
                    objCompHash.Add("@StoreID", rmaInfo.StoreID);
                    objCompHash.Add("@RmaDocXml", rmaDocXML);
                    objCompHash.Add("@DocRePrint", rmaInfo.DocRePrint);
                    objCompHash.Add("@MaxShipmentDate", rmaInfo.MaxShipmentDate.ToShortDateString() == "1/1/0001" ? DateTime.Now.AddDays(10) : rmaInfo.MaxShipmentDate);
                    objCompHash.Add("@IsAPI", rmaInfo.IsAPI);
                    objCompHash.Add("@RMANUMBERAPI", rmaInfo.RMANUMBERAPI);
                    objCompHash.Add("@ContactCountry", rmaInfo.Country);
                    objCompHash.Add("@DoNotSendShippingLabel", rmaInfo.DoNotSendShippingLabel);
                    objCompHash.Add("@AllowShippingLabel", rmaInfo.AllowShippingLabel);
                    objCompHash.Add("@CustomerRMANumber", rmaInfo.CustomerRMANumber);
                    objCompHash.Add("@POID", rmaInfo.POID);
                    objCompHash.Add("@ReceiveStatusID", rmaInfo.ReceiveStatusID);
                    objCompHash.Add("@TriageStatusID", rmaInfo.TriageStatusID);
                    objCompHash.Add("@RMASource", rmaInfo.RMASource);
                    objCompHash.Add("@rmanumber", rmaInfo.RmaNumber);

                    arrSpFieldSeq = new string[] { "@RmaGUID", "@rmadate", "@RmaStatusID", "@ponum"
                    , "@rmaCustomername", "@ContactAddress", "@ContactState", "@ContactCity", "@ContactZip"
                    , "@ContactPhone", "@ContactEmail", "@Comment","@AVComments", "@rmaxml", "@UserID", "@CreatedBy",
                    "@ModifiedBy", "@LocationCode", "@StoreID", "@RmaDocXml","@DocRePrint","@MaxShipmentDate","@IsAPI","@RMANUMBERAPI","@ContactCountry",
                    "@DoNotSendShippingLabel","@AllowShippingLabel","@CustomerRMANumber","@POID","@ReceiveStatusID", "@TriageStatusID", "@RMASource", "@rmanumber" };

                    dataTable = db.GetTableRecords(objCompHash, "av_rma_insert_update_New", arrSpFieldSeq);
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        response.ReturnCode = 1;
                        response.CustomerRMANumberExists = dataTable.Rows[0]["CustomerRMANumberExists"] as string;
                        response.RMANumberExists = dataTable.Rows[0]["RMANumberExists"] as string;
                        response.RmaNumber = dataTable.Rows[0]["rmanumber"] as string;
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return response;
        }
        public  RmaModel GetRMAInfo(int rmaGUID)
        {
            RmaModel objRMA = new RmaModel();
            using (DBConnect db = new DBConnect())
            {
                DataSet dataSet = default;// new DataSet();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@RmaGUID", rmaGUID);
                    objCompHash.Add("@rmanumber", "");
                    objCompHash.Add("@rmadate", "");
                    objCompHash.Add("@rmastatusID", 0);
                    objCompHash.Add("@companyid", -1);
                    objCompHash.Add("@rmaGUIDs", "");
                    objCompHash.Add("@UPC", "");

                    arrSpFieldSeq = new string[] { "@RmaGUID", "@rmanumber", "@rmadate", "@rmastatusID", "@companyid", "@rmaGUIDs", "@UPC" };

                    dataSet = db.GetDataSet(objCompHash, "av_rma_select", arrSpFieldSeq);

                    if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {

                            objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                            objRMA.TriageStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TriageStatusID", 0, false));
                            objRMA.ReceiveStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ReceiveStatusID", 0, false));
                            objRMA.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                            objRMA.CustomerRMANumber = clsGeneral.getColumnData(dataRow, "CustomerRmaNumber", string.Empty, false) as string;


                            objRMA.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue.ToShortDateString(), false));
                            objRMA.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "POGUID", 0, false));
                            objRMA.RmaStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaStatusID", 0, false));
                            objRMA.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));
                            objRMA.ContactName = clsGeneral.getColumnData(dataRow, "RmaContactName", string.Empty, false) as string;
                            objRMA.Comment = clsGeneral.getColumnData(dataRow, "Comment", string.Empty, false) as string;
                            objRMA.LanComment = clsGeneral.getColumnData(dataRow, "AVComments", string.Empty, false) as string;
                            objRMA.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                            objRMA.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                            //objRMA.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                            // objRMA.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                            objRMA.Address1 = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                            objRMA.City = clsGeneral.getColumnData(dataRow, "ContactCity", string.Empty, false) as string;
                            objRMA.State = clsGeneral.getColumnData(dataRow, "ContactState", string.Empty, false) as string;
                            objRMA.ZipCode = clsGeneral.getColumnData(dataRow, "ContactZip", string.Empty, false) as string;
                            objRMA.ContactNumber = clsGeneral.getColumnData(dataRow, "ContactPhone", string.Empty, false) as string;
                            objRMA.Email = clsGeneral.getColumnData(dataRow, "ContactEmail", string.Empty, false) as string;
                            //objRMA.LocationCode = clsGeneral.getColumnData(dataRow, "LocationCode", string.Empty, false) as string;
                            objRMA.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;

                            //objRMA.DocComment = clsGeneral.getColumnData(dataRow, "DOCComment", string.Empty, false) as string;
                            objRMA.MaxShipmentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "MaxShipmentDate", DateTime.MinValue.ToShortDateString(), false));
                            objRMA.ContactCountry = clsGeneral.getColumnData(dataRow, "ContactCountry", string.Empty, false) as string;
                            objRMA.DoNotSendShippingLabel = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "DoNotSendShippingLabel", false, false));
                            objRMA.AllowShippingLabel = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowShippingLabel", false, false));



                            //if (dataSet.Tables[1].Rows.Count > 0)
                            //    objRMA.RmaDetails = GetRMADetail(dataSet.Tables[1]);
                            if (dataSet.Tables[1].Rows.Count > 0)
                                objRMA.RmaDetail = GetRMADetail(dataSet.Tables[1], dataSet.Tables[3]);


                            if (dataSet.Tables.Count > 2 && dataSet.Tables[2].Rows.Count > 0)
                                objRMA.RmaDocumentList = PopulateRmaDocuments(dataSet.Tables[2]);

                        }
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return objRMA;
        }


        public  RmaModel GetRMASearch(int companyID, string poNumber, string trackingNumber)
        {
            //List<RmaModel> categoryList = new List<CategoryModel>();
            RmaModel rmaModel = default; // new RmaModel();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default; //new DataSet();
                Hashtable objCompHash = new Hashtable();
                string category = string.Empty;
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@PO_Num", poNumber);
                    objCompHash.Add("@TrackingNumber", trackingNumber);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@PO_Num", "@TrackingNumber" };
                    ds = db.GetDataSet(objCompHash, "av_RMA_By_PO", arrSpFieldSeq);
                    rmaModel = PopulateRMA(ds);
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
            return rmaModel;
        }
        public  List<RmaDetailModel> ValidateESN(int POID, List<ESNList> esnList)
        {
            
            List<RmaDetailModel> rmaList = default;// new List<RmaDetailModel>();
            using (DBConnect db = new DBConnect())
            {
                RmaDetailModel rmaDetail = default;

                string esnXML = clsGeneral.SerializeObject(esnList);

                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                string category = string.Empty;
                try
                {
                    objCompHash.Add("@POID", POID);
                    objCompHash.Add("@ESNXML", esnXML);

                    arrSpFieldSeq = new string[] { "@POID", "@ESNXML" };
                    dt = db.GetTableRecords(objCompHash, "av_ValidateESNs", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        rmaList = new List<RmaDetailModel>();
                        foreach (DataRow dataRow2 in dt.Rows)
                        {
                            rmaDetail = new RmaDetailModel();
                            rmaDetail.ErrorMessage = clsGeneral.getColumnData(dataRow2, "ErrorMessage", string.Empty, false) as string;
                            rmaDetail.ESN = clsGeneral.getColumnData(dataRow2, "ESN", string.Empty, false) as string;
                            rmaDetail.SKU = clsGeneral.getColumnData(dataRow2, "Item_Code", string.Empty, false) as string;
                            rmaDetail.ProductName = clsGeneral.getColumnData(dataRow2, "ItemName", string.Empty, false) as string;
                            rmaDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "Quantity", 0, false));
                            rmaDetail.SKUQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "SKUQty", 0, false));
                            rmaDetail.PO_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "PO_ID", 0, false));
                            rmaDetail.POD_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "POD_ID", 0, false));
                            rmaDetail.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "ItemCompanyGUID", 0, false));
                            rmaDetail.RmaDetGUID = 0;// Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "RmaGUID", 0, false));
                                                     // rmaDetail.RmaNumber = clsGeneral.getColumnData(dataRow2, "ESN", string.Empty, false) as string;
                            rmaList.Add(rmaDetail);
                        }
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
        public  List<RmaDetailModel> POValidateESN(List<ESNList> esnList)
        {
            RmaDetailModel rmaDetail = default;
            List<RmaDetailModel> rmaList = default;// new List<RmaDetailModel>();
            using (DBConnect db = new DBConnect())
            {
                string esnXML = clsGeneral.SerializeObject(esnList);

                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                string category = string.Empty;
                try
                {
                    //objCompHash.Add("@POID", POID);
                    objCompHash.Add("@ESNXML", esnXML);

                    arrSpFieldSeq = new string[] { "@ESNXML" };
                    dt = db.GetTableRecords(objCompHash, "av_RMAValidateESNs", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        rmaList = new List<RmaDetailModel>();
                        foreach (DataRow dataRow2 in dt.Rows)
                        {
                            rmaDetail = new RmaDetailModel();
                            rmaDetail.ErrorMessage = clsGeneral.getColumnData(dataRow2, "ErrorMessage", string.Empty, false) as string;
                            rmaDetail.ESN = clsGeneral.getColumnData(dataRow2, "ESN", string.Empty, false) as string;
                            rmaDetail.SKU = clsGeneral.getColumnData(dataRow2, "Item_Code", string.Empty, false) as string;
                            rmaDetail.ProductName = clsGeneral.getColumnData(dataRow2, "ItemName", string.Empty, false) as string;
                            rmaDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "Quantity", 0, false));
                            rmaDetail.SKUQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "SKUQty", 0, false));
                            rmaDetail.PO_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "PO_ID", 0, false));
                            rmaDetail.POD_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "POD_ID", 0, false));
                            rmaDetail.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "ItemCompanyGUID", 0, false));
                            rmaDetail.RmaDetGUID = 0;// Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "RmaGUID", 0, false));
                                                     // rmaDetail.RmaNumber = clsGeneral.getColumnData(dataRow2, "ESN", string.Empty, false) as string;
                            rmaList.Add(rmaDetail);
                        }
                    }
                }
                catch (Exception objExp)
                {
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                  //  db = null;
                }
            }

            return rmaList;
        }

        public  List<RmaDetailModel> GetRMADetail(DataTable dataTable, DataTable accessoryTable)
        {
            int accessoryID = 3;
            List<RmaDetailModel> rmaDetail = default;// new List<RmaDetailModel>();
            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    rmaDetail = new List<RmaDetailModel>();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RmaDetailModel objRMADETAIL = new RmaDetailModel();
                        objRMADETAIL.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMADETAIL.RmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "rmaDetGUID", 0, false));
                        objRMADETAIL.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        objRMADETAIL.Reason = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Reason", 0, false));
                        objRMADETAIL.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                       // objRMADETAIL.CallTime = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CallTime", 0, false));
                        objRMADETAIL.Notes = clsGeneral.getColumnData(dataRow, "Notes", string.Empty, false) as string;
                        //objRMADETAIL.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                        objRMADETAIL.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                        //objRMADETAIL.po = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "po_id", 0, false));
                        objRMADETAIL.POD_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pod_id", 0, false));
                        objRMADETAIL.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                        objRMADETAIL.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                        //objRMADETAIL.f = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                        //objRMADETAIL.i = clsGeneral.getColumnData(dataRow, "ItemDescription", string.Empty, false) as string;

                        objRMADETAIL.SKU = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;
                        objRMADETAIL.ProductName = clsGeneral.getColumnData(dataRow, "itemname", string.Empty, false) as string;
                        objRMADETAIL.Warranty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Warranty", 0, false));
                        objRMADETAIL.DispositionID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Disposition", 0, false));
                        //objRMADETAIL.RepairEstId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RepairEstID", 0, false));
                        objRMADETAIL.TriageStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TriageStatusID", 0, false));
                        objRMADETAIL.TriageNotes = clsGeneral.getColumnData(dataRow, "TriageNotes", string.Empty, false) as string;
                        //objRMADETAIL.ShippingTrackingNumber = clsGeneral.getColumnData(dataRow, "ShippingTrackingNumber", string.Empty, false) as string;
                        //objRMADETAIL.NewSKU = clsGeneral.getColumnData(dataRow, "NewSKU", string.Empty, false) as string;
                       // objRMADETAIL.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", DateTime.MinValue, false));
                       // objRMADETAIL.ReplacementSKUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));


                        //List<RMAAccessory> rmaAccessoryList = new List<RMAAccessory>();

                        //if (accessoryTable != null && accessoryTable.Rows.Count > 0)
                        //{
                        //    RMAAccessory objRMAAccessory = null;
                        //    foreach (DataRow dataRowItem in accessoryTable.Select("RMADetGUID = " + objRMADETAIL.rmaDetGUID.ToString()))
                        //    {
                        //        accessoryID = 2;
                        //        objRMAAccessory = new RMAAccessory();
                        //        objRMAAccessory.RMADetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "RMADetGUID", 0, false));
                        //        objRMAAccessory.AccessoryNumber = clsGeneral.getColumnData(dataRowItem, "AccessoryNumber", string.Empty, false) as string;
                        //        objRMAAccessory.AccessoryDescription = clsGeneral.getColumnData(dataRowItem, "AccessoryDescription", string.Empty, false) as string;
                        //        objRMAAccessory.AccessoryID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "AccessoryID", 0, false));
                        //        objRMAAccessory.IsChecked = true;
                        //        rmaAccessoryList.Add(objRMAAccessory);
                        //    }
                        //}
                        //objRMADETAIL.AccessoryID = accessoryID;
                        //objRMADETAIL.RMAAccessoryList = rmaAccessoryList;
                        rmaDetail.Add(objRMADETAIL);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex, this); //throw ex;
            }


            return rmaDetail;
        }


        public  List<CustomerRMAStatus> GetCustomerRMAStatusList(int CompanyID, bool rma)
        {
            List<CustomerRMAStatus> customerRMAStatusList = default;// new List<CustomerRMAStatus>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", CompanyID);
                    objCompHash.Add("@RMA", rma);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@RMA" };

                    dataTable = db.GetTableRecords(objCompHash, "av_Customer_RMAStatus_Select", arrSpFieldSeq);

                    customerRMAStatusList = PopulatecustomerRMAStatus(dataTable);

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
            return customerRMAStatusList;
        }
        public  List<CustomerRMAStatus> GetRmaDetailStatusList()
        {
            List<CustomerRMAStatus> rmaStatusList = default;// new List<CustomerRMAStatus>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", 0);
                    //objCompHash.Add("@RMA", rma);

                    arrSpFieldSeq = new string[] { "@CompanyID" };

                    dataTable = db.GetTableRecords(objCompHash, "av_RmaDetail_Status_Select", arrSpFieldSeq);

                    rmaStatusList = PopulatecustomerRMAStatus(dataTable);
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

        public List<RmaTriageStatus> GetTriageStatusList()
        {
            List<RmaTriageStatus> statusList = default;// new List<RmaTriageStatus>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@TriageStatusID", 0);

                    arrSpFieldSeq = new string[] { "@TriageStatusID" };

                    dataTable = db.GetTableRecords(objCompHash, "av_RMA_Triage_Statuses_Select", arrSpFieldSeq);

                    statusList = PopulateStatus(dataTable);

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
            return statusList;
        }
        public  List<RmaReceiveStatus> GetReceiveRMAStatusList()
        {
            List<RmaReceiveStatus> statusList = default;// new List<RmaReceiveStatus>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@ReceiveStatusID", 0);

                    arrSpFieldSeq = new string[] { "@ReceiveStatusID" };

                    dataTable = db.GetTableRecords(objCompHash, "av_RMA_Receive_Statuses_Select", arrSpFieldSeq);

                    statusList = PopulateReceiveRMAStatus(dataTable);

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
            return statusList;
        }
        
        private List<RmaReceiveStatus> PopulateReceiveRMAStatus(DataTable dataTable)
        {
            List<RmaReceiveStatus> statusList = default;// new List<RmaReceiveStatus>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                statusList = new List<RmaReceiveStatus>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RmaReceiveStatus obj = new RmaReceiveStatus();
                    obj.ReceiveStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ReceiveStatusID", 0, false));
                    obj.ReceiveStatus = clsGeneral.getColumnData(dataRow, "ReceiveStatus", string.Empty, false) as string;

                    statusList.Add(obj);
                }

            }
            return statusList;
        }
        private  List<CustomerRMAStatus> PopulatecustomerRMAStatus(DataTable dataTable)
        {
            List<CustomerRMAStatus> custRMAStatusList = default;// new List<CustomerRMAStatus>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                custRMAStatusList = new List<CustomerRMAStatus>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    CustomerRMAStatus objCustomerShipBy = new CustomerRMAStatus();
                    objCustomerShipBy.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    objCustomerShipBy.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                    objCustomerShipBy.StatusDescription = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;

                    custRMAStatusList.Add(objCustomerShipBy);
                }

            }
            return custRMAStatusList;
        }

        private  List<RmaTriageStatus> PopulateStatus(DataTable dataTable)
        {
            List<RmaTriageStatus> statusList = default;// new List<RmaTriageStatus>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                statusList = new List<RmaTriageStatus>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RmaTriageStatus obj = new RmaTriageStatus();
                    obj.TriageStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TriageStatusID", 0, false));
                    obj.TriageStatus = clsGeneral.getColumnData(dataRow, "TriageStatus", string.Empty, false) as string;

                    statusList.Add(obj);
                }

            }
            return statusList;
        }

        private List<RmaDocument> PopulateRmaDocuments(DataTable dataTable)
        {
            List<RmaDocument> rmaDocList = default;// new List<RmaDocument>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                rmaDocList = new List<RmaDocument>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RmaDocument objRMAdoc = new RmaDocument();

                    objRMAdoc.RmaDocID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaDocID", 0, false));
                   // objRMAdoc.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                    objRMAdoc.RmaDocName = clsGeneral.getColumnData(dataRow, "RmaDocument", string.Empty, false) as string;
                    objRMAdoc.DocType = clsGeneral.getColumnData(dataRow, "DocType", string.Empty, false) as string;
                    //objRMAdoc.ModifiedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ModifiedDate", DateTime.Now, false));
                    rmaDocList.Add(objRMAdoc);
                }
            }
            return rmaDocList;
        }
        

        private  RmaInfo PopulateRma(DataSet ds)
        {
            RmaInfo rmaInfo = default;
            List<RMAReceive> receiveList = default;
            RMAReceive rmaReceive = default;
            List<ReceiveDetail> receiveDetails = default;
            ReceiveDetail receiveDetail = default;
            List<RMADetail> rmaDetails = default;
            RMADetail rmaDetail = default;
            List<RmaTracking> rmaTrackings = default;
            RmaTracking rmaTracking = default;
            List<RmaComment> customerComments = default;
            RmaComment customerComment = default;
            List<RmaComment> internalComments = default;
            RmaComment internalComment = default;
            int RMAReceiveGUID = 0;
            string dispostion = string.Empty, warranty = string.Empty;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    rmaInfo = new RmaInfo();
                    rmaDetails = new List<RMADetail>();
                    receiveList = new List<RMAReceive>();
                    rmaTrackings = new List<RmaTracking>();
                    customerComments = new List<RmaComment>();
                    internalComments = new List<RmaComment>();

                    rmaInfo.CustomerAddress1 = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                    rmaInfo.CustomerCity = clsGeneral.getColumnData(dataRow, "ContactCity", string.Empty, false) as string;
                    rmaInfo.CustomerContactName = clsGeneral.getColumnData(dataRow, "RmaContactName", string.Empty, false) as string;
                    rmaInfo.CustomerContactNumber = clsGeneral.getColumnData(dataRow, "ContactPhone", string.Empty, false) as string;
                    rmaInfo.CustomerCountry = "US"; //clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                    rmaInfo.CustomerEmail = clsGeneral.getColumnData(dataRow, "ContactEmail", string.Empty, false) as string;
                    rmaInfo.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                    rmaInfo.CustomerRMANumber = clsGeneral.getColumnData(dataRow, "CustomerRmaNumber", string.Empty, false) as string;
                    rmaInfo.CustomerState = clsGeneral.getColumnData(dataRow, "ContactState", string.Empty, false) as string;
                    rmaInfo.CustomerZipCode = clsGeneral.getColumnData(dataRow, "ContactZip", string.Empty, false) as string;
                    rmaInfo.RmaDate = clsGeneral.getColumnData(dataRow, "RmaDate", string.Empty, false) as string;
                    rmaInfo.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                    rmaInfo.RmaStatus = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    rmaInfo.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;
                    rmaInfo.TriageStatus = clsGeneral.getColumnData(dataRow, "TriageStatus", string.Empty, false) as string;
                    rmaInfo.ReceiveStatus = clsGeneral.getColumnData(dataRow, "ReceiveStatus", string.Empty, false) as string;

                    //rmaInfo.RmaDate = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                    if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            rmaTracking = new RmaTracking();
                            rmaTracking.Comments = clsGeneral.getColumnData(row, "Comments", string.Empty, false) as string;
                            rmaTracking.FinalPostage = Convert.ToDecimal(clsGeneral.getColumnData(row, "FinalPostage", 0, false));
                            rmaTracking.Weight = Convert.ToDecimal(clsGeneral.getColumnData(row, "ShipWeight", 0, false));
                            rmaTracking.TrackingId = Convert.ToInt32(clsGeneral.getColumnData(row, "TrackingId", 0, false));
                            rmaTracking.FinalPostage = Convert.ToDecimal(clsGeneral.getColumnData(row, "FinalPostage", 0, false));
                            rmaTracking.Package = clsGeneral.getColumnData(row, "ShipPackage", string.Empty, false) as string;
                            rmaTracking.ShipDate = clsGeneral.getColumnData(row, "ShipDate", string.Empty, false) as string;
                            rmaTracking.ShipVia = clsGeneral.getColumnData(row, "ShipByText", string.Empty, false) as string;
                            rmaTracking.TrackingNumber = clsGeneral.getColumnData(row, "TrackingNumber", string.Empty, false) as string;
                            rmaTracking.Prepaid = clsGeneral.getColumnData(row, "Prepaid", string.Empty, false) as string;
                            rmaTrackings.Add(rmaTracking);
                        }
                    }
                    rmaInfo.RmaTrackings = rmaTrackings;
                    if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[2].Rows)
                        {
                            rmaDetail = new RMADetail();
                            rmaDetail.RmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(row, "RmaDetGUID", 0, false));
                            rmaDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(row, "Quantity", 0, false));
                            rmaDetail.ESN = clsGeneral.getColumnData(row, "ESN", string.Empty, false) as string;
                            rmaDetail.Notes = clsGeneral.getColumnData(row, "Notes", string.Empty, false) as string;
                            rmaDetail.ProductName = clsGeneral.getColumnData(row, "ItemName", string.Empty, false) as string;
                            rmaDetail.Reason = clsGeneral.getColumnData(row, "Reason", string.Empty, false) as string;
                            rmaDetail.SKU = clsGeneral.getColumnData(row, "SKU", string.Empty, false) as string;
                            rmaDetail.Status = clsGeneral.getColumnData(row, "StatusText", string.Empty, false) as string;
                            Disposition enumDispostion = (Disposition)Convert.ToInt32(clsGeneral.getColumnData(row, "Disposition", 0, false));
                            dispostion = enumDispostion.ToString();
                            Warranty enumWarranty = (Warranty)Convert.ToInt32(clsGeneral.getColumnData(row, "Warranty", 0, false));
                            warranty = enumWarranty.ToString();
                            rmaDetail.Disposition = dispostion; // clsGeneral.getColumnData(row, "Disposition", string.Empty, false) as string;
                            rmaDetail.Warranty = warranty; // clsGeneral.getColumnData(row, "Warranty", string.Empty, false) as string;


                            rmaDetail.TriageNotes = clsGeneral.getColumnData(row, "TriageNotes", string.Empty, false) as string;
                            rmaDetail.TriageStatus = clsGeneral.getColumnData(row, "TriageStatus", string.Empty, false) as string;
                            //rmaDetail.a = clsGeneral.getColumnData(row, "Reason", string.Empty, false) as string;
                            rmaDetails.Add(rmaDetail);
                        }
                    }
                    rmaInfo.RMADetails = rmaDetails;
                    if (ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[3].Rows)
                        {
                            rmaReceive = new RMAReceive();
                            receiveDetails = new List<ReceiveDetail>();

                            rmaReceive.ReceiveDate = clsGeneral.getColumnData(row, "CreateDate", string.Empty, false) as string;
                            rmaReceive.Comments = clsGeneral.getColumnData(row, "Comments", string.Empty, false) as string;
                            rmaReceive.ApprovedBy = clsGeneral.getColumnData(row, "Username", string.Empty, false) as string;
                            RMAReceiveGUID = Convert.ToInt32(clsGeneral.getColumnData(row, "RMAReceiveGUID", 0, false));

                            if (ds.Tables.Count > 1 && ds.Tables[4].Rows.Count > 0)
                            {
                                foreach (DataRow row1 in ds.Tables[4].Select("RMAReceiveGUID=" + RMAReceiveGUID))
                                {
                                    receiveDetail = new ReceiveDetail();
                                    receiveDetail.ESNReceived = clsGeneral.getColumnData(row1, "ESNReceived", string.Empty, false) as string;
                                    receiveDetail.ProductName = clsGeneral.getColumnData(row1, "ItemName", string.Empty, false) as string;
                                    receiveDetail.QtyReceived = Convert.ToInt32(clsGeneral.getColumnData(row1, "QtyReceived", 0, false));
                                    receiveDetail.ShippingTrackingNumber = clsGeneral.getColumnData(row1, "ShippingTrackingNumber", string.Empty, false) as string;
                                    receiveDetail.SKU = clsGeneral.getColumnData(row1, "SKU", string.Empty, false) as string;
                                    receiveDetail.ReceiveStatus = clsGeneral.getColumnData(row1, "ReceiveStatus", string.Empty, false) as string;

                                    receiveDetails.Add(receiveDetail);
                                }
                            }
                            rmaReceive.ReceiveDetails = receiveDetails;
                            receiveList.Add(rmaReceive);
                        }
                    }
                    rmaInfo.ReceiveList = receiveList;
                    if (ds.Tables.Count > 5 && ds.Tables[5].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[5].Rows)
                        {
                            customerComment = new RmaComment();
                            customerComment.Comments = clsGeneral.getColumnData(row, "Comments", string.Empty, false) as string;
                            customerComment.CreateDate = clsGeneral.getColumnData(row, "CommentDate", string.Empty, false) as string;
                            customerComment.CommentBy = clsGeneral.getColumnData(row, "Username", string.Empty, false) as string;
                            customerComments.Add(customerComment);
                        }
                    }
                    rmaInfo.CustomerComments = customerComments;
                    if (ds.Tables.Count > 6 && ds.Tables[6].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[6].Rows)
                        {
                            internalComment = new RmaComment();
                            internalComment.Comments = clsGeneral.getColumnData(row, "Comments", string.Empty, false) as string;
                            internalComment.CreateDate = clsGeneral.getColumnData(row, "CommentDate", string.Empty, false) as string;
                            internalComment.CommentBy = clsGeneral.getColumnData(row, "Username", string.Empty, false) as string;
                            internalComments.Add(internalComment);
                        }
                    }
                    rmaInfo.InternalComments = internalComments;
                    rmaInfo.RMADocuments = string.Empty;
                    if (ds.Tables.Count > 7 && ds.Tables[7].Rows.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[7].Rows)
                        {
                            rmaInfo.RMADocuments = clsGeneral.getColumnData(row, "RMADocument", string.Empty, false) as string;

                        }
                    }


                        }
                    }
            return rmaInfo;
        }
        private  List<RmaReceiveDetail> PopulateRmaReceive(DataTable dataTable)
        {
            List<RmaReceiveDetail> rmaList = default;// new List<RmaReceiveDetail>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                rmaList = new List<RmaReceiveDetail>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RmaReceiveDetail objRMA = new RmaReceiveDetail();

                    objRMA.RMADetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RMADetGUID", 0, false));
                    objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                    objRMA.RMAReceiveDetailGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RMAReceiveDetailGUID", 0, false));
                    objRMA.RMAReceiveGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RMAReceiveGUID", 0, false));
                    objRMA.ReceiveStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ReceiveStatusID", 0, false));
                    objRMA.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Quantity", 0, false));
                    objRMA.QtyReceived = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "QtyReceived", 0, false));
                    objRMA.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                    objRMA.ESNReceived = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    objRMA.ShippingTrackingNumber = clsGeneral.getColumnData(dataRow, "ShippingTrackingNumber", string.Empty, false) as string;
                    objRMA.ReceiveStatus = clsGeneral.getColumnData(dataRow, "ReceiveStatus", string.Empty, false) as string;
                    objRMA.ItemName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    objRMA.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    rmaList.Add(objRMA);
                }
            }
            return rmaList;
        }
        private  List<RmaReceiveTracking> PopulateRmaTracking(DataTable dataTable)
        {
            List<RmaReceiveTracking> rmaTrackingList = default;// new List<RmaReceiveTracking>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                rmaTrackingList = new List<RmaReceiveTracking>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RmaReceiveTracking rmaReceiveTracking = new RmaReceiveTracking();

                    //objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                    rmaReceiveTracking.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    rmaTrackingList.Add(rmaReceiveTracking);
                }
            }
            return rmaTrackingList;
        }

        private  RmaModel PopulateRMA(DataSet ds)
        {
            RmaModel rmaInfo = default;
            List<RmaDetailModel> existingRmaList = default;// new List<RmaDetailModel>();
            RmaDetailModel existingRmaDetail = default;
            RmaDetailModel rmaDetail = default;
            List<RmaDetailModel> rmaList = default;// new List<RmaDetailModel>();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    existingRmaList = new List<RmaDetailModel>();
                    rmaInfo = new RmaModel();
                    rmaInfo.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));
                    rmaInfo.Address1 = clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false) as string;
                    rmaInfo.Address2 = clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false) as string;
                    rmaInfo.ContactNumber = clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false) as string;
                    rmaInfo.ContactName = clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, false) as string;
                    rmaInfo.City = clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, false) as string;
                    rmaInfo.State = clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false) as string;
                    rmaInfo.ZipCode = clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, false) as string;
                    rmaInfo.StoreID = clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false) as string;
                    rmaInfo.PoStatus = clsGeneral.getColumnData(dataRow, "PoStatus", string.Empty, false) as string;
                    rmaInfo.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    if(ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach(DataRow dataRow1 in ds.Tables[1].Rows)
                        {
                            existingRmaDetail = new RmaDetailModel();
                           // existingRmaDetail.ESN = clsGeneral.getColumnData(dataRow1, "ESN", string.Empty, false) as string;
                            existingRmaDetail.SKU = clsGeneral.getColumnData(dataRow1, "Item_Code", string.Empty, false) as string;
                      //      existingRmaDetail.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                        //    existingRmaDetail.RmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow1, "RmaDetGUID", 0, false));
                            existingRmaDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow1, "Quantity", 0, false));
                            existingRmaDetail.RmaNumber = clsGeneral.getColumnData(dataRow1, "RmaNumber", string.Empty, false) as string;
                            existingRmaList.Add(existingRmaDetail);
                        }
                    }

                    if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                    {
                        rmaList = new List<RmaDetailModel>();
                        foreach (DataRow dataRow2 in ds.Tables[2].Rows)
                        {
                            rmaDetail = new RmaDetailModel();
                            rmaDetail.ErrorMessage = string.Empty;
                            rmaDetail.ESN = clsGeneral.getColumnData(dataRow2, "ESN", string.Empty, false) as string;
                            rmaDetail.SKU = clsGeneral.getColumnData(dataRow2, "Item_Code", string.Empty, false) as string;
                            rmaDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "Quantity", 0, false));
                            rmaDetail.SKUQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "SKUQty", 0, false));
                            rmaDetail.POD_ID = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "POD_ID", 0, false));
                            rmaDetail.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "ItemCompanyGUID", 0, false));
                            rmaDetail.RmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "RmaDetGUID", 0, false));
                            rmaDetail.ProductName = clsGeneral.getColumnData(dataRow2, "ItemName", string.Empty, false) as string;
                            rmaList.Add(rmaDetail);
                        }
                    }
                    rmaInfo.ExistingRmaDetail = existingRmaList ;
                    rmaInfo.RmaDetail = rmaList;

                }

            }
            return rmaInfo;
        }
    }
}
