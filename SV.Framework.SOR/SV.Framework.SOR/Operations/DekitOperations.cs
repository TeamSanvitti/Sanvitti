using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;

namespace SV.Framework.SOR
{
    public class DekitOperations :BaseCreateInstance
    {
        public int DeKittingCronJob()
        {
            SV.Framework.DAL.SOR.DekitOperations dekitOperations = SV.Framework.DAL.SOR.DekitOperations.CreateInstance<SV.Framework.DAL.SOR.DekitOperations>();

            int returnValue = dekitOperations.DeKittingCronJob(); ;
            return returnValue;
        }
        public  int DeKittingRequestInsertUpdate(DekitServiceOrder request, out string errorMessage)
        {
            SV.Framework.DAL.SOR.DekitOperations dekitOperations = SV.Framework.DAL.SOR.DekitOperations.CreateInstance<SV.Framework.DAL.SOR.DekitOperations>();

            errorMessage = string.Empty;
            int returnValue = dekitOperations.DeKittingRequestInsertUpdate(request, out errorMessage);

            return returnValue;

        }
        public  int DeKittingStatusUpdate(Int64 deKittingID, int userID, string deKittingStatus)
        {
            SV.Framework.DAL.SOR.DekitOperations dekitOperations = SV.Framework.DAL.SOR.DekitOperations.CreateInstance<SV.Framework.DAL.SOR.DekitOperations>();

            int returnValue = dekitOperations.DeKittingStatusUpdate(deKittingID, userID, deKittingStatus);
            return returnValue;

        }
        public  int DeKittingDelete(Int64 deKittingID)
        {
            SV.Framework.DAL.SOR.DekitOperations dekitOperations = SV.Framework.DAL.SOR.DekitOperations.CreateInstance<SV.Framework.DAL.SOR.DekitOperations>();

            int returnValue = dekitOperations.DeKittingDelete(deKittingID);
            
            return returnValue;
        }

        
        
        public  List<DekitESN> ValidateDeKittingESN(DekitServiceOrder request, out string errorMessage, out bool IsValidate)
        {
            IsValidate = true;
            List<DekitESN> esnList = default;
            errorMessage = string.Empty;
            SV.Framework.DAL.SOR.DekitOperations dekitOperations = SV.Framework.DAL.SOR.DekitOperations.CreateInstance<SV.Framework.DAL.SOR.DekitOperations>();
            esnList = dekitOperations.ValidateDeKittingESN(request, out errorMessage, out IsValidate);

            return esnList;
        }
        public  string GenerateServiceOrder()
        {
            SV.Framework.DAL.SOR.DekitOperations dekitOperations = SV.Framework.DAL.SOR.DekitOperations.CreateInstance<SV.Framework.DAL.SOR.DekitOperations>();
            string returnValue = dekitOperations.GenerateServiceOrder();

            return returnValue;

        }
        public List<ServiceRequestStatus> GetDeKitStatusList()
        {
            SV.Framework.DAL.SOR.DekitOperations dekitOperations = SV.Framework.DAL.SOR.DekitOperations.CreateInstance<SV.Framework.DAL.SOR.DekitOperations>();

            List<ServiceRequestStatus> sorStatusList = dekitOperations.GetDeKitStatusList();
            return sorStatusList;
        }

        public  List<DekittingDetail> GetDekittingSearch(string dekitRquestNumber, string customerRequestNumber, int companyID, string fromDate, string toDate, string SKU,  string ESN, int DeKitStatusID)
        {
            SV.Framework.DAL.SOR.DekitOperations dekitOperations = SV.Framework.DAL.SOR.DekitOperations.CreateInstance<SV.Framework.DAL.SOR.DekitOperations>();

            List<DekittingDetail> dekittingList = dekitOperations.GetDekittingSearch(dekitRquestNumber, customerRequestNumber, companyID, fromDate, toDate, SKU, ESN, DeKitStatusID);// new List<DekittingDetail>();
            
            return dekittingList;
        }

        public  DekittingDetail GetDekittingDetail(Int64 DeKittingID)
        {
            SV.Framework.DAL.SOR.DekitOperations dekitOperations = SV.Framework.DAL.SOR.DekitOperations.CreateInstance<SV.Framework.DAL.SOR.DekitOperations>();

            DekittingDetail dekittingDetail = dekitOperations.GetDekittingDetail(DeKittingID);// new DekittingDetail();
            return dekittingDetail;
        }
        
    }
}
