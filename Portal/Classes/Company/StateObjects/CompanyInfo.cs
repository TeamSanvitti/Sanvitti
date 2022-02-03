using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    public class CompanyInfo
    {
        public string CompanyAccountNumber { get; set; }
        public string CompanyShortName { get; set; }
        public string CompanyName { get; set; }
        
        public List<CompanyAddresses> CompanyAddresses { get; set; }
        public List<CompanyStore> Stores { get; set; }
        //public List<StoreLocation> officeAndShippAddress { get; set; }
        public string Email { get; set; }
        public string GroupEmail { get; set; }
        public string Website { get; set; }
        public string BussinessType { get; set; }
        //public int CompanySType { get; set; }
        public string Comment { get; set; }
        //public int CompanyAccountStatus { get; set; }
        
    }
}