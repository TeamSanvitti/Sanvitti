using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class ContainerLabelInfo
    {
        public string ContainerID { get; set; }
        public int CasePackQuantity { get; set; }

        public string PO_Num { get; set; }

        public string ShipContact_Name { get; set; }

        public string ShipTo_City { get; set; }
        public string ShipTo_Address1 { get; set; }
        public string ShipTo_Address2 { get; set; }

        public string ShipTo_State { get; set; }
        public string ShipTo_Zip { get; set; }
        public string Ship_Via { get; set; }
        public string DPCI { get; set; }
        //public string ShipTo_Zip { get; set; }
        // public string ShipTo_Zip { get; set; }

    }
}
