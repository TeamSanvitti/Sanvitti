using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Inventory
{
    [XmlRoot(ElementName = "currentinventory", IsNullable = true)]
    [Serializable]
    public class InventoryList
    {
        private List<clsInventory> inventoryList;

        public InventoryList()
        {
            inventoryList = new List<clsInventory>();
        }

        [XmlElement(ElementName = "inventory", IsNullable = true)]
        public List<clsInventory> CurrentInventory
        {
            set
            {
                inventoryList = value;
            }
            get
            {
                return inventoryList;
            }
        }

        public bool Exists(string itemCode)
        {
            bool found = false;
            foreach (clsInventory item in CurrentInventory)
            {

                if (string.Compare(item.ItemCode, itemCode, true) == 0)
                {
                    found = true;
                    break;
                }
            }
            return found;
        }
    }
}
