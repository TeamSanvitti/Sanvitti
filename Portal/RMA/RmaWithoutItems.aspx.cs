using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.RMA
{
    public partial class RmaWithoutItems : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindRMA();
        }

        private void BindRMA()
        {
            int companyID = -1;
            string RmaNumber, rmaDate, rmaDateTo;
            RmaNumber = rmaDate = rmaDateTo = string.Empty;
            
            List<avii.Classes.RMA> objRmaList = null;
           // objRmaList = avii.Classes.RMAUtility.GetRMAListWithoutItems(RmaNumber, rmaDate, rmaDateTo, companyID);
            if (objRmaList.Count > 0)
            {
                gvRma.DataSource = objRmaList;
                gvRma.DataBind();
                lblCount.Text = "<strong>Total count:</strong> " + objRmaList.Count.ToString();
                lblMsg.Text = string.Empty;

            }
            else
            {
                gvRma.DataSource = null;
                gvRma.DataBind();
                lblMsg.Text = "Records not found";
                lblCount.Text = string.Empty;
                        
            }
                        
        }
    }
}