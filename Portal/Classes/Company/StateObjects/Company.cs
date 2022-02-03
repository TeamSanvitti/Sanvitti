using System;
using System.Collections.Generic;

namespace avii.Classes
{
    [Serializable]
    public class Company
    {
        public int CompanyID { get; set; }
        public string CompanyAccountNumber { get; set; }
        public string CompanyName { get; set; }
        public List<Address> CompanyAddresses { get; set; }
        public List<StoreLocation> Stores { get; set; }
        public List<StoreLocation> officeAndShippAddress { get; set; }
        //public bool Active { get; set; }
        public string Email { get; set; }
        public string GroupEmail { get; set; }
        public List<SalesPerson> AssingedSalesPerson { get; set; }
        public string Website { get; set; }
        public string BussinessType { get; set; }
        public int CompanySType { get; set; }
        public string CompanyShortName { get; set; }
        public string Comment { get; set; }
        public int CompanyAccountStatus { get; set; }
        public bool Active { get; set; }
        public List<CustomerEmail> CustomerEmailList { get; set; }
        public List<CustomerWarehouseCode> WarehouseCodeList { get; set; }
        public List<IntegrationModel> IntegrationList { get; set; }
        public bool IsEmail { get; set; }
        public int PricingTypeID { get; set; }

        public string LogoPath { get; set; }
        public int UserID { get; set; }
        //public string APIName { get; set; }
        //public string APIAddress { get; set; }
        //public string APIUserName { get; set; }
        //public string APIPassword { get; set; }


    }
}
