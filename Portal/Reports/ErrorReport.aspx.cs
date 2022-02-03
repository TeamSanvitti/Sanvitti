using System;
using System.Data;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Reports
{
    public partial class ErrorReport : System.Web.UI.Page
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
        private void SearchPurchaseOrder()
        {
            string fromDate, toDate, searchText;
            fromDate = toDate = null;
            searchText = string.Empty;

            try
            {
                lblMsg.Text = string.Empty;


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
                searchText = (txtSearch.Text.Trim().Length > 0 ? txtSearch.Text.Trim() : string.Empty);


                DataTable reportTable = ReportOperations.GetcustomErrorReport(fromDate, toDate, searchText);
                if (reportTable != null && reportTable.Rows.Count > 0)
                {
                    gvError.DataSource = reportTable;
                    gvError.DataBind();
                    lblCount.Text = "Total records: " + reportTable.Rows.Count;

                    Session["error"] = reportTable;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    lblMsg.Text = "No record exists";
                    Session["error"] = null;
                    gvError.DataSource = null;
                    gvError.DataBind();
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchPurchaseOrder();
            //string poNum, UPC, 
            //gvOrders.DataSource = ReportOperations.GetFulfillmentLogReport
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;

            gvError.DataSource = null;
            gvError.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvError.PageIndex = e.NewPageIndex;

            if (Session["error"] != null)
            {
                DataTable dt = (DataTable)Session["error"];
                gvError.DataSource = dt;
                gvError.DataBind();
            }
            else
            {
                SearchPurchaseOrder();
            }

        }
    }
}