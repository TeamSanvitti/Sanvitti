using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.SOR
{
    public class ServiceOrderCSV
    {
        public string ServiceOrderNumber { get; set; }
        public string IMEI { get; set; }
        public string ICCID { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string KittedSKU { get; set; }
        public string Customer { get; set; }
        public string Date { get; set; }
        public string Qty { get; set; }
        public string QtyPerOrder { get; set; }

    }

}
