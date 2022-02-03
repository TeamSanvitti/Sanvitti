using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;

namespace avii.Classes
{
    public class FulfillmentShippingLineItemOperation
    {

        public static List<FulfillmentShippingLineItem> GetShippingLineItems(int POID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<FulfillmentShippingLineItem> skuList = new List<FulfillmentShippingLineItem>();
            try
            {
                objCompHash.Add("@POID", POID);

                arrSpFieldSeq = new string[] { "@POID" };
                dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderShippingWithLineItems", arrSpFieldSeq);
                skuList = PopulateShippingLineItems(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return skuList;

        }
        public static int CreatePurchaseOrderShippingLineItems(FulfillmentShippingLine model)
        {
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            int returnValue = 0;
            DataTable dt = LoadPOLineitems(model.LineItems);
            try
            {
                objCompHash.Add("@PO_ID", model.POID);
                objCompHash.Add("@ShipByID", model.ShipById);
                objCompHash.Add("@TrackingNumber", model.TrackingNumber);
                objCompHash.Add("@ShipDate", model.ShipDate);
                objCompHash.Add("@Comments", model.Comments);
                objCompHash.Add("@POShippingLineItem", dt);
                arrSpFieldSeq = new string[] { "@PO_ID", "@ShipByID", "@TrackingNumber", "@ShipDate", "@Comments", "@POShippingLineItem"};
                db.ExeCommand(objCompHash, "av_PurchaseOrderShipping_LineItemInsert", arrSpFieldSeq);
                returnValue = 1;
            }
            catch (Exception objExp)
            {
                //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return returnValue;
        }

        private static List<FulfillmentShippingLineItem> PopulateShippingLineItems(DataTable dt)
        {
            
            
            FulfillmentShippingLineItem skuInfo = null;
            List<FulfillmentShippingLineItem> skuList = new List<FulfillmentShippingLineItem>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    
                            skuInfo = new FulfillmentShippingLineItem();
                            skuInfo.SKU = clsGeneral.getColumnData(row, "SKU", string.Empty, false) as string;
                            skuInfo.Quantity = Convert.ToInt32(clsGeneral.getColumnData(row, "Qty", 0, false));
                            skuInfo.PODID = Convert.ToInt32(clsGeneral.getColumnData(row, "POD_ID", 0, false));

                            skuList.Add(skuInfo);
                        
                   
                }
            }
            return skuList;

        }
        private static FulfillmentShippingLine PopulateShippingLineItem(DataSet dataset)
        {
            // List<EsnHeaders> headerList = new List<EsnHeaders>();
            FulfillmentShippingLine shppingDetail = new FulfillmentShippingLine();
            FulfillmentShippingLineItem skuInfo = null;
            List<FulfillmentShippingLineItem> skuList = null;
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataset.Tables[0].Rows)
                {
                    skuList = new List<FulfillmentShippingLineItem>();

                    //objESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    //objESN.MslNumber = clsGeneral.getColumnData(dataRow, "MSL", string.Empty, false) as string;
                    shppingDetail.ShipDate = clsGeneral.getColumnData(dataRow, "ShipDate", string.Empty, false) as string;
                    shppingDetail.ShipById = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShipById", 0, false));
                    shppingDetail.POID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "POID", 0, false));
                    shppingDetail.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;
                    
                    if (dataset.Tables.Count > 1 && dataset.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataset.Tables[1].Rows)
                        {
                            skuInfo = new FulfillmentShippingLineItem();
                            skuInfo.SKU = clsGeneral.getColumnData(row, "SKU", string.Empty, false) as string;
                            skuInfo.Quantity = Convert.ToInt32(clsGeneral.getColumnData(row, "Quantity", 0, false));
                            skuInfo.PODID = Convert.ToInt32(clsGeneral.getColumnData(row, "POD_ID", 0, false));
                            
                            skuList.Add(skuInfo);
                        }
                    }
                    shppingDetail.LineItems = skuList;
                    // headerList.Add(objESN);
                }
            }
            return shppingDetail;

        }
        private static DataTable LoadPOLineitems(List<FulfillmentShippingLineItem> lineItems)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("POD_ID", typeof(System.Int32));
            dt.Columns.Add("Quantity", typeof(System.Int32));



            DataRow row;
            if (lineItems != null && lineItems.Count > 0)
            {
                foreach (FulfillmentShippingLineItem item in lineItems)
                {
                    row = dt.NewRow();
                    row["POD_ID"] = item.PODID;
                    row["Quantity"] = item.Quantity;


                    dt.Rows.Add(row);
                }
            }


            return dt;
        }

    }
}