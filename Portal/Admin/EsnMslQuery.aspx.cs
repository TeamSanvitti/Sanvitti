using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
//using avii.Classes;
using System.Reflection;
using SV.Framework.Models.Inventory;
using SV.Framework.Inventory;

namespace avii.Admin
{
    public partial class EsnMslQuery : System.Web.UI.Page
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
                bindCategory(ddlCategoryFilter);
            }
        }
        private void bindCategory(DropDownList ddlCategory)
        {

            ProductController objProductController = new ProductController();

            ddlCategoryFilter.Items.Clear();
            int catid = -1, parentcat = -1;
            int iactive = 1;
            //if (chkactive.Checked)
            //    iactive = -1;

            List<avii.Classes.ItemCategory> lstItemCategoryList = objProductController.getItemCategoryTree(0, 0, 1, true, iactive, -1, true, false, false, true);
            ddlCategory.DataSource = lstItemCategoryList;
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "CategoryGUID";
            ddlCategory.DataBind();

            ListItem li = new ListItem("--Select Category--", "0");
            ddlCategory.Items.Insert(0, li);


        }

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyId";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            MslOperation mslOperation = MslOperation.CreateInstance<MslOperation>();
            int companyId = 0, categoryID = 0;
            string custOrderNumber = string.Empty, shipFrpm = string.Empty, shipTo = string.Empty, ESN = string.Empty, trackingNumber = string.Empty, SKU = string.Empty;
            custOrderNumber = txtCustOrderNumber.Text.Trim();
            shipFrpm = txtShipFrom.Text.Trim();
            shipTo = txtShipTo.Text.Trim();
            ESN = txtESN.Text.Trim();
            SKU = txtSKU.Text.Trim();
            if (ddlCategoryFilter.SelectedIndex > 0)
                categoryID = Convert.ToInt32(ddlCategoryFilter.SelectedValue);

            trackingNumber = txtTrackingNo.Text.Trim();
            lblCount.Text = string.Empty;
            lblTotalQty.Text = string.Empty;
            lblMsg.Text = string.Empty;
            string sortExpression = "ESNHeaderId";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;
            lblMsg.Text = string.Empty;

            if (dpCompany.SelectedIndex > 0)
                companyId = Convert.ToInt32(dpCompany.SelectedValue);

            if (companyId == 0 && categoryID == 0 && string.IsNullOrEmpty(custOrderNumber) && string.IsNullOrEmpty(SKU) && string.IsNullOrEmpty(shipFrpm) && string.IsNullOrEmpty(shipTo) && string.IsNullOrEmpty(ESN) && string.IsNullOrEmpty(trackingNumber))
            {
                lblMsg.Text = "Please select the search criteria";
                pnlSearch.Visible = false;
                Session["esnheader"] = null;
                gvMSL.DataSource = null;
                gvMSL.DataBind();
            }
            else
            {
                List<EsnHeaders> esnHeadersList = mslOperation.GetESNwithHeaderList(companyId, custOrderNumber, shipFrpm, shipTo, ESN, trackingNumber, SKU, categoryID);
                if (esnHeadersList != null && esnHeadersList.Count > 0)
                {
                    pnlSearch.Visible = true;
                    Session["esnheader"] = esnHeadersList;
                    gvMSL.DataSource = esnHeadersList;
                    gvMSL.DataBind();
                    var sum = esnHeadersList.Sum(x => x.OrderQty);

                    lblCount.Text = "<strong>Total Records:</strong> " + esnHeadersList.Count;
                    lblTotalQty.Text = "<strong>Total Quantity Received:</strong> " + sum.ToString();
                }
                else
                {
                    pnlSearch.Visible = false;
                    lblMsg.Text = "No record exists for selected criteria";
                    Session["esnheader"] = null;
                    gvMSL.DataSource = null;
                    gvMSL.DataBind();
                }
            }
        }
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblTotalQty.Text = string.Empty;
            txtCustOrderNumber.Text = string.Empty;
            txtShipFrom.Text = string.Empty;
            txtShipTo.Text = string.Empty;
            txtESN.Text = string.Empty;
            txtTrackingNo.Text = string.Empty;
            dpCompany.SelectedIndex = 0;
            ddlCategoryFilter.SelectedIndex = 0;
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            txtSKU.Text = string.Empty;
            Session["esnheader"] = null;
            gvMSL.DataSource = null;
            gvMSL.DataBind();

        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMSL.PageIndex = e.NewPageIndex;
            if (Session["esnheader"] != null)
            {
                List<EsnHeaders> esnHeadersList = (List<EsnHeaders>)Session["esnheader"];

                gvMSL.DataSource = esnHeadersList;
                gvMSL.DataBind();
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
        public List<EsnHeaders> Sort<TKey>(List<EsnHeaders> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<EsnHeaders>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<EsnHeaders>();
            }
        }
        protected void gvMSL_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["esnheader"] != null)
            {
                List<EsnHeaders> esnHeadersList = (List<EsnHeaders>)Session["esnheader"];

                if (esnHeadersList != null && esnHeadersList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        esnHeadersList = Sort<EsnHeaders>(esnHeadersList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        esnHeadersList = Sort<EsnHeaders>(esnHeadersList, SortExp, SortDirection.Descending);
                    }
                    Session["esnheader"] = esnHeadersList;
                    gvMSL.DataSource = esnHeadersList;
                    gvMSL.DataBind();
                }
            }
        }

        protected void imgEdit_Command(object sender, CommandEventArgs e)
        {
            int ESNHeaderID = Convert.ToInt32(e.CommandArgument);

            Session["ESN_HeaderID"] = ESNHeaderID;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('/admin/managemslesn.aspx')</script>", false);

        }
    }
}