using System;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
{
    [Serializable]
    [XmlRoot(ElementName = "StoreLocation", IsNullable = true)]
    public class StoreLocation
    {
        private int companyID;
        private int? companyAddressID = 0;
        private string storeID;
        private string storeName;
        private Address storeAddress = new Address();
        private ContactInfo storeContact = new ContactInfo();
        private bool? active;
        private string storeFlag;
        private string compositeKeyStoreIdStoreName;
        private int? userStoreFlag;

        public string ShipContactName { get; set; }
        [XmlElement(ElementName = "UserStoreFlag", IsNullable = true)]
        public int? UserStoreFlag
        {
            get
            {
                return userStoreFlag;
            }
            set { userStoreFlag = value; }

        }


        [XmlIgnore]
        public string StoreFlag
        {
            get
            {
                return storeFlag;
            }
            set { storeFlag = value; }

        }
        //[XmlIgnore]
        [XmlElement(ElementName = "AddressID", IsNullable = true)]
        public int? CompanyAddressID
        {
            get
            {
                return companyAddressID;
            }
            set { companyAddressID = value; }

        }
        [XmlIgnore]
        public int CompanyID
        {
            get
            {
                return companyID;
            }
            set { companyID = value; }

        }
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

        [XmlElement(ElementName = "CompositeKeyStoreIdStoreName", IsNullable = true)]
        public string CompositeKeyStoreIdStoreName
        {
            get
            {
                return compositeKeyStoreIdStoreName;
            }
            set { compositeKeyStoreIdStoreName = value; }

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
        [XmlElement(ElementName = "Active", IsNullable = true)]
        public bool? Active
        {
            get
            {
                return active;
            }
            set { active = value; }

        }
    }
}
