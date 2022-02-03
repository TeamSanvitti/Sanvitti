using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class PackingslipOperation : BaseCreateInstance
    {
        public  void PackingSlipInsertUpdate(int poid, string packingSlip)
        {
            SV.Framework.DAL.Fulfillment.PackingslipOperation packingslipOperation = SV.Framework.DAL.Fulfillment.PackingslipOperation.CreateInstance<DAL.Fulfillment.PackingslipOperation>();

            packingslipOperation.PackingSlipInsertUpdate(poid, packingSlip);

        }

    }
}
