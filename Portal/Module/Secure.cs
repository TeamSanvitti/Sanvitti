using System;
using System.Web;

namespace avii.Module
{
    public class Secure : IHttpModule
    {
        public Secure()
        {
        }

        public void Dispose()
        {
        }

        public void Init(HttpApplication application)
        {
            application.BeginRequest += (new EventHandler(this.Application_BeginRequest));
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            //HttpApplication httpApp;
            //HttpContext httpContext;
            //try
            //{
            //    httpApp = (HttpApplication)source;
            //    httpContext = httpApp.Context;
            //    string str = httpContext.Request.RawUrl;
            //    //XDocument xmlDoc = XDocument.Load(httpContext.Server.MapPath("Secure.xml"));
            //    //var q = from c in xmlDoc.Descendants("Page")
            //    //        where c.Attribute("URL").Value.Trim().ToLower().Equals(str.Trim().ToLower())
            //    //        select (string)c.Element("Secure");
            //    //foreach (string name in q)
            //    //{
            //    //    if (Convert.ToBoolean(name))
            //    //    {
            //    //        httpContext.Response.Redirect(httpContext.Request.Url.ToString().Replace("http", "https"));
            //    //    }
            //    //    else
            //    //    {
            //    if (httpContext.Request.Url.ToString().Contains("https"))
            //    {
            //        httpContext.Response.Redirect(httpContext.Request.Url.ToString());
            //    }
            //    else if (httpContext.Request.Url.ToString().Contains("http"))
            //    {
            //        httpContext.Response.Redirect(httpContext.Request.Url.ToString().Replace("http", "https"));
            //    }
            //    else if (httpContext.Request.Url.ToString().IndexOf("https") < 0 || httpContext.Request.Url.ToString().IndexOf("http") < 0)
            //    {
            //        httpContext.Response.Redirect("https://" + httpContext.Request.Url.ToString());
            //    }
            //}

            //catch (Exception ex)
            //{
                
            //}
        }
    }
}