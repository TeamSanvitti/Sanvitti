using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class EsnReplacementOperation : BaseCreateInstance
    {
        
        public int ESNReplacementUpdate(string FulfillmentNumber, string AssignedESN, string ESN, string comment, int approvedBy, int userID, out string errorMessage)
        {
            SV.Framework.DAL.Inventory.EsnReplacementOperation esnReplacementOperation = SV.Framework.DAL.Inventory.EsnReplacementOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnReplacementOperation>();

            errorMessage = "";
            int returnResult = esnReplacementOperation.ESNReplacementUpdate(FulfillmentNumber, AssignedESN, ESN, comment, approvedBy, userID, out errorMessage);
            return returnResult;
        }

        public  List<ESNReplacedInfo> ESNReplacementValidate(string AssignedESN, string ESN)
        {
            SV.Framework.DAL.Inventory.EsnReplacementOperation esnReplacementOperation = SV.Framework.DAL.Inventory.EsnReplacementOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnReplacementOperation>();

            List<ESNReplacedInfo> ESNInfo = esnReplacementOperation.ESNReplacementValidate(AssignedESN, ESN);
            
            return ESNInfo;
        }
        public string ESNReplacementValidateOld(string AssignedESN, string ESN)
        {
            SV.Framework.DAL.Inventory.EsnReplacementOperation esnReplacementOperation = SV.Framework.DAL.Inventory.EsnReplacementOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnReplacementOperation>();

            string errorMessage = esnReplacementOperation.ESNReplacementValidateOld(AssignedESN, ESN);
            
            return errorMessage;
        }

        public  FulfillemntInfo GetFulfillmentInfo(string fulfillmentNumber, int companyID, string ESN, out string Status)
        {
            SV.Framework.DAL.Inventory.EsnReplacementOperation esnReplacementOperation = SV.Framework.DAL.Inventory.EsnReplacementOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnReplacementOperation>();

            Status = "";
            FulfillemntInfo fulfillemntInfo = esnReplacementOperation.GetFulfillmentInfo(fulfillmentNumber, companyID, ESN, out Status);
            
            return fulfillemntInfo;
        }       
    }
}
