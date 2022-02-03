using System;
using System.Collections.Generic;

namespace avii.Classes
{

    public enum EsnStatus
    {
        Pending=1,
        Assigned=2,
        Cancelled=3,
        Returned=4,
        NotAssign=5
    }

    public class clsEsn
    {
        private int esnGUID;
        private string esn;
        private string modelNumber;
        private string poNumber;
        private string msl;
        private string hex;
        private string akey;
        private string otksl;
        private string comment;
        private DateTime uploadDate;
        private DateTime modifiedDate;
        private List<clsEsnLog> esnLogList;
        public clsEsn(string PurchaseOrderNumber, string ModelNumber)
        {
            poNumber = PurchaseOrderNumber;
            modelNumber = ModelNumber;
        }

        public clsEsn(string ModelNumber)
        {
            modelNumber = ModelNumber;
        }

        public int ESNGUID
        {
            get
            {
                return esnGUID;
            }
        }
        public string PurchaseOrder
        {
            get
            {
                return poNumber;
            }
        }
        public string ESN
        {
            get
            {
                return esn;
            }
            set
            {
                esn = value;
            }
        }        
        public string MSL
        {
            get
            {
                return msl;
            }
            set
            {
                msl = value;
            }
        }
        public string Hex
        {
            get
            {
                return hex;
            }
            set
            {
                hex = value;
            }
        }
        public string Akey
        {
            get
            {
                return akey;
            }
            set
            {
                akey = value;
            }
        }                                                                         
        public string Otksl
        {
            get
            {
                return otksl;
            }
            set
            {
                otksl = value;
            }
        }
        public List<clsEsnLog> EsnLog
        {
            get
            {
                return esnLogList;
            }
            set
            {
                esnLogList = value;
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
        public DateTime UploadDate
        {
            get
            {
                return uploadDate;
            }
            set
            {
                uploadDate = value;
            }
        }
        public DateTime ModifiedDate
        {
            get
            {
                return modifiedDate;
            }
            set
            {
                modifiedDate = value;
            }
        }
    }

    public class clsEsnLog
    {
        private int esnLogGUID;
        private EsnStatus status;
        private DateTime logDate;

        public clsEsnLog()
        {
        }

        public clsEsnLog(int EnsLogGUID)
        {
            esnLogGUID = esnLogGUID;
        }
    
        public EsnStatus EsnStatus
        {
            get
            {
                return status;
            }
            set
            {
                status = value;
            }
        }

        public DateTime EsnLogDate
        {
            get {
                return logDate;
            }
            set
            {
                logDate = value;
            }

        }

    }

}
