using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class LoginUser
    {
        public int SignInID { get; set; }
        public string SessionStartDate { get; set; }
        public string SessionEndDate { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }

    }
}