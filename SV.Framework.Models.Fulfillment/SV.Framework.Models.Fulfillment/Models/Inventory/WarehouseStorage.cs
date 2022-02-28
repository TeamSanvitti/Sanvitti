using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Inventory
{
    public class WarehouseStorage
    {
        public int WarehouseStorageID { get; set; }
        public int WarehouseID { get; set; }
        public int CompanyID { get; set; }
        public int UserID { get; set; }        
        public string WarehouseLocation { get; set; }
        public string Aisle { get; set; }
        public string Bay { get; set; }
        public string RowLevel { get; set; }
        public string CompanyName { get; set; }
        public string Specialinstructions { get; set; }
        public string WarehouseCity { get; set; }
        public string CreatedBy { get; set; }
        public string CreateDate { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public List<LocationEsnInfo> ESNs { get; set; }
    }


}
