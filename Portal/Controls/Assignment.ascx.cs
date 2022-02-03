using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.Controls
{
    public partial class Assignment : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void BindFileUploadMapping(int uploadedFileID, int moduleID)
        {
            List<avii.Classes.EsnList> esnList = avii.Classes.ReportOperations.GetFileUploadMappingList(uploadedFileID, moduleID);

            if (esnList != null && esnList.Count > 0)
            {
                rptESN.DataSource = esnList;
                rptESN.DataBind();
            }
            else
            {
                rptESN.DataSource = null;
                rptESN.DataBind();
                lblESN.Text = "No records found";
            }
        }
        
    }
}