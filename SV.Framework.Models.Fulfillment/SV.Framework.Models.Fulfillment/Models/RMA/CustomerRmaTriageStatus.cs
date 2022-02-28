using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class CustomerRmaTriageStatus
    {
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public int Pending { get; set; }
        public int Complete { get; set; }
        public int InProcess { get; set; }
        public int NotRequired { get; set; }

    }

}
