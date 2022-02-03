using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.DAL.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class InvalidEsnOperation:BaseCreateInstance
    {
        
        public  List<InvalidEsn> GetInvalidEsnList(int companyId, int categoryID, string sku, int IsAssignedFlag)
        {
            SV.Framework.DAL.Inventory.InvalidEsnOperation invalidEsnOperation = SV.Framework.DAL.Inventory.InvalidEsnOperation.CreateInstance<SV.Framework.DAL.Inventory.InvalidEsnOperation>();

            List<InvalidEsn> InvalidEsnList = invalidEsnOperation.GetInvalidEsnList(companyId, categoryID, sku, IsAssignedFlag);

            return InvalidEsnList;

        }
        
    }
}
