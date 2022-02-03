using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Fulfillment;
using SV.Framework.Models.Fulfillment;
namespace avii
{
    public partial class UnprovisionPoSearch : System.Web.UI.Page
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
                BindStatus();
                if (Session["adm"] != null)
                {
                    BindCustomer();

                }
                else
                    trCustomer.Visible = false;
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
        }
        private void BindStatus()
        {
            UnprovisioningOperation unprovisioningOperation = UnprovisioningOperation.CreateInstance<UnprovisioningOperation>();

            List<UnprovisionStatus> statusList = unprovisioningOperation.GetUnprovisioingStatus();
            if (statusList != null && statusList.Count > 0)
            {
                ddlStatus.DataSource = statusList;
                ddlStatus.DataValueField = "StatusID";
                ddlStatus.DataTextField = "StatusText";
                ddlStatus.DataBind();

                System.Web.UI.WebControls.ListItem newList = new System.Web.UI.WebControls.ListItem("", "0");
                ddlStatus.Items.Insert(0, newList);
            }
            else
            {
                ddlStatus.DataSource = null;
                ddlStatus.DataBind();
               // lblMsg.Text = "No users are assigned to selected Customer";
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RequestSearch();
        }
        private void RequestSearch()
        {
            UnprovisioningOperation unprovisioningOperation = UnprovisioningOperation.CreateInstance<UnprovisioningOperation>();

            lblMsg.Text = "";
            lblCount.Text = "";
            gvUnprovision.DataSource = null;
            gvUnprovision.DataBind();
            //btnDownload.Visible = false;
            Session["unpoList"] = null;
            string dateFrom = "", dateTo = "";
            int companyID = 0, statusID = 0;
            string fulfillmentNumber = txtPO.Text.Trim();
            if (Session["adm"] != null)
            {
                if (dpCompany.SelectedIndex > 0)
                {
                    int.TryParse(dpCompany.SelectedValue, out companyID);
                }
                else
                {
                    lblMsg.Text = "Customer is required!";
                    return;
                }
            }
            else
            {
                avii.Classes.UserInfo userInfo = (avii.Classes.UserInfo)Session["userInfo"];
                if (userInfo != null)
                    companyID = userInfo.CompanyGUID;
            }

            if (ddlStatus.SelectedIndex > 0)
                int.TryParse(ddlStatus.SelectedValue, out statusID);

            dateFrom = txtDateFrom.Text.Trim();
            dateTo = txtDateTo.Text.Trim();
            List<UnprovisioingInfo> poList = unprovisioningOperation.GetUnprovisioingRequestSearch(companyID, fulfillmentNumber, statusID, dateFrom, dateTo);
            if (poList != null && poList.Count > 0)
            {
                Session["unpoList"] = poList;

                lblCount.Text = "<strong>Total Count:</strong> " + poList.Count;
                gvUnprovision.DataSource = poList;
                gvUnprovision.DataBind();

                if (Session["adm"] == null)
                {
                    gvUnprovision.Columns[9].Visible = false;
                }


                // btnDownload.Visible = true;

            }
            else
                lblMsg.Text = "No record found";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            lblCount.Text = "";
            gvUnprovision.DataSource = null;
            gvUnprovision.DataBind();
            //btnDownload.Visible = false;
            Session["unpoList"] = null;
            txtPO.Text = "";
            txtDateFrom.Text = "";
            txtDateTo.Text = "";
            if (Session["adm"] != null)
            {
                dpCompany.SelectedIndex = 0;
            }
            ddlStatus.SelectedIndex = 0;

        }

        protected void lnkReject_Command(object sender, CommandEventArgs e)
        {
            int UnprovisioningID = Convert.ToInt32(e.CommandArgument);
            string status = "Rejected";
            UpdateUnprovisionStatus(UnprovisioningID, status);
            //RequestSearch();
        }
        private void UpdateUnprovisionStatus(int UnprovisioningID, string status)
        {
            UnprovisioningOperation unprovisioningOperation = UnprovisioningOperation.CreateInstance<UnprovisioningOperation>();

            int userID = Convert.ToInt32(Session["UserID"]);
            //string status = "Rejected";
            UnprovisionPORequest request = new UnprovisionPORequest();
            request.UnprovisioningID = UnprovisioningID;
            request.ApprovedBy = userID;
            request.Status = status;

            string returnMessage = unprovisioningOperation.UnprovisioingRequestInsert(request);
            if (string.IsNullOrEmpty(returnMessage))
            {
                RequestSearch();
                lblMsg.Text = status + " successfully!";
            } 
            else
            {
                lblMsg.Text = returnMessage;
            }
        }

        protected void lnkApproved_Command(object sender, CommandEventArgs e)
        {
            int UnprovisioningID = Convert.ToInt32(e.CommandArgument);
            string status = "Approved";
            UpdateUnprovisionStatus(UnprovisioningID, status);
            
        }

        protected void lnkCancel_Command(object sender, CommandEventArgs e)
        {
            int UnprovisioningID = Convert.ToInt32(e.CommandArgument);
            string status = "Cancelled";
            UpdateUnprovisionStatus(UnprovisioningID, status);
            //RequestSearch();
        }

        protected void gvUnprovision_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {                
                HiddenField hdStatus = e.Row.FindControl("hdStatus") as HiddenField;
                LinkButton lnkReject = e.Row.FindControl("lnkReject") as LinkButton;
                LinkButton lnkCancel = e.Row.FindControl("lnkCancel") as LinkButton;
               // LinkButton lnkApproved = e.Row.FindControl("lnkApproved") as LinkButton;
                if (Session["adm"] != null)
                {
                    if (lnkCancel != null)
                    {
                        lnkCancel.Visible = false;
                    }
                }
                else
                {
                    if(hdStatus.Value.ToLower()=="received") //Received
                    {
                        lnkCancel.Visible = true;
                    }
                    if (lnkReject != null)
                    {
                        lnkReject.Visible = false;
                    }
                }
            }
        }
    }
}