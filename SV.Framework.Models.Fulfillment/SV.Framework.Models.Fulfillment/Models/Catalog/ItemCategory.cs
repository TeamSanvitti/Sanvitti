using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{
    [Serializable]
    public class ItemCategory
    {
        private string _categoryDescription;
        private int _categoryGUID;
        private string _categoryImage;
        private string _categoryName;
        private string _parentcategoryName;
        private string _comments;
        private int _parentCategoryGUID;
        private int _active;
        private bool isESNRequired;
        public string CategoryWithProductAllowed { get; set; }
        public int CategoryGUID
        {
            get
            {
                return _categoryGUID;
            }
            set
            {
                _categoryGUID = value;
            }
        }

        public string ParentCategoryName
        {
            get
            {
                return _parentcategoryName;
            }
            set
            {
                _parentcategoryName = value;
            }
        }
        public string CategoryName
        {
            get
            {
                return _categoryName;
            }
            set
            {
                _categoryName = value;
            }
        }

        public string CategoryDescription
        {
            get
            {
                return _categoryDescription;
            }
            set
            {
                _categoryDescription = value;
            }
        }

        public string CategoryImage
        {
            get
            {
                return _categoryImage;
            }
            set
            {
                _categoryImage = value;
            }
        }

        public string Comments
        {
            get
            {
                return _comments;
            }
            set
            {
                _comments = value;
            }
        }

        public int ParentCategoryGUID
        {
            get
            {
                return _parentCategoryGUID;
            }
            set
            {
                _parentCategoryGUID = value;
            }
        }

        public int isActive
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }
        public bool IsESNRequired
        {
            get
            {
                return isESNRequired;
            }
            set
            {
                isESNRequired = value;
            }
        }
    }

}
