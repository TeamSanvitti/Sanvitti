using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using avii.Classes;
namespace avii
{
    public partial class ForecastQuery : System.Web.UI.Page
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
                if (Session["adm"] != null)
                {
                    BindCustomer();
                }
                else
                {
                    dpCompany.Visible = false;
                    lblCname.Visible = false;
                }

                if (Request["search"] != null && Request["search"] != "")
                    BindSearchedForecast();



            }
        }

        private void BindSearchedForecast()
        {
            string searchCriteria;
            string[] searchArr;
            int companyID = 0;

            if (Session["forecastsearch"] != null)
            {
                searchCriteria = (string)Session["forecastsearch"];
                searchArr = searchCriteria.Split('|');
                companyID = Convert.ToInt32(searchArr[0]);

                if ("1/1/0001" != searchArr[1].ToString())
                    txtFcDateFrom.Text = searchArr[1].ToString();
                if ("1/1/0001" != searchArr[2].ToString())
                    txtFcDateTo.Text = searchArr[2].ToString();

                txtForecastNumber.Text = searchArr[3].ToString();
                txtSKU.Text = searchArr[4].ToString();
                ddlStatus.SelectedValue = searchArr[5].ToString();
                if (companyID > 0)
                    dpCompany.SelectedValue = companyID.ToString();
                BindForecast();
            }
            else
            {
                lblMsg.Text = "Session Expire!";
            }
        }
        private void BindCustomer()
        {
            DataTable dt = avii.Classes.clsCompany.GetCompany(0, 0);
            //ViewState["customer"] = dt;
            dpCompany.DataSource = dt;
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        //protected void imgPOHistory_OnCommand(object sender, CommandEventArgs e)
        //{

        //    int poID = Convert.ToInt32(e.CommandArgument);
        //    ViewState["poid"] = poID;
            
        //    RegisterStartupScript("jsUnblockDialog", "unblockHistoryDialog();");
        //}
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindForecast();
        }
        private void BindForecast()
        {
            int companyID, statusID;
            companyID = statusID = 0;
            DateTime fromDate, toDate;
            string forecastNumber, sku;
            forecastNumber = sku = null;
            lblMsg.Text = string.Empty;

            try
            {
                fromDate = toDate = Convert.ToDateTime("1/1/0001");
                if (Session["adm"] == null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                        companyID = userInfo.CompanyGUID;
                    }

                }
                else
                {


                    if (dpCompany.SelectedIndex > 0)
                        companyID = Convert.ToInt32(dpCompany.SelectedValue);
                }

                forecastNumber = txtForecastNumber.Text.Trim().Length > 0 ? txtForecastNumber.Text.Trim() : null;
                sku = txtSKU.Text.Trim().Length > 0 ? txtSKU.Text.Trim() : null;

                statusID = Convert.ToInt32(ddlStatus.SelectedValue);
                if (txtFcDateFrom.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFcDateFrom.Text, out dt))
                        fromDate = dt;
                    else
                        throw new Exception("Forecast Date From does not have correct format(MM/DD/YYYY)");
                }
                if (txtFcDateTo.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFcDateTo.Text, out dt))
                        toDate = dt;
                    else
                        throw new Exception("Forecast Date To does not have correct format(MM/DD/YYYY)");
                }

                if (companyID == 0 && txtFcDateFrom.Text.Trim().Length == 0 && txtFcDateTo.Text.Trim().Length == 0
                    && string.IsNullOrEmpty(forecastNumber) && string.IsNullOrEmpty(sku) && statusID == 0)
                {
                    lblMsg.Text = "Please select the search criteria";
                }
                else
                    PopulateData(companyID, fromDate, toDate, forecastNumber, sku, statusID);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void PopulateData(int companyID, DateTime fromDate, DateTime toDate, string forecastNumber, string sku, int statusID)
        {
            Session["forecastsearch"] = companyID.ToString() + "|" + fromDate.ToShortDateString() + "|" + toDate.ToShortDateString() + "|" + forecastNumber + "|" + sku + "|" + statusID.ToString();

            List<avii.Classes.FulfillmentForecast> forecastList = avii.Classes.SKUPricesOperations.GetFulfillmentForecast(companyID, fromDate, toDate, forecastNumber, sku, statusID);


            if (forecastList != null && forecastList.Count > 0)
            {

                Session["forecastList"] = forecastList;
                gvForecast.DataSource = forecastList;
                lblCount.Text = "<strong>Total Forecast:</strong> " + Convert.ToString(forecastList.Count);
                lblMsg.Text = string.Empty;

            }
            else
            {

                Session["forecastList"] = null;
                lblCount.Text = string.Empty;
                gvForecast.DataSource = null;
                lblMsg.Text = "No records found";
            }

            gvForecast.DataBind();
        }
        private void ClearData()
        {
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            gvForecast.DataSource = null;
            gvForecast.DataBind();
            txtFcDateFrom.Text = string.Empty;
            txtFcDateTo.Text = string.Empty;
            
            txtForecastNumber.Text = string.Empty;
            txtSKU.Text = string.Empty;
            ddlStatus.SelectedIndex = 0;
            if (Session["adm"] != null)
            {
                dpCompany.SelectedIndex = 0;
                
            }
            
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearData();
        }
        protected void gvForecast_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ImageButton imgHistory = e.Row.FindControl("imgHistory") as ImageButton;
                if (imgHistory != null)
                {
                    imgHistory.OnClientClick = "openDialogAndBlock('Forecast History', '" + imgHistory.ClientID + "')";

                }
                ImageButton imgView = e.Row.FindControl("imgView") as ImageButton;
                if (imgView != null)
                {
                    imgView.OnClientClick = "openForecastDialogAndBlock('Forecast Line Items', '" + imgView.ClientID + "')";

                }


            }

        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        protected void imgDelete_OnCommand(object sender, CommandEventArgs e)
        {
            
            try
            {
                ImageButton imgDelete = (ImageButton)sender;
                GridViewRow row = (GridViewRow)imgDelete.NamingContainer;
                int index = row.RowIndex;
                
                int forecastID = Convert.ToInt32(e.CommandArgument);
                int userID = 0;
                string fcSource = "W";
                UserInfo userInfo = Session["userInfo"] as UserInfo;
                if (userInfo != null)
                {

                    userID = userInfo.UserGUID;

                    SKUPricesOperations.FulfillmentForecastDelete(forecastID, userID, fcSource);
                    List<avii.Classes.FulfillmentForecast> forecastList = Session["forecastList"] as List<avii.Classes.FulfillmentForecast>;
                    if (forecastList.Count >= index)
                    {
                        forecastList[index].Status = "Deleted";
                    }
                    lblMsg.Text = "Deleted successfully";

                    gvForecast.DataSource = forecastList;
                    gvForecast.DataBind();
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }


        }

        protected void imgViewFC_OnCommand(object sender, CommandEventArgs e)
        {
            try
            {
                Control tmp2 = LoadControl("~/controls/ucForecastDetail.ascx");
                avii.Controls.ucForecastDetail ctlViewForecastDetail = tmp2 as avii.Controls.ucForecastDetail;
                plnForecast.Controls.Clear();
                string info = e.CommandArgument.ToString();
                string[] arr = info.Split(',');
                if (arr.Length > 1)
                {
                    int forecastID = Convert.ToInt32(arr[0]);
                    string forecastNumber = arr[1];
                    lblFcNum.Text = forecastNumber;
                    if (tmp2 != null)
                    {

                        ctlViewForecastDetail.BindForecastDetail(forecastID);
                    }
                    plnForecast.Controls.Add(ctlViewForecastDetail);

                    RegisterStartupScript("jsUnblockDialog", "unblockForecastDialog();");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }


        }
        protected void imgFCHistory_OnCommand(object sender, CommandEventArgs e)
        {
            try
            {
                Control tmp2 = LoadControl("~/controls/ForecastHistory.ascx");
                avii.Controls.ForecastHistory ctlViewHistory = tmp2 as avii.Controls.ForecastHistory;
                pnlHistory.Controls.Clear();
                string info = e.CommandArgument.ToString();
                string[] arr = info.Split(',');
                if (arr.Length > 1)
                {
                    int forecastID = Convert.ToInt32(arr[0]);
                    string forecastNumber = arr[1];
                    lblForecastNum.Text = forecastNumber;
                    if (tmp2 != null)
                    {

                        ctlViewHistory.BindForecastLog(forecastID);
                    }
                    pnlHistory.Controls.Add(ctlViewHistory);

                    RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }


        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvForecast.PageIndex = e.NewPageIndex;
            if (Session["forecastList"] != null)
            {
                List<FulfillmentForecast> forecastList = (List<FulfillmentForecast>)Session["forecastList"];

                gvForecast.DataSource = forecastList;
                gvForecast.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }

    }
}