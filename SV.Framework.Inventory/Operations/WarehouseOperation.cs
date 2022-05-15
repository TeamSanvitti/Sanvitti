using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class WarehouseOperation : BaseCreateInstance
    {

        public List<WarehouseInfo> GetWarehouse(int warehouseID)
        {
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();

            List<WarehouseInfo> warehouseList = warehouseOperation.GetWarehouse(warehouseID);// new List<Carriers>();
            return warehouseList;
        }
        public List<WarehouseStorage> GetWarehouseStorage(string warehouseCity, string warehouseLocation, int companyID)
        {
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();

            List<WarehouseStorage> warehouseList = warehouseOperation.GetWarehouseStorage(warehouseCity, warehouseLocation, companyID);// new List<Carriers>();
            return warehouseList;
        }
        public List<WhLocationInfo> GetWarehouseLocationReport(string warehouseCity, string warehouseLocation, int companyID, string SKU, string ReceiveFromDate, string ReceiveToDate)
        {
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();

            List<WhLocationInfo> warehouseList = warehouseOperation.GetWarehouseLocationReport(warehouseCity, warehouseLocation, companyID, SKU, ReceiveFromDate, ReceiveToDate);
            return warehouseList;
        }
        public List<WhLocationInfo> GetWarehouseLocationReplacement(string warehouseLocation, int companyID, string SKU)
        {
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();

            List<WhLocationInfo> warehouseList = warehouseOperation.GetWarehouseLocationReplacement(warehouseLocation, companyID, SKU);
            return warehouseList;
        }

        public List<WhLocationInfo> GetWarehouseLocationHistory(string warehouseLocation, int companyID)
        {
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();

            List<WhLocationInfo> warehouseList = warehouseOperation.GetWarehouseLocationHistory(warehouseLocation, companyID);
            return warehouseList;
        }


        public WarehouseStorage GetWarehouseStorageInfo(int warehouseStorageID)
        {
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();

            WarehouseStorage warehouseStorage = warehouseOperation.GetWarehouseStorageInfo(warehouseStorageID);// new List<Carriers>();
            return warehouseStorage;
        }
        public WarehouseStorage GetWarehouseLocationInfo(string warehouseLocation)
        {
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();

            WarehouseStorage warehouseStorage = warehouseOperation.GetWarehouseLocationInfo(warehouseLocation);// new List<Carriers>();
            return warehouseStorage;
        }

        public string ValidateWarehouseStorage(WarehouseStorage request)
        {
            string errorMessage = default;

            if (string.IsNullOrWhiteSpace(request.Aisle))
                errorMessage = "Level is required!";

            if (string.IsNullOrWhiteSpace(request.Aisle))
                errorMessage = "Bay is required!";

            if (string.IsNullOrWhiteSpace(request.Aisle))
                errorMessage = "Aisle is required!";

            if (string.IsNullOrWhiteSpace(request.WarehouseLocation))
                errorMessage = "Warehouse location is required!";

            if (request.CompanyID == 0)
                errorMessage = "Customer is required!";

            if (request.WarehouseID == 0)
                errorMessage = "Warehouse is required!";

            return errorMessage;
        }
        public string CreateWarehouseStorage(WarehouseStorage request)
        {
            string errorMessage = default;
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();

            errorMessage = warehouseOperation.CreateWarehouseStorage(request);
            return errorMessage;
        }
        public string DeleteWarehouseStorage(int warehouseStorageID, int userID)
        {
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();

            string returnMessage = warehouseOperation.DeleteWarehouseStorage(warehouseStorageID, userID);

            return returnMessage;
        }
        public List<EsnInfo> GetWarehouseLocationDetail(int ItemCompanyGUID, string warehouseLocation, string ReceiveFromDate, string ReceiveToDate, out List<NonEsnStorage> accessoryLoactionList)
        {
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();

            List<EsnInfo> esnWarehouseList = default;
            accessoryLoactionList = default;
            esnWarehouseList = warehouseOperation.GetWarehouseLocationDetail(ItemCompanyGUID, warehouseLocation, ReceiveFromDate, ReceiveToDate, out accessoryLoactionList);

            return esnWarehouseList;


        }
        public List<EsnInfo> WarehouseLocationEsnValidate(List<EsnInfo> ESNs, int companyID, string whLocation)
        {
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();

            List<EsnInfo> esnList = warehouseOperation.WarehouseLocationEsnValidate(ESNs, companyID, whLocation);

            return esnList;
        }
        public int WarehouseLocationNonEsnUpdate(int storageID, int userId, int ItemCompanyGUID, string whLocationOld, string whLocationNew, int qty, int oldQty, int loginUserID, string Comment, out string errorMessage)
        {
            errorMessage = "";
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();
            return warehouseOperation.WarehouseLocationNonEsnUpdate(storageID, userId, ItemCompanyGUID, whLocationOld, whLocationNew, qty, oldQty, loginUserID, Comment, out errorMessage);

        }
        public int WarehouseLocationESNUpdate(List<EsnInfo> ESNs, int userId, int ItemCompanyGUID, string whLocationOld, string whLocationNew, int qty, int loginUserID, string Comment, out string errorMessage)
        {
            errorMessage = "";
            SV.Framework.DAL.Inventory.WarehouseOperation warehouseOperation = SV.Framework.DAL.Inventory.WarehouseOperation.CreateInstance<SV.Framework.DAL.Inventory.WarehouseOperation>();
            return warehouseOperation.WarehouseLocationESNUpdate(ESNs, userId, ItemCompanyGUID, whLocationOld, whLocationNew, qty, loginUserID, Comment, out errorMessage);

        }


    }
}
