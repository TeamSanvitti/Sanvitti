using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Configuration;

namespace avii.Admin
{
    public partial class SimQuery : System.Web.UI.Page
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
                //trSKU.Visible = false;
                ddlSKU.Visible = false;
                lblSKU.Visible = false;
            }
        }

        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void dpCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int companyID = 0;

            gvSIM.DataSource = null;
            gvSIM.DataBind();
            //rptESN.Visible = false;
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;

            //trSKU.Visible = true;
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            if (companyID > 0)
                BindCompanySKU(companyID);
            else
            {
                //trSKU.Visible = false;
                ddlSKU.Visible = false;
                lblSKU.Visible = false;
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
            }
        }
        private void BindCompanySKU(int companyID)
        {
            lblMsg.Text = string.Empty;
            List<CompanySKUno> skuList = MslOperation.GetCompanySKUList(companyID, 1);
            if (skuList != null && skuList.Count > 0)
            {
                ddlSKU.DataSource = skuList;
                ddlSKU.DataValueField = "ItemcompanyGUID";
                ddlSKU.DataTextField = "SKU";
                ddlSKU.Visible = true;
                lblSKU.Visible = true;
                ddlSKU.DataBind();

                ListItem item = new ListItem("", "");
                ddlSKU.Items.Insert(0, item);
            }
            else
            {
                ddlSKU.DataSource = null;
                ddlSKU.DataBind();
                lblMsg.Text = "No SIM SKU/Product are assigned to select Customer";
                
                ddlSKU.Visible = false;
                lblSKU.Visible = false;
                

            }


        }

        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        
        protected void lnkRMA_OnCommand(object sender, CommandEventArgs e)
        {
            
                int rmaGUID = Convert.ToInt32(e.CommandArgument);
                string companyAccountNumber = string.Empty; //dpCompany.SelectedValue;
                if (rmaGUID>0)
                {

                    Control tmp1 = LoadControl("../controls/RMADetails.ascx");
                    avii.Controls.RMADetails ctlRMADetails = tmp1 as avii.Controls.RMADetails;
                    pnlRMA.Controls.Clear();
                    ctlRMADetails.BindRMA(rmaGUID, false);

                    pnlRMA.Controls.Add(ctlRMADetails);

                    RegisterStartupScript("jsUnblockDialog", "unblockdivRMADialog();");



                }
            
        }
        protected void lnkPoNum_OnCommand(object sender, CommandEventArgs e)
        {
            //if (dpCompany.SelectedIndex > 0)
            {
                string poNum = e.CommandArgument.ToString();
                string companyAccountNumber = string.Empty; //dpCompany.SelectedValue;
                if (!string.IsNullOrEmpty(poNum))
                {

                    Control tmp1 = LoadControl("../controls/PODetails.ascx");
                    avii.Controls.PODetails ctlPODetails = tmp1 as avii.Controls.PODetails;
                    pnlPO.Controls.Clear();
                    ctlPODetails.BindPO(poNum, companyAccountNumber);

                    pnlPO.Controls.Add(ctlPODetails);

                    RegisterStartupScript("jsUnblockDialog", "unblockDialog();");



                }
            }
            //else
                //lblMsg.Text = "Select a customer!";
        }
        protected void imgViewSimLog_Click(object sender, CommandEventArgs e)
        {

            
            string sim = e.CommandArgument.ToString();
            
            if (!string.IsNullOrEmpty(sim))
            {

                Control tmp1 = LoadControl("../controls/SimLogControl.ascx");
                avii.Controls.SimLogControl ctlSim = tmp1 as avii.Controls.SimLogControl;
                pnlSim.Controls.Clear();
                ctlSim.BindSimlog(sim);

                pnlSim.Controls.Add(ctlSim);

                RegisterStartupScript("jsUnblockDialog", "unblockdivSimLogDialog();");


            }
            
        }
        protected void search_click(object sender, EventArgs e)
        {
            BindSim();
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            txtSIM.Text = string.Empty;
            txtESN.Text = string.Empty;
            dpCompany.SelectedIndex = 0;

            ddlSKU.DataSource = null;
            ddlSKU.DataBind();
            gvSIM.DataSource = null;
            gvSIM.DataBind();
            ddlSKU.Visible = false;
            lblSKU.Visible = false;
        }
        
        private void BindSim()
        {
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            int companyID, itemCompanyGUID;
            string sim, esn;
            sim = esn = string.Empty;
            companyID = itemCompanyGUID = 0;
            sim = txtSIM.Text.Trim();
            esn = txtESN.Text.Trim();

            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                if (ddlSKU.SelectedIndex > 0)
                    itemCompanyGUID = Convert.ToInt32(ddlSKU.SelectedValue);
            }
            if (companyID == 0 && itemCompanyGUID == 0 && string.IsNullOrEmpty(sim) && string.IsNullOrEmpty(esn))
            {
                lblMsg.Text = "Please select the search criteria";
            }
            else
            {
                List<SimInfo> simList = SimOperations.GetSimList(companyID, itemCompanyGUID, sim, esn);
                if (simList != null && simList.Count > 0)
                {
                    gvSIM.DataSource = simList;
                    gvSIM.DataBind();
                    lblCount.Text = "SIM count: " + simList.Count;
                }
                else
                {
                    gvSIM.DataSource = null;
                    gvSIM.DataBind();
                    lblMsg.Text = "No records found.";
                }
            }

        }
        protected void gvSIM_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;

            ImageButton imgSim = e.Row.FindControl("imgSim") as ImageButton;
            imgSim.OnClientClick = "opendivSimLogDialogAndBlock('View SIM Log', '" + imgSim.ClientID + "')";
            LinkButton lnkPoNum = (LinkButton)e.Row.FindControl("lnkPoNum");
            lnkPoNum.OnClientClick = "openDialogAndBlock('Fulfillment Detail', '" + lnkPoNum.ClientID + "')";

            LinkButton lnkRMA = (LinkButton)e.Row.FindControl("lnkRMA");
            lnkRMA.OnClientClick = "opendivRMADialogAndBlock('RMA Detail', '" + lnkRMA.ClientID + "')";

            

            
            

        }
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            //gvRma.PageIndex = e.NewPageIndex;

            //if (Session["result"] != null)
            //{
            //    List<avii.Classes.RMA> objRmaList = (List<avii.Classes.RMA>)Session["result"];
            //    gvRma.DataSource = objRmaList;
            //    gvRma.DataBind();

            //    // avii.Classes.PurchaseOrders pos = (avii.Classes.PurchaseOrders)Session["POS"];
            //    // BindGrid1(pos);
            //}
        }


    }
}