using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Collections;
using System.Xml.Serialization;

namespace avii.Classes
{
    public class ContainerOperation
    {
        //private SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

        public static List<SV.Framework.LabelGenerator.CartonBoxID> GetPurchaseOrderBoxIDs(int companyID, string poNum, int poID, string BoxNumber)
        {
            List<SV.Framework.LabelGenerator.CartonBoxID> boxList = new List<SV.Framework.LabelGenerator.CartonBoxID>();
            SV.Framework.LabelGenerator.CartonBoxID cartonBox = null;

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return boxList;
        }
        private static List<SV.Framework.LabelGenerator.CartonBoxID> PopulateCartonBoxes(DataTable dt)
        {
            List<SV.Framework.LabelGenerator.CartonBoxID> cartonBoxes = null;
            SV.Framework.LabelGenerator.CartonBoxID cartonBoxID = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                cartonBoxes = new List<SV.Framework.LabelGenerator.CartonBoxID>();
                foreach (DataRow row in dt.Rows)
                {
                    cartonBoxID = new SV.Framework.LabelGenerator.CartonBoxID();
                    cartonBoxID.FulfillmentNumber = Convert.ToString(clsGeneral.getColumnData(row, "PO_Num", string.Empty, false));
                    cartonBoxID.SKU = Convert.ToString(clsGeneral.getColumnData(row, "Item_Code", string.Empty, false));
                    cartonBoxID.BoxID = Convert.ToString(clsGeneral.getColumnData(row, "BoxNumber", string.Empty, false));
                    cartonBoxID.BoxDesc = Convert.ToString(clsGeneral.getColumnData(row, "BoxDesc", string.Empty, false));
                    cartonBoxes.Add(cartonBoxID);
                }
            }

            return cartonBoxes;

        }
        public static List<ContainerInfo> GetContainerInfo(int companyID, string poNum, string trackingNumbr, out List<FulfillmentContainer> containers, out List<FulfillmentAssignNonESN> nonESNList, out List<POTracking> trackingList)
        {
            trackingList = new List<POTracking>();
            POTracking tracking = null;

            nonESNList = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            List<ContainerInfo> containerList = null;
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
                        foreach (DataRow row in ds.Tables[3].Rows)
                        {
                            tracking = new POTracking();
                            tracking.TrackingNumber = Convert.ToString(clsGeneral.getColumnData(row, "TrackingNumber", string.Empty, false));
                            trackingList.Add(tracking);
                        }
                    }


                }
                //db.ExeCommand(objCompHash, "av_PurchaseOrderESN_Assign", arrSpFieldSeq, "@poRecordCount", out poRecordCount, "@poErrorMessage", out poErrorMessage);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return containerList;

        }
        public static List<ContainerInfo> GetContainerInfo(int companyID, string poNum, string trackingNumbr, out List<FulfillmentContainer> containers, out List<FulfillmentPallet> pallets)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            List<ContainerInfo> containerList = null;
            containers = null;
            pallets = null;
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return containerList;
        }
        public static List<FulfillmentContainer> GenerateContainerIDs(int containerCount, int poid, int palletCount, int companyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            List<FulfillmentContainer> containerList = null;
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return containerList;
        }
        public static List<FulfillmentContainer> GenerateContainerIDs(int containerCount, int poid, int palletCount, int companyID, out List<FulfillmentPallet> palletList)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            List<FulfillmentContainer> containerList = null;
            palletList = null;
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return containerList;
        }

        public static List<FulfillmentContainer> GetPalletContainersSearch(int companyID, string poNum, string trackingNumber, out List<FulfillmentPallet> palletList)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            List<FulfillmentContainer> containerList = null;
            palletList = null;
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return containerList;
        }
       
        public static string ContainerIDInsert(int poid, int casePackQuantity, string containerIDs, int userId, List<FulfillmentContainer> containerList, 
            int containersCount, int containerUpdatedCount, string palletIDs, List<FulfillmentPallet> palletList, string Comment, List<PalletContainersMapping> mappings)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            string mappingXML = clsGeneral.SerializeObject(mappings);

            string containersXML = clsGeneral.SerializeObject(containerList);
            if (palletList != null && palletList.Count > 0)
                containersXML = containersXML + clsGeneral.SerializeObject(palletList);

            SV.Framework.Models.Fulfillment.ContainerLogModel logModel = new SV.Framework.Models.Fulfillment.ContainerLogModel();
            string response = "";

            logModel.ActionName = "Container Create";
            logModel.CreateUserID = userId;
            logModel.StatusID = 0;
            logModel.PO_ID = poid;
            logModel.ContainerCount = containersCount;
            logModel.ContainerUpdatedCount = containerUpdatedCount;
            logModel.FulfillmentNumber = "";
            logModel.Comment = string.Empty;

            logModel.RequestData = containersXML;

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            string returnMessage = string.Empty;
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
                response = objExp.Message;
                logModel.Comment = response;
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
               logOperations.FulfillmentLogInsert(logModel);
            }
            return returnMessage;
        }

        private static DataTable ContaierData(List<FulfillmentContainer> containerList, int PalletQuantity, string Code)
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

        private static DataTable ContaierData(List<FulfillmentContainer> containerList)
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

        public static string ContainerIDInsert(int poid, int casePackQuantity, int userId, List<FulfillmentContainer> containerList,
            int containersCount, int containerUpdatedCount, string palletIDs, List<FulfillmentPallet> palletList, string Comment, 
            List<PalletContainersMapping> mappings, string Code, int PalletQuantity)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            string mappingXML = clsGeneral.SerializeObject(mappings);

            string containersXML = clsGeneral.SerializeObject(containerList);
            if (palletList != null && palletList.Count > 0)
                containersXML = containersXML + clsGeneral.SerializeObject(palletList);

            SV.Framework.Models.Fulfillment.ContainerLogModel logModel = new SV.Framework.Models.Fulfillment.ContainerLogModel();
            string response = "";

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
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            string returnMessage = string.Empty;
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                logOperations.FulfillmentLogInsert(logModel);

            }
            return returnMessage;
        }

        public static string PalletContainersMappingInsertUpdate(int poid, int userId, List<PalletContainersMapping> mappingList)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            string containersXML = clsGeneral.SerializeObject(mappingList);
            
            SV.Framework.Models.Fulfillment.ContainerLogModel logModel = new SV.Framework.Models.Fulfillment.ContainerLogModel();
            string response = "";

            logModel.ActionName = "PalletContainer Mapping";
            logModel.CreateUserID = userId;
            logModel.StatusID = 0;
            logModel.PO_ID = poid;
            logModel.FulfillmentNumber = "";
            logModel.Comment = string.Empty;

            logModel.RequestData = containersXML;

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            string returnMessage = string.Empty;
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                logOperations.FulfillmentLogInsert(logModel);

            }
            returnMessage = response;
            return returnMessage;
        }

        public static string PalletContainersMappingDelete(int poid, int userId, List<PalletContainersMapping> mappingList)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            string containersXML = clsGeneral.SerializeObject(mappingList);

            SV.Framework.Models.Fulfillment.ContainerLogModel logModel = new SV.Framework.Models.Fulfillment.ContainerLogModel();
            string response = "";

            logModel.ActionName = "Mapping Delete";
            logModel.CreateUserID = userId;
            logModel.StatusID = 0;
            logModel.PO_ID = poid;
            logModel.FulfillmentNumber = "";
            logModel.Comment = string.Empty;

            logModel.RequestData = containersXML;

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            string returnMessage = string.Empty;
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
                logOperations.FulfillmentLogInsert(logModel);

            }
            returnMessage = response;
            return returnMessage;
        }

        public static string ContainerIDInsert(int poid, int casePackQuantity, string containerIDs, int userId)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            string returnMessage = string.Empty;
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return returnMessage;
        }
        public static int ContainerIDDelete(int poid, int userId)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            string containersXML = "POID: " + poid.ToString(); ;

            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            string response = "";

            logModel.ActionName = "Container Delete";
            logModel.CreateUserID = userId;
            logModel.StatusID = 0;
            logModel.PO_ID = poid;
            logModel.FulfillmentNumber = "";
            logModel.Comment = string.Empty;

            logModel.RequestData = containersXML;

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            int returnResult = 0;
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);

                logOperations.FulfillmentLogInsert(logModel);

            }
            return returnResult;
        }
        
        public static int ContainerIDDelete(int poid, int userId, string ContainerID)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            string containersXML = "POID: " + poid.ToString() + " ContainerID: " + ContainerID;

            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            string response = "";

            logModel.ActionName = "Container Delete";
            logModel.CreateUserID = userId;
            logModel.StatusID = 0;
            logModel.PO_ID = poid;
            logModel.FulfillmentNumber = "";
            logModel.Comment = string.Empty;

            logModel.RequestData = containersXML;

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            int returnResult = 0;
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);

                logOperations.FulfillmentLogInsert(logModel);

            }
            return returnResult;
        }

        public static int PalletIDDelete(int poid, int userId, string PalletID)
        {
            SV.Framework.Fulfillment.LogOperations logOperations = SV.Framework.Fulfillment.LogOperations.CreateInstance<SV.Framework.Fulfillment.LogOperations>();

            string containersXML = "POID: " + poid.ToString() + " PalletID: " + PalletID;

            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            string response = "";

            logModel.ActionName = "Pallet Delete";
            logModel.CreateUserID = userId;
            logModel.StatusID = 0;
            logModel.PO_ID = poid;
            logModel.FulfillmentNumber = "";
            logModel.Comment = string.Empty;

            logModel.RequestData = containersXML;

            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            int returnResult = 0;
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
                logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);

                logOperations.FulfillmentLogInsert(logModel);

            }
            return returnResult;
        }

        public static string ContainerIDValidate(int poid, int casePackQuantity, string containerIDs, int userId)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            string returnMessage = string.Empty;
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return returnMessage;
        }

        private static List<FulfillmentContainer> PopulateContainerIDMapping(DataTable dt)
        {

            FulfillmentContainer containerNumber = null;
            List<FulfillmentContainer> containerSKUs = null;
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
        
        private static List<FulfillmentContainer> PopulateContainerIDs(DataTable dt)
        {

            FulfillmentContainer containerNumber = null;
            List<FulfillmentContainer> containerSKUs = null;
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
        private static List<FulfillmentContainer> PopulateContainerID(DataTable dt)
        {

            FulfillmentContainer containerNumber = null;
            List<FulfillmentContainer> containerSKUs = null;
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
        private static List<FulfillmentPallet> PopulatePalletID(DataTable dt)
        {

            FulfillmentPallet fulfillmentPallet = null;
            List<FulfillmentPallet> pallets = null;
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

        private static List<ContainerInfo> PopulateContainerInfo(DataTable dt)
        {

            ContainerInfo skuItem = null;
            List<ContainerInfo> containerSKUs = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    containerSKUs = new List<ContainerInfo>();
                    foreach (DataRow dRowItem in dt.Rows)
                    {

                        skuItem = new ContainerInfo();
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

        private static List<FulfillmentAssignNonESN> PopulateESNLineItems(DataTable dt)
        {

            FulfillmentAssignNonESN lineItem = null;
            List<FulfillmentAssignNonESN> purchaseOrderItems = null;
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

    }
    public class ContainerInfo
    {
        public string Code { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public int Quantity { get; set; }
        public int PoQuantity { get; set; }
        public int ContainerQuantity { get; set; }
        public int ContainerRequired { get; set; }
        public int PalletRequired { get; set; }
        public int PalletQuantity { get; set; }
        public int ItemCompanyGUID { get; set; }
        public int POID { get; set; }
        public int StatusID { get; set; }
        public int CurrentStock { get; set; }
        [XmlIgnore]
        public string ErrorMessage { get; set; }
        public List<FulfillmentContainer> Containers { get; set; }
    }

    public class FulfillmentContainer
    {
        [XmlIgnore]
        public string PalletID { get; set; }
        public string ContainerID { get; set; }
        [XmlIgnore]
        public int POID { get; set; }
        public string Code { get; set; }

        //public int ItemCompanyGUID { get; set; }
    }
    public class FulfillmentPallet
    {
        [XmlIgnore]
        public string Comment { get; set; }
        public string PalletID { get; set; }
        [XmlIgnore]
        public int POID { get; set; }

        //public int ItemCompanyGUID { get; set; }
    }
    public class PalletContainersMapping
    {
        public string PalletID { get; set; }
        public string ContainerID { get; set; }
        
    }

}