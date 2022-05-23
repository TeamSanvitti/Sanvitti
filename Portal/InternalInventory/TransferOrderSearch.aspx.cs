using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;
using SV.Framework.Models.Common;
using SV.Framework.Inventory;
using System.Configuration;
using System.Reflection;

namespace avii.InternalInventory
{
    public partial class TransferOrderSearch : System.Web.UI.Page
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
            LoadTransferOrders();
        }
        private void LoadTransferOrders()
        {
            lblMsg.Text = "";
            lblCount.Text = "";
            gvOrders.DataSource = null;
            gvOrders.DataBind();

            SV.Framework.Inventory.TransferOrderOperation orderOperations = SV.Framework.Inventory.TransferOrderOperation.CreateInstance<SV.Framework.Inventory.TransferOrderOperation>();

            string transferOrderNumber =txtOrderTransferNumber.Text.Trim();
            string fromDate =txtDateFrom.Text.Trim();
            string toDate =txtDateTo.Text.Trim();
            string SKU =txtSKU.Text.Trim();
            int companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if(companyID == 0 && string.IsNullOrWhiteSpace(transferOrderNumber) && string.IsNullOrWhiteSpace(fromDate) && string.IsNullOrWhiteSpace(toDate) && string.IsNullOrWhiteSpace(SKU))
            {
                lblMsg.Text = "Please enter search criteria!";
            }
            else
            {
                List<TransferOrder> orderList = orderOperations.GetTransferOrders(transferOrderNumber, SKU, companyID, fromDate, toDate);
                if (orderList != null && orderList.Count > 0)
                {
                    Session["orderList"] = orderList;
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
        private void ClearForm()
        {

            lblCount.Text = "";
            lblMsg.Text = "";
            Session["orderList"] = null;
            gvOrders.DataSource = null;
            gvOrders.DataBind();
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            txtSKU.Text = "";
            txtOrderTransferNumber.Text = "";
            dpCompany.SelectedIndex = 0;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void gvOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvOrders.PageIndex = e.NewPageIndex;

            if (Session["orderList"] != null)
            {
                List<TransferOrder> orderList = Session["orderList"] as List<TransferOrder>;

                gvOrders.DataSource = orderList;
                gvOrders.DataBind();
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
        public List<TransferOrder> Sort<TKey>(List<TransferOrder> list, string sortBy, SortDirection direction)
        {
            PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
            if (direction == SortDirection.Ascending)
            {
                return list.OrderBy(e => property.GetValue(e, null)).ToList<TransferOrder>();
            }
            else
            {
                return list.OrderByDescending(e => property.GetValue(e, null)).ToList<TransferOrder>();
            }
        }

        protected void gvOrders_Sorting(object sender, GridViewSortEventArgs e)
        {
            string Sortdir = GetSortDirection(e.SortExpression);
            string SortExp = e.SortExpression;

            if (Session["orderList"] != null)
            {
                List<TransferOrder> orderList = (List<TransferOrder>)Session["orderList"];

                if (orderList != null && orderList.Count > 0)
                {
                    var list = orderList;
                    if (Sortdir == "ASC")
                    {
                        list = Sort<TransferOrder>(list, SortExp, SortDirection.Ascending);
                    }
                    else
                    {
                        list = Sort<TransferOrder>(list, SortExp, SortDirection.Descending);
                    }
                    Session["orderList"] = list;
                    gvOrders.DataSource = list;
                    gvOrders.DataBind();
                }
            }

        }

        protected void lnkAccept_Command(object sender, CommandEventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            string orderInfo = Convert.ToString(e.CommandArgument);
            string[] array = orderInfo.Split(','); 
            string orderStatus = "";
            if (linkButton != null && linkButton.Text.ToLower().Contains("approve"))
            {
                int rowIndex = Convert.ToInt32(array[1]);

                orderStatus = linkButton.Text;
                GridViewRow row = gvOrders.Rows[rowIndex];

                TextBox textBox = row.FindControl("txtQty") as TextBox;
                if (textBox != null)
                {
                    Session["orderqty"] = textBox.Text;
                }
            }
            SV.Framework.Inventory.TransferOrderOperation orderOperations = SV.Framework.Inventory.TransferOrderOperation.CreateInstance<SV.Framework.Inventory.TransferOrderOperation>();
            Int64 orderTransferID = Convert.ToInt64(array[0]);
            int userID = Convert.ToInt32(Session["UserID"]);

            string returnMessage = orderOperations.OrderTransferStatusUpdate(orderTransferID, orderStatus, userID);
            lblMsg.Text = returnMessage;
            if(returnMessage.ToLower().Contains("approve"))
            {
                Session["orderTransferID"] = orderTransferID;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../admin/ManageMslEsn.aspx')</script>", false);
            }
        }
        protected void lnkReceive_Command(object sender, CommandEventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            string orderInfo = Convert.ToString(e.CommandArgument);
            string[] array = orderInfo.Split(',');
            string orderStatus = "";
            if (linkButton != null)
            {
                int rowIndex = Convert.ToInt32(array[1]);

                orderStatus = linkButton.Text;
                GridViewRow row = gvOrders.Rows[rowIndex];

                TextBox textBox = row.FindControl("txtQty") as TextBox;
                if (textBox != null)
                {
                    Session["orderqty"] = textBox.Text;
                }
            }
            SV.Framework.Inventory.TransferOrderOperation orderOperations = SV.Framework.Inventory.TransferOrderOperation.CreateInstance<SV.Framework.Inventory.TransferOrderOperation>();
            Int64 orderTransferID = Convert.ToInt64(array[0]);
            
            //int userID = Convert.ToInt32(Session["UserID"]);

            //string returnMessage = orderOperations.OrderTransferStatusUpdate(orderTransferID, orderStatus, userID);
            //lblMsg.Text = returnMessage;
            //if (orderStatus.ToLower().Contains("approve"))
            {
                Session["orderTransferID"] = orderTransferID;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../admin/ManageMslEsn.aspx')</script>", false);
            }
        }

        protected void lnkReject_Command(object sender, CommandEventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            Int64 orderTransferID = Convert.ToInt64(e.CommandArgument);

            string orderStatus = "";
            if (linkButton != null)
            {
                
                orderStatus = linkButton.Text;               

                SV.Framework.Inventory.TransferOrderOperation orderOperations = SV.Framework.Inventory.TransferOrderOperation.CreateInstance<SV.Framework.Inventory.TransferOrderOperation>();
                int userID = Convert.ToInt32(Session["UserID"]);

                string returnMessage = orderOperations.OrderTransferStatusUpdate(orderTransferID, orderStatus, userID);
                LoadTransferOrders();
                lblMsg.Text = returnMessage;
            }
        }

        protected void lnkCancel_Command(object sender, CommandEventArgs e)
        {
            LinkButton linkButton = sender as LinkButton;
            Int64 orderTransferID = Convert.ToInt64(e.CommandArgument);

            string orderStatus = "";
            if (linkButton != null)
            {

                orderStatus = linkButton.Text;

                SV.Framework.Inventory.TransferOrderOperation orderOperations = SV.Framework.Inventory.TransferOrderOperation.CreateInstance<SV.Framework.Inventory.TransferOrderOperation>();
                int userID = Convert.ToInt32(Session["UserID"]);

                string returnMessage = orderOperations.OrderTransferStatusUpdate(orderTransferID, orderStatus, userID);
                LoadTransferOrders();
                lblMsg.Text = returnMessage;
            }
        }

        protected void imgTO_Command(object sender, CommandEventArgs e)
        {
            Int64 orderTransferID = Convert.ToInt64(e.CommandArgument);
            Session["OrderTransferID"] = orderTransferID;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('TransferOrderAssignments.aspx')</script>", false);

        }
    }
}