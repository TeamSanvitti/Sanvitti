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
    public static class EsnManagementDB
    {
        public static List<clsEsnAssignment> GerPurchaseOrders_WithESN(string PONumber, string POFromDate, string POToDate, int UserID, int CompanyID,
                                                     int statusId, string esn, string MslNumber, int esnflag, int mdnflag)
        {
            List<clsEsnAssignment> lstEsnmgmt = new List<clsEsnAssignment>();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dataTable = new DataTable();

            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@Po_Num", PONumber);
                objCompHash.Add("@From_Date", POFromDate);
                objCompHash.Add("@To_Date", POToDate);
                objCompHash.Add("@StatusID", statusId);
                objCompHash.Add("@CompanyID", CompanyID);
                objCompHash.Add("@UserID", UserID);
                objCompHash.Add("@esn", esn);
                objCompHash.Add("@MslNumber", MslNumber);
                objCompHash.Add("@esnflag", esnflag);
                objCompHash.Add("@mdn", mdnflag);
                arrSpFieldSeq = new string[] { "@Po_Num", "@From_Date", "@To_Date", "@StatusID", "@CompanyID", "@UserID", "@esn", "@MslNumber", "@esnflag", "@mdn" };
                dataTable = db.GetTableRecords(objCompHash, "Aero_PurchaseOrder_Items_Select", arrSpFieldSeq);
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        clsEsnAssignment objesnmgmt = new clsEsnAssignment();
                        objesnmgmt.Po_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "po_id", 0, false));
                        objesnmgmt.Pod_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "pod_id", 0, false));
                        objesnmgmt.PONumber = clsGeneral.getColumnData(dataRow, "Po_Num", string.Empty, false) as string;
                        objesnmgmt.PODate = Convert.ToDateTime(clsGeneral.getColumnData(dataRow, "po_date", null, false));
                        objesnmgmt.Itemcode = clsGeneral.getColumnData(dataRow, "item_code", string.Empty, false) as string;
                        objesnmgmt.Msid = clsGeneral.getColumnData(dataRow, "msid", string.Empty, false) as string;
                        objesnmgmt.MDN = clsGeneral.getColumnData(dataRow, "mdn", string.Empty, false) as string;
                        objesnmgmt.Passcode = clsGeneral.getColumnData(dataRow, "pass_code", string.Empty, false) as string;
                        objesnmgmt.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                        objesnmgmt.LineNo = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "line_no", 0, false));
                        objesnmgmt.esn = clsGeneral.getColumnData(dataRow, "esn", string.Empty, false) as string;
                        objesnmgmt.MslNumber = clsGeneral.getColumnData(dataRow, "MslNumber", string.Empty, false) as string;
                        objesnmgmt.FmUpc = clsGeneral.getColumnData(dataRow, "fmupc", string.Empty, false) as string;
                        objesnmgmt.CategoryFlag = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "categoryflag", 0, false));
                        lstEsnmgmt.Add(objesnmgmt);
                    }
                }

            }
            catch (Exception objExp)
            {
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                db.DBClose();
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }
            return lstEsnmgmt;
        }

        public static List<clsEsnAssignment> ValidateESN(List<clsEsnxml> esnAssignmentList)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            DataTable dataTable = new DataTable();
            Hashtable objCompHash = new Hashtable();
            List<clsEsnAssignment> esns = null;
            string esnListXML = string.Empty;
            try
            {
                esnListXML = SerializeObjetToXML(esnAssignmentList, "ArrayOfClsEsnxml", "clsEsnxml");
                if (!string.IsNullOrEmpty(esnListXML))
                {
                    objCompHash.Add("@EsnMslXml", esnListXML);
                    arrSpFieldSeq = new string[] { "@EsnMslXml" };
                    dataTable = db.GetTableRecords(objCompHash, "av_EsnMsl_Validation", arrSpFieldSeq);

                    esns = PopulateESNAssignment(dataTable);
                }
            }
            catch (Exception objExp)
            {
                //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return esns;
        }

        public static void InsertEsn(string sXml, int usertype)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@EsnMslXml", sXml);
                objCompHash.Add("@usertype", usertype);
                arrSpFieldSeq = new string[] { "@EsnMslXml", "@usertype" };
                db.ExeCommand(objCompHash, "av_EsnMslLog_insert_update", arrSpFieldSeq);
            }
            catch (Exception objExp)
            {
                //errorMessage = "CreatePurchaseOrderDB:" + objExp.Message.ToString();
                throw objExp;
            }
            finally
            {
                objCompHash = null;
                arrSpFieldSeq = null;
            }
        }

        public static string SerializeObjetToXML(object obj, string rootNodeName, string listName)
        {

            XmlSerializer objXMLSerializer = new XmlSerializer(obj.GetType());
            MemoryStream memstr = new MemoryStream();
            XmlTextWriter xmltxtwr = new XmlTextWriter(memstr, Encoding.UTF8);
            string sXML = "";
            try
            {
                objXMLSerializer.Serialize(xmltxtwr, obj);
                sXML = Encoding.UTF8.GetString(memstr.GetBuffer());
                sXML = "<" + rootNodeName + ">" + sXML.Substring(sXML.IndexOf("<" + listName + ">"));
                sXML = sXML.Substring(0, (sXML.LastIndexOf(Convert.ToChar(62)) + 1));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                xmltxtwr.Close();
                memstr.Close();
            }

            return sXML;
        }

        private static List<clsEsnAssignment> PopulateESNAssignment(DataTable dataTable)
        {
            List<clsEsnAssignment> esns = null;
            clsEsnAssignment esn = null;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                esns = new List<clsEsnAssignment>();
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    esn = new clsEsnAssignment();
                    esn.esn = clsGeneral.getColumnData(dataRow, "esn", string.Empty, false) as string;
                    esn.MslNumber = clsGeneral.getColumnData(dataRow, "msl", string.Empty, false) as string;
                    esn.FmUpc = clsGeneral.getColumnData(dataRow, "FMUPC", string.Empty, false) as string;
                    esn.Itemcode = clsGeneral.getColumnData(dataRow, "Item_code", string.Empty, false) as string;
                    //esn.LineNo = (Int32)clsGeneral.getColumnData(dataRow, "Line_No", 0, false);
                    esn.MDN = clsGeneral.getColumnData(dataRow, "MDN", string.Empty, false) as string;
                    esn.Msid = clsGeneral.getColumnData(dataRow, "MSID", string.Empty, false) as string;
                    esn.MslNumber = clsGeneral.getColumnData(dataRow, "MSL", string.Empty, false) as string;
                    esn.UPC = clsGeneral.getColumnData(dataRow, "UPC", string.Empty, false) as string;
                    esn.PODate = (DateTime)clsGeneral.getColumnData(dataRow, "PO_Date", DateTime.Now.ToShortDateString(), false);
                    esn.PONumber = clsGeneral.getColumnData(dataRow, "PO_Num", string.Empty, false) as string;
                    esn.Po_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "PO_ID", 0, false));
                    esn.Pod_id = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "Pod_id", 0, false));

                    esns.Add(esn);
                }
            }

            return esns;
        }
    }

}
