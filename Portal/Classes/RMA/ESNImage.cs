using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class ESNImage
    {
        public string FileName { get; set; }
        public int RMADelGUID { get; set; }
        public int PictureID { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }

    }
}