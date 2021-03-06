using System.ComponentModel;
using System.Web.Services;

namespace avii
{
	/// <summary>
	/// Summary description for AerovoiceContent.
	/// </summary>
	public class AerovoiceContent : System.Web.Services.WebService
	{
		public AerovoiceContent()
		{
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if(disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion

		// WEB SERVICE EXAMPLE
		// The HelloWorld() example service returns the string Hello World
		// To build, uncomment the following lines then save and build the project
		// To test this web service, press F5

		[WebMethod]
		public string GetPhoneTypes()
		{
			Classes.clsPType oType = new Classes.clsPType();
			return oType.GetPhoneTypes().GetXml();
		}

		[WebMethod]
		public string GetPhones(string sTypes)
		{
			return Classes.clsItem.GetPhones(null,null,null,null,null,null).GetXml();
		}
	}
}
