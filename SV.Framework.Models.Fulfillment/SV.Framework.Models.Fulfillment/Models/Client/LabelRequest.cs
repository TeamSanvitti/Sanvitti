using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class LabelRequest
    {

        private clsAuthentication _auth;

        public LabelRequest()
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
