using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace LabelApp.Models
{
    public class svShipBy
    {
        [Key]
        public int ShipByID { get; set; }
        public string ShipByCode { get; set; }
        public string ShipByText { get; set; }
        public string Nationality { get; set; }
        public bool Active { get; set; }
        public string Carrier { get; set; }
    }
}
