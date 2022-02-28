using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace SV.Framework.Models.Catalog
{
    [XmlRoot(ElementName = "ItemImaes", IsNullable = true)]
    public class InventoryItemImages
    {        
        private int? itemGUID;
        private int? imageGUID;
        private int? itemsizeGUID;
        private CatalogEnums.ImageType? imageType;
        private string imageName;
        private string imageURL;
        private string color;
        private int colorGuid;
        private string weight;
        [XmlElement(ElementName = "imageGUID", IsNullable = true)]
        public int? ImageGUID
        {
            get
            {
                return imageGUID;
            }
            set
            {
                imageGUID = value;
            }
        }
        [XmlElement(ElementName = "ItemSizeGUID", IsNullable = true)]
        public int? ItemSizeGUID
        {
            get
            {
                return itemsizeGUID;
            }
            set
            {
                itemsizeGUID = value;
            }
        }

        [XmlElement(ElementName = "weight", IsNullable = true)]
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

        [XmlElement(ElementName = "imageType", IsNullable = true)]
        public CatalogEnums.ImageType? ImageType
        {
            get
            {
                return imageType;
            }
            set
            {
                imageType = value;
            }
        }

        [XmlElement(ElementName = "imageName", IsNullable = true)]
        public string ImageName
        {
            get
            {
                return imageName;
            }
            set
            {
                imageName = value;
            }
        }

        [XmlElement(ElementName = "imageURL", IsNullable = true)]
        public string ImageURL
        {
            get
            {
                return imageURL;
            }
            set
            {
                imageURL = value;
            }
        }
        [XmlElement(ElementName = "imagecolor", IsNullable = true)]
        public string Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
        [XmlElement(ElementName = "imagecolorID", IsNullable = true)]
        public int ColorGUID
        {
            get
            {
                return colorGuid;
            }
            set
            {
                colorGuid = value;
            }
        }
    }

}
