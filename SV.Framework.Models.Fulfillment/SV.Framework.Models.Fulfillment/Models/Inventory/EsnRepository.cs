using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class EsnRepository
    {
        public string UnusedESN { get; set; }
        public string InPorocessESN { get; set; }
        public string ShippedESN { get; set; }
        public string RmaESN { get; set; }
    }
}
