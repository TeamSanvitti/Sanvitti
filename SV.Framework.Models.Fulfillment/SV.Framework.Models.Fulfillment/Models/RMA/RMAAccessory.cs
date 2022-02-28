using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.RMA
{
    [Serializable]
    public class RMAAccessory
    {
        [XmlIgnore]
        public int AccessoryID { get; set; }
        public string AccessoryNumber { get; set; }
        public string AccessoryDescription { get; set; }
        [XmlIgnore]
        public bool IsChecked { get; set; }
        [XmlIgnore]
        public int RMADetGUID { get; set; }


    }


}
