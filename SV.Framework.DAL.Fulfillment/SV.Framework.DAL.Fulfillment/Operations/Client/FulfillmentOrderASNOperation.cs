using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class FulfillmentOrderASNOperation : BaseCreateInstance
    {
        public  List<FulfillmentOrderASN> GetPurchaseOrderASNFile(string POIDs)
        {
            List<FulfillmentOrderASN> poDetails = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POIDs", POIDs);
                    arrSpFieldSeq = new string[] { "@POIDs" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrder_ASNFile", arrSpFieldSeq);
                    poDetails = PopulateFulfillmentASN(dt);
                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);
                }
                finally
                {
                    //db = null;

                }
            }
            return poDetails;
        }
        private  List<FulfillmentOrderASN> PopulateFulfillmentASN(DataTable dt)
        {
            string PO = "", FO = "";
            List<FulfillmentOrderASN> asnList = default;//null;// new List<PurchaseOrderShipmentDB>();
            FulfillmentOrderASN asnInfo = default;//null;
            if (dt != null && dt.Rows.Count > 0)
            {
                asnList = new List<FulfillmentOrderASN>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    asnInfo = new FulfillmentOrderASN();

                    asnInfo.ARZM = clsGeneral.getColumnData(dataRow, "ARZM", string.Empty, false) as string;
                    PO = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    string[] poParts = PO.Split('-');
                    if(poParts.Length > 1)
                    {
                        FO = PO;
                        PO = poParts[0];                        
                    }

                    asnInfo.PO = PO;
                    asnInfo.FO = FO;

                    asnInfo.Line = clsGeneral.getColumnData(dataRow, "Line", string.Empty, false) as string;
                    asnInfo.Model = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    asnInfo.ProductDescription = clsGeneral.getColumnData(dataRow, "SKUName", string.Empty, false) as string;
                    asnInfo.Qty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                    asnInfo.Cartons = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TotalContainers", 0, false));
                    asnInfo.DateShipped = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.Now, false)).ToString("MM/dd/yyyy");
                    asnInfo.City = clsGeneral.getColumnData(dataRow, "ShipTo_City", string.Empty, false) as string;
                    asnInfo.State = clsGeneral.getColumnData(dataRow, "ShipTo_State", string.Empty, false) as string;

                    asnInfo.Carrier = clsGeneral.getColumnData(dataRow, "Carrier", string.Empty, false) as string;
                    asnInfo.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    asnInfo.SupplierName = clsGeneral.getColumnData(dataRow, "SupplierName", string.Empty, false) as string;
                    asnInfo.SupplierAddress = clsGeneral.getColumnData(dataRow, "SupplierAddress", string.Empty, false) as string;
                    asnInfo.SupplierCity = clsGeneral.getColumnData(dataRow, "SupplierCity", string.Empty, false) as string;
                    asnInfo.SupplierState = clsGeneral.getColumnData(dataRow, "SupplierState", string.Empty, false) as string;
                    asnInfo.Pallets = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TotalPallets", 0, false));

                    asnInfo.Weight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));
                    asnInfo.UEDF_FileName = clsGeneral.getColumnData(dataRow, "UEDF_FileName", string.Empty, false) as string;
                    asnInfo.UEDF_DateTime = Convert.ToString(clsGeneral.getColumnData(dataRow, "UEDFCSTDateTime", "", false));
                    asnList.Add(asnInfo);
                }
            }
            return asnList;
        }


    }
    
}
