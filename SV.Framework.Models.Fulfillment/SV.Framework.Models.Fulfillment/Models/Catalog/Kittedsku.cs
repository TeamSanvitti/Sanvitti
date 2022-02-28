using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{

    public class Kittedsku
    {
        public int ItemCompanyGUID { get; set; }
        public List<Kitrawsku> RawSKUs { get; set; }
        public string Status { get; set; }
    }
}
