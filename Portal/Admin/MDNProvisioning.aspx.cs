using System;
using System.Data;

namespace avii.Admin
{
    public partial class MDNProvisioning : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request["p"]!=null)
            {
                GetMDNDEtails(Convert.ToInt32(Request["p"]));
            }
        }
        private void GetMDNDEtails(int pod_ID)
        {
            DataTable esnDt = Session["esnsearch"] as DataTable;
            if (esnDt != null && esnDt.Rows.Count > 0)
            { 
                DataRow[] rows = esnDt.Select(string.Format("POD_ID='{0}' ", pod_ID));
                    DataRow searchedRow = null;
                    if (rows.Length > 0)
                    {
                        searchedRow = rows[0];
                        lblSKU.Text = Convert.ToString(searchedRow["item_code"]);
                        lblESN.Text = Convert.ToString(searchedRow["esn"]);
                        lblMDN.Text = Convert.ToString(searchedRow["mdn"]);
                        lblPO.Text = Convert.ToString(searchedRow["PurchaseOrderNumber"]);
                        lblUPC.Text = Convert.ToString(searchedRow["upc"]);
                    }
                     
            }

        }
    }
}