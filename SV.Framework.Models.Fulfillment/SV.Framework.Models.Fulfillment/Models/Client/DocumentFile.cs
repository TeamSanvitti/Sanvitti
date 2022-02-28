using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
    public class DocumentFile
    {
        public int PO_ID { get; set; }
        public string FulfillmentNumber { get; set; }
        public string ModuleName { get; set; }
        public string PODate { get; set; }
        public string FulfillmentStatus { get; set; }
        public int LineItemCount { get; set; }
        public Int64 DocumentSourceID { get; set; }
        public Int64 FileUploadID { get; set; }
        public string FileName { get; set; }
        public string FileDescription { get; set; }
        public string UploadedBy { get; set; }
        public string UploadDate { get; set; }        
    }
}
