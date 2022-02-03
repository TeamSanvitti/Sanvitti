using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.States;
namespace avii.Category
{
    public partial class CategorySearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = ConfigurationSettings.AppSettings["LogonPage"].ToString();
                }
                catch
                {
                    url = "/avii/logon.aspx";
                }
                if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                BindPrentCategories();
                if (Request["search"] != null)
                {
                    if(Session["categorysearch"]!=null)
                    {
                        string search = Session["categorysearch"] as string;
                        string[] arr = search.Split('~');
                        if(arr != null && arr.Length > 0)
                        {
                            txtCategory.Text = arr[0];
                            ddlParent.SelectedValue = arr[1];
                            chkBase.Checked = Convert.ToBoolean(arr[2]);
                            chkLeaf.Checked = Convert.ToBoolean(arr[3]);
                            ddlStatus.SelectedValue = arr[4];

                        }

                        BindCategories();
                    }
                    //Session["categorysearch"] = model.CategoryName + "~" + model.ParentCategoryGUID + "~" + model.BaseCategories + "~" + model.LeafCategories + "~" + model.CategoryStatusID;

                    
                }
            }
        }

        private void BindPrentCategories()
        {
            CategoryModel model = new CategoryModel();
            model.CategoryName = string.Empty;
            model.BaseCategories = true;
            model.LeafCategories = false;
            model.ParentCategoryGUID = -1;
            model.CategoryStatusID = -1;

            List<CategoryModel> categoryList = SV.Framework.Operations.CategoryOperation.GetCategoryList(model);
            if (categoryList != null && categoryList.Count > 0)
            {
                ddlParent.DataSource = categoryList;
                ddlParent.DataTextField = "CategoryName";
                ddlParent.DataValueField = "CategoryGUID";
                ddlParent.DataBind();
                ListItem listItem = new ListItem("", "-1");
                ddlParent.Items.Insert(0, listItem);

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {

            BindCategories();
        }
        private void BindCategories()
        {
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            //int CategoryStatusID = -1;
            CategoryModel model = new CategoryModel();
            string sortExpression = "CategoryGUID";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            model.CategoryName = txtCategory.Text.Trim();
            model.BaseCategories = chkBase.Checked;
            model.LeafCategories = chkLeaf.Checked;
            model.ParentCategoryGUID = Convert.ToInt32(ddlParent.SelectedValue);
            model.CategoryStatusID = Convert.ToInt32(ddlStatus.SelectedValue);

            Session["categorysearch"] = model.CategoryName + "~" + model.ParentCategoryGUID + "~" + model.BaseCategories + "~" + model.LeafCategories + "~" + model.CategoryStatusID;


            List<CategoryModel> categoryList = SV.Framework.Operations.CategoryOperation.GetCategoryList(model);
            if (categoryList != null && categoryList.Count > 0)
            {
                gvCategory.DataSource = categoryList;
                gvCategory.DataBind();
                Session["categoryList"] = categoryList;
                lblCount.Text = "<strong>Total count:</strong> " + categoryList.Count;
            }
            else
            {
                gvCategory.DataSource = null;
                gvCategory.DataBind();
                lblMsg.Text = "No record exists.";
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            txtCategory.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            ddlParent.SelectedIndex = 0;
            chkLeaf.Checked = false;
            chkBase.Checked = false;
            gvCategory.DataSource = null;
            gvCategory.DataBind();

        }
        protected void gvCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCategory.PageIndex = e.NewPageIndex;
            if (Session["categoryList"] != null)
            {
                List<CategoryModel> categoryList = (List<CategoryModel>)Session["categoryList"];

                gvCategory.DataSource = categoryList;
                gvCategory.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }
        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        private List<CategoryModel> Sort<TKey>(List<CategoryModel> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<CategoryModel>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<CategoryModel>();
            }
        }
        protected void gvCategory_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["categoryList"] != null)
            {
                List<CategoryModel> categoryList = (List<CategoryModel>)Session["categoryList"];

                if (categoryList != null && categoryList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        categoryList = Sort<CategoryModel>(categoryList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        categoryList = Sort<CategoryModel>(categoryList, SortExp, SortDirection.Descending);
                    }
                    Session["categoryList"] = categoryList;
                    gvCategory.DataSource = categoryList;
                    gvCategory.DataBind();
                }
            }
        }

        protected void imgEdit_Command(object sender, CommandEventArgs e)
        {
            int CategoryGUID = Convert.ToInt32(e.CommandArgument);

            if (CategoryGUID > 0)
            {
                Session["CategoryGUID"] = CategoryGUID;
                Response.Redirect("~/Category/ManageCategory.aspx");
            }
        }
        protected void imgDel_Command(object sender, CommandEventArgs e)
        {
            int CategoryGUID = Convert.ToInt32(e.CommandArgument);

            int returnResult = SV.Framework.Operations.CategoryOperation.CategoryDelete(CategoryGUID);
            if (returnResult > 0)
            {
                BindCategories();
                lblMsg.Text = "Deleted successfully";
                
            }
            else
            {
                lblMsg.Text = "Could not delete";
            }
        }
    }
}