using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;

namespace avii.Classes
{
    public class UserInfo
    {
        private int userGuid;
        private int companyGuid;
        private string userName;
        private string pwd;
        private ContactInfo contactInfo;
        private List<UserRole> activeRoles;
        private SalesPerson salesPerson;
        private string usertype;
        private string email;
        private string companyName;
        private string companyAccountNumber;
        
        private string menuxml;
        private List<CustomerEmail> customerEmails;
        private int signInID;
        private bool isEmail;
        private string logoPath;
        private string menuCss;
        private string styleCss;
        

        public UserInfo(int GUID)
        {
            userGuid = GUID;
        }
        public bool POCustNoValidate { get; set; }

        public string MenuCss
        {
            get
            {
                return menuCss;
            }
            set
            {
                menuCss = value;
            }
        }
        public string StyleCss
        {
            get
            {
                return styleCss;
            }
            set
            {
                styleCss = value;
            }
        }
        public string LogoPath
        {
            get
            {
                return logoPath;
            }
            set
            {
                logoPath = value;
            }
        }
        public bool IsEmail
        {
            get
            {
                return isEmail;
            }
            set
            {
                isEmail = value;
            }
        }
        public string CompanyAccountNumber
        {
            get
            {
                return companyAccountNumber;
            }
            set
            {
                companyAccountNumber = value;
            }
        }
        public int UserGUID
        {
            get
            {
                return userGuid;
            }
        }
        public int CompanyGUID
        {
            get
            {
                return companyGuid;
            }
            set
            {
                companyGuid = value;
            }
        }
        public int SignInID
        {
            get
            {
                return signInID;
            }
            set
            {
                signInID = value;
            }
        }
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                userName = value;
            }
        }
        public string UserPassword
        {
            get
            {
                return pwd;
            }
            set
            {
                pwd = value;
            }
        }
        public string CompanyName
        {
            get
            {
                return companyName;
            }
            set
            {
                companyName = value;
            }
        }
        public ContactInfo ContactInfo
        {
            get
            {
                return contactInfo;
            }
            set
            {
                contactInfo = value;
            }
        }
        public List<UserRole> ActiveRoles
        {
            get
            {
                return activeRoles;
            }
            set
            {
                activeRoles = value;
            }
        }
        public List<CustomerEmail> CustomerEmails
        {
            get
            {
                return customerEmails;
            }
            set
            {
                customerEmails = value;
            }
        }

        public string UserType
        {
            get
            {
                return usertype;
            }
            set
            {
                usertype = value;
            }
        }
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
            }
        }
        public string MenuXml
        {
            get
            {
                return menuxml;
            }
            set
            {
                menuxml = value;
            }
        }
        public string MenuList { get; set; }
    }

    //public class ContactInfo
    //{
    //    private string contactName;
    //    private string address;
    //    private string city;
    //    private string state;
    //    private string zip;
    //    private string homePhone;
    //    private string workPhone;
    //    private string cellPhone;
    //    private string email1;
    //    private string email2;

    //    public string ContactName
    //    {
    //        get
    //        {
    //            return contactName;
    //        }
    //        set
    //        {
    //            contactName = value;
    //        }
    //    }
    //    public string Address
    //    {
    //        get
    //        {
    //            return address;
    //        }
    //        set
    //        {
    //            address = value;
    //        }
    //    }
    //    public string City
    //    {
    //        get
    //        {
    //            return city;
    //        }
    //        set
    //        {
    //            city = value;
    //        }
    //    }
    //    public string State
    //    {
    //        get
    //        {
    //            return state;
    //        }
    //        set
    //        {
    //            state = value;
    //        }
    //    }
    //    public string Zip
    //    {
    //        get
    //        {
    //            return zip;
    //        }
    //        set
    //        {
    //            zip = value;
    //        }
    //    }
    //    public string HomePhone
    //    {
    //        get
    //        {
    //            return homePhone;
    //        }
    //        set
    //        {
    //            homePhone = value;
    //        }
    //    }
    //    public string CellPhone
    //    {
    //        get
    //        {
    //            return cellPhone;
    //        }
    //        set
    //        {
    //            cellPhone = value;
    //        }
    //    }
    //    public string WorkPhone
    //    {
    //        get
    //        {
    //            return workPhone;
    //        }
    //        set
    //        {
    //            workPhone = value;
    //        }
    //    }
    //    public string Email1
    //    {
    //        get
    //        {
    //            return email1;
    //        }
    //        set
    //        {
    //            email1 = value;
    //        }
    //    }
    //    public string Email2
    //    {
    //        get
    //        {
    //            return email2;
    //        }
    //        set
    //        {
    //            email2 = value;
    //        }
    //    }

    //}

    public class UserRole
    {
        private int roleGUID;
        private string roleName;
        private List<UserModule> activeModules;

        public UserRole()
        {
        }

        public int RoleGUID
        {
            get
            {
                return roleGUID;
            }
            set
            {
                roleGUID = value;
            }

        }

        public string RoleName
        {
            get
            {
                return roleName;
            }
            set
            {
                roleName = value;
            }
        }

        public List<UserModule> ActiveModules
        {
            get
            {
                return activeModules;
            }
            set
            {
                activeModules = value;
            }
        }

    }

    public class UserPermission
    {
        private int permGUID;
        private string permName;

        public UserPermission()
        {

        }

        public int PermissionGUID
        {
            get
            {
                return permGUID;
            }
            set
            {
                permGUID = value;
            }
        }

        public string PermissionName
        {
            get
            {
                return permName;
            }
            set
            {
                permName = value;
            }
        }

    }

    public class UserModule
    {
        private int modGUID;
        private string modName;
        private List<UserPermission> activePermissions;

        public UserModule()
        {

        }

        public int ModuleGUID
        {
            get
            {
                return modGUID;
            }
            set
            {
                modGUID = value;
            }
        }

        public string ModuleName
        {
            get
            {
                return modName;
            }
            set
            {
                modName = value;
            }
        }

        public List<UserPermission> ActivePermissions
        {
            get
            {
                return activePermissions;
            }
            set
            {
                activePermissions = value;
            }
        }
    }

    //public class SalesPerson
    //{
    //    private string employeeNumber;
    //    private ContactInfo contactInfo;

    //    public string EmployeeNumber
    //    {
    //        get
    //        {
    //            return employeeNumber;
    //        }
    //        set
    //        {
    //            employeeNumber = value;
    //        }
    //    }
    //    public ContactInfo ContactInfo
    //    {
    //        get
    //        {
    //            return contactInfo;
    //        }
    //        set
    //        {
    //            contactInfo = value;
    //        }
    //    }
    //}

    public class user_utility
    {
        public UserInfo getUserInfo(int userid)
        {
            UserInfo objUserInfo = new UserInfo(userid);
            //menuUtility objMenu = new menuUtility();

            string MenuList = string.Empty;
            ContactInfo objContactInfo = new ContactInfo();
            DataSet ds = new DataSet();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@CompanyID", -1);
                objCompHash.Add("@UserName", "");
                objCompHash.Add("@type", "");
                objCompHash.Add("@userid", userid);
                objCompHash.Add("@roleGuid", -1);
                objCompHash.Add("@userflag", 0);
                
                arrSpFieldSeq = new string[] { "@CompanyID", "@UserName", "@type", "@userid", "@roleGuid", "@userflag" };

                ds = db.GetDataSet(objCompHash, "aero_userinfo_select", arrSpFieldSeq);

                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            objUserInfo.POCustNoValidate = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "POCustNoValidate", false, false));
                            objUserInfo.CompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                            objUserInfo.SignInID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "SignInID", 0, false));
                            objUserInfo.CompanyAccountNumber = clsGeneral.getColumnData(dataRow, "CompanyAccountNumber", string.Empty, false) as string;
                            objUserInfo.IsEmail = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsEmail", false, false));
                            objUserInfo.LogoPath = clsGeneral.getColumnData(dataRow, "LogoPath", string.Empty, false) as string;
                            objUserInfo.MenuCss = clsGeneral.getColumnData(dataRow, "MenuCss", string.Empty, false) as string;
                            objUserInfo.StyleCss = clsGeneral.getColumnData(dataRow, "StyleCss", string.Empty, false) as string;
                            objUserInfo.ContactInfo = objContactInfo;
                            objUserInfo.ActiveRoles = getUserRolelist(userid);
                            List<avii.Classes.UserRole> roleList = objUserInfo.ActiveRoles;
                            string roleguids = string.Empty;
                            for (int m = 0; m < roleList.Count; m++)
                            {
                                if (roleguids == string.Empty)
                                    roleguids = roleList[m].RoleGUID.ToString();
                                else
                                    roleguids = roleguids + "," + roleList[m].RoleGUID.ToString();
                            }
                            objUserInfo.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                            objUserInfo.UserPassword = clsGeneral.getColumnData(dataRow, "pwd", string.Empty, false) as string;
                            objUserInfo.UserType = clsGeneral.getColumnData(dataRow, "usertype", string.Empty, false) as string;
                            objUserInfo.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                            objUserInfo.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                            //objUserInfo.MenuXml = objMenu.menuxml(roleguids, objUserInfo.UserType);
                            objUserInfo.MenuXml = MenuOperations.MenuHTML(roleguids, objUserInfo.UserType, out MenuList);
                            objUserInfo.MenuList = MenuList;

                            if (ds.Tables.Count > 1 && ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                                objUserInfo.CustomerEmails = PopulateCustomerEmail(ds.Tables[1]);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }


            return objUserInfo;
        }
        private List<CustomerEmail> PopulateCustomerEmail(DataTable dataTable)
        {
            List<CustomerEmail> customerEmails = new List<CustomerEmail>();
            try
            {

                
                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        CustomerEmail objCustomerEmail = new CustomerEmail();
                        objCustomerEmail.ModuleGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleGUID", 0, false));
                        objCustomerEmail.ModuleName = clsGeneral.getColumnData(dataRow, "title", string.Empty, false) as string; ;
                        objCustomerEmail.CompanyEmail = clsGeneral.getColumnData(dataRow, "CompanyEmail", string.Empty, false) as string;
                        objCustomerEmail.Email = clsGeneral.getColumnData(dataRow, "CustomerEmail", string.Empty, false) as string; ;
                        objCustomerEmail.OverrideEmail = clsGeneral.getColumnData(dataRow, "OverrideEmail", string.Empty, false) as string; ;


                        customerEmails.Add(objCustomerEmail);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            


            return customerEmails;
        }
        public List<UserInfo> getUserInfolist(int userid)
        {
            List<UserInfo> userlist = new List<UserInfo>();



            ContactInfo objContactInfo = new ContactInfo();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@UserName", "");
                objCompHash.Add("@CompanyID", -1);
                objCompHash.Add("@type", "");
                objCompHash.Add("@userid", userid);
                objCompHash.Add("@roleGuid", -1);
                objCompHash.Add("@userflag", 0);



                arrSpFieldSeq = new string[] { "@UserName", "@CompanyID", "@type", "@userid", "@roleGuid", "@userflag" };

                dataTable = db.GetTableRecords(objCompHash, "aero_userinfo_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        UserInfo objUserInfo = new UserInfo(userid);

                        objUserInfo.CompanyGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyGUID", 0, false));
                        objUserInfo.ContactInfo = objContactInfo;
                        objUserInfo.ActiveRoles = getUserRolelist(userid);
                        objUserInfo.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                        objUserInfo.UserPassword = clsGeneral.getColumnData(dataRow, "pwd", string.Empty, false) as string;
                        objUserInfo.UserType = clsGeneral.getColumnData(dataRow, "usertype", string.Empty, false) as string;
                        objUserInfo.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                        objUserInfo.CompanyName = clsGeneral.getColumnData(dataRow, "CompanyName", string.Empty, false) as string;
                        userlist.Add(objUserInfo);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }


            return userlist;
        }

        public List<UserRole> getUserRolelist(int UserID)
        {
            List<UserRole> UserRolelist = new List<UserRole>();
            List<UserModule> UserModulelist = new List<UserModule>();


            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@RoleGUID", -1);
                objCompHash.Add("@rolename", "");
                objCompHash.Add("@moduleGUIDs", "");
                objCompHash.Add("@active", 1);
                objCompHash.Add("@UserGuid", UserID);
                objCompHash.Add("@userroleflag", 1);
                objCompHash.Add("@uniqueflag", 0);

                arrSpFieldSeq = new string[] { "@RoleGUID", "@rolename", "@moduleGUIDs", "@active", "@UserGuid", "@userroleflag", "@uniqueflag" };

                dataTable = db.GetTableRecords(objCompHash, "av_role_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        UserRole objUserRole = new UserRole();
                        objUserRole.RoleGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "roleGUID", 0, false));
                        objUserRole.RoleName = clsGeneral.getColumnData(dataRow, "RoleName", string.Empty, false) as string; ;
                        objUserRole.ActiveModules = getModuleList(objUserRole.RoleGUID, true); ;

                        UserRolelist.Add(objUserRole);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }


            return UserRolelist;
        }

        public List<UserPermission> getPermissionList(int moduleGUID, int roleguid)
        {
            List<UserPermission> PermissionList = new List<UserPermission>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@PermissionGUID", 0);
                objCompHash.Add("@moduleGUID", moduleGUID);
                objCompHash.Add("@roleguid", roleguid);
                objCompHash.Add("@permissionname", "");
                arrSpFieldSeq = new string[] { "@PermissionGUID", "@moduleGUID", "@roleguid", "@permissionname" };

                dataTable = db.GetTableRecords(objCompHash, "av_Permission_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        UserPermission objPermission = new UserPermission();
                        objPermission.PermissionGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PermissionGUID", 0, false));
                        objPermission.PermissionName = clsGeneral.getColumnData(dataRow, "PermissionName", string.Empty, false) as string;

                        PermissionList.Add(objPermission);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return PermissionList;
        }


        public List<UserModule> getModuleList(int roleguid, bool active)
        {
            List<UserModule> moduleList = new List<UserModule>();

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@moduleGUID", -1);
                objCompHash.Add("@moduleparentGUID", -1);
                objCompHash.Add("@roleguid", roleguid);
                objCompHash.Add("@modulename", "");
                objCompHash.Add("@usertype", "");
                objCompHash.Add("@active", active);
                objCompHash.Add("@moduletitle", "");

                arrSpFieldSeq = new string[] { "@moduleGUID", "@moduleparentGUID", "@roleguid", "@modulename", "@usertype", "@active", "@moduletitle" };

                dataTable = db.GetTableRecords(objCompHash, "av_module_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        UserModule objModule = new UserModule();
                        objModule.ModuleGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleGUID", 0, false));
                        objModule.ModuleName = clsGeneral.getColumnData(dataRow, "moduleName", string.Empty, false) as string;
                        objModule.ActivePermissions = getPermissionList(objModule.ModuleGUID, roleguid);
                        moduleList.Add(objModule);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return moduleList;
        }


        public List<UserPermission> getUserPermissionList(string pagename, List<avii.Classes.UserRole> roleList, string entitytype)
        {

            List<avii.Classes.UserPermission> permissionlist = new List<avii.Classes.UserPermission>();
            List<avii.Classes.UserPermission> PermissionList = new List<UserPermission>();

            try
            {
                avii.Classes.user_utility objUser = new avii.Classes.user_utility();
                avii.Classes.AccessControlMappingUtility objAccesscontrol = new avii.Classes.AccessControlMappingUtility();
                List<avii.Classes.AccessControlMapping> controllist = objAccesscontrol.getmappingControls(pagename, -1, entitytype);

                for (int i = 0; i < roleList.Count; i++)
                {
                    if (controllist.Count > 0)
                    {

                        int moduleguid = controllist[0].ModuleGUID;
                        PermissionList = objUser.getPermissionList(moduleguid, roleList[i].RoleGUID);
                        for (int k = 0; k < PermissionList.Count; k++)
                        {
                            UserPermission objPermission = new UserPermission();
                            objPermission.PermissionGUID = PermissionList[k].PermissionGUID;
                            objPermission.PermissionName = PermissionList[k].PermissionName;
                            permissionlist.Add(objPermission);
                        }

                    }

                }



            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //db = null;
                //objCompHash = null;
                //arrSpFieldSeq = null;
            }

            return permissionlist;
        }

        public static List<avii.Classes.UserPermission> GetPermissions(string pageName)
        {
            
            avii.Classes.user_utility objUserUtility = new avii.Classes.user_utility();

            avii.Classes.UserInfo userList = (avii.Classes.UserInfo) System.Web.HttpContext.Current.Session["userInfo"];
            string entitytype = string.Empty;
            string usertype = System.Configuration.ConfigurationManager.AppSettings["UserType"];
            if (userList.UserType == usertype)
                entitytype = "adm";
            else
                entitytype = "usr";

            List<avii.Classes.UserRole> roleList = userList.ActiveRoles;

            List<avii.Classes.UserPermission> permissionList = objUserUtility.getUserPermissionList(pageName, roleList, entitytype);
            
            return permissionList;
        }
        public static void UserSignInUpdate(int signinID, int userID, string sessionID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();

            try
            {
                objCompHash.Add("@SignInID", signinID);
                objCompHash.Add("@UserID", userID);
                objCompHash.Add("@SessionStartDate", DateTime.Now);
                objCompHash.Add("@SessionEndDate", DateTime.Now);
                objCompHash.Add("@SessionID", sessionID);

                arrSpFieldSeq = new string[] { "@SignInID", "@UserID", "@SessionStartDate", "@SessionEndDate", "@SessionID" };
                db.ExeCommand(objCompHash, "sv_UserSignIn_CREATE_UPDATE", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw objExp;
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
    }

    public class AccessControlMapping
    {

        private int _controlmappingGuid;
        private string _pagename;
        private string _control;
        private string _parentControl;
        private string _controlType;
        private bool _mode;
        private int _permissionGuid;
        private int _moduleGUID;

        public int ControlmappingGuid
        {
            get
            {
                return _controlmappingGuid;
            }
            set
            {
                _controlmappingGuid = value;
            }

        }
        public string Pagename
        {
            get
            {
                return _pagename;
            }
            set
            {
                _pagename = value;
            }

        }
        public string Control
        {
            get
            {
                return _control;
            }
            set
            {
                _control = value;
            }

        }
        public string ParentControl
        {
            get
            {
                return _parentControl;
            }
            set
            {
                _parentControl = value;
            }

        }
        public string ControlType
        {
            get
            {
                return _controlType;
            }
            set
            {
                _controlType = value;
            }

        }
        public bool Mode
        {
            get
            {
                return _mode;
            }
            set
            {
                _mode = value;
            }

        }
        public int PermissionGuid
        {
            get
            {
                return _permissionGuid;
            }
            set
            {
                _permissionGuid = value;
            }
        }
        public int ModuleGUID
        {
            get
            {
                return _moduleGUID;
            }
            set
            {
                _moduleGUID = value;
            }
        }

    }
    public class AccessControlMappingUtility
    {
        public List<AccessControlMapping> getmappingControls(string pagename, int permissionGUID, string entitytype)
        {
            List<AccessControlMapping> accessControlList = new List<AccessControlMapping>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@pagename", pagename);
                objCompHash.Add("@entity_xid", permissionGUID);
                objCompHash.Add("@entitytype", entitytype);

                arrSpFieldSeq = new string[] { "@pagename", "@entity_xid", "@entitytype" };

                dataTable = db.GetTableRecords(objCompHash, "av_accessControlMap_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        AccessControlMapping objaccessControl = new AccessControlMapping();

                        objaccessControl.ControlmappingGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "ControlmappingGuid", 0, false));
                        objaccessControl.Control = clsGeneral.getColumnData(dataRow, "Control", string.Empty, false) as string;
                        objaccessControl.Pagename = clsGeneral.getColumnData(dataRow, "Page_name", string.Empty, false) as string;
                        objaccessControl.ControlType = clsGeneral.getColumnData(dataRow, "Control_Type", string.Empty, false) as string;
                        objaccessControl.ParentControl = clsGeneral.getColumnData(dataRow, "Parent_Control", string.Empty, false) as string;
                        objaccessControl.Mode = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "mode", false, false));
                        objaccessControl.PermissionGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "entity_xid", 0, false));
                        objaccessControl.ModuleGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleguid", 0, false));
                        accessControlList.Add(objaccessControl);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return accessControlList;
        }
    }


}
