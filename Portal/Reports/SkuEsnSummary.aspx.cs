using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Reports
{
    public partial class SkuEsnSummary : System.Web.UI.Page
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
                BindSkuESNSummary();
        }


        private void BindSkuESNSummary()
        {
            int companyID = 0;
            if (Session["adm"] == null)
            {
                avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                if (userInfo != null)
                {
                    
                    companyID = userInfo.CompanyGUID;
                    
                }
            }
            List<avii.Classes.SkuEsnList> skuESNList = avii.Classes.ReportOperations.GetSkuEsnSummary(companyID);
            if (skuESNList != null && skuESNList.Count > 0)
            {
                gvESN.DataSource = skuESNList;
                gvESN.DataBind();
                lblCount.Text = "Total count: " + skuESNList.Count;
                Session["skuesnlist"] = skuESNList;
            }
            else
            {
                lblCount.Text = string.Empty;
                lblMsg.Text ="No record found";
                gvESN.DataSource = null;
                gvESN.DataBind();
            }
        }
        protected void gvESN_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
                return;

            LinkButton lnkESN = e.Row.FindControl("lnkESN") as LinkButton;
            if (lnkESN != null)
                lnkESN.OnClientClick = "openDialogAndBlock('Unused ESN', '" + lnkESN.ClientID + "')";

            LinkButton lnkCode = e.Row.FindControl("lnkCode") as LinkButton;
            if (lnkCode != null)
                lnkCode.OnClientClick = "openProductDialogAndBlock('Product Detail', '" + lnkCode.ClientID + "')";
            
        }
        private void RegisterStartupScript(string key, string script)
        {
            ScriptManager.RegisterStartupScript(phrJsRunner, phrJsRunner.GetType(), key, script, true);
        }

        protected void lnkViewProductInfo_Click(object sender, CommandEventArgs e)
        {

            Control tmp2 = LoadControl("~/controls/ProductsDetail.ascx");
            avii.Controls.ProductsDetail ctlViewProduct = tmp2 as avii.Controls.ProductsDetail;
            pnlESN.Controls.Clear();
            int itemGUID = Convert.ToInt32(e.CommandArgument);
            //ViewState["poid"] = poID;
            if (tmp2 != null)
            {

                ctlViewProduct.GetItemDetails(itemGUID);
            }
            pnlProduct.Controls.Add(ctlViewProduct);

            Control tmp = LoadControl("~/controls/ProductSKUs.ascx");
            avii.Controls.ProductSKUs ctlProductSKU = tmp as avii.Controls.ProductSKUs;

            tabPanel.Controls.Clear();

            if (tmp != null)
            {

                ctlProductSKU.BindItemSKU(itemGUID);
            }
            tabPanel.Controls.Add(ctlProductSKU);


            RegisterStartupScript("jsUnblockDialog", "unblockProductDialog();");

            //ModalPopupExtender1.Show();
        }
        protected void imgViewESN_Click(object sender, CommandEventArgs e)
        {
            DateTime dt = DateTime.MinValue;
            Control tmp2 = LoadControl("~/controls/ViewESN.ascx");
            avii.Controls.ViewESN ctlViewESN = tmp2 as avii.Controls.ViewESN;
            pnlESN.Controls.Clear();
            int itemGUID = Convert.ToInt32(e.CommandArgument);
            //ViewState["poid"] = poID;
            if (tmp2 != null)
            {

                ctlViewESN.BindESN(itemGUID, 0, dt, dt);
            }
            pnlESN.Controls.Add(ctlViewESN);
            List<SkuEsnList> skuEsnList = (List<SkuEsnList>)Session["skuesnlist"];
            var skulist = (from item in skuEsnList where item.ItemGUID.Equals(itemGUID) select item).ToList();
            if (skulist != null && skulist.Count > 0)
            {
                //Session["sku"] = skulist[0].ProductCode;
                lblProduct.Text = skulist[0].ProductCode;
            }
            RegisterStartupScript("jsUnblockDialog", "unblockDialog();");

            //ModalPopupExtender1.Show();
        }
        
        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvESN.PageIndex = e.NewPageIndex;
            if (Session["skuesnlist"] != null)
            {
                List<SkuEsnList> skuEsnList = (List<SkuEsnList>)Session["skuesnlist"];

                gvESN.DataSource = skuEsnList;
                gvESN.DataBind();
            }
            else
                lblMsg.Text = "Session expire!";

        }

        
    }
}