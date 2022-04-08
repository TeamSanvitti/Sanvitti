using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;
using SV.Framework.Inventory;
using System.Reflection;

namespace avii.Warehouse
{
    public partial class WhLocationReport : System.Web.UI.Page
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
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                BindCustomer();
                BindWarehouse();
            }
        }


        private void BindWarehouse()
        {
            SV.Framework.Inventory.WarehouseOperation warehouseOperation = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();
            ddlWarehouse.DataSource = warehouseOperation.GetWarehouse(0);
            ddlWarehouse.DataValueField = "WarehouseID";
            ddlWarehouse.DataTextField = "WarehouseCity";
            ddlWarehouse.DataBind();
            ListItem item = new ListItem("", "0");
            ddlWarehouse.Items.Insert(0, item);
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        //private void BindCompanySKU(int companyID, bool IsEdit)
        //{
        //    MslOperation mslOperation = MslOperation.CreateInstance<MslOperation>();
        //    lblMsg.Text = string.Empty;
        //    List<CompanySKUno> skuList = mslOperation.GetCompanySKUs(companyID, 4);
        //    if (skuList != null)
        //    {
        //        ViewState["skulist"] = skuList;
        //        if (IsEdit)
        //        {
        //            ddlSKU.DataSource = skuList;
        //            ddlSKU.DataValueField = "itemcompanyguid";
        //            ddlSKU.DataTextField = "sku";
        //            ddlSKU.DataBind();
        //        }

        //        //  ddlSKU.DataBind();
        //        //  ListItem item = new ListItem("", "0");
        //        //  ddlSKU.Items.Insert(0, item);
        //    }
        //    else
        //    {
        //        ViewState["skulist"] = null;
        //        ddlSKU.DataSource = null;
        //        ddlSKU.DataBind();
        //        lblMsg.Text = "No SKU are assigned to selected Customer";

        //    }


        //}

        //protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    int companyID = 0;

        //    //gvMSL.DataSource = null;
        //    //gvMSL.DataBind();
        //    lblMsg.Text = string.Empty;
        //    //lblConfirm.Text = string.Empty;

        //    // btnSubmit.Visible = false;
        //    //btnUpload.Visible = true;
        //    // btnSubmit2.Visible = false;
        //    // pnlSubmit.Visible = false;
        //    //lblCount.Text = string.Empty;
        //    string CustInfo = string.Empty;
        //    //  trSKU.Visible = true;
        //    if (dpCompany.SelectedIndex > 0)
        //    {
        //        companyID = Convert.ToInt32(dpCompany.SelectedValue);

        //    }
        //    if (companyID > 0)
        //    {
        //        BindCompanySKU(companyID, true);

        //    }
        //    else
        //    {
        //        //  trSKU.Visible = true;
        //        ddlSKU.DataSource = null;
        //        ddlSKU.DataBind();
        //    }
        //}

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            WhLocationBind();
        }
        protected void gvWHCode_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWHCode.PageIndex = e.NewPageIndex;

            if (Session["whLocations"] != null)
            {
                List<WhLocationInfo> whLocations = Session["whLocations"] as List<WhLocationInfo>;

                gvWHCode.DataSource = whLocations;
                gvWHCode.DataBind();
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

            if (Session["whLocations"] != null)
            {
                List<WhLocationInfo> whLocations = (List<WhLocationInfo>)Session["whLocations"];

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
                    Session["whLocations"] = list;
                    gvWHCode.DataSource = list;
                    gvWHCode.DataBind();
                }
            }
        }

        private void WhLocationBind()
        {
            SV.Framework.Inventory.WarehouseOperation warehouseOperation = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();

            lblCount.Text = "";
            lblMsg.Text = "";
            Session["whLocations"] = null;
            gvWHCode.DataSource = null;
            gvWHCode.DataBind();
            string sortExpression = "LastReceivedDate";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            string warehouseCity = ddlWarehouse.SelectedItem.Text.Trim();
            string warehouseLocation = txtWarehouseLocation.Text.Trim();
            int companyID = Convert.ToInt32(dpCompany.SelectedValue);
            string fromDate = txtDateFrom.Text.Trim();
            string toDate = txtDateTo.Text.Trim();
            string SKU = txtSKU.Text.Trim();

            if (string.IsNullOrEmpty(warehouseCity) && string.IsNullOrEmpty(warehouseLocation) && string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(SKU) && companyID == 0)
            {
                lblMsg.Text = "Please enter search criteria!";
            }
            else
            {
                Session["whlocationreport"] = warehouseCity + "," + warehouseLocation + "," + companyID.ToString() + "," + SKU + "," + fromDate + "," + toDate;
                List<WhLocationInfo> whLocations = warehouseOperation.GetWarehouseLocationReport(warehouseCity, warehouseLocation, companyID, SKU, fromDate, toDate);
                if (whLocations != null && whLocations.Count > 0)
                {
                    Session["whLocations"] = whLocations;
                    gvWHCode.DataSource = whLocations;
                    gvWHCode.DataBind();
                    lblCount.Text = "<b>Total count: </b>" + whLocations.Count;
                }
                else
                {
                    lblMsg.Text = "No record found";

                }
            }
        }


        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}