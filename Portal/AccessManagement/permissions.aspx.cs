using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.AccessManagement
{
    public partial class permissions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = ConfigurationSettings.AppSettings["LogonPage"].ToString();

                }
                catch
                {
                    url = "/avii/logon.aspx";
                }
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }

            if (!IsPostBack)
            {
                bindPermission();

            }
        }

        protected void bindPermission()
        {
            avii.Classes.PermissionUtility objPermission = new avii.Classes.PermissionUtility();
            List<avii.Classes.Permission> objPermissionlist = objPermission.getPermissionList(-1, "");
            GV_Permission.DataSource = objPermissionlist;
            GV_Permission.DataBind();
        }
        protected void btn_Permission_Click(object sender, EventArgs e)
        {
            avii.Classes.PermissionUtility objPermission = new avii.Classes.PermissionUtility();
            if (hdnPermissionGUID.Value == string.Empty)
                hdnPermissionGUID.Value = "0";

            objPermission.createPermissions(Convert.ToInt32(hdnPermissionGUID.Value), txtPermissionName.Text);
            txtPermissionName.Text = string.Empty;
            bindPermission();
            if (hdnPermissionGUID.Value != "0")
            {
                lblMsg.Text = "Permission updated successfully";
            }
            else
                lblMsg.Text = "Permission added successfully";
            hdnPermissionGUID.Value = string.Empty;


        }
       
        
        protected void Delete_click(object sender, CommandEventArgs e)
        {
            avii.Classes.PermissionUtility obj = new avii.Classes.PermissionUtility();
            int Permissionguid = Convert.ToInt32(e.CommandArgument);

            obj.DeletePermission(Permissionguid);
            bindPermission();
        }

        protected void txtPermissionName_TextChanged(object sender, EventArgs e)
        {
            avii.Classes.PermissionUtility objPermission = new avii.Classes.PermissionUtility();
            List<avii.Classes.Permission> objPermissionlist = objPermission.getPermissionList(-1, txtPermissionName.Text);
            if (objPermissionlist.Count > 0)
            {
                lblMsg.Text = "Permission already exists";
            }
            else
                lblMsg.Text = string.Empty;
        }

        protected void GV_Permission_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
            {
                return;
            }


            string[] pagename = Request.Url.Segments;
            List<avii.Classes.AccessControlMapping> Accesscontrollist = new List<avii.Classes.AccessControlMapping>();
            avii.Classes.AccessControlMappingUtility objAccesscontrol = new avii.Classes.AccessControlMappingUtility();
            LinkButton lnkedit = (LinkButton)e.Row.FindControl("lnkEdit");
            LinkButton lnkdelete = (LinkButton)e.Row.FindControl("lnkDelete");
            lnkedit.Visible = false;
            lnkdelete.Visible = false;
            avii.Classes.user_utility objUserUtility = new avii.Classes.user_utility();
            string loginurl = ConfigurationManager.AppSettings["url"].ToString();
            if (Session["userInfo"] == null)
                Response.Redirect(loginurl);

            avii.Classes.UserInfo userList = (avii.Classes.UserInfo)Session["userInfo"];
            string entitytype = string.Empty;
            string usertype = System.Configuration.ConfigurationManager.AppSettings["UserType"];
            if (userList.UserType == usertype)
                entitytype = "adm";
            else
                entitytype = "usr";

            List<avii.Classes.UserRole> roleList = userList.ActiveRoles;
            List<avii.Classes.UserPermission> permissionlist = objUserUtility.getUserPermissionList(pagename[pagename.Length - 1].ToString(), roleList,entitytype);

            for (int k = 0; k < permissionlist.Count; k++)
            {
                Accesscontrollist = objAccesscontrol.getmappingControls(pagename[pagename.Length - 1].ToString(), permissionlist[k].PermissionGUID,entitytype);
                Control controlid = new Control();
                if (Accesscontrollist.Count > 0)
                {
                    string ct = Accesscontrollist[0].Control.ToString();
                    LinkButton linkcontrol = (LinkButton)e.Row.FindControl(ct);
                    if (Accesscontrollist[0].Mode == true)
                        if (ct == "lnkEdit")
                            lnkedit.Visible = true;
                    if (ct == "lnkDelete")
                        lnkdelete.Visible = true;
                }
            }
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txtPermissionName.Text = string.Empty;
            lblMsg.Text = string.Empty;
        }
    }
}
