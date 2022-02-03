using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;


namespace avii.Admin
{
    public partial class EsnManagement : System.Web.UI.Page
    {
        int utype = 0;
     //   List<clsEsnxml> objesnlist = new List<clsEsnxml>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    HeadAdmin.Visible = false;
                    pnlCompany.Visible = false;
                    btnSearch.Text = "Search for Provisioning";
                    btnClear.Text = "Clear Assigned Data";
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
            else
            {
                utype = 1;
                MenuHeader.Visible = false;
                HeadAdmin.Visible = true;
            }
 
        
            if (!Page.IsPostBack)
            {

                if (Session["adm"] != null)
                {

                    pnlCompany.Visible = true;
                    bindCustomerDropDown();
                }
                else
                {
                    pnlCompany.Visible = false;
                    btnSearch.Text = "Search for Provisioning";
                    btnClear.Text = "Clear Assigned Data";

                }
            }
        }
        protected void bindCustomerDropDown()
        {
            dpCompany.DataSource = avii.Classes.clsCompany.GetCompany(0, 0);
            dpCompany.DataValueField = "CompanyID";
            dpCompany.DataTextField = "CompanyName";
            dpCompany.DataBind();

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
              
                
            int mdn = 0;       
            string poNum, fromDate, toDate, esn, mslNumber;
            poNum = fromDate = toDate = esn = mslNumber = null;
            lblMsg.Text = string.Empty;
           // EsnManagementDB obj = new EsnManagementDB();
            int userID = 0, companyID = 0, statusID = 0;

            if (Session["adm"] == null)
            {
                int.TryParse(Session["UserID"].ToString(), out userID);
            }
            else
            {
                companyID = Convert.ToInt32(dpCompany.SelectedValue);
            }

            esn = (txtEsn.Text.Trim().Length > 0 ? txtEsn.Text.Trim() : null);
            poNum = (txtPONum.Text.Trim().Length > 0 ? txtPONum.Text.Trim() : null);
            mslNumber = (txtMslNumber.Text.Trim().Length > 0 ? txtMslNumber.Text.Trim() : null);

            int.TryParse(dpStatusList.SelectedIndex.ToString(), out statusID);
            
            if (txtFromDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtFromDate.Text, out dt))
                    fromDate = dt.ToShortDateString();
                else
                    throw new Exception("PO Date From does not have correct format(MM/DD/YYYY)");
            }

            if (txtToDate.Text.Trim().Length > 0)
            {
                DateTime dt;
                if (DateTime.TryParse(txtToDate.Text, out dt))
                    toDate = dt.ToShortDateString();
                else
                    throw new Exception("PO Date To does not have correct format(MM/DD/YYYY)");

            }
            if (chkMDN.Checked)
            {
                mdn = 1;
            }
            bool validForm = true;
            try
            {

                if (string.IsNullOrEmpty(poNum) && string.IsNullOrEmpty(fromDate) && string.IsNullOrEmpty(toDate) && string.IsNullOrEmpty(esn) &&
                    string.IsNullOrEmpty(mslNumber) && dpCompany.SelectedIndex == 0 && dpStatusList.SelectedIndex == 0 && chkMDN.Checked == false)
                {
                    validForm = false;
                }
                if (validForm)
                {

                    List<clsEsnAssignment> lstesnmgmt = EsnManagementDB.GerPurchaseOrders_WithESN(poNum, fromDate, toDate, userID, companyID, statusID, esn, mslNumber, 0, mdn);
                    if (lstesnmgmt != null && lstesnmgmt.Count > 0)
                    {
                        Session["listesn"] = lstesnmgmt;
                        grdEsn.DataSource = lstesnmgmt;
                        grdEsn.DataBind();
                        lblCount.Text = lstesnmgmt.Count.ToString();
                        lblCounttext.Visible = true;
                        pnlPagesize.Visible = true;
                        btnClear.Visible = true;
                        if (Session["adm"] == null)
                        {
                            btnValidateESN.Visible = false;
                            btnSubmit.Visible = true;
                            btnSubmit.Enabled = true;
                        }
                        else
                            btnValidateESN.Visible = true;
                    }
                    else
                    {
                        FormClean();
                        lblMsg.Text = "No record exists for selected criteria";
                    }
                }
                else
                {
                    lblMsg.Text = "Please select the search criteria";
                    Session["listesn"] = null;
                    grdEsn.DataSource = null;
                    grdEsn.DataBind();
                }
                
                
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
                    
        }                 

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            FormClean();
        }

        private void FormClean()
        {
            try
            {
                lblMsg.Text = string.Empty;
                txtPONum.Text = string.Empty;
                txtFromDate.Text = string.Empty;
                txtToDate.Text = string.Empty;
                dpStatusList.Text = string.Empty;
                txtMslNumber.Text = string.Empty;
                txtEsn.Text = string.Empty;
                btnValidateESN.Visible = false;
                btnSubmit.Visible = false;
                btnClear.Visible = false;
                lblCounttext.Visible = false;
                lblCount.Text = string.Empty;
                chkMDN.Checked = false;
                pnlPagesize.Visible = false;
                grdEsn.DataSource = null;
                grdEsn.DataBind();
                hdnvalidate.Value = string.Empty;
                hdnmsl.Value = string.Empty;
                Session["listesn"] = null;
            }
            catch (Exception ex)
            { }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
           
            try
            {
                
                foreach (GridViewRow r in grdEsn.Rows)
                {

                    TextBox txtEsn = (TextBox)r.FindControl("txtEsn");
                    Label lblMsl = (Label)r.FindControl("lblMsl");
                    TextBox txtfmupc = (TextBox)r.FindControl("txtfmupc");
                    TextBox txtMdn = (TextBox)r.FindControl("txtMdn");
                    TextBox txtMSID = (TextBox)r.FindControl("txtMSID");
                    if (hdnvalidate.Value == "1")
                    {
                        if (lblMsl.Text == ConfigurationManager.AppSettings["missing"].ToString() || lblMsl.Text == ConfigurationManager.AppSettings["invalid"].ToString())
                        {

                            if (Session["adm"] != null)
                            {
                                txtEsn.Text = string.Empty;
                                lblMsl.Text = string.Empty;
                                txtfmupc.Text = string.Empty;
                            }
                            
                            txtMdn.Text = string.Empty;
                            txtMSID.Text = string.Empty;
                        }
                    }
                    else
                    {
                        if (Session["adm"] != null)
                        {
                            txtEsn.Text = string.Empty;
                            lblMsl.Text = string.Empty;
                            txtfmupc.Text = string.Empty;
                        }
                        
                        txtMdn.Text = string.Empty;
                        txtMSID.Text = string.Empty;
                    }

                }
                hdnvalidate.Value = string.Empty;
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

        protected List<clsEsnxml> generatexmlList()
        {
            List<clsEsnxml> esnList = new List<clsEsnxml>();
            clsEsnxml esnData = null;
            foreach (GridViewRow r in grdEsn.Rows)
            {
                TextBox txtEsn = (TextBox)r.FindControl("txtEsn");
                if (txtEsn.Text.Trim().Length > 0)
                {
                    esnData = new clsEsnxml();
                    Label lblMsl = (Label)r.FindControl("lblMsl");
                    HiddenField hdnpoid = (HiddenField)r.FindControl("hdnpoid");
                    TextBox txtfmupc = (TextBox)r.FindControl("txtfmupc");
                    TextBox txtMdn = (TextBox)r.FindControl("txtMdn");
                    TextBox txtMSID = (TextBox)r.FindControl("txtMSID");
                    HiddenField hdnpodid = (HiddenField)r.FindControl("hdnpodid");
                    if (lblMsl.Text.Trim().Length == 0)
                        esnData.ifMissing = 1;
                    else
                        esnData.ifMissing = 0;

                    esnData.esn = txtEsn.Text;
                    esnData.MslNumber = lblMsl.Text;
                    esnData.Po_id = Convert.ToInt32(hdnpoid.Value);
                    esnData.FmUpc = txtfmupc.Text;
                    esnData.Pod_id = Convert.ToInt32(hdnpodid.Value);
                    esnData.MDN = txtMdn.Text;
                    esnData.MSID = txtMSID.Text;
                    
                    esnList.Add(esnData);
                }

            }
            return esnList;
        }

        private void VerifyESNs(List<clsEsnxml> esns, ref List<clsEsnxml> missingMsls, ref List<clsEsnxml> assignedMsls)
        {
            missingMsls = new List<clsEsnxml>();
            assignedMsls = new List<clsEsnxml>();

            hdnmsl.Value = string.Empty;
            foreach (clsEsnxml esn in esns)
            {
                if (string.IsNullOrEmpty(esn.MslNumber))
                {
                    missingMsls.Add(esn);
                    hdnmsl.Value = "1";
                }
                else
                {                 
                    assignedMsls.Add(esn);
                }
            }
        }

        private Int32 IsMissingESNExist(List<clsEsnAssignment> esns)
        {
            int ctr = 0;
            foreach (clsEsnAssignment esn in esns)
            {
                if (string.IsNullOrEmpty(esn.MslNumber))
                {
                   hdnmsl.Value = "1";
                   ctr = ctr + 1;
                }
            }

            return ctr;
        }
   
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                List<clsEsnxml> missingMsls = null;
                List<clsEsnxml> assignedMsls = null;
                bool saved = false;

                lblMsg.Text = string.Empty;
                List<clsEsnxml> esns = generatexmlList();
                if (esns != null & esns.Count > 0)
                {
                    VerifyESNs(esns, ref missingMsls, ref assignedMsls);

                    if (assignedMsls != null && assignedMsls.Count > 0 )
                    {
                        EsnManagementDB.InsertEsn(EsnManagementDB.SerializeObjetToXML(assignedMsls, "ArrayOfClsEsnxml", "clsEsnxml"), utype);
                        saved = true;
                        btnSubmit.Enabled = false;
                        FormClean();
                        lblMsg.Text = "ESNs are Successfully Assigned!";
                        
                    }

                    if (missingMsls != null && missingMsls.Count > 0 )
                    {
                        if (saved ==false)
                            lblMsg.Text = "Missing ESNs found in the list, please assign corresponding MSL before assigning ESN";
                        else
                        {
                            lblMsg.Text = "ESNs are successfully assigned. There are missing MSL for assigned ESNs, please assign corresponding MSL before assigning ESN";
                        }

                        grdEsn.DataSource = null;
                        grdEsn.DataBind();
                    }
                    else if (saved == false)
                    {
                        lblMsg.Text = "No ESN has been assigned to save";
                    }
                }
                else
                {
                    lblMsg.Text = "ESNs are not assigned";
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
           
        }

        protected void btnValidateESN_Click(object sender, EventArgs e)
        {
            try
            {
                lblMsg.Text = string.Empty;
               clsEsnxml objesnmgmt = new clsEsnxml();
                List<clsEsnxml>  esns = generatexmlList();

                if (esns != null && esns.Count > 0)
                {
                    List<clsEsnAssignment> esnAssigned = EsnManagementDB.ValidateESN(esns);
                    if (IsMissingESNExist(esnAssigned) < esnAssigned.Count)
                    {
                        hdnvalidate.Value = "1";
                        btnSubmit.Visible = true;
                        btnSubmit.Enabled = true;
                    }
                    else
                    {
                        lblMsg.Text = "Assigned ESN cannot be validated. The assigned ESNs does not exist in ESN repository";
                    }

                    lblCount.Text = esnAssigned.Count.ToString();
                    grdEsn.DataSource = esnAssigned;
                    grdEsn.DataBind();

                }
                else        
                {
                    lblMsg.Text = "No ESN is assign to Validate";
                }



                
                //foreach (GridViewRow row in grdEsn.Rows)
                //{
                //    TextBox txtEsn = (TextBox)row.FindControl("txtEsn");
                //    Label lblMsl = (Label)row.FindControl("lblMsl");

                //    esnmanagementUtility objesn = new esnmanagementUtility();
                //    if (txtEsn.Text != string.Empty)
                //    {
                //        List<clsEsnmanagement> esnlist = objesn.GerPurchaseOrders_WithESN(null, null, null, 0, 0, 0, txtEsn.Text.Trim(), null, 1,0);
                //        if (esnlist.Count > 0)
                //        {
                //            txtEsn.Text = esnlist[0].esn;
                //            lblMsl.Text = esnlist[0].MslNumber;
                //            if (lblMsl.Text == null || lblMsl.Text == string.Empty)
                //                lblMsl.Text = ConfigurationManager.AppSettings["missing"].ToString();
                            
                //        }
                //        else
                //        {
                //            lblMsl.Text = ConfigurationManager.AppSettings["invalid"].ToString();
                            
                //        }
                //    }
                //    else
                //    {
                //        lblMsl.Text =string.Empty;
                        
                //    }

                //}

            }
            catch(Exception ex)
            {
                lblMsg.Text = ex.Message.ToString();
            }
        }

       
        protected void grdEsn_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row as GridViewRow;
            TextBox txtEsn = (TextBox)row.FindControl("txtEsn");
            Label lblEsn = (Label)row.FindControl("lblEsn");
            Label lblfmupc = (Label)row.FindControl("lblfmupc");
            TextBox txtfmupc = (TextBox)row.FindControl("txtfmupc");
            if (Session["adm"] == null)
            {


                if (row.RowType == DataControlRowType.DataRow)
                {
                    lblEsn.Visible = true;
                    txtEsn.Visible = false;
                    lblfmupc.Visible = true;
                    txtfmupc.Visible = false;
                }
                
            }
            else
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    lblEsn.Visible = false;
                    txtEsn.Visible = true;
                    lblfmupc.Visible = false;
                    txtfmupc.Visible = true;
                }
            }

        }

      
        protected void grdEsn_RowEditing(object sender, GridViewEditEventArgs e)
        {
            grdEsn.EditIndex = e.NewEditIndex;
        }

        protected void grdEsn_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdEsn.PageIndex = e.NewPageIndex;
            List<clsEsnAssignment> lstesnmgmt=(List<clsEsnAssignment>)Session["listesn"];
            grdEsn.DataSource = lstesnmgmt;
            grdEsn.DataBind();
        }

        protected void ddlPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            grdEsn.PageSize = Convert.ToInt32(ddlPageSize.SelectedValue);
            List<clsEsnAssignment> lstesnmgmt = (List<clsEsnAssignment>)Session["listesn"];
            grdEsn.DataSource = lstesnmgmt;
            grdEsn.DataBind();
        }
    }
}
