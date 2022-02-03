using System;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
{
    [Serializable]
    [XmlRoot(ElementName = "RMAComments", IsNullable = true)]
    public class RMAComments
    {
        //[XmlElement(ElementName = "rmaDetGUID", IsNullable = true)]
        [XmlElement(ElementName = "CommentID", IsNullable = true)]
        public int? CommentID { get; set; }
        [XmlIgnore]
        public string UserName { get; set; }
        [XmlIgnore]
        public DateTime CreateDate { get; set; }
        [XmlIgnore]
        public string Comments { get; set; }
        [XmlIgnore]
        public string UserType { get; set; }
        [XmlIgnore]
        public Boolean Exclude { get; set; }



    }
}