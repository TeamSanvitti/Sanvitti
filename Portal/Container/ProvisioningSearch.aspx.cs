using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Container
{
    public partial class ProvisioningSearch : System.Web.UI.Page
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
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 1);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblPO.Text = "";

            string dateFrom = string.Empty, dateTo = string.Empty, fulfillmentNumber = string.Empty;
            int companyID = 0;
            if(dpCompany.SelectedIndex > 0)
            {
                string sortExpression = "ProvisioningDate";
                string sortDirection = "DESC";
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = sortExpression;

                companyID = Convert.ToInt32(dpCompany.SelectedValue);

                dateFrom = txtDateFrom.Text.Trim();
                dateTo = txtDateTo.Text.Trim();
                fulfillmentNumber = txtPoNum.Text.Trim();
                List<SV.Framework.Fulfillment.POProvisioning> pOProvisionings = SV.Framework.Fulfillment.ProvisioningOperation.GetPurchaseOrderProvisioning(companyID, fulfillmentNumber, dateFrom, dateTo);
                if(pOProvisionings != null && pOProvisionings.Count > 0)
                {
                    gvPO.DataSource = pOProvisionings;
                    gvPO.DataBind();
                    Session["pOProvisionings"] = pOProvisionings;
                    lblPO.Text = "Total count: " + pOProvisionings.Count;

                }
                else
                {
                    gvPO.DataSource = null;
                    gvPO.DataBind();
                    lblMsg.Text = "No record found";
                }
            }
            else
            {
                lblMsg.Text = "Customer required!";
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblPO.Text = "";
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            txtPoNum.Text = "";
            dpCompany.SelectedIndex = 0;
            gvPO.DataSource = null;
            gvPO.DataBind();
        }


        protected void imgViewPO_OnCommand(object sender, CommandEventArgs e)
        {
            int poid = Convert.ToInt32(e.CommandArgument);
            Session["poid"] = poid;

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('/container/ProvisioningView.aspx')</script>", false);

           // Response.Redirect("~/container/ProvisioningView.aspx");
        }
        protected void lnkViewPO_Command(object sender, CommandEventArgs e)
        {
            int poid = Convert.ToInt32(e.CommandArgument);
            Session["poid"] = poid;
            Session["provisioning"] = 1;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../FulfillmentDetails.aspx')</script>", false);

        }
        protected void gvPO_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPO.PageIndex = e.NewPageIndex;
            if (Session["pOProvisionings"] != null)
            {
                List<SV.Framework.Fulfillment.POProvisioning> poList = (List<SV.Framework.Fulfillment.POProvisioning>)Session["pOProvisionings"];

                gvPO.DataSource = poList;
                gvPO.DataBind();
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
        public List<SV.Framework.Fulfillment.POProvisioning> Sort<TKey>(List<SV.Framework.Fulfillment.POProvisioning> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<SV.Framework.Fulfillment.POProvisioning>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<SV.Framework.Fulfillment.POProvisioning>();
            }
        }
        protected void gvPO_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["pOProvisionings"] != null)
            {
                List<SV.Framework.Fulfillment.POProvisioning> pOProvisionings = (List<SV.Framework.Fulfillment.POProvisioning>)Session["pOProvisionings"];

                if (pOProvisionings != null && pOProvisionings.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        pOProvisionings = Sort<SV.Framework.Fulfillment.POProvisioning>(pOProvisionings, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        pOProvisionings = Sort<SV.Framework.Fulfillment.POProvisioning>(pOProvisionings, SortExp, SortDirection.Descending);
                    }
                    Session["pOProvisionings"] = pOProvisionings;
                    gvPO.DataSource = pOProvisionings;
                    gvPO.DataBind();
                }
            }
        }

        
    }
}