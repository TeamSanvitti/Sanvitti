using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SV.Framework.Vendor
{
    public class eBayOperations
    {
        public static async Task<eBayOrderInfo>  GeteBayOrders(string url, eBayOrderRequest request, bool IsApi)
        {            
            eBayOrderInfo eBayOrderInfo = new eBayOrderInfo();
            if(IsApi)
                eBayOrderInfo = await ApiHelper.PostAsync<eBayOrderInfo, eBayOrderRequest>(request, url);
            else
                eBayOrderInfo = GeteBayOrderInfo(1);
            //eBayOrderInfo = await ApiHelper.GetAsync<eBayOrderInfo>(url, headers);
            //string responseJson = "";
            return eBayOrderInfo;
        }

        public static async Task<eBayOrderInfo> GeteBayOrders(string url, Dictionary<string, string> headers)
        {

            eBayOrderInfo eBayOrderInfo = new eBayOrderInfo();
          //  eBayOrderInfo = await ApiHelper.PostAsync<eBayOrderInfo>(url, headers);
            //eBayOrderInfo = await ApiHelper.GetAsync<eBayOrderInfo>(url, headers);

            eBayOrderInfo = GeteBayOrderInfo(1);



            //string responseJson = "";

            return eBayOrderInfo;
        }
        public static int eBayOrderInsertUpdate(int logID, eBayOrderInfo requestData, int UserID, out string errorMessage)
        {
            int returnValue = 0;
           
            errorMessage = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable orderdt = new DataTable();
            DataTable lineItemsdt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            orderdt = OrderTables(requestData, out lineItemsdt);
            //DataTable esnDT = ESNData(serviceOrder.SODetail);
            //string RequestData = clsGeneral.SerializeObject(request);
            try
            {
                objCompHash.Add("@LogID", logID);
                objCompHash.Add("@UserID", UserID);
                objCompHash.Add("@sveBayOrderType", orderdt);
                objCompHash.Add("@sveBayOrderLineItemsType", lineItemsdt);                

                arrSpFieldSeq = new string[] { "@LogID", "@UserID", "@sveBayOrderType", "@sveBayOrderLineItemsType" };
                object obj = db.ExecuteScalar(objCompHash, "sveBayOrderInsertUpdate", arrSpFieldSeq);
                if (obj != null)
                {
                    returnValue = Convert.ToInt32(obj);
                }
            }
            catch (Exception objExp)
            {

                errorMessage = objExp.Message;
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return returnValue;

        }
        private static DataTable OrderTables(eBayOrderInfo eBayOrderInfo, out DataTable lineItemDT)
        {
            DataTable dt = new DataTable();
            lineItemDT = new DataTable();


            lineItemDT.Columns.Add("orderId", typeof(System.String));
            lineItemDT.Columns.Add("Quantity", typeof(System.Int32));
            lineItemDT.Columns.Add("lineItemId", typeof(System.String));
            lineItemDT.Columns.Add("lineItemFulfillmentStatus", typeof(System.String));
            lineItemDT.Columns.Add("sku", typeof(System.String));
            lineItemDT.Columns.Add("Title", typeof(System.String));


            dt.Columns.Add("orderFulfillmentStatus", typeof(System.String));
            dt.Columns.Add("fulfillmentInstructionsType", typeof(System.String));
            dt.Columns.Add("minEstimatedDeliveryDate", typeof(System.DateTime));
            dt.Columns.Add("maxEstimatedDeliveryDate", typeof(System.DateTime));
            dt.Columns.Add("ebaySupportedFulfillment", typeof(System.Boolean));
            dt.Columns.Add("shippingCarrierCode", typeof(System.String));
            dt.Columns.Add("shippingServiceCode", typeof(System.String));
            dt.Columns.Add("shipTophoneNumber", typeof(System.String));
            dt.Columns.Add("shipTofullName", typeof(System.String));
            dt.Columns.Add("shipTostateOrProvince", typeof(System.String));
            dt.Columns.Add("shipTocity", typeof(System.String));
            dt.Columns.Add("shipTocountryCode", typeof(System.String));
            dt.Columns.Add("shipTopostalCode", typeof(System.String));
            dt.Columns.Add("shipToaddressLine1", typeof(System.String));
            dt.Columns.Add("shipToemail", typeof(System.String));
            dt.Columns.Add("orderId", typeof(System.String));
            dt.Columns.Add("lastModifiedDate", typeof(System.DateTime));
            dt.Columns.Add("creationDate", typeof(System.DateTime));

            DataRow row;
            DataRow lineItemrow;

            if (eBayOrderInfo != null && eBayOrderInfo.orders != null && eBayOrderInfo.orders.Count > 0)
            {
                foreach (eBayOrders item in eBayOrderInfo.orders)
                {
                    row = dt.NewRow();
                    row["orderFulfillmentStatus"] = item.orderFulfillmentStatus;
                    row["fulfillmentInstructionsType"] = item.fulfillmentStartInstructions[0].fulfillmentInstructionsType;
                    row["minEstimatedDeliveryDate"] = item.fulfillmentStartInstructions[0].minEstimatedDeliveryDate;
                    row["maxEstimatedDeliveryDate"] = item.fulfillmentStartInstructions[0].maxEstimatedDeliveryDate;
                    row["ebaySupportedFulfillment"] = item.fulfillmentStartInstructions[0].ebaySupportedFulfillment;
                    row["shippingCarrierCode"] = item.fulfillmentStartInstructions[0].shippingStep.shippingCarrierCode;
                    row["shippingServiceCode"] = item.fulfillmentStartInstructions[0].shippingStep.shippingServiceCode;
                    row["shipTophoneNumber"] = item.fulfillmentStartInstructions[0].shippingStep.shipTo.primaryPhone.phoneNumber;
                    row["shipTofullName"] = item.fulfillmentStartInstructions[0].shippingStep.shipTo.fullName;
                    row["shipTostateOrProvince"] = item.fulfillmentStartInstructions[0].shippingStep.shipTo.contactAddress.stateOrProvince;
                    row["shipTocity"] = item.fulfillmentStartInstructions[0].shippingStep.shipTo.contactAddress.city;
                    row["shipTocountryCode"] = item.fulfillmentStartInstructions[0].shippingStep.shipTo.contactAddress.countryCode;
                    row["shipTopostalCode"] = item.fulfillmentStartInstructions[0].shippingStep.shipTo.contactAddress.postalCode;
                    row["shipToaddressLine1"] = item.fulfillmentStartInstructions[0].shippingStep.shipTo.contactAddress.addressLine1;
                    row["shipToemail"] = item.fulfillmentStartInstructions[0].shippingStep.shipTo.email;
                    row["orderId"] = item.orderId;
                    row["lastModifiedDate"] = item.lastModifiedDate;
                    row["creationDate"] = item.creationDate;


                    dt.Rows.Add(row);
                    foreach (LineItems lineitem in item.lineItems)
                    {
                        lineItemrow = lineItemDT.NewRow();
                        lineItemrow["orderId"] = item.orderId;
                        lineItemrow["Quantity"] = lineitem.quantity;
                        lineItemrow["lineItemId"] = lineitem.lineItemId;
                        lineItemrow["lineItemFulfillmentStatus"] = lineitem.lineItemFulfillmentStatus;
                        lineItemrow["sku"] = lineitem.sku;
                        lineItemrow["Title"] = lineitem.title;
                        lineItemDT.Rows.Add(lineItemrow);
                    }
                }
            }
            return dt;
        }
        public static int eBayOrderRefreshLogInsert(eBayOrderInfo eBayOrderInfo, int UserID, out string errorMessage)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(eBayOrderInfo);
            string requestData = jsonString;
            int returnValue = 0;

            errorMessage = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable orderdt = new DataTable();
            DataTable lineItemsdt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            //DataTable esnDT = ESNData(serviceOrder.SODetail);
            //string RequestData = clsGeneral.SerializeObject(request);
            try
            {
                objCompHash.Add("@CreatedBy", UserID);
                objCompHash.Add("@LogData", jsonString);
                


                arrSpFieldSeq = new string[] { "@CreatedBy", "@LogData" };
                object obj = db.ExecuteScalar(objCompHash, "sveBayOrderRefreshLogInsert", arrSpFieldSeq);
                if(obj != null)
                {
                    returnValue = Convert.ToInt32(obj); 
                }

            }
            catch (Exception objExp)
            {
                errorMessage = objExp.Message;
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return returnValue;

        }
        public static eBayOrderInfo GeteBayOrderInfo(int logId)
        {
            eBayOrderInfo ebayOrderInfo = new eBayOrderInfo();// new FulfillmentEsNInfo();
            DBConnect db = new DBConnect();
            string logData = "";
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@LogID", logId);

                arrSpFieldSeq = new string[] { "@LogID" };
                dt = db.GetTableRecords(objCompHash, "sveBayOrderLogSelect", arrSpFieldSeq);
                if (dt != null && dt.Rows.Count > 0)
                {
                    
                    foreach (DataRow row in dt.Rows)
                    {

                        logData = Convert.ToString(clsGeneral.getColumnData(row, "LogData", string.Empty, false));
                        
                        ///JObject json = JObject.Parse(logData);
                        ebayOrderInfo = JsonConvert.DeserializeObject<eBayOrderInfo>(logData);

                    }
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                db = null;

            }


            return ebayOrderInfo;
        }

        public static eBayOrderInfo eBayOrderValidate(eBayOrderInfo requestData, out string errorMessage)
        {
            //int returnValue = 0;
            eBayOrderInfo ebayOrderInfoNew = new eBayOrderInfo();

            errorMessage = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable orderdt = new DataTable();
            DataTable lineItemsdt = new DataTable();
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            orderdt = OrderTables(requestData, out lineItemsdt);
            //DataTable esnDT = ESNData(serviceOrder.SODetail);
            //string RequestData = clsGeneral.SerializeObject(request);
            try
            {
                objCompHash.Add("@sveBayOrderType", orderdt);
                objCompHash.Add("@sveBayOrderLineItemsType", lineItemsdt);
                arrSpFieldSeq = new string[] { "@sveBayOrderType", "@sveBayOrderLineItemsType" };
                
                ds = db.GetDataSet(objCompHash, "sveBayOrderValidate", arrSpFieldSeq);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ebayOrderInfoNew = PopulateOrders(ds);
                    
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
            finally
            {
                db = null;

            }
            return ebayOrderInfoNew;
        }
        private static eBayOrderInfo PopulateOrders(DataSet ds)
        {
            eBayOrderInfo ebayOrderInfo = new eBayOrderInfo();
            List<eBayOrders> orders = new List<eBayOrders>();
            List<LineItems> lineItems = new List<LineItems>();
            eBayOrders eBayOrder = null;
            LineItems lineItem = null;
            List<FulfillmentStartInstructions> fulfillmentStartInstructions = new List<FulfillmentStartInstructions>();
            FulfillmentStartInstructions fulfillmentStartInstruction = null;
            ShippingStep shippingStep = null;
            ShipTo shipTo = null;
            PrimaryPhone primaryPhone  = null;
            ContactAddress contactAddress = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    lineItems = new List<LineItems>();
                    fulfillmentStartInstructions = new List<FulfillmentStartInstructions>();
                    fulfillmentStartInstruction = new FulfillmentStartInstructions();
                    shippingStep = new ShippingStep();
                    shipTo = new ShipTo();
                    primaryPhone = new PrimaryPhone();
                    contactAddress = new ContactAddress();
                    eBayOrder = new eBayOrders();
                    eBayOrder.creationDate = Convert.ToString(clsGeneral.getColumnData(row, "creationDate", string.Empty, false));
                    eBayOrder.orderFulfillmentStatus = Convert.ToString(clsGeneral.getColumnData(row, "orderFulfillmentStatus", string.Empty, false));
                    eBayOrder.orderId = Convert.ToString(clsGeneral.getColumnData(row, "orderId", string.Empty, false));
                    eBayOrder.lastModifiedDate = Convert.ToDateTime(clsGeneral.getColumnData(row, "lastModifiedDate", DateTime.Now, false));
                    eBayOrder.legacyOrderId = Convert.ToString(clsGeneral.getColumnData(row, "ErrorMessage", string.Empty, false));
                    //eBayOrder.fulfillmentStartInstructions[0].fulfillmentInstructionsType = Convert.ToString(clsGeneral.getColumnData(row, "fulfillmentInstructionsType", string.Empty, false));
                    
                    fulfillmentStartInstruction.ebaySupportedFulfillment = Convert.ToBoolean(clsGeneral.getColumnData(row, "ebaySupportedFulfillment", false, false));
                    fulfillmentStartInstruction.maxEstimatedDeliveryDate = Convert.ToDateTime(clsGeneral.getColumnData(row, "maxEstimatedDeliveryDate", DateTime.Now, false));
                    fulfillmentStartInstruction.minEstimatedDeliveryDate = Convert.ToDateTime(clsGeneral.getColumnData(row, "minEstimatedDeliveryDate", DateTime.Now, false));
                   
                    shippingStep.shippingCarrierCode = Convert.ToString(clsGeneral.getColumnData(row, "shippingCarrierCode", string.Empty, false));
                    shippingStep.shippingServiceCode = Convert.ToString(clsGeneral.getColumnData(row, "shippingServiceCode", string.Empty, false));
                    
                    primaryPhone.phoneNumber = Convert.ToString(clsGeneral.getColumnData(row, "shipTophoneNumber", string.Empty, false));
                    shipTo.fullName = Convert.ToString(clsGeneral.getColumnData(row, "shipTofullName", string.Empty, false));
                    shipTo.email = Convert.ToString(clsGeneral.getColumnData(row, "shipToemail", string.Empty, false));
                    contactAddress.addressLine1 = Convert.ToString(clsGeneral.getColumnData(row, "shipToaddressLine1", string.Empty, false));
                    contactAddress.city = Convert.ToString(clsGeneral.getColumnData(row, "shipTocity", string.Empty, false));
                    contactAddress.countryCode = Convert.ToString(clsGeneral.getColumnData(row, "shipTocountryCode", string.Empty, false));
                    contactAddress.postalCode = Convert.ToString(clsGeneral.getColumnData(row, "shipTopostalCode", string.Empty, false));
                    contactAddress.stateOrProvince = Convert.ToString(clsGeneral.getColumnData(row, "shipTostateOrProvince", string.Empty, false));
                    shipTo.primaryPhone = primaryPhone;
                    shipTo.contactAddress = contactAddress;

                    shippingStep.shipTo = shipTo;
                    fulfillmentStartInstruction.shippingStep = shippingStep;

                    fulfillmentStartInstructions.Add(fulfillmentStartInstruction);
                    eBayOrder.fulfillmentStartInstructions = fulfillmentStartInstructions;

                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow linitemsrow in ds.Tables[1].Select("orderId = '" + eBayOrder.orderId +"'"  ))
                        {
                            lineItem = new LineItems();
                            lineItem.lineItemFulfillmentStatus = Convert.ToString(clsGeneral.getColumnData(linitemsrow, "lineItemFulfillmentStatus", string.Empty, false));
                            lineItem.listingMarketplaceId = Convert.ToString(clsGeneral.getColumnData(linitemsrow, "ErrorMessage", string.Empty, false));
                            lineItem.title = Convert.ToString(clsGeneral.getColumnData(linitemsrow, "title", string.Empty, false));
                            lineItem.sku = Convert.ToString(clsGeneral.getColumnData(linitemsrow, "sku", string.Empty, false));
                            lineItem.lineItemId = Convert.ToInt64(clsGeneral.getColumnData(linitemsrow, "lineItemId", 0, false));
                            lineItem.quantity = Convert.ToInt32(clsGeneral.getColumnData(linitemsrow, "quantity", 0, false));
                            lineItems.Add(lineItem);
                        }
                    }
                    eBayOrder.lineItems = lineItems;
                    orders.Add(eBayOrder);


                }
            }
            ebayOrderInfo.orders = orders;


            return ebayOrderInfo;
        }




    }
}
