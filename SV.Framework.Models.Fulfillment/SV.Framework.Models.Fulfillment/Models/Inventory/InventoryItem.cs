using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Inventory
{

    [XmlRoot(ElementName = "inventoryitem", IsNullable = true)]
    [Serializable]
    public class InventoryItem
    {
        private string _esn;
        private string _boxId;

        [XmlElement(ElementName = "esn", IsNullable = true)]
        public string Esn
        {
            get
            {
                return _esn;
            }
            set
            {
                _esn = value;
            }
        }

        [XmlElement(ElementName = "boxid", IsNullable = true)]
        public string BoxId
        {
            get
            {
                if (!string.IsNullOrEmpty(_boxId))
                {
                    return _boxId;
                }
                else
                {
                    return string.Empty;
                }
            }
            set
            {
                _boxId = value;
            }
        }
    }

}
