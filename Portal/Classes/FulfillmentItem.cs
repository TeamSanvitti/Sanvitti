using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    [XmlRoot(ElementName = "fulfillmentitem", IsNullable = true)]
    public class FulfillmentItem
    {
        private string sku;
        private string mdnNumber;
        private int? lineNumber = 0;
        private int? qty = 1;
        public FulfillmentItem()
        { }
        [XmlElement(ElementName = "sku")]
        public string SKU
        {
            get
            {
                return sku;
            }
            set
            {
                sku = value;
            }
        }
        
        //[XmlElement(ElementName = "mdnnumber")]
        public string MdnNumber
        {
            get
            {
                return mdnNumber;
            }
            set
            {
                mdnNumber = value;
            }
        }
        [XmlElement(ElementName = "linenumber")]
        public int? LineNumber
        {
            get
            {
                if (lineNumber == null)
                    return 0;
                else
                    return lineNumber;
            }
            set
            {
                lineNumber = value;
            }
        }
        [XmlElement(ElementName = "Quantity")]
        public int? Quantity
        {
            get
            {
                if (qty == null || qty == 0)
                    return 0;
                else
                    return qty;
            }
            set
            {
                qty = value;
            }
        }

    }
}