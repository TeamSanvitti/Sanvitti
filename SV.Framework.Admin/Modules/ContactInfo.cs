using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Admin
{

    //[XmlRoot(ElementName = "ContactInfo", IsNullable = true)]
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
}
