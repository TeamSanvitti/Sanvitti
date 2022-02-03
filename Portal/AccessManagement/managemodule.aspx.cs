using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.AccessManagement
{
    public partial class managemodule : System.Web.UI.Page
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
                
                bindGV_Module("");
                
                if (Request["search"] != null)
                {
                    if (Session["search"] != null)
                    {
                        string search = (string)Session["search"];
                        string[] searcharray = search.Split(',');
                        ddlType.SelectedValue = searcharray[0];
                        txtModuleTitle.Text = searcharray[1];
                        bindGV_Module(txtModuleTitle.Text);
                    }
                    
                }


            }
        }

        protected void bindGV_Module(string modulename)
        {
            avii.Classes.ModuleUtility objModule = new avii.Classes.ModuleUtility();
            List<avii.Classes.Module> moduletreelist = new List<avii.Classes.Module>();
            List<avii.Classes.Module> objModulelist = new List<avii.Classes.Module>();
            avii.Classes.Module obj2 = new avii.Classes.Module();
            List<avii.Classes.Module> objModulelisttree = new List<avii.Classes.Module>();

            if (modulename == string.Empty || modulename == null)
            {
                moduletreelist = objModule.getModuleTree(-1, true, 0, -1, true, ddlType.SelectedValue, false, "");
            }
            else
            {
                objModulelist = objModule.getModuleList(-1, -1, "", ddlType.SelectedValue, false, modulename);
                if (objModulelist.Count>0)
              {
                    int moduleguid = objModulelist[0].ModuleGUID;
                    int moduleparentguid = objModulelist[0].ModuleParentGUID;
                    obj2.ModuleGUID = objModulelist[0].ModuleGUID;
                    obj2.ModuleName = objModulelist[0].ModuleName;
                    obj2.ModuleParentGUID = objModulelist[0].ModuleParentGUID;
                    obj2.Title = objModulelist[0].Title;
                    obj2.Url = objModulelist[0].Url;
                    obj2.UserType = objModulelist[0].UserType;
                    obj2.Active = objModulelist[0].Active;
                    obj2.IsItem = objModulelist[0].IsItem;
                    objModulelisttree.Add(obj2);
                    recchild(objModulelisttree, moduleguid);
                    recparent(objModulelisttree, moduleparentguid);
              }
                    moduletreelist = objModule.getModuleTree1(-1, false, 0, -1, true, "", false, "", objModulelisttree);
            } 
            GV_Module.DataSource = moduletreelist;
            GV_Module.DataBind();
            if (GV_Module.Rows.Count < 1)
            {
                //btndelete.Visible = false;
                //btnInactive.Visible = false;
                pnlDelete.Visible = false;
               // deletePermission = false;
                lblMsg.Text = "No Matching Records Found!";
            }
            else
            {
                pnlDelete.Visible = true;
                
                lblMsg.Text = string.Empty;
                
            }

        }
        //Recursive Function to locate parent Module
        protected void recparent(List<avii.Classes.Module> objModulelisttree,int moduleparentguid)
        {
            
            avii.Classes.ModuleUtility objModule = new avii.Classes.ModuleUtility();
            List<avii.Classes.Module> list2 = objModule.getModuleList(moduleparentguid, -1, "", "", false, "");
            for (int j = 0; j < list2.Count; j++)
            {
                avii.Classes.Module obj1 = new avii.Classes.Module();
                obj1.ModuleGUID = list2[j].ModuleGUID;
                obj1.ModuleName = list2[j].ModuleName;
                obj1.ModuleParentGUID = list2[j].ModuleParentGUID;
                obj1.Title = list2[j].Title;
                obj1.Url = list2[j].Url;
                obj1.UserType = list2[j].UserType;
                obj1.Active = list2[j].Active;
                obj1.IsItem = list2[j].IsItem;
                objModulelisttree.Add(obj1);
             }
            int list2count = list2.Count;
            if (list2count > 0)
            {
                for (int i = 0; i < list2count; i++)
                {
                    recparent(objModulelisttree, list2[i].ModuleParentGUID);
                 
                }
               
            }
            
           
        }
        //Recursive Function to locate child Module
        protected void recchild(List<avii.Classes.Module> objModulelisttree, int moduleid)
        {
            avii.Classes.ModuleUtility objModule = new avii.Classes.ModuleUtility();
            List<avii.Classes.Module> list1 = objModule.getModuleList(-1, moduleid, "", "", false, "");
            for (int i = 0; i < list1.Count; i++)
            {
                avii.Classes.Module obj = new avii.Classes.Module();
                obj.ModuleGUID = list1[i].ModuleGUID;
                obj.ModuleName = list1[i].ModuleName;
                obj.ModuleParentGUID = list1[i].ModuleParentGUID;
                obj.Title = list1[i].Title;
                obj.Url = list1[i].Url;
                obj.UserType = list1[i].UserType;
                obj.Active = list1[i].Active;
                obj.IsItem = list1[i].IsItem;
                objModulelisttree.Add(obj);

            }

            int list1count = list1.Count;
            if (list1count > 0)
            {
                for (int i = 0; i < list1count; i++)
                {
                    recchild(objModulelisttree, list1[i].ModuleGUID);
                }
            }



        }
       
        protected void btn_cancel_Click(object sender, EventArgs e)
        {

            txtModuleTitle.Text = string.Empty;
            lblMsg.Text = string.Empty;
            ddlType.SelectedIndex = 0;
            bindGV_Module("");
        }

        protected void Delete_click(object sender, CommandEventArgs e)
        {
            avii.Classes.ModuleUtility obj = new avii.Classes.ModuleUtility();
            int moduleguid = Convert.ToInt32(e.CommandArgument);

            obj.DeleteModule(moduleguid, 0);
            bindGV_Module("");
        }


        protected void GV_Module_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.DataItem == null)
            {
                return;
            }
            string[] pagename = Request.Url.Segments;
            List<avii.Classes.AccessControlMapping> controllist = new List<avii.Classes.AccessControlMapping>();
            avii.Classes.AccessControlMappingUtility objAccesscontrol = new avii.Classes.AccessControlMappingUtility();
            LinkButton lnkedit = (LinkButton)e.Row.FindControl("lnkEdit");

            lnkedit.Visible = false;
            
            avii.Classes.user_utility objUserUtility = new avii.Classes.user_utility();
            string loginurl = ConfigurationManager.AppSettings["url"].ToString();
            if (Session["userInfo"] == null)
                Response.Redirect(loginurl);

            avii.Classes.UserInfo userList = (avii.Classes.UserInfo)Session["userInfo"];
            string usertype = System.Configuration.ConfigurationManager.AppSettings["UserType"];
            string entitytype = string.Empty;
            if (userList.UserType == usertype)
                entitytype = "adm";
            else
                entitytype = "usr";

            List<avii.Classes.UserRole> roleList = userList.ActiveRoles;
            controllist = objAccesscontrol.getmappingControls(pagename[pagename.Length - 1].ToString(), -1, entitytype); 
            
            List<avii.Classes.UserPermission> permissionlist = objUserUtility.getUserPermissionList(pagename[pagename.Length - 1].ToString(), roleList, entitytype);
            for (int k = 0; k < permissionlist.Count; k++)
            {
                //Accesscontrollist = objAccesscontrol.getmappingControls(pagename[pagename.Length - 1].ToString(), permissionlist[k].PermissionGUID, entitytype);
                var Accesscontrollist = (from item in controllist where item.PermissionGuid.Equals(permissionlist[k].PermissionGUID) select item).ToList();
                
                Control controlid = new Control();
                if (Accesscontrollist.Count > 0)
                {
                    string ct = Accesscontrollist[0].Control.ToString();
                    LinkButton linkcontrol = (LinkButton)e.Row.FindControl(ct);
                    if (Accesscontrollist[0].Mode == true)
                        if (ct == "lnkEdit")
                            lnkedit.Visible = true;
                  
                }
            }
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindGV_Module(txtModuleTitle.Text);

            Session["search"] = ddlType.SelectedValue + "," + txtModuleTitle.Text;
        }

        protected void btnInactive_Click(object sender, EventArgs e)
        {
            avii.Classes.ModuleUtility obj = new avii.Classes.ModuleUtility();
            foreach (GridViewRow row in GV_Module.Rows)
            {
                CheckBox chkmodule = (CheckBox)row.FindControl("chkmodule");
                HiddenField hdnModuleGUID = (HiddenField)row.FindControl("hdnModuleGUID");
                HiddenField hdnParentModuleGUID = (HiddenField)row.FindControl("hdnParentModuleGUID");
                if (chkmodule.Checked)
                {
                    obj.DeleteModule(Convert.ToInt32(hdnModuleGUID.Value), 0);
                }
            }
            bindGV_Module(txtModuleTitle.Text);
            lblMsg.Text = "Modules Deactivated Sucessfully!";
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            avii.Classes.ModuleUtility obj = new avii.Classes.ModuleUtility();
            foreach (GridViewRow row in GV_Module.Rows)
            {
                CheckBox chkmodule = (CheckBox)row.FindControl("chkmodule");
                HiddenField hdnModuleGUID = (HiddenField)row.FindControl("hdnModuleGUID");
                HiddenField hdnParentModuleGUID = (HiddenField)row.FindControl("hdnParentModuleGUID");
                if (chkmodule.Checked)
                {
                    obj.DeleteModule(Convert.ToInt32(hdnModuleGUID.Value), 1);
                }
            }
            bindGV_Module(txtModuleTitle.Text);
            lblMsg.Text = "Modules deleted sucessfully!";

        }


        protected void chkmodule_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GV_Module.Rows)
            {

                CheckBox chkmodule = (CheckBox)row.FindControl("chkmodule");
                HiddenField hdnModuleGUID = (HiddenField)row.FindControl("hdnModuleGUID");
                HiddenField hdnModuleparentGUID = (HiddenField)row.FindControl("hdnParentModuleGUID");

                int moduleGUID = 9999;
                int moduleGUID2 = 9999;
                int moduleGUID3 = 9999;
                if (chkmodule.Checked)
                {

                    moduleGUID = Convert.ToInt32(hdnModuleGUID.Value);

                    if (moduleGUID == Convert.ToInt32(hdnModuleparentGUID.Value))
                    {
                        chkmodule.Checked = true;
                        moduleGUID2 = Convert.ToInt32(hdnModuleGUID.Value);

                    }
                    if (moduleGUID2 == Convert.ToInt32(hdnModuleparentGUID.Value))
                    {
                        chkmodule.Checked = true;
                        moduleGUID3 = Convert.ToInt32(hdnModuleGUID.Value);
                    }
                    if (moduleGUID3 == Convert.ToInt32(hdnModuleparentGUID.Value))
                    {
                        chkmodule.Checked = true;

                    }


                }
                else
                {

                    moduleGUID = Convert.ToInt32(hdnModuleGUID.Value);



                    if (moduleGUID == Convert.ToInt32(hdnModuleparentGUID.Value))
                    {
                        chkmodule.Checked = false;
                        moduleGUID2 = Convert.ToInt32(hdnModuleGUID.Value);
                    }
                    if (moduleGUID2 == Convert.ToInt32(hdnModuleparentGUID.Value))
                    {
                        chkmodule.Checked = false;
                        moduleGUID3 = Convert.ToInt32(hdnModuleGUID.Value);
                    }
                    if (moduleGUID3 == Convert.ToInt32(hdnModuleparentGUID.Value))
                    {
                        chkmodule.Checked = false;

                    }

                }

            }
        }

        protected void chkmodule_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkmoduleclicked=(CheckBox)sender;
            int moduleGUID = 9999;
            int moduleGUID2 = 9999;
            int moduleGUID3 = 9999;
            foreach (GridViewRow row in GV_Module.Rows)
            {

                CheckBox chkmodule = (CheckBox)row.FindControl("chkmodule");
                HiddenField hdnModuleGUID = (HiddenField)row.FindControl("hdnModuleGUID");
                HiddenField hdnModuleparentGUID = (HiddenField)row.FindControl("hdnParentModuleGUID");

               
                if (chkmoduleclicked.Checked)
                {
                    if (chkmodule.Checked)
                    {

                    moduleGUID = Convert.ToInt32(hdnModuleGUID.Value);
                    }

                    if (moduleGUID == Convert.ToInt32(hdnModuleparentGUID.Value))
                    {
                        chkmodule.Checked = true;
                        moduleGUID2 = Convert.ToInt32(hdnModuleGUID.Value);

                    }
                    if (moduleGUID2 == Convert.ToInt32(hdnModuleparentGUID.Value))
                    {
                        chkmodule.Checked = true;
                        moduleGUID3 = Convert.ToInt32(hdnModuleGUID.Value);
                    }
                    if (moduleGUID3 == Convert.ToInt32(hdnModuleparentGUID.Value))
                    {
                        chkmodule.Checked = true;

                    }
                }

                else if (!chkmoduleclicked.Checked)
                {
                    if(!chkmodule.Checked)
                    moduleGUID = Convert.ToInt32(hdnModuleGUID.Value);



                    if (moduleGUID == Convert.ToInt32(hdnModuleparentGUID.Value))
                    {
                        chkmodule.Checked = false;
                        moduleGUID2 = Convert.ToInt32(hdnModuleGUID.Value);
                    }
                    if (moduleGUID2 == Convert.ToInt32(hdnModuleparentGUID.Value))
                    {
                        chkmodule.Checked = false;
                        moduleGUID3 = Convert.ToInt32(hdnModuleGUID.Value);
                    }
                    if (moduleGUID3 == Convert.ToInt32(hdnModuleparentGUID.Value))
                    {
                        chkmodule.Checked = false;

                    }
                }

            }
                
        
        }

        protected void btnsearchModule_Click(object sender, EventArgs e)
        {
            bindGV_Module(txtModuleTitle.Text);
            Session["search"] = ddlType.SelectedValue+","+txtModuleTitle.Text;
            
        }

        protected void lnkEdit_Click(object sender, CommandEventArgs e)
        {
            int ModuleGUID = Convert.ToInt32(e.CommandArgument);
            LinkButton linkEdit = (LinkButton)sender;
            linkEdit.PostBackUrl = "addmodules.aspx?Moduleid=" + ModuleGUID;
        }

        
    }

}

