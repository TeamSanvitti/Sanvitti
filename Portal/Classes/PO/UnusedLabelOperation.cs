using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class UnusedLabelOperation
    {
        public static string UnusedLabelInsertUpdate(UnusedLabelInfo request)
        {
            string returnMessage = "Could not cancel label";
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            int returnValue = 0;

            try
            {
                objCompHash.Add("@UserID", request.UserID);
                objCompHash.Add("@svLabelIdsType", request.dataTable);
                
                
                arrSpFieldSeq = new string[] { "@UserID", "@svLabelIdsType" };
                returnValue = db.ExecuteNonQuery(objCompHash, "svUnusedLabel_Cancel", arrSpFieldSeq);
                if (returnValue > 0)
                {
                    returnMessage = "";
                }
                else
                {
                   // returnMessage = "ModelNumber and SKU already exist";
                }
            }
            catch (Exception objExp)
            {
                returnMessage = objExp.Message;
                //logRequest.Comment = objExp.Message;
                //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                //throw objExp;
            }
            finally
            {
                // SV.Framework.Admin.ItemLogOperation.ItemLogInsert(logRequest);
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return returnMessage;
        }

        public static List<UnusedLabel> GetUnusedLabels(int CompanyID, string FromDate, string ToDate, string TrackingNumber, bool IsCancel)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<UnusedLabel> unusedLabelList = new List<UnusedLabel>();
            try
            {
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@FromDate", FromDate == "" ? null : FromDate);
                objCompHash.Add("@ToDate", ToDate == "" ? null : ToDate);
                objCompHash.Add("@TrackingNumber", TrackingNumber);
                objCompHash.Add("@IsCancel", IsCancel);

                arrSpFieldSeq = new string[] { "@CompanyID", "@FromDate", "@ToDate", "@TrackingNumber", "@IsCancel" };
                dt = db.GetTableRecords(objCompHash, "av_ShippingLabelUnusedSelect", arrSpFieldSeq);
                unusedLabelList = PopulateUnusedLabels(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return unusedLabelList;

        }
        private static List<UnusedLabel> PopulateUnusedLabels(DataTable dt)
        {
            UnusedLabel unusedLabel = null;
            List<UnusedLabel> unusedLabelList = new List<UnusedLabel>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {

                    unusedLabel = new UnusedLabel();
                    
                    unusedLabel.TrackingNumber = clsGeneral.getColumnData(row, "TrackingNumber", string.Empty, false) as string;
                    unusedLabel.AssignedTo = clsGeneral.getColumnData(row, "AssignedTo", string.Empty, false) as string;
                    unusedLabel.AssignToNumber = clsGeneral.getColumnData(row, "AssignToNumber", string.Empty, false) as string;
                    unusedLabel.LabelGenerationDate = clsGeneral.getColumnData(row, "LabelGenerationDate", string.Empty, false) as string;
                    unusedLabel.LabelType = clsGeneral.getColumnData(row, "LabelType", string.Empty, false) as string;
                    unusedLabel.ShipmentMethod = clsGeneral.getColumnData(row, "ShipmentMethod", string.Empty, false) as string;
                    unusedLabel.ShipPackage = clsGeneral.getColumnData(row, "ShipPackage", string.Empty, false) as string;
                   // unusedLabel.TrackingNumber = clsGeneral.getColumnData(row, "TrackingNumber", string.Empty, false) as string;
                    unusedLabel.FinalPostage = Convert.ToDecimal(clsGeneral.getColumnData(row, "FinalPostage", 0, false));
                    unusedLabel.ShippingWeight = Convert.ToDecimal(clsGeneral.getColumnData(row, "ShippingWeight", 0, false));
                    unusedLabel.POID = Convert.ToInt32(clsGeneral.getColumnData(row, "PO_ID", 0, false));
                    unusedLabel.ID = Convert.ToInt32(clsGeneral.getColumnData(row, "ID", 0, false));
                    unusedLabel.LabelSource = clsGeneral.getColumnData(row, "LabelSource", string.Empty, false) as string;
                    unusedLabelList.Add(unusedLabel);


                }
            }
            return unusedLabelList;

        }
    }
}