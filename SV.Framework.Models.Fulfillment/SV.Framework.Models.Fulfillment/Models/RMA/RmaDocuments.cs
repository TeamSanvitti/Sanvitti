using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.RMA
{

    [Serializable]
    [XmlRoot(ElementName = "RmaDocument", IsNullable = true)]
    public class RmaDocuments
    {
        private int? rmaDocID;
        private int rmaGUID;
        private string rmaDocument;
        private DateTime modifiedDate;
        private string docType;

        [XmlIgnore]
        public int RmaGUID
        {
            get
            {
                return rmaGUID;
            }
            set
            {
                rmaGUID = value;
            }
        }
        [XmlElement(ElementName = "RmaDocID", IsNullable = true)]
        public int? RmaDocID
        {
            get
            {
                return rmaDocID;
            }
            set
            {
                rmaDocID = value;
            }
        }

        [XmlElement(ElementName = "RmaDocument", IsNullable = true)]
        public string RmaDocument
        {
            get
            {
                return rmaDocument;
            }
            set
            {
                rmaDocument = value;
            }
        }

        [XmlElement(ElementName = "DocType", IsNullable = true)]
        public string DocType
        {
            get
            {
                return docType;
            }
            set
            {
                docType = value;
            }
        }
        //[XmlElement(ElementName = "ModifiedDate", IsNullable = true)]
        [XmlIgnore]
        public DateTime ModifiedDate
        {
            get
            {
                return modifiedDate;
            }
            set
            {
                modifiedDate = value;
            }
        }

    }
}
