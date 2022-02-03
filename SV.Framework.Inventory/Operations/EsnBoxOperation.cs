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
    public class EsnBoxOperation : BaseCreateInstance
    {  
        public  string BoxIDUpdate(int esnID, string boxID, string ESN)
        {
            SV.Framework.DAL.Inventory.EsnBoxOperation esnBoxOperation = SV.Framework.DAL.Inventory.EsnBoxOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnBoxOperation>();
            string returnMessage = esnBoxOperation.BoxIDUpdate(esnID, boxID, ESN);
            return returnMessage;
        }
        public  EsnBoxIDInfo GetEsnBoxIDInfo(string ESN)
        {
            SV.Framework.DAL.Inventory.EsnBoxOperation esnBoxOperation = SV.Framework.DAL.Inventory.EsnBoxOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnBoxOperation>();
            EsnBoxIDInfo esnBoxIDInfo = esnBoxOperation.GetEsnBoxIDInfo(ESN); ;
            
            return esnBoxIDInfo;
        }        
    }

}
