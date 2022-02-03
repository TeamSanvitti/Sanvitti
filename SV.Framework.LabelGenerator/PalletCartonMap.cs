using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SV.Framework.LabelGenerator
{
    public class PalletCartonMap
    {
        public string PalletID { get; set; }
        public List<CartonModel> Cartons { get; set; }
    }
    public class CartonModel
    {
        public string Column1 { get; set; }
        public string BOXColumn1 { get; set; }
        public string Column2 { get; set; }
        public string BOXColumn2 { get; set; }
    }
}
