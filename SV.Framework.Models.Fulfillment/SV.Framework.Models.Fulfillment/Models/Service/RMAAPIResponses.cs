using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.RMA;

namespace SV.Framework.Models.Service
{

    public class RMAResponse
    {
        private List<RMAReport> _rmaList;
        private string _errorCode;
        private string _comment;
        public RMAResponse()
        {
            _rmaList = new List<RMAReport>();
        }
        public List<RMAReport> RmaReportList
        {
            get
            {
                return _rmaList;
            }
            set
            {
                _rmaList = value;
            }
        }

        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }
    }
    public class CancelRMAResponse
    {
        public string ErrorCode { get; set; }
        public string Comment { get; set; }

    }
    public class RmaEsnListingResponse
    {
        private List<RmaEsnStatuses> rmaEsnListing;
        private string returnCode;
        private string comment;
        public RmaEsnListingResponse()
        {
            rmaEsnListing = new List<RmaEsnStatuses>();
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
        public List<RmaEsnStatuses> RmaEsnListing
        {
            get
            {
                return rmaEsnListing;
            }
            set
            {
                rmaEsnListing = value;
            }
        }
    }

    public class RmaEsnDetailResponse
    {
        private List<RMAESNDetail> rmaEsnDetail;
        private string returnCode;
        private string comment;
        public RmaEsnDetailResponse()
        {
            rmaEsnDetail = new List<RMAESNDetail>();
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
        public List<RMAESNDetail> RmaEsnDetail
        {
            get
            {
                return rmaEsnDetail;
            }
            set
            {
                rmaEsnDetail = value;
            }
        }

    }

    public class RMAAPIResponse
    {
        private string _rmaNumber;
        private string _errorCode;
        private string _comment;
        private string _rmaDate;

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

        public string ErrorCode
        {
            get
            {
                return _errorCode;
            }
            set
            {
                _errorCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return _comment;
            }
            set
            {
                _comment = value;
            }
        }
        public string RMADate
        {
            get
            {
                return _rmaDate;
            }
            set
            {
                _rmaDate = value;
            }
        }

    }
}
