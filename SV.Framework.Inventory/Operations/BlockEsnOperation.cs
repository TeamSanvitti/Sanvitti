using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.DAL.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
   public class BlockEsnOperation : BaseCreateInstance
    {
        
        public  List<BlockESN> BlockEsnSearch(int CompanyID, string FromDate, string ToDate, string SKU, string esn, string action, string status)
        {
            SV.Framework.DAL.Inventory.BlockEsnOperation blockEsnOperation = SV.Framework.DAL.Inventory.BlockEsnOperation.CreateInstance<SV.Framework.DAL.Inventory.BlockEsnOperation>();

            List<BlockESN> EsnList = blockEsnOperation.BlockEsnSearch( CompanyID,  FromDate,  ToDate,  SKU,  esn,  action,  status);

            return EsnList;

        }

        public List<BlockEsn> BlockEsnValidate(int ItemCompanyGUID, string esnData)
        {
            SV.Framework.DAL.Inventory.BlockEsnOperation blockEsnOperation = SV.Framework.DAL.Inventory.BlockEsnOperation.CreateInstance<SV.Framework.DAL.Inventory.BlockEsnOperation>();

            List<BlockEsn> EsnList = blockEsnOperation.BlockEsnValidate(ItemCompanyGUID, esnData);

            return EsnList;

        }

        public  string ValidateRequiredFields(BlockEsnInfo request)
        {
            string returnMessage = string.Empty;
            System.Text.StringBuilder sb = new StringBuilder();
            if (string.IsNullOrEmpty(request.Comment))
                sb.Append("Comment required!, ");
            if (string.IsNullOrEmpty(request.ESNData))
                sb.Append("ESNs required!, ");
            if(request.ItemCompanyGUID == 0)
                sb.Append("SKU required!, ");
            if (request.ReceivedBy == 0)
                sb.Append("Received by required!, ");
            returnMessage = sb.ToString();
            return returnMessage;
        }
        public  int StockBlockInsert(BlockEsnInfo request)
        {
            SV.Framework.DAL.Inventory.BlockEsnOperation blockEsnOperation = SV.Framework.DAL.Inventory.BlockEsnOperation.CreateInstance<SV.Framework.DAL.Inventory.BlockEsnOperation>();

            int returnValue = blockEsnOperation.StockBlockInsert(request);

            return returnValue;
        }
        public  int StockBlockUpdate(int blockID, string status, int UserID)
        {
            SV.Framework.DAL.Inventory.BlockEsnOperation blockEsnOperation = SV.Framework.DAL.Inventory.BlockEsnOperation.CreateInstance<SV.Framework.DAL.Inventory.BlockEsnOperation>();

            int returnValue = blockEsnOperation.StockBlockUpdate(blockID, status, UserID);
           
            
            return returnValue;
        }
    }
}
 