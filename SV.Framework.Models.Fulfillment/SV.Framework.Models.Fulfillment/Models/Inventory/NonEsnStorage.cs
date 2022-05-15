using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class NonEsnStorage
    {
        public int SequenceNumber { get; set; }
        public string WareHouseLocation { get; set; }
        public string BoxID { get; set; }
        public string CategoryName { get; set; }
        public string SKU { get; set; }
        public string ProductName { get; set; }
        public string Aisle { get; set; }
        public string Bay { get; set; }
        public string RowLevel { get; set; }
        public int Quantity { get; set; }
        public bool Inspected { get; set; }
        public DateTime UploadDate { get; set; }

    }

    public class NonESNInventory
    {
        public int CompanyID { get; set; }
        public int ESNHeaderId { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public int UserID { get; set; }
        public int ItemCompanyGUID { get; set; }
        public int PalletCount { get; set; }
        public int CartonCount { get; set; }
        public int PiecesPerBox { get; set; }
        public int TotalQuantity { get; set; }
        public string Comment { get; set; }
        public string ReceivedAs { get; set; }
        public List<NonEsnStorage> nonEsnList { get; set; }
    }

    public class NonEsnHeader
    {
        public string CategoryWithProductAllowed { get; set; }
        public string UserName { get; set; }
        public bool IsESN { get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public int CompanyID { get; set; }
        public int ESNHeaderId { get; set; }
        public string OrderNumber { get; set; }
        public string CustomerOrderNumber { get; set; }
        public DateTime ReceivedDate { get; set; }
        public string UploadDate { get; set; }
        //public string ShipDate { get; set; }
        //public string Shipvia { get; set; }
        //public string TrackingNumber { get; set; }
        public int TotalQty { get; set; }
        //public int ShipQty { get; set; }
        public int ItemCompanyGUID { get; set; }
        //public decimal UnitPrice { get; set; }
        public string SKU { get; set; }
        public String CompanyAccountNumber { get; set; }
        public List<NonEsnStorage> StorageList { get; set; }
        public string ProductName { get; set; }
        public string ReceivedAs { get; set; }
        public bool IsInspection { get; set; }
        public int PalletCount { get; set; }
        public int CartonCount { get; set; }
        public int PiecesPerBox { get; set; }
        public string WareHouseLocation { get; set; }
        public string BoxID { get; set; }
        //public string ReceivedBy { get; set; }
        public int AssignedQty { get; set; }
        //public int TotalQty { get; set; }
    }


}
