using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SV.Framework.SOR;

namespace avii.Dekit
{
    public partial class DekitCronJob : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SV.Framework.SOR.DekitOperations dekitOperations = SV.Framework.SOR.DekitOperations.CreateInstance<SV.Framework.SOR.DekitOperations>();

            dekitOperations.DeKittingCronJob();
        }
    }
}