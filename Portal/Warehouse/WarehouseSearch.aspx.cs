using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Inventory;
using SV.Framework.Models.Inventory;


namespace avii.Warehouse
{
    public partial class WarehouseSearch : System.Web.UI.Page
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
                BindCompany();
                BindWarehouse();
                FillSearchCriteria();
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

        private void FillSearchCriteria()
        {            
            if(Session["wh"] != null)
            {
                Session["wh"] = null;
                if (Session["whsearch"] != null)
                {
                    string searchCriterias = Session["whsearch"] as string;

                    string[] searchArry = searchCriterias.Split(',');
                    if (searchArry.Length > 0)
                    {
                        ddlWarehouse.SelectedItem.Text = searchArry[0];
                        txtWarehouseLocation.Text = searchArry[1];
                        ddlCompany.SelectedValue = searchArry[2];
                        WarehouseBind();
                    }

                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblCount.Text = "";
            ddlCompany.SelectedIndex = 0;
            ddlWarehouse.SelectedIndex = 0;
            //txtWarehousecity.Text = "";
            txtWarehouseLocation.Text = "";
            gvWHCode.DataSource = null;
            gvWHCode.DataBind();
        }

        private void BindCompany()
        {
            ddlCompany.DataSource = avii.Classes.RMAUtility.getRMAUserCompany(-1, "", 1, -1);
            ddlCompany.DataValueField = "companyID";
            ddlCompany.DataTextField = "companyName";
            ddlCompany.DataBind();
            ListItem item = new ListItem("", "0");
            ddlCompany.Items.Insert(0, item);
        }
        private void WarehouseBind()
        {
            SV.Framework.Inventory.WarehouseOperation warehouseOperation = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();

            lblCount.Text = "";
            lblMsg.Text = "";
            Session["warehouseStorages"] = null;
            gvWHCode.DataSource = null;
            gvWHCode.DataBind();
            string sortExpression = "CreatedDateTime";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            string warehouseCity = ddlWarehouse.SelectedItem.Text.Trim();
            string warehouseLocation = txtWarehouseLocation.Text.Trim();
            int companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            if (string.IsNullOrEmpty(warehouseCity) && string.IsNullOrEmpty(warehouseLocation) && companyID == 0)
            {
                lblMsg.Text = "Please enter search criteria!";
            }
            else
            {
                Session["whsearch"] = warehouseCity + "," + warehouseLocation + "," + companyID.ToString();
                List<WarehouseStorage> warehouseStorages = warehouseOperation.GetWarehouseStorage(warehouseCity, warehouseLocation, companyID);
                if (warehouseStorages != null && warehouseStorages.Count > 0)
                {
                    Session["warehouseStorages"] = warehouseStorages;
                    gvWHCode.DataSource = warehouseStorages;
                    gvWHCode.DataBind();
                    lblCount.Text = "<b>Total count: </b>" + warehouseStorages.Count;
                }
                else
                {
                    lblMsg.Text = "No record found";
                    
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            WarehouseBind();
        }

        protected void gvWHCode_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWHCode.PageIndex = e.NewPageIndex;

            if (Session["warehouseStorages"] != null)
            {
                List<WarehouseStorage> warehouseStorages = Session["warehouseStorages"] as List<WarehouseStorage>;

                gvWHCode.DataSource = warehouseStorages;
                gvWHCode.DataBind();
            }
        }

        protected void imgEdit_Command(object sender, CommandEventArgs e)
        {
            Session["WarehouseStorageID"] = Convert.ToInt32(e.CommandArgument);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('/warehouse/managewarehouse.aspx')</script>", false);
            //Response.Redirect("/managewarehouse.aspx");
        }

        protected void imgDelete_Command(object sender, CommandEventArgs e)
        {
            SV.Framework.Inventory.WarehouseOperation warehouseOperation = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();
            int WarehouseStorageID = Convert.ToInt32(e.CommandArgument);
            int userID = Convert.ToInt32(Session["UserID"]);

            lblMsg.Text = warehouseOperation.DeleteWarehouseStorage(WarehouseStorageID, userID);
        }

        protected void btnNewLocation_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('/warehouse/managewarehouse.aspx')</script>", false);
            
        }

        protected void imgView_Command(object sender, CommandEventArgs e)
        {
            Session["WarehouseLocation"] = Convert.ToString(e.CommandArgument);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('/warehouse/ViewWarehouseLocation.aspx')</script>", false);

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
        public List<WarehouseStorage> Sort<TKey>(List<WarehouseStorage> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<WarehouseStorage>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<WarehouseStorage>();
            }
        }
        protected void gvgvWHCode_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["warehouseStorages"] != null)
            {
                List<WarehouseStorage> warehouseStorages = (List<WarehouseStorage>)Session["warehouseStorages"];

                if (warehouseStorages != null && warehouseStorages.Count > 0)
                {
                    var list = warehouseStorages;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<WarehouseStorage>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<WarehouseStorage>(list, SortExp, SortDirection.Descending);
                    }
                    Session["warehouseStorages"] = list;
                    gvWHCode.DataSource = list;
                    gvWHCode.DataBind();
                }
            }
        }

    }
}