using System;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
{
    [Serializable]
    [XmlRoot(ElementName = "UserStoreMap", IsNullable = true)]
    public class UserStoreMap
    {
        private int? _userid;
        private int? _addressID;

        [XmlElement(ElementName = "userID", IsNullable = true)]
        public int? UserID
        {
            get { return _userid; }
            set { _userid = value; }
        }
        [XmlElement(ElementName = "AddressID", IsNullable = true)]
        public int? AddressID
        {
            get { return _addressID; }
            set { _addressID = value; }
        }
    }
}