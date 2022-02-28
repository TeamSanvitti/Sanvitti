using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{
    public class CatalogEnums
    {

        public enum AvailableType
        {
            Available = 0,
            NotAvailable = 1,
            Cancelled = 2,
            Other = 3
        }
        public enum DeviceTypeCategory
        {
            Phone = 1,
            Accessory = 2,
            Other = 0
        }
        public enum ImageType
        {
            Main = 0,
            front = 1,
            Accessories = 2,
            Other = 3
        }
        public enum ItemPricingType
        {
            Regular = 1,
            Special = 2

        }
        public enum InventoryItemType
        {
            New = 1,
            Refurbish = 2,
            PreOwned = 3
        }

    }
}
