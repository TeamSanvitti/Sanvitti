using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    public class ServiceOrders
    {
        public int ServiceOrderId { get; set; }
        public string ServiceOrderNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string SKU { get; set; }
        public string ESN { get; set; }
        public int CompanyId { get; set; }
        public string OrderDate { get; set; }
        public DateTime OrderDate2 { get; set; }
        public int KittedSKUId { get; set; }
        public int Quantity { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string CompanyName { get; set; }
        public string CategoryName { get; set; }
        public string ProductName { get; set; }
        public List<ServiceOrderDetail> SODetail { get; set; }

    }
    public class ServiceOrderCSV
    {
        public string ServiceOrderNumber { get; set; }
        public string IMEI { get; set; }
        public string ICCID { get; set; }
        public string CustomerOrderNumber { get; set; }
        public string KittedSKU { get; set; }
        public string Customer { get; set; }
        public string Date { get; set; }
        public string Qty { get; set; }
        public string QtyPerOrder { get; set; }

    }
    public class SOSKUSummary
    {
        public string SKU { get; set; }
        public int Quantity { get; set; }
        
    }
    public class ServiceOrderDetail
    {
        public Int64 KitID { get; set; }

        [XmlIgnore]
        public bool IsSim { get; set; }
        [XmlIgnore]
        public int Qty { get; set; }

        [XmlIgnore]
        public int Id { get; set; }
        [XmlIgnore]
        public int OrderDetailId { get; set; }
        public string ESN { get; set; }
        //[XmlIgnore]
        public int ItemCompanyGUID { get; set; }
        [XmlIgnore]
        public bool IsPrint { get; set; }
        [XmlIgnore]
        public string ValidationMsg { get; set; }
        [XmlIgnore]
        public string ICCIDValidationMsg { get; set; }
        [XmlIgnore]
        public string BatchNumber { get; set; }
        //[XmlIgnore]
        public int MappedItemCompanyGUID { get; set; }
        public string ICCID { get; set; }
        //[XmlIgnore]
        public string SKU { get; set; }
        [XmlIgnore]
        public string MappedSKU { get; set; }
        [XmlIgnore]
        public string UPC { get; set; }
        public int RowNumber { get; set; }
        


    }
    [Serializable]
    public class SOQtyValidate
    {
        private int id;
        private int iItemCompanyGUID;
        private int qty;
        private string startESN;
        private string endESN;
        public int Qty
        {
            get
            {
                return qty;
            }
            set
            {
                qty = value;
            }
        }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public int ItemCompanyGUID
        {
            get
            {
                return iItemCompanyGUID;
            }
            set
            {
                iItemCompanyGUID = value;
            }
        }
        public string StartESN
        {
            get
            {
                return startESN;
            }
            set
            {
                startESN = value;
            }
        }
        public string EndESN
        {
            get
            {
                return endESN;
            }
            set
            {
                endESN = value;
            }
        }
      
       



    }
    public class SOCSV
    {
        public string SKU { get; set; }
        public string ESN { get; set; }
        public string ICCID { get; set; }

    }
}