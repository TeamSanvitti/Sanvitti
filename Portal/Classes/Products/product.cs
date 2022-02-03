using System;
using System.Collections;
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

namespace avii.Classes
{
    #region-Model Class
    public class product
    {
        private string _modelNum;

        private string _technology;
        private string _maker;
        private int _makerguid;
        private int _technologyid;
        public string ModelNum
        {
            get
            {
                return _modelNum;
            }
            set
            {
                _modelNum = value;
            }
        }


        public string Technology
        {
            get
            {
                return _technology;
            }
            set
            {
                _technology = value;
            }
        }
        public string Maker
        {
            get
            {
                return _maker;
            }
            set
            {
                _maker = value;
            }
        }
        public int MakerGuid
        {
            get
            {
                return _makerguid;
            }
            set
            {
                _makerguid = value;
            }
        }
        public int Technologyguid
        {
            get
            {
                return _technologyid;
            }
            set
            {
                _technologyid = value;
            }
        }
    }
    public class ItemCompanyAssign
    {
        private int _itemguid;
        private int _companyid;
        private string _sku;
        public int ItemGUID
        {
            get
            {
                return _itemguid;
            }
            set
            {
                _itemguid = value;
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
        public string SKU
        {
            get
            {
                return _sku;
            }
            set
            {
                _sku = value;
            }
        }
    }

    public class attribute
    {
        private int _attributeguid;
        private string _attributename;
        private bool _active;
        public int AttributeGuid
        {
            get
            {
                return _attributeguid;
            }
            set
            {
                _attributeguid = value;
            }
        }
        public string AttributeName
        {
            get
            {
                return _attributename;
            }
            set
            {
                _attributename = value;
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
    }
    public class attributevalue
    {
        private int _attributevalueguid;
        private int _attributeguid;
        private int _productguid;
        private string _attributevalue;
        private string _attributename;

        public int AttributeValueGuid
        {
            get {
                return _attributevalueguid;
            }
            set {
                _attributevalueguid = value;
            }
        
        }
        public int AttributeGuid
        {
            get {
                return _attributeguid;
            }
            set {
                _attributeguid = value;
            }
        }
        public int ProductGuid
        {

            get {
                return _productguid;
            }
            set {
                _productguid = value;
            }
        }
        public string AttributeValue
        {
            get {
                return _attributevalue;
            }
            set {
                _attributevalue = value;
            }
        }
        public string AttributeName
        {
            get
            {
                return _attributename;
            }
            set
            {
                _attributename = value;
            }
        }
    }
    #endregion 
    #region-Controller Class
    public class ProductUtility
    {
        public static List<product> GetMakerList(int technologyID)
        {
            List<product> makerlist = new List<product>();
            //ContactInfo objContactInfo = new ContactInfo();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@TechnologyID", technologyID);

                arrSpFieldSeq = new string[] { "@TechnologyID" };

                dataTable = db.GetTableRecords(objCompHash, "av_TechnoMaker_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        product objmaker = new product();

                        objmaker.MakerGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "makerguid", 0, false));
                        objmaker.Maker = clsGeneral.getColumnData(dataRow, "makername", string.Empty, false) as string;
                        makerlist.Add(objmaker);
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


            return makerlist;
        }
        public List<product> getMakerList()
        {
            List<product> makerlist = new List<product>();
            //ContactInfo objContactInfo = new ContactInfo();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@makerguid", -1);

                arrSpFieldSeq = new string[] { "@makerguid" };

                dataTable = db.GetTableRecords(objCompHash, "av_maker_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        product objmaker = new product();

                        objmaker.MakerGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "makerguid", 0, false));
                        objmaker.Maker = clsGeneral.getColumnData(dataRow, "makername", string.Empty, false) as string;
                        makerlist.Add(objmaker);
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


            return makerlist;
        }
        public List<ItemCompanyAssign> getItemCompanyAssignList(int companyID, string sku, int itemguid)
        {
            List<ItemCompanyAssign> ItemCompanyAssignList = new List<ItemCompanyAssign>();
            //ContactInfo objContactInfo = new ContactInfo();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@companyID", companyID);
                objCompHash.Add("@sku", sku);
                objCompHash.Add("@itemguid", itemguid);

                arrSpFieldSeq = new string[] { "@companyID", "@sku", "@itemguid" };

                dataTable = db.GetTableRecords(objCompHash, "av_ItemCompanyAssign_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        ItemCompanyAssign objItemCompanyAssign = new ItemCompanyAssign();

                        objItemCompanyAssign.ItemGUID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "itemguid", 0, false));
                        objItemCompanyAssign.CompanyID = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "CompanyID", 0, false));
                        objItemCompanyAssign.SKU = clsGeneral.getColumnData(dataRow, "sku", string.Empty, false) as string;
                        ItemCompanyAssignList.Add(objItemCompanyAssign);
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


            return ItemCompanyAssignList;
        }
        public List<product> getTechnologyList(int makerid, int technologyguid)
        {
            List<product> technologylist = new List<product>();
            //ContactInfo objContactInfo = new ContactInfo();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@makerguid", makerid);
                objCompHash.Add("@CarrierGUID", technologyguid);

                arrSpFieldSeq = new string[] { "@makerguid", "@CarrierGUID" };


                dataTable = db.GetTableRecords(objCompHash, "av_Carriers_Select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        product objtechnology = new product();

                        objtechnology.Technologyguid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "technologyid", 0, false));
                        objtechnology.Technology = clsGeneral.getColumnData(dataRow, "technology", string.Empty, false) as string;
                        technologylist.Add(objtechnology);
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


            return technologylist;
        }
        public List<product> getModelList(int technologyid, int makerid)
        {
            List<product> modellist = new List<product>();
            //ContactInfo objContactInfo = new ContactInfo();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {

                objCompHash.Add("@technologyid", technologyid);
                objCompHash.Add("@makerguid", makerid);

                arrSpFieldSeq = new string[] { "@technologyid", "@makerguid" };

                dataTable = db.GetTableRecords(objCompHash, "av_model_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        product objmodel = new product();

                        objmodel.ModelNum = clsGeneral.getColumnData(dataRow, "modelnumber", string.Empty, false) as string;
                        modellist.Add(objmodel);
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


            return modellist;
        }

        //public static DataTable GetCompanyWarehouseCode()
        //{
        //    DataTable dataTable = new DataTable();
        //    DBConnect db = new DBConnect();
        //    string[] arrSpFieldSeq;
        //    Hashtable objCompHash = new Hashtable();
        //    try
        //    {

        //        objCompHash.Add("@CompanyID", 0);


        //        arrSpFieldSeq = new string[] { "@CompanyID" };

        //        dataTable = db.GetTableRecords(objCompHash, "av_CompanyWarehouseCode_Select", arrSpFieldSeq);

                

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
        //    return dataTable;
        //}



    }
    public class AttributeUtility
    {
        public void createAttributes(int attributeGuid, string attributeName, bool active)
        {

            DBConnect objDB = new DBConnect();
            Hashtable objCompHash = new Hashtable();
            string[] arrSpFieldSeq;
            try
            {
                objCompHash.Add("@attributeGuid", attributeGuid);
                objCompHash.Add("@attributeName", attributeName);
                objCompHash.Add("@active", active);
                arrSpFieldSeq = new string[] { "@attributeGuid", "@attributeName", "@active" };
                objDB.ExecuteNonQuery(objCompHash, "av_productAttribute_insert_update", arrSpFieldSeq);
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
        public List<attribute> getattributeList(int attributeGUID, string attributename,int active)
        {
            List<attribute> attributeList = new List<attribute>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                objCompHash.Add("@attributeGUID", attributeGUID);
                objCompHash.Add("@attributename", attributename);
                objCompHash.Add("@active",active);

                arrSpFieldSeq = new string[] { "@attributeGUID","@attributename","@active" };

                dataTable = db.GetTableRecords(objCompHash, "av_productattribute_select", arrSpFieldSeq);

                if (dataTable != null && dataTable.Rows.Count > 0)
                {

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        attribute objattribute = new attribute();
                        objattribute.AttributeGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow, "attributeGUID", 0, false));
                        objattribute.AttributeName = clsGeneral.getColumnData(dataRow, "attributeName", string.Empty, false) as string;
                        objattribute.Active = Convert.ToBoolean(clsGeneral.getColumnData(dataRow, "active", false, false));
                        attributeList.Add(objattribute);
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

            return attributeList;
        }
        public void Deleteattribute(int attributeGUID)
        {
            DBConnect db = new DBConnect();
            string[] arrSpFieldSeq;
            Hashtable objCompHash = new Hashtable();
            try
            {
                arrSpFieldSeq = new string[] { "@attributeGUID" };
                objCompHash.Add("@attributeGUID", attributeGUID);
                db.ExeCommand(objCompHash, "av_productattribute_delete", arrSpFieldSeq);
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
    public class AttributeValueUtility
    {
        public void createAttributesValues(int attributevalueGuid,int attributeGuid,int productGuid,string attributeValue)
        {

            DBConnect objDB = new DBConnect();
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
                throw new Exception(objExp.Message.ToString());
            }
            finally
            {
                objDB.DBClose();
                objCompHash = null;
                arrSpFieldSeq = null;
            }


        }
        public List<attributevalue> getattributevalueList(int attributevalueGuid, int attributeGuid, int productGuid, string attributeValue)
        {
            List<attributevalue> attributevalueList = new List<attributevalue>();
            DataTable dataTable = new DataTable();
            DBConnect db = new DBConnect();
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

                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        attributevalue objattributevalue = new attributevalue();
                        objattributevalue.AttributeValueGuid = Convert.ToInt32(clsGeneral.getColumnData(dataRow,"attributevalueguid",0,false));

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
                throw ex;
            }
            finally
            {
                db = null;
                objCompHash = null;
                arrSpFieldSeq = null;
            }

            return attributevalueList;
        }
        public void Deleteattributevalue(int attributevalueGuid)
        {
            DBConnect db = new DBConnect();
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
                throw exp;
            }
        }
    }
    #endregion
}
