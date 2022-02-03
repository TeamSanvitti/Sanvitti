using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace avii.Classes.UserManagement
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

        public UserInfo(int GUID)
        {
            userGuid = GUID;
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
    }

    public class ContactInfo
    {
        private string contactName;
        private string address;
        private string city;
        private string state;
        private string zip;
        private string homePhone;
        private string workPhone;
        private string cellPhone;
        private string email1;
        private string email2;

        public string ContactName
        {
            get
            {
                return contactName;
            }
            set
            {
                contactName = value;
            }
        }
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
            }
        }
        public string City
        {
            get
            {
                return city;
            }
            set
            {
                city = value;
            }
        }
        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;
            }
        }
        public string Zip
        {
            get
            {
                return zip;
            }
            set
            {
                zip = value;
            }
        }
        public string HomePhone
        {
            get
            {
               return homePhone;
            }
            set
            {
                homePhone = value;
            }
        }
        public string CellPhone
        {
            get
            {
                return cellPhone;
            }
            set
            {
                cellPhone = value;
            }
        }
        public string WorkPhone
        {
            get
            {
                return workPhone;
            }
            set
            {
                workPhone = value;
            }
        }
        public string Email1
        {
            get
            {
                return email1;
            }
            set
            {
                email1 = value;
            }
        }
        public string Email2
        {
            get
            {
                return email2;
            }
            set
            {
                email2 = value;
            }
        }

    }

    public class UserRole
    {
        private int roleGUID;
        private string roleName;
        private List<UserModule> activeModules;

        public UserRole(int guid)
        {
            roleGUID = guid;
        }

        public int RoleGUID
        {
            get
            {
                return roleGUID;
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

        public UserPermission(int guid)
        {
            permGUID = guid;
        }

        public int PermissionGUID
        {
            get
            {
                return permGUID;
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

            public UserModule(int guid)
            {
                modGUID = guid;
            }

            public int PermissionGUID
            {
                get
                {
                    return modGUID;
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

    public class SalesPerson
    {
        private string employeeNumber;
        private ContactInfo contactInfo;

        public string EmployeeNumber
        {
            get
            {
                return employeeNumber;
            }
            set
            {
                employeeNumber = value;
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
    }


}
