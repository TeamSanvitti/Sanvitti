using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Catalog
{
    [Serializable]
    [XmlRoot(ElementName = "CustomerWarehouseCode", IsNullable = true)]
    public class CustomerWarehouseCode
    {
        private int? _warehouseCodeGUID;
        private int _companyID;
        private string _warehouseCode;
        private string _companyName;
        private bool? _active;
        private string _whCodecompanyName;

        [XmlElement(ElementName = "WarehouseCodeGUID", IsNullable = true)]
        public int? WarehouseCodeGUID
        {
            get { return _warehouseCodeGUID; }
            set { _warehouseCodeGUID = value; }
        }
        [XmlIgnore]
        [XmlElement(ElementName = "CompanyID", IsNullable = true)]
        public int CompanyID
        {
            get
            {
                return _companyID;
            }
            set { _companyID = value; }

        }
        [XmlElement(ElementName = "WarehouseCode", IsNullable = true)]
        public string WarehouseCode
        {
            get { return _warehouseCode; }
            set { _warehouseCode = value; }
        }
        [XmlIgnore]

        [XmlElement(ElementName = "CompanyName", IsNullable = true)]
        public string CompanyName
        {
            get { return _companyName; }
            set { _companyName = value; }
        }
        [XmlElement(ElementName = "Active", IsNullable = true)]
        public bool? Active
        {
            get { return _active; }
            set { _active = value; }
        }
        [XmlIgnore]
        public string WHCodecompanyName
        {
            get { return _whCodecompanyName; }
            set { _whCodecompanyName = value; }
        }


    }
}
