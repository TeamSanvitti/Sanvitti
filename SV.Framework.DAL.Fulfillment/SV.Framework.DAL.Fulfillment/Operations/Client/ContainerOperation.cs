using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml.Serialization;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class ContainerOperation: BaseCreateInstance
    {
        public  List<CartonBoxID> GetPurchaseOrderBoxIDs(int companyID, string poNum, int poID, string BoxNumber)
        {
            List<CartonBoxID> boxList = default;//new List<CartonBoxID>();

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@FulfillmentNumber", poNum);
                    objCompHash.Add("@POID", poID);
                    objCompHash.Add("@BoxNumber", BoxNumber);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@FulfillmentNumber", "@POID", "@BoxNumber" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderBoxID_Select", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        boxList = PopulateCartonBoxes(dt);
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return boxList;
        }
        public  List<ContainerInfo> GetContainerInfo(int companyID, string poNum, string trackingNumbr, out List<FulfillmentContainer> containers, out List<FulfillmentAssignNonESN> nonESNList, out List<POTracking> trackingList)
        {
            trackingList = default;// new List<POTracking>();
            POTracking tracking = default;//;
            List<ContainerInfo> containerList = default;//;
            nonESNList = default;//;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                
                containers = null;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@companyID", companyID);
                    objCompHash.Add("@PoNum", poNum);
                    objCompHash.Add("@TrackingNumber", trackingNumbr);

                    arrSpFieldSeq = new string[] { "@companyID", "@PoNum", "@TrackingNumber" };
                    ds = db.GetDataSet(objCompHash, "av_PurchaseOrderContainer_B2B_Select", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        containerList = PopulateContainerInfo(ds.Tables[0]);
                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                            containers = PopulateContainerID(ds.Tables[1]);
                        if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                            nonESNList = PopulateESNLineItems(ds.Tables[2]);
                        if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
                        {
                            trackingList = new List<POTracking>();
                            foreach (DataRow row in ds.Tables[3].Rows)
                            {
                                tracking = new POTracking();
                                tracking.TrackingNumber = Convert.ToString(clsGeneral.getColumnData(row, "TrackingNumber", string.Empty, false));
                                trackingList.Add(tracking);
                            }
                        }
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return containerList;

        }
        public  List<ContainerInfo> GetContainerInfo(int companyID, string poNum, string trackingNumbr, out List<FulfillmentContainer> containers, out List<FulfillmentPallet> pallets)
        {
            List<ContainerInfo> containerList = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();

                containers = default;//null;
                pallets = default;//null;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@companyID", companyID);
                    objCompHash.Add("@PoNum", poNum);
                    objCompHash.Add("@TrackingNumber", trackingNumbr);

                    arrSpFieldSeq = new string[] { "@companyID", "@PoNum", "@TrackingNumber" };
                    ds = db.GetDataSet(objCompHash, "av_PurchaseOrderContainer_Select", arrSpFieldSeq);
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        containerList = PopulateContainerInfo(ds.Tables[0]);
                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                            containers = PopulateContainerID(ds.Tables[1]);
                        if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
                            pallets = PopulatePalletID(ds.Tables[2]);
                    }
                    //db.ExeCommand(objCompHash, "av_PurchaseOrderESN_Assign", arrSpFieldSeq, "@poRecordCount", out poRecordCount, "@poErrorMessage", out poErrorMessage);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return containerList;
        }
        public  List<FulfillmentContainer> GenerateContainerIDs(int containerCount, int poid, int palletCount, int companyID)
        {
            List<FulfillmentContainer> containerList = default;//null;
            //DBConnect db = new DBConnect();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", poid);
                    objCompHash.Add("@containerCount", containerCount);
                    objCompHash.Add("@PalletCount", palletCount);
                    objCompHash.Add("@CompanyID", companyID);


                    arrSpFieldSeq = new string[] { "@POID", "@containerCount", "@PalletCount", "@CompanyID" };
                    dt = db.GetTableRecords(objCompHash, "Av_GeneratePOContanierID", arrSpFieldSeq);

                    containerList = PopulateContainerIDs(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return containerList;
        }
        public  List<FulfillmentContainer> GenerateContainerIDs(int containerCount, int poid, int palletCount, int companyID, out List<FulfillmentPallet> palletList)
        {
            List<FulfillmentContainer> containerList = default;//null;
            palletList = default;// null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", poid);
                    objCompHash.Add("@containerCount", containerCount);
                    objCompHash.Add("@PalletCount", palletCount);
                    objCompHash.Add("@CompanyID", companyID);


                    arrSpFieldSeq = new string[] { "@POID", "@containerCount", "@PalletCount", "@CompanyID" };
                    ds = db.GetDataSet(objCompHash, "Av_GeneratePOContanierID", arrSpFieldSeq);

                    containerList = PopulateContainerIDs(ds.Tables[0]);
                    palletList = PopulatePalletID(ds.Tables[1]);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                   // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return containerList;
        }

        public  List<FulfillmentContainer> GetPalletContainersSearch(int companyID, string poNum, string trackingNumber, out List<FulfillmentPallet> palletList)
        {
            List<FulfillmentContainer> containerList = default;//null;
            palletList = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@companyID", companyID);
                    objCompHash.Add("@PoNum", poNum);
                    objCompHash.Add("@TrackingNumber", trackingNumber);


                    arrSpFieldSeq = new string[] { "@companyID", "@PoNum", "@TrackingNumber" };
                    ds = db.GetDataSet(objCompHash, "av_PurchaseOrderContainerPallet_Mapping", arrSpFieldSeq);

                    containerList = PopulateContainerIDMapping(ds.Tables[0]);
                    palletList = PopulatePalletID(ds.Tables[1]);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return containerList;
        }

        public  string ContainerIDInsert(int poid, int casePackQuantity, string containerIDs, int userId, List<FulfillmentContainer> containerList,
            int containersCount, int containerUpdatedCount, string palletIDs, List<FulfillmentPallet> palletList, string Comment, List<PalletContainersMapping> mappings)
        {
            string response = default;
            string returnMessage = default;
            using (DBConnect db = new DBConnect())
            {
                string mappingXML = clsGeneral.SerializeObject(mappings);

                string containersXML = clsGeneral.SerializeObject(containerList);
                if (palletList != null && palletList.Count > 0)
                    containersXML = containersXML + clsGeneral.SerializeObject(palletList);

                ContainerLogModel logModel = new ContainerLogModel();
               

                logModel.ActionName = "Container Create";
                logModel.CreateUserID = userId;
                logModel.StatusID = 0;
                logModel.PO_ID = poid;
                logModel.ContainerCount = containersCount;
                logModel.ContainerUpdatedCount = containerUpdatedCount;
                logModel.FulfillmentNumber = "";
                logModel.Comment = string.Empty;

                logModel.RequestData = containersXML;

                //  DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", poid);
                    objCompHash.Add("@ContainerIDs", containerIDs);
                    objCompHash.Add("@CasePackQuantity", casePackQuantity);
                    objCompHash.Add("@UserId", userId);
                    objCompHash.Add("@PalletIDs", palletIDs);
                    objCompHash.Add("@Comments", Comment);
                    objCompHash.Add("@MappingXML", mappingXML);


                    arrSpFieldSeq = new string[] { "@POID", "@ContainerIDs", "@CasePackQuantity", "@UserId", "@PalletIDs", "@Comments", "@MappingXML" };
                    object obj = db.ExecuteScalar(objCompHash, "av_PurchaseOrderContainers_InsertUpdate", arrSpFieldSeq);
                    if (obj != null)
                    {
                        returnMessage = obj.ToString();
                        if (string.IsNullOrEmpty(returnMessage))
                        {
                            response = "Submitted successfully";
                        }
                        else
                            response = returnMessage + " ContainerID(s) already exists. Please generate again and submit";
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    response = objExp.Message;
                    logModel.Comment = response;
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                    logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                    LogOperations logOperations = new LogOperations();
                    logOperations.FulfillmentLogInsert(logModel);
                    //LogOperations.FulfillmentLogInsert(logModel);
                }
            }
            return returnMessage;
        }

        
        public  string ContainerIDInsert(int poid, int casePackQuantity, int userId, List<FulfillmentContainer> containerList,
            int containersCount, int containerUpdatedCount, string palletIDs, List<FulfillmentPallet> palletList, string Comment,
            List<PalletContainersMapping> mappings, string Code, int PalletQuantity)
        {
            string response = "";
            string returnMessage = "";
            using (DBConnect db = new DBConnect())
            {
                string mappingXML = clsGeneral.SerializeObject(mappings);

                string containersXML = clsGeneral.SerializeObject(containerList);
                if (palletList != null && palletList.Count > 0)
                    containersXML = containersXML + clsGeneral.SerializeObject(palletList);

                ContainerLogModel logModel = new ContainerLogModel();


                logModel.ActionName = "Container Create";
                logModel.CreateUserID = userId;
                logModel.StatusID = 0;
                logModel.PO_ID = poid;
                logModel.ContainerCount = containersCount;
                logModel.ContainerUpdatedCount = containerUpdatedCount;
                logModel.FulfillmentNumber = "";
                logModel.Comment = string.Empty;

                logModel.RequestData = containersXML;
                DataTable ContaierDT = ContaierData(containerList, PalletQuantity, Code);
                // DBConnect db = new DBConnect();
                string[] arrSpFieldSeq;

                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", poid);
                    objCompHash.Add("@TMP", ContaierDT);
                    objCompHash.Add("@CasePackQuantity", casePackQuantity);
                    objCompHash.Add("@UserId", userId);
                    objCompHash.Add("@PalletIDs", palletIDs);
                    objCompHash.Add("@Comments", Comment);
                    objCompHash.Add("@MappingXML", mappingXML);


                    arrSpFieldSeq = new string[] { "@POID", "@TMP", "@CasePackQuantity", "@UserId", "@PalletIDs", "@Comments", "@MappingXML" };
                    object obj = db.ExecuteScalar(objCompHash, "av_PurchaseOrderContainers_InsertUpdate", arrSpFieldSeq);
                    if (obj != null)
                    {
                        returnMessage = obj.ToString();
                        if (string.IsNullOrEmpty(returnMessage))
                        {
                            response = "Submitted successfully";
                        }
                        else
                            response = returnMessage + " ContainerID(s) already exists. Please generate again and submit";
                    }
                }
                catch (Exception objExp)
                {
                    response = objExp.Message;
                    logModel.Comment = response;
                    //  throw new Exception(objExp.Message.ToString());
                    Logger.LogMessage(objExp, this);
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                    logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                    LogOperations logOperations = new LogOperations();
                    logOperations.FulfillmentLogInsert(logModel);
                    //LogOperations.FulfillmentLogInsert(logModel);

                }
            }
            return returnMessage;
        }

        public  string PalletContainersMappingInsertUpdate(int poid, int userId, List<PalletContainersMapping> mappingList)
        {
            string returnMessage = default;//string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string containersXML = clsGeneral.SerializeObject(mappingList);

                ContainerLogModel logModel = new ContainerLogModel();
                string response = "";

                logModel.ActionName = "PalletContainer Mapping";
                logModel.CreateUserID = userId;
                logModel.StatusID = 0;
                logModel.PO_ID = poid;
                logModel.FulfillmentNumber = "";
                logModel.Comment = string.Empty;

                logModel.RequestData = containersXML;

                string[] arrSpFieldSeq;
                int returnResult = 0;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@MappingXML", containersXML);
                    objCompHash.Add("@PO_ID", poid);

                    arrSpFieldSeq = new string[] { "@MappingXML", "@PO_ID" };
                    object obj = db.ExecuteNonQuery(objCompHash, "av_PalletContainers_Mapping_InsertUpdate", arrSpFieldSeq);
                    if (obj != null)
                    {
                        returnResult = Convert.ToInt32(obj);
                        if (returnResult > 0)
                        {
                            response = "Submitted successfully";
                        }
                        else
                        {

                        }

                    }
                }
                catch (Exception objExp)
                {
                    response = objExp.Message;
                    logModel.Comment = response;
                    Logger.LogMessage(objExp, this);
                   // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                    logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                    LogOperations logOperations = new LogOperations();
                    logOperations.FulfillmentLogInsert(logModel);
                    //LogOperations.FulfillmentLogInsert(logModel);

                }
                returnMessage = response;
            }
            return returnMessage;
        }

        public  string PalletContainersMappingDelete(int poid, int userId, List<PalletContainersMapping> mappingList)
        {
            string returnMessage = default;//string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string containersXML = clsGeneral.SerializeObject(mappingList);

                ContainerLogModel logModel = new ContainerLogModel();
                string response = "";

                logModel.ActionName = "Mapping Delete";
                logModel.CreateUserID = userId;
                logModel.StatusID = 0;
                logModel.PO_ID = poid;
                logModel.FulfillmentNumber = "";
                logModel.Comment = string.Empty;

                logModel.RequestData = containersXML;
               
                string[] arrSpFieldSeq;
               
                int returnResult = 0;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@PO_ID", poid);

                    arrSpFieldSeq = new string[] { "@PO_ID" };
                    object obj = db.ExecuteScalar(objCompHash, "av_PalletContainers_Mapping_Delete", arrSpFieldSeq);
                    if (obj != null)
                    {
                        returnResult = Convert.ToInt32(obj);
                        if (returnResult > 0)
                        {
                            response = "Deleted successfully";
                        }
                        else
                        {
                            response = "Data not deleted";
                        }

                    }
                }
                catch (Exception objExp)
                {
                    response = objExp.Message;
                    logModel.Comment = response;
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                    logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);

                    LogOperations logOperations = new LogOperations();
                    logOperations.FulfillmentLogInsert(logModel);
                    //LogOperations.FulfillmentLogInsert(logModel);

                }
                returnMessage = response;
            }
            return returnMessage;
        }

        public  string ContainerIDInsert(int poid, int casePackQuantity, string containerIDs, int userId)
        {
            string returnMessage = default;// string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", poid);
                    objCompHash.Add("@ContainerIDs", containerIDs);
                    objCompHash.Add("@CasePackQuantity", casePackQuantity);
                    objCompHash.Add("@UserId", userId);

                    arrSpFieldSeq = new string[] { "@POID", "@ContainerIDs", "@CasePackQuantity", "@UserId" };
                    object obj = db.ExecuteScalar(objCompHash, "av_PurchaseOrderContainers_InsertUpdate", arrSpFieldSeq);
                    if (obj != null)
                        returnMessage = obj.ToString();

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnMessage;
        }
        public  int ContainerIDDelete(int poid, int userId)
        {
            int returnResult = 0;
            using (DBConnect db = new DBConnect())
            {
                string containersXML = "POID: " + poid.ToString(); ;

                FulfillmentLogModel logModel = new FulfillmentLogModel();
                string response = "";

                logModel.ActionName = "Container Delete";
                logModel.CreateUserID = userId;
                logModel.StatusID = 0;
                logModel.PO_ID = poid;
                logModel.FulfillmentNumber = "";
                logModel.Comment = string.Empty;

                logModel.RequestData = containersXML;

                string[] arrSpFieldSeq;
                
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", poid);
                    objCompHash.Add("@UserId", userId);


                    arrSpFieldSeq = new string[] { "@POID", "@UserId" };
                    object obj = db.ExecuteScalar(objCompHash, "av_PurchaseOrderContainers_Delete", arrSpFieldSeq);
                    if (obj != null)
                        returnResult = Convert.ToInt32(obj);

                    if (returnResult > 0)
                    {
                        response = "Deleted successfully";
                    }
                    else
                        response = "ContainerID(s) in use cannot be deleted";


                }
                catch (Exception objExp)
                {
                    response = objExp.Message;
                    logModel.Comment = response;
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                    logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                    LogOperations logOperations = new LogOperations();
                    logOperations.FulfillmentLogInsert(logModel);
                    //LogOperations.FulfillmentLogInsert(logModel);

                }
            }
            return returnResult;
        }

        public  int ContainerIDDelete(int poid, int userId, string ContainerID)
        {
            int returnResult = 0;
            using (DBConnect db = new DBConnect())
            {
                string containersXML = "POID: " + poid.ToString() + " ContainerID: " + ContainerID;

                FulfillmentLogModel logModel = new FulfillmentLogModel();
                string response = "";

                logModel.ActionName = "Container Delete";
                logModel.CreateUserID = userId;
                logModel.StatusID = 0;
                logModel.PO_ID = poid;
                logModel.FulfillmentNumber = "";
                logModel.Comment = string.Empty;

                logModel.RequestData = containersXML;

                string[] arrSpFieldSeq;
                
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", poid);
                    objCompHash.Add("@UserId", userId);
                    objCompHash.Add("@ContainerID", ContainerID);


                    arrSpFieldSeq = new string[] { "@POID", "@UserId", "@ContainerID" };
                    object obj = db.ExecuteScalar(objCompHash, "av_PurchaseOrderContainers_Delete", arrSpFieldSeq);
                    if (obj != null)
                        returnResult = Convert.ToInt32(obj);

                    if (returnResult > 0)
                    {
                        response = "Deleted successfully";
                    }
                    else
                        response = ContainerID + " ContainerID in use cannot be deleted";


                }
                catch (Exception objExp)
                {
                    response = objExp.Message;
                    logModel.Comment = response;
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                    logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                    LogOperations logOperations = new LogOperations();
                    logOperations.FulfillmentLogInsert(logModel);
                    // LogOperations.FulfillmentLogInsert(logModel);

                }
            }
            return returnResult;
        }

        public  int PalletIDDelete(int poid, int userId, string PalletID)
        {
            int returnResult = 0;
            using (DBConnect db = new DBConnect())
            {
                string containersXML = "POID: " + poid.ToString() + " PalletID: " + PalletID;

                FulfillmentLogModel logModel = new FulfillmentLogModel();
                string response = "";

                logModel.ActionName = "Pallet Delete";
                logModel.CreateUserID = userId;
                logModel.StatusID = 0;
                logModel.PO_ID = poid;
                logModel.FulfillmentNumber = "";
                logModel.Comment = string.Empty;

                logModel.RequestData = containersXML;

                string[] arrSpFieldSeq;
                
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", poid);
                    objCompHash.Add("@UserId", userId);
                    objCompHash.Add("@PalletID", PalletID);


                    arrSpFieldSeq = new string[] { "@POID", "@UserId", "@PalletID" };
                    object obj = db.ExecuteScalar(objCompHash, "av_PurchaseOrderPallets_Delete", arrSpFieldSeq);
                    if (obj != null)
                        returnResult = Convert.ToInt32(obj);

                    if (returnResult > 0)
                    {
                        response = "Deleted successfully";
                    }
                    else
                        response = PalletID + " PalletID in use cannot be deleted";


                }
                catch (Exception objExp)
                {
                    response = objExp.Message;
                    logModel.Comment = response;
                    Logger.LogMessage(objExp, this);
                   // throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                    logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                    LogOperations logOperations = new LogOperations();
                    logOperations.FulfillmentLogInsert(logModel);
                    // LogOperations.FulfillmentLogInsert(logModel);

                }
            }
            return returnResult;
        }

        public  string ContainerIDValidate(int poid, int casePackQuantity, string containerIDs, int userId)
        {
            string returnMessage = default;//string.Empty;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", poid);
                    objCompHash.Add("@ContainerIDs", containerIDs);
                    objCompHash.Add("@CasePackQuantity", casePackQuantity);
                    objCompHash.Add("@UserId", userId);


                    arrSpFieldSeq = new string[] { "@POID", "@ContainerIDs", "@CasePackQuantity", "@UserId" };
                    object obj = db.ExecuteScalar(objCompHash, "av_PurchaseOrderContainers_Validate", arrSpFieldSeq);
                    if (obj != null)
                        returnMessage = obj.ToString();

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                   // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return returnMessage;
        }

        private  List<FulfillmentContainer> PopulateContainerIDMapping(DataTable dt)
        {

            FulfillmentContainer containerNumber = default;// null;
            List<FulfillmentContainer> containerSKUs = default;//null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    containerSKUs = new List<FulfillmentContainer>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {

                        containerNumber = new FulfillmentContainer();
                        containerNumber.ContainerID = (string)clsGeneral.getColumnData(dRowItem, "ContainerID", string.Empty, false);
                        containerNumber.Code = (string)clsGeneral.getColumnData(dRowItem, "BoxNumber", string.Empty, false);
                        containerNumber.PalletID = (string)clsGeneral.getColumnData(dRowItem, "PalletID", string.Empty, false);
                        containerNumber.POID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "PO_ID", 0, false));

                        containerSKUs.Add(containerNumber);
                    }



                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulateContainerIDs : " + ex.Message);
            }


            return containerSKUs;
        }

        private  List<FulfillmentContainer> PopulateContainerIDs(DataTable dt)
        {

            FulfillmentContainer containerNumber = default;//null;
            List<FulfillmentContainer> containerSKUs = default;//null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    containerSKUs = new List<FulfillmentContainer>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {

                        containerNumber = new FulfillmentContainer();
                        containerNumber.ContainerID = (string)clsGeneral.getColumnData(dRowItem, "containerNumber", string.Empty, false);
                        containerNumber.Code = (string)clsGeneral.getColumnData(dRowItem, "CODE", string.Empty, false);
                        containerNumber.POID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "PO_ID", 0, false));

                        containerSKUs.Add(containerNumber);
                    }



                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulateContainerIDs : " + ex.Message);
            }


            return containerSKUs;
        }
        private  List<FulfillmentContainer> PopulateContainerID(DataTable dt)
        {

            FulfillmentContainer containerNumber = default;//null;
            List<FulfillmentContainer> containerSKUs = default;//null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    containerSKUs = new List<FulfillmentContainer>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {

                        containerNumber = new FulfillmentContainer();
                        containerNumber.ContainerID = (string)clsGeneral.getColumnData(dRowItem, "containerID", string.Empty, false);
                        containerNumber.Code = (string)clsGeneral.getColumnData(dRowItem, "Code", string.Empty, false);
                        containerNumber.POID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "PO_ID", 0, false));
                        containerSKUs.Add(containerNumber);
                    }



                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulateContainerIDs : " + ex.Message);
            }


            return containerSKUs;
        }
        private  List<FulfillmentPallet> PopulatePalletID(DataTable dt)
        {

            FulfillmentPallet fulfillmentPallet = default;//null;
            List<FulfillmentPallet> pallets = default;//null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    pallets = new List<FulfillmentPallet>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {

                        fulfillmentPallet = new FulfillmentPallet();
                        fulfillmentPallet.PalletID = (string)clsGeneral.getColumnData(dRowItem, "PalletID", string.Empty, false);
                        fulfillmentPallet.Comment = (string)clsGeneral.getColumnData(dRowItem, "Comments", string.Empty, false);
                        fulfillmentPallet.POID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "PO_ID", 0, false));
                        pallets.Add(fulfillmentPallet);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulateContainerIDs : " + ex.Message);
            }
            return pallets;
        }

        private  List<ContainerInfo> PopulateContainerInfo(DataTable dt)
        {

            ContainerInfo skuItem = default;//null;
            List<ContainerInfo> containerSKUs = default;//null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    containerSKUs = new List<ContainerInfo>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {
                        skuItem = new ContainerInfo();
                        skuItem.KitType = (string)clsGeneral.getColumnData(dRowItem, "KitType", string.Empty, false);
                        skuItem.ErrorMessage = (string)clsGeneral.getColumnData(dRowItem, "ErrorMessage", string.Empty, false);
                        skuItem.Code = (string)clsGeneral.getColumnData(dRowItem, "Code", string.Empty, false);
                        skuItem.CategoryName = (string)clsGeneral.getColumnData(dRowItem, "CategoryName", string.Empty, false);
                        skuItem.SKU = (string)clsGeneral.getColumnData(dRowItem, "SKU", string.Empty, false);
                        skuItem.ProductName = (string)clsGeneral.getColumnData(dRowItem, "ItemName", string.Empty, false);
                        skuItem.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "Qty", 0, false));
                        skuItem.PoQuantity = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "PoQuantity", 0, false));
                        skuItem.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ItemCompanyGUID", 0, false));
                        skuItem.ContainerRequired = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ContainerRequired", 0, false));
                        skuItem.PalletQuantity = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "PalletQuantity", 0, false));
                        skuItem.PalletRequired = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "PalletRequired", 0, false));
                        skuItem.ContainerQuantity = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ContainerQuantity", 0, false));
                        skuItem.POID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "PO_ID", 0, false));
                        skuItem.StatusID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "StatusID", 0, false));
                        skuItem.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "Stock_in_hand", 0, false));
                        skuItem.IsKittedBox = Convert.ToBoolean(clsGeneral.getColumnData(dRowItem, "IsKittedBox", 0, false));
                        containerSKUs.Add(skuItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulateContainerInfo : " + ex.Message);
            }


            return containerSKUs;
        }

        private  List<FulfillmentAssignNonESN> PopulateESNLineItems(DataTable dt)
        {

            FulfillmentAssignNonESN lineItem = default;//null;
            List<FulfillmentAssignNonESN> purchaseOrderItems = default;//null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    purchaseOrderItems = new List<FulfillmentAssignNonESN>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {
                        //purchaseOrderEsn = new FulfillmentAssignESN();

                        lineItem = new FulfillmentAssignNonESN();
                        lineItem.SKU = Convert.ToString(clsGeneral.getColumnData(dRowItem, "Item_Code", string.Empty, false));
                        lineItem.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ItemCompanyGUID", 0, false));
                        lineItem.Qty = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ESNQuantityRequired", 0, false));
                        lineItem.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "Stock_in_hand", 0, false));
                        lineItem.POD_ID = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "POD_ID", 0, false));
                        lineItem.CategoryName = Convert.ToString(clsGeneral.getColumnData(dRowItem, "CategoryName", string.Empty, false));
                        lineItem.ProductName = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ItemName", string.Empty, false));
                        lineItem.ErrorMessage = Convert.ToString(clsGeneral.getColumnData(dRowItem, "ErrorMessage", string.Empty, false));

                        // lineItem.IsAssign = Convert.ToBoolean(clsGeneral.getColumnData(dRowItem, "IsAssign", false, false));
                        // IsOnlyNonEsnItems = Convert.ToBoolean(clsGeneral.getColumnData(dRowItem, "IsOnlyNonEsnItems", false, false));
                        purchaseOrderItems.Add(lineItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("purchaseOrderItems : " + ex.Message);
            }


            return purchaseOrderItems;
        }

        private DataTable ContaierData(List<FulfillmentContainer> containerList, int PalletQuantity, string Code)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ContainerID", typeof(System.String));
            dt.Columns.Add("Code", typeof(System.String));
            int itr = 0;
            string combineCode = "";
            DataRow row;
            if (containerList != null && containerList.Count > 0)
            {
                foreach (FulfillmentContainer item in containerList)
                {
                    itr = itr + 1;
                    combineCode = Code + itr.ToString();
                    row = dt.NewRow();
                    row["ContainerID"] = item.ContainerID;
                    row["Code"] = item.Code;// combineCode;

                    dt.Rows.Add(row);
                    if (PalletQuantity == itr)
                    {
                        itr = 0;
                    }
                }
            }
            return dt;
        }

        private DataTable ContaierData(List<FulfillmentContainer> containerList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ContainerID", typeof(System.String));


            DataRow row;

            if (containerList != null && containerList.Count > 0)
            {
                foreach (FulfillmentContainer item in containerList)
                {
                    row = dt.NewRow();
                    row["ContainerID"] = item.ContainerID;

                    dt.Rows.Add(row);
                }
            }
            return dt;
        }
        private List<CartonBoxID> PopulateCartonBoxes(DataTable dt)
        {
            List<CartonBoxID> cartonBoxes = default;//null;
            CartonBoxID cartonBoxID = default;//null;
            if (dt != null && dt.Rows.Count > 0)
            {
                cartonBoxes = new List<CartonBoxID>();
                foreach (DataRow row in dt.Rows)
                {
                    cartonBoxID = new CartonBoxID();
                    cartonBoxID.FulfillmentNumber = Convert.ToString(clsGeneral.getColumnData(row, "PO_Num", string.Empty, false));
                    cartonBoxID.SKU = Convert.ToString(clsGeneral.getColumnData(row, "Item_Code", string.Empty, false));
                    cartonBoxID.BoxID = Convert.ToString(clsGeneral.getColumnData(row, "BoxNumber", string.Empty, false));
                    cartonBoxID.BoxDesc = Convert.ToString(clsGeneral.getColumnData(row, "BoxDesc", string.Empty, false));
                    cartonBoxes.Add(cartonBoxID);
                }
            }

            return cartonBoxes;

        }
    }
}
