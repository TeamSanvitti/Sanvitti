using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class DishLabelOperations : BaseCreateInstance
    {
        public  List<PalletModel> GetPalletLabelInfo(int poid, string poNumber, string palletID)
        {
            List<PalletModel> palletList = default;//null;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                
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
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return palletList;
        }

        public  List<MasterCartonInfo> GetMasterCartonLabelInfo(int poid)
        {
            List<MasterCartonInfo> cartonList = default;//null;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                
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
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return cartonList;
        }
        public  List<MasterCartonInfo> GetMasterCartonLabelByContainerID(string ContainerID, int poid)
        {
            List<MasterCartonInfo> cartonList = default;//null;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;//new DataSet();

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
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return cartonList;
        }

        //public  DataTable ESNData(List<SV.Framework.Inventory.EsnUploadNew> mslEsnList)
        //{
        //    DataTable dt = new DataTable();

        //    dt.Columns.Add("ESN", typeof(System.String));
        //    dt.Columns.Add("BatchNumber", typeof(System.String));
        //    dt.Columns.Add("ICC_ID", typeof(System.String));
        //    dt.Columns.Add("MeidHex", typeof(System.String));
        //    dt.Columns.Add("MeidDec", typeof(System.String));
        //    dt.Columns.Add("Location", typeof(System.String));
        //    dt.Columns.Add("MSL", typeof(System.String));
        //    dt.Columns.Add("OTKSL", typeof(System.String));
        //    dt.Columns.Add("SerialNumber", typeof(System.String));


        //    DataRow row;
        //    int rowNumber = 1;
        //    if (mslEsnList != null && mslEsnList.Count > 0)
        //    {
        //        foreach (SV.Framework.Inventory.EsnUploadNew item in mslEsnList)
        //        {
        //            row = dt.NewRow();
        //            row["ESN"] = item.ESN;
        //            row["BatchNumber"] = item.MslNumber;
        //            row["ICC_ID"] = rowNumber;
        //            row["MeidHex"] = item.MeidHex;
        //            row["MeidDec"] = item.MeidDec;
        //            row["Location"] = item.Location;
        //            row["MSL"] = item.MSL;
        //            row["OTKSL"] = item.OTKSL;
        //            row["SerialNumber"] = item.SerialNumber;

        //            dt.Rows.Add(row);
        //            rowNumber = rowNumber + 1;
        //        }
        //    }
        //    return dt;
        //}

        public  List<PosKitInfo> GetPosLabels(int companyID, int ESNAuthorizationID, DataTable dt)
        {
            List<PosKitInfo> posKits = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();


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
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return posKits;
        }

        private  DataTable ESNData(string[] esnList)
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

        public  List<PosKitInfo> GetPosLabelByESN(string esn, string shippRequestDate)
        {
            List<PosKitInfo> posKits = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;//new DataSet();

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
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                   // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return posKits;
        }

        public  List<PosKitInfo> GetPosLabel(int poid, string[] esnList)
        {
            List<PosKitInfo> posKits = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                
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
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //  db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return posKits;
        }

        public  List<PosKitInfo> GetPosKitLabel(int poid)
        {
            List<PosKitInfo> posKits = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();


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
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return posKits;
        }
        public  List<PalletCartonMap> GetPalletCartonMappingLabel(int poid)
        {
            List<PalletCartonMap> pallets = default;// null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;//new DataSet();


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
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return pallets;
        }
        public  List<CartonBoxID> GetPurchaseOrderBoxIDs(int companyID, string poNum, int poID, string BoxNumber)
        {
            List<CartonBoxID> boxList = default;//new List<CartonBoxID>();
           
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;// new DataTable();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@FulfillmentNumber", poNum);
                    objCompHash.Add("@POID", poID);
                    objCompHash.Add("@BoxNumber", BoxNumber);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@FulfillmentNumber", "@POID", "@BoxNumber" };
                    dt = db.GetTableRecords(objCompHash, "av_PurchaseOrderBoxID_Select", arrSpFieldSeq);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        boxList = PopulateCartonBoxes(dt);
                    }
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this);
                    //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return boxList;
        }
        private  List<CartonBoxID> PopulateCartonBoxes(DataTable dt)
        {
            List<CartonBoxID> cartonBoxes = default;//null;
            CartonBoxID cartonBoxID = default;//null;
            if (dt != null && dt.Rows.Count > 0)
            {
                cartonBoxes = new List<CartonBoxID>();
                foreach (DataRow row in dt.Rows)
                {
                    cartonBoxID = new CartonBoxID();
                    cartonBoxID.FulfillmentNumber = Convert.ToString(clsGeneral.getColumnData(row, "PO_Num", string.Empty, false));
                    cartonBoxID.SKU = Convert.ToString(clsGeneral.getColumnData(row, "Item_Code", string.Empty, false));
                    cartonBoxID.BoxID = Convert.ToString(clsGeneral.getColumnData(row, "BoxNumber", string.Empty, false));
                    cartonBoxID.BoxDesc = Convert.ToString(clsGeneral.getColumnData(row, "BoxDesc", string.Empty, false));
                    cartonBoxes.Add(cartonBoxID);
                }
            }

            return cartonBoxes;

        }

        private  List<PalletCartonMap> PopulatePalletCartonMapping(DataSet ds)
        {

            PalletCartonMap palletInfo = default;//null;
            List<PalletCartonMap> pallets = default;//null;
            CartonModel cartonItem = default;//null;
            List<CartonModel> CartonItems = default;//null;
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
                    pallets = new List<PalletCartonMap>();
                    foreach (DataRow dRowItem in ds.Tables[0].Rows)
                    {

                        palletInfo = new PalletCartonMap();
                        palletInfo.PalletID = (string)clsGeneral.getColumnData(dRowItem, "PalletID", string.Empty, false);
                        PoPalletId = Convert.ToInt32(clsGeneral.getColumnData(dRowItem, "PoPalletId", 0, false));
                        CartonItems = new List<CartonModel>();
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
                                    cartonItem = new CartonModel();
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


        private  List<PosKitInfo> PopulatePOSKit(DataSet ds)
        {

            List<PosKitInfo> posKits = default;//null;
            PosKitInfo posKitInfo = default;//null;
            List<KitBoxInfo> boxItems = default;//null;
            KitBoxInfo kitBoxInfo = default;//null;
            string dishactivationcode = System.Configuration.ConfigurationManager.AppSettings["dishactivationcode"].ToString();

            try
            {
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    posKits = new List<PosKitInfo>();
                    foreach (DataRow dRowItem in ds.Tables[0].Rows)
                    {
                        boxItems = new List<KitBoxInfo>();

                        posKitInfo = new PosKitInfo();
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
                                kitBoxInfo = new KitBoxInfo();
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

        private  List<MasterCartonInfo> PopulateCartons(DataSet ds)
        {
           
            MasterCartonInfo cartonInfo = default;//null;
            List<MasterCartonInfo> cartons = default;//null;
            CartonItem cartonItem = default;//null;
            List<CartonItem> CartonItems = default;//null;

            try
            {
                if (ds != null && ds.Tables.Count > 0  && ds.Tables[0].Rows.Count > 0)
                {
                    cartons = new List<MasterCartonInfo>();
                    foreach (DataRow dRowItem in ds.Tables[0].Rows)
                    {

                        cartonInfo = new MasterCartonInfo();
                        CartonItems = new List<CartonItem>();
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
                                cartonItem = new CartonItem();
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

        private  List<PalletModel> PopulatePalletID(DataTable dt)
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

            PalletModel fulfillmentPallet = default;//null;
            List<PalletModel> pallets = default;//null;
            string poNumber;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    pallets = new List<PalletModel>();
                    foreach (DataRow dRowItem  in dt.Rows)
                    {
                        //, , , , T1.Contact_Name, ,	, , , , T5.,

                        fulfillmentPallet = new PalletModel();
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