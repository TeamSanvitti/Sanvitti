using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class CategoryOperations
    {
        public static List<Categories> GetItemCategoryList(int carrierID, int makerID)
        {
            List<Categories> categoriesList = new List<Categories>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CarrierID", carrierID);
                objCompHash.Add("@MakerID", makerID);

                arrSpFieldSeq = new string[] { "@CarrierID", "@MakerID" };

                dataTable = db.GetTableRecords(objCompHash, "av_ItemCategories_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    categoriesList = PopulateCarriers(dataTable);
                }

            }
            catch (Exception ex)
            {
                //throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return categoriesList;
        }
        public static List<Categories> PopulateCarriers(DataTable dataTable)
        {
            List<Categories> categoriesList = new List<Categories>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        Categories categoryObj = new Categories();
                        categoryObj.CategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryGUID", 0, false));
                        categoryObj.CarrierID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CarrierID", 0, false));
                        categoryObj.MakerGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MakerGUID", 0, false));
                        categoryObj.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                        categoryObj.CategoryImage = clsGeneral.getColumnData(dataRow, "CateogoryImage", string.Empty, false) as string;
                        categoryObj.MakerName = clsGeneral.getColumnData(dataRow, "MakerName", string.Empty, false) as string;
                        //carriersObj.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "Active", false, false));

                        categoriesList.Add(categoryObj);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }


            return categoriesList;
        }

        
    }
}