using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class UnusedLabel
    {
        public string LabelSource { get; set; }
        public int ID { get; set; }
        public int POID { get; set; }
        public string LabelGenerationDate { get; set; }
        public string ShipmentMethod { get; set; }
        public string ShipPackage { get; set; }
        public string TrackingNumber { get; set; }
        public decimal ShippingWeight { get; set; }
        public decimal FinalPostage { get; set; }
        
        
        
        public string LabelType { get; set; }
        public string AssignedTo { get; set; }
        public string AssignToNumber { get; set; }



    }
    public class UnusedLabelInfo
    {
        public DataTable dataTable { get; set; }
        public int UserID { get; set; }
    }

    }