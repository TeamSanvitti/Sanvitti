using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Models.Common;

namespace avii.Reports
{
    public partial class ShipmentSummary : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                BindCustomer();
                BindShipPackage();
                BindShipVia();
            }
        }
        private void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private void BindShipVia()
        {
            try
            {
                SV.Framework.Fulfillment.PurchaseOrder purchaseOrderOperation = SV.Framework.Fulfillment.PurchaseOrder.CreateInstance<SV.Framework.Fulfillment.PurchaseOrder>();
                List<ShipBy> shipBy = purchaseOrderOperation.GetShipByList();

                ddlShipVia.DataSource = shipBy;
                ddlShipVia.DataTextField = "ShipCodeNText";
                ddlShipVia.DataValueField = "ShipByCode";
                ddlShipVia.DataBind();
                System.Web.UI.WebControls.ListItem item1 = new System.Web.UI.WebControls.ListItem("", "");
                ddlShipVia.Items.Insert(0, item1);

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void BindShipPackage()
        {
            ddlShape.Items.Clear();
            string[] itemNames = System.Enum.GetNames(typeof(SV.Framework.Common.LabelGenerator.ShipPackageShape));
            for (int i = 0; i <= itemNames.Length - 1; i++)
            {
                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(itemNames[i], itemNames[i]);
                ddlShape.Items.Add(item);
            }
            System.Web.UI.WebControls.ListItem item1 = new System.Web.UI.WebControls.ListItem("", "");
            ddlShape.Items.Insert(0, item1);

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SV.Framework.Fulfillment.FulfillmentReportOperation fulfillmentReportOperation = SV.Framework.Fulfillment.FulfillmentReportOperation.CreateInstance<SV.Framework.Fulfillment.FulfillmentReportOperation>();

            bool IsValid = true;
            int companyID = 0;
            decimal totalShipPrice = 0;
            string shipVia = string.Empty, labelType = string.Empty, shipPackage = string.Empty, dateFrom = string.Empty, dateTo = string.Empty;
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (ddlLabelType.SelectedIndex > 0)
                labelType = ddlLabelType.SelectedValue;
            if (ddlShape.SelectedIndex > 0)
                shipPackage = ddlShape.SelectedValue;
            if (ddlShipVia.SelectedIndex > 0)
                shipVia = ddlShipVia.SelectedValue;

            dateFrom = txtDateFrom.Text.Trim();
            dateTo = txtDateTo.Text.Trim();
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            btnSummary.Visible = false;
            string sortExpression = "LabelGenerationDate";
            string sortDirection = "DESC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            if (companyID == 0 && !string.IsNullOrEmpty(shipVia) && !string.IsNullOrEmpty(labelType) && !string.IsNullOrEmpty(shipPackage) && !string.IsNullOrEmpty(dateFrom) && !string.IsNullOrEmpty(dateTo))
            {
                IsValid = false;
            }
            if(IsValid)
            {
                List<ShipmentInfo> shipmentList = fulfillmentReportOperation.GetShipmentSummary(companyID, labelType, dateFrom, dateTo, shipVia, shipPackage);
                if (shipmentList != null && shipmentList.Count > 0)
                {
                    Session["shipmentList"] = shipmentList;
                    gvShipment.DataSource = shipmentList;
                    gvShipment.DataBind();
                    btnSummary.Visible = true;
                    totalShipPrice = shipmentList.Sum(x => x.FinalPostage);

                    lblCount.Text = "<strong>Total count:</strong> " + shipmentList.Count + ", &nbsp;&nbsp; &nbsp;&nbsp; <strong>Total Cost:</strong> $" + String.Format("{0:F2}", totalShipPrice);
                }
                else
                {
                    lblMsg.Text = "No record exists.";
                    gvShipment.DataSource = null;
                    gvShipment.DataBind();
                }
            }
            else
            {
                lblMsg.Text = "Please select the search criteria";
                gvShipment.DataSource = null;
                gvShipment.DataBind();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Reset();
        }
        private void Reset()
        {
            lblMsg.Text = string.Empty;
            txtDateFrom.Text = string.Empty;
            txtDateTo.Text = string.Empty;
            dpCompany.SelectedIndex = 0;
            ddlLabelType.SelectedIndex = 0;
            ddlShape.SelectedIndex = 0;
            ddlShipVia.SelectedIndex = 0;
            gvShipment.DataSource = null;
            gvShipment.DataBind();

        }

        protected void gvShipment_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvShipment_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvShipment.PageIndex = e.NewPageIndex;
            if (Session["shipmentList"] != null)
            {
                List<ShipmentInfo> shipmentList = (List<ShipmentInfo>)Session["shipmentList"];

                gvShipment.DataSource = shipmentList;
                gvShipment.DataBind();
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
        public List<ShipmentInfo> Sort<TKey>(List<ShipmentInfo> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<ShipmentInfo>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<ShipmentInfo>();
            }
        }

        protected void gvShipment_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["shipmentList"] != null)
            {
                List<ShipmentInfo> shipmentList = (List<ShipmentInfo>)Session["shipmentList"];

                if (shipmentList != null && shipmentList.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        shipmentList = Sort<ShipmentInfo>(shipmentList, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        shipmentList = Sort<ShipmentInfo>(shipmentList, SortExp, SortDirection.Descending);
                    }
                    Session["shipmentList"] = shipmentList;
                    gvShipment.DataSource = shipmentList;
                    gvShipment.DataBind();
                }
            }
        }

        protected void btnSummary_Click(object sender, EventArgs e)
        {
            rptPO.DataSource = null;
            rptPO.DataBind();
            rptRMA.DataSource = null;
            rptRMA.DataBind();
            if (Session["shipmentList"] != null)
            {
                List<ShipmentInfo> shipmentList = Session["shipmentList"] as List<ShipmentInfo>;
                if (shipmentList != null && shipmentList.Count > 0)
                {

                    var poShipmentList = shipmentList.Where(w => w.LabelType.ToLower() == "fulfillment").GroupBy(x => new { x.ShipMethod, x.ShipPackage, x.LabelType }).Select(g => new {
                        ShipMethod = g.Key.ShipMethod,
                        ShipPackage = g.Key.ShipPackage,
                        Cost = g.Sum(c=> Math.Round(Convert.ToDecimal(c.FinalPostage), 2))
                    }).ToList();

                    var rmaShipmentList = shipmentList.Where(w => w.LabelType.ToUpper() == "RMA").GroupBy(x => new { x.ShipMethod, x.ShipPackage, x.LabelType }).Select(g => new {
                        ShipMethod = g.Key.ShipMethod,
                        ShipPackage = g.Key.ShipPackage,
                        Cost = g.Sum(c => Math.Round(Convert.ToDecimal(c.FinalPostage), 2))
                    }).ToList();

                   if(poShipmentList != null && poShipmentList.Count > 0)
                    {
                        rptPO.DataSource = poShipmentList;
                        rptPO.DataBind();
                    }
                    if (rmaShipmentList != null && rmaShipmentList.Count > 0)
                    {
                        rptRMA.DataSource = rmaShipmentList;
                        rptRMA.DataBind();
                    }

                    //var shipmentList1 = shipmentList.Where(w => w.LabelType == "Fulfillment").GroupBy(x => new { x.ShipMethod, x.ShipPackage, x.LabelType }).ToList();
                }
            }
            RegisterStartupScript("jsUnblockDialog", "unblockSummaryDialog();");

        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }


        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }

        protected void lnkView_Command(object sender, CommandEventArgs e)
        {
            string commandValue = Convert.ToString(e.CommandArgument);
            string[] array = commandValue.Split(',');
           // int id = 0;
            string url = string.Empty;
            if(array != null && array.Length > 0)
            {
                
                if(array.Length > 1)
                {
                    if (array[1].ToUpper() == "RMA")
                    {
                        url = "../RMA/RmaView.aspx";
                        Session["rmaGUID"] = array[0];
                    }
                    else
                    {
                        if (array.Length > 2)
                        {
                            url = "../FulfillmentDetails.aspx";
                            Session["poNum"] = array[2];
                            Session["po_id"] = array[0];
                        }
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('"+url+"')</script>", false);

                }
            }

        }
    }
}