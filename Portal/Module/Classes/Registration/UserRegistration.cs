using System;
using System.Collections.Generic;

namespace avii.Classes
{
    [Serializable]
    public class UserRegistration
    {


        public UserRegistration()
        {
            AccountStatusID = 0;
            AerovoiceAdminUserName = string.Empty;
            CompanyID = 0;
            //CompanyAddressID = 0;
            Email = string.Empty;
            IsAdmin = false;
            Password = string.Empty;
            TilaForm = string.Empty;
            WelcomeForm = string.Empty;
            V2Form = string.Empty;
            UserID = 0;
            UserName = _userName;
            UserType = string.Empty;
            Active = false;
            CompanyName = string.Empty;
            CompanyAccNo = string.Empty;
            AccStatus = string.Empty;
            Stores = string.Empty;
            OfficeAndShippAddress = new List<StoreLocation>();
            UserStores = new List<StoreLocation>();
        }
        private string _userName;
        public int AccountStatusID { get; set; }
        public string AerovoiceAdminUserName { get; set; }
        public int CompanyID { get; set; }
        //public int CompanyAddressID { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
        public string TilaForm { get; set; }
        public string WelcomeForm { get; set; }
        public string V2Form { get; set; }
        public int UserID { get; set; }
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        public string UserType { get; set; }
        public bool Active { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAccNo { get; set; }
        public string AccStatus { get; set; }
        public string Stores { get; set; }
        public List<StoreLocation> OfficeAndShippAddress { get; set; }
        public List<StoreLocation> UserStores { get; set; }

    }
}