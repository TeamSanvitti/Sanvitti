using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Customer
{

    public class Users
    {
        private string userName;
        private string email;
        private string status;
        private string rolesAssigned;
        [XmlIgnore]
        public int UserID { get; set; }

        [XmlElement(ElementName = "UserName", IsNullable = true)]
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        [XmlElement(ElementName = "Email", IsNullable = true)]
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        [XmlElement(ElementName = "Status", IsNullable = true)]
        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        [XmlElement(ElementName = "RolesAssigned", IsNullable = true)]
        public string RolesAssigned
        {
            get { return rolesAssigned; }
            set { rolesAssigned = value; }
        }


    }
}
