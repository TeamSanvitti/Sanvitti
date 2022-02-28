using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.RMA
{
    #region class RMA - Model

    [XmlRoot(ElementName = "rmastatus", IsNullable = true)]
    public class RMA_Status
    {
        private int? _RmaGUID;
        private int? _UserID;
        private int? _RmaStatusID;

        [XmlElement(ElementName = "rmaGUID", IsNullable = true)]
        public int? RmaGUID
        {
            get
            {
                return _RmaGUID;
            }
            set
            {
                _RmaGUID = value;
            }
        }
        [XmlElement(ElementName = "UserID", IsNullable = true)]
        public int? UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }
        [XmlElement(ElementName = "RmaStatusID", IsNullable = true)]
        public int? RmaStatusID
        {
            get
            {
                return _RmaStatusID;
            }
            set
            {
                _RmaStatusID = value;
            }
        }

    }

    public class RMACSV
    {
        public string RMANumber { get; set; }
        public string CustomerName { get; set; }
        public string RMADate { get; set; }
        public string TrackingNumber { get; set; }
        // public string StoreID { get; set; }
        public string RMAStatus { get; set; }
        public string ESN { get; set; }
        public string ESNStatus { get; set; }
        public string TriageNotes { get; set; }
        public string TriageStatus { get; set; }
    }
    [Serializable]
    public class RMA
    {
        private int _RmaGUID;
        private string _RmaNumber;
        private DateTime _RmaDate;
        private int _RmaStatusID;

        private int _UserID;
        private int _POGUID;
        private string _RmaContactName;
        private string _Comment;
        private int _CreatedBy;
        private DateTime _CreatedDate;
        private int _ModifiedBy;
        private DateTime _ModifiedDate;
        private string _status;
        private string _po_num;
        private List<SV.Framework.Models.Old.RMA.RMADetail> _rmaDetails;
        private RMAUserCompany _rmaUserCompany;
        private string addrress;
        private string city;
        private string state;
        private string zip;
        private string avComments;
        private string email;
        private string phone;
        private string locationCode;
        private string storeID;
        private string rmaDocument;
        private List<RmaDocuments> rmaDocuments;
        private string docComment;
        private DateTime maxShipmentDate = DateTime.MinValue;
        private string contactCountry;
        private bool doNotSendShippingLabel;
        private bool allowShippingLabel;
        private string _CustomerRMANumber;
        public RMA()
        {
            _rmaDetails = new List<SV.Framework.Models.Old.RMA.RMADetail>();
            _rmaUserCompany = new RMAUserCompany();
        }
        [XmlIgnore]
        public bool ByPO { get; set; }
        [XmlIgnore]
        public string RMASource { get; set; }
        [XmlIgnore]
        public bool AllowShippingLabel
        {
            get
            {
                return allowShippingLabel;
            }
            set
            {
                allowShippingLabel = value;
            }
        }
        [XmlIgnore]
        public bool DoNotSendShippingLabel
        {
            get
            {
                return doNotSendShippingLabel;
            }
            set
            {
                doNotSendShippingLabel = value;
            }
        }

        [XmlIgnore]
        public string ContactCountry
        {
            get
            {
                return contactCountry;
            }
            set
            {
                contactCountry = value;
            }
        }

        [XmlIgnore]
        public DateTime MaxShipmentDate
        {
            get
            {
                return maxShipmentDate;
            }
            set
            {
                maxShipmentDate = value;
            }
        }

        [XmlIgnore]
        public string DocComment
        {
            get
            {
                return docComment;
            }
            set
            {
                docComment = value;
            }
        }
        [XmlIgnore]
        public string TrackingNumber { get; set; }
        [XmlIgnore]
        public string ShipDate { get; set; }
        [XmlIgnore]
        public string ShipWeight { get; set; }
        [XmlIgnore]
        public decimal FinalPostage { get; set; }

        [XmlIgnore]
        public string CompanyLogo { get; set; }

        //[XmlIgnore]
        //public string ShipPackage { get; set; }
        [XmlIgnore]
        public string ShipComment { get; set; }
        [XmlIgnore]
        public string ShipMethod { get; set; }

        [XmlIgnore]
        public string RmaDocument
        {
            get
            {
                return rmaDocument;
            }
            set
            {
                rmaDocument = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "RMADocument", IsNullable = true)]
        public List<RmaDocuments> RmaDocumentList
        {
            get
            {
                return rmaDocuments;
            }
            set
            {
                rmaDocuments = value;
            }
        }

        [XmlIgnore]
        public RMAUserCompany RMAUserCompany
        {
            get { return _rmaUserCompany; }
            set { _rmaUserCompany = value; }
        }

        public List<SV.Framework.Models.Old.RMA.RMADetail> RmaDetails
        {
            get
            {
                return _rmaDetails;
            }
            set
            {
                _rmaDetails = value;
            }
        }

        [XmlIgnore]
        public int RmaGUID
        {
            get
            {
                return _RmaGUID;
            }
            set
            {
                _RmaGUID = value;
            }
        }

        public string RmaNumber
        {
            get
            {
                return _RmaNumber;
            }
            set
            {
                _RmaNumber = value;
            }
        }
        public string CustomerRMANumber
        {
            get
            {
                return _CustomerRMANumber;
            }
            set
            {
                _CustomerRMANumber = value;
            }
        }


        public string StoreID
        {
            get
            {
                return storeID;
            }
            set
            {
                storeID = value;
            }
        }

        public DateTime RmaDate
        {
            get
            {
                return _RmaDate;
            }
            set
            {
                _RmaDate = value;
            }
        }

        [XmlIgnore]
        public int RmaStatusID
        {
            get
            {
                return _RmaStatusID;
            }
            set
            {
                _RmaStatusID = value;
            }
        }

        [XmlIgnore]
        public int UserID
        {
            get
            {
                return _UserID;
            }
            set
            {
                _UserID = value;
            }
        }

        [XmlIgnore]
        public int POGUID
        {
            get
            {
                return _POGUID;
            }
            set
            {
                _POGUID = value;
            }
        }

        public string RmaContactName
        {
            get
            {
                return _RmaContactName;
            }
            set
            {
                _RmaContactName = value;
            }
        }


        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }

        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
            }
        }

        public string Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                _Comment = value;
            }
        }

        [XmlIgnore]
        public string AVComments
        {
            get
            {
                return avComments;
            }
            set
            {
                avComments = value;
            }
        }


        public int CreatedBy
        {
            get
            {
                return _CreatedBy;
            }
            set
            {
                _CreatedBy = value;
            }
        }


        public DateTime CreatedDate
        {
            get
            {
                return _CreatedDate;
            }
            set
            {
                _CreatedDate = value;
            }
        }

        [XmlIgnore]
        public int ModifiedBy
        {
            get
            {
                return _ModifiedBy;
            }
            set
            {
                _ModifiedBy = value;
            }
        }

        [XmlIgnore]
        public DateTime ModifiedDate
        {
            get
            {
                return _ModifiedDate;
            }
            set
            {
                _ModifiedDate = value;
            }
        }

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


        public string PoNum
        {
            get
            {
                return _po_num;
            }
            set
            {
                _po_num = value;
            }
        }

        [XmlIgnore]
        public string LocationCode
        {
            get
            {
                return locationCode;
            }
            set
            {
                locationCode = value;
            }
        }


        public string Address
        {
            get { return addrress; }
            set { addrress = value; }
        }


        public string City
        {
            get { return city; }
            set { city = value; }
        }


        public string State
        {
            get { return state; }
            set { state = value; }
        }



        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }
    }

    #endregion RMA

    [Serializable]
    public class RMAEsnLookUp
    {
        private string _ESN;
        private string _itemCode;
        private string _upc;
        private int _rmaDetGUID;
        private string _Reason;
        private int _CallTime;
        private int _StatusID;
        private string _Notes;
        private bool _WaferSealed;
        private int _poguid;
        private int _poStatusId;
        private int _podid;
        private string avso = string.Empty;

        public string AVSalesOrderNumber
        {
            get
            {
                return avso;
            }
            set
            {
                avso = value;
            }
        }

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
        public string itemCode
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
        public int rmaDetGUID
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
        public string Reason
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
        public int CallTime
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
        public int StatusID
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
        public bool WaferSealed
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
        public int Po_Id
        {
            get
            {
                return _poguid;
            }
            set
            {
                _poguid = value;
            }

        }
        public int Pod_Id
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

    }

    
    //public class RMAAPIRequest
    //{
    //    private RMANew _rma;
    //    private SV.Framework.Models.Fulfillment.clsAuthentication _auth;

    //    public RMAAPIRequest()
    //    {
    //        _rma = new RMANew();
    //        _auth = new SV.Framework.Models.Fulfillment.clsAuthentication();
    //    }


    //    public RMANew RMA
    //    {
    //        get
    //        {
    //            return _rma;
    //        }
    //        set
    //        {
    //            _rma = value;
    //        }
    //    }

    //    public SV.Framework.Models.Fulfillment.clsAuthentication Authentication
    //    {
    //        get
    //        {
    //            return _auth;
    //        }
    //        set
    //        {
    //            _auth = value;
    //        }
    //    }
    //}
}
