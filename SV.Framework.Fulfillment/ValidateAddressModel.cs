using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Fulfillment
{
    public class ValidateAddressModel
    {
        public int PO_ID { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }
        public string Zip4 { get; set; }
        public string Status { get; set; }
        public string AddressCleansingResult { get; set; }
        public string VerificationLevel { get; set; }
        public string CompanyName { get; set; }
        public bool AddressMatch { get; set; }
        public bool CityStateZipOK { get; set; }
        public List<CandidateAddress> CandidateAddresses { get; set; }

    }
    public class CandidateAddress
    {
        public string Address1 { get; set; }
        //public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Zip4 { get; set; }
        public string CountryCode { get; set; }

    }
}
