using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace avii
{
    public partial class CustomerUserQuery : System.Web.UI.Page
    {
        private avii.Classes.clsAdmUser oUser = new avii.Classes.clsAdmUser();
        private DataTable dtType;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Customer

            string loginurl = "~/logon.aspx";
            if (Session["userInfo"] == null)
                Response.Redirect(loginurl);
            if (!IsPostBack)
            {
                if (Session["adm"] == null)
                {



                    avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];
                    if (objUserInfo != null)
                    {

                        ViewState["companyID"] = objUserInfo.CompanyGUID;
                        
                    }
                }
                getroles();
                if (Request["search"] != null && Request["search"] != "")
                {
                    string searchCriteria = (string)Session["searchUser"];
                    string[] searchArr = searchCriteria.Split('~');
                    txt_user.Text = searchArr[0].ToString();
                    
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

        
        private void bindUser()
        {

            avii.Classes.UserUtility objUser = new avii.Classes.UserUtility();
            int comp = -1;
            int role = -1;
            string type = "Company";
            if (ViewState["companyID"] != null)
                comp = Convert.ToInt32(ViewState["companyID"]);
            if (ddlRole.SelectedIndex > 0)
                role = Convert.ToInt32(ddlRole.SelectedValue);
            
            if (comp == -1 && role == -1 && type == "" && txt_user.Text == "")
            {
                lbl_message.Text = "Please select search criteria";
            }
            else
            {
                string searchCriteria = txt_user.Text + "~" + comp + "~" + type + "~" + role;


                Session["searchUser"] = searchCriteria;

                List<avii.Classes.clsUserManagement> objList = objUser.getUserList(txt_user.Text, comp, type, -1, role, 0, false);
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
            //string type = "Customer";
            int comp = -1;
            int role = -1;
            //avii.Classes.UserUtility objUser = new avii.Classes.UserUtility();
            //if (ViewState["companyID"] != null)
            //    comp = Convert.ToInt32(ViewState["companyID"]);
            //if (ddlRole.SelectedIndex > 0)
            //    role = Convert.ToInt32(ddlRole.SelectedValue);
            //List<avii.Classes.clsUserManagement> objList = objUser.getUserList(txt_user.Text, comp, type, -1, role, 0, false);
            GV_User.DataSource = null;
            GV_User.DataBind();
            lbl_message.Text = string.Empty;
        }

        protected void Edit_click(object sender, CommandEventArgs e)
        {
            int userguid = Convert.ToInt32(e.CommandArgument);
            string url = string.Empty;

            url = "CustomerUsers.aspx";
            //url = "CustomerUsers.aspx?userguid=" + userguid;
            Session["userguid"] = userguid;
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
            //int userID = 0;
            int companyID = 0;

            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
            //if (Session["adm"] == null)
            //{
            //    avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];
            //    if (objUserInfo != null)
            //    {
            //        userID = objUserInfo.UserGUID;
            //        ViewState["usertype"] = "Customer";
            //    }
            //}
            avii.Classes.RoleUtility objrole = new avii.Classes.RoleUtility();
            List<avii.Classes.Roles> rolelist = objrole.getRoleList(-1, "", true, "", 0,"Customer");
            ddlRole.DataSource = rolelist;
            ddlRole.DataTextField = "RoleName";
            ddlRole.DataValueField = "RoleID";
            ddlRole.DataBind();
            ListItem ls = new ListItem("", "0");
            ddlRole.Items.Insert(0, ls);
        }
        
    }

}
