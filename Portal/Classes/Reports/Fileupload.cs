using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class Fileupload
    {
        public string CustomerName { get; set; }
        public DateTime UploadDate { get; set; }
        public string UploadedBy { get; set; }
        public string FileName { get; set; }
        public string ModuleName { get; set; }
        public string Comments { get; set; }
        public string Resource { get; set; }
        public int ModuleID { get; set; }
        public int CompanyID { get; set; }
        public int UploadedFileID { get; set; }
    }
}