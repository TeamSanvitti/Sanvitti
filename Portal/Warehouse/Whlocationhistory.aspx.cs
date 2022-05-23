using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;
//using SV.Framework.Inventory;

namespace avii.Warehouse
{
    public partial class Whlocationhistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetWarehouseLocationHistory();

        }
        public void GetWarehouseLocationHistory()
        {
            string warehouseLocation;
            int companyID = 0;
            SV.Framework.Inventory.WarehouseOperation warehouseOperation = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();
            lblCount.Text = "";
            lblMsg.Text = "";
            Session["whlhistory"] = null;
            gvWhHistory.DataSource = null;
            gvWhHistory.DataBind();

            if (Session["whhistorysearchcriteria"] != null)
            {
                
                string whlocationsearchcriteria = Session["whhistorysearchcriteria"] as string;
                if (whlocationsearchcriteria != null)
                {
                    string[] array = whlocationsearchcriteria.Split(',');
                    warehouseLocation = array[0];
                    companyID = Convert.ToInt32(array[1]);

                    string sortExpression = "LastReceivedDate";
                    string sortDirection = "DESC";
                    ViewState["SortDirection"] = sortDirection;
                    ViewState["SortExpression"] = sortExpression;

                    List<WhLocationInfo> warehouseList = warehouseOperation.GetWarehouseLocationHistory(warehouseLocation, companyID);
                    if (warehouseList != null && warehouseList.Count > 0)
                    {
                        Session["whlhistory"] = warehouseList;
                        gvWhHistory.DataSource = warehouseList;
                        gvWhHistory.DataBind();
                        lblCount.Text = "<b>Total Count: </b>" + warehouseList.Count;

                    }
                    else
                    {
                        lblMsg.Text = "No record found!";
                    }
                }
            }
        }

        protected void gvWhHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWhHistory.PageIndex = e.NewPageIndex;

            if (Session["whlhistory"] != null)
            {
                List<WhLocationInfo> whLocations = Session["whlhistory"] as List<WhLocationInfo>;

                gvWhHistory.DataSource = whLocations;
                gvWhHistory.DataBind();
            }

        }

        protected void gvWhHistory_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["whlhistory"] != null)
            {
                List<WhLocationInfo> whLocations = (List<WhLocationInfo>)Session["whlhistory"];

                if (whLocations != null && whLocations.Count > 0)
                {
                    var list = whLocations;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<WhLocationInfo>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<WhLocationInfo>(list, SortExp, SortDirection.Descending);
                    }
                    Session["whlhistory"] = list;
                    gvWhHistory.DataSource = list;
                    gvWhHistory.DataBind();
                }
            }
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
        public List<WhLocationInfo> Sort<TKey>(List<WhLocationInfo> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<WhLocationInfo>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<WhLocationInfo>();
            }
        }
        protected void gvWHCode_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["whlhistory"] != null)
            {
                List<WhLocationInfo> whLocations = (List<WhLocationInfo>)Session["whlhistory"];

                if (whLocations != null && whLocations.Count > 0)
                {
                    var list = whLocations;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<WhLocationInfo>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<WhLocationInfo>(list, SortExp, SortDirection.Descending);
                    }
                    Session["whlhistory"] = list;
                    gvWhHistory.DataSource = list;
                    gvWhHistory.DataBind();
                }
            }
        }

    }
}