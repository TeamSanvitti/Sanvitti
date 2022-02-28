using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class FulfillmentLogModel
    {
        public int CreateUserID { get; set; }
        public string ActionName { get; set; }
        public string FulfillmentNumber { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public string Comment { get; set; }
     //   public string ShortRequestData { get; set; }
        //public string ShortResponseData { get; set; }

        public int PO_ID { get; set; }
        public int StatusID { get; set; }
    }
    public class ContainerLogModel
    {
        public int CreateUserID { get; set; }
        public string ActionName { get; set; }
        public string FulfillmentNumber { get; set; }
        public string RequestData { get; set; }
        public string ResponseData { get; set; }
        public string Comment { get; set; }
        //   public string ShortRequestData { get; set; }
        //public string ShortResponseData { get; set; }
        
        public int PO_ID { get; set; }
        public int StatusID { get; set; }
        public int ContainerCount { get; set; }
        public int ContainerUpdatedCount { get; set; }
    }
}
