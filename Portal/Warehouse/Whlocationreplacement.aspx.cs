using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;

namespace avii.Warehouse
{
    public partial class Whlocationreplacement : System.Web.UI.Page
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
                //BindWarehouse();
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            WhLocationBind();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }
        private void WhLocationBind()
        {
            SV.Framework.Inventory.WarehouseOperation warehouseOperation = SV.Framework.Inventory.WarehouseOperation.CreateInstance<SV.Framework.Inventory.WarehouseOperation>();

            lblCount.Text = "";
            lblMsg.Text = "";
            Session["whLocationreplacement"] = null;
            gvWHCode.DataSource = null;
            gvWHCode.DataBind();
            string sortExpression = "LastReceivedDate";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

           // string warehouseCity = ddlWarehouse.SelectedItem.Text.Trim();
            string warehouseLocation = txtWarehouseLocation.Text.Trim();
            int companyID = Convert.ToInt32(dpCompany.SelectedValue);
           // string fromDate = txtDateFrom.Text.Trim();
           // string toDate = txtDateTo.Text.Trim();
            string SKU = txtSKU.Text.Trim();

            if (string.IsNullOrEmpty(warehouseLocation) && string.IsNullOrEmpty(SKU) && companyID == 0)
            {
                lblMsg.Text = "Please enter search criteria!";
            }
            else
            {
                Session["whlocationreplacement"] = warehouseLocation + "," + companyID.ToString() + "," + SKU;
                List<WhLocationInfo> whLocations = warehouseOperation.GetWarehouseLocationReplacement(warehouseLocation, companyID, SKU);
                if (whLocations != null && whLocations.Count > 0)
                {
                    Session["whLocationreplacement"] = whLocations;
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

        private void ClearForm()
        {

            lblCount.Text = "";
            lblMsg.Text = "";
            Session["whLocationreplacement"] = null;
            gvWHCode.DataSource = null;
            gvWHCode.DataBind();
           // txtDateFrom.Text = "";
            //txtDateTo.Text = "";
            txtSKU.Text = "";
            txtWarehouseLocation.Text = "";
            //ddlWarehouse.SelectedIndex = 0;
            dpCompany.SelectedIndex = 0;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void lnkView_Command(object sender, CommandEventArgs e)
        {
            string whLocationdetiail = Convert.ToString(e.CommandArgument);
            string[] array = whLocationdetiail.Split(',');
            string searchCriteria = array[0] + "," + array[1] + "," + array[2] + "," + "";
            //string whlocationreport = Session["whlocationreport"] as string;
            //if (whlocationreport != null)
            //{
            //    string[] array2 = whlocationreport.Split(',');
            //    searchCriteria = searchCriteria + "," + array2[4] + "," + array2[5];

            //}
            Session["whlocationsearchcriteria"] = searchCriteria;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('WhLocationDetail.aspx')</script>", false);

        }

        protected void lnkHistory_Command(object sender, CommandEventArgs e)
        {
            string whLocationdetiail = Convert.ToString(e.CommandArgument);
            string[] array = whLocationdetiail.Split(',');
            string searchCriteria = array[0] + "," + array[1];
            Session["whhistorysearchcriteria"] = searchCriteria;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('whlocationhistory.aspx')</script>", false);

        }





        protected void gvWHCode_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvWHCode.PageIndex = e.NewPageIndex;

            if (Session["whLocationreplacement"] != null)
            {
                List<WhLocationInfo> whLocations = Session["whLocationreplacement"] as List<WhLocationInfo>;

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

            if (Session["whLocationreplacement"] != null)
            {
                List<WhLocationInfo> whLocations = (List<WhLocationInfo>)Session["whLocationreplacement"];

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
                    Session["whLocationreplacement"] = list;
                    gvWHCode.DataSource = list;
                    gvWHCode.DataBind();
                }
            }
        }

        protected void imgView_Command(object sender, CommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            Session["rowIndex"] = rowIndex;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('whlocationrelocate.aspx')</script>", false);

        }
    }
}