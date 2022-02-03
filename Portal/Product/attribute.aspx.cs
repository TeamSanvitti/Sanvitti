using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace avii.product
{
    public partial class attribute : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindAttribute();
              
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            avii.Classes.AttributeUtility objAttribute = new avii.Classes.AttributeUtility();
            if(hdnattributeGuid.Value==string.Empty)
            {
            objAttribute.createAttributes(-1, txtAttribute.Text, chkActive.Checked);
            }
            else{
                objAttribute.createAttributes(Convert.ToInt32(hdnattributeGuid.Value), txtAttribute.Text, chkActive.Checked);
            }
            reset();
            lblMessage.Text = "Attribute submit succesfully";
            bindAttribute();
        }
        protected void reset()
        {
            txtAttribute.Text = string.Empty;
            chkActive.Checked = false;
        }
        protected void bindAttribute()
        {
            avii.Classes.AttributeUtility objAttribute = new avii.Classes.AttributeUtility();
            List<avii.Classes.attribute> listAttribute = objAttribute.getattributeList(-1, "", -1);
            GvAttribute.DataSource = listAttribute;
            GvAttribute.DataBind();
        }
        protected void Delete_click(object sender, CommandEventArgs e)
        {
            avii.Classes.AttributeUtility objAttribute = new avii.Classes.AttributeUtility();
            int attributeguid = Convert.ToInt32(e.CommandArgument);
            objAttribute.Deleteattribute(attributeguid);
            bindAttribute();         
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            reset();
            lblMessage.Text = "";
        }

        protected void txtAttribute_TextChanged(object sender, EventArgs e)
        {
            avii.Classes.AttributeUtility objAttribute = new avii.Classes.AttributeUtility();
            List<avii.Classes.attribute> listAttribute = objAttribute.getattributeList(-1, txtAttribute.Text, -1);
            if (listAttribute.Count > 0)
            {
                lblMessage.Text = "Attribute already exists";
            }
            else
                lblMessage.Text = string.Empty;
        }
       
        
    }
}
