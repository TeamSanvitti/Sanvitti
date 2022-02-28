using System;
using System.Collections.Generic;
using System.Text;

namespace SV.Framework.Models.Catalog
{

    public class attributevalue
    {
        
        private int _attributevalueguid;
        private int _attributeguid;
        private int _productguid;
        private string _attributevalue;
        private string _attributename;

        public int AttributeValueGuid
        {
            get
            {
                return _attributevalueguid;
            }
            set
            {
                _attributevalueguid = value;
            }

        }
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
        public int ProductGuid
        {

            get
            {
                return _productguid;
            }
            set
            {
                _productguid = value;
            }
        }
        public string AttributeValue
        {
            get
            {
                return _attributevalue;
            }
            set
            {
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
}
