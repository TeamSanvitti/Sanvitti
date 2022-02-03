using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace avii.Classes
{
    public class FulfillmentValidation
    {
        public static int ValidateFulfillmentOrder(List<FulfillmentNumber> poList, List<StoreIDs> storeList, List<ShipVia> shipViaList, List<FulfillmentSKU> skuList, List<avii.Classes.State> stateList, string companyAccountNumber, out int poRecordCount, out string poErrorMessage, out string poStoreIDErrorMessage, out string poShipViaErrorMessage, out string poSKUsErrorMessage, out string poStateErrorMessage, out string errorMessage)
        {
            string poXML = clsGeneral.SerializeObject(poList);

            string storeXML = clsGeneral.SerializeObject(storeList);

            string shipviaXML = clsGeneral.SerializeObject(shipViaList);
            string skuXML = clsGeneral.SerializeObject(skuList);
            string stateXML = clsGeneral.SerializeObject(stateList);

            int returnValue = 0;
            poRecordCount = 0;
            poErrorMessage = string.Empty;
            poStoreIDErrorMessage = string.Empty;
            poShipViaErrorMessage = string.Empty;
            poSKUsErrorMessage = string.Empty;
            errorMessage = string.Empty;
            poStateErrorMessage = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyAccountNumber", companyAccountNumber);
                objCompHash.Add("@piXMLData", poXML);
                objCompHash.Add("@piStoreXMLData", storeXML);
                objCompHash.Add("@piSKUsXMLData", skuXML);
                objCompHash.Add("@piShipViaXMLData", shipviaXML);
                objCompHash.Add("@piStateXMLData", stateXML);
                arrSpFieldSeq = new string[] { "@piCompanyAccountNumber", "@piXMLData", "@piStoreXMLData", "@piSKUsXMLData", "@piShipViaXMLData", "@piStateXMLData" };
                returnValue = db.ExCommand(objCompHash, "av_Fulfillment_Validations", arrSpFieldSeq, "@poRecordCount", "@poErrorMessage", "@poStoreIDErrorMessage", "@poSKUsErrorMessage", "@poShipViaErrorMessage", "@poStateErrorMessage", out poRecordCount, out poErrorMessage, out poStoreIDErrorMessage, out poSKUsErrorMessage, out poShipViaErrorMessage, out poStateErrorMessage);

            }
            catch (Exception objExp)
            {
                errorMessage = "av_Fulfillment_Validations: " + objExp.Message.ToString();
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }


            return returnValue;
        }

        public static int ValidateFulfillmentOrder(List<FulfillmentNumber> poList, List<FulfillmentSKU> skuList, string companyAccountNumber, out int poRecordCount, out string poErrorMessage, out string poStoreIDErrorMessage, out string poShipViaErrorMessage, out string poSKUsErrorMessage, out string poStateErrorMessage, out string errorMessage)
        {
            string poXML = clsGeneral.SerializeObject(poList);

            string storeXML = string.Empty;//clsGeneral.SerializeObject(storeList);

            string shipviaXML = string.Empty;//clsGeneral.SerializeObject(shipViaList);
            string skuXML = clsGeneral.SerializeObject(skuList);
            string stateXML = string.Empty;//clsGeneral.SerializeObject(stateList);

            int returnValue = 0;
            poRecordCount = 0;
            poErrorMessage = string.Empty;
            poStoreIDErrorMessage = string.Empty;
            poShipViaErrorMessage = string.Empty;
            poSKUsErrorMessage = string.Empty;
            errorMessage = string.Empty;
            poStateErrorMessage = string.Empty;
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@piCompanyAccountNumber", companyAccountNumber);
                objCompHash.Add("@piXMLData", poXML);
                objCompHash.Add("@piStoreXMLData", storeXML);
                objCompHash.Add("@piSKUsXMLData", skuXML);
                objCompHash.Add("@piShipViaXMLData", shipviaXML);
                objCompHash.Add("@piStateXMLData", stateXML);
                arrSpFieldSeq = new string[] { "@piCompanyAccountNumber", "@piXMLData", "@piStoreXMLData", "@piSKUsXMLData", "@piShipViaXMLData", "@piStateXMLData" };
                returnValue = db.ExCommand(objCompHash, "av_Fulfillment_Validations", arrSpFieldSeq, "@poRecordCount", "@poErrorMessage", "@poStoreIDErrorMessage", "@poSKUsErrorMessage", "@poShipViaErrorMessage", "@poStateErrorMessage", out poRecordCount, out poErrorMessage, out poStoreIDErrorMessage, out poSKUsErrorMessage, out poShipViaErrorMessage, out poStateErrorMessage);

            }
            catch (Exception objExp)
            {
                errorMessage = "av_Fulfillment_Validations: " + objExp.Message.ToString();
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }


            return returnValue;
        }

    }
}