using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Inventory
{
    [XmlRoot(ElementName = "esnroot", IsNullable = true)]
    public class clsEsnxml
    {
        private int? _poid = 0;
        private int? _podid = 0;
        private string _esn;
        private string _MslNumber;
        private string _fmupc;
        private string _mdn;
        private string _msid;
        private int? _ifmissing = 0;
        private int? rmaID = 0;


        [XmlElement(ElementName = "RMAGUID", IsNullable = true)]
        public int? RMAGUID
        {
            get { return rmaID; }
            set
            {
                rmaID = value;
            }
        }

        [XmlElement(ElementName = "PO_ID", IsNullable = true)]
        public int? Po_id
        {
            get { return _poid; }
            set
            {
                _poid = value;
            }
        }
        [XmlElement(ElementName = "POD_ID", IsNullable = true)]
        public int? Pod_id
        {
            get { return _podid; }
            set
            {
                _podid = value;
            }
        }
        [XmlElement(ElementName = "ESN", IsNullable = true)]
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
        [XmlElement(ElementName = "MSL", IsNullable = true)]
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
        [XmlElement(ElementName = "MDN", IsNullable = true)]
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
        [XmlElement(ElementName = "MSID", IsNullable = true)]
        public string MSID
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

        [XmlElement(ElementName = "FMUPC", IsNullable = true)]
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
        [XmlElement(ElementName = "ifmissing", IsNullable = true)]
        public int? ifMissing
        {
            get
            {
                return _ifmissing;
            }
            set
            {
                _ifmissing = value;
            }
        }

    }
}
