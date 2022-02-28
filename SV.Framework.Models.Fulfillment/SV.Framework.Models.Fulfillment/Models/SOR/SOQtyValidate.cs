using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.SOR
{
    [Serializable]
    public class SOQtyValidate
    {
        private int id;
        private int iItemCompanyGUID;
        private int qty;
        private string startESN;
        private string endESN;
        public int Qty
        {
            get
            {
                return qty;
            }
            set
            {
                qty = value;
            }
        }
        public int Id
        {
            get
            {
                return id;
            }
            set
            {
                id = value;
            }
        }
        public int ItemCompanyGUID
        {
            get
            {
                return iItemCompanyGUID;
            }
            set
            {
                iItemCompanyGUID = value;
            }
        }
        public string StartESN
        {
            get
            {
                return startESN;
            }
            set
            {
                startESN = value;
            }
        }
        public string EndESN
        {
            get
            {
                return endESN;
            }
            set
            {
                endESN = value;
            }
        }





    }

}
