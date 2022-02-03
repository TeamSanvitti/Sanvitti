using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class UnprovisioningOperation : BaseCreateInstance
    {
        public  List<UnprovisionPOs> GetPOUnprovisioingSearch(int companyID, string fulfillmentNumber)
        {
            UnprovisionPOs fulfillmentInfo = default;//null;// new FulfillmentEsNInfo();
            List<UnprovisionPOs> poList = default;//null;//new List<FulfillmentEsn>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@FulfillmentNumber", fulfillmentNumber);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@FulfillmentNumber" };

                    ds = db.GetDataSet(objCompHash, "av_UnprovisioingPO_Search", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        poList = new List<UnprovisionPOs>();
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            fulfillmentInfo = new UnprovisionPOs();
                            fulfillmentInfo.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));
                            fulfillmentInfo.LineItemCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "LineItemCount", 0, false));

                            fulfillmentInfo.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                            fulfillmentInfo.CustomerOrderNumber = clsGeneral.getColumnData(dataRow, "CustomerOrderNumber", string.Empty, false) as string;
                            fulfillmentInfo.ShipTo_Address = clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false) as string;
                            fulfillmentInfo.ShipTo_Address2 = clsGeneral.getColumnData(dataRow, "ShipTo_Address1", string.Empty, false) as string;
                            fulfillmentInfo.ShipTo_City = clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, false) as string;
                            fulfillmentInfo.ShipTo_State = clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false) as string;
                            fulfillmentInfo.ShipTo_Zip = clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, false) as string;
                            fulfillmentInfo.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                            fulfillmentInfo.Contact_Name = clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, false) as string;
                            fulfillmentInfo.Contact_Phone = clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false) as string;
                            fulfillmentInfo.POSource = clsGeneral.getColumnData(dataRow, "POSource", string.Empty, false) as string;
                            fulfillmentInfo.POType = clsGeneral.getColumnData(dataRow, "POType", string.Empty, false) as string;
                            fulfillmentInfo.Store_ID = clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false) as string;
                            fulfillmentInfo.StatusText = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                            fulfillmentInfo.Ship_Via = clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false) as string;
                            //  fulfillmentInfo.POType = clsGeneral.getColumnData(dataRow, "POType", string.Empty, false) as string;

                            //fulfillmentInfo.PO_Date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, false)).ToString("MM/dd/yyyy");
                            //fulfillmentInfo.ShipTo_Date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.Now, false)).ToString("MM/dd/yyyy");
                            //fulfillmentInfo.requestedshipdate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "requestedshipdate", DateTime.Now, false)).ToString("MM/dd/yyyy");

                            fulfillmentInfo.PO_Date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now, false));
                            fulfillmentInfo.ShipTo_Date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.Now, false));
                            fulfillmentInfo.requestedshipdate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "requestedshipdate", DateTime.Now, false));
                            poList.Add(fulfillmentInfo);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //  
                }
                finally
                {
                    //db = null;

                }
            }

            return poList;
        }

        public  string UnprovisioingRequestInsert(UnprovisionPORequest request)
        {
            string errorMessage = default;//string.Empty;
            int recordCount = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //string sCode = string.Empty;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@UnprovisioningID", request.UnprovisioningID);
                    objCompHash.Add("@CreatedBy", request.CreatedBy);
                    objCompHash.Add("@RequestedBy", request.RequestedBy);
                    objCompHash.Add("@ApprovedBy", request.ApprovedBy);
                    objCompHash.Add("@PO_ID", request.POID);
                    objCompHash.Add("@CustomerComment", request.CustomerComment);
                    objCompHash.Add("@AdminComment", request.AdminComment);
                    objCompHash.Add("@Status", request.Status);

                    arrSpFieldSeq = new string[] { "@UnprovisioningID", "@CreatedBy", "@RequestedBy", "@ApprovedBy", "@PO_ID",
                "@CustomerComment", "@AdminComment", "@Status"};
                    DataSet ds = db.GetDataSet(objCompHash, "av_UnprovisioningRequest_InsertUpdate", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            recordCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RowCounts", 0, false));
                        }
                        if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                        {
                            List<POESNInfo> esnList = new List<POESNInfo>();
                            POESNInfo poinfo = null;
                            foreach (DataRow dataRow1 in ds.Tables[2].Rows)
                            {
                                poinfo = new POESNInfo();
                                request.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow1, "POID", 0, false));
                                poinfo.ContainerID = clsGeneral.getColumnData(dataRow1, "ContainerID", string.Empty, false) as string;
                                poinfo.BatchNumber = clsGeneral.getColumnData(dataRow1, "BatchNumber", string.Empty, false) as string;
                                poinfo.ESN = clsGeneral.getColumnData(dataRow1, "esn", string.Empty, false) as string;
                                poinfo.ICCID = clsGeneral.getColumnData(dataRow1, "ICC_ID", string.Empty, false) as string;
                                poinfo.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow1, "ItemCompanyGUID", 0, false));
                                poinfo.Location = clsGeneral.getColumnData(dataRow1, "BoxNumber", string.Empty, false) as string;
                                poinfo.TrackingNumber = clsGeneral.getColumnData(dataRow1, "TrackingNumber", string.Empty, false) as string;
                                // poinfo.ContainerID = clsGeneral.getColumnData(dataRow1, "ContainerID", string.Empty, false) as string;
                                esnList.Add(poinfo);
                            }
                            FulfillmentLogModel logModel = new FulfillmentLogModel();
                            string response = "Submitted successfully";
                            if (esnList != null && esnList.Count > 0)
                            {
                                try
                                {
                                    string esnXML = clsGeneral.SerializeObject(esnList);

                                    logModel.ActionName = "UnProvisioning";
                                    logModel.CreateUserID = request.ApprovedBy;
                                    logModel.StatusID = 0;
                                    logModel.PO_ID = request.POID;
                                    logModel.FulfillmentNumber = "";
                                    logModel.Comment = string.Empty;

                                    logModel.RequestData = esnXML;
                                }
                                catch (Exception ex11)
                                {
                                    logModel.Comment = ex11.Message;
                                    response = ex11.Message;
                                }
                                finally
                                {
                                    logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                                    SV.Framework.DAL.Fulfillment.LogOperations logOperations = new LogOperations();
                                    logOperations.FulfillmentLogInsert(logModel);
                                }
                            }
                        }
                    }
                    // recordCount = db.ExecuteNonQuery(objCompHash, "av_UnprovisioningRequest_InsertUpdate", arrSpFieldSeq);
                    if (recordCount > 0)
                    {
                        errorMessage = "";// ResponseErrorCode.UpdatedSuccessfully.ToString();
                    }
                    else
                    {
                        errorMessage = clsGeneral.ResponseErrorCode.DataNotUpdated.ToString();
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //  
                    errorMessage = objExp.Message.ToString();
                }
                finally
                {
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return errorMessage;
        }

        public  List<UnprovisionStatus> GetUnprovisioingStatus()
        {
            UnprovisionStatus statusInfo = default;//null;// new FulfillmentEsNInfo();
            List<UnprovisionStatus> statusList = default;//null;//new List<FulfillmentEsn>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@StatusID", 0);

                    arrSpFieldSeq = new string[] { "@StatusID" };

                    dt = db.GetTableRecords(objCompHash, "av_Unprovisioning_Status_Select", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        statusList = new List<UnprovisionStatus>();
                        foreach (DataRow dataRow in dt.Rows)
                        {
                            statusInfo = new UnprovisionStatus();
                            statusInfo.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "StatusID", 0, false));

                            statusInfo.StatusText = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                            statusList.Add(statusInfo);
                        }

                    }
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //  
                }
                finally
                {
                   // db = null;

                }

            }
            return statusList;
        }

        public  List<UnprovisioingInfo> GetUnprovisioingRequestSearch(int companyID, string fulfillmentNumber, int statusID, string DateFrom, string DateTo)
        {
            UnprovisioingInfo fulfillmentInfo = default;//null;// new FulfillmentEsNInfo();
            List<UnprovisioingInfo> poList = default;//null;//new List<FulfillmentEsn>();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@FulfillmentNumber", fulfillmentNumber);
                    objCompHash.Add("@StatusID", statusID);
                    objCompHash.Add("@FromDate", string.IsNullOrEmpty(DateFrom) ? null : DateFrom);
                    objCompHash.Add("@ToDate", string.IsNullOrEmpty(DateTo) ? null : DateTo);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@FulfillmentNumber", "@StatusID", "@FromDate", "@ToDate" };
                    dt = db.GetTableRecords(objCompHash, "av_UnprovisioningRequest_Select", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        poList = new List<UnprovisioingInfo>();
                        foreach (DataRow row in dt.Rows)
                        {
                            fulfillmentInfo = new UnprovisioingInfo();
                            fulfillmentInfo.IsVisible = Convert.ToBoolean(clsGeneral.getColumnData(row, "IsVisible", false, false)); ;
                            fulfillmentInfo.UnprovisioningID = Convert.ToInt32(clsGeneral.getColumnData(row, "UnprovisioningID", 0, false)); ;
                            fulfillmentInfo.TotalQty = Convert.ToInt32(clsGeneral.getColumnData(row, "TotalQty", 0, false)); ;
                            fulfillmentInfo.FulfillmentNumber = Convert.ToString(clsGeneral.getColumnData(row, "PO_Num", string.Empty, false));
                            fulfillmentInfo.RequestedBy = Convert.ToString(clsGeneral.getColumnData(row, "RequestedByUser", string.Empty, false));
                            fulfillmentInfo.ApprovedBy = Convert.ToString(clsGeneral.getColumnData(row, "ApprovedByUser", string.Empty, false));
                            fulfillmentInfo.CustomerComment = Convert.ToString(clsGeneral.getColumnData(row, "CustomerComment", string.Empty, false));
                            fulfillmentInfo.Status = Convert.ToString(clsGeneral.getColumnData(row, "StatusText", string.Empty, false));
                            fulfillmentInfo.CreateDate = Convert.ToDateTime(clsGeneral.getColumnData(row, "CreateDate", string.Empty, false));
                            poList.Add(fulfillmentInfo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //  
                }
                finally
                {
                    // db = null;

                }
            }

            return poList;
        }



    }
}
