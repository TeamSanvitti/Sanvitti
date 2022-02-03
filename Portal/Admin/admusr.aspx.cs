using System;
using System.Data;
using System.Web.UI.WebControls;
using avii.Classes;
namespace avii.Admin
{
	/// <summary>
	/// Summary description for admusr.
	/// </summary>
	public class admusr : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.DataGrid dgUsr;
	
		private Int32 iEditIndex;
		private DataTable dtType;
		private avii.Classes.clsAdmUser oUser = new clsAdmUser();
		private void Page_Load(object sender, System.EventArgs e)
		{
			if (!this.IsPostBack)
			{
				datagridBind(1);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion

		private void datagridBind(Int32 iRowAdd)
		{ 
			if (ViewState["Type"] == null) 
				dtType = oUser.GetUsers();
			else
				dtType = (DataTable)ViewState["Type"];
			AddTableRows(iRowAdd);
			ViewState["Type"] = dtType;
			dgUsr.DataSource = dtType;
			dgUsr.DataBind();
		}

		private void AddTableRows(Int32 iRowAdd)
		{
			if (ViewState["Type"] == null)
                dtType = oUser.GetUsers();
			else
				dtType = (DataTable)ViewState["Type"];
			if (iRowAdd > 0)
			{
				DataRow dRow;
				dRow = dtType.NewRow();
				dtType.Rows.InsertAt(dRow,0);
				
			}
		}

		protected void dg_ItemCreated( System.Object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{

			System.Web.UI.WebControls.LinkButton lb;
			if (dgUsr.CurrentPageIndex ==0) 
			{
				if (e.Item.ItemIndex >= 0)
				{
					lb = (LinkButton)e.Item.Cells[0].Controls[0];
					if (e.Item.ItemIndex == 0)
					{
					
						if (lb.Text == "Edit")
							lb.Text = "Add";
						else if (lb.Text == "Update")
							lb.Text = "Insert";
					}
				}
			}

		}

		protected void dg_Cancel( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			dgUsr.EditItemIndex = -1;
			datagridBind(0);
		}

		protected void dg_Edit( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs  e)
		{
			iEditIndex = e.Item.ItemIndex;
			ViewState["EditIndex"] = iEditIndex;
			dgUsr.EditItemIndex = iEditIndex;
			datagridBind(0);
			
		}

		protected void dg_Update( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string username, accStatus,email, pwd, companyId, accountNumber, userid ;
			iEditIndex = (Int32)ViewState["EditIndex"];
            username = ((TextBox)e.Item.Cells[2].Controls[1]).Text;
            pwd = ((System.Web.UI.HtmlControls.HtmlInputText)e.Item.Cells[3].Controls[1]).Value;
            email = ((TextBox)e.Item.Cells[4].Controls[1]).Text;
            companyId = ((DropDownList)e.Item.Cells[5].Controls[1]).SelectedValue;
            accountNumber = ((TextBox)e.Item.Cells[6].Controls[1]).Text;
            accStatus = ((DropDownList)e.Item.Cells[7].Controls[1]).SelectedValue;
            userid = (e.Item.Cells[8].Text == "&nbsp;" || e.Item.Cells[8].Text == ""?"0":e.Item.Cells[8].Text);
            if (iEditIndex >= 0)
            {
                avii.Classes.clsUser.InsertNewUser(username, pwd, email, accountNumber, Convert.ToInt32(accStatus), Convert.ToInt32(companyId), Convert.ToInt32(userid));
                ViewState["Type"] = dtType;
                dgUsr.EditItemIndex = -1;
                datagridBind(1);
            }
		}
	
		protected void dg_ItemCommand( System.Object sender, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			if (e.CommandName == "delete" && e.Item.ItemIndex > 0)
			{
                if (e.Item.Cells[4].Text != "&nbsp;")
                {
                    int userID = 0;
                    if (int.TryParse(e.Item.Cells[8].Text.Trim(), out userID))
                    {
                        avii.Classes.clsUser.DeleteUser(userID);
                    }
                }
				ViewState["Type"] = null;
				datagridBind(1);
			}
		}

        private DataTable GetCompanyList(int companyID)
        {
            DataTable dataTable = Session["companylist"] as DataTable;
            if (!(dataTable != null && dataTable.Rows.Count > 0))
            {
                dataTable = clsCompany.GetCompany(companyID, 0);
            }

            return dataTable;
        }

		protected void dg_ItemBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                LinkButton btn = e.Item.Cells[1].Controls[0] as LinkButton;
                if (btn != null && btn is LinkButton)
                {
                    btn.Attributes.Add("onclick", "return confirm('are you sure you want to delete this')");
                }
            }
            if (e.Item.ItemType == ListItemType.EditItem)
            {

                DropDownList dp = e.Item.Cells[5].Controls[1] as DropDownList;
                if (dp != null)
                {
                    dp.DataSource = GetCompanyList(0);
                    dp.DataTextField = "CompanyName";
                    dp.DataValueField = "CompanyID";
                    dp.DataBind();
                } 
                
                DataTable dataTable = ViewState["Type"] as DataTable;
                if (dataTable != null && dataTable.Rows.Count> 0)
                {
                    int rec = (dgUsr.CurrentPageIndex * dgUsr.PageSize) + dgUsr.EditItemIndex;
                    DataRow dRow = dataTable.Rows[rec];
                    if (dRow != null)
                    {
                        dp = e.Item.Cells[7].FindControl("dpStatus") as DropDownList;
                        if (dp != null)
                        {
                            dp.SelectedValue = dRow["AccountStatusID"].ToString();
                        }

                        dp = e.Item.Cells[5].FindControl("dpCompany") as DropDownList;
                        if (dp != null)
                        {
                            dp.SelectedValue = dRow["CompanyID"].ToString();
                        }
                    }
                }

            }
		}

	}
}
