using System;
using System.Configuration;
using System.Data;


namespace avii.Admin
{
    public partial class UploadEsn : System.Web.UI.Page
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

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string poNum, fromDate, toDate, esn, mslNumber;
            poNum = fromDate = toDate = esn = mslNumber = null;
            lblMsg.Text = string.Empty;
            int userID = 0, companyID = 0, statusID = 0;

            if (Session["UserID"] != null)
            {
                int.TryParse(Session["UserID"].ToString(), out userID);
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

            try
            {
                DataTable table = avii.Classes.PurchaseOrder.GerPurchaseOrders_WithESN(poNum, fromDate, toDate, userID, companyID, statusID, esn, mslNumber);
                if (table != null && table.Rows.Count > 0)
                {
                    grdEsn.DataSource = table;
                    grdEsn.DataBind();
                    btnValidateESN.Visible = true;
                    btnClear.Visible = true;
                }
                else
                {
                    FormClean();
                    lblMsg.Text = "No record exists for selected criteria";
                }
            }
            catch (Exception ex)
            {

            }
                    
        }                 

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            FormClean();
        }

        private void FormClean()
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
            grdEsn.DataSource = null;
            grdEsn.DataBind();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {

        }
    }
}
