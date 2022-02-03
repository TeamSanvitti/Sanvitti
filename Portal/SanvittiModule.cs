using System;
using System.Web;

namespace avii
{
    public class SanvittiModule : IHttpModule
    {
        /// <summary>
        /// You will need to configure this module in the Web.config file of your
        /// web and register it with IIS before being able to use it. For more information
        /// see the following link: https://go.microsoft.com/?linkid=8101007
        /// </summary>
        #region IHttpModule Members

        public void Dispose()
        {
            //clean-up code here.
        }

        public void Init(HttpApplication context)
        {
            // Below is an example of how you can handle LogRequest event and provide 
            // custom logging implementation for it
            //context.= new EventHandler(context_AuthorizeRequest);
            context.PreRequestHandlerExecute += context_PreRequestHandlerExecute;
            //context.BeginRequest += new EventHandler(begin_request);
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            HttpApplication app = sender as HttpApplication;
            avii.Error.CustomError customError = new Error.CustomError();

            if (app != null)
            {
                string url = HttpContext.Current.Request.Url.AbsolutePath.ToLower();
                string userMenu = string.Empty;
                if (HttpContext.Current.Session != null && HttpContext.Current.Session["allmenus"] != null)
                {
                    userMenu = Convert.ToString(HttpContext.Current.Session["allmenus"]).ToLower();
                    if(userMenu.Contains(url) || url.Contains("/error/customerror.aspx"))
                    {
                       // if (url.Contains("/error/customerror.aspx"))
                         //   HttpContext.Current.Session["IsAuth"] = "fail";

                    }
                    else
                    {
                        //IsAuth = true;
                        HttpContext.Current.Session["IsAuth"] = "fail";
                        HttpContext.Current.Response.Redirect("~/error/customerror.aspx");
                        
                    }

                }
                //Page page = app.Context.CurrentHandler as Page;
                //if (page != null) {

                //    if (HttpContext.Current.Request.Url.AbsoluteUri.Contains("dashboard.aspx")) { HttpContext.Current.Response.Redirect("~/error/CustomError.aspx"); }

//                }
            }
        }
        //public bool IsAuth { get; set; }
        public void begin_request(object sender, EventArgs e)
        {
            HttpContext Context = ((HttpApplication)sender).Context;
            string url = Context.Request.Url.AbsolutePath;


            if (url.Contains("Home"))
            {
                url = url.Replace("Home", "NewHome");

            }
            //Context.Response.Redirect(url);
            Context.Server.Transfer(url);
        }


        #endregion
        public void context_AuthorizeRequest(object sender, EventArgs e)
        {
            //We change uri for invoking correct handler
            HttpContext context = ((HttpApplication)sender).Context;
            string urlpath = context.Request.Path;
            if (context.Request.RawUrl.Contains(".aspx"))
            {
                string url = context.Request.RawUrl.Replace(".bspx", ".aspx");
                context.Server.Transfer(url);
            }
        }

        public void OnLogRequest(Object source, EventArgs e)
        {
            //custom logging logic can go here
        }
    }
}
