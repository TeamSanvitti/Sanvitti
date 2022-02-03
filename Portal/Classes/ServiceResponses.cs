using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{

    public class InventoryEsnResponse
    {
        //private List<EsnInfo> esnInfoList;
        private string returnCode;
        private string comment;
        public InventoryEsnResponse()
        {
            //esnInfoList = new List<EsnInfo>();
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
        //public List<EsnInfo> EsnInfoList
        //{
        //    get { return esnInfoList; }
        //    set { esnInfoList = value; }
        //}
    }

    public class RmaEsnListingResponse
    {
        private List<avii.Classes.RmaEsnStatuses> rmaEsnListing;
        private string returnCode;
        private string comment;
        public RmaEsnListingResponse()
        {
            rmaEsnListing = new List<RmaEsnStatuses>();
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
        public List<avii.Classes.RmaEsnStatuses> RmaEsnListing
        {
            get
            {
                return rmaEsnListing;
            }
            set
            {
                rmaEsnListing = value;
            }
        }
    }

    public class EsnRepositoryDetailResponse
    {
        private List<avii.Classes.EsnRepositoryDetail> esnDetail;
        private string returnCode;
        private string comment;
        public EsnRepositoryDetailResponse()
        {
            esnDetail = new List<EsnRepositoryDetail>();
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
        public List<avii.Classes.EsnRepositoryDetail> ESNDetail
        {
            get
            {
                return esnDetail;
            }
            set
            {
                esnDetail = value;
            }
        }
    }
    public class BadESNResponse
    {
        private List<avii.Classes.EsnList> esnList;
        private string returnCode;
        private string comment;
        public BadESNResponse()
        {
            esnList = new List<EsnList>();
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
        public List<avii.Classes.EsnList> ESNList
        {
            get
            {
                return esnList;
            }
            set
            {
                esnList = value;
            }
        }
    }

    public class ReassignSKUResponse
    {
        private List<avii.Classes.ReassignSKU> reassignSKUList;
        private string returnCode;
        private string comment;
        public ReassignSKUResponse()
        {
            reassignSKUList = new List<ReassignSKU>();
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
        public List<avii.Classes.ReassignSKU> ReassignSKUList
        {
            get
            {
                return reassignSKUList;
            }
            set
            {
                reassignSKUList = value;
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

    public class EsnInventoryResponse
    {
        private List<avii.Classes.SkusEsnList> esnInventory;
        private string returnCode;
        private string comment;
        public EsnInventoryResponse()
        {
            esnInventory = new List<SkusEsnList>();
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
        public List<avii.Classes.SkusEsnList> EsnInventory
        {
            get
            {
                return esnInventory;
            }
            set
            {
                esnInventory = value;
            }
        }
    }
    public class CustomerShipViaResponse
    {
        private avii.Classes.Customers customers;
        private string returnCode;
        private string comment;
        public CustomerShipViaResponse()
        {
            customers = new Customers();
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
        public avii.Classes.Customers Customer
        {
            get
            {
                return customers;
            }
            set
            {
                customers = value;
            }
        }
    }

    public class EsnRepositoryResponse
    {
        private List<avii.Classes.EsnRepository> esnRepository;
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
        public List<avii.Classes.EsnRepository> EsnList
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

    public class FulfillmentTrackingResponse
    {
        private List<avii.Classes.FulfillmentTracking> trackingList;
        private string returnCode;
        private string comment;
        public FulfillmentTrackingResponse()
        {
            trackingList = new List<FulfillmentTracking>();
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
        public List<avii.Classes.FulfillmentTracking> TrackingList
        {
            get
            {
                return trackingList;
            }
            set
            {
                trackingList = value;
            }
        }
    }

    public class CustomerSKUResponse
    {
        private List<avii.Classes.CustomerSKUs> skuList;
        private string returnCode;
        private string comment;
        public CustomerSKUResponse()
        {
            SKUList = new List<CustomerSKUs>();
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
        public List<avii.Classes.CustomerSKUs> SKUList
        {
            get
            {
                return skuList;
            }
            set
            {
                skuList = value;
            }
        }
    }


    public class ProductResponse
    {
        private List<avii.Classes.Brands> productList;
        private string returnCode;
        private string comment;
        public ProductResponse()
        {
            productList = new List<Brands>();
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
        public List<avii.Classes.Brands> ProductList
        {
            get
            {
                return productList;
            }
            set
            {
                productList = value;
            }
        }
    }
    public class RmaEsnDetailResponse
    {
        private List<avii.Classes.RMAESNDetail> rmaEsnDetail;
        private string returnCode;
        private string comment;
        public RmaEsnDetailResponse()
        {
            rmaEsnDetail = new List<RMAESNDetail>();
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
        public List<avii.Classes.RMAESNDetail> RmaEsnDetail
        {
            get
            {
                return rmaEsnDetail;
            }
            set
            {
                rmaEsnDetail = value;
            }
        }

    }
}