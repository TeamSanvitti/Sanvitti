using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using avii.Classes;
using System.Configuration;
using System.Data;


namespace avii
{
    public partial class CustomerUsers : System.Web.UI.Page
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
                if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!Page.IsPostBack)
            {
                int userID = 0;
                //pnlStore.Visible = false;
                
                if (Session["adm"] == null)
                {
                    
                    avii.Classes.UserInfo objUserInfo = (UserInfo)Session["userInfo"];
                    if (objUserInfo != null)
                    {

                        BindStoreLocation(objUserInfo.CompanyGUID, objUserInfo.UserGUID);
                        ViewState["userid"] = objUserInfo.UserGUID;
                        ViewState["companyID"] = objUserInfo.CompanyGUID;
                        
                    }
                }

                getroles();

                bindStatus();
                string userType = System.Configuration.ConfigurationManager.AppSettings["UserType"];
                ViewState["usertype"] = userType;

                if (Session["userguid"] != null)
                {
                    userID = Convert.ToInt32(Session["userguid"]);
                    ViewState["userguid"] = userID;
                    getUser(userID);
                    btn_cancel.Text = "Back To Search";
                }
            }
        }
        private void BindStoreLocation(int companyID, int userID)
        {
            //if (userID > 0)
            //{
            //    Session["dtstore"] = UserStoreOperation.GetUserStoreLocationList(companyID, userID);
            //}
            List<StoreLocation> storeLocationList = UserStoreOperation.GetStoreLocationList(companyID,userID);
            Session["dtstore"] = storeLocationList;
            rptStores.DataSource = storeLocationList;
            rptStores.DataBind();
            if (storeLocationList.Count > 0)
                pnlStore.Visible = true;
            else
            {
                pnlStore.Visible = false;
                //lbl_message.Text = "No store assigned to this user, please contact administrator to get more information.";
            }


        }
        protected void ddType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userType = "Company";
            lbl_message.Text = string.Empty;
            //if (ViewState["usertype"] != null)
            //    userType = ViewState["usertype"].ToString();

            
            rptStores.DataSource = null;
            rptStores.DataBind();
            //pnlStore.Visible = false;
            
        }
        protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            int userID = 0;
            int companyID = 0;
            lbl_message.Text = string.Empty;
            getroles();
            if (ViewState["userguid"] != null)
                userID = Convert.ToInt32(ViewState["userguid"]);
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
            
            

            BindStoreLocation(companyID, userID);
        }
        protected void rptProducts_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
               // HiddenField hdnAddressID = e.Item.FindControl("hdnAddressID") as HiddenField;
               // CheckBox chkStore = e.Item.FindControl("chkStore") as CheckBox;

                //if (ViewState["userguid"] != null)
               // {
                    //if (Session["dtstore"] != null)
                    //{
                    //    int addressID = Convert.ToInt32(hdnAddressID.Value);
                    //    chkStore.Checked = false;
                    //    List<StoreLocation> Stores = (List<StoreLocation>)Session["dtstore"];
                    //    if(Stores!= null && Stores.Count > 0)
                    //    {
                    //        foreach (StoreLocation item in Stores)
                    //        {
                    //            if(item.UserStoreFlag > 0)
                    //            {
                    //                chkStore.Checked = true;
                    //            }
                    //        }
                    //    }
                    //    //for (int j = 0; j < dtStore.Rows.Count; j++)
                    //    //{
                    //    //    if (addressID == Convert.ToInt32(dtStore.Rows[j]["addressid"]))
                    //    //    {
                    //    //        chkStore.Checked = true;
                    //    //    }
                    //    //}
                    //}
                //}
            }
        }
        private void bindStatus()
        {
            dd_status.DataSource = avii.Classes.UserUtility.GetStatus();
            dd_status.DataTextField = "AccStatus";
            dd_status.DataValueField = "StatusID";
            dd_status.DataBind();
        }
        
        private void getUser(int userid)
        {
            //avii.Classes.UserUtility objuser = new UserUtility();
            string userType = ViewState["usertype"].ToString();
            clsUserManagement userObj = UserUtility.getUserInfo(string.Empty, -1, string.Empty, userid, -1, 0);
            if (userObj != null)
            {
                txt_user.Text = userObj.UserName;
                txt_email.Text = userObj.Email;
                txt_pwd.Attributes.Add("value", userObj.Password);
                txt_con_pwd.Attributes.Add("value", userObj.Password);
                dd_status.SelectedValue = userObj.AccountStatusID.ToString();
                //dd_type.SelectedValue = userObj.UserType;
                //txtCompanyAccount.Text = userObj.CompanyAccNo;

                if (userObj.UserType.ToLower() == userType.ToLower())
                {
                    hdnuserType.Value = userObj.UserType;
                    //trCustomer.Style.Add("display", "none");
                    //trCustomer.Visible = false;
                }
                if (userObj.CompanyName.ToLower() == userType.ToLower())
                {
                    //ddlcompany.SelectedIndex = 0;
                }
                else
                {
                    //ddlcompany.SelectedValue = userObj.CompanyID.ToString();
                }
                //if (ViewState["userid"] != null)
                    //ddlcompany.Enabled = false;
                BindStoreLocation(userObj.CompanyID, userid);

                List<UserRoleMap> userRoleList = userObj.UserRoleList;

                for (int i = 0; i < userRoleList.Count; i++)
                {
                    for (int j = 0; j < chkRoles.Items.Count; j++)
                    {
                        if (chkRoles.Items[j].Value == userRoleList[i].RoleGuid.ToString())
                        {

                            chkRoles.Items[j].Selected = true;
                        }
                    }
                }

            }


            //DataTable dtuser = objuser.get_UserList("", -1, "",userid,-1);
            //if (dtuser.Rows.Count > 0)
            //{
            //    txt_user.Text = dtuser.Rows[0]["username"].ToString().Trim();
            //    txt_email.Text = dtuser.Rows[0]["email"].ToString().Trim();
            //    txt_pwd.Attributes.Add("value", dtuser.Rows[0]["pwd"].ToString().Trim());
            //    txt_con_pwd.Attributes.Add("value", dtuser.Rows[0]["pwd"].ToString().Trim());
            //    dd_status.SelectedValue = dtuser.Rows[0]["AccountStatusID"].ToString();
            //    dd_type.SelectedValue = dtuser.Rows[0]["usertype"].ToString();

            //    if (dtuser.Rows[0]["usertype"].ToString() == userType)
            //    {
            //        hdnuserType.Value = dtuser.Rows[0]["usertype"].ToString();
            //    }
            //    if (dtuser.Rows[0]["CompanyName"].ToString() == userType)
            //    {
            //        ddlcompany.SelectedIndex = 0;
            //    }
            //    else
            //    {
            //        ddlcompany.SelectedValue = dtuser.Rows[0]["companyid"].ToString();
            //    }
            //    txtCompanyAccount.Text = dtuser.Rows[0]["companyaccountnumber"].ToString();
            //    for (int i = 0; i < dtuser.Rows.Count; i++)
            //    {
            //        for (int j = 0; j < chkRoles.Items.Count; j++)
            //        {
            //            if (chkRoles.Items[j].Value == dtuser.Rows[i]["roleguid"].ToString())
            //            {

            //                chkRoles.Items[j].Selected = true;
            //            }
            //        }
            //    }
            //}
        }
        
        private void getroles()
        {
            int userID = 0;
            int companyID = 0;
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
            
            
            if (ViewState["userid"] != null)
                userID = Convert.ToInt32(ViewState["userid"]);
            avii.Classes.RoleUtility objrole = new avii.Classes.RoleUtility();
            List<avii.Classes.Roles> rolelist = objrole.getRoleList(-1, "", true, "", 0, "Customer");
            //List<avii.Classes.Roles> rolelist = objrole.GetCustRoleList(0);
            if (rolelist.Count > 0)
            {
                chkRoles.DataSource = rolelist;
                chkRoles.DataTextField = "RoleName";
                chkRoles.DataValueField = "RoleID";
                chkRoles.DataBind();
                ViewState["role"] = null;
            }
            else
            {
                chkRoles.DataSource = rolelist;
                chkRoles.DataTextField = "RoleName";
                chkRoles.DataValueField = "RoleID";
                chkRoles.DataBind();
                lbl_message.Text = "Role not created please go to access management and create the role";
                ViewState["role"] = 1;
            }
            for (int i = 0; i < chkRoles.Items.Count; i++)
            {
                if (chkRoles.Items[i].Text == ConfigurationManager.AppSettings["deafultrole"].ToString())
                {
                    chkRoles.Items[i].Selected = true;
                    chkRoles.Items[i].Enabled = false;
                }
                chkRoles.Items[i].Attributes["class"] = "copy10grey";

            }

        }

        protected void btn_add_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                if (ViewState["role"] != null)
                {
                    lbl_message.Text = "Atleast one role should be assigned";
                    return;
                }



                avii.Classes.UserUtility objUser = new avii.Classes.UserUtility();
                int returnValue = 0;
                int status, company, userid;
                userid = 0;
                company = 0;
                string username, password, email, companyAccount, userType;
                bool active = true;
                status = Convert.ToInt32(dd_status.SelectedValue);
                if (ViewState["companyID"] != null)
                    company = Convert.ToInt32(ViewState["companyID"]);
            
                //company = Convert.ToInt32(ddlcompany.SelectedValue);

                if (ViewState["userguid"] != null)
                {
                    userid = Convert.ToInt32(ViewState["userguid"]);
                }

                username = (txt_user.Text.Trim().Length > 0 ? txt_user.Text.Trim() : null);
                companyAccount = string.Empty;

                password = (txt_pwd.Text.Trim().Length > 0 ? txt_pwd.Text.Trim() : null);
                email = (txt_email.Text.Trim().Length > 0 ? txt_email.Text.Trim() : null);
                userType = "Company";

                if (username != null)
                {
                    clsUserManagement UserObj = GetUserinfo(userid, username, password, email, status, company, companyAccount, userType, active);
                    if (UserObj.UserRoleList.Count == 0)
                    {
                        lbl_message.Text = "Atleast one role should be assigned";
                        return;
                    }
                    if (UserObj.UserStoreList.Count == 0 && ViewState["usertype"].ToString() != userType)
                    {
                        lbl_message.Text = "Store is required";
                        return;
                    }
                    else
                    {
                        returnValue = UserUtility.InsertNewUser(UserObj);

                        if (returnValue > 0)
                        {
                            if (returnValue == 1)
                                lbl_message.Text = "Username already exist!";
                            if (returnValue == 2)
                                lbl_message.Text = "Email already exist!";
                            if (returnValue == 3)
                                lbl_message.Text = "Username and email are already exist!";


                            return;
                        }
                        //int userguid = objUser.InsertNewUser(userid, txt_user.Text, txt_pwd.Text,txt_email.Text, txtCompanyAccount.Text, Convert.ToInt32(dd_status.SelectedValue), Convert.ToInt32(ddlcompany.SelectedValue), chkActive.Checked, dd_type.SelectedValue);
                        //for (int i = 0; i < chkRoles.Items.Count; i++)
                        //{
                        //    if (chkRoles.Items[i].Selected)
                        //    {
                        //        objUser.InsertUserRole(userguid, Convert.ToInt32(chkRoles.Items[i].Value));
                        //    }
                        //    chkRoles.Items[i].Attributes["class"] = "copy10grey";

                        //}



                        if (userid > 0)
                        {
                            lbl_message.Text = "User Updated Sucessfully!!";
                            //Response.Redirect("userquery.aspx?search=1");
                        }
                        else
                        {
                            reset();
                            lbl_message.Text = "User Added Successfully!!";

                        }
                    }
                }
                else
                {
                    lbl_message.Text = "User name can not be empty";
                    SetFocus(txt_user);
                }
            }

        }
        private clsUserManagement GetUserinfo(int userid, string username, string password, string email, int status, int company, string compAcc, string userType, bool active)
        {
            clsUserManagement userObj = new clsUserManagement();
            List<UserRoleMap> userRoleList = new List<UserRoleMap>();
            for (int i = 0; i < chkRoles.Items.Count; i++)
            {
                if (chkRoles.Items[i].Selected)
                {
                    UserRoleMap userRoleObj = new UserRoleMap();
                    userRoleObj.UserGuid = userid;
                    userRoleObj.RoleGuid = Convert.ToInt32(chkRoles.Items[i].Value);
                    userRoleList.Add(userRoleObj);
                }
            }

            List<UserStoreMap> userStoreList = new List<UserStoreMap>();

            foreach (RepeaterItem item in rptStores.Items)
            {
                CheckBox chkStore = item.FindControl("chkStore") as CheckBox;
                HiddenField hdnAddressID = item.FindControl("hdnAddressID") as HiddenField;
                if (chkStore.Checked)
                {
                    UserStoreMap userStoreObj = new UserStoreMap();
                    userStoreObj.UserID = userid;
                    userStoreObj.AddressID = Convert.ToInt32(hdnAddressID.Value);
                    userStoreList.Add(userStoreObj);
                }


            }

            userObj.UserID = userid;
            userObj.AccountStatusID = status;
            userObj.CompanyAccNo = compAcc;
            userObj.Active = active;
            userObj.CompanyID = company;
            userObj.Email = email;
            userObj.Password = password;
            //userObj.RoleGuid = 0;
            userObj.UserName = username;
            userObj.UserRoleList = userRoleList;
            userObj.UserStoreList = userStoreList;
            userObj.UserType = userType;
            return userObj;

        }
        private void reset()
        {
            getroles();
            txt_user.Text = string.Empty;
            txt_con_pwd.Attributes.Add("value", string.Empty);
            txt_email.Text = string.Empty;
            txt_pwd.Attributes.Add("value", string.Empty);
            txt_user.Text = string.Empty;
            //chkActive.Checked = false;
            dd_status.SelectedIndex = 0;
            //dd_type.SelectedIndex = 0;
            //ddlcompany.SelectedIndex = 0;
            lbl_message.Text = string.Empty;
            BindStoreLocation(Convert.ToInt32(ViewState["companyID"]), Convert.ToInt32(ViewState["userid"]));
            //rptStores.DataSource = null;
            //rptStores.DataBind();
            //pnlStore.Visible = false;
            //trCustomer.Visible = true;
            //trCustomer.Style.Add("display", "block");
        }

        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            if (ViewState["userguid"] == null)
                reset();
            else
                Response.Redirect("customeruserquery.aspx?search=1", false);

        }

        protected void txt_user_TextChanged(object sender, EventArgs e)
        {
            avii.Classes.UserUtility objUser = new avii.Classes.UserUtility();
            List<avii.Classes.clsUserManagement> objList = objUser.getUserList(txt_user.Text, -1, "", -1, -1, 1, true);
            if (objList.Count > 0)
            {
                lbl_message.Text = "Username already exists!";
                SetFocus(txt_user);
            }
            else
            {
                lbl_message.Text = string.Empty;
                SetFocus(txt_email);
            }

            for (int i = 0; i < chkRoles.Items.Count; i++)
            {
                chkRoles.Items[i].Attributes["class"] = "copy10grey";

            }
            SetFocus(txt_email);
        }

    }
}
