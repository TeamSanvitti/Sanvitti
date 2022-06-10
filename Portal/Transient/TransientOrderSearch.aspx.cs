using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Common;
using SV.Framework.Inventory;
using SV.Framework.Models.Inventory;

namespace avii.Transient
{
    public partial class TransientOrderSearch : System.Web.UI.Page
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
                //if (Session["UserID"] == null)
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
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }




        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadTransientOrders();
        }
        private void LoadTransientOrders()
        {
            lblMsg.Text = "";
            lblCount.Text = "";
            gvOrders.DataSource = null;
            gvOrders.DataBind();

            TransientOrderOperation orderOperations = SV.Framework.Inventory.TransientOrderOperation.CreateInstance<SV.Framework.Inventory.TransientOrderOperation>();
             
            int companyID = 0, memoNumber = 0;
            string SKU = "", fromDate = "", todate = "", Supplier = "";

            int.TryParse(dpCompany.SelectedValue, out companyID);
            int.TryParse(txtMenoNumber.Text.Trim(), out memoNumber);
            SKU = txtSKU.Text.Trim();
            fromDate = txtDateFrom.Text.Trim();
            todate = txtDateTo.Text.Trim();
            Supplier = txtSupplierName.Text.Trim();
            if (companyID == 0 && memoNumber == 0 && string.IsNullOrWhiteSpace(SKU) && string.IsNullOrWhiteSpace(Supplier) && string.IsNullOrWhiteSpace(fromDate) && string.IsNullOrWhiteSpace(todate))
            {
                lblMsg.Text = "Please enter search criteria!";
            }
            else
            {
                List<TransientReceiveOrder> orderList = orderOperations.GetTransientOrders(memoNumber, SKU, companyID, fromDate, todate, Supplier);
                if (orderList != null && orderList.Count > 0)
                {
                    Session["transientOrders"] = orderList;
                    gvOrders.DataSource = orderList;
                    gvOrders.DataBind();
                    lblCount.Text = "<b>Total count: </b>" + orderList.Count;
                }
                else
                {
                    lblMsg.Text = "No record found";
                }
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {

        }
        protected void lnkAccept_Command(object sender, CommandEventArgs e)
        {

        }

        protected void lnkReceive_Command(object sender, CommandEventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            string orderInfo = Convert.ToString(e.CommandArgument);
            string[] array = orderInfo.Split(',');
            string orderStatus = "";
            bool IsESNRequired = true;
            string url = "../admin/ManageMslEsn.aspx";

            if (linkButton != null)
            {
                //    if (array.Length > 1)
                //    {
                //        int rowIndex = Convert.ToInt32(array[1]);

                orderStatus = linkButton.Text;
                //        GridViewRow row = gvOrders.Rows[rowIndex];

                //        TextBox textBox = row.FindControl("txtQty") as TextBox;
                //        if (textBox != null)
                //        {
                //            Session["orderqty"] = textBox.Text;
                //        }
                //    }
            }

            if (array.Length > 2)
                IsESNRequired = Convert.ToBoolean(array[2]);
            if (!IsESNRequired)
                url = "../admin/NonEsnInventory.aspx";

            if (array.Length > 3)
                Session["orderqty"] = array[3];

            TransientOrderOperation orderOperations = TransientOrderOperation.CreateInstance<TransientOrderOperation>();
            Int64 transientOrderID = Convert.ToInt64(array[0]);

            int userID = Convert.ToInt32(Session["UserID"]);

            string returnMessage = orderOperations.OrderTransientStatusUpdate(transientOrderID, orderStatus, userID);
            lblMsg.Text = returnMessage;
            //if (orderStatus.ToLower().Contains("approve"))
            {
                Session["transientOrderID"] = transientOrderID;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('" + url + "')</script>", false);
            }
        }

        protected void lnkReject_Command(object sender, CommandEventArgs e)
        {

        }

        protected void lnkCancel_Command(object sender, CommandEventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            Int64 transientOrderID = Convert.ToInt64(e.CommandArgument);

            string orderStatus = "";
            if (linkButton != null)
            {

                orderStatus = linkButton.Text;

                TransientOrderOperation orderOperations = TransientOrderOperation.CreateInstance<TransientOrderOperation>();
                int userID = Convert.ToInt32(Session["UserID"]);
                string returnMessage = orderOperations.OrderTransientStatusUpdate(transientOrderID, orderStatus, userID);
                LoadTransientOrders();
                lblMsg.Text = returnMessage;
            }
        }
    }
}