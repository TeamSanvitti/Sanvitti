using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Xml.Serialization;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;


namespace SV.Framework.Fulfillment
{
    public class ContainerOperation : BaseCreateInstance
    {        
        public  List<CartonBoxID> GetPurchaseOrderBoxIDs(int companyID, string poNum, int poID, string boxNumber)
        {
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();
            List<CartonBoxID> boxList = containerOperation.GetPurchaseOrderBoxIDs(companyID, poNum, poID, boxNumber);
            //List<CartonBoxID> boxList = default;
            //CartonBoxID cartonBoxID = default;
            //if (boxListdb != null && boxListdb.Count > 0)
            //{
            //    boxList = new List<CartonBoxID>();
            //    foreach (CartonBoxID item in boxListdb)
            //    {
            //        cartonBoxID = new CartonBoxID();
            //        cartonBoxID.BoxDesc = item.BoxDesc;
            //        cartonBoxID.BoxID = item.BoxID;
            //        cartonBoxID.FulfillmentNumber = item.FulfillmentNumber;
            //        cartonBoxID.SKU = item.SKU;
            //        boxList.Add(cartonBoxID);
            //    }
            //}
            return boxList;
        }

        //public static List<ContainerInfo> GetContainerInfo(int companyID, string poNum, string trackingNumbr, out List<FulfillmentContainer> containers, out List<FulfillmentAssignNonESN> nonESNList, out List<POTracking> trackingList)
        //{
        //    trackingList = new List<POTracking>();
        //    POTracking tracking = null;

        //    nonESNList = null;
        //    DBConnect db = new DBConnect();
        //    string[] arrSpFieldSeq;
        //    DataSet ds = new DataSet();
        //    List<ContainerInfo> containerList = null;
        //    containers = null;
        //    Hashtable objCompHash = new Hashtable();
        //    try
        //    {
        //        objCompHash.Add("@companyID", companyID);
        //        objCompHash.Add("@PoNum", poNum);
        //        objCompHash.Add("@TrackingNumber", trackingNumbr);

        //        arrSpFieldSeq = new string[] { "@companyID", "@PoNum", "@TrackingNumber" };
        //        ds = db.GetDataSet(objCompHash, "av_PurchaseOrderContainer_B2B_Select", arrSpFieldSeq);
        //        if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //        {
        //            containerList = PopulateContainerInfo(ds.Tables[0]);
        //            if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        //                containers = PopulateContainerID(ds.Tables[1]);
        //            if (ds.Tables.Count > 2 && ds.Tables[2].Rows.Count > 0)
        //                nonESNList = PopulateESNLineItems(ds.Tables[2]);
        //            if (ds != null && ds.Tables.Count > 3 && ds.Tables[3].Rows.Count > 0)
        //            {
        //                foreach (DataRow row in ds.Tables[3].Rows)
        //                {
        //                    tracking = new POTracking();
        //                    tracking.TrackingNumber = Convert.ToString(clsGeneral.getColumnData(row, "TrackingNumber", string.Empty, false));
        //                    trackingList.Add(tracking);
        //                }
        //            }


        //        }
        //        //db.ExeCommand(objCompHash, "av_PurchaseOrderESN_Assign", arrSpFieldSeq, "@poRecordCount", out poRecordCount, "@poErrorMessage", out poErrorMessage);
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        db = null;
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }
        //    return containerList;

        //}

        public List<ContainerInfo> GetContainerInfo(int companyID, string poNum, string trackingNumbr, out List<FulfillmentContainer> containers, out List<FulfillmentAssignNonESN> nonESNList, out List<POTracking> trackingList)
        {
            containers = default;
            nonESNList = default;// new List<POTracking>();
            trackingList = default;//;
            List<ContainerInfo> containerList = default;//;
            nonESNList = default;//;
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();

            containerList = containerOperation.GetContainerInfo(companyID, poNum, trackingNumbr, out containers, out nonESNList, out trackingList);
            return containerList;
        }

        public List<ContainerInfo> GetContainerInfo(int companyID, string poNum, string trackingNumbr, out List<FulfillmentContainer> containers, out List<FulfillmentPallet> pallets)
        {           
            containers = default;
            pallets = default;
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();
            List<ContainerInfo> containerList = containerOperation.GetContainerInfo(companyID, poNum, trackingNumbr, out containers, out pallets);

            return containerList;
        }
        //public static List<FulfillmentContainer> GenerateContainerIDs(int containerCount, int poid, int palletCount, int companyID)
        //{
        //    DBConnect db = new DBConnect();
        //    string[] arrSpFieldSeq;
        //    DataTable dt = new DataTable();
        //    List<FulfillmentContainer> containerList = null;
        //    Hashtable objCompHash = new Hashtable();
        //    try
        //    {
        //        objCompHash.Add("@POID", poid);
        //        objCompHash.Add("@containerCount", containerCount);
        //        objCompHash.Add("@PalletCount", palletCount);
        //        objCompHash.Add("@CompanyID", companyID);


        //        arrSpFieldSeq = new string[] { "@POID", "@containerCount", "@PalletCount", "@CompanyID" };
        //        dt = db.GetTableRecords(objCompHash, "Av_GeneratePOContanierID", arrSpFieldSeq);

        //        containerList = PopulateContainerIDs(dt);

        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        db = null;
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }
        //    return containerList;
        //}
        
        public  List<FulfillmentContainer> GenerateContainerIDs(int containerCount, int poid, int palletCount, int companyID, out List<FulfillmentPallet> palletList)
        {
            palletList = null;
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();
            List<FulfillmentContainer> containerList = containerOperation.GenerateContainerIDs(containerCount, poid, palletCount, companyID, out palletList);
            
            return containerList;
        }

        public  List<FulfillmentContainer> GetPalletContainersSearch(int companyID, string poNum, string trackingNumber, out List<FulfillmentPallet> palletList)
        {
            palletList = default;
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();
            List<FulfillmentContainer> containerList = containerOperation.GetPalletContainersSearch(companyID, poNum, trackingNumber, out palletList);
            
            return containerList;
        }

        //public static string ContainerIDInsert(int poid, int casePackQuantity, string containerIDs, int userId, List<FulfillmentContainer> containerList,
        //    int containersCount, int containerUpdatedCount, string palletIDs, List<FulfillmentPallet> palletList, string Comment, 
        //    List<PalletContainersMapping> mappings)
        //{
        //    string mappingXML = clsGeneral.SerializeObject(mappings);

        //    string containersXML = clsGeneral.SerializeObject(containerList);
        //    if (palletList != null && palletList.Count > 0)
        //        containersXML = containersXML + clsGeneral.SerializeObject(palletList);

        //    SV.Framework.Fulfillment.ContainerLogModel logModel = new SV.Framework.Fulfillment.ContainerLogModel();
        //    string response = "";

        //    logModel.ActionName = "Container Create";
        //    logModel.CreateUserID = userId;
        //    logModel.StatusID = 0;
        //    logModel.PO_ID = poid;
        //    logModel.ContainerCount = containersCount;
        //    logModel.ContainerUpdatedCount = containerUpdatedCount;
        //    logModel.FulfillmentNumber = "";
        //    logModel.Comment = string.Empty;

        //    logModel.RequestData = containersXML;

        //    DBConnect db = new DBConnect();
        //    string[] arrSpFieldSeq;
        //    DataTable dt = new DataTable();
        //    string returnMessage = string.Empty;
        //    Hashtable objCompHash = new Hashtable();
        //    try
        //    {
        //        objCompHash.Add("@POID", poid);
        //        objCompHash.Add("@ContainerIDs", containerIDs);
        //        objCompHash.Add("@CasePackQuantity", casePackQuantity);
        //        objCompHash.Add("@UserId", userId);
        //        objCompHash.Add("@PalletIDs", palletIDs);
        //        objCompHash.Add("@Comments", Comment);
        //        objCompHash.Add("@MappingXML", mappingXML);


        //        arrSpFieldSeq = new string[] { "@POID", "@ContainerIDs", "@CasePackQuantity", "@UserId", "@PalletIDs", "@Comments", "@MappingXML" };
        //        object obj = db.ExecuteScalar(objCompHash, "av_PurchaseOrderContainers_InsertUpdate", arrSpFieldSeq);
        //        if (obj != null)
        //        {
        //            returnMessage = obj.ToString();
        //            if (string.IsNullOrEmpty(returnMessage))
        //            {
        //                response = "Submitted successfully";
        //            }
        //            else
        //                response = returnMessage + " ContainerID(s) already exists. Please generate again and submit";
        //        }
        //    }
        //    catch (Exception objExp)
        //    {
        //        response = objExp.Message;
        //        logModel.Comment = response;
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        db = null;
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //        logModel.ResponseData = response;// BaseAerovoice.SerializeObject<PurchaseOrderResponse>(response);
        //        SV.Framework.Fulfillment.LogOperations.FulfillmentLogInsert(logModel);
        //    }
        //    return returnMessage;
        //}

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

        public  string ContainerIDInsert(int poid, int casePackQuantity, int userId, List<FulfillmentContainer> containerList,
            int containersCount, int containerUpdatedCount, string palletIDs, List<FulfillmentPallet> palletList, string Comment,
            List<PalletContainersMapping> mappings, string Code, int PalletQuantity)
        {
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();
            string returnMessage = containerOperation.ContainerIDInsert(poid, casePackQuantity, userId, containerList, containersCount, 
                containerUpdatedCount, palletIDs, palletList, Comment, mappings, Code, PalletQuantity);
            
            return returnMessage;
        }

        public  string PalletContainersMappingInsertUpdate(int poid, int userId, List<PalletContainersMapping> mappingList)
        {
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();
            string returnMessage = containerOperation.PalletContainersMappingInsertUpdate(poid, userId, mappingList);
            
            return returnMessage;
        }

        public  string PalletContainersMappingDelete(int poid, int userId, List<PalletContainersMapping> mappingList)
        {
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();
            string returnMessage = containerOperation.PalletContainersMappingDelete(poid, userId, mappingList);


            return returnMessage;
        }

        public  string ContainerIDInsert(int poid, int casePackQuantity, string containerIDs, int userId)
        {
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();
            string returnMessage = containerOperation.ContainerIDInsert(poid, casePackQuantity, containerIDs, userId);

            return returnMessage;
        }
        public  int ContainerIDDelete(int poid, int userId)
        {
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();
            int returnResult = containerOperation.ContainerIDDelete(poid,  userId);             
            return returnResult;
        }

        public  int ContainerIDDelete(int poid, int userId, string ContainerID)
        {
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();
            int returnResult = containerOperation.ContainerIDDelete(poid, userId, ContainerID);            
            return returnResult;
        }

        public  int PalletIDDelete(int poid, int userId, string PalletID)
        {
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();
            int returnResult = containerOperation.PalletIDDelete(poid, userId, PalletID);            
            return returnResult;
        }

        public  string ContainerIDValidate(int poid, int casePackQuantity, string containerIDs, int userId)
        {
            SV.Framework.DAL.Fulfillment.ContainerOperation containerOperation = SV.Framework.DAL.Fulfillment.ContainerOperation.CreateInstance<DAL.Fulfillment.ContainerOperation>();
            string returnMessage = containerOperation.ContainerIDValidate(poid, casePackQuantity, containerIDs, userId);             
            return returnMessage;
        }
    }
}
