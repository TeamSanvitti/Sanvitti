using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.RMA;
using SV.Framework.Models.Common;

namespace SV.Framework.RMA
{
    public class RmaOperation : BaseCreateInstance 
    {
        
        public  RmaInfo GetRMA(int rmaGUID)
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            RmaInfo rmaInfo = rmaOperation.GetRMA(rmaGUID);// new RmaInfo();
            return rmaInfo;
        }

        public  int RMAReceiveInsert(RmaReceive rmaInfo)
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            int ReturnCode = rmaOperation.RMAReceiveInsert(rmaInfo);
            return ReturnCode;
        }
        public int RMAReceiveDelete(int rmaReceiveDetailGUID, int userID)
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            int ReturnCode = rmaOperation.RMAReceiveDelete(rmaReceiveDetailGUID, userID);
            return ReturnCode;
        }

        public  RmaReceive GetRmaReceiveSearch(int companyID, string rmaNumber, string trackingNumber)
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            RmaReceive rmaReceive = rmaOperation.GetRmaReceiveSearch(companyID, rmaNumber, trackingNumber);// new RmaReceive();
            return rmaReceive;
        }

        public  string ValidateRmaFields(RmaModel rmaInfo)
        {
            string returnResult = string.Empty;
            if(string.IsNullOrEmpty(rmaInfo.Address1))
            {
                returnResult = "Address is required!";
            }
            if (string.IsNullOrEmpty(rmaInfo.City))
            {
                if (string.IsNullOrEmpty(returnResult))
                    returnResult = "City is required!";
                else
                {
                    returnResult = returnResult + "\n City is required!";
                }
            }
            if (string.IsNullOrEmpty(rmaInfo.ContactName))
            {
                if (string.IsNullOrEmpty(returnResult))
                    returnResult = "Contact name is required!";
                else
                {
                    returnResult = returnResult + "\n name is required!";
                }
            }
            if (string.IsNullOrEmpty(rmaInfo.ContactNumber))
            {
                if (string.IsNullOrEmpty(returnResult))
                    returnResult = "Contact number is required!";
                else
                {
                    returnResult = returnResult + "\n Contact number is required!";
                }
            }
            if (string.IsNullOrEmpty(rmaInfo.Email))
            {
                if (string.IsNullOrEmpty(returnResult))
                    returnResult = "Email is required!";
                else
                {
                    returnResult = returnResult + "\n Email is required!";
                }
            }
            if (string.IsNullOrEmpty(rmaInfo.State))
            {
                if (string.IsNullOrEmpty(returnResult))
                    returnResult = "State is required!";
                else
                {
                    returnResult = returnResult + "\n State is required!";
                }
            }
            if (string.IsNullOrEmpty(rmaInfo.ZipCode))
            {
                if (string.IsNullOrEmpty(returnResult))
                    returnResult = "Zip code is required!";
                else
                {
                    returnResult = returnResult + "\n Zip code is required!";
                }
            }


            return returnResult;
        }
        public  string ValidateRmaStatusWithLineItemsStatuses(RmaModel rmaInfo)
        {
            string returnResult = string.Empty;
            if(rmaInfo.Status.ToLower() == "completed")
            {
                if(rmaInfo.ReceiveStatus.ToLower() == "received")
                {

                }
                else
                {
                    returnResult = "RMA status cannot be completed without receive!";
                }
            }
            return returnResult;
        }
        public RmaResponse RMAInsertUpdate(RmaModel rmaInfo)
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            RmaResponse response = rmaOperation.RMAInsertUpdate(rmaInfo);

            return response;
        }
        public  RmaModel GetRMAInfo(int rmaGUID)
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            RmaModel objRMA = rmaOperation.GetRMAInfo(rmaGUID);

            return objRMA;
        }


        public  RmaModel GetRMASearch(int companyID, string poNumber, string trackingNumber)
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            RmaModel rmaModel = rmaOperation.GetRMASearch(companyID,poNumber,trackingNumber); // new RmaModel();
            return rmaModel;
        }
        public  List<RmaDetailModel> ValidateESN(int POID, List<ESNList> esnList)
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            List<RmaDetailModel> rmaList = rmaOperation.ValidateESN(POID, esnList);// new List<RmaDetailModel>();
            return rmaList;
        }
        public  List<RmaDetailModel> POValidateESN(List<ESNList> esnList)
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            List<RmaDetailModel> rmaList = rmaOperation.POValidateESN(esnList);// new List<RmaDetailModel>();
            
            return rmaList;
        }

        

        
        public List<CustomerRMAStatus> GetCustomerRMAStatusList(int CompanyID, bool rma)
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            List<CustomerRMAStatus> customerRMAStatusList = rmaOperation.GetCustomerRMAStatusList(CompanyID, rma);// new List<CustomerRMAStatus>();
            return customerRMAStatusList;
        }
        public List<CustomerRMAStatus> GetRmaDetailStatusList()
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            List<CustomerRMAStatus> rmaStatusList = rmaOperation.GetRmaDetailStatusList();// new List<CustomerRMAStatus>();
            return rmaStatusList;
        }

        public List<RmaTriageStatus> GetTriageStatusList()
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            List<RmaTriageStatus> statusList = rmaOperation.GetTriageStatusList();// new List<RmaTriageStatus>();
            return statusList;
        }
        public List<RmaReceiveStatus> GetReceiveRMAStatusList()
        {
            SV.Framework.DAL.RMA.RmaOperation rmaOperation = SV.Framework.DAL.RMA.RmaOperation.CreateInstance<SV.Framework.DAL.RMA.RmaOperation>();

            List<RmaReceiveStatus> statusList = rmaOperation.GetReceiveRMAStatusList();// new List<RmaReceiveStatus>();
            return statusList;
        }


    }
}
