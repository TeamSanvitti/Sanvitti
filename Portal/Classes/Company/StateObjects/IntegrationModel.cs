using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class IntegrationModel
    {
        public int IntegrationID { get; set; }
        public int IntegrationModuleID { get; set; }
        public string APIAddress { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool? Active { get; set; }
        public string APIName { get; set; }
    }
    public class IntegrationModule
    {
        public int IntegrationModuleID { get; set; }
        public string IntegrationName { get; set; }

    }
}