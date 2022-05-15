using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.SOR
{
    public class DekitOrderDetail
    {
        public Int64 DeKittingID { get; set; }
        public Int64 DeKittingDetailID { get; set; }
        public int ItemCompanyGUID { get; set; }
        public int DestinationItemCompanyGUID { get; set; }
        public int Quantity { get; set; }
        public string WhLocation { get; set; }


    }

}
