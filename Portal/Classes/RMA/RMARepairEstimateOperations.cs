using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using avii.Classes;

namespace avii
{
    public class RMARepairEstimateOperations
    {

        public static void RMARepairEstimate_Delete(int repairEstID, out int statusID)
        {
            statusID = 1;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {

                objCompHash.Add("@RepairEstID", repairEstID);



                arrSpFieldSeq = new string[] { "@RepairEstID" };
                db.ExeCommand(objCompHash, "av_RMA_Repair_Estimate_Delete", arrSpFieldSeq, "@StatusID", out statusID);


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

        public static RMARepairEstimate GetRMARepairEstimateInfo(int rmadetGUID)
        {
            RMARepairEstimate rmaRepairEstimate = new RMARepairEstimate();
            string[] arrSpFieldSeq;
            DataTable dt;
            Hashtable objCompHash = new Hashtable();
            DBConnect db = new DBConnect();
            try
            {
                objCompHash.Add("@RMADetGUID", rmadetGUID);
                
                arrSpFieldSeq = new string[] { "@RMADetGUID" };

                dt = db.GetTableRecords(objCompHash, "av_RMA_Repair_Estimate_Info", arrSpFieldSeq);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt.Rows)
                    {

                        //RMAReport objRMA = new RMAReport();
                        rmaRepairEstimate.RepairEstID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RepairEstID", 0, false));
                        //rmaRepairEstimate.RMANumber = clsGeneral.getColumnData(dataRow, "RmaNumber", string.Empty, false) as string;
                        rmaRepairEstimate.EstimatedReadyDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "EstReadyDate", DateTime.MinValue, false));
                        rmaRepairEstimate.RepairDescription = clsGeneral.getColumnData(dataRow, "RepairDescription", string.Empty, false) as string;
                        rmaRepairEstimate.RepairEstimate = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "RepairEstimate", 0, false));
                        
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
            return rmaRepairEstimate;

        }

        public static void RMARepairEstimate_InsertUpdate(RMARepairEstimate repairEstimate, int statusID, int userID, string statusAction, string disposition)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {

                objCompHash.Add("@RepairEstID", repairEstimate.RepairEstID);
                objCompHash.Add("@RMADetGUID", repairEstimate.RMADetGUID);
                objCompHash.Add("@RepairDescription", repairEstimate.RepairDescription);
                objCompHash.Add("@RepairEstimate", repairEstimate.RepairEstimate);
                objCompHash.Add("@EstReadyDate", repairEstimate.EstimatedReadyDate);
                objCompHash.Add("@StatusID", statusID);
                objCompHash.Add("@CreatedBy", userID);
                objCompHash.Add("@ModifiedBy", userID);
                objCompHash.Add("@DecissionBy", userID);
                objCompHash.Add("@Action", statusAction);
                objCompHash.Add("@Disposition", disposition);




                arrSpFieldSeq = new string[] { "@RepairEstID", "@RMADetGUID", "@RepairDescription", "@RepairEstimate", "@EstReadyDate", "@StatusID", "@CreatedBy", "@ModifiedBy", "@DecissionBy", "@Action", "@Disposition" };
                db.ExeCommand(objCompHash, "av_RMA_Repair_Estimate_InsertUpdate", arrSpFieldSeq);


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

        public static List<RMARepairEstimate> GetRMARepairEstimateList(string storeID, string rmaNumber, string esn, DateTime fromDate, DateTime toDate, int statusID, int companyID)
        {
            List<RMARepairEstimate> rmaRepairEstimateList = new List<RMARepairEstimate>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {


                objCompHash.Add("@StoreID", storeID);
                objCompHash.Add("@RMANumber", rmaNumber);
                objCompHash.Add("@FromDate", fromDate.ToShortDateString() == "1/1/0001" ? null : fromDate.ToShortDateString());
                objCompHash.Add("@ToDate", toDate.ToShortDateString() == "1/1/0001" ? null : toDate.ToShortDateString());
                objCompHash.Add("@ESN", esn);
                objCompHash.Add("@StatusID", statusID);
                objCompHash.Add("@CompanyID", companyID);

                arrSpFieldSeq = new string[] { "@StoreID", "@RMANumber", "@FromDate", "@ToDate", "@ESN", "@StatusID", "@CompanyID" };

                dataTable = db.GetTableRecords(objCompHash, "av_RMA_Repair_Estimate_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    rmaRepairEstimateList = PopulateRepairEstimateList(dataTable);
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

            return rmaRepairEstimateList;
        }

        private static List<RMARepairEstimate> PopulateRepairEstimateList(DataTable dataTable)
        {
            List<RMARepairEstimate> rmaRepairEstimateList = new List<RMARepairEstimate>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RMARepairEstimate objRMA = new RMARepairEstimate();

                    objRMA.RepairEstID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RepairEstID", 0, false));
                    objRMA.RMADetGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "rmaDetGUID", 0, false));
                    objRMA.StoreID = clsGeneral.getColumnData(dataRow, "StoreID", string.Empty, false) as string;
                    objRMA.RMANumber = clsGeneral.getColumnData(dataRow, "RMANumber", string.Empty, false) as string;
                    objRMA.ESN = clsGeneral.getColumnData(dataRow, "esn", string.Empty, false) as string;
                    objRMA.RepairDescription = clsGeneral.getColumnData(dataRow, "RepairDescription", string.Empty, false) as string;
                    objRMA.ItemDescription = clsGeneral.getColumnData(dataRow, "ItemDescription", string.Empty, false) as string;
                    objRMA.ItemNumber = clsGeneral.getColumnData(dataRow, "Item_Code", string.Empty, false) as string;
                    objRMA.Email = clsGeneral.getColumnData(dataRow, "ContactEmail", string.Empty, false) as string;
                    objRMA.ContactName = clsGeneral.getColumnData(dataRow, "ContactName", string.Empty, false) as string;
                    objRMA.RMAReason = clsGeneral.getColumnData(dataRow, "RMAREASON", string.Empty, false) as string;

                    

                    objRMA.RepairEstimate = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "RepairEstimate", 0, false));
                    objRMA.EstimatedReadyDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "EstReadyDate", DateTime.Now, false));
                    rmaRepairEstimateList.Add(objRMA);
                }
            }
            return rmaRepairEstimateList;
        }
        

    }
}