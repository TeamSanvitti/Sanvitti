using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.AccessManagement
{
    public partial class rolequery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url =  ConfigurationSettings.AppSettings["LogonPage"].ToString();

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
                bindModule();

                if (Request["search"] != null && Request["search"] != "")
                {
                    string searchCriteria = (string)Session["searchRole"];
                    string[] searchArr = searchCriteria.Split('~');

                    txt_role.Text = searchArr[0].ToString();
                    string module = searchArr[1].ToString();
                    string[] modulearr = module.Split(',');
                    for (int i = 0; i < modulearr.Length; i++)
                    {
                        for (int j = 0; j<lbModulelist.Items.Count; j++)
                        {
                            if (lbModulelist.Items[j].Value == modulearr[i].ToString())
                            {
                                lbModulelist.Items[j].Selected = true;
                            }
                        }
                    }
                    bindroles();
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
            for (int i = 0; i < lbModulelist.Items.Count; i++)
            {
                lbModulelist.Items[i].Selected = false;
            }
            txt_role.Text = string.Empty;
            lblMsg.Text = string.Empty;
        }
        
        protected void bindroles()
        {
            avii.Classes.RoleUtility objRoles = new avii.Classes.RoleUtility();
            string roles = null;
             string module = string.Empty;
            for (int i = 0; i < lbModulelist.Items.Count; i++)
            {
                if (lbModulelist.Items[i].Selected)
                {
                    if(module=="")
                    module = lbModulelist.Items[i].Value;
                    else
                        module =  module + "," + lbModulelist.Items[i].Value;
                }
            }
            roles = txt_role.Text.Trim();

                
            //if (string.IsNullOrEmpty(roles)&&lbModulelist.SelectedIndex<0)
            //{
            //    rolesGV.DataSource = null;
            //    rolesGV.DataBind();
            //    lblMsg.Text = "Please select the search criteria";
            //}
            //else
            {
                string searchCriteria = roles + "~" + module;


                Session["searchRole"] = searchCriteria;
                List<avii.Classes.Roles> rolelist=new List<avii.Classes.Roles>();
                rolelist = objRoles.getRoleList(-1, roles, true, module,0);
                
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
        }
        protected void Edit_click(object sender, CommandEventArgs e)
        {
            int roleGUID = Convert.ToInt32(e.CommandArgument);
            string url = "roleMgmt.aspx?roleGUID=" + roleGUID;
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
        protected void bindModule()
        {
            avii.Classes.ModuleUtility objmodule = new avii.Classes.ModuleUtility();
            List<avii.Classes.Module> objModulelist = objmodule.getModuleList(-1,-1,"","adm",true,"");
            lbModulelist.DataSource = objModulelist;
            lbModulelist.DataTextField = "title";
            lbModulelist.DataValueField = "moduleguid";
            lbModulelist.DataBind();
        }

        protected void rolesGV_RowDataBound(object sender, GridViewRowEventArgs e)
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
    }
}
