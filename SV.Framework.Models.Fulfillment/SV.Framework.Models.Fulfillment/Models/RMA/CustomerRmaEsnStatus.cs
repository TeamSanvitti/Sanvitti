using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class CustomerRmaEsnStatus
    {
        public int Total { get; set; }
        public string CustomerName { get; set; }
        public int Pending { get; set; }
        public int Completed { get; set; }
        public int Cancelled { get; set; }
        public int ExternalESN { get; set; }
        public int ReturnToStock { get; set; }
        public int SentToSupplier { get; set; }

    }

}
