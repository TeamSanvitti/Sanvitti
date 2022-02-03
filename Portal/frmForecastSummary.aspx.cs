using System;
using System.Data;

namespace avii
{
    public partial class frmForecastSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                bindCustomerDropDown();
                bindMakerDropDown();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int customerID, userID, makerID;
            string sku = null;
            DateTime dateFrom, dateTo;

            lblMsg.Text = string.Empty;
            customerID = userID = makerID = 0;
            dateFrom = dateTo = DateTime.MinValue;
            avii.Classes.ItemForecastController controller = new avii.Classes.ItemForecastController();
            if (ddlCustomer.SelectedIndex > 0)
            {
                int.TryParse(ddlCustomer.SelectedValue, out customerID);
            }

            if (ddlMaker.SelectedIndex > 0)
            {
                int.TryParse(ddlMaker.SelectedValue, out makerID);
            }        

            if (txtSKU.Text.Trim().Length > 0)
            {
                sku = txtSKU.Text.Trim();
            }

            if (txtDateFrom.Text.Trim().Length > 0)
            {
                DateTime.TryParse(txtDateFrom.Text.Trim(), out dateFrom);
            }

            if (txtDateTo.Text.Trim().Length > 0)
            {
                DateTime.TryParse(txtDateTo.Text.Trim(), out dateTo);
            }

            DataTable dataTable = controller.GetForecastSummary(customerID, 0, sku, makerID, dateFrom, dateTo);
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                lblMsg.Text = string.Empty;
                dlForecast.DataSource = dataTable;
                dlForecast.DataBind();
            }
            else
            {
                lblMsg.Text = "No record found with selected search criteria";
                dlForecast.DataSource = null;
                dlForecast.DataBind();
            }
        }

        protected void bindCustomerDropDown()
        {
            ddlCustomer.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            ddlCustomer.DataValueField = "CompanyID";
            ddlCustomer.DataTextField = "CompanyName";
            ddlCustomer.DataBind();
        }

        protected void bindMakerDropDown()
        {
            avii.Classes.ProductUtility product = new Classes.ProductUtility();
            ddlMaker.DataSource = product.getMakerList();
            ddlMaker.DataValueField = "MakerGuid";
            ddlMaker.DataTextField = "Maker";
            ddlMaker.DataBind();
        }

        protected void btnSearchCancel_Click(object sender, EventArgs e)
        {
            ddlCustomer.SelectedIndex = 0;
            txtDateFrom.Text = string.Empty;
            txtDateTo.Text = string.Empty;
            txtSKU.Text = string.Empty;
            dlForecast.DataSource = null;
            dlForecast.DataBind();
            lblMsg.Text = string.Empty;
            ddlMaker.SelectedIndex = 0;
        }
    }
}