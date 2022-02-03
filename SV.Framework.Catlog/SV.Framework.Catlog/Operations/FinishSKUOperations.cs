using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace SV.Framework.Catalog
{
    public class FinishSKUOperations : BaseCreateInstance
    {
        public List<CompanySKUno> GetCompanyFinalOrRawSKUList(int companyID, bool IsFinalSKU)
        {
            SV.Framework.DAL.Catalog.FinishSKUOperations finishSKUOperations = SV.Framework.DAL.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.DAL.Catalog.FinishSKUOperations>();

            List<CompanySKUno> skuList = finishSKUOperations.GetCompanyFinalOrRawSKUList(companyID, IsFinalSKU);
            return skuList;
        }

        public List<RawSKU> GetCompanyAssignedRawSKUList(int companyID, int itemCompanyGUID, bool IsCustomer, bool IsESNRequired)
        {
            SV.Framework.DAL.Catalog.FinishSKUOperations finishSKUOperations = SV.Framework.DAL.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.DAL.Catalog.FinishSKUOperations>();

            List<RawSKU> skuList = finishSKUOperations.GetCompanyAssignedRawSKUList(companyID, itemCompanyGUID, IsCustomer, IsESNRequired);
            return skuList;
        }
        public  List<RawSKU> GetKittedAssignedRawSKUs(int companyID, int itemCompanyGUID)
        {
            SV.Framework.DAL.Catalog.FinishSKUOperations finishSKUOperations = SV.Framework.DAL.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.DAL.Catalog.FinishSKUOperations>();

            List<RawSKU> skuList = finishSKUOperations.GetKittedAssignedRawSKUs(companyID, itemCompanyGUID);
            return skuList;
        }
        public  bool FinishedSKUInsertUpdate(DataTable skuDt, int itemCompanyGUID, int userId, Kittedsku kittedsku, string ActionName)
        {
            SV.Framework.DAL.Catalog.FinishSKUOperations finishSKUOperations = SV.Framework.DAL.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.DAL.Catalog.FinishSKUOperations>();

            bool returnValue = finishSKUOperations.FinishedSKUInsertUpdate(skuDt, itemCompanyGUID, userId, kittedsku, ActionName);
            return returnValue;
        }



    }
}
