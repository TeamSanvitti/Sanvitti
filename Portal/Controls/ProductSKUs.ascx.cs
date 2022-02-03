using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Controls
{
    public partial class ProductSKUs : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void BindItemSKU(int itemGUID)
        {
            lblSKU.Text = string.Empty;
            ProductController objProduct = new ProductController();
            List<CompanySKUno> lstCompanySKUno = objProduct.getCompanySKUnoList(itemGUID, -1, "");
            if (lstCompanySKUno != null && lstCompanySKUno.Count > 0)
            {
                gvSKU.DataSource = lstCompanySKUno;
                gvSKU.DataBind();
            }
            else
            {
                gvSKU.DataSource = lstCompanySKUno;
                gvSKU.DataBind();
                lblSKU.Text = "No record found";
            }
        }

    }
}