using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class NonEsnOperation: BaseCreateInstance
    {
        
        public  List<NonEsnHeader> GetNonESNwithHeaderList(int companyID, string CustOrderNumber, string ShipFrom, string ShipTo, string SKU, int categoryID, string location)
        {
            SV.Framework.DAL.Inventory.NonEsnOperation nonEsnOperation = SV.Framework.DAL.Inventory.NonEsnOperation.CreateInstance<SV.Framework.DAL.Inventory.NonEsnOperation>();

            List<NonEsnHeader> headerList = nonEsnOperation.GetNonESNwithHeaderList(companyID, CustOrderNumber, ShipFrom, ShipTo, SKU, categoryID, location);
            
            return headerList;
        }

        public  NonEsnHeader GetNonESNwithHeaderDetails(int ESNHeaderId)
        {
            SV.Framework.DAL.Inventory.NonEsnOperation nonEsnOperation = SV.Framework.DAL.Inventory.NonEsnOperation.CreateInstance<SV.Framework.DAL.Inventory.NonEsnOperation>();

            NonEsnHeader nonEsnHeader = nonEsnOperation.GetNonESNwithHeaderDetails(ESNHeaderId);
            
            return nonEsnHeader;
        }

        public  int NonESNInventoryInsert(NonESNInventory request, out int insertCout, out int updateCount, out string errorMessage)
        {
            SV.Framework.DAL.Inventory.NonEsnOperation nonEsnOperation = SV.Framework.DAL.Inventory.NonEsnOperation.CreateInstance<SV.Framework.DAL.Inventory.NonEsnOperation>();

            updateCount = 0;
            insertCout = 0;
            errorMessage = default;
            
            int returnResult = nonEsnOperation.NonESNInventoryInsert(request, out insertCout, out updateCount, out errorMessage);


            return returnResult;
        }
        public int NonESNInventoryDelete(int ESNHeaderId, int UserID, out string errorMessage)
        {
            SV.Framework.DAL.Inventory.NonEsnOperation nonEsnOperation = SV.Framework.DAL.Inventory.NonEsnOperation.CreateInstance<SV.Framework.DAL.Inventory.NonEsnOperation>();
            errorMessage = default;

            int returnResult = nonEsnOperation.NonESNInventoryDelete(ESNHeaderId, UserID, out errorMessage);

            return returnResult;
        }

    }
}
