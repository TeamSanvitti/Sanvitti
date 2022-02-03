
namespace avii.Classes
{
    public class MenuItem
    {
        private int _moduleguid;
        private string _moduleName;
        private string _title;
        private string _url;
        private bool _active;
        private int _moduleparentGUID;
        private bool _isitem;
        private string _usertype;
        private bool _isSubMenu;
        public bool IsReadOnly { get; set; }
        public bool IsLink { get; set; }
        public bool IsSubMenu
        {
            get
            {
                return _isSubMenu;
            }
            set
            {
                _isSubMenu = value;
            }
        }
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
}