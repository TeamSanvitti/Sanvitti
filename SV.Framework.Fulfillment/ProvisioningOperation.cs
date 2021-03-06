using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SV.Framework.Fulfillment
{
    public class ProvisioningOperation
    {
        public static List<POProvisioning> GetPurchaseOrderProvisioning(int CompanyID, string FulfillmentNumber, string FromDate, string ToDate)
        {
            
            List<POProvisioning> poDetails = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@FulfillmentNumber", FulfillmentNumber);
                objCompHash.Add("@FromDate", !string.IsNullOrEmpty(FromDate) ? FromDate : null);
                objCompHash.Add("@ToDate", !string.IsNullOrEmpty(ToDate) ? ToDate : null);


                arrSpFieldSeq = new string[] { "@CompanyID", "@FulfillmentNumber", "@FromDate", "@ToDate" };
                dt = db.GetTableRecords(objCompHash, "av_Purchaseorder_Provisioning_Select", arrSpFieldSeq);

                poDetails = PopulateFulfillmentDetail(dt);

            }


            catch (Exception ex)
            {

            }
            finally
            {
                db = null;

            }


            return poDetails;
        }
        public static POProvisioningInfo GetPurchaseOrderProvisioningView(int poID)
        {
            POProvisioningInfo pOProvisioningInfo = null;
            List<POProvisioning> poDetails = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POID", poID);
               

                arrSpFieldSeq = new string[] { "@POID" };
                ds = db.GetDataSet(objCompHash, "av_Purchaseorder_Provisioning_View", arrSpFieldSeq);

                pOProvisioningInfo = PopulatePOProvisioningInfo(ds);

            }


            catch (Exception ex)
            {

            }
            finally
            {
                db = null;

            }


            return pOProvisioningInfo;
        }
        private static POProvisioningInfo PopulatePOProvisioningInfo(DataSet ds)
        {
            POProvisioningInfo pOProvisioningInfo = new POProvisioningInfo();
            List<FulfillmentHeader> FulfillmentHeaders = null;
            List<ProvisioningDetail> poDetails = null;// new List<PurchaseOrderShipmentDB>();
            ProvisioningDetail poDetail = null;
            FulfillmentHeader fulfillmentHeader = null;
            if (ds != null && ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
            {
                poDetails = new List<ProvisioningDetail>();
                FulfillmentHeaders = new List<FulfillmentHeader>();
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    //poDetail = new ProvisioningDetail();
                    fulfillmentHeader = new FulfillmentHeader();
                    fulfillmentHeader.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    fulfillmentHeader.POStatus = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    fulfillmentHeader.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    fulfillmentHeader.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    fulfillmentHeader.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                   // poDetail.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));
                    fulfillmentHeader.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));

                    fulfillmentHeader.PODate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", string.Empty, false)).ToString("MM/dd/yyyy");
                    fulfillmentHeader.ShipTo_Date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", string.Empty, false)).ToString("MM/dd/yyyy");

                    FulfillmentHeaders.Add(fulfillmentHeader);

                }
                if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[1].Rows)
                    {
                        poDetail = new ProvisioningDetail();
                        poDetail.ContainerID = clsGeneral.getColumnData(dataRow, "ContainerID", string.Empty, false) as string;
                        poDetail.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                        poDetail.HEX = clsGeneral.getColumnData(dataRow, "MeidHex", string.Empty, false) as string;
                        poDetail.DEC = clsGeneral.getColumnData(dataRow, "MeidDec", string.Empty, false) as string;
                        poDetail.Location = clsGeneral.getColumnData(dataRow, "Location", string.Empty, false) as string;
                        poDetail.BoxID = clsGeneral.getColumnData(dataRow, "BoxNumber", string.Empty, false) as string;
                        poDetail.SerialNumber = clsGeneral.getColumnData(dataRow, "SerialNumber", string.Empty, false) as string;
                        // poDetail.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));

                        poDetails.Add(poDetail);

                    }
                }
            }
            pOProvisioningInfo.FulfillmentHeaders = FulfillmentHeaders;
            pOProvisioningInfo.ProvisioningDetails = poDetails;
            return pOProvisioningInfo;
        }

        private static List<POProvisioning> PopulateFulfillmentDetail(DataTable dt)
        {

            List<POProvisioning> poDetails = null;// new List<PurchaseOrderShipmentDB>();
            POProvisioning poDetail = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                poDetails = new List<POProvisioning>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    poDetail = new POProvisioning();                   
                    
                    poDetail.FulfillmentNumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    poDetail.POStatus = clsGeneral.getColumnData(dataRow, "StatusText", string.Empty, false) as string;
                    poDetail.POID = Convert.ToInt32( clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));
                    poDetail.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                    poDetail.PODate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "PO_Date", string.Empty, false));//.ToString("MM/dd/yyyy");
                    poDetail.ShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", string.Empty, false));//.ToString("MM/dd/yyyy");
                    poDetail.RequestedShipDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "RequestedShipDate", string.Empty, false));//.ToString("MM/dd/yyyy");
                    poDetail.ProvisioningDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CreateDate", string.Empty, false));//.ToString("MM/dd/yyyy");
                    
                    poDetails.Add(poDetail);

                }
            }

            return poDetails;
        }

    }
}
