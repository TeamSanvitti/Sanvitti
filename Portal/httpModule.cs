using System;
using System.Web;
//using System.Security.Principal;
//using System.Diagnostics;

namespace avii
{
	/// <summary>
	/// Summary description for httpModule.
	/// </summary>
	public class httpModule: IHttpModule
	{
		private bool bAdm = false,bTrans = false;
		public httpModule()
		{
		}

	private HttpApplication httpApp;
	private string sadm =  string.Empty, sCust = string.Empty;
		HttpApplication application;
		HttpRequest req;
		HttpResponse response;
	public void Init(HttpApplication httpApp)
	{
		this.httpApp = httpApp;
		httpApp.BeginRequest += new EventHandler(onBegReq);
		HttpApplication application = (HttpApplication)httpApp;
		req = application.Context.Request;
//		req = application.Context.Request;
        if (req.RawUrl.ToLower().IndexOf("logon") > 0 && req.RawUrl.ToLower().IndexOf("content") < 0)
		{
			bTrans = false;
		}
		else
		{
			httpApp.AcquireRequestState += new EventHandler(onAcq);
			httpApp.AuthenticateRequest += new EventHandler(OnAuthentication);
			httpApp.EndRequest += new EventHandler(onEndReq);

		}

	}

		void onBegReq(object sender, EventArgs a)
		{
			HttpApplication application = (HttpApplication)sender;
			req = application.Context.Request;
			response = application.Context.Response;
			if (req.RawUrl.ToLower().IndexOf("admin")>0)
				bAdm = true;
			else
				bAdm = false;

		}

		void onEndReq(object sender, EventArgs a)
		{
			if (bTrans == false && req.RawUrl.ToLower().IndexOf("logon")<0 && bAdm == true && req.RawUrl.ToLower().IndexOf("content") < 0)
			{
                response.Redirect(@"/logon.aspx?usr=1", true);
			}

		}
		void onAcq(object sender, EventArgs a)
		{
            try
            {
                if (req.RawUrl.ToLower().IndexOf(".asmx") < 0)
                {
                    HttpApplication application = (HttpApplication)sender;
                    if (application.Context.Session != null && application.Session["adm"] != null)
                    {
                        sadm = (application.Session["adm"] != null ? application.Session["adm"].ToString().Trim() : string.Empty);
                        if (sadm.Length == 0 && bAdm == true)
                            bTrans = false;
                        else
                            bTrans = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("This is from onAcq:" + ex.Message);
            }
		}
	void OnAuthentication(object sender, EventArgs a)
	{
//		application = (HttpApplication)sender;
//		req = application.Context.Request;
//		if (req.RawUrl.ToLower().IndexOf("admin")>0)
//			bAdm = true;
			
	}

	public void Dispose()
	{}
	}
}



