using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class RMAReceive
    {
        public string ReceiveDate { get; set; }
        public string ApprovedBy { get; set; }
        public string Comments { get; set; }
        public List<ReceiveDetail> ReceiveDetails { get; set; }

    }

}
