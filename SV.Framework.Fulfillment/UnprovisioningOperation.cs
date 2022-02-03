using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Fulfillment;

namespace SV.Framework.Fulfillment
{
    public class UnprovisioningOperation
    {
        public static T CreateInstance<T>()
        {
            T retObj = default;
            if (retObj == null)
            {
                retObj = Activator.CreateInstance<T>();
            }
            return retObj;
        }
        private SV.Framework.DAL.Fulfillment.UnprovisioningOperation unprovisioningOperation = SV.Framework.DAL.Fulfillment.UnprovisioningOperation.CreateInstance<DAL.Fulfillment.UnprovisioningOperation>();

        public  List<UnprovisionPOs> GetPOUnprovisioingSearch(int companyID, string fulfillmentNumber)
        {
           // UnprovisionPOs fulfillmentInfo = null;// new FulfillmentEsNInfo();
            List<UnprovisionPOs> poList = unprovisioningOperation.GetPOUnprovisioingSearch(companyID, fulfillmentNumber);//new List<FulfillmentEsn>();
           

            return poList;
        }

        public  string UnprovisioingRequestInsert(UnprovisionPORequest request)
        {
            string errorMessage = unprovisioningOperation.UnprovisioingRequestInsert(request);
           

            return errorMessage;
        }

        public  List<UnprovisionStatus> GetUnprovisioingStatus()
        {
            List<UnprovisionStatus> statusList = unprovisioningOperation.GetUnprovisioingStatus();//new List<FulfillmentEsn>();
           
            return statusList;
        }

        public  List<UnprovisioingInfo> GetUnprovisioingRequestSearch(int companyID, string fulfillmentNumber, int statusID, string DateFrom, string DateTo)
        {
            
            List<UnprovisioingInfo> poList = unprovisioningOperation.GetUnprovisioingRequestSearch(companyID, fulfillmentNumber, statusID, DateFrom, DateTo);//new List<FulfillmentEsn>();
           
            return poList;
        }



    }
}
