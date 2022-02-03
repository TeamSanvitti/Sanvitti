using avii.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Fulfillment;
namespace avii
{
    public partial class UnprovisionPoRequest : System.Web.UI.Page
    {
        private SV.Framework.Fulfillment.UnprovisioningOperation unprovisioningOperation = SV.Framework.Fulfillment.UnprovisioningOperation.CreateInstance<SV.Framework.Fulfillment.UnprovisioningOperation>();

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
                if (Session["adm"] == null)
                {
                    trCustomer.Visible = false;
                }
                else
                    BindCustomers();
            }
        }

        protected void BindCustomers()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 1);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPO();

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }

        protected void imgPO_Command(object sender, CommandEventArgs e)
        {
            int poID = Convert.ToInt32(e.CommandArgument);

            Session["unpoid"] = poID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('FulfillmentDetails.aspx')</script>", false);

        }
        private void BindPO()
        {
            string poNum;
            int companyID = 0;
            lblMsg.Text = "";
            lblCount.Text = "";

            string sortExpression = "PurchaseOrderDate";
            string sortDirection = "ASC";
            ViewState["SortDirection"] = sortDirection;
            ViewState["SortExpression"] = sortExpression;

            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                }
                else
                {
                    lblMsg.Text = "{Customer is required!";
                }
            }
            else
            {
                UserInfo userInfo = Session["userInfo"] as UserInfo;
                if (userInfo != null)
                    companyID = userInfo.CompanyGUID;
            }
            poNum = txtPO.Text.Trim();

            if (!string.IsNullOrEmpty(poNum))
            {
                try
                {
                    List<UnprovisionPOs> poList = unprovisioningOperation.GetPOUnprovisioingSearch(companyID, poNum);// avii.Classes.PurchaseOrder.GerPurchaseOrdersNew(poNum, contactName, fromDate, toDate, userID, statusID, companyID, esn, avOrder, mslNumber, phoneCategory, sku, storeID, null, null, shipFrom, shipTo, 0, trackingNumber, customerOrderNumber, POType, StockInDemand);
                    Session["UPO"] = poList;
                    if (poList != null && poList.Count > 0)
                    {
                        lblCount.Text = "<strong>Total count:</strong> " + poList.Count.ToString();
                        gvPOQuery.DataSource = poList;
                        gvPOQuery.DataBind();
                    }
                    else
                    {
                        lblCount.Text = string.Empty;
                        gvPOQuery.DataSource = null;
                        gvPOQuery.DataBind();
                        lblMsg.Text = "No record exists";
                    }
                }
                catch (Exception ex)
                {
                    if (ex.Message.ToString().Contains("Timeout expired"))
                        lblMsg.Text = "PO Query is taking longer time than usual, add more search criteria for faster result.";
                    else
                        lblMsg.Text = ex.Message;
                }
            }
            else
            {
                //CleanForm();
                lblMsg.Text = "Please enter the fulfillment number";
            }
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);
        }

    }
}