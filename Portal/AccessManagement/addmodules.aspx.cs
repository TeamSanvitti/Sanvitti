using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace avii.AccessManagement
{
    public partial class addmodules : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["adm"] == null)
            {
                string url = "/avii/logon.aspx";
                try
                {
                    url = System.Configuration.ConfigurationSettings.AppSettings["LogonPage"].ToString();

                }
                catch
                {
                    url = "/avii/logon.aspx";
                }
                //if (Session["UserID"] == null)
                {
                    Response.Redirect(url);
                }
            }
            if (!IsPostBack)
            {
                
                //Retrieving data in case od Edit
                if (Request["Moduleid"] != null)
                {
                    int moduleGUID = Convert.ToInt32(Request["Moduleid"]);
                    ViewState["moduleguid"] = moduleGUID;
                    //hdnModuleGuid.Value = Request["Moduleid"];
                    avii.Classes.ModuleUtility objModule = new avii.Classes.ModuleUtility();
                    List<avii.Classes.Module> listModule = objModule.getModuleList(moduleGUID, -1, "", "", false, "");
                    
                    txtModuleName.Text = listModule[0].ModuleName;
                    txtTile.Text = listModule[0].Title;
                    txtUrl.Text = listModule[0].Url;
                    ddlType.SelectedValue = listModule[0].UserType;
                    
                    bindParentModule();
                    ViewState["ModuleParentGUID"] = listModule[0].ModuleParentGUID.ToString();
                    ddlparentModule.SelectedValue = listModule[0].ModuleParentGUID.ToString();
                    chkActive.Checked = listModule[0].Active;
                    chkitem.Checked = listModule[0].IsItem;

                    

                    btn_cancel.Text = "Back to search";
                }
                else
                {
                    bindParentModule();
                    btn_cancel.Text = "Cancel";
                    //lbtn_cancel.Visible = false;
                    //btn_cancel.Visible = true;
                }
                
            }
        }
        protected void bindParentModule()
        {
            avii.Classes.ModuleUtility objModule = new avii.Classes.ModuleUtility();
            ddlparentModule.DataSource = objModule.getModuleTree(-1, true, -1, -1, true, ddlType.SelectedValue, false, "");
            ddlparentModule.DataTextField = "title";
            ddlparentModule.DataValueField = "ModuleGUID";
            ddlparentModule.DataBind();
            ListItem lst = new ListItem("", "0");
            ddlparentModule.Items.Insert(0, lst);
        }

        private void ClearForm()
        {
            txtModuleName.Text = string.Empty;
            txtTile.Text = string.Empty;
            txtUrl.Text = string.Empty;
            chkActive.Checked = false;
            ddlType.SelectedIndex = 0;
            ddlparentModule.SelectedIndex = 0;
            chkitem.Checked = false;
            hdnCancel.Value = string.Empty;
            lblMsg.Text = string.Empty;
        }
        protected void btn_cancel_Click(object sender, EventArgs e)
        {
            if (ViewState["moduleguid"] == null)
            {
                bindParentModule();
                ClearForm();
                

                //Intendation in Parent Module DropdownList
                ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>parentDropdown()</script>", false);
            }
            else
                Response.Redirect("managemodule.aspx?search=1", false);
        }
        
      

        protected void txtModuleName_TextChanged(object sender, EventArgs e)
        {

            if (hdnCancel.Value != "1")
            {
                avii.Classes.ModuleUtility objModule = new avii.Classes.ModuleUtility();
                List<avii.Classes.Module> objModulelist = objModule.getModuleList(-1, -1, txtModuleName.Text, ddlType.SelectedValue, false,"");
                if (objModulelist.Count > 0)
                {
                    lblMsg.Text = "Module already exist";
                   
                }
                else
                {
                    lblMsg.Text = string.Empty;
                }

                SetFocus(txtTile);
             

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>parentDropdown()</script>", false);


        }

       
        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            bindParentModule();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>parentDropdown()</script>", false);
        }

        protected void btnaddModule_Click(object sender, EventArgs e)
        {
            int returnValue = 0;
            int moduleGUID = 0;
            avii.Classes.ModuleUtility objModule = new avii.Classes.ModuleUtility();
            if (ViewState["moduleguid"] != null)
                moduleGUID = Convert.ToInt32(ViewState["moduleguid"]);

            returnValue = objModule.createModules(moduleGUID, txtModuleName.Text, txtTile.Text, txtUrl.Text, chkActive.Checked, Convert.ToInt32(ddlparentModule.SelectedValue), chkitem.Checked, ddlType.SelectedValue);
            if (returnValue > 0)
            {
                lblMsg.Text = "Module already exist";
                return;
            }
            if (moduleGUID > 0)
                lblMsg.Text = "Record Updated Successfuly";
            else
            {
                ClearForm();
                lblMsg.Text = "Record Added Successfuly";
            }
            //bindParentModule();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "temp", "<script language='javascript'>parentDropdown()</script>", false);

            if (ViewState["ModuleParentGUID"] != null)
            {
                ddlparentModule.SelectedValue = Convert.ToString(ViewState["ModuleParentGUID"]);
            }
        }

        
      


    }

}

