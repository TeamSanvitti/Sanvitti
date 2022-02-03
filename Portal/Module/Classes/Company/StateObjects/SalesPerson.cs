using System.Xml;
using System.Xml.Serialization;
//using Sv.Framework.DataMembers;

namespace avii.Classes
{
    //[Serializable]
    [XmlRoot(ElementName = "SalesPerson", IsNullable = true)]
    public class SalesPerson
    {
        private int? userID;
        private string employeeNumber;

        private ContactInfo contactInfo;
        [XmlElement(ElementName = "UserID", IsNullable = true)]
        public int? UserID 
        { 
            get { return userID; } 
            set { userID = value; } 
        }
        [XmlIgnore]
        public string EmployeeNumber
        {
            get
            {
                return employeeNumber;
            }
            set
            {
                employeeNumber = value;
            }
        }
        [XmlIgnore]
        public ContactInfo ContactsInfo
        {
            get
            {
                return contactInfo;
            }
            set
            {
                contactInfo = value;
            }
        }
    }
}
