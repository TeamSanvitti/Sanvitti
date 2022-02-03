using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
//using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
{
    public enum ForecastByID
    {
        ForecastGUID,
        ItemGUID
    }

    public class ItemForecastController
    {
        DBConnect objDB = new DBConnect();
        protected int _userid = 0;
        protected int _companyid = 0;
        protected int _itemTypeid;
        protected DateTime? _dateFrom;
        protected DateTime? _dateTo;
        protected string _sku;
        protected string _brand;
        protected List<avii.Classes.Controller.Forecast> objForecastList= new List<avii.Classes.Controller.Forecast>();

        public int UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
            }
        }
        public int CompanyID
        {
            get
            {
                return _companyid;
            }
            set
            {
                _companyid = value;
            }
        }

        public DateTime? DateFrom
        {
            get
            {
                return _dateFrom;
            }
            set
            {
                _dateFrom = value;
            }
        }

        public DateTime? DateTo
        {
            get
            {
                return _dateTo;
            }
            set
            {
                _dateTo = value;
            }
        }

        public string SKU
        {
            get
            {
                return _sku;
            }
            set
            {
                _sku = value;
            }
        }

        public int ItemTypeID
        {
            get
            {
                return _itemTypeid;
            }
            set
            {
                _itemTypeid = value;
            }
        }

        public string Brand
        {
            get
            {
                return _brand;
            }
            set
            {
                _brand = value;
            }
        }

        public ItemForecastController()
        { }


        public ItemForecastController(int userid)
        {
            this.UserID = userid;
        }


        public static Forecast GetForecast(int forecastID)
        {
            Forecast forecast = null;
            forecast = ForecastDB.GetForecast(forecastID);

            return forecast;
        }

        //public List<avii.Classes.Controller.Forecast> GetForecastList(int itemID)
        //{
        //    List<avii.Classes.Controller.Forecast> forecastList = null;
        //    DataSet ds = new DataSet();
        //    Hashtable objCompHash = new Hashtable();
        //    string[] arrSpFieldSeq;

        //    try
        //    {
        //        objCompHash.Add("@userID", this.UserID);
        //        objCompHash.Add("@itemID", itemID);
        //        arrSpFieldSeq = new string[] { "@userID", "@itemID" };
        //         ds = objDB.GetDataSet(objCompHash, "Forecast_select", arrSpFieldSeq);

        //        forecastList = populateForecast(ds);
        //    }
        //    catch (Exception objExp)
        //    {
        //        throw new Exception(objExp.Message.ToString());
        //    }
        //    finally
        //    {
        //        objDB.DBClose();
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }

        //    return forecastList;
        //}

        public List<avii.Classes.Controller.Forecast> GetForecastList(int itemID, DateTime? dateFrom, DateTime? dateTo)
        {
            List<avii.Classes.Controller.Forecast> forecastList = null;
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@CompanyID", this.CompanyID);
                objCompHash.Add("@itemID", itemID);
                objCompHash.Add("@dateFrom", dateFrom);
                objCompHash.Add("@dateTo", dateTo);

                arrSpFieldSeq = new string[] { "@CompanyID", "@itemID", "@dateFrom", "@dateTo" };
                ds = objDB.GetDataSet(objCompHash, "Forecast_select", arrSpFieldSeq);

                forecastList = populateForecast(ds);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return forecastList;
        }


        ////public List<avii.Classes.Controller.Forecast> GetForecastList(int itemID)
        ////{
        ////    DataSet ds = new DataSet();
        ////    Hashtable objCompHash = new Hashtable();
        ////    string[] arrSpFieldSeq;

        ////    try
        ////    {
        ////        objCompHash.Add("@userID", UserID);
        ////        objCompHash.Add("@itemID", itemID);
        ////        arrSpFieldSeq = new string[] { "@userID", "@itemID" };
        ////        ds = objDB.GetDataSet(objCompHash, "Forecast_select", arrSpFieldSeq);

        ////        return populateForecastlist(ds);
        ////    }
        ////    catch (Exception objExp)
        ////    {
        ////        throw new Exception(objExp.Message.ToString());
        ////    }
        ////    finally
        ////    {
        ////        objDB.DBClose();
        ////        //db = null;
        ////        objCompHash = null;
        ////        arrSpFieldSeq = null;
        ////    }
        ////}

        public List<avii.Classes.Controller.ItemForecast> GetItemForecast()
        {
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@CompanyID", this.CompanyID);
                objCompHash.Add("@itemTypeID", this.ItemTypeID);
                objCompHash.Add("@SKU", this.SKU);
                objCompHash.Add("@Brand", this.Brand);
                arrSpFieldSeq = new string[] { "@CompanyID", "@itemTypeID", "@SKU", "@Brand" };
                ds = objDB.GetDataSet(objCompHash, "ItemForecast_select", arrSpFieldSeq);

                return populateItemForecast(ds);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                //db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public List<avii.Classes.Controller.ItemForecast> GetItemForecast(int companyID, int itemTypeID)
        {
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@CompanyID", this.CompanyID);
                objCompHash.Add("@itemTypeID", itemTypeID);
                arrSpFieldSeq = new string[] { "@CompanyID", "@itemTypeID" };
                ds = objDB.GetDataSet(objCompHash, "ItemForecast_select", arrSpFieldSeq);

                return populateItemForecast(ds);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                //db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        private List<avii.Classes.Controller.Forecast> populateForecast(DataSet ds)
        {
            List<avii.Classes.Controller.Forecast> forecastList = null;
            avii.Classes.Controller.Forecast forecast = null;
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                forecastList = new List<avii.Classes.Controller.Forecast>();
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    forecast = new avii.Classes.Controller.Forecast();
                    forecast.ForecastGUID = (int)dataRow["ForecastGUID"];
                    forecast.ItemID = (int) dataRow["ItemID"];
                    forecast.ForecastDate = Convert.ToDateTime(dataRow["ForecastDate"]);
                    forecast.Qty = (int) dataRow["qty"];
                    forecast.StatusID = (int)dataRow["ForecastStatusID"];
                    forecast.POForecastGUID = (int)dataRow["FPOForecastGUID"];
                    
                    forecastList.Add(forecast);
                }
            }
            return forecastList;
        }

        private avii.Classes.Controller.Forecast populateForecast(DataTable dataTable)
        {
            avii.Classes.Controller.Forecast forecast = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                forecast = new avii.Classes.Controller.Forecast();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    forecast.ForecastGUID = (int)dataRow["ForecastGUID"];
                    forecast.ItemID = (int)dataRow["ItemID"];
                    forecast.ForecastDate = Convert.ToDateTime(dataRow["ForecastDate"]);
                    forecast.Qty = (int)dataRow["qty"];
                    forecast.StatusID = (int)dataRow["ForecastStatusID"];
                }
            }
            return forecast;
        }

        private List<avii.Classes.Controller.Forecast> populateForecastlist(DataSet ds)
        {
            List<avii.Classes.Controller.Forecast> forecastList = new List<avii.Classes.Controller.Forecast>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    avii.Classes.Controller.Forecast forecast = new avii.Classes.Controller.Forecast();
                    forecast.ForecastGUID = (int)dataRow["ForecastGUID"];
                    forecast.ItemID = (int)dataRow["ItemID"];
                    forecast.ForecastDate = Convert.ToDateTime(dataRow["ForecastDate"]);
                    
                    forecast.Qty = (int)dataRow["qty"];
                    forecast.StatusID = (int)dataRow["ForecastStatusID"];

                    forecastList.Add(forecast);
                }
            }
            return forecastList;
        }

        private List<avii.Classes.Controller.ItemForecast> populateItemForecast(DataSet ds)
        {
            List<avii.Classes.Controller.ItemForecast> itemforecastList = new List<avii.Classes.Controller.ItemForecast>();
            if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    avii.Classes.Controller.ItemForecast itemforecast = new avii.Classes.Controller.ItemForecast();
                    itemforecast.ItemType = (avii.Classes.Controller.AvItemType)dataRow["ItemType"];
                    itemforecast.ItemID = (int)dataRow["ItemID"];
                    itemforecast.ItemDesc = (string)dataRow["ItemDesc"];
                    itemforecast.ItemSKU = (string)dataRow["ItemSKU"]; 
                    itemforecast.ItemForecasts = GetForecastList((int)dataRow["ItemID"],  this.DateFrom, this.DateTo);
                    itemforecast.ItemTypeID = (int)dataRow["itemType"];
                    itemforecast.UPC = dataRow["upc"] as string;
                    itemforecastList.Add(itemforecast);
                }
            }
            return itemforecastList;
        }


        # region createForecastLis - creates the list of forecast objects to be passed to the stored proc as XML --
        public void createForecastList(int forecastGUID, int itemID, DateTime forecastDate, int qty, bool isLocked)
        {
            avii.Classes.Controller.Forecast objForecastTemp = new avii.Classes.Controller.Forecast();
            objForecastTemp.ForecastGUID = forecastGUID;
            objForecastTemp.ItemID = itemID;
            objForecastTemp.ForecastDate = forecastDate;
            objForecastTemp.Qty = qty;
            objForecastTemp.StatusID = Convert.ToInt32(isLocked);

            objForecastList.Add(objForecastTemp);
        }
        # endregion

        # region createForecast - uses the list of forecast objects to creat the DB entries --
        public void createForecast()
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            string FXml = "";
            try
            {
                XmlSerializer objXMLSerializer = new XmlSerializer(this.objForecastList.GetType());
                MemoryStream memstr = new MemoryStream();
                XmlTextWriter xmltxtwr = new XmlTextWriter(memstr, Encoding.UTF8);

                // __________ begin - serialise the forecast list to pass to the stored proc ________
                objXMLSerializer.Serialize(xmltxtwr, this.objForecastList);
                xmltxtwr.Close();
                memstr.Close();

                //FXml = memstr.ToString();
                FXml = Encoding.UTF8.GetString(memstr.GetBuffer());
                FXml = "<ArrayOfForecast>" + FXml.Substring(FXml.IndexOf("<Forecast>"));
                FXml = FXml.Substring(0, (FXml.LastIndexOf(Convert.ToChar(62)) + 1));
                // __________ end - serialise the forecast list to pass to the stored proc ________

                objCompHash.Add("@F_Xml", FXml);
                objCompHash.Add("@UserID", this.UserID);
                arrSpFieldSeq = new string[] { "@F_Xml", "@UserID" };
                db.ExeCommand(objCompHash, "Forecast_Create", arrSpFieldSeq);

                //errorMessage = ResponseErrorCode.UploadedSuccessfully.ToString();
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
            //return errorMessage;
        }
        #endregion


        public static void InsertUpdateForecast(List<avii.Classes.Controller.Forecast> objForecastList, int comapnyID)
        {
            string forecastXml = clsGeneral.SerializeObject(objForecastList);
            
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ForecastXml", forecastXml);
                objCompHash.Add("@CompanyID", comapnyID);


                arrSpFieldSeq = new string[] { "@ForecastXml", "@CompanyID" };
                db.ExeCommand(objCompHash, "av_Forecast_InsertUpdate", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public void insertUpdateItemForecast(int forecastGUID, int itemID, DateTime forecastDate, int qty, bool isLocked)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@forecastGUID", forecastGUID);
                objCompHash.Add("@userID", this.UserID);
                objCompHash.Add("@itemID", itemID);
                objCompHash.Add("@forecastDate", forecastDate);
                objCompHash.Add("@qty", qty);
                objCompHash.Add("@forecastStatusID", Convert.ToInt32(isLocked));


                arrSpFieldSeq = new string[] { "@forecastGUID", "@userID", "@itemID", "@forecastDate", "@qty", "@forecastStatusID" };
                db.ExeCommand(objCompHash, "Forecast_Update", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }


        public Hashtable getCustomerList(string adminUsername)
        {
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            Hashtable hstCustomer = new Hashtable();

            try
            {
                objCompHash.Add("@AdmUser", adminUsername);


                arrSpFieldSeq = new string[] { "@AdmUser" };
               
                ds = objDB.GetDataSet(objCompHash, "Customer_Select", arrSpFieldSeq);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        if (!hstCustomer.Contains(dataRow["userName"]))
                            hstCustomer.Add(dataRow["userName"], dataRow["userID"]);
                    }
                }

                return hstCustomer;
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                //db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public Hashtable getCustomerList()
        {
            DataSet ds = new DataSet();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            Hashtable hstCustomer = new Hashtable();

            try
            {
                arrSpFieldSeq = new string[] { "" };
                ds = objDB.GetDataSet(objCompHash, "Customer_Select", arrSpFieldSeq);

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dataRow in ds.Tables[0].Rows)
                    {
                        if (!hstCustomer.Contains(dataRow["userName"]))
                            hstCustomer.Add(dataRow["userName"], dataRow["userID"]);
                    }
                }

                return hstCustomer;
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                //db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public DataTable GetForecastSummary(int CompanyID, int userID, string SKU, int MakerID, DateTime FromDate, DateTime ToDate)
        {
            DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;

            try
            {
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@SKU", SKU);
                objCompHash.Add("@makerID", MakerID);
                if (FromDate != null && FromDate.Year > 1)
                    objCompHash.Add("@DateFrom", FromDate);

                if (ToDate != null && ToDate.Year > 1)
                    objCompHash.Add("@DateTo", ToDate);

                arrSpFieldSeq = new string[] { "@CompanyID", "@UserID", "@SKU", "@makerID", "@DateFrom", "@DateTo" };
                dataTable = objDB.GetTableRecords(objCompHash, "av_Forecast_Summary", arrSpFieldSeq);

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }   

            return dataTable;
        }
    }
}
