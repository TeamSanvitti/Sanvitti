using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.RMA
{

    [Serializable]
    public class RMAUserCompany
    {
        private int _companyID;
        private string _companyName;
        private string _email;
        private int _userid;
        [XmlIgnore]
        public bool POCustNoValidate { get; set; }
        public int UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
            }

        }
        public int CompanyID
        {
            get
            {
                return _companyID;
            }
            set
            {
                _companyID = value;
            }
        }
        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
            }
        }
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

    }
}
