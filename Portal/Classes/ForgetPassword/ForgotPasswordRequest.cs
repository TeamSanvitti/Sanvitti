using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class ForgotPasswordRequest
    {
        public int UserID { get; set; }
        public int SecurityCode { get; set; }
        public string EmailBody { get; set; }
    }
}