using System;
using System.Text;
using System.Web.UI.WebControls;

namespace avii.Admin
{
    public partial class esnlist : System.Web.UI.Page
    {
        private int phoneId = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                int.TryParse(Request.Params["id"].ToString(),out phoneId);
                if (phoneId > 0)
                {
                    this.grdView.DataSource = avii.Classes.PurchaseOrder.GetPhoneEsns(phoneId);
                    this.grdView.DataBind();
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool bSelected = false;
            foreach (GridViewRow row in grdView.Rows)
            {
                if (((CheckBox)row.FindControl("chk")).Checked)
                {
                    bSelected = true;
                    stringBuilder.Append(grdView.DataKeys[row.RowIndex].Value.ToString() + ",");
                }
            }

            if (bSelected)
            {
                avii.Classes.PurchaseOrder.DeleteEsns(stringBuilder.ToString());
                this.grdView.DataSource = avii.Classes.PurchaseOrder.GetPhoneEsns(phoneId);
                this.grdView.DataBind();
            }
        }
    }
}
