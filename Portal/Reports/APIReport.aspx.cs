using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Reports
{
    public partial class APIReport : System.Web.UI.Page
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
                txtEndDate.Text = DateTime.Now.ToShortDateString();

                txtFromDate.Text = DateTime.Now.AddDays(-1).ToShortDateString();
                if (Session["adm"] != null)
                {
                    BindCustomer();


                }
                else
                    trCustomer.Visible = false;


            }

        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
        }
        private void BindLog()
        {
            bool validForm = true;
            int companyID = 0;
            string fromDate, toDate, ResponseData, RequestData, moduleName, status;
            fromDate = toDate = null;
            ResponseData = RequestData = moduleName = status = string.Empty;
            lblCount.Text = "";
            try
            {
                RequestData = txtRequest.Text.Trim();
                ResponseData = txtResponse.Text.Trim();

                fromDate = txtFromDate.Text.Trim();
                toDate = txtEndDate.Text.Trim();
                moduleName = ddlModule.SelectedValue;
                status = ddlStatus.SelectedValue;

                lblMsg.Text = string.Empty;
                
                if (Session["adm"] != null)
                {
                    if (dpCompany.SelectedIndex > 0)
                        companyID = Convert.ToInt32(dpCompany.SelectedValue);
                }
                else
                {
                    if (Session["userInfo"] != null)
                    {
                        avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                        if (userInfo != null)
                        {
                            companyID = userInfo.CompanyGUID;
                            // ViewState["companyID"] = companyID;
                        }
                    }
                }

                string sortExpression = "RequestTimeStamp";
                string sortDirection = "DESC";
                ViewState["SortDirection"] = sortDirection;
                ViewState["SortExpression"] = sortExpression;

                if (Session["adm"] != null)
                {
                    if (companyID == 0 && string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(RequestData) && string.IsNullOrEmpty(ResponseData) && string.IsNullOrEmpty(moduleName) && string.IsNullOrEmpty(status))
                    {
                        validForm = false;
                    }
                }
                else if (string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(RequestData) && string.IsNullOrEmpty(ResponseData) && string.IsNullOrEmpty(moduleName) && string.IsNullOrEmpty(status))
                {
                    validForm = false;
                }
                if (validForm)
                {
                    List<LogModel> logList = LogOperations.GetAPILogReport(fromDate, toDate, RequestData, ResponseData, moduleName, status, companyID);
                    if (logList != null && logList.Count > 0)
                    {
                        gvLog.DataSource = logList;
                        gvLog.DataBind();

                        Session["logList"] = logList;
                        lblCount.Text = "<strong>Total Log:</strong> " + logList.Count;
                    }
                    else
                    {
                        lblMsg.Text = "No record exists";
                        Session["logList"] = null;
                        gvLog.DataSource = null;
                        gvLog.DataBind();
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";
                    Session["logList"] = null;
                    gvLog.DataSource = null;
                    gvLog.DataBind();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindLog();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

            ClearAll();
        }
        private void ClearAll()
        {
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtRequest.Text = string.Empty;
            txtResponse.Text = string.Empty;
            ddlModule.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            gvLog.DataSource = null;
            gvLog.DataBind();
            Session["logList"] = null;

        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }
        //
        protected void lnkRequest_Click(object sender, CommandEventArgs e)
        {
            lblRequestData.Text = string.Empty;
            try
            {
                Int64 logID = Convert.ToInt64(e.CommandArgument);
                List<LogModel> logList = HttpContext.Current.Session["logList"] as List<LogModel>;

                var lgList = (from item in logList where item.LogID.Equals(logID) select item).ToList();


                if (lgList != null && lgList.Count > 0)
                {
                    lblRequestData.Text = lgList[0].RequestData;

                }
                else
                    lblRequestData.Text = "No data found";
                RegisterStartupScript("jsUnblockDialog", "unblockRequestDialog();");
            }
            catch (Exception ex)
            {
                lblRequestData.Text = ex.Message;
            }


        }
        protected void lnkResponse_Click(object sender, CommandEventArgs e)
        {
            lblResponseData.Text = string.Empty;
            try
            {
                Int64 logID = Convert.ToInt64(e.CommandArgument);
                List<LogModel> logList = HttpContext.Current.Session["logList"] as List<LogModel>;

                var lgList = (from item in logList where item.LogID.Equals(logID) select item).ToList();


                if (lgList != null && lgList.Count > 0)
                {
                    lblResponseData.Text = lgList[0].ResponseData;

                }
                else
                    lblResponseData.Text = "No data found";
                RegisterStartupScript("jsUnblockDialog", "unblockResponseDialog();");
            }
            catch (Exception ex)
            {
                lblResponseData.Text = ex.Message;
            }


        }
        protected void gvLog_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //Label lblAE = e.Row.FindControl("lblAE") as Label;
                //if(lblAE != null)
                //    availableBalance += Convert.ToInt32(lblAE.Text);

                LinkButton lnkRequest = e.Row.FindControl("lnkRequest") as LinkButton;
                if (lnkRequest != null)
                {
                    lnkRequest.OnClientClick = "openRequestDialogAndBlock('Request data', '" + lnkRequest.ClientID + "')";

                }
                LinkButton lnkResponse = e.Row.FindControl("lnkResponse") as LinkButton;
                if (lnkResponse != null)
                {
                    lnkResponse.OnClientClick = "openResponseDialogAndBlock('Response data', '" + lnkResponse.ClientID + "')";

                }


            }

        }
        protected void gvLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLog.PageIndex = e.NewPageIndex;


            if (Session["logList"] != null)
            {
                List<LogModel> logList = (List<LogModel>)Session["logList"];

                gvLog.DataSource = logList;
                gvLog.DataBind();
            }
            else
            {
                BindLog();
            }

        }


        private string GetSortDirection(string column)
        {
            string sortDirection = "ASC";
            string sortExpression = ViewState["SortExpression"] as string;
            if (sortExpression != null)
            {
                if (sortExpression == column)
                {
                    string lastDirection = ViewState["SortDirection"] as string;
                    if ((lastDirection != null) && (lastDirection == "ASC"))
                    {
                        sortDirection = "DESC";
                    }
                }
            }
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = column;
            return sortDirection;
        }
        public List<LogModel> Sort<TKey>(List<LogModel> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<LogModel>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<LogModel>();
            }
        }
        protected void gvLog_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["logList"] != null)
            {
                List<LogModel> logList = (List<LogModel>)Session["logList"];

                if (logList != null && logList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        logList = Sort<LogModel>(logList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        logList = Sort<LogModel>(logList, SortExp, SortDirection.Descending);
                    }
                    Session["logList"] = logList;
                    gvLog.DataSource = logList;
                    gvLog.DataBind();
                }
            }
        }

    }

}