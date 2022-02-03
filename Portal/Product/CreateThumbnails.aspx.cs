using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace avii.product
{
    public partial class CreateThumbnails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(Server.MapPath(Request.QueryString["file"]));
            int height = Convert.ToInt32(Request.QueryString["h"]);
            int width=Convert.ToInt32(Request.QueryString["w"]);
            System.Drawing.Image thumbnailImage = image.GetThumbnailImage(height, width, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero);
            Response.ContentType = "image/jpeg";
            thumbnailImage.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
        public bool ThumbnailCallback()
        {
            return true;
        }
    }
}
