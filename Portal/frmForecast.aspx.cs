using System;
using System.Web.UI.WebControls;
using avii.Classes;

namespace avii
{
    public partial class frmForecast : System.Web.UI.Page
    {

        ItemForecastController objItemForecast = new ItemForecastController(6);

        protected global::System.Web.UI.WebControls.GridView gvForecast;

        protected void Page_Load(object sender, EventArgs e)
        {
            //gvForecast.DataSource = objItemForecast.GetForecastList(8);
            if (!IsPostBack)
            {
                bindGrid();
            }
        }

        protected void bindGrid()
        {
            gvForecast.DataSource = objItemForecast.GetItemForecast();
            gvForecast.DataBind();
        }

        protected void dlsItemForecast_ItemDataBound(object sender, DataListItemEventArgs e)
        {
            if (ListItemType.Item == e.Item.ItemType || ListItemType.AlternatingItem == e.Item.ItemType)
            {
                CheckBox chkLocked = (CheckBox)e.Item.FindControl("chkLocked");
                TextBox txtQty = (TextBox)e.Item.FindControl("txtQty");
                if (chkLocked != null)
                    chkLocked.Attributes.Add("onclick", "checkQty(this)");

                if (txtQty != null)
                    txtQty.Attributes.Add("onkeypress", "isNumberKey(event)");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow grv in gvForecast.Rows)
            {
                HiddenField hdnItemID = (HiddenField)grv.FindControl("hdnItemID");
                DataList dlsForecast = (DataList)grv.FindControl("dlsItemForecast");

                if (dlsForecast != null)
                {
                    foreach (DataListItem dls in dlsForecast.Items)
                    {
                        HiddenField hdnForecastGUID = (HiddenField)dls.FindControl("hidForecastGUID");
                        HiddenField hdnForecastDate = (HiddenField)dls.FindControl("hidForecastDate");
                        HiddenField hdnForecastStatusID = (HiddenField)dls.FindControl("hidForecastStatusID");
                        CheckBox chkLocked = (CheckBox)dls.FindControl("chkLocked");
                        TextBox txtQty = (TextBox)dls.FindControl("txtQty");

                        if (txtQty != null)
                        {

                            if ("" == txtQty.Text)
                                txtQty.Text = "0";
                            if (0 == Convert.ToInt32(hdnForecastStatusID.Value))
                            {
                                objItemForecast.insertUpdateItemForecast(
                                    Convert.ToInt32(hdnForecastGUID.Value)
                                    , Convert.ToInt32(hdnItemID.Value)
                                    , Convert.ToDateTime(hdnForecastDate.Value)
                                    , Convert.ToInt32(txtQty.Text)
                                    , chkLocked.Checked
                                    );
                            }
                        }
                    }
                }
            }
            bindGrid();
        }
    }

}