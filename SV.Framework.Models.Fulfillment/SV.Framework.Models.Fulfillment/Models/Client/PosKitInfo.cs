using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Framework.Models.Fulfillment
{
    public class PosKitInfo
    {
        public String SWVersion { get; set; }
        public String HWVersion { get; set; }
        public String SKU { get; set; }
        public String UPC { get; set; }
        public String SerialNum { get; set; }
        public String ESN { get; set; }
        public String IMEI2 { get; set; }
        public String MEID { get; set; }
        public String HEX { get; set; }
        public String ICCID { get; set; }
        public String ItemName { get; set; }
        public String CompanyName { get; set; }
        public String ProductType { get; set; }
        public String ShipDate { get; set; }
        public String OSType { get; set; }
        public List<KitBoxInfo> KitBoxList { get; set; }
        //public String Accesory { get; set; }

    }
    public class KitBoxInfo
    {
        public String DisplayName { get; set; }
        public String OriginCountry { get; set; }

    }
}
