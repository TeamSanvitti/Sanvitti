using System;
using System.Data;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;


namespace avii.Reports
{
    public partial class CustomerSkuEsnSummary : System.Web.UI.Page
    {
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

            if (!IsPostBack)
            {
                btnDownload.Visible = false;
                if (Session["adm"] == null)
                {
                    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                    if (userInfo != null)
                    {
                        //dpCompany.SelectedValue = userInfo.CompanyAccountNumber;

                        lblCompany.Text = userInfo.CompanyName;
                        dpCompany.Visible = false;
                        ViewState["companyid"] = userInfo.CompanyGUID;
                        BindCompanySKU(userInfo.CompanyGUID);


                    }
                }
                else
                {
                    BindCustomer();
                    ddlSKU.Visible = false;
                    lblSKU.Visible = false;
            
                }
            }
        }

        private void BindCustomer()
        {
            DataTable dt = avii.Classes.clsCompany.GetCompany(0, 0);
            ViewState["customer"] = dt;
            dpCompany.DataSource = dt;
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        private void BindCompanySKU(int companyID)
        {
            lblMsg.Text = string.Empty;
            int IsSim = 0;
            if (ddlOption.SelectedIndex == 0)
                IsSim = 0;
            else
                IsSim = 1;
            List<CompanySKUno> skuList = MslOperation.GetCompanySKUList(companyID, IsSim);
            if (skuList != null)
            {
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "ItemcompanyGUID";
                ddlSKU.DataTextField = "SKU";

                ddlSKU.DataBind();
                ddlSKU.Items.Insert(0, new ListItem("", "0"));
                ddlSKU.Visible = true;
                lblSKU.Visible = true;
            }
            else
            {
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                ddlSKU.Items.Insert(0, new ListItem("", "0"));
                lblMsg.Text = "No SKU are assigned to select Customer";

            }






        }
        
        private void BindSkuESNSummary()
        {
            int companyID = 0;
            int duration = 365;
            int itemCompanyGuid = 0;
            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;
            lblMsg.Text = string.Empty;
            //if(ddlDuration != null && ddlDuration.se
            //duration = Convert.ToInt32(ddlDuration.SelectedValue);
            //DateTime dt = new DateTime(;

            if (ddlSKU.SelectedIndex > 0)
                itemCompanyGuid = Convert.ToInt32(ddlSKU.SelectedValue);
            
            if (txtFromDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtFromDate.Text, out dt))
                    fromDate = dt;
                else
                    throw new Exception("Date From does not have correct format(MM/DD/YYYY)");
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtToDate.Text, out dt))
                    toDate = dt;
                else
                    throw new Exception("Date To does not have correct format(MM/DD/YYYY)");
            }
            if (fromDate != DateTime.MinValue && toDate == DateTime.MinValue)
            {
                if (!chkESN.Checked)
                    lblDate.Text = "<b>Fulfillment Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + "<b> &nbsp; &nbsp; Unused ESN Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
                else
                    lblDate.Text = "<b>Fulfillment Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
            }
            //lblDate.Text = fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString(); //esnList[esnList.Count - 1].UploadDate.ToShortDateString() + " - " + esnList[0].UploadDate.ToShortDateString()
            else
                if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
                {
                    if (!chkESN.Checked)
                        lblDate.Text = "<b>Fulfillment Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString() + "<b> &nbsp; &nbsp; Unused ESN Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString();
                    else
                        lblDate.Text = "<b>Fulfillment Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString();
                }
                else
                    if (fromDate == DateTime.MinValue && toDate == DateTime.MinValue)
                    {
                        if (!chkESN.Checked)
                            lblDate.Text = "<b>Fulfillment Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + "<b> &nbsp; &nbsp; Unused ESN Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
                        else
                            lblDate.Text = "<b>Fulfillment Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
                    }

            //if (fromDate != DateTime.MinValue && toDate == DateTime.MinValue)
            //{
            //    if (!chkESN.Checked)
            //        lblDate.Text = "<b>Upload Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>Fulfillment Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + "<b> &nbsp; &nbsp; Unused ESN Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
            //    else
            //        lblDate.Text = "<b>Upload Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>Fulfillment Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
            //}
            ////lblDate.Text = fromDate.ToShortDateString() + " - " + DateTime.Now.ToShortDateString(); //esnList[esnList.Count - 1].UploadDate.ToShortDateString() + " - " + esnList[0].UploadDate.ToShortDateString()
            //else
            //    if (fromDate != DateTime.MinValue && toDate != DateTime.MinValue)
            //    {
            //        if (!chkESN.Checked)
            //            lblDate.Text = "<b>Upload Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString() + " &nbsp; &nbsp; " + "<b>Fulfillment Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString() + "<b> &nbsp; &nbsp; Unused ESN Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString();
            //        else
            //            lblDate.Text = "<b>Upload Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString() + " &nbsp; &nbsp; " + "<b>Fulfillment Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + fromDate.ToShortDateString() + " - " + toDate.ToShortDateString();
            //    }
            //    else
            //        if (fromDate == DateTime.MinValue && toDate == DateTime.MinValue)
            //        {
            //            if (!chkESN.Checked)
            //                lblDate.Text = "<b>Upload Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>Fulfillment Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + "<b> &nbsp; &nbsp; Unused ESN Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
            //            else
            //                lblDate.Text = "<b>Upload Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>Fulfillment Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString() + " &nbsp; &nbsp; " + "<b>RMA Date Range:</b> " + DateTime.Now.AddDays(-duration).ToShortDateString() + " - " + DateTime.Now.ToShortDateString();
            //        }

            if (ViewState["companyid"] != null)
            {
                //avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                //if (userInfo != null)
                //{

                companyID = Convert.ToInt32(ViewState["companyid"]);

                //lblCompany.Text = userInfo.CompanyName;
                //dpCompany.Visible=false;
                //}
            }
            else
            {
                if (dpCompany.SelectedIndex > 0)
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                    lblCompany.Text = string.Empty;
                }
                else
                {
                    lblMsg.Text = "Customer is required!";
                    return;
                }
            }
            if (companyID > 0)
            {
                List<avii.Classes.SkusEsnList> skuESNList = null;
                if (ddlOption.SelectedIndex == 0)
                    skuESNList = avii.Classes.ReportOperations.GetCustomerSkuEsnSummary(companyID, itemCompanyGuid, fromDate, toDate, chkESN.Checked);
                else
                    skuESNList = avii.Classes.ReportOperations.GetCustomerSkuSimSummary(companyID, itemCompanyGuid, fromDate, toDate, chkESN.Checked);
                if (skuESNList != null && skuESNList.Count > 0)
                {
                    btnDownload.Visible = true;
                    gvESN.DataSource = skuESNList;
                    gvESN.DataBind();
                    lblCount.Text = "Total count: " + skuESNList.Count;
                    Session["skusesnlist"] = skuESNList;
                }
                else
                {
                    btnDownload.Visible = false;
                    lblCount.Text = string.Empty;
                    lblMsg.Text = "No record found";
                    gvESN.DataSource = null;
                    gvESN.DataBind();
                }
            }
                //}
            //}

            
        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;

            ddlSKU.Visible = true;
            lblSKU.Visible = true;
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
                BindCompanySKU(companyID);
            else
            {
                ddlSKU.Visible = false;
                lblSKU.Visible = false;

            }
        }
        protected void ddlOption_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            
            gvESN.DataSource = null;
            gvESN.DataBind();

            ddlSKU.Visible = true;
            lblSKU.Visible = true;
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {

                    companyID = userInfo.CompanyGUID;
                }
            }
            else
            {
                if (dpCompany.SelectedIndex > 0)
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }
            if (companyID > 0)
                BindCompanySKU(companyID);
            else
            {
                ddlSKU.Visible = false;
                lblSKU.Visible = false;

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            BindSkuESNSummary();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            chkESN.Checked = false;
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
            gvESN.DataSource = null;
            gvESN.DataBind();
            txtFromDate.Text = string.Empty;
            txtToDate.Text = string.Empty;
            //ddlDuration.SelectedIndex = 0;
            lblDate.Text = string.Empty;
            btnDownload.Visible = false;

        }

        private int processedTotal = 0;
        private int shippedTotal = 0;
        private int unusedTotal = 0;
        private int rmaTotal = 0;
        private int availableBalance = 0;

        protected void gvESN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.DataItem == null)
              //  return;

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                bool isESN = true;
                if (ddlOption.SelectedIndex > 0)
                    isESN = false;
                //Label lblAE = e.Row.FindControl("lblAE") as Label;
                //if(lblAE != null)
                //    availableBalance += Convert.ToInt32(lblAE.Text);

                LinkButton lnkESN = e.Row.FindControl("lnkESN") as LinkButton;
                if (lnkESN != null)
                {
                    if (isESN)
                        lnkESN.OnClientClick = "openDialogAndBlock('Unused ESN', '" + lnkESN.ClientID + "')";
                    else
                        lnkESN.OnClientClick = "openDialogAndBlock('Unused SIM', '" + lnkESN.ClientID + "')";
                    unusedTotal += Convert.ToInt32(lnkESN.Text);
                }
                LinkButton lnkPESN = e.Row.FindControl("lnkPESN") as LinkButton;
                if (lnkPESN != null)
                {
                    if (isESN)
                        lnkPESN.OnClientClick = "openDialogAndBlockP('Processed ESN', '" + lnkPESN.ClientID + "')";
                    else
                        lnkPESN.OnClientClick = "openDialogAndBlockP('Processed SIM', '" + lnkPESN.ClientID + "')";

                    processedTotal += Convert.ToInt32(lnkPESN.Text);
                }
                LinkButton lnkSESN = e.Row.FindControl("lnkSESN") as LinkButton;
                if (lnkSESN != null)
                {
                    if (isESN)
                        lnkSESN.OnClientClick = "openDialogAndBlockS('Shipped ESN', '" + lnkSESN.ClientID + "')";
                    else
                        lnkSESN.OnClientClick = "openDialogAndBlockS('Shipped SIM', '" + lnkSESN.ClientID + "')";

                    shippedTotal += Convert.ToInt32(lnkSESN.Text);
                }
                LinkButton lnkRMA = e.Row.FindControl("lnkRMA") as LinkButton;
                if (lnkRMA != null)
                {
                    if (isESN)
                        lnkRMA.OnClientClick = "openDialogAndBlockR('RMA ESN', '" + lnkRMA.ClientID + "')";
                    else
                        lnkRMA.OnClientClick = "openDialogAndBlockR('RMA SIM', '" + lnkRMA.ClientID + "')";
                    rmaTotal += Convert.ToInt32(lnkRMA.Text);
                }

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                //Label lnkESN = e.Row.FindControl("lnkESN") as Label;
                Literal ltProcessed = (Literal)e.Row.FindControl("ltProcessed");
                Literal ltUnused = (Literal)e.Row.FindControl("ltUnused");
                //Literal ltClosedBalance = (Literal)e.Row.FindControl("ltClosedBalance");
                Literal ltShipped = (Literal)e.Row.FindControl("ltShipped");
                Literal ltRMA = (Literal)e.Row.FindControl("ltRMA");
                ltProcessed.Text = processedTotal.ToString();
                ltUnused.Text = unusedTotal.ToString();
                ltShipped.Text = shippedTotal.ToString();
                ltRMA.Text = rmaTotal.ToString();
                //ltClosedBalance.Text = availableBalance.ToString();


            }
            //LinkButton lnkCode = e.Row.FindControl("lnkCode") as LinkButton;
            //if (lnkCode != null)
            //    lnkCode.OnClientClick = "openProductDialogAndBlock('Product Detail', '" + lnkCode.ClientID + "')";

        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void lnkViewProductInfo_Click(object sender, CommandEventArgs e)
        {

            //Control tmp2 = LoadControl("~/controls/ProductsDetail.ascx");
            //avii.Controls.ProductsDetail ctlViewProduct = tmp2 as avii.Controls.ProductsDetail;
            //pnlESN.Controls.Clear();
            //int itemGUID = Convert.ToInt32(e.CommandArgument);
            ////ViewState["poid"] = poID;
            //if (tmp2 != null)
            //{

            //    ctlViewProduct.GetItemDetails(itemGUID);
            //}
            //pnlProduct.Controls.Add(ctlViewProduct);

            //Control tmp = LoadControl("~/controls/ProductSKUs.ascx");
            //avii.Controls.ProductSKUs ctlProductSKU = tmp as avii.Controls.ProductSKUs;

            //tabPanel.Controls.Clear();

            //if (tmp != null)
            //{

            //    ctlProductSKU.BindItemSKU(itemGUID);
            //}
            //tabPanel.Controls.Add(ctlProductSKU);


            //RegisterStartupScript("jsUnblockDialog", "unblockProductDialog();");

            //ModalPopupExtender1.Show();
        }
        protected void imgViewESN_Click(object sender, CommandEventArgs e)
        {
            //int duration = 90;
            Session["esns"] = null;
            lblMsg.Text = string.Empty;
            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;

            if (txtFromDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtFromDate.Text, out dt))
                    fromDate = dt;
                else
                    throw new Exception("Date From does not have correct format(MM/DD/YYYY)");
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtToDate.Text, out dt))
                    toDate = dt;
                else
                    throw new Exception("Date To does not have correct format(MM/DD/YYYY)");
            }

            //if(ddlDuration != null && ddlDuration.se
            //duration = Convert.ToInt32(ddlDuration.SelectedValue);
            Session["esns"] = null;
            Control tmp2 = LoadControl("~/controls/ViewESN.ascx");
            avii.Controls.ViewESN ctlViewESN = tmp2 as avii.Controls.ViewESN;
            pnlESN.Controls.Clear();
            int itemCompanyGUID = Convert.ToInt32(e.CommandArgument);
            //ViewState["poid"] = poID;
            if (tmp2 != null)
            {
                
                ctlViewESN.BindESN(0, itemCompanyGUID, fromDate, toDate);
            }
            pnlESN.Controls.Add(ctlViewESN);
            List<SkusEsnList> skuEsnList = (List<SkusEsnList>)Session["skusesnlist"];
            var skulist = (from item in skuEsnList where item.ItemCompanyGUID.Equals(itemCompanyGUID) select item).ToList();
            if (skulist != null && skulist.Count > 0)
            {
                //Session["sku"] = skulist[0].ProductCode;
                lblProduct.Text = skulist[0].SKU;
            }
            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");

            //ModalPopupExtender1.Show();
        }

        protected void imgViewProcessedESN_Click(object sender, CommandEventArgs e)
        {
            //int duration = 90;
            bool isESN = true;
            if (ddlOption.SelectedIndex > 0)
                isESN = false;

            Session["esns"] = null;
            lblMsg.Text = string.Empty;
            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;

            if (txtFromDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtFromDate.Text, out dt))
                    fromDate = dt;
                else
                    throw new Exception("Date From does not have correct format(MM/DD/YYYY)");
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtToDate.Text, out dt))
                    toDate = dt;
                else
                    throw new Exception("Date To does not have correct format(MM/DD/YYYY)");
            }

            //if(ddlDuration != null && ddlDuration.se
            //duration = Convert.ToInt32(ddlDuration.SelectedValue);
            //Session["esns"] = null;
            Control tmp2 = LoadControl("~/controls/ViewESN.ascx");
            avii.Controls.ViewESN ctlViewESN = tmp2 as avii.Controls.ViewESN;
            pnlProcessed.Controls.Clear();
            int itemCompanyGUID = Convert.ToInt32(e.CommandArgument);
            //ViewState["poid"] = poID;
            if (tmp2 != null)
            {

                ctlViewESN.BindSKUsESNsList(2, itemCompanyGUID, fromDate, toDate, isESN);
            }
            pnlProcessed.Controls.Add(ctlViewESN);
            List<SkusEsnList> skuEsnList = (List<SkusEsnList>)Session["skusesnlist"];
            var skulist = (from item in skuEsnList where item.ItemCompanyGUID.Equals(itemCompanyGUID) select item).ToList();
            if (skulist != null && skulist.Count > 0)
            {
                //Session["sku"] = skulist[0].ProductCode;
                lblSku2.Text = skulist[0].SKU;
            }
            RegisterStartupScript("jsUnblockDialog", "unblockDialogP();");

            //ModalPopupExtender1.Show();
        }
        protected void imgViewShippedESN_Click(object sender, CommandEventArgs e)
        {
            //int duration = 90;
            //Session["esns"] = null;
            lblMsg.Text = string.Empty;
            bool isESN = true;
            if (ddlOption.SelectedIndex > 0)
                isESN = false;

            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;

            if (txtFromDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtFromDate.Text, out dt))
                    fromDate = dt;
                else
                    throw new Exception("Date From does not have correct format(MM/DD/YYYY)");
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtToDate.Text, out dt))
                    toDate = dt;
                else
                    throw new Exception("Date To does not have correct format(MM/DD/YYYY)");
            }

            //if(ddlDuration != null && ddlDuration.se
            //duration = Convert.ToInt32(ddlDuration.SelectedValue);
            Session["esns"] = null;
            Control tmp2 = LoadControl("~/controls/ViewESN.ascx");
            avii.Controls.ViewESN ctlViewESN = tmp2 as avii.Controls.ViewESN;
            pnlShipped.Controls.Clear();
            int itemCompanyGUID = Convert.ToInt32(e.CommandArgument);
            //ViewState["poid"] = poID;
            if (tmp2 != null)
            {

                ctlViewESN.BindSKUsESNsList(3, itemCompanyGUID, fromDate, toDate, isESN);
            }
            pnlShipped.Controls.Add(ctlViewESN);
            List<SkusEsnList> skuEsnList = (List<SkusEsnList>)Session["skusesnlist"];
            var skulist = (from item in skuEsnList where item.ItemCompanyGUID.Equals(itemCompanyGUID) select item).ToList();
            if (skulist != null && skulist.Count > 0)
            {
                //Session["sku"] = skulist[0].ProductCode;
                lblSku3.Text = skulist[0].SKU;
            }
            RegisterStartupScript("jsUnblockDialog", "unblockDialogS();");

            //ModalPopupExtender1.Show();
        }
        protected void imgViewRMAESN_Click(object sender, CommandEventArgs e)
        {
            //int duration = 90;
            //Session["esns"] = null;
            lblMsg.Text = string.Empty;
            bool isESN = true;
            if (ddlOption.SelectedIndex > 0)
                isESN = false;

            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;

            if (txtFromDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtFromDate.Text, out dt))
                    fromDate = dt;
                else
                    throw new Exception("Date From does not have correct format(MM/DD/YYYY)");
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtToDate.Text, out dt))
                    toDate = dt;
                else
                    throw new Exception("Date To does not have correct format(MM/DD/YYYY)");
            }

            //if(ddlDuration != null && ddlDuration.se
            //duration = Convert.ToInt32(ddlDuration.SelectedValue);
            Session["esns"] = null;
            Control tmp2 = LoadControl("~/controls/ViewESN.ascx");
            avii.Controls.ViewESN ctlViewESN = tmp2 as avii.Controls.ViewESN;
            pnlRMA.Controls.Clear();
            int itemCompanyGUID = Convert.ToInt32(e.CommandArgument);
            //ViewState["poid"] = poID;
            if (tmp2 != null)
            {

                ctlViewESN.BindSkuRmaESNsList(3, itemCompanyGUID, fromDate, toDate, isESN);
            }
            pnlRMA.Controls.Add(ctlViewESN);
            List<SkusEsnList> skuEsnList = (List<SkusEsnList>)Session["skusesnlist"];
            var skulist = (from item in skuEsnList where item.ItemCompanyGUID.Equals(itemCompanyGUID) select item).ToList();
            if (skulist != null && skulist.Count > 0)
            {
                //Session["sku"] = skulist[0].ProductCode;
                lblSKU4.Text = skulist[0].SKU;
            }
            RegisterStartupScript("jsUnblockDialog", "unblockDialogR();");

            //ModalPopupExtender1.Show();
        }
        
        

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvESN.PageIndex = e.NewPageIndex;
            if (Session["skusesnlist"] != null)
            {
                List<SkusEsnList> skuEsnList = (List<SkusEsnList>)Session["skusesnlist"];

                gvESN.DataSource = skuEsnList;
                gvESN.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            if (dllDownload.SelectedIndex == 1)
                DownloadUnusedESNs();
            else
                if (dllDownload.SelectedIndex == 2)
                    DownloadUsedESNs();

        }
        private void DownloadUnusedESNs()
        {
            int companyID = 0;
            //int duration = 90;
            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;

            if (txtFromDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtFromDate.Text, out dt))
                    fromDate = dt;
                else
                    throw new Exception("Date From does not have correct format(MM/DD/YYYY)");
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtToDate.Text, out dt))
                    toDate = dt;
                else
                    throw new Exception("Date To does not have correct format(MM/DD/YYYY)");
            }

            string downLoadPath = string.Empty;
            downLoadPath = System.Configuration.ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();

            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {

                    companyID = userInfo.CompanyGUID;
                }
            }
            else
            {

                if (dpCompany.SelectedIndex > 0)
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                }
            }
            try
            {
                List<MslESN> esnList = null;
                if (ddlOption.SelectedIndex == 0)
                    esnList = ReportOperations.GetCustomerUnusedEsnList(companyID, fromDate, toDate, chkESN.Checked);
                else
                    esnList = ReportOperations.GetCustomerUnusedSIMList(companyID, fromDate, toDate, chkESN.Checked);

                if (esnList != null && esnList.Count > 0)
                {
                    string path = Server.MapPath(downLoadPath).ToString();
                    string fileName = "UnusedEsn" + Session.SessionID + ".csv";
                    bool found = false;
                    System.IO.FileInfo file = null;
                    file = new System.IO.FileInfo(path + fileName);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    if (ddlOption.SelectedIndex == 0)
                        sb.Append("SKU,ESN,UploadDate\r\n");
                    else
                        sb.Append("SKU,SIM,UploadDate\r\n");


                    foreach (avii.Classes.MslESN esnObj in esnList)
                    {
                        sb.Append(esnObj.SKU + ","
                                    + esnObj.ESN + ","
                                    + esnObj.UploadDate.ToShortDateString()
                                    + "\r\n");

                        found = true;
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
                else
                { lblMsg.Text = "No records found"; }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            //}
            //}
        }

        protected void btnDwnlUsedESN_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            DownloadUnusedESNs();
        }
        private void DownloadUsedESNs()
        {
            int companyID = 0;
            //int duration = 90;
            DateTime fromDate, toDate;
            fromDate = toDate = DateTime.MinValue;

            if (txtFromDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtFromDate.Text, out dt))
                    fromDate = dt;
                else
                    throw new Exception("Date From does not have correct format(MM/DD/YYYY)");
            }
            if (txtToDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtToDate.Text, out dt))
                    toDate = dt;
                else
                    throw new Exception("Date To does not have correct format(MM/DD/YYYY)");
            }

            string downLoadPath = string.Empty;
            downLoadPath = System.Configuration.ConfigurationManager.AppSettings["FullfillmentFilesStoreage"].ToString();

            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {

                    companyID = userInfo.CompanyGUID;
                }
            }
            else
            {

                if (dpCompany.SelectedIndex > 0)
                {
                    companyID = Convert.ToInt32(dpCompany.SelectedValue);
                }
            }
            try
            {
                List<MslESN> esnList = null;
                if(ddlOption.SelectedIndex==0)
                    esnList = ReportOperations.GetCustomerUsedEsnList(companyID, fromDate, toDate);
                else
                    esnList = ReportOperations.GetCustomerUsedSIMList(companyID, fromDate, toDate);

                if (esnList != null && esnList.Count > 0)
                {
                    string path = Server.MapPath(downLoadPath).ToString();
                    string fileName = "UsedEsn" + Session.SessionID + ".csv";
                    bool found = false;
                    System.IO.FileInfo file = null;
                    file = new System.IO.FileInfo(path + fileName);
                    if (file.Exists)
                    {
                        file.Delete();
                    }

                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    if (ddlOption.SelectedIndex == 0)
                        sb.Append("SKU,ESN,UploadDate\r\n");
                    else
                        sb.Append("SKU,SIM,UploadDate\r\n");

                    foreach (avii.Classes.MslESN esnObj in esnList)
                    {
                        sb.Append(esnObj.SKU + ","
                                    + esnObj.ESN + ","
                                    + esnObj.UploadDate.ToShortDateString()
                                    + "\r\n");

                        found = true;
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
                else
                { lblMsg.Text = "No records found"; }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
            //  }
            //}
        }
    }
}