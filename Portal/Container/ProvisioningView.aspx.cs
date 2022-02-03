using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Container
{
    public partial class ProvisioningView : System.Web.UI.Page
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
                if(Session["poid"] != null)
                {
                    int poid = Convert.ToInt32(Session["poid"]);
                    Session["poid"] = null;
                    GetPODetail(poid);
                }
            }
        }
        private void GetPODetail(int poid)
        {
            SV.Framework.Fulfillment.POProvisioningInfo pOProvisioningView = SV.Framework.Fulfillment.ProvisioningOperation.GetPurchaseOrderProvisioningView(poid);
            if(pOProvisioningView != null && pOProvisioningView.FulfillmentHeaders != null)
            {
                if(pOProvisioningView.FulfillmentHeaders.Count> 0)
                {
                    lblPoDate.Text = pOProvisioningView.FulfillmentHeaders[0].PODate;
                    lblPONum.Text = pOProvisioningView.FulfillmentHeaders[0].FulfillmentNumber;
                    lblPoStatus.Text = pOProvisioningView.FulfillmentHeaders[0].POStatus;
                    lblShipDate.Text = pOProvisioningView.FulfillmentHeaders[0].ShipTo_Date;

                    rptSKU.DataSource = pOProvisioningView.FulfillmentHeaders;
                    rptSKU.DataBind();
                }
                if (pOProvisioningView.ProvisioningDetails != null && pOProvisioningView.ProvisioningDetails.Count > 0)
                {
                    gvESNs.DataSource = pOProvisioningView.ProvisioningDetails;
                    gvESNs.DataBind();
                    Session["ProvisioningDetails"] = pOProvisioningView.ProvisioningDetails;
                }
            }
        }

        private void BindEsn(string esn)
        {
            lblEsnMsg.Text = "";
            List<avii.Classes.ESNLog> esnLogs = avii.Classes.clsInventoryDB.GetEsnLogs(esn);
            if (esnLogs != null && esnLogs.Count > 0)
            {
                rptEsnLog.DataSource = esnLogs;
                rptEsnLog.DataBind();
            }
            else
            {

                rptEsnLog.DataSource = null;
                rptEsnLog.DataBind();
                lblEsnMsg.Text = "No record exists";
            }
        }

        protected void imgESN_Command(object sender, CommandEventArgs e)
        {
            string esn = Convert.ToString(e.CommandArgument);
            BindEsn(esn);

            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void gvESNs_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
               
                if (e.Row.RowIndex >= 0)
                {
                    ImageButton imgESN = (ImageButton)e.Row.FindControl("imgESN");
                    imgESN.OnClientClick = "openDialogAndBlock('ESN Log', '" + imgESN.ClientID + "')";
                }
            }
        }

        protected void gvESNs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvESNs.PageIndex = e.NewPageIndex;
            if (Session["ProvisioningDetails"] != null)
            {
                List<SV.Framework.Fulfillment.ProvisioningDetail> poList = (List<SV.Framework.Fulfillment.ProvisioningDetail>)Session["ProvisioningDetails"];

                gvESNs.DataSource = poList;
                gvESNs.DataBind();
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
        public List<SV.Framework.Fulfillment.ProvisioningDetail> Sort<TKey>(List<SV.Framework.Fulfillment.ProvisioningDetail> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<SV.Framework.Fulfillment.ProvisioningDetail>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<SV.Framework.Fulfillment.ProvisioningDetail>();
            }
        }
        protected void gvESNs_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["ProvisioningDetails"] != null)
            {
                List<SV.Framework.Fulfillment.ProvisioningDetail> ProvisioningDetails = (List<SV.Framework.Fulfillment.ProvisioningDetail>)Session["ProvisioningDetails"];

                if (ProvisioningDetails != null && ProvisioningDetails.Count > 0)
                {
                    //var list = shipments;
                    if (Sortdir == "ASC")
                    {
                        ProvisioningDetails = Sort<SV.Framework.Fulfillment.ProvisioningDetail>(ProvisioningDetails, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        ProvisioningDetails = Sort<SV.Framework.Fulfillment.ProvisioningDetail>(ProvisioningDetails, SortExp, SortDirection.Descending);
                    }
                    Session["ProvisioningDetails"] = ProvisioningDetails;
                    gvESNs.DataSource = ProvisioningDetails;
                    gvESNs.DataBind();
                }
            }
        }
    }
}