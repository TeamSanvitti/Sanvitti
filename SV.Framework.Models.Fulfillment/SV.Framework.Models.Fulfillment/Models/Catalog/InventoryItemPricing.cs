using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{
    public class InventoryItemPricing
    {
        private int itemGUID;
        private int pricingGUID;
        private int custTypeGuid;
        private string custType;
        private double webPrice;
        private double retailPrice;
        private double wholeSalePrice;
        private CatalogEnums.ItemPricingType priceType;
        private string _itemSize;
        private string _itemSizeprice;
        private string _itempricensize;
        private double itemprice;
        private int _itemsizeguid;
        private int _sizeguid;

        public int ItemGUID
        {
            get
            {
                return itemGUID;
            }
            set
            {
                itemGUID = value;
            }
        }
        public int SizeGUID
        {
            get
            {
                return _sizeguid;
            }
            set
            {
                _sizeguid = value;
            }
        }
        public int ItemsizeGUID
        {
            get
            {
                return _itemsizeguid;
            }
            set
            {
                _itemsizeguid = value;
            }
        }
        public int PricingGUID
        {
            get
            {
                return pricingGUID;
            }
            set
            {
                pricingGUID = value;
            }
        }

        public int CustTypeGuid
        {
            get
            {
                return custTypeGuid;
            }
            set
            {
                custTypeGuid = value;
            }
        }

        public string CustType
        {
            get
            {
                return custType;
            }
            set
            {
                custType = value;
            }
        }
        public string ItemSizeprice
        {
            get
            {
                return _itemSizeprice;
            }
            set
            {
                _itemSizeprice = value;
            }
        }
        public string ItemPricenSize
        {
            get
            {
                return _itempricensize;
            }
            set
            {
                _itempricensize = value;
            }
        }


        public double Itemprice
        {
            get
            {
                return itemprice;
            }
            set
            {
                itemprice = value;
            }
        }

        public double WebPrice
        {
            get
            {
                return webPrice;
            }
            set
            {
                webPrice = value;
            }
        }

        public double RetailPrice
        {
            get
            {
                return retailPrice;
            }
            set
            {
                retailPrice = value;
            }
        }

        public double WholeSalePrice
        {
            get
            {
                return wholeSalePrice;
            }
            set
            {
                wholeSalePrice = value;
            }
        }

        public CatalogEnums.ItemPricingType PriceType
        {
            get
            {
                return priceType;
            }
            set
            {
                priceType = value;
            }
        }

        public string ItemSize
        {
            get
            {
                return _itemSize;
            }
            set
            {
                _itemSize = value;
            }
        }
    }

}
