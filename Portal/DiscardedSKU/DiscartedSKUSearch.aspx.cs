using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;
using SV.Framework.Inventory;

namespace avii.DiscartedSKU
{
    public partial class DiscartedSKUSearch : System.Web.UI.Page
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
                BindCustomer();
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
            lblMsg.Text = "";
            lblCount.Text = "";
            gvSKU.DataSource = null;
            gvSKU.DataBind();
            DiscardSKUOperation discardSKUOperation = DiscardSKUOperation.CreateInstance<DiscardSKUOperation>();

            int companyID = Convert.ToInt32(dpCompany.SelectedValue);
            string SKU = txtSKU.Text.Trim();    
            string fromDate = txtDateFrom.Text.Trim();  
            string toDate = txtDateTo.Text.Trim();  
            if(companyID==0 && string.IsNullOrWhiteSpace(SKU) && string.IsNullOrWhiteSpace(fromDate) && string.IsNullOrWhiteSpace(toDate))
            {
                lblMsg.Text = "Please select search criteria!";
            }
            else
            {
                if(companyID > 0)
                {
                    List<SV.Framework.Models.Inventory.DiscartedSKU> discartedSKUs = discardSKUOperation.DiscartedSKUSearch(companyID, fromDate, toDate, SKU);
                    if(discartedSKUs != null && discartedSKUs.Count > 0)
                    {
                        gvSKU.DataSource = discartedSKUs;
                        gvSKU.DataBind();
                        lblCount.Text = "<strong>Total count:</strong> " + discartedSKUs.Count.ToString();


                    }
                    else
                    {
                        lblMsg.Text = "No record found";
                    }
                }
                else
                {
                    lblMsg.Text = "Customer is required!";
                }

            }


        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Clear();
        }
        protected void gvSKU_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSKU.PageIndex = e.NewPageIndex;
            if (Session["DiscartedSKU"] != null)
            {
                List<SV.Framework.Models.Inventory.DiscartedSKU> DiscartedSKUList = (List<SV.Framework.Models.Inventory.DiscartedSKU>)Session["DiscartedSKU"];

                gvSKU.DataSource = DiscartedSKUList;
                gvSKU.DataBind();
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
        public List<SV.Framework.Models.Inventory.DiscartedSKU> Sort<TKey>(List<SV.Framework.Models.Inventory.DiscartedSKU> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<SV.Framework.Models.Inventory.DiscartedSKU>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<SV.Framework.Models.Inventory.DiscartedSKU>();
            }
        }
        protected void gvSKU_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["DiscartedSKU"] != null)
            {
                List<SV.Framework.Models.Inventory.DiscartedSKU> DiscartedSKUList = (List<SV.Framework.Models.Inventory.DiscartedSKU>)Session["DiscartedSKU"];

                if (DiscartedSKUList != null && DiscartedSKUList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        DiscartedSKUList = Sort<SV.Framework.Models.Inventory.DiscartedSKU>(DiscartedSKUList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        DiscartedSKUList = Sort<SV.Framework.Models.Inventory.DiscartedSKU>(DiscartedSKUList, SortExp, SortDirection.Descending);
                    }
                    Session["DiscartedSKU"] = DiscartedSKUList;

                    gvSKU.DataSource = DiscartedSKUList;
                    gvSKU.DataBind();
                }
            }
        }
        private void Clear()
        {
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            txtDateFrom.Text = string.Empty;
            txtDateTo.Text = string.Empty;
            txtSKU.Text = string.Empty;
            if (Session["adm"] != null)
            {
                dpCompany.SelectedIndex = 0;
            }
           
            gvSKU.DataSource = null;
            gvSKU.DataBind();
        }

    }
}