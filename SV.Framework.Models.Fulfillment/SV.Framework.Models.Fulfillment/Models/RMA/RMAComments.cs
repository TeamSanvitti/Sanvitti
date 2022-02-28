using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.RMA
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
