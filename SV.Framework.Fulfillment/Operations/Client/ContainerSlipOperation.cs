using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class ContainerSlipOperation : BaseCreateInstance
    {       
        public List<ContainerModel> GetContainerLabelInfo(int poID)
        {
            SV.Framework.DAL.Fulfillment.ContainerSlipOperation containerSlipOperation = SV.Framework.DAL.Fulfillment.ContainerSlipOperation.CreateInstance<DAL.Fulfillment.ContainerSlipOperation>();
            List<ContainerModel> containers = containerSlipOperation.GetContainerLabelInfo(poID);
            
            return containers;
        }
        

    }
}
