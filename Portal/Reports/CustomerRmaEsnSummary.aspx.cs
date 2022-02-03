using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using avii.Classes;
using System.Reflection;
using SV.Framework.Models.RMA;
//using SV.Framework.Models.Common;

namespace avii.Reports
{
    public partial class CustomerRmaEsnSummary : System.Web.UI.Page
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
                //btnPrint.Visible = false;
                btnDownload.Visible = false;
                BindCustomer();
                BindRMAStatuses();
                int timeInterval = 30;
                if (timeInterval == 1)
                    lblDuration.Text = DateTime.Now.ToShortDateString();
                else
                {
                    lblDuration.Text = "RMA Date Range: " + DateTime.Now.AddDays(-timeInterval).ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
                }

                //if (Request["pos"] != null && Request["t"] != null && Request["cid"] != null)
                //{
                //    Session["postatus"] = Request["pos"].ToString();
                //    Session["days"] = Request["t"].ToString();
                //    Session["cid"] = Request["cid"].ToString();

                //    Response.Redirect("~/POQueryNew.aspx");
                //}
            }
        }
        private void BindRMAStatuses()
        {
            try
            {
                int companyID = 0;
                SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

                List<CustomerRMAStatus> customerRMAStatusList = rmaOperation.GetCustomerRMAStatusList(companyID, true);
                if (customerRMAStatusList != null && customerRMAStatusList.Count > 0)
                {
                    ddlRmaStaus.DataSource = customerRMAStatusList;
                    ddlRmaStaus.DataValueField = "StatusID";
                    ddlRmaStaus.DataTextField = "StatusDescription";

                    ddlRmaStaus.DataBind();
                    ddlRmaStaus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                    ddlEsnStatus.DataSource = customerRMAStatusList;
                    ddlEsnStatus.DataValueField = "StatusID";
                    ddlEsnStatus.DataTextField = "StatusDescription";

                    ddlEsnStatus.DataBind();
                    ddlEsnStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                    

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
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
            //int timeInterval = 0;
            int esnStatusID = 0;
            int rmaStatusID = 0;
            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;
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

            if (companyID > 0)
                PopulateData(companyID, fromDate, toDate, esnStatusID, rmaStatusID);
            else
                lblMsg.Text = "Please select Customer!";
        }
        private void PopulateData(int companyID, DateTime fromDate, DateTime toDate, int esnStatusID, int rmaStatusID)
        {
            SV.Framework.RMA.RmaReportOperation rmaReportOperation = SV.Framework.RMA.RmaReportOperation.CreateInstance<SV.Framework.RMA.RmaReportOperation>();

            //if (Session["adm"] == null)
            //{
            //    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            //    if (userInfo != null)
            //    {
            //        companyID = userInfo.CompanyGUID;

            //    }

            //}
            //if (timeInterval == 0)
            //{
            //    lblCount.Text = string.Empty;
            //    gvRMA.DataSource = null;
            //    lblMsg.Text = string.Empty;
            //    Session["rmarsnstatuses"] = null;
            //}
            //else
            {
                List<RMAEsnDetail> rmaStatusList = rmaReportOperation.GetRmaEsnOnlyReport(companyID, "", "", fromDate, toDate, esnStatusID, rmaStatusID);

              //  List<RmaEsnStatuses> rmaStatusList = ReportOperations.GetCustomerRmaEsnStatusReport(companyID, fromDate, toDate, esnStatusID, rmaStatusID);

                Session["rmarsnstatuses"] = rmaStatusList;
                if (rmaStatusList != null && rmaStatusList.Count > 0)
                {
                    gvRMA.DataSource = rmaStatusList;
                    lblCount.Text = "<strong>Total ESN:</strong> " + Convert.ToString(rmaStatusList.Count);
                    lblMsg.Text = string.Empty;
                   // btnPrint.Visible = true;
                    btnDownload.Visible = true;
                }
                else
                {
                    //btnPrint.Visible = false;
                    btnDownload.Visible = false;
                    lblCount.Text = string.Empty;
                    gvRMA.DataSource = null;
                    lblMsg.Text = "No records found";
                }
            }
            gvRMA.DataBind();
        }
        private void ReloadData()
        {
            List<RMAEsnDetail> rmaStatusList = null;
            if (Session["rmarsnstatuses"] != null)
            {
                rmaStatusList = (List<RMAEsnDetail>)Session["rmarsnstatuses"];


                Session["rmarsnstatuses"] = rmaStatusList;
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
               // btnPrint.Visible = false;
                gvRMA.DataSource = null;
                lblMsg.Text = string.Empty;
                lblMsg.Text = "Session expire!";
            }
            gvRMA.DataBind();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (Session["adm"] != null)
                dpCompany.SelectedIndex = 0;

            //ddlDuration.SelectedIndex = 0;

            lblMsg.Text = string.Empty;
            lblDuration.Text = string.Empty;
          //  btnPrint.Visible = false;
            btnDownload.Visible = false;
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
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadRmaEsnInfo();
        }
        private void DownloadRmaEsnInfo()
        {
            string downLoadPath = string.Empty;
            downLoadPath = System.Configuration.ConfigurationManager.AppSettings["RMAFilesStorage"].ToString();

            List<RMAEsnDetail> rmaStatusList = (Session["rmarsnstatuses"]) as List<RMAEsnDetail>;
            
            if (rmaStatusList != null && rmaStatusList.Count > 0)
            {
                
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = "Esn" + DateTime.Now.Ticks + ".csv";
                bool found = false;
                //System.IO.FileInfo file = null;
                //file = new System.IO.FileInfo(path + fileName);
                //if (file.Exists)
                //{
                //    file.Delete();
                //}

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                sb.Append("RMA#,RmaDate,RmaStatus,ESN,SKU,ProductName,Fulfillment#,EsnStatus,TriageDate,TriageStatus,Reason,TrackingNumber\r\n");

                foreach (RMAEsnDetail rmaESN in rmaStatusList)
                {
                    sb.Append(rmaESN.RmaNumber + ","
                        + rmaESN.RmaDate + ","
                        + rmaESN.RmaStatus + ","
                        + rmaESN.ESN + ","
                                + rmaESN.SKU + ","
                                + rmaESN.ProductName + ","
                                + rmaESN.FulfillmentNumber + "," 
                                + rmaESN.EsnStatus + ","
                                + rmaESN.TriageDate + ","
                                + rmaESN.TriageStatus + ","
                                + rmaESN.Reason + ","
                                + rmaESN.TrackingNumber 

                                + "\r\n");

                        found = true;
                }

                //try
                //{
                //    using (StreamWriter sw = new StreamWriter(file.FullName))
                //    {
                //        sw.WriteLine(sb.ToString());
                //        sw.Flush();
                //        sw.Close();
                //    }
                //}
                //catch (Exception ex)
                //{
                //    lblMsg.Text = ex.Message;
                //}

                if (found)
                {
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(sb.ToString());
                    Response.Flush();
                    Response.End();
                    //Response.Clear();
                    //Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    //Response.ContentType = "application/octet-stream";
                    //Response.WriteFile(file.FullName);
                    //Response.End();
                }
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

            if (Session["rmarsnstatuses"] != null)
            {
                List<RMAEsnDetail> rmaList = (List<RMAEsnDetail>)Session["rmarsnstatuses"];

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
                    Session["rmarsnstatuses"] = rmaList;
                    gvRMA.DataSource = rmaList;
                    gvRMA.DataBind();
                }
            }
        }

    }
}