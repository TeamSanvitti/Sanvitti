using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Fulfillment
{
    public class UEDFOperation : BaseCreateInstance
    {

        public edfFileGeneric GetUedfFileDetail (int  POID, string transDate)
        {
            edfFileGeneric edfFileInfo = default;// new edfFileGeneric();

            string errorMessage = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;//new DataSet();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", POID);


                    //arrSpFieldSeq = new string[] { "@piStatusID" };
                    arrSpFieldSeq = new string[] { "@POID" };
                    ds = db.GetDataSet(objCompHash, "av_UedfFile_Select", arrSpFieldSeq);

                    edfFileInfo = PopulateEdfFile(ds, transDate);
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrWhiteSpace(errorMessage))
                        errorMessage = ex.ToString();
                    else
                        errorMessage = string.Concat(errorMessage, ex.ToString());

                }
                finally
                {
                    //db = null;

                }
            }

            return edfFileInfo;
        }

        public  edfFileGeneric GetAuthorizationFileDetail(int POID)
        {
            edfFileGeneric edfFileInfo = default;//new edfFileGeneric();

            string errorMessage = default;//null;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;// new DataSet();
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@POID", POID);

                    //arrSpFieldSeq = new string[] { "@piStatusID" };
                    arrSpFieldSeq = new string[] { "@POID" };
                    ds = db.GetDataSet(objCompHash, "av_UedfFile_Select", arrSpFieldSeq);

                    edfFileInfo = PopulateAuthorizationFile(ds);
                }
                catch (Exception ex)
                {
                    if (string.IsNullOrWhiteSpace(errorMessage))
                        errorMessage = ex.ToString();
                    else
                        errorMessage = string.Concat(errorMessage, ex.ToString());
                }
                finally
                {
                    //db = null;

                }
            }

            return edfFileInfo;
        }


        private static edfFileGeneric PopulateEdfFile(DataSet ds, string transDate)
        {

            edfFileGeneric edfFile = default;//null;// new List<PurchaseOrderShipmentDB>();
            edfHeader edfHeader = default;//null;
            edfData edfData = default;//null;
            product product = default;//null;
            skuInfo skuInfo = default;//default;//null;
            List<detail> details = default;//null;
            detail detail = default;//null;
            shipping shipping = default;//null;
            List<shipping> shippings = default;//null;
            device device = default;//null;
            List<device> devices = default;//null;

            if (ds != null && ds.Tables.Count > 0 & ds.Tables[0].Rows.Count > 0)
            {
                edfFile = new edfFileGeneric();
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    edfData = new edfData();
                    edfHeader = new edfHeader();
                    product = new product();
                    skuInfo = new skuInfo();
                    details = new List<detail>();
                    edfFile.date = transDate; //Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.Now, false)).ToString("yyyy-MM-dd");
                    edfFile.totalDeviceCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TotalQty", 0, false));
                    edfFile.headerCount = 1;

                    edfHeader.deviceCount = edfFile.totalDeviceCount;
                    edfHeader.locationDestination = "022";
                    edfHeader.uedfRevisionNumber = "XMLAUTH";
                    edfHeader.transactionType = "010";
                    edfHeader.phoneType = "RW";
                    edfHeader.phoneOwnership = "DISH";
                    edfHeader.poOrder = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;

                    product.edfSerialType = "H";
                    product.entSerialType = "E";

                    skuInfo.equipType = "CP";
                    //skuInfo.manufId = 436;
                    //skuInfo.manufName = "APPLE, INC";
                    skuInfo.masterSerialAttribute = "imeiDec";
                    skuInfo.preferredSerialIndicator = "imeiDec";
                    skuInfo.manufId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ManufactureCode", 436, false));
                    skuInfo.manufName = clsGeneral.getColumnData(dataRow, "DisplayName", string.Empty, false) as string;

                    skuInfo.sku = clsGeneral.getColumnData(dataRow, "Item_Code", string.Empty, false) as string;
                    skuInfo.skuName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    skuInfo.sfwVer = clsGeneral.getColumnData(dataRow, "SWVersion", string.Empty, false) as string;

                    product.skuInfo = skuInfo;

                    if (ds.Tables.Count > 1 & ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow1 in ds.Tables[1].Rows)
                        {
                            detail = new detail();
                            edfFile.fileSequence = Convert.ToInt32(clsGeneral.getColumnData(dataRow1, "fileSequence", 0, false)); ;

                            detail.lpn = clsGeneral.getColumnData(dataRow1, "PalletID", string.Empty, false) as string;
                            if (ds.Tables.Count > 2 & ds.Tables[2].Rows.Count > 0)
                            {
                                shippings = new List<shipping>();
                                foreach (DataRow dataRow2 in ds.Tables[2].Select("PalletID = '" + detail.lpn + "'"))
                                {
                                    shipping = new shipping();
                                    shipping.carton = clsGeneral.getColumnData(dataRow2, "ContainerID", string.Empty, false) as string;

                                    if (ds.Tables.Count > 3 & ds.Tables[3].Rows.Count > 0)
                                    {
                                        devices = new List<device>();
                                        foreach (DataRow dataRow3 in ds.Tables[3].Select("ContainerID = '" + shipping.carton + "'"))
                                        {
                                            device = new device();
                                            device.meidHex = clsGeneral.getColumnData(dataRow3, "MeidHex", string.Empty, false) as string;
                                            device.meidDec = clsGeneral.getColumnData(dataRow3, "MeidDec", string.Empty, false) as string;
                                            device.imeiDec = clsGeneral.getColumnData(dataRow3, "ESN", string.Empty, false) as string;
                                            device.serialNumber = clsGeneral.getColumnData(dataRow3, "serialNumber", string.Empty, false) as string;
                                            device.msl = clsGeneral.getColumnData(dataRow3, "MSL", string.Empty, false) as string;
                                            device.otksl = clsGeneral.getColumnData(dataRow3, "otksl", string.Empty, false) as string;
                                            devices.Add(device);
                                        }
                                        shipping.devices = devices;
                                    }

                                    shippings.Add(shipping);
                                }
                                detail.shippings = shippings;
                            }
                            details.Add(detail);
                        }
                        product.details = details;
                    }
                    edfData.product = product;
                    edfData.edfHeader = edfHeader;
                }
            }
            edfFile.edfData = edfData;
            return edfFile;
        }

        private static edfFileGeneric PopulateAuthorizationFile(DataSet ds)
        {
            edfFileGeneric edfFile = default;//null;// new List<PurchaseOrderShipmentDB>();
            edfHeader edfHeader = default;//null;
            edfData edfData = default;//null;
            product product = default;//null;
            skuInfo skuInfo = default;//null;
            List<detail> details = default;// null;
            detail detail = default;//null;
            shipping shipping = default;//null;
            List<shipping> shippings = default;//null;
            device device = default;//null;
            List<device> devices = default;//null;

            if (ds != null && ds.Tables.Count > 0 & ds.Tables[0].Rows.Count > 0)
            {
                edfFile = new edfFileGeneric();
                foreach (DataRow dataRow in ds.Tables[0].Rows)
                {
                    edfData = new edfData();
                    edfHeader = new edfHeader();
                    product = new product();
                    skuInfo = new skuInfo();
                    details = new List<detail>();
                    edfFile.date = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ShipTo_Date", DateTime.Now, false)).ToString("yyyy-MM-dd");
                    edfFile.totalDeviceCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TotalQty", 0, false));
                    edfFile.headerCount = 1;

                    edfHeader.deviceCount = edfFile.totalDeviceCount;
                    edfHeader.locationDestination = "000";
                    edfHeader.uedfRevisionNumber = "XMLAUTH";
                    edfHeader.transactionType = "040";
                    edfHeader.phoneType = "RW";
                    edfHeader.phoneOwnership = "DISH";
                    edfHeader.poOrder = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string; ;

                    product.edfSerialType = "H";
                    product.entSerialType = "E";

                    skuInfo.equipType = "CP";
                    skuInfo.manufId = 436;
                    skuInfo.manufName = "APPLE, INC";
                    skuInfo.masterSerialAttribute = "imeiDec";
                    skuInfo.preferredSerialIndicator = "imeiDec";
                    skuInfo.sku = clsGeneral.getColumnData(dataRow, "Item_Code", string.Empty, false) as string; ;
                    skuInfo.skuName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string; ;
                    skuInfo.sfwVer = clsGeneral.getColumnData(dataRow, "SWVersion", string.Empty, false) as string;

                    product.skuInfo = skuInfo;

                    if (ds.Tables.Count > 1 & ds.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow1 in ds.Tables[1].Rows)
                        {
                            detail = new detail();
                            edfFile.fileSequence = Convert.ToInt32(clsGeneral.getColumnData(dataRow1, "fileSequence", 0, false)); ;

                            detail.lpn = clsGeneral.getColumnData(dataRow1, "PalletID", string.Empty, false) as string;
                            if (ds.Tables.Count > 2 & ds.Tables[2].Rows.Count > 0)
                            {
                                shippings = new List<shipping>();
                                foreach (DataRow dataRow2 in ds.Tables[2].Select("PalletID = '" + detail.lpn + "'"))
                                {
                                    shipping = new shipping();
                                    shipping.carton = clsGeneral.getColumnData(dataRow2, "ContainerID", string.Empty, false) as string;

                                    if (ds.Tables.Count > 3 & ds.Tables[3].Rows.Count > 0)
                                    {
                                        devices = new List<device>();
                                        foreach (DataRow dataRow3 in ds.Tables[3].Select("ContainerID = '" + shipping.carton + "'"))
                                        {
                                            device = new device();
                                            device.meidHex = clsGeneral.getColumnData(dataRow3, "MeidHex", string.Empty, false) as string;
                                            device.meidDec = clsGeneral.getColumnData(dataRow3, "MeidDec", string.Empty, false) as string;
                                            device.imeiDec = clsGeneral.getColumnData(dataRow3, "ESN", string.Empty, false) as string;
                                            device.serialNumber = clsGeneral.getColumnData(dataRow3, "serialNumber", string.Empty, false) as string;
                                            device.msl = clsGeneral.getColumnData(dataRow3, "MSL", string.Empty, false) as string;
                                            device.otksl = clsGeneral.getColumnData(dataRow3, "otksl", string.Empty, false) as string;
                                            devices.Add(device);
                                        }
                                        shipping.devices = devices;
                                    }

                                    shippings.Add(shipping);
                                }
                                detail.shippings = shippings;
                            }
                            details.Add(detail);
                        }
                        product.details = details;
                    }
                    edfData.product = product;
                    edfData.edfHeader = edfHeader;
                }
            }
            edfFile.edfData = edfData;
            return edfFile;
        }

    }
}
