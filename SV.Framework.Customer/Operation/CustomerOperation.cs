using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Customer;
using SV.Framework.Models.Service;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Customer
{
    public class CustomerOperation : BaseCreateInstance
    {
        public CompanyInfo GetCompanyInfo(int userID)
        {
            SV.Framework.DAL.Admin.CustomerOperation customerOperation = SV.Framework.DAL.Admin.CustomerOperation.CreateInstance<SV.Framework.DAL.Admin.CustomerOperation>();
            return customerOperation.GetCompanyInfo(userID);
        }
        public UsersResponse GetAssignedUsers(int userID)
        {
            SV.Framework.DAL.Admin.CustomerOperation customerOperation = SV.Framework.DAL.Admin.CustomerOperation.CreateInstance<SV.Framework.DAL.Admin.CustomerOperation>();

            UsersResponse serviceResponse = new UsersResponse();
            try
            {
                CompanyInformation companyInfo = customerOperation.AssignedUsersDB(userID);
                if (companyInfo != null && companyInfo.CompanyName != string.Empty)
                {
                    serviceResponse.CompanyInfo = companyInfo;
                    serviceResponse.Comment = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                    serviceResponse.ReturnCode = ResponseErrorCode.SuccessfullyRetrieved.ToString();
                }
                else
                {
                    serviceResponse.CompanyInfo = null;
                    serviceResponse.Comment = ResponseErrorCode.NoRecordsFound.ToString();
                    serviceResponse.ReturnCode = ResponseErrorCode.NoRecordsFound.ToString();
                }


            }
            catch (Exception ex)
            {
                serviceResponse.CompanyInfo = null;
                serviceResponse.Comment = ex.Message;
                serviceResponse.ReturnCode = ResponseErrorCode.InternalError.ToString();
            }
            //serviceResponse = DownloadPoHeader(pos);
            return serviceResponse;
        }

        

    }
}
