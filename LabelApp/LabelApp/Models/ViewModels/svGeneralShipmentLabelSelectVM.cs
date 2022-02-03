using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LabelApp.Models.ViewModels
{
    public class svGeneralShipmentLabelSelectVM
    {
        public int ShipmentID { get; set; }
        [Display(Name = "From name")]
        public string FromName { get; set; }
        [Display(Name = "To name")]
        public string ToName { get; set; }
        [Display(Name = "From date")]
        public string FromDate { get; set; }
        [Display(Name = "To date")]
        public string ToDate { get; set; }
        public string TrackingNumber { get; set; }
        public IEnumerable<svGeneralShipmentLabel> LabelList { get; set; }
       // public IList<svGeneralShipmentLabelSelect> svGeneralShipmentLabelSelect { get; set; }
    }
}
