using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Reports
{
    public partial class LoginReports : System.Web.UI.Page
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
                bindCompany();
        }
        private void bindCompany()
        {
            DataTable dt = ReportOperations.GetCompanyList(0);
            if (dt != null)
            {
                DataRow toInsert = dt.NewRow();
                toInsert[0] = 237;
                toInsert[1] = "Lan Global";

                // insert in the desired place
                dt.Rows.InsertAt(toInsert, dt.Rows.Count - 1);

                ddlCompany.DataSource = dt;
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataValueField = "CompanyID";
                ddlCompany.DataBind();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchLoginReport();
            //string poNum, UPC, 
            //gvOrders.DataSource = ReportOperations.GetFulfillmentLogReport
        }
        private void SearchLoginReport()
        {
            bool validForm = true;
            string fromDate, toDate, userName;
            int companyID = 0;
            fromDate = toDate = userName = null;
            string sortExpression = "SignInID";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            try
            {
                lblMsg.Text = string.Empty;
                lblCount.Text = string.Empty;

                userName = (txtUserName.Text.Trim().Length > 0 ? txtUserName.Text.Trim() : null);

                if (txtFromDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFromDate.Text, out dt))
                        fromDate = dt.ToShortDateString();
                    else
                        throw new Exception("Start Date From does not have correct format(MM/DD/YYYY)");
                }
                if (txtEndDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtEndDate.Text, out dt))
                        toDate = dt.ToShortDateString();
                    else
                        throw new Exception("End Date To does not have correct format(MM/DD/YYYY)");

                }
                
                companyID = (ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue.Trim()) : 0);


                if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && ddlCompany.SelectedIndex == 0)
                {
                    validForm = false;
                }
                if (validForm)
                {
                    List<LoginUser> userList = ReportOperations.GetUserLoginLogReport(userName, companyID, fromDate, toDate);
                    if (userList != null && userList.Count > 0)
                    {
                        gvLoginReport.DataSource = userList;
                        gvLoginReport.DataBind();
                        Session["userList"] = userList;
                        lblCount.Text = "<strong>Total Count:</strong> " + userList.Count;
                    }
                    else
                    {
                        Session["userList"] = null;
                        lblMsg.Text = "No record exists";

                        gvLoginReport.DataSource = null;
                        gvLoginReport.DataBind();
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";

                    gvLoginReport.DataSource = null;
                    gvLoginReport.DataBind();
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
            Session["userList"] = null;
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            txtUserName.Text = string.Empty;
            ddlCompany.SelectedIndex = 0;
            txtFromDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            gvLoginReport.DataSource = null;
            gvLoginReport.DataBind();
        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLoginReport.PageIndex = e.NewPageIndex;

            SearchLoginReport();
        }
        protected void imgDelete_Commnad(object sender, CommandEventArgs e)
        {
            ReportOperations.DeleteLoginReport(Convert.ToInt32(e.CommandArgument));
            SearchLoginReport();
            lblMsg.Text = "Deleted successfully";
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
        public List<LoginUser> Sort<TKey>(List<LoginUser> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<LoginUser>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<LoginUser>();
            }
        }
        protected void gvLoginReport_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["userList"] != null)
            {
                List<LoginUser> userList = (List<LoginUser>)Session["userList"];

                if (userList != null && userList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        userList = Sort<LoginUser>(userList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        userList = Sort<LoginUser>(userList, SortExp, SortDirection.Descending);
                    }
                    Session["userList"] = userList;
                    gvLoginReport.DataSource = userList;
                    gvLoginReport.DataBind();
                }
            }
        }

    }
}