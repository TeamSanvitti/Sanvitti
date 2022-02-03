using System;
using System.Collections.Generic;
using System.Xml;

namespace avii.Classes
{
    public class menuUtility
    {
        public string menuxml(string roleguids, string level)
        {
            string menu_string = string.Empty;
            avii.Classes.DBConnect db = new avii.Classes.DBConnect();
            XmlDocument doc = new XmlDocument();

            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);

            XmlNode menusNode = doc.CreateElement("menu");
            doc.AppendChild(menusNode);
            avii.Classes.MenuUtility objModule = new avii.Classes.MenuUtility();
            List<avii.Classes.Module> objModulelist = objModule.getModuleList(-1, 0, roleguids, level);
            for (int i = 0; i < objModulelist.Count; i++)
            {
                XmlNode menuNode = doc.CreateElement("item");

                XmlAttribute menuAttribute = doc.CreateAttribute("id");
                menuAttribute.Value = objModulelist[i].ModuleName;
                menuNode.Attributes.Append(menuAttribute);
                XmlAttribute menuAttribute1 = doc.CreateAttribute("text");
                menuAttribute1.Value = objModulelist[i].Title;
                menuNode.Attributes.Append(menuAttribute1);
                XmlAttribute menuAttribute2 = doc.CreateAttribute("url");
                menuAttribute2.Value = objModulelist[i].Url;
                menuNode.Attributes.Append(menuAttribute2);

                avii.Classes.ModuleUtility objModule1 = new avii.Classes.ModuleUtility();
                List<avii.Classes.Module> objModulelist1 = objModule.getModuleList(-1, Convert.ToInt32(objModulelist[i].ModuleGUID), roleguids, level);
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

                    List<avii.Classes.Module> objModulelist2 = objModule.getModuleList(-1, Convert.ToInt32(objModulelist1[j].ModuleGUID), roleguids, level);
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
                        
                        List<avii.Classes.Module> objModulelist3 = objModule.getModuleList(-1, Convert.ToInt32(objModulelist2[k].ModuleGUID), roleguids, level);
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
            menu_string = doc.InnerXml.Trim();
            return menu_string;

        }
    }

}


