using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
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
}
