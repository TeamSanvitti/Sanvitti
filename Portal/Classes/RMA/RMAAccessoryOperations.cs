using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Data;
namespace avii.Classes
{
    public class RMAAccessoryOperations
    {

        //public static void RMAAccessories_InsertUpdate(List<RMAAccessory> rmaAccessoryList, int rmaDetsailID)
        //{

        //    string rmaAccessoryXml = clsGeneral.SerializeObject(rmaAccessoryList);

        //    DBConnect db = new DBConnect();
        //    string[] arrSpFieldSeq;
        //    Hashtable objCompHash = new Hashtable();

        //    try
        //    {

        //        objCompHash.Add("@RMADetailID", rmaDetsailID);
        //        objCompHash.Add("@AccessoryXML", rmaAccessoryXml);


        //        arrSpFieldSeq = new string[] { "@RMADetailID", "@AccessoryXML" };
        //        db.ExeCommand(objCompHash, "av_RMA_Accessoeries_Insert_Update", arrSpFieldSeq);


        //    }
        //    catch (Exception objExp)
        //    {

        //        throw objExp;
        //    }
        //    finally
        //    {
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }
        //}

        public static List<RMAAccessory> GetItemAccessoryList(string itemCode)
        {
            List<RMAAccessory> rmaAccessoryList = new List<RMAAccessory>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {


                objCompHash.Add("@ITEMNUMBER", itemCode);

                arrSpFieldSeq = new string[] { "@ITEMNUMBER" };

                dataTable = db.GetTableRecords(objCompHash, "ALB_PRODUCT_ACCESSORY_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    rmaAccessoryList = PopulateRMAAccessoryList(dataTable);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return rmaAccessoryList;
        }

        private static List<RMAAccessory> PopulateRMAAccessoryList(DataTable dataTable)
        {
            List<RMAAccessory> rmaAccessoryList = new List<RMAAccessory>();
            if (dataTable != null && dataTable.Rows.Count > 0)
            {

                foreach (DataRow dataRow in dataTable.Rows)
                {
                    RMAAccessory objRMA = new RMAAccessory();

                    objRMA.AccessoryID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AccessoryID", 0, false));
                    objRMA.AccessoryNumber = clsGeneral.getColumnData(dataRow, "ACCITEMNUMBER", string.Empty, false) as string;
                    objRMA.AccessoryDescription = clsGeneral.getColumnData(dataRow, "ACCESSORYDESCRIPTION", string.Empty, false) as string;

                    objRMA.IsChecked = false;

                    rmaAccessoryList.Add(objRMA);
                }
            }
            return rmaAccessoryList;
        }

    }
}