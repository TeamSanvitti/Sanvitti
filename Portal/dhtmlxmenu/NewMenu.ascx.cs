using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Configuration;

namespace avii.dhtmlxmenu
{
    public partial class NewMenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //avii.Classes.menuUtility obj = new avii.Classes.menuUtility();
            avii.Classes.UserInfo userList = (avii.Classes.UserInfo)Session["userInfo"];
            string usertype = System.Configuration.ConfigurationManager.AppSettings["UserType"];
            string company = System.Configuration.ConfigurationManager.AppSettings["company"];
            if (userList != null)
            {
                if (Session["usermenu"] == null)
                {
                    string controlentity = "";

                    if (userList.CompanyName == company)
                    {
                        controlentity = userList.CompanyName;
                    }
                    else if (userList.UserType == usertype)
                    {
                        controlentity = "adm";

                    }
                    else
                        controlentity = "usr";

                    string entitytype = string.Empty;

                    if (userList.UserType == usertype)
                        entitytype = "adm";
                    else
                        entitytype = "usr";
                    string adminFilter = Session["admFilter"] as string;
                    string roleguids = string.Empty;
                    avii.Classes.user_utility objUser = new avii.Classes.user_utility();

                    List<avii.Classes.UserPermission> permissionlist = new List<avii.Classes.UserPermission>();
                    List<avii.Classes.AccessControlMapping> Accesscontrollist = new List<avii.Classes.AccessControlMapping>();

                    {
                        List<avii.Classes.UserRole> roleList = userList.ActiveRoles;

                        for (int m = 0; m < roleList.Count; m++)
                        {
                            if (roleguids == string.Empty)
                                roleguids = roleList[m].RoleGUID.ToString();
                            else
                                roleguids = roleguids + "," + roleList[m].RoleGUID.ToString();
                        }

                        List<avii.Classes.UserModule> moduleList = new List<avii.Classes.UserModule>();

                        string[] pagename = Request.Url.Segments;
                        avii.Classes.AccessControlMappingUtility objAccesscontrol = new avii.Classes.AccessControlMappingUtility();
                        List<avii.Classes.AccessControlMapping> controllist = objAccesscontrol.getmappingControls(pagename[pagename.Length - 1].ToString(), -1, controlentity);
                        for (int l = 0; l < controllist.Count; l++)
                        {
                            string ct = controllist[l].Control.ToString();
                            Control all = new Control();
                            if (controllist[l].ParentControl == null || controllist[l].ParentControl == "")
                            {
                                all = Page.FindControl(ct);
                                if (all != null)
                                    all.Visible = false;
                            }

                        }

                        for (int i = 0; i < roleList.Count; i++)
                        {
                            moduleList = roleList[i].ActiveModules;
                            for (int j = 0; j < moduleList.Count; j++)
                            {
                                if (controllist.Count > 0 && controllist[0].ModuleGUID == moduleList[j].ModuleGUID)
                                {
                                    permissionlist = moduleList[j].ActivePermissions;
                                    for (int k = 0; k < permissionlist.Count; k++)
                                    {

                                        Accesscontrollist = objAccesscontrol.getmappingControls(pagename[pagename.Length - 1].ToString(), permissionlist[k].PermissionGUID, entitytype);
                                        Control controlid = new Control();
                                        if (Accesscontrollist.Count > 0)
                                        {
                                            for (int l = 0; l < Accesscontrollist.Count; l++)
                                            {
                                                string ct = Accesscontrollist[l].Control.ToString();
                                                controlid = Page.FindControl(ct);
                                                if (Accesscontrollist[l].Mode == true)
                                                    if (controlid != null)
                                                        controlid.Visible = true;
                                            }

                                        }

                                    }
                                }

                            }

                        }


                    }

                    hdnmenu.Value = userList.MenuXml;
                    Session["usermenu"] = userList.MenuXml;
                }
                else
                {
                    hdnmenu.Value = Session["usermenu"] as string;
                }

                if (hdnmenu.Value == "" || hdnmenu.Value == null)
                {
                    string loginurl = ConfigurationManager.AppSettings["url"].ToString();
                    Response.Redirect(loginurl);
                }


            }
            else
            {

                string defaultUser = System.Configuration.ConfigurationManager.AppSettings["defaultuser"];
                if (!string.IsNullOrEmpty(defaultUser))
                {
                    hdnmenu.Value = avii.Classes.MenuOperations.MenuXML(defaultUser, "defaultmenu");
                }
                else

                    hdnmenu.Value = avii.Classes.MenuOperations.MenuXML("2", "defaultmenu");


            }
        }
    }
}