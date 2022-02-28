using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Catalog
{
    [XmlRoot(ElementName = "ModulePermission", IsNullable = true)]
    public class InventoryItemSizes
    {
        private int? itemGUID;
        private int? sizeGUID;
        private int? size_GUID;
        private string size;
        private CatalogEnums.AvailableType? avail;
        private string comment;
        private string _itemsizetype;
        private string weight;
        private int? _itemimageGUID;
        //private int colorGUID;
        //private string color;

        //[XmlElement(ElementName = "colorGUID", IsNullable = true)]
        //public int ColorGUID
        //{
        //    get
        //    {
        //        return colorGUID;
        //    }
        //    set
        //    {
        //        colorGUID = value;
        //    }
        //}
        //[XmlElement(ElementName = "color", IsNullable = true)]
        public string ItemSizetype
        {
            get
            {
                return _itemsizetype;
            }
            set
            {
                _itemsizetype = value;
            }
        }
        public string Weight
        {
            get
            {
                return weight;
            }
            set
            {
                weight = value;
            }
        }

        [XmlElement(ElementName = "itemGUID", IsNullable = true)]
        public int? ItemGUID
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
        [XmlElement(ElementName = "itemGUID", IsNullable = true)]
        public int? ItemimageGUID
        {
            get
            {
                return _itemimageGUID;
            }
            set
            {
                _itemimageGUID = value;
            }
        }
        [XmlElement(ElementName = "size_GUID", IsNullable = true)]
        public int? Size_GUID
        {
            get
            {
                return size_GUID;
            }
            set
            {
                size_GUID = value;
            }
        }


        [XmlElement(ElementName = "sizeGUID", IsNullable = true)]
        public int? SizeGUID
        {
            get
            {
                return sizeGUID;
            }
            set
            {
                sizeGUID = value;
            }
        }
        [XmlElement(ElementName = "sizeGUID", IsNullable = true)]
        public string Size
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
            }
        }
        [XmlElement(ElementName = "availableType", IsNullable = true)]
        public CatalogEnums.AvailableType? Available
        {
            get
            {
                return avail;
            }
            set
            {
                avail = value;
            }
        }

        [XmlElement(ElementName = "sizeText", IsNullable = true)]
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
    }

}
