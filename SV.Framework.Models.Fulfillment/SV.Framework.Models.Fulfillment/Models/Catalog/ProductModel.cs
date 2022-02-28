using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{
    public class ProductModel
    {
        public int ItemGuid { get; set; }
        public int CategoryID { get; set; }
        public string ModelNumber { get; set; }
        public string ItemName { get; set; }
        public string ItemDesc { get; set; }
        public string ItemFullDesc { get; set; }
        public string UPCcode { get; set; }
        public int Active { get; set; }
        public int MakerGUID { get; set; }
        public int Companyid { get; set; }
        public int userID { get; set; }
        public int ProductTypeID { get; set; }
        public int ConditionID { get; set; }
        public bool? ReStock { get; set; }
        public bool? AllowRMA { get; set; }
        public bool? showunderCatalog { get; set; }
        public List<Carriers> carrierList { get; set; }
        public bool? allowSIM { get; set; }
        public bool? IsKitted { get; set; }
        public bool? allowESN { get; set; }
        public bool? IsDisplayName { get; set; }
        public double? Weight { get; set; }
        public string storage { get; set; }
        public int storageQty { get; set; }
        public int esnLength { get; set; }
        public int meidLength { get; set; }
        public string OSType { get; set; }


    }
}
