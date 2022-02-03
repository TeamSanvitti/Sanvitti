using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class SKUPricesOperations
    {
        public static List<ForecastInfo> GetForecastReport(int companyID, DateTime fromDate, DateTime toDate, string sku)
        {
            DBConnect db = new DBConnect();
            List<ForecastInfo> forecastList = new List<ForecastInfo>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate.ToShortDateString() == "1/1/0001" ? null : fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString() == "1/1/0001" ? null : toDate.ToShortDateString());
                objParams.Add("@SKU", sku);
                
                arrSpFieldSeq =
                new string[] { "@CompanyID", "@FromDate", "@ToDate", "@SKU" };


                dataTable = db.GetTableRecords(objParams, "AV_CustomerForecast_Report", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        ForecastInfo forecast = new ForecastInfo();

                        forecast.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        forecast.ForecastNumber = clsGeneral.getColumnData(dataRow, "ForecastNumber", string.Empty, false) as string;
                        forecast.ForecastDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ForecastDate", 0, false));
                        forecast.Status = clsGeneral.getColumnData(dataRow, "Statustext", string.Empty, false) as string;
                        forecast.ForecastID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ForecastID", 0, false));
                        forecast.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        forecast.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                        forecast.Price = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "Price", 0, false));
                        forecast.TotalPrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "TotalPrice", 0, false));
                        
                        forecastList.Add(forecast);


                    }
                }
            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return forecastList;
        }

        public static List<ForecastLineItem> GetForecastDetail(int forecastID)
        {
            DBConnect db = new DBConnect();
            List<ForecastLineItem> forecastLineItems = new List<ForecastLineItem>();
            ForecastLineItem forecastLineItem = null;

            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();
            
            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@ForecastID", forecastID);

                arrSpFieldSeq =
                new string[] { "@ForecastID" };
                
                dataTable = db.GetTableRecords(objParams, "AV_FulfillmentForecast_Detail_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow2 in dataTable.Rows)
                    {

                        forecastLineItem = new ForecastLineItem();
                        forecastLineItem.SKU = clsGeneral.getColumnData(dataRow2, "sku", string.Empty, false) as string;
                        forecastLineItem.Comments = clsGeneral.getColumnData(dataRow2, "Comments", string.Empty, false) as string;

                        
                        forecastLineItem.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "Qty", 0, false));
                        forecastLineItem.Price = Convert.ToDouble(clsGeneral.getColumnData(dataRow2, "Price", 0, false));
                        forecastLineItem.TotalPrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow2, "TotalPrice", 0, false));
                        forecastLineItem.LineItemStatus = (ForecastStatuses)Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "StatusID", 1, false));
                        forecastLineItems.Add(forecastLineItem);

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return forecastLineItems;
        }

        public static List<ForecastLog> GetFulfillmentForecastLog(int forecastID)
        {
            DBConnect db = new DBConnect();
            List<ForecastLog> forecastlogList = new List<ForecastLog>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@ForecastID", forecastID);
                
                arrSpFieldSeq = new string[] { "@ForecastID"};


                dataTable = db.GetTableRecords(objParams, "AV_Forecast_Log", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        ForecastLog forecastLog = new ForecastLog();
                        forecastLog.ForecastSource = clsGeneral.getColumnData(dataRow, "FCSource", string.Empty, false) as string;
                        forecastLog.UserName = clsGeneral.getColumnData(dataRow, "userName", string.Empty, false) as string;
                        forecastLog.Comments = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                        forecastLog.ForecastStatus = clsGeneral.getColumnData(dataRow, "Statustext", string.Empty, false) as string;
                        //forecast.ForecastID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ForecastID", 0, false));
                        forecastLog.ForecastDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ForecastDate", DateTime.MaxValue, false));
                        forecastLog.ModifiedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ModifiedDate", DateTime.MaxValue, false));
                        forecastlogList.Add(forecastLog);


                    }
                }
            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return forecastlogList;
        }

        public static FulfillmentForecast GetFulfillmentForecastInfo(int forecastID)
        {
            DBConnect db = new DBConnect();
            FulfillmentForecast forecast = new FulfillmentForecast();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();
            DataSet ds = new DataSet();

            DataTable dataTable = new DataTable();

            try
            {

                //objParams.Add("@CompanyID", companyID);
                //objParams.Add("@FromDate", fromDate.ToShortDateString() == "1/1/0001" ? null : fromDate.ToShortDateString());
                //objParams.Add("@ToDate", toDate.ToShortDateString() == "1/1/0001" ? null : toDate.ToShortDateString());
                //objParams.Add("@ForecastNumber", forecastNumber);
                //objParams.Add("@SKU", sku);
                //objParams.Add("@StatusID", statusID);
                objParams.Add("@ForecastID", forecastID);

                arrSpFieldSeq =
                new string[] {  "@ForecastID" };


                ds = db.GetDataSet(objParams, "AV_FulfillmentForecast_Select", arrSpFieldSeq);
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        //FulfillmentForecast forecast = new FulfillmentForecast();
                        forecast.ForecastNumber = clsGeneral.getColumnData(dataRow, "ForecastNumber", string.Empty, false) as string;
                        forecast.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        forecast.Status = clsGeneral.getColumnData(dataRow, "Statustext", string.Empty, false) as string;
                        forecast.ForecastID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ForecastID", 0, false));
                        forecast.ForecastDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ForecastDate", 0, false));
                        forecast.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        //forecastList.Add(forecast);

                        if (ds != null && ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        {
                            List<ForecastLineItem> forecastLineItems = new List<ForecastLineItem>();
                            ForecastLineItem forecastLineItem = null;
                            foreach (DataRow dataRow2 in ds.Tables[1].Rows)
                            {

                                forecastLineItem = new ForecastLineItem();
                                forecastLineItem.SKU = clsGeneral.getColumnData(dataRow2, "sku", string.Empty, false) as string;
                                forecastLineItem.Quantity = Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "Qty", 0, false));
                                forecastLineItem.Price = Convert.ToDouble(clsGeneral.getColumnData(dataRow2, "Price", 0, false));
                                forecastLineItem.TotalPrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow2, "TotalPrice", 0, false));
                                forecastLineItem.LineItemStatus = (ForecastStatuses)Convert.ToInt32(clsGeneral.getColumnData(dataRow2, "StatusID", 1, false));
                                forecastLineItem.Comments = clsGeneral.getColumnData(dataRow2, "Comments", string.Empty, false) as string;

                                forecastLineItems.Add(forecastLineItem);

                            }
                            forecast.ForecastLineItems = forecastLineItems;

                        }


                    }
                }
            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return forecast;
        }

        public static List<FulfillmentForecast> GetFulfillmentForecast(int companyID, DateTime fromDate, DateTime toDate, string forecastNumber, string sku, int statusID)
        {
            DBConnect db = new DBConnect();
            List<FulfillmentForecast> forecastList = new List<FulfillmentForecast>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();

            DataTable dataTable = new DataTable();

            try
            {

                objParams.Add("@CompanyID", companyID);
                objParams.Add("@FromDate", fromDate.ToShortDateString() == "1/1/0001" ? null : fromDate.ToShortDateString());
                objParams.Add("@ToDate", toDate.ToShortDateString() == "1/1/0001" ? null : toDate.ToShortDateString());
                objParams.Add("@ForecastNumber", forecastNumber);
                objParams.Add("@SKU", sku);
                objParams.Add("@StatusID", statusID);
                objParams.Add("@ForecastID", 0);

                arrSpFieldSeq =
                new string[] { "@CompanyID", "@FromDate", "@ToDate", "@ForecastNumber", "@SKU", "@StatusID", "@ForecastID" };


                dataTable = db.GetTableRecords(objParams, "AV_FulfillmentForecast_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        FulfillmentForecast forecast = new FulfillmentForecast();
                        forecast.ForecastNumber = clsGeneral.getColumnData(dataRow, "ForecastNumber", string.Empty, false) as string;
                        forecast.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        forecast.Status = clsGeneral.getColumnData(dataRow, "Statustext", string.Empty, false) as string;
                        forecast.ForecastID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ForecastID", 0, false));
                        forecast.ForecastDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ForecastDate", 0, false));
                        forecastList.Add(forecast);


                    }
                }
            }

            catch (Exception ex)
            {

                throw ex;

            }

            finally
            {

                db.DBClose();
                objParams = null;
                arrSpFieldSeq = null;

            }
            return forecastList;
        }

        public static List<ForecastComments> GetForecastComments(int forecastID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<ForecastComments> forecastCommentsList = null;
            try
            {
                objCompHash.Add("@ForecastID", forecastID);

                arrSpFieldSeq = new string[] { "@ForecastID" };
                dt = db.GetTableRecords(objCompHash, "av_Forecast_Comments_select", arrSpFieldSeq);
                forecastCommentsList = PopulateForecastComments(dt);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
            }

            return forecastCommentsList;
        }
        public static List<CompanySKUno> GetCustomerForecastSKUList(int companyID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<CompanySKUno> skuList = null;
            try
            {
                objCompHash.Add("@CompanyID", companyID);

                arrSpFieldSeq = new string[] { "@CompanyID" };
                dt = db.GetTableRecords(objCompHash, "av_Customer_Forecast_SKU_select", arrSpFieldSeq);
                skuList = PopulateSKU(dt);

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

        public static bool SKUPriceReject(List<SKUPrices> skuPriceList, int companyID, int userID, bool isProduct)
        {
            bool returnValue = false;
            DBConnect db = new DBConnect();
            string skuXML = clsGeneral.SerializeObject(skuPriceList);
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            //int retIntVal = 0;
            try
            {
                objCompHash.Add("@XMLDATA", skuXML);
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@IsProduct", isProduct);


                arrSpFieldSeq = new string[] { "@XMLDATA", "@CompanyID", "@UserID", "@IsProduct" };
                db.ExeCommand(objCompHash, "av_Customer_SKU_Pricing_Reject", arrSpFieldSeq);
                //db.ExeCommand(objCompHash, "Av_SIM_InsertUpdate", arrSpFieldSeq, "@poErrorMessage", out errorMessage);
                returnValue = true;
                //if (errorMessage == string.Empty)
                //    returnValue = true;
                //else
                //    returnValue = false;
            }
            catch (Exception objExp)
            {
                returnValue = false;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return returnValue;
        }
        
        public static bool SKUPriceApprove(List<SKUPrices> skuPriceList, int companyID, int userID, bool isProduct)
        {
            bool returnValue = false;
            DBConnect db = new DBConnect();
            string skuXML = clsGeneral.SerializeObject(skuPriceList);
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            //int retIntVal = 0;
            try
            {
                objCompHash.Add("@XMLDATA", skuXML);
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@IsProduct", isProduct);


                arrSpFieldSeq = new string[] { "@XMLDATA", "@CompanyID", "@UserID", "@IsProduct" };
                db.ExeCommand(objCompHash, "av_Customer_SKU_Pricing_Approve", arrSpFieldSeq);
                //db.ExeCommand(objCompHash, "Av_SIM_InsertUpdate", arrSpFieldSeq, "@poErrorMessage", out errorMessage);
                returnValue = true;
                //if (errorMessage == string.Empty)
                //    returnValue = true;
                //else
                //    returnValue = false;
            }
            catch (Exception objExp)
            {
                returnValue = false;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return returnValue;
        }
        public static bool FulfillmentForecastDelete(int forecastID, int userID, string fcSource)
        {
            bool returnValue = false;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            //int retIntVal = 0;
            try
            {

                objCompHash.Add("@ForecastID", forecastID);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@FCSource", fcSource);

                arrSpFieldSeq = new string[] { "@ForecastID", "@UserID", "@FCSource" };
                db.ExeCommand(objCompHash, "av_FulfillmentForecast_Delete", arrSpFieldSeq);
                //db.ExeCommand(objCompHash, "", arrSpFieldSeq, "@poErrorMessage", out errorMessage);
                returnValue = true;
                //if (errorMessage == string.Empty)
                //    returnValue = true;
                //else
                //    returnValue = false;
            }
            catch (Exception objExp)
            {
                returnValue = false;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return returnValue;
        }
        
        public static bool FulfillmentForecastInsertUpdate(FulfillmentForecast forecastInfo, int companyID, int userID, string fcSource, bool isAdmin, int statusID, out string forecastNumber)
        {
            forecastNumber = string.Empty;
            bool returnValue = false;
            DBConnect db = new DBConnect();
            string forecastXML = clsGeneral.SerializeObject(forecastInfo);
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            //int retIntVal = 0;
            try
            {
                objCompHash.Add("@XMLDATA", forecastXML);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@FCSource", fcSource);
                objCompHash.Add("@IsAdmin", isAdmin);
                objCompHash.Add("@StatusID", statusID);

                arrSpFieldSeq = new string[] { "@XMLDATA", "@UserID", "@CompanyID", "@FCSource", "@IsAdmin", "@StatusID" };
                db.ExeCommand(objCompHash, "av_FulfillmentForecast_InsertUpdate", arrSpFieldSeq, "@ForecastNumber", out forecastNumber);
                //db.ExeCommand(objCompHash, "", arrSpFieldSeq, "@poErrorMessage", out errorMessage);
                returnValue = true;
                //if (errorMessage == string.Empty)
                //    returnValue = true;
                //else
                //    returnValue = false;
            }
            catch (Exception objExp)
            {
                returnValue = false;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return returnValue;
        }
        
        public static bool SKUPriceInsertUpdate(List<SKUPrices> skuPriceList, int companyID, int userID)
        {
            bool returnValue = false;
            DBConnect db = new DBConnect();
            string skuXML = clsGeneral.SerializeObject(skuPriceList);
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            //int retIntVal = 0;
            try
            {
                objCompHash.Add("@XMLDATA", skuXML);
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@UserID", userID);


                arrSpFieldSeq = new string[] { "@XMLDATA", "@CompanyID", "@UserID" };
                db.ExeCommand(objCompHash, "av_Customer_SKU_Pricing_InsertUpdate", arrSpFieldSeq);
                //db.ExeCommand(objCompHash, "", arrSpFieldSeq, "@poErrorMessage", out errorMessage);
                returnValue = true;
                //if (errorMessage == string.Empty)
                //    returnValue = true;
                //else
                //    returnValue = false;
            }
            catch (Exception objExp)
            {
                returnValue = false;
                throw new Exception(objExp.Message.ToString());

            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return returnValue;
        }
        
        public static List<SKUPrices> GetCompanySKUPricesToApprove(int companyID, bool isProduct)
        {
            List<SKUPrices> skuPricesList = new List<SKUPrices>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@IsProduct", isProduct);



                arrSpFieldSeq = new string[] { "@CompanyID", "@IsProduct" };

                dataTable = db.GetTableRecords(objCompHash, "Av_SKUPriceToApprove_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    skuPricesList = PopulateSKUPriceInfo(dataTable);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return skuPricesList;
        }
        
        public static List<SKUPrices> GetCompanySKUPrices(int companyID, int itemCompanyGUID)
        {
            List<SKUPrices> skuPricesList = new List<SKUPrices>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@ItemCompanyGUID", itemCompanyGUID);


                arrSpFieldSeq = new string[] { "@CompanyID", "@ItemCompanyGUID" };

                dataTable = db.GetTableRecords(objCompHash, "Av_SKU_Pricing_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    skuPricesList = PopulateSKUPrices(dataTable);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return skuPricesList;
        }
        
        private static List<SKUPrices> PopulateSKUPrices(DataTable dataTable)
        {
            List<SKUPrices> skuPricesList = new List<SKUPrices>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        SKUPrices skuPrices = new SKUPrices();
                        skuPrices.SKUPrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "AV Partner Prices", 0, false));
                        skuPrices.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                        //skuPrices.AVSupplyChainPrices = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "AV Supply Chain Prices", 0, false));
                        //skuPrices.AVSpotBuy = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "AV Spot Buy", false, false));

                        skuPricesList.Add(skuPrices);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return skuPricesList;
        }

        private static List<SKUPrices> PopulateSKUPriceInfo(DataTable dataTable)
        {
            List<SKUPrices> skuPricesList = new List<SKUPrices>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        SKUPrices skuPrices = new SKUPrices();
                        skuPrices.SKUPrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "skuPrice", 0, false));
                        skuPrices.ProposePrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "ProposePrice", 0, false));
                        skuPrices.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                        skuPrices.ProductCode = clsGeneral.getColumnData(dataRow, "ItemCode", string.Empty, false) as string;
                        
                        skuPrices.SKULastPrice = Convert.ToDouble(clsGeneral.getColumnData(dataRow, "SKULastPrice", 0, false));

                        skuPrices.ChangeDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ChangeDate", DateTime.MaxValue, false));
                        skuPrices.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                        
                        skuPricesList.Add(skuPrices);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return skuPricesList;
        }

        private static List<CompanySKUno> PopulateSKU(DataTable dataTable)
        {
            List<CompanySKUno> skuList = new List<CompanySKUno>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    CompanySKUno objInventoryItem = new CompanySKUno();
                    objInventoryItem.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    //objInventoryItem.MASSKU = clsGeneral.getColumnData(dataRow, "SKUPRICE", string.Empty, false) as string;
                    objInventoryItem.ItemcompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));

                    skuList.Add(objInventoryItem);
                }
            }
            return skuList;

        }

        private static List<ForecastComments> PopulateForecastComments(DataTable dataTable)
        {
            List<ForecastComments> commentsList = new List<ForecastComments>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    ForecastComments objComments = new ForecastComments();
                    objComments.Comments = clsGeneral.getColumnData(dataRow, "Comments", string.Empty, false) as string;
                    objComments.CommentDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "CommentDate", 0, false));

                    commentsList.Add(objComments);
                }
            }
            return commentsList;

        }
    }
}