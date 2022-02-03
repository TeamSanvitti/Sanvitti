using System;
using System.Data;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Reports
{
    public partial class AuditESNReports : System.Web.UI.Page
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
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchPurchaseOrder();
            
        }
        private void SearchPurchaseOrder()
        {
            bool validForm = true;
            string esn, fromDate, toDate, module;
            esn = fromDate = toDate = module = null;
            try
            {
                lblMsg.Text = string.Empty;
                esn = (txtESN.Text.Trim().Length > 0 ? txtESN.Text.Trim() : null);
                module = (ddlModules.SelectedIndex > 0 ? ddlModules.SelectedValue : null);
                
                if (txtFromDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFromDate.Text, out dt))
                        fromDate = dt.ToShortDateString();
                    else
                        throw new Exception("Date From does not have correct format(MM/DD/YYYY)");
                }
                if (txtEndDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtEndDate.Text, out dt))
                        toDate = dt.ToShortDateString();
                    else
                        throw new Exception("Date To does not have correct format(MM/DD/YYYY)");

                }
                


                if (string.IsNullOrEmpty(module) && string.IsNullOrEmpty(esn) && string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate))
                {
                    validForm = false;
                }
                if (validForm)
                {
                    DataTable reportTable = ReportOperations.GetAuditESNLogReport(esn, module, fromDate, toDate);
                    if (reportTable != null && reportTable.Rows.Count > 0)
                    {
                        gvRMA.DataSource = reportTable;
                        gvRMA.DataBind();

                        //Session["fulfillmentreport"] = reportTable;
                    }
                    else
                    {
                        lblMsg.Text = "No record exists";
                        //Session["fulfillmentreport"] = null;
                        gvRMA.DataSource = null;
                        gvRMA.DataBind();
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";
                    //Session["fulfillmentreport"] = null;
                    gvRMA.DataSource = null;
                    gvRMA.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRMA.PageIndex = e.NewPageIndex;

            SearchPurchaseOrder();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            lblMsg.Text = string.Empty;
            txtESN.Text = string.Empty;
            ddlModules.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            gvRMA.DataSource = null;
            gvRMA.DataBind();
        }

        protected void imgDelete_Commnad(object sender, CommandEventArgs e)
        {

            ReportOperations.DeleteAuditESNReport(Convert.ToInt32(e.CommandArgument));
            SearchPurchaseOrder();
            lblMsg.Text = "Deleted successfully";
        }
    }
}