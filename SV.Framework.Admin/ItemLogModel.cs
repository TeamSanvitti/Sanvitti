using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Admin
{
    public class ItemLog
    {
        public string UserName { get; set; }
        public string ProductName { get; set; }
        public string ModelNumber { get; set; }
        public string CategoryName { get; set; }
        public string ActionName { get; set; }
        public string SKU { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public string Comment { get; set; }
        public DateTime CreateDate { get; set; }
        //   public string ShortRequestData { get; set; }
        //public string ShortResponseData { get; set; }

       // public int ItemGUID { get; set; }
        //public int ItemCompanyGUID { get; set; }
        public string Status { get; set; }
        //public int ContainerCount { get; set; }
        //public int ContainerUpdatedCount { get; set; }
    }

    public class ItemLogModel
    {
        public int CreateUserID { get; set; }
        public string ActionName { get; set; }
        public string SKU { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public string Comment { get; set; }
        //   public string ShortRequestData { get; set; }
        //public string ShortResponseData { get; set; }

        public int ItemGUID { get; set; }
        public int ItemCompanyGUID { get; set; }
        public string Status { get; set; }
        //public int ContainerCount { get; set; }
        //public int ContainerUpdatedCount { get; set; }
    }

    public class Kittedsku
    {
        public int ItemCompanyGUID { get; set; }
        public List<Kitrawsku> RawSKUs { get; set; }
        public string Status { get; set; }
    }
    public class Kitrawsku
    {
        public int ItemCompanyGUID { get; set; }
        public int Quantity { get; set; }
        public string DisplayName { get; set; }
        
        //public int ContainerCount { get; set; }
        //public int ContainerUpdatedCount { get; set; }
    }
}
