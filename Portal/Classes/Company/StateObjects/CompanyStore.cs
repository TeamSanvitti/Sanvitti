using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;


namespace avii.Classes
{
    [Serializable]
    [XmlRoot(ElementName = "CompanyStore", IsNullable = true)]
    public class CompanyStore
    {
        private string storeID;
        private string storeName;
        private Address storeAddress = new Address();
        private ContactInfo storeContact = new ContactInfo();
        [XmlElement(ElementName = "StoreID", IsNullable = true)]
        public string StoreID
        {
            get
            {
                return storeID;
            }
            set { storeID = value; }

        }
        [XmlElement(ElementName = "StoreName", IsNullable = true)]
        public string StoreName
        {
            get
            {
                return storeName;
            }
            set { storeName = value; }

        }

        [XmlElement(ElementName = "Address", IsNullable = true)]
        public Address StoreAddress
        {
            get
            {
                return storeAddress;
            }
            set { storeAddress = value; }

        }
        [XmlElement(ElementName = "ContactInfo", IsNullable = true)]
        public ContactInfo StoreContact
        {
            get
            {
                return storeContact;
            }
            set { storeContact = value; }

        }
    }
}