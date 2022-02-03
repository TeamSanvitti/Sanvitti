using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Reports
{
    public partial class MASQueueReport : System.Web.UI.Page
    {
        bool grid1SelectCommand = false;
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
        }
        private void SearchPurchaseOrder()
        {
            string fromDate, toDate, poNum, salesOrderNum;
            fromDate = toDate = null;
            poNum = salesOrderNum = string.Empty;
            
            
            try
            {
                lblMsg.Text = string.Empty;
                
                poNum = (txtPO.Text.Trim().Length > 0 ? txtPO.Text.Trim() : string.Empty);
                salesOrderNum = (txtSO.Text.Trim().Length > 0 ? txtSO.Text.Trim() : string.Empty);

                if (txtFromDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFromDate.Text, out dt))
                        fromDate = dt.ToShortDateString();
                    else
                        throw new Exception("Date From does not have correct format(MM/DD/YYYY)");
                }
                if (txtEndDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtEndDate.Text, out dt))
                        toDate = dt.ToShortDateString();
                    else
                        throw new Exception("Date To does not have correct format(MM/DD/YYYY)");

                }
                if (!chkFromMas.Checked && !chkToMas.Checked)
                {
                    lblMsg.Text = "Please select atleast one the the check box(To MAS or From MAS)";
                    lblCount.Text = string.Empty;
                    
                    Session["masqueue"] = null;
                    gvPOQuery.DataSource = null;
                    gvPOQuery.DataBind();
                    lblFromMas.Text = string.Empty;
                    lblFrmMasMsg.Text = string.Empty;
                    lblToMasMsg.Text = string.Empty;
                    Session["frommasqueue"] = null;
                    gvFromMas.DataSource = null;
                    gvFromMas.DataBind();
                }
                else
                {
                    DataTable reportTable = null;
                    DataTable reportFromMas = null;
                    if (chkToMas.Checked)
                    {
                        reportTable = ReportOperations.GetMASLogReport(fromDate, toDate);
                        if (reportTable != null && reportTable.Rows.Count > 0)
                        {
                            gvPOQuery.DataSource = reportTable;
                            gvPOQuery.DataBind();
                            lblCount.Text = "Total records: " + reportTable.Rows.Count;
                            toMasPnl.Visible = true;
                            Session["masqueue"] = reportTable;
                            lblToMasMsg.Text = string.Empty;
                        }
                        else
                        {
                            lblCount.Text = "Total records: 0";
                            //lblToMasMsg.Text = string.Empty;
                            lblToMasMsg.Text = "No record exists";
                            Session["masqueue"] = null;
                            gvPOQuery.DataSource = null;
                            gvPOQuery.DataBind();
                            toMasPnl.Visible = true;
                        }
                    }
                    else
                    {
                        lblCount.Text = string.Empty;
                        lblToMasMsg.Text = string.Empty;
                        Session["masqueue"] = null;
                        gvPOQuery.DataSource = null;
                        gvPOQuery.DataBind();
                        toMasPnl.Visible = false;
                    }
                    if (chkFromMas.Checked)
                    {
                        reportFromMas = ReportOperations.GetPOFromMASReport(fromDate, toDate, poNum, salesOrderNum);

                        if (reportFromMas != null && reportFromMas.Rows.Count > 0)
                        {
                            gvFromMas.DataSource = reportFromMas;
                            gvFromMas.DataBind();
                            frmMasPnl.Visible = true;
                            lblFromMas.Text = "Total records: " + reportFromMas.Rows.Count;
                            lblFrmMasMsg.Text = string.Empty;
                            Session["frommasqueue"] = reportFromMas;
                        }
                        else
                        {
                            lblFromMas.Text = "Total records: 0";
                            lblFrmMasMsg.Text = "No record exists";
                            Session["frommasqueue"] = null;
                            gvFromMas.DataSource = null;
                            gvFromMas.DataBind();
                            frmMasPnl.Visible = true;
                        }
                    }
                    else
                    {
                        lblFrmMasMsg.Text = string.Empty;
                        lblFromMas.Text = string.Empty;
                        Session["frommasqueue"] = null;
                        gvFromMas.DataSource = null;
                        gvFromMas.DataBind();
                        frmMasPnl.Visible = false;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            SearchPurchaseOrder();
            //string poNum, UPC, 
            //gvOrders.DataSource = ReportOperations.GetFulfillmentLogReport
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearAll();
        }
        private void ClearAll()
        {
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtSO.Text = string.Empty;
            txtPO.Text = string.Empty;
            lblFromMas.Text = string.Empty;
            lblFrmMasMsg.Text = string.Empty;
            frmMasPnl.Visible = false;
            gvPOQuery.DataSource = null;
            gvPOQuery.DataBind();
            gvFromMas.DataSource = null;
            gvFromMas.DataBind();
            toMasPnl.Visible = false;
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //DateTime tempDate = (DataBinder.Eval(e.Row.DataItem, "Tracking.ShipToDate") == null ? DateTime.MinValue : Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Tracking.ShipToDate")));
                //if (tempDate.CompareTo(DateTime.MinValue) == 0)
                //{
                //    e.Row.Cells[8].Text = string.Empty;
                //}

                ////Add delete confirmation message for Customer
                if (e.Row.RowIndex >= 0)
                {
                    ImageButton img = (ImageButton)e.Row.FindControl("imgDelPo");
                    img.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to delete this sales order number " +
                    DataBinder.Eval(e.Row.DataItem, "SalesOrderNo") + "')");
                }
            }
        }
        
        //this event occurs for any operation on the row of the grid
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Check if Add button clicked
            string gvChild, salesNum;
            int iIndex = 0;
            gvChild = string.Empty;

            if (e.CommandName == "sel" && grid1SelectCommand == false)
            {
                grid1SelectCommand = true;
                GridView gv = (GridView)sender;
                Int32 rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                ImageButton img = (ImageButton)gv.Rows[rowIndex].Cells[0].Controls[0];
                GridView childgv = (GridView)gv.Rows[rowIndex].FindControl("GridView2");

                if (Session["adm"] == null)
                {
                    foreach (DataControlField dc in childgv.Columns)
                    {
                        if (dc.HeaderText.Equals("Delete")) //dc.HeaderText.Equals("Edit") ||
                        {
                            dc.Visible = false;
                        }
                    }
                }

                if (img.AlternateText == "-")
                {
                    img.AlternateText = "+";
                    childgv.Visible = false;
                    img.ImageUrl = "../images/plus.gif";
                }
                else
                {
                    salesNum = Convert.ToString(gv.DataKeys[rowIndex].Value);
                    string[] arr = salesNum.Split(',');
                    //ViewState["poid"] = salesNum;
                    //ViewState["RowIndex"] = rowIndex;

                    childgv.DataSource = ReportOperations.ChildDataSource(arr[0], string.Empty);
                    childgv.DataBind();
                    childgv.Visible = true;
                    img.AlternateText = "-";
                    img.ImageUrl = "../images/minus.gif";
                }
                gvPOQuery.RowCommand -= GridView1_RowCommand;
            }
        }

        
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string salesOrderNo = gvPOQuery.DataKeys[e.RowIndex].Value.ToString();
            try
            {

                ReportOperations.DeleteMasReport(salesOrderNo);

                SearchPurchaseOrder();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sales Order deleted successfully');</script>", false);
                //BindPO();
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order deleted successfully');</script>");
                //gvPOQuery.DataSource = ds;
                //gvPOQuery.DataBind();
                //btnSearch_Click(sender, e);
            }
            catch { }
        }

        //This event occurs after RowDeleting to catch any constraints while deleting
        protected void GridView1_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            //Check if there is any exception while deleting
            if (e.Exception != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
                e.ExceptionHandled = true;
            }
        }

        

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPOQuery.PageIndex = e.NewPageIndex;

            if (Session["masqueue"] != null)
            {
                DataTable dt = (DataTable)Session["masqueue"];
                gvPOQuery.DataSource = dt;
                gvPOQuery.DataBind();
            }
            
        }

        protected void gvFromMas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvFromMas.PageIndex = e.NewPageIndex;

            if (Session["frommasqueue"] != null)
            {
                DataTable dt = (DataTable)Session["frommasqueue"];
                gvFromMas.DataSource = dt;
                gvFromMas.DataBind();
            }

        }
        //this event occurs for any operation on the row of the grid
        protected void gvFromMas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Check if Add button clicked
            string gvChild, salesNum;
            //int iIndex = 0;
            gvChild = string.Empty;

            if (e.CommandName == "sel" && grid1SelectCommand == false)
            {
                grid1SelectCommand = true;
                GridView gv = (GridView)sender;
                Int32 rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                ImageButton img = (ImageButton)gv.Rows[rowIndex].Cells[0].Controls[0];
                GridView childgv = (GridView)gv.Rows[rowIndex].FindControl("GridView2");

                //if (Session["adm"] == null)
                //{
                //    foreach (DataControlField dc in childgv.Columns)
                //    {
                //        if (dc.HeaderText.Equals("Delete")) //dc.HeaderText.Equals("Edit") ||
                //        {
                //            dc.Visible = false;
                //        }
                //    }
                //}

                if (img.AlternateText == "-")
                {
                    img.AlternateText = "+";
                    childgv.Visible = false;
                    img.ImageUrl = "../images/plus.gif";
                }
                else
                {
                    salesNum = Convert.ToString(gv.DataKeys[rowIndex].Value);
                    //string[] arr = salesNum.Split(',');
                    //ViewState["poid"] = salesNum;
                    //ViewState["RowIndex"] = rowIndex;

                    childgv.DataSource = ReportOperations.ChildDataSourceFromMas(salesNum, string.Empty);
                    childgv.DataBind();
                    childgv.Visible = true;
                    img.AlternateText = "-";
                    img.ImageUrl = "../images/minus.gif";
                }
                gvFromMas.RowCommand -= gvFromMas_RowCommand;
            }
        }

        
    }
}