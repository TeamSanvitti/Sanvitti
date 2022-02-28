using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.RMA
{
    public class RmaDocument
    {
        public int? RmaDocID { get; set; }
        public string RmaDocName { get; set; }
        public string DocType { get; set; }
    }
}
