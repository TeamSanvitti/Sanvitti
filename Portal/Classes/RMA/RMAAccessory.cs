using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
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