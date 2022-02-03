using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Inventory
{
    public class ProductController : BaseCreateInstance
    {

        public List<ItemCategory> GetItemsCategoryList(int categoryGUID, string scatname
           , int hasitems, bool withparent, int active, int parentcategoryGUID, bool excludeKitted, bool OnlyNonEsn, bool IsEsnRequired)
        {
            
            List<ItemCategory> lstItemCategoryList = default;// new List<ItemCategory>();

            using (DBConnect objDB = new DBConnect())
            {
                ItemCategory objItemCategory= default;//;

                DataSet ds = default;//new DataSet();
                Hashtable objCompHash = new Hashtable();
                string[] arrSpFieldSeq;
                try
                {
                    int catid = categoryGUID;
                    if (parentcategoryGUID > 0)
                        catid = parentcategoryGUID;

                    objCompHash.Add("@CategoryGUID", categoryGUID);
                    //objCompHash.Add("@hasitems", hasitems);
                    objCompHash.Add("@CategoryName", scatname);
                    //objCompHash.Add("@active", active);
                    //objCompHash.Add("@tabflag", tabflag);
                    //objCompHash.Add("@categoryName1", categoryMen);
                    //objCompHash.Add("@categoryName2", categoryWomen);
                    objCompHash.Add("@excludeKitted", excludeKitted);
                    objCompHash.Add("@OnlyNonESN", OnlyNonEsn);
                    objCompHash.Add("@OnlyESN", IsEsnRequired);


                    //arrSpFieldSeq = new string[] { "@categoryGUID", "@hasitems", "@categoryName", "@active", "@categoryName1", "@categoryName2" };
                    arrSpFieldSeq = new string[] { "@CategoryGUID", "@CategoryName", "@excludeKitted", "@OnlyNonESN", "@OnlyESN" };
                    ds = objDB.GetDataSet(objCompHash, "av_itemcategory_select", arrSpFieldSeq);

                    if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {
                        lstItemCategoryList = new List<ItemCategory>();
                        int iteratefrom = 1;
                        if (withparent)
                            iteratefrom = 0;
                        for (int ictr = iteratefrom; ictr < ds.Tables[0].Rows.Count; ictr++)
                        {
                            DataRow dataRow = ds.Tables[0].Rows[ictr];

                            objItemCategory = new ItemCategory();
                            objItemCategory.CategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CategoryGUID", 0, false));
                            objItemCategory.CategoryName = clsGeneral.getColumnData(dataRow, "categoryName", string.Empty, false) as string;
                            //objItemCategory.ParentCategoryName = clsGeneral.getColumnData(dataRow, "parentcatname", string.Empty, false) as string;
                            objItemCategory.CategoryDescription = clsGeneral.getColumnData(dataRow, "categoryDescription", string.Empty, false) as string;
                            objItemCategory.CategoryImage = clsGeneral.getColumnData(dataRow, "categoryImage", string.Empty, false) as string;
                            objItemCategory.Comments = clsGeneral.getColumnData(dataRow, "comments", string.Empty, false) as string;
                            objItemCategory.ParentCategoryGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ParentCategoryGUID", 0, false));
                            //objItemCategory.isActive = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "active", 0, false)); 
                            objItemCategory.IsESNRequired = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsESNRequired", false, false));
                            objItemCategory.CategoryWithProductAllowed = Convert.ToString(clsGeneral.getColumnData(dataRow, "CategoryWithProductAllowed", string.Empty, false));

                            lstItemCategoryList.Add(objItemCategory);
                        }
                    }
                }
                catch (Exception objExp)
                {
                    throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return lstItemCategoryList;
        }


    }
}
