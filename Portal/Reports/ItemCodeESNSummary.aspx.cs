using System;
using System.Data;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Reports
{
    public partial class ItemCodeESNSummary : System.Web.UI.Page
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
                BindItemCodeESN();
                DateTime today = DateTime.Today;
                DateTime yearEarlier = today.AddDays(-365);

                lblFromDate.Text = yearEarlier.ToShortDateString();
                lblToDate.Text = DateTime.Today.ToShortDateString();
            }
        }
        private void BindItemCodeESN()
        {
            DataTable reportTable = null;
            if (Session["itemcodeesn"] == null)
                reportTable = ReportOperations.GetItemCode_ESN_Summary();
            else
                reportTable = Session["itemcodeesn"] as DataTable;

            if (reportTable != null && reportTable.Rows.Count > 0)
            {
                gvESN.DataSource = reportTable;
                gvESN.DataBind();

                Session["itemcodeesn"] = reportTable;
            }
            else
            {
                lblMsg.Text = "No record exists";
                Session["itemcodeesn"] = null;
                gvESN.DataSource = null;
                gvESN.DataBind();
            }
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvESN.PageIndex = e.NewPageIndex;

            BindItemCodeESN();
        }
    }
}