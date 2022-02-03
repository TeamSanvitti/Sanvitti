using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using avii.Classes;
//using Microsoft.Office.Interop;
namespace avii
{
   
    public partial class PurchaseOrderQuery : System.Web.UI.Page
    {
        DataSet ds;
        string gvUniqueID = String.Empty;
        int gvNewPageIndex = 0;
        int gvEditIndex = -1;
        string gvSortExpr = String.Empty;
        string downLoadPath = string.Empty;
        string writer = "csv";
        bool grid1SelectCommand = false;
        private string gvSortDir
        {

            get { return ViewState["SortDirection"] as string ?? "ASC"; }

            set { ViewState["SortDirection"] = value; }

        }

        protected void bindCustomerDropDown()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 1);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

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
                if (Session["userInfo"] == null)
                {
                    Response.Redirect(url);
                }
            }
            //else
            //{
            //    MenuHeader.Visible = false;
            //    HeadAdmin.Visible = true;
            //}

            if (!IsPostBack)
            {

                if (Session["adm"] == null)
                {
                    foreach (DataControlField dc in GridView1.Columns)
                    {
                        if (dc.HeaderText.Equals("Edit") || dc.HeaderText.Equals("Delete"))
                        {
                            dc.Visible = false;
                        }
                    }
                }
                else
                {
                    pnlCompany.Visible = true;
                    bindCustomerDropDown();

                }

                lblMsg.Text = string.Empty;
                BindPOFromDashboard();
            }
            downLoadPath = ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();
        }
        //Search PO based on dashboard status & PO date search critria
        private void BindPOFromDashboard()
        {
            string statusID = "0";
            int days = 30;
            int companyID = 0;

            if (Session["postatus"] != null && Session["days"] != null)
            {
                days = Convert.ToInt32(Session["days"]);
                statusID = Convert.ToString(Session["postatus"]);
                DateTime today = DateTime.Today;
                DateTime poDate = today.AddDays(-days);
                dpStatusList.SelectedValue = statusID;
                txtFromDate.Text = poDate.ToShortDateString();
                if (Session["adm"] != null)
                {
                    if (Session["cid"] != null)
                        companyID = Convert.ToInt32(Session["cid"]);
                    if (companyID > 0)
                        dpCompany.SelectedValue = companyID.ToString();
                    Session["cid"] = null;
                }
                BindPO();
                Session["postatus"] = null;
                Session["days"] = null;
            }
        }
        private void CleanSummary()
        {
            lblPendingPOs.Text = string.Empty;
            lblShippedPOs.Text = string.Empty;
            lblProcessedPOs.Text = string.Empty;
            lblTotalPOs.Text = string.Empty;

            lnkSummary.Visible = false;
            pnlSummary.Visible = false;
        }

        private void CleanForm()
        {
            CleanSummary();   
            trUpload.Visible = false;
            btnUpload.Visible = false;
            btnDown.Visible = false;
            btnPoHeader.Visible = false;
            btnEsn_Excel.Visible = false;
            btnDownPO.Visible = false;
            btnPoDetail.Visible = false;
            btnPoDetailTrk.Visible = false;
            btnChangeStatus.Visible = false;
            lblMsg.Text = string.Empty;
            txtFmUpc.Text = string.Empty;
            this.txtFromDate.Text = string.Empty;
            txtPONum.Text = string.Empty;
            txtStoreID.Text = string.Empty;
            txtToDate.Text = string.Empty;
            this.txtMslNumber.Text = string.Empty;
            this.txtAvNumber.Text = string.Empty;
            txtEsn.Text = string.Empty;
            //dpPhoneCategory.SelectedIndex = -1;
            txtShipFrom.Text = string.Empty;
            txtShipTo.Text = string.Empty;
            trStatus.Visible = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CleanForm();
        }

        protected void btnDownPO_Click(object sender, EventArgs e)
        {
            //CleanForm();
            DownloadPOsNew();
            //DownloadPOs();

        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            DownloadPO();
        }

        protected void btnEsn_Excel_Click(object sender, EventArgs e)
        {
            DownloadESNData_Excel();
        }
        
        protected void btnUpload_Click(object sender, EventArgs e)
        {
           // CleanForm();
            if (trUpload.Visible)
            {
                trUpload.Visible = false;
            }
            else
            {
                //trUpload.Visible = true;
            }
        }


        protected void btn_UpdClick(object sender, EventArgs e)
        {
            bool esnExists = false;
            bool esnIncorrectFormat = false;
            bool uploadEsn = false;
            if (flnUpload.PostedFile.FileName.Trim().Length == 0)
            {
                lblMsg.Text = "Select file to upload";
            }
            else
            {                              
                if (flnUpload.PostedFile.ContentLength > 0)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(Server.MapPath(downLoadPath) + flnUpload.FileName);
                    if (file.Exists)
                    {
                        file.Delete();
                    }
                    flnUpload.PostedFile.SaveAs(file.FullName);

                    System.Text.StringBuilder sbEsns = new StringBuilder();
                    StringWriter stringWriter = new StringWriter();
                    using (XmlTextWriter xw = new XmlTextWriter(stringWriter))
                    {
                        try
                        {
                            xw.WriteStartElement("purchaseorder");
                            using (StreamReader sr = new StreamReader(file.FullName))
                            {
                                string line;
                                string esn = string.Empty;
                                long esnTest = 0;
                                clsInventory inventory = new clsInventory();
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] arr = line.Split(',');
                                    try
                                    {
                                        if (Convert.ToInt32(arr[0]) > 0)
                                        {
                                            esn = arr[4];
                                            if (!string.IsNullOrEmpty(esn))
                                            {
                                                if (!long.TryParse(esn, out esnTest))
                                                {
                                                    sbEsns.Append(esn + " ");
                                                    esnIncorrectFormat = true;
                                                }
                                                else
                                                {
                                                    esnExists = clsInventoryDB.ValidateEsnExists(esn);
                                                    if (esnExists)
                                                    {
                                                        sbEsns.Append(esn + " ");
                                                        esnExists = false;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        uploadEsn = true;
                                                        xw.WriteStartElement("item");
                                                        xw.WriteElementString("ponumber", arr[0]);
                                                        xw.WriteElementString("podId", arr[1]);
                                                        xw.WriteElementString("itemcode", arr[2]);
                                                        xw.WriteElementString("esn", esn);
                                                        sbEsns.Append(esn + ",");
                                                        xw.WriteEndElement();
                                                    }
                                                    esn = string.Empty;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception exception)
                                    {
                                        //lblMsg.Text = exception.Message;
                                    }
                                }
                            }
                            xw.Flush();
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = ex.Message;
                        }
                    }

                    if (uploadEsn && esnExists == false && esnIncorrectFormat == false)
                    {
                        XmlDocument xdoc = new XmlDocument();
                        xdoc.LoadXml(stringWriter.ToString());
                        UploadESNs(xdoc.OuterXml);
                        trUpload.Visible = false;
                        lblMsg.Text = "Uploaded successfully";
                        xdoc = null;
                    }
                    else if (esnIncorrectFormat)
                    {
                        lblMsg.Text = "Incorrect format " + sbEsns.ToString();
                    }
                    else if (esnExists)
                    {
                        lblMsg.Text = "Check your ESN List, some of the ESN(s) are already assigned " + sbEsns.ToString();
                    }
                }
            }
        }

        private void GenerateSummary(avii.Classes.PurchaseOrders pos)
        {
            Hashtable hshItems = new Hashtable();
            int countItems = 0;
            int pendingCount, shippedCount, processedCount, total;
            pendingCount = shippedCount = processedCount = total = 0;
            foreach (avii.Classes.BasePurchaseOrder po in pos.PurchaseOrderList)
            {
                if (po.PurchaseOrderStatus == PurchaseOrderStatus.Pending)
                {
                    pendingCount++;
                }
                else if (po.PurchaseOrderStatus == PurchaseOrderStatus.Shipped)
                {
                    shippedCount++;
                }
                else if (po.PurchaseOrderStatus == PurchaseOrderStatus.Processed)
                {
                    processedCount++;
                }

                foreach (avii.Classes.BasePurchaseOrderItem pitems in po.PurchaseOrderItems)
                {
                    if (!hshItems.ContainsKey(pitems.ItemCode))
                    {
                        hshItems.Add(pitems.ItemCode, 1);
                    }
                    else
                    {
                        countItems = Convert.ToInt32(hshItems[pitems.ItemCode].ToString());
                        countItems = countItems + 1;
                        hshItems[pitems.ItemCode] = countItems;
                    }
                }
            }

            lblTotalPOs.Text = pos.PurchaseOrderList.Count.ToString();
            lblPendingPOs.Text = pendingCount.ToString();
            lblShippedPOs.Text = shippedCount.ToString();
            lblProcessedPOs.Text = processedCount.ToString();

        }
        private void BindPO()
        {
            string timetaken;

            bool validForm = true;
            string poNum, fromDate, toDate, contactName, storeID, statusID, esn, avOrder, mslNumber, phoneCategory, itemCode, fmUPC, zoneGUID, shipFrom, shipTo;
            poNum = fromDate = toDate = shipFrom = shipTo = null;
            trUpload.Visible = false;
            lblMsg.Text = string.Empty;
            lnkSummary.Visible = false;
            int userID = 0, companyID = 0;
            try
            {
                UserInfo userInfo = Session["userInfo"] as UserInfo;
                if (userInfo != null)
                {
                    userID = userInfo.UserGUID;
                }
                else
                {
                    Response.Redirect(ConfigurationSettings.AppSettings["LogonPage"].ToString());
                }

                CleanSummary();
                fmUPC = (txtFmUpc.Text.Trim().Length > 0 ? txtFmUpc.Text.Trim() : null);
                poNum = (txtPONum.Text.Trim().Length > 0 ? txtPONum.Text.Trim() : null);
                contactName = (txtCustName.Text.Trim().Length > 0 ? txtCustName.Text.Trim() : null);
                storeID = (txtStoreID.Text.Trim().Length > 0 ? txtStoreID.Text.Trim() : null);
                zoneGUID = dpZone.SelectedValue;
                if (txtFromDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtFromDate.Text, out dt))
                        fromDate = dt.ToShortDateString();
                    else
                        throw new Exception("PO Date From does not have correct format(MM/DD/YYYY)");
                }
                if (pnlCompany.Visible == true)
                {
                    if (dpCompany.SelectedIndex > 0)
                    {
                        companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    }
                }
                if (txtToDate.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtToDate.Text, out dt))
                        toDate = dt.ToShortDateString();
                    else
                        throw new Exception("PO Date To does not have correct format(MM/DD/YYYY)");

                }

                if (txtShipFrom.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtShipFrom.Text, out dt))
                        shipFrom = dt.ToShortDateString();
                    else
                        throw new Exception("Shipping From Date To does not have correct format(MM/DD/YYYY)");

                }

                if (txtShipTo.Text.Trim().Length > 0)
                {
                    DateTime dt;
                    if (DateTime.TryParse(txtShipTo.Text, out dt))
                        shipTo = dt.ToShortDateString();
                    else
                        throw new Exception("Shipping To Date To does not have correct format(MM/DD/YYYY)");
                }

                statusID = (dpStatusList.SelectedIndex > 0 ? dpStatusList.SelectedValue : null);

                timetaken = DateTime.Now.ToLongTimeString();

                esn = null;
                if (txtEsn.Text.Trim().Length > 0)
                {
                    esn = txtEsn.Text.Trim();
                }

                avOrder = null;
                if (this.txtAvNumber.Text.Trim().Length > 0)
                {
                    avOrder = txtAvNumber.Text.Trim();
                }

                mslNumber = null;
                if (this.txtMslNumber.Text.Trim().Length > 0)
                {
                    mslNumber = this.txtMslNumber.Text.Trim();
                }
                itemCode = null;
                if (this.txtItemCode.Text.Trim().Length > 0)
                {
                    itemCode = this.txtItemCode.Text.Trim();
                }

                phoneCategory = null;
                //if (dpPhoneCategory.SelectedIndex > 0)
                //{
                //    phoneCategory = dpPhoneCategory.SelectedValue;
                //}

                if (string.IsNullOrEmpty(storeID) && string.IsNullOrEmpty(poNum) && string.IsNullOrEmpty(contactName) && string.IsNullOrEmpty(itemCode) &&
                        string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && dpStatusList.SelectedIndex == 0 && string.IsNullOrEmpty(fmUPC)
                        && dpCompany.SelectedIndex == 0 && string.IsNullOrEmpty(esn) && string.IsNullOrEmpty(avOrder) && string.IsNullOrEmpty(mslNumber) &&
                        string.IsNullOrEmpty(shipFrom) && string.IsNullOrEmpty(shipTo))
                {
                    validForm = false;
                }


                if (validForm)
                {
                    avii.Classes.PurchaseOrders pos = avii.Classes.PurchaseOrder.GerPurchaseOrders(poNum, contactName, fromDate, toDate, userID, statusID, companyID, esn, avOrder, mslNumber, phoneCategory, itemCode, storeID, fmUPC, zoneGUID, shipFrom, shipTo, 0);
                    Session["POS"] = pos;
                    if (pos != null && pos.PurchaseOrderList.Count > 0)
                    {
                        //GenerateSummary(pos);
                        lnkSummary.Visible = true;
                        btnDownPO.Visible = true;
                        //btnPoDetail.Visible = true;
                        btnPoDetailTrk.Visible = true;
                        btnEsn_Excel.Visible = true;
                        btnPoHeader.Visible = true;
                        btnDown.Visible = true;

                        if (Session["adm"] != null)
                        {
                            //this.btnUpload.Visible = true;
                            btnChangeStatus.Visible = true;
                        }
                        //if (userID != 2 && userID !=0)
                        //{
                        //    if (GridView1.Columns.Count > 7)
                        //    {
                        //        GridView1.Columns[8].Visible = false;
                        //        GridView1.Columns[9].Visible = false;
                        //    }
                        //}
                        GridView1.DataSource = pos.PurchaseOrderList;
                        GridView1.DataBind();
                        //BindGrid1(ds);
                    }
                    else
                    {
                        btnPoHeader.Visible = false;
                        btnEsn_Excel.Visible = false;
                        btnDown.Visible = false;
                        btnDownPO.Visible = false;
                        btnPoDetail.Visible = false;
                        btnPoDetailTrk.Visible = false;
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                        btnUpload.Visible = false;
                        btnChangeStatus.Visible = false;
                        lblMsg.Text = "No record exists";
                    }
                }
                else
                {
                    CleanForm();
                    lblMsg.Text = "Please select the search criteria";
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindPO();
        }


        private void BindGrid1(avii.Classes.PurchaseOrders pos)
        {
            if (pos != null && pos.PurchaseOrderList.Count > 0)
            {
                GridView1.DataSource = pos.PurchaseOrderList;
                GridView1.DataBind();
            }
        }

        //This procedure returns the Sort Direction
        private string GetSortDirection()
        {
            switch (gvSortDir)
            {
                case "ASC":
                    gvSortDir = "DESC";
                    break;

                case "DESC":
                    gvSortDir = "ASC";
                    break;
            }
            return gvSortDir;
        }

        //This procedure prepares the query to bind the child GridView
        private List<avii.Classes.BasePurchaseOrderItem> ChildDataSource(int poID, string strSort)
        {
            avii.Classes.PurchaseOrders purchaseOrders = Session["POS"] as avii.Classes.PurchaseOrders;
            if (purchaseOrders != null && purchaseOrders.PurchaseOrderList.Count > 0)
            {
                Classes.BasePurchaseOrder purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);

                return purchaseOrder.PurchaseOrderItems;
            }
            else
            {
                return null;
            }
        }

        private List<avii.Classes.BasePurchaseOrderItem> ChildDataSourcebyPODID(int podID, string strSort)
        {
            avii.Classes.PurchaseOrders purchaseOrders = (avii.Classes.PurchaseOrders)Session["POS"];
            if (purchaseOrders != null && purchaseOrders.PurchaseOrderList.Count > 0)
            {
                Classes.BasePurchaseOrder purchaseOrder = purchaseOrders.FindPurchaseOrderbyPodID(podID);

                return purchaseOrder.PurchaseOrderItems;
            }
            else
            {
                return null;
            }
        }

        #region GridView1 Event Handlers
        //This event occurs for each row
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DateTime tempDate = (DataBinder.Eval(e.Row.DataItem, "Tracking.ShipToDate") == null ? DateTime.MinValue : Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Tracking.ShipToDate")));
                if (tempDate.CompareTo(DateTime.MinValue)== 0)
                {
                    e.Row.Cells[8].Text = string.Empty;
                }

                ////Add delete confirmation message for Customer
                if (e.Row.RowIndex >= 0)
                {
                    LinkButton l = (LinkButton)e.Row.FindControl("linkDeleteCust");
                    l.Attributes.Add("onclick", "javascript:return " +
                    "confirm('Are you sure you want to delete this Customer " +
                    DataBinder.Eval(e.Row.DataItem, "PurchaseOrderID") + "')");
                }
            }
        }

        //This event occurs for any operation on the row of the grid
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Check if Add button clicked
            string gvChild;
            int iIndex = 0, poID;
            gvChild = string.Empty;

            if (e.CommandName == "sel" && grid1SelectCommand ==  false)
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
                    poID = Convert.ToInt32(gv.DataKeys[rowIndex].Value);
                    childgv.DataSource = ChildDataSource(poID, string.Empty);
                    childgv.DataBind();
                    childgv.Visible = true;
                    img.AlternateText = "-";
                    img.ImageUrl = "../images/minus.gif";
                }
                GridView1.RowCommand -= GridView1_RowCommand;
            }           
        }

        //This event occurs on click of the Update button
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridView gv = (GridView)sender;
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }            
            if (gv.EditIndex >= 0)
            {
                GridViewRow gvRow = gv.Rows[gv.EditIndex];
                if (gvRow.RowType == DataControlRowType.DataRow)
                {
                    avii.Classes.PurchaseOrders purchaseOrders = (avii.Classes.PurchaseOrders)Session["POS"];

                    int poID = 0;

                    poID = Convert.ToInt32(gv.DataKeys[gv.EditIndex].Value);
                    avii.Classes.BasePurchaseOrder purchaseOrder = new avii.Classes.BasePurchaseOrder(poID);
                    purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);
                    
                    purchaseOrder.Shipping.ContactName = ((TextBox)(gvRow.FindControl("txtContactName"))).Text;
                    purchaseOrder.Shipping.ShipToAttn = ((TextBox)(gvRow.FindControl("txtShipAttn"))).Text;
                    purchaseOrder.Tracking.ShipToBy = ((TextBox)(gvRow.FindControl("txtVia"))).Text;
                    purchaseOrder.Tracking.ShipToTrackingNumber = ((TextBox)(gvRow.FindControl("txtTrack"))).Text;
                    purchaseOrder.Tracking.PurchaseOrderNumber = purchaseOrder.PurchaseOrderNumber;
                    avii.Classes.PurchaseOrder.UpdatePurchaseOrder(purchaseOrder, userID);
                    gv.EditIndex = -1;
                    Session["POS"] = purchaseOrders;
                    GridView1.DataSource = purchaseOrders.PurchaseOrderList;
                    GridView1.DataBind();
                }
            }
           
        }

        
        //This event occurs after RowUpdating to catch any constraints while updating
        protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //Check if there is any exception while deleting
            if (e.Exception != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
                e.ExceptionHandled = true;
            }
        }

        //This event occurs on click of the Delete button
        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string poId = GridView1.DataKeys[e.RowIndex].Value.ToString();
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            try
            {

                avii.Classes.PurchaseOrder.DeletePurchaseOrder(Convert.ToInt32(poId), userID);
                DataSet ds = (DataSet)Session["PO"];
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.Rows[e.RowIndex];
                    if (dr != null)
                    {
                       dr.Delete();
                    }
                    dt = ds.Tables[1];

                    DataRow[] drs = dt.Select("POID = " + poId);
                    if (drs.Length > 0)
                    {
                        foreach (DataRow drd in drs)
                        {
                            drd.Delete();
                        }
                    }
                }
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order deleted successfully');</script>");
                GridView1.DataSource = ds;
                GridView1.DataBind();
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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = e.NewEditIndex;
            GridView1.EditIndex = gvEditIndex;

            GridView1.DataSource = ((avii.Classes.PurchaseOrders)Session["POS"]).PurchaseOrderList;
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            btnSearch_Click(sender, e);
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            
            if (Session["POS"] != null)
            {
                avii.Classes.PurchaseOrders pos = (avii.Classes.PurchaseOrders)Session["POS"];
                BindGrid1(pos);
            }
        }
        #endregion

        #region GridView2 Event Handlers
        protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvNewPageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = e.NewEditIndex;
            gvTemp.EditIndex = gvEditIndex;
            gvTemp.DataSource = ChildDataSourcebyPODID(Convert.ToInt32(gvTemp.DataKeys[gvEditIndex].Value), string.Empty);
            gvTemp.DataBind();
        }

        protected void GridView2_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = -1;
            //ds = (DataSet)Session["PO"];
            GridView1.DataSource = ((avii.Classes.PurchaseOrders)Session["POS"]).PurchaseOrderList;
                    
            GridView1.DataBind();
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string esn, msl, fmupc, mdn, msid;
            esn = msl = fmupc = mdn = msid = string.Empty;
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            try
            {
                GridView gvTemp = (GridView)sender;
                gvUniqueID = gvTemp.UniqueID;

                //Get the values stored in the text boxes
                int podID= Convert.ToInt32(gvTemp.DataKeys[e.RowIndex].Value);
                string returnMessage = string.Empty;
                if (Session["adm"] != null)
                {
                    esn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtEsn")).Text.Trim();
                    msl = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMslNumber")).Text.Trim();
                    fmupc = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtFMUPC")).Text.Trim();
                    mdn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMdn")).Text.Trim();
                    msid = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMsid")).Text.Trim();

                    avii.Classes.PurchaseOrder.PurchaseOrderUpdateDetail(podID, esn, msl, msid, mdn, null, fmupc, 1, userID, null, null, null, null, null, out returnMessage);
                }
                else
                    {
                        mdn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMdn")).Text.Trim();
                        msid = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtMsid")).Text.Trim();

                        avii.Classes.PurchaseOrder.PurchaseOrderUpdateDetail(podID, null, null, msid, mdn, null, null, 1, userID, null, null, null, null, null, out returnMessage);
                    }
                if (!string.IsNullOrEmpty(returnMessage))
                {
                    lblMsg.Text = returnMessage;
                    return;
                }
                    gvTemp.EditIndex = -1;
                    List<avii.Classes.BasePurchaseOrderItem> purchaseOrderList = ChildDataSourcebyPODID(podID, null);
                    if (purchaseOrderList != null && purchaseOrderList.Count > 0)
                    {
                        foreach (avii.Classes.BasePurchaseOrderItem pitem in purchaseOrderList)
                        {
                            if (pitem.PodID == podID)
                            {
                                pitem.ESN = esn;
                                pitem.MslNumber = msl;
                                pitem.FmUPC = fmupc;
                                pitem.MdnNumber = mdn;
                                pitem.MsID = msid;
                            }
                        }
                    }

                    gvTemp.DataSource = purchaseOrderList;
                    gvTemp.DataBind();
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Detail is updated successfully');</script>");
                    GridView1.EditIndex = -1;
                //}
            }
            catch { }
        }

        protected void GridView2_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {
            //Check if there is any exception while deleting
            if (e.Exception != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
                e.ExceptionHandled = true;
            }
        }

        protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            string poId = (string)gvTemp.DataKeys[e.RowIndex].Value.ToString();

            //Prepare the Update Command of the DataSource control
            string strSQL = "";
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
            }
            try
            {
                avii.Classes.PurchaseOrder.DeletePurchaseOrderDetail(Convert.ToInt32(poId), userID);
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Order deleted successfully');</script>");
                GridView1.DataBind();
            }
            catch { }
        }

        protected void GridView2_RowDeleted(object sender, GridViewDeletedEventArgs e)
        {
            //Check if there is any exception while deleting
            if (e.Exception != null)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + e.Exception.Message.ToString().Replace("'", "") + "');</script>");
                e.ExceptionHandled = true;
            }
        }

        protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (Session["adm"] == null && e.Row.Controls.Count > 0)
            {
                TextBox txESN = e.Row.FindControl("txtEsn") as TextBox;
                if (txESN != null)
                {
                    txESN.Visible = false;

                    TextBox txMsl = e.Row.FindControl("txtMslNumber") as TextBox;
                    if (txMsl != null)
                        txMsl.Visible = false;
                    TextBox txfmupc = e.Row.FindControl("txtFMUPC") as TextBox;
                    if (txfmupc != null)
                        txfmupc.Visible = false;
                }
            }
        }

        protected void GridView2_Sorting(object sender, GridViewSortEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvSortExpr = e.SortExpression;
            GridView1.DataBind();
        }
        #endregion

        private void SendPurchaseOrder(DataSet ds, int PoID)
        {
            //DataRow poRow = null;
            //int? poNumber;
            //DataRow[] PodRows;
            //Qualution.PurchaseOrder po = null; 
            //Qualution.AerovoiceService av = new Qualution.AerovoiceService();

            //poRow = ds.Tables[0].Select("PO_ID = " + PoID.ToString())[0];
            //if (poRow != null)
            //{
            //    po = new Qualution.PurchaseOrder();
            //    poNumber = Convert.ToInt32(poRow["PO_Num"]);
            //    if (poNumber != null)
            //    {
            //        po.poNumber = poNumber;
            //        po.poNumberSpecified = true;
            //    }

            //    po.storeId = (poRow["Store_ID"] != DBNull.Value ? poRow["Store_ID"].ToString() : null);
            //    po.trackingNumber = (poRow["Ship_Via"] != DBNull.Value ? poRow["Ship_Via"].ToString() : null);

            //    //Get PO Details
            //    PodRows = ds.Tables[1].Select("PO_ID = " + PoID.ToString());
            //    if (PodRows != null)
            //    {
            //        Qualution.OrderDetail[] poDetails = new Qualution.OrderDetail[PodRows.Length];
            //        Qualution.OrderDetail poDetail;
            //        int ictr = 0;
            //        foreach (DataRow dRow in PodRows)
            //        {
            //            poDetail = new Qualution.OrderDetail();
            //            poDetail.esn = (dRow["ESN"] != DBNull.Value ? dRow["ESN"].ToString() : null);
            //            poDetail.mslCode = (dRow["Pass_Code"] != DBNull.Value ? dRow["Pass_Code"].ToString() : null);
            //            poDetail.itemId = (dRow["Item_code"] != DBNull.Value ? dRow["Item_code"].ToString() : null);
            //            poDetails.SetValue(poDetail, ictr);
            //            ictr++;
            //        }
            //        po.orderDetail = poDetails;

            //        if (po.poNumber > 0)
            //        {
            //            av.setShipAdvice(po);
            //            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order is successfully send');</script>");
            //        }
            //        else
            //        {
            //            throw new Exception("Could not retrieve PO Number, please validate the record.");
            //        }
            //    }
            //}
        }


        private void UploadESNs(string POXml)
        {
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;
                //ViewState["userid"] = userID;
            }

            avii.Classes.PurchaseOrder.UpLoadESN(POXml, userID);

        }

        private string GetSelectedIndexs()
        {
            StringBuilder sbIndex = new StringBuilder();
            if (chkDownload.Checked)
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (((CheckBox)GridView1.Rows[row.RowIndex].Cells[1].Controls[1]).Checked)
                    {
                        sbIndex.Append("#" + (GridView1.PageSize * (GridView1.PageIndex) + row.RowIndex) + "#");
                    }
                }
            }

            return sbIndex.ToString();
        }

        private void ExportPO()
        {
            try
            {
                Response.Clear();
                Response.Charset = "";
                Response.ContentType = "application/vnd.ms-excel";

                System.IO.StringWriter stringWrite = new System.IO.StringWriter();

                System.Web.UI.HtmlTextWriter htmlWrite = new System.Web.UI.HtmlTextWriter(stringWrite);

                //DataSet ds = new DataSet();
                //clscon.Return_DS(ds, Convert.ToString(ViewState["str"]));
                //GridView dg = new GridView();
                //dg.DataSource = ds.Tables[0];

                //dg.DataBind();

                GridView1.RenderControl(htmlWrite);
                Response.Write(stringWrite.ToString());
                Response.End();
            }

            catch (Exception ex)
            {

                Response.Write(ex.Message);

            }

        }

        private void DownloadPO()
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool bSelected = true;
            
            if (bSelected == false)
            {

                ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");
                
            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".csv";
                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                if (writer == "xml")
                {
                    XmlTextWriter xw = new XmlTextWriter(path + fileName, System.Text.Encoding.ASCII);
                    try
                    {
                        xw.WriteStartElement("purchaseorder");
                        foreach (GridViewRow row in GridView1.Rows)
                        {
                            if (((CheckBox)row.FindControl("chk")).Checked)
                            {                                
                                found = true;
                                GridView gridViewDetail = ((GridView)row.FindControl("GridView2"));
                                foreach (GridViewRow row1 in gridViewDetail.Rows)
                                {
                                        xw.WriteStartElement("item");
                                        xw.WriteElementString("ponumber", ((Label)row.FindControl("lblPoNum")).Text);
                                        xw.WriteElementString("podId", gridViewDetail.DataKeys[row1.RowIndex].Value.ToString());
                                        xw.WriteElementString("itemcode", ((Label)row1.FindControl("lblItemCode")).Text);

                                        xw.WriteElementString("esn", ((Label)row1.FindControl("lblEsn")).Text);
                                        xw.WriteElementString("fmupc", ((Label)row1.FindControl("lblFMUPC")).Text);
                                        xw.WriteElementString("ShippDate", ((Label)row.FindControl("lblShippDate")).Text);
                                        xw.WriteEndElement();
                                 }
                            }
                        }
                        xw.Flush();
                        xw.Close();
                    }
                    catch (Exception ex)
                    {
                        xw.Close();
                        lblMsg.Text = ex.Message;
                    }
                }
                else if (writer == "csv")
                {
                    string posItems = stringBuilder.ToString();
                    bool addRecord = true;
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    avii.Classes.PurchaseOrders pos = (avii.Classes.PurchaseOrders)Session["POS"];
                    int lineCounter = 0;
                    string gridIndexes = GetSelectedIndexs();

                    sb.Append("PoNum,PODID,ItemCode,Esn,FmUPC,ShippDate\n");

                    lineCounter = 0; 
                    foreach (avii.Classes.BasePurchaseOrder po in pos.PurchaseOrderList)
                    {  
                        if (chkDownload.Checked)
                        {
                            if (gridIndexes.IndexOf('#' + lineCounter.ToString() + '#') < 0)
                            {
                                addRecord = false;
                            }
                            else
                                addRecord = true;
                        }
                        else
                            addRecord = true;

                        if (addRecord)
                        {
                            found = true;
                            foreach (avii.Classes.BasePurchaseOrderItem poitem in po.PurchaseOrderItems)
                            {
                                sb.Append(po.PurchaseOrderNumber + "," + poitem.PodID.ToString() + "," +
                                    poitem.ItemCode + "," + poitem.ESN + "," + poitem.FmUPC + "," + po.Tracking.ShipToDate.ToShortDateString().Replace("1/1/0001","")  +"\n");
                            
                            }
                        }
                       
                        lineCounter++;
                    }

                    if (found)
                    {
                        try
                        {
                            using (StreamWriter sw = new StreamWriter(file.FullName))
                            {
                                sw.WriteLine(sb.ToString());
                                sw.Flush();
                                sw.Close();
                            }
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = ex.Message;
                        }
                    }
                }

                if (found)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                   // Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
            }
        }
        private void DownloadPOsNew()
        {

            bool bSelected = true;

            if (bSelected == false)
            {

                ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");

            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".xls";

                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                this.EnableViewState = false;




                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                avii.Classes.PurchaseOrders pos = (avii.Classes.PurchaseOrders)Session["POS"];
                int lineCounter = 0;
                bool addRecord = true;

                string gridIndexes = GetSelectedIndexs();

                //sb.Append("H,Customer,AerovoiceOrderNumber,PurchaseOrderNumber,CustomerNumber,PurchaseOrderDate,storeid,ContactName,ShipAddress,ShipCity,ShipState,ShipZip,Shipby,TrackingNumber\n");
                //sb.Append("D,Qty,ItemCode,ESN,MdnNumber,MslNumber,PassCode,UPC,PhoneCategory,PurchaseOrderNumber,Lineno,Fm-UPC\n");

                using (StringWriter sw = new StringWriter())
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a table to contain the grid
                    Table table = new Table();

                    //  Gridline to box the cells
                    table.GridLines = System.Web.UI.WebControls.GridLines.Both;

                    string[] columns = { "H", "Customer", "AerovoiceOrderNumber", "PurchaseOrderNumber", "CustomerNumber", "PurchaseOrderDate", "storeid", "ContactName", "ShipAddress", "ShipCity", "ShipState", "ShipZip", "Shipby", "TrackingNumber" };

                    string[] dColumns = { "D", "Qty", "ItemCode", "ESN", "MdnNumber", "MslNumber", "PassCode", "UPC", "PhoneCategory", "PurchaseOrderNumber", "Lineno", "Fm - UPC", "", "" };

                    TableRow tRow = new TableRow();
                    TableCell tCell;
                    foreach (string name in columns)
                    {
                        tCell = new TableCell();
                        tCell.Text = name;
                        tRow.Cells.Add(tCell);
                    }
                    table.Rows.Add(tRow);

                    tRow = new TableRow();
                    foreach (string dName in dColumns)
                    {
                        tCell = new TableCell();
                        tCell.Text = dName;
                        tRow.Cells.Add(tCell);
                    }

                    table.Rows.Add(tRow);

                    lineCounter = 0;
                    string address1 = string.Empty;
                    string address2 = string.Empty;
                    foreach (avii.Classes.BasePurchaseOrder po in pos.PurchaseOrderList)
                    {
                        if (chkDownload.Checked)
                        {
                            if (gridIndexes.IndexOf('#' + lineCounter.ToString() + '#') < 0)
                            {
                                addRecord = false;
                            }
                            else
                                addRecord = true;
                        }
                        else
                            addRecord = true;

                        if (addRecord)
                        {
                            try
                            {
                                tRow = new TableRow();
                                tCell = new TableCell();
                                tCell.Text = "H";
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.CustomerName;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.AerovoiceSalesOrderNumber;
                                tRow.Cells.Add(tCell);


                                tCell = new TableCell();
                                tCell.Text = po.PurchaseOrderNumber;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.CustomerNumber;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.PurchaseOrderDate.ToShortDateString();
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = (string.IsNullOrEmpty(po.StoreID) ? string.Empty : po.StoreID);
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Shipping.ContactName;
                                tRow.Cells.Add(tCell);

                                address1 = (string.IsNullOrEmpty(po.Shipping.ShipToAddress) ? string.Empty : po.Shipping.ShipToAddress.Replace(',', ' '));
                                address2 = (string.IsNullOrEmpty(po.Shipping.ShipToAddress2) ? string.Empty : po.Shipping.ShipToAddress2.Replace(',', ' '));

                                tCell = new TableCell();

                                tCell.Text = address1 + " " + address2;

                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Shipping.ShipToCity;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Shipping.ShipToState;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Shipping.ShipToZip;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Tracking.ShipToBy;
                                tRow.Cells.Add(tCell);

                                tCell = new TableCell();
                                tCell.Text = po.Tracking.ShipToTrackingNumber;
                                tRow.Cells.Add(tCell);

                                table.Rows.Add(tRow);
                                // Purchase Order Details
                                foreach (avii.Classes.BasePurchaseOrderItem pItem in po.PurchaseOrderItems)
                                {


                                    tRow = new TableRow();
                                    tCell = new TableCell();
                                    tCell.Text = "D";
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.Quantity.ToString();
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.ItemCode;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.ESN.ToString();
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.MdnNumber;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.MslNumber;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.PassCode;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.UPC.ToString();
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.PhoneCategory.ToString();
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = po.PurchaseOrderNumber;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.LineNo.ToString();
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = pItem.FmUPC;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = string.Empty;
                                    tRow.Cells.Add(tCell);

                                    tCell = new TableCell();
                                    tCell.Text = string.Empty;
                                    tRow.Cells.Add(tCell);

                                    table.Rows.Add(tRow);


                                }


                            }
                            catch (Exception ex)
                            {
                                System.Threading.Thread.Sleep(5000);
                                Response.Write(ex.Message);
                            }
                        }
                        lineCounter++;
                    }

                    //  Htmlwriter into the table
                    table.RenderControl(htw);

                    //  Htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }



            }
        }
        private void DownloadPOs()
        {
            bool bSelected = true;
            
            if (bSelected == false)
            {

                ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");

            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".csv";
                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                avii.Classes.PurchaseOrders pos = (avii.Classes.PurchaseOrders)Session["POS"];
                int lineCounter = 0;
                bool addRecord = true;

                string gridIndexes = GetSelectedIndexs();

                sb.Append("H,Customer,AerovoiceOrderNumber,PurchaseOrderNumber,CustomerNumber,PurchaseOrderDate,storeid,ContactName,ShipAddress,ShipCity,ShipState,ShipZip,Shipby,TrackingNumber\n");
                sb.Append("D,Qty,ItemCode,ESN,MdnNumber,MslNumber,PassCode,UPC,PhoneCategory,PurchaseOrderNumber,Lineno,Fm-UPC\n");

                lineCounter = 0;
                foreach (avii.Classes.BasePurchaseOrder po in pos.PurchaseOrderList)
                {
                    if (chkDownload.Checked)
                    {
                        if (gridIndexes.IndexOf('#' + lineCounter.ToString() + '#') < 0)
                        {
                            addRecord = false;
                        }
                        else
                            addRecord = true;
                    }
                    else
                        addRecord = true;

                    if (addRecord)
                    {
                        found = true;
                        sb.Append("H," + po.CustomerName + "," + po.AerovoiceSalesOrderNumber + "," + po.PurchaseOrderNumber + "," + po.CustomerNumber + "," + po.PurchaseOrderDate.ToShortDateString() + "," +
                            (string.IsNullOrEmpty(po.StoreID)?string.Empty:po.StoreID) + "," + po.Shipping.ContactName + "," + 
                            (string.IsNullOrEmpty(po.Shipping.ShipToAddress)?string.Empty:po.Shipping.ShipToAddress.Replace(',', ' ')) + " " + 
                            (string.IsNullOrEmpty(po.Shipping.ShipToAddress2)?string.Empty:po.Shipping.ShipToAddress2.Replace(',', ' ')) + "," + 
                            po.Shipping.ShipToCity + "," + po.Shipping.ShipToState + "," + po.Shipping.ShipToZip + "," + 
                            po.Tracking.ShipToBy + "," + po.Tracking.ShipToTrackingNumber + "\n");

                        foreach (avii.Classes.BasePurchaseOrderItem pItem in po.PurchaseOrderItems)
                        {
                            sb.Append("D," + pItem.Quantity.ToString() + "," + pItem.ItemCode + "," + pItem.ESN + "," + pItem.MdnNumber + "," + pItem.MslNumber + "," + pItem.PassCode + "," + pItem.UPC + "," + pItem.PhoneCategory.ToString() +"," + po.PurchaseOrderNumber + ","  + pItem.LineNo.ToString() + "," + pItem.FmUPC + "\n");
                        }

                    }

                    lineCounter++;
                }

                try
                {
                    using (StreamWriter sw = new StreamWriter(file.FullName))
                    {
                        sw.WriteLine(sb.ToString());
                        sw.Flush();
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }
               
                if (found)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    // Response.AddHeader("Content-Length", file.Length.ToString());
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
            }
        }

        protected void btnChangeStatus_Click(object sender, EventArgs e)
        {
            dpStatus.SelectedIndex = 0;
            if (trStatus.Visible == false)
            {
                trStatus.Visible = true;
            }
            else
            {
                trStatus.Visible = false;
            }
        }

        protected void btn_UpdStatusClick(object sender, EventArgs e)
        {
            avii.Classes.PurchaseOrders purchaseOrders = (avii.Classes.PurchaseOrders)Session["POS"];

            int poID = 0;
           
            int userID = 0;
            UserInfo userInfo = Session["userInfo"] as UserInfo;
            if (userInfo != null)
            {
                userID = userInfo.UserGUID;

            }
            List<PurchaseOrderChangeStatus> poList = new List<PurchaseOrderChangeStatus>();
            if (trStatus.Visible == true && dpStatus.SelectedIndex > 0)
            {
                StringBuilder stringBuilder = new StringBuilder();
                bool bSelected = false;
                foreach (GridViewRow row in GridView1.Rows)
                {
                    if (((CheckBox)row.FindControl("chk")).Checked)
                    {
                        poID = Convert.ToInt32(GridView1.DataKeys[row.RowIndex].Value);
                        avii.Classes.BasePurchaseOrder purchaseOrder = new avii.Classes.BasePurchaseOrder(poID);
                        purchaseOrder = purchaseOrders.FindPurchaseOrder(poID);
                        purchaseOrder.PurchaseOrderStatusID = Convert.ToInt32(dpStatus.SelectedValue);
                        purchaseOrder.PurchaseOrderStatus = (avii.Classes.PurchaseOrderStatus)Convert.ToInt16(dpStatus.SelectedValue);
                
                        PurchaseOrderChangeStatus po = new PurchaseOrderChangeStatus();
                        Label lblPoNum = (Label)row.FindControl("lblPoNum");
                        HiddenField hdnCANo = (HiddenField)row.FindControl("hdnCANo");
                        po.CompanyAccountNumber = hdnCANo.Value;
                        po.PurchaseOrderNumber = lblPoNum.Text;
                        //po.POID = Convert.ToInt32(gvPOQuery.DataKeys[row.RowIndex].Value);
                        poList.Add(po);
                        bSelected = true;
                        //stringBuilder.Append(gvPOQuery.DataKeys[row.RowIndex].Value.ToString() + ",");
                    }
                }

                if (bSelected == false)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");
                }
                else
                {
                    int returnValue = avii.Classes.PurchaseOrder.SetPurchaseOrderChangeStatusDB(poList, dpStatus.SelectedItem.Text, userID, 0);
                    if (returnValue == 0)
                    {
                        GridView1.EditIndex = -1;
                        Session["POS"] = purchaseOrders;
                        GridView1.DataSource = purchaseOrders.PurchaseOrderList;
                        GridView1.DataBind();

                        lblMsg.Text = "Updated successfully";
                    }
                    trStatus.Visible = false;
                }
            }
        }


        protected void lnkInventorySummary_Click(object sender, EventArgs e)
        {
            if (this.pnlSummary.Visible)
            {
                lnkSummary.Text = "Show Inventory Summary";
                pnlSummary.Visible = false;
            }
            else
            {
                lnkSummary.Text = "Hide Inventory Summary";
                pnlSummary.Visible = true;
            }
        }

        protected void lnkSummary_Click(object sender, EventArgs e)
        {
            if (this.pnlSummary.Visible)
            {
                lnkSummary.Text = "Show Purchase Order Summary";
                pnlSummary.Visible = false;
            }
            else
            {
                lnkSummary.Text = "Hide Purchase Order Summary";
                pnlSummary.Visible = true;
            }
        }

        protected void btnSendESN_Click(object sender, EventArgs e)
        {
            bool bSelected = false;
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (((CheckBox)row.FindControl("chk")).Checked)
                {
                    bSelected = true;
                    string poNum = GridView1.DataKeys[row.RowIndex].Value.ToString();

                    if (!string.IsNullOrEmpty(poNum))
                    {
                        if (Session["POS"] != null)
                        {
                            avii.Classes.PurchaseOrders purchaseOrders = Session["POS"] as avii.Classes.PurchaseOrders;
                            BasePurchaseOrder po = purchaseOrders.FindPurchaseOrderbyNumber(poNum);
                            if (po != null)
                            {
                                SendEsn(po);
                            }
                            else
                            {
                                lblMsg.Text = "Could not find purchase order, please try again";
                            }
                        }
                    }

                }
            }

        }


        private void SendEsn(BasePurchaseOrder po)
        {
            //Qualution.ProblemResultResponse response = new Qualution.ProblemResultResponse();
            //Qualution.OrderDetail orderDetail = null;
            //Qualution.PurchaseOrder purchaseOrder = new Qualution.PurchaseOrder();
            //Qualution.AerovoiceService aerovoiceService = new Qualution.AerovoiceService();
            //List<Qualution.OrderDetail> orderDetails = null;

            //purchaseOrder.poNumber = Convert.ToInt32(po.PurchaseOrderNumber);
            //purchaseOrder.poNumberSpecified = true;
            //orderDetails = new List<Qualution.OrderDetail>();
            //if (po.PurchaseOrderItems != null && po.PurchaseOrderItems.Count > 0)
            //{
            //    foreach (BasePurchaseOrderItem esnItem in po.PurchaseOrderItems)
            //    {
            //        if (!string.IsNullOrEmpty(esnItem.ESN))
            //        {
            //            orderDetail = new Qualution.OrderDetail();
            //            orderDetail.esn = esnItem.ESN;

            //            orderDetail.itemId = esnItem.ItemCode;// "KYO-414-0";
            //            orderDetail.mslCode = esnItem.MslNumber;
            //            orderDetail.lineNo = esnItem.LineNo;

            //            orderDetail.lineNoSpecified = true;
            //            orderDetails.Add(orderDetail);
            //        }
            //    }

            //    purchaseOrder.orderDetail = orderDetails.ToArray();
            //    if (purchaseOrder.orderDetail != null && purchaseOrder.orderDetail.Length > 0)
            //    {
            //        response = aerovoiceService.setEsns(purchaseOrder);

            //        if (response.resultCode != null && response.resultCode == 0)
            //        {
            //            foreach (BasePurchaseOrderItem esnItem in po.PurchaseOrderItems)
            //            {
            //                avii.Classes.PurchaseOrder.SetESNServiceLogging(esnItem.PodID, purchaseOrder.poNumber.ToString(), esnItem.ESN, esnItem.MslNumber);
            //            }
            //        }
            //        string msg = String.Format("CallESNService: Purchase Order#: {0} is processed with response status of {1} ({2})", po.PurchaseOrderNumber, response.resultCode, response.problemDesc);
            //        if (response.resultCode != null && response.resultCode > 0)
            //        {
            //            lblMsg.Text = msg;
            //        }

            //        //EventLog.WriteEntry("AeroSerivce", msg, EventLogEntryType.Information);
            //    }
            //}
            //else
            //{
            //    lblMsg.Text = String.Format("ESN: Purchase Order#: {0} has no ESN Item assigned)", po.PurchaseOrderNumber);
                
            //    //EventLog.WriteEntry("AeroSerivce", String.Format("ESN: Purchase Order#: {0} has no ESN Item assigned)", pItem.PurchaseOrderNumber), EventLogEntryType.Warning);
            //}
                    

        }

        protected void btnPoDetail_Click(object sender, EventArgs e)
        {
            //DownloadPoDetails();

        }

        private void DownloadPoDetails()
        {
            bool bSelected = true;

            if (bSelected == false)
            {
                ClientScript.RegisterStartupScript(GetType(), "Purchase Order", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");
            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".csv";
                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                avii.Classes.PurchaseOrders pos = (avii.Classes.PurchaseOrders)Session["POS"];
                int lineCounter = 0;
                bool addRecord = true;

                string gridIndexes = GetSelectedIndexs();

                sb.Append("PurchaseOrderNumber,LineNo,ItemCode,MslNumber,ESN,TrackingNumber,ShippingMethod,ShippingDate\n");
                
                lineCounter = 0;
                foreach (avii.Classes.BasePurchaseOrder po in pos.PurchaseOrderList)
                {
                    if (chkDownload.Checked)
                    {
                        if (gridIndexes.IndexOf('#' + lineCounter.ToString() + '#') < 0)
                        {
                            addRecord = false;
                        }
                        else
                            addRecord = true;
                    }
                    else
                        addRecord = true;

                    if (addRecord)
                    {
                        found = true;
                        foreach (avii.Classes.BasePurchaseOrderItem pItem in po.PurchaseOrderItems)
                        {
                            sb.Append(po.PurchaseOrderNumber + ","
                                    + pItem.LineNo + ","
                                    + pItem.ItemCode + "," 
                                    + pItem.MslNumber + ", " 
                                    + pItem.ESN + "," 
                                    + po.Tracking.ShipToTrackingNumber + ","
                                    + po.Tracking.ShipToBy + ","
                                    
                                    + (po.Tracking.ShipToDate.Year == 1?string.Empty:po.Tracking.ShipToDate.ToShortDateString()) + ","
                                    +"\n");
                        }
                    }

                    lineCounter++;
                }

                try
                {
                    using (StreamWriter sw = new StreamWriter(file.FullName))
                    {
                        sw.WriteLine(sb.ToString());
                        sw.Flush();
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }

                if (found)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
            }
        }


        protected void btnPoDetailTrk_Click(object sender, EventArgs e)
        {
            DownloadPoDetailsTracking();
        }


        private void DownloadExcel()
        {

        }

        private void DownloadESNData_Excel()
        {
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=PoEsnData.xls");
            this.EnableViewState = false;
            
            avii.Classes.PurchaseOrders pos = (avii.Classes.PurchaseOrders)Session["POS"];
            
            using (StringWriter sw = new StringWriter())
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a table to contain the grid
                Table table = new Table();

                //  Gridline to box the cells
                table.GridLines = System.Web.UI.WebControls.GridLines.Both;

                string[] columns = { "PoNum", "PODID", "ItemCode", "Esn", "FmUPC" };
                
                TableRow tRow = new TableRow();
                TableCell tCell;
                foreach (string name in columns)
                {
                    tCell = new TableCell();
                    tCell.Text = name;
                    tRow.Cells.Add(tCell);
                }

                table.Rows.Add(tRow);

                foreach (avii.Classes.BasePurchaseOrder po in pos.PurchaseOrderList)
                {
                    foreach (avii.Classes.BasePurchaseOrderItem poitem in po.PurchaseOrderItems)
                    {
                        try
                        {
                            tRow = new TableRow();
                            tCell = new TableCell();
                            tCell.Text = po.PurchaseOrderNumber;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = poitem.PodID.ToString();
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = poitem.ItemCode;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = poitem.ESN.ToString();
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = poitem.FmUPC;
                            tRow.Cells.Add(tCell);

                            table.Rows.Add(tRow);
                        }
                        catch (Exception ex)
                        {
                            System.Threading.Thread.Sleep(5000);
                            Response.Write(ex.Message);
                        }
                    }
                }
 
                //  Htmlwriter into the table
                table.RenderControl(htw);

                //  Htmlwriter into the response
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }


        }

        private void DownloadPoDetailsTracking()
        {
            bool bSelected = true;

            if (bSelected == false)
            {
                ClientScript.RegisterStartupScript(GetType(), "Purchase OrderD2", "<SCRIPT LANGUAGE='javascript'>alert('Please select Purchase Order(s)');</script>");
            }
            else
            {
                string path = Server.MapPath(downLoadPath).ToString();
                string fileName = Session.SessionID + ".csv";
                bool found = false;
                System.IO.FileInfo file = null;
                file = new System.IO.FileInfo(path + fileName);
                if (file.Exists)
                {
                    file.Delete();
                }

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                avii.Classes.PurchaseOrders pos = (avii.Classes.PurchaseOrders)Session["POS"];
                int lineCounter = 0;
                bool addRecord = true;

                string gridIndexes = GetSelectedIndexs();

                sb.Append("D,Qty,ItemCode,ESN,Msl,UPC,FM-UPC,PhoneCategory,PurchaseOrderNumber,StoreID,AVSalesOrder,TrackingNumber,MDN,MSID,PassCode\n");

                lineCounter = 0;
                foreach (avii.Classes.BasePurchaseOrder po in pos.PurchaseOrderList)
                {
                    if (chkDownload.Checked)
                    {
                        if (gridIndexes.IndexOf('#' + lineCounter.ToString() + '#') < 0)
                        {
                            addRecord = false;
                        }
                        else
                            addRecord = true;
                    }
                    else
                        addRecord = true;

                    if (addRecord)
                    {
                        found = true;
                        foreach (avii.Classes.BasePurchaseOrderItem pItem in po.PurchaseOrderItems)
                        {
                            sb.Append("D," 
                                    + pItem.Quantity.ToString() + ","
                                    + pItem.ItemCode + ","
                                    + pItem.ESN + ","
                                    + pItem.MslNumber + ","
                                    + pItem.UPC + ","
                                    + pItem.FmUPC + ","
                                    + pItem.PhoneCategory + ","
                                    + po.PurchaseOrderNumber + ","
                                    + po.StoreID + ","
                                    + po.AerovoiceSalesOrderNumber + ","
                                    + po.Tracking.ShipToTrackingNumber + ","
                                    + pItem.MdnNumber + ","
                                    + pItem.MsID + ","
                                    + pItem.PassCode + ","

                                    + "\n");
                        }
                    }

                    lineCounter++;
                }

                try
                {
                    using (StreamWriter sw = new StreamWriter(file.FullName))
                    {
                        sw.WriteLine(sb.ToString());
                        sw.Flush();
                        sw.Close();
                    }
                }
                catch (Exception ex)
                {
                    lblMsg.Text = ex.Message;
                }

                if (found)
                {
                    Response.Clear();
                    Response.AddHeader("Content-Disposition", "attachment; filename=" + file.Name);
                    Response.ContentType = "application/octet-stream";
                    Response.WriteFile(file.FullName);
                    Response.End();
                }
            }
        }

        protected void btnPoHeader_Click(object sender, EventArgs e)
        {

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.ms-excel";
            Response.AddHeader("Content-Disposition", "attachment;filename=PoEsnData.xls");
            this.EnableViewState = false;

            avii.Classes.PurchaseOrders pos = (avii.Classes.PurchaseOrders)Session["POS"];

            using (StringWriter sw = new StringWriter())
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                //  Create a table to contain the grid
                Table table = new Table();

                //  Gridline to box the cells
                table.GridLines = System.Web.UI.WebControls.GridLines.Both;

                string[] columns = { "AerovoiceOrderNumber", "PurchaseOrderNumber", "CustomerAccountNumber", "PurchaseOrderDate", "storeid", "ContactName", "ShipAddress", "ShipCity", "ShipState", "ShipZip", "Shipby", "TrackingNumber" };

                TableRow tRow = new TableRow();
                TableCell tCell;
                foreach (string name in columns)
                {
                    tCell = new TableCell();
                    tCell.Text = name;
                    tRow.Cells.Add(tCell);
                }

                table.Rows.Add(tRow);

                foreach (avii.Classes.BasePurchaseOrder po in pos.PurchaseOrderList)
                {
                    foreach (avii.Classes.BasePurchaseOrderItem poitem in po.PurchaseOrderItems)
                    {
                        try
                        {
                            tRow = new TableRow();
                            tCell = new TableCell();
                            tCell.Text = po.AerovoiceSalesOrderNumber;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = po.PurchaseOrderNumber;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = po.CustomerAccountNumber;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = po.PurchaseOrderDate.ToShortDateString();
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = po.StoreID.ToString();
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = po.Shipping.ContactName;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = po.Shipping.ShipToAddress + " " + po.Shipping.ShipToAddress2; ;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = po.Shipping.ShipToCity;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = po.Shipping.ShipToState;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = po.Shipping.ShipToZip;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = po.Tracking.ShipToBy;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = po.Tracking.ShipToTrackingNumber;
                            tRow.Cells.Add(tCell);

                            table.Rows.Add(tRow);

                        }
                        catch (Exception ex)
                        {
                            System.Threading.Thread.Sleep(5000);
                            Response.Write(ex.Message);
                        }
                    }
                }

                //  Htmlwriter into the table
                table.RenderControl(htw);

                //  Htmlwriter into the response
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    
    
    }
}
