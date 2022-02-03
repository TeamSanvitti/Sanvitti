using System;
using System.Collections.Generic;
using System.Configuration;

namespace SV.Framework.Common.LabelGenerator
{
    public class GetConfigurations
    {
        public static string ACCOUNT_ID;
        public static string REQUESTER_ID;
        public static string PASS_PHRASE;
        
        public static string HOST_COMPANY_NAME ;
        public static string HOST_CONTACT_NAME ;
        public static string HOST_ADDRESS_NAME;
        public static string HOST_CITY;
        public static string HOST_POSTAL;
        public static string HOST_STATE;
        public static string HOST_EMAIL;
        public static string SHOW_RETURN_ADDRESS;
        public static string CONTENTS_TYPE;

        static  GetConfigurations()
        {
            
            GetConfigurationHostSettings();
            GetConfigurationEndicia();
        }

        public static PrintFromSettings GetConfigurationHostSettings()
        {
            PrintFromSettings printFromSettings = null;
            var hostSettings = ConfigurationManager.GetSection("ShippingLabelFromSettings") as ShippingLabelFromSettings;
            if (hostSettings == null || hostSettings.PrintFromSettings == null)
            {
                throw new Exception("Host company settings are not configured");
            }
            else
            {
                printFromSettings = hostSettings.PrintFromSettings;
                HOST_COMPANY_NAME = hostSettings.PrintFromSettings.CompanyName;
                HOST_CONTACT_NAME = hostSettings.PrintFromSettings.ContactName;
                HOST_ADDRESS_NAME = hostSettings.PrintFromSettings.Address;
                HOST_CITY = hostSettings.PrintFromSettings.City;
                HOST_POSTAL = hostSettings.PrintFromSettings.Postal;
                HOST_STATE = hostSettings.PrintFromSettings.State;
                HOST_EMAIL  = hostSettings.PrintFromSettings.Email;
                SHOW_RETURN_ADDRESS = hostSettings.PrintFromSettings.ShowReturnAddress;
                CONTENTS_TYPE = hostSettings.PrintFromSettings.ContentType;
                
            }

            return printFromSettings;
        }



        public static EndiciaAccontInfo GetConfigurationEndicia()
        {
            EndiciaAccontInfo endiciaAccontInfo = null;
            var productSettings = ConfigurationManager.GetSection("EndiciaAccountSettings") as EndiciaAccountSettings;
            if (productSettings == null || productSettings.EndiciaAccontInfo == null)
            {
                throw new Exception("Endicia settings are not defined");
            }
            else
            {
                endiciaAccontInfo = productSettings.EndiciaAccontInfo;

                ACCOUNT_ID = productSettings.EndiciaAccontInfo.AccountID;;
                REQUESTER_ID = productSettings.EndiciaAccontInfo.RequesterID;
                PASS_PHRASE = productSettings.EndiciaAccontInfo.PassPhrase;
            }

            return endiciaAccontInfo;
        }
    }
}
