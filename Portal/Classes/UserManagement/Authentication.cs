using System;
using System.Xml.Serialization;

namespace avii.Classes
{
    [XmlRoot(ElementName = "Authentication", IsNullable = true)]
    public class UserCredentials
    {
        private string userName;
        private string password;

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

    }

    [XmlRoot(ElementName = "CredentialValidation", IsNullable = true)]
    public class CredentialValidation
    {
        private int userID;
        private int companyID;
        private string userName;
        private string companyAccountNumber;
        private string companyName;

        public CredentialValidation()
        {

        }

        public int UserID
        {
            get
            {
                return userID;
            }
            set
            {
                userID = value;
            }
        }
        public int CompanyID
        {
            get
            {
                return companyID;
            }
            set
            {
                companyID = value;
            }
        }

        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }

        public string CompanyAccountNumber
        {
            get
            {
                return companyAccountNumber;
            }
            set
            {
                companyAccountNumber = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
            }
        }

        public DateTime LoginTime
        {
            get
            {
                if (!string.IsNullOrEmpty(userName))
                    return DateTime.Today;
                else
                    return DateTime.MinValue;
            }
        }

    }
}