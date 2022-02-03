using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Inventory;

namespace avii.ESN
{
    public partial class EsnBoxIDEdit : System.Web.UI.Page
    {
        SV.Framework.Inventory.EsnBoxOperation EsnBoxOperation = SV.Framework.Inventory.EsnBoxOperation.CreateInstance<SV.Framework.Inventory.EsnBoxOperation>();

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
                if (Session["esn"] != null)
                {
                    string ESN = Convert.ToString(Session["esn"]);
                    GetBoxIDInfo(ESN);
                }
            }
        }
        private void GetBoxIDInfo(string ESN)
        {   
            EsnBoxIDInfo esnBoxIDInfo = EsnBoxOperation.GetEsnBoxIDInfo(ESN);
            if(esnBoxIDInfo != null && !string.IsNullOrEmpty(esnBoxIDInfo.ESN) )
            {
                ViewState["esnid"] = esnBoxIDInfo.EsnID;
                ViewState["poid"] = esnBoxIDInfo.POID;
                if (esnBoxIDInfo.AssignedBoxItems > 0)
                    txtAssignedQtyBox.Text = esnBoxIDInfo.AssignedBoxItems.ToString();
                
                txtBoxID.Text = esnBoxIDInfo.BoxID;
                if (esnBoxIDInfo.BoxItems > 0)
                    txtBoxItems.Text = esnBoxIDInfo.BoxItems.ToString();
                txtCategory.Text = esnBoxIDInfo.CategoryName;
                txtDec.Text = esnBoxIDInfo.DEC;
                txtHex.Text = esnBoxIDInfo.HEX;
                txtESN.Text = esnBoxIDInfo.ESN;
                txtReceiveDate.Text = esnBoxIDInfo.ReceiveDate;
                txtSerialNo.Text = esnBoxIDInfo.SerialNumber;
                txtSKU.Text = esnBoxIDInfo.SKU;
                lnkPO.Text = esnBoxIDInfo.FulfillmentNumber;
                if(!string.IsNullOrEmpty(esnBoxIDInfo.ErrorMessage))
                {
                    btnAssign.Visible = false;
                    btnRemove.Visible = false;
                    lblMsg.Text = esnBoxIDInfo.ErrorMessage;
                }

            }
        }
        protected void btnRemove_Click(object sender, EventArgs e)
        {
            if (ViewState["esnid"] != null)
            {
                int esnID = Convert.ToInt32(ViewState["esnid"]);
                string boxID = "";// txtBoxID.Text.Trim();
                string esn = txtESN.Text.Trim();
                string returnMessage = EsnBoxOperation.BoxIDUpdate(esnID, boxID, esn);
                if (string.IsNullOrEmpty(returnMessage))
                    lblMsg.Text = "Submitted successfully";
                else
                    lblMsg.Text = returnMessage;
            }
        }

        protected void btnAssign_Click(object sender, EventArgs e)
        {
            if (ViewState["esnid"] != null)
            {
                int esnID = Convert.ToInt32(ViewState["esnid"]);
                string boxID = txtBoxID.Text.Trim();

                int boxItems = 0, AssignedQty = 0;
                if (!string.IsNullOrEmpty(txtAssignedQtyBox.Text))
                    AssignedQty = Convert.ToInt32(txtAssignedQtyBox.Text);
                if (!string.IsNullOrEmpty(txtBoxItems.Text))
                    boxItems = Convert.ToInt32(txtBoxItems.Text);
                if (!string.IsNullOrEmpty(boxID))
                {
                    if (boxItems == AssignedQty)
                    {
                        lblMsg.Text = "This box has already assigned qunatity";
                    }
                    else
                    {
                        string esn = txtESN.Text.Trim();
                        string returnMessage = EsnBoxOperation.BoxIDUpdate(esnID, boxID, esn);
                        if (string.IsNullOrEmpty(returnMessage))
                            lblMsg.Text = "Submitted successfully";
                        else
                            lblMsg.Text = returnMessage;
                    }
                }
                else
                {
                    lblMsg.Text = "BoxID is required!";
                }
            }
        }

        protected void lnkPO_Click(object sender, EventArgs e)
        {
            if (ViewState["poid"] != null)
            {
                int poID = Convert.ToInt32(ViewState["poid"]);
                if (poID > 0)
                {
                    Session["poid"] = poID;
                    Session["unused"] = poID;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>OpenNewPage('../FulfillmentDetails.aspx')</script>", false);
                }
            }
        }
    }
}