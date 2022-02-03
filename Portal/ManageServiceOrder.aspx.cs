using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
//using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
using SV.Framework.Common.LabelGenerator;
using System.IO;
using System.Collections;

namespace avii
{
    public partial class ManageServiceOrder : System.Web.UI.Page
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
                int soid = 0;
                BindCustomer();
                string soRequestInfo = string.Empty;
                
                if ((Request["soid"] != null && Request["soid"] != "") || Session["soid"] != null)
                {
                    if (Session["soid"] != null)
                        soid = Convert.ToInt32(Session["soid"]);
                    else
                        soid = Convert.ToInt32(Request["soid"]);
                    ViewState["soid"] = soid;
                    GetServiceOrderDetail(soid);
                    btnSubmit.Visible = false;
                    btnPrint.Visible = true;
                    btnValidate.Visible = false;
                    //btValidate.Visible = false;
                    btnCancel.Visible = true;
                    pnlSearch.Visible = true;
                    btnAdd.Visible = false;
                    btnSearch.Visible = false;
                    trHr.Visible = false;

                }
                else
                {
                    if (Session["soRequestInfo"] != null)
                    {
                        int serviceOerderNo1 = ServiceOrderOperation.GenerateServiceOrder();
                        txtSONumber.Text = serviceOerderNo1.ToString();

                        soRequestInfo = Session["soRequestInfo"] as string;
                        string[] arr = soRequestInfo.Split(',');
                        if (arr.Length > 0)
                        {
                            txtCustOrderNo.Text = arr[0];

                            if (arr.Length > 1)
                                dpCompany.SelectedValue = arr[1];

                            GetServiceRequestNumber();
                            txtCustOrderNo.Enabled = false;
                            dpCompany.Enabled = false;

                            btnSearch.Visible = false;
                            Session["soRequestInfo"] = null;

                        }
                    }
                    else
                    {
                        btnSubmit.Visible = false;
                        //btnPrint.Visible = false;
                        btnValidate.Visible = false;
                        //btValidate.Visible = false;
                        btnCancel.Visible = false;
                        pnlSearch.Visible = false;
                        btnAdd.Visible = false;

                        int serviceOerderNo = ServiceOrderOperation.GenerateServiceOrder();
                        txtSONumber.Text = serviceOerderNo.ToString();
                        txtOrderDate.Text = DateTime.Now.ToShortDateString();
                        txtSONumber.Enabled = false;
                        txtOrderDate.Enabled = false;
                    }
                }

            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }

        protected void GetServiceOrderDetail(int soid)
        {
            ViewState["soid"] = soid;
            bool IsMappedSKU = false;
            ServiceOrders serviceOrder = ServiceOrderOperation.GetServiceOrderDetail(soid);
            if(serviceOrder != null)
            {
                Session["serviceOrder"] = serviceOrder;
                btnDownload.Visible = true;
                dpCompany.Enabled = false;
                ddlKitted.Enabled = false;
                rdUpload.Enabled = false;
                rdScan.Enabled = false;

                dpCompany.SelectedValue = serviceOrder.CompanyId.ToString();
                BindCompanySKU(serviceOrder.CompanyId);
                txtCustOrderNo.Text = serviceOrder.CustomerOrderNumber;
                txtOrderDate.Text = serviceOrder.OrderDate;
                txtOrderQty.Text = serviceOrder.Quantity.ToString();
                txtSONumber.Text = serviceOrder.ServiceOrderNumber;
                ddlKitted.SelectedValue = serviceOrder.KittedSKUId.ToString();

                BindRawSKUs(serviceOrder.CompanyId, serviceOrder.KittedSKUId);
                List<ServiceOrderDetail> esnList = serviceOrder.SODetail;
                var esnStartlist = (from item in esnList where item.Id.Equals(1) select item).ToList();

                int ItemCompanyGUID = 0;
                foreach (RepeaterItem item in rptESN.Items)
                {
                    TextBox txtICCID = item.FindControl("txtICCID") as TextBox;
                    
                    HiddenField hdSKUId = item.FindControl("hdSKUId") as HiddenField;
                    int.TryParse(hdSKUId.Value, out ItemCompanyGUID);
                    if (esnStartlist != null && esnStartlist.Count > 0)
                    {
                        var esnStarts = (from items in esnStartlist where items.ItemCompanyGUID.Equals(ItemCompanyGUID) select items).ToList();
                        if (esnStarts != null && esnStarts.Count > 0 && esnStarts[0].ItemCompanyGUID > 0)
                        {
                            txtICCID.Text = esnStarts[0].ESN;
                        }
                    }
                }
                //var po = (from item in pos.PurchaseOrderList where item.PurchaseOrderID.Equals(poid) select item).ToList();
                Session["esnList"] = serviceOrder.SODetail;

                gvSOEsn.DataSource = serviceOrder.SODetail;
                gvSOEsn.DataBind();

                foreach (GridViewRow item in gvSOEsn.Rows)
                {

                    TextBox txtESN = item.FindControl("txtESN") as TextBox;
                  //  TextBox txtMSL = item.FindControl("txtMSL") as TextBox;
                    TextBox txtICCID = item.FindControl("txtICCID") as TextBox;
                    if (!string.IsNullOrEmpty(txtICCID.Text.Trim()))
                        IsMappedSKU = true;
                    //CheckBox chkPrint = item.FindControl("chkPrint") as CheckBox;
                   // chkPrint.Enabled = false;
                    txtESN.Enabled = false;
                 //   txtMSL.Enabled = false;
                    txtICCID.Enabled = false;
                }
                if (!IsMappedSKU)
                {
                    gvSOEsn.Columns[3].Visible = false;
                    gvSOEsn.Columns[4].Visible = false;
                }

            }

        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadToExcel();
            //DownloadToCSV();
        }
        private void DownloadToExcel()
        {
            //List<ServiceOrderCSV> soList = new List<ServiceOrderCSV>();
            //ServiceOrderCSV serviceOrderCSV = null;
            int itr = 1;
            bool isSim = false;
            string ESN = string.Empty, ICCID = string.Empty;
            if (Session["serviceOrder"] != null)
            {
                ServiceOrders serviceOrders = Session["serviceOrder"] as ServiceOrders;
                using (StringWriter sw = new StringWriter())
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a table to contain the grid
                    Table table = new Table();

                    //  Gridline to box the cells
                    table.GridLines = System.Web.UI.WebControls.GridLines.Both;

                    string[] columns = { "ServiceOrderNumber", "IMEI", "ICCID", "CustomerOrderNumber", "KittedSKU", "Customer", "Date", "Qty", "QtyPerOrder" };

                    TableRow tRow = new TableRow();
                    TableCell tCell;
                    foreach (string name in columns)
                    {
                        tCell = new TableCell();
                        tCell.Text = name;
                        tRow.Cells.Add(tCell);
                    }

                    table.Rows.Add(tRow);

                    //foreach (avii.Classes.BasePurchaseOrder po in serviceOrders.SODetail)
                    {
                        foreach (ServiceOrderDetail sodetail in serviceOrders.SODetail)
                        {
                            isSim = sodetail.IsSim;
                            if (sodetail.IsSim)
                            {
                                ESN = "";
                                ICCID = "#" + sodetail.ESN;
                                //ICCID = string.Format("{0}",ICCID);
                            }
                            else
                            {
                                ESN = "#" + sodetail.ESN;
                                ICCID = "#" + sodetail.ICCID;
                            }
                            
                            tRow = new TableRow();
                            tCell = new TableCell();
                            //tCell.
                            tCell.Text = serviceOrders.ServiceOrderNumber;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = ESN;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = ICCID;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = serviceOrders.CustomerOrderNumber;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = serviceOrders.SKU;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = serviceOrders.CompanyName;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = serviceOrders.OrderDate;
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            tCell.Text = sodetail.Qty.ToString();
                            tRow.Cells.Add(tCell);

                            tCell = new TableCell();
                            if (itr == 1)
                                tCell.Text = serviceOrders.Quantity.ToString();
                            else
                                tCell.Text = "";
                            tRow.Cells.Add(tCell);

                            table.Rows.Add(tRow);
                            itr = itr + 1;
                        }
                    }

                    //  Htmlwriter into the table
                    table.RenderControl(htw);


                    //if (soList != null && soList.Count > 0)
                    {

                        //string string2CSV = soList.ToCSV();

                        Response.Clear();
                        Response.Buffer = true;
                        Response.AddHeader("content-disposition", "attachment;filename=ServiceOrderReport.xls");
                        Response.Charset = "";
                        //Response.ContentType = "application/text";
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.Output.Write(sw);
                        Response.Flush();
                        Response.End();
                    }
                }
            }

        }

        private void DownloadToCSV()
        {
            List<ServiceOrderCSV> soList = new List<ServiceOrderCSV>();
            ServiceOrderCSV serviceOrderCSV = null;
            int itr = 1;
            bool isSim = false;
            string ESN = string.Empty, ICCID = string.Empty;
            if (Session["serviceOrder"] != null)
            {
                ServiceOrders serviceOrders = Session["serviceOrder"] as ServiceOrders;
                foreach (ServiceOrderDetail sodetail in serviceOrders.SODetail)
                {
                    isSim = sodetail.IsSim;
                    if (sodetail.IsSim)
                    {
                        ESN = "";
                        ICCID = sodetail.ESN;
                    }
                    else
                    {
                        ESN = sodetail.ESN;
                        ICCID = sodetail.ICCID;

                    }

                    serviceOrderCSV = new ServiceOrderCSV();
                    serviceOrderCSV.IMEI = ESN;
                    serviceOrderCSV.ICCID = ICCID;
                    serviceOrderCSV.Date = serviceOrders.OrderDate;
                    serviceOrderCSV.ServiceOrderNumber = serviceOrders.ServiceOrderNumber;
                    serviceOrderCSV.CustomerOrderNumber = serviceOrders.CustomerOrderNumber;
                    serviceOrderCSV.KittedSKU = serviceOrders.SKU;
                    serviceOrderCSV.Customer = serviceOrders.CompanyName;
                    serviceOrderCSV.Qty = sodetail.Qty.ToString();
                    if (itr == 1)
                        serviceOrderCSV.QtyPerOrder = serviceOrders.Quantity.ToString();
                    else
                        serviceOrderCSV.QtyPerOrder = "";
                    itr = itr + 1;

                    soList.Add(serviceOrderCSV);
                }
                if (soList != null && soList.Count > 0)
                {

                    string string2CSV = soList.ToCSV();

                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename=ServiceOrderReport.csv");
                    Response.Charset = "";
                    Response.ContentType = "application/text";
                    Response.Output.Write(string2CSV);
                    Response.Flush();
                    Response.End();
                }
            }

        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            gvSOEsn.DataSource = null;
            gvSOEsn.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
           // lblConfirm.Text = string.Empty;

            btnSubmit.Visible = false;
            btnPrint.Visible = false;
            btnDownload.Visible = false;
            lblCount.Text = string.Empty;
            ClearForm();
            txtCustOrderNo.Text = string.Empty;
            ddlKitted.Items.Clear();
            string CustInfo = string.Empty;
            //  trSKU.Visible = true;
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
                BindCompanySKU(companyID);
            else
            {
                //  trSKU.Visible = true;
                ddlKitted.DataSource = null;
                ddlKitted.DataBind();

            }
        }
        protected void ddlKitted_SelectedIndexChanged(object sender, EventArgs e)
        {
            rptESN.DataSource = null;
            rptESN.DataBind();

            gvSOEsn.DataSource = null;
            gvSOEsn.DataBind();

            int companyID = 0, itemCompanyGUID = 0;
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                if (ddlKitted != null && ddlKitted.Items.Count > 0)
                {
                    if (ddlKitted.SelectedIndex > 0)
                    {
                        itemCompanyGUID = Convert.ToInt32(ddlKitted.SelectedValue);
                        BindRawSKUs(companyID, itemCompanyGUID);
                    }
                    else
                    {
                        lblMsg.Text = "SKU is required!";
                        ddlKitted.DataSource = null;
                        ddlKitted.DataBind();
                    }

                }
                else
                {
                    lblMsg.Text = "No SKU/Product are assigned to selected Customer";
                    ddlKitted.Items.Clear();
                    ddlKitted.DataSource = null;
                    ddlKitted.DataBind();

                }
            }
            else
            {
                lblMsg.Text = "Customer is required!";
                dpCompany.DataSource = null;
                dpCompany.DataBind();
            }

        }
        protected void btnValidate_Click(object sender, EventArgs e)
        {
            IncreementIndex = 1;
            string errorMessage = string.Empty;
            int ItemCompanyGUID = 0, KittedSKUId = 0, mappedItemCompanyGUID = 0;
            KittedSKUId = Convert.ToInt32(ddlKitted.SelectedValue);
            bool IsValidate = true, IsMappedSKU = false;
            ServiceOrders serviceOrder = new ServiceOrders();
            List<ServiceOrderDetail> esnList = new List<ServiceOrderDetail>();
            ServiceOrderDetail esnDetail = null;
            string esn = string.Empty, iccid = string.Empty;
            foreach (GridViewRow item in gvSOEsn.Rows)
            {
                esnDetail = new ServiceOrderDetail();
                TextBox txtESN = item.FindControl("txtESN") as TextBox;
                TextBox txtICCID = item.FindControl("txtICCID") as TextBox;
                CheckBox chkPrint = item.FindControl("chkPrint") as CheckBox;
                Label hdnSKUId = item.FindControl("hdnSKUId") as Label;
                Label hdnMappedItemCompanyGUID = item.FindControl("hdnMappedItemCompanyGUID") as Label;
                int.TryParse(hdnSKUId.Text, out ItemCompanyGUID);
                int.TryParse(hdnMappedItemCompanyGUID.Text, out mappedItemCompanyGUID);
                if (mappedItemCompanyGUID > 0)
                {
                    IncreementIndex = 2;
                    IsMappedSKU = true;
                }
                esn = txtESN.Text.Trim();
                iccid = txtICCID.Text.Trim();
                //if (!string.IsNullOrWhiteSpace(esn))
                {
                    esnDetail.ESN = esn;
                    esnDetail.IsPrint = chkPrint.Checked;
                    esnDetail.ItemCompanyGUID = ItemCompanyGUID;
                    esnDetail.ICCID = iccid;
                    esnDetail.MappedItemCompanyGUID = mappedItemCompanyGUID;
                    esnList.Add(esnDetail);
                }
                esn = string.Empty;
            }
            serviceOrder.SODetail = esnList;
            serviceOrder.ServiceOrderId = 0;
            serviceOrder.CustomerOrderNumber = txtCustOrderNo.Text.Trim();
            serviceOrder.KittedSKUId = KittedSKUId;
            //if (!string.IsNullOrEmpty(serviceOrder.CustomerOrderNumber))
            {
                esnList = ServiceOrderOperation.ValidateServiceOrder(serviceOrder, out errorMessage, out IsValidate);
                if (esnList != null && esnList.Count > 0)
                {
                    gvSOEsn.DataSource = esnList;
                    gvSOEsn.DataBind();
                    gvSOEsn.Columns[5].Visible = false;
                    if (!IsMappedSKU)
                    {
                        gvSOEsn.Columns[3].Visible = false;
                        gvSOEsn.Columns[4].Visible = false;
                    }
                    Session["esnList"] = esnList;
                    //pnlSearch.Visible = true;
                    lblCount.Text = "Total count: " + esnList.Count;
                    //txtOrderQty.Text = esnList.Count.ToString();

                    if (IsValidate && string.IsNullOrEmpty(errorMessage))
                    {
                        btnAdd.Visible = false;
                        btnValidate.Visible = false;
                        //btValidate.Visible = false;
                        foreach (GridViewRow item in gvSOEsn.Rows)
                        {
                            
                            TextBox txtESN = item.FindControl("txtESN") as TextBox;
                            txtESN.Enabled = false;
                            TextBox txtICCID = item.FindControl("txtICCID") as TextBox;
                            txtICCID.Enabled = false;
                        }
                        txtCustOrderNo.Enabled = false;
                        txtOrderQty.Enabled = false;
                        
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                    }
                    else
                    {
                        if (!IsValidate)
                        {
                            //btnValidate.Visible = false;
                            btnValidate.Visible = true;
                        }
                        if (!string.IsNullOrEmpty(errorMessage))
                            lblMsg.Text = errorMessage + " Customer order number already exists!";
                    }
                }
                else
                {
                    gvSOEsn.DataSource = null;
                    gvSOEsn.DataBind();
                }
            }
            //else
            //    lblMsg.Text = "Customer order number is required!";
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            int userId = 0;
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
            {
                
                userId = userInfo.UserGUID;
                
            }
            bool IsMappedSKU = false;
            int ItemCompanyGUID = 0, mappedItemCompanyGUID = 0;
            string errorMessage = string.Empty;
            ServiceOrders serviceOrder = new ServiceOrders();
            //List<ServiceOrderDetail> esnList = Session["esnList"] as List<ServiceOrderDetail>;
            List<ServiceOrderDetail> esnList = new List<ServiceOrderDetail>();
            ServiceOrderDetail esnDetail = null;
            foreach (GridViewRow item in gvSOEsn.Rows)
            {
                esnDetail = new ServiceOrderDetail();
                TextBox txtESN = item.FindControl("txtESN") as TextBox;
                CheckBox chkPrint = item.FindControl("chkPrint") as CheckBox;
                Label hdnSKUId = item.FindControl("hdnSKUId") as Label;
                Label hdnMappedItemCompanyGUID = item.FindControl("hdnMappedItemCompanyGUID") as Label;
                Label hdSKU = item.FindControl("hdSKU") as Label;
                Label hdUPC = item.FindControl("hdUPC") as Label;
                TextBox txtICCID = item.FindControl("txtICCID") as TextBox;
                
                int.TryParse(hdnSKUId.Text, out ItemCompanyGUID);
                int.TryParse(hdnMappedItemCompanyGUID.Text, out mappedItemCompanyGUID);
                if (mappedItemCompanyGUID > 0)
                    IsMappedSKU = true;
                esnDetail.ESN = txtESN.Text.Trim();
                esnDetail.ICCID = txtICCID.Text;
                esnDetail.UPC = hdUPC.Text;
                esnDetail.SKU = hdSKU.Text;
                esnDetail.IsPrint = chkPrint.Checked;
                esnDetail.ItemCompanyGUID = ItemCompanyGUID;
                esnDetail.MappedItemCompanyGUID = mappedItemCompanyGUID;
                esnList.Add(esnDetail);
            }
            Session["esnList"] = esnList;
            serviceOrder.SODetail = esnList;
            serviceOrder.ServiceOrderId = 0;
            serviceOrder.CustomerOrderNumber = txtCustOrderNo.Text.Trim();
            serviceOrder.ServiceOrderNumber = txtSONumber.Text.Trim();
            serviceOrder.OrderDate = txtOrderDate.Text.Trim();
            serviceOrder.Quantity = Convert.ToInt32(txtOrderQty.Text);
            serviceOrder.KittedSKUId = Convert.ToInt32(ddlKitted.SelectedValue);



            int returnResult = ServiceOrderOperation.ServiceOrderInsertUpdate(serviceOrder, userId, out errorMessage);
            if (returnResult > 0 && string.IsNullOrEmpty(errorMessage))
            {
                lblMsg.Text = "Submitted successfully";
                foreach (GridViewRow item in gvSOEsn.Rows)
                {
                    
                    TextBox txtESN = item.FindControl("txtESN") as TextBox;
                   // TextBox txtMSL = item.FindControl("txtMSL") as TextBox;
                    TextBox txtICCID = item.FindControl("txtICCID") as TextBox;
                    CheckBox chkPrint = item.FindControl("chkPrint") as CheckBox;
                   // chkPrint.Enabled = false;
                    txtESN.Enabled = false;
                   // txtMSL.Enabled = false;
                    txtICCID.Enabled = false;
                }
                btnSubmit.Visible = false;
                btnPrint.Visible = true;
                //btnDownload.Visible = true;
                gvSOEsn.Columns[5].Visible = true;
                if (!IsMappedSKU)
                {
                    gvSOEsn.Columns[3].Visible = false;
                    gvSOEsn.Columns[4].Visible = false;
                }
                // int serviceOerderNo = ServiceOrderOperation.GenerateServiceOrder();
                // txtSONumber.Text = serviceOerderNo.ToString();
                // ClearForm();
                // dpCompany.SelectedIndex = 0;
            }
            else
            {
                lblMsg.Text = errorMessage;
            }

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            if (ViewState["soid"] != null)
            {
                Response.Redirect("ServiceOrderSearch.aspx?search=so", false);
            }
            else
            {
                rdUpload.Checked = false;
                rdScan.Checked = true;
                lblCounts.Text = string.Empty;
                lblCount.Text = string.Empty;
                lblMsg.Text = string.Empty;
                ClearForm();
                txtCustOrderNo.Text = string.Empty;
                ddlKitted.Items.Clear();
                dpCompany.SelectedIndex = 0;
            }
        }

        private void ClearForm()
        {
            rdUpload.Checked = false;
            rdScan.Checked = true;
            lblCounts.Text = string.Empty;
            lblCount.Text = string.Empty;

            btnSubmit.Visible = false;
            //btnPrint.Visible = false;
            btnValidate.Visible = false;
            //btValidate.Visible = false;
            btnCancel.Visible = false;
            pnlSearch.Visible = false;
            btnAdd.Visible = false;
            //BindCustomer();
            //int serviceOerderNo = ServiceOrderOperation.GenerateServiceOrder();
            //txtSONumber.Text = serviceOerderNo.ToString();
            txtOrderDate.Text = DateTime.Now.ToShortDateString();
            txtSONumber.Enabled = false;
            txtOrderDate.Enabled = false;
            txtOrderQty.Text = string.Empty;
            //txtCustOrderNo.Text = string.Empty;
            txtCustOrderNo.Enabled = true;
            txtOrderQty.Enabled = true;

            rptESN.DataSource = null;
            rptESN.DataBind();
            gvSOEsn.DataSource = null;
            gvSOEsn.DataBind();
            
            


        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            List<Model> models = new List<Model>();
            string kittedSKU = ddlKitted.SelectedItem.Text;
            ESNLabelOperation slabel = new ESNLabelOperation();
            ServiceOrderDetail soDetail = null;
            bool IsPrint = false;

            foreach (GridViewRow item in gvSOEsn.Rows)
            {
                soDetail = new ServiceOrderDetail();
                TextBox txtESN = item.FindControl("txtESN") as TextBox;
                CheckBox chkPrint = item.FindControl("chkPrint") as CheckBox;
                // HiddenField hdnSKUId = item.FindControl("hdnSKUId") as HiddenField;
                Label hdSKU = item.FindControl("hdSKU") as Label;
                Label hdUPC = item.FindControl("hdUPC") as Label;
                TextBox txtICCID = item.FindControl("txtICCID") as TextBox;
                soDetail.ESN = txtESN.Text.Trim();
                soDetail.ICCID = txtICCID.Text;
                soDetail.UPC = hdUPC.Text;
                soDetail.SKU = kittedSKU;
                soDetail.IsPrint = chkPrint.Checked;
                if (soDetail.IsPrint)
                {
                    IsPrint = true;
                    models.Add(new Model(soDetail.SKU, soDetail.ESN, soDetail.ICCID, soDetail.UPC));
                }
            }
            if (IsPrint)
            {
                var memSt = slabel.ExportToPDF(models);
                if (memSt != null)
                {
                    string fileType = ".pdf";
                    string filename = DateTime.Now.Ticks + fileType;
                    Response.Clear();
                    //Response.ContentType = "application/pdf";
                    Response.ContentType = "application/octet-stream";
                    Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                    Response.Buffer = true;
                    Response.Clear();
                    var bytes = memSt.ToArray();
                    Response.OutputStream.Write(bytes, 0, bytes.Length);
                    Response.OutputStream.Flush();


                    lblMsg.Text = "Label generated successfully.";

                    //string imagePath = "~/pdffiles/";
                    //string fileType = ".pdf";
                    //string fileDirctory = HttpContext.Current.Server.MapPath(imagePath);
                    //string filename = DateTime.Now.Ticks + fileType;
                    //slabel.RetriveLabelFromMemory(memSt, filename, fileDirctory);
                    //string baseurl = System.Configuration.ConfigurationManager.AppSettings["url"].ToString();
                    //string filePath = baseurl + "/pdffiles/" + filename;
                    //lnk_Print.HRef = filePath;
                    //lnk_Print.Visible = true;
                }
                else
                    lblMsg.Text = "Technical error!";
            }
            else
            {
                lblMsg.Text = "Please select ESN to generate label!";
            }

        }
        private void GetServiceRequestNumber()
        {
            int companyID = 0;
            string sorNumber = string.Empty;
            rdUpload.Checked = false;
            rdScan.Checked = true;
            lblCounts.Text = string.Empty;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            ClearForm();
            SV.Framework.SOR.ServiceRequestOperations serviceRequestOperations = SV.Framework.SOR.ServiceOrderOperation.CreateInstance<SV.Framework.SOR.ServiceRequestOperations>();

            SV.Framework.Models.SOR.ServiceRequestDetail serviceRequestDetail = null;
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                BindCompanySKU(companyID);
                sorNumber = txtCustOrderNo.Text.Trim();
                if (!string.IsNullOrWhiteSpace(sorNumber))
                {
                    serviceRequestDetail = serviceRequestOperations.ServiceRequestNumberSearch(companyID, sorNumber);

                    if(serviceRequestDetail != null && !string.IsNullOrWhiteSpace(serviceRequestDetail.Status))
                    {
                        if(serviceRequestDetail.Status == "Received")
                        {
                            ddlKitted.SelectedValue = serviceRequestDetail.ItemcompanyGUID.ToString();
                            txtOrderQty.Text = serviceRequestDetail.Quantity.ToString();
                            ddlKitted.Enabled = false;
                            txtOrderQty.Enabled = false;
                            
                            if (serviceRequestDetail.RawSKUs != null && serviceRequestDetail.RawSKUs.Count > 0)
                            {
                                rptESN.DataSource = serviceRequestDetail.RawSKUs;
                                rptESN.DataBind();
                                foreach (SV.Framework.Models.SOR.KittedRawSKU item in serviceRequestDetail.RawSKUs)
                                {
                                    if(!string.IsNullOrWhiteSpace(item.StockMsg))
                                    {
                                        lblMsg.Text = "Raw SKU# " + item.SKU + " - " + item.StockMsg;
                                    }
                                    
                                }
                                if (lblMsg.Text == "")
                                {
                                    Session["RawSKUs"] = serviceRequestDetail.RawSKUs;
                                    btnAdd.Visible = true;
                                    trScan.Visible = true;
                                    
                                }
                                pnlSearch.Visible = true;
                            }
                            else
                            {
                                rptESN.DataSource = null;
                                rptESN.DataBind();
                                lblMsg.Text = "No SKU are assigned!";
                            }

                        }
                        else
                        {
                            lblMsg.Text = "Customer order number already " + serviceRequestDetail.Status;
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Customer order number not exists";
                    }
                }
                else
                {
                    lblMsg.Text = "Customer order number is required!";
                }
            }
            else
            {
                lblMsg.Text = "Customer is required!";
            }

        }

        private void BindRawSKUs(int companyID, int itemCompanyGUID)
        {
            //ddlSKU.Items.Clear();
            //plnSKU.Visible = false;
            //btnSubmit.Visible = false;
            //btnCancel.Visible = false;
            //trHr.Visible = false;
            lblMsg.Text = string.Empty;
            
            //companyID = itemCompanyGUID = 0;
            if (companyID > 0)
            {
                if (itemCompanyGUID > 0)
                {
                    //companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    //if (ddlKitted != null && ddlKitted.Items.Count > 0)
                    {
                        //if (ddlKitted.SelectedIndex > 0)
                         //   itemCompanyGUID = Convert.ToInt32(ddlKitted.SelectedValue);

                        List<RawSKU> rawSKUList = FinishSKUOperations.GetKittedAssignedRawSKUs(companyID, itemCompanyGUID);
                        if (rawSKUList != null && rawSKUList.Count > 0)
                        {
                            rptESN.DataSource = rawSKUList;
                            //ddlSKU.DataValueField = "ItemcompanyGUID";
                            //ddlSKU.DataTextField = "SKU";
                            rptESN.DataBind();
                           // ListItem newList = new ListItem("", "");
                           // ddlSKU.Items.Insert(0, newList);
                            Session["RawSKUs"] = rawSKUList;
                            btnAdd.Visible = true;
                            trScan.Visible = true;

                            //btnValidate.Visible = false;
                            //btnValidate.Visible = true;
                            //btValidate.Visible = false;
                            //if (lblMsg.Text == string.Empty)
                            //{
                            //    //plnSKU.Visible = true;
                            //    btnSubmit.Visible = true;
                            //    btnCancel.Visible = true;
                            //    //trHr.Visible = true;
                            //    //lblMsg.CssClass = "errorGreenMsg";
                            //    //lblConfirm.Text = "SIM file is ready to upload";
                            //    //btnSearch.Visible = false;
                            //    btnSubmit.Visible = true;
                            //    /// btnSubmit2.Visible = true;
                            //    pnlSearch.Visible = true;
                            //    // trSKU.Visible = false;

                            //}
                            //else
                            //{
                            //    // trSKU.Visible = false;
                            //    //btnSearch.Visible = true;
                            //    btnSubmit.Visible = false;

                            //    //  btnSubmit2.Visible = false;
                            //    //pnlSearch.Visible = false;

                            //}
                        }
                        else
                        {
                            //btnValidate.Visible = false;
                            //btnSubmit.Visible = false;
                            //btnCancel.Visible = false;
                            // ddlSKU.Items.Clear();
                            //trHr.Visible = false;
                            rptESN.DataSource = null;
                            rptESN.DataBind();
                            lblMsg.Text = "No SKU are assigned!";
                        }
                    }
                    //else
                    //{
                    //    lblMsg.Text = "No SKU/Product are assigned to selected Customer";
                    //    ddlKitted.Items.Clear();
                    //    ddlKitted.DataSource = null;
                    //    ddlKitted.DataBind();

                    //}

                }
                else
                {
                    lblMsg.Text = "SKU is required!";
                    ddlKitted.DataSource = null;
                    ddlKitted.DataBind();
                }
            }
            else
            {
                lblMsg.Text = "Customer is required!";
                dpCompany.DataSource = null;
                dpCompany.DataBind();
            }
        }

        private void BindCompanySKU(int companyID)
        {
            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = FinishSKUOperations.GetCompanyFinalOrRawSKUList(companyID, true);
            if (skuList != null && skuList.Count > 0)
            {
                ddlKitted.DataSource = skuList;
                ddlKitted.DataValueField = "ItemcompanyGUID";
                ddlKitted.DataTextField = "SKU";

                ddlKitted.DataBind();
                ListItem newList = new ListItem("", "");
                ddlKitted.Items.Insert(0, newList);
            }
            if (skuList != null)
            {
                ViewState["skulist"] = skuList;
                ddlKitted.DataSource = skuList;
                ddlKitted.DataValueField = "ItemcompanyGUID";
                ddlKitted.DataTextField = "SKU";


                ddlKitted.DataBind();
                ListItem item = new ListItem("", "0");
                ddlKitted.Items.Insert(0, item);
            }
            else
            {
                ViewState["skulist"] = null;
                ddlKitted.DataSource = null;
                ddlKitted.DataBind();
                lblMsg.Text = "No SKU are assigned to selected Customer";

            }


        }
        public int IncreementIndex { get; set; }
        protected void btnGenerateEsn_Click(object sender, EventArgs e)
        {
            IncreementIndex = 1;
            //btnSubmit.Visible = false;
            //btnPrint.Visible = false;
            // DataTable dt = new DataTable();
            //btnCancel.Visible = false;
            //pnlSearch.Visible = true;
            string errorMessage = string.Empty, skus = string.Empty;
            int validQty = 0;

            List<SOQtyValidate> sOQtyValidates = new List<SOQtyValidate>();
            List<ServiceOrderDetail> esnList = new List<ServiceOrderDetail>();
            List<ServiceOrderDetail> esnRangeList = new List<ServiceOrderDetail>();
            ServiceOrderDetail esnInfo = null;
            SOQtyValidate sOQtyValidate = null;
            Int64 esn = 0;
            lblMsg.Text = string.Empty;
            int qty = 0, itemQty = 0, requiredQty = 0; ;
            int ItemcompanyGUID = 0, itr = 1, mappedItemCompanyGUID = 0;
            bool ICCID = false;
            bool IsMappedSKU = false;
            bool IsESNRequired = false;
            string esns = string.Empty;
            int.TryParse(txtOrderQty.Text.Trim(), out qty);
            if (qty > 0)
            {
                foreach (RepeaterItem item in rptESN.Items)
                {

                    sOQtyValidate = new SOQtyValidate();
                    itemQty = 0;
                    esn = 0;
                    HiddenField hdIsESNRequired = item.FindControl("hdIsESNRequired") as HiddenField;
                    IsESNRequired = Convert.ToBoolean(hdIsESNRequired.Value);
                    if (IsESNRequired)
                    {
                        TextBox txtICCID = item.FindControl("txtICCID") as TextBox;
                        TextBox txtsku = item.FindControl("txtsku") as TextBox;
                        HiddenField hdSKUId = item.FindControl("hdSKUId") as HiddenField;
                        HiddenField hdMappedItemCompanyGUID = item.FindControl("hdMappedItemCompanyGUID") as HiddenField;

                        HiddenField hdnQty = item.FindControl("hdnQty") as HiddenField;
                        int.TryParse(hdnQty.Value, out requiredQty);
                        if (requiredQty == 0)
                            requiredQty = 1;
                        itemQty = qty * requiredQty;


                        int.TryParse(hdSKUId.Value, out ItemcompanyGUID);
                        int.TryParse(hdMappedItemCompanyGUID.Value, out mappedItemCompanyGUID);
                        if (mappedItemCompanyGUID > 0)
                        {
                            IsMappedSKU = true;
                            IncreementIndex = 2;
                        }

                        //if (!string.IsNullOrEmpty(txtICCID.Text))
                        //    ICCID = true;

                        //Int64.TryParse(txtICCID.Text, out esn);
                        //if (esn > 0)
                        {
                            sOQtyValidate.StartESN = txtICCID.Text;
                            sOQtyValidate.ItemCompanyGUID = ItemcompanyGUID;
                            //sOQtyValidate.MappedItemCompanyGUID = mappedItemCompanyGUID;
                            sOQtyValidate.Id = itr;
                            sOQtyValidate.Qty = itemQty;
                            //if (string.IsNullOrEmpty(esns))
                            //{
                            //    esns = txtICCID.Text.Trim();
                            //    skus = hdSKUId.Value;
                            //}
                            //else
                            //{
                            //    esns = esns + "," + txtICCID.Text.Trim();
                            //    skus = skus + "," + hdSKUId.Value;
                            //}

                            for (int i = 1; i <= itemQty; i++)
                            {
                                esnInfo = new ServiceOrderDetail();
                                esnInfo.ItemCompanyGUID = ItemcompanyGUID;
                                esnInfo.SKU = txtsku.Text;
                                esnInfo.MappedItemCompanyGUID = mappedItemCompanyGUID;
                                esnInfo.ESN = string.Empty;// esn.ToString();
                                esnInfo.ICCID = string.Empty;//esnInfo.ESN;
                                esnInfo.BatchNumber = "";
                                esnInfo.ValidationMsg = "";
                                esnInfo.IsPrint = true;
                                esnList.Add(esnInfo);
                                esn = esn + 1;
                            }
                            esn = esn - 1;
                            sOQtyValidate.EndESN = esn.ToString();
                            itr = itr + 1;
                            sOQtyValidates.Add(sOQtyValidate);
                        }
                    }
                    //else
                    //{
                    //    lblMsg.Text = "ICCID Starts# is required!";
                    //    return;
                    //}
                }
            }
            else
            {
                lblMsg.Text = "Quantity is required!";
                return;
            }
            //esnRangeList = ServiceOrderOperation.ValidateServiceOrderEsnRange(sOQtyValidates, out errorMessage, out validQty);
            //if(!string.IsNullOrEmpty(errorMessage) && validQty == -1)
            //{
            //    lblMsg.Text = errorMessage + " not exists in inventory for the selected SKUs";
            //    return;
            //}
            //if (validQty == 1)
            //{
            //    lblMsg.Text = "Insufficient quantities in inventory stock please reduce the quantity";
            //    return;
            //}
            if (esnList != null && esnList.Count > 0 && esnList.Count <= 100)
            {
                trUpload.Visible = false;
                trScan.Visible = true;
                gvSOEsn.DataSource = esnList;
                gvSOEsn.DataBind();
                btnValidate.Visible = true;
                gvSOEsn.Columns[5].Visible = false;
                if(!IsMappedSKU)
                {
                    gvSOEsn.Columns[3].Visible = false;
                    gvSOEsn.Columns[4].Visible = false;
                }
                //pnlSearch.Visible = true;
                lblCount.Text = "Total count: " + esnList.Count;
            }
            else
            {
                trScan.Visible = false;
                trUpload.Visible = true;
                btnValidate.Visible = false;
                rdScan.Checked = false;
                rdUpload.Checked = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Service order has required more than 100 ESNs only upto 100 ESNs are allowed using scanning!')</script>", false);
                //if(!ICCID)
                //{
                //    lblMsg.Text = "ICCID is required!";
                //}
                //else
                //    lblMsg.Text = "There is no ESN to validate!";

                //btnValidate.Visible = false;
                lblCount.Text = string.Empty;
                gvSOEsn.DataSource = null;
                gvSOEsn.DataBind();
            }
        }

        protected void btValidate_Click(object sender, EventArgs e)
        {
            string errorMessage = string.Empty;
            int ItemCompanyGUID = 0;
            bool IsValidate = true;
            ServiceOrders serviceOrder = new ServiceOrders();
            List<ServiceOrderDetail> esnList = new List<ServiceOrderDetail>();
            ServiceOrderDetail esnDetail = null;
            foreach (GridViewRow item in gvSOEsn.Rows)
            {
                esnDetail = new ServiceOrderDetail();
                TextBox txtESN = item.FindControl("txtESN") as TextBox;
                CheckBox chkPrint = item.FindControl("chkPrint") as CheckBox;
                HiddenField hdnSKUId = item.FindControl("hdnSKUId") as HiddenField;
                int.TryParse(hdnSKUId.Value, out ItemCompanyGUID);
                esnDetail.ESN = txtESN.Text.Trim();
                esnDetail.IsPrint = chkPrint.Checked;
                esnDetail.ItemCompanyGUID = ItemCompanyGUID;
                
                esnList.Add(esnDetail);
            }
            serviceOrder.SODetail = esnList;
            serviceOrder.ServiceOrderId = 0;
            serviceOrder.CustomerOrderNumber = txtCustOrderNo.Text.Trim();
            //if (!string.IsNullOrEmpty(serviceOrder.CustomerOrderNumber))
            {
                esnList = ServiceOrderOperation.ValidateServiceOrder(serviceOrder, out errorMessage, out IsValidate);
                if (esnList != null && esnList.Count > 0)
                {
                    gvSOEsn.DataSource = esnList;
                    gvSOEsn.DataBind();

                    gvSOEsn.Columns[5].Visible = false;
                    Session["esnList"] = esnList;
                    //pnlSearch.Visible = true;
                    lblCount.Text = "Total count: " + esnList.Count;
                    if (IsValidate && string.IsNullOrEmpty(errorMessage))
                    {
                        btnAdd.Visible = false;
                        btnSubmit.Visible = true;
                        btnCancel.Visible = true;
                    }
                }
                else
                {
                    gvSOEsn.DataSource = null;
                    gvSOEsn.DataBind();
                }
            }
            //else
              //  lblMsg.Text = "Customer order number is required!";
        }

        protected void gvSOEsn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //header select all function
            if (e.Row.RowType == DataControlRowType.Header)
            {
                ((CheckBox)e.Row.FindControl("chkAll")).Attributes.Add("onclick",
                    "javascript:SelectAll('" +
                    ((CheckBox)e.Row.FindControl("chkAll")).ClientID + "')");
            }

        }

        protected void btnUploadValidate_Click(object sender, EventArgs e)
        {
            UploadInventoryInfo();
        }
        private void UploadInventoryInfo()
        {
            ServiceOrders serviceOrder = new ServiceOrders();
            List<ServiceOrderDetail> esnList = new List<ServiceOrderDetail>();
            ServiceOrderDetail esnDetail = null;
            string errorMessage = string.Empty;
            int KittedSKUId = 0, qty=0;
            
            bool IsValidate = true, IsMappedSKU = false;
            int numberOfESN = 0, requiredQty = 0, mappedItemCompanyGUID = 0;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            Hashtable hshESN = new Hashtable();
            bool IsESNRequired = false;
            bool columnsIncorrectFormat = false;
            string customerOrderNumber = txtCustOrderNo.Text.Trim();
            string serviceOrderNumber = txtSONumber.Text.Trim();
            if (dpCompany.SelectedIndex > 0)
            {
                if (ddlKitted.SelectedIndex > 0)
                {
                    KittedSKUId = Convert.ToInt32(ddlKitted.SelectedValue);
                    int.TryParse(txtOrderQty.Text.Trim(), out qty);
                    if (qty > 0)
                    {
                        foreach (RepeaterItem item in rptESN.Items)
                        {
                            HiddenField hdIsESNRequired = item.FindControl("hdIsESNRequired") as HiddenField;
                            IsESNRequired = Convert.ToBoolean(hdIsESNRequired.Value);
                            if (IsESNRequired)
                            {
                                HiddenField hdMappedItemCompanyGUID = item.FindControl("hdMappedItemCompanyGUID") as HiddenField;

                                int.TryParse(hdMappedItemCompanyGUID.Value, out mappedItemCompanyGUID);
                                if (mappedItemCompanyGUID > 0)
                                {
                                    IsMappedSKU = true;
                                }
                                HiddenField hdnQty = item.FindControl("hdnQty") as HiddenField;
                                int.TryParse(hdnQty.Value, out requiredQty);
                                if (requiredQty == 0)
                                    requiredQty = 1;
                                numberOfESN += qty * requiredQty;
                            }
                        }
                        if (!string.IsNullOrEmpty(serviceOrderNumber))
                        {
                            serviceOrder.CompanyId = Convert.ToInt32(dpCompany.SelectedValue);
                            serviceOrder.CustomerOrderNumber = customerOrderNumber;
                            serviceOrder.ServiceOrderNumber = serviceOrderNumber;
                            serviceOrder.KittedSKUId = KittedSKUId;

                            serviceOrder.Quantity = qty;

                            try
                            {
                                if (flnUpload.PostedFile.FileName.Trim().Length == 0)
                                {
                                    lblMsg.Text = "Select file to upload";
                                }
                                else
                                {
                                    if (flnUpload.PostedFile.ContentLength > 0)
                                    {
                                        string fileName = UploadFile();
                                        string extension = Path.GetExtension(flnUpload.PostedFile.FileName);
                                        extension = extension.ToLower();
                                        string invalidColumns = string.Empty;

                                        if (extension == ".csv")
                                        {
                                            try
                                            {
                                                using (StreamReader sr = new StreamReader(fileName))
                                                {
                                                    string line;
                                                    string esn, ICCID;
                                                    int i = 0;
                                                    while ((line = sr.ReadLine()) != null)
                                                    {

                                                        if (!string.IsNullOrEmpty(line) && i == 0)
                                                        {
                                                            i = 1;
                                                            line = line.ToLower();
                                                            string[] headerArray = line.Split(',');

                                                            if (headerArray[0].Trim() != "esn")
                                                            {
                                                                if (string.IsNullOrEmpty(invalidColumns))
                                                                    invalidColumns = headerArray[0];
                                                                else
                                                                    invalidColumns = invalidColumns + ", " + headerArray[0];
                                                                columnsIncorrectFormat = true;
                                                            }
                                                            if (headerArray.Length > 1 && headerArray[1].Trim() != "")
                                                            {
                                                                if (headerArray[1].Trim() != "iccid")
                                                                {
                                                                    if (string.IsNullOrEmpty(invalidColumns))
                                                                        invalidColumns = headerArray[1];
                                                                    else
                                                                        invalidColumns = invalidColumns + ", " + headerArray[1];
                                                                    columnsIncorrectFormat = true;
                                                                }
                                                            }

                                                        }
                                                        else if (!string.IsNullOrEmpty(line) && i > 0)
                                                        {
                                                            esn = ICCID = string.Empty;// fmupc = lteICCID = lteIMSI = otksl = akey = msl = string.Empty;
                                                            string[] arr = line.Split(',');
                                                            try
                                                            {

                                                                if (arr.Length > 0)
                                                                {
                                                                    esn = arr[0].Trim();
                                                                }
                                                                if (arr.Length > 1)
                                                                {
                                                                    ICCID = arr[1].Trim();

                                                                }

                                                                if (string.IsNullOrEmpty(esn))
                                                                {
                                                                    lblMsg.Text = "Missing required data";
                                                                }

                                                                esnDetail = new ServiceOrderDetail();

                                                                if (hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                                {
                                                                    //uploadEsn = false;
                                                                    throw new ApplicationException("Duplicate ESN(s) exists in the file");
                                                                }
                                                                else if (!hshESN.ContainsKey(esn) && !string.IsNullOrEmpty(esn))
                                                                {
                                                                    hshESN.Add(esn, esn);
                                                                }
                                                                //uploadEsn = true;
                                                                esnDetail.ESN = esn;
                                                                esnDetail.ICCID = ICCID;
                                                                esnList.Add(esnDetail);

                                                                esn = string.Empty;
                                                                ICCID = string.Empty;
                                                            }
                                                            catch (ApplicationException ex)
                                                            {
                                                                throw ex;
                                                            }
                                                            catch (Exception exception)
                                                            {
                                                                lblMsg.Text = exception.Message;
                                                            }
                                                        }
                                                    }

                                                    sr.Close();
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                lblMsg.Text = ex.Message;
                                            }


                                            if (esnList != null && esnList.Count > 0 && columnsIncorrectFormat == false)
                                            {
                                                //rptESN.DataSource = esnList;
                                                //rptESN.DataBind();
                                                //lblCount.Text = "Total count: " + esnList.Count;
                                                Session["esns"] = esnList;

                                                int n = 0;
                                                int poRecordCount = 0;
                                                //string poErrorMessage = string.Empty;
                                                //string invalidLTEESNMessage = string.Empty;
                                                //string invalidESNMessage = string.Empty;
                                                //string invalidSkuESNMessage = string.Empty;
                                                //string esnExistsMessage = string.Empty;
                                                //string badESNMessage = string.Empty;
                                                //string errorMessage = string.Empty;
                                                List<ServiceOrderDetail> esnList1 = new List<ServiceOrderDetail>();
                                                double totalChunk = 0;
                                                try
                                                {

                                                    totalChunk = (double)esnList.Count / 15000;
                                                    n = Convert.ToInt16(Math.Ceiling(totalChunk));
                                                    int esnCount = 15000;
                                                    //int skip = 1000;
                                                    int listLength = esnList.Count;
                                                    List<ServiceOrderDetail> esnToUpload = null;
                                                    //var esnToUpload;
                                                    //for (int i = 0; i < n; i++)
                                                    for (int i = 0; i < listLength; i = i + 15000)
                                                    {

                                                        esnToUpload = new List<ServiceOrderDetail>();
                                                        serviceOrder.SODetail = new List<ServiceOrderDetail>();
                                                        //invalidLTEESNMessage = invalidESNMessage = invalidSkuESNMessage = esnExistsMessage = badESNMessage = string.Empty;
                                                        //esnToAdd = new List<FulfillmentAssignESN>();
                                                        //if (esnList.Count < 1000)
                                                        //{   esnCount = esnList.Count;
                                                        //    skip = esnList.Count;
                                                        //    if (i > 0)
                                                        //        skip = skip + 1000;
                                                        //}

                                                        esnToUpload = (from item in esnList.Skip(i).Take(esnCount) select item).ToList();

                                                        serviceOrder.SODetail = esnToUpload;
                                                        //Upload/Assign ESN to POs
                                                        int returnValue = 0;
                                                        string AlreadyInUseICCIDMessase = string.Empty, ICCIDNotExistsMessase = string.Empty, InvalidICCIDMessase = string.Empty, AlreadyMappedESNMessase = string.Empty;

                                                        //List<ServiceOrderDetail> esnList2 = ServiceOrderOperation.ValidateServiceOrder(serviceOrder, out errorMessage, out IsValidate);
                                                        esnList = ServiceOrderOperation.Validate_ServiceOrder(serviceOrder, out errorMessage, out IsValidate);
                                                        if (esnList != null && esnList.Count > 0)
                                                        {
                                                            foreach (ServiceOrderDetail item in esnList)
                                                            {
                                                                if (item.MappedItemCompanyGUID > 0)
                                                                    IsMappedSKU = true;
                                                            }
                                                            gvSOEsn.DataSource = esnList;
                                                            gvSOEsn.DataBind();
                                                            foreach (GridViewRow item in gvSOEsn.Rows)
                                                            {

                                                                TextBox txtESN = item.FindControl("txtESN") as TextBox;
                                                                txtESN.Enabled = false;
                                                                TextBox txtICCID = item.FindControl("txtICCID") as TextBox;
                                                                txtICCID.Enabled = false;
                                                            }
                                                            gvSOEsn.Columns[5].Visible = false;
                                                            if (!IsMappedSKU)
                                                            {
                                                                gvSOEsn.Columns[3].Visible = false;
                                                                gvSOEsn.Columns[4].Visible = false;
                                                            }
                                                            Session["esnList"] = esnList;
                                                            //pnlSearch.Visible = true;
                                                            lblCounts.Text = "Total count: " + esnList.Count;
                                                            //txtOrderQty.Text = esnList.Count.ToString();

                                                            if (IsValidate && string.IsNullOrEmpty(errorMessage) && numberOfESN == esnList.Count)
                                                            {
                                                                btnAdd.Visible = false;
                                                                btnValidate.Visible = false;
                                                                //btValidate.Visible = false;
                                                                trUpload.Visible = false;
                                                                txtCustOrderNo.Enabled = false;
                                                                txtOrderQty.Enabled = false;

                                                                btnSubmit.Visible = true;
                                                                btnCancel.Visible = true;
                                                            }
                                                            else
                                                            {
                                                                if (!IsValidate)
                                                                {
                                                                    //btnValidate.Visible = false;
                                                                    trUpload.Visible = true;
                                                                }
                                                                if(numberOfESN != esnList.Count)
                                                                {
                                                                    lblMsg.Text = "The ESN/ICCID required for number of kits entered are not correct!";
                                                                }
                                                                if (!string.IsNullOrEmpty(errorMessage))
                                                                    lblMsg.Text = errorMessage + " Customer order number already exists!";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            gvSOEsn.DataSource = null;
                                                            gvSOEsn.DataBind();
                                                        }
                                                        //esnList1.AddRange(esnList2);





                                                    }

                                                }
                                                catch (Exception ex)
                                                {
                                                    lblMsg.Text = ex.Message;
                                                }
                                                if (!string.IsNullOrEmpty(errorMessage))
                                                {
                                                    lblMsg.Text = errorMessage;
                                                    return;
                                                }
                                                if (lblMsg.Text == string.Empty)
                                                {
                                                    //lblMsg.CssClass = "errorGreenMsg";
                                                    //lblConfirm.Text = "ESN file is ready to upload";
                                                    //btnUpload.Visible = false;
                                                    //btnSubmit.Visible = true;
                                                    //btnSubmit2.Visible = true;
                                                    //pnlSubmit.Visible = true;
                                                    //row1.Visible = false;
                                                    //row2.Visible = false;
                                                    ////btnSearch.Visible = false;

                                                }
                                                else
                                                {
                                                    //btnUpload.Visible = true;
                                                    //btnSubmit.Visible = false;
                                                    //row1.Visible = true;
                                                    //row2.Visible = true;
                                                    //btnSubmit2.Visible = false;
                                                    //pnlSubmit.Visible = false;

                                                }

                                            }
                                            else
                                            {
                                                gvSOEsn.DataSource = null;
                                                gvSOEsn.DataBind();

                                                if (esnList != null && esnList.Count == 0)
                                                {
                                                    if (columnsIncorrectFormat)
                                                    {
                                                        lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                                    }
                                                    else
                                                        lblMsg.Text = "There is no ESN to upload";

                                                }
                                                if (esnList != null)
                                                {
                                                    if (columnsIncorrectFormat)
                                                    {
                                                        lblMsg.Text = invalidColumns + " column(s) name is not correct";
                                                    }
                                                    else
                                                        lblMsg.Text = "There is no ESN to upload";
                                                }
                                            }
                                        }
                                        else
                                            lblMsg.Text = "Invalid file!";
                                    }
                                    else
                                        lblMsg.Text = "Invalid file!";
                                }
                            }
                            catch (Exception ex)
                            {
                                lblMsg.Text = ex.Message;
                            }

                        }
                    }
                    else
                        lblMsg.Text = "Please enter number of kits!";

                }
                else
                    lblMsg.Text = "Please select Kitted SKU!";

            }
            else
                lblMsg.Text = "Please select customer!";
        }
        private string fileStoreLocation = "~/UploadedData/";
        private string UploadFile()
        {

            string actualFilename = string.Empty;
            Int32 maxFileSize = 1572864;
            actualFilename = System.IO.Path.GetFileName(flnUpload.PostedFile.FileName);
            if (ConfigurationManager.AppSettings["PurchaseOrderFilesStoreage"] != null)
            {
                fileStoreLocation = ConfigurationManager.AppSettings["PurchaseOrderFilesStoreage"].ToString();
            }

            fileStoreLocation = Server.MapPath(fileStoreLocation);
            if (File.Exists(fileStoreLocation + actualFilename))
            {
                actualFilename = System.Guid.NewGuid().ToString() + actualFilename;
            }

            flnUpload.PostedFile.SaveAs(fileStoreLocation + actualFilename);

            FileInfo fileInfo = new FileInfo(fileStoreLocation + actualFilename);

            if (ConfigurationManager.AppSettings["maxCSVfilesize"] != null)
            {
                if (Int32.TryParse(ConfigurationManager.AppSettings["maxCSVfilesize"].ToString(), out maxFileSize))
                {
                    if (fileInfo.Length > maxFileSize)
                    {
                        fileInfo.Delete();
                        throw new Exception("File size is greater than " + maxFileSize + " bytes");
                    }
                }
            }



            return fileStoreLocation + actualFilename;
        }

        protected void rdUpload_CheckedChanged(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            btnCancel.Visible = false;
            btnSubmit.Visible = false;
            btnValidate.Visible = false;
            lblCount.Text = string.Empty;
            lblCounts.Text = string.Empty;
            gvSOEsn.DataSource = null;
            gvSOEsn.DataBind();
            trUpload.Visible = true;
            trScan.Visible = false;
        }

        protected void rdScan_CheckedChanged(object sender, EventArgs e)
        {
            btnCancel.Visible = false;
            btnSubmit.Visible = false;
            btnValidate.Visible = false;
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            lblCounts.Text = string.Empty;
            gvSOEsn.DataSource = null;
            gvSOEsn.DataBind();
            trUpload.Visible = false;
            trScan.Visible = true;
            btnAdd.Visible = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetServiceRequestNumber();
        }
    }
}