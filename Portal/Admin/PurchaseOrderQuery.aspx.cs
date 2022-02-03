using System;
using System.Text;
using System.IO;
using System.Xml;using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

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
        private string gvSortDir
        {

            get { return ViewState["SortDirection"] as string ?? "ASC"; }

            set { ViewState["SortDirection"] = value; }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            downLoadPath = ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();
        }

        private void CleanForm()
        {
            trUpload.Visible = false;
            btnUpload.Visible = false;
            btnDown.Visible = false;
            lblMsg.Text = string.Empty;
            txtFromDate.Text = string.Empty;
            txtPONum.Text = string.Empty;
            txtStoreID.Text = string.Empty;
            txtToDate.Text = string.Empty;
            GridView1.DataSource = null;
            GridView1.DataBind();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            CleanForm();
        }

        protected void btnDown_Click(object sender, EventArgs e)
        {
            //CleanForm();
            DownloadPO();
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
                trUpload.Visible = true;
            }
        }


        protected void btn_UpdClick(object sender, EventArgs e)
        {
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


                    StringWriter stringWriter = new StringWriter();
                    using (XmlTextWriter xw = new XmlTextWriter(stringWriter))
                    {
                        try
                        {
                            xw.WriteStartElement("purchaseorder");
                            using (StreamReader sr = new StreamReader(file.FullName))
                            {
                                string line;
                                while ((line = sr.ReadLine()) != null)
                                {
                                    string[] arr = line.Split(',');
                                    try
                                    {
                                        if (Convert.ToInt32(arr[0]) > 0)
                                        {
                                            xw.WriteStartElement("item");
                                            xw.WriteElementString("ponumber", arr[0]);
                                            xw.WriteElementString("podId", arr[1]);
                                            xw.WriteElementString("itemcode", arr[2]);
                                            xw.WriteElementString("esn", arr[3]);
                                            xw.WriteElementString("mslcode", arr[4]);
                                            xw.WriteEndElement();
                                        }
                                    }
                                    catch { }
                                }
                            }
                            xw.Flush();
                        }
                        catch (Exception ex)
                        {
                            lblMsg.Text = ex.Message;
                        }
                    }

                    XmlDocument xdoc = new XmlDocument();
                    xdoc.LoadXml(stringWriter.ToString());
                    UploadPO(xdoc.OuterXml);
                    trUpload.Visible = false;
                    xdoc = null;
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string timetaken;
            string poNum, storeID, fromDate, toDate;
            poNum = storeID = fromDate = toDate = null;
            trUpload.Visible = false;
            try
            {
                poNum = (txtPONum.Text.Trim().Length > 0 ? txtPONum.Text.Trim() : null);
                storeID = (txtStoreID.Text.Trim().Length > 0 ? txtStoreID.Text.Trim() : null);
                if (txtFromDate.Text.Trim().Length > 0)
                {
                    fromDate = Convert.ToDateTime(txtFromDate.Text).ToShortDateString();
                }
                if (txtToDate.Text.Trim().Length > 0)
                {
                    toDate = Convert.ToDateTime(txtToDate.Text).ToShortDateString();
                }
                timetaken = DateTime.Now.ToLongTimeString();
                ds = avii.Classes.PurchaseOrder.GerPurchaseOrders(poNum, storeID, fromDate, toDate);
                timetaken = timetaken + " - " + DateTime.Now.ToLongTimeString();
                this.lblTime.Text = timetaken;
                if (ds.Tables.Count > 0)
                {
                    btnUpload.Visible = true;
                    btnDown.Visible = true;
                    ViewState["ds"] = ds;
                    BindGrid1(ds);
                }
                else
                {
                    btnUpload.Visible = false;
                    btnDown.Visible = false;
                    lblMsg.Text = "No record exists";
                }

            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }


        private void BindGrid1(DataSet ds)
        {
            GridView1.DataSource = ds;
            GridView1.DataBind();
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
        private DataView ChildDataSource(string strCustometId, string strSort)
        {
            ds = (DataSet)ViewState["ds"];
            
            if (ds != null)
            {
                ds.Tables[1].DefaultView.RowFilter = "PO_ID = " + strCustometId;
                //gv.DataSource = ds.Tables[1].DefaultView;
                //gv.DataBind();
                return ds.Tables[1].DefaultView;
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
            GridViewRow row = e.Row;
            string strSort = string.Empty;

            // Make sure we aren't in header/footer rows
            if (row.DataItem == null)
            {
                return;
            }

            // Find Selection button and assing command like arguments
            Button btn = (Button)row.FindControl("btnAction");

            //Find Child GridView control
            GridView gv = new GridView();
            gv = (GridView)row.FindControl("GridView2");

            if (btn != null)
            {
                btn.CommandArgument = e.Row.RowIndex.ToString() + "," + ((DataRowView)e.Row.DataItem)["PO_ID"].ToString() + "," + gv.ClientID;
            }

            //Check if any additional conditions (Paging, Sorting, Editing, etc) to be applied on child GridView
            if (gv.UniqueID == gvUniqueID)
            {
                gv.PageIndex = gvNewPageIndex;
                gv.EditIndex = gvEditIndex;
                //Check if Sorting used
                if (gvSortExpr != string.Empty)
                {
                    GetSortDirection();
                    strSort = " ORDER BY " + string.Format("{0} {1}", gvSortExpr, gvSortDir);
                }
                //Expand the Child grid
                ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["PO_ID"].ToString() + "','one');</script>");
            }

            //Prepare the query for Child GridView by passing the Customer ID of the parent row
            gv.DataSource = ChildDataSource(((DataRowView)e.Row.DataItem)["PO_ID"].ToString(), strSort);
            gv.DataBind();
            
            //Add delete confirmation message for Customer
            LinkButton l = (LinkButton)e.Row.FindControl("linkDeleteCust");
            l.Attributes.Add("onclick", "javascript:return " +
            "confirm('Are you sure you want to delete this Customer " +
            DataBinder.Eval(e.Row.DataItem, "PO_ID") + "')");
            
        }

        //This event occurs for any operation on the row of the grid
        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Check if Add button clicked
            string poID, gvChild;
            int iIndex = 0;
            poID = gvChild = string.Empty;
            //if (e.CommandArgument.ToString().Split(',').Length > 1)
            //{
            //    iIndex = Convert.ToInt32(e.CommandArgument.ToString().Split(',')[0]);
            //    poID = e.CommandArgument.ToString().Split(',')[1];
            //    gvChild = e.CommandArgument.ToString().Split(',')[2];
            //}
            //else
            //{
            //    poID = e.CommandArgument.ToString();
            //}

            if (e.CommandName == "sel")
            {
                GridView gv = (GridView)sender;
                Int32 rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                GridView childgv = (GridView)gv.Rows[rowIndex].FindControl("GridView2");

                //object o = e.CommandSource;
                //GridView gvParent = (GridView)(sender);
                //GridView gv = new GridView();
                //GridViewRow gvr = gvParent.Rows[iIndex];
                //gv = (GridView)gvr.FindControl(gvChild);

                childgv.DataSource = ChildDataSource(poID, string.Empty);
                childgv.DataBind();
                childgv.Visible = true;
            }
            /*else if (e.CommandName == "select")
            {
                //Find Child GridView control
                GridView gv = new GridView();
                Panel pnl = (Panel)GridView1.FindControl("pnl" + poID);
                if (pnl != null)
                {
                    pnl.Visible = true;
                }
                //gv = (GridView)row.FindControl("GridView2");

                ////Check if any additional conditions (Paging, Sorting, Editing, etc) to be applied on child GridView
                //if (gv.UniqueID == gvUniqueID)
                //{
                //    gv.PageIndex = gvNewPageIndex;
                //    gv.EditIndex = gvEditIndex;
                //    //Check if Sorting used
                //    if (gvSortExpr != string.Empty)
                //    {
                //        GetSortDirection();
                //        //strSort = " ORDER BY " + string.Format("{0} {1}", gvSortExpr, gvSortDir);
                //    }
                //    //Expand the Child grid
                //   // ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["PO_ID"].ToString() + "','one');</script>");
                //}

                ////Prepare the query for Child GridView by passing the Customer ID of the parent row
                //gv.DataSource = ChildDataSource(((DataRowView)e.Row.DataItem)["PO_ID"].ToString());
                //gv.DataBind();
            
            }*/
        }

        //This event occurs on click of the Update button
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            //Get the values stored in the text boxes
            string contactName = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtContactName")).Text;
            string shipAttn = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtShipAttn")).Text;
            string shipVia = ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtVia")).Text;
            //string shipAddress= ((TextBox)GridView1.Rows[e.RowIndex].FindControl("txtAddress")).Text;
            string poID = GridView1.DataKeys[e.RowIndex].Value.ToString();

            try
            {
                avii.Classes.PurchaseOrder.PurchaseOrderUpdate(Convert.ToInt32(poID), contactName, shipAttn, shipVia, null , null, null, null, null);

                DataSet ds = (DataSet)ViewState["ds"];
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.Rows[e.RowIndex];
                    if (dr != null)
                    {
                        dr["Contact_Name"] = contactName;
                        dr["ShipTo_Attn"] = shipAttn;
                        dr["Ship_Via"] = shipVia;
                    }
                }
                GridView1.EditIndex = -1;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order updated successfully');</script>");
                
               //btnSearch_Click(sender, e);
            }
            
            catch { }
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
            try
            {

                avii.Classes.PurchaseOrder.DeletePurchaseOrder(Convert.ToInt32(poId));
                DataSet ds = (DataSet)ViewState["ds"];
                if (ds != null)
                {
                    DataTable dt = ds.Tables[0];
                    DataRow dr = dt.Rows[e.RowIndex];
                    if (dr != null)
                    {
                       dr.Delete();
                    }
                    dt = ds.Tables[1];

                    DataRow[] drs = dt.Select("PO_ID = " + poId);
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

            ds = (DataSet)ViewState["ds"];
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            btnSearch_Click(sender, e);
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
            ds = (DataSet)ViewState["ds"];
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void GridView2_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = -1;
            ds = (DataSet)ViewState["ds"];
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridView gvTemp = (GridView)sender;
                gvUniqueID = gvTemp.UniqueID;

                //Get the values stored in the text boxes
                string podID= gvTemp.DataKeys[e.RowIndex].Value.ToString();
                string esn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtEsn")).Text;
                string passCode = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtPass")).Text;


                avii.Classes.PurchaseOrder.PurchaseOrderUpdateDetail(Convert.ToInt32(podID), esn, null, null, passCode);

                DataSet ds = (DataSet)ViewState["ds"];
                if (ds != null)
                {
                    DataTable dt = ds.Tables[1];
                    DataRow dr = dt.Select("POD_ID = " + podID)[0];
                    if (dr != null)
                    {
                        dr["ESN"] = esn;
                        dr["Pass_Code"] = passCode;
                    }
                }
                GridView1.EditIndex = -1;
                GridView1.DataSource = ds;
                GridView1.DataBind();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order Detail is updated successfully');</script>");
                GridView1.EditIndex = -1;
                
               // btnSearch_Click(sender, e);
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

            try
            {
                avii.Classes.PurchaseOrder.DeletePurchaseOrderDetail(Convert.ToInt32(poId));
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
            //Check if this is our Blank Row being databound, if so make the row invisible
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((DataRowView)e.Row.DataItem)["PO_ID"].ToString() == String.Empty) e.Row.Visible = false;
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
            DataRow poRow = null;
            int? poNumber;
            DataRow[] PodRows;
            com.qualution.www.PurchaseOrder po = null; 
            com.qualution.www.AerovoiceService av = new avii.com.qualution.www.AerovoiceService();

            poRow = ds.Tables[0].Select("PO_ID = " + PoID.ToString())[0];
            if (poRow != null)
            {
                po = new avii.com.qualution.www.PurchaseOrder();
                poNumber = Convert.ToInt32(poRow["PO_Num"]);
                if (poNumber != null)
                {
                    po.poNumber = poNumber;
                    po.poNumberSpecified = true;
                }

                po.storeId = (poRow["Store_ID"] != DBNull.Value ? poRow["Store_ID"].ToString() : null);
                po.trackingNumber = (poRow["Ship_Via"] != DBNull.Value ? poRow["Ship_Via"].ToString() : null);

                //Get PO Details
                PodRows = ds.Tables[1].Select("PO_ID = " + PoID.ToString());
                if (PodRows != null)
                {
                    com.qualution.www.OrderDetail[] poDetails = new avii.com.qualution.www.OrderDetail[PodRows.Length];
                    com.qualution.www.OrderDetail poDetail;
                    int ictr = 0;
                    foreach (DataRow dRow in PodRows)
                    {
                        poDetail = new avii.com.qualution.www.OrderDetail();
                        poDetail.esn = (dRow["ESN"] != DBNull.Value ? dRow["ESN"].ToString() : null);
                        poDetail.mslCode = (dRow["Pass_Code"] != DBNull.Value ? dRow["Pass_Code"].ToString() : null);
                        poDetail.itemId = (dRow["Item_code"] != DBNull.Value ? dRow["Item_code"].ToString() : null);
                        poDetails.SetValue(poDetail, ictr);
                        ictr++;
                    }
                    po.orderDetail = poDetails;

                    if (po.poNumber > 0)
                    {
                        av.setShipAdvice(po);
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Purchase Order is successfully send');</script>");
                    }
                    else
                    {
                        throw new Exception("Could not retrieve PO Number, please validate the record.");
                    }
                }
            }
        }

        private void UploadPO(string POXml)
        {
            avii.Classes.PurchaseOrder.UpLoadESN(POXml);
        }

        private void DownloadPO()
        {
            bool bSelected = false;
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (((CheckBox)row.FindControl("chk")).Checked)
                {
                    bSelected = true;
                }
            }
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
                                    xw.WriteElementString("mslcode", ((Label)row1.FindControl("lblPass")).Text);
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
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    foreach (GridViewRow row in GridView1.Rows)
                    {
                        if (((CheckBox)row.FindControl("chk")).Checked)
                        {
                            found = true;
                            GridView gridViewDetail = ((GridView)row.FindControl("GridView2"));
                            int lineCounter = 0;
                            foreach (GridViewRow row1 in gridViewDetail.Rows)
                            {
                                if (lineCounter == 0)
                                {
                                    sb.Append("PoNum,PODID,ItemCode,Esn,PassCode\n");
                                }
                                sb.Append(((Label)row.FindControl("lblPoNum")).Text + "," +
                                          gridViewDetail.DataKeys[row1.RowIndex].Value.ToString() + "," +
                                          ((Label)row1.FindControl("lblItemCode")).Text + "," +
                                          ((Label)row1.FindControl("lblEsn")).Text + "," +
                                          ((Label)row1.FindControl("lblPass")).Text
                                       +"\n" );
                                lineCounter++;
                            }
                        }
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


        /*
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            string strSort = string.Empty;

            // Make sure we aren't in header/footer rows
            if (row.DataItem == null)
            {
                return;
            }

            //Find Child GridView control
            GridView gv = new GridView();
            gv = (GridView)row.FindControl("GridView2");
            if (ds != null && gv != null)
            {
                ds.Tables[1].DefaultView.RowFilter = "PO_ID = " + ((DataRowView)e.Row.DataItem)["PO_ID"].ToString();
                gv.DataSource = ds.Tables[1].DefaultView;
                gv.DataBind();
            }
            ////Check if any additional conditions (Paging, Sorting, Editing, etc) to be applied on child GridView
            //if (gv.UniqueID == gvUniqueID)
            //{
            //    gv.PageIndex = gvNewPageIndex;
            //    gv.EditIndex = gvEditIndex;
            //    //Check if Sorting used
            //    if (gvSortExpr != string.Empty)
            //    {
            //        GetSortDirection();
            //        strSort = " ORDER BY " + string.Format("{0} {1}", gvSortExpr, gvSortDir);
            //    }
            //    //Expand the Child grid
            //    ClientScript.RegisterStartupScript(GetType(), "Expand", "<SCRIPT LANGUAGE='javascript'>expandcollapse('div" + ((DataRowView)e.Row.DataItem)["PO_ID"].ToString() + "','one');</script>");
            //}

            //Prepare the query for Child GridView by passing the Customer ID of the parent row
            //gv.DataSource = ChildDataSource(((DataRowView)e.Row.DataItem)["PO_ID"].ToString(), strSort);
            //gv.DataBind();

            //Add delete confirmation message for Customer
            //LinkButton l = (LinkButton)e.Row.FindControl("linkDeleteCust");
            //l.Attributes.Add("onclick", "javascript:return " +
            //"confirm('Are you sure you want to delete this Customer " +
            //DataBinder.Eval(e.Row.DataItem, "CustomerID") + "')");

        }

        protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
        {

                    gdOrders.DataBind();
        }

        protected void GridView2_RowEditing(object sender, GridViewEditEventArgs e)
        {

            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = e.NewEditIndex;
            //gdOrders.DataSource = (DataSet)ViewState["ds"];
            gdOrders.DataBind();
            
        }

        protected void GridView2_CancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView gvTemp = (GridView)sender;
            gvUniqueID = gvTemp.UniqueID;
            gvEditIndex = -1;
            gdOrders.DataBind();
        }

        protected void GridView2_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            try
            {
                GridView gvTemp = (GridView)sender;
                gvUniqueID = gvTemp.UniqueID;

                //Get the values stored in the text boxes
                string orderID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblOrderID")).Text;
                string qty = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtQty")).Text;
                string cost = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtCost")).Text;
                string upc = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtUpc")).Text;
                string esn = ((TextBox)gvTemp.Rows[e.RowIndex].FindControl("txtEsn")).Text;

                ////Prepare the Update Command of the DataSource control
                //AccessDataSource dsTemp = new AccessDataSource();
                //dsTemp.DataFile = "App_Data/Northwind.mdb";
                //string strSQL = "";
                //strSQL = "UPDATE Orders set Freight = " + float.Parse(strFreight) + "" +
                //         ",ShipName = '" + strShipperName + "'" +
                //         ",ShipAddress = '" + strShipAdress + "'" +
                //         " WHERE OrderID = " + strOrderID;
                //dsTemp.UpdateCommand = strSQL;
                //dsTemp.Update();
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Order updated successfully');</script>");

                ////Reset Edit Index
                gvEditIndex = -1;

                gdOrders.DataBind();
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
            gvUniqueID = gvTemp.UniqueID;

            //Get the value        
            string strOrderID = ((Label)gvTemp.Rows[e.RowIndex].FindControl("lblOrderID")).Text;

            //Prepare the Update Command of the DataSource control
            string strSQL = "";

            try
            {
                //strSQL = "DELETE from Orders WHERE OrderID = " + strOrderID;
                //AccessDataSource dsTemp = new AccessDataSource();
                //dsTemp.DataFile = "App_Data/Northwind.mdb";
                //dsTemp.DeleteCommand = strSQL;
                //dsTemp.Delete();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Order deleted successfully');</script>");
                gdOrders.DataBind();
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
        */


    }
}
