using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;



namespace avii.Classes
{
    #region class RMA - Model

    //[Serializable]
    //public enum RMAResponseErrorCode
    //{
    //    MissingParameter,
    //    UploadedSuccessfully,
    //    ErrowWhileLoadingData
        
    //}
    
    public class RMAResponse
    {
        public int RMACount { get; set; }
        public string RMANumber { get; set; }
        public string ErrorCode { get; set; }

    }

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

    [Serializable]
    [XmlRoot(ElementName = "RMA", IsNullable = true)]
    public class RMA
    {
        private int? _RmaGUID;
        private string _RmaNumber;
        private DateTime? _RmaDate;
        private int? _RmaStatusID;
        
        private int? _UserID;
        private int _POGUID;
        private string _RmaContactName;
        private string _Comment;
        private int? _CreatedBy;  
        private DateTime _CreatedDate;
        private int? _ModifiedBy;
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
        private string rmaDocument;

        public RMA()
        {
            _rmaDetails = new List<RMADetail>();
            _rmaUserCompany = new RMAUserCompany();
        }
        [XmlIgnore]
        public RMAUserCompany RMAUserCompany
        {
            get { return _rmaUserCompany; }
            set { _rmaUserCompany = value; }
        }

        
        [XmlElement(ElementName = "RMAGUID", IsNullable = true)]
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
        [XmlElement(ElementName = "RmaNumber", IsNullable = true)]
        public string RmaNumber {
            get {
                return _RmaNumber;
            }
            set {
                _RmaNumber = value;
            }
        }
        [XmlElement(ElementName = "RmaDate", IsNullable = true)]
        public DateTime? RmaDate
        {
            get {
                return _RmaDate;
            }
            set {
                _RmaDate = value;
            }
        }
        [XmlElement(ElementName = "RmaStatusID", IsNullable = true)]
        public int? RmaStatusID {
            get {
                return _RmaStatusID;
            }
            set {
                _RmaStatusID = value;
            }
        }
        [XmlElement(ElementName = "UserID", IsNullable = true)]
        public int? UserID {
            get {
                return _UserID;
            }
            set {
                _UserID = value;
            }
        }
        [XmlIgnore]
        public int POGUID {
            get {
                return _POGUID;
            }
            set {
                _POGUID = value;
            }
        }
        [XmlElement(ElementName = "RmaContactName", IsNullable = true)]
        public string RmaContactName {
            get {
                return _RmaContactName;
            }
            set {
                _RmaContactName = value;
            }
        }

        [XmlElement(ElementName = "Email", IsNullable = true)]
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
        [XmlElement(ElementName = "Phone", IsNullable = true)]
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
        [XmlElement(ElementName = "Comment", IsNullable = true)]
        public string Comment {
            get {
                return _Comment;
            }
            set {
                _Comment = value;
            }
        }
        [XmlElement(ElementName = "AVComments", IsNullable = true)]
        public string AVComments {
            get {
                return avComments;
            }
            set {
                avComments = value;
            }
        }
        [XmlElement(ElementName = "CreatedBy", IsNullable = true)]
        public int? CreatedBy {
            get {
                return _CreatedBy;
            }
            set {
                _CreatedBy = value;
            }
        }
        [XmlIgnore]
        public DateTime CreatedDate {
            get {
                return _CreatedDate;
            }
            set {
                _CreatedDate = value;
            }
        }
        [XmlElement(ElementName = "ModifiedBy", IsNullable = true)]
        public int? ModifiedBy {
            get {
                return _ModifiedBy;
            }
            set {
                _ModifiedBy = value;
            }
        }
        [XmlIgnore]
        public DateTime ModifiedDate {
            get {
                return _ModifiedDate;
            }
            set{
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
        [XmlIgnore]
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
        [XmlElement(ElementName = "RmaDocument", IsNullable = true)]
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
        

         [XmlElement(ElementName = "Address", IsNullable = true)]
        public string Address
        {
            get { return addrress; }
            set { addrress = value; }
        }

         [XmlElement(ElementName = "City", IsNullable = true)]
        public string City
        {
            get { return city; }
            set { city = value; }
        }

         [XmlElement(ElementName = "State", IsNullable = true)]
        public string State
        {
            get { return state; }
            set { state = value; }
        }


        [XmlElement(ElementName = "Zip", IsNullable = true)]
        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }
        [XmlElement(ElementName = "RMADetail", IsNullable = true)]
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
            set {
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

            get {

                return _poStatusId;
            }
            set {
                _poStatusId = value;
            }
        
        }
        
    }

    #region class RMADetail - Model
    [Serializable]
    [XmlRoot(ElementName = "rmadetail", IsNullable = true)]
    public class RMADetail { 
        private int? _rmaDetGUID;
        private int? _rmaGUID;
        private string _ESN;
        private string _Reason;
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
        private bool allowRMA;
        private bool allowDuplicate;
        private string _validation;
        private DateTime? _recievedOn;
        private int? _deviceState;
        private int? _deviceDesignation;
        private int? _deviceCondition;
        private string _deviceDefact;
        private double? _reStockingFee;
        private DateTime? _reStockingDate;
        private string _newSKU;
        private string _locationCode;
        private int? _warranty;
        private DateTime? _warrantyExpieryDate;
        private int? _disposition;

        [XmlElement(ElementName = "DeviceState", IsNullable = true)]
        public int? DeviceState
        {
            get
            {
                return _deviceState;
            }
            set
            {
                _deviceState = value;
            }
        }
        [XmlElement(ElementName = "DeviceDesignation", IsNullable = true)]
        public int? DeviceDesignation
        {
            get
            {
                return _deviceDesignation;
            }
            set
            {
                _deviceDesignation = value;
            }
        }
        [XmlElement(ElementName = "DeviceCondition", IsNullable = true)]
        public int? DeviceCondition
        {
            get
            {
                return _deviceCondition;
            }
            set
            {
                _deviceCondition = value;
            }
        }
        [XmlElement(ElementName = "DeviceDefact", IsNullable = true)]
        public string DeviceDefact
        {
            get
            {
                return _deviceDefact;
            }
            set
            {
                _deviceDefact = value;
            }
        }
        [XmlElement(ElementName = "ReStockingFee", IsNullable = true)]
        public double? ReStockingFee
        {
            get
            {
                return _reStockingFee;
            }
            set
            {
                _reStockingFee = value;
            }
        }
        [XmlElement(ElementName = "NewSKU", IsNullable = true)]
        public string NewSKU
        {
            get
            {
                return _newSKU;
            }
            set
            {
                _newSKU = value;
            }
        }

        [XmlElement(ElementName = "LocationCode", IsNullable = true)]
        public string LocationCode
        {
            get
            {
                return _locationCode;
            }
            set
            {
                _locationCode = value;
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
        [XmlElement(ElementName = "WarrantyExpieryDate", IsNullable = true)]
        public DateTime? WarrantyExpieryDate
        {
            get
            {
                return _warrantyExpieryDate;
            }
            set
            {
                _warrantyExpieryDate = value;
            }
        }
        [XmlElement(ElementName = "ReStockingDate", IsNullable = true)]
        public DateTime? ReStockingDate
        {
            get
            {
                return _reStockingDate;
            }
            set
            {
                _reStockingDate = value;
            }
        }
        [XmlIgnore]
        public string Validation
        {

            get
            {

                return _validation;
            }
            set
            {
                _validation = value;
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
        

        [XmlElement(ElementName = "rmaDetGUID", IsNullable = true)]
        public int? rmaDetGUID {
            get {
                return _rmaDetGUID;
            }
            set {
                _rmaDetGUID = value;
            }
        }
        [XmlElement(ElementName = "RecievedOn", IsNullable = true)]
        public DateTime? RecievedOn
        {
            get
            {
                return _recievedOn;
            }
            set
            {
                _recievedOn = value;
            }
        }
        [XmlElement(ElementName = "rmaGUID", IsNullable = true)]
        [XmlIgnore]
        public int? rmaGUID {
            get {
                return _rmaGUID;
            }
            set {
                _rmaGUID = value;
            }
        }

        [XmlElement(ElementName = "AVSalesOrderNumber", IsNullable = true)]
        [XmlIgnore]
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
        [XmlIgnore]
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
        public string ESN {
            get {
                return _ESN;
            }
            set {
                _ESN = value;
            }
        }
        [XmlElement(ElementName = "Reason", IsNullable = true)]
        public string Reason {
            get {
                return _Reason;
            }
            set {
                _Reason = value;
            }
        }
        [XmlElement(ElementName = "CallTime", IsNullable = true)]
        public int? CallTime {
            get {
                return _CallTime;
            }
            set {
                _CallTime = value;
            }
        }
        [XmlElement(ElementName = "StatusID", IsNullable = true)]
        public int? StatusID {
            get {
                return _StatusID;
            }
            set{
                _StatusID = value;
            }
        }
        [XmlElement(ElementName = "Notes", IsNullable = true)]
        public string Notes {
            get {
                return _Notes;
            }
            set {
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
        [XmlIgnore]
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
        [XmlElement(ElementName = "Po_id", IsNullable = true)]
        [XmlIgnore]
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
        [XmlElement(ElementName = "Pod_id", IsNullable = true)]
        [XmlIgnore]
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
        
    }
    #endregion RMADetail
   
    
}
