using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;

namespace avii.EsnReplacement
{
    public partial class EsnReplacement : System.Web.UI.Page
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
                BindCustomer();
            }
        }
        protected void BindCustomer()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();
        }
        protected void btnValidate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            SV.Framework.Inventory.EsnReplacementOperation esnReplacementOperation = SV.Framework.Inventory.EsnReplacementOperation.CreateInstance<SV.Framework.Inventory.EsnReplacementOperation>();

            string assignedESN = lblESN.Text;
            string ESN = txtReplacedESN.Text;
            if (!string.IsNullOrEmpty(ESN))
            {
                List<ESNReplacedInfo> esnReplacedInfo = esnReplacementOperation.ESNReplacementValidate(assignedESN, ESN);
                if (esnReplacedInfo != null && esnReplacedInfo .Count > 0 && string.IsNullOrEmpty(esnReplacedInfo[0].ErrorMessage))
                {
                    btnValidate.Visible = false;
                    btnSubmit.Visible = true;
                    Table5.Visible = true;
                    rptESN.DataSource = esnReplacedInfo;
                    rptESN.DataBind();
                }
                else
                    lblMsg.Text = esnReplacedInfo[0].ErrorMessage;
            }
            else
                lblMsg.Text = "Replaced ESN is required!";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //btnValidate.Visible = false;
            //btnSubmit.Visible = true;
            //btnCancel.Visible = true;
            SV.Framework.Inventory.EsnReplacementOperation esnReplacementOperation = SV.Framework.Inventory.EsnReplacementOperation.CreateInstance<SV.Framework.Inventory.EsnReplacementOperation>();

            string errorMessage = "";
            string assignedESN = lblESN.Text;
            string FulfillmentNumber = txtPO.Text;
            string ESN = txtReplacedESN.Text;
            string comment = txtComment.Text.Trim();
            int userID = 0, approvedBy = 0;
            userID = Convert.ToInt32(Session["UserID"]);

            if (!string.IsNullOrEmpty(ESN))
            {
                if (ddlUser.SelectedIndex > 0)
                {
                    approvedBy = Convert.ToInt32(ddlUser.SelectedValue);
                    int returnValue = esnReplacementOperation.ESNReplacementUpdate(FulfillmentNumber, assignedESN, ESN, comment, approvedBy, userID, out errorMessage);
                    if (returnValue > 0)
                    {
                        btnSubmit.Enabled = false;

                        lblMsg.Text = "Replaced successfully and affected esn sent back to stock.";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(errorMessage))
                            lblMsg.Text = errorMessage;
                        else
                            lblMsg.Text = "Could not replaced";
                    }
                }
                else
                {

                }
            }
            else
                lblMsg.Text = "Replaced ESN is required!";

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            btnSubmit.Enabled = false;
            tblPO.Visible = false;
            btnValidate.Visible = false;
            btnSubmit.Visible = false;
            btnCancel.Visible = false;

            ddlUser.Items.Clear();
            txtReplacedESN.Text = "";
            txtComment.Text = "";
            txtESN.Text = "";
            txtPO.Text = "";
            dpCompany.SelectedIndex = 0;
            rptESN.DataSource = null;
            rptESN.DataBind();



        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string fulfillmentNumber = "", ESN = "", status = "";
            int companyID = 0;
            lblMsg.Text = "";
            tblPO.Visible = false;
            btnValidate.Visible = false;
            btnSubmit.Visible = false;
            SV.Framework.Inventory.EsnReplacementOperation esnReplacementOperation = SV.Framework.Inventory.EsnReplacementOperation.CreateInstance<SV.Framework.Inventory.EsnReplacementOperation>();

            //btnCancel.Visible = false;
            ddlUser.Items.Clear();
            txtReplacedESN.Text = "";
            txtComment.Text = "";
            ddlUser.Items.Clear();
            rptESN.DataSource = null;
            rptESN.DataBind();
            fulfillmentNumber = txtPO.Text.Trim();
            ESN = txtESN.Text.Trim();

            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);

                if (!string.IsNullOrEmpty(fulfillmentNumber))
                {
                    if (!string.IsNullOrEmpty(ESN))
                    {
                        FulfillemntInfo fulfillemntInfo = esnReplacementOperation.GetFulfillmentInfo(fulfillmentNumber, companyID, ESN, out status);
                        if(fulfillemntInfo != null && !string.IsNullOrEmpty(fulfillemntInfo.PoStatus))
                        {

                            rptLineItems.DataSource = fulfillemntInfo.LineItems;
                            rptLineItems.DataBind();

                            lblESN.Text = fulfillemntInfo.ESN;

                            lblContactName.Text = fulfillemntInfo.ContactName;
                            lblPODate.Text = fulfillemntInfo.FulfillemntDate;
                            lblFulfillmentNo.Text = fulfillemntInfo.FulfillemntNumber;
                            lblStatus.Text = fulfillemntInfo.PoStatus;
                            if(!string.IsNullOrEmpty(fulfillemntInfo.ESN))
                            {

                                BindUser();
                                tblPO.Visible = true;
                                btnValidate.Visible = true;
                                btnCancel.Visible = true;
                            }
                            else
                            {
                                lblMsg.Text = "ESN not found!";
                            }
                            
                        }
                    }
                    else
                    {
                        lblMsg.Text = "ESN required!";
                    }
                }
                else
                {
                    lblMsg.Text = "Fulfillment number required!";
                }
            }
            else
            {
                lblMsg.Text = "Customer required!";
            }

            ScriptManager.RegisterStartupScript(this, this.GetType(), "stop loader", "StopProgress();", true);

        }

        private void BindUser()
        {
            int companyID = 0;
            string sortExpression = "UserName";
            string sortDirection = "ASC";
            string sortBy = sortExpression + " " + sortDirection;
            int statusID = 2;
            
            if (dpCompany.SelectedIndex > 0)
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            List<avii.Classes.UserRegistration> userList = avii.Classes.RegistrationOperation.GetUserInfoList(companyID, string.Empty, sortBy, statusID);
            if (userList != null && userList.Count > 0)
            {
                ddlUser.DataSource = userList;
                ddlUser.DataTextField = "UserName";
                ddlUser.DataValueField = "UserID";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

            }
            else
            {
                lblMsg.Text = "No approved by found";
            }

        }

    }
}