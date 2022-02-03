using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class EsnReverseOperation : BaseCreateInstance
    {   
        public string ESNReverseUpdate(List<ESNReverse> esns, int userID)
        {
            SV.Framework.DAL.Inventory.EsnReverseOperation esnReverseOperation = SV.Framework.DAL.Inventory.EsnReverseOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnReverseOperation>();

            string errorMessage = esnReverseOperation.ESNReverseUpdate(esns, userID);

            return errorMessage;
        }

        public  List<ESNReverse> ESNValidate(List<ESNReverse> esns, int CompanyID, out List<ESNskuReverse> skuList)
        {
            SV.Framework.DAL.Inventory.EsnReverseOperation esnReverseOperation = SV.Framework.DAL.Inventory.EsnReverseOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnReverseOperation>();

            skuList = default;// new List<ESNskuReverse>();
            List<ESNReverse> esnList = esnReverseOperation.ESNValidate(esns, CompanyID, out skuList);
            return esnList;
        }
        

    }
}
