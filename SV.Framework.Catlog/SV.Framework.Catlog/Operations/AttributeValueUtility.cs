using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using SV.Framework.Models.Catalog;
using SV.Framework.Models.Common;

namespace SV.Framework.Catalog
{
    public class AttributeValueUtility : BaseCreateInstance
    {
        public void createAttributesValues(int attributevalueGuid, int attributeGuid, int productGuid, string attributeValue)
        {
            SV.Framework.DAL.Catalog.AttributeValueUtility attributeValueUtility = SV.Framework.DAL.Catalog.AttributeValueUtility.CreateInstance<SV.Framework.DAL.Catalog.AttributeValueUtility>();
            attributeValueUtility.createAttributesValues(attributevalueGuid, attributeGuid, productGuid, attributeValue);
        }
        public List<attributevalue> getattributevalueList(int attributevalueGuid, int attributeGuid, int productGuid, string attributeValue)
        {
            SV.Framework.DAL.Catalog.AttributeValueUtility attributeValueUtility = SV.Framework.DAL.Catalog.AttributeValueUtility.CreateInstance<SV.Framework.DAL.Catalog.AttributeValueUtility>();

            List<attributevalue> attributevalueList = attributeValueUtility.getattributevalueList(attributevalueGuid,  attributeGuid,  productGuid,  attributeValue);// new List<attributevalue>();
            return attributevalueList;
        }
        public void Deleteattributevalue(int attributevalueGuid)
        {
            SV.Framework.DAL.Catalog.AttributeValueUtility attributeValueUtility = SV.Framework.DAL.Catalog.AttributeValueUtility.CreateInstance<SV.Framework.DAL.Catalog.AttributeValueUtility>();
            attributeValueUtility.Deleteattributevalue(attributevalueGuid);

        }
    }
}
