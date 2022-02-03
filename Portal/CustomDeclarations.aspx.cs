using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;


namespace avii
{
    public partial class CustomDeclarations : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["poLabelID"] != null)
                {
                    int poLabelID = 0;
                    poLabelID = Convert.ToInt32(Session["poLabelID"]);

                    BindCustomDeclarations(poLabelID);
                }
            }
        }

        private void BindCustomDeclarations(int poLabelID)
        {
            List<CustomValues> customValues = ShippingLabelOperation.GetCustomdeclarations(poLabelID);
            if(customValues != null && customValues.Count > 0)
            {
                rptCustom.DataSource = customValues;
                rptCustom.DataBind();
            }

        }


    }
}