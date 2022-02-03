using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class Categories
    {
        public int CategoryGUID { get; set; }
        public string CategoryName { get; set; }
        public string MakerName { get; set; }
        public string CategoryImage { get; set; }
        public int MakerGUID { get; set; }
        public int CarrierID { get; set; }
    }
}