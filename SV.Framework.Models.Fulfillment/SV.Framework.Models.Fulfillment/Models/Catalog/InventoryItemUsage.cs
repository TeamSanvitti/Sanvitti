using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Catalog;

namespace SV.Framework.Models.Catalog
{
    public class InventoryItemUsage
    {
        private int companyGuid;
        private string companyName;
        private string sku;
        private List<InventoryItemPricing> itemPricing;

        public int CompanyGuid
        {
            get
            {
                return companyGuid;
            }
            set
            {
                companyGuid = value;
            }
        }

        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
            }
        }

        public string Sku
        {
            get
            {
                return sku;
            }
            set
            {
                sku = value;
            }
        }

        public List<InventoryItemPricing> ItemPricing
        {
            get
            {
                return itemPricing;
            }
            set
            {
                itemPricing = value;
            }
        }

    }

}
