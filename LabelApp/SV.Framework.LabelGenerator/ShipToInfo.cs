using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SV.Framework.Common.LabelGenerator
{
    
    [XmlRoot(ElementName = "ShipInfo", IsNullable = true)]
    public class ShipInfo
    {
        private string _shipToAddress;
        private string _shipToAddress2;
        private string _shipToCity;
        private string _shipToState;
        private string _shipToZip;
        private string _contactName;
        private string _contactPhone;
        private string _shipToAttn;
        private string _shipToCountry;

        [XmlElement(ElementName = "contactname", IsNullable = true)]
        public string ContactName
        {
            get
            {
                if (string.IsNullOrEmpty(_contactName))
                    return string.Empty;
                else
                    return _contactName;
            }
            set
            {
                _contactName = value;
            }
        }

        [XmlElement(ElementName = "contactphone", IsNullable = true)]
        public string ContactPhone
        {
            get
            {
                if (string.IsNullOrEmpty(_contactPhone))
                    return string.Empty;
                else
                    return _contactPhone;
            }
            set
            {
                _contactPhone = value;
            }
        }

        [XmlElement(ElementName = "shiptoattn", IsNullable = true)]
        public string ShipToAttn
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToAttn))
                    return string.Empty;
                else
                    return _shipToAttn;
            }
            set
            {
                _shipToAttn = value;
            }
        }

        [XmlElement(ElementName = "shiptoaddress1", IsNullable = true)]
        public string ShipToAddress
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToAddress))
                    return string.Empty;
                else
                    return _shipToAddress;
            }
            set
            {
                _shipToAddress = value;
            }
        }

        [XmlElement(ElementName = "shiptoaddress2", IsNullable = true)]
        public string ShipToAddress2
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToAddress2))
                    return string.Empty;
                else
                    return _shipToAddress2;
            }
            set
            {
                _shipToAddress2 = value;
            }
        }

        [XmlElement(ElementName = "shiptocity", IsNullable = true)]
        public string ShipToCity
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToCity))
                    return string.Empty;
                else
                    return _shipToCity;
            }
            set
            {
                _shipToCity = value;
            }
        }

        [XmlElement(ElementName = "shiptostate", IsNullable = true)]
        public string ShipToState
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToState))
                    return string.Empty;
                else
                    return _shipToState;
            }
            set
            {
                _shipToState = value;
            }
        }

        [XmlElement(ElementName = "shiptozip", IsNullable = true)]
        public string ShipToZip
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToZip))
                    return string.Empty;
                else
                    return _shipToZip;
            }
            set
            {
                _shipToZip = value;
            }
        }
        [XmlElement(ElementName = "shiptocountry", IsNullable = true)]
        public string ShipToCountry
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToCountry))
                    return string.Empty;
                else
                    return _shipToCountry;
            }
            set
            {
                _shipToCountry = value;
            }
        }
    }

}
