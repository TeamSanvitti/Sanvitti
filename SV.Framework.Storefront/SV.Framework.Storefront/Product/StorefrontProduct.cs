using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SV.Framework.Storefront.Product
{
    public class StorefrontProduct
    {
        public string requestType { get; set; }
        public string authCode { get; set; }
        public List<ProductRequest> requests { get; set; }
    }
    public class ProductRequest
    {
        public string sku { get; set; }
        public string locale { get; set; }
        public ProductInfo product { get; set; }
        public string condition { get; set; }
        public string conditionDescription { get; set; }
        public Availability availability { get; set; }

    }
    public class ProductInfo
    {
        public string title { get; set; }
        //public string locale { get; set; }
        //public Aspects aspects { get; set; }
        public Dictionary<string, string[]> aspects { get; set; }
        public string description { get; set; }
        public string[] imageUrls { get; set; }

    }
    //public class Aspects
    //{
    //    public Dictionary<string, string> keyValuePairs { get; set; }
        
    //}
    public class Availability
    {
        public ShipToLocationAvailability shipToLocationAvailability { get; set; }
    }
    public class ShipToLocationAvailability
    {
        public int quantity { get; set; }
    }



    public class ProductRequestModel
    {
        public int ItemGUID { get; set; }
        public string ProductSource { get; set; }
        public int CompanyID { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public string MakerName { get; set; }
        public string CreatedDate { get; set; }
        public string UserName { get; set; }
        public string ModifiedDate { get; set; }
        public int MakerGUID { get; set; }
        public string SKU { get; set; }
        public string UPC { get; set; }
        public string ModelNumber { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public decimal Weight { get; set; }
        public int OpeningStock { get; set; }
        public string CountryOrRegion { get; set; }
        public string Locale { get; set; }
        public string Condition { get; set; }
        public string SerialNumber { get; set; }
        public string ConditionDesc { get; set; }
        public string WeightDimension { get; set; }
        public string Location { get; set; }
        public string WarehouseCode { get; set; }
        public int MinimumStock { get; set; }
        public int MaximumStock { get; set; }
        public bool Active { get; set; }
        public bool IsSync { get; set; }
        public int UserID { get; set; }
    }

    public class AddProductResponse
    {
        [DisplayName("responses")]
        public List<ProductResponse> Responses { get; set; }
    }
    public class ProductResponse
    {
        [DisplayName("statusCode")]
        public string StatusCode { get; set; }
        [DisplayName("sku")]
        public string SKU { get; set; }
        [DisplayName("locale")]
        
        public string Locale { get; set; }
        [DisplayName("warnings")]
        public string[] Warnings { get; set; }
        [DisplayName("errors")]
        public string[] Errors { get; set; }
    }

    public class GetProductRequest
    {
       // [DisplayName("limit")]
        public int limit { get; set; }
       // [DisplayName("offset")]
        public int offset { get; set; }
       // [DisplayName("requestType")]

        public string requestType { get; set; }
      //  [DisplayName("authCode")]
        public string authCode { get; set; }
        
    }
    public class GeteBayProductResponse
    {
        public int total { get; set; }
        public int size { get; set; }
        public string href { get; set; }
        public string next { get; set; }
        public int limit { get; set; }
        public List<ProductRequest> inventoryItems { get; set; }
    }

}
