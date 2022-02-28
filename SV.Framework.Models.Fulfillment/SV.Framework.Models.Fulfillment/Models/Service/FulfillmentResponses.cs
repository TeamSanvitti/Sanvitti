using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.RMA;
using SV.Framework.Models.Customer;

namespace SV.Framework.Models.Service
{
    //public class PurchaseOrderResponse
    //{
    //    private string _purchaseOrderNumber;
    //    private string _errorCode;
    //    private string _comment;

    //    public string PurchaseOrderNumber
    //    {
    //        get
    //        {
    //            return _purchaseOrderNumber;
    //        }
    //        set
    //        {
    //            _purchaseOrderNumber = value;
    //        }
    //    }

    //    public string ErrorCode
    //    {
    //        get
    //        {
    //            return _errorCode;
    //        }
    //        set
    //        {
    //            _errorCode = value;
    //        }
    //    }

    //    public string Comment
    //    {
    //        get
    //        {
    //            return _comment;
    //        }
    //        set
    //        {
    //            _comment = value;
    //        }
    //    }
    //}

    public class PurchaseOrderInfoResponse
    {
        private clsPurchaseOrder _purchaseOrder;
        private string _errorCode;
        private string _comment;

        public clsPurchaseOrder PurchaseOrder
        {
            get
            {
                return _purchaseOrder;
            }
            set
            {
                _purchaseOrder = value;
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
    public class FulfillmentResponse
    {

        private string returnCode;
        private string comment;
        public FulfillmentResponse()
        {

        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
    }

    public class CancelPurchaseOrderResponse
    {
        private string _errorCode;
        private string _comment;

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
    public class PurchaseOrderShipmentAPIResponse
    {
        private List<PurchaseOrderShipping> shipments;
        private string returnCode;
        private string comment;
        public PurchaseOrderShipmentAPIResponse()
        {
            shipments = new List<PurchaseOrderShipping>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<PurchaseOrderShipping> Shipments
        {
            get
            {
                return shipments;
            }
            set
            {
                shipments = value;
            }
        }
    }


    public class PurchaseOrderShippingResponse
    {
        private List<PurchaseOrderShipment> shipments;
        private string returnCode;
        private string comment;
        public PurchaseOrderShippingResponse()
        {
            shipments = new List<PurchaseOrderShipment>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<PurchaseOrderShipment> Shipments
        {
            get
            {
                return shipments;
            }
            set
            {
                shipments = value;
            }
        }
    }

    public class SetPurchaseOrderShippingResponse
    {
        private string returnCode;
        private string comment;

        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }

    }

    
    public class EsnRepositoryResponse
    {
        private List<EsnRepository> esnRepository;
        private string returnCode;
        private string comment;
        public EsnRepositoryResponse()
        {
            esnRepository = new List<EsnRepository>();
        }
        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        public List<EsnRepository> EsnList
        {
            get
            {
                return esnRepository;
            }
            set
            {
                esnRepository = value;
            }
        }
    }

    public class ShippByResponse
    {
        private List<ShipBy> _shipBy;
        private string _errorCode;
        private string _comment;
        public ShippByResponse()
        {
            _shipBy = new List<ShipBy>();
        }
        public List<ShipBy> ShipByList
        {
            get
            {
                return _shipBy;
            }
            set
            {
                _shipBy = value;
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

    public class InventoryResponse
    {
        private List<InventorySKU> _inventoryList;
        private string _errorCode;
        private string _comment;

        public InventoryResponse()
        {
            _inventoryList = new List<InventorySKU>();
        }

        public List<InventorySKU> InventoryList
        {
            get
            {
                return _inventoryList;
            }
            set
            {
                _inventoryList = value;
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

    public class CompanyStoreResponse
    {
        private CompanyInfo _companyInfo;
        private string _errorCode;
        private string _comment;

        public CompanyStoreResponse()
        {
            _companyInfo = new CompanyInfo();
        }

        public CompanyInfo CompanyInformation
        {
            get
            {
                return _companyInfo;
            }
            set
            {
                _companyInfo = value;
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

    //public class FulfillmentReportResponse
    //{
    //    private List<FulfillmentOrdersProvisional> fulfillmentOrderList;
    //    private string returnCode;
    //    private string comment;
    //    public FulfillmentReportResponse()
    //    {
    //        fulfillmentOrderList = new List<FulfillmentOrdersProvisional>();
    //    }
    //    public string ReturnCode
    //    {
    //        get
    //        {
    //            return returnCode;
    //        }
    //        set
    //        {
    //            returnCode = value;
    //        }
    //    }

    //    public string Comment
    //    {
    //        get
    //        {
    //            return comment;
    //        }
    //        set
    //        {
    //            comment = value;
    //        }
    //    }
    //    public List<FulfillmentOrdersProvisional> FulfillmentOrderList
    //    {
    //        get { return fulfillmentOrderList; }
    //        set { fulfillmentOrderList = value; }
    //    }
    //}

    

}
