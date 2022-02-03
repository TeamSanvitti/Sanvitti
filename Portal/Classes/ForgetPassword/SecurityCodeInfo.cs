using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class SecurityCodeInfo
    {
        public int IsValid { get; set; }
        public int IsExpired { get; set; }
        public int Used { get; set; }
        public int UserID { get; set; }

    }
}