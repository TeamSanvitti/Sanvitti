using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class MasterCartonInfo
    {
        public List<CartonItem> CartonItems { get; set; }
        public String SWVersion { get; set; }
        public String HWVersion { get; set; }
        public String ShipDate { get; set; }
        public String ContainerID { get; set; }
        public String SKU { get; set; }
        public String CartonQty { get; set; }
        public String Comments { get; set; }
        public String UPC { get; set; }
        public String ProductType { get; set; }
        public String OSType { get; set; }

    }

    public class CartonItem
    {
        public String IMEI { get; set; }
        public string HEX { get; set; }
        public String MEID { get; set; }

    }
}
