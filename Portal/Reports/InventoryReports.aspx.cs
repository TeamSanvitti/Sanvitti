using System;
using System.Data;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Reports
{
    public partial class InventoryReports : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = System.Configuration.ConfigurationSettings.AppSettings["LogonPage"].ToString();

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
                BindCompany();
        }

        private void BindCompany()
        {
            ddlCompany.DataSource = ReportOperations.GetCompanyList(0);
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyID";
            ddlCompany.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvInventory.PageIndex = e.NewPageIndex;

            SearchInventory();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchInventory();
            
        }
        private void SearchInventory()
        {
            int companyID = 0;
            string itemCode = string.Empty;
            if (ddlCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(ddlCompany.SelectedValue);
            itemCode = txtItemcode.Text.Trim();
            DataTable reportTable = ReportOperations.CustomerInventoryReport(companyID, itemCode);
            gvInventory.DataSource = reportTable;
            gvInventory.DataBind();
            if (reportTable.Rows.Count > 0)
                lblCount.Text = "Total Count: " + reportTable.Rows.Count;
            else
            {
                lblCount.Text = string.Empty;
                lblMsg.Text = "No record exists";
            }
                           
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ddlCompany.SelectedIndex = 0;
            txtItemcode.Text = string.Empty;
            lblMsg.Text = string.Empty;
            gvInventory.DataSource = null;
            gvInventory.DataBind();
            lblCount.Text = string.Empty;
            
        }
    }
}