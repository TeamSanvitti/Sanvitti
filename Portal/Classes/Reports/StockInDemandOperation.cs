using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class StockInDemandOperation
    {
        public static List<StockInDemand> GetStockInDemandList(int companyID, string SKU, string fromDate, string toDate)
        {
            DBConnect db = new DBConnect();
            List<StockInDemand> stockList = new List<StockInDemand>();
            string[] arrSpFieldSeq;

            Hashtable objParams = new Hashtable();
            DataTable dataTable = new DataTable();

            try
            {


                objParams.Add("@CompanyID", companyID);
                objParams.Add("@SKU", SKU);
                objParams.Add("@FromDate", fromDate == "" ? null : fromDate);
                objParams.Add("@ToDate", toDate == "" ? null : toDate);

                arrSpFieldSeq =
                new string[] { "@CompanyID", "@SKU", "@FromDate", "@ToDate" };

                dataTable = db.GetTableRecords(objParams, "av_Stock_In_Demand_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        StockInDemand stockInDemand = new StockInDemand();
                        
                        stockInDemand.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                        stockInDemand.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                        stockInDemand.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                        stockInDemand.RequiredQunatity = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Qty", 0, false));
                        stockInDemand.OrderCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderCount", 0, false));
                        stockInDemand.CurrentStock = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_hand", 0, false));
                        stockInDemand.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        stockInDemand.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                        stockList.Add(stockInDemand);
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
            return stockList;
        }

    }
}