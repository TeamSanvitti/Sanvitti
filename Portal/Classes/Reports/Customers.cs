using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace avii.Classes
{
    [XmlRoot(ElementName = "Customers", IsNullable = true)]
    public class Customers
    {
        private string customerName;
        private List<ShipViaList> shipVia;

        public Customers()
        {
            shipVia = new List<ShipViaList>();
        }
        public string CustomerName
        {
            get
            {
                return customerName;
            }
            set
            {
                customerName = value;
            }
        }
        [XmlArray("ShipViaList"), XmlArrayItem("ShipVia", IsNullable = true)]
         public List<ShipViaList> ShipVia
         {
             get
             {
                 return shipVia;

             }
             set
             {
                 shipVia = value;
             }
         }

    }
}