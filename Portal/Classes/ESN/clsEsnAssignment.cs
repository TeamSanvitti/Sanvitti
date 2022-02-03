using System;
namespace avii.Classes
{
    [Serializable]
   public class clsEsnAssignment
    {
        private int _poid;
        private int _podid;
        private string _PONumber;
        private DateTime _po_date;
        private string _itemcode;
        private int _lineNo;
        private string _msid;
        private string _mdn;
        private string _passcode;
        private string _esn;
        private string _MslNumber;
        private string _fmupc;
        private string _upc;
        private int _categoryFlag;
        
        
        public int Po_id
        {
            get { return _poid; }
            set {
                _poid = value;
            }
        }
        public int Pod_id
        {
            get { return _podid; }
            set
            {
                _podid = value;
            }
        }
        public string PONumber
        {
            get
            {
                return _PONumber;
            }
            set
            {
                _PONumber = value;
            }
        }
        public DateTime PODate
        {
            get
            {
                return _po_date;
            }
            set
            {
                _po_date = value;
            }
        }
        public string Itemcode
        {
            get
            {
                return _itemcode;
            }
            set
            {
                _itemcode = value;
            }
        }
        public int LineNo
        {
            get
            {
                return _lineNo;
            }
            set
            {
                _lineNo = value;
            }
        }
        public string Msid
        {
            get
            {
                return _msid;
            }
            set
            {
                _msid = value;
            }
        }
        public string MDN
        {
            get
            {
                return _mdn;
            }
            set
            {
                _mdn = value;
            }
        }
        public string Passcode
        {
            get
            {
                return _passcode;
            }
            set
            {
                _passcode = value;
            }
        }
        public string esn
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
        public string MslNumber
        {
            get
            {
                return _MslNumber;
            }
            set
            {
                _MslNumber = value;
            }
        }
        public string FmUpc
        {
            get
            {
                return _fmupc;
            }
            set
            {
                _fmupc = value;
            }
        }
        public string UPC
        {
            get {
                return _upc;
            }
            set {
                _upc = value;
            }
        }
        public int CategoryFlag
        {
            get { return _categoryFlag; }
            set { _categoryFlag = value; }
        }

    }
  
  
}
