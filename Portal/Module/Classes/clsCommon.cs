using System;

namespace avii.Classes
{
	/// <summary>
	/// Summary description for clsCommon.
	/// </summary>
	public abstract class clsCommon
	{
		public const int ThumbnailWidth = 94;
		public const int ThumbnailHeight = 107;

		public const int OtherThumbnailWidth = 94;
		public const int OtherThumbnailHeight = 67;

		public static string PicType = "I"; // I = Item, S = Service Provider, M = Manufacturer
		public clsCommon()
		{
		}

		public  static int fnUploadFile(System.Web.UI.HtmlControls.HtmlInputFile fileCompImage, string strDir, string strSavedFileName)
		{
			try
			{
				if (fileCompImage.PostedFile.FileName.Trim().Length > 0) //Checking for valid file
				{		
					int IntFileSize =fileCompImage.PostedFile.ContentLength;
					if (IntFileSize <=0 || IntFileSize>950000)
						throw new Exception ("File size is greater than required size.");
					else
					{
						if (fileCompImage.PostedFile.ContentType.ToUpper() == "IMAGE/GIF" || fileCompImage.PostedFile.ContentType.ToUpper() == "IMAGE/PJPEG") 
						{
							fileCompImage.PostedFile.SaveAs(@strDir + strSavedFileName);
//							using(System.Drawing.Image image = System.Drawing.Image.FromStream(fileCompImage.PostedFile.InputStream))
//								if (PicType == "I")
//								{
//									using(Bitmap bitmap = new Bitmap(image, ThumbnailWidth, ThumbnailHeight))
//									{
//										bitmap.Save(@strDir + strSavedFileName, image.RawFormat);
//									}
//								}
//								else
//								{
//									using(Bitmap bitmap = new Bitmap(image, OtherThumbnailWidth, OtherThumbnailHeight))
//									{
//										bitmap.Save(@strDir + strSavedFileName, image.RawFormat);
//									}
//								}

							return 0;
						}
						else
							throw new Exception ("Only GIF or JPEG files are accepted.");
					}
				}
				else
					return -1;
				//					throw new Exception ("Can not upload the file because of system error, please try again.");
			}
			catch (Exception objExp)
			{
				throw new Exception (objExp.Message );
			}
		}
	
		public  static int fnUploadDocument(System.Web.UI.HtmlControls.HtmlInputFile fileCompImage, string strDir, string strSavedFileName)
		{
			try
			{
				if (fileCompImage.PostedFile.FileName.Trim().Length > 0) //Checking for valid file
				{		
//					int IntFileSize =fileCompImage.PostedFile.ContentLength;
//					if (IntFileSize <=0 || IntFileSize>950000)
//						throw new Exception ("File size is greater than required size.");
//					else
//					{
						fileCompImage.PostedFile.SaveAs(@strDir + strSavedFileName);
						return 0;
//					}
				}
				else
					return -1;
				//					throw new Exception ("Can not upload the file because of system error, please try again.");
			}
			catch (Exception objExp)
			{
				throw new Exception (objExp.Message );
			}
		}
	
	
	
	
	
	
	
	
	
	}
}
