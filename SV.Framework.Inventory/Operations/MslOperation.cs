using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.DAL.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class MslOperation : BaseCreateInstance
    {
        
        public List<EsnMslDetail> AssignMSL2FulfillmentESN(string ESN, int userID, int companyID, out int poRecordCount, out string poErrorMessage)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            poRecordCount = 0;
            poErrorMessage = default;
            List<EsnMslDetail> esnList = mslOperation.AssignMSL2FulfillmentESN(ESN, userID, companyID, out poRecordCount, out poErrorMessage);
            return esnList;
        }
        public List<EsnMslDetail> GetMissingMSL(int companyID)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            List<EsnMslDetail> esnList = mslOperation.GetMissingMSL(companyID);
            return esnList;

        }
        public List<ESNLog> GetEsnLog(string esn)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            List<ESNLog> esnLogDetail = mslOperation.GetEsnLog(esn);
            return esnLogDetail;
        }
        public bool MslEsnInsertUpdate(List<EsnUpload> mslEsnList, int itemCompanyGUID, int userID, string fileName, string comment,
            DateTime uploadDate, out int insertCout, out int updateCount, out string errorMessage)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            insertCout = 0;
            updateCount = 0;
            errorMessage = default;
            bool returnValue = mslOperation.MslEsnInsertUpdate(mslEsnList,  itemCompanyGUID,  userID,  fileName,  comment, uploadDate, out  insertCout, out  updateCount, out  errorMessage);

            return returnValue;
        }
        public bool MslEsnInsertUpdateNew(EsnHeaderUpload esnInfo, string fileName, Int64 orderTransferID, Int64 transientOrderID, out int insertCout, out int updateCount, out string errorMessage)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            insertCout = 0;
            updateCount = 0;
            errorMessage = default;
            bool returnValue = mslOperation.MslEsnInsertUpdateNew(esnInfo,  fileName, orderTransferID, transientOrderID, out  insertCout, out  updateCount, out  errorMessage);

            return returnValue;
        }
        public string GenerateOrderNumber()
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            string returnValue = mslOperation.GenerateOrderNumber();
            return returnValue;
        }
        public int ESNDelete(string ESN)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            int returnValue = mslOperation.ESNDelete(ESN);
            
            return returnValue;
        }

        public int ESNDelete(List<EsnUploadNew> esnList)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            int returnValue = mslOperation.ESNDelete(esnList);
            return returnValue;
        }
        public List<CompanySKUno> GetCompanySKUList(int companyID, int isSIM)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            List<CompanySKUno> skuList = mslOperation.GetCompanySKUList(companyID, isSIM);
            return skuList;
        }
        public List<CompanySKUno> GetCompanySKUs(int companyID, int isSIM)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            List<CompanySKUno> skuList = mslOperation.GetCompanySKUs(companyID, isSIM);
            return skuList;
        }
        public List<CompanySKUno> GetCompanySKUsNew(int companyID, int isSIM, string ModelNumber)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            List<CompanySKUno> skuList = mslOperation.GetCompanySKUsNew(companyID, isSIM, ModelNumber);
            return skuList;
        }

        public List<EsnUpload> MslESNs_Validate(List<EsnUpload> mslEsnList, int itemCompanyGUID, out string errorMessage, out string duplicateESN, out string simMessage, out bool isLTE, out bool isSim, out int returnValue, out string poEsnMessage)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            poEsnMessage = string.Empty;
            errorMessage = string.Empty;
            duplicateESN = string.Empty;
            isLTE = false;
            simMessage = string.Empty;
            isSim = false;
            returnValue = 0;
            List<EsnUpload> esnList = mslOperation.MslESNs_Validate(mslEsnList, itemCompanyGUID, out errorMessage, out duplicateESN, out simMessage, out isLTE, out isSim, out returnValue, out poEsnMessage);

            return esnList;
        }
        public List<EsnUploadNew> MslESNs_ValidateNew(List<EsnUploadNew> mslEsnList, int itemCompanyGUID, string OrderNumber, out string errorMessage, out string duplicateESN, out string simMessage, out bool isLTE, out bool isOrderNumber, out int returnValue, out string poEsnMessage)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            poEsnMessage = string.Empty;
            errorMessage = string.Empty;
            duplicateESN = string.Empty;
            isLTE = false;
            simMessage = string.Empty;
            isOrderNumber = false;
            returnValue = 0;
            List<EsnUploadNew> esnList = mslOperation.MslESNs_ValidateNew(mslEsnList, itemCompanyGUID, OrderNumber, out errorMessage, out duplicateESN, out simMessage, out isLTE, out isOrderNumber, out returnValue, out poEsnMessage);
            
            return esnList;
        }

        public List<EsnUploadNew> MslESNs_ValidateNew2(List<EsnUploadNew> mslEsnList, int itemCompanyGUID, string OrderNumber, out string errorMessage, out string duplicateESN, out string simMessage, out bool isLTE, out bool isOrderNumber, out int returnValue, out string poEsnMessage, out string poESNquarantine, out string poESNBoxIDs)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            poESNBoxIDs = string.Empty;
            poEsnMessage = string.Empty;
            errorMessage = string.Empty;
            duplicateESN = string.Empty;
            poESNquarantine = string.Empty;
            isLTE = false;
            simMessage = string.Empty;
            isOrderNumber = false;
            returnValue = 0;
            List<EsnUploadNew> esnList = mslOperation.MslESNs_ValidateNew2(mslEsnList,  itemCompanyGUID, OrderNumber, out errorMessage, out duplicateESN, out simMessage, out isLTE, out isOrderNumber, out returnValue, out poEsnMessage, out poESNquarantine, out  poESNBoxIDs);
            return esnList;
        }
        public List<EsnUploadNew> MslESNs_ValidateNew1(List<EsnUploadNew> mslEsnList, int itemCompanyGUID, string OrderNumber, Int64 orderTransferID, out string errorMessage, 
            out string duplicateESN, out string simMessage, out bool isLTE, out bool isOrderNumber, out int returnValue, out string poEsnMessage, 
            out string poESNquarantine, out string poESNBoxIDs, out string poLocations)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            poLocations = string.Empty;
            poESNBoxIDs = string.Empty;
            poEsnMessage = string.Empty;
            errorMessage = string.Empty;
            duplicateESN = string.Empty;
            poESNquarantine = string.Empty;
            isLTE = false;
            simMessage = string.Empty;
            isOrderNumber = false;
            returnValue = 0;
            List<EsnUploadNew> esnList = mslOperation.MslESNs_ValidateNew1(mslEsnList, itemCompanyGUID, OrderNumber, orderTransferID, out errorMessage, out duplicateESN, out simMessage, out isLTE, out isOrderNumber, out returnValue, out poEsnMessage, out poESNquarantine, out poESNBoxIDs, out poLocations);
            return esnList;
        }

        public List<EsnHeaders> GetESNwithHeaderList(int companyID, string CustOrderNumber, string ShipFrom, string ShipTo, string ESN, string TrackingNumber, string SKU, int categoryID, string location)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            List<EsnHeaders> headerList = mslOperation.GetESNwithHeaderList(companyID, CustOrderNumber, ShipFrom, ShipTo, ESN, TrackingNumber, SKU, categoryID, location);
            return headerList;
        }
        public EsnHeaders GetESNwithHeaderDetail(int ESNHeaderId)
        {
            DAL.Inventory.MslOperation mslOperation = DAL.Inventory.MslOperation.CreateInstance<DAL.Inventory.MslOperation>();

            EsnHeaders headerDetail = mslOperation.GetESNwithHeaderDetail(ESNHeaderId);
            return headerDetail;
        }


    }
}
