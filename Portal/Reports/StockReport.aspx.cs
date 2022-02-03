using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Reports
{
    public partial class StockReport : System.Web.UI.Page
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
                if (Session["adm"] == null)
                {
                    pnlSearch.Visible = false;
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        BindStockReport(userInfo.CompanyGUID);

                    }

                }
                else
                {
                    pnlSearch.Visible = true;
                    BindCompany();

                }

                
            }
        }
        private void BindCompany()
        {
            ddlCompany.DataSource = avii.Classes.ReportOperations.GetCompanyList(0);
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyID";
            ddlCompany.DataBind();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int companyID = 0;
            companyID = (ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue.Trim()) : 0);
            ViewState["cid"] = companyID;
            if (companyID > 0)
                BindStockReport(companyID);
            else
                lblMsg.Text = "Please select Customer";
        
        }
        private void BindStockReport(int companyID)
        {
            
            lblMsg.Text = string.Empty;
            

            List<avii.Classes.StockInHand> stockList = avii.Classes.ReportOperations.GetStockInHandReport(companyID, 0);
            if (stockList!= null && stockList.Count > 0)
            {

                gvStockReport.DataSource = stockList;
                Session["stockList"] = stockList;
                
            }
            else
            {
                gvStockReport.DataSource = null;
                lblMsg.Text = "No records found";
            }
            gvStockReport.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            int companyID = 0;

            if (ViewState["cid"] != null)
                companyID = Convert.ToInt32(ViewState["cid"]);
            gvStockReport.PageIndex = e.NewPageIndex;
            if (Session["stockList"] != null)
            {
                List<avii.Classes.StockInHand> stockList = (List<avii.Classes.StockInHand>)Session["stockList"];
                gvStockReport.DataSource = stockList;
                gvStockReport.DataBind();
            }
            else
                BindStockReport(companyID);
            //SeaporchLoginReport();
        }
    }
}