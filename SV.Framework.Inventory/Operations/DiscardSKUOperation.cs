using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;
using SV.Framework.DAL.Inventory;

namespace SV.Framework.Inventory
{
    public class DiscardSKUOperation : BaseCreateInstance
    {
        public int DiscartedSKUInsert(DiscartedSKU request)
        {
            SV.Framework.DAL.Inventory.DiscardSKUOperation discardSKUOperation = SV.Framework.DAL.Inventory.DiscardSKUOperation.CreateInstance<SV.Framework.DAL.Inventory.DiscardSKUOperation>();

            int returnValue = discardSKUOperation.DiscartedSKUInsert(request);
            return returnValue;
        }
        public SkuInfo SKUInfoBySKUId(int ItemCompanyGUID)
        {
            SV.Framework.DAL.Inventory.DiscardSKUOperation discardSKUOperation = SV.Framework.DAL.Inventory.DiscardSKUOperation.CreateInstance<SV.Framework.DAL.Inventory.DiscardSKUOperation>();

            return discardSKUOperation.SKUInfoBySKUId(ItemCompanyGUID);
        }
        public List<DiscartedSKU> DiscartedSKUSearch(int CompanyID, string FromDate, string ToDate, string SKU)
        {
            SV.Framework.DAL.Inventory.DiscardSKUOperation discardSKUOperation = SV.Framework.DAL.Inventory.DiscardSKUOperation.CreateInstance<SV.Framework.DAL.Inventory.DiscardSKUOperation>();

            List<DiscartedSKU> DiscartedSKUList = discardSKUOperation.DiscartedSKUSearch(CompanyID, FromDate, ToDate, SKU);
            return DiscartedSKUList;
        }

    }
}
