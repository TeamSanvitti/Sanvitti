using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class DishLabelOperations
    {

        public static List<SV.Framework.LabelGenerator.PalletModel> GetPalletLabelInfo(int poid, string poNumber, string palletID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dt = new DataTable();
            List<SV.Framework.LabelGenerator.PalletModel> palletList = null;
            
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POID", poid);
                objCompHash.Add("@FulfillmentNumber", poNumber);
                objCompHash.Add("@PalletID", palletID);
                

                arrSpFieldSeq = new string[] { "@POID", "@FulfillmentNumber", "@PalletID" };
                dt = db.GetTableRecords(objCompHash, "av_PalletLabel_Select", arrSpFieldSeq);

                palletList = PopulatePalletID(dt);
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
            return palletList;
        }

        public static List<SV.Framework.LabelGenerator.MasterCartonInfo> GetMasterCartonLabelInfo(int poid)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            List<SV.Framework.LabelGenerator.MasterCartonInfo> cartonList = null;

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POID", poid);


                arrSpFieldSeq = new string[] { "@POID" };
                ds = db.GetDataSet(objCompHash, "av_MasterCartonLabel_Select", arrSpFieldSeq);

                cartonList = PopulateCartons(ds);
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
            return cartonList;
        }
        public static List<SV.Framework.LabelGenerator.MasterCartonInfo> GetMasterCartonLabelByContainerID(string ContainerID, int poid)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            List<SV.Framework.LabelGenerator.MasterCartonInfo> cartonList = null;

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@ContainerID", ContainerID);
                objCompHash.Add("@POID", poid);


                arrSpFieldSeq = new string[] { "@ContainerID", "@POID" };
                ds = db.GetDataSet(objCompHash, "av_MasterCartonLabel_ByContainerID", arrSpFieldSeq);

                cartonList = PopulateCartons(ds);
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
            return cartonList;
        }

        public static DataTable ESNData(List<SV.Framework.Models.Inventory.EsnUploadNew> mslEsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ESN", typeof(System.String));
            dt.Columns.Add("BatchNumber", typeof(System.String));
            dt.Columns.Add("ICC_ID", typeof(System.String));
            dt.Columns.Add("MeidHex", typeof(System.String));
            dt.Columns.Add("MeidDec", typeof(System.String));
            dt.Columns.Add("Location", typeof(System.String));
            dt.Columns.Add("MSL", typeof(System.String));
            dt.Columns.Add("OTKSL", typeof(System.String));
            dt.Columns.Add("SerialNumber", typeof(System.String));


            DataRow row;
            int rowNumber = 1;
            if (mslEsnList != null && mslEsnList.Count > 0)
            {
                foreach (SV.Framework.Models.Inventory.EsnUploadNew item in mslEsnList)
                {
                    row = dt.NewRow();
                    row["ESN"] = item.ESN;
                    row["BatchNumber"] = item.MslNumber;
                    row["ICC_ID"] = rowNumber;
                    row["MeidHex"] = item.MeidHex;
                    row["MeidDec"] = item.MeidDec;
                    row["Location"] = item.Location;
                    row["MSL"] = item.MSL;
                    row["OTKSL"] = item.OTKSL;
                    row["SerialNumber"] = item.SerialNumber;

                    dt.Rows.Add(row);
                    rowNumber = rowNumber + 1;
                }
            }
            return dt;
        }

        public static List<SV.Framework.LabelGenerator.PosKitInfo> GetPosLabels(int companyID, int ESNAuthorizationID, DataTable dt)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            List<SV.Framework.LabelGenerator.PosKitInfo> posKits = null;

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyID);
                objCompHash.Add("@ESNAuthorizationID", ESNAuthorizationID);
                objCompHash.Add("@ESNTable", dt);

                arrSpFieldSeq = new string[] { "@CompanyID", "@ESNAuthorizationID", "@ESNTable" };
                ds = db.GetDataSet(objCompHash, "av_POSAuthorization_Select", arrSpFieldSeq);

                posKits = PopulatePOSKit(ds);
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
            return posKits;
        }

        public static DataTable ESNData(string[] esnList)
        {
            //string[] esnList = Esns.Split(',');
            DataTable dt = new DataTable();

            dt.Columns.Add("ROWID", typeof(System.Int32));
            dt.Columns.Add("ESN", typeof(System.String));
            int rowID = 1;
            DataRow row;

            if (esnList != null && esnList.Length > 0)
            {
                foreach (string item in esnList)
                {
                    row = dt.NewRow();
                    row["ROWID"] = rowID;
                    row["ESN"] = item;
                    dt.Rows.Add(row);
                    rowID = rowID + 1;
                }
            }
            return dt;
        }

        public static List<SV.Framework.LabelGenerator.PosKitInfo> GetPosLabelByESN(string esn, string shippRequestDate)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            List<SV.Framework.LabelGenerator.PosKitInfo> posKits = null;
           // DataTable dt = ESNData(esnList);
            Hashtable objCompHash = new Hashtable();
            try
            {
                //objCompHash.Add("@ItemCompanyGUID", ItemCompanyGUID);
                objCompHash.Add("@ShippRequestDate", shippRequestDate);
                objCompHash.Add("@ESN", esn);

                //arrSpFieldSeq = new string[] { "@ItemCompanyGUID", "@ShippRequestDate", "@ESNTable" };
                arrSpFieldSeq = new string[] { "@ShippRequestDate", "@ESN" };
                ds = db.GetDataSet(objCompHash, "av_POSLabelByESN_Select", arrSpFieldSeq);

                posKits = PopulatePOSKit(ds);
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
            return posKits;
        }

        public static List<SV.Framework.LabelGenerator.PosKitInfo> GetPosLabel(int poid, string[] esnList)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            List<SV.Framework.LabelGenerator.PosKitInfo> posKits = null;
            DataTable dt = ESNData(esnList);
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POID", poid);
                objCompHash.Add("@ESNTable", dt);


                arrSpFieldSeq = new string[] { "@POID", "@ESNTable" };
                ds = db.GetDataSet(objCompHash, "av_POSLabel_Select", arrSpFieldSeq);

                posKits = PopulatePOSKit(ds);
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
            return posKits;
        }

        public static List<SV.Framework.LabelGenerator.PosKitInfo> GetPosKitLabel(int poid)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            List<SV.Framework.LabelGenerator.PosKitInfo> posKits = null;

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POID", poid);


                arrSpFieldSeq = new string[] { "@POID" };
                ds = db.GetDataSet(objCompHash, "av_POSKIT_Select", arrSpFieldSeq);

                posKits = PopulatePOSKit(ds);
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
            return posKits;
        }
        public static List<SV.Framework.LabelGenerator.PalletCartonMap> GetPalletCartonMappingLabel(int poid)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataSet ds = new DataSet();
            List<SV.Framework.LabelGenerator.PalletCartonMap> pallets = null;

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@POID", poid);


                arrSpFieldSeq = new string[] { "@POID" };
                ds = db.GetDataSet(objCompHash, "av_CartonPerPallet_Select", arrSpFieldSeq);

                pallets = PopulatePalletCartonMapping(ds);
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
            return pallets;
        }
        private static List<SV.Framework.LabelGenerator.PalletCartonMap> PopulatePalletCartonMapping(DataSet ds)
        {

            SV.Framework.LabelGenerator.PalletCartonMap palletInfo = null;
            List<SV.Framework.LabelGenerator.PalletCartonMap> pallets = null;
            SV.Framework.LabelGenerator.CartonModel cartonItem = null;
            List<SV.Framework.LabelGenerator.CartonModel> CartonItems = null;
            List<string> list1 = new List<string>();
            List<string> listC1 = new List<string>();
            List<string> listC2 = new List<string>();
            List<string> listBOXC1 = new List<string>();
            List<string> listBOXC2 = new List<string>();
            string containerID, BOXID;
            int PoPalletId = 0;
            int totalcartonCount = 0;
            int cartonCount = 0;

            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    pallets = new List<SV.Framework.LabelGenerator.PalletCartonMap>();
                    foreach (DataRow dRowItem in ds.Tables[0].Rows)
                    {

                        palletInfo = new SV.Framework.LabelGenerator.PalletCartonMap();
                        palletInfo.PalletID = (string)clsGeneral.getColumnData(dRowItem, "PalletID", string.Empty, false);
                        PoPalletId = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "PoPalletId", 0, false));
                        CartonItems = new List<SV.Framework.LabelGenerator.CartonModel>();
                        list1 = new List<string>();
                        listC1 = new List<string>();
                        listC2 = new List<string>();
                        listBOXC1 = new List<string>();
                        listBOXC2 = new List<string>();
                        int j = 0;
                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dRowItem1 in ds.Tables[1].Select("PoPalletId = " + PoPalletId))
                            {
                                j += 1;
                                containerID = (string)clsGeneral.getColumnData(dRowItem1, "ContainerID", string.Empty, false);
                                BOXID = (string)clsGeneral.getColumnData(dRowItem1, "BoxNumber", string.Empty, false);
                                list1.Add(containerID);
                                if (j % 2 == 0)
                                {
                                    listC2.Add(containerID);
                                    listBOXC2.Add(BOXID);
                                }
                                else
                                {
                                    listC1.Add(containerID);
                                    listBOXC1.Add(BOXID);
                                }
                            }
                            if(list1 != null && list1.Count > 0 && listC1.Count > 0)
                            {
                                totalcartonCount = list1.Count;
                                cartonCount = totalcartonCount / 2;
                                for(int i = 0; i < cartonCount; i++)
                                {
                                    cartonItem = new SV.Framework.LabelGenerator.CartonModel();
                                    cartonItem.Column1 = listC1[i];
                                    cartonItem.BOXColumn1 = listBOXC1[i];
                                    if (i < listC2.Count)
                                    {
                                        cartonItem.Column2 = listC2[i];
                                        cartonItem.BOXColumn2 = listBOXC2[i];
                                    }
                                    else
                                    {
                                        cartonItem.Column2 = "";
                                        cartonItem.BOXColumn2 = "";
                                    }

                                    CartonItems.Add(cartonItem);

                                }

                            }                           
                        }

                        palletInfo.Cartons = CartonItems;                        
                        pallets.Add(palletInfo);
                    }



                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulateContainerIDs : " + ex.Message);
            }


            return pallets;
        }


        private static List<SV.Framework.LabelGenerator.PosKitInfo> PopulatePOSKit(DataSet ds)
        {

            List<SV.Framework.LabelGenerator.PosKitInfo> posKits = null;
            SV.Framework.LabelGenerator.PosKitInfo posKitInfo = null;
            List<SV.Framework.LabelGenerator.KitBoxInfo> boxItems = null;
            SV.Framework.LabelGenerator.KitBoxInfo kitBoxInfo = null;
            string dishactivationcode = System.Configuration.ConfigurationManager.AppSettings["dishactivationcode"].ToString();

            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    posKits = new List<SV.Framework.LabelGenerator.PosKitInfo>();
                    foreach (DataRow dRowItem in ds.Tables[0].Rows)
                    {
                        boxItems = new List<SV.Framework.LabelGenerator.KitBoxInfo>();

                        posKitInfo = new SV.Framework.LabelGenerator.PosKitInfo();
                        posKitInfo.ShipDate = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_Date", string.Empty, false); //(clsGeneral.getColumnData(dRowItem, "ShipTo_Date", string.Empty, false)).ToString("MM/dd/yyyy");
                        posKitInfo.UPC = (string)clsGeneral.getColumnData(dRowItem, "UPC", string.Empty, false);
                        posKitInfo.SKU = (string)clsGeneral.getColumnData(dRowItem, "SKU", string.Empty, false);
                        posKitInfo.ItemName = (string)clsGeneral.getColumnData(dRowItem, "ItemName", string.Empty, false);
                        posKitInfo.SWVersion = (string)clsGeneral.getColumnData(dRowItem, "SWVersion", string.Empty, false);
                        posKitInfo.HWVersion = (string)clsGeneral.getColumnData(dRowItem, "HWVersion", string.Empty, false);
                        posKitInfo.ESN = (string)clsGeneral.getColumnData(dRowItem, "ESN", string.Empty, false);
                        posKitInfo.MEID = (string)clsGeneral.getColumnData(dRowItem, "MeidDec", string.Empty, false);
                        posKitInfo.HEX = (string)clsGeneral.getColumnData(dRowItem, "MeidHex", string.Empty, false);
                        posKitInfo.ICCID = (string)clsGeneral.getColumnData(dRowItem, "ICC_ID", string.Empty, false);
                        posKitInfo.SerialNum = dishactivationcode + posKitInfo.HEX;  //(string)clsGeneral.getColumnData(dRowItem, "SerialNumber", string.Empty, false);
                        posKitInfo.CompanyName = (string)clsGeneral.getColumnData(dRowItem, "CompanyName", string.Empty, false);
                        posKitInfo.ProductType = (string)clsGeneral.getColumnData(dRowItem, "ProductType", string.Empty, false);
                        posKitInfo.OSType = (string)clsGeneral.getColumnData(dRowItem, "OSType", string.Empty, false);

                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dRowItem1 in ds.Tables[1].Rows)
                            {
                                kitBoxInfo = new SV.Framework.LabelGenerator.KitBoxInfo();
                                kitBoxInfo.DisplayName = (string)clsGeneral.getColumnData(dRowItem1, "DisplayName", string.Empty, false);
                                kitBoxInfo.OriginCountry = (string)clsGeneral.getColumnData(dRowItem1, "OriginCountry", string.Empty, false);
                                boxItems.Add(kitBoxInfo);

                            }
                        }
                        posKitInfo.KitBoxList = boxItems;

                        posKits.Add(posKitInfo);


                    }



                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulateContainerIDs : " + ex.Message);
            }


            return posKits;
        }

        private static List<SV.Framework.LabelGenerator.MasterCartonInfo> PopulateCartons(DataSet ds)
        {
           
            SV.Framework.LabelGenerator.MasterCartonInfo cartonInfo = null;
            List<SV.Framework.LabelGenerator.MasterCartonInfo> cartons = null;
            SV.Framework.LabelGenerator.CartonItem cartonItem = null;
            List<SV.Framework.LabelGenerator.CartonItem> CartonItems = null;

            try
            {
                if (ds != null && ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
                {
                    cartons = new List<SV.Framework.LabelGenerator.MasterCartonInfo>();
                    foreach (DataRow dRowItem in ds.Tables[0].Rows)
                    {

                        cartonInfo = new SV.Framework.LabelGenerator.MasterCartonInfo();
                        CartonItems = new List<SV.Framework.LabelGenerator.CartonItem>();
                        cartonInfo.ShipDate = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_Date", string.Empty, false); //(clsGeneral.getColumnData(dRowItem, "ShipTo_Date", string.Empty, false)).ToString("MM/dd/yyyy");
                        cartonInfo.UPC = (string)clsGeneral.getColumnData(dRowItem, "UPC", string.Empty, false);
                        cartonInfo.SKU = (string)clsGeneral.getColumnData(dRowItem, "Item_Code", string.Empty, false);
                        cartonInfo.Comments = (string)clsGeneral.getColumnData(dRowItem, "Comments", string.Empty, false);
                        cartonInfo.SWVersion = (string)clsGeneral.getColumnData(dRowItem, "SWVersion", string.Empty, false);
                        cartonInfo.HWVersion = (string)clsGeneral.getColumnData(dRowItem, "HWVersion", string.Empty, false);
                        cartonInfo.ContainerID = (string)clsGeneral.getColumnData(dRowItem, "ContainerID", string.Empty, false);
                        cartonInfo.ProductType = (string)clsGeneral.getColumnData(dRowItem, "ProductType", string.Empty, false);
                        cartonInfo.OSType = (string)clsGeneral.getColumnData(dRowItem, "OSType", string.Empty, false);

                        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                        {
                            foreach (DataRow dRowItem1 in ds.Tables[1].Select("ContainerID = '" +cartonInfo.ContainerID +"'" ))
                            {
                                cartonItem = new SV.Framework.LabelGenerator.CartonItem();
                                cartonItem.IMEI = (string)clsGeneral.getColumnData(dRowItem1, "ESN", string.Empty, false);
                                cartonItem.HEX = (string)clsGeneral.getColumnData(dRowItem1, "MeidHex", string.Empty, false);
                                cartonItem.MEID = (string)clsGeneral.getColumnData(dRowItem1, "MeidDec", string.Empty, false);
                                CartonItems.Add(cartonItem);
                                cartonInfo.CartonQty = CartonItems.Count.ToString();
                            }
                        }
                        cartonInfo.CartonItems = CartonItems;
                        //Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ShipTo_Address", 0, false));
                        cartons.Add(cartonInfo);
                    }



                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulateContainerIDs : " + ex.Message);
            }


            return cartons;
        }

        private static List<SV.Framework.LabelGenerator.PalletModel> PopulatePalletID(DataTable dt)
        {
            string shipFromContactName = ConfigurationSettings.AppSettings["ShipFromContactName"].ToString();
            string shipFromContactName2 = ConfigurationSettings.AppSettings["ShipFromContactName2"].ToString();
            string shipFromAddress = ConfigurationSettings.AppSettings["ShipFromAddress"].ToString();
            string shipFromCity = ConfigurationSettings.AppSettings["ShipFromCity"].ToString();
            string shipFromState = ConfigurationSettings.AppSettings["ShipFromState"].ToString();
            string shipFromZip = ConfigurationSettings.AppSettings["ShipFromZip"].ToString();
            string shipFromCountry = ConfigurationSettings.AppSettings["ShipFromCountry"].ToString();
            string shipFromAttn = ConfigurationSettings.AppSettings["ShipFromAttn"].ToString();
            string shipFromPhone = ConfigurationSettings.AppSettings["ShipFromPhone"].ToString();

            SV.Framework.LabelGenerator.PalletModel fulfillmentPallet = null;
            List<SV.Framework.LabelGenerator.PalletModel> pallets = null;
            string poNumber;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    pallets = new List<SV.Framework.LabelGenerator.PalletModel>();
                    foreach (DataRow dRowItem  in dt.Rows)
                    {
                        //, , , , T1.Contact_Name, ,	, , , , T5.,

                        fulfillmentPallet = new SV.Framework.LabelGenerator.PalletModel();
                        fulfillmentPallet.ShipDate = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_Date", string.Empty, false);
                        poNumber = (string)clsGeneral.getColumnData(dRowItem, "PO_Num", string.Empty, false);
                        string[] array = poNumber.Split('-');
                        
                        fulfillmentPallet.PoNumber = array[0];

                        // if (array.Length > 1)
                        fulfillmentPallet.FO = poNumber; // (string)clsGeneral.getColumnData(dRowItem, "PO_Num", string.Empty, false);
                        //else
                        //    fulfillmentPallet.FO = "";

                        fulfillmentPallet.OSType = (string)clsGeneral.getColumnData(dRowItem, "OSType", string.Empty, false);
                        fulfillmentPallet.ProductType = (string)clsGeneral.getColumnData(dRowItem, "ProductType", string.Empty, false);
                        fulfillmentPallet.SKU = (string)clsGeneral.getColumnData(dRowItem, "Item_Code", string.Empty, false);
                        fulfillmentPallet.Comments = (string)clsGeneral.getColumnData(dRowItem, "Comments", string.Empty, false);
                        fulfillmentPallet.PalletID = (string)clsGeneral.getColumnData(dRowItem, "PalletID", string.Empty, false);
                        fulfillmentPallet.ShippingAddressLine1 = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_Address", string.Empty, false);
                        fulfillmentPallet.ShippingAddressLine2 = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_Address2", string.Empty, false);
                        fulfillmentPallet.ShippingCity = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_City", string.Empty, false);
                        fulfillmentPallet.ShippingState = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_State", string.Empty, false);
                        fulfillmentPallet.ShippingZipCode = (string)clsGeneral.getColumnData(dRowItem, "ShipTo_Zip", string.Empty, false);
                        fulfillmentPallet.CustomerName = (string)clsGeneral.getColumnData(dRowItem, "CompanyName", string.Empty, false);
                        fulfillmentPallet.CartonCount = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "CartonCount", 0, false)).ToString();
                        fulfillmentPallet.ItemCount = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ItemCount", 0, false)).ToString();
                        fulfillmentPallet.SNo = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "SNo", 0, false)).ToString();
                        fulfillmentPallet.TotalPallet = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "TotalPallet", 0, false)).ToString();
                        

                        fulfillmentPallet.AddressLine1 = shipFromAddress;                        
                        fulfillmentPallet.AddressLine2 = "";
                        fulfillmentPallet.City = shipFromCity;
                        fulfillmentPallet.State = shipFromState;
                        fulfillmentPallet.CompanyName = shipFromContactName;
                        fulfillmentPallet.ZipCode = shipFromZip;

                        //Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "ShipTo_Address", 0, false));
                        pallets.Add(fulfillmentPallet);
                    }



                }
            }
            catch (Exception ex)
            {
                throw new Exception("PopulateContainerIDs : " + ex.Message);
            }


            return pallets;
        }


    }
}