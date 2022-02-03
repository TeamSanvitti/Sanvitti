using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii.Controls
{
    public partial class ProductsDetail : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void GetItemDetails(int itemID)
        {
            ProductController objProduct = new ProductController();
            List<InventoryItems> lsitemList = objProduct.getItemList(-1, itemID, -1, -1, -1, "", -1, "", "", "", -1, "", "", -1, 0, 0, false);

            if (lsitemList.Count > 0)
            {
                lblName.Text = lsitemList[0].ItemName;
                lblProductCode.Text = lsitemList[0].ItemCode;
                lblModelNumber.Text = lsitemList[0].ModelNumber;
                lblShortDesc.Text = lsitemList[0].ItemDesc1;
                lblFullDesc.Text = lsitemList[0].ItemDesc2;

                lblUPC.Text = lsitemList[0].Upc;
                lblColor.Text = lsitemList[0].ItemColor;

                lblCategory.Text = lsitemList[0].ItemCategory;
                lblMaker.Text = lsitemList[0].ItemMaker;
                lblCondition.Text = lsitemList[0].ItemType.ToString();
                //txtSKU.Text = lsitemList[0].SKU;

                //ddlCompany.SelectedValue = lsitemList[0].CompanyGUID.ToString();
                //ddlitemType.SelectedValue = lsitemList[0].Item_Type.ToString();

                if (!string.IsNullOrEmpty(lsitemList[0].ItemTechnology))
                {
                    lblCarrier.Text = lsitemList[0].ItemTechnology;
                    

                }
                //ddlTechnology1.SelectedValue = lsitemList[0].TechnologyID.ToString();
                chkActive.Checked = lsitemList[0].Active;
                chkAllowLTE.Checked = lsitemList[0].AllowLTE;
                chkAllowRMA.Checked = lsitemList[0].AllowRMA;
                chkShowunderCatalog.Checked = lsitemList[0].ShowunderCatalog;
                //if (lsitemList[0].Active)
                //    lblActive.Text = "Yes";
                //else
                //    lblActive.Text = "No";
                //if (lsitemList[0].AllowRMA)
                //    lblAllowRMA.Text = "Yes";
                //else
                //    lblAllowRMA.Text = "No";
                //if (lsitemList[0].AllowLTE)
                //    lblAllowLTE.Text = "Yes";
                //else
                //    lblAllowLTE.Text = "No";
                //if (lsitemList[0].ShowunderCatalog)
                //    lblShowUnderCatalog.Text = "Yes";
                //else
                //    lblShowUnderCatalog.Text = "No";

                //chkAllowRMA.Checked = lsitemList[0].AllowRMA;
                //chkAllowLTE.Checked = lsitemList[0].AllowLTE;
                //chkShowunderCatalog.Checked = lsitemList[0].ShowunderCatalog;
                lblDoc.Text = lsitemList[0].ItemDocument;
                



            }
        }
        
    }
}