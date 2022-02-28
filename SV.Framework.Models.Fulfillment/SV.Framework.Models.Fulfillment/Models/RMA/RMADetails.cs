using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.RMA
{

    [Serializable]
    [XmlRoot(ElementName = "RmaDetail", IsNullable = true)]
    public class RMADetails
    {
        private string _ESN = string.Empty;
        private RMAReason _Reason;
        private int _CallTime;
        private string _Notes = string.Empty;
        private string _Linkedpo = string.Empty;
        private int _Linkedpoline = 0;

        //private DateTime _recievedOn;
        //private DeviceState _deviceState;
        //private DeviceDesignation _deviceDesignation;
        //private DeviceCondition _deviceCondition;
        //private string _deviceDefect = string.Empty;
        //private double _reStockingFee;
        //private DateTime _reStockingDate;
        //private string _newSKU = string.Empty;
        //private string _locationCode = string.Empty;
        private Warranty _warranty;
        private DateTime _warrantyExpieryDate;
        private Disposition _disposition;
        public RMADetails()
        {
            //_deviceState = DeviceState.None;
            //_deviceDesignation = DeviceDesignation.None;
            //_deviceCondition = DeviceCondition.None;
            _disposition = Disposition.None;
            _warranty = Warranty.None;
            _Reason = RMAReason.None;


        }

        //[XmlElement(ElementName = "DeviceState", IsNullable = false)]
        //public DeviceState DeviceState
        //{
        //    get
        //    {
        //        if (_deviceState == null)
        //            return DeviceState.None;
        //        else
        //            return _deviceState;
        //    }
        //    set
        //    {
        //        _deviceState = value;
        //    }
        //}
        //[XmlElement(ElementName = "DeviceDesignation", IsNullable = false)]
        //public DeviceDesignation DeviceDesignation
        //{
        //    get
        //    {
        //        if (_deviceDesignation == null)
        //            return DeviceDesignation.None;
        //        else
        //            return _deviceDesignation;
        //    }
        //    set
        //    {
        //        _deviceDesignation = value;
        //    }
        //}
        //[XmlElement(ElementName = "DeviceCondition", IsNullable = false)]
        //public DeviceCondition DeviceCondition
        //{
        //    get
        //    {
        //        if (_deviceCondition == null)
        //            return DeviceCondition.None;
        //        else
        //            return _deviceCondition;
        //    }
        //    set
        //    {
        //        _deviceCondition = value;
        //    }
        //}

        //[XmlElement(ElementName = "DeviceDefact", IsNullable = true)]
        //public string DeviceDefect
        //{
        //    get
        //    {
        //        return _deviceDefect;
        //    }
        //    set
        //    {
        //        _deviceDefect = value;
        //    }
        //}
        //[XmlElement(ElementName = "ReStockingFee", IsNullable = false)]
        //public double ReStockingFee
        //{
        //    get
        //    {
        //        if (_reStockingFee == null)
        //            return 0;
        //        else
        //            return _reStockingFee;
        //    }
        //    set
        //    {
        //        _reStockingFee = value;
        //    }
        //}
        //[XmlElement(ElementName = "NewSKU", IsNullable = true)]
        //public string NewSKU
        //{
        //    get
        //    {
        //        return _newSKU;
        //    }
        //    set
        //    {
        //        _newSKU = value;
        //    }
        //}
        //[XmlElement(ElementName = "LocationCode", IsNullable = true)]
        //public string LocationCode
        //{
        //    get
        //    {
        //        return _locationCode;
        //    }
        //    set
        //    {
        //        _locationCode = value;
        //    }
        //}
        [XmlElement(ElementName = "Disposition", IsNullable = false)]
        public Disposition Disposition
        {
            get
            {

                if (_disposition == null)
                    return Disposition.None;
                else
                    return _disposition;
            }
            set
            {
                _disposition = value;
            }
        }
        [XmlElement(ElementName = "Warranty", IsNullable = false)]
        public Warranty Warranty
        {
            get
            {

                if (_warranty == null)
                    return Warranty.None;
                else
                    return _warranty;
            }
            set
            {
                _warranty = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "WarrantyExpiryDate", DataType = "date")]
        public DateTime WarrantyExpieryDate
        {
            get
            {
                if (_warrantyExpieryDate != null)
                    return DateTime.MinValue;
                else
                    return _warrantyExpieryDate;
            }
            set
            {
                _warrantyExpieryDate = value;
            }
        }
        //[XmlElement(ElementName = "ReStockingDate", DataType = "date")]
        //public DateTime ReStockingDate
        //{
        //    get
        //    {
        //        if (_reStockingDate != null)
        //            return DateTime.MinValue;
        //        else
        //            return _reStockingDate;
        //    }
        //    set
        //    {
        //        _reStockingDate = value;
        //    }
        //}
        //[XmlElement(ElementName = "RecievedOn", DataType = "date")]
        //public DateTime RecievedOn
        //{
        //    get
        //    {
        //        if (_reStockingDate != null)
        //            return DateTime.Now;
        //        else
        //            return _recievedOn;
        //    }
        //    set
        //    {
        //        _recievedOn = value;
        //    }
        //}
        [XmlElement(ElementName = "ESN", IsNullable = true)]
        public string ESN
        {
            get
            {
                return _ESN;
            }
            set
            {
                _ESN = value;
            }
        }
        [XmlElement(ElementName = "Reason", IsNullable = false)]
        public RMAReason Reason
        {
            get
            {

                if (_Reason == null)
                    return RMAReason.None;
                else
                    return _Reason;
            }
            set
            {
                _Reason = value;
            }
        }
        [XmlIgnore]
        [XmlElement(ElementName = "CallTime", IsNullable = false)]
        public int CallTime
        {
            get
            {
                if (_CallTime == null)
                    return 0;
                else
                    return _CallTime;
            }
            set
            {
                _CallTime = value;
            }
        }
        [XmlElement(ElementName = "Notes", IsNullable = true)]
        public string Notes
        {
            get
            {
                return _Notes;
            }
            set
            {
                _Notes = value;
            }
        }
        [XmlElement(ElementName = "LinkedPurchaseOrder", IsNullable = true)]
        public string LinkedPO
        {
            get
            {
                return _Linkedpo;
            }
            set
            {
                _Linkedpo = value;
            }
        }
        [XmlElement(ElementName = "LinkedPOLineNumber", IsNullable = false)]
        public int LinkedPOLineNumber
        {
            get
            {
                if (_Linkedpoline == null)
                    return 0;
                else
                    return _Linkedpoline;
            }
            set
            {
                _Linkedpoline = value;
            }
        }


    }
}
