using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    [Serializable]
    [XmlRoot(ElementName = "Trackings", IsNullable = true)]
    public class Trackings
    {
        private string fulfillmentNumber;
        private string trackingNumber;
        private string avOrderNumber;
        //private int? lineNumber;
        private string shipmentType;
        private string shippingVia;
        private string comments;

        private DateTime? shipDate = DateTime.Now;


        [XmlElement(ElementName = "ShipDate", IsNullable = true)]
        public DateTime? ShipDate
        {
            get
            {
                //shipDate = DateTime.SpecifyKind(shipDate, DateTimeKind.Unspecified);

                return shipDate;
                //return DateTime.SpecifyKind(shipDate, DateTimeKind.Unspecified); ;
            }
            set
            {
                shipDate = value;
            }
        }

        [XmlElement(ElementName = "FulfillmentNumber", IsNullable = true)]
        public string FulfillmentNumber
        {
            get
            {
                if (string.IsNullOrEmpty(fulfillmentNumber))
                    return string.Empty;
                else
                    return fulfillmentNumber;
            }
            set
            {
                fulfillmentNumber = value;
            }
        }
        [XmlElement(ElementName = "Tracking", IsNullable = true)]
        public string Tracking
        {
            get
            {
                if (string.IsNullOrEmpty(trackingNumber))
                    return string.Empty;
                else
                    return trackingNumber;
            }
            set
            {
                trackingNumber = value;
            }
        }

        [XmlElement(ElementName = "AvOrderNumber", IsNullable = true)]
        public string AvOrderNumber
        {
            get
            {
                if (string.IsNullOrEmpty(avOrderNumber))
                    return string.Empty;
                else
                    return avOrderNumber;
            }
            set
            {
                avOrderNumber = value;
            }
        }
        [XmlElement(ElementName = "ShippingVia", IsNullable = true)]
        public string ShippingVia
        {
            get
            {
                if (string.IsNullOrEmpty(shippingVia))
                    return string.Empty;
                else
                    return shippingVia;
            }
            set
            {
                shippingVia = value;
            }
        }
        //[XmlElement(ElementName = "LineNo", IsNullable = true)]
        //public int? LineNo
        //{
        //    get
        //    {
        //        return lineNumber;
        //    }
        //    set
        //    {
        //        lineNumber = value;
        //    }
        //}
        [XmlElement(ElementName = "ShipmentType", IsNullable = true)]
        public string ShipmentType
        {
            get
            {
                if (string.IsNullOrEmpty(shipmentType))
                    return string.Empty;
                else
                    return shipmentType;
            }
            set
            {
                shipmentType = value;
            }
        }
        [XmlElement(ElementName = "Comments", IsNullable = true)]
        public string Comments
        {
            get
            {
                if (string.IsNullOrEmpty(comments))
                    return string.Empty;
                else
                    return comments;
            }
            set
            {
                comments = value;
            }
        }

    }
}