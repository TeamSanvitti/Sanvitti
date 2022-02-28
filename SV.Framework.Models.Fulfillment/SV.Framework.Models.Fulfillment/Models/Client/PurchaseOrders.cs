using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    [XmlRoot(ElementName = "purchaseorders", IsNullable = true)]
    public class PurchaseOrders
    {
        public PurchaseOrders()
        {
            _purchaseOrders = new List<BasePurchaseOrder>();
        }

        private List<BasePurchaseOrder> _purchaseOrders;

        public List<BasePurchaseOrder> PurchaseOrderList
        {
            get
            {
                return _purchaseOrders;
            }
            set
            {
                _purchaseOrders = value;
            }
        }

        public BasePurchaseOrder FindPurchaseOrder(int PurchaseOrderID)
        {
            BasePurchaseOrder purchaseOrder = null;
            if (_purchaseOrders != null)
            {
                foreach (BasePurchaseOrder po in _purchaseOrders)
                {
                    if (po.PurchaseOrderID == PurchaseOrderID)
                    {
                        purchaseOrder = po;
                    }
                }
            }

            return purchaseOrder;
        }

        public BasePurchaseOrder FindPurchaseOrderbyPodID(int PurchaseOrderItemID)
        {
            BasePurchaseOrder purchaseOrder = null;
            if (_purchaseOrders != null)
            {
                foreach (BasePurchaseOrder po in _purchaseOrders)
                {
                    foreach (BasePurchaseOrderItem poItem in po.PurchaseOrderItems)
                    {
                        if (poItem.PodID == PurchaseOrderItemID)
                        {
                            purchaseOrder = po;
                        }
                    }
                }
            }
            return purchaseOrder;
        }

        public BasePurchaseOrder FindPurchaseOrderbyNumber(string PurchaseOrderNumber)
        {
            BasePurchaseOrder purchaseOrder = null;
            if (_purchaseOrders != null)
            {
                foreach (BasePurchaseOrder po in _purchaseOrders)
                {
                    if (po.PurchaseOrderNumber.Equals(PurchaseOrderNumber))
                    {
                        purchaseOrder = po;
                    }
                }
            }
            return purchaseOrder;
        }

    }
}
