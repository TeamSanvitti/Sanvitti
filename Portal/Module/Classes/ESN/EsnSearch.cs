using System;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
{
    [Serializable]
    [XmlRoot(ElementName = "EsnSearch", IsNullable = true)]
    public class EsnSearch
    {
        private string _esn;
        private string _meid;
        private string _akey;

        [XmlElement(ElementName = "ESN", IsNullable = true)]
        public string ESN
        {
            get
            {
                return _esn;
            }
            set
            {
                _esn = value;
            }
        }
        [XmlElement(ElementName = "AKEY", IsNullable = true)]
        public string AKEY
        {
            get
            {
                return _akey;
            }
            set
            {
                _akey = value;
            }
        }
        [XmlElement(ElementName = "MEID", IsNullable = true)]
        public string MEID
        {
            get
            {
                return _meid;
            }
            set
            {
                _meid = value;
            }
        }
        //public string ESN { get; set; }
        //public string MEID { get; set; }
        //public string AKEY { get; set; }
    }
}