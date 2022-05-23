using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Fulfillment
{
   public class edfFileGeneric
    {
        public string date { get; set; }
       // public string productType { get; set; }
        public int fileSequence { get; set; }
        public int totalDeviceCount { get; set; }
        public int headerCount { get; set; }
        public edfData edfData { get; set; }
       
    }

    public class edfData
    {
        public edfHeader edfHeader { get; set; }
        public product product { get; set; }
        
    }
    public class edfHeader
    {
        public int deviceCount { get; set; }
        public string phoneType { get; set; }
        public string phoneOwnership { get; set; }
        public string transactionType { get; set; }
        public string poOrder { get; set; }
        public string locationDestination { get; set; }
        public string uedfRevisionNumber { get; set; }
    }
    public class product
    {
        public string edfSerialType { get; set; }
        public string entSerialType { get; set; }
        public skuInfo skuInfo { get; set; }
        public List<detail> details { get; set; }
    }
    public class skuInfo
    {
        public string sku { get; set; }
        public string skuName { get; set; }
        public string equipType { get; set; }
        public int manufId { get; set; }
        public string manufName { get; set; }
        public string preferredSerialIndicator { get; set; }
        public string masterSerialAttribute { get; set; }
        public string sfwVer { get; set; }
    }
    public class detail
    {
        public List<shipping> shippings { get; set; }
        public string lpn { get; set; }
        //public string fileSequence { get; set; }
    }
    public class shipping
    {
       // public string lpn { get; set; }
        public string carton { get; set; }
        public List<device> devices { get; set; }
    }
    public class device
    {
        public string meidHex { get; set; }
        public string meidDec { get; set; }
        public string imeiDec { get; set; }
        public string imeiDec2 { get; set; }
        public string serialNumber { get; set; }
        public string msl { get; set; }
        public string otksl { get; set; }
        


    }
    public class serialization
    {
        public string meidHex { get; set; }
        public string meidDec { get; set; }
        public string imeiDec { get; set; }
    }
}
