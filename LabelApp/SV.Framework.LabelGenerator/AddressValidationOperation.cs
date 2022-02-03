using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SV.Framework.LabelGenerator
{
    public interface iValidateAddress
    {
        string AccountID { get; set; }
        string PassPharase { get; set; }
        string RequesterID { get; set; }
        string Name { get; set; }
        string Company { get; set; }

        string Address1 { get; set; }
        string Address2 { get; set; }
        string City { get; set; }
        string State { get; set; }
        string PostalCode { get; set; }
        string CountryCode { get; set; }

    }
    public class ValidateAddress : iValidateAddress
    {
        public string AccountID { get; set; }
        public string PassPharase { get; set; }
        public string RequesterID { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string CountryCode { get; set; }

    }

    
    public class Addresss
    {
        private string name;
        private string company;
        private string address1;
        private string address2;

        private string city;
        private string state;
        private string postalCode;
        private string zip4;
        private string countryCode;
        private string urbanization;
        private int deliveryPoint;
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }
        public string Company
        {
            get
            {
                return this.company;
            }
            set
            {
                this.company = value;
            }
        }
        public string Address1
        {
            get
            {
                return this.address1;
            }
            set
            {
                this.address1 = value;
            }
        }
        public string Address2
        {
            get
            {
                return this.address2;
            }
            set
            {
                this.address2 = value;
            }
        }
        public string City
        {
            get
            {
                return this.city;
            }
            set
            {
                this.city = value;
            }
        }
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }
        public string PostalCode
        {
            get
            {
                return this.postalCode;
            }
            set
            {
                this.postalCode = value;
            }
        }
        public string Zip4
        {
            get
            {
                return this.zip4;
            }
            set
            {
                this.zip4 = value;
            }
        }
        public string CountryCode
        {
            get
            {
                return this.countryCode;
            }
            set
            {
                this.countryCode = value;
            }
        }
        public string Urbanization
        {
            get
            {
                return this.urbanization;
            }
            set
            {
                this.urbanization = value;
            }
        }
        public int DeliveryPoint
        {
            get
            {
                return this.deliveryPoint;
            }
            set
            {
                this.deliveryPoint = value;
            }
        }

    }
    //public class CertifiedIntermediary
    //{
    //    public string AccountID { get; set; }
    //    public string PassPhrase { get; set; }
    //    //public string Token { get; set; }
    //    //public string TokenTimeStamp { get; set; }

    //}
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "www.envmgr.com/LabelService")]
    public partial class ValidateAddressRequest
    {
        public string RequesterID { get; set; }
        public SV.Framework.Common.LabelGenerator.CertifiedIntermediary CertifiedIntermediary { get; set; }
        public Addresss Address { get; set; }
    }
    public class AddressValidationOperation
    {
        public static string SerializeObject<T>(T obj)
        {
            StringWriter xmlstringVal = null;
            XmlWriterSettings xmlSettings = new XmlWriterSettings();
            XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            try
            {
                xmlstringVal = new StringWriter();
                ///xmlSettings.NamespaceHandling = NamespaceHandling.OmitDuplicates;
                xmlSettings.OmitXmlDeclaration = true;
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlWriter xmlWriter = XmlWriter.Create(xmlstringVal, xmlSettings);
                xs.Serialize(xmlWriter, obj, ns);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xmlSettings = null;
                xmlstringVal.Dispose();
            }

            return xmlstringVal.ToString().Trim();
            //try
            //{
            //    string xmlString = null;
            //    MemoryStream memoryStream = new MemoryStream();
            //    XmlSerializer xs = new XmlSerializer(typeof(T));
            //    XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
            //    xs.Serialize(xmlTextWriter, obj);
            //    memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
            //    xmlString = UTF8ByteArrayToString(memoryStream.ToArray()); return xmlString.Trim();
            //}
            //catch (Exception ex)
            //{
            //    return string.Empty;
            //}
        }

        public ValidateAddressResponse ValidateAddress(iValidateAddress addressRequest)
        {
            SV.Framework.Common.LabelGenerator.EwsLabelService service = new SV.Framework.Common.LabelGenerator.EwsLabelService();

            ValidateAddressRequest request = PopulateValidateAddressRequest(addressRequest);
            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            ValidateAddressResponse resonse = service.ValidateAddress(request);

            return resonse;
        }
        private ValidateAddressRequest PopulateValidateAddressRequest(iValidateAddress addressRequest)
        {
            ValidateAddressRequest request = new ValidateAddressRequest();
            string RequesterID = addressRequest.RequesterID;// "Lang";
            SV.Framework.Common.LabelGenerator.CertifiedIntermediary certifiedIntermediary = new SV.Framework.Common.LabelGenerator.CertifiedIntermediary();
            SV.Framework.LabelGenerator.Addresss address = new SV.Framework.LabelGenerator.Addresss();
            certifiedIntermediary.AccountID = addressRequest.AccountID;// "1353742";
            certifiedIntermediary.PassPhrase = addressRequest.PassPharase;// "12031Lan1@";
            request.CertifiedIntermediary = certifiedIntermediary;
            address.Address1 = addressRequest.Address1;// "1100 Glendon Ave";
            address.Address2 = addressRequest.Address2;//"";
            address.Name = addressRequest.Name;// "alexandra";
            address.Company = addressRequest.Company;// "Freedom Pop";
            address.City = addressRequest.City;// "Los Angeles";
            address.State = addressRequest.State;// "CA";
            address.CountryCode = addressRequest.CountryCode;// "US";
            address.PostalCode = addressRequest.PostalCode;// "90024";

            request.RequesterID = RequesterID;
            request.Address = address;

            return request;
        }

    }

    public class CandidateAddress
    {
        private string address1;
        //private string address2;

        private string city;
        private string state;
        private string postalCode;
        private string zip4;
        private string countryCode;
        
        public string Address1
        {
            get
            {
                return this.address1;
            }
            set
            {
                this.address1 = value;
            }
        }
        //public string Address2
        //{
        //    get
        //    {
        //        return this.address2;
        //    }
        //    set
        //    {
        //        this.address2 = value;
        //    }
        //}
        public string City
        {
            get
            {
                return this.city;
            }
            set
            {
                this.city = value;
            }
        }
        public string State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }
        public string PostalCode
        {
            get
            {
                return this.postalCode;
            }
            set
            {
                this.postalCode = value;
            }
        }
        public string Zip4
        {
            get
            {
                return this.zip4;
            }
            set
            {
                this.zip4 = value;
            }
        }
        public string CountryCode
        {
            get
            {
                return this.countryCode;
            }
            set
            {
                this.countryCode = value;
            }
        }
       
    }
    
    //[System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    //[System.SerializableAttribute()]
    //[System.Diagnostics.DebuggerStepThroughAttribute()]
    //[System.ComponentModel.DesignerCategoryAttribute("code")]
    //[System.Xml.Serialization.XmlTypeAttribute(Namespace = "www.envmgr.com/LabelService")]
    public class CandidateAddresses
    {
        public List<CandidateAddress> Address { get; set; }
        
    }
    public class StatusCode
    {
        List<Footnote> Footnotes { get; set; }
        List<DpvFootnote> DpvFootnotes { get; set; }

        public int ReturnCode { get; set; }
    }
    public class DpvFootnote
    {
        public string Value { get; set; }
    }
    public class Footnote {
        public string Value { get; set; }
    }
    [System.CodeDom.Compiler.GeneratedCodeAttribute("wsdl", "4.6.1055.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "www.envmgr.com/LabelService")]

    public partial class ValidateAddressResponse
    {
        public string Status { get; set; }
        public Addresss Address { get; set; }
        public bool AddressMatch { get; set; }
        public bool CityStateZipOK { get; set; }
        public bool ResidentialDeliveryIndicator { get; set; }
        public bool IsPOBox { get; set; }

        public List<SV.Framework.Common.LabelGenerator.Address> CandidateAddresses { get; set; }

        public StatusCode StatusCodes { get; set; }
        public string AddressCleansingResult { get; set; }
        public string VerificationLevel { get; set; }
        public string AddressCleansedHash { get; set; }



    }
}
