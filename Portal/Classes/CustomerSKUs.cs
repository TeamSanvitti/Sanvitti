using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace avii.Classes
{
    
    public class Brands
    {
        public string BrandName { get; set; }
        public List<ProductsCategory> CategoryList { get; set; }
            
    }

    public class ProductsCategory
    {
        public string CategoryName { get; set; }
        public List<PublicProduct> ProductList { get; set; }
        
    }
    public class PublicProduct
    {
        public string Color { get; set; }
        public string ProductCode { get; set; }
        public string ModelNumber { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string OperationalSystem { get; set; }
        public string ScreenSize { get; set; }
        public string CarrierName { get; set; }
        public string DocumentPath { get; set; }
        public string ProductCondition { get; set; }
        public bool ShowunderCatalog { get; set; }
        public string UPC { get; set; }
        public int DisplayPriority { get; set; }
        public string SpintorLockTypeText { get; set; }
        public string SimCardTypeText { get; set; }
        public decimal Weight { get; set; }
        public List<ProductsImage> ImageList { get; set; }
        public List<ProductsAttribute> AttributeList { get; set; }
        public List<ProductSpecification> SpecificationList { get; set; }
        public List<ProductCamera> CameraList { get; set; }



    }
    public class CustomerSKUs
    {
        public string SKU { get; set; }
        public string ProductCode { get; set; }
        public string ModelNumber { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string LongDescription { get; set; }
        public string Brand { get; set; }
        public string CategoryName { get; set; }
        public string CarrierName { get; set; }
        public string DocumentPath { get; set; }
        public string ProductCondition { get; set; }
        public bool ShowunderCatalog { get; set; }
        public string UPC { get; set; }
        public List<ProductsImage> ImageList { get; set; }
        public List<ProductsAttribute> AttributeList { get; set; }
        public string SKUDescription { get; set; }
        public int QtyAvailable { get; set; }
        public int ItemCompanyGUID { get; set; }

    }
}


public class ProductCamera
{
    public string CameraType { get; set; }
    public string Pixel { get; set; }
    public string Zoom { get; set; }
}

public class ProductsImage
{
    public string ImagePath { get; set; }
}

public class ProductSpecification
{
    public string Specification { get; set; }
}


public class ProductsAttribute
{
    public string AttributeName { get; set; }
    public string AttributeValue { get; set; }
}