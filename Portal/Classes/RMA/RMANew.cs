using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
{
    [Serializable]
    [XmlRoot(ElementName = "RMA", IsNullable = true)]
    public class RMANew
    {
        private string _RmaNumber;
        private string _CustomerRmaNumber;
        private DateTime _RmaDate;
        private string _RmaContactName = string.Empty;
        private string addrress = string.Empty;
        private string city = string.Empty;
        private string state = string.Empty;
        private string zip = string.Empty;
        private string email = string.Empty;
        private string phone = string.Empty;
        private string _Comment = string.Empty;
        private string avComments = string.Empty;
        //private string locationCode;
        private string storeID;
        
        private List<RMADetails> _rmaDetails;

        
        public RMANew()
        {
            _rmaDetails = new List<RMADetails>();
        }

        public string StoreID
        {
            get
            {
                return storeID;
            }
            set
            {
                storeID = value;
            }
        }

        [XmlElement(ElementName = "RMADetail")]
        //[XmlArray("RMADetail"), XmlArrayItem("RmaESN", IsNullable = true)]
        public List<RMADetails> RmaDetails
        {
            get
            {
                return _rmaDetails;
            }
            set
            {
                _rmaDetails = value;
            }
        }

        [XmlElement(ElementName = "RmaNumber", IsNullable = true)]
        public string RmaNumber
        {
            get
            {
                return _RmaNumber;
            }
            set
            {
                _RmaNumber = value;
            }
        }

        [XmlElement(ElementName = "CustomerRmaNumber", IsNullable = true)]
        public string CustomerRmaNumber
        {
            get
            {
                return _CustomerRmaNumber;
            }
            set
            {
                _CustomerRmaNumber = value;
            }
        }
        //[XmlElement(ElementName = "RmaDate", IsNullable = true)]
        [XmlElement(ElementName = "RmaDate", DataType = "date")]
        public DateTime RmaDate
        {
            get
            {
                return _RmaDate;
            }
            set
            {
                _RmaDate = value;
            }
        }
        [XmlElement(ElementName = "RmaContactName", IsNullable = true)]
        public string RmaContactName
        {
            get
            {
                return _RmaContactName;
            }
            set
            {
                _RmaContactName = value;
            }
        }
        [XmlElement(ElementName = "Email", IsNullable = true)]
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        [XmlElement(ElementName = "Phone", IsNullable = true)]
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }
        [XmlElement(ElementName = "Address", IsNullable = true)]
        public string Address
        {
            get { return addrress; }
            set { addrress = value; }
        }
        [XmlElement(ElementName = "City", IsNullable = true)]
        public string City
        {
            get { return city; }
            set { city = value; }
        }
        [XmlElement(ElementName = "State", IsNullable = true)]
        public string State
        {
            get { return state; }
            set { state = value; }
        }
        [XmlElement(ElementName = "Zip", IsNullable = true)]
        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }
        [XmlElement(ElementName = "Comment", IsNullable = true)]
        public string Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                _Comment = value;
            }
        }
        //[XmlElement(ElementName = "AVComments", IsNullable = true)]
        [XmlIgnore]
        public string AVComments
        {
            get
            {
                return avComments;
            }
            set
            {
                avComments = value;
            }
        }
       

    }
}