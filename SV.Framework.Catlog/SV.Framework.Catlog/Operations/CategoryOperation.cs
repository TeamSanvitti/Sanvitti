using System;
using System.Collections;
using System.Data;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace SV.Framework.Catalog
{

    public class CategoryOperation : BaseCreateInstance
    {
        public int CategoryDelete(int categoryGUID)
        {
            SV.Framework.DAL.Catalog.CategoryOperation categoryOperation = SV.Framework.DAL.Catalog.CategoryOperation.CreateInstance<SV.Framework.DAL.Catalog.CategoryOperation>();

            int returnValue = categoryOperation.CategoryDelete(categoryGUID);

            return returnValue;

        }

        public  int CategoryInsertUpdate(CategoryModel request, out string errorMessage)
        {
            SV.Framework.DAL.Catalog.CategoryOperation categoryOperation = SV.Framework.DAL.Catalog.CategoryOperation.CreateInstance<SV.Framework.DAL.Catalog.CategoryOperation>();

            errorMessage = string.Empty;
            int returnValue = categoryOperation.CategoryInsertUpdate(request, out errorMessage);
            return returnValue;

        }
        public  List<CategoryModel> GetCategoryList(CategoryModel model)
        {
            SV.Framework.DAL.Catalog.CategoryOperation categoryOperation = SV.Framework.DAL.Catalog.CategoryOperation.CreateInstance<SV.Framework.DAL.Catalog.CategoryOperation>();

            List<CategoryModel> categoryList = categoryOperation.GetCategoryList(model);// new List<CategoryModel>();
            return categoryList;
        }

        public  CategoryModel GetCategoryInfo(int categoryGUID)
        {
            SV.Framework.DAL.Catalog.CategoryOperation categoryOperation = SV.Framework.DAL.Catalog.CategoryOperation.CreateInstance<SV.Framework.DAL.Catalog.CategoryOperation>();

            CategoryModel categoryModel = categoryOperation.GetCategoryInfo(categoryGUID);// new CategoryModel();
            return categoryModel;
        }
        
    }
}
