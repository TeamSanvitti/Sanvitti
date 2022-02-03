using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
{
    [Serializable]
    [XmlRoot(ElementName = "FulfillmentChangeStatus", IsNullable = true)]
    public class FulfillmentChangeStatus
    {
        private string fulfillmentOrder;

        [XmlElement(ElementName = "FulfillmentOrder", IsNullable = true)]
        public string FulfillmentOrder
        {
            get
            {
                return fulfillmentOrder;
            }
            set
            {
                fulfillmentOrder = value;
            }
        }
        
    }
}