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
    public partial class RMADetailReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                 //   lblCompany.Text = userInfo.CompanyName;
                   // dpCompany.Visible = false;

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
            if(!IsPostBack)
            {
               // btnPrint.Visible = false;
                btnDownload.Visible = false;

            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            int companyID = 0;
            //int timeInterval = 0;
            int esnStatusID = 0;
            int rmaStatusID = 0;
            DateTime fromDate, toDate;
            string rmaNumber = string.Empty, esn = string.Empty;
            fromDate = toDate = DateTime.MinValue;

            //if (Session["adm"] == null)
            //{
            //    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            //    if (userInfo != null)
            //    {
            //        //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

            //        lblCompany.Text = userInfo.CompanyName;
            //        dpCompany.Visible = false;
            //        companyID = userInfo.CompanyGUID;
            //    }

            //}
            //else
            //{
            //    lblCompany.Text = string.Empty;

            //    if (dpCompany.SelectedIndex > 0)
            //        companyID = Convert.ToInt32(dpCompany.SelectedValue);
            //}
            //if (ddlDuration.SelectedIndex > 0)
            //timeInterval = Convert.ToInt32(ddlDuration.SelectedValue);

            esnStatusID = Convert.ToInt32(ddlEsnStatus.SelectedValue);
            rmaStatusID = Convert.ToInt32(ddlRmaStaus.SelectedValue);
            //if (timeInterval == 1)
            //    lblDuration.Text = DateTime.Now.ToShortDateString();
            //else
            //{
            //    lblDuration.Text = "RMA Date Range: " + DateTime.Now.AddDays(-timeInterval).ToShortDateString() + " - " + DateTime.Now.ToShortDateString();

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

            //if (companyID > 0)
            if (chkESN.Checked)
                PopulateEsnDetail(companyID, rmaNumber, esn, fromDate, toDate, esnStatusID, rmaStatusID);
            else
                PopulateData(companyID, rmaNumber, esn, fromDate, toDate, esnStatusID, rmaStatusID);
            //else
            //    lblMsg.Text = "Please select Customer!";
        }
        private void PopulateData(int companyID, string rmaNumber, string esn, DateTime fromDate, DateTime toDate, int esnStatusID, int rmaStatusID)
        {
            pnlESNDetail.Visible = false;
            string sortExpression = "RMADate";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;


            List<RMAEsnDetail> rmaStatusList = ReportOperations.GetRmaEsnOnlyReport(companyID, rmaNumber, esn, fromDate, toDate, esnStatusID, rmaStatusID);

            Session["RMAEsnDetail"] = rmaStatusList;
            if (rmaStatusList != null && rmaStatusList.Count > 0)
            {
                pnlRMA.Visible = true;
                gvRMA.DataSource = rmaStatusList;
                lblCount.Text = "<strong>Total ESN:</strong> " + Convert.ToString(rmaStatusList.Count);
                lblMsg.Text = string.Empty;
             //   btnPrint.Visible = true;
                btnDownload.Visible = true;
            }
            else
            {
             //   btnPrint.Visible = false;
                pnlRMA.Visible = false;
                lblCount.Text = string.Empty;
                gvRMA.DataSource = null;
                lblMsg.Text = "No records found";
            }
            gvRMA.DataBind();
        }
        private void PopulateEsnDetail(int companyID, string rmaNumber, string esn, DateTime fromDate, DateTime toDate, int esnStatusID, int rmaStatusID)
        {
            pnlRMA.Visible = false;

            string sortExpression = "RMADate";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;


            List<RMAESNDetail> rmaStatusList = ReportOperations.GetRmaEsnDetailReport(companyID, rmaNumber, esn, fromDate, toDate, esnStatusID, rmaStatusID);

            Session["RMAESNDetail"] = rmaStatusList;
            if (rmaStatusList != null && rmaStatusList.Count > 0)
            {
                pnlESNDetail.Visible = true;
                gvEsnDetail.DataSource = rmaStatusList;
                lblCountdetail.Text = "<strong>Total ESN:</strong> " + Convert.ToString(rmaStatusList.Count);
                lblMsg.Text = string.Empty;
               // btnPrint.Visible = true;
                //btnDownload.Visible = true;
            }
            else
            {
                pnlESNDetail.Visible = false;
                //   btnPrint.Visible = false;
                //Button2.Visible = false;
                lblCountdetail.Text = string.Empty;
                gvEsnDetail.DataSource = null;
                lblMsg.Text = "No records found";
            }
            gvEsnDetail.DataBind();
        }
        private void ReloadData()
        {
            List<RMAEsnDetail> rmaStatusList = null;
            if (Session["RMAEsnDetail"] != null)
            {
                rmaStatusList = (List<RMAEsnDetail>)Session["RMAEsnDetail"];


                Session["RMAEsnDetail"] = rmaStatusList;
                if (rmaStatusList != null && rmaStatusList.Count > 0)
                {
                    gvRMA.DataSource = rmaStatusList;
                    lblCount.Text = "<strong>Total ESN:</strong> " + Convert.ToString(rmaStatusList.Count);
                    lblMsg.Text = string.Empty;
                }
                else
                {
                    lblCount.Text = string.Empty;
                    gvRMA.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }
            else
            {
                lblCount.Text = string.Empty;
                btnDownload.Visible = false;
                //btnPrint.Visible = false;
                gvRMA.DataSource = null;
                lblMsg.Text = string.Empty;
                lblMsg.Text = "Session expire!";
            }
            gvRMA.DataBind();
        }
        private void ReloadESNDetail()
        {
            List<RMAESNDetail> rmaStatusList = null;
            if (Session["RMAESNDetail"] != null)
            {
                rmaStatusList = (List<RMAESNDetail>)Session["RMAESNDetail"];


                Session["RMAESNDetail"] = rmaStatusList;
                if (rmaStatusList != null && rmaStatusList.Count > 0)
                {
                    gvEsnDetail.DataSource = rmaStatusList;
                    lblCountdetail.Text = "<strong>Total ESN:</strong> " + Convert.ToString(rmaStatusList.Count);
                    lblMsg.Text = string.Empty;
                    pnlESNDetail.Visible = true;
                }
                else
                {
                    pnlESNDetail.Visible = false;
                    lblCountdetail.Text = string.Empty;
                    gvEsnDetail.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }
            else
            {
                lblCountdetail.Text = string.Empty;
               pnlESNDetail.Visible = false;
                //btnPrint.Visible = false;
                gvEsnDetail.DataSource = null;
                lblMsg.Text = string.Empty;
                lblMsg.Text = "Session expire!";
            }
            gvEsnDetail.DataBind();
        }
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            //if (Session["adm"] != null)
            //    dpCompany.SelectedIndex = 0;

            //ddlDuration.SelectedIndex = 0;

            lblMsg.Text = string.Empty;
            //lblDuration.Text = string.Empty;
            // btnPrint.Visible = false;
            //btnDownload.Visible = false;
            pnlESNDetail.Visible = false;
            pnlRMA.Visible = false;
            lblCount.Text = string.Empty;
            //lblRMA.Text = string.Empty;
            gvRMA.DataSource = null;
            gvRMA.DataBind();
            //ReloadPOStatus(0, 0);
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;


        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvRMA.PageIndex = e.NewPageIndex;
            ReloadData();

        }
        protected void gvEsnDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEsnDetail.PageIndex = e.NewPageIndex;
            ReloadESNDetail();

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
        public List<RMAEsnDetail> Sort<TKey>(List<RMAEsnDetail> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<RMAEsnDetail>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<RMAEsnDetail>();
            }
        }
        protected void gvRMA_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["RMAEsnDetail"] != null)
            {
                List<RMAEsnDetail> rmaList = (List<RMAEsnDetail>)Session["RMAEsnDetail"];

                if (rmaList != null && rmaList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        rmaList = Sort<RMAEsnDetail>(rmaList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        rmaList = Sort<RMAEsnDetail>(rmaList, SortExp, SortDirection.Descending);
                    }
                    Session["RMAEsnDetail"] = rmaList;
                    gvRMA.DataSource = rmaList;
                    gvRMA.DataBind();
                }
            }
        }

        private string GetSortDirection2(string column)
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
        public List<RMAESNDetail> Sort2<TKey>(List<RMAESNDetail> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<RMAESNDetail>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<RMAESNDetail>();
            }
        }
        protected void gvEsnDetail_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection2(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["RMAESNDetail"] != null)
            {
                List<RMAESNDetail> rmaList = (List<RMAESNDetail>)Session["RMAESNDetail"];

                if (rmaList != null && rmaList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        rmaList = Sort2<RMAESNDetail>(rmaList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        rmaList = Sort2<RMAESNDetail>(rmaList, SortExp, SortDirection.Descending);
                    }
                    gvEsnDetail.DataSource = rmaList;
                    gvEsnDetail.DataBind();
                }
            }
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            if (chkESN.Checked)
                DownloadRmaEsnDetail();
            else
                DownloadRmaEsnInfo();
        }
        private void DownloadRmaEsnInfo()
        {
            //string downLoadPath = string.Empty;
            //downLoadPath = System.Configuration.ConfigurationManager.AppSettings["RMAFilesStorage"].ToString();

            List<RMAEsnDetail> rmaStatusList = (Session["RMAEsnDetail"]) as List<RMAEsnDetail>;

            if (rmaStatusList != null && rmaStatusList.Count > 0)
            {

                string string2CSV = rmaStatusList.ToCSV();

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=RMAESNDetailExport.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(string2CSV);
                Response.Flush();
                Response.End();

            }
        }
        private void DownloadRmaEsnDetail()
        {
            //string downLoadPath = string.Empty;
            //downLoadPath = System.Configuration.ConfigurationManager.AppSettings["RMAFilesStorage"].ToString();

            List<RMAESNDetail> rmaList = (Session["RMAESNDetail"]) as List<RMAESNDetail>;

            if (rmaList != null && rmaList.Count > 0)
            {

                string string2CSV = rmaList.ToCSV();

                Response.Clear();
                Response.Buffer = true;
                Response.AddHeader("content-disposition", "attachment;filename=RMAESNDetailExport.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(string2CSV);
                Response.Flush();
                Response.End();

            }
        }

    }
}