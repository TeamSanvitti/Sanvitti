using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class RmaHistory
    {
        public string Status { get; set; }
        public string ModuleName { get; set; }
        public string Comments { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

    }
}