using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class TransferOrderOperation : BaseCreateInstance
    {
        public string CreateInternalTransferOrder(TransferOrder request)
        {
            string errorMessage = "";
            SV.Framework.DAL.Inventory.TransferOrderOperation transferOrderOperation = SV.Framework.DAL.Inventory.TransferOrderOperation.CreateInstance<SV.Framework.DAL.Inventory.TransferOrderOperation>();

            errorMessage = transferOrderOperation.CreateInternalTransferOrder(request);
            return errorMessage;
        }
        public List<TransferOrder> GetTransferOrders(string OrderTransferNumber, string SKU, int DestinationCompanyID, string fromDate, string toDate)
        {
            SV.Framework.DAL.Inventory.TransferOrderOperation transferOrderOperation = SV.Framework.DAL.Inventory.TransferOrderOperation.CreateInstance<SV.Framework.DAL.Inventory.TransferOrderOperation>();

            List<TransferOrder> orderList = transferOrderOperation.GetTransferOrders(OrderTransferNumber, SKU, DestinationCompanyID, fromDate, toDate);
            return orderList;
        }
        public TransferOrder GetTransferOrderDetail(Int64 orderTransferID)
        {
            SV.Framework.DAL.Inventory.TransferOrderOperation transferOrderOperation = SV.Framework.DAL.Inventory.TransferOrderOperation.CreateInstance<SV.Framework.DAL.Inventory.TransferOrderOperation>();

            TransferOrder orderDetail = transferOrderOperation.GetTransferOrderDetail(orderTransferID);
            return orderDetail;
        }
        public string OrderTransferStatusUpdate(Int64 orderTransferID, string OrderTransferStatus, int userID)
        {
            SV.Framework.DAL.Inventory.TransferOrderOperation transferOrderOperation = SV.Framework.DAL.Inventory.TransferOrderOperation.CreateInstance<SV.Framework.DAL.Inventory.TransferOrderOperation>();

            return transferOrderOperation.OrderTransferStatusUpdate(orderTransferID, OrderTransferStatus, userID);

        }
        public List<TransferOrderAssignment> GetTransferOrderAssignments(Int64 orderTransferId)
        {
            SV.Framework.DAL.Inventory.TransferOrderOperation transferOrderOperation = SV.Framework.DAL.Inventory.TransferOrderOperation.CreateInstance<SV.Framework.DAL.Inventory.TransferOrderOperation>();

            List<TransferOrderAssignment> orderList = transferOrderOperation.GetTransferOrderAssignments(orderTransferId);// new List<Carriers>();
            return orderList;
        }
    }
}