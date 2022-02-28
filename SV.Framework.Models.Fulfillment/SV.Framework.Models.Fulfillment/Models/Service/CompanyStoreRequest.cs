using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Fulfillment;

namespace SV.Framework.Models.Service
{
    public class CompanyStoreRequest
    {

        private clsAuthentication _auth;

        public CompanyStoreRequest()
        {
            _auth = new clsAuthentication();
        }



        //[XmlElement(ElementName = "Authentication", IsNullable = true)]
        public clsAuthentication Authentication
        {
            get
            {
                return _auth;
            }
            set
            {
                _auth = value;
            }
        }
    }

}
