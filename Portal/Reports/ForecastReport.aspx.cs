using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Reports
{
    public partial class ForecastReport : System.Web.UI.Page
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
                BindYears();
                btnDownload.Visible = false;
                if (Session["adm"] == null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                        lblCompany.Text = userInfo.CompanyName;
                        dpCompany.Visible = false;
                        ViewState["companyid"] = userInfo.CompanyGUID;
                        BindCompanySKU(userInfo.CompanyGUID);


                    }
                }
                else
                {
                    BindCustomer();
                    ddlSKU.Visible = false;
                    lblSKU.Visible = false;

                }
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
        private void BindCompanySKU(int companyID)
        {
            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = SKUPricesOperations.GetCustomerForecastSKUList(companyID);
            if (skuList != null && skuList.Count > 0)
            {
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "SKU";
                ddlSKU.DataTextField = "SKU";

                ddlSKU.DataBind();
                ListItem newList = new ListItem("", "");
                ddlSKU.Items.Insert(0, newList);

                ddlSKU.Visible = true;
                lblSKU.Visible = true;
         
            }
            else
            {
                ddlSKU.Visible = false;
                lblSKU.Visible = false;
         
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblMsg.Text = "No SKU price assigned. Please contact Lan Global Admin";
            }
        }

        protected void gvForecast_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                ImageButton imgComment = e.Row.FindControl("imgComment") as ImageButton;
                if (imgComment != null)
                {
                    imgComment.OnClientClick = "openDialogAndBlock('Forecast Comments', '" + imgComment.ClientID + "')";

                }
                


            }

        }
        protected void imgComment_OnCommand(object sender, CommandEventArgs e)
        {
            try
            {
                Control tmp2 = LoadControl("~/controls/ForecastComment.ascx");
                avii.Controls.ForecastComment ctlComments = tmp2 as avii.Controls.ForecastComment;
                pnlComment.Controls.Clear();
                int forecastID = Convert.ToInt32(e.CommandArgument);

                if (forecastID > 0)
                {
                    //lblCMsg.Text = forecastNumber;
                    if (tmp2 != null)
                    {

                        ctlComments.BindForecastComments(forecastID);
                    }
                    pnlComment.Controls.Add(ctlComments);

                    RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }


        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        
        private void BindForecastReport()
        {
            int companyID = 0;
            string currentYear = DateTime.Now.Year.ToString();
            string sku = string.Empty;
            //int itemCompanyGuid = 0;
            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;
            lblMsg.Text = string.Empty;
            
            if (ddlSKU.SelectedIndex > 0)
                sku = ddlSKU.SelectedValue;

            //if (ddlMonth.SelectedIndex > 0)
            //    fromDate = Convert.ToDateTime("1/" + ddlMonth.SelectedValue + currentYear);

            //if (ddlMonth.SelectedIndex > 0 && ddlYears.SelectedIndex > 0)
            if (ddlMonth.SelectedIndex > 0)
            {
                fromDate = Convert.ToDateTime(ddlMonth.SelectedValue + "/" + "1/" + ddlYears.SelectedValue);
                var lastDayOfMonth = DateTime.DaysInMonth(fromDate.Year, fromDate.Month);

                toDate = Convert.ToDateTime(ddlMonth.SelectedValue + "/" + lastDayOfMonth + "/" + ddlYears.SelectedValue);
                //DateTime dt = DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);   
            }
            if (ViewState["companyid"] != null)
            {
            
                companyID = Convert.ToInt32(ViewState["companyid"]);

            }
            else
            {
                if (dpCompany.SelectedIndex > 0)
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    lblCompany.Text = string.Empty;
                }
                else
                {
                    lblMsg.Text = "Customer is required!";
                    return;
                }
            }
            if (companyID > 0)
            {
                List<avii.Classes.ForecastInfo> forecastList = SKUPricesOperations.GetForecastReport(companyID, fromDate, toDate, sku);
                if (forecastList != null && forecastList.Count > 0)
                {
                    btnDownload.Visible = true;
                    gvForecast.DataSource = forecastList;
                    gvForecast.DataBind();
                    lblCount.Text = "Total count: " + forecastList.Count;
                    Session["forecastreport"] = forecastList;
                }
                else
                {
                    Session["forecastreport"] = null;
                    btnDownload.Visible = false;
                    lblCount.Text = string.Empty;
                    lblMsg.Text = "No record found";
                    gvForecast.DataSource = null;
                    gvForecast.DataBind();
                }
            }
            


        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;

            ddlSKU.Visible = true;
            lblSKU.Visible = true;
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
                BindCompanySKU(companyID);
            else
            {
                ddlSKU.Visible = false;
                lblSKU.Visible = false;

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindForecastReport();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            ddlMonth.SelectedIndex = 0;
            ddlYears.SelectedIndex = 0;
            if (Session["adm"] != null)
            {
                dpCompany.SelectedIndex = 0;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblSKU.Visible = false;
                ddlSKU.Visible = false;
            
            }
            else
            {
                ddlSKU.SelectedIndex = 0;
            }
            lblCount.Text = string.Empty;
            gvForecast.DataSource = null;
            gvForecast.DataBind();
            btnDownload.Visible = false;

        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            DownloadReport();
        }
        private void DownloadReport()
        {

            string downLoadPath = string.Empty;
            downLoadPath = System.Configuration.ConfigurationManager.AppSettings["forecast"].ToString();
            if (Session["forecastreport"] != null)
            {
                try
                {
                    List<ForecastInfo> forecastList = (Session["forecastreport"]) as List<ForecastInfo>;

                    if (forecastList != null && forecastList.Count > 0)
                    {
                        string path = Server.MapPath(downLoadPath).ToString();
                        string fileName = "fc" + Session.SessionID + ".csv";
                        bool found = false;
                        System.IO.FileInfo file = null;
                        file = new System.IO.FileInfo(path + fileName);
                        if (file.Exists)
                        {
                            file.Delete();
                        }

                        System.Text.StringBuilder sb = new System.Text.StringBuilder();

                        sb.Append("CustomerName,ForecastNumber,ForecastDate,SKU,Qty,Price,TotalPrice\r\n");

                        foreach (avii.Classes.ForecastInfo forecast in forecastList)
                        {
                            sb.Append(forecast.CustomerName + ","
                                        + forecast.ForecastNumber + ","
                                        + forecast.ForecastDate.ToShortDateString() + ","
                                        + forecast.SKU + ","
                                        + forecast.Quantity.ToString() + ","
                                        + Math.Round(forecast.Price, 2).ToString() + ","
                                        + Math.Round(forecast.TotalPrice, 2).ToString() 
                                        + "\r\n");

                            found = true;
                        }

                        try
                        {
                            using (StreamWriter sw = new StreamWriter(file.FullName))
                            {
                                sw.WriteLine(sb.ToString());
                                sw.Flush();
                                sw.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = ex.Message;
                        }

                        if (found)
                        {
                            Response.Clear();
                            Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                            Response.ContentType = "application/octet-stream";
                            Response.WriteFile(file.FullName);
                            Response.End();
                        }
                    }
                    else
                    { lblMsg.Text = "No records found"; }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }

            }
            else
                lblMsg.Text = "Session expired!";
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvForecast.PageIndex = e.NewPageIndex;
            if (Session["forecastreport"] != null)
            {
                List<ForecastInfo> forecastList = (List<ForecastInfo>)Session["forecastreport"];

                gvForecast.DataSource = forecastList;
                gvForecast.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }
        private void BindYears()
        {
            int currentYear = Convert.ToInt32(DateTime.Now.Year);
            string year = string.Empty;
            int n = 10;
            string[] yearList = new string[10];
            for (int i = 0; i < n; i++)
            {

                yearList[i] = Convert.ToString(currentYear + i);
                //i = +i;
            }

            ddlYears.DataSource = yearList;
            ddlYears.DataBind();



        }
    }
}