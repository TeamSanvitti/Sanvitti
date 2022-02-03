using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.RoleMgmt
{
    public partial class userQuery : System.Web.UI.Page
    {
        private avii.Classes.clsAdmUser oUser = new avii.Classes.clsAdmUser();
        private DataTable dtType;
        protected void Page_Load(object sender, EventArgs e)
        {
            string loginurl = "~/logon.aspx";
            if (Session["adm"] == null)
                Response.Redirect(loginurl);

            if (!IsPostBack)
            {
                ddlCompany.DataSource = GetCompanyList(0);
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataValueField = "CompanyID";
                ddlCompany.DataBind();
                getroles();
                if (Request["search"] != null && Request["search"] != "")
                {
                    string searchCriteria = (string)Session["searchUser"];
                    string[] searchArr = searchCriteria.Split('~');
                    txt_user.Text = searchArr[0].ToString();
                    ddlCompany.SelectedValue = searchArr[1].ToString();
                    ddlType.SelectedValue = searchArr[2].ToString();
                    ddlRole.SelectedValue = searchArr[3].ToString();

                    bindUser();
                }

            }
        }

        protected void GV_User_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GV_User.PageIndex = e.NewPageIndex;
            bindUser();
        }

        private DataTable GetCompanyList(int companyID)
        {
            DataTable dataTable = Session["companylist"] as DataTable;
            if (!(dataTable != null && dataTable.Rows.Count > 0))
            {
                dataTable = avii.Classes.clsCompany.GetCompany(companyID, 0);
            }

            return dataTable;
        }
        private void bindUser()
        {
            
            avii.Classes.UserUtility objUser = new avii.Classes.UserUtility();
            int comp = -1;
            int role = -1;
            string type = string.Empty;
            string userName = string.Empty;
            userName = txt_user.Text.Trim().Length > 0 ? txt_user.Text.Trim() : string.Empty;
            if (ddlCompany.SelectedIndex > 0)
                comp = Convert.ToInt32(ddlCompany.SelectedValue);
            if (ddlRole.SelectedIndex > 0)
                role = Convert.ToInt32(ddlRole.SelectedValue);
            if (ddlType.SelectedIndex > 0)
                type = ddlType.SelectedValue;
            if (comp == -1 && role == -1 && type == "" && txt_user.Text == "")
            {
                lbl_message.Text = "Please select search criteria";
            }
            else
            {
                string searchCriteria = txt_user.Text.Trim() + "~" + comp + "~" + type + "~" + role;


                Session["searchUser"] = searchCriteria;

                List<avii.Classes.clsUserManagement> objList = objUser.getUserList(userName, comp, type, -1, role, 0, false);
                if (objList.Count == 0)
                    lbl_message.Text = "No Records found";
                else
                    lbl_message.Text = string.Empty;

                GV_User.DataSource = objList;
                GV_User.DataBind();
            }
        }
        private void AddTableRows(Int32 iRowAdd)
        {
            if (ViewState["Type"] == null)
                dtType = oUser.GetUsers();
            else
                dtType = (DataTable)ViewState["Type"];
            if (iRowAdd > 0)
            {
                DataRow dRow;
                dRow = dtType.NewRow();
                dtType.Rows.InsertAt(dRow, 0);

            }
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            txt_user.Text = "";
            ddlRole.SelectedIndex = 0;
            ddlType.SelectedIndex = 0;
            ddlCompany.SelectedIndex = 0;
            int comp = -1;
            int role = -1;
            avii.Classes.UserUtility objUser = new avii.Classes.UserUtility();
            if (ddlCompany.SelectedIndex > 0)
                comp = Convert.ToInt32(ddlCompany.SelectedValue);
            if (ddlRole.SelectedIndex > 0)
                role = Convert.ToInt32(ddlRole.SelectedValue);
            List<avii.Classes.clsUserManagement> objList = objUser.getUserList(txt_user.Text, comp, ddlType.SelectedValue, -1, role, 0, false);
            GV_User.DataSource = objList;
            GV_User.DataBind();
            lbl_message.Text = string.Empty;
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {

        }
        protected void Edit_click(object sender, CommandEventArgs e)
        {
            int userguid = Convert.ToInt32(e.CommandArgument);
            string url = string.Empty;

            url = "Users.aspx?userGUID=" + userguid;

            Response.Redirect(url);
        }
        protected void Delete_click(object sender, CommandEventArgs e)
        {
            avii.Classes.UserUtility objuser = new avii.Classes.UserUtility();
            int userguid = Convert.ToInt32(e.CommandArgument);
            objuser.DeleteUser(userguid);
            bindUser();
            lbl_message.Text = "User Inactive Successfuly";
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            bindUser();
        }
        protected void getroles()
        {
            avii.Classes.RoleUtility objrole = new avii.Classes.RoleUtility();
            List<avii.Classes.Roles> rolelist = objrole.getRoleList(-1, "", true, "", 0);
            ddlRole.DataSource = rolelist;
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleID";
            ddlRole.DataBind();
            ListItem ls = new ListItem("", "0");
            ddlRole.Items.Insert(0, ls);
        }
        protected void GV_User_RowDataBound(object sender, GridViewRowEventArgs e)
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
