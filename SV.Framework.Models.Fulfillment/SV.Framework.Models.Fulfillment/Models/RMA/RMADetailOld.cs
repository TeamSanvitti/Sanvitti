using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Old.RMA
{
    #region class RMADetail - Model
    [Serializable]
    [XmlRoot(ElementName = "rmadetail", IsNullable = true)]
    public class RMADetail
    {
        private int? _rmaDetGUID = 0;
        private int? _rmaGUID;
        private string _ESN;
        private int? _Reason;
        private int? _CallTime;
        private int? _StatusID;
        private string _Notes;
        private int? _WaferSealed;
        private string _itemCode;
        private string _upc;
        private string _status;
        private int? _poid;
        private int? _podid;
        private int _poStatusId;
        private string avSalesOrderNumber;
        private string poNum;
        private int? _warranty;
        private int? _disposition;
        private bool allowDuplicate;
        private bool allowRMA;
        private DateTime warrantyDate;
        private int repairEstId;
        private string _itemDesc;
        private int? triageStatusID;
        private string triageNotes;

        private string newSKU;
        private string newESN;
        private string shippingTrackingNumber;

        private DateTime createDate;

        private int accessoryID;
        private List<SV.Framework.Models.RMA.RMAAccessory> rmaAccessoryList;

        private int? replacementSKUID = 0;



        public int? ReplacementSKUID
        {
            get
            {
                return replacementSKUID;
            }
            set
            {
                replacementSKUID = value;
            }

        }
        public List<SV.Framework.Models.RMA.RMAAccessory> RMAAccessoryList
        {
            get
            {
                return rmaAccessoryList;
            }
            set
            {
                rmaAccessoryList = value;
            }
        }
        [XmlIgnore]
        public int AccessoryID
        {
            get
            {
                return accessoryID;
            }
            set
            {
                accessoryID = value;
            }

        }
        [XmlIgnore]
        public DateTime CreateDate
        {
            get
            {
                return createDate;
            }
            set
            {
                createDate = value;
            }

        }

        //[XmlIgnore]
        public string ShippingTrackingNumber
        {
            get
            {
                return shippingTrackingNumber;
            }
            set
            {
                shippingTrackingNumber = value;
            }

        }

        //[XmlIgnore]
        public string NewESN
        {
            get
            {
                return newESN;
            }
            set
            {
                newESN = value;
            }

        }

        //[XmlIgnore]
        public string NewSKU
        {
            get
            {
                return newSKU;
            }
            set
            {
                newSKU = value;
            }

        }

        //private string triageStatus;

        //[XmlIgnore]
        //public string TriageStatus
        //{
        //    get
        //    {
        //        return triageStatus;
        //    }
        //    set
        //    {
        //        triageStatus = value;
        //    }

        //}

        //[XmlIgnore]
        public string TriageNotes
        {
            get
            {
                return triageNotes;
            }
            set
            {
                triageNotes = value;
            }

        }


        //[XmlIgnore]
        public int? TriageStatusID
        {
            get
            {
                return triageStatusID;
            }
            set
            {
                triageStatusID = value;
            }
        }

        [XmlIgnore]
        public string ItemDescription
        {

            get
            {

                return _itemDesc;
            }
            set
            {
                _itemDesc = value;
            }

        }


        [XmlIgnore]
        public int RepairEstId
        {

            get
            {

                return repairEstId;
            }
            set
            {
                repairEstId = value;
            }

        }

        [XmlIgnore]
        public bool AllowRMA
        {

            get
            {

                return allowRMA;
            }
            set
            {
                allowRMA = value;
            }

        }

        [XmlIgnore]
        public DateTime WarrantyExpirationDate
        {

            get
            {

                return warrantyDate;
            }
            set
            {
                warrantyDate = value;
            }

        }

        [XmlIgnore]
        public bool AllowDuplicate
        {

            get
            {

                return allowDuplicate;
            }
            set
            {
                allowDuplicate = value;
            }

        }

        [XmlIgnore]
        public int PoStatusId
        {

            get
            {

                return _poStatusId;
            }
            set
            {
                _poStatusId = value;
            }

        }

        //[XmlIgnore]
        [XmlElement(ElementName = "rmaDetGUID", IsNullable = true)]
        public int? rmaDetGUID
        {
            get
            {
                return _rmaDetGUID;
            }
            set
            {
                _rmaDetGUID = value;
            }
        }


        [XmlElement(ElementName = "rmaGUID", IsNullable = true)]
        [XmlIgnore]
        public int? rmaGUID
        {
            get
            {
                return _rmaGUID;
            }
            set
            {
                _rmaGUID = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "AVSalesOrderNumber", IsNullable = true)]
        public string AVSalesOrderNumber
        {
            get
            {
                return avSalesOrderNumber;
            }
            set
            {
                avSalesOrderNumber = value;
            }
        }

        [XmlElement(ElementName = "PurchaseOrderNumber", IsNullable = true)]
        public string PurchaseOrderNumber
        {
            get
            {
                return poNum;
            }
            set
            {
                poNum = value;
            }
        }

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
        [XmlElement(ElementName = "Reason", IsNullable = true)]
        public int? Reason
        {
            get
            {
                return _Reason;
            }
            set
            {
                _Reason = value;
            }
        }
        [XmlElement(ElementName = "CallTime", IsNullable = true)]
        public int? CallTime
        {
            get
            {
                return _CallTime;
            }
            set
            {
                _CallTime = value;
            }
        }

        //[XmlIgnore]
        [XmlElement(ElementName = "StatusID", IsNullable = true)]
        public int? StatusID
        {
            get
            {
                return _StatusID;
            }
            set
            {
                _StatusID = value;
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
        [XmlElement(ElementName = "WaferSealed", IsNullable = true)]
        [XmlIgnore]
        public int? WaferSealed
        {
            get
            {
                return _WaferSealed;
            }
            set
            {
                _WaferSealed = value;
            }
        }

        [XmlElement(ElementName = "ItemCode", IsNullable = true)]
        public string ItemCode
        {
            get
            {
                return _itemCode;
            }
            set
            {
                _itemCode = value;
            }
        }
        [XmlElement(ElementName = "Maker", IsNullable = true)]
        [XmlIgnore]
        public string UPC
        {
            get
            {
                return _upc;
            }
            set
            {
                _upc = value;
            }
        }
        [XmlElement(ElementName = "Status", IsNullable = true)]
        [XmlIgnore]
        public string Status
        {
            get
            {
                return _status;
            }
            set
            {
                _status = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "Po_id", IsNullable = true)]
        public int? Po_id
        {
            get
            {
                return _poid;
            }
            set
            {
                _poid = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "Pod_id", IsNullable = true)]
        public int? Pod_id
        {
            get
            {
                return _podid;
            }
            set
            {
                _podid = value;
            }
        }
        [XmlElement(ElementName = "Warranty", IsNullable = true)]
        public int? Warranty
        {
            get
            {
                return _warranty;
            }
            set
            {
                _warranty = value;
            }
        }
        [XmlElement(ElementName = "Disposition", IsNullable = true)]
        public int? Disposition
        {
            get
            {
                return _disposition;
            }
            set
            {
                _disposition = value;
            }
        }

    }
    #endregion RMADetail

}
