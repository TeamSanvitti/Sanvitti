using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.RMA;
using SV.Framework.Models.Common;
using SV.Framework.Models.Fulfillment;

namespace SV.Framework.Models.Service
{

    public class RMASearchCriteria
    {
        private string _rmaNumber;
        private RMAStatus _rmaStatus;
        private string _esn;

        private string _returnReason;
        private DateTime _rma_from_date;// = DateTime.Today.AddYears(-1);
        private DateTime _rma_to_date;// = DateTime.Today;

        private clsAuthentication _auth;
        public RMASearchCriteria()
        {
            _auth = new clsAuthentication();
            _rmaStatus = RMAStatus.None;
        }
        public string RMANumber
        {
            get
            {
                return _rmaNumber;
            }
            set
            {
                _rmaNumber = value;
            }
        }
        public RMAStatus RMAStatus
        {
            get
            {
                return _rmaStatus;
            }
            set
            {
                _rmaStatus = value;
            }
        }
        public string ESN
        {
            get
            {
                return _esn;
            }
            set
            {
                _esn = value;
            }
        }
        public string ReturnReason
        {
            get
            {
                return _returnReason;
            }
            set
            {
                _returnReason = value;
            }
        }
        public DateTime RMA_To_Date
        {
            get
            {
                return _rma_to_date;
            }
            set
            {
                _rma_to_date = value;
            }
        }
        public DateTime RMA_From_Date
        {
            get
            {
                return _rma_from_date;
            }
            set
            {
                _rma_from_date = value;
            }
        }

        public clsAuthentication Authentication
        {
            get
            {
                return _auth;
            }
            set
            {
                _auth = value;
            }
        }
    }
    public class RMARequests
    {
        private string _rmaNumber;

        private clsAuthentication _auth;
        public RMARequests()
        {
            _auth = new clsAuthentication();

        }
        public string RMANumber
        {
            get
            {
                return _rmaNumber;
            }
            set
            {
                _rmaNumber = value;
            }
        }


        public clsAuthentication Authentication
        {
            get
            {
                return _auth;
            }
            set
            {
                _auth = value;
            }
        }
    }

    public class RMAAPIRequest
    {
        private RMANew _rma;
        private clsAuthentication _auth;

        public RMAAPIRequest()
        {
            _rma = new RMANew();
            _auth = new clsAuthentication();
        }


        public RMANew RMA
        {
            get
            {
                return _rma;
            }
            set
            {
                _rma = value;
            }
        }

        public clsAuthentication Authentication
        {
            get
            {
                return _auth;
            }
            set
            {
                _auth = value;
            }
        }
    }

}
