using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii
{
    public partial class frmPoAsn : System.Web.UI.Page
    {
        private static readonly string SIRO_DATASOURCE = "QUALUTION-ONDS1-SIRO";
        private static readonly string IWIRELESS_DATASOURCE = "QUALUTION-ONDS1";
        private static readonly int SIRO_GUID = 42;
        private static readonly int IWIRELESS_GUID = 2;


        protected void Page_Load(object sender, EventArgs e)
        {
            txtPO.Attributes.Add("onkeydown", "if(event.which || event.keyCode){if ((event.which == 13) || (event.keyCode == 13)) {document.getElementById('" + this.btnSearch.ClientID + "').click();return false;}} else {return true}; ");

            if (Session["adm"] == null)
            {
                Response.Redirect("./logon.aspx?usr=1");
            }

            if (!IsPostBack)
            {
                bindCustomerDropDown();
            }
        }


        protected void btnSendEsn_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            int companyID = 0;
            try
            {
                avii.Classes.PurchaseOrders pos = Session["PO_Temp"] as avii.Classes.PurchaseOrders;
                if (pos != null && pos.PurchaseOrderList != null && pos.PurchaseOrderList.Count > 0)
                {
                    foreach (avii.Classes.BasePurchaseOrder po in pos.PurchaseOrderList)
                    {
                        Qualution.ProblemResultResponse response = new Qualution.ProblemResultResponse();
                        Qualution.OrderDetail orderDetail = null;
                        Qualution.PurchaseOrder purchaseOrder = new Qualution.PurchaseOrder();
                        Qualution.AerovoiceService aerovoiceService = new Qualution.AerovoiceService();
                        List<Qualution.OrderDetail> orderDetails = null;
                        if (response == null)
                        {
                            lblMsg.Text = "Can not create Qualution object";
                        }
                        else
                        {
                            purchaseOrder.poNumber = Convert.ToInt32(po.PurchaseOrderNumber);
                            purchaseOrder.poNumberSpecified = true;
                            if (po.CompanyID == SIRO_GUID)
                            {
                                purchaseOrder.dataSource = SIRO_DATASOURCE;
                            }
                            else if (po.CompanyID == IWIRELESS_GUID)
                            {
                                purchaseOrder.dataSource = IWIRELESS_DATASOURCE;
                            }

                            orderDetails = new List<Qualution.OrderDetail>();
                            if (po.PurchaseOrderItems != null && po.PurchaseOrderItems.Count > 0)
                            {
                                foreach (BasePurchaseOrderItem esnItem in po.PurchaseOrderItems)
                                {
                                    if (!string.IsNullOrEmpty(esnItem.ESN))
                                    {
                                        orderDetail = new Qualution.OrderDetail();
                                        orderDetail.esn = esnItem.ESN;

                                        if (!string.IsNullOrEmpty(esnItem.FmUPC))
                                        {
                                            orderDetail.pointOfSaleUpc = esnItem.FmUPC;
                                        }
                                        orderDetail.itemId = esnItem.ItemCode;// "KYO-414-0";
                                        orderDetail.mslCode = esnItem.MslNumber;
                                        orderDetail.lineNo = esnItem.LineNo;

                                        orderDetail.lineNoSpecified = true;
                                        orderDetails.Add(orderDetail);
                                    }
                                }

                                purchaseOrder.orderDetail = orderDetails.ToArray();

                                if (purchaseOrder.orderDetail != null && purchaseOrder.orderDetail.Length > 0)
                                {
                                    try
                                    {
                                        response = aerovoiceService.setEsns(purchaseOrder);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }

                                    if (response.resultCode != null && response.resultCode >= 0)
                                    {
                                        foreach (BasePurchaseOrderItem esnItem in po.PurchaseOrderItems)
                                        {
                                            avii.Classes.PurchaseOrder.SetESNServiceLogging(esnItem.PodID, purchaseOrder.poNumber.ToString(),
                                                                esnItem.ESN, esnItem.MslNumber, response.resultCode.ToString(), response.problemDesc, string.Empty);
                                        }

                                        msg = String.Format("CallESNService: Purchase Order#: {0} is processed with response status of {1} ({2})", purchaseOrder.poNumber, response.resultCode, response.problemDesc);

                                        lblMsg.Text = msg;
                                    }
                                    else
                                    {
                                        lblMsg.Text = "iWireless server is not running";
                                    }

                                }
                                else
                                {
                                    lblMsg.Text = "iWireless server is not running";

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }

        }

        protected void btnSendAsn_Click(object sender, EventArgs e)
        {
            string msg = string.Empty;
            int companyID = 0;
            try
            {
                avii.Classes.PurchaseOrders pos = Session["PO_Temp"] as avii.Classes.PurchaseOrders;
                if (pos != null && pos.PurchaseOrderList != null && pos.PurchaseOrderList.Count > 0)
                {
                    foreach (avii.Classes.BasePurchaseOrder po in pos.PurchaseOrderList)
                    {
                        Qualution.ProblemResultResponse response = new Qualution.ProblemResultResponse();
                        Qualution.OrderDetail orderDetail = null;
                        Qualution.PurchaseOrder purchaseOrder = new Qualution.PurchaseOrder();
                        Qualution.AerovoiceService aerovoiceService = new Qualution.AerovoiceService();
                        List<Qualution.OrderDetail> orderDetails = null;
                        if (response == null)
                        {
                            lblMsg.Text = "Can not create Qualution object";
                        }
                        else
                        {
                            purchaseOrder.poNumber = Convert.ToInt32(po.PurchaseOrderNumber);
                            purchaseOrder.trackingNumber = po.Tracking.ShipToTrackingNumber;
                            purchaseOrder.poNumberSpecified = true;
                            if (po.CompanyID == SIRO_GUID)
                            {
                                purchaseOrder.dataSource = SIRO_DATASOURCE;
                            }
                            else if (po.CompanyID == IWIRELESS_GUID)
                            {
                                purchaseOrder.dataSource = IWIRELESS_DATASOURCE;
                            }

                            orderDetails = new List<Qualution.OrderDetail>();
                            if (po.PurchaseOrderItems != null && po.PurchaseOrderItems.Count > 0)
                            {
                                foreach (BasePurchaseOrderItem esnItem in po.PurchaseOrderItems)
                                {
                                    if (!string.IsNullOrEmpty(esnItem.ESN))
                                    {
                                        orderDetail = new Qualution.OrderDetail();
                                        orderDetail.esn = esnItem.ESN;

                                        if (!string.IsNullOrEmpty(esnItem.FmUPC))
                                        {
                                            orderDetail.pointOfSaleUpc = esnItem.FmUPC;
                                        }
                                        orderDetail.itemId = esnItem.ItemCode;// "KYO-414-0";
                                        orderDetail.mslCode = esnItem.MslNumber;
                                        orderDetail.lineNo = esnItem.LineNo;
                                        orderDetail.trackingNo = po.Tracking.ShipToTrackingNumber;
                                        orderDetail.lineNoSpecified = true;
                                        orderDetails.Add(orderDetail);
                                    }
                                }

                                purchaseOrder.orderDetail = orderDetails.ToArray();

                                if (purchaseOrder.orderDetail != null && purchaseOrder.orderDetail.Length > 0)
                                {
                                    try
                                    {
                                        response = aerovoiceService.setShipAdvice(purchaseOrder);
                                    }
                                    catch (Exception ex)
                                    {
                                        throw ex;
                                    }

                                    if (response.resultCode != null && response.resultCode >= 0)
                                    {
                                        avii.Classes.PurchaseOrder.SetASNServiceLogging((int)po.PurchaseOrderID, po.PurchaseOrderNumber,po.Tracking.ShipToTrackingNumber);
                                                                                                   
                                        msg = String.Format("CallASNService: Purchase Order#: {0} is processed with response status of {1} ({2})", purchaseOrder.poNumber, response.resultCode, response.problemDesc);

                                        lblMsg.Text = msg;
                                    }
                                    else
                                    {
                                        lblMsg.Text = "iWireless server is not running";
                                    }

                                }
                                else
                                {
                                    lblMsg.Text = "iWireless server is not running";

                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ClearFormSearch();
            if (txtPO.Text.Trim().Length > 0 && dpCompany.SelectedIndex > 0 )
            {
                int companyID = 0;
                int.TryParse(dpCompany.SelectedValue.ToString(), out companyID);
                avii.Classes.PurchaseOrders pos = avii.Classes.PurchaseOrder.GerPurchaseOrders(txtPO.Text.Trim(), companyID);
                if (pos != null && pos.PurchaseOrderList != null && pos.PurchaseOrderList.Count > 0)
                {
                    Session["PO_Temp"] = pos;
                    grdPO.DataSource = pos.PurchaseOrderList;
                    grdPO.DataBind();
                    pnlPO.Visible = true;
                    if (pos.PurchaseOrderList.Count == 1)
                    {
                        lblAsnStatus.Text = pos.PurchaseOrderList[0].SentASN;
                        lblEsnStatus.Text = pos.PurchaseOrderList[0].SentESN;
                        lblStatus.Text = pos.PurchaseOrderList[0].PurchaseOrderStatus.ToString();

                        btnSendASN.Visible = true;
                        btnSendESN.Visible = true;

                    }

                    GetEsn_AsnData(txtPO.Text.Trim(), companyID);

                }
                else
                {
                    pnlPO.Visible = false;
                    lblMsg.Text = "No record exists for selected criteria";
                }
            }
            else
            {
                pnlPO.Visible = false;
                lblMsg.Text = "Purchase Order# and Customer selection should not be blank";
            }

        }

        private void GetPOData(string purchaseOrderNum, int companyID)
        {
            avii.Classes.PurchaseOrders pos = avii.Classes.PurchaseOrder.GerPurchaseOrders(purchaseOrderNum, companyID);
            if (pos != null && pos.PurchaseOrderList != null && pos.PurchaseOrderList.Count > 0)
            {
                Session["PO_Temp"] = pos;
                grdPO.DataSource = pos.PurchaseOrderList;
                grdPO.DataBind();

                if (pos.PurchaseOrderList.Count == 1)
                {
                    lblAsnStatus.Text = pos.PurchaseOrderList[0].SentASN;
                    lblEsnStatus.Text = pos.PurchaseOrderList[0].SentESN;
                    lblStatus.Text = pos.PurchaseOrderList[0].PurchaseOrderStatus.ToString();
                }

            }
        }

        protected void bindCustomerDropDown()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //Check if Add button clicked
            string gvChild;
            int iIndex = 0, poID;
            gvChild = string.Empty;

            if (e.CommandName == "sel")
            {
                GridView gv = (GridView)sender;
                Int32 rowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                ImageButton img = (ImageButton)gv.Rows[rowIndex].Cells[0].Controls[0];
                GridView childgv = (GridView)gv.Rows[rowIndex].FindControl("GridView2");

                if (Session["adm"] == null)
                {
                    foreach (DataControlField dc in childgv.Columns)
                    {
                        if (dc.HeaderText.Equals("Edit") || dc.HeaderText.Equals("Delete"))
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
            }

        }
                    //This procedure prepares the query to bind the child GridView
        private List<avii.Classes.BasePurchaseOrderItem> ChildDataSource(int poID, string strSort)
        {
            avii.Classes.PurchaseOrders purchaseOrders = (avii.Classes.PurchaseOrders)Session["PO_Temp"];
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
        private void ClearFormSearch()
        {
            lblMsg.Text = string.Empty;
            btnSendASN.Visible = false;
            btnSendESN.Visible = false;
            lblStatus.Text = string.Empty;
            lblAsnStatus.Text = string.Empty;
            lblEsnStatus.Text = string.Empty;
        }

        private void ClearForm()
        {
            pnlPO.Visible = false;
            btnSendASN.Visible = false;
            btnSendESN.Visible = false;
            lblMsg.Text = string.Empty;


            lblStatus.Text = string.Empty;
            lblEsnStatus.Text = string.Empty;
            lblAsnStatus.Text = string.Empty;

            txtPO.Text = string.Empty;
            dpCompany.SelectedIndex = 0;

            grdPO.DataSource = null;
            grdPO.DataBind();

            grdESN.DataSource = null;
            grdESN.DataBind();

            grdErrorLog.DataSource = null;
            grdErrorLog.DataBind();

            grdASN.DataSource = null;
            grdASN.DataBind();

            tblESN.Visible = false;
            tblAsn.Visible = false;
            tblErrorLog.Visible = false;

        }

        private bool ValidateSucessESN_ASN(DataTable dataTable)
        {
            bool returnValue = false;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                DataRow[] dRows = dataTable.Select("ReturnCode = 0");
                if (dRows != null && dRows.Length > 0)
                {
                    returnValue = true;
                }
            }

            return returnValue;
        }

        private void GetEsn_AsnData(string purchaseOrderNum, int companyID)
        {
            bool esnSent, asnSent;
            esnSent = asnSent = false;
            if (!string.IsNullOrEmpty(purchaseOrderNum))
            {
                DataSet ds = avii.Classes.PurchaseOrder.GetPurchaseOrder_ESN_ASN(purchaseOrderNum, companyID);

                if (ds.Tables != null && ds.Tables.Count >= 3)
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        tblESN.Visible = true;
                        grdESN.DataSource = ds.Tables[1];
                        grdESN.DataBind();

                        esnSent = ValidateSucessESN_ASN(ds.Tables[1]);
                    }
                    else
                    {
                        tblESN.Visible = false;
                    }

                    if (ds.Tables[2].Rows.Count > 0)
                    {
                        tblAsn.Visible = true;
                        grdASN.DataSource = ds.Tables[2];
                        grdASN.DataBind();

                        asnSent = ValidateSucessESN_ASN(ds.Tables[2]);
                    }
                    else
                    {
                        tblAsn.Visible = false;
                    }

                    if (esnSent)
                    {
                        btnSendESN.Visible = false;
                    }

                    if (asnSent)
                    {
                        btnSendASN.Visible = false;
                    }

                    if (ds.Tables.Count > 3)
                    {
                        tblErrorLog.Visible = true;
                        grdErrorLog.DataSource = ds.Tables[3];
                        grdErrorLog.DataBind();
                    }
                    else
                    {
                        tblErrorLog.Visible = false;
                    }
                }
            }
        }

        private void PopulateForm(DataTable dataTable)
        {
            string poNumber, poStatus, asnStatus, esnStatus;
            poNumber = poStatus = asnStatus = esnStatus = string.Empty;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                poNumber = (string)clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false);
                poStatus = (string)clsGeneral.getColumnData(dataRow, "PO_Status", string.Empty, false);
                poStatus = (string)clsGeneral.getColumnData(dataRow, "PO_Status", string.Empty, false);
                poStatus = (string)clsGeneral.getColumnData(dataRow, "PO_Status", string.Empty, false);

            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearForm();
        }
    }
}
