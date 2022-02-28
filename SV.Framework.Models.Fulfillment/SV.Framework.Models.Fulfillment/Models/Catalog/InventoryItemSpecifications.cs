using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{
    public class InventoryItemSpecifications
    {
        private int specGuid;
        private string spec;
        private int itemGUID;

        public int SpecificationGuid
        {
            get
            {
                return specGuid;
            }
            set
            {
                specGuid = value;
            }
        }

        public string Specificaiton
        {
            get
            {
                return spec;
            }
            set
            {
                spec = value;
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

        //public int ItemGUID
        //{
        //    get
        //    {
        //        throw new System.NotImplementedException();
        //    }
        //    set
        //    {
        //    }
        //}
    }

}
