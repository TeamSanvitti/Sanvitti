using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using System.Linq;
using SV.Framework.Models.Inventory;
using SV.Framework.DAL.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class EsnAuthorizationOperation : BaseCreateInstance
    {
        
        public  XElement CreateAuthorizationFile(List<ESNAuthorization> esnList, string file_Sequence, string currentDate)
        {
            //string currentDate = DateTime.Now.ToString("yyyy-MM-dd");
            //Int64 ticks = DateTime.Now.Ticks;

            string filePrefix = "spdsh";
            string transDate = currentDate.Replace("-", "");

            //string fileName = filePrefix + "_" + transDate + "_" + file_Sequence.ToString() + ".xml";

            //string filePath = "c:/xml/" + fileName;

            XNamespace tns = "http://integration.sprint.com/interfaces/UEDF/v1/UEDF.xsd";
            XElement edfFileGeneric = new XElement(tns + "edfFileGeneric",
                new XAttribute(XNamespace.Xmlns + "tns", "http://integration.sprint.com/interfaces/UEDF/v1/UEDF.xsd"));

            XElement date = new XElement(tns + "date", currentDate);
            edfFileGeneric.Add(date);

            XElement fileSequence = new XElement(tns + "fileSequence", file_Sequence);
            edfFileGeneric.Add(fileSequence);

            XElement totalDeviceCount = new XElement(tns + "totalDeviceCount", esnList.Count);
            edfFileGeneric.Add(totalDeviceCount);

            XElement headerCount = new XElement(tns + "headerCount", 1);
            edfFileGeneric.Add(headerCount);

           // string[] array = edfFileInfo.edfData.edfHeader.poOrder.Split('-');
           // string poOrder = array[0];
            XElement edfData = new XElement(tns + "edfData");


            XElement edfHeader = new XElement(tns + "edfHeader",
                       new XElement(tns + "deviceCount", esnList.Count),
                       new XElement(tns + "phoneType", "RW"),
                       new XElement(tns + "phoneOwnership", "DISH"),
                       new XElement(tns + "transactionType", "040"),
                       //new XElement(tns + "poOrder", poOrder),
                       // new XElement(tns + "factOrder", edfFileInfo.edfData.edfHeader.poOrder),
                       new XElement(tns + "locationDestination", "000"),
                       new XElement(tns + "uedfRevisionNumber", "XMLAUTH")
                       );
            edfData.Add(edfHeader);

            XElement product = new XElement(tns + "product");

            XElement edfSerialType = new XElement(tns + "edfSerialType", "H");
            product.Add(edfSerialType);

            //XElement entSerialType = new XElement(tns + "entSerialType", edfFileInfo.edfData.product.entSerialType);
            //product.Add(entSerialType);

            XElement skuInfo = new XElement(tns + "skuInfo",
                          new XElement(tns + "sku", esnList[0].SKU),
                          new XElement(tns + "skuName", esnList[0].SKUName),
                          new XElement(tns + "equipType", "CP"),
                          new XElement(tns + "manufId", esnList[0].ManfId),
                          new XElement(tns + "manufName", esnList[0].ManfName),
                          //new XElement(tns + "preferredSerialIndicator", edfFileInfo.edfData.product.skuInfo.preferredSerialIndicator),
                          //new XElement(tns + "masterSerialAttribute", edfFileInfo.edfData.product.skuInfo.masterSerialAttribute),
                          new XElement(tns + "sfwVer", esnList[0].SWVersion)
                          );
            product.Add(skuInfo);



            //loop start from here for multi details
            //XElement shipping = new XElement(tns + "shipping");

           // List<detail> details = edfFileInfo.edfData.product.details;
            foreach (ESNAuthorization device1 in esnList)
            {
                //foreach (shipping shipping1 in detail1.shippings)
                {
                    XElement detail = new XElement(tns + "detail");

                    //XElement shipping = new XElement(tns + "shipping");
                    //XElement lpn = new XElement(tns + "lpn", detail1.lpn.Replace("(", "").Replace(")", ""));
                    //shipping.Add(lpn);

                    //XElement carton = new XElement(tns + "carton", shipping1.carton.Replace("(", "").Replace(")", ""));
                    //shipping.Add(carton);

                    //detail.Add(shipping);

                  //  foreach (device device1 in shipping1.devices)
                    {
                        XElement device = new XElement(tns + "device");
                        XElement serialization = new XElement(tns + "serialization");

                        XElement meidHex = new XElement(tns + "meidHex", device1.MeidHex);
                        serialization.Add(meidHex);

                        XElement meidDec = new XElement(tns + "meidDec", device1.MeidDec);
                        serialization.Add(meidDec);

                        XElement imeiDec = new XElement(tns + "imeiDec", device1.ESN);
                        serialization.Add(imeiDec);

                        //XElement serialNumber = new XElement(tns + "serialNumber", device1.serialNumber);
                        //serialization.Add(serialNumber);
                        XElement authentication = new XElement(tns + "authentication");

                        XElement msl = new XElement(tns + "msl", device1.MSL);
                        authentication.Add(msl);
                        XElement otksl = new XElement(tns + "otksl", device1.OTKSL);
                        authentication.Add(otksl);

                        device.Add(serialization);
                        device.Add(authentication);

                        detail.Add(device);

                    }
                    product.Add(detail);
                }


            }

            edfData.Add(product);

            edfFileGeneric.Add(edfData);


            //edfFileGeneric.Save(filePath);

            return edfFileGeneric;
        }


        public  List<RawSKUInfo> GetCompanyRawSKUs(int companyID, bool IsESNRequired)
        {
            SV.Framework.DAL.Inventory.EsnAuthorizationOperation esnAuthorizationOperation = SV.Framework.DAL.Inventory.EsnAuthorizationOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnAuthorizationOperation>();

            List<RawSKUInfo> skuList = esnAuthorizationOperation.GetCompanyRawSKUs(companyID, IsESNRequired);
            
            return skuList;
        }


        public  int ESNAuthorizationInsert(List<ESNAuthorization> esnList, List<EsnUploadNew> esnauthList, int ItemCompanyGUID, int userID, 
            int KittedItemCompanyGUID, string RunNumber, string PlannedProvisioingDate, out int ESNAuthorizationID)
        {
            SV.Framework.DAL.Inventory.EsnAuthorizationOperation esnAuthorizationOperation = SV.Framework.DAL.Inventory.EsnAuthorizationOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnAuthorizationOperation>();

            ESNAuthorizationID = 0;
            int sequenceNumber = esnAuthorizationOperation.ESNAuthorizationInsert(esnList, esnauthList, ItemCompanyGUID, userID, KittedItemCompanyGUID, RunNumber, PlannedProvisioingDate, out ESNAuthorizationID);
            
            
            return sequenceNumber;
        }

        public  List<ESNAuthorization> GetESNAuthorizations(List<ESNAuthorization> ESNs, int companyID)
        {
            SV.Framework.DAL.Inventory.EsnAuthorizationOperation esnAuthorizationOperation = SV.Framework.DAL.Inventory.EsnAuthorizationOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnAuthorizationOperation>();

            List<ESNAuthorization> esnList = esnAuthorizationOperation.GetESNAuthorizations(ESNs, companyID);

            return esnList;
        }
        public  List<ESNAuthorization> GetESNAuthorizations(int ESNHeaderID, int ItemCompanyGUID, int ServiceOrderID, out string SequenceNumber)
        {
            SV.Framework.DAL.Inventory.EsnAuthorizationOperation esnAuthorizationOperation = SV.Framework.DAL.Inventory.EsnAuthorizationOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnAuthorizationOperation>();

            SequenceNumber = "";
            List<ESNAuthorization> esnList = esnAuthorizationOperation.GetESNAuthorizations(ESNHeaderID, ItemCompanyGUID, ServiceOrderID, out SequenceNumber);

            return esnList;
        }
        public bool ValidateESNs(List<ESNAuthorization> EsnList, int esnLength, int decLength, out string returnMessage)
        {
            bool IsValidate = true;
            System.Text.StringBuilder InvalidEsnSB = new System.Text.StringBuilder();
            System.Text.StringBuilder esnLenSB = new System.Text.StringBuilder();
            System.Text.StringBuilder decLenSB = new System.Text.StringBuilder();
            System.Text.StringBuilder esnNumSB = new System.Text.StringBuilder();
            System.Text.StringBuilder decNumSB = new System.Text.StringBuilder();
            System.Text.StringBuilder hexNumSB = new System.Text.StringBuilder();
            System.Text.StringBuilder esnDupSB = new System.Text.StringBuilder();
            System.Text.StringBuilder decDupSB = new System.Text.StringBuilder();
            System.Text.StringBuilder hexDupSB = new System.Text.StringBuilder();


            returnMessage = "";

            foreach(ESNAuthorization item in EsnList)
            {
                if(!Luhn.checkLuhn(item.ESN))
                {
                    InvalidEsnSB.Append(item.ESN + ",");
                }
            }
            if(InvalidEsnSB != null && InvalidEsnSB.ToString().Length > 0)
            {
                InvalidEsnSB.Append(" invalid ESN(s) </br>");
                IsValidate = false;
            }


            if (esnLength > 0)
            {
                var esnWithInvalidLen = (from item in EsnList where item.ESN.Length != esnLength select item).ToList();
                if (esnWithInvalidLen.Count > 0)
                {
                    foreach (var item in esnWithInvalidLen)
                        esnLenSB.Append(item.ESN + ",");

                    esnLenSB.Append(" ESN(s) length should be " + esnLength + " </br>");
                    IsValidate = false;
                }
            }
            if (decLength > 0)
            {
                List<ESNAuthorization> decWithInvalidLen = (from item in EsnList where item.MeidDec.Length != decLength select item).ToList();
                if (decWithInvalidLen.Count > 0)
                {
                    foreach (ESNAuthorization item in decWithInvalidLen)
                    {
                        decLenSB.Append(item.MeidDec+",");
                    }
                    decLenSB.Append(" DEC(s) length should be " + decLength + " </br>");
                    
                    IsValidate = false;
                }
            }
            var esnWithInvalidNumber = (from item in EsnList where !IsWholeNumber(item.ESN) select item).ToList();
            if (esnWithInvalidNumber.Count > 0)
            {
                foreach (var item in esnWithInvalidNumber)
                    esnNumSB.Append(item.ESN + ",");

                //esnNumSB.Append(String.Join(",", esnWithInvalidNumber));

                esnNumSB.Append(" ESN(s) having value other than numbers </br>");
                IsValidate = false;
            }
            var decWithInvalidNumber = (from item in EsnList where !IsWholeNumber(item.MeidDec) select item).ToList();
            if (decWithInvalidNumber.Count > 0)
            {
                foreach (var item in decWithInvalidNumber)
                    esnLenSB.Append(item.MeidDec + ",");

                //decNumSB.Append(String.Join(",", decWithInvalidNumber));
                decNumSB.Append(" DEC(s) having value other than numbers </br>");
                IsValidate = false;

            }
            var hexWithInvalidNumber = (from item in EsnList where !IsWholeNumber(item.MeidHex) select item).ToList();
            if (hexWithInvalidNumber.Count > 0)
            {
                foreach (var item in hexWithInvalidNumber)
                    hexNumSB.Append(item.MeidHex + ",");

               // hexNumSB.Append(String.Join(",", hexWithInvalidNumber));

                hexNumSB.Append( " HEX(s) having value other than numbers </br>");
                IsValidate = false;

            }

            //var esnWithInvalidNumber2 = (from item in EsnList where !item.MeidHex.Any(char.IsDigit) select item).ToList();

            // EsnList.Any(char.IsDigit);


            var ESNs = EsnList.GroupBy(e => new { e.ESN }).Where(g => g.Count() > 1).ToList();
            if (ESNs.Count > 0)
            {
                foreach (var item in ESNs)
                    esnDupSB.Append(item.Key.ESN + ",");

                //esnDupSB.Append(String.Join(",", ESNs));
                esnDupSB.Append(" Duplicate ESN(s) exists in the file  </br>");
                IsValidate = false;
            }
            var Decs = EsnList.GroupBy(e => new { e.MeidDec }).Where(g => g.Count() > 1).ToList();
            if (Decs.Count > 0 && !string.IsNullOrEmpty(Decs[0].Key.MeidDec))
            {
                foreach (var item in Decs)
                    decDupSB.Append(item.Key.MeidDec + ",");

                //decDupSB.Append(String.Join(",", Decs));
                decDupSB.Append(" Duplicate DEC(s) exists in the file  </br>");
                IsValidate = false;

            }
            var Hecs = EsnList.GroupBy(e => new { e.MeidHex }).Where(g => g.Count() > 1).ToList();
            if (Hecs.Count > 0 && !string.IsNullOrEmpty(Hecs[0].Key.MeidHex))
            {
                foreach (var item in Hecs)
                    hexDupSB.Append(item.Key.MeidHex + ",");

                //hexDupSB.Append(String.Join(",", Hecs.ToList()));
                hexDupSB.Append("Duplicate HEX(s) exists in the file  </br>");
                IsValidate = false;
            }

            returnMessage = InvalidEsnSB.ToString() + esnLenSB.ToString() + decLenSB.ToString() + esnNumSB.ToString() + decNumSB.ToString() + hexNumSB.ToString() + esnDupSB.ToString() + decDupSB.ToString() + hexDupSB.ToString();
            //query[0].


            return IsValidate;
        }
        public  bool ValidateUEDFFileData(List<ESNAuthorization> EsnList, int SequenceNumber, out string returnMessage)
        {
            bool IsValidate = true;

            //System.Text.StringBuilder InvalidEsnSB = new System.Text.StringBuilder();
            System.Text.StringBuilder errorSB = new System.Text.StringBuilder();
            
            returnMessage = "";
            if(SequenceNumber == 0)
            {
                errorSB.Append(" Sequence number cannot be 0! </br>");
                IsValidate = false;
            }
            var ManfIDs = (from item in EsnList where string.IsNullOrWhiteSpace(item.ManfId) select item).ToList();
            if (ManfIDs.Count > 0)
            {
                errorSB.Append(" Manufacturing code cannot be empty! </br>");
                IsValidate = false;
            }
            var ManfNames = (from item in EsnList where string.IsNullOrWhiteSpace(item.ManfName) select item).ToList();
            if (ManfNames.Count > 0)
            {
                errorSB.Append(" Manufacturing name cannot be empty! </br>");
                IsValidate = false;
            }
            var esns = (from item in EsnList where string.IsNullOrWhiteSpace(item.ESN) select item).ToList();
            if (esns.Count > 0)
            {
                errorSB.Append(" ESN cannot be empty! </br>");
                IsValidate = false;
            }
            var decs = (from item in EsnList where string.IsNullOrWhiteSpace(item.MeidDec) select item).ToList();
            if (decs.Count > 0)
            {
                errorSB.Append(" DEC cannot be empty! </br>");
                IsValidate = false;
            }
            var hexs = (from item in EsnList where string.IsNullOrWhiteSpace(item.MeidHex) select item).ToList();
            if (hexs.Count > 0)
            {
                errorSB.Append(" HEX cannot be empty! </br>");
                IsValidate = false;
            }
            var SKUs = (from item in EsnList where string.IsNullOrWhiteSpace(item.SKU) select item).ToList();
            if (SKUs.Count > 0)
            {
                errorSB.Append(" SKU cannot be empty! </br>");
                IsValidate = false;
            }
            var skuNames = (from item in EsnList where string.IsNullOrWhiteSpace(item.SKUName) select item).ToList();
            if (skuNames.Count > 0)
            {
                errorSB.Append(" SKU name cannot be empty! </br>");
                IsValidate = false;
            }
            var SWVersions = (from item in EsnList where string.IsNullOrWhiteSpace(item.SWVersion) select item).ToList();
            if (SWVersions.Count > 0)
            {
                errorSB.Append(" SWVersion cannot be empty! </br>");
                IsValidate = false;
            }
            
            returnMessage = errorSB.ToString();
            //query[0].


            return IsValidate;
        }

        public  bool IsWholeNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }

        public  List<KittedSKUs> GetAuthorizationKittedSKU(int ESNHeaderID)
        {
            SV.Framework.DAL.Inventory.EsnAuthorizationOperation esnAuthorizationOperation = SV.Framework.DAL.Inventory.EsnAuthorizationOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnAuthorizationOperation>();

            List<KittedSKUs> skuList = esnAuthorizationOperation.GetAuthorizationKittedSKU(ESNHeaderID);

            
            return skuList;
        }
        public  List<KittedSKUs> GetKittedSKUByRawSKU(int ItemCompanyGUID)
        {
            SV.Framework.DAL.Inventory.EsnAuthorizationOperation esnAuthorizationOperation = SV.Framework.DAL.Inventory.EsnAuthorizationOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnAuthorizationOperation>();

            List<KittedSKUs> skuList = esnAuthorizationOperation.GetKittedSKUByRawSKU(ItemCompanyGUID);

           
            return skuList;
        }

        public  List<KittedSKUs> GetESNAuthorizedSKUs(int CompanyID)
        {
            SV.Framework.DAL.Inventory.EsnAuthorizationOperation esnAuthorizationOperation = SV.Framework.DAL.Inventory.EsnAuthorizationOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnAuthorizationOperation>();

            List<KittedSKUs> skuList = esnAuthorizationOperation.GetESNAuthorizedSKUs(CompanyID);

           
            return skuList;
        }

        public  List<ESNAuthorizatedInfo> GetESNAuthorizedSearch(int CompanyID, int ItemCompanyGUID, string DateFrom, string DateTo, string runNumber)
        {
            SV.Framework.DAL.Inventory.EsnAuthorizationOperation esnAuthorizationOperation = SV.Framework.DAL.Inventory.EsnAuthorizationOperation.CreateInstance<SV.Framework.DAL.Inventory.EsnAuthorizationOperation>();

            List<ESNAuthorizatedInfo> authList = esnAuthorizationOperation.GetESNAuthorizedSearch(CompanyID, ItemCompanyGUID,  DateFrom,  DateTo,  runNumber);


            return authList;
        }

    }



}
