using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;
using System.Data;

namespace SV.Framework.Inventory
{ 
    public class InventoryReportOperation : BaseCreateInstance
    {
        public List<EsnInfo> GetESNQueryWithRepository(List<SV.Framework.Models.Fulfillment.EsnList> esnList)
        {
            SV.Framework.DAL.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.DAL.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.DAL.Inventory.InventoryReportOperation>();

            List<EsnInfo> esnInfoList = inventoryReportOperation.GetESNQueryWithRepository(esnList);
            return esnInfoList;
        }
        public List<EsnInfo> GetESNQuery(List<SV.Framework.Models.Fulfillment.EsnList> esnList)
        {
            SV.Framework.DAL.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.DAL.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.DAL.Inventory.InventoryReportOperation>();

            List<EsnInfo> esnInfoList = inventoryReportOperation.GetESNQueryWithRepository(esnList);

            return esnInfoList;
        }
        public List<EsnRepositoryDetail> GetCustomerEsnRepositoryDetail(int companyID, int itemCompanyGUID, DateTime fromDate, DateTime toDate, bool unUsedESN, bool showAllUnusedESN)
        {
            SV.Framework.DAL.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.DAL.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.DAL.Inventory.InventoryReportOperation>();
            return inventoryReportOperation.GetCustomerEsnRepositoryDetail(companyID, itemCompanyGUID, fromDate, toDate, unUsedESN, showAllUnusedESN);
        }
        public List<EsnRepository> GetCustomerEsnRepositoryDownload(int companyID, int itemCompanyGUID, DateTime fromDate, DateTime toDate, bool unUsedESN, bool showAllUnusedESN)
        {
            SV.Framework.DAL.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.DAL.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.DAL.Inventory.InventoryReportOperation>();
            return inventoryReportOperation.GetCustomerEsnRepositoryDownload(companyID, itemCompanyGUID, fromDate, toDate, unUsedESN, showAllUnusedESN);
        }
        public List<ReassignSKU> GetReassignSkuReport(int companyID, DateTime fromDate, DateTime toDate, string esn, string sku)
        {
            SV.Framework.DAL.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.DAL.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.DAL.Inventory.InventoryReportOperation>();
            return inventoryReportOperation.GetReassignSkuReport(companyID, fromDate, toDate, esn, sku);
        }


        public DataTable GetESNList(List<clsEsnxml> esnList)
        {
            SV.Framework.DAL.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.DAL.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.DAL.Inventory.InventoryReportOperation>();

            return inventoryReportOperation.GetESNList(esnList);
        }
        public int DeleteEsnsWithXls(List<clsEsnxml> esnList)
        {
            SV.Framework.DAL.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.DAL.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.DAL.Inventory.InventoryReportOperation>();
            return inventoryReportOperation.DeleteEsnsWithXls(esnList);
        }
        public List<StockInDemand> GetStockInDemandList(int companyID, string SKU, string fromDate, string toDate)
        {
            SV.Framework.DAL.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.DAL.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.DAL.Inventory.InventoryReportOperation>();
            return inventoryReportOperation.GetStockInDemandList(companyID, SKU, fromDate, toDate);
        }

        public List<StockCount> GetStockCountSummary(int companyId, string StockDateFrom, string StockDateTo, string sku, bool includeDisabledSKU)
        {
            SV.Framework.DAL.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.DAL.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.DAL.Inventory.InventoryReportOperation>();

            List<StockCount> stockList = inventoryReportOperation.GetStockCountSummary(companyId, StockDateFrom, StockDateTo, sku, includeDisabledSKU);

            return stockList;

        }
        public List<CurrentStock> GetCurrentStock(int companyId, string SKU, bool IsDisable, bool IsKitted)
        {
            SV.Framework.DAL.Inventory.InventoryReportOperation inventoryReportOperation = SV.Framework.DAL.Inventory.InventoryReportOperation.CreateInstance<SV.Framework.DAL.Inventory.InventoryReportOperation>();
            return inventoryReportOperation.GetCurrentStock(companyId, SKU, IsDisable, IsKitted);
        }


    }
}
