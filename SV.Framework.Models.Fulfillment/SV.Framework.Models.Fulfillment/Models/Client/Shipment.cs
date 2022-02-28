using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{

    public class Shipment
    {
        private List<Shipping> shippingList;
        private List<ReturnShipment> returnshipmentList;

        public Shipment()
        {
            shippingList = new List<Shipping>();
            returnshipmentList = new List<ReturnShipment>();
        }



        //[XmlArray("details"), XmlArrayItem("lineitem", IsNullable = true)]
        [XmlElement(ElementName = "shippingitem", IsNullable = true)]
        public List<Shipping> ShippingList
        {
            get
            {
                return shippingList;
            }
            set
            {
                shippingList = value;
            }

        }
        [XmlElement(ElementName = "returnshipmentitem", IsNullable = true)]
        public List<ReturnShipment> ReturnshipmentList
        {
            get
            {
                return returnshipmentList;
            }
            set
            {
                returnshipmentList = value;
            }

        }
    }
}
