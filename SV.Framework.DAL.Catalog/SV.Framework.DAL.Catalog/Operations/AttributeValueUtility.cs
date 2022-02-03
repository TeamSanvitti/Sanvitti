using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace SV.Framework.DAL.Catalog
{
    public class AttributeValueUtility : BaseCreateInstance
    {
        public void createAttributesValues(int attributevalueGuid, int attributeGuid, int productGuid, string attributeValue)
        {
            using (DBConnect objDB = new DBConnect())
            {
                Hashtable objCompHash = new Hashtable();
                string[] arrSpFieldSeq;
                try
                {
                    objCompHash.Add("@attributevalueGuid", attributevalueGuid);
                    objCompHash.Add("@attributeGuid", attributeGuid);
                    objCompHash.Add("@productGuid", productGuid);
                    objCompHash.Add("@attributevalue", attributeValue);

                    arrSpFieldSeq = new string[] { "@attributevalueguid", "@attributeGuid", "@productGuid", "@attributevalue" };
                    objDB.ExecuteNonQuery(objCompHash, "av_productAttributevalue_insert_update", arrSpFieldSeq);
                }
                catch (Exception objExp)
                {
                    Logger.LogMessage(objExp, this); //throw new Exception(objExp.Message.ToString());
                }
                finally
                {
                    objDB.DBClose();
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }

        }
        public List<attributevalue> getattributevalueList(int attributevalueGuid, int attributeGuid, int productGuid, string attributeValue)
        {
            List<attributevalue> attributevalueList = default;// new List<attributevalue>();
            DataTable dataTable = default;// new DataTable();
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    objCompHash.Add("@attributevalueGuid", attributevalueGuid);
                    objCompHash.Add("@attributeGuid", attributeGuid);
                    objCompHash.Add("@productGuid", productGuid);
                    objCompHash.Add("@attributevalue", attributeValue);

                    arrSpFieldSeq = new string[] { "@attributevalueGuid", "@attributeGuid", "@productGuid", "@attributevalue" };
                    dataTable = db.GetTableRecords(objCompHash, "av_productattributevalue_select", arrSpFieldSeq);

                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        attributevalueList = new List<attributevalue>();

                        foreach (DataRow dataRow in dataTable.Rows)
                        {
                            attributevalue objattributevalue = new attributevalue();
                            objattributevalue.AttributeValueGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "attributevalueguid", 0, false));

                            objattributevalue.AttributeGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "attributeguid", 0, false));
                            objattributevalue.ProductGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "productguid", 0, false));
                            objattributevalue.AttributeValue = clsGeneral.getColumnData(dataRow, "attributevalue", string.Empty, false) as string;
                            objattributevalue.AttributeName = clsGeneral.getColumnData(dataRow, "attributename", string.Empty, false) as string;
                            attributevalueList.Add(objattributevalue);
                        }
                    }

                }
                catch (Exception ex)
                {
                    Logger.LogMessage(ex, this); //throw ex;
                }
                finally
                {
                    //db = null;
                    objCompHash = null;
                    arrSpFieldSeq = null;
                }
            }
            return attributevalueList;
        }
        public void Deleteattributevalue(int attributevalueGuid)
        {
            using (DBConnect db = new DBConnect())
            {
                string[] arrSpFieldSeq;
                Hashtable objCompHash = new Hashtable();
                try
                {
                    arrSpFieldSeq = new string[] { "@attributevalueGuid" };
                    objCompHash.Add("@attributevalueGuid", attributevalueGuid);
                    db.ExeCommand(objCompHash, "av_productattributevalue_delete", arrSpFieldSeq);
                }
                catch (Exception exp)
                {
                    Logger.LogMessage(exp, this); // throw exp;
                }
            }
        }
    }
}
