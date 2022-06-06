using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SV.Framework.Fulfillment
{
    public class FulfillmentOrderASNOperation
    {
        public static List<FulfillmentOrderASN> GetPurchaseOrderASNFile(string POIDs)
        {
            List<FulfillmentOrderASN> poDetails = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
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

            }
            finally
            {
                db = null;

            }
            return poDetails;
        }
        private static List<FulfillmentOrderASN> PopulateFulfillmentASN(DataTable dt)
        {
            string PO = "", FO = "";
            List<FulfillmentOrderASN> asnList = null;// new List<PurchaseOrderShipmentDB>();
            FulfillmentOrderASN asnInfo = null;
            if (dt != null && dt.Rows.Count > 0)
            {
                asnList = new List<FulfillmentOrderASN>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    asnInfo = new FulfillmentOrderASN();

                    asnInfo.ARZM = clsGeneral.getColumnData(dataRow, "ARZM", string.Empty, false) as string;
                    PO = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    FO = clsGeneral.getColumnData(dataRow, "FactOrderNumber", string.Empty, false) as string;
                    
                    //string[] poParts = PO.Split('-');
                    //if(poParts.Length > 1)
                    //{
                    //    FO = PO;
                    //    PO = poParts[0];                        
                    //}

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
                    asnInfo.FileSequence = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "FileSequence", 1, false));

                    asnInfo.Weight = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "ShippingWeight", 0, false));
                    asnInfo.UEDF_FileName = clsGeneral.getColumnData(dataRow, "UEDF_FileName", string.Empty, false) as string;
                    asnInfo.UEDF_DateTime = Convert.ToString(clsGeneral.getColumnData(dataRow, "UEDFCSTDateTime", "", false));
                    asnList.Add(asnInfo);
                }
            }
            return asnList;
        }


    }
    public class FulfillmentOrderASN
    {
        public int FileSequence { get; set; }
        public string ARZM { get; set; }
        public string PO { get; set; }
        public string FO { get; set; }
        public string Line { get; set; }
        public string Model { get; set; }
        public string ProductDescription { get; set; }
        public int Qty { get; set; }
        public int Cartons { get; set; }
        public string DateShipped { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Carrier { get; set; }
        public string TrackingNumber { get; set; }
        public string SupplierName { get; set; }
        public string SupplierAddress { get; set; }
        public string SupplierCity { get; set; }
        public string SupplierState { get; set; }
        public int Pallets { get; set; }
        public decimal Weight { get; set; }
        public string UEDF_FileName { get; set; }
        public string UEDF_DateTime { get; set; }

        

    }

}
