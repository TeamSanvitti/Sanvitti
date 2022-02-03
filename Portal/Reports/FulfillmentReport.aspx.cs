using System;
using System.Data;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Reports
{
    public partial class FulfillmentReport : System.Web.UI.Page
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
            {
                bindCompany();
                if (Request["po"] != null && Request["cid"] != null)
                {
                    txtPONo.Text = Request["po"].ToString();
                    ddlCompany.SelectedValue = Request["cid"].ToString();
                    SearchPurchaseOrder();
                }
            }
        }
        private void bindCompany()
        {
            ddlCompany.DataSource = ReportOperations.GetCompanyList(0);
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyID";
            ddlCompany.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchPurchaseOrder();
            //string poNum, UPC, 
            //gvOrders.DataSource = ReportOperations.GetFulfillmentLogReport
        }
        private void SearchPurchaseOrder()
        {
            bool validForm = true;
            string poNum, fromDate, toDate, SKU;
            int companyID = 0;
            poNum = fromDate = toDate = SKU = null;
            try
            {
                lblMsg.Text = string.Empty;
                SKU = (txtSKU.Text.Trim().Length > 0 ? txtSKU.Text.Trim() : null);
                poNum = (txtPONo.Text.Trim().Length > 0 ? txtPONo.Text.Trim() : null);
                
                
                if (txtFromDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFromDate.Text, out dt))
                        fromDate = dt.ToShortDateString();
                    else
                        throw new Exception("PO Date From does not have correct format(MM/DD/YYYY)");
                }
                if (txtEndDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtEndDate.Text, out dt))
                        toDate = dt.ToShortDateString();
                    else
                        throw new Exception("PO Date To does not have correct format(MM/DD/YYYY)");

                }
                companyID = (ddlCompany.SelectedIndex > 0 ? Convert.ToInt32( ddlCompany.SelectedValue): 0);
                
                
                if (string.IsNullOrEmpty(SKU) && string.IsNullOrEmpty(poNum) && string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && ddlCompany.SelectedIndex == 0)
                {
                    validForm = false;
                }
                if (validForm)
                {
                    DataTable reportTable = ReportOperations.GetFulfillmentLogReport(poNum, SKU, fromDate, toDate, companyID);
                    if (reportTable.Rows.Count > 0)
                    {
                        gvOrders.DataSource = reportTable;
                        gvOrders.DataBind();

                        Session["fulfillmentreport"] = reportTable;
                    }
                    else
                    {
                        lblMsg.Text = "No record exists";
                        Session["fulfillmentreport"] = null;
                        gvOrders.DataSource = null;
                        gvOrders.DataBind();
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";
                    Session["fulfillmentreport"] = null;
                    gvOrders.DataSource = null;
                    gvOrders.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            lblMsg.Text = string.Empty;
            txtSKU.Text = string.Empty;
            txtPONo.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            ddlCompany.SelectedIndex = 0;
            gvOrders.DataSource = null;
            gvOrders.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrders.PageIndex = e.NewPageIndex;

            SearchPurchaseOrder();
        }
        protected void imgDelete_Commnad(object sender, CommandEventArgs e)
        {

            ReportOperations.DeleteFulfillmentReport(Convert.ToInt32(e.CommandArgument));
            lblMsg.Text = "Deleted successfully";

            SearchPurchaseOrder();
        }
        protected void imgEditOrder_Commnad(object sender, CommandEventArgs e)
        {

            int fulfillmentLogID = Convert.ToInt32(e.CommandArgument);
            
            DataTable orderTable = new DataTable();
            
            orderTable = (DataTable)Session["fulfillmentreport"];
            DataRow[] rows = orderTable.Select(string.Format("fulfillmentLogID='{0}' ", fulfillmentLogID));
            DataRow searchedRow = null;
            if (rows.Length > 0)
            {
                searchedRow = rows[0];
                string poXML = Convert.ToString(searchedRow["podataXML"]);
                poXML = poXML.Replace("<", "&lt;");
                poXML = poXML.Replace("<", "&gt;");
                lblPoXML.Text = poXML;
                
                
            }
            ModalPopupExtender1.Show();
           
            //string url = "PurchaseOrderForm.aspx?po=" + poGUID;
            //Response.Redirect(url, false);
        }
    }
}
