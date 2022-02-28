using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{
    public class ItemCameraInfo
    {
        public int ItemCameraID { get; set; }
        public int ItemGUID { get; set; }
        public int CameraID { get; set; }
        public string CameraType { get; set; }
        public string Pixel { get; set; }
        public string Zoom { get; set; }

    }
}
