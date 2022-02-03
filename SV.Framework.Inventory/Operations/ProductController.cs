using System;
using System.Collections.Generic;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.Inventory
{
    public class ProductController : BaseCreateInstance
    {
        
        public List<ItemCategory> GetItemCategoryTree(int catID, int depth, int hasitems
            , bool withparent, int active, int parentcatID, bool withIndent, bool excludeKitted, bool OnlyNonEsn, bool IsEsnRequired)
        {

            if (0 == catID)
                catID = -1;
            List<ItemCategory> lstItemCategoryTree = new List<ItemCategory>();

            TraverseCategories(ref lstItemCategoryTree, GetItemsCategoryList(catID, "", hasitems, withparent, active
                , parentcatID, excludeKitted, OnlyNonEsn, IsEsnRequired), 0, 0, withIndent);

            return lstItemCategoryTree;
        }
        public List<ItemCategory> TraverseCategories(ref List<ItemCategory> itemcategoryListTree
            , List<ItemCategory> itemcategoryList, int catid, int depth, bool withIndent)
        {
            if (itemcategoryList != null && itemcategoryList.Count > 0)
            {
                //itemcategoryListTree = new List<ItemCategory>();
                for (int ictr = 0; ictr < itemcategoryList.Count; ictr++)
                {
                    if (catid == itemcategoryList[ictr].ParentCategoryGUID)
                    {
                        for (int j = 0; j < depth; j++)
                        {
                            if (withIndent)
                                itemcategoryList[ictr].CategoryName = "&nbsp;&nbsp;&nbsp;" + itemcategoryList[ictr].CategoryName;
                            else
                                itemcategoryList[ictr].CategoryName = itemcategoryList[ictr].CategoryName;
                        }

                        itemcategoryListTree.Add(itemcategoryList[ictr]);
                        itemcategoryListTree = TraverseCategories(ref itemcategoryListTree, itemcategoryList, itemcategoryList[ictr].CategoryGUID, depth + 1, withIndent);
                    }
                }
            }
            return itemcategoryListTree;
        }

        public List<ItemCategory> GetItemsCategoryList(int categoryGUID, string scatname
           , int hasitems, bool withparent, int active, int parentcategoryGUID, bool excludeKitted, bool OnlyNonEsn, bool IsEsnRequired)
        {
            SV.Framework.DAL.Inventory.ProductController productController = SV.Framework.DAL.Inventory.ProductController.CreateInstance<SV.Framework.DAL.Inventory.ProductController>();

            List<ItemCategory> lstItemCategoryList = productController.GetItemsCategoryList(categoryGUID, scatname
           , hasitems, withparent, active, parentcategoryGUID, excludeKitted, OnlyNonEsn, IsEsnRequired);

            return lstItemCategoryList;
        }


    }
}
