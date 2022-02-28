using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class RmaResponse
    {
        public int ReturnCode { get; set; }
        public string RmaNumber { get; set; }
        public string RMANumberExists { get; set; }
        public string CustomerRMANumberExists { get; set; }
    }

}
