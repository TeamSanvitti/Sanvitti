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
        public static string MenuXML(string roleGuids, string level)
        {
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