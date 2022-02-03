using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace SV.Framework.Catalog
{
    public class ItemLogOperation : BaseCreateInstance
    {
        public  void ItemLogInsert(ItemLogModel request)
        {
            SV.Framework.DAL.Catalog.ItemLogOperation itemLogOperation = SV.Framework.DAL.Catalog.ItemLogOperation.CreateInstance<SV.Framework.DAL.Catalog.ItemLogOperation>();
            itemLogOperation.ItemLogInsert(request);
        }

        public  List<ItemLog> GetProductLog(int itemGUID, int itemCompanyGUID)
        {
            SV.Framework.DAL.Catalog.ItemLogOperation itemLogOperation = SV.Framework.DAL.Catalog.ItemLogOperation.CreateInstance<SV.Framework.DAL.Catalog.ItemLogOperation>();

            List<ItemLog> logList = itemLogOperation.GetProductLog(itemGUID, itemCompanyGUID);// new List<ItemLog>();
            return logList;

        }
        
    }
}
