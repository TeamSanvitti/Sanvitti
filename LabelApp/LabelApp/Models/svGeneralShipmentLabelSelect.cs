using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabelApp.Models
{
    public class svGeneralShipmentLabelSelect
    {
        public int ShipmentID { get; set; }
        public string FromName { get; set; }
        public string FromAddress1 { get; set; }
        public string FromAddress2 { get; set; }
        public string FromCity { get; set; }
        public string FromState { get; set; }
        public string FromZip { get; set; }
        public string FromPhone { get; set; }
        public string ToName { get; set; }
        public string ToCompany { get; set; }
        public string ToAddress1 { get; set; }
        public string ToAddress2 { get; set; }
        public string ToCity { get; set; }
        public string ToState { get; set; }
        public string ToZip { get; set; }
        public string ToPhone { get; set; }
        public string ToEmail { get; set; }
        public string ShipVia { get; set; }
        public string ShipPackage { get; set; }
        public string Carrier { get; set; }
        public string TrackingNumber { get; set; }
        public DateTime RequestedShipmentdate { get; set; }
        public decimal FinalPostage { get; set; }
        public string ShipmentWeight { get; set; }
       
    }
}
