using System.Xml.Serialization;
namespace avii.Classes
{
    [XmlRoot(ElementName = "Authentication", IsNullable = true)]
    public class clsAuthentication
    {
        private string _userName;
        private string _password;

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }


    }
}
