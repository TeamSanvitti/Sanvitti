using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Inventory
{
    [Serializable]
    public class CompanySKUno
    {
        private int? mappedItemCompanyGUID;
        private int itemcompanyGUID;
        private int itemGUID;
        private int companyID;
        private string sku;
        //private string massku;
        private string warehouseCode;
        private string companyname;
        private int poExists;
        private bool finishedSKU;
        private bool allowESN;
        private int categoryID;
        private int parentCategoryID;
        [XmlIgnore]
        public int CurrentStock { get; set; }

        [XmlIgnore]
        public bool IsDisable { get; set; }
        [XmlIgnore]
        public int MinimumStockLevel { get; set; }
        [XmlIgnore]
        public int MaximumStockLevel { get; set; }


        [XmlIgnore]
        public int ContainerQty { get; set; }
        [XmlIgnore]
        public int PalletQuantity { get; set; }
        [XmlIgnore]
        public string DPCI { get; set; }
        [XmlIgnore]
        public string SWVersion { get; set; }
        [XmlIgnore]
        public string HWVersion { get; set; }
        [XmlIgnore]
        public string BoxDesc { get; set; }

        ////private double price;
        [XmlIgnore]
        public int CategoryID
        {
            get
            {
                return categoryID;
            }
            set
            {
                categoryID = value;
            }
        }
        [XmlIgnore]
        public int ParentCategoryGUID
        {
            get
            {
                return parentCategoryID;
            }
            set
            {
                parentCategoryID = value;
            }
        }
        [XmlIgnore]
        public bool AllowESN
        {
            get
            {
                return allowESN;
            }
            set
            {
                allowESN = value;
            }
        }

        //public double Price
        //{
        //    get
        //    {
        //        return price;
        //    }
        //    set
        //    {
        //        price = value;
        //    }
        //}

        //public bool IsFinishedSKU
        //{
        //    get
        //    {
        //        return finishedSKU;
        //    }
        //    set
        //    {
        //        finishedSKU = value;
        //    }
        //}
        public int PoExists
        {
            get
            {
                return poExists;
            }
            set
            {
                poExists = value;
            }
        }

        public int ItemcompanyGUID
        {
            get
            {
                return itemcompanyGUID;
            }
            set
            {
                itemcompanyGUID = value;
            }
        }
        [XmlIgnore]
        public int? MappedItemCompanyGUID
        {
            get
            {
                return mappedItemCompanyGUID;
            }
            set
            {
                mappedItemCompanyGUID = value;
            }
        }

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
        public int CompanyID
        {
            get
            {
                return companyID;
            }
            set
            {
                companyID = value;
            }
        }
        public string SKU
        {
            get
            {
                return sku;
            }
            set
            {
                sku = value;
            }
        }
        [XmlIgnore]
        public string MappedSKU { get; set; }
        //public string MASSKU
        //{
        //    get
        //    {
        //        return massku;
        //    }
        //    set
        //    {
        //        massku = value;
        //    }
        //}
        public string WarehouseCode
        {
            get
            {
                return warehouseCode;
            }
            set
            {
                warehouseCode = value;
            }
        }
        public string CompanyName
        {
            get
            {
                return companyname;
            }
            set
            {
                companyname = value;
            }
        }
    }

}
