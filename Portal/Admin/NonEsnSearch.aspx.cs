using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;

namespace avii.Admin
{
    public partial class NonEsnSearch : System.Web.UI.Page
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
                if(Session["nonesnsearch2"] != null)
                {
                    //int companyID = 0, itemCompanyGUID = 0, categoryGUID = 0;
                   // string custOrderNumber, SKU, fromdate, todate;
                    string searchText = Convert.ToString(Session["nonesnsearch2"]);
                    Session["nonesnsearch2"] = null;
                    string[] searchArray = searchText.Split('~');
                    if(searchArray != null)
                    {
                        //companyID = Convert.ToInt32(searchArray[0]);
                        //custOrderNumber = searchArray[1];
                        //fromdate = searchArray[2];
                        //todate = searchArray[3];
                        //SKU = searchArray[4];
                        //categoryGUID = Convert.ToInt32(searchArray[5]);

                        {
                            dpCompany.SelectedValue = searchArray[0];
                            //dpCompany.SelectedValue = searchArray[0];
                            ddlCategoryFilter.SelectedValue = searchArray[5];
                            txtCustOrderNumber.Text = searchArray[1];
                            txtShipFrom.Text = searchArray[2];
                            txtShipTo.Text = searchArray[3];
                            txtSKU.Text = searchArray[4];
                            txtLocation.Text = searchArray[6];
                            BindSearch();
                        }
                    }
                }
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

            List<avii.Classes.ItemCategory> lstItemCategoryList = objProductController.getItemCategoryTree(0, 0, 1, true, iactive, -1, true, true, true, false);
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
        private void BindSearch()
        {
            SV.Framework.Inventory.NonEsnOperation nonEsnOperation = SV.Framework.Inventory.NonEsnOperation.CreateInstance<SV.Framework.Inventory.NonEsnOperation>();

            int companyId = 0, categoryID = 0;
            string location = string.Empty, custOrderNumber = string.Empty, shipFrpm = string.Empty, shipTo = string.Empty, ESN = string.Empty, trackingNumber = string.Empty, SKU = string.Empty;
            custOrderNumber = txtCustOrderNumber.Text.Trim();
            shipFrpm = txtShipFrom.Text.Trim();
            shipTo = txtShipTo.Text.Trim();
            location = txtLocation.Text.Trim();
          //  ESN = txtESN.Text.Trim();
            SKU = txtSKU.Text.Trim();
            if (ddlCategoryFilter.SelectedIndex > 0)
                categoryID = Convert.ToInt32(ddlCategoryFilter.SelectedValue);

           // trackingNumber = txtTrackingNo.Text.Trim();
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

            if (companyId == 0 && categoryID == 0 && string.IsNullOrEmpty(custOrderNumber) && string.IsNullOrEmpty(SKU) && string.IsNullOrEmpty(shipFrpm) && string.IsNullOrEmpty(shipTo) && string.IsNullOrEmpty(ESN) && string.IsNullOrEmpty(trackingNumber) && string.IsNullOrEmpty(location))
            {
                lblMsg.Text = "Please select the search criteria";
                pnlSearch.Visible = false;
                Session["nonesnheader"] = null;
                gvMSL.DataSource = null;
                gvMSL.DataBind();
            }
            else
            {
                Session["nonesnsearch"] = companyId + "~" + custOrderNumber + "~" + shipFrpm + "~" + shipTo + "~" + SKU + "~" + categoryID.ToString() + "~" + location;

                List<NonEsnHeader> esnHeadersList = nonEsnOperation.GetNonESNwithHeaderList(companyId, custOrderNumber, shipFrpm, shipTo, SKU, categoryID, location);
                if (esnHeadersList != null && esnHeadersList.Count > 0)
                {
                    pnlSearch.Visible = true;
                    Session["nonesnheader"] = esnHeadersList;
                    gvMSL.DataSource = esnHeadersList;
                    gvMSL.DataBind();
                    var sum = esnHeadersList.Sum(x => x.TotalQty);

                    lblCount.Text = "<strong>Total Records:</strong> " + esnHeadersList.Count;
                    lblTotalQty.Text = "<strong>Total Quantity Received:</strong> " + sum.ToString();
                }
                else
                {
                    pnlSearch.Visible = false;
                    lblMsg.Text = "No record exists for selected criteria";
                    Session["nonesnheader"] = null;
                    gvMSL.DataSource = null;
                    gvMSL.DataBind();
                }
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSearch();   
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblTotalQty.Text = string.Empty;
            txtCustOrderNumber.Text = string.Empty;
            txtShipFrom.Text = string.Empty;
            txtShipTo.Text = string.Empty;
           // txtESN.Text = string.Empty;
            //txtTrackingNo.Text = string.Empty;
            dpCompany.SelectedIndex = 0;
            ddlCategoryFilter.SelectedIndex = 0;
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            txtSKU.Text = string.Empty;
            Session["nonesnheader"] = null;
            gvMSL.DataSource = null;
            gvMSL.DataBind();

        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMSL.PageIndex = e.NewPageIndex;
            if (Session["nonesnheader"] != null)
            {
                List<NonEsnHeader> esnHeadersList = (List<NonEsnHeader>)Session["nonesnheader"];

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
        public List<NonEsnHeader> Sort<TKey>(List<NonEsnHeader> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<NonEsnHeader>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<NonEsnHeader>();
            }
        }
        protected void gvMSL_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["nonesnheader"] != null)
            {
                List<NonEsnHeader> esnHeadersList = (List<NonEsnHeader>)Session["nonesnheader"];

                if (esnHeadersList != null && esnHeadersList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        esnHeadersList = Sort<NonEsnHeader>(esnHeadersList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        esnHeadersList = Sort<NonEsnHeader>(esnHeadersList, SortExp, SortDirection.Descending);
                    }
                    Session["nonesnheader"] = esnHeadersList;
                    gvMSL.DataSource = esnHeadersList;
                    gvMSL.DataBind();
                }
            }
        }

        protected void edit_Command(object sender, CommandEventArgs e)
        {
            string search =  Session["nonesnsearch"] as string;
            Session["nonesnsearch2"] = search;
            Session["nonesnsearch"] = null;

            Session["esnheaderid"] = Convert.ToInt32(e.CommandArgument);
            Response.Redirect("~/admin/NonEsnInventory.aspx");


        }

        protected void imgDelete_Command(object sender, CommandEventArgs e)
        {

        }
    }
}