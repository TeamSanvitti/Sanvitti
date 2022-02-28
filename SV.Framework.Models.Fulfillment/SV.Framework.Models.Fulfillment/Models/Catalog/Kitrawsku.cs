using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{

    public class Kitrawsku
    {
        public int ItemCompanyGUID { get; set; }
        public int Quantity { get; set; }
        public string DisplayName { get; set; }

        //public int ContainerCount { get; set; }
        //public int ContainerUpdatedCount { get; set; }
    }
}
