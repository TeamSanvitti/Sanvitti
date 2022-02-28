using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{

    public class Fulfillment
    {
        private string fulfillmentNumber;
        private string contactName;
        private string address1;
        private string address2;
        private string city;
        private string state;
        private string zip;
        private string contactPhone;
        private string shipVia;
        private string comments;

        private List<FulfillmentItem> fulfillmentItems;

        public Fulfillment()
        {
            fulfillmentItems = new List<FulfillmentItem>();
        }
        //public Fulfillment(string fulfillmentNum)
        //{
        //    fulfillmentNumber = fulfillmentNum;
        //    fulfillmentItems = new List<FulfillmentItem>();
        //}
        public string FulfillmentNumber
        {
            get
            {
                return fulfillmentNumber;
            }
            set
            {
                fulfillmentNumber = value;
            }
        }

        [XmlElement(ElementName = "contactname")]
        public string ContactName
        {
            get
            {
                return contactName;
            }
            set
            {
                contactName = value;
            }
        }
        [XmlElement(ElementName = "address1")]
        public string Address1
        {
            get
            {
                return address1;
            }
            set
            {
                address1 = value;
            }
        }
        [XmlElement(ElementName = "address2")]
        public string Address2
        {
            get
            {
                return address2;
            }
            set
            {
                address2 = value;
            }
        }
        [XmlElement(ElementName = "city")]
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }
        [XmlElement(ElementName = "state")]
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        [XmlElement(ElementName = "zip")]
        public string Zip
        {
            get
            {
                return zip;
            }
            set
            {
                zip = value;
            }
        }
        [XmlElement(ElementName = "contactphone")]
        public string ContactPhone
        {
            get
            {
                return contactPhone;
            }
            set
            {
                contactPhone = value;
            }
        }
        [XmlElement(ElementName = "shipvia")]
        public string ShipVia
        {
            get
            {
                return shipVia;
            }
            set
            {
                shipVia = value;
            }
        }
        [XmlElement(ElementName = "comments")]
        public string Comments
        {
            get
            {
                return comments;
            }
            set
            {
                comments = value;
            }
        }

        public List<FulfillmentItem> FulfillmentItems
        {
            get
            {
                return fulfillmentItems;

            }
            set
            {
                fulfillmentItems = value;
            }
        }


    }
}
