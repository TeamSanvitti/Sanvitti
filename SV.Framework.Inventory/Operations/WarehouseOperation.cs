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

        public  List<WarehouseInfo> GetWarehouse(int warehouseID)
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



    }
}
