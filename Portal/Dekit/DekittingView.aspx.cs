//using avii.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.SOR;
using SV.Framework.Models.Common;
using SV.Framework.SOR;

namespace avii.Dekit
{
    public partial class DekittingView : System.Web.UI.Page
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
                //btnSubmit.Visible = false;
                //btnCancel.Visible = false;
                //pnlSKU.Visible = false;
                if(Session["dekittingID"] != null)
                {
                    Int64 dekittingID = Convert.ToInt64(Session["dekittingID"]);
                    ViewState["dekittingID"] = dekittingID;
                    GetDekittingDetail(dekittingID);
                    Session["dekittingID"] = null;
                }
            }

        }

        private void GetDekittingDetail(Int64 dekittingID)
        {
            DekitOperations dekitOperations = DekitOperations.CreateInstance<DekitOperations>();

            DekittingDetail dekittingDetail = dekitOperations.GetDekittingDetail(dekittingID);
            if(dekittingDetail != null && dekittingDetail.DeKittingID > 0)
            {
                if("Received" != dekittingDetail.DeKitStatus)
                {
                    btnReject.Visible = false;
                    btnSubmit.Visible = false;
                }
                lblCustomerRequestNo.Text = dekittingDetail.CustomerRequestNumber;
                lblCreatedBy.Text = dekittingDetail.CreatedBy;
                lblCustomer.Text = dekittingDetail.CustomerName;
                lblDekitRequestNo.Text = dekittingDetail.DekitRequestNumber;
                lblSKU.Text = dekittingDetail.SKU;
                lblStatus.Text = dekittingDetail.DeKitStatus;
                lblRequestedBy.Text = dekittingDetail.ApprovedBy;
                lblQty.Text = dekittingDetail.Quantity.ToString();


                if (dekittingDetail.RawSKUs != null && dekittingDetail.RawSKUs.Count > 0)
                {
                    rptSKUs.DataSource = dekittingDetail.RawSKUs;
                    rptSKUs.DataBind();
                }
                if (dekittingDetail.EsnList != null && dekittingDetail.EsnList.Count > 0)
                {
                    gvEsn.DataSource = dekittingDetail.EsnList;
                    gvEsn.DataBind();
                }
            }
        }
        //private void BindSORStatus()
        //{
        //    List<ServiceRequestStatus> statusList = DekitOperations.GetDeKitStatusList();
        //    ddlStatus.DataSource = statusList;
        //    ddlStatus.DataValueField = "StatusID";
        //    ddlStatus.DataTextField = "Status";
        //    ddlStatus.DataBind();
        //}
        
        private void BindRawSKUs(int companyID, int itemCompanyGUID)
        {
            pnlSKU.Visible = true;
            Session["RawSKUs"] = null;
            lblMsg.Text = string.Empty;
            SV.Framework.Catalog.FinishSKUOperations FinishSKUOperations = SV.Framework.Catalog.FinishSKUOperations.CreateInstance<SV.Framework.Catalog.FinishSKUOperations>();

            if (companyID > 0)
            {
                if (itemCompanyGUID > 0)
                {
                    {
           
                        List<SV.Framework.Models.Catalog.RawSKU> rawSKUList = FinishSKUOperations.GetKittedAssignedRawSKUs(companyID, itemCompanyGUID);
                        if (rawSKUList != null && rawSKUList.Count > 0)
                        {
                            rptSKUs.DataSource = rawSKUList;
                            rptSKUs.DataBind();
                            Session["RawSKUs"] = rawSKUList;

                        }
                        else
                        {
                            rptSKUs.DataSource = null;
                            rptSKUs.DataBind();
                            lblMsg.Text = "No SKU are assigned!";

                        }
                    }
                    
                }
                else
                {
                    lblMsg.Text = "SKU is required!";
                  
                }
            }
            
        }       

        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SV.Framework.SOR.DekitOperations dekitOperations = SV.Framework.SOR.DekitOperations.CreateInstance<SV.Framework.SOR.DekitOperations>();

            Int64 dekittingID = Convert.ToInt64(ViewState["dekittingID"]);
            int userID = 0;
           avii.Classes. UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
                userID = userInfo.UserGUID;
            // ViewState["dekittingID"] = dekittingID;

            int returnResult = dekitOperations.DeKittingStatusUpdate(dekittingID, userID, "Approved");
            if(returnResult > 0)
            {
                lblMsg.Text = "Approved successfully";
                btnCancel.Text = "Back To Search";
            }
        }
        
        
        protected void btnCancel_Click(object sender, EventArgs e)
        {

            Response.Redirect("~/DeKit/DekittingRequestSearch.aspx");
            //ClearForm();
        }


        protected void btnReject_Click(object sender, EventArgs e)
        {
            SV.Framework.SOR.DekitOperations dekitOperations = SV.Framework.SOR.DekitOperations.CreateInstance<SV.Framework.SOR.DekitOperations>();

            Int64 dekittingID = Convert.ToInt64(ViewState["dekittingID"]);
            // ViewState["dekittingID"] = dekittingID;
            int userID = 0;
            avii.Classes. UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
                userID = userInfo.UserGUID;

            int returnResult = dekitOperations.DeKittingStatusUpdate(dekittingID, userID, "Rejected");
            if (returnResult > 0)
            {
                lblMsg.Text = "Rejected successfully";
                btnCancel.Text = "Back To Search";
            }
        }
    }
}