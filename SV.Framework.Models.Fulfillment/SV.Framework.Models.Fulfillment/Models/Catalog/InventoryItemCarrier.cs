using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{
    public class InventoryItemCarrier
    {
        private int carGuid;
        private string carrierName;
        private string carrierLogo;

        public int CarrierGuid
        {
            get
            {
                return carGuid;
            }
            set
            {
                carGuid = value;
            }
        }

        public string CarrierName
        {
            get
            {
                return carrierName;
            }
            set
            {
                carrierName = value;
            }
        }

        public string CarrierLogo
        {
            get
            {
                return carrierLogo;
            }
            set
            {
                carrierLogo = value;
            }
        }
    }

}
