using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;
using System.Data;

namespace SV.Framework.DAL.Inventory
{
    public class TransferOrderOperation : BaseCreateInstance
    {
        public string CreateInternalTransferOrder(TransferOrder request)
        {
            string errorMessage = default;
           
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@OrderTransferID", request.OrderTransferID);
                    objCompHash.Add("@OrderTransferNumber", request.OrderTransferNumber);
                    objCompHash.Add("@OrderTransferDate", request.OrderTransferDate);
                    objCompHash.Add("@SourceCompanyID", request.SourceCompanyID);
                    objCompHash.Add("@SourceItemCompanyGUID", request.SourceItemCompanyGUID);
                    objCompHash.Add("@DestinationCompanyID", request.DestinationCompanyID);
                    objCompHash.Add("@DestinationItemCompanyGUID", request.DestinationItemCompanyGUID);
                    objCompHash.Add("@RequestedQty", request.RequestedQty);
                    objCompHash.Add("@RequestedBy", request.RequestedBy);
                    objCompHash.Add("@Comments", request.Comment);
                    objCompHash.Add("@CreatedBy", request.CreatedBy);

                    arrSpFieldSeq = new string[] { "@OrderTransferID", "@OrderTransferNumber", "@OrderTransferDate", "@SourceCompanyID","@SourceItemCompanyGUID",
                        "@DestinationCompanyID","@DestinationItemCompanyGUID", "@RequestedQty","@RequestedBy","@Comments","@CreatedBy" };
                    db.ExeCommand(objCompHash, "av_InternalTransferOrder_Insert", arrSpFieldSeq, "@poErrorMessage", out errorMessage);
                }
                catch (Exception exp)
                {
                    errorMessage = exp.Message;
                    Logger.LogMessage(exp, this);   //throw exp;
                }
            }
            return errorMessage;
        }


        public List<TransferOrder> GetTransferOrders(int OrderTransferNumber, string SKU, int DestinationCompanyID, string fromDate, string toDate)
        {
            List<TransferOrder> orderList = default;// new List<Carriers>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@OrderTransferID", 0);

                    objCompHash.Add("@OrderTransferNumber", OrderTransferNumber);
                    objCompHash.Add("@SKU", SKU);
                    objCompHash.Add("@DestinationCompanyID", DestinationCompanyID);
                    objCompHash.Add("@OrderTransferDateFrom", string.IsNullOrEmpty(fromDate)?null : fromDate);
                    objCompHash.Add("@OrderTransferDateTo", string.IsNullOrEmpty(toDate) ? null : toDate);

                    arrSpFieldSeq = new string[] { "@OrderTransferID", "@OrderTransferNumber", "@SKU", "@DestinationCompanyID", "@OrderTransferDateFrom", "@OrderTransferDateTo" };

                    dataTable = db.GetTableRecords(objCompHash, "av_InternalTransferOrder_Select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        orderList = PopulateTransferOrders(dataTable);
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

        public TransferOrder GetTransferOrderDetail(Int64 orderTransferID)
        {
            TransferOrder orderDetail = default;// new List<Carriers>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@OrderTransferID", orderTransferID);
                    objCompHash.Add("@OrderTransferNumber", "");
                    objCompHash.Add("@SKU", "");
                    objCompHash.Add("@DestinationCompanyID", 0);
                    objCompHash.Add("@OrderTransferDateFrom", null);
                    objCompHash.Add("@OrderTransferDateTo", null);

                    arrSpFieldSeq = new string[] { "@OrderTransferID", "@OrderTransferNumber", "@SKU", "@DestinationCompanyID", "@OrderTransferDateFrom", "@OrderTransferDateTo" };

                    dataTable = db.GetTableRecords(objCompHash, "av_InternalTransferOrder_Select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        orderDetail = PopulateTransferOrderDetail(dataTable);
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
            return orderDetail;
        }

        public string OrderTransferStatusUpdate(Int64 orderTransferID, string OrderTransferStatus, int userID)
        {
            string returnMessage = "Not " + OrderTransferStatus;

            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@OrderTransferID", orderTransferID);
                    objCompHash.Add("@OrderTransferStatus", OrderTransferStatus);
                    objCompHash.Add("@UserID", userID);

                    arrSpFieldSeq = new string[] { "@OrderTransferID", "@OrderTransferStatus", "@UserID" };
                    int returnValue = db.ExecuteNonQuery(objCompHash, "av_InternalTransferOrderStatusUpdate", arrSpFieldSeq);
                    if (returnValue > 0)
                        returnMessage = OrderTransferStatus + " successfully";
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

        public List<TransferOrderAssignment> GetTransferOrderAssignments(Int64 orderTransferId)
        {
            List<TransferOrderAssignment> orderList = default;// new List<Carriers>();
            using (DBConnect db = new DBConnect())
            {
                DataTable dataTable = default;// new DataTable();
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@OrderTransferID", orderTransferId);

                    
                    arrSpFieldSeq = new string[] { "@OrderTransferID" };

                    dataTable = db.GetTableRecords(objCompHash, "av_InternalTransferOrderAssignment_Select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        orderList = PopulateTransferOrderAssignments(dataTable);
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
        private List<TransferOrderAssignment> PopulateTransferOrderAssignments(DataTable dataTable)
        {
            List<TransferOrderAssignment> orderList = default;// new List<Carriers>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    orderList = new List<TransferOrderAssignment>();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        TransferOrderAssignment orderInfo = new TransferOrderAssignment();
                        //orderInfo.AssignedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderTransferID", 0, false));
                        orderInfo.RequestedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RequestedQty", 0, false));
                        orderInfo.TransferedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TransferedQty", 0, false));
                        orderInfo.OrderTransferNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderTransferNumber", 0, false));
                        orderInfo.SKU = clsGeneral.getColumnData(dataRow, "DestinationSKU", string.Empty, false) as string;
                        orderInfo.ProductName = clsGeneral.getColumnData(dataRow, "DestinationItemName", string.Empty, false) as string;
                        orderInfo.OrderTransferDate = clsGeneral.getColumnData(dataRow, "OrderTransferDate", string.Empty, false) as string;
                        orderInfo.TransferedDate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "TransferedDate", DateTime.Now, false));
                        orderInfo.OrderTransferStatus = clsGeneral.getColumnData(dataRow, "OrderTransferStatus", string.Empty, false) as string;
                        orderInfo.OrderTransferAssignmentStatus = clsGeneral.getColumnData(dataRow, "OrderTransferAssignmentStatus", string.Empty, false) as string;
                        orderInfo.AssignedBy = clsGeneral.getColumnData(dataRow, "Username", string.Empty, false) as string;
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

        private TransferOrder PopulateTransferOrderDetail(DataTable dataTable)
        {
            TransferOrder orderInfo = default;// new List<Carriers>();
            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    orderInfo = new TransferOrder();

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        orderInfo.OrderTransferID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderTransferID", 0, false));
                        orderInfo.DestinationItemCompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DestinationItemCompanyGUID", 0, false));
                        orderInfo.CategoryID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryID", 0, false));
                        orderInfo.RequestedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RequestedQty", 0, false));
                        orderInfo.SourceStock_in_Hand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "SourceStock_in_Hand", 0, false));
                        orderInfo.DestinationStock_in_Hand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DestinationStock_in_Hand", 0, false));
                        orderInfo.OrderTransferNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderTransferNumber", 0, false));
                        orderInfo.SourceCustomer = clsGeneral.getColumnData(dataRow, "SourceCustomer", string.Empty, false) as string;
                        orderInfo.SourceSKU = clsGeneral.getColumnData(dataRow, "SourceSKU", string.Empty, false) as string;
                        orderInfo.SourceItemName = clsGeneral.getColumnData(dataRow, "SourceItemName", string.Empty, false) as string;
                        orderInfo.DestinationCompanyName = clsGeneral.getColumnData(dataRow, "DestinationCompanyName", string.Empty, false) as string;
                        orderInfo.CustomerInfo = clsGeneral.getColumnData(dataRow, "CustomerInfo", string.Empty, false) as string;
                        orderInfo.DestinationSKU = clsGeneral.getColumnData(dataRow, "DestinationSKU", string.Empty, false) as string;
                        orderInfo.DestinationItemName = clsGeneral.getColumnData(dataRow, "DestinationItemName", string.Empty, false) as string;
                        orderInfo.DestinationSKU = clsGeneral.getColumnData(dataRow, "DestinationSKU", string.Empty, false) as string;
                        orderInfo.OrderTransferStatus = clsGeneral.getColumnData(dataRow, "OrderTransferStatus", string.Empty, false) as string;
                        //orderInfo.TransferOrderDate = clsGeneral.getColumnData(dataRow, "CreateDate", string.Empty, false) as string;
                        orderInfo.OrderTransferDateTime = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "OrderTransferDate", DateTime.Now, false));
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

        private List<TransferOrder> PopulateTransferOrders(DataTable dataTable)
        {
            List<TransferOrder> orderList = default;// new List<Carriers>();

            try
            {
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    orderList = new List<TransferOrder>();
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        TransferOrder orderInfo = new TransferOrder();
                        orderInfo.OrderTransferID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderTransferID", 0, false));
                        orderInfo.TransferQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "TransferedQty", 0, false));
                        orderInfo.ToBeTransferQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ToBeTransferQty", 0, false));
                        orderInfo.RequestedQty = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RequestedQty", 0, false));
                        orderInfo.SourceStock_in_Hand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "SourceStock_in_Hand", 0, false));
                        orderInfo.DestinationStock_in_Hand = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "DestinationStock_in_Hand", 0, false));
                        orderInfo.OrderTransferNumber = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "OrderTransferNumber", 0, false));
                        orderInfo.IsESNRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESNRequired", false, false));
                        orderInfo.SourceCustomer = clsGeneral.getColumnData(dataRow, "SourceCustomer", string.Empty, false) as string;
                        orderInfo.SourceSKU = clsGeneral.getColumnData(dataRow, "SourceSKU", string.Empty, false) as string;
                        orderInfo.SourceItemName = clsGeneral.getColumnData(dataRow, "SourceItemName", string.Empty, false) as string;
                        orderInfo.DestinationCompanyName = clsGeneral.getColumnData(dataRow, "DestinationCompanyName", string.Empty, false) as string;
                        orderInfo.DestinationSKU = clsGeneral.getColumnData(dataRow, "DestinationSKU", string.Empty, false) as string;
                        orderInfo.DestinationItemName = clsGeneral.getColumnData(dataRow, "DestinationItemName", string.Empty, false) as string;
                        orderInfo.DestinationSKU = clsGeneral.getColumnData(dataRow, "DestinationSKU", string.Empty, false) as string;
                        //orderInfo.TransferOrderDate = clsGeneral.getColumnData(dataRow, "CreateDate", string.Empty, false) as string;
                        orderInfo.OrderTransferDateTime = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "OrderTransferDate", DateTime.Now, false));
                        orderInfo.OrderTransferStatus = clsGeneral.getColumnData(dataRow, "OrderTransferStatus", string.Empty, false) as string;
                        orderInfo.ApprovedUser = clsGeneral.getColumnData(dataRow, "ApprovedUser", string.Empty, false) as string;
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
