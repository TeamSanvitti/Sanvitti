using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Inventory
{
    public class NonEsnOperation : BaseCreateInstance
    {
        public List<NonEsnHeader> GetNonESNwithHeaderList(int companyID, string CustOrderNumber, string ShipFrom, string ShipTo, string SKU, int categoryID, string location)
        {
            List<NonEsnHeader> headerList = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();
                
                try
                {
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@CustOrderNumber", CustOrderNumber);
                    objCompHash.Add("@ShipFrom", string.IsNullOrEmpty(ShipFrom) ? null : ShipFrom);
                    objCompHash.Add("@ShipTo", string.IsNullOrEmpty(ShipTo) ? null : ShipTo);
                    //objCompHash.Add("@ESN", ESN);
                    //objCompHash.Add("@TrackingNumber", TrackingNumber);
                    objCompHash.Add("@SKU", SKU);
                    objCompHash.Add("@CategoryID", categoryID);
                    objCompHash.Add("@Location", location);


                    arrSpFieldSeq = new string[] { "@CompanyID", "@CustOrderNumber", "@ShipFrom", "@ShipTo", "@SKU", "@CategoryID", "@Location" };
                    dt = db.GetTableRecords(objCompHash, "Av_NonESN_Receive_Select", arrSpFieldSeq);
                    headerList = PopulateESNwithHeaders(dt);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    // db = null;
                }
            }
            return headerList;
        }

        public NonEsnHeader GetNonESNwithHeaderDetails(int ESNHeaderId)
        {
            NonEsnHeader nonEsnHeader = default;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataSet ds = default;//new DataSet();
                Hashtable objCompHash = new Hashtable();
                
                try
                {
                    //objCompHash.Add("@CompanyID", companyID);
                    //objCompHash.Add("@CustOrderNumber", CustOrderNumber);
                    //objCompHash.Add("@ShipFrom", string.IsNullOrEmpty(ShipFrom) ? null : ShipFrom);
                    //objCompHash.Add("@ShipTo", string.IsNullOrEmpty(ShipTo) ? null : ShipTo);
                    ////objCompHash.Add("@ESN", ESN);
                    ////objCompHash.Add("@TrackingNumber", TrackingNumber);
                    //objCompHash.Add("@SKU", SKU);
                    //objCompHash.Add("@CategoryID", categoryID);
                    //// objCompHash.Add("@ESNHeaderId", 0);
                    objCompHash.Add("@ESNHeaderId", ESNHeaderId);


                    arrSpFieldSeq = new string[] { "@ESNHeaderId" };
                    ds = db.GetDataSet(objCompHash, "Av_NonESN_Receive_Select", arrSpFieldSeq);
                    nonEsnHeader = PopulateESNwithHeaderDetail(ds);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return nonEsnHeader;
        }

        public  int NonESNInventoryInsert(NonESNInventory request, out int insertCout, out int updateCount, out string errorMessage)
        {
            insertCout = 0;
            updateCount = 0;
            errorMessage = default;
            //ESNAuthorizationID = 0;
            int returnResult = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();
                //  string nonesnXML = clsGeneral.SerializeObject(request.nonEsnList);

                DataTable dtStorageData = NonESNData(request.nonEsnList);
                // List<RawSKUInfo> skuList = null;
                try
                {
                    objCompHash.Add("@CompanyID", request.CompanyID);
                    objCompHash.Add("@ESNHeaderId", request.ESNHeaderId);
                    objCompHash.Add("@OrderNumber", request.OrderNumber);
                    objCompHash.Add("@CustOrderNumber", request.CustomerOrderNumber);
                    objCompHash.Add("@TotalQty", request.TotalQuantity);
                    objCompHash.Add("@PalletCount", request.PalletCount);
                    objCompHash.Add("@CartonCount", request.CartonCount);
                    objCompHash.Add("@PiecesPerBox", request.PiecesPerBox);
                    objCompHash.Add("@ItemCompanyGUID", request.ItemCompanyGUID);

                    objCompHash.Add("@UserID", request.UserID);
                    objCompHash.Add("@Comment", request.Comment);
                    objCompHash.Add("@ReceivedAs", request.ReceivedAs);
                    objCompHash.Add("@SupplierName", request.SupplierName);
                    objCompHash.Add("@InventoryStatusID", 2);
                    objCompHash.Add("@OrderTransferID", request.OrderTransferID);
                    objCompHash.Add("@TransientOrderID", request.TransientOrderID);

                    objCompHash.Add("@avNonESNUpload", dtStorageData);

                    arrSpFieldSeq = new string[] { "@CompanyID", "@ESNHeaderId", "@OrderNumber", "@CustOrderNumber", "@TotalQty", "@PalletCount",
                    "@CartonCount","@PiecesPerBox", "@ItemCompanyGUID", "@UserID", "@Comment", "@ReceivedAs","@SupplierName", "@InventoryStatusID",
                        "@OrderTransferID", "@TransientOrderID", "@avNonESNUpload" };
                    db.ExecCommand(objCompHash, "Av_NonESN_InsertUpdate", arrSpFieldSeq, "@poInsertCount", out insertCout, "@poUpdateCount", out updateCount, "@poErrorMessage", out errorMessage, "@InvalidStock", out returnResult);

                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return returnResult;
        }

        public int NonESNInventoryDelete(int ESNHeaderId, int UserID,  out string errorMessage)
        {
            errorMessage = default;
            //ESNAuthorizationID = 0;
            int returnResult = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                //DataTable dt = default;//new DataTable();
                Hashtable objCompHash = new Hashtable();
                
                try
                {
                    objCompHash.Add("@ESNHeaderId", ESNHeaderId);                    
                    objCompHash.Add("@UserID", UserID);
                    
                    arrSpFieldSeq = new string[] { "@ESNHeaderId", "@UserID" };
                    db.ExeCommand(objCompHash, "Av_NonESN_Delete", arrSpFieldSeq,  "@poErrorMessage", out errorMessage );
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    //db = null;
                }
            }
            return returnResult;
        }

        private static NonEsnHeader PopulateESNwithHeaderDetail(DataSet dataset)
        {
            // List<EsnHeaders> headerList = new List<EsnHeaders>();
            NonEsnHeader headerDetail = default;//new NonEsnHeader();
            NonEsnStorage esnInfo = default;//null;
            List<NonEsnStorage> StorageList = default;//null;
            if (dataset != null && dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                headerDetail = new NonEsnHeader();
                foreach (DataRow dataRow in dataset.Tables[0].Rows)
                {
                    StorageList = new List<NonEsnStorage>();

                    //objESN.ESN = clsGeneral.getColumnData(dataRow, "ESN", string.Empty, false) as string;
                    headerDetail.UploadDate = clsGeneral.getColumnData(dataRow, "OrderDate", string.Empty, false) as string;
                    headerDetail.CustomerOrderNumber = clsGeneral.getColumnData(dataRow, "CustOrderNumber", string.Empty, false) as string;
                    headerDetail.ESNHeaderId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ESNHeaderId", 0, false));
                   // headerDetail.OrderDate = clsGeneral.getColumnData(dataRow, "OrderDate", string.Empty, false) as string;
                    headerDetail.OrderNumber = clsGeneral.getColumnData(dataRow, "OrderNumber", string.Empty, false) as string;
                    headerDetail.TotalQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderQty", 0, false));
                    //headerDetail.ShipQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ShipQty", 0, false));
                    headerDetail.CompanyAccountNumber = clsGeneral.getColumnData(dataRow, "CompanyAccountNumber", string.Empty, false) as string;
                    //headerDetail.ShipDate = clsGeneral.getColumnData(dataRow, "ShipDate", string.Empty, false) as string;
                    //objESN.ICC_ID = clsGeneral.getColumnData(dataRow, "icc_id", string.Empty, false) as string;
                    //headerDetail.Shipvia = clsGeneral.getColumnData(dataRow, "Shipvia", string.Empty, false) as string;
                    //headerDetail.UnitPrice = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "UnitPrice", 0, false));
                    headerDetail.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    headerDetail.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                    headerDetail.CategoryID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryID", 0, false));
                    headerDetail.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    headerDetail.ReceivedAs = clsGeneral.getColumnData(dataRow, "ReceivedAs", string.Empty, false) as string;
                    headerDetail.IsInspection = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsInspection", false, false));
                    headerDetail.CartonCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CartonCount", 0, false));
                    headerDetail.PiecesPerBox = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PiecesPerBox", 0, false));
                    headerDetail.PalletCount = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PalletCount", 0, false));
                    headerDetail.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                    headerDetail.CategoryID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryID", 0, false));


                    headerDetail.CategoryWithProductAllowed = clsGeneral.getColumnData(dataRow, "CategoryWithProductAllowed", string.Empty, false) as string;
                    headerDetail.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    headerDetail.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                    headerDetail.SupplierName = clsGeneral.getColumnData(dataRow, "SupplierName", string.Empty, false) as string;
                    //headerDetail.TrackingNumber = clsGeneral.getColumnData(dataRow, "TrackingNumber", string.Empty, false) as string;

                    if (dataset.Tables.Count > 1 && dataset.Tables[1].Rows.Count > 0)
                    {
                        foreach (DataRow row in dataset.Tables[1].Rows)
                        {
                            esnInfo = new NonEsnStorage();
                            esnInfo.WareHouseLocation = clsGeneral.getColumnData(row, "WareHouseLocation", string.Empty, false) as string;
                            esnInfo.SequenceNumber = Convert.ToInt32(clsGeneral.getColumnData(row, "SequenceNumber", 0, false));
                            esnInfo.Quantity = Convert.ToInt32(clsGeneral.getColumnData(row, "Quantity", 0, false));
                            esnInfo.BoxID = clsGeneral.getColumnData(row, "BoxID", string.Empty, false) as string;
                            esnInfo.Inspected = Convert.ToBoolean(clsGeneral.getColumnData(row, "Inspected", false, false));

                            StorageList.Add(esnInfo);
                        }
                    }
                    headerDetail.StorageList = StorageList;
                    // headerList.Add(objESN);
                }
            }
            return headerDetail;

        }

        private List<NonEsnHeader> PopulateESNwithHeaders(DataTable dt)
        {
            List<NonEsnHeader> headerList = default;//List<NonEsnHeader>();
            NonEsnHeader objESN = default;//null;


            if (dt != null && dt.Rows.Count > 0)
            {
                headerList = new List<NonEsnHeader>();
                foreach (DataRow dataRow in dt.Rows)
                {
                    objESN = new NonEsnHeader();
                    //objESN.WareHouseLocation = clsGeneral.getColumnData(dataRow, "WareHouseLocation", string.Empty, false) as string;
                    //objESN.BoxID = clsGeneral.getColumnData(dataRow, "BoxID", string.Empty, false) as string;
                    objESN.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    objESN.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                    objESN.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                    objESN.ReceivedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "UploadDate", DateTime.Now, false));
                    objESN.UploadDate = clsGeneral.getColumnData(dataRow, "OrderDate", string.Empty, false) as string;
                    objESN.CustomerOrderNumber = clsGeneral.getColumnData(dataRow, "CustOrderNumber", string.Empty, false) as string;
                    objESN.ESNHeaderId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ESNHeaderId", 0, false));
                   // objESN.OrderDate = clsGeneral.getColumnData(dataRow, "OrderDate", string.Empty, false) as string;
                    objESN.OrderNumber = clsGeneral.getColumnData(dataRow, "OrderNumber", string.Empty, false) as string;
                   objESN.TotalQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderQty", 0, false));
                    objESN.AssignedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AssignedQty", 0, false));

                    //objESN.ShipDate = clsGeneral.getColumnData(dataRow, "ShipDate", string.Empty, false) as string;
                    //objESN.ICC_ID = clsGeneral.getColumnData(dataRow, "icc_id", string.Empty, false) as string;
                    //objESN.Shipvia = clsGeneral.getColumnData(dataRow, "Shipvia", string.Empty, false) as string;
                    //objESN.UnitPrice = Convert.ToDecimal(clsGeneral.getColumnData(dataRow, "UnitPrice", 0, false));

                    //objESN.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                    objESN.ReceivedAs = clsGeneral.getColumnData(dataRow, "ReceivedAs", string.Empty, false) as string;
                    objESN.IsESN = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESN", 0, false));
                    //objESN.IsInspection = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsInspection", 0, false));
                    objESN.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;

                    headerList.Add(objESN);
                }
            }
            return headerList;

        }

        private  DataTable NonESNData(List<NonEsnStorage> nonEsnList)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("SquenceNumber", typeof(System.Int32));
            dt.Columns.Add("WareHouseLocation", typeof(System.String));
            dt.Columns.Add("BoxID", typeof(System.String));
            dt.Columns.Add("Quantity", typeof(System.Int32));
            dt.Columns.Add("Inspected", typeof(System.Boolean));
            DataRow row;

            if (nonEsnList != null && nonEsnList.Count > 0)
            {
                foreach (NonEsnStorage item in nonEsnList)
                {
                    row = dt.NewRow();
                    row["SquenceNumber"] = item.SequenceNumber;
                    row["WareHouseLocation"] = item.WareHouseLocation;
                    row["BoxID"] = item.BoxID;
                    row["Quantity"] = item.Quantity;
                    row["Inspected"] = item.Inspected;
                    
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

    }
}
