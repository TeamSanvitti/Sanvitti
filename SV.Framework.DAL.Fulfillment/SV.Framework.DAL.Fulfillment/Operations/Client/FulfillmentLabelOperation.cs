using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.IO;
//using iTextSharp.text.pdf;
//using iTextSharp.text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class FulfillmentLabelOperation : BaseCreateInstance
    {
        public  List<FulfillmentLabel> GetFulfillments(string fromDate, string toDate, string poNum, int statusId, string ShipVia, string poType, int companyID)
        {
            
            List<FulfillmentLabel> poList = default;//null;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objParams = new Hashtable();

                DataTable dataTable = default;// new DataTable();

                try
                {

                    objParams.Add("@CompanyID", companyID);
                    objParams.Add("@Po_Num", poNum);
                    objParams.Add("@From_Date", string.IsNullOrWhiteSpace(fromDate) ? null : fromDate);
                    objParams.Add("@To_Date", string.IsNullOrWhiteSpace(toDate) ? null : toDate);
                    objParams.Add("@StatusID", statusId);
                    objParams.Add("@ShipVia", ShipVia);
                    objParams.Add("@POType", poType);

                    arrSpFieldSeq =
                    new string[] { "@CompanyID", "@Po_Num", "@From_Date", "@To_Date", "@StatusID", "@ShipVia", "@POType" };


                    dataTable = db.GetTableRecords(objParams, "Av_PurchaseOrder_Batch_Select", arrSpFieldSeq);
                    poList = PopulatePOs(dataTable);

                }

                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);
                    //throw ex;

                }

                finally
                {

                   // db.DBClose();
                    objParams = null;
                    arrSpFieldSeq = null;

                }
            }
            return poList;
        }

        public  List<FulfillmentLabel> GetLabels(string PoNums, int companyID)
        {
            //DBConnect db = new DBConnect();
            List<FulfillmentLabel> poList = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;

                Hashtable objParams = new Hashtable();

                DataTable dataTable = default;// new DataTable();

                try
                {

                    objParams.Add("@CompanyID", companyID);
                    objParams.Add("@Po_Nums", PoNums);

                    arrSpFieldSeq =
                    new string[] { "@CompanyID", "@Po_Nums" };


                    dataTable = db.GetTableRecords(objParams, "Av_PurchaseOrder_BatchLabel_Select", arrSpFieldSeq);
                    poList = PopulatePOLabels(dataTable);

                }

                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);
                   // throw ex;

                }

                finally
                {

                   // db.DBClose();
                    objParams = null;
                    arrSpFieldSeq = null;

                }
            }
            return poList;
        }


        private  List<FulfillmentLabel> PopulatePOLabels(DataTable dataTable)
        {
            List<FulfillmentLabel> poList = default;//new List<FulfillmentLabel>();
            FulfillmentLabel fulfillmentLabel = default;//null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                poList = new List<FulfillmentLabel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    fulfillmentLabel = new FulfillmentLabel();
                    fulfillmentLabel.ShipMethod = clsGeneral.getColumnData(dataRow, "ShipByCode", string.Empty, false) as string;
                    fulfillmentLabel.ShippingLabelImage = clsGeneral.getColumnData(dataRow, "ShippingLabel", string.Empty, false) as string;
                    fulfillmentLabel.ShipPackage = clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false) as string;
                    poList.Add(fulfillmentLabel);


                }
            }
            return poList;
        }

        private  List<FulfillmentLabel> PopulatePOs(DataTable dataTable)
        {
            List<FulfillmentLabel> poList = default;//new List<FulfillmentLabel>();
            FulfillmentLabel fulfillmentLabel = default;//null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                poList = new List<FulfillmentLabel>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    fulfillmentLabel = new FulfillmentLabel();
                    fulfillmentLabel.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    fulfillmentLabel.FulfillmentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.MinValue, false)).ToShortDateString();
                    fulfillmentLabel.ContactName = clsGeneral.getColumnData(dataRow, "Contact_Name", string.Empty, false) as string;
                    fulfillmentLabel.ContactPhone = clsGeneral.getColumnData(dataRow, "Contact_Phone", string.Empty, false) as string;
                    fulfillmentLabel.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.MinValue, false)).ToShortDateString();
                    fulfillmentLabel.ShipMethod = clsGeneral.getColumnData(dataRow, "Ship_Via", string.Empty, false) as string;
                   // fulfillmentLabel.ShippingLabelImage = clsGeneral.getColumnData(dataRow, "ShippingLabel", string.Empty, false) as string;
                    fulfillmentLabel.Status = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    fulfillmentLabel.StoreID = clsGeneral.getColumnData(dataRow, "Store_ID", string.Empty, false) as string;
                    fulfillmentLabel.StreetAddress1 = clsGeneral.getColumnData(dataRow, "ShipTo_Address", string.Empty, false) as string;
                    fulfillmentLabel.StreetAddress2 = clsGeneral.getColumnData(dataRow, "ShipTo_Address2", string.Empty, false) as string;
                    fulfillmentLabel.ShipToCity = clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, false) as string;
                    fulfillmentLabel.ShipToState = clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false) as string;
                    fulfillmentLabel.ShipToZip = clsGeneral.getColumnData(dataRow, "ShipTo_Zip", string.Empty, false) as string;
                    fulfillmentLabel.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));

                    fulfillmentLabel.ShippingWeight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));

                    fulfillmentLabel.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    fulfillmentLabel.ShipPackage = clsGeneral.getColumnData(dataRow, "ShipPackage", string.Empty, false) as string;
                    fulfillmentLabel.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;


                    //fulfillmentLabel.LogID = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "LogID", 0, false));
                    //fulfillmentLabel. = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "ExceptionOccured", false, false));
                    //log.TimeDifference = Convert.ToInt64(clsGeneral.getColumnData(dataRow, "TimeDifference", 0, false));
                    poList.Add(fulfillmentLabel);

                }
            }
            return poList;
        }

    }
}