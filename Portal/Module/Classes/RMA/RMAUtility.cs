using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;


namespace avii.Classes
{
    public class RMAUtility
    {

        public static List<RMADetail> RMAValidation(List<RMA> rmaList)
        {

            List<RMADetail> rmaDetailList = new List<RMADetail>();
            string maxLengthESN = "Esn Should be between 8 to 30 digits!";
            string duplicateESN = "ESN already added to the RMA!";
            Hashtable hshEsnDuplicateCheck = new Hashtable();
            //for (int i =0; i < rmaList.Count; i++)
            foreach(RMA rmaObj in rmaList)
            {
                rmaDetailList = rmaObj.RmaDetails;

                //for (int j = 0; j < rmaDetailList.Count; j++)
                foreach(RMADetail rmaDetailObj in rmaDetailList)
                {
                    if (string.IsNullOrEmpty(rmaDetailObj.ESN))
                    {
                        rmaDetailObj.Validation = "ESN can not be empty";

                    }
                    else
                    {
                        if (hshEsnDuplicateCheck.ContainsKey(rmaDetailObj.ESN))
                        {
                            rmaDetailObj.Validation = duplicateESN; //string.Format("Duplicate ESN ({0}) is found", rmaDetailObj.ESN);
                        }

                        if (rmaDetailObj.ESN.Length < 8 || rmaDetailObj.ESN.Length > 30)
                        {
                            rmaDetailObj.Validation = maxLengthESN;
                        }
                       
                        RMADetail objRMAesn = new avii.Classes.RMADetail();
                        objRMAesn.ESN = rmaDetailObj.ESN;
                        ValidateESN(ref objRMAesn, rmaObj.RMAUserCompany.CompanyID, 0);
                        
                        if (!objRMAesn.AllowDuplicate)
                        {
                            rmaDetailObj.Validation = string.Format("RMA ({0}) already exists", rmaDetailObj.ESN);
                            
                        }
                        if (!objRMAesn.AllowRMA)
                        {
                            rmaDetailObj.Validation = string.Format("RMA is not allowed for this item({1}) related to ESN({0}).", rmaDetailObj.ESN, objRMAesn.UPC);
                            
                        }
                    }
                    rmaDetailList.Add(rmaDetailObj);
                }

            }
            
            return rmaDetailList;
        }
        private static void ValidateESN(ref RMADetail avEsn, int companyID, int rmaGUID)
        {
            //esn lookup
            if (!string.IsNullOrEmpty(avEsn.ESN))
            {

                List<avii.Classes.RMADetail> esnlist = RMAUtility.getRMAesn(companyID, avEsn.ESN, "", 0, rmaGUID);
                if (esnlist.Count > 0)
                {

                    avEsn.UPC = esnlist[0].UPC.ToString();
                    //avEsn.rmaDetGUID = esnlist[0].rmaDetGUID;
                    //avEsn.AVSalesOrderNumber = esnlist[0].AVSalesOrderNumber;
                    //avEsn.PurchaseOrderNumber = esnlist[0].PurchaseOrderNumber;
                    avEsn.AllowRMA = esnlist[0].AllowRMA;
                    avEsn.AllowDuplicate = esnlist[0].AllowDuplicate;

                }
            }
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
                objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false) as string;
                objRMADETAIL.Reason = clsGeneral.getColumnData(dataRow, "Reason", 0, false).ToString();
                objRMADETAIL.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                objRMADETAIL.CallTime = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CallTime", 0, false));
                objRMADETAIL.Notes = clsGeneral.getColumnData(dataRow, "Notes", string.Empty, false) as string;
                objRMADETAIL.WaferSealed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WaferSealed", 0, false));
                objRMADETAIL.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                objRMADETAIL.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                objRMADETAIL.Po_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "poid", 0, false));
                objRMADETAIL.Pod_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pod_id", 0, false));
                objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false) as string;
                objRMADETAIL.PurchaseOrderNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;

                objRMADETAIL.ItemCode = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;

                objRMADETAIL.AllowRMA = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowRMA", false, false));
                objRMADETAIL.AllowDuplicate = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowDuplicate", false, false));
                objRMADETAIL.RecievedOn = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RecievedOn", "01/01/1901", false)); 
                objRMADETAIL.DeviceCondition = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DeviceCondition", 0, false));
                objRMADETAIL.DeviceDefact = clsGeneral.getColumnData(dataRow, "DeviceDefact", string.Empty, false) as string;
                objRMADETAIL.DeviceDesignation = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DeviceCondition", 0, false)); 
                objRMADETAIL.DeviceState = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DeviceCondition", 0, false));
                objRMADETAIL.Disposition = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DeviceCondition", 0, false));
                objRMADETAIL.LocationCode = clsGeneral.getColumnData(dataRow, "LocationCode", string.Empty, false) as string;
                objRMADETAIL.NewSKU = clsGeneral.getColumnData(dataRow, "NewSKU", string.Empty, false) as string;
                objRMADETAIL.ReStockingDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ReStockingDate", "01/01/1901", false)); 
                objRMADETAIL.ReStockingFee = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "ReStockingFee", 0, false));
                objRMADETAIL.Warranty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Warranty", 0, false));
                objRMADETAIL.WarrantyExpieryDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "WarrantyExpieryDate", "01/01/1901", false)); 
                rmaDetailList.Add(objRMADETAIL);
            }
            return rmaDetailList;
        }
        public static List<RMA> GetRMASummary(int companyID, int rmastatusID)
        {
            List<RMA> rmaList = new List<RMA>();
            DataSet dataSet = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RmaGUID", 0);
                objCompHash.Add("@rmanumber", null);
                objCompHash.Add("@rmadate", null);
                objCompHash.Add("@rmadateTo", null);
                objCompHash.Add("@rmastatusID", rmastatusID);
                objCompHash.Add("@companyid", companyID);
                objCompHash.Add("@rmaGUIDs", string.Empty);
                objCompHash.Add("@UPC", string.Empty);
                objCompHash.Add("@esn", string.Empty);
                objCompHash.Add("@avso", string.Empty);
                objCompHash.Add("@po_num", string.Empty);
                objCompHash.Add("@returnReason", 0);


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
                        //objRMA.RmaDetails = getRMADetail(objRMA.RmaGUID, -1, string.Empty);
                        objRMA.RMAUserCompany.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        objRMA.RMAUserCompany.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objRMA.RMAUserCompany.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
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
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return rmaList;
        }

        public static List<RMA> GetRMAStatusReport(int rmaGUID)
        {
            List<RMA> rmaList = new List<RMA>();
            DataSet dataSet = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RmaGUID", rmaGUID);
               

                arrSpFieldSeq = new string[] { "@RmaGUID" };

                dataSet = db.GetDataSet(objCompHash, "av_RMALog_SELECT", arrSpFieldSeq);


                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        RMA objRMA = new RMA();
                        objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMA.ModifiedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "LastModfiedDate", DateTime.MinValue, false));
                        objRMA.RmaStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RMAStatusID", 0, false));
                        objRMA.RmaContactName = clsGeneral.getColumnData(dataRow, "Username", string.Empty, false) as string;
                        objRMA.Comment = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                        objRMA.Status = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                        objRMA.AVComments = clsGeneral.getColumnData(dataRow, "RMAXML", string.Empty, false) as string;                        
                        


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
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return rmaList;
        }
        public static List<RMA> getRMAList(int rmaGUID, string rmanumber, string rmadate, string rmadateTo, int rmastatusID, int companyID, string rmaGUIDs, string UPC, string esn, string avso, string poNum, string returnReason)
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
                        objRMA.ModifiedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ModIFiedDate", DateTime.MinValue, false));
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
                        if (dataSet.Tables[1].Rows.Count > 0)
                        {
                            HttpContext.Current.Session["rmadetail"] = dataSet.Tables[1];
                            objRMA.RmaDetails = PopulateRMADetailList(dataSet.Tables[1]);
                            HttpContext.Current.Session["rmadetaillist"] = objRMA.RmaDetails;


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
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return rmaList;
        }
        public static List<RMA> GetRMAList(int rmaGUID, string rmanumber, string rmadate, string rmadateTo, int rmastatusID, int companyID, string rmaGUIDs, string UPC, string esn, string avso, string poNum, string returnReason)
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

                dataSet = db.GetDataSet(objCompHash, "av_rma_new_select", arrSpFieldSeq);


                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {
                        RMA objRMA = new RMA();
                        objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMA.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        objRMA.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue, false));
                        objRMA.ModifiedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ModIFiedDate", DateTime.MinValue, false));
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
                        objRMA.RmaDocument = clsGeneral.getColumnData(dataRow, "RMADocument", string.Empty, false) as string;
                        
                        if (dataSet.Tables[1].Rows.Count > 0)
                        {
                            //HttpContext.Current.Session["rmadetail"] = dataSet.Tables[1];
                            objRMA.RmaDetails = PopulateRMADetailList(dataSet.Tables[1]);
                            HttpContext.Current.Session["rmadetaillist"] = objRMA.RmaDetails;


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
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return rmaList;
        }

        public static RMA getRMAInfo(int rmaGUID, string rmanumber, string rmadate,int rmastatusID, int companyID, string rmaGUIDs)
        {
            RMA objRMA = new RMA();
            DataTable dataTable = new DataTable();
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

                dataTable = db.GetTableRecords(objCompHash, "av_rma_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {

                        objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMA.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
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
                        objRMA.RmaDetails = getRMADetail(Convert.ToInt32(objRMA.RmaGUID), -1, string.Empty);
                        
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

            return objRMA;
        }
        public static RMA GetNewRMAInfo(int rmaGUID, string rmanumber, string rmadate, int rmastatusID, int companyID, string rmaGUIDs)
        {
            RMA objRMA = new RMA();
            //DataTable dataTable = new DataTable();
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

                //dataTable = db.GetTableRecords(objCompHash, "av_rma_new_select", arrSpFieldSeq);
                dataSet = db.GetDataSet(objCompHash, "av_rma_new_select", arrSpFieldSeq);
                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {

                        objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMA.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        objRMA.RmaDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RmaDate", DateTime.MinValue.ToShortDateString(), false));
                        objRMA.POGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "POGUID", 0, false));
                        objRMA.RmaStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaStatusID", 0, false));
                        objRMA.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "UserID", 0, false));
                        objRMA.RmaContactName = clsGeneral.getColumnData(dataRow, "RmaContactName", string.Empty, false) as string;
                        objRMA.Comment = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                        objRMA.AVComments = clsGeneral.getColumnData(dataRow, "AVComments", string.Empty, false) as string;
                        objRMA.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                        objRMA.RMAUserCompany.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        objRMA.RMAUserCompany.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        objRMA.RMAUserCompany.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                        objRMA.Address = clsGeneral.getColumnData(dataRow, "Address1", string.Empty, false) as string;
                        objRMA.City = clsGeneral.getColumnData(dataRow, "City", string.Empty, false) as string;
                        objRMA.State = clsGeneral.getColumnData(dataRow, "State", string.Empty, false) as string;
                        objRMA.Zip = clsGeneral.getColumnData(dataRow, "Zip", string.Empty, false) as string;
                        objRMA.Phone = clsGeneral.getColumnData(dataRow, "officePhone1", string.Empty, false) as string;
                        objRMA.Email = clsGeneral.getColumnData(dataRow, "Email", string.Empty, false) as string;
                        objRMA.RmaDetails = PopulateRMADetailList(dataSet.Tables[1]);//getRMADetail(Convert.ToInt32(objRMA.RmaGUID), -1, string.Empty);
                        objRMA.RmaDocument = clsGeneral.getColumnData(dataRow, "RMADocument", string.Empty, false) as string;
                        
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

            return objRMA;
        }

        public static RMA GetRMADetails(int rmaGUID)
        {
            RMA objRMA = new RMA();
            DataSet dataSet = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RmaGUID", rmaGUID);
                objCompHash.Add("@rmanumber", null);
                objCompHash.Add("@rmadate", null);
                objCompHash.Add("@rmadateTo", null);
                objCompHash.Add("@rmastatusID", 0);
                objCompHash.Add("@companyid", 0);
                objCompHash.Add("@rmaGUIDs", string.Empty);
                objCompHash.Add("@UPC", string.Empty);
                objCompHash.Add("@esn", string.Empty);
                objCompHash.Add("@avso", string.Empty);
                objCompHash.Add("@po_num", string.Empty);
                objCompHash.Add("@returnReason", 0);


                arrSpFieldSeq = new string[] { "@RmaGUID", "@rmanumber", "@rmadate", "@rmadateTo", "@rmastatusID", "@companyid", "@rmaGUIDs", "@UPC", "@esn", "@avso", "@po_num", "@returnReason" };



                //arrSpFieldSeq = new string[] { "@RmaGUID", "@rmanumber", "@rmadate", "@rmastatusID", "@companyid", "@rmaGUIDs", "@UPC" };

                dataSet = db.GetDataSet(objCompHash, "av_rma_select", arrSpFieldSeq);

                if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataSet.Tables[0].Rows)
                    {

                        objRMA.RmaGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RmaGUID", 0, false));
                        objRMA.RmaNumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
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
                        //objRMA.RmaDetails = getRMADetail(objRMA.RmaGUID, -1, string.Empty);
                        if (dataSet.Tables[1].Rows.Count > 0)
                        {
                            //HttpContext.Current.Session["rmadetail"] = dataSet.Tables[1];
                            objRMA.RmaDetails = PopulateRMADetailList(dataSet.Tables[1]);
                        }
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

            return objRMA;
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
                        objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false) as string;
                        objRMADETAIL.Reason = clsGeneral.getColumnData(dataRow, "Reason", 0, false).ToString();
                        objRMADETAIL.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));
                        objRMADETAIL.CallTime = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CallTime", 0, false));
                        objRMADETAIL.Notes = clsGeneral.getColumnData(dataRow, "Notes", string.Empty, false) as string;
                        objRMADETAIL.WaferSealed = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "WaferSealed", 0, false));
                        objRMADETAIL.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                        objRMADETAIL.Status = clsGeneral.getColumnData(dataRow, "Status", string.Empty, false) as string;
                        objRMADETAIL.Po_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "poid", 0, false));
                        objRMADETAIL.Pod_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pod_id", 0, false));
                        objRMADETAIL.AVSalesOrderNumber = clsGeneral.getColumnData(dataRow, "AerovoiceSalesOrderNumber", string.Empty, false) as string;
                        objRMADETAIL.PurchaseOrderNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                        
                        objRMADETAIL.ItemCode = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;
                        objRMADETAIL.AllowRMA = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowRMA", false, false));
                        objRMADETAIL.AllowDuplicate = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowDuplicate", false, false));
                        
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
                db.DBClose();
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
            reasonHashTable.Add("5", "Others");
            reasonHashTable.Add("6", "MissingParts");
            reasonHashTable.Add("7", "ReturnToStock");
            reasonHashTable.Add("8", "BuyerRemorse");
            reasonHashTable.Add("9", "PhysicalAbuse");
            reasonHashTable.Add("10", "LiquidDamage");
            reasonHashTable.Add("11", "DropCalls");
            reasonHashTable.Add("12", "Software");
            reasonHashTable.Add("13", "ActivationIssues");
            reasonHashTable.Add("14", "CoverageIssues");
            reasonHashTable.Add("15", "LoanerProgram");
            

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
                db.DBClose();
                db = null;
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
        public static DataTable update_RMA(RMA rmaIbfo)
        {
            string ESNXml = SerializeObject(rmaIbfo.RmaDetails);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@RmaGUID", rmaIbfo.RmaGUID);
                objCompHash.Add("@rmanumber", rmaIbfo.RmaNumber);
                objCompHash.Add("@rmadate", rmaIbfo.RmaDate);
                objCompHash.Add("@RmaStatusID", rmaIbfo.RmaStatusID);
                objCompHash.Add("@CreatedBy", rmaIbfo.CreatedBy);
                objCompHash.Add("@ModifiedBy", rmaIbfo.ModifiedBy);
                objCompHash.Add("@UserID", rmaIbfo.UserID);
                objCompHash.Add("@ponum", rmaIbfo.PoNum);
                objCompHash.Add("@rmaCustomername", rmaIbfo.RmaContactName);
                objCompHash.Add("@ContactAddress", rmaIbfo.Address);
                objCompHash.Add("@ContactState", rmaIbfo.State);
                objCompHash.Add("@ContactCity", rmaIbfo.City);
                objCompHash.Add("@ContactZip", rmaIbfo.Zip);
                objCompHash.Add("@ContactPhone", rmaIbfo.Phone);
                objCompHash.Add("@ContactEmail", rmaIbfo.Email);
                objCompHash.Add("@Comment", rmaIbfo.Comment);
                objCompHash.Add("@AVComments", rmaIbfo.AVComments);
                objCompHash.Add("@rmaxml", ESNXml);



                arrSpFieldSeq = new string[] { "@RmaGUID", "@rmanumber", "@rmadate", "@RmaStatusID", "@CreatedBy", "@ModifiedBy", "@UserID", "@ponum"
                    , "@rmaCustomername", "@ContactAddress", "@ContactState", "@ContactCity", "@ContactZip", "@ContactPhone", "@ContactEmail", "@Comment","@AVComments", "@rmaxml" };


                dataTable = db.GetTableRecords(objCompHash, "av_rma_insert_update", arrSpFieldSeq);

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
            return dataTable;
        }

        public static RMAResponse Update_RMA(RMA rmaIbfo)
        {
            RMAResponse rmaResponse = new Classes.RMAResponse();
            string ESNXml = SerializeObject(rmaIbfo.RmaDetails);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@RmaGUID", rmaIbfo.RmaGUID);
                objCompHash.Add("@rmanumber", rmaIbfo.RmaNumber);
                objCompHash.Add("@rmadate", rmaIbfo.RmaDate);
                objCompHash.Add("@RmaStatusID", rmaIbfo.RmaStatusID);
                objCompHash.Add("@CreatedBy", rmaIbfo.CreatedBy);
                objCompHash.Add("@ModifiedBy", rmaIbfo.ModifiedBy);
                objCompHash.Add("@UserID", rmaIbfo.UserID);
                objCompHash.Add("@ponum", rmaIbfo.PoNum);
                objCompHash.Add("@rmaCustomername", rmaIbfo.RmaContactName);
                objCompHash.Add("@ContactAddress", rmaIbfo.Address);
                objCompHash.Add("@ContactState", rmaIbfo.State);
                objCompHash.Add("@ContactCity", rmaIbfo.City);
                objCompHash.Add("@ContactZip", rmaIbfo.Zip);
                objCompHash.Add("@ContactPhone", rmaIbfo.Phone);
                objCompHash.Add("@ContactEmail", rmaIbfo.Email);
                objCompHash.Add("@Comment", rmaIbfo.Comment);
                objCompHash.Add("@AVComments", rmaIbfo.AVComments);
                objCompHash.Add("@rmaxml", ESNXml);
                

                string outParam = string.Empty;
                int rmaCount = 0;
                arrSpFieldSeq = new string[] { "@RmaGUID", "@rmanumber", "@rmadate", "@RmaStatusID", "@CreatedBy", "@ModifiedBy", "@UserID", "@ponum"
                    , "@rmaCustomername", "@ContactAddress", "@ContactState", "@ContactCity", "@ContactZip", "@ContactPhone", "@ContactEmail", "@Comment","@AVComments", "@rmaxml" };


                rmaCount = db.ExCommand(objCompHash, "av_rma_insert_update", arrSpFieldSeq, "@outparam", out outParam);
                rmaResponse.RMACount = rmaCount;
                rmaResponse.RMANumber = outParam;
                rmaResponse.ErrorCode = string.Empty; 
            }
            catch (Exception objExp)
            {
                rmaResponse.ErrorCode = objExp.Message.ToString(); 
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
        public static RMAResponse Update_RMA_New(RMA rmaIbfo)
        {
            RMAResponse rmaResponse = new Classes.RMAResponse();
            string ESNXml = SerializeObject(rmaIbfo.RmaDetails);
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@RmaGUID", rmaIbfo.RmaGUID);
                objCompHash.Add("@rmanumber", rmaIbfo.RmaNumber);
                objCompHash.Add("@rmadate", rmaIbfo.RmaDate);
                objCompHash.Add("@RmaStatusID", rmaIbfo.RmaStatusID);
                objCompHash.Add("@CreatedBy", rmaIbfo.CreatedBy);
                objCompHash.Add("@ModifiedBy", rmaIbfo.ModifiedBy);
                objCompHash.Add("@UserID", rmaIbfo.UserID);
                objCompHash.Add("@ponum", rmaIbfo.PoNum);
                objCompHash.Add("@rmaCustomername", rmaIbfo.RmaContactName);
                objCompHash.Add("@ContactAddress", rmaIbfo.Address);
                objCompHash.Add("@ContactState", rmaIbfo.State);
                objCompHash.Add("@ContactCity", rmaIbfo.City);
                objCompHash.Add("@ContactZip", rmaIbfo.Zip);
                objCompHash.Add("@ContactPhone", rmaIbfo.Phone);
                objCompHash.Add("@ContactEmail", rmaIbfo.Email);
                objCompHash.Add("@Comment", rmaIbfo.Comment);
                objCompHash.Add("@AVComments", rmaIbfo.AVComments);
                objCompHash.Add("@rmaxml", ESNXml);
                objCompHash.Add("@RmaDocument", rmaIbfo.RmaDocument);


                string outParam = string.Empty;
                int rmaCount = 0;
                arrSpFieldSeq = new string[] { "@RmaGUID", "@rmanumber", "@rmadate", "@RmaStatusID", "@CreatedBy", "@ModifiedBy", "@UserID", "@ponum"
                    , "@rmaCustomername", "@ContactAddress", "@ContactState", "@ContactCity", "@ContactZip", "@ContactPhone", "@ContactEmail", "@Comment","@AVComments", "@rmaxml","@RmaDocument" };


                rmaCount = db.ExCommand(objCompHash, "av_rma_New_insert_update", arrSpFieldSeq, "@outparam", out outParam);
                rmaResponse.RMACount = rmaCount;
                rmaResponse.RMANumber = outParam;
                rmaResponse.ErrorCode = string.Empty;
            }
            catch (Exception objExp)
            {
                rmaResponse.ErrorCode = objExp.Message.ToString();
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
        #endregion
        # region Update Aero_ESNService returncode to zero(0) when RAM status changed to Approved - alter the DB entries --
        public  void update_ESNService_returncode(int RmaGUID, string ESN, string itemCode)
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
                db.DBClose();
                db = null;
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
                db.DBClose();
                db = null;
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
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        #endregion
        # region Delete RMA & RMA DETAIL  to delete the DB entries --
        public static void delete_RMA_RMADETAIL(int RmaGUID, int rmaDetGUID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {

                objCompHash.Add("@rmaGUID", RmaGUID);
                objCompHash.Add("@rmaDetGUID", rmaDetGUID);


                arrSpFieldSeq = new string[] { "@rmaGUID", "@rmaDetGUID" };
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
        #endregion


        #region function Generate RMADetail from PO
        public  List<RMADetail> getRMADetailFromPO(string ponumber)
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
                        objRMADETAIL.Reason = clsGeneral.getColumnData(dataRow, "Reason", string.Empty, false) as string;
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
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return rmaDetail;
        }

        #endregion
        public static List<RMADetail> getRMAesn(int companyID, string esn, string po_num, int webservicecall, int rmaGUID)
        {
            List<RMADetail> rmaEsnLookup = new List<RMADetail>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@companyID", companyID);
                objCompHash.Add("@ESN", esn);

                objCompHash.Add("@po_num", po_num);
                objCompHash.Add("@webservicecall", webservicecall);
                objCompHash.Add("@RMAGUID", rmaGUID);

                arrSpFieldSeq = new string[] { "@companyID", "@ESN", "@po_num", "@webservicecall", "@RMAGUID" };

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
                        objRMAesn.rmaDetGUID = 0;
                        objRMAesn.Reason = "";
                        objRMAesn.StatusID = 1;
                        objRMAesn.CallTime = 0;
                        objRMAesn.Notes = "";
                        objRMAesn.PoStatusId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "statusid", 0, false));
                        objRMAesn.AllowRMA = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowRMA", false, false));
                        objRMAesn.AllowDuplicate = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "AllowDuplicate", false, false));
                        objRMAesn.ItemCode = clsGeneral.getColumnData(dataRow, "itemcode", string.Empty, false) as string;
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
                db.DBClose();
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
                db.DBClose();
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
                db.DBClose();
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
                db.DBClose();
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
                db.DBClose();
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
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return dataTable;
        }

        public static DataTable GetCustomerRMAStatus(int companyID, int timeInterval)
        {

            DataTable dataTable = new DataTable();
            DataSet ds = new DataSet();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@TimeInterval", timeInterval);


                arrSpFieldSeq = new string[] { "@CompanyID", "@TimeInterval" };

                ds = db.GetDataSet(objCompHash, "AV_Customer_RMAStatus", arrSpFieldSeq);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                        dataTable = ds.Tables[0];
                    if (ds.Tables.Count > 1)
                        HttpContext.Current.Session["POCount"] = ds.Tables[1];
                    else
                        HttpContext.Current.Session["POCount"] = null;
                    if (ds.Tables.Count > 2)
                        HttpContext.Current.Session["SKUCount"] = ds.Tables[2];
                    else
                        HttpContext.Current.Session["SKUCount"] = null;

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

            return dataTable;
        }

        public static void UploadRMA(List<RMA> rmaList)
        {
            string RMAXml = SerializeObject(rmaList);

        }

        public static int GetCustomerRMACOUNT(int userID)
        {
            int returnCount = 0;
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {

                objCompHash.Add("@UserID", userID);

                arrSpFieldSeq = new string[] { "@UserID" };

                db.ExeCommand(objCompHash, "av_CustomerRMACount_Select", arrSpFieldSeq, "@RMACOUNT", out  returnCount);
               

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

            return returnCount;
        }
    }
}
