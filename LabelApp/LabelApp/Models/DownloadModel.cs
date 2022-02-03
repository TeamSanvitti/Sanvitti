using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelApp.Models
{
    public class DownloadModel
    {
        //[key]
        public int ShipmentID { get; set; }
        public string Height { get; set; }
        public string Width { get; set; }
        public string TrackingNumber { get; set; }
        public string Base64String { get; set; }
        public string Carrier { get; set; }
        public string FinalPostage { get; set; }

    }
}
