using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.RMA;
using SV.Framework.Models.Common;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Service;

namespace SV.Framework.DAL.RMA
{
    public class RMAUtility : BaseCreateInstance
    {
        public  RMAUserCompany getRMAUserCompanyInfo(int companyID, string email, int allcompay, int userid)
        {
            RMAUserCompany objRMAcompany = default;// new RMAUserCompany();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
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
                        objRMAcompany = new RMAUserCompany();
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
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return objRMAcompany;
        }

        public void RmaPackingSlipInsertUpdate(int rmaGUID, string packingSlip)
        {
            using (DBConnect db = new DBConnect())
            {
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
                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }

        public  List<RMAComments> GetRMACommentList(int rmaGUID, bool exclude)
        {
            List<RMAComments> rmaCommentList = default;// new List<RMAComments>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
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
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    db.DBClose();
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return rmaCommentList;
        }


        public void RMA_Detail_Picture_InsertUpdate(int pictureID, int rmaDetGUID, string fileName, int userID)
        {
            using (DBConnect db = new DBConnect())
            {
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

                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }

        public List<SV.Framework.Models.RMA.RMA> GetRMAList(int rmaGUID, string rmanumber, string rmadate, string rmadateTo, int rmastatusID, int companyID, string rmaGUIDs, string UPC, string esn, string avso, string poNum, string returnReason, string storeID, out DataTable detailDT)
        {
            detailDT = default;
            List<SV.Framework.Models.RMA.RMA> rmaList = default;// new List<SV.Framework.Models.RMA.RMA>();
            
            using (DBConnect db = new DBConnect())
            {
                DataSet dataSet = default;// new DataSet();
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
                        rmaList = new List<Models.RMA.RMA>();
                        foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                        {
                            SV.Framework.Models.RMA.RMA objRMA = new SV.Framework.Models.RMA.RMA();
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
                            objRMA.ShipWeight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShipWeight", 0, false)) == 0 ? "" : Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShipWeight", 0, false)).ToString();
                            objRMA.ShipComment = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                            objRMA.FinalPostage = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "FinalPostage", 0, false));
                            objRMA.CompanyLogo = clsGeneral.getColumnData(dataRow, "LogoPath", string.Empty, false) as string;

                            if (dataSet.Tables[1].Rows.Count > 0)
                            {
                                detailDT = new DataTable();
                                detailDT = dataSet.Tables[1];
                                //HttpContext.Current.Session["rmadetail"] = dataSet.Tables[1];

                            }
                            rmaList.Add(objRMA);
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
            return rmaList;
        }
        public List<RmaHistory> GetRMAHistory(int rmaGUID)
        {
            List<RmaHistory> historyList = default;// new List<RmaHistory>();
            using (DBConnect db = new DBConnect())
            {
                RmaHistory model = default;
                DataTable dataTable = default;// new DataTable();
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
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    db.DBClose();
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return historyList;
        }
        public  CancelRMAResponse CancelRMA(string rmaNumber, int userID)
        {
            CancelRMAResponse rmaResponse = new CancelRMAResponse();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dataTable = default;// new DataTable();
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
                    // throw objExp;
                }
                finally
                {
                    db.DBClose();
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;

                }
            }
            return rmaResponse;
        }
        public List<RMAESNDetail> GetRmaEsnDetailReport(int companyID, string RMANumber, string ESN, DateTime fromDate, DateTime toDate, int esnStatusID, int rmaStatusID)
        {
            List<RMAESNDetail> rmaList = default;// new List<RMAESNDetail>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
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
                        rmaList = new List<RMAESNDetail>();
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            RMAESNDetail rmaESN = new RMAESNDetail();

                            rmaESN.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false)).ToString("MM/dd/yyyy");

                            rmaESN.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                            rmaESN.BatchNumber = clsGeneral.getColumnData(dataRow, "BatchNumber", string.Empty, false) as string;
                            rmaESN.ICCID = clsGeneral.getColumnData(dataRow, "ICCID", string.Empty, false) as string;
                            rmaESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                            rmaESN.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                            rmaESN.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;


                            rmaESN.RmaStatus = clsGeneral.getColumnData(dataRow, "RmaStatus", string.Empty, false) as string;

                            rmaESN.TriageDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "TriageDate", DateTime.MinValue, false)).ToString("MM/dd/yyyy");
                            if (rmaESN.TriageDate == "01-01-0001" || rmaESN.TriageDate == "01/010001")
                                rmaESN.TriageDate = "";

                            rmaESN.TriageStatus = clsGeneral.getColumnData(dataRow, "TriageStatus", string.Empty, false) as string;
                            rmaESN.Reason = clsGeneral.getColumnData(dataRow, "RMAReason", string.Empty, false) as string;
                            rmaESN.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;

                            rmaESN.CustomerName = clsGeneral.getColumnData(dataRow, "RmaContactName", string.Empty, false) as string;
                            rmaESN.Address = clsGeneral.getColumnData(dataRow, "ContactAddress", string.Empty, false) as string;
                            rmaESN.City = clsGeneral.getColumnData(dataRow, "ContactCity", string.Empty, false) as string;
                            rmaESN.State = clsGeneral.getColumnData(dataRow, "ContactState", string.Empty, false) as string;
                            rmaESN.Zip = clsGeneral.getColumnData(dataRow, "ContactZip", string.Empty, false) as string;
                            // rmaESN.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;

                            rmaList.Add(rmaESN);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return rmaList;
        }

        public List<RmaEsnStatuses> GetCustomerRmaEsnStatusReport(int companyID, DateTime fromDate, DateTime toDate, int esnStatusID, int rmaStatusID)
        {
            List<RmaEsnStatuses> rmaStatusList = default;// new List<RmaEsnStatuses>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();

                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@FromDate", fromDate.ToShortDateString());
                    objCompHash.Add("@ToDate", toDate.ToShortDateString());
                    objCompHash.Add("@RmaStatusID", rmaStatusID);
                    objCompHash.Add("@EsnStatusID", esnStatusID);
                    //objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@RmaStatusID", "@EsnStatusID" };

                    dataTable = db.GetTableRecords(objCompHash, "AV_Customer_RMA_ESN_Report", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        rmaStatusList = new List<RmaEsnStatuses>();
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            RmaEsnStatuses rmaESN = new RmaEsnStatuses();
                            rmaESN.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false));
                            rmaESN.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                            rmaESN.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                            rmaESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                            rmaESN.RmaStatus = clsGeneral.getColumnData(dataRow, "RmaStatus", string.Empty, false) as string;
                            rmaESN.EsnStatus = clsGeneral.getColumnData(dataRow, "EsnStatus", string.Empty, false) as string;
                            rmaESN.AVPO = clsGeneral.getColumnData(dataRow, "AVPO", string.Empty, false) as string;
                            rmaStatusList.Add(rmaESN);
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
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

        public List<RMAReport> GetRMAReport(string rmaNumber, string rmaFromDate, string rmaToDate, int statusID, int companyid, int returnReason, string esn, int userID)
        {
            List<RMAReport> rmaReportList = default;// new List<RMAReport>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@rmanumber", rmaNumber);
                    objCompHash.Add("@rmaFromdate", rmaFromDate);
                    objCompHash.Add("@rmaTodate", rmaToDate);
                    objCompHash.Add("@rmastatusID", statusID);
                    objCompHash.Add("@companyid", companyid);
                    objCompHash.Add("@UPC", string.Empty);
                    objCompHash.Add("@ESN", esn??"");
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
                            objRMA.LanglobalNotes = (clsGeneral.getColumnData(dataRow, "AVComments", string.Empty, false) as string).Trim();
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
            }
            return rmaReportList;

        }

        public DataTable Update_RMA_API(SV.Framework.Models.RMA.RMA rmaInfo, bool docPrint, bool API, bool generateRMA)
        {
            DataTable dataTable = default;// new DataTable();
            using (DBConnect db = new DBConnect())
            {
                string ESNXml = clsGeneral.SerializeObject(rmaInfo.RmaDetails);
                string rmaDocXML = clsGeneral.SerializeObject(rmaInfo.RmaDocumentList);
                string[] arrSpFieldSeq;

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
                    objCompHash.Add("@MaxShipmentDate", rmaInfo.MaxShipmentDate.ToShortDateString() == "01-01-0001" ? DateTime.Now.AddDays(10) : rmaInfo.MaxShipmentDate.ToShortDateString() == "1/1/0001" ? DateTime.Now.AddDays(10) : rmaInfo.MaxShipmentDate);
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
            }
            return dataTable;
        }

        public  List<SV.Framework.Models.Old.RMA.RMADetail> GetRMAesn(int companyID, string esn, string po_num, int webservicecall, int rmaGUID, int userID)
        {
            
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = default;// new List<SV.Framework.Models.Old.RMA.RMADetail>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();

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

                    arrSpFieldSeq = new string[] { "@ESN", "@UserID", "@companyID", "@po_num", "@webservicecall", "@RMAGUID" };

                    dataTable = db.GetTableRecords(objCompHash, "av_rma_esn_select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        rmaEsnLookup = new List<SV.Framework.Models.Old.RMA.RMADetail>();
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            SV.Framework.Models.Old.RMA.RMADetail objRMAesn = new SV.Framework.Models.Old.RMA.RMADetail();

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
                   // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return rmaEsnLookup;
        }

        public DataSet GetRmaDocumentLists(int rmaGUID)
        {
            //List<RmaDocuments> rmaDocList = new List<RmaDocuments>();
            DataSet ds = default;// new DataSet();
            using (DBConnect db = new DBConnect())
            {
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
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    db.DBClose();
                    //   db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return ds;
        }

        public void Delete_RMA_Document(int rmaGUID, int rmaDocID, int userID)
        {
            using (DBConnect db = new DBConnect())
            {
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

                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }

        public void delete_RMA_RMADETAIL(int RmaGUID, int rmaDetGUID, int userID)
        {
            using (DBConnect db = new DBConnect())
            {
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

                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
        }

        public  SV.Framework.Models.RMA.RMA getRMAInfo(int rmaGUID, string rmanumber, string rmadate, int rmastatusID, int companyID, string rmaGUIDs)
        {
            SV.Framework.Models.RMA.RMA objRMA = default;// new SV.Framework.Models.RMA.RMA();
            using (DBConnect db = new DBConnect())
            {
                DataSet dataSet = default;// new DataSet();
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
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return objRMA;
        }

        public List<SV.Framework.Models.Old.RMA.RMADetail> getRMAesn(int companyID, string esn, string po_num, int webservicecall, int rmaGUID, int userID)
        {
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaEsnLookup = default;// new List<RMADetail>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();

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

                    arrSpFieldSeq = new string[] { "@ESN", "@UserID", "@companyID", "@po_num", "@webservicecall", "@RMAGUID" };

                    dataTable = db.GetTableRecords(objCompHash, "av_rma_esn_select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        rmaEsnLookup = new List<Models.Old.RMA.RMADetail>();
                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            SV.Framework.Models.Old.RMA.RMADetail objRMAesn = new SV.Framework.Models.Old.RMA.RMADetail();

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
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return rmaEsnLookup;
        }

        public  DataTable update_RMA(SV.Framework.Models.RMA.RMA rmaInfo, bool docPrint)
        {
            DataTable dataTable = default;// new DataTable();
            using (DBConnect db = new DBConnect())
            {
                string ESNXml = clsGeneral.SerializeObject(rmaInfo.RmaDetails);
                string rmaDocXML = clsGeneral.SerializeObject(rmaInfo.RmaDocumentList);
                string[] arrSpFieldSeq;
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

                    Logger.LogMessage(objExp, this); //throw objExp;
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;

                }
            }
            return dataTable;
        }

        public DataTable GetRMASummary(string rmaNumber, string sFromDate, string sToDate, int statusID, int companyid, string upc, string esn)
        {
            DataTable oDt = default;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@rmanumber", rmaNumber);
                    objCompHash.Add("@rmaFromdate", sFromDate);
                    objCompHash.Add("@rmaTodate", sToDate);
                    objCompHash.Add("@rmastatusID", statusID);
                    objCompHash.Add("@companyid", companyid);
                    objCompHash.Add("@UPC", upc);
                    objCompHash.Add("@ESN", esn);
                    arrSpFieldSeq = new string[] { "@rmanumber", "@rmaFromdate", "@rmaTodate", "@rmastatusID", "@companyid", "@UPC", "@ESN" };
                    oDt = db.GetTableRecords(objCompHash, "av_RMA_Summary", arrSpFieldSeq);
                    //return oDt;
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                    oDt = null;
                    db.DBClose();
                }

            }
            return oDt;

        }

        public DataTable GetRMAReport(string rmaNumber, string sFromDate, string sToDate, int statusID, int companyid, string upc, string esn)
        {
            DataTable oDt;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();

                try
                {
                    objCompHash.Add("@rmanumber", rmaNumber);
                    objCompHash.Add("@rmaFromdate", sFromDate);
                    objCompHash.Add("@rmaTodate", sToDate);
                    objCompHash.Add("@rmastatusID", statusID);
                    objCompHash.Add("@companyid", companyid);
                    objCompHash.Add("@UPC", upc);
                    objCompHash.Add("@ESN", esn);
                    arrSpFieldSeq = new string[] { "@rmanumber", "@rmaFromdate", "@rmaTodate", "@rmastatusID", "@companyid", "@UPC", "@ESN" };
                    oDt = db.GetTableRecords(objCompHash, "av_RMA_Detail_Report", arrSpFieldSeq);
                    return oDt;
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                    oDt = null;
                    db.DBClose();
                }
            }
            return oDt;
        }

        private List<RMAComments> PopulateRMAComments(DataTable dataTable)
        {
            List<RMAComments> rmaCommentList = default;// new List<RMAComments>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                rmaCommentList = new List<RMAComments>();
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

        public  List<SV.Framework.Models.Old.RMA.RMADetail> GetRMADetail(DataTable dataTable, DataTable accessoryTable)
        {
            int accessoryID = 3;
            List<SV.Framework.Models.Old.RMA.RMADetail> rmaDetail = default;// new List<RMADetail>();
            try
            {

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    rmaDetail = new List<Models.Old.RMA.RMADetail>();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {

                        SV.Framework.Models.Old.RMA.RMADetail objRMADETAIL = new SV.Framework.Models.Old.RMA.RMADetail();
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
                Logger.LogMessage(ex, this); //throw ex;
            }


            return rmaDetail;
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

    }
}
