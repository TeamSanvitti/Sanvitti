using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;


namespace avii.Classes
{
    [Serializable]
    [XmlRoot(ElementName = "RMAChangeStatus", IsNullable = true)]
    public class RMAChangeStatus
    {
        private string rmaNumber;

        [XmlElement(ElementName = "RmaNumber", IsNullable = true)]
        public string RmaNumber
        {
            get
            {
                return rmaNumber;
            }
            set
            {
                rmaNumber = value;
            }
        }
        
    }
}