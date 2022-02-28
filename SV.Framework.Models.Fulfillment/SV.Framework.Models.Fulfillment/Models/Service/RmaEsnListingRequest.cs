using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Authenticate;
using SV.Framework.Models.Common;

namespace SV.Framework.Models.Service
{

    public class RmaEsnListingRequest
    {
        private UserCredentials userCredentials;
        private DateTime fromDate = DateTime.MinValue;
        private DateTime toDate = DateTime.MinValue;
        private RMAStatus esnStatus = RMAStatus.None;
        private RMAStatus rmaStatus = RMAStatus.None;

        public RmaEsnListingRequest()
        {
            userCredentials = new UserCredentials();
        }

        public UserCredentials UserCredentials
        {
            get
            {
                return userCredentials;
            }
            set
            {
                userCredentials = value;
            }
        }


        public DateTime FromDate
        {
            get
            {
                return fromDate;
            }
            set
            {
                fromDate = value;
            }
        }
        public DateTime ToDate
        {
            get
            {
                return toDate;
            }
            set
            {
                toDate = value;
            }
        }
        public RMAStatus EsnStatus
        {
            get
            {
                return esnStatus;
            }
            set
            {
                esnStatus = value;
            }
        }
        public RMAStatus RmaStatus
        {
            get
            {
                return rmaStatus;
            }
            set
            {
                rmaStatus = value;
            }
        }

    }
}
