using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Authenticate;

namespace SV.Framework.Models.Service
{
    public class EsnRepositoryRequest
    {

        private UserCredentials userCredentials;
        private DateTime fromDate = DateTime.MinValue;
        private DateTime toDate = DateTime.MinValue;

        private bool unusedOnly = false;
        private bool showAllUnusedEsn = false;

        public EsnRepositoryRequest()
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

        public bool UnusedOnly
        {
            get
            {
                return unusedOnly;
            }
            set
            {
                unusedOnly = value;
            }
        }
        public bool ShowAllUnusedEsn
        {
            get
            {
                return showAllUnusedEsn;
            }
            set
            {
                showAllUnusedEsn = value;
            }
        }

    }

}
