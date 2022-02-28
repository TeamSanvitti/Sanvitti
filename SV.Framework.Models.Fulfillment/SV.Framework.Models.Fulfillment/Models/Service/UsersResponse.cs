using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Customer;

namespace SV.Framework.Models.Service
{
    public class UsersResponse
    {
        private CompanyInformation companyInfo;
        private string returnCode;
        private string comment;
        public UsersResponse()
        {
            companyInfo = new CompanyInformation();
        }
        public CompanyInformation CompanyInfo
        {
            get
            {
                return companyInfo;
            }
            set
            {
                companyInfo = value;
            }
        }

        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
    }


}
