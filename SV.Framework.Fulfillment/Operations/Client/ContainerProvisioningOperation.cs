using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml.Serialization;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class ContainerProvisioningOperation : BaseCreateInstance
    {
        public List<ContainerESNInfo> GetContainerESNsNew(int companyID, string fulfillmentNumber, List<ContainerESN> esnList) 
        {
            SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation containerProvisioningOperation = SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation.CreateInstance<DAL.Fulfillment.ContainerProvisioningOperation>();

            List<ContainerESNInfo> esnsList = containerProvisioningOperation.GetContainerESNsNew(companyID, fulfillmentNumber, esnList);

            return esnsList;
        }
        
        public List<ContainerESNInfo> GetContainerESNsNew2(int companyID, string fulfillmentNumber, List<ContainerESN> esnList)
        {
            SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation containerProvisioningOperation = SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation.CreateInstance<DAL.Fulfillment.ContainerProvisioningOperation>();

            List<ContainerESNInfo> esnsList = containerProvisioningOperation.GetContainerESNsNew2(companyID, fulfillmentNumber, esnList);

            return esnsList;
        }
        public List<ContainerESNInfo> GetNonESNContainer(int POID, List<ContainerNonESN> esnList, int containerQty)
        {
            SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation containerProvisioningOperation = SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation.CreateInstance<DAL.Fulfillment.ContainerProvisioningOperation>();

            List<ContainerESNInfo> esnsList = containerProvisioningOperation.GetNonESNContainer(POID, esnList, containerQty);

            return esnsList;
        }

            public List<ContainerESNInfo> GetContainerESNs(int companyID, string fulfillmentNumber, List<ContainerESN> esnList)
        {
            SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation containerProvisioningOperation = SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation.CreateInstance<DAL.Fulfillment.ContainerProvisioningOperation>();

            List<ContainerESNInfo> esnsList = containerProvisioningOperation.GetContainerESNs(companyID, fulfillmentNumber, esnList);
            return esnsList;
        }

        public int  PurchaseOrderContainerESNAssign(int companyID, string fulfillmentNumber, int userId, List<ContainerESNInfo> esnList)
        {
            SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation containerProvisioningOperation = SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation.CreateInstance<DAL.Fulfillment.ContainerProvisioningOperation>();

            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            SV.Framework.DAL.Fulfillment.LogOperations logOperations = SV.Framework.DAL.Fulfillment.LogOperations.CreateInstance<SV.Framework.DAL.Fulfillment.LogOperations>();
            int poRecordCount = containerProvisioningOperation.PurchaseOrderContainerESNAssign(companyID, fulfillmentNumber, userId, esnList);
            return poRecordCount;
        }

        
        public int PurchaseOrderContainerESNAssign2(int companyID, string fulfillmentNumber, int userId, List<ContainerESNInfo> esnList, int poid)
        {
            SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation containerProvisioningOperation = SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation.CreateInstance<DAL.Fulfillment.ContainerProvisioningOperation>();

            SV.Framework.DAL.Fulfillment.LogOperations logOperations = SV.Framework.DAL.Fulfillment.LogOperations.CreateInstance<SV.Framework.DAL.Fulfillment.LogOperations>();
            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();

            int poRecordCount = containerProvisioningOperation.PurchaseOrderContainerESNAssign2(companyID, fulfillmentNumber, userId, esnList, poid);
            return poRecordCount;
        }
        public int PurchaseOrderContainerNonESNAssign(int poid, int userId, List<ContainerNonESN> esnList, int containerQty)
        {
            SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation containerProvisioningOperation = SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation.CreateInstance<DAL.Fulfillment.ContainerProvisioningOperation>();
            SV.Framework.DAL.Fulfillment.LogOperations logOperations = SV.Framework.DAL.Fulfillment.LogOperations.CreateInstance<SV.Framework.DAL.Fulfillment.LogOperations>();
            SV.Framework.Models.Fulfillment.FulfillmentLogModel logModel = new SV.Framework.Models.Fulfillment.FulfillmentLogModel();
            int poRecordCount = containerProvisioningOperation.PurchaseOrderContainerNonESNAssign(poid, userId, esnList, containerQty);

            return poRecordCount;
        }



        public List<SV.Framework.LabelGenerator.ContainerModel> GetContainerLabelInfo(int poID)
        {
            SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation containerProvisioningOperation = SV.Framework.DAL.Fulfillment.ContainerProvisioningOperation.CreateInstance<DAL.Fulfillment.ContainerProvisioningOperation>();

            List<SV.Framework.LabelGenerator.ContainerModel> containers = containerProvisioningOperation.GetContainerLabelInfo(poID);

            return containers;
        }        
    }   

}