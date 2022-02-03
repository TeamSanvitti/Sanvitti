using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    public class FulfillmentMultiTracking
    {
        public DateTime? ShipDate { get; set; }
        public string ShipViaCode { get; set; }
        public string CustomerAccountNumber { get; set; }
        public string Comments { get; set; }
        
        public List<MultiTracking> Trackings { get; set; }
    }

    public class MultiTracking
    {
        private string fulfillmentNumber;
        private string trackingNumber;
        // private string esn;
        //private string shipViaCode;
        //private string avsoNumber;
        //private string shipmentType;
        //private DateTime? shipDate = DateTime.Now;


        //[XmlElement(ElementName = "ShipDate", IsNullable = true)]
        //public DateTime? ShipDate
        //{
        //    get
        //    {

        //        return shipDate;
        //    }
        //    set
        //    {
        //        shipDate = value;
        //    }
        //}
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

        //[XmlElement(ElementName = "esn", IsNullable = true)]
        //public string ESN
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(esn))
        //            return string.Empty;
        //        else
        //            return esn;
        //    }
        //    set
        //    {
        //        esn = value;
        //    }
        //}
        //[XmlElement(ElementName = "ShipViaCode", IsNullable = true)]
        //public string ShipViaCode
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(shipViaCode))
        //            return string.Empty;
        //        else
        //            return shipViaCode;
        //    }
        //    set
        //    {
        //        shipViaCode = value;
        //    }
        //}

        //[XmlElement(ElementName = "avsonumber", IsNullable = true)]
        //public string AVSONumber
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(avsoNumber))
        //            return string.Empty;
        //        else
        //            return avsoNumber;
        //    }
        //    set
        //    {
        //        avsoNumber = value;
        //    }
        //}

        //[XmlIgnore]
        //[XmlElement(ElementName = "shipmenttype", IsNullable = true)]
        //public string ShipmentType
        //{
        //    get
        //    {
        //        if (string.IsNullOrEmpty(shipmentType))
        //            return string.Empty;
        //        else
        //            return shipmentType;
        //    }
        //    set
        //    {
        //        shipmentType = value;
        //    }
        //}


    }

    
}