using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Controls
{
    public partial class CustomerStores : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void BindStores(int companyID)
        {
            List<avii.Classes.StoreLocation> storeList = avii.Classes.StoreOperations.GetCompanyStoreList(companyID, 0);
            if (storeList != null && storeList.Count > 0)
            {
                gvStores.DataSource = storeList;
                gvStores.DataBind();
            }
            else
            {
                gvStores.DataSource = null;
                gvStores.DataBind();
                lblStore.Text = "No record exists!";
            }
        }
        protected void DeleteStore_click(object sender, CommandEventArgs e)
        {
            ViewState["flag"] = 1;
            if (ViewState["companyID"] != null)
            {
                int addressID = Convert.ToInt32(e.CommandArgument);
                avii.Classes.CompanyOperations.DeleteCompanyAndStore(0, addressID);
                //gvCompany.Rebind();
                BindStores(Convert.ToInt32(ViewState["companyID"]));

                lblStore.Text = "Store deleted successfully";
            }
        }
    }
}