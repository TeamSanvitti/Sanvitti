using System;
//using Sv.Framework.DataMembers.Enums;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
{

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
}