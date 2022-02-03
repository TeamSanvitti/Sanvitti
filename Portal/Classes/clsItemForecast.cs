using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace avii.Classes.Controller
{
    #region Enums
    public enum ForecastStatus
    {
        Open = 0,
        Locked = 1,
    }

    public enum AvItemType
    {
        OEM=1,
        Debrand=2,
        Future=3
    }
    #endregion

    #region Forecast
    [XmlRoot(ElementName = "ForecastInfo", IsNullable = true)]
    public class Forecast
    {
        private int? _forecastGuid;
        private DateTime? _forecastDate;
        private int? _itemID;
        private AvItemType avitemtype;
        private int? _statusID;
        private int? _qty;
        private int _poforecastGUID;


        // Pass 0 for new forecast;
        [XmlIgnore]
        public int POForecastGUID
        {
            get
            {
                return _poforecastGUID;
            }
            set
            {
                _poforecastGUID = value;
            }
        }

        
        [XmlElement(ElementName = "forecastGUID", IsNullable = true)]
        public int? ForecastGUID
        {
            get
            {
                return _forecastGuid;
            }
            set
            {
                _forecastGuid = value;
            }
        }

        [XmlElement(ElementName = "forecastdate", IsNullable = true)]
        public DateTime? ForecastDate
        {
            get
            {
                return _forecastDate;
            }
            set
            {
                _forecastDate = value;
            }
        }

        [XmlElement(ElementName = "itemID", IsNullable = true)]
        public int? ItemID
        {
            get
            {
                return _itemID;
            }
            set
            {
                _itemID = value;
            }
        }

        [XmlIgnore]
        [XmlElement(ElementName = "itemtype", IsNullable = true)]
        public avii.Classes.Controller.AvItemType ItemType
        {
            get
            {
                return avitemtype;
            }
            set
            {
                avitemtype = value;
            }
        }

        [XmlElement(ElementName = "forecastStatusID", IsNullable = true)]
        public int? StatusID
        {
            get
            {
                return _statusID;
            }
            set
            {
                _statusID = value;
            }
        }

        [XmlElement(ElementName = "qty", IsNullable = true)]
        public int? Qty
        {
            get
            {
                return _qty;
            }
            set
            {
                _qty = value;
            }
        }

        public void getForecast(int itemID)
        {
            //throw new System.NotImplementedException();
        }

    }
    #endregion

    #region Forecasts
    public class Forecasts
    {
        private List<Forecast> forecasts;

        public List<Forecast> ForecastList
        {
            get
            {
                return forecasts;
            }
            set
            {
                forecasts = value;
            }
        }

        public void getForecasts(int _itemID)
        {
            //throw new System.NotImplementedException();
        }
    }
    #endregion

    #region UserItem
    public class UserItem
    {
        private int _userID;
        private int _itemID;
        private string _itemSKU;
        private string _itemDesc;

        public int UserID
        {
            get
            {
                return _userID;
            }
            set
            {
                _userID = value;
            }
        }

        public int ItemID
        {
            get
            {
                return _itemID;
            }
            set
            {
                _itemID = value;
            }
        }

        public string ItemSKU
        {
            get
            {
                return _itemSKU;
            }
            set
            {
                _itemSKU = value;
            }
        }

        public string ItemDesc
        {
            get
            {
                return _itemDesc;
            }
            set
            {
                _itemDesc = value;
            }
        }

        public void getUserItem(int _userID)
        {
            //throw new System.NotImplementedException();
        }
    }
    #endregion

    #region ItemForecast
    public class ItemForecast
    {
        private int _itemID;
        private AvItemType itemTypeId;
        private List<avii.Classes.Controller.Forecast> _itemForecasts;
        private string _itemDesc;
        private int _createdBy;
        private DateTime _createdDate;
        private int _modifiedBy;
        private DateTime _modifiedDate;
        private string _itemSKU;
        private string upc;

        public ItemForecast()
        {
            //throw new System.NotImplementedException();
        }

        public System.Collections.Generic.List<Forecast> ItemForecasts
        {
            get
            {
                return _itemForecasts;
            }
            set
            {
                _itemForecasts = value;
            }
        }

        public int ItemTypeID
        {
            get
            {
                return (int)itemTypeId;
            }
            set
            {
                itemTypeId = (AvItemType)value;
            }
        }

        public AvItemType ItemType
        {
            get
            {
                return itemTypeId;
            }
            set
            {
                itemTypeId = value;
            }
        }

        public int ItemID
        {
            get
            {
                return _itemID;
            }
            set
            {
                _itemID = value;
            }
        }

        public string ItemDesc
        {
            get
            {
                return _itemDesc;
            }
            set
            {
                _itemDesc = value;
            }
        }

        public string UPC
        {
            get
            {
                return upc;
            }
            set
            {
                upc = value;
            }
        }

        public int CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                _createdBy = value;
            }
        }

        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }

        public int ModifiedBy
        {
            get
            {
                return _modifiedBy;
            }
            set
            {
                _modifiedBy = value;
            }
        }

        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }
            set
            {
                _modifiedDate = value;
            }
        }

        public string ItemSKU
        {
            get
            {
                return _itemSKU;
            }
            set
            {
                _itemSKU = value;
            }
        }

        public void getItemForecast(int _userID, int _itemID)
        {
            //throw new System.NotImplementedException();
        }
    }
    #endregion
}
