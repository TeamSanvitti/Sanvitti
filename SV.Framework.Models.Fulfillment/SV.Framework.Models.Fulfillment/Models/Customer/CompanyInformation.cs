using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Customer
{

    [Serializable]
    [XmlRoot(ElementName = "CompanyInfo", IsNullable = true)]
    public class CompanyInformation
    {
        private string companyName;
        private string companyAccountNumber;
        private string companyShortName;
        private List<Users> users;

        public CompanyInformation()
        {
            users = new List<Users>();
        }

        [XmlElement(ElementName = "CompanyAccountNumber", IsNullable = true)]
        public string CompanyAccountNumber
        {
            get { return companyAccountNumber; }
            set { companyAccountNumber = value; }
        }
        [XmlElement(ElementName = "CompanyName", IsNullable = true)]
        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        [XmlElement(ElementName = "CompanyShortName", IsNullable = true)]
        public string CompanyShortName
        {
            get { return companyShortName; }
            set { companyShortName = value; }
        }
        [XmlElement(ElementName = "Users", IsNullable = true)]
        public List<Users> Users
        {
            get { return users; }
            set { users = value; }
        }

    }

}
