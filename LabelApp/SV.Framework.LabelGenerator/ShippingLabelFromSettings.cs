using System;
using System.Collections.Generic;
using System.Configuration;

namespace SV.Framework.Common.LabelGenerator
{
    public class ShippingLabelFromSettings : ConfigurationSection
    {
        [ConfigurationProperty("PrintFromSettings")]
        public PrintFromSettings PrintFromSettings
        {
            get
            {
                return (PrintFromSettings)this["PrintFromSettings"];
            }
            set
            {
                value = (PrintFromSettings)this["PrintFromSettings"];
            }
        }

    }

    public class PrintFromSettings : ConfigurationElement
    {
        [ConfigurationProperty("CompanyName", DefaultValue = "LanGlobal Inc.", IsRequired = true)]
        public string CompanyName
        {
            get
            {
                return (string)this["CompanyName"];
            }
            set
            {
                value = (string)this["CompanyName"];
            }
        }
        [ConfigurationProperty("ContactName", DefaultValue = "LanGlobal Inc.", IsRequired = true)]
        public string ContactName
        {
            get
            {
                return (string)this["ContactName"];
            }
            set
            {
                value = (string)this["ContactName"];
            }
        }
        [ConfigurationProperty("Address", DefaultValue = "LanGlobal Inc.", IsRequired = true)]
        public string Address
        {
            get
            {
                return (string)this["Address"];
            }
            set
            {
                value = (string)this["Address"];
            }
        }
        [ConfigurationProperty("City", DefaultValue = "City", IsRequired = true)]
        public string City
        {
            get
            {
                return (string)this["City"];
            }
            set
            {
                value = (string)this["City"];
            }
        }

        [ConfigurationProperty("State", DefaultValue = "CA", IsRequired = true)]
        public string State
        {
            get
            {
                return (string)this["State"];
            }
            set
            {
                value = (string)this["State"];
            }
        }


        [ConfigurationProperty("Postal", DefaultValue = "98011", IsRequired = true)]
        public string Postal
        {
            get
            {
                return (string)this["Postal"];
            }
            set
            {
                value = (string)this["Postal"];
            }
        }
        [ConfigurationProperty("Email", DefaultValue = "lan#lan.com", IsRequired = true)]
        public string Email
        {
            get
            {
                return (string)this["Email"];
            }
            set
            {
                value = (string)this["Email"];
            }
        }
        [ConfigurationProperty("ContentType", DefaultValue = "ContentType", IsRequired = true)]
        public string ContentType
        {
            get
            {
                return (string)this["ContentType"];
            }
            set
            {
                value = (string)this["ContentType"];
            }
        }
        [ConfigurationProperty("ShowReturnAddress", DefaultValue = "True", IsRequired = true)]
        public string ShowReturnAddress
        {
            get
            {
                return (string)this["ShowReturnAddress"];
            }
            set
            {
                value = (string)this["ShowReturnAddress"];
            }
        }
    }


    }
