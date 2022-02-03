
using System;
//using System.Linq;
//using System.Xml.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace avii.Classes
{
    #region Enum
    public enum ForecastStatus
    {
        Pending = 1,
        Cancelled = 2,
        Achieved = 3,
        NotAchieved = 4,
        Other = 0
    }

    public enum ForecastConfirmed
    {
        Confirmed = 0,
        NotConfirmed = 1
    }
    #endregion

    #region Classes
    public class ForecastInternal
    {
        private int itemID;
        private string phoneMaker;
        private int statusId;

        public int ItemID
        {
            get
            {
                return itemID;
            }
            set
            {
                itemID = value;
            }
        }

        public int StatusId
        {
            get
            {
                return statusId;
            }
            set
            {
                statusId = value;
            }
        }

        public string PhoneMaker
        {
            get
            {
                return phoneMaker;
            }
            set
            {
                phoneMaker = value;
            }
        }
    }

    public class Forecast
    {
        private DateTime forecastDate;
        private string forecastSku;
        private int forecastQty;
        private ForecastConfirmed confirmed;
        private ForecastStatus status;
        private ForecastInternal forecastInternal;

        public Forecast()
        {
            forecastInternal = new ForecastInternal();
        }

        public DateTime ForecastDate
        {
            get
            {
                return forecastDate;
            }
            set
            {
                forecastDate = value;
            }
        }

        public string ForecastSku
        {
            get
            {
                return forecastSku;
            }
            set
            {
                forecastSku = value;
            }
        }

        public int ForecastQty
        {
            get
            {
                return forecastQty;
            }
            set
            {
                forecastQty = value;
            }
        }

        public ForecastConfirmed ForecastConfirmed
        {
            get
            {
                return confirmed;
            }
            set
            {
                confirmed = value;
            }
        }

        public ForecastStatus ForecastStatus
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public ForecastInternal ForecastInternalData
        {
            get
            {
                return forecastInternal;
            }
            set
            {
                forecastInternal = value;
            }
        }

    }

    internal static class ForecastUtility
    {
        public static List<Forecast> PopulateForecastList(DataTable dataTable)
        {
            List<Forecast> forecastList = null;
            Forecast forecast = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                forecastList = new List<Forecast>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    forecast = PopulateForecast(dataRow);
                    forecastList.Add(forecast);
                }
            }

            return forecastList;
        }

        public static Forecast PopulateForecast(DataTable dataTable)
        {
            Forecast forecast = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                forecast = PopulateForecast(dataTable.Rows[0]);
            }

            return forecast;
        }


        public static Forecast PopulateForecast(DataRow dataRow)
        {
            Forecast forecast = null;
            if (dataRow != null)
            {
                //ForecastGUID, ForecastDate, ItemID, Qty, ForecastStatus, Confirmed, SKU, PhoneMaker
                forecast = new Forecast();

                forecast.ForecastSku = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, true) as string;
                forecast.ForecastDate = (DateTime)clsGeneral.getColumnData(dataRow, "ForecastDate", DateTime.Now, false);
                forecast.ForecastQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                forecast.ForecastStatus = (ForecastStatus)clsGeneral.getColumnData(dataRow, "ForecastStatusID", 1, false);
                forecast.ForecastInternalData.ItemID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemID", 0, false));
                forecast.ForecastInternalData.StatusId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ForecastStatusID", 1, false));
                forecast.ForecastInternalData.PhoneMaker = clsGeneral.getColumnData(dataRow, "PhoneMaker", 0, false) as string;
            }

            return forecast;
        }


    }

    public static class ForecastDB
    {
        public static Forecast GetForecast(int forecastID)
        {
            DataTable dataTable = new DataTable();
            Forecast forecast = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ForecastGUID", forecastID);

                arrSpFieldSeq = new string[] { "@ForecastGUID" };

                dataTable = db.GetTableRecords(objCompHash, "Av_Forecast_Select", arrSpFieldSeq);
                forecast = avii.Classes.ForecastUtility.PopulateForecast(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return forecast;
        }

        public static List<Forecast> GetForecastList(DateTime forecastDate, string itemID)
        {
            DataTable dataTable = new DataTable();
            List<Forecast> forecastList = null;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ForecastDate", forecastDate);
                objCompHash.Add("@ItemID", itemID);

                arrSpFieldSeq = new string[] { "@ForecastDate", "@ItemID" };

                dataTable = db.GetTableRecords(objCompHash, "Av_Forecast_Select", arrSpFieldSeq);
                forecastList = avii.Classes.ForecastUtility.PopulateForecastList(dataTable);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return forecastList;
        }

        public static void SetForecast(List<Forecast> forecastList)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ForecastDate", null);
                objCompHash.Add("@Item", null);
                objCompHash.Add("@Qty", null);
                objCompHash.Add("@Forecaststatus", null);

                arrSpFieldSeq = new string[] { "@ForecastDate", "@ItemID", "@Qty", "@Forecaststatus" };

                foreach (Forecast forecast in forecastList)
                {
                    if (forecast.ForecastQty > 0)
                    {
                        objCompHash["@ForecastDate"] = forecast.ForecastDate;
                        objCompHash["@ItemID"] = forecast.ForecastInternalData.ItemID;
                        objCompHash["@Qty"] = forecast.ForecastQty;
                        objCompHash["@Forecaststatus"] = forecast.ForecastInternalData.StatusId;

                        db.ExeCommand(objCompHash, "Av_Forecast_Insert", arrSpFieldSeq);
                    }
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
    }

    #endregion
}
