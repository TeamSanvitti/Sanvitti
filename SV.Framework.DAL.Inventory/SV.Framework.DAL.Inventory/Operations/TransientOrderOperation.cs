using System;
using System.Collections;
using System.Collections.Generic;
//using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;
using System.Data;

namespace SV.Framework.DAL.Inventory
{
    public class TransientOrderOperation : BaseCreateInstance
    {
        public string CreateTransientOrderInsertUpdate(TransientReceiveOrder request,  out Int64 transientOrderID)
        {
            string errorMessage = default;
            transientOrderID = 0;
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@TransientOrderID", request.TransientOrderID);
                    objCompHash.Add("@TransientOrderDate", request.TransientOrderDateTime);
                    objCompHash.Add("@CompanyID", request.CompanyID);
                    objCompHash.Add("@ItemCompanyGUID", request.ItemCompanyGUID);
                    objCompHash.Add("@OrderedQty", request.OrderedQty);
                    objCompHash.Add("@SupplierName", request.SupplierName);
                    objCompHash.Add("@ProposedReceiveDate", request.ProposedReceiveDate);
                    objCompHash.Add("@Comments", request.Comment);
                    objCompHash.Add("@UserID", request.UserID);
                    objCompHash.Add("@RequestedBy", request.RequestedBy);

                    arrSpFieldSeq = new string[] { "@TransientOrderID", "@TransientOrderDate", "@CompanyID", "@ItemCompanyGUID","@OrderedQty",
                        "@SupplierName","@ProposedReceiveDate", "@Comments","@UserID","@RequestedBy" };
                    db.ExeCommand(objCompHash, "av_TransientOrderInsertUpdate", arrSpFieldSeq, "@poErrorMessage", "@poErrorMessage", out errorMessage, out transientOrderID);
                }
                catch (Exception exp)
                {
                    errorMessage = exp.Message;
                    Logger.LogMessage(exp, this);   //throw exp;
                }
            }
            return errorMessage;
        }

        public List<TransientReceiveOrder> GetTransientOrders(int MemoNumber, string SKU, int CompanyID, string fromDate, string toDate, string SupplierName)
        {
            List<TransientReceiveOrder> orderList = default;//
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@TransientOrderID", 0);
                    objCompHash.Add("@MemoNumber", MemoNumber);
                    objCompHash.Add("@SKU", SKU);
                    objCompHash.Add("@CompanyID", CompanyID);
                    objCompHash.Add("@OrderTransferDateFrom", string.IsNullOrEmpty(fromDate) ? null : fromDate);
                    objCompHash.Add("@OrderTransferDateTo", string.IsNullOrEmpty(toDate) ? null : toDate);
                    objCompHash.Add("@SupplierName", SupplierName);

                    arrSpFieldSeq = new string[] { "@TransientOrderID", "@MemoNumber", "@SKU", "@CompanyID", "@DateFrom", "@DateTo", "@SupplierName" };

                    dataTable = db.GetTableRecords(objCompHash, "av_TransientOrder_Select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        orderList = PopulateOrders(dataTable);
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return orderList;
        }
        public TransientReceiveOrder GetTransientOrderDetail(Int64 TransientOrderID)
        {
            TransientReceiveOrder orderInfo = default;//
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@TransientOrderID", TransientOrderID);
                    //objCompHash.Add("@MemoNumber", MemoNumber);
                    //objCompHash.Add("@SKU", SKU);
                    //objCompHash.Add("@CompanyID", CompanyID);
                    //objCompHash.Add("@OrderTransferDateFrom", string.IsNullOrEmpty(fromDate) ? null : fromDate);
                    //objCompHash.Add("@OrderTransferDateTo", string.IsNullOrEmpty(toDate) ? null : toDate);
                    //objCompHash.Add("@SupplierName", SupplierName);

                    arrSpFieldSeq = new string[] { "@TransientOrderID", "@MemoNumber", "@SKU", "@CompanyID", "@DateFrom", "@DateTo", "@SupplierName" };

                    dataTable = db.GetTableRecords(objCompHash, "av_TransientOrder_Select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        orderInfo = PopulateOrderDetail(dataTable);
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return orderInfo;
        }

        public string OrderTransientStatusUpdate(Int64 TransientOrderID, string OrderTransientStatus, int userID)
        {
            string returnMessage = "Not " + OrderTransientStatus;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@TransientOrderID", TransientOrderID);
                    objCompHash.Add("@OrderTransientStatus", OrderTransientStatus);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@TransientOrderID", "@OrderTransientStatus", "@UserID" };
                    int returnValue = db.ExecuteNonQuery(objCompHash, "av_TransientOrderStatusUpdate", arrSpFieldSeq);
                    if (returnValue > 0)
                        returnMessage = OrderTransientStatus + " successfully";
                    // else
                    //   returnMessage = "Warehouse location in use you cannot delete!";
                }
                catch (Exception exp)
                {
                    returnMessage = exp.Message;
                    Logger.LogMessage(exp, this);   //throw exp;
                }
            }
            return returnMessage;
        }

        public List<TransientOrderAssignment> GetTransientOrderAssignments(Int64 TransientOrderID)
        {
            List<TransientOrderAssignment> orderList = default;// new List<Carriers>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@TransientOrderID", TransientOrderID);


                    arrSpFieldSeq = new string[] { "@TransientOrderID" };

                    dataTable = db.GetTableRecords(objCompHash, "av_TransientOrderReceive_Select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        orderList = PopulateTransferOrderReceive(dataTable);
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this);   //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return orderList;
        }
        private List<TransientOrderAssignment> PopulateTransferOrderReceive(DataTable dataTable)
        {
            List<TransientOrderAssignment> orderList = default;// new List<Carriers>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    orderList = new List<TransientOrderAssignment>();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        TransientOrderAssignment orderInfo = new TransientOrderAssignment();
                        //orderInfo.AssignedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderTransferID", 0, false));
                       // orderInfo.RequestedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RequestedQty", 0, false));
                        orderInfo.ReceivedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ReceivedQty", 0, false));
                        orderInfo.MemoNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MemoNumber", 0, false));
                        orderInfo.SKU = clsGeneral.getColumnData(dataRow, "DestinationSKU", string.Empty, false) as string;
                        orderInfo.ProductName = clsGeneral.getColumnData(dataRow, "DestinationItemName", string.Empty, false) as string;
                        orderInfo.TransientOrderDate = clsGeneral.getColumnData(dataRow, "TransientOrderDate", string.Empty, false) as string;
                        orderInfo.ReceivedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ReceivedDate", DateTime.Now, false));
                        orderInfo.OrderTransientStatus = clsGeneral.getColumnData(dataRow, "OrderTransientStatus", string.Empty, false) as string;
                        orderInfo.OrderTransientReceiveStatus = clsGeneral.getColumnData(dataRow, "OrderTransientReceiveStatus", string.Empty, false) as string;
                        orderInfo.ReceivedByUser = clsGeneral.getColumnData(dataRow, "Username", string.Empty, false) as string;
                        orderInfo.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;

                        //orderInfo.CreatedBy = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                        // warehouseInfo.Specialinstructions = clsGeneral.getColumnData(dataRow, "Specialinstructions", string.Empty, false) as string;


                        orderList.Add(orderInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex, this);   //throw ex;
            }
            return orderList;
        }

        private TransientReceiveOrder PopulateOrderDetail(DataTable dataTable)
        {
            TransientReceiveOrder orderInfo = default;

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    orderInfo= new TransientReceiveOrder();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        orderInfo.TransientOrderID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TransientOrderID", 0, false));
                        orderInfo.ReceivedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ReceivedQty", 0, false));
                        orderInfo.ToBeReceiveQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ToBeReceiveQty", 0, false));
                        orderInfo.OrderedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderedQty", 0, false));
                        orderInfo.CategoryID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryID", 0, false));
                        orderInfo.ItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ItemCompanyGUID", 0, false));
                        orderInfo.RequestedBy = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RequestedBy", 0, false));
                        orderInfo.Stock_in_Hand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_Hand", 0, false));
                        // orderInfo.DestinationStock_in_Hand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DestinationStock_in_Hand", 0, false));
                        orderInfo.MemoNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MemoNumber", 0, false));
                        orderInfo.IsESNRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESNRequired", false, false));
                        orderInfo.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        orderInfo.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                        orderInfo.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                        orderInfo.SupplierName = clsGeneral.getColumnData(dataRow, "SupplierName", string.Empty, false) as string;
                        //orderInfo.OrderTransferStatus = clsGeneral.getColumnData(dataRow, "OrderTransferStatus", string.Empty, false) as string;
                        // orderInfo.OrderTransferAssignmentStatus = clsGeneral.getColumnData(dataRow, "DestinationItemName", string.Empty, false) as string;
                        orderInfo.CustomerInfo = clsGeneral.getColumnData(dataRow, "CustomerInfo", string.Empty, false) as string;
                        //orderInfo.TransientOrderDate = clsGeneral.getColumnData(dataRow, "CreateDate", string.Empty, false) as string;
                        orderInfo.TransientOrderDateTime = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "TransientOrderDate", DateTime.Now, false));
                        orderInfo.ProposedReceiveDateTime = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ProposedReceiveDate", DateTime.Now, false));
                        orderInfo.OrderTransientStatus = clsGeneral.getColumnData(dataRow, "OrderTransientStatus", string.Empty, false) as string;
                        orderInfo.ApprovedUser = clsGeneral.getColumnData(dataRow, "ApprovedUser", string.Empty, false) as string;
                        //orderInfo.TransferedUser = clsGeneral.getColumnData(dataRow, "TransferedUser", string.Empty, false) as string;
                        orderInfo.CreatedByUser = clsGeneral.getColumnData(dataRow, "CreatedByUser", string.Empty, false) as string;
                        orderInfo.RequestedByUser = clsGeneral.getColumnData(dataRow, "RequestedByUser", string.Empty, false) as string;
                        orderInfo.RequestedBy = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RequestedBy", 0, false));

                        //orderInfo.CreatedBy = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                        // warehouseInfo.Specialinstructions = clsGeneral.getColumnData(dataRow, "Specialinstructions", string.Empty, false) as string;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex, this);   //throw ex;
            }
            return orderInfo;
        }


        private List<TransientReceiveOrder> PopulateOrders(DataTable dataTable)
        {
            List<TransientReceiveOrder> orderList = default;// new List<Carriers>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    orderList = new List<TransientReceiveOrder>();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        TransientReceiveOrder orderInfo = new TransientReceiveOrder();
                        orderInfo.TransientOrderID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TransientOrderID", 0, false));
                        orderInfo.ReceivedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ReceivedQty", 0, false));
                        orderInfo.ToBeReceiveQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ToBeReceiveQty", 0, false));
                        orderInfo.OrderedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderedQty", 0, false));
                        orderInfo.Stock_in_Hand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Stock_in_Hand", 0, false));
                       // orderInfo.DestinationStock_in_Hand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DestinationStock_in_Hand", 0, false));
                        orderInfo.MemoNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "MemoNumber", 0, false));
                        orderInfo.IsESNRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESNRequired", false, false));
                        orderInfo.CustomerName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        orderInfo.CategoryName = clsGeneral.getColumnData(dataRow, "CategoryName", string.Empty, false) as string;
                        orderInfo.SKU = clsGeneral.getColumnData(dataRow, "SKU", string.Empty, false) as string;
                        orderInfo.ProductName = clsGeneral.getColumnData(dataRow, "ItemName", string.Empty, false) as string;
                        orderInfo.SupplierName = clsGeneral.getColumnData(dataRow, "SupplierName", string.Empty, false) as string;

                        //orderInfo.OrderTransferStatus = clsGeneral.getColumnData(dataRow, "OrderTransferStatus", string.Empty, false) as string;

                        // orderInfo.OrderTransferAssignmentStatus = clsGeneral.getColumnData(dataRow, "DestinationItemName", string.Empty, false) as string;
                         orderInfo.CustomerInfo = clsGeneral.getColumnData(dataRow, "CustomerInfo", string.Empty, false) as string;
                        //orderInfo.TransferOrderDate = clsGeneral.getColumnData(dataRow, "CreateDate", string.Empty, false) as string;
                        orderInfo.TransientOrderDateTime = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "TransientOrderDate", DateTime.Now, false));
                        orderInfo.ProposedReceiveDateTime = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "ProposedReceiveDate", DateTime.Now, false));
                        orderInfo.OrderTransientStatus = clsGeneral.getColumnData(dataRow, "OrderTransientStatus", string.Empty, false) as string;

                        //orderInfo.ApprovedUser = clsGeneral.getColumnData(dataRow, "ApprovedUser", string.Empty, false) as string;
                        //orderInfo.TransferedUser = clsGeneral.getColumnData(dataRow, "TransferedUser", string.Empty, false) as string;
                        orderInfo.CreatedByUser = clsGeneral.getColumnData(dataRow, "CreatedByUser", string.Empty, false) as string;
                        orderInfo.RequestedByUser = clsGeneral.getColumnData(dataRow, "RequestedByUser", string.Empty, false) as string;

                        //orderInfo.CreatedBy = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                        // warehouseInfo.Specialinstructions = clsGeneral.getColumnData(dataRow, "Specialinstructions", string.Empty, false) as string;


                        orderList.Add(orderInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.LogMessage(ex, this);   //throw ex;
            }
            return orderList;
        }


    }
}
