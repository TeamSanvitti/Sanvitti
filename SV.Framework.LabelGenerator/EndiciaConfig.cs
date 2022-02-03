using System.Configuration;

namespace SV.Framework.Common.LabelGenerator
{

        public class EndiciaAccountSettings : ConfigurationSection
        {
            [ConfigurationProperty("EndiciaAccontInfo")]
            public EndiciaAccontInfo EndiciaAccontInfo
            {
                get
                {
                    return (EndiciaAccontInfo)this["EndiciaAccontInfo"];
                }
                set
                {
                    value = (EndiciaAccontInfo)this["EndiciaAccontInfo"];
                }
            }

        }
    

    public class EndiciaAccontInfo : ConfigurationElement
    {

        [ConfigurationProperty("PassPhrase", DefaultValue = "atheskyisred", IsRequired = true)]
        public string PassPhrase
        {
            get
            {
                return (string)this["PassPhrase"];
            }
            set
            {
                value = (string)this["PassPhrase"];
            }
        }


        [ConfigurationProperty("AccountID", DefaultValue = "2553271", IsRequired = true)]
        public string AccountID
        {
            get
            {
                return (string)this["AccountID"];
            }
            set
            {
                value = (string)this["AccountID"];
            }
        }


        [ConfigurationProperty("RequesterID", DefaultValue = "lxxx", IsRequired = true)]
        public string RequesterID
        {
            get
            {
                return (string)this["RequesterID"];
            }
            set
            {
                value = (string)this["RequesterID"];
            }
        }

    }

}
