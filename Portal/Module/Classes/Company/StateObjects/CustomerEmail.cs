using System;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes  
{
    [Serializable]
    [XmlRoot(ElementName = "CustomerEmail", IsNullable = true)]
    public class CustomerEmail
    {
        private int _companyID;
        private int _userID;
        private int? _moduleGUID;
        private string _companyEmail;
        private string _email;
        private string _overrideEmail;
        private string _moduleNmae;


        [XmlIgnore]
        [XmlElement(ElementName = "CompanyID", IsNullable = true)]
        public int CompanyID
        {
            get
            {
                return _companyID;
            }
            set { _companyID = value; }

        }

        [XmlIgnore]
        [XmlElement(ElementName = "UserID", IsNullable = true)]
        public int UserID
        {
            get
            {
                return _userID;
            }
            set { _userID = value; }

        }
        [XmlElement(ElementName = "ModuleGUID", IsNullable = true)]
        public int? ModuleGUID 
        {
            get
            {
                return _moduleGUID;
            }
            set { _moduleGUID = value; }

        }
        [XmlIgnore]
        [XmlElement(ElementName = "CompanyEmail", IsNullable = true)]
        public string CompanyEmail
        {
            get
            {
                return _companyEmail;
            }
            set 
            {
                _companyEmail = value; 
            }

        }
        

        [XmlElement(ElementName = "Email", IsNullable = true)]
        public string Email
        {
            get
            {
                return _email;
            }
            set 
            { 
                _email = value; 
            }

        }
        [XmlElement(ElementName = "OverrideEmail", IsNullable = true)]
        public string OverrideEmail
        {
            get
            {
                return _overrideEmail;
            }
            set
            {
                _overrideEmail = value;
            }

        }
        [XmlIgnore]
        public string ModuleName
        {
            get
            {
                return _moduleNmae;
            }
            set
            {
                _moduleNmae = value;
            }

        }
       
    }
}