using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;



namespace avii.Classes
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
        private List<RMADetail> _rmaDetails;
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
            _rmaDetails = new List<RMADetail>();
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

        public List<RMADetail> RmaDetails
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
    public class RMAUserCompany
    {
        private int _companyID;
        private string _companyName;
        private string _email;
        private int _userid;

        public int UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
            }

        }
        public int CompanyID
        {
            get
            {
                return _companyID;
            }
            set
            {
                _companyID = value;
            }
        }
        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

    }
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
        private List<RMAAccessory> rmaAccessoryList;

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
        public List<RMAAccessory> RMAAccessoryList
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

    public class RMAAPIRequest
    {
        private RMANew _rma;
        private clsAuthentication _auth;

        public RMAAPIRequest()
        {
            _rma = new RMANew();
            _auth = new clsAuthentication();
        }


        public RMANew RMA
        {
            get
            {
                return _rma;
            }
            set
            {
                _rma = value;
            }
        }

        public clsAuthentication Authentication
        {
            get
            {
                return _auth;
            }
            set
            {
                _auth = value;
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

    #region Enums

    [Serializable]
    public enum RMAReason
    {

        None = 0,
        DOA = 1,
        AudioIssues = 2,
        ScreenIssues = 3,
        PowerIssues = 4,
        Others = 5,
        MissingParts = 6,
        ReturnToStock = 7,
        BuyerRemorse = 8,
        PhysicalAbuse = 9,
        LiquidDamage = 10,
        DropCalls = 11,
        Software = 12,
        ActivationIssues = 13,
        CoverageIssues = 14,
        LoanerProgram = 15,
        ShippingError = 16,
        HardwareIssues = 17
    }

    //[Serializable]
    //public enum DeviceCondition
    //{
    //    None = 0,
    //    New = 1,
    //    Used = 2,
    //    Damaged = 3
    //}

    [Serializable]
    public enum Warranty
    {
        None = 0,
        Warranty = 1,
        OutOfWarranty = 2
    }

    [Serializable]
    public enum Disposition
    {
        None = 0,
        Credit = 1,
        Replaced = 2,
        Repair = 3,
        Discarded = 4
    }



    //[Serializable]
    //public enum DeviceState
    //{
    //    None = 0,
    //    Complete = 1,
    //    InComplete = 2
    //}

    //[Serializable]
    //public enum DeviceDesignation
    //{
    //    None = 0,
    //    RTS = 1,
    //    RTM = 2,
    //    Other = 3
    //}

    #endregion




}
public class RMAResponse
{
    private List<avii.Classes.RMAReport> _rmaList;
    private string _errorCode;
    private string _comment;
    public RMAResponse()
    {
        _rmaList = new List<avii.Classes.RMAReport>();
    }
    public List<avii.Classes.RMAReport> RmaReportList
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
