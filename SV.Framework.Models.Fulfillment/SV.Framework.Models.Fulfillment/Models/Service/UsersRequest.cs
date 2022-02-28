using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Fulfillment;

namespace SV.Framework.Models.Service
{
    public class UsersRequest
    {
        private clsAuthentication _auth;
        public UsersRequest()
        {
            _auth = new clsAuthentication();
        }

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
