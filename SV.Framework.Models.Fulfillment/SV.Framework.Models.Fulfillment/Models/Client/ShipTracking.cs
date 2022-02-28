using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    [XmlType("shippinginfo")]
    [XmlRoot(ElementName = "shippinginfo", IsNullable = true)]
    public class ShipTracking
    {
        private string _purchaseOrderNumber;
        private DateTime _shipToDate;
        private string _shipToTackingNumber;
        private string _shipToBy;

        [XmlIgnore]
        [XmlElement(ElementName = "purchaseordernumber", IsNullable = true)]
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


        [XmlElement(ElementName = "shiptodate", DataType = "date")]
        public DateTime ShipToDate
        {
            get
            {
                return _shipToDate;
            }
            set
            {
                _shipToDate = value;
            }
        }

        [XmlElement(ElementName = "shiptoby", IsNullable = true)]
        public string ShipToBy
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToBy))
                    return string.Empty;
                else
                    return _shipToBy;
            }
            set
            {
                _shipToBy = value;
            }
        }

        [XmlElement(ElementName = "trackingnumber", IsNullable = true)]
        public string ShipToTrackingNumber
        {
            get
            {
                if (string.IsNullOrEmpty(_shipToTackingNumber))
                    return string.Empty;
                else
                    return _shipToTackingNumber;
            }
            set
            {
                _shipToTackingNumber = value;
            }
        }

    }
}
