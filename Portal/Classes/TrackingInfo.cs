using System;
using System.Xml.Serialization;

namespace avii.Classes
{
    [Serializable]
    [XmlRoot(ElementName = "TrackingInfo", IsNullable = true)]
    public class TrackingInfo
    {
        private string _purchaseOrderNumber;
        private string _trackingNumber;
        private string _salesOrderNumber;
        //private int? _statusID;
        //private int? _companyID;
        //private DateTime? _shipDate = DateTime.Now;

        [XmlElement(ElementName = "PurchaseOrderNumber", IsNullable = true)]
        public string PurchaseOrderNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_purchaseOrderNumber))
                    return string.Empty;
                else
                    return _purchaseOrderNumber;
            }
            set
            {
                _purchaseOrderNumber = value;
            }
        }
        [XmlElement(ElementName = "TrackingNumber", IsNullable = true)]
        public string ShipToTrackingNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_trackingNumber))
                    return string.Empty;
                else
                    return _trackingNumber;
            }
            set
            {
                _trackingNumber = value;
            }
        }
        [XmlElement(ElementName = "SalesOrderNumber", IsNullable = true)]
        public string SalesOrderNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_salesOrderNumber))
                    return string.Empty;
                else
                    return _salesOrderNumber;
            }
            set
            {
                _salesOrderNumber = value;
            }
        }

        //[XmlElement(ElementName = "StatusID", IsNullable = true)]
        //public int? StatusID
        //{
        //    get
        //    {
                
        //        return _statusID;
        //    }
        //    set
        //    {
        //        _statusID = value;
        //    }
        //}
        //[XmlElement(ElementName = "CompanyID", IsNullable = true)]
        //public int? CompanyID
        //{
        //    get
        //    {

        //        return _companyID;
        //    }
        //    set
        //    {
        //        _companyID = value;
        //    }
        //}
        //[XmlElement(ElementName = "ShipDate", IsNullable = true)]
        //public DateTime? ShipDate
        //{
        //    get
        //    {
        //        if (_shipDate == null)
        //            return DateTime.Now;
        //        else
        //            return _shipDate;
        //    }
        //    set
        //    {
        //        _shipDate = value;
        //    }
        //}
    }
}