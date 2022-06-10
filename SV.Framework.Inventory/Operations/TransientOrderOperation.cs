using System;
using System.Collections.Generic;
using System.Text;

using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class TransientOrderOperation : BaseCreateInstance
    {
        public string CreateTransientOrderInsertUpdate(TransientReceiveOrder request, out Int64 transientOrderID)
        {
            string errorMessage = "";
            SV.Framework.DAL.Inventory.TransientOrderOperation OrderOperation = SV.Framework.DAL.Inventory.TransientOrderOperation.CreateInstance<SV.Framework.DAL.Inventory.TransientOrderOperation>();

            errorMessage = OrderOperation.CreateTransientOrderInsertUpdate(request, out transientOrderID);
            return errorMessage;
        }
        public List<TransientReceiveOrder> GetTransientOrders(int MemoNumber, string SKU, int CompanyID, string fromDate, string toDate, string SupplierName)
        {
            SV.Framework.DAL.Inventory.TransientOrderOperation transientOrderOperation = SV.Framework.DAL.Inventory.TransientOrderOperation.CreateInstance<SV.Framework.DAL.Inventory.TransientOrderOperation>();

            List<TransientReceiveOrder> orderList = transientOrderOperation.GetTransientOrders(MemoNumber, SKU, CompanyID, fromDate, toDate, SupplierName);
            return orderList;
        }
        public TransientReceiveOrder GetTransientOrderDetail(Int64 TransientOrderID)
        {
            SV.Framework.DAL.Inventory.TransientOrderOperation transientOrderOperation = SV.Framework.DAL.Inventory.TransientOrderOperation.CreateInstance<SV.Framework.DAL.Inventory.TransientOrderOperation>();
            TransientReceiveOrder orderDetail = transientOrderOperation.GetTransientOrderDetail(TransientOrderID);
            return orderDetail;
        }

        public string OrderTransientStatusUpdate(Int64 TransientOrderID, string OrderTransientStatus, int userID)
        {
            SV.Framework.DAL.Inventory.TransientOrderOperation transientOrderOperation = SV.Framework.DAL.Inventory.TransientOrderOperation.CreateInstance<SV.Framework.DAL.Inventory.TransientOrderOperation>();

            return transientOrderOperation.OrderTransientStatusUpdate(TransientOrderID, OrderTransientStatus, userID);
        }
        public List<TransientOrderAssignment> GetTransientOrderAssignments(Int64 TransientOrderID)
        {
            SV.Framework.DAL.Inventory.TransientOrderOperation transientOrderOperation = SV.Framework.DAL.Inventory.TransientOrderOperation.CreateInstance<SV.Framework.DAL.Inventory.TransientOrderOperation>();
            return transientOrderOperation.GetTransientOrderAssignments(TransientOrderID);
        }

    }
}
