using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace avii.Classes
{
    [XmlType("fulfillmentorder")]
   [XmlRoot(ElementName = "fulfillmentorder", IsNullable = true)]
    public class FulfillmentOrder  : BasePurchaseOrder
    {
        public FulfillmentOrder()
            : base()
        { }

        public FulfillmentOrder(int PurchaseOrderID)
            : base(PurchaseOrderID)
        { }
    }
}
