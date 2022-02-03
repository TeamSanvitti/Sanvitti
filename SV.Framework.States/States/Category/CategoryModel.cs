using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.States
{
    public class CategoryModel
    {
        public int CategoryGUID { get; set; }
        public int IsEdit { get; set; }
        public int ParentCategoryGUID { get; set; }
        public string CategoryName { get; set; }
        public string P_CategoryName { get; set; }
        public string CategoryDesc { get; set; }
        public string CategoryImage { get; set; }
        public string Status { get; set; }
        public bool BaseCategories { get; set; }
        public bool LeafCategories { get; set; }
        public bool CategoryStatus { get; set; }
        public int CategoryStatusID { get; set; }
        public bool ESNRequired { get; set; }
        public bool KittedBox { get; set; }
        public bool SIMRequired { get; set; }
        public bool IsSIM { get; set; }
        public bool RMAAllowed { get; set; }
        public bool SKUMapping { get; set; }
        public bool ReStocking { get; set; }
        public bool Forecasting { get; set; }
        public int IsDelete { get; set; }



    }
}
