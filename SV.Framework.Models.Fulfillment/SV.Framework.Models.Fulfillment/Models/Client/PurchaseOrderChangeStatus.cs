using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Fulfillment
{
    [XmlRoot(ElementName = "PurchaseOrderChangeStatus", IsNullable = true)]
    public class PurchaseOrderChangeStatus
    {

        private string purchaseOrderNumber;
        private string companyAccountNumber;

        [XmlElement(ElementName = "PurchaseOrderNumber", IsNullable = true)]
        public string PurchaseOrderNumber
        {
            get { return purchaseOrderNumber; }
            set { purchaseOrderNumber = value; }
        }
        [XmlElement(ElementName = "companyAccountNumber", IsNullable = true)]
        public string CompanyAccountNumber
        {
            get { return companyAccountNumber; }
            set { companyAccountNumber = value; }
        }

    }
}
