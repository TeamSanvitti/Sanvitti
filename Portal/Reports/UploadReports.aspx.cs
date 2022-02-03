using System;
using System.Data;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Reports
{
    public partial class UploadReports : System.Web.UI.Page
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
            string fromDate, toDate, moduleName;
            int statusID = 0;
            
            fromDate = toDate =moduleName = null;
            try
            {
                lblMsg.Text = string.Empty;

                moduleName = (ddlModules.SelectedIndex > 0 ? ddlModules.SelectedValue.Trim() : null);
                statusID = (ddlStatus.SelectedIndex > 0 ? Convert.ToInt32(ddlStatus.SelectedValue.Trim()) : 0);

                if (txtFromDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFromDate.Text, out dt))
                        fromDate = dt.ToShortDateString();
                    else
                        throw new Exception("Upload Date From does not have correct format(MM/DD/YYYY)");
                }
                if (txtEndDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtEndDate.Text, out dt))
                        toDate = dt.ToShortDateString();
                    else
                        throw new Exception("Upload Date To does not have correct format(MM/DD/YYYY)");

                }


                if (string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(moduleName) && statusID==0)
                {
                    validForm = false;
                }
                if (validForm)
                {
                    DataTable reportTable = ReportOperations.GetUploadLogReport(fromDate, toDate, moduleName, statusID);
                    if (reportTable.Rows.Count > 0)
                    {
                        gvOrders.DataSource = reportTable;
                        gvOrders.DataBind();

                        Session["uploadreport"] = reportTable;
                    }
                    else
                    {
                        lblMsg.Text = "No record exists";
                        Session["uploadreport"] = null;
                        gvOrders.DataSource = null;
                        gvOrders.DataBind();
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";
                    Session["uploadreport"] = null;
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
            txtFromDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
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

            ReportOperations.DeleteUploadReport(Convert.ToInt32(e.CommandArgument));
            SearchPurchaseOrder();
            lblMsg.Text = "Deleted successfully";

            
        }
        protected void imgEditOrder_Commnad(object sender, CommandEventArgs e)
        {

            int uploadID = Convert.ToInt32(e.CommandArgument);

            DataTable orderTable = new DataTable();

            orderTable = (DataTable)Session["uploadreport"];
            DataRow[] rows = orderTable.Select(string.Format("UploadID='{0}' ", uploadID));
            DataRow searchedRow = null;
            if (rows.Length > 0)
            {
                searchedRow = rows[0];
                string poXML = Convert.ToString(searchedRow["UploadData"]);
                poXML = poXML.Replace("<", "&lt;");
                poXML = poXML.Replace("<", "&gt;");
                lblPoXML.Text = poXML;


            }
            ModalPopupExtender1.Show();

           
        }

    }
}