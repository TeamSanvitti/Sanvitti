using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Sanvitti1.AccessManagement
{
    public partial class CustomerRoleMgmt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string loginurl = "~/logon.aspx";
            if (Session["userInfo"] == null)
                Response.Redirect(loginurl);
            if (!Page.IsPostBack)
            {
                avii.Classes.UserInfo objUserInfo = (avii.Classes.UserInfo)Session["userInfo"];
                if (objUserInfo != null)
                {
                    ViewState["companyID"] = objUserInfo.CompanyGUID;
                }
                
                //In Case of Edit
                if (Request["roleGUID"] != null && Request["roleGUID"] != "")
                {
                    int roleGUID = Convert.ToInt32(Request["roleGUID"]);
                    ViewState["roleGUID"] = roleGUID;

                    grid_bind(roleGUID);
                    btn_cancel.Visible = false;
                    lnkBacktosearch.Visible = true;

                }
                else
                {
                    btn_cancel.Visible = true;
                    lnkBacktosearch.Visible = false;

                    grid_bind(-1);

                }
                if (Request["update"] != null)
                {
                    lblMsg.Text = "Role Updated Successfully";
                }
            }
        }
        protected void grid_bind(int roleguid)
        {

            avii.Classes.RoleUtility objRoles = new avii.Classes.RoleUtility();
            if (ViewState["roleGUID"] != null)
            {
                
                List<avii.Classes.RoleModulePermission> listPermission = objRoles.getpermissions(Convert.ToInt32(ViewState["roleGUID"]), 0);
                Session["permisions"] = listPermission;

                avii.Classes.Roles objRole = avii.Classes.RoleUtility.GetRoleInfo(Convert.ToInt32(ViewState["roleGUID"]), "", true, "", 0, 0, 0);
                if (objRole != null)
                {
                    txt_role.Text = objRole.RoleName;
                    cbk_active.Checked = objRole.active;
                    ViewState["defaultrole"] = objRole.DefaultRole;
                    //ddlCompany.SelectedValue = objRolelist[0].CompanyID.ToString();
                    //txtPriority.Text = objRolelist[0].Priority.ToString();
                }
            }
            avii.Classes.ModuleUtility objModule = new avii.Classes.ModuleUtility();
            List<avii.Classes.Module> objModulelistUser = objModule.GetCustomerModuleTree(-1, true, -1, -1, true, "usr", true, "");
            gvCustModules.DataSource = objModulelistUser;
            gvCustModules.DataBind();

               
            
        }


        protected void GVCust_rowDataBound(object sender, GridViewRowEventArgs e)
        {

            
            //List<avii.Classes.Permission> objPermissionlist = objPermission.getPermissionList(-1, "");

            GridViewRow row = e.Row;
            //if (row.RowType == DataControlRowType.Header)
            //{

            //    DataList dl_permissionheader = (DataList)row.FindControl("dl_permissionheadercust");
            //    dl_permissionheader.DataSource = objPermissionlist;
            //    dl_permissionheader.DataBind();
            //}
            if (row.DataItem == null)
            {
                return;
            }

            HiddenField hdnModuleparentGUID = (HiddenField)row.FindControl("hdnModuleparentGUID1");
            HiddenField hdnModuleGUID = (HiddenField)row.FindControl("hdnModuleGUID1");
            //CheckBoxList chkPernmissionlist = (CheckBoxList)row.FindControl("cbk_permissionscust");

            CheckBox chkPernmissionlist = (CheckBox)row.FindControl("chkAccess");

            if (ViewState["roleGUID"] != null && ViewState["roleGUID"] != "")
            {

                List<avii.Classes.RoleModulePermission> listPermission = (List<avii.Classes.RoleModulePermission>)Session["permisions"];
                //var permission = from 
                //Label lblRole = (Label)row.FindControl("lblTitle");

                if (listPermission != null && listPermission.Count > 0)
                {

                    
                    var permissions = (from item in listPermission where item.ModuleId.Equals(Convert.ToInt32(hdnModuleGUID.Value)) select item).ToList();
                    if (permissions != null && permissions.Count > 0)
                    {
                        var permission = (from item in permissions where item.PermissionId.Equals(1) select item).ToList();
                        if (permission != null && permission.Count > 0)
                        {
                            chkPernmissionlist.Checked = false;
                        }
                        else
                            chkPernmissionlist.Checked = true;
                    }

                }
            }
                //chkPernmissionlist.DataSource = objPermissionlist;
                //chkPernmissionlist.DataTextField = "";
                //chkPernmissionlist.DataValueField = "PermissionGUID";
                //chkPernmissionlist.DataBind();
                if (Convert.ToInt32(hdnModuleparentGUID.Value.Trim()) != 0)
                    chkPernmissionlist.CssClass = "check" + hdnModuleparentGUID.Value;
                else
                    chkPernmissionlist.CssClass = "check" + hdnModuleGUID.Value;

            
            //string st = ViewState["roleGUID"];
            //if (ViewState["roleGUID"] != null && ViewState["roleGUID"] != "")
            //{
                //List<avii.Classes.Roles> objRolelist = objRoles.getRoleList(Convert.ToInt32(ViewState["roleGUID"]), "", cbk_active.Checked, "", 0);
                //if (objRolelist.Count > 0)
                //{
                //txt_role.Text = objRolelist[0].RoleName.ToString();
                //cbk_active.Checked = Convert.ToBoolean(objRolelist[0].active);
                
                //avii.Classes.RoleUtility obj = new avii.Classes.RoleUtility();
                //List<avii.Classes.RoleModulePermission> listPermission = obj.getpermissions(Convert.ToInt32(ViewState["roleGUID"]), Convert.ToInt32(hdnModuleGUID.Value));
                
            //    for (int j = 0; j < listPermission.Count; j++)
            //    {
            //        for (int i = 0; i < chkPernmissionlist.Items.Count; i++)
            //        {
            //            if (chkPernmissionlist.Items[i].Value == Convert.ToString(listPermission[j].PermissionId))
            //            {
            //                chkPernmissionlist.Items[i].Selected = true;
            //                if (Convert.ToString(listPermission[j].PermissionId) == "1")
            //                {
            //                    for (int k = 1; k < chkPernmissionlist.Items.Count; k++)
            //                    {
            //                        chkPernmissionlist.Items[k].Enabled = false;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    //}
            //}

            //for (int i = 0; i < chkPernmissionlist.Items.Count; i++)
            //{
            //    if (Convert.ToInt32(hdnModuleparentGUID.Value.Trim()) != 0)
            //        chkPernmissionlist.Items[i].Attributes["class"] = "check" + hdnModuleparentGUID.Value + i.ToString();
            //    else
            //        chkPernmissionlist.Items[i].Attributes["class"] = "check" + hdnModuleGUID.Value + i.ToString();

            //    chkPernmissionlist.Items[i].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
            //}
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {

            
            foreach (GridViewRow row in gvCustModules.Rows)
            {
                CheckBox chkPernmissionlist = (CheckBox)row.FindControl("chkAccess");
                chkPernmissionlist.Checked = false;
                chkPernmissionlist.Enabled = true;

                //for (int i = 0; i < chkPernmissionlist.Items.Count; i++)
                //{
                //    chkPernmissionlist.Items[i].Selected = false;
                //    chkPernmissionlist.Items[i].Enabled = true;
                //}
            }
            
            //Clear all the checkboxex.
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>reset()</script>", false);
            lblMsg.Text = string.Empty;
        }
        protected void btn_role_Click(object sender, EventArgs e)
        {
            avii.Classes.RoleUtility obj = new avii.Classes.RoleUtility();
            int roleGUID = 0;
            int companyID = 0;
            if (ViewState["companyID"] != null)
                companyID = Convert.ToInt32(ViewState["companyID"]);
            bool defaultRole = false;
            int priority = 3;

            if (ViewState["defaultrole"] != null)
                defaultRole = Convert.ToBoolean(ViewState["defaultrole"]);

            //if (txtPriority.Text.Trim() != string.Empty)
            //    priority = Convert.ToInt32(txtPriority.Text.Trim());


            List<avii.Classes.RoleModulePermissionMap> objRoleModulePermissionList = new List<avii.Classes.RoleModulePermissionMap>();


            foreach (GridViewRow row in gvCustModules.Rows)
            {
                avii.Classes.RoleModulePermissionMap rolePermission = new avii.Classes.RoleModulePermissionMap();
                
                //CheckBoxList cb = (CheckBoxList)row.FindControl("cbk_permissionscust");
                CheckBox chkAccess = (CheckBox)row.FindControl("chkAccess");
                HiddenField hdnModuleGUID = (HiddenField)row.FindControl("hdnModuleGUID1");
                HiddenField hdnModuleparentGUID = (HiddenField)row.FindControl("hdnModuleparentGUID1");


                rolePermission.ModuleID = Convert.ToInt32(hdnModuleGUID.Value);
                if (chkAccess.Checked)
                    rolePermission.PermissionID = "2";
                else
                    rolePermission.PermissionID = "1";

                objRoleModulePermissionList.Add(rolePermission);
                
            }

            if (ViewState["roleGUID"] != null && ViewState["roleGUID"] != "")
            {
                roleGUID = Convert.ToInt32(ViewState["roleGUID"]);
                int role_GUID = obj.createRole(roleGUID, txt_role.Text, 1, cbk_active.Checked, companyID, priority, objRoleModulePermissionList, defaultRole);

            }
            else
            {
                roleGUID = obj.createRole(roleGUID, txt_role.Text, 1, cbk_active.Checked, companyID, priority, objRoleModulePermissionList, defaultRole);

            }
            int moduleGUID = 9999;
            
            //foreach (GridViewRow row in gvCustModules.Rows)
            //{
            //    //CheckBoxList cb = (CheckBoxList)row.FindControl("cbk_permissionscust");
            //    CheckBox chkAccess = (CheckBox)row.FindControl("chkAccess");
            //    HiddenField hdnModuleGUID = (HiddenField)row.FindControl("hdnModuleGUID1");
            //    HiddenField hdnModuleparentGUID = (HiddenField)row.FindControl("hdnModuleparentGUID1");
            //    //string permission = string.Empty;



            //    //for (int i = 0; i < cb.Items.Count; i++)
            //    //{
            //    //    if (cb.Items[1].Enabled == false)
            //    //    {
            //    //        moduleGUID = Convert.ToInt32(hdnModuleGUID.Value);
            //    //    }
            //    //    if (moduleGUID != Convert.ToInt32(hdnModuleparentGUID.Value))
            //    //    {
            //    //        int per = 1;
            //    //        if (cb.Items[i].Selected && cb.Items[i].Enabled)
            //    //        {
            //    //            per = Convert.ToInt32(cb.Items[i].Value);
            //                obj.mapPermission(roleGUID, Convert.ToInt32(hdnModuleGUID.Value), 1, 0);
            //    //        }
            //    //        else if (cb.SelectedIndex < 0)
            //    //        {
            //    //            obj.mapPermission(roleGUID, Convert.ToInt32(hdnModuleGUID.Value), per, 0);
            //    //            break;
            //    //        }
            //    //    }
            //    //}



            //}
            


            if (Convert.ToInt32(ViewState["roleGUID"]) > 0)
            {
                lblMsg.Text = "Role updated successfully!";
                // reset();
                /// ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>reset()</script>", false);
            }
            else
            {

                lblMsg.Text = "Role added successfully!";
                reset();
            }


        }
        protected void reset()
        {
            txt_role.Text = string.Empty;
            cbk_active.Checked = false;
            
            foreach (GridViewRow row in gvCustModules.Rows)
            {
                CheckBox chkAccess = (CheckBox)row.FindControl("chkAccess");
                chkAccess.Checked = false;
                //CheckBoxList chkPernmissionlist = (CheckBoxList)row.FindControl("cbk_permissionscust");
                //for (int i = 0; i < chkPernmissionlist.Items.Count; i++)
                //{
                //    chkPernmissionlist.Items[i].Selected = false;
                //    chkPernmissionlist.Items[i].Enabled = true;
                //}
            }
            
        }
        
        protected void cbk_permissionscust_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnTabindex.Value = "1";
            string selecteditem = Request.Form["__EVENTTARGET"].ToString();

            string[] itemarr = selecteditem.Split('$');
            if (itemarr.Length > 3)
                selecteditem = itemarr[3].ToString();
            else
                selecteditem = "0";

            int selectedindex = Convert.ToInt32(selecteditem);

            CheckBoxList cbk_permissions = (CheckBoxList)sender;
            bool itemselected = false;

            if (cbk_permissions.Items[selectedindex].Selected)
                itemselected = true;
            for (int i = 1; i < cbk_permissions.Items.Count; i++)
            {
                if (cbk_permissions.Items[0].Selected)
                {
                    cbk_permissions.Items[i].Enabled = false;
                    cbk_permissions.Items[i].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                }
                else
                    cbk_permissions.Items[i].Enabled = true;

            }
            int moduleGUID = 9999;
            int moduleGUID2 = 9999;
            int moduleGUID3 = 9999;
            foreach (GridViewRow row in gvCustModules.Rows)
            {

                CheckBoxList chklist = (CheckBoxList)row.FindControl("cbk_permissionscust");
                HiddenField hdnModuleGUID = (HiddenField)row.FindControl("hdnModuleGUID1");
                HiddenField hdnModuleparentGUID = (HiddenField)row.FindControl("hdnModuleparentGUID1");

                if (selectedindex > 0)
                {
                    if (itemselected)
                    {
                        if (chklist.Items[selectedindex].Selected == true)
                        {
                            moduleGUID = Convert.ToInt32(hdnModuleGUID.Value);
                        }
                        if (moduleGUID == Convert.ToInt32(hdnModuleparentGUID.Value))
                        {
                            chklist.Items[selectedindex].Selected = true;
                            moduleGUID2 = Convert.ToInt32(hdnModuleGUID.Value);

                        }
                        if (moduleGUID2 == Convert.ToInt32(hdnModuleparentGUID.Value))
                        {
                            chklist.Items[selectedindex].Selected = true;
                            moduleGUID3 = Convert.ToInt32(hdnModuleGUID.Value);
                        }
                        if (moduleGUID3 == Convert.ToInt32(hdnModuleparentGUID.Value))
                        {
                            chklist.Items[selectedindex].Selected = true;

                        }


                    }
                    else
                    {
                        if (chklist.Items[selectedindex].Selected == false)
                        {
                            moduleGUID = Convert.ToInt32(hdnModuleGUID.Value);
                        }


                        if (moduleGUID == Convert.ToInt32(hdnModuleparentGUID.Value))
                        {
                            chklist.Items[selectedindex].Selected = false;
                            moduleGUID2 = Convert.ToInt32(hdnModuleGUID.Value);
                        }
                        if (moduleGUID2 == Convert.ToInt32(hdnModuleparentGUID.Value))
                        {
                            chklist.Items[selectedindex].Selected = false;
                            moduleGUID3 = Convert.ToInt32(hdnModuleGUID.Value);
                        }
                        if (moduleGUID3 == Convert.ToInt32(hdnModuleparentGUID.Value))
                        {
                            chklist.Items[selectedindex].Selected = false;

                        }

                    }
                }
                else
                {
                    if (itemselected)
                    {

                        for (int j = 0; j < chklist.Items.Count; j++)
                        {


                            if (chklist.Items[1].Enabled == false)
                            {
                                moduleGUID = Convert.ToInt32(hdnModuleGUID.Value);
                            }
                            if (moduleGUID == Convert.ToInt32(hdnModuleparentGUID.Value))
                            {
                                chklist.Items[0].Selected = true;
                                moduleGUID2 = Convert.ToInt32(hdnModuleGUID.Value);
                            }
                            if (moduleGUID2 == Convert.ToInt32(hdnModuleparentGUID.Value))
                            {
                                chklist.Items[selectedindex].Selected = true;
                                moduleGUID3 = Convert.ToInt32(hdnModuleGUID.Value);
                            }
                            if (moduleGUID3 == Convert.ToInt32(hdnModuleparentGUID.Value))
                            {
                                chklist.Items[selectedindex].Selected = true;
                            }

                        }

                        for (int k = 1; k < chklist.Items.Count; k++)
                        {
                            if (moduleGUID == Convert.ToInt32(hdnModuleparentGUID.Value))
                            {
                                chklist.Items[k].Enabled = false;
                            }
                        }
                        for (int l = 1; l < chklist.Items.Count; l++)
                        {
                            if (moduleGUID2 == Convert.ToInt32(hdnModuleparentGUID.Value))
                            {
                                chklist.Items[l].Enabled = false;
                            }
                        }
                        for (int p = 1; p < chklist.Items.Count; p++)
                        {
                            if (moduleGUID3 == Convert.ToInt32(hdnModuleparentGUID.Value))
                            {
                                chklist.Items[p].Enabled = false;
                            }
                        }

                    }
                    else
                    {

                        for (int j = 0; j < chklist.Items.Count; j++)
                        {


                            if (chklist.Items[1].Enabled == true)
                            {
                                moduleGUID = Convert.ToInt32(hdnModuleGUID.Value);
                            }
                            if (moduleGUID == Convert.ToInt32(hdnModuleparentGUID.Value))
                            {
                                chklist.Items[0].Selected = false;
                                moduleGUID2 = Convert.ToInt32(hdnModuleGUID.Value);
                            }
                            if (moduleGUID2 == Convert.ToInt32(hdnModuleparentGUID.Value))
                            {
                                chklist.Items[selectedindex].Selected = false;
                                moduleGUID3 = Convert.ToInt32(hdnModuleGUID.Value);
                            }
                            if (moduleGUID3 == Convert.ToInt32(hdnModuleparentGUID.Value))
                            {
                                chklist.Items[selectedindex].Selected = false;
                            }

                        }

                        for (int k = 1; k < chklist.Items.Count; k++)
                        {
                            if (moduleGUID == Convert.ToInt32(hdnModuleparentGUID.Value))
                            {
                                chklist.Items[k].Enabled = true;
                            }
                        }
                        for (int m = 1; m < chklist.Items.Count; m++)
                        {
                            if (moduleGUID2 == Convert.ToInt32(hdnModuleparentGUID.Value))
                            {
                                chklist.Items[m].Enabled = true;
                            }
                        }
                        for (int q = 1; q < chklist.Items.Count; q++)
                        {
                            if (moduleGUID3 == Convert.ToInt32(hdnModuleparentGUID.Value))
                            {
                                chklist.Items[q].Enabled = true;
                            }
                        }

                    }
                }

            }
        }
        protected void txt_role_TextChanged(object sender, EventArgs e)
        {
            avii.Classes.RoleUtility objRoles = new avii.Classes.RoleUtility();
            List<avii.Classes.Roles> rolelist = objRoles.getRoleList(-1, txt_role.Text, true, "", 1, 0, 0);
            if (rolelist.Count > 0)
            {
                lblMsg.Text = "Role Name already exists.";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>confirmmessage(1)</script>", false);
            }
            else
            {
                lblMsg.Text = string.Empty;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>confirmmessage(0)</script>", false);
            }
            SetFocus(cbk_active);
        }

    }
}