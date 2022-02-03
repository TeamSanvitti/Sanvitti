using System;

namespace avii.Classes
{
    public class RMAInfo
    {

        public string RMANumber { get; set; }
        public DateTime RMADate { get; set; }
        public string RMAStatus { get; set; }
        public string ContactName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string StoreID { get; set; }
        public string Country { get; set; }
        
        public DateTime MaxShipmentDate { get; set; }
        public int ESNCount { get; set; }




    }
    public class RMAInfoRequest
    {
        //private SV.Framework.DataMembers.State.UserCredentials userCredentials;

        private string trackingNumber;
        //public RMAInfoRequest()
        //{
        //    userCredentials = new SV.Framework.DataMembers.State.UserCredentials();

        //}


        //public SV.Framework.DataMembers.State.UserCredentials UserCredentials
        //{
        //    get
        //    {
        //        return userCredentials;
        //    }
        //    set
        //    {
        //        userCredentials = value;
        //    }
        //}
        public string TrackingNumber
        {
            get
            {
                return trackingNumber;
            }
            set
            {
                trackingNumber = value;
            }
        }
        
        
    }
    public class RMAInfoResponse
    {

        private RMAInfo rma;
        private string returnCode;
        private string comment;
        public RMAInfoResponse()
        {
            rma = new RMAInfo();
        }
        public RMAInfo rmaInfo
        {
            get { return rma; }
            set { rma = value; }
        }

        public string ReturnCode
        {
            get
            {
                return returnCode;
            }
            set
            {
                returnCode = value;
            }
        }

        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
    }



    
}