using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace avii.Classes
{

    public class clsUserManagement
    {

        private int _userid;
        private string _userName;
        private string _password;
        private string _email;
        private bool _isAdmin;
        private string _tilaForm;
        private string _welcomeForm;
        private string _v2Form;
        private int _companyid;
        private int _accountStatusid;
        private string _aerovoiceAdminUserName;
        private string _userType;
        private bool _active;
        private string _companyName;
        private string _companyAccNo;
        private string _accstatus;
        private int _roleguid;
        private List<UserRoleMap> _userRoleList;

        private List<UserStoreMap> _userStoreList;

        public List<UserStoreMap> UserStoreList
        {
            get
            {
                return _userStoreList;
            }
            set
            {
                _userStoreList = value;
            }
        }


        public List<UserRoleMap> UserRoleList
        {
            get
            {
                return _userRoleList;
            }
            set
            {
                _userRoleList = value;
            }
        }
        public int AccountStatusID
        {
            get
            {
                return _accountStatusid;
            }
            set
            {
                _accountStatusid = value;
            }
        }

        public string AerovoiceAdminUserName
        {
            get
            {
                return _aerovoiceAdminUserName;
            }
            set
            {
                _aerovoiceAdminUserName = value;
            }
        }

        public int CompanyID
        {
            get
            {
                return _companyid;
            }
            set
            {
                _companyid = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public bool IsAdmin
        {
            get
            {
                return _isAdmin;
            }
            set
            {
                _isAdmin = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public string TilaForm
        {
            get
            {
                return _tilaForm;
            }
            set
            {
                _tilaForm = value;
            }
        }

        public string WelcomeForm
        {
            get
            {
                return _welcomeForm;
            }
            set
            {
                _welcomeForm = value;
            }
        }

        public string V2Form
        {
            get
            {
                return _v2Form;
            }
            set
            {
                _v2Form = value;
            }
        }

        public int UserID
        {
            get
            {
                return _userid;
            }
            set
            {
                _userid = value;
            }
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string UserType
        {
            get
            {
                return _userType;
            }
            set
            {
                _userType = value;
            }
        }
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }
        public string CompanyName
        {
            get
            {
                return _companyName;
            }
            set
            {
                _companyName = value;
            }
        }
        public string CompanyAccNo
        {
            get
            {
                return _companyAccNo;
            }
            set
            {
                _companyAccNo = value;
            }
        }
        public string AccStatus
        {
            get
            {
                return _accstatus;
            }
            set
            {
                _accstatus = value;
            }
        }
        public int RoleGuid
        {
            get
            {
                return _roleguid;
            }
            set
            {
                _roleguid = value;
            }
        }
    }
    public class Roles
    {
        private int _roleid;
        private string _roleName;
        private DateTime _createdDate;
        private DateTime _modifiedDate;
        private int _createdBy;
        private int _modifiedBy;
        private string _permission;
        private int _moduleid;
        private bool _active;

        public int RoleTypeID { get; set; }
        public int RoleID
        {
            get
            {
                return _roleid;
            }
            set
            {
                _roleid = value;
            }
        }

        public string RoleName
        {
            get
            {
                return _roleName;
            }
            set
            {
                _roleName = value;
            }
        }
        public string Permission
        {
            get
            {
                return _permission;
            }
            set
            {
                _permission = value;
            }
        }
        public int ModuleId
        {
            get
            {
                return _moduleid;
            }
            set
            {
                _moduleid = value;
            }
        }
        public DateTime CreatedDate
        {
            get
            {
                return _createdDate;
            }
            set
            {
                _createdDate = value;
            }
        }
        public DateTime ModifiedDate
        {
            get
            {
                return _modifiedDate;
            }
            set
            {
                _modifiedDate = value;
            }
        }
        public int CreatedBy
        {
            get
            {
                return _createdBy;
            }
            set
            {
                _createdBy = value;
            }
        }
        public int ModifiedBy
        {
            get
            {
                return _modifiedBy;
            }
            set
            {
                _modifiedBy = value;
            }
        }
        public bool active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }
    }
    public class Permission
    {
        private int _Permissionguid;
        private string _PermissionName;
        public int PermissionGUID
        {
            get
            {
                return _Permissionguid;
            }
            set
            {
                _Permissionguid = value;
            }
        }
        public string PermissionName
        {
            get
            {
                return _PermissionName;
            }
            set
            {
                _PermissionName = value;
            }
        }
    }
    public class Module
    {
        private int _moduleguid;
        private string _moduleName;
        private string _title;
        private string _url;
        private bool _active;
        private int _moduleparentGUID;
        private bool _isitem;
        private string _usertype;

        public int ModuleGUID
        {
            get
            {
                return _moduleguid;
            }
            set
            {
                _moduleguid = value;
            }
        }
        public int ModuleParentGUID
        {
            get
            {
                return _moduleparentGUID;
            }
            set
            {
                _moduleparentGUID = value;
            }
        }

        public string ModuleName
        {
            get
            {
                return _moduleName;
            }
            set
            {
                _moduleName = value;
            }
        }
        public string Url
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
            }
        }
        public string UserType
        {
            get
            {
                return _usertype;
            }
            set
            {
                _usertype = value;
            }
        }
        public bool Active
        {
            get
            {
                return _active;
            }
            set
            {
                _active = value;
            }
        }
        public bool IsItem
        {
            get
            {
                return _isitem;
            }
            set
            {
                _isitem = value;
            }
        }

    }
    public class RoleModulePermission
    {
        private int _roleid;
        private int _moduleid;
        private int _permissionid;
        public int RoleId
        {
            get
            {
                return _roleid;
            }
            set
            {
                _roleid = value;
            }

        }
        public int ModuleId
        {
            get
            {
                return _moduleid;
            }
            set
            {
                _moduleid = value;
            }

        }
        public int PermissionId
        {
            get
            {
                return _permissionid;
            }
            set
            {
                _permissionid = value;
            }

        }
    }

    [XmlRoot(ElementName = "userRoles", IsNullable = true)]
    public class UserRoleMap
    {
        private int? _userGuid;
        private int? _roleGuid;

        [XmlElement(ElementName = "userGUID", IsNullable = true)]
        public int? UserGuid
        {
            get { return _userGuid; }
            set { _userGuid = value; }
        }
        [XmlElement(ElementName = "roleGUID", IsNullable = true)]
        public int? RoleGuid
        {
            get { return _roleGuid; }
            set { _roleGuid = value; }
        }
    }
    public class UserUtility
    {
        public void DeleteUser(int userID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                arrSpFieldSeq = new string[] { "@UserID" };
                objCompHash.Add("@UserID", userID);
                db.ExeCommand(objCompHash, "Aero_UserInfo_Delete", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        private static string UTF8ByteArrayToString(byte[] characters)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            string constructedString = encoding.GetString(characters);
            return (constructedString);
        }
        public static string SerializeObject<T>(T obj)
        {
            try
            {
                string xmlString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(typeof(T));
                XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
                xs.Serialize(xmlTextWriter, obj);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                xmlString = UTF8ByteArrayToString(memoryStream.ToArray()); return xmlString.Trim();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static int InsertNewUser(clsUserManagement userObj)
        {
            int returnValue = 0;
            string roleXml = clsGeneral.SerializeObject(userObj.UserRoleList);
            string storeXml = clsGeneral.SerializeObject(userObj.UserStoreList);
            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                //if (!string.IsNullOrEmpty(userName))
                {
                    objCompHash.Add("@UserID", userObj.UserID);
                    objCompHash.Add("@Username", userObj.UserName);
                    objCompHash.Add("@Pwd", userObj.Password);
                    objCompHash.Add("@Email", userObj.Email);
                    objCompHash.Add("@CompanyID", userObj.CompanyID);
                    objCompHash.Add("@AccountStatusID", userObj.AccountStatusID);                
                    objCompHash.Add("@active", userObj.Active);
                    objCompHash.Add("@userType", userObj.UserType);
                    objCompHash.Add("@rolexml", roleXml);
                    objCompHash.Add("@StoreXml", storeXml);

                    arrSpFieldSeq = new string[] { "@UserID", "@Username", "@Pwd", "@Email", "@CompanyID", "@AccountStatusID", "@active", "@userType", "@rolexml", "@StoreXml" };
                   returnValue = db.ExecCommand(objCompHash, "Aero_UserInfo_insert_update", arrSpFieldSeq);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    userid = Convert.ToInt32(dt.Rows[0]["userguid"]);
                    //}
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return returnValue;
        }

        public int InsertNewUser1(int userid, string userName, string pwd, string emailAddress, string accountNumber, int accountStatusID, int companyID, bool active, string usertype)
        {

            DataTable dt = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    objCompHash.Add("@UserID", userid);
                    objCompHash.Add("@Username", userName);
                    objCompHash.Add("@Pwd", pwd);
                    objCompHash.Add("@Email", emailAddress);
                    objCompHash.Add("@CompanyID", companyID);
                    objCompHash.Add("@AccountStatusID", accountStatusID);
                    objCompHash.Add("@AccountNumber", accountNumber);

                    objCompHash.Add("@active", active);
                    objCompHash.Add("@userType", usertype);



                    arrSpFieldSeq = new string[] { "@UserID", "@Username", "@Pwd", "@Email", "@CompanyID", "@AccountStatusID", "@AccountNumber", "@active", "@userType" };


                    dt = db.GetTableRecords(objCompHash, "Aero_UserInfo_INSERTupdate", arrSpFieldSeq);
                    if (dt.Rows.Count > 0)
                    {
                        userid = Convert.ToInt32(dt.Rows[0]["userguid"]);
                    }
                }
            }
            catch (Exception exp)
            {
                throw exp;
            }
            return userid;
        }

        public void InsertUserRole(int Userid, int roleid)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {


                objCompHash.Add("@UserGuid", Userid);
                objCompHash.Add("@RoleGuid", roleid);


                arrSpFieldSeq = new string[] { "@UserGuid", "@RoleGuid" };

                db.ExecuteScalar(objCompHash, "av_UserRoleMap_insert", arrSpFieldSeq);


            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
        public static DataTable GetStatus()
        {
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq = null;
            string sCode = string.Empty;
            Hashtable objCompHash = new Hashtable();
            dataTable = db.GetTableRecords(objCompHash, "av_status_Select", arrSpFieldSeq);
            return dataTable;

        }
        public static clsUserManagement getUserInfo(string userName, int companyid, string type, int userid, int roleid, int userflag)
        {
            //clsUserManagement objUserList = new clsUserManagement();
            clsUserManagement objuser = new clsUserManagement();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyid);
                objCompHash.Add("@UserName", userName);
                objCompHash.Add("@type", type);
                objCompHash.Add("@userid", userid);
                objCompHash.Add("@roleguid", roleid);
                objCompHash.Add("@userflag", userflag);
                
                arrSpFieldSeq = new string[] { "@CompanyID", "@UserName", "@type", "@userid", "@roleguid", "@userflag" };

                dataTable = db.GetTableRecords(objCompHash, "Aero_UserInfo_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        

                        objuser.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                        objuser.Password = clsGeneral.getColumnData(dataRow, "pwd", string.Empty, false) as string;
                        objuser.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                        objuser.AccountStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AccountStatusID", 0, false));
                        objuser.CompanyName = clsGeneral.getColumnData(dataRow, "companyname", string.Empty, false) as string;
                        objuser.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        objuser.CompanyAccNo = clsGeneral.getColumnData(dataRow, "companyaccountnumber", string.Empty, false) as string;
                        objuser.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "userid", 0, false));
                        objuser.UserType = clsGeneral.getColumnData(dataRow, "usertype", string.Empty, false) as string;
                        objuser.AccStatus = clsGeneral.getColumnData(dataRow, "accstatus", string.Empty, false) as string;
                        objuser.RoleGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "roleguid", 0, false));
                        objuser.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", 0, false));
                        objuser.UserRoleList = getUsersRoleList(objuser.UserID);

                        //objUserList.Add(objuser);
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

            return objuser;
        }

        public List<clsUserManagement> getUserList(string userName, int companyid, string type, int userid, int roleid, int userflag, bool active)
        {
            List<clsUserManagement> objUserList = new List<clsUserManagement>();
            RoleUtility roleObj = new RoleUtility();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyid);
                objCompHash.Add("@UserName", userName);
                objCompHash.Add("@type", type);
                objCompHash.Add("@userid", userid);
                objCompHash.Add("@roleguid", roleid);
                objCompHash.Add("@userflag", userflag);
             

                arrSpFieldSeq = new string[] { "@CompanyID", "@UserName", "@type", "@userid", "@roleguid", "@userflag" };

                dataTable = db.GetTableRecords(objCompHash, "Aero_UserInfo_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        clsUserManagement objuser = new clsUserManagement();

                        objuser.UserName = clsGeneral.getColumnData(dataRow, "UserName", string.Empty, false) as string;
                        objuser.Password = clsGeneral.getColumnData(dataRow, "pwd", string.Empty, false) as string;
                        objuser.Email = clsGeneral.getColumnData(dataRow, "email", string.Empty, false) as string;
                        objuser.AccountStatusID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "AccountStatusID", 0, false));
                        objuser.CompanyName = clsGeneral.getColumnData(dataRow, "companyname", string.Empty, false) as string;
                        objuser.CompanyAccNo = clsGeneral.getColumnData(dataRow, "companyaccountnumber", string.Empty, false) as string;
                        objuser.UserID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "userid", 0, false));
                        objuser.UserType = clsGeneral.getColumnData(dataRow, "usertype", string.Empty, false) as string;
                        objuser.AccStatus = clsGeneral.getColumnData(dataRow, "accstatus", string.Empty, false) as string;
                        objuser.RoleGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "roleguid", 0, false));
                        objuser.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", 0, false));
                        objuser.UserRoleList = getUsersRoleList(objuser.UserID);

                        objUserList.Add(objuser);
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

            return objUserList;
        }

        public static List<UserRoleMap> getUsersRoleList(int userid)
        {
            List<UserRoleMap> objUserRoleList = new List<UserRoleMap>();
            //RoleUtility roleObj = new RoleUtility();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@userguid", userid);
                //objCompHash.Add("@roleguid", roleid);


                arrSpFieldSeq = new string[] { "@userguid" };

                dataTable = db.GetTableRecords(objCompHash, "av_UserRoleMap_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        UserRoleMap objuserRole = new UserRoleMap();

                        objuserRole.UserGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "userguid", 0, false));
                        objuserRole.RoleGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "roleguid", 0, false));

                        objUserRoleList.Add(objuserRole);
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

            return objUserRoleList;
        }

        
        public DataTable get_UserList(string userName, int companyid, string type, int userid, int roleid)
        {

            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@CompanyID", companyid);
                objCompHash.Add("@UserName", userName);
                objCompHash.Add("@type", userName);
                objCompHash.Add("@userid", userid);
                objCompHash.Add("@roleguid", roleid);
                objCompHash.Add("@userflag", 1);
                

                arrSpFieldSeq = new string[] { "@CompanyID", "@UserName", "@type", "@userid", "@roleguid", "@userflag" };

                dataTable = db.GetTableRecords(objCompHash, "Aero_UserInfo_select", arrSpFieldSeq);

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

            return dataTable;
        }
    }


    [XmlRoot(ElementName = "ModulePermission", IsNullable = true)]
    public class RoleModulePermissionMap
    {
        private int? moduleID;
        private string permissionID;

        [XmlElement(ElementName = "moduleGUID", IsNullable = true)]
        public int? ModuleID
        {
            get
            {
                return moduleID;
            }
            set
            {
                moduleID = value;
            }
        }
        [XmlElement(ElementName = "permissionGUID", IsNullable = true)]
        public string PermissionID
        {
            get
            {
                return permissionID;
            }
            set
            {
                permissionID = value;
            }
        }

    }
    [XmlRoot(ElementName = "item", IsNullable = true)]




    public class RoleUtility
    {
        protected List<RoleModulePermissionMap> objRoleModulePermissionList = new List<RoleModulePermissionMap>();

        #region serializeObjetToXMLString - generic function to serliaze an object to XML string
        public string serializeObjetToXMLString(object obj, string rootNodeName, string listName)
        {
            XmlSerializer objXMLSerializer = new XmlSerializer(obj.GetType());
            MemoryStream memstr = new MemoryStream();
            XmlTextWriter xmltxtwr = new XmlTextWriter(memstr, Encoding.UTF8);
            string sXML = "";

            objXMLSerializer.Serialize(xmltxtwr, obj);
            xmltxtwr.Close();
            memstr.Close();

            sXML = Encoding.UTF8.GetString(memstr.GetBuffer());
            sXML = "<" + rootNodeName + ">" + sXML.Substring(sXML.IndexOf("<" + listName + ">"));
            sXML = sXML.Substring(0, (sXML.LastIndexOf(Convert.ToChar(62)) + 1));

            return sXML;
        }

        # endregion

        # region createRoleModulePermissionList - creates the list of modules and permission objects to be passed to the stored proc as XML --
        public void createRoleModulePermissionList(int moduleID, string permissionID)
        {
            RoleModulePermissionMap objRoleModulepermissionTemp = new RoleModulePermissionMap();
            objRoleModulepermissionTemp.ModuleID = moduleID;
            objRoleModulepermissionTemp.PermissionID = permissionID;


            objRoleModulePermissionList.Add(objRoleModulepermissionTemp);

        }
        #endregion

        #region Create Admin -- Roles
        public int createRole(int roleID, string roleName, int userID, bool active, int roleTypeID)
        {
            int roleid = 0;
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            DataTable dt = new DataTable();
            string[] arrSpFieldSeq;
            try
            {

                //string ModuleermissionXml = serializeObjetToXMLString((object)this.objRoleModulePermissionList, "ArrayOfRoleModulePermissionMap", "RoleModulePermissionMap");

                objCompHash.Add("@RoleGUID", roleID);
                objCompHash.Add("@roleName", roleName);
                objCompHash.Add("@createdby", userID);
                objCompHash.Add("@ModifiedBy", userID);
                objCompHash.Add("@active", active);
                objCompHash.Add("@RoleTypeID", roleTypeID);
                //objCompHash.Add("@modileid", moduleid);
                // objCompHash.Add("@ModuleermissionXml", ModuleermissionXml);

                //arrSpFieldSeq = new string[] { "@RoleGUID", "@roleName", "@createdby", "@ModifiedBy", "@active" };
                arrSpFieldSeq = new string[] { "@RoleGUID", "@roleName", "@createdby", "@ModifiedBy", "@active", "@RoleTypeID" };
                //objDB.ExecuteNonQuery(objCompHash, "av_role_insertupdate1", arrSpFieldSeq);
                dt = objDB.GetTableRecords(objCompHash, "av_role_insertupdate", arrSpFieldSeq);
                if (dt.Rows.Count > 0)
                {
                    roleid = Convert.ToInt32(dt.Rows[0]["roleid"]);
                }
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return roleid;
        }
        public void mapPermission(int roleID, int moduleid, int permissionid, int flag)
        {
            //int roleid = 0;
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            DataTable dt = new DataTable();
            string[] arrSpFieldSeq;
            try
            {

                //string ModuleermissionXml = serializeObjetToXMLString((object)this.objRoleModulePermissionList, "ArrayOfRoleModulePermissionMap", "RoleModulePermissionMap");

                objCompHash.Add("@RoleGUID", roleID);
                objCompHash.Add("@Moduleguid", moduleid);
                objCompHash.Add("@Permissionguid", permissionid);
                objCompHash.Add("@flag", flag);
                //objCompHash.Add("@modileid", moduleid);
                // objCompHash.Add("@ModuleermissionXml", ModuleermissionXml);

                arrSpFieldSeq = new string[] { "@RoleGUID", "@Moduleguid", "@Permissionguid", "@flag" };
                objDB.ExecuteNonQuery(objCompHash, "[av_rolemodulepermissionmap_insert_update]", arrSpFieldSeq);


            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            // return roleid;
        }
        #endregion

        public List<Roles> getRoleList(int roleGUID, string roleName, bool active, string moduleid, int uniqueflag, string roleType="")
        {
            List<Roles> objRolesList = new List<Roles>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RoleGUID", roleGUID);
                objCompHash.Add("@rolename", roleName);
                objCompHash.Add("@moduleGUIDs", moduleid);
                objCompHash.Add("@active", active);
                objCompHash.Add("@userGuid", 0);
                objCompHash.Add("@userroleflag", 0);
                objCompHash.Add("@uniqueflag", uniqueflag);
                objCompHash.Add("@RoleType", roleType);

                arrSpFieldSeq = new string[] { "@RoleGUID", "@rolename", "@moduleGUIDs", "@active", "@userGuid", "@userroleflag", "@uniqueflag", "@RoleType" };
                //arrSpFieldSeq = new string[] { "@RoleGUID", "@rolename", "@moduleGUIDs", "@active", "@userGuid", "@userroleflag", "@uniqueflag" };

                dataTable = db.GetTableRecords(objCompHash, "av_role_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        Roles objRoles = new Roles();
                        objRoles.RoleID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "roleGUID", 0, false));
                        objRoles.RoleTypeID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RoleTypeID", 0, false));
                        objRoles.RoleName = clsGeneral.getColumnData(dataRow, "roleName", string.Empty, false) as string;
                        objRoles.active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                        objRolesList.Add(objRoles);
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

            return objRolesList;
        }
        public List<Roles> GetCustRoleList(int roleGUID)
        {
            List<Roles> objRolesList = new List<Roles>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RoleGUID", 0);
                



                arrSpFieldSeq = new string[] { "@RoleGUID" };

                dataTable = db.GetTableRecords(objCompHash, "av_Customer_role_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        Roles objRoles = new Roles();
                        objRoles.RoleID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "roleGUID", 0, false));
                        objRoles.RoleName = clsGeneral.getColumnData(dataRow, "roleName", string.Empty, false) as string;
                        objRoles.active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                        objRolesList.Add(objRoles);
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

            return objRolesList;
        }

        public List<RoleModulePermission> getpermissions(int roleGUID, int moduleid)
        {
            List<RoleModulePermission> objPermission = new List<RoleModulePermission>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RoleID", roleGUID);

                objCompHash.Add("@moduleID", moduleid);



                arrSpFieldSeq = new string[] { "@RoleID", "@moduleID" };

                dataTable = db.GetTableRecords(objCompHash, "av_permissionMap_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RoleModulePermission objRoles = new RoleModulePermission();
                        objRoles.RoleId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "roleGUID", 0, false));
                        objRoles.ModuleId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleGUID", 0, false));
                        objRoles.PermissionId = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "permissionguid", 0, false));

                        objPermission.Add(objRoles);
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

            return objPermission;
        }

        public void DeleteRole(int roleID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                arrSpFieldSeq = new string[] { "@RoleGUID" };
                objCompHash.Add("@RoleGUID", roleID);
                db.ExeCommand(objCompHash, "av_role_Delete", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


        public static List<RoleTypes> GetRoleTypes()
        {
            List<RoleTypes> roleTypeList = new List<RoleTypes>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@RoleTypeID", 0);
                                             
                arrSpFieldSeq = new string[] { "@RoleTypeID" };

                dataTable = db.GetTableRecords(objCompHash, "av_RoleType_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        RoleTypes objRoles = new RoleTypes();
                        objRoles.RoleTypeID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "RoleTypeID", 0, false));
                        objRoles.RoleType = clsGeneral.getColumnData(dataRow, "RoleType", string.Empty, false) as string;
                        roleTypeList.Add(objRoles);
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

            return roleTypeList;
        }


    }
    public class ModuleUtility
    {
        public List<Module> getModuleList(int moduleGUID, int moduleparentGUID, string modulename, string type, bool active, string moduletitle)
        {
            List<Module> moduleList = new List<Module>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@moduleGUID", moduleGUID);
                objCompHash.Add("@moduleparentGUID", moduleparentGUID);
                objCompHash.Add("@roleguid", 0);
                objCompHash.Add("@modulename", modulename);
                objCompHash.Add("@usertype", type);
                objCompHash.Add("@active", active);
                objCompHash.Add("@moduletitle", moduletitle);
                //objCompHash.Add("@search",0);
                arrSpFieldSeq = new string[] { "@moduleGUID", "@moduleparentGUID", "@roleguid", "@modulename", "@usertype", "@active", "@moduletitle" };

                dataTable = db.GetTableRecords(objCompHash, "av_module_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        Module objModule = new Module();
                        objModule.ModuleGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleGUID", 0, false));
                        objModule.ModuleParentGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleparentGUID", 0, false));
                        objModule.ModuleName = clsGeneral.getColumnData(dataRow, "moduleName", string.Empty, false) as string;
                        objModule.Title = clsGeneral.getColumnData(dataRow, "title", string.Empty, false) as string;
                        objModule.Url = clsGeneral.getColumnData(dataRow, "url", string.Empty, false) as string;
                        objModule.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                        objModule.IsItem = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "isitem", false, false));
                        objModule.UserType = clsGeneral.getColumnData(dataRow, "level", string.Empty, false) as string;
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
        //public List<Module> getModuleList1(int moduleGUID, int moduleparentGUID, string modulename, string type, bool active, string moduletitle)
        //{
        //    List<Module> moduleList = new List<Module>();
        //    DataTable dataTable = new DataTable();
        //    DBConnect db = new DBConnect();
        //    string[] arrSpFieldSeq;
        //    Hashtable objCompHash = new Hashtable();
        //    try
        //    {
        //        objCompHash.Add("@moduleGUID", moduleGUID);
        //        objCompHash.Add("@moduleparentGUID", moduleparentGUID);
        //        objCompHash.Add("@roleguid", 0);
        //        objCompHash.Add("@modulename", modulename);
        //        objCompHash.Add("@usertype", type);
        //        objCompHash.Add("@active", active);
        //        objCompHash.Add("@moduletitle", moduletitle);
        //        objCompHash.Add("@search", 1);
        //        arrSpFieldSeq = new string[] { "@moduleGUID", "@moduleparentGUID", "@roleguid", "@modulename", "@usertype", "@active", "@moduletitle", "@search" };

        //        dataTable = db.GetTableRecords(objCompHash, "av_module_select", arrSpFieldSeq);

        //        if (dataTable != null && dataTable.Rows.Count > 0)
        //        {

        //            foreach (DataRow dataRow in dataTable.Rows)
        //            {
        //                Module objModule = new Module();
        //                objModule.ModuleGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleGUID", 0, false));
        //                objModule.ModuleParentGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleparentGUID", 0, false));
        //                objModule.ModuleName = clsGeneral.getColumnData(dataRow, "moduleName", string.Empty, false) as string;
        //                objModule.Title = clsGeneral.getColumnData(dataRow, "title", string.Empty, false) as string;
        //                objModule.Url = clsGeneral.getColumnData(dataRow, "url", string.Empty, false) as string;
        //                objModule.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
        //                objModule.IsItem = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "isitem", false, false));
        //                objModule.UserType = clsGeneral.getColumnData(dataRow, "level", string.Empty, false) as string;
        //                moduleList.Add(objModule);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        db = null;
        //        objCompHash = null;
        //        arrSpFieldSeq = null;
        //    }

        //    return moduleList;
        //}
        #region getModuleTree
        public List<Module> getModuleTree(int ModuleID, bool withparent, int active, int parentcatID, bool withIndent, string type, bool isactive, string moduletitle)
        {
            DBConnect objDB = new DBConnect();
            if (0 == ModuleID)
                ModuleID = -1;
            List<Module> lstModuleTree = new List<Module>();

            traverseModules(ref lstModuleTree, getModuleList(ModuleID, parentcatID, "", type, isactive, moduletitle), 0, 0, withIndent);

            return lstModuleTree;
        }
        public List<Module> getModuleTree1(int ModuleID, bool withparent, int active, int parentcatID, bool withIndent, string type, bool isactive, string moduletitle, List<avii.Classes.Module> modulelist)
        {
            DBConnect objDB = new DBConnect();
            if (0 == ModuleID)
                ModuleID = -1;
            List<Module> lstModuleTree = new List<Module>();

            traverseModules(ref lstModuleTree, modulelist, 0, 0, withIndent);

            return lstModuleTree;
        }

        #endregion

        #region traverseCategories
        public List<Module> traverseModules(ref List<Module> itemModuleTree
            , List<Module> itemModuleList, int Moduleid, int depth, bool withIndent)
        {
            for (int ictr = 0; ictr < itemModuleList.Count; ictr++)
            {
                if (Moduleid == itemModuleList[ictr].ModuleParentGUID)
                {
                    for (int j = 0; j < depth; j++)
                    {
                        if (withIndent)
                        {
                            itemModuleList[ictr].Title = "&nbsp;&nbsp;&nbsp;" + itemModuleList[ictr].Title;
                            itemModuleList[ictr].ModuleName = "&nbsp;&nbsp;&nbsp;" + itemModuleList[ictr].ModuleName;
                        }
                        else
                        {
                            itemModuleList[ictr].Title = itemModuleList[ictr].Title;
                            itemModuleList[ictr].ModuleName = itemModuleList[ictr].ModuleName;

                        }
                    }

                    itemModuleTree.Add(itemModuleList[ictr]);
                    itemModuleTree = traverseModules(ref itemModuleTree, itemModuleList, itemModuleList[ictr].ModuleGUID, depth + 1, withIndent);
                }
            }
            return itemModuleTree;
        }
        #endregion
        #region Create Modules
        public int createModules(int moduleGUID, string moduleName, string title, string url, bool active, int moduleparentguid, bool isitem, string usertype)
        {
            int returnValue = 0;
            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@moduleGuid", moduleGUID);
                objCompHash.Add("@moduleName", moduleName);
                objCompHash.Add("@title", title);
                objCompHash.Add("@url", url);
                objCompHash.Add("@active", active);
                objCompHash.Add("@moduleparentguid", moduleparentguid);
                objCompHash.Add("@isitem", isitem);
                objCompHash.Add("@usertype", usertype);
                arrSpFieldSeq = new string[] { "@moduleGuid", "@moduleName", "@title", "@url", "@active", "@moduleparentguid", "@isitem", "@usertype" };
                returnValue = objDB.ExecCommand(objCompHash, "av_module_insertupdate", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return returnValue;
        }
        #endregion
        public void DeleteModule(int moduleID, int delete)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                arrSpFieldSeq = new string[] { "@ModuleGUID", "@delete" };
                objCompHash.Add("@ModuleGUID", moduleID);
                objCompHash.Add("@delete", delete);
                db.ExeCommand(objCompHash, "av_module_delete", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }


    }
    public class MenuUtility
    {
        public List<Module> getModuleList(int moduleGUID, int moduleparentGUID, string roleguids, string level)
        {
            List<Module> moduleList = new List<Module>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@moduleGUID", moduleGUID);
                objCompHash.Add("@moduleparentGUID", moduleparentGUID);
                objCompHash.Add("@roleguids", roleguids);
                objCompHash.Add("@level", level);

                arrSpFieldSeq = new string[] { "@moduleGUID", "@moduleparentGUID", "@roleguids", "@level" };

                dataTable = db.GetTableRecords(objCompHash, "av_menumodule", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        Module objModule = new Module();
                        objModule.ModuleGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleGUID", 0, false));
                        objModule.ModuleParentGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleparentGUID", 0, false));
                        objModule.ModuleName = clsGeneral.getColumnData(dataRow, "moduleName", string.Empty, false) as string;
                        objModule.Title = clsGeneral.getColumnData(dataRow, "title", string.Empty, false) as string;
                        objModule.Url = clsGeneral.getColumnData(dataRow, "url", string.Empty, false) as string;
                        objModule.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                        objModule.IsItem = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "isitem", false, false));
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

    }
    public class PermissionUtility
    {
        public List<Permission> getPermissionList(int PermissionGUID, string permissionname)
        {
            List<Permission> PermissionList = new List<Permission>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@PermissionGUID", PermissionGUID);
                objCompHash.Add("@moduleguid", 0);
                objCompHash.Add("@roleguid", 0);
                objCompHash.Add("@permissionname", permissionname);


                arrSpFieldSeq = new string[] { "@PermissionGUID", "@moduleguid", "@roleguid", "@permissionname" };

                dataTable = db.GetTableRecords(objCompHash, "av_Permission_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        Permission objPermission = new Permission();
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
        #region Create Permissions
        public void createPermissions(int PermissionGUID, string PermissionName)
        {

            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@PermissionGuid", PermissionGUID);
                objCompHash.Add("@PermissionName", PermissionName);

                arrSpFieldSeq = new string[] { "@PermissionGuid", "@PermissionName" };
                objDB.ExecuteNonQuery(objCompHash, "av_Permission_insertupdate", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }
        #endregion
        public void DeletePermission(int PermissionID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                arrSpFieldSeq = new string[] { "@PermissionGUID" };
                objCompHash.Add("@PermissionGUID", PermissionID);
                db.ExeCommand(objCompHash, "av_Permission_delete", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

    }

    public class RoleTypes
    {
        public int RoleTypeID { get; set; }
        public string RoleType { get; set; }
    }

}












