using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Common;
using SV.Framework.Models.Inventory;

namespace SV.Framework.Inventory
{
    public class KittingOperations : BaseCreateInstance
    {
        public  string ESNKittingInsert(string ESN, DateTime currentCSTDateTime, int userID)
        {
            SV.Framework.DAL.Inventory.KittingOperations kittingOperations = SV.Framework.DAL.Inventory.KittingOperations.CreateInstance<SV.Framework.DAL.Inventory.KittingOperations>();
            string errorMessage = kittingOperations.ESNKittingInsert(ESN, currentCSTDateTime, userID);
            return errorMessage;
        }

        public string PurchaseOrderKittingUpdate(string poAssignIDs, DateTime currentCSTDateTime, int userID)
        {
            SV.Framework.DAL.Inventory.KittingOperations kittingOperations = SV.Framework.DAL.Inventory.KittingOperations.CreateInstance<SV.Framework.DAL.Inventory.KittingOperations>();

            string errorMessage = kittingOperations.PurchaseOrderKittingUpdate(poAssignIDs, currentCSTDateTime, userID);
            return errorMessage;
        }
        public  KittingInfo GetKittingInfoByESN(string ESN, int ItemCompanyGUID)
        {
            SV.Framework.DAL.Inventory.KittingOperations kittingOperations = SV.Framework.DAL.Inventory.KittingOperations.CreateInstance<SV.Framework.DAL.Inventory.KittingOperations>();

            KittingInfo kittingInfo = kittingOperations.GetKittingInfoByESN(ESN, ItemCompanyGUID);
            return kittingInfo;
        }

        public KittingInfo GetPurchaseOrderKittingInfo(string poNumber, string ESN, string boxID, string palletID)
        {
            SV.Framework.DAL.Inventory.KittingOperations kittingOperations = SV.Framework.DAL.Inventory.KittingOperations.CreateInstance<SV.Framework.DAL.Inventory.KittingOperations>();

            KittingInfo kittingInfo = kittingOperations.GetPurchaseOrderKittingInfo(poNumber, ESN, boxID, palletID);// new KittingInfo();
            return kittingInfo;
        }
        public List<PurchaseOrderKittingSummary> GetPurchaseOrderKittingSummary(string poNumber, string ESN, string boxID, int userID, string fromDate, string toDate)
        {
            SV.Framework.DAL.Inventory.KittingOperations kittingOperations = SV.Framework.DAL.Inventory.KittingOperations.CreateInstance<SV.Framework.DAL.Inventory.KittingOperations>();

            List<PurchaseOrderKittingSummary> kittingSummary = kittingOperations.GetPurchaseOrderKittingSummary(poNumber, ESN, boxID, userID, fromDate, toDate);
            return kittingSummary;
        }
        public List<POPallet> GetPurchaseOrderPallets(string poNumber)
        {
            SV.Framework.DAL.Inventory.KittingOperations kittingOperations = SV.Framework.DAL.Inventory.KittingOperations.CreateInstance<SV.Framework.DAL.Inventory.KittingOperations>();

            List<POPallet> pallets = kittingOperations.GetPurchaseOrderPallets(poNumber);
            
            return pallets;
        }
        public List<SKUiNFO> GetKittedSKUs(string ESN)
        {
            SV.Framework.DAL.Inventory.KittingOperations kittingOperations = SV.Framework.DAL.Inventory.KittingOperations.CreateInstance<SV.Framework.DAL.Inventory.KittingOperations>();

            List<SKUiNFO> SKUs = kittingOperations.GetKittedSKUs(ESN);
            return SKUs;
        }


    }
}
