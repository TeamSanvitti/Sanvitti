using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{

    public class POEsn
    {
        public string ESN { get; set; }
        //
        public string Hex { get; set; }
        public string Dec { get; set; }
        public string SerialNumber { get; set; }
        public string PalletID { get; set; }
        public string ContainerID { get; set; }

        public string BoxID { get; set; }
        public string ICCID { get; set; }

    }
    public class POEsnCsv
    {
        public string ESN { get; set; }
        //
        public string Hex { get; set; }
        public string Dec { get; set; }
        public string SerialNumber { get; set; }
        public string PalletID { get; set; }
        public string ContainerID { get; set; }

        public string BoxID { get; set; }
        //public string ICCID { get; set; }

    }
}
