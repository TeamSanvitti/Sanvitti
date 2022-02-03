using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelApp.Models.ViewModels
{
    public class svGeneralShipmentLabelVM
    {
        public svGeneralShipmentLabel svGeneralShipmentLabel { get; set; }
        public IEnumerable<SelectListItem> ShipViaSelectList { get; set; }
        public IEnumerable<SelectListItem> PackageTypeSelectList { get; set; }
    }
}
