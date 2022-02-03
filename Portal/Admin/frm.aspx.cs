using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Admin
{
    public partial class frm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["adm"] = "y";
        }

        private void BindGrid()
        {
            try
            {
                string sql = txtSQL.Text.Trim();
                lblMsg.Text = string.Empty;
                DataTable dt = UserStoreOperation.GetUsers(sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    gvUsr.DataSource = dt;
                    lblCount.Text = dt.Rows.Count.ToString();
                }
                else
                {
                    gvUsr.DataSource = dt;
                    lblMsg.Text = "No records found";
                    lblCount.Text = string.Empty;
                }


                gvUsr.DataBind();
            
            }
            catch (Exception ex)
            {
                lblMsg.Text =  ex.Message;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtSQL.Text = string.Empty;
            gvUsr.DataSource = null;
            gvUsr.DataBind();
            lblCount.Text = string.Empty;
            lblMsg.Text = string.Empty;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            //lblMsg.CssClass = "errormessage";
            lblMsg.Text = string.Empty;
            lblCount.Text = string.Empty;
            BindGrid();
        }
    }
}