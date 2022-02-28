using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{

    public class ESNAuthorization
    {

        //public int RowID { get; set; }
        public string SKU { get; set; }
        public string ESN { get; set; }
        public string MeidHex { get; set; }
        public string MeidDec { get; set; }
        public string MSL { get; set; }
        public string OTKSL { get; set; }
        public string SKUName { get; set; }
        public string SWVersion { get; set; }
        public string ManfId { get; set; }
        public string ManfName { get; set; }
        //public string ErrorMessage { get; set; }
    }
}
