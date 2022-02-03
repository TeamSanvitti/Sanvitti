using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class AssignInventotyOperation : BaseCreateInstance
    {
        public void AssignInventoryService()
        {
            SV.Framework.DAL.Fulfillment.AssignInventotyOperation assignInventotyOperation = SV.Framework.DAL.Fulfillment.AssignInventotyOperation.CreateInstance<DAL.Fulfillment.AssignInventotyOperation>();
            assignInventotyOperation.AssignInventoryService();            
        }

    }
}
