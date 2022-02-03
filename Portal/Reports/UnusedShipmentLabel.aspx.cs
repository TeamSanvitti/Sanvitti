using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace avii.Reports
{
    public partial class UnusedShipmentLabel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = ConfigurationSettings.AppSettings["LogonPage"].ToString();
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
        protected void BindCustomer()
        {
            ddlCustomer.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            ddlCustomer.DataValueField = "CompanyID";
            ddlCustomer.DataTextField = "CompanyName";
            ddlCustomer.DataBind();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindUnusedLables();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }

        private void BindUnusedLables()
        {
            SV.Framework.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.Fulfillment.FulfillmentReportOperation>();

            bool IsCancel = false;
            int companyID = 0;
            string trackingNumber = string.Empty, dateFrom = string.Empty, dateTo = string.Empty;
            dateFrom = txtFromDate.Text.Trim();
            dateTo = txtToDate.Text.Trim();
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            btnDownload.Visible = false;
            btnCancelled.Visible = false;
            trackingNumber = txtTrackingNo.Text.Trim();
            gvLabel.DataSource = null;
            gvLabel.DataBind();
            string sortExpression = "ID";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;
            if (ddlStatus.SelectedIndex == 2)
                IsCancel = true;

            if (ddlCustomer.SelectedIndex > 0)
                companyID = Convert.ToInt32(ddlCustomer.SelectedValue);
            if (companyID > 0)
            {
                List<UnusedLabel> UnusedLabels = fulfillmentReportOperation.GetUnusedLabels(companyID, dateFrom, dateTo, trackingNumber, IsCancel);
                if(UnusedLabels != null && UnusedLabels.Count > 0)
                {
                    gvLabel.DataSource = UnusedLabels;
                    gvLabel.DataBind();
                    Session["UnusedLabels"] = UnusedLabels;
                    decimal totalShipPrice = UnusedLabels.Sum(x => x.FinalPostage);

                    lblCount.Text = "<strong>Total count:</strong> " + UnusedLabels.Count + ", &nbsp;&nbsp; &nbsp;&nbsp; <strong>Total Shipment Cost:</strong> $" + string.Format("{0:0.00}", totalShipPrice);

                    //lblCount.Text = "<strong>Total count:</strong> " + UnusedLabels.Count;

                    
                    btnDownload.Visible = true;
                    btnCancelled.Visible = true;
                }
                else
                {
                    gvLabel.DataSource = null;
                    gvLabel.DataBind();
                    lblMsg.Text = "No record exists.";
                }
            }
            else
                lblMsg.Text = "Customer is required!";


        }
        private void UnusedLabelsDownloadCSV()
        {
            List<UnusedLabel> UnusedLabels = Session["UnusedLabels"] as List<UnusedLabel>;
            if (UnusedLabels != null && UnusedLabels.Count > 0)
            {
                    string string2CSV = UnusedLabels.ToCSV();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=UnusedShipmentLabels.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();
                
            }
            //ServiceRequestDownload
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            btnDownload.Visible = false;
            btnCancelled.Visible = false;
            txtTrackingNo.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            ddlCustomer.SelectedIndex = 0;
            ddlStatus.SelectedIndex = 0;
            gvLabel.DataSource = null;
            gvLabel.DataBind();

        }

        protected void gvLabel_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLabel.PageIndex = e.NewPageIndex;
            if (Session["UnusedLabels"] != null)
            {
                List<UnusedLabel> UnusedLabels = (List<UnusedLabel>)Session["UnusedLabels"];

                gvLabel.DataSource = UnusedLabels;
                gvLabel.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

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
        public List<UnusedLabel> Sort<TKey>(List<UnusedLabel> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<UnusedLabel>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<UnusedLabel>();
            }
        }
        
        protected void gvLabel_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["UnusedLabels"] != null)
            {
                List<UnusedLabel> UnusedLabels = (List<UnusedLabel>)Session["UnusedLabels"];

                if (UnusedLabels != null && UnusedLabels.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        UnusedLabels = Sort<UnusedLabel>(UnusedLabels, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        UnusedLabels = Sort<UnusedLabel>(UnusedLabels, SortExp, SortDirection.Descending);
                    }
                    Session["UnusedLabels"] = UnusedLabels;
                    gvLabel.DataSource = UnusedLabels;
                    gvLabel.DataBind();
                }
            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            UnusedLabelsDownloadCSV();
        }

        protected void lnkPO_Command(object sender, CommandEventArgs e)
        {
            int poID = Convert.ToInt32(e.CommandArgument);
            Session["unused"] = poID;
            Session["poid"] = poID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../FulfillmentDetails.aspx')</script>", false);

        }
        protected void gvLabel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //header select all function
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("allchk")).Attributes.Add("onclick",
                    "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("allchk")).ClientID + "')");
            }
        }

        protected void btnCancelled_Click(object sender, EventArgs e)
        {
            SV.Framework.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.Fulfillment.FulfillmentReportOperation>();

            int userID = 0;
            bool IsValid = false;
            string unUsedIDs = "", tackingIDs = "", unUsedLabel = "", trackingLabel = "";
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(System.Int32));
                dt.Columns.Add("LabelSource", typeof(System.String));

                DataRow dtrow;

                foreach (GridViewRow row in gvLabel.Rows)
                {
                    CheckBox chkItem = row.FindControl("chkItem") as CheckBox;
                    if (chkItem.Checked)
                    {
                        dtrow = dt.NewRow();
                        HiddenField hdID = row.FindControl("hnID") as HiddenField;
                        HiddenField hnSource = row.FindControl("hnSource") as HiddenField;

                        dtrow["ID"] = Convert.ToInt32(hdID.Value);
                        dtrow["LabelSource"] = hnSource.Value;
                        dt.Rows.Add(dtrow);

                        if (!IsValid)
                            IsValid = true;
                    }

                }
                if (!IsValid)
                {
                    lblMsg.Text = "Please select check box in the grid to cancel the label(s)!";
                }
                else
                {
                    UnusedLabelInfo unusedLabelInfo = new UnusedLabelInfo();
                    unusedLabelInfo.dataTable = dt;
                    unusedLabelInfo.UserID = Convert.ToInt32(Session["UserID"]);
                    string returnMessage = fulfillmentReportOperation.UnusedLabelInsertUpdate(unusedLabelInfo);
                    if (string.IsNullOrEmpty(returnMessage))
                    {
                        BindUnusedLables();
                        lblMsg.Text = "Submitted successfully";
                    }
                    else
                        lblMsg.Text = returnMessage;
                }
            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
    }
}