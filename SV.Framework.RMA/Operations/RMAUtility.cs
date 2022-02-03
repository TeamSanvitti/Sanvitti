using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.RMA;
using SV.Framework.Models.Common;
using SV.Framework.Models.Service;
using System.Text.RegularExpressions;
using System.Linq;

namespace SV.Framework.RMA
{
    public class RMAUtility : BaseCreateInstance
    {
        public RMAUserCompany getRMAUserCompanyInfo(int companyID, string email, int allcompay, int userid)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();

            RMAUserCompany objRMAcompany = rmaUtility.getRMAUserCompanyInfo(companyID, email, allcompay, userid);

            return objRMAcompany;
        }
        public void RmaPackingSlipInsertUpdate(int rmaGUID, string packingSlip)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();

            rmaUtility.RmaPackingSlipInsertUpdate(rmaGUID, packingSlip);

        }
        public List<RMAComments> GetRMACommentList(int rmaGUID, bool exclude)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();

            List<RMAComments> rmaCommentList = rmaUtility.GetRMACommentList(rmaGUID, exclude);// new List<RMAComments>();

            return rmaCommentList;
        }
        public List<SV.Framework.Models.RMA.RMA> GetRMAList(int rmaGUID, string rmanumber, string rmadate, string rmadateTo, int rmastatusID, int companyID, string rmaGUIDs, string UPC, string esn, string avso, string poNum, string returnReason, string storeID, out DataTable detailDT)
        {
            detailDT = default;
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();

            List<SV.Framework.Models.RMA.RMA> rmaList = rmaUtility.GetRMAList(rmaGUID, rmanumber, rmadate, rmadateTo, rmastatusID, companyID, rmaGUIDs, UPC, esn, avso, poNum, returnReason, storeID, out detailDT);// new List<SV.Framework.Models.RMA.RMA>();
            return rmaList;

        }
        public void delete_RMA_RMADETAIL(int RmaGUID, int rmaDetGUID, int userID)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();
            rmaUtility.delete_RMA_RMADETAIL(RmaGUID, rmaDetGUID, userID);
        }
        public void RMA_Detail_Picture_InsertUpdate(int pictureID, int rmaDetGUID, string fileName, int userID)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();
            rmaUtility.RMA_Detail_Picture_InsertUpdate(pictureID, rmaDetGUID, fileName, userID);
        }

        public List<RmaHistory> GetRMAHistory(int rmaGUID)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();

            List<RmaHistory> historyList = rmaUtility.GetRMAHistory(rmaGUID);// new List<RmaHistory>();

            return historyList;
        }

        public DataSet GetRmaDocumentLists(int rmaGUID)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();

            DataSet ds = rmaUtility.GetRmaDocumentLists(rmaGUID);
            return ds;

        }
        public void Delete_RMA_Document(int rmaGUID, int rmaDocID, int userID)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();
            rmaUtility.Delete_RMA_Document(rmaGUID, rmaDocID, userID);
        }
        public  Hashtable getReasonHashtable()
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
        public SV.Framework.Models.RMA.RMA getRMAInfo(int rmaGUID, string rmanumber, string rmadate, int rmastatusID, int companyID, string rmaGUIDs)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();

            SV.Framework.Models.RMA.RMA objRMA = rmaUtility.getRMAInfo(rmaGUID, rmanumber, rmadate, rmastatusID, companyID, rmaGUIDs);

            return objRMA;
        }
        public List<SV.Framework.Models.Old.RMA.RMADetail> getRMAesn(int companyID, string esn, string po_num, int webservicecall, int rmaGUID, int userID)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();

            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = rmaUtility.getRMAesn(companyID, esn, po_num, webservicecall, rmaGUID, userID);// new List<RMADetail>();

            return rmaEsnLookup;
        }
        public  DataTable update_RMA(SV.Framework.Models.RMA.RMA rmaInfo, bool docPrint)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();

            DataTable dataTable = rmaUtility.update_RMA(rmaInfo, docPrint);

            return dataTable;
        }
        public DataTable GetRMASummary(string rmaNumber, string sFromDate, string sToDate, int statusID, int companyid, string upc, string esn)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();

            DataTable dataTable = rmaUtility.GetRMASummary(rmaNumber, sFromDate, sToDate, statusID, companyid, upc, esn);
            return dataTable;
        }
        public DataTable GetRMAReport(string rmaNumber, string sFromDate, string sToDate, int statusID, int companyid, string upc, string esn)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();
            DataTable dataTable = rmaUtility.GetRMAReport(rmaNumber, sFromDate, sToDate, statusID, companyid, upc, esn);
            return dataTable;
        }
        public CancelRMAResponse CancelRMA(string rmaNumber, int userID)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();
            return rmaUtility.CancelRMA(rmaNumber, userID);
        }
        public List<RMAReport> GetRMAReport(string rmaNumber, string rmaFromDate, string rmaToDate, int statusID, int companyid, int returnReason, string esn, int userID)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();
            return rmaUtility.GetRMAReport(rmaNumber, rmaFromDate, rmaToDate, statusID, companyid, returnReason, esn, userID);
        }
        public List<RmaEsnStatuses> GetCustomerRmaEsnStatusReport(int companyID, DateTime fromDate, DateTime toDate, int esnStatusID, int rmaStatusID)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();
            return rmaUtility.GetCustomerRmaEsnStatusReport(companyID, fromDate, toDate, esnStatusID, rmaStatusID);
        }
        public List<RMAESNDetail> GetRmaEsnDetailReport(int companyID, string RMANumber, string ESN, DateTime fromDate, DateTime toDate, int esnStatusID, int rmaStatusID)
        {
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();
            return rmaUtility.GetRmaEsnDetailReport(companyID, RMANumber, ESN, fromDate, toDate, esnStatusID, rmaStatusID);
        }





        public RMAAPIResponse Update_RMA_New(RMANew rmaInfo, int userID, int companyID, bool API)
        {

            int statusID = 1;
            int reasonID = 0;
            SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();
            string unassignedESN = "Unassigned ESN";
            int maxEsn = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["maxEsn"]);
            string externalESN = "Not Found";
            string successMessage = "RMA is successfully added with <u><b>RMA# {0}</b></u>. Please do not send your returns until RMA is APPROVED by <b>LAN GLOBAL Returns Department</b>.";
            string dateMsg = "Invalid RMA Date! Can not create RMA before 7 days back.";
            DateTime currentDate = DateTime.Now;
            DateTime rmaDate = new DateTime();

            string returnMessage = string.Empty;
            //string outParam = string.Empty;

            RMAAPIResponse rmaResponse = new RMAAPIResponse();
            rmaResponse.ErrorCode = string.Empty;


            rmaDate = Convert.ToDateTime(rmaInfo.RmaDate);
            TimeSpan diffResult = currentDate - rmaDate;
            if (diffResult.Days > 7)
            {
                rmaResponse.ErrorCode = dateMsg;
            }
            if (ValidateRequiredFields(rmaInfo, out returnMessage))
            {

                List<SV.Framework.Models.Old.RMA.RMADetail> rmaDetailList = new List<SV.Framework.Models.Old.RMA.RMADetail>();

                try
                {
                    foreach (RMADetails rmaDetail in rmaInfo.RmaDetails)
                    {
                        SV.Framework.Models.Old.RMA.RMADetail rmaDetailObj = new SV.Framework.Models.Old.RMA.RMADetail();
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
                            SV.Framework.Models.RMA.RMA rmaObj = new SV.Framework.Models.RMA.RMA();
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

                            DataTable rmaDT = rmaUtility.Update_RMA_API(rmaObj, true, API, generateRMA);

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
                    rmaResponse.ErrorCode = "Internal error";// ResponseErrorCode.InconsistantData.ToString();
                    rmaResponse.Comment = objExp.Message;
                    throw objExp;
                }
            }
            else
                rmaResponse.ErrorCode = returnMessage;

            //rmaResponse.RMANumber = outParam;

            return rmaResponse;
        }

        private  void ValidateESN(ref SV.Framework.Models.Old.RMA.RMADetail avEsn, int companyID, int rmaGUID, int userID)
        {
            //esn lookup
            if (!string.IsNullOrEmpty(avEsn.ESN))
            {
                SV.Framework.DAL.RMA.RMAUtility rmaUtility = SV.Framework.DAL.RMA.RMAUtility.CreateInstance<SV.Framework.DAL.RMA.RMAUtility>();

                List<SV.Framework.Models.Old.RMA.RMADetail> esnlist = rmaUtility.GetRMAesn(companyID, avEsn.ESN, "", 0, rmaGUID, userID);

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
        private bool ValidateRequiredFields(RMANew rmaInfo, out string returnMessage)
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
        private bool isEmail(string inputEmail)
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
        private  string GenerateRMA()
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



    }
}
