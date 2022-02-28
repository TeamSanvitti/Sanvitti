using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{

    public class CustomerRmaDisposition
    {
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public int Credit { get; set; }
        public int Discarded { get; set; }
        public int Repair { get; set; }
        public int Replaced { get; set; }

    }
}
