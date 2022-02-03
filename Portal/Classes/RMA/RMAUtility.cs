using System;
using System.Collections.Generic;
using System.Web;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Text.RegularExpressions;

namespace avii.Classes
{
    public class RMAUtility
    {

        public static void RMA_Detail_Picture_InsertUpdate(int pictureID, int rmaDetGUID, string fileName, int userID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {

                objCompHash.Add("@PictureID", pictureID);
                objCompHash.Add("@RMADetGUID", rmaDetGUID);
                objCompHash.Add("@FileName", fileName);
                objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@PictureID", "@RMADetGUID", "@FileName", "@UserID" };
                db.ExeCommand(objCompHash, "av_RMA_Detail_Pictures_Insert_Update", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static void RMA_Detail_Picture_Delete(int pictureID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {

                objCompHash.Add("@PictureID", pictureID);
                

                arrSpFieldSeq = new string[] { "@PictureID" };
                db.ExeCommand(objCompHash, "av_RMA_Detail_Pictures_Delete", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static List<ESNImage> GetESNImageList(int rmaDelGUID)
        {
            List<ESNImage> imageList = new List<ESNImage>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {


                objCompHash.Add("@RMADetGUID", rmaDelGUID);

                arrSpFieldSeq = new string[] { "@RMADetGUID" };

                dataTable = db.GetTableRecords(objCompHash, "av_RMA_Detail_Pictures_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        ESNImage objESNImage = new ESNImage();
                        objESNImage.PictureID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PictureID", 0, false));
                        objESNImage.RMADelGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RMADelGUID", 0, false));
                        objESNImage.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", 0, false));
                        objESNImage.FileName = clsGeneral.getColumnData(dataRow, "FileName", string.Empty, false) as string;
                        objESNImage.CreatedBy = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;

                        imageList.Add(objESNImage);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return imageList;
        }

        public static List<RmaHistory> GetRMAHistory(int rmaGUID)
        {
            List<RmaHistory> historyList = new List<RmaHistory>();
            RmaHistory model = null;
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RMAGUID", rmaGUID);

                arrSpFieldSeq = new string[] { "@RMAGUID" };

                dataTable = db.GetTableRecords(objCompHash, "av_RMA_History_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        model = new RmaHistory();
                        model.ModuleName = Convert.ToString(clsGeneral.getColumnData(dataRow, "ModuleName", string.Empty, false));
                        model.Comments = Convert.ToString(clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false));
                        model.Status = Convert.ToString(clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false));
                       // model.RMADelGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RMADelGUID", 0, false));
                        model.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", 0, false));
                        //model.FileName = clsGeneral.getColumnData(dataRow, "FileName", string.Empty, false) as string;
                        model.CreatedBy = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;

                        historyList.Add(model);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return historyList;
        }

        public static List<CustomerSKUss> GetCustomerSKUList(int companyID, string sku)
        {
            List<CustomerSKUss> skuList = new List<CustomerSKUss>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {


                objCompHash.Add("@CompanyID", companyID);
                //objCompHash.Add("@UserID", userID);
                objCompHash.Add("@SKU", sku);

                //arrSpFieldSeq = new string[] { "@CompanyID", "@UserID", "@SKU" };
                arrSpFieldSeq = new string[] { "@CompanyID", "@SKU" };

                dataTable = db.GetTableRecords(objCompHash, "av_Customer_SKUs_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CustomerSKUss objSKU = new CustomerSKUss();
                        objSKU.QtyAvailable = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "QTYAVAILABLE", 0, false));
                        objSKU.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));

                        objSKU.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                        objSKU.SKUDescription = clsGeneral.getColumnData(dataRow, "ITEMDESCRIPTIOIN", string.Empty, false) as string;
                        skuList.Add(objSKU);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return skuList;
        }

        public static DataTable SKUandQtyAvailableInfo(int companyID, string sku)
        {
            string returnMsg = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@SKU", sku);

                arrSpFieldSeq = new string[] { "@CompanyID", "@SKU" };
                dt = db.GetTableRecords(objCompHash, "av_Customer_SKU_QtyAvailable", arrSpFieldSeq);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    returnMsg = dt.Rows[0]["QTYAVAILABLE"].ToString();
                //}

            }
            catch (Exception objExp)
            {
                //returnMsg = objExp.Message;
                throw new Exception(objExp.Message);
            }
            finally
            {
                db.DBClose();
                db = null;
            }

            return dt;
        }


        private static List<RMAComments> PopulateRMAComments(DataTable dataTable)
        {
            List<RMAComments> rmaCommentList = new List<RMAComments>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RMAComments objRMAComment = new RMAComments();



                    objRMAComment.CommentID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CommentID", 0, false));
                    objRMAComment.Exclude = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Exclude", 0, false));

                    objRMAComment.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                    objRMAComment.Comments = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                    objRMAComment.UserType = clsGeneral.getColumnData(dataRow, "UserType", string.Empty, false) as string;

                    objRMAComment.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CommentDate", DateTime.Now, false));
                    rmaCommentList.Add(objRMAComment);
                }
            }
            return rmaCommentList;
        }

        public static List<RMAComments> GetRMACommentList(int rmaGUID, bool exclude)
        {
            List<RMAComments> rmaCommentList = new List<RMAComments>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {


                objCompHash.Add("@RMAGUID", rmaGUID);
                objCompHash.Add("@Exclude", exclude);

                arrSpFieldSeq = new string[] { "@RMAGUID", "@Exclude" };

                dataTable = db.GetTableRecords(objCompHash, "av_RMA_Comments_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    rmaCommentList = PopulateRMAComments(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return rmaCommentList;
        }

        public static void ExcludeInclude_RMAComments(List<avii.Classes.RMAComments> rmaCommentsList, int rmaGUID)
        {
            string rmaXml = SerializeObject(rmaCommentsList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            // poRecordCount = 0;
            try
            {
                objCompHash.Add("@RmaXML", rmaXml);
                objCompHash.Add("@RMAGUID", rmaGUID);
                //objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@RmaXML", "@RMAGUID" };
                //db.ExeCommand(objCompHash, "AV_RMA_ChangeStaus", arrSpFieldSeq, "@poRecordCount", out poRecordCount);
                db.ExeCommand(objCompHash, "AV_RMA_Comments_Exclude_Include", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static RMATrackingInfo GetRMATrackingInfo(int rmaGUID)
        {
            RMATrackingInfo rmaRMATrackingInfo = new RMATrackingInfo();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {


                objCompHash.Add("@RMAGUID", rmaGUID);
                // objCompHash.Add("@DocType", docType);

                arrSpFieldSeq = new string[] { "@RMAGUID" };

                dataTable = db.GetTableRecords(objCompHash, "av_RMATracking_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {


                        rmaRMATrackingInfo.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                        rmaRMATrackingInfo.EventType = clsGeneral.getColumnData(dataRow, "EventType", string.Empty, false) as string;
                        rmaRMATrackingInfo.TrackingDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", DateTime.Now, false));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return rmaRMATrackingInfo;
        }

        private static List<RmaDocuments> PopulateRmaDocuments(DataTable dataTable)
        {
            List<RmaDocuments> rmaDocList = new List<RmaDocuments>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RmaDocuments objRMAdoc = new RmaDocuments();

                    objRMAdoc.RmaDocID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaDocID", 0, false));
                    objRMAdoc.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                    objRMAdoc.RmaDocument = clsGeneral.getColumnData(dataRow, "RmaDocument", string.Empty, false) as string;
                    objRMAdoc.DocType = clsGeneral.getColumnData(dataRow, "DocType", string.Empty, false) as string;
                    objRMAdoc.ModifiedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ModifiedDate", DateTime.Now, false));
                    rmaDocList.Add(objRMAdoc);
                }
            }
            return rmaDocList;
        }
        public static List<RmaDocuments> GetRmaDocumentList(int rmaGUID)
        {
            List<RmaDocuments> rmaDocList = new List<RmaDocuments>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {


                objCompHash.Add("@RmaGUID", rmaGUID);
                // objCompHash.Add("@DocType", docType);

                arrSpFieldSeq = new string[] { "@RmaGUID" };

                dataTable = db.GetTableRecords(objCompHash, "av_RMADocument_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    rmaDocList = PopulateRmaDocuments(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return rmaDocList;
        }

        public static DataSet GetRmaDocumentLists(int rmaGUID)
        {
            List<RmaDocuments> rmaDocList = new List<RmaDocuments>();
            DataSet ds = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {


                objCompHash.Add("@RmaGUID", rmaGUID);
                //objCompHash.Add("@DocType", docType);

                arrSpFieldSeq = new string[] { "@RmaGUID" };

                ds = db.GetDataSet(objCompHash, "av_RMADocument_Select", arrSpFieldSeq);


            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return ds;
        }
        public static void Delete_RMA_Document(int rmaGUID, int rmaDocID, int userID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {

                objCompHash.Add("@RmaGUID", rmaGUID);
                objCompHash.Add("@RmaDocID", rmaDocID);

                objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@RmaGUID", "@RmaDocID", "@UserID" };
                db.ExeCommand(objCompHash, "av_RmaDocument_delete", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static RMAResponse GetRMAList(RMASearchCriteria rmaRequest)
        {
            RMAResponse serviceResponse = new RMAResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            string requestXML = clsGeneral.SerializeObject(rmaRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "GetRMA";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            try
            {

                if (rmaRequest != null)
                {

                    int userId = 0;
                    int companyID = 0;
                    if (PurchaseOrder.AuthenticateRequestNew(rmaRequest.Authentication, out userId, out companyID))
                    {
                        request.UserID = userId;
                        request.CompanyID = companyID;
                        List<RMAReport> rmaList = GetRMAReport(rmaRequest.RMANumber, rmaRequest.RMA_From_Date.ToShortDateString(), rmaRequest.RMA_To_Date.ToShortDateString(), (int)rmaRequest.RMAStatus, companyID, 0, rmaRequest.ESN, userId);

                        if (rmaList != null && rmaList.Count > 0)
                        {
                            serviceResponse.RmaReportList = rmaList;
                            serviceResponse.ErrorCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                        }
                        else
                            serviceResponse.ErrorCode = ResponseErrorCode.NoRecordsFound.ToString();
                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = serviceResponse.ErrorCode;

                }
            }
            catch (Exception exc)
            {
                ex = exc;
                serviceResponse.RmaReportList = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();

            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }
            return serviceResponse;
        }

        public static List<RMAReport> GetRMAReport(string rmaNumber, string rmaFromDate, string rmaToDate, int statusID, int companyid, int returnReason, string esn, int userID)
        {
            List<RMAReport> rmaReportList = new List<RMAReport>();
            string[] arrSpFieldSeq;
            DataTable dt;
            Hashtable objCompHash = new Hashtable();
            DBConnect db = new DBConnect();
            try
            {
                objCompHash.Add("@rmanumber", rmaNumber);
                objCompHash.Add("@rmaFromdate", rmaFromDate);
                objCompHash.Add("@rmaTodate", rmaToDate);
                objCompHash.Add("@rmastatusID", statusID);
                objCompHash.Add("@companyid", companyid);
                objCompHash.Add("@UPC", string.Empty);
                objCompHash.Add("@ESN", esn);
                objCompHash.Add("@returnReason", returnReason);
                objCompHash.Add("@UserID", userID);
                arrSpFieldSeq = new string[] { "@rmanumber", "@rmaFromdate", "@rmaTodate", "@rmastatusID", "@companyid", "@UPC", "@ESN", "@returnReason", "@UserID" };

                dt = db.GetTableRecords(objCompHash, "av_RMA_Detail_Report", arrSpFieldSeq);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {

                        RMAReport objRMA = new RMAReport();
                        //objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMA.RMANumber = (clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string).Trim();
                        objRMA.RMADate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false));
                        objRMA.ESN = (clsGeneral.getColumnData(dataRow, "esn", string.Empty, false) as string).Trim();
                        objRMA.SKU = (clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false) as string).Trim();
                        objRMA.Status = (clsGeneral.getColumnData(dataRow, "RMAStatus", string.Empty, false) as string).Trim();
                        objRMA.DefectReason = (clsGeneral.getColumnData(dataRow, "ReasonTxt", string.Empty, false) as string).Trim();
                        objRMA.Notes = (clsGeneral.getColumnData(dataRow, "Comment", string.Empty, false) as string).Trim();
                        objRMA.WSANotes = (clsGeneral.getColumnData(dataRow, "AVComments", string.Empty, false) as string).Trim();
                        rmaReportList.Add(objRMA);
                    }
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
                dt = null;
                db.DBClose();
            }
            return rmaReportList;

        }

        public static List<RMA> GetRMAList(int rmaGUID, string rmanumber, string rmadate, string rmadateTo, int rmastatusID, int companyID,
            string rmaGUIDs, string UPC, string esn, string avso, string poNum, string returnReason)
        {
            List<RMA> rmaList = new List<RMA>();
            DataSet dataSet = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RmaGUID", rmaGUID);
                objCompHash.Add("@rmanumber", rmanumber);
                objCompHash.Add("@rmadate", rmadate);
                objCompHash.Add("@rmadateTo", rmadateTo);
                objCompHash.Add("@rmastatusID", rmastatusID);
                objCompHash.Add("@companyid", companyID);
                objCompHash.Add("@rmaGUIDs", rmaGUIDs);
                objCompHash.Add("@UPC", UPC);
                objCompHash.Add("@esn", esn);
                objCompHash.Add("@avso", avso);
                objCompHash.Add("@po_num", poNum);
                objCompHash.Add("@returnReason", returnReason);


                arrSpFieldSeq = new string[] { "@RmaGUID", "@rmanumber", "@rmadate", "@rmadateTo", "@rmastatusID", "@companyid", "@rmaGUIDs", "@UPC", "@esn", "@avso", "@po_num", "@returnReason" };

                dataSet = db.GetDataSet(objCompHash, "av_rma_select", arrSpFieldSeq);


                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        RMA objRMA = new RMA();
                        objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMA.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        objRMA.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false));
                        objRMA.POGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "POGUID", 0, false));
                        objRMA.RmaStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaStatusID", 0, false));
                        objRMA.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));
                        objRMA.RmaContactName = clsGeneral.getColumnData(dataRow, "RmaContactName", string.Empty, false) as string;
                        objRMA.Address = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                        objRMA.City = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                        objRMA.State = clsGeneral.getColumnData(dataRow, "ContactCity", string.Empty, false) as string;
                        objRMA.Zip = clsGeneral.getColumnData(dataRow, "ContactZip", string.Empty, false) as string;
                        objRMA.Comment = clsGeneral.getColumnData(dataRow, "Comment", string.Empty, false) as string;
                        objRMA.AVComments = clsGeneral.getColumnData(dataRow, "AVComments", string.Empty, false) as string;
                        objRMA.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                        objRMA.RMAUserCompany.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        objRMA.RMAUserCompany.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objRMA.RMAUserCompany.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                        objRMA.LocationCode = clsGeneral.getColumnData(dataRow, "LocationCode", string.Empty, false) as string;
                        if (dataSet.Tables[1].Rows.Count > 0)
                        {
                            //HttpContext.Current.Session["rmadetail"] = dataSet.Tables[1];
                            objRMA.RmaDetails = PopulateRMADetailList(dataSet.Tables[1]);
                        }


                        rmaList.Add(objRMA);
                    }
                }

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

            return rmaList;
        }

        public static List<RMADetail> PopulateRMADetailList(DataTable dataTable)
        {

            List<RMADetail> rmaDetailList = new List<RMADetail>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                RMADetail objRMADETAIL = new RMADetail();
                objRMADETAIL.rmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                objRMADETAIL.rmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "rmaDetGUID", 0, false));
                objRMADETAIL.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "SalesOrderNumber", string.Empty, false) as string;
                objRMADETAIL.Reason = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Reason", 0, false));
                objRMADETAIL.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                objRMADETAIL.CallTime = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CallTime", 0, false));
                objRMADETAIL.Notes = clsGeneral.getColumnData(dataRow, "Notes", string.Empty, false) as string;
                objRMADETAIL.WaferSealed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WaferSealed", 0, false));
                objRMADETAIL.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                objRMADETAIL.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                objRMADETAIL.Po_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "poid", 0, false));
                objRMADETAIL.Pod_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pod_id", 0, false));
                objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "SalesOrderNumber", string.Empty, false) as string;
                objRMADETAIL.PurchaseOrderNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;

                objRMADETAIL.ItemCode = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;
                rmaDetailList.Add(objRMADETAIL);
            }
            return rmaDetailList;
        }

        public static List<RMA> getRMAList(int rmaGUID, string rmanumber, string rmadate, string rmadateTo, int rmastatusID, int companyID, string rmaGUIDs, string UPC, string esn, string avso, string poNum, string returnReason, string storeID)
        {
            List<RMA> rmaList = new List<RMA>();
            DataSet dataSet = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RmaGUID", rmaGUID);
                objCompHash.Add("@rmanumber", rmanumber);
                objCompHash.Add("@rmadate", rmadate);
                objCompHash.Add("@rmadateTo", rmadateTo);
                objCompHash.Add("@rmastatusID", rmastatusID);
                objCompHash.Add("@companyid", companyID);
                objCompHash.Add("@rmaGUIDs", rmaGUIDs);
                objCompHash.Add("@UPC", UPC);
                objCompHash.Add("@esn", esn);
                objCompHash.Add("@avso", avso);
                objCompHash.Add("@po_num", poNum);
                objCompHash.Add("@returnReason", returnReason);
                objCompHash.Add("@StoreID", storeID);

                arrSpFieldSeq = new string[] { "@RmaGUID", "@rmanumber", "@rmadate", "@rmadateTo", "@rmastatusID", "@companyid", "@rmaGUIDs", "@UPC", "@esn", "@avso", "@po_num", "@returnReason", "@StoreID" };
                dataSet = db.GetDataSet(objCompHash, "av_rma_select", arrSpFieldSeq);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        RMA objRMA = new RMA();
                        objRMA.ByPO = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ByPO", false, false));
                        objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMA.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        objRMA.CustomerRMANumber = clsGeneral.getColumnData(dataRow, "CustomerRMANumber", string.Empty, false) as string;
                        objRMA.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false));
                        objRMA.POGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "POGUID", 0, false));
                        objRMA.RmaStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaStatusID", 0, false));
                        objRMA.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));
                        objRMA.RmaContactName = clsGeneral.getColumnData(dataRow, "RmaContactName", string.Empty, false) as string;
                        objRMA.Address = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                        objRMA.City = clsGeneral.getColumnData(dataRow, "ContactCity", string.Empty, false) as string;
                        objRMA.State = clsGeneral.getColumnData(dataRow, "ContactState", string.Empty, false) as string;
                        objRMA.Zip = clsGeneral.getColumnData(dataRow, "ContactZip", string.Empty, false) as string;
                        objRMA.Comment = clsGeneral.getColumnData(dataRow, "Comment", string.Empty, false) as string;
                        objRMA.AVComments = clsGeneral.getColumnData(dataRow, "AVComments", string.Empty, false) as string;
                        objRMA.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                        objRMA.PoNum = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                        //objRMA.RmaDetails = getRMADetail(objRMA.RmaGUID, -1, string.Empty);
                        objRMA.RMAUserCompany.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        objRMA.RMAUserCompany.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objRMA.RMAUserCompany.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                        objRMA.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                        objRMA.RMASource = clsGeneral.getColumnData(dataRow, "RMASource", string.Empty, false) as string;

                        //objRMA.LocationCode = clsGeneral.getColumnData(dataRow, "LocationCode", string.Empty, false) as string;
                        objRMA.Phone = clsGeneral.getColumnData(dataRow, "ContactPhone", string.Empty, false) as string;
                        objRMA.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;
                        objRMA.RmaDocument = clsGeneral.getColumnData(dataRow, "RmaDocument", string.Empty, false) as string;
                        objRMA.DocComment = clsGeneral.getColumnData(dataRow, "DOCComment", string.Empty, false) as string;

                        objRMA.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                        objRMA.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipDate", DateTime.MinValue, false)).ToShortDateString();
                        objRMA.ShipMethod = clsGeneral.getColumnData(dataRow, "ShipByCode", string.Empty, false) as string;
                        objRMA.ShipWeight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShipWeight", 0, false))==0?"": Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShipWeight", 0, false)).ToString();
                        objRMA.ShipComment = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                        objRMA.FinalPostage = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "FinalPostage", 0, false));
                        objRMA.CompanyLogo = clsGeneral.getColumnData(dataRow, "LogoPath", string.Empty, false) as string;

                        if (dataSet.Tables[1].Rows.Count > 0)
                        {
                            HttpContext.Current.Session["rmadetail"] = dataSet.Tables[1];
                            //objRMA.RmaDetails = PopulateRMADetailList(dataSet.Tables[1]);
                        }
                        rmaList.Add(objRMA);
                    }
                }
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
            return rmaList;
        }

        public static List<RMA> GetAaronsRMAList(int rmaGUID, string rmanumber, string rmadate, string rmadateTo, int rmastatusID, int companyID, string rmaGUIDs, string UPC, string esn, string avso, string poNum, string returnReason, string storeID, int userID, bool isAarons)
        {
            List<RMA> rmaList = new List<RMA>();
            DataSet dataSet = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RmaGUID", rmaGUID);
                objCompHash.Add("@rmanumber", rmanumber);
                objCompHash.Add("@rmadate", rmadate);
                objCompHash.Add("@rmadateTo", rmadateTo);
                objCompHash.Add("@rmastatusID", rmastatusID);
                objCompHash.Add("@companyid", companyID);
                objCompHash.Add("@rmaGUIDs", rmaGUIDs);
                objCompHash.Add("@UPC", UPC);
                objCompHash.Add("@esn", esn);
                objCompHash.Add("@avso", avso);
                objCompHash.Add("@po_num", poNum);
                objCompHash.Add("@returnReason", returnReason);
                objCompHash.Add("@StoreID", storeID);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@IsAarons", isAarons);


                arrSpFieldSeq = new string[] { "@RmaGUID", "@rmanumber", "@rmadate", "@rmadateTo", "@rmastatusID", "@companyid", "@rmaGUIDs", "@UPC", "@esn", "@avso", "@po_num", "@returnReason", "@StoreID", "@UserID", "@IsAarons" };

                dataSet = db.GetDataSet(objCompHash, "av_rma_select", arrSpFieldSeq);


                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        RMA objRMA = new RMA();
                        objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMA.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        objRMA.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false));
                        objRMA.POGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "POGUID", 0, false));
                        objRMA.RmaStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaStatusID", 0, false));
                        objRMA.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));
                        objRMA.RmaContactName = clsGeneral.getColumnData(dataRow, "RmaContactName", string.Empty, false) as string;
                        objRMA.Address = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                        objRMA.City = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                        objRMA.State = clsGeneral.getColumnData(dataRow, "ContactCity", string.Empty, false) as string;
                        objRMA.Zip = clsGeneral.getColumnData(dataRow, "ContactZip", string.Empty, false) as string;
                        objRMA.Comment = clsGeneral.getColumnData(dataRow, "Comment", string.Empty, false) as string;
                        objRMA.AVComments = clsGeneral.getColumnData(dataRow, "AVComments", string.Empty, false) as string;
                        objRMA.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                        //objRMA.RmaDetails = getRMADetail(objRMA.RmaGUID, -1, string.Empty);
                        objRMA.RMAUserCompany.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        objRMA.RMAUserCompany.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objRMA.RMAUserCompany.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                        objRMA.LocationCode = clsGeneral.getColumnData(dataRow, "LocationCode", string.Empty, false) as string;
                        objRMA.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;
                        objRMA.RmaDocument = clsGeneral.getColumnData(dataRow, "RmaDocument", string.Empty, false) as string;
                        objRMA.DocComment = clsGeneral.getColumnData(dataRow, "DOCComment", string.Empty, false) as string;

                        if (dataSet.Tables[1].Rows.Count > 0)
                        {
                            HttpContext.Current.Session["rmadetail"] = dataSet.Tables[1];
                            //objRMA.RmaDetails = PopulateRMADetailList(dataSet.Tables[1]);
                        }
                        if (dataSet.Tables.Count > 2 && dataSet.Tables[2].Rows.Count > 0)
                        {
                            HttpContext.Current.Session["rmaaccessories"] = dataSet.Tables[2];
                            //objRMA.RmaDetails = PopulateRMADetailList(dataSet.Tables[1]);
                        }

                        rmaList.Add(objRMA);
                    }
                }

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

            return rmaList;
        }

        public static RMA getRMAInfo(int rmaGUID, string rmanumber, string rmadate, int rmastatusID, int companyID, string rmaGUIDs)
        {
            RMA objRMA = new RMA();
            DataSet dataSet = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RmaGUID", rmaGUID);
                objCompHash.Add("@rmanumber", rmanumber);
                objCompHash.Add("@rmadate", rmadate);
                objCompHash.Add("@rmastatusID", rmastatusID);
                objCompHash.Add("@companyid", companyID);
                objCompHash.Add("@rmaGUIDs", rmaGUIDs);
                objCompHash.Add("@UPC", "");

                arrSpFieldSeq = new string[] { "@RmaGUID", "@rmanumber", "@rmadate", "@rmastatusID", "@companyid", "@rmaGUIDs", "@UPC" };

                dataSet = db.GetDataSet(objCompHash, "av_rma_select", arrSpFieldSeq);

                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {

                        objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMA.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        objRMA.CustomerRMANumber = clsGeneral.getColumnData(dataRow, "CustomerRmaNumber", string.Empty, false) as string;

                        
                        objRMA.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue.ToShortDateString(), false));
                        objRMA.POGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "POGUID", 0, false));
                        objRMA.RmaStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaStatusID", 0, false));
                        objRMA.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));
                        objRMA.RmaContactName = clsGeneral.getColumnData(dataRow, "RmaContactName", string.Empty, false) as string;
                        objRMA.Comment = clsGeneral.getColumnData(dataRow, "Comment", string.Empty, false) as string;
                        objRMA.AVComments = clsGeneral.getColumnData(dataRow, "AVComments", string.Empty, false) as string;
                        objRMA.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                        objRMA.RMAUserCompany.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        objRMA.RMAUserCompany.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objRMA.RMAUserCompany.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                        objRMA.Address = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                        objRMA.City = clsGeneral.getColumnData(dataRow, "ContactCity", string.Empty, false) as string;
                        objRMA.State = clsGeneral.getColumnData(dataRow, "ContactState", string.Empty, false) as string;
                        objRMA.Zip = clsGeneral.getColumnData(dataRow, "ContactZip", string.Empty, false) as string;
                        objRMA.Phone = clsGeneral.getColumnData(dataRow, "ContactPhone", string.Empty, false) as string;
                        objRMA.Email = clsGeneral.getColumnData(dataRow, "ContactEmail", string.Empty, false) as string;
                        objRMA.LocationCode = clsGeneral.getColumnData(dataRow, "LocationCode", string.Empty, false) as string;
                        objRMA.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;

                        objRMA.DocComment = clsGeneral.getColumnData(dataRow, "DOCComment", string.Empty, false) as string;
                        objRMA.MaxShipmentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "MaxShipmentDate", DateTime.MinValue.ToShortDateString(), false));
                        objRMA.ContactCountry = clsGeneral.getColumnData(dataRow, "ContactCountry", string.Empty, false) as string;
                        objRMA.DoNotSendShippingLabel = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "DoNotSendShippingLabel", false, false));
                        objRMA.AllowShippingLabel = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowShippingLabel", false, false));



                        //if (dataSet.Tables[1].Rows.Count > 0)
                        //    objRMA.RmaDetails = GetRMADetail(dataSet.Tables[1]);
                        if (dataSet.Tables[1].Rows.Count > 0)
                            objRMA.RmaDetails = GetRMADetail(dataSet.Tables[1], dataSet.Tables[3]);


                        if (dataSet.Tables.Count > 2 && dataSet.Tables[2].Rows.Count > 0)
                            objRMA.RmaDocumentList = PopulateRmaDocuments(dataSet.Tables[2]);

                    }
                }

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

            return objRMA;
        }

        public static List<RMADetail> GetRMADetail(DataTable dataTable)
        {
            List<RMADetail> rmaDetail = new List<RMADetail>();
            try
            {


                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RMADetail objRMADETAIL = new RMADetail();
                        objRMADETAIL.rmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMADETAIL.rmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "rmaDetGUID", 0, false));
                        objRMADETAIL.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        //objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false) as string;
                        objRMADETAIL.Reason = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Reason", 0, false));
                        objRMADETAIL.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                        objRMADETAIL.CallTime = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CallTime", 0, false));
                        objRMADETAIL.Notes = clsGeneral.getColumnData(dataRow, "Notes", string.Empty, false) as string;
                        objRMADETAIL.WaferSealed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WaferSealed", 0, false));
                        objRMADETAIL.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                        objRMADETAIL.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                        //objRMADETAIL.Po_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "poid", 0, false));
                        //objRMADETAIL.Pod_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pod_id", 0, false));
                        //objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false) as string;
                        //objRMADETAIL.PurchaseOrderNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                        objRMADETAIL.ItemDescription = clsGeneral.getColumnData(dataRow, "ItemDescription", string.Empty, false) as string;

                        objRMADETAIL.ItemCode = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;
                        objRMADETAIL.Warranty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Warranty", 0, false));
                        objRMADETAIL.Disposition = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Disposition", 0, false));
                        objRMADETAIL.RepairEstId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RepairEstID", 0, false));
                        objRMADETAIL.TriageStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TriageStatusID", 0, false));
                        objRMADETAIL.TriageNotes = clsGeneral.getColumnData(dataRow, "TriageNotes", string.Empty, false) as string;
                        objRMADETAIL.ShippingTrackingNumber = clsGeneral.getColumnData(dataRow, "ShippingTrackingNumber", string.Empty, false) as string;
                        objRMADETAIL.NewSKU = clsGeneral.getColumnData(dataRow, "NewSKU", string.Empty, false) as string;
                        objRMADETAIL.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", DateTime.MinValue, false));

                        rmaDetail.Add(objRMADETAIL);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return rmaDetail;
        }
        public static List<RMADetail> GetRMADetail(DataTable dataTable, DataTable accessoryTable)
        {
            int accessoryID = 3;
            List<RMADetail> rmaDetail = new List<RMADetail>();
            try
            {

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {

                        RMADetail objRMADETAIL = new RMADetail();
                        objRMADETAIL.rmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMADETAIL.rmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "rmaDetGUID", 0, false));
                        objRMADETAIL.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        //objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false) as string;
                        objRMADETAIL.Reason = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Reason", 0, false));
                        objRMADETAIL.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                        objRMADETAIL.CallTime = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CallTime", 0, false));
                        objRMADETAIL.Notes = clsGeneral.getColumnData(dataRow, "Notes", string.Empty, false) as string;
                        objRMADETAIL.WaferSealed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WaferSealed", 0, false));
                        objRMADETAIL.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                        objRMADETAIL.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                        objRMADETAIL.Po_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "po_id", 0, false));
                        objRMADETAIL.Pod_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pod_id", 0, false));
                        //objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false) as string;
                        objRMADETAIL.PurchaseOrderNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                        objRMADETAIL.ItemDescription = clsGeneral.getColumnData(dataRow, "ItemDescription", string.Empty, false) as string;

                        objRMADETAIL.ItemCode = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;
                        objRMADETAIL.Warranty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Warranty", 0, false));
                        objRMADETAIL.Disposition = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Disposition", 0, false));
                        objRMADETAIL.RepairEstId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RepairEstID", 0, false));
                        objRMADETAIL.TriageStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TriageStatusID", 0, false));
                        objRMADETAIL.TriageNotes = clsGeneral.getColumnData(dataRow, "TriageNotes", string.Empty, false) as string;
                        objRMADETAIL.ShippingTrackingNumber = clsGeneral.getColumnData(dataRow, "ShippingTrackingNumber", string.Empty, false) as string;
                        objRMADETAIL.NewSKU = clsGeneral.getColumnData(dataRow, "NewSKU", string.Empty, false) as string;
                        objRMADETAIL.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", DateTime.MinValue, false));
                        objRMADETAIL.ReplacementSKUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));


                        List<RMAAccessory> rmaAccessoryList = new List<RMAAccessory>();

                        if (accessoryTable != null && accessoryTable.Rows.Count > 0)
                        {
                            RMAAccessory objRMAAccessory = null;
                            foreach (DataRow dataRowItem in accessoryTable.Select("RMADetGUID = " + objRMADETAIL.rmaDetGUID.ToString()))
                            {
                                accessoryID = 2;
                                objRMAAccessory = new RMAAccessory();
                                objRMAAccessory.RMADetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "RMADetGUID", 0, false));
                                objRMAAccessory.AccessoryNumber = clsGeneral.getColumnData(dataRowItem, "AccessoryNumber", string.Empty, false) as string;
                                objRMAAccessory.AccessoryDescription = clsGeneral.getColumnData(dataRowItem, "AccessoryDescription", string.Empty, false) as string;
                                objRMAAccessory.AccessoryID = Convert.ToInt32(clsGeneral.getColumnData(dataRowItem, "AccessoryID", 0, false));
                                objRMAAccessory.IsChecked = true;
                                rmaAccessoryList.Add(objRMAAccessory);
                            }
                        }
                        objRMADETAIL.AccessoryID = accessoryID;
                        objRMADETAIL.RMAAccessoryList = rmaAccessoryList;
                        rmaDetail.Add(objRMADETAIL);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return rmaDetail;
        }


        public static RMAAPIResponse CreateNewRMA(RMAAPIRequest rmaRequest, int userID)
        {
            RMAAPIResponse serviceResponse = new RMAAPIResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();
            //RMAResponse.RMANumber = RMARequest.RMA.RmaNumber;
            //DataTable rmaDT = new DataTable();
            //rmaDT = RMAUtility.update_RMA(RMARequest.RMA);
            string errorMessage = string.Empty;
            try
            {
                //serviceResponse = Update_RMA_New(rmaRequest.RMA, userID);
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorCode = ResponseErrorCode.InternalError.ToString();
                serviceResponse.Comment = ex.Message;
            }
            return serviceResponse;
        }

        #region function getRMADetail
        public static List<RMADetail> getRMADetail(int RMAGuid, int RMADetGUID, string ESN)
        {
            List<RMADetail> rmaDetail = new List<RMADetail>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@rmaguid", RMAGuid);
                objCompHash.Add("@rmaDetGUID", RMADetGUID);
                objCompHash.Add("@ESN", ESN);

                arrSpFieldSeq = new string[] { "@rmaguid", "@rmaDetGUID", "@ESN" };

                dataTable = db.GetTableRecords(objCompHash, "av_rma_detail_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RMADetail objRMADETAIL = new RMADetail();
                        objRMADETAIL.rmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMADETAIL.rmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "rmaDetGUID", 0, false));
                        objRMADETAIL.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "SalesOrderNumber", string.Empty, false) as string;
                        objRMADETAIL.Reason = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Reason", 0, false));
                        objRMADETAIL.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                        objRMADETAIL.CallTime = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CallTime", 0, false));
                        objRMADETAIL.Notes = clsGeneral.getColumnData(dataRow, "Notes", string.Empty, false) as string;
                        objRMADETAIL.WaferSealed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WaferSealed", 0, false));
                        objRMADETAIL.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                        objRMADETAIL.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                        objRMADETAIL.Po_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "poid", 0, false));
                        objRMADETAIL.Pod_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pod_id", 0, false));
                        objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "SalesOrderNumber", string.Empty, false) as string;
                        objRMADETAIL.PurchaseOrderNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;

                        objRMADETAIL.ItemCode = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;
                        objRMADETAIL.Warranty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Warranty", 0, false));
                        objRMADETAIL.Disposition = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Disposition", 0, false));

                        rmaDetail.Add(objRMADETAIL);
                    }
                }
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

            return rmaDetail;
        }


        public static Hashtable getReasonHashtable()
        {
            Hashtable reasonHashTable = new Hashtable();

            reasonHashTable.Add("1", "DOA");
            reasonHashTable.Add("2", "AudioIssues");
            reasonHashTable.Add("3", "ScreenIssues");
            reasonHashTable.Add("4", "PowerIssues");
            reasonHashTable.Add("5", "Other");
            reasonHashTable.Add("6", "MissingParts");
            reasonHashTable.Add("7", "ReturnToStock");
            reasonHashTable.Add("8", "BuyerRemorse");
            reasonHashTable.Add("9", "PhysicalAbuse");
            reasonHashTable.Add("10", "LiquidDamage");
            reasonHashTable.Add("11", "DropCalls");
            reasonHashTable.Add("12", "Software");

            return reasonHashTable;
        }

        #endregion begin getRMADetail

        # region Update RMA  to alter the DB entries --
        public static void updateRMA(int rmaGuid, string rmaNumber, DateTime rmaDate, int rmaStatusID, int modifiedBy)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@RmaGUID", rmaGuid);
                objCompHash.Add("@rmanumber", rmaNumber);
                objCompHash.Add("@rmadate", rmaDate);
                objCompHash.Add("@RmaStatusID", rmaStatusID);
                objCompHash.Add("@ModifiedBy", modifiedBy);

                arrSpFieldSeq = new string[] { "@RmaGUID", "@rmanumber", "@rmadate", "@RmaStatusID", "@ModifiedBy" };
                db.ExeCommand(objCompHash, "av_rma_insert_update", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        #endregion


        private static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }
        public static string SerializeObject<T>(T obj)
        {
            //try
            //{
            //    string xmlString = null;
            //    MemoryStream memoryStream = new MemoryStream();
            //    XmlSerializer xs = new XmlSerializer(typeof(T));
            //    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            //    xs.Serialize(xmlTextWriter, obj);
            //    memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            //    xmlString = UTF8ByteArrayToString(memoryStream.ToArray()); return xmlString.Trim();
            //}
            //catch (Exception ex)
            //{
            //    return string.Empty;
            //}

            StringWriter xmlstringVal = null;
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            try
            {
                xmlstringVal = new StringWriter();
                string xmlString = null;
                ///xmlSettings.NamespaceHandling = NamespaceHandling.OmitDuplicates;
                xmlSettings.OmitXmlDeclaration = true;
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlWriter xmlWriter = XmlWriter.Create(xmlstringVal, xmlSettings);
                xs.Serialize(xmlWriter, obj, ns);
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
            finally
            {
                xmlSettings = null;
                xmlstringVal.Dispose();
            }

            return xmlstringVal.ToString().Trim();
        }

        # region Update RMA & DETAIL  to alter the DB entries --
        private static bool isEmail(string inputEmail)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                  @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                  @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(inputEmail))
                return (true);
            else
                return (false);
        }
        private static bool ValidateRequiredFields(RMANew rmaInfo, out string returnMessage)
        {
            returnMessage = string.Empty;
            bool isValidate = false;


            //if (string.IsNullOrEmpty(rmaInfo.RmaNumber))
            //    returnMessage = "RMANumber can not be empty";


            if (string.IsNullOrEmpty(rmaInfo.Address))
                returnMessage = "Address can not be empty";
            if (string.IsNullOrEmpty(rmaInfo.City))
                returnMessage = "City can not be empty";
            if (string.IsNullOrEmpty(rmaInfo.Email))
                returnMessage = "Email can not be empty";
            else
            {
                if (!isEmail(rmaInfo.Email))
                    returnMessage = "Not a valid email";
                if (rmaInfo.Email.ToLower().IndexOf("langlobal.com") > -1)
                    returnMessage = "Not a valid email";

            }
            if (string.IsNullOrEmpty(rmaInfo.RmaContactName))
                returnMessage = "Contact Name can not be empty";
            if (string.IsNullOrEmpty(rmaInfo.RmaDate.ToString()))
                returnMessage = "RMA date can not be empty";
            if (string.IsNullOrEmpty(rmaInfo.State))
                returnMessage = "State can not be empty";
            if (string.IsNullOrEmpty(rmaInfo.Zip))
                returnMessage = "Zip can not be empty";

            if (string.IsNullOrEmpty(returnMessage))
                isValidate = true;

            return isValidate;
        }
        public static RMAAPIResponse CreateNewRMA(RMAAPIRequest rmaRequest, bool API, out int userID)
        {
            RMAAPIResponse serviceResponse = new RMAAPIResponse();
            string esn = string.Empty;
            userID = 0;
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            string requestXML = clsGeneral.SerializeObject(rmaRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "SetRMA";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            try
            {
                if (rmaRequest != null)
                {

                    userID = 0;
                    int companyID = 0;
                    if (PurchaseOrder.AuthenticateRequestNew(rmaRequest.Authentication, out userID, out companyID))
                    {

                        request.UserID = userID;
                        request.CompanyID = companyID;
                        serviceResponse = Update_RMA_New(rmaRequest.RMA, userID, companyID, API);
                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                        //RMAResponse.RMANumber = RMARequest.RMA.RmaNumber;
                    }
                    request.UserID = userID;

                    request.CompanyID = companyID;
                    request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                    request.ResponseTimeStamp = DateTime.Now;
                    request.ExceptionOccured = false;
                    request.ReturnMessage = string.IsNullOrWhiteSpace(serviceResponse.Comment) ? serviceResponse.ErrorCode : serviceResponse.Comment;

                }
            }
            catch (Exception exc)
            {
                ex = exc;
                //serviceResponse.RMACount = 0;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();

            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }
            return serviceResponse;
        }
        private static void ValidateESN(ref RMADetail avEsn, int companyID, int rmaGUID, int userID)
        {
            //esn lookup
            if (!string.IsNullOrEmpty(avEsn.ESN))
            {

                List<avii.Classes.RMADetail> esnlist = RMAUtility.getRMAesn(companyID, avEsn.ESN, "", 0, rmaGUID, userID);

                if (esnlist.Count > 0)
                {
                    if (esnlist.Count == 1)
                    {
                        avEsn.ItemCode = esnlist[0].ItemCode;
                        avEsn.UPC = esnlist[0].UPC;
                        avEsn.rmaDetGUID = esnlist[0].rmaDetGUID;
                        avEsn.AVSalesOrderNumber = esnlist[0].AVSalesOrderNumber;
                        avEsn.PurchaseOrderNumber = esnlist[0].PurchaseOrderNumber;
                        avEsn.AllowRMA = esnlist[0].AllowRMA;
                        avEsn.AllowDuplicate = esnlist[0].AllowDuplicate;
                    }
                }
            }
        }
        public static RMAAPIResponse Update_RMA_New(RMANew rmaInfo, int userID, int companyID, bool API)
        {
            int statusID = 1;
            int reasonID = 0;
            string unassignedESN = "Unassigned ESN";
            int maxEsn = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["maxEsn"]);
            string externalESN = "Not Found";
            string successMessage = "RMA is successfully added with <u><b>RMA# {0}</b></u>. Please do not send your returns until RMA is APPROVED by <b>LAN GLOBAL Returns Department</b>.";
            string dateMsg = "Invalid RMA Date! Can not create RMA before 7 days back.";
            DateTime currentDate = DateTime.Now;
            DateTime rmaDate = new DateTime();

            string returnMessage = string.Empty;
            //string outParam = string.Empty;

            RMAAPIResponse rmaResponse = new Classes.RMAAPIResponse();
            rmaResponse.ErrorCode = string.Empty;


            rmaDate = Convert.ToDateTime(rmaInfo.RmaDate);
            TimeSpan diffResult = currentDate - rmaDate;
            if (diffResult.Days > 7)
            {
                rmaResponse.ErrorCode = dateMsg;
            }
            if (ValidateRequiredFields(rmaInfo, out returnMessage))
            {

                List<RMADetail> rmaDetailList = new List<RMADetail>();

                try
                {
                    foreach (RMADetails rmaDetail in rmaInfo.RmaDetails)
                    {
                        RMADetail rmaDetailObj = new RMADetail();
                        rmaDetailObj.ESN = rmaDetail.ESN;
                        if (rmaDetailObj.ESN.Length < 8 || rmaDetailObj.ESN.Length > 20)
                        {
                            rmaResponse.ErrorCode = "Esn Should be between 15 to 20 digits!";
                            break;
                        }
                        else
                        {
                            var EsnList = (from item in rmaInfo.RmaDetails where item.ESN.Equals(rmaDetail.ESN) select item).ToList();
                            if (EsnList.Count > 1)
                            {
                                rmaResponse.ErrorCode = string.Format("Duplicate ESN ({0}) is found", rmaDetail.ESN);
                                break;
                            }
                            if (0 == (int)rmaDetail.Warranty && 0 == (int)rmaDetail.Disposition)
                            {
                                rmaResponse.ErrorCode = " Type is required <br /> Disposition is required";
                                break;
                            }
                            else
                            {
                                if (0 == (int)rmaDetail.Warranty)
                                {
                                    rmaResponse.ErrorCode = "Type is required";
                                    break;
                                }
                                //if (0 == (int)rmaDetail.Disposition)
                                //{
                                //    rmaResponse.ErrorCode = "Disposition is required";
                                //    break;
                                //}
                            }

                            ValidateESN(ref rmaDetailObj, companyID, 0, userID);
                            if (!rmaDetailObj.AllowDuplicate)
                            {
                                rmaResponse.ErrorCode = string.Format("RMA ({0}) already exists", rmaDetail.ESN);
                                break;

                            }

                            if (externalESN == rmaDetailObj.ItemCode)
                            {
                                //rmaResponse.ErrorCode = "RMA is not allowed for External Esn";
                                if (string.IsNullOrEmpty(rmaDetail.Notes))
                                {
                                    rmaResponse.ErrorCode = string.Format("This ESN/MEID({0}) cannot be found in our system.  You can still request an RMA for this item, but you will have to provide the Invoice Number and Date on the NOTES field", rmaDetail.ESN);
                                    break;
                                }
                            }
                            if (unassignedESN == rmaDetailObj.ItemCode)
                            {
                                rmaResponse.ErrorCode = "RMA is not allowed for unassigned ESN";
                                break;
                            }

                            if (!rmaDetailObj.AllowRMA)
                            {

                                rmaResponse.ErrorCode = string.Format("RMA is not allowed for this item({1}) related to ESN({0}).", rmaDetailObj.ESN, rmaDetailObj.UPC);
                                break;
                            }
                            if ((rmaDetail.WarrantyExpieryDate - DateTime.Now).Days < 0 && (int)rmaDetail.Warranty == 1 && string.IsNullOrEmpty(rmaDetail.Notes) && (int)rmaDetail.Disposition > 0)
                            {
                                //if (string.IsNullOrEmpty(rmaDetail.Notes))
                                {
                                    rmaResponse.ErrorCode = string.Format("This ESN/MEID({0}) has an expired warranty.  Please provide additional information on the NOTES field", rmaDetail.ESN);
                                }
                            }

                            rmaDetailObj.CallTime = rmaDetail.CallTime;
                            rmaDetailObj.Disposition = 0;//(int)rmaDetail.Disposition;
                            rmaDetailObj.Notes = rmaDetail.Notes;
                            reasonID = (int)rmaDetail.Reason;
                            rmaDetailObj.Reason = reasonID;
                            rmaDetailObj.StatusID = statusID;
                            rmaDetailObj.rmaDetGUID = 0;
                            rmaDetailObj.Warranty = (int)rmaDetail.Warranty;
                            rmaDetailObj.WarrantyExpirationDate = (rmaDetail.WarrantyExpieryDate.ToString().IndexOf("01-01-0001") > -1 ? Convert.ToDateTime("01/01/1901") : rmaDetail.WarrantyExpieryDate.ToString().IndexOf("01/01/0001") > -1 ? Convert.ToDateTime("01/01/1901") : rmaDetail.WarrantyExpieryDate.ToString().IndexOf("1/1/0001") > -1 ? Convert.ToDateTime("01/01/1901") : rmaDetail.WarrantyExpieryDate.ToString() != "" ? rmaDetail.WarrantyExpieryDate : Convert.ToDateTime("01/01/1901"));//(rmaDetail.WarrantyExpieryDate.ToString() != "" ? rmaDetail.WarrantyExpieryDate : Convert.ToDateTime("01/01/1901")); //DateTime.Now;
                            rmaDetailList.Add(rmaDetailObj);
                        }
                    }
                    if (rmaDetailList != null && rmaDetailList.Count > maxEsn)
                        rmaResponse.ErrorCode = string.Format("Maximum of {0} ESNs are allowed per RMA request", maxEsn);

                    if (rmaDetailList != null && rmaDetailList.Count > 0 && rmaResponse.ErrorCode == string.Empty)
                    {

                        if (rmaResponse.ErrorCode == string.Empty)
                        {
                            List<RmaDocuments> rmaDocList = new List<RmaDocuments>();
                            bool generateRMA = false;
                            string rmaNumber = GenerateRMA();
                            RMA rmaObj = new RMA();
                            if (string.IsNullOrEmpty(rmaInfo.RmaNumber))
                            {
                                rmaObj.RmaNumber = rmaNumber;
                                generateRMA = true;
                            }
                            else
                                rmaObj.RmaNumber = rmaInfo.RmaNumber;
                            rmaObj.CustomerRMANumber = rmaInfo.CustomerRmaNumber ?? string.Empty;
                            rmaObj.Address = rmaInfo.Address;
                            rmaObj.AVComments = rmaInfo.AVComments;
                            rmaObj.City = rmaInfo.City;
                            rmaObj.Comment = rmaInfo.Comment;
                            rmaObj.CreatedBy = userID;
                            rmaObj.LocationCode = string.Empty;
                            rmaObj.ModifiedBy = 0;
                            rmaObj.Phone = rmaInfo.Phone;
                            rmaObj.Email = rmaInfo.Email;
                            rmaObj.RmaContactName = rmaInfo.RmaContactName;
                            rmaObj.RmaDate = rmaInfo.RmaDate;
                            rmaObj.State = rmaInfo.State;
                            rmaObj.StoreID = rmaInfo.StoreID;
                            rmaObj.Zip = rmaInfo.Zip;
                            rmaObj.UserID = userID;
                            rmaObj.RmaStatusID = 1;
                            rmaObj.RmaGUID = 0;
                            rmaObj.RmaDetails = rmaDetailList;
                            rmaObj.RmaDocumentList = rmaDocList;


                            string rmaNumberAPI = string.Empty, custRmaNumber = string.Empty;

                            DataTable rmaDT = RMAUtility.update_RMA_API(rmaObj, true, API, generateRMA);

                            if (rmaDT != null && rmaDT.Rows.Count > 0)
                            {
                                rmaNumber = rmaDT.Rows[0]["rmanumber"] as string;
                                rmaNumberAPI = rmaDT.Rows[0]["RMANumberExists"] as string;
                                custRmaNumber = rmaDT.Rows[0]["CustomerRMANumberExists"] as string;


                                if (!string.IsNullOrEmpty(rmaNumberAPI) && API)
                                {
                                    rmaResponse.ErrorCode = "RMANumberAlreadyExists";
                                    rmaResponse.Comment = "The RMA # " + rmaNumberAPI + " you entered already exists  in our system. Please change the RMA # and re-submit the RMA!";
                                    rmaResponse.RMANumber = rmaNumberAPI;
                                }
                                if (!string.IsNullOrWhiteSpace(rmaObj.CustomerRMANumber) && !string.IsNullOrEmpty(custRmaNumber) && API)
                                {
                                    rmaResponse.ErrorCode = "CustomerRMANumberAlreadyExists";
                                    rmaResponse.Comment = "The Customer RMA # " + rmaNumberAPI + " you entered already exists  in our system. Please change the Customer RMA # and re-submit the RMA!";
                                    rmaResponse.RMANumber = custRmaNumber;
                                }
                                else
                                {
                                    rmaResponse.ErrorCode = string.Empty;
                                    rmaResponse.Comment = string.Format(successMessage, rmaNumber);//ResponseErrorCode.UploadedSuccessfully.ToString();
                                    rmaResponse.RMANumber = rmaNumber;
                                    rmaResponse.RMADate = rmaInfo.RmaDate.ToShortDateString();
                                }


                            }
                        }
                    }
                    else
                    {
                        if (rmaResponse.ErrorCode == string.Empty)
                            rmaResponse.ErrorCode = "RMA does not have ESN assigned."; ;
                    }
                }
                catch (Exception objExp)
                {
                    rmaResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
                    rmaResponse.Comment = objExp.Message;
                    throw objExp;
                }
            }
            else
                rmaResponse.ErrorCode = returnMessage;

            //rmaResponse.RMANumber = outParam;

            return rmaResponse;
        }
        private static string GenerateRMA()
        {
            string rmaNumber = string.Empty;
            string month = string.Empty;
            string day = string.Empty;
            string st = System.Configuration.ConfigurationManager.AppSettings["av_rma"].ToString();
            DateTime dt = new DateTime();

            dt = DateTime.Now;
            try
            {
                // rmaGUID = RMAUtility.getRMAguid(0);

                month = dt.Month.ToString();
                day = dt.Day.ToString();
                if (month.Length < 2)
                    month = "0" + month;
                if (day.Length < 2)
                    day = "0" + day;
                rmaNumber = st + dt.Year.ToString() + month + day; //+ "-" + rmaGUID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return rmaNumber;

        }
        public static DataTable update_RMA_API(RMA rmaInfo, bool docPrint, bool API, bool generateRMA)
        {
            string ESNXml = SerializeObject(rmaInfo.RmaDetails);
            string rmaDocXML = SerializeObject(rmaInfo.RmaDocumentList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@RmaGUID", rmaInfo.RmaGUID);
                objCompHash.Add("@rmadate", rmaInfo.RmaDate);
                objCompHash.Add("@RmaStatusID", rmaInfo.RmaStatusID);
                objCompHash.Add("@ponum", rmaInfo.PoNum);
                objCompHash.Add("@rmaCustomername", rmaInfo.RmaContactName);
                objCompHash.Add("@ContactAddress", rmaInfo.Address);
                objCompHash.Add("@ContactState", rmaInfo.State);
                objCompHash.Add("@ContactCity", rmaInfo.City);
                objCompHash.Add("@ContactZip", rmaInfo.Zip);
                objCompHash.Add("@ContactPhone", rmaInfo.Phone);
                objCompHash.Add("@ContactEmail", rmaInfo.Email);
                objCompHash.Add("@Comment", rmaInfo.Comment);
                objCompHash.Add("@AVComments", rmaInfo.AVComments);
                objCompHash.Add("@rmaxml", ESNXml);
                objCompHash.Add("@UserID", rmaInfo.UserID);
                objCompHash.Add("@CreatedBy", rmaInfo.CreatedBy);
                objCompHash.Add("@ModifiedBy", rmaInfo.ModifiedBy);
                objCompHash.Add("@LocationCode", rmaInfo.LocationCode);
                objCompHash.Add("@StoreID", rmaInfo.StoreID);
                objCompHash.Add("@RmaDocXml", rmaDocXML);
                objCompHash.Add("@DocRePrint", docPrint);
                objCompHash.Add("@MaxShipmentDate", rmaInfo.MaxShipmentDate.ToShortDateString() == "01-01-0001" ? DateTime.Now.AddDays(10): rmaInfo.MaxShipmentDate.ToShortDateString() == "1/1/0001" ? DateTime.Now.AddDays(10) : rmaInfo.MaxShipmentDate);
                objCompHash.Add("@IsAPI", API);
                objCompHash.Add("@RMANUMBERAPI", generateRMA == true ? string.Empty : rmaInfo.RmaNumber);
                objCompHash.Add("@CustomerRMANumber", rmaInfo.CustomerRMANumber);

                objCompHash.Add("@rmanumber", rmaInfo.RmaNumber);

                arrSpFieldSeq = new string[] { "@RmaGUID", "@rmadate", "@RmaStatusID", "@ponum"
                    , "@rmaCustomername", "@ContactAddress", "@ContactState", "@ContactCity", "@ContactZip"
                    , "@ContactPhone", "@ContactEmail", "@Comment","@AVComments", "@rmaxml", "@UserID", "@CreatedBy", "@ModifiedBy", "@LocationCode",
                    "@StoreID", "@RmaDocXml","@DocRePrint","@MaxShipmentDate", "@IsAPI","@RMANUMBERAPI", "@CustomerRMANumber", "@rmanumber" };


                dataTable = db.GetTableRecords(objCompHash, "av_rma_insert_update", arrSpFieldSeq);

            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;

            }
            return dataTable;
        }

        public static DataTable update_RMA(RMA rmaInfo, bool docPrint)
        {
            string ESNXml = SerializeObject(rmaInfo.RmaDetails);
            string rmaDocXML = SerializeObject(rmaInfo.RmaDocumentList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@RmaGUID", rmaInfo.RmaGUID);
                objCompHash.Add("@rmadate", rmaInfo.RmaDate);
                objCompHash.Add("@RmaStatusID", rmaInfo.RmaStatusID);
                objCompHash.Add("@ponum", rmaInfo.PoNum);
                objCompHash.Add("@rmaCustomername", rmaInfo.RmaContactName);
                objCompHash.Add("@ContactAddress", rmaInfo.Address);
                objCompHash.Add("@ContactState", rmaInfo.State);
                objCompHash.Add("@ContactCity", rmaInfo.City);
                objCompHash.Add("@ContactZip", rmaInfo.Zip);
                objCompHash.Add("@ContactPhone", rmaInfo.Phone);
                objCompHash.Add("@ContactEmail", rmaInfo.Email);
                objCompHash.Add("@Comment", rmaInfo.Comment);
                objCompHash.Add("@AVComments", rmaInfo.AVComments);
                objCompHash.Add("@rmaxml", ESNXml);
                objCompHash.Add("@UserID", rmaInfo.UserID);
                objCompHash.Add("@CreatedBy", rmaInfo.CreatedBy);
                objCompHash.Add("@ModifiedBy", rmaInfo.ModifiedBy);
                objCompHash.Add("@LocationCode", rmaInfo.LocationCode);
                objCompHash.Add("@StoreID", rmaInfo.StoreID);
                objCompHash.Add("@RmaDocXml", rmaDocXML);
                objCompHash.Add("@DocRePrint", docPrint);
                objCompHash.Add("@MaxShipmentDate", rmaInfo.MaxShipmentDate.ToShortDateString() == "1/1/0001" ? DateTime.Now.AddDays(10) : rmaInfo.MaxShipmentDate);
                objCompHash.Add("@ContactCountry", rmaInfo.ContactCountry);
                objCompHash.Add("@DoNotSendShippingLabel", rmaInfo.DoNotSendShippingLabel);
                objCompHash.Add("@AllowShippingLabel", rmaInfo.AllowShippingLabel);

                objCompHash.Add("@CustomerRMANumber", rmaInfo.CustomerRMANumber);
                objCompHash.Add("@RMASource", rmaInfo.RMASource);
                objCompHash.Add("@rmanumber", rmaInfo.RmaNumber);
                
                

                arrSpFieldSeq = new string[] { "@RmaGUID", "@rmadate", "@RmaStatusID", "@ponum"
                    , "@rmaCustomername", "@ContactAddress", "@ContactState", "@ContactCity", "@ContactZip"
                    , "@ContactPhone", "@ContactEmail", "@Comment","@AVComments", "@rmaxml", "@UserID", "@CreatedBy", 
                    "@ModifiedBy", "@LocationCode", "@StoreID", "@RmaDocXml","@DocRePrint","@MaxShipmentDate","@ContactCountry",
                    "@DoNotSendShippingLabel","@CustomerRMANumber", "@RMASource", "@rmanumber" };


                dataTable = db.GetTableRecords(objCompHash, "av_rma_insert_update", arrSpFieldSeq);

            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;

            }
            return dataTable;
        }

        public static void UpdateRMAChangeStatus(List<avii.Classes.RMAChangeStatus> rmaList, int statusID, int userID, out int poRecordCount)
        {
            string rmaXml = SerializeObject(rmaList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            poRecordCount = 0;
            try
            {
                objCompHash.Add("@RmaXML", rmaXml);
                objCompHash.Add("@StatusID", statusID);
                objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@RmaXML", "@StatusID", "@UserID" };
                db.ExeCommand(objCompHash, "AV_RMA_ChangeStaus", arrSpFieldSeq, "@poRecordCount", out poRecordCount);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        #endregion
        # region Update Aero_ESNService returncode to zero(0) when RAM status changed to Approved - alter the DB entries --
        public void update_ESNService_returncode(int RmaGUID, string ESN, string itemCode)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;

            Hashtable objCompHash = new Hashtable();

            try
            {


                objCompHash.Add("@RmaGUID", RmaGUID);
                objCompHash.Add("@ESN", ESN);
                objCompHash.Add("@itemcode", itemCode);


                arrSpFieldSeq = new string[] { "@RmaGUID", "@ESN", "@itemcode" };
                db.ExeCommand(objCompHash, "av_ESNService_Update", arrSpFieldSeq);






            }
            catch (Exception objExp)
            {
                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;

            }

        }
        #endregion
        # region Update RMA DETAIL  to alter the DB entries --
        public static void update_RMA_DETAIL(int rmaDetGUID, int RmaGUID, string esn, string Reason, int CallTime, int StatusID, string notes, int poid, int podid)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@rmaDetGUID", rmaDetGUID);
                objCompHash.Add("@RmaGUID", RmaGUID);
                objCompHash.Add("@ESN", esn);
                objCompHash.Add("@Reason", Reason);
                objCompHash.Add("@CallTime", CallTime);
                objCompHash.Add("@StatusID", StatusID);
                objCompHash.Add("@Notes", notes);
                objCompHash.Add("@poid", poid);
                objCompHash.Add("@podid", podid);


                arrSpFieldSeq = new string[] { "@rmaDetGUID", "@RmaGUID", "@ESN", "@Reason", "@CallTime", "@StatusID", "@Notes", "@poid", "@podid" };
                db.ExeCommand(objCompHash, "av_rma_detail_insert_update", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        #endregion
        # region Update RMA BatchUpdate   to alter the DB entries --
        public static void update_RMA_batchupdate(List<avii.Classes.RMA_Status> rmaStatusList)
        {
            string rmaXml = SerializeObject(rmaStatusList);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@rmaxml", rmaXml);


                arrSpFieldSeq = new string[] { "@rmaxml" };
                db.ExeCommand(objCompHash, "av_rmastatus_batchupdate", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        #endregion
        # region Delete RMA & RMA DETAIL  to delete the DB entries --
        public static void delete_RMA_RMADETAIL(int RmaGUID, int rmaDetGUID, int userID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {

                objCompHash.Add("@rmaGUID", RmaGUID);
                objCompHash.Add("@rmaDetGUID", rmaDetGUID);
                objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@rmaGUID", "@rmaDetGUID", "@UserID" };
                db.ExeCommand(objCompHash, "av_rma_delete", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        public static void Delete_RMA_receive(int rmaDetGUID, int userID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {


                objCompHash.Add("@rmaDetGUID", rmaDetGUID);
                objCompHash.Add("@CreatedBy", userID);


                arrSpFieldSeq = new string[] { "@rmaDetGUID", "@CreatedBy" };
                db.ExeCommand(objCompHash, "av_RMA_Receive_Delete", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        #endregion


        #region function Generate RMADetail from PO
        public List<RMADetail> getRMADetailFromPO(string ponumber)
        {
            List<RMADetail> rmaDetail = new List<RMADetail>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@Po_Num", ponumber);

                arrSpFieldSeq = new string[] { "@Po_Num" };

                dataTable = db.GetTableRecords(objCompHash, "Av_PurchaseOrder_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RMADetail objRMADETAIL = new RMADetail();
                        objRMADETAIL.rmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMADETAIL.rmaDetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "rmaDetGUID", 0, false));
                        objRMADETAIL.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        objRMADETAIL.Reason = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Reason", 0, false));
                        objRMADETAIL.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                        objRMADETAIL.CallTime = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CallTime", 0, false));
                        objRMADETAIL.Notes = clsGeneral.getColumnData(dataRow, "Notes", string.Empty, false) as string;
                        objRMADETAIL.WaferSealed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WaferSealed", 0, false));
                        objRMADETAIL.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                        objRMADETAIL.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;

                        rmaDetail.Add(objRMADETAIL);
                    }
                }
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

            return rmaDetail;
        }

        #endregion
        public static List<RMADetail> getRMAesn(int companyID, string esn, string po_num, int webservicecall, int rmaGUID, int userID)
        {
            List<RMADetail> rmaEsnLookup = new List<RMADetail>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                
                objCompHash.Add("@ESN", esn);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@companyID", companyID);
                objCompHash.Add("@po_num", po_num);
                objCompHash.Add("@webservicecall", webservicecall);
                objCompHash.Add("@RMAGUID", rmaGUID);

                arrSpFieldSeq = new string[] {  "@ESN", "@UserID", "@companyID", "@po_num", "@webservicecall", "@RMAGUID" };

                    dataTable = db.GetTableRecords(objCompHash, "av_rma_esn_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RMADetail objRMAesn = new RMADetail();

                        objRMAesn.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        objRMAesn.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AVSO", string.Empty, false) as string;
                        objRMAesn.PurchaseOrderNumber = clsGeneral.getColumnData(dataRow, "po_num", string.Empty, false) as string;
                        objRMAesn.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                        objRMAesn.ItemCode = clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false) as string;
                        objRMAesn.rmaDetGUID = 0;
                        objRMAesn.Reason = 0;
                        objRMAesn.StatusID = 1;
                        objRMAesn.CallTime = 0;
                        objRMAesn.Notes = "";
                        objRMAesn.AllowRMA = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowRMA", false, false));
                        objRMAesn.PoStatusId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "statusid", 0, false));
                        objRMAesn.AllowDuplicate = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowDuplicate", false, false));
                        objRMAesn.WarrantyExpirationDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "WARRANTYEXPIRATIONDATE", DateTime.Now, false));

                        rmaEsnLookup.Add(objRMAesn);
                    }
                }
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

            return rmaEsnLookup;
        }
        public static string getRMAguid(int rmaGUID)
        {
            string rma_guid = string.Empty;
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {

                objCompHash.Add("@rmaGUID", rmaGUID);

                arrSpFieldSeq = new string[] { "@rmaGUID" };

                dataTable = db.GetTableRecords(objCompHash, "av_rma_guid", arrSpFieldSeq);
                if (dataTable.Rows.Count > 0)
                    rma_guid = dataTable.Rows[0]["rmaguid"].ToString();

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

            return rma_guid;
        }
        public static RMAUserCompany getUserEmail(int companyID)
        {
            string email = string.Empty;
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            RMAUserCompany objUserCompany = new RMAUserCompany();
            try
            {

                objCompHash.Add("@companyid", companyID);

                arrSpFieldSeq = new string[] { "@companyid" };

                dataTable = db.GetTableRecords(objCompHash, "av_userEmail_select", arrSpFieldSeq);
                if (dataTable.Rows.Count > 0)
                {
                    objUserCompany.Email = dataTable.Rows[0]["email"].ToString();
                    objUserCompany.UserID = Convert.ToInt32(dataTable.Rows[0]["userid"]);
                }


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

            return objUserCompany;
        }
        public static List<RMAUserCompany> getRMAUserCompany(int companyID, string email, int allcompay, int userid)
        {
            List<RMAUserCompany> rmaUserCompany = new List<RMAUserCompany>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@companyID", companyID);
                objCompHash.Add("@email", email);
                objCompHash.Add("@allcompay", allcompay);
                objCompHash.Add("@userid", userid);


                arrSpFieldSeq = new string[] { "@companyID", "@email", "@allcompay", "@userid" };

                dataTable = db.GetTableRecords(objCompHash, "av_usercompany_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RMAUserCompany objRMAcompany = new RMAUserCompany();

                        objRMAcompany.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        objRMAcompany.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objRMAcompany.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;

                        rmaUserCompany.Add(objRMAcompany);
                    }
                }
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

            return rmaUserCompany;
        }
        public static RMAUserCompany getRMAUserCompanyInfo(int companyID, string email, int allcompay, int userid)
        {
            RMAUserCompany objRMAcompany = new RMAUserCompany();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@companyID", companyID);
                objCompHash.Add("@email", email);
                objCompHash.Add("@allcompay", allcompay);
                objCompHash.Add("@userid", userid);


                arrSpFieldSeq = new string[] { "@companyID", "@email", "@allcompay", "@userid" };

                dataTable = db.GetTableRecords(objCompHash, "av_usercompany_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        objRMAcompany.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        objRMAcompany.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "userID", 0, false));
                        objRMAcompany.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objRMAcompany.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                    }
                }
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

            return objRMAcompany;
        }
        public static DataTable getimportRMAList(string filepath)
        {

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@filepath", filepath);

                arrSpFieldSeq = new string[] { "@filepath" };

                dataTable = db.GetTableRecords(objCompHash, "av_rma_import", arrSpFieldSeq);



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

            return dataTable;
        }

        // Called by webmethod - Insert rma log
        public static void RMALogInsert(RMAAPIRequest RMARequest, RMAAPIResponse RMAResponse, int userID)
        {
            try
            {
                string requestXML = clsGeneral.SerializeObject(RMARequest);
                string responseXML = clsGeneral.SerializeObject(RMAResponse);
                RMALogInsertDB(RMARequest.RMA.RmaNumber, userID, requestXML, responseXML);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static CancelRMAResponse CancelRMA(RMARequests rmaRequest)
        {
            CancelRMAResponse serviceResponse = new CancelRMAResponse();
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            string requestXML = clsGeneral.SerializeObject(rmaRequest);

            LogModel request = new LogModel();
            request.RequestData = requestXML;
            request.ModuleName = "CancelRMA";
            request.RequestTimeStamp = DateTime.Now;
            Exception ex = null;

            try
            {

                if (rmaRequest != null)
                {

                    int userId = PurchaseOrder.AuthenticateRequest(rmaRequest.Authentication, out ex);
                    if (ex != null)
                    {
                        serviceResponse.Comment = ex.Message;
                        serviceResponse.ErrorCode = ResponseErrorCode.ErrowWhileLoadingData.ToString();

                        request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                        request.ResponseTimeStamp = DateTime.Now;
                        request.ExceptionOccured = true;
                        request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();
                        //LogOperations.ApiLogInsert(request);
                    }
                    if (userId > 0)
                    {
                        serviceResponse = CancelRMA(rmaRequest.RMANumber, userId);
                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                    request.UserID = userId;
                    request.CompanyID= 0;

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
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
                request.ResponseData = clsGeneral.SerializeObject(serviceResponse);
                request.ResponseTimeStamp = DateTime.Now;
                request.ExceptionOccured = true;
                request.ReturnMessage = ResponseErrorCode.ErrowWhileLoadingData.ToString();

            }
            finally
            {
                LogOperations.ApiLogInsert(request);
            }
            return serviceResponse;
        }
        public static CancelRMAResponse CancelRMA(string rmaNumber, int userID)
        {
            CancelRMAResponse rmaResponse = new CancelRMAResponse();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();
            string outParam = string.Empty;
            int statusID = 0;
            try
            {
                objCompHash.Add("@rmanumber", rmaNumber);
                objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@rmanumber", "@UserID" };


                statusID = db.ExCommand(objCompHash, "av_rma_Cancel", arrSpFieldSeq, "@outparam", out outParam);

                if (outParam == rmaNumber)
                {
                    rmaResponse.ErrorCode = string.Empty;
                    rmaResponse.Comment = ResponseErrorCode.CancelledSuccessfully.ToString();
                }
                else if (outParam == string.Empty && statusID == 0)
                {
                    rmaResponse.ErrorCode = ResponseErrorCode.RMANotExists.ToString();
                    rmaResponse.Comment = ResponseErrorCode.RMANotExists.ToString();
                }
                else if (outParam == string.Empty && statusID > 1)
                {
                    rmaResponse.ErrorCode = ResponseErrorCode.RMACannotBeCancelled.ToString();
                    rmaResponse.Comment = "RMA should be in pending state for cancellation";
                }

            }

            catch (Exception objExp)
            {
                //serviceResponse.Comment = ex.Message;
                rmaResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
                rmaResponse.Comment = objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;

            }
            return rmaResponse;
        }

        private static void RMALogInsertDB(string RMANumber, int userID, string requestXml, string responseXml)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RMANumber", RMANumber);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@RequestXml", requestXml);
                objCompHash.Add("@ResponseXml", responseXml);

                arrSpFieldSeq = new string[] { "@RMANumber", "@UserID", "@RequestXml", "@ResponseXml" };
                db.ExeCommand(objCompHash, "sv_RMALog_Insert", arrSpFieldSeq);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }


        }
        public static RMAResponse SetRMA(RMARequest rmaRequest)
        {
            RMAResponse serviceResponse = new RMAResponse();
            string esn = string.Empty;
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            Exception exc = null;
            try
            {

                if (rmaRequest != null)
                {

                    int userId = PurchaseOrder.AuthenticateRequest(rmaRequest.Authentication, out exc);
                    if (userId > 0)
                    {
                        //serviceResponse = Update_RMA_New(rmaRequest.RmaInformation, userId);
                    }
                }
            }
            catch (Exception ex)
            {
                //serviceResponse.RMACount = 0;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
            }
            return serviceResponse;
        }
        public static RmaResponses GetRMA(RMARequests rmaRequest)
        {
            RmaResponses serviceResponse = new RmaResponses();
            serviceResponse.ErrorCode = ResponseErrorCode.MissingParameter.ToString();
            Exception exc = null;
            try
            {

                if (rmaRequest != null)
                {

                    int userId = PurchaseOrder.AuthenticateRequest(rmaRequest.Authentication, out exc);
                    if (userId > 0)
                    {
                        RMAInfo rmaInfo = new RMAInfo(); //GetNewRMADeail(rmaRequest.RMANumber, userId);

                        //if (rmaInfo != null && rmaInfo.RmaDetails != null && rmaInfo.RmaDetails.Count > 0)
                        //{
                        //    serviceResponse.RmaInformation = rmaInfo;
                        //    serviceResponse.ErrorCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                        //}
                        //else
                        //    serviceResponse.ErrorCode = ResponseErrorCode.NoRecordsFound.ToString();
                    }
                    else
                    {
                        serviceResponse.Comment = "Cannot authenticate user";
                        serviceResponse.ErrorCode = ResponseErrorCode.CannotAuthenticateUser.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                serviceResponse.RmaInformation = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ErrorCode = ResponseErrorCode.InconsistantData.ToString();
            }
            return serviceResponse;
        }


        public static void RmaPackingSlipInsertUpdate(int rmaGUID, string packingSlip)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {

                objCompHash.Add("@RmaGUID", rmaGUID);
                objCompHash.Add("@PackingSlip", packingSlip);


                arrSpFieldSeq = new string[] { "@RmaGUID", "@PackingSlip" };
                db.ExeCommand(objCompHash, "av_RmaPackingSlipInsert", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {

                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }



    }
}
