using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sanvitti1.AccessManagement
{
    public partial class CustomerRoleQuery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string loginurl = "~/logon.aspx";
            if (Session["userInfo"] == null)
                Response.Redirect(loginurl);
            if (!IsPostBack)
            {

                avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];
                if (objUserInfo != null)
                {
                    ViewState["companyID"] = objUserInfo.CompanyGUID;
                }
                if (Request["search"] != null && Request["search"] != "")
                {
                    if (Session["searchCustRole"] != null)
                    {
                        string searchCriteria = (string)Session["searchCustRole"];
                        //string[] searchArr = searchCriteria.Split('~');

                        txt_role.Text = searchCriteria;
                        //string module = searchArr[1].ToString();

                        bindroles();
                    }
                    else
                        lblMsg.Text = "Session expire!";
                }
            }
        }
        protected void btn_Search_click(object sender, EventArgs e)
        {
            lblMsg.Text = string.Empty;
            bindroles();
        }
        protected void btn_cancel_click(object sender, EventArgs e)
        {
            rolesGV.DataSource = null;
            rolesGV.DataBind();
            
            txt_role.Text = string.Empty;
            lblMsg.Text = string.Empty;
        }
        protected void bindroles()
        {
            int companyID = 0;
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);

            avii.Classes.RoleUtility objRoles = new avii.Classes.RoleUtility();
            string roles = null;
            string module = string.Empty;
            
            roles = txt_role.Text.Trim();

            string searchCriteria = roles;

            Session["searchCustRole"] = searchCriteria;
            List<avii.Classes.Roles> rolelist = new List<avii.Classes.Roles>();
            rolelist = objRoles.getRoleList(-1, roles, true, module, 0, 0, companyID);

            if (rolelist.Count > 0)
            {
                rolesGV.DataSource = rolelist;
                rolesGV.DataBind();
            }
            else
            {
                rolesGV.DataSource = null;
                rolesGV.DataBind();
                lblMsg.Text = "No matching record exists for selected search criteria";
            }

        }
        protected void Edit_click(object sender, CommandEventArgs e)
        {
            int roleGUID = Convert.ToInt32(e.CommandArgument);
            string url = "CustomerRoleMgmt.aspx?roleGUID=" + roleGUID;
            Response.Redirect(url);
        }
        protected void Delete_click(object sender, CommandEventArgs e)
        {
            avii.Classes.RoleUtility obj = new avii.Classes.RoleUtility();
            int roleguid = Convert.ToInt32(e.CommandArgument);
            obj.DeleteRole(roleguid);
            bindroles();
            lblMsg.Text = "Record Deleted Successfuly";
        }
        

       
    }
}