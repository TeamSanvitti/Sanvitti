using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Customer
{
    public class CompanyInfo
    {
        public string CompanyAccountNumber { get; set; }
        public string CompanyShortName { get; set; }
        public string CompanyName { get; set; }

        public List<CompanyAddresses> CompanyAddresses { get; set; }
        public List<CompanyStore> Stores { get; set; }
        //public List<StoreLocation> officeAndShippAddress { get; set; }
        public string Email { get; set; }
        public string GroupEmail { get; set; }
        public string Website { get; set; }
        public string BussinessType { get; set; }
        //public int CompanySType { get; set; }
        public string Comment { get; set; }
        //public int CompanyAccountStatus { get; set; }

    }

    public class CompanyAddresses
    {
        public Address CompanyAddress { get; set; }
        public ContactInfo CompanyContactInfo { get; set; }
    }
    [Serializable]
    //[XmlRoot(ElementName = "Address", IsNullable = true)]
    public class Address
    {
        private AddressType addressType = AddressType.None;
        private string addess1 = string.Empty;
        private string address2 = string.Empty;
        private string address3 = string.Empty;
        private string city = string.Empty;
        private string state = string.Empty;
        private string zip = string.Empty;
        private string country = string.Empty;

        [XmlElement(ElementName = "AddressType")]
        public AddressType AddressType
        {
            get
            {
                return addressType;
            }
            set
            {
                addressType = value;
            }
        }
        [XmlElement(ElementName = "Address1", IsNullable = true)]
        public string Address1
        {
            get
            {
                if (string.IsNullOrEmpty(addess1))
                    return string.Empty;
                else
                    return addess1;
            }
            set
            {
                addess1 = value;
            }
        }
        [XmlElement(ElementName = "Address2", IsNullable = true)]
        public string Address2
        {
            get
            {
                if (string.IsNullOrEmpty(address2))
                    return string.Empty;
                else
                    return address2;
            }
            set
            {
                address2 = value;
            }
        }
        [XmlElement(ElementName = "Address3", IsNullable = true)]
        [XmlIgnore]
        public string Address3
        {
            get
            {
                if (string.IsNullOrEmpty(address3))
                    return string.Empty;
                else
                    return address3;
            }
            set
            {
                address3 = value;
            }
        }
        [XmlElement(ElementName = "City", IsNullable = true)]
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
        [XmlElement(ElementName = "State", IsNullable = true)]
        public string State
        {
            get
            {
                if (string.IsNullOrEmpty(state))
                    return string.Empty;
                else
                    return state;
            }
            set
            {
                state = value;
            }
        }
        [XmlElement(ElementName = "Zip", IsNullable = true)]
        public string Zip
        {
            get
            {
                if (string.IsNullOrEmpty(zip))
                    return string.Empty;
                else
                    return zip;
            }
            set
            {
                zip = value;
            }
        }
        [XmlElement(ElementName = "Country", IsNullable = true)]
        public string Country
        {
            get
            {
                if (string.IsNullOrEmpty(country))
                    return string.Empty;
                else
                    return country;
            }
            set
            {
                country = value;
            }
        }

    }
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
    [Serializable]
    public class ContactInfo
    {
        private string contactName;
        private string homePhone;
        //private string workPhone;
        private string officePhone1 = string.Empty;
        private string officePhone2 = string.Empty;
        private string cellPhone;
        private string email1;
        private string email2;
        private string comments;

        [XmlElement(ElementName = "Comment", IsNullable = true)]
        public string Comment
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

        [XmlElement(ElementName = "ContactName", IsNullable = true)]
        public string ContactName
        {
            get
            {
                if (string.IsNullOrEmpty(contactName))
                    return string.Empty;
                else
                    return contactName;
            }
            set
            {
                contactName = value;
            }
        }
        [XmlElement(ElementName = "OfficePhone1", IsNullable = true)]
        public string OfficePhone1
        {
            get
            {
                if (string.IsNullOrEmpty(officePhone1))
                    return string.Empty;
                else
                    return officePhone1;
            }
            set
            {
                officePhone1 = value;
            }
        }
        [XmlElement(ElementName = "OfficePhone2", IsNullable = true)]
        public string OfficePhone2
        {
            get
            {
                if (string.IsNullOrEmpty(officePhone2))
                    return string.Empty;
                else
                    return officePhone2;
            }
            set
            {
                officePhone2 = value;
            }
        }
        [XmlElement(ElementName = "HomePhone", IsNullable = true)]
        public string HomePhone
        {
            get
            {
                if (string.IsNullOrEmpty(homePhone))
                    return string.Empty;
                else
                    return homePhone;
            }
            set
            {
                homePhone = value;
            }
        }
        [XmlElement(ElementName = "CellPhone", IsNullable = true)]
        public string CellPhone
        {
            get
            {
                if (string.IsNullOrEmpty(cellPhone))
                    return string.Empty;
                else
                    return cellPhone;
            }
            set
            {
                cellPhone = value;
            }
        }

        [XmlElement(ElementName = "Email1", IsNullable = true)]
        public string Email1
        {
            get
            {
                if (string.IsNullOrEmpty(email1))
                    return string.Empty;
                else
                    return email1;
            }
            set
            {
                email1 = value;
            }
        }
        [XmlElement(ElementName = "Email2", IsNullable = true)]
        public string Email2
        {
            get
            {
                if (string.IsNullOrEmpty(email2))
                    return string.Empty;
                else
                    return email2;
            }
            set
            {
                email2 = value;
            }
        }

    }
    [Serializable]
    public enum AddressType
    {
        [XmlEnumAttribute(Name = "0")]
        None = 0,
        [XmlEnumAttribute(Name = "1")]
        Office1 = 1,
        [XmlEnumAttribute(Name = "2")]
        Office2 = 2,
        [XmlEnumAttribute(Name = "3")]
        Office3 = 3,
        [XmlEnumAttribute(Name = "4")]
        Shipping = 4,
        [XmlEnumAttribute(Name = "5")]
        Home = 5,
        [XmlEnumAttribute(Name = "6")]
        Unknown = 6,
        [XmlEnumAttribute(Name = "7")]
        General = 7,
        [XmlEnumAttribute(Name = "8")]
        Store = 8,
        [XmlEnumAttribute(Name = "9")]
        Maker = 9
    }


}
