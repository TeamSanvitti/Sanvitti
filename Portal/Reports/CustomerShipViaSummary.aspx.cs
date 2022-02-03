using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Reports
{
    public partial class CustomerShipViaSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                    lblCompany.Text = userInfo.CompanyName;
                    dpCompany.Visible = false;

                }
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
                BindCustomer();
                
            }
        }
        private void BindCustomer()
        {
            DataTable dt = avii.Classes.clsCompany.GetCompany(0, 0);
            ViewState["customer"] = dt;
            dpCompany.DataSource = dt;
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int companyID = 0;
            DateTime fromDate, toDate;
            fromDate = toDate = Convert.ToDateTime("1/1/0001");
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                    lblCompany.Text = userInfo.CompanyName;
                    dpCompany.Visible = false;
                    companyID = userInfo.CompanyGUID;
                }

            }
            else
            {
                lblCompany.Text = string.Empty;

                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }
            
            if (txtFromDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtFromDate.Text, out dt))
                    fromDate = dt;
                else
                    throw new Exception("RMA Date From does not have correct format(MM/DD/YYYY)");
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtToDate.Text, out dt))
                    toDate = dt;
                else
                    throw new Exception("RMA Date To does not have correct format(MM/DD/YYYY)");
            }

            PopulateData(companyID, fromDate, toDate);
            
        }

        private void PopulateData(int companyID, DateTime fromDate, DateTime toDate)
        {


            List<CustomerShipViaCodes> shipViaList = ReportOperations.GetCustomersShipViaSummary(companyID, fromDate, toDate);

            Session["CustomerShipViaCodes"] = shipViaList;
            if (shipViaList != null && shipViaList.Count > 0)
            {
                gvShipVia.DataSource = shipViaList;
                lblCount.Text = "<strong>Total Customers:</strong> " + Convert.ToString(shipViaList.Count-1);
                lblMsg.Text = string.Empty;
                    
            }
            else
            {
                Session["CustomerShipViaCodes"] = null;
                lblCount.Text = string.Empty;
                gvShipVia.DataSource = null;
                lblMsg.Text = "No records found";
            }
            
            gvShipVia.DataBind();
        }
        private void ReloadData()
        {
            List<CustomerShipViaCodes> shipViaList = null;
            if (Session["CustomerShipViaCodes"] != null)
            {
                shipViaList = (List<CustomerShipViaCodes>)Session["CustomerShipViaCodes"];


                Session["CustomerShipViaCodes"] = shipViaList;
                if (shipViaList != null && shipViaList.Count > 0)
                {
                    gvShipVia.DataSource = shipViaList;
                    lblCount.Text = "<strong>Total ESN:</strong> " + Convert.ToString(shipViaList.Count);
                    lblMsg.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvShipVia.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }
            else
            {
                lblCount.Text = string.Empty;
                gvShipVia.DataSource = null;
                lblMsg.Text = string.Empty;
                lblMsg.Text = "Session expire!";
            }
            gvShipVia.DataBind();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["adm"] != null)
                dpCompany.SelectedIndex = 0;

            
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            gvShipVia.DataSource = null;
            gvShipVia.DataBind();
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;


        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvShipVia.PageIndex = e.NewPageIndex;
            ReloadData();

        }
    }
}