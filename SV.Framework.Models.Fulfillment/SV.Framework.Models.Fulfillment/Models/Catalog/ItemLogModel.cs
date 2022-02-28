using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{
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

    
}
