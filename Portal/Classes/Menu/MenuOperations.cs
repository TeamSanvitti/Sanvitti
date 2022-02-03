using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;


namespace avii.Classes
{
    public class MenuOperations
    {
        public static string MenuHTML(string roleGuids, string level, out string allMenustring)
        {
            string menuString = "<ul class='navbar-nav mr-auto'>";
            string substring = string.Empty;
            allMenustring = string.Empty;

            List<avii.Classes.MenuItem> objModulelist00 = MenuOperations.GetModuleList(-1, 0, roleGuids, level);
            System.Web.HttpContext.Current.Session["MenuItems"] = objModulelist00;
           // string allmenus= string.Join(" ", objModulelist00);
            string allmenus = Newtonsoft.Json.JsonConvert.SerializeObject(objModulelist00);
            System.Web.HttpContext.Current.Session["allmenus"] = allmenus;

            List<avii.Classes.MenuItem> objModulelist = (from item in objModulelist00 where item.IsItem.Equals(true) select item).ToList();

            var objModulelist0 = (from item in objModulelist where item.ModuleParentGUID.Equals(0) select item).ToList();

            for (int i = 0; i < objModulelist0.Count; i++)
            {


                var objModulelist1 = (from item in objModulelist where item.ModuleParentGUID.Equals(objModulelist0[i].ModuleGUID) select item).ToList();

                if (objModulelist0[i].IsSubMenu)
                {
                    // 0 level
                    substring = substring + "<li class='dropdown nav-item'>";
                    substring = substring + "<a href='#' class='dropdown-toggle nav-link' data-toggle='dropdown'>" + objModulelist0[i].Title + "<b class='caret'></b></a> <ul class='dropdown-menu'>";
                    // 1 level
                    for (int j = 0; j < objModulelist1.Count; j++)
                    {
                        var objModulelist2 = (from item in objModulelist where item.ModuleParentGUID.Equals(objModulelist1[j].ModuleGUID) select item).ToList();

                        if (objModulelist1[j].IsSubMenu)
                        {
                            substring = substring + "<li class='nav-item'>";
                            substring = substring + "<a class='nav-link' href='#' >" + objModulelist1[j].Title + "<i class='icon-arrow-right'></i></a> <ul class='dropdown-menu sub-menu'>";
                            for (int k = 0; k < objModulelist2.Count; k++)
                            {
                                substring = substring + "<li class='nav-item'><a class='nav-link' href='" + objModulelist2[k].Url + "'" + "> " + objModulelist2[k].Title + "</a></li>";
                            }

                            substring = substring + "</ul></li>";
                        }
                        else
                        {
                            if (objModulelist1[j].IsLink)
                                substring = substring + "<li class='nav-item'><a target='_blank' class='nav-link' href='" + objModulelist1[j].Url + "'" + "> " + objModulelist1[j].Title + "</a></li>";
                            else
                                substring = substring + "<li class='nav-item'><a class='nav-link'  href='" + objModulelist1[j].Url + "'" + "> " + objModulelist1[j].Title + "</a></li>";
                        }
                    }

                    substring = substring + "</ul></li>";
                }
                else
                {
                    //0 level
                    substring = substring + "<li class='nav-item'><a class='nav-link' href='" + objModulelist0[i].Url + "'" + "> " + objModulelist0[i].Title + "</a></li>";

                    //if (objModulelist1[j].IsItem)
                    //{
                    //    substring = "<li>";
                    //    substring = substring + "<a href='#' >'" + objModulelist0[i].Title + "'<i class=" + "icon-arrow-right" + "></i></a> <ul class=" + "dropdown-menu sub-menu" + ">";


                    //    substring = substring + "</ul></li>";
                    //}
                    //else
                    //{
                    //    substring = substring + "<li><a  href='" + objModulelist0[i].Url + "'" + "> '" + objModulelist0[i].Title + "'</a></li>";
                    //}
                }





            }

            var objModulelist01 = (from item in objModulelist00 where item.ModuleParentGUID.Equals(0) select item).ToList();

            for (int i = 0; i < objModulelist01.Count; i++)
            {


                var objModulelist11 = (from item in objModulelist00 where item.ModuleParentGUID.Equals(objModulelist01[i].ModuleGUID) select item).ToList();

                if (objModulelist01[i].IsSubMenu)
                {
                    // 0 level
                    allMenustring = allMenustring + "<li class='dropdown nav-item'>";
                    allMenustring = allMenustring + "<a href='#' class='dropdown-toggle nav-link' data-toggle='dropdown'>" + objModulelist01[i].Title + "<b class='caret'></b></a> <ul class='dropdown-menu'>";
                    // 1 level
                    for (int j = 0; j < objModulelist11.Count; j++)
                    {
                        var objModulelist22 = (from item in objModulelist where item.ModuleParentGUID.Equals(objModulelist11[j].ModuleGUID) select item).ToList();

                        if (objModulelist11[j].IsSubMenu)
                        {
                            allMenustring = allMenustring + "<li class='nav-item'>";
                            allMenustring = allMenustring + "<a class='nav-link' href='#' >" + objModulelist11[j].Title + "<i class='icon-arrow-right'></i></a> <ul class='dropdown-menu sub-menu'>";
                            for (int k = 0; k < objModulelist22.Count; k++)
                            {
                                allMenustring = allMenustring + "<li class='nav-item'><a class='nav-link' href='" + objModulelist22[k].Url + "'" + "> " + objModulelist22[k].Title + "</a></li>";
                            }

                            allMenustring = allMenustring + "</ul></li>";
                        }
                        else
                        {
                            allMenustring = allMenustring + "<li class='nav-item'><a class='nav-link'  href='" + objModulelist11[j].Url + "'" + "> " + objModulelist11[j].Title + "</a></li>";
                        }
                    }

                    allMenustring = allMenustring + "</ul></li>";
                }
                else
                {
                    //0 level
                    allMenustring = allMenustring + "<li class='nav-item'><a class='nav-link' href='" + objModulelist01[i].Url + "'" + "> " + objModulelist01[i].Title + "</a></li>";
                                        
                }

                //allMenustring = "<ul class='navbar-nav mr-auto'>" + allMenustring;




            }


            if (level.ToLower() == "defaultmenu")
            { menuString = menuString + substring + "<li class='nav-item user_none'><a href = '../Logon.aspx' ><img src = '../img/user_logo.png' /></a></li></ul>"; 
             allMenustring = "<ul class='navbar-nav mr-auto'>" + allMenustring + "<li class='nav-item user_none'><a href = '../Logon.aspx' ><img src = '../img/user_logo.png' /></a></li></ul>"; }
            else
            { menuString = menuString + substring + "<li class='nav-item user_none'><a href = '../Logout.aspx' ><img src = '../img/user_logo2.png' /></a></li></ul>"; 
             allMenustring = "<ul class='navbar-nav mr-auto'>" + allMenustring + "<li class='nav-item user_none'><a href = '../Logout.aspx' ><img src = '../img/user_logo2.png' /></a></li></ul>"; }
            return menuString;

        }

        public static string MenuHTML(string roleGuids, string level)
        {
            string menuString = "<ul class='navbar-nav mr-auto'>";
            string substring = string.Empty;
            

            List<avii.Classes.MenuItem> objModulelist00 = MenuOperations.GetModuleList(-1, 0, roleGuids, level);

            List<avii.Classes.MenuItem> objModulelist = (from item in objModulelist00 where item.IsItem.Equals(true) select item).ToList();

            var objModulelist0 = (from item in objModulelist where item.ModuleParentGUID.Equals(0) select item).ToList();

            for (int i = 0; i < objModulelist0.Count; i++)
            {
                
                
                var objModulelist1 = (from item in objModulelist where item.ModuleParentGUID.Equals(objModulelist0[i].ModuleGUID) select item).ToList();

                if (objModulelist0[i].IsSubMenu)
                {
                    // 0 level
                    substring = substring + "<li class='dropdown nav-item'>";
                    substring = substring + "<a href='#' class='dropdown-toggle nav-link' data-toggle='dropdown'>" + objModulelist0[i].Title + "<b class='caret'></b></a> <ul class='dropdown-menu'>";
                    // 1 level
                    for (int j = 0; j < objModulelist1.Count; j++)
                    {
                        var objModulelist2 = (from item in objModulelist where item.ModuleParentGUID.Equals(objModulelist1[j].ModuleGUID) select item).ToList();

                        if (objModulelist1[j].IsSubMenu)
                        {
                            substring = substring + "<li class='nav-item'>";
                            substring = substring + "<a class='nav-link' href='#' >" + objModulelist1[j].Title + "<i class='icon-arrow-right'></i></a> <ul class='dropdown-menu sub-menu'>";
                            for (int k = 0; k < objModulelist2.Count; k++)
                            {
                                substring = substring + "<li class='nav-item'><a class='nav-link' href='" + objModulelist2[k].Url + "'" + "> " + objModulelist2[k].Title + "</a></li>";
                            }

                                substring = substring + "</ul></li>";
                        }
                        else
                        {
                            substring = substring + "<li class='nav-item'><a class='nav-link'  href='" + objModulelist1[j].Url + "'" + "> " + objModulelist1[j].Title + "</a></li>";
                        }
                    }

                    substring = substring + "</ul></li>";
                }
                else
                {
                    //0 level
                    substring = substring + "<li class='nav-item'><a class='nav-link' href='" + objModulelist0[i].Url + "'" + "> " + objModulelist0[i].Title + "</a></li>";

                    //if (objModulelist1[j].IsItem)
                    //{
                    //    substring = "<li>";
                    //    substring = substring + "<a href='#' >'" + objModulelist0[i].Title + "'<i class=" + "icon-arrow-right" + "></i></a> <ul class=" + "dropdown-menu sub-menu" + ">";


                    //    substring = substring + "</ul></li>";
                    //}
                    //else
                    //{
                    //    substring = substring + "<li><a  href='" + objModulelist0[i].Url + "'" + "> '" + objModulelist0[i].Title + "'</a></li>";
                    //}
                }

                

                
                
            }



            if (level.ToLower() == "defaultmenu")
                menuString = menuString + substring + "<li class='nav-item user_none'><a href = '../Logon.aspx' ><img src = '../img/user_logo.png' /></a></li></ul>";
            else
                menuString = menuString + substring + "<li class='nav-item user_none'><a href = '../Logout.aspx' ><img src = '../img/user_logo2.png' /></a></li></ul>";
            return menuString;

        }

        public static string MenuXML(string roleGuids, string level)
        {
            string menuuu = MenuHTML(roleGuids, level);
            string menuString = string.Empty;
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            XmlDocument doc = new XmlDocument();

            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode menusNode = doc.CreateElement("menu");
            doc.AppendChild(menusNode);
            
            List<avii.Classes.MenuItem> objModulelist = MenuOperations.GetModuleList(-1, 0, roleGuids, level);
            var objModulelist0 = (from item in objModulelist where item.ModuleParentGUID.Equals(0) select item).ToList();

            for (int i = 0; i < objModulelist0.Count; i++)
            {
                XmlNode menuNode = doc.CreateElement("item");

                XmlAttribute menuAttribute = doc.CreateAttribute("id");
                menuAttribute.Value = objModulelist0[i].ModuleName;
                menuNode.Attributes.Append(menuAttribute);
                XmlAttribute menuAttribute1 = doc.CreateAttribute("text");
                menuAttribute1.Value = objModulelist0[i].Title;
                menuNode.Attributes.Append(menuAttribute1);
                XmlAttribute menuAttribute2 = doc.CreateAttribute("url");
                menuAttribute2.Value = objModulelist0[i].Url;
                menuNode.Attributes.Append(menuAttribute2);

                //var poInfoList = (from item in objModulelist where item.ModuleParentGUID.Equals(objModulelist[i].ModuleGUID) select item).ToList();
                var objModulelist1 = (from item in objModulelist where item.ModuleParentGUID.Equals(objModulelist0[i].ModuleGUID) select item).ToList();
                //avii.Classes.ModuleUtility objModule1 = new avii.Classes.ModuleUtility();
                //List<avii.Classes.Module> objModulelist1 = objModule.getModuleList(-1, Convert.ToInt32(objModulelist[i].ModuleGUID), roleGuids, level);
                for (int j = 0; j < objModulelist1.Count; j++)
                {
                    XmlNode menuitem = doc.CreateElement("item");
                    XmlAttribute childAttribute = doc.CreateAttribute("id");
                    childAttribute.Value = objModulelist1[j].ModuleName;
                    menuitem.Attributes.Append(childAttribute);
                    XmlAttribute childAttribute1 = doc.CreateAttribute("text");
                    childAttribute1.Value = objModulelist1[j].Title;
                    menuitem.Attributes.Append(childAttribute1);
                    XmlAttribute childAttribute2 = doc.CreateAttribute("url");
                    childAttribute2.Value = objModulelist1[j].Url;
                    menuitem.Attributes.Append(childAttribute2);


                    var objModulelist2 = (from item in objModulelist where item.ModuleParentGUID.Equals(objModulelist1[j].ModuleGUID) select item).ToList();
                
                    //List<avii.Classes.Module> objModulelist2 = objModule.getModuleList(-1, Convert.ToInt32(objModulelist1[j].ModuleGUID), roleGuids, level);
                    for (int k = 0; k < objModulelist2.Count; k++)
                    {
                        XmlNode submenuitem = doc.CreateElement("item");
                        XmlAttribute subchildAttribute = doc.CreateAttribute("id");
                        subchildAttribute.Value = objModulelist2[k].ModuleName;
                        submenuitem.Attributes.Append(subchildAttribute);
                        XmlAttribute subchildAttribute1 = doc.CreateAttribute("text");
                        subchildAttribute1.Value = objModulelist2[k].Title;
                        submenuitem.Attributes.Append(subchildAttribute1);
                        XmlAttribute subchildAttribute2 = doc.CreateAttribute("url");
                        subchildAttribute2.Value = objModulelist2[k].Url;
                        submenuitem.Attributes.Append(subchildAttribute2);

                        var objModulelist3 = (from item in objModulelist where item.ModuleParentGUID.Equals(objModulelist2[k].ModuleGUID) select item).ToList();
                
                        //List<avii.Classes.Module> objModulelist3 = objModule.getModuleList(-1, Convert.ToInt32(objModulelist2[k].ModuleGUID), roleGuids, level);
                        for (int l = 0; l < objModulelist3.Count; l++)
                        {
                            XmlNode _submenuitem = doc.CreateElement("item");
                            XmlAttribute _subchildAttribute = doc.CreateAttribute("id");
                            _subchildAttribute.Value = objModulelist3[l].ModuleName;
                            _submenuitem.Attributes.Append(_subchildAttribute);
                            XmlAttribute _subchildAttribute1 = doc.CreateAttribute("text");
                            _subchildAttribute1.Value = objModulelist3[l].Title;
                            _submenuitem.Attributes.Append(_subchildAttribute1);
                            XmlAttribute _subchildAttribute2 = doc.CreateAttribute("url");
                            _subchildAttribute2.Value = objModulelist3[l].Url;
                            _submenuitem.Attributes.Append(_subchildAttribute2);

                            submenuitem.AppendChild(_submenuitem);

                        }
                        menuitem.AppendChild(submenuitem);

                    }
                    menuNode.AppendChild(menuitem);
                }
                menusNode.AppendChild(menuNode);
            }

            doc.Save(Console.Out);
            menuString = doc.InnerXml.Trim();
            return menuString;

        }
        public static List<MenuItem> GetModuleList(int moduleGUID, int moduleparentGUID, string roleguids, string level)
        {
            List<MenuItem> menuItemList = new List<MenuItem>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            int subMenu = 0;
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@moduleGUID", moduleGUID);
                objCompHash.Add("@moduleparentGUID", moduleparentGUID);
                objCompHash.Add("@roleguids", roleguids);
                objCompHash.Add("@level", level);

                arrSpFieldSeq = new string[] { "@moduleGUID", "@moduleparentGUID", "@roleguids", "@level" };

                dataTable = db.GetTableRecords(objCompHash, "av_MenuModule", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        MenuItem objModule = new MenuItem();
                        objModule.ModuleGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleGUID", 0, false));
                        objModule.ModuleParentGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "moduleparentGUID", 0, false));
                        objModule.ModuleName = clsGeneral.getColumnData(dataRow, "moduleName", string.Empty, false) as string;
                        objModule.Title = clsGeneral.getColumnData(dataRow, "title", string.Empty, false) as string;
                        objModule.Url = clsGeneral.getColumnData(dataRow, "url", string.Empty, false) as string;
                        objModule.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                        objModule.IsItem = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "isitem", false, false));
                        objModule.IsLink = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsLink", false, false));
                        objModule.IsReadOnly = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "IsReadOnly", false, false));
                        subMenu = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "SubMenu", 0, false));

                        objModule.IsSubMenu = subMenu > 0 ? true : false;

                        menuItemList.Add(objModule);
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

            return menuItemList;
        }
    }
}