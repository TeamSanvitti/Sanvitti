using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.RMA;


namespace avii.RMA
{
    public partial class ManageRmaReceive : System.Web.UI.Page
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
                ReadOlnyAccess();
                BindCompany();
                GetReceiveStatuses();

                //if (Session["adm"] == null)
                //{
                //    avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                //    dpCompany.SelectedValue = userInfo.CompanyGUID.ToString();                    
                //    dpCompany.Enabled = false;
                //}

            }
        }

        private void ReadOlnyAccess()
        {
            //string url = HttpContext.Current.Request.Url.AbsoluteUri;
            // http://localhost:1302/TESTERS/Default6.aspx

            string path = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            // /TESTERS/Default6.aspx
            if (path != null)
            {
                path = path.ToLower();
                List<avii.Classes.MenuItem> menuItems = Session["MenuItems"] as List<avii.Classes.MenuItem>;
                foreach (avii.Classes.MenuItem item in menuItems)
                {
                    if (item.Url.ToLower().Contains(path) && item.IsReadOnly)
                    {
                        ViewState["IsReadOnly"] = true;
                        btnSubmit.Visible = false;
                    }
                }

            }

        }
        private void BindCompany()
        {
            //avii.Classes.RMAUtility rmaObj = new avii.Classes.RMAUtility();
            try
            {
                dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
                dpCompany.DataValueField = "companyID";
                dpCompany.DataTextField = "companyName";
                dpCompany.DataBind();
                //ListItem item = new ListItem("", "0");
                //ddlCompany.Items.Insert(0, item);
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        private void BindTracking()
        {
            
            if (Session["rmaTrackings"] != null)
            {
                List<RmaReceiveTracking> trackings = Session["rmaTrackings"] as List<RmaReceiveTracking>;
                if (trackings != null && trackings.Count > 0)
                {
                    ddlRmaTracking.DataSource = trackings;
                    ddlRmaTracking.DataValueField = "TrackingNumber";
                    ddlRmaTracking.DataTextField = "TrackingNumber";
                    ddlRmaTracking.DataBind();
                    ddlRmaTracking.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", ""));

                    if (trackings.Count == 1)
                    {
                        ddlRmaTracking.SelectedIndex = 1;
                    }
                    else
                    {

                    }
                    
                }
            }
        }
        private void GetReceiveStatuses()
        {
            try
            {
                SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

                List<RmaReceiveStatus> statusList = rmaOperation.GetReceiveRMAStatusList();
                
                if (statusList != null && statusList.Count > 0)
                {
                    Session["receivestatusList"] = statusList;

                    ddlReceiveStatus.DataSource = statusList;
                    ddlReceiveStatus.DataValueField = "ReceiveStatusID";
                    ddlReceiveStatus.DataTextField = "ReceiveStatus";
                    ddlReceiveStatus.DataBind();
                    ddlReceiveStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        private void BindReceiveStatuses(DropDownList ddlStatus)
        {
            try
            {
                SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

                List<RmaReceiveStatus> statusList = new List<RmaReceiveStatus>();// avii.Classes.CompanyOperations.GetReceiveRMAStatusList();
                if(Session["receivestatusList"] !=null)
                {
                    statusList = Session["receivestatusList"] as List<RmaReceiveStatus>;
                }
                else
                {
                    statusList = rmaOperation.GetReceiveRMAStatusList();
                }
                if (statusList != null && statusList.Count > 0)
                {
                    Session["receivestatusList"] = statusList;
                    ddlStatus.DataSource = statusList;
                    ddlStatus.DataValueField = "ReceiveStatusID";
                    ddlStatus.DataTextField = "ReceiveStatus";
                    ddlStatus.DataBind();
                    ddlStatus.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ReceiveSearch();

            BindTracking();
        }
        private void ReceiveSearch()
        {
            SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

            trRMA.Visible = false;
            trReceived.Visible = false;
            btnSubmit.Visible = false;
            lblMsg.Text = string.Empty;
            txtComment.Text = string.Empty;
            // RmaGUID = 10;
            int companyID = 0;
            string rmaNumber, trackingNumber;
            rmaNumber = trackingNumber = string.Empty;
            if (dpCompany.SelectedIndex > 0)
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
                rmaNumber = txtRMA.Text.Trim();
                trackingNumber = txtTrackingNo.Text.Trim();
                if (string.IsNullOrEmpty(rmaNumber) && string.IsNullOrEmpty(trackingNumber))
                {
                    lblMsg.Text = "Please select second search criteria!";
                }
                else
                {
                    RmaReceive rmaReceive = rmaOperation.GetRmaReceiveSearch(companyID, rmaNumber, trackingNumber);
                    if (rmaReceive != null && rmaReceive.ReceiveList != null && rmaReceive.ReceiveList.Count > 0)
                    {

                        //if (rmaReceive.TrackingList != null && rmaReceive.TrackingList.Count > 0)
                        {
                            Session["rmaTrackings"] = rmaReceive.TrackingList;
                            lblRMA.Text = rmaReceive.RmaNumber;
                            lblCustomerRMA.Text = rmaReceive.CustomerRmaNumber;
                            lblRmaDate.Text = rmaReceive.RmaDate;
                            lblRmaStatus.Text = rmaReceive.RmaStatus;
                            btnSubmit.Visible = true;
                            trRMA.Visible = true;

                        }
                        //else
                        //{

                        //    lblMsg.Text = "Label not generated yet!";
                        //    btnSubmit.Visible = false;
                        //}
                        BindUser();
                        ViewState["rmaGUID"] = rmaReceive.ReceiveList[0].RmaGUID;
                        rptRma.DataSource = rmaReceive.ReceiveList;
                        rptRma.DataBind();
                        if (rmaReceive.ReceivedList != null && rmaReceive.ReceivedList.Count > 0)
                        {
                            rptReceived.DataSource = rmaReceive.ReceivedList;
                            rptReceived.DataBind();
                            trReceived.Visible = true;

                        }
                        else
                        {
                            rptReceived.DataSource = null;
                            rptReceived.DataBind();
                            trReceived.Visible = false;

                        }
                        // List<RmaReceiveTracking> rmaTrackings = 

                    }
                    else
                    {
                        lblMsg.Text = "No record found";
                        trRMA.Visible = false;

                    }
                }
            }
            else
            {
                lblMsg.Text = "Customer is required!";
            }

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
            if(userList != null && userList.Count > 0)
            {
                ddlUser.DataSource = userList;
                ddlUser.DataTextField = "UserName";
                ddlUser.DataValueField = "UserID";
                ddlUser.DataBind();
                ddlUser.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));


                ddlReceivedBy.DataSource = userList;
                ddlReceivedBy.DataTextField = "UserName";
                ddlReceivedBy.DataValueField = "UserID";
                ddlReceivedBy.DataBind();
                ddlReceivedBy.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", "0"));

                
                //avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
                //if (userInfo != null)
                //    ddlUser.SelectedValue = userInfo.UserGUID.ToString();

            }
            else
            {
                lblMsg.Text = "No approved by found";
            }
            
        }

        protected void rptRma_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
                ((CheckBox)e.Item.FindControl("allchk")).Attributes.Add("onclick",
                    "javascript:SelectAll('" +
                    ((CheckBox)e.Item.FindControl("allchk")).ClientID + "')");
            }

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DropDownList ddlTracking = (DropDownList)e.Item.FindControl("ddlTracking");
                HiddenField hdnTracking = (HiddenField)e.Item.FindControl("hdnTracking");
                DropDownList ddlStatus = (DropDownList)e.Item.FindControl("ddlStatus");
                HiddenField hdnReceiveStatusID = (HiddenField)e.Item.FindControl("hdnStatusID");
                BindReceiveStatuses(ddlStatus);
                ddlStatus.SelectedValue = hdnReceiveStatusID.Value;


                if (Session["rmaTrackings"] != null)
                {
                    List<RmaReceiveTracking> trackings = Session["rmaTrackings"] as List<RmaReceiveTracking>;
                    if(trackings != null && trackings.Count > 0)
                    {
                        ddlTracking.DataSource = trackings;
                        ddlTracking.DataValueField = "TrackingNumber";
                        ddlTracking.DataTextField = "TrackingNumber";
                        ddlTracking.DataBind();
                        ddlTracking.Items.Insert(0, new System.Web.UI.WebControls.ListItem("", ""));

                        if (trackings.Count==1)
                        {
                            ddlTracking.SelectedIndex = 1;
                        }  
                        else
                        {
                            
                        }
                        if (!string.IsNullOrEmpty(hdnTracking.Value))
                            ddlTracking.SelectedValue = hdnTracking.Value;
                    }
                }
            }
        }
        
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

            int rmaReceiveGUID = 0, rmaGUID = 0, userID = 0, approvedByID = 0, receivedBy = 0;
            string Comments = txtComment.Text.Trim();
            avii.Classes.UserInfo userInfo = Session["userInfo"] as avii.Classes.UserInfo;
            if (userInfo != null)
                userID = userInfo.UserGUID;

            if (ViewState["rmaGUID"]!=null)
                rmaGUID = Convert.ToInt32(ViewState["rmaGUID"]);

            RmaReceive rmaReceive = new RmaReceive();
            List<RmaReceiveDetail> rmaReceiveList = new List<RmaReceiveDetail>();
            RmaReceiveDetail rmaReceiveDetail = null;
            if (ddlUser.SelectedIndex > 0)
            {
                if (ddlUser.SelectedIndex > 0)
                {
                    rmaReceive.ReceivedByID = Convert.ToInt32(ddlReceivedBy.SelectedValue);
                    rmaReceive.ApprovedID = Convert.ToInt32(ddlUser.SelectedValue);
                    rmaReceive.UserID = userID;
                    rmaReceive.Comment = Comments;
                    rmaReceive.RMAGUID = rmaGUID;
                    rmaReceive.RMAReceiveGUID = rmaReceiveGUID;

                    foreach (RepeaterItem item in rptRma.Items)
                    {
                        CheckBox chkItem = item.FindControl("chkItem") as CheckBox;
                        if (chkItem.Checked)
                        {
                            rmaReceiveDetail = new RmaReceiveDetail();
                            HiddenField hdReceiveDetGUID = item.FindControl("hdReceiveDetGUID") as HiddenField;
                            HiddenField hdRmaDelGUID = item.FindControl("hdRmaDelGUID") as HiddenField;
                            TextBox txtQtyReceived = item.FindControl("txtQtyReceived") as TextBox;
                            Label lblESN = item.FindControl("lblESN") as Label;
                            DropDownList ddlTracking = item.FindControl("ddlTracking") as DropDownList;
                            //HiddenField hdnTracking = item.FindControl("hdnTracking") as HiddenField;
                            DropDownList ddlStatus = item.FindControl("ddlStatus") as DropDownList;
                            //HiddenField hdnStatus = item.FindControl("hdnStatus") as HiddenField;
                            if (ddlTracking.SelectedIndex == 0)
                            {
                                ddlTracking.Focus();
                                lblMsg.Text = "Tracking number is required!";
                                return;
                            }
                            if (string.IsNullOrEmpty(txtQtyReceived.Text))
                            {
                                txtQtyReceived.Focus();
                                lblMsg.Text = "Received quantity is required!";
                                return;
                            }
                            rmaReceiveDetail.ESNReceived = lblESN.Text;
                            rmaReceiveDetail.QtyReceived = Convert.ToInt32(txtQtyReceived.Text);
                            rmaReceiveDetail.RMADetGUID = Convert.ToInt32(hdRmaDelGUID.Value);
                            rmaReceiveDetail.RMAReceiveDetailGUID = Convert.ToInt32(hdReceiveDetGUID.Value);
                            rmaReceiveDetail.ShippingTrackingNumber = ddlTracking.SelectedValue;
                            rmaReceiveDetail.ReceiveStatusID = Convert.ToInt32(ddlStatus.SelectedValue);
                            rmaReceiveList.Add(rmaReceiveDetail);
                        }
                    }
                    rmaReceive.ReceiveList = rmaReceiveList;
                    if (rmaReceiveList != null && rmaReceiveList.Count > 0)
                    {
                        int returnResult = rmaOperation.RMAReceiveInsert(rmaReceive);
                        if (returnResult > 0)
                        {
                            lblMsg.Text = "Submitted successfully";
                            btnSubmit.Visible = false;
                        }
                        else
                        {
                            lblMsg.Text = "Data not saved";
                        }
                    }
                    else
                    {
                        lblMsg.Text = "Please select atleast one SKU";
                        //btnSubmit.Visible = false;
                    }
                }
                else
                {
                    lblMsg.Text = "Received by is required!";
                }
            }
            else
            {
                lblMsg.Text = "Approved by is required!";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            txtRMA.Text = string.Empty;
            txtTrackingNo.Text = string.Empty;
            trRMA.Visible = false;
            rptRma.DataSource = null;
            rptRma.DataBind();
            dpCompany.SelectedIndex = 0;
            ddlUser.Items.Clear();
            txtComment.Text = string.Empty;
        }

        protected void imgDelReceive_Command(object sender, CommandEventArgs e)
        {
            SV.Framework.RMA.RmaOperation rmaOperation = SV.Framework.RMA.RmaOperation.CreateInstance<SV.Framework.RMA.RmaOperation>();

            int RMAReceiveDetailGUID = Convert.ToInt32(e.CommandArgument);
            int userID = Convert.ToInt32(Session["UserID"]);
            if(RMAReceiveDetailGUID > 0)
            {
                int returnResult = rmaOperation.RMAReceiveDelete(RMAReceiveDetailGUID, userID);
                if(returnResult > 0)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>alert('Deleted successfully')</script>", false);

                    ReceiveSearch();
                    lblMsg.Text = "Deleted successfully";
                }
                else
                {
                    lblMsg.Text = "Record not Deleted";
                }
            }
            else
                lblMsg.Text = "Record not Deleted";
        }

        protected void ddlRmaTracking_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptRma.Items)
            {
                DropDownList ddlTracking = item.FindControl("ddlTracking") as DropDownList;
                ddlTracking.SelectedValue = ddlRmaTracking.SelectedValue;
            }
        }

        protected void ddlReceiveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (RepeaterItem item in rptRma.Items)
            {
                DropDownList ddlStatus = item.FindControl("ddlStatus") as DropDownList;
                ddlStatus.SelectedValue = ddlReceiveStatus.SelectedValue;
            }
        }
    }
}