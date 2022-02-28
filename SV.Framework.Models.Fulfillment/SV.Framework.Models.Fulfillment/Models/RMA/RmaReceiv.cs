using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class RmaReceive
    {
        public int RMAReceiveGUID { get; set; }
        public int UserID { get; set; }
        public string Comment { get; set; }
        public string RmaNumber { get; set; }
        public string CustomerRmaNumber { get; set; }
        public string RmaDate { get; set; }
        public string RmaStatus { get; set; }
        public int RMAGUID { get; set; }
        public int ApprovedID { get; set; }
        public int ReceivedByID { get; set; }
        public List<RmaReceiveDetail> ReceiveList { get; set; }
        public List<RmaReceiveDetail> ReceivedList { get; set; }
        public List<RmaReceiveTracking> TrackingList { get; set; }

    }

}
