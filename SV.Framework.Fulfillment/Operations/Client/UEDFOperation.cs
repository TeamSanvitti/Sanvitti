using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace SV.Framework.Fulfillment
{
    public class UEDFOperation : BaseCreateInstance
    {        
        public  XElement CreateUEDFFile(edfFileGeneric edfFileInfo)
        {
            string filePrefix = "spappledsh";
            string transDate = edfFileInfo.date.Replace("-","");

            string fileName = filePrefix +"_" + transDate + "_"+ edfFileInfo.fileSequence.ToString() + ".xml";

            string filePath = "c:/xml/" + fileName;

            XNamespace tns = "http://integration.sprint.com/interfaces/UEDF/v1/UEDF.xsd";
            XElement edfFileGeneric = new XElement(tns + "edfFileGeneric",
                new XAttribute(XNamespace.Xmlns + "tns", "http://integration.sprint.com/interfaces/UEDF/v1/UEDF.xsd"));

            XElement date = new XElement(tns + "date", edfFileInfo.date);                
            edfFileGeneric.Add(date);

            XElement fileSequence = new XElement(tns + "fileSequence", edfFileInfo.fileSequence);
            edfFileGeneric.Add(fileSequence);

            XElement totalDeviceCount = new XElement(tns + "totalDeviceCount", edfFileInfo.totalDeviceCount);
            edfFileGeneric.Add(totalDeviceCount);

            XElement headerCount = new XElement(tns + "headerCount", edfFileInfo.headerCount);
            edfFileGeneric.Add(headerCount);

            string[] array = edfFileInfo.edfData.edfHeader.poOrder.Split('-');
            string poOrder = array[0];
            XElement edfData = new XElement(tns + "edfData");

            XElement edfHeader = new XElement(tns + "edfHeader",
                       new XElement(tns + "deviceCount", edfFileInfo.totalDeviceCount),
                       new XElement(tns + "phoneType", edfFileInfo.edfData.edfHeader.phoneType),
                       new XElement(tns + "phoneOwnership", edfFileInfo.edfData.edfHeader.phoneOwnership),
                       new XElement(tns + "transactionType", edfFileInfo.edfData.edfHeader.transactionType),
                       new XElement(tns + "poOrder", edfFileInfo.edfData.edfHeader.poOrder),
                       new XElement(tns + "factOrder", edfFileInfo.edfData.edfHeader.factOrder),
                       new XElement(tns + "locationDestination", edfFileInfo.edfData.edfHeader.locationDestination),
                       new XElement(tns + "uedfRevisionNumber", edfFileInfo.edfData.edfHeader.uedfRevisionNumber)

                       );
            edfData.Add(edfHeader);

            XElement product = new XElement(tns + "product");
            
            XElement edfSerialType = new XElement(tns + "edfSerialType", edfFileInfo.edfData.product.edfSerialType);
            product.Add(edfSerialType);

            //XElement entSerialType = new XElement(tns + "entSerialType", edfFileInfo.edfData.product.entSerialType);
            //product.Add(entSerialType);

            XElement skuInfo = new XElement(tns + "skuInfo",
                          new XElement(tns + "sku", edfFileInfo.edfData.product.skuInfo.sku),
                          new XElement(tns + "skuName", edfFileInfo.edfData.product.skuInfo.skuName),
                          new XElement(tns + "equipType", edfFileInfo.edfData.product.skuInfo.equipType),
                          new XElement(tns + "manufId", edfFileInfo.edfData.product.skuInfo.manufId),
                          new XElement(tns + "manufName", edfFileInfo.edfData.product.skuInfo.manufName),
                          //new XElement(tns + "preferredSerialIndicator", edfFileInfo.edfData.product.skuInfo.preferredSerialIndicator),
                          //new XElement(tns + "masterSerialAttribute", edfFileInfo.edfData.product.skuInfo.masterSerialAttribute),
                          new XElement(tns + "sfwVer", edfFileInfo.edfData.product.skuInfo.sfwVer)
                          );
            product.Add(skuInfo);

            

            //loop start from here for multi details
            //XElement shipping = new XElement(tns + "shipping");
            
            List<detail> details = edfFileInfo.edfData.product.details;
            foreach (detail detail1 in details)
            {                
                foreach (shipping shipping1 in detail1.shippings)
                {
                    XElement detail = new XElement(tns + "detail");
                    XElement shipping = new XElement(tns + "shipping");
                    XElement lpn = new XElement(tns + "lpn", detail1.lpn.Replace("(", "").Replace(")", ""));
                    shipping.Add(lpn);

                    XElement carton = new XElement(tns + "carton", shipping1.carton.Replace("(", "").Replace(")", ""));
                    shipping.Add(carton);

                    detail.Add(shipping);
                    foreach (device device1 in shipping1.devices)
                    {
                        XElement device = new XElement(tns + "device");
                        XElement serialization = new XElement(tns + "serialization");

                        if (edfFileInfo.edfData.product.edfSerialType.ToUpper() == "H")
                        {
                            XElement meidHex = new XElement(tns + "meidHex", device1.meidHex);
                            serialization.Add(meidHex);

                            XElement meidDec = new XElement(tns + "meidDec", device1.meidDec);
                            serialization.Add(meidDec);

                            XElement imeiDec = new XElement(tns + "imeiDec", device1.imeiDec);
                            serialization.Add(imeiDec);
                        }
                        else if (edfFileInfo.edfData.product.edfSerialType.ToUpper() == "H5")
                        {
                            XElement meidHex = new XElement(tns + "meidHex", device1.meidHex);
                            serialization.Add(meidHex);

                            XElement meidDec = new XElement(tns + "meidDec", device1.meidDec);
                            serialization.Add(meidDec);

                            XElement imeiDec = new XElement(tns + "imeiDec", device1.imeiDec);
                            serialization.Add(imeiDec);

                            XElement imeiDec2 = new XElement(tns + "imeiDec2", device1.imeiDec2);
                            serialization.Add(imeiDec2);
                        }
                        else if (edfFileInfo.edfData.product.edfSerialType.ToUpper() == "H3")
                        {
                            XElement imeiDec = new XElement(tns + "imeiDec", device1.imeiDec);
                            serialization.Add(imeiDec);

                        }

                        //XElement serialNumber = new XElement(tns + "serialNumber", device1.serialNumber);
                        //serialization.Add(serialNumber);
                        XElement authentication = new XElement(tns + "authentication");

                        XElement msl = new XElement(tns + "msl", device1.msl);
                        authentication.Add(msl);
                        XElement otksl = new XElement(tns + "otksl", device1.otksl);
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
        public  void CreateUEDFFile()
        {
            string fileName = DateTime.Now.Ticks.ToString() + ".xml";
            string filePath = "c:/xml/" + fileName;

            XNamespace tns = "http://integration.sprint.com/interfaces/UEDF/v1/UEDF.xsd";
            XElement edfFileGeneric = new XElement(tns + "edfFileGeneric",
                new XAttribute(XNamespace.Xmlns + "tns", "http://integration.sprint.com/interfaces/UEDF/v1/UEDF.xsd"));

            XElement date = new XElement(tns + "date", DateTime.Now);

            edfFileGeneric.Add(date);

            XElement fileSequence = new XElement(tns + "fileSequence", 12345);

            edfFileGeneric.Add(fileSequence);
            XElement totalDeviceCount = new XElement(tns + "totalDeviceCount", 120);

            edfFileGeneric.Add(totalDeviceCount);
            XElement headerCount = new XElement(tns + "headerCount", 1);

            edfFileGeneric.Add(headerCount);


            XElement edfData = new XElement(tns + "edfData");

            XElement edfHeader = new XElement(tns + "edfHeader",
                       new XElement(tns + "deviceCount", 1),
                       new XElement(tns + "phoneType", "NW"),
                       new XElement(tns + "phoneOwnership", "DISH"),
                       new XElement(tns + "transactionType", "010"),
                       new XElement(tns + "poOrder", "TESTPO001"),
                       new XElement(tns + "locationDestination", "022"),
                       new XElement(tns + "uedfRevisionNumber", "XMLSHIP")

                       );

            edfData.Add(edfHeader);

            XElement product = new XElement(tns + "product");

            XElement edfSerialType = new XElement(tns + "edfSerialType", "H5");
            product.Add(edfSerialType);

            XElement entSerialType = new XElement(tns + "entSerialType", "E");
            product.Add(entSerialType);

            XElement skuInfo = new XElement(tns + "skuInfo",
                          new XElement(tns + "sku", "190198776525"),
                          new XElement(tns + "skuName", "IPH XR BK128 NBST BOXSGL"),
                          new XElement(tns + "equipType", "CP"),
                          new XElement(tns + "manufId", "436"),
                          new XElement(tns + "manufName", "APPLE, INC"),
                          new XElement(tns + "preferredSerialIndicator", "imeiDec"),
                          new XElement(tns + "masterSerialAttribute", "imeiDec")
                          );
            product.Add(skuInfo);


            //loop start from here for multi details
            XElement detail = new XElement(tns + "detail");

            XElement shipping = new XElement(tns + "shipping");
            XElement lpn = new XElement(tns + "lpn", "111111111111111111");
            shipping.Add(lpn);

            XElement carton = new XElement(tns + "carton", "2222222222222222222222");
            shipping.Add(carton);

            detail.Add(shipping);

            XElement device = new XElement(tns + "device");
            XElement serialization = new XElement(tns + "serialization");
            XElement meidHex = new XElement(tns + "meidHex", "333333333333333333333333");
            serialization.Add(meidHex);
            XElement meidDec = new XElement(tns + "meidDec", "44444444444444444444444");
            serialization.Add(meidDec);
            XElement imeiDec = new XElement(tns + "imeiDec", "55555555555555555555555");
            serialization.Add(imeiDec);
            device.Add(serialization);
            detail.Add(device);


            XElement device2 = new XElement(tns + "device");
            XElement serialization2 = new XElement(tns + "serialization");
            XElement meidHex2 = new XElement(tns + "meidHex", "6666666666666666666666");
            serialization2.Add(meidHex2);
            XElement meidDec2 = new XElement(tns + "meidDec", "7777777777777777777777");
            serialization2.Add(meidDec2);
            XElement imeiDec2 = new XElement(tns + "imeiDec", "8888888888888888888");
            serialization2.Add(imeiDec2);
            device2.Add(serialization2);
            detail.Add(device2);




            product.Add(detail);
            edfData.Add(product);

            edfFileGeneric.Add(edfData);


            edfFileGeneric.Save(filePath);

            //return edfFileGeneric;
        }
        public  void CreateUEDFXML()
        {
            string fileName = DateTime.Now.Ticks.ToString() + ".xml";
            string filePath = "c:/xml/" + fileName;

            XNamespace tns = "http://integration.sprint.com/interfaces/UEDF/v1/UEDF.xsd";
            XElement edfFileGeneric = new XElement(tns + "edfFileGeneric",
                new XAttribute(XNamespace.Xmlns + "tns", "http://integration.sprint.com/interfaces/UEDF/v1/UEDF.xsd"),
                new XElement(tns + "date", DateTime.Now),
                new XElement(tns + "fileSequence", 12345),
                new XElement(tns + "totalDeviceCount", 120),
                new XElement(tns + "headerCount", 1),
                new XElement(tns + "edfData",
                    new XElement(tns + "edfHeader",
                       new XElement(tns + "deviceCount", 1),
                       new XElement(tns + "phoneType", "NW"),
                       new XElement(tns + "phoneOwnership", "DISH"),
                       new XElement(tns + "transactionType", "010"),
                       new XElement(tns + "poOrder", "TESTPO001"),
                       new XElement(tns + "locationDestination", "022"),
                       new XElement(tns + "uedfRevisionNumber", "XMLSHIP")

                       ),
                    new XElement(tns + "product",
                       new XElement(tns + "edfSerialType", "H5"),
                       new XElement(tns + "entSerialType", "E"),

                       new XElement(tns + "skuInfo",
                           new XElement(tns + "sku", "190198776525"),
                           new XElement(tns + "skuName", "IPH XR BK128 NBST BOXSGL"),
                           new XElement(tns + "equipType", "CP"),
                           new XElement(tns + "manufId", "436"),
                           new XElement(tns + "manufName", "APPLE, INC"),
                           new XElement(tns + "preferredSerialIndicator", "imeiDec"),
                           new XElement(tns + "masterSerialAttribute", "imeiDec")
                           ),

                       new XElement(tns + "detail",
                           new XElement(tns + "sku", "190198776525"),
                           new XElement(tns + "skuName", "IPH XR BK128 NBST BOXSGL"),
                           new XElement(tns + "equipType", "CP"),
                           new XElement(tns + "manufId", "436"),
                           new XElement(tns + "manufName", "APPLE, INC"),
                           new XElement(tns + "preferredSerialIndicator", "imeiDec"),
                           new XElement(tns + "masterSerialAttribute", "imeiDec")
                           )



                       )


                )


            );

            edfFileGeneric.Save(filePath);


            //Console.WriteLine(root);
        }
        public  bool ValidateUEDFFileData(edfFileGeneric edfFileInfo, out string returnMessage)
        {SV.Framework.DAL.Fulfillment.UEDFOperation urdfOperation = SV.Framework.DAL.Fulfillment.UEDFOperation.CreateInstance<SV.Framework.DAL.Fulfillment.UEDFOperation>();

            bool IsValidate = true;

            //System.Text.StringBuilder InvalidEsnSB = new System.Text.StringBuilder();
            System.Text.StringBuilder errorSB = new System.Text.StringBuilder();

            returnMessage = "";
            if (edfFileInfo.fileSequence == 0)
            {
                errorSB.Append(" Sequence number cannot be 0! </br>");
                IsValidate = false;
            }
            if (edfFileInfo.edfData.product.skuInfo.manufId==0)
            {
                errorSB.Append(" Manufacturing code cannot be empty! </br>");
                IsValidate = false;
            }
            if (string.IsNullOrWhiteSpace(edfFileInfo.edfData.product.skuInfo.manufName))
            {
                errorSB.Append(" Manufacturing name cannot be empty! </br>");
                IsValidate = false;
            }
            if (string.IsNullOrWhiteSpace(edfFileInfo.edfData.edfHeader.poOrder))
            {
                errorSB.Append(" PO Order cannot be empty! </br>"); 
                IsValidate = false;
            }
            if (string.IsNullOrWhiteSpace(edfFileInfo.edfData.edfHeader.factOrder))
            {
                errorSB.Append(" Fact Order cannot be empty! </br>");
                IsValidate = false;
            }
            
            if (string.IsNullOrWhiteSpace(edfFileInfo.edfData.product.skuInfo.sku))
            {
                errorSB.Append(" SKU cannot be empty! </br>");
                IsValidate = false;
            }
            if (string.IsNullOrWhiteSpace(edfFileInfo.edfData.product.skuInfo.skuName))
            {
                errorSB.Append(" SKU name cannot be empty! </br>");
                IsValidate = false;
            }
            if (string.IsNullOrWhiteSpace(edfFileInfo.edfData.product.skuInfo.sfwVer))
            {
                errorSB.Append(" SWVersion cannot be empty! </br>");
                IsValidate = false;
            }
            if (string.IsNullOrWhiteSpace(edfFileInfo.edfData.product.skuInfo.equipType))
            {
                errorSB.Append(" equipType cannot be empty! </br>");
                IsValidate = false;
            }
            if (string.IsNullOrWhiteSpace(edfFileInfo.edfData.product.edfSerialType))
            {
                errorSB.Append(" edfSerialType cannot be empty! </br>");
                IsValidate = false;
            }
            if (string.IsNullOrWhiteSpace(edfFileInfo.date))
            {
                errorSB.Append(" Date cannot be empty! </br>");
                IsValidate = false;
            }
            if (edfFileInfo.totalDeviceCount == 0)
            {
                errorSB.Append(" Device count cannot be 0! </br>");
                IsValidate = false;
            }
            
            if (edfFileInfo.headerCount == 0)
            {
                errorSB.Append(" Header count cannot be 0! </br>");
                IsValidate = false;
            }
            if (edfFileInfo.edfData.product.details != null)
            {
                List<detail> details = edfFileInfo.edfData.product.details;

                var pallets = (from item in details where string.IsNullOrWhiteSpace(item.lpn) select item).ToList();
                if (pallets.Count > 0)
                {
                    errorSB.Append(" lpn cannot be empty! </br>");
                    IsValidate = false;
                }
                bool IsValidation = true;
                foreach (detail detail1 in details)
                {
                    if(!IsValidation)
                    {
                        break;
                    }
                    var cartons = (from item in detail1.shippings where string.IsNullOrWhiteSpace(item.carton) select item).ToList();
                    if (cartons.Count > 0)
                    {
                        errorSB.Append(" Carton cannot be empty! </br>");
                        IsValidate = false;
                        IsValidation = false;
                        //break;
                    }

                    foreach (shipping shipping1 in detail1.shippings)
                    {
                        var ESNs = (from item in shipping1.devices where string.IsNullOrWhiteSpace(item.imeiDec) select item).ToList();
                        if (ESNs.Count > 0)
                        {
                            errorSB.Append(" imeiDec cannot be empty! </br>");
                            IsValidate = false;
                            IsValidation = false;
                            break;
                        }
                        var HEXs = (from item in shipping1.devices where string.IsNullOrWhiteSpace(item.meidHex) select item).ToList();
                        if (HEXs.Count > 0)
                        {
                            errorSB.Append(" meidHex cannot be empty! </br>");
                            IsValidate = false;
                            IsValidation = false;
                            break;
                        }
                        var DECs = (from item in shipping1.devices where string.IsNullOrWhiteSpace(item.meidDec) select item).ToList();
                        if (DECs.Count > 0)
                        {
                            errorSB.Append(" meidDec cannot be empty! </br>");
                            IsValidate = false;
                            IsValidation = false;
                            break;
                        }
                        //if (string.IsNullOrWhiteSpace(detail1.lpn))
                        //{
                        //    errorSB.Append(" lpn cannot be empty! </br>");
                        //    IsValidate = false;
                        //}
                        //if (string.IsNullOrWhiteSpace(shipping1.carton))
                        //{
                        //    errorSB.Append(" Carton cannot be empty! </br>");
                        //    IsValidate = false;
                        //}

                        //foreach (device device1 in shipping1.devices)
                        //{
                            //if (string.IsNullOrWhiteSpace(device1.meidHex))
                            //{
                            //    errorSB.Append(" meidHex cannot be empty! </br>");
                            //    IsValidate = false;
                            //}
                            //if (string.IsNullOrWhiteSpace(device1.meidDec))
                            //{
                            //    errorSB.Append(" meidDec cannot be empty! </br>");
                            //    IsValidate = false;
                            //}
                            //if (string.IsNullOrWhiteSpace(device1.imeiDec))
                            //{
                            //    errorSB.Append(" imeiDec cannot be empty! </br>");
                            //    IsValidate = false;
                            //}
                            //if (string.IsNullOrWhiteSpace(device1.msl))
                            //{
                            //    errorSB.Append(" msl cannot be empty! </br>");
                            //    IsValidate = false;
                            //}
                            //if (string.IsNullOrWhiteSpace(device1.otksl))
                            //{
                            //    errorSB.Append(" otksl cannot be empty! </br>");
                            //    IsValidate = false;
                            //}
                        //}
                    }
                }
            }
            returnMessage = errorSB.ToString();
            
            return IsValidate;
        }

        public  edfFileGeneric GetUedfFileDetail (int  POID, string transDate)
        {
            SV.Framework.DAL.Fulfillment.UEDFOperation urdfOperation = SV.Framework.DAL.Fulfillment.UEDFOperation.CreateInstance<SV.Framework.DAL.Fulfillment.UEDFOperation>();

            edfFileGeneric edfFileInfo = urdfOperation.GetUedfFileDetail(POID, transDate);
            
            return edfFileInfo;
        }

        public  edfFileGeneric GetAuthorizationFileDetail(int POID)
        {
            SV.Framework.DAL.Fulfillment.UEDFOperation urdfOperation = SV.Framework.DAL.Fulfillment.UEDFOperation.CreateInstance<SV.Framework.DAL.Fulfillment.UEDFOperation>();

            edfFileGeneric edfFileInfo = urdfOperation.GetAuthorizationFileDetail(POID);

            return edfFileInfo;
        }
    }
}
