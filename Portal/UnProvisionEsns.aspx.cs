using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.Models.Common;
using SV.Framework.Models.Fulfillment;
using SV.Framework.Fulfillment;


namespace avii
{
    public partial class UnProvisionEsns : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int poid = 0;
                if (Session["poid"] != null)
                {
                    poid = Convert.ToInt32(Session["poid"]);
                    //ViewState["poid"] = poid;
                    BindProvisioning(poid);
                }
            }
        }
        private void BindProvisioning(int poid)
        {
            lblMsg.Text = "";
            try
            {
                PurchaseOrder purchaseOrderOperation = PurchaseOrder.CreateInstance<PurchaseOrder>();

                List<POEsn> esnList = purchaseOrderOperation.GetUnProvisionEsnList(poid);
                if (esnList != null && esnList.Count > 0)
                {
                    Session["poesnlist"] = esnList;
                    rptSKUESN.DataSource = esnList;
                    rptSKUESN.DataBind();
                }
                else
                {
                    rptSKUESN.DataSource = null;
                    rptSKUESN.DataBind();
                    lblMsg.Text = "No records exists!";
                    
                }
            }
            catch (Exception ex)
            {
                lblMsg.Text = ex.Message;
            }
        }

    }
}